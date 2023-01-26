using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinForms = System.Windows.Forms;



namespace Duplicate_File_Scanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // list of files contains all duplicate files found
        private List<DuplicateFile> duplicateFiles = new List<DuplicateFile>();
        IFileScanner fileScanner = new FileScanner();

        public MainWindow()
        {
            InitializeComponent();
        }

        // opens directory explorer
        private void btnDirectory_Click(object sender, RoutedEventArgs e)
        {
            // opens directory window
            FolderBrowserDialog folderDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            var dialogResult = folderDialog.ShowDialog();
            // set directory to text box
            if (dialogResult == WinForms.DialogResult.OK)
            {
                txbDirectory.Text = folderDialog.SelectedPath;
                btnScan.IsEnabled = true;
            }// end if
           
        }// end btnDirectory



        // scans directory for all files and compares the files
        private void btnScan_Click(object sender, RoutedEventArgs e)
        {
            UpdateView();
        }// end btnScan


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // if aggree to delete
            if (System.Windows.MessageBox.Show("Delete selected files?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var selectedItems = DuplicateListView.SelectedItems.OfType<DuplicateFile>();

                foreach (var file in selectedItems)
                {
                    System.IO.File.Delete(file.FilePath);
                }

                UpdateView();
            }// end if
        }

        private void UpdateView()
        {
            
            if (duplicateFiles.Any())
            {
                duplicateFiles.Clear();
            }

            duplicateFiles = fileScanner.GetDuplicateFilesInDir(txbDirectory.Text);

            // if no duplicate files found
            if (duplicateFiles.Count == 0)
            {
                duplicateFiles.Add(new DuplicateFile() 
                { 
                    DuplicateGroup = "", FilePath = "_No duplicate files found!"
                });
            }// end if

            // data bind list of duplicates to lisbox
            DuplicateListView.ItemsSource = duplicateFiles;
        }
    }
}
