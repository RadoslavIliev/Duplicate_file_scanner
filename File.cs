using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duplicate_File_Scanner
{
    public class File 
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
        public static bool CompareFiles(string file1, string file2)
        {
            // Open the two files
            using (var stream1 = System.IO.File.OpenRead(file1))
            using (var stream2 = System.IO.File.OpenRead(file2))
            {
                // Compare the two files byte by byte
                if (stream1.Length != stream2.Length)
                {
                    return false;
                }

                for (int i = 0; i < stream1.Length; i++)
                {
                    if (stream1.ReadByte() != stream2.ReadByte())
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // return file path without main dirrectory
        public override string ToString()
        {
            return Path.Remove(0,mainDirectory.Length + 1);
        }

        public int DuplicateFileGroup { get; set; }
    }
}
