using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Duplicate_File_Scanner
{
    public class FileScanner : IFileScanner
    {
        public List<DuplicateFile> GetDuplicateFilesInDir(string path)
        {
            var groupDuplicateFiles = new Dictionary<int, List<string>>();
            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            groupDuplicateFiles = GroupDuplicateFiles(files);
            //groupDuplicateFiles = groupDuplicateFiles.Where(g => g.Value.Count() >= 2)
            //    .ToDictionary(kvp => kvp.Key.Substring(0, 8) + "...", kvp => kvp.Value);

            List<DuplicateFile> duplicateFiles = new List<DuplicateFile>();
            for (int i = 0; i < groupDuplicateFiles.Count; i++)
            {
                var key = groupDuplicateFiles.Keys.ElementAt(i);
                foreach (var file in groupDuplicateFiles[key])
                {
                    duplicateFiles.Add(new DuplicateFile()
                    {
                        GroupKey = i,
                        DuplicateGroup = key.ToString(),
                        FilePath = file
                    });
                }
            }
            return duplicateFiles;

            //return groupDuplicateFiles.SelectMany(x => x.Value.Select(
            //    v => new DuplicateFile (){ DuplicateGroup = x.Key, FilePath = v }))
            //    .ToList();
        }

        public Dictionary<int, List<string>> GroupDuplicateFiles(string[] filePaths)
        {
            var fileGroups = new Dictionary<long, List<string>>();

            foreach (string filePath in filePaths)
            {
                var fileInfo = new FileInfo(filePath);
                var fileSize = fileInfo.Length;

                // Group the files by size
                if (fileGroups.ContainsKey(fileSize))
                {
                    fileGroups[fileSize].Add(filePath);
                }
                else
                {
                    fileGroups[fileSize] = new List<string> { filePath };
                }
            }

            var duplicateFiles = new Dictionary<int, List<string>>();
            var counter = 0;
            foreach (var fileGroup in fileGroups.Values)
            {
                if (fileGroup.Count == 1)
                {
                    continue;
                }

                var fileHashes = new Dictionary<string, List<string>>();
                foreach (string filePath in fileGroup)
                {
                    byte[] fileHash;
                    using (var stream = System.IO.File.OpenRead(filePath))
                    {
                        fileHash = ComputeHash(stream);
                    }

                    // Convert the hash to a string
                    var hashString = BitConverter.ToString(fileHash);

                    // Add the file to the appropriate group
                    if (fileHashes.ContainsKey(hashString))
                    {
                        fileHashes[hashString].Add(filePath);
                    }
                    else
                    {
                        fileHashes[hashString] = new List<string> { filePath };
                    }
                }

                foreach (var duplicateFileGroup in fileHashes.Values)
                {
                    if (duplicateFileGroup.Count > 1)
                    {
                        duplicateFiles[counter] = duplicateFileGroup;
                        counter++;
                    }
                }
            }

            return duplicateFiles;
        }



        private static byte[] ComputeHash(Stream stream)
        {
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                return sha.ComputeHash(stream);
            }
        }
    }
}
