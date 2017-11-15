using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duplicate_File_Scanner
{
    class File 
    {
        // contains file path
       string path;
        // contains file size
       Int64 fileSize;
        // string contains main directory set by the user
        public static string mainDirectory = "";

        // constructor
        public File(string path, Int64 fileSize)
        {
            Path = path;
            FileSize = FileSize;
        }


        // constructor
        public File(string path) : this(path, 0)
        {
            GetFileSize();
        }

        // set and get for path
        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                this.path = value;
            }
        }
        public long FileSize { get => fileSize; set => fileSize = value; }


        // get file size
        private void GetFileSize()
        {
            if (FileSize == 0)
            {
                this.fileSize = new System.IO.FileInfo(this.path).Length;
            }
        }

        // Compare two files 
        public static bool CompareFiles(File file1,File file2)
        {
            bool equal = false;
            // if size is equal and have different names
            if ((file1.FileSize == file2.FileSize) && (file1.Path != file2.Path))
            {
                // open both files
                using (FileStream f1 = System.IO.File.OpenRead(file1.Path))
                using (FileStream f2 = System.IO.File.OpenRead(file2.Path))
                {
                    equal = true;
                    // read byte by byte
                    for (int i = 0; i < file1.FileSize; i++)
                    { 
                        // if 2 bytes are different 
                        if (f1.ReadByte() != f2.ReadByte())
                        { 
                            equal = false;
                            break;
                        }
                    }
                }

        
            }

            return equal;
        }

        // return file path without main dirrectory
        public override string ToString()
        {
            return Path.Remove(0,mainDirectory.Length + 1);
        }
    }
}
