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
        // list of files contains all files found in directory
        private List<File> Files = new List<File>();
        // list of files contains all duplicate files found
        private BindingList<File> duplicateFiles = new BindingList<File>();

        public MainWindow()
        {
            InitializeComponent();
        }

        // opens directory explorer
        private void btnDirectory_Click(object sender, RoutedEventArgs e)
        {
            // opens directory window
            WinForms.FolderBrowserDialog folderDialog = new WinForms.FolderBrowserDialog
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
            // clear from previus searches
            duplicateFiles.Clear();
           
            // get all files from all sub folders
            GetAllFiles();
            
            // scan found files for duplicates
            CompareFiles();

            // clear files list
            Files.Clear();

            // if no duplicate files found
            if (duplicateFiles.Count == 0)
            {
                File.mainDirectory = "";
                duplicateFiles.Add(new File("_No duplicate files found!",1));
            }// end if

            // data bind list of duplicates to lisbox
            lsbFiles.ItemsSource = duplicateFiles;
        }// end btnScan

        // get all files from current and all subfolders
        private void GetAllFiles()
        {

            File.mainDirectory = txbDirectory.Text;

            // contain all file paths
            List<string> files = new List<string>();
            // contain all folders
            List<string> folders = new List<string>();

            // get all subfolder of current directory
            folders.Add(File.mainDirectory);

            // for each foler get all sub folder and add to folders
            for (int i = 0; i < folders.Count; i++)
            {
                // get all subfolders
                folders.AddRange(Directory.GetDirectories(folders[i]));
                // get all files
                files.AddRange(Directory.GetFiles(folders[i]));
                
            }
            // add all files to List<Files>
            foreach (var file in files)
            {
                Files.Add(new File(file));
            }
        }// end GetFiles()

        // search for duplicate files
        private void CompareFiles()
        {
            // for every file
            for (int i = 0; i < Files.Count; i++)
            {
                // search for duplicates
                for (int j = i + 1; j < Files.Count; j++)
                {
                    // if file not compared
                    if (!duplicateFiles.Contains(Files[j]) && !duplicateFiles.Contains(Files[i]))
                    {
                        // if files are equal
                        if (File.CompareFiles(Files[i], Files[j]))
                        {
                            // add to list of duplicates
                            duplicateFiles.Add(Files[i]);
                            duplicateFiles.Add(Files[j]);
                        }// end if
                    }// end if

                }// end for
            }// end for
        }// end Compare Files



        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // if aggree to delete
            if (!(System.Windows.MessageBox.Show("Delete selected files?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No))
            {
                // get all selected items
                var selected = lsbFiles.SelectedItems;

                List<string> selectedFiles = new List<string>();

                // for each selected item
                foreach (var x in selected)
                {
                    selectedFiles.Add(File.mainDirectory + "\\" + x.ToString());
                }// end foreach

                // delete each file
                foreach (var file in selectedFiles)
                {
                    System.IO.File.Delete(file);
                }// end foreach

                // clear from previus searches
                duplicateFiles.Clear();

                // get all files from all sub folders
                GetAllFiles();

                // scan found files for duplicates
                CompareFiles();

                // clear files list
                Files.Clear();

                // if no duplicate files found
                if (duplicateFiles.Count == 0)
                {
                    File.mainDirectory = "";
                    duplicateFiles.Add(new File("_No duplicate files found!", 1));
                }// end if

                // data bind list of duplicates to lisbox
                lsbFiles.ItemsSource = duplicateFiles;

            }// end if

        }
    }
}
