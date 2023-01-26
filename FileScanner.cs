using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duplicate_File_Scanner
{
    public class FileScanner : IFileScanner
    {
        public List<DuplicateFile> GetDuplicateFilesInDir(string path)
        {
            var groupDuplicateFiles = new Dictionary<string, List<string>>();
            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            groupDuplicateFiles = GroupDuplicateFiles(files)
                .Where(g => g.Value.Count() >= 2)
                .ToDictionary(kvp => kvp.Key.Substring(0, 8) + "...", kvp => kvp.Value);

            List<DuplicateFile> duplicateFiles = new List<DuplicateFile>();
            for (int i = 0; i < groupDuplicateFiles.Count; i++)
            {
                var key = groupDuplicateFiles.Keys.ElementAt(i);
                foreach (var file in groupDuplicateFiles[key])
                {
                    duplicateFiles.Add(new DuplicateFile()
                    {
                        GroupKey = i,
                        DuplicateGroup = key,
                        FilePath = file
                    });
                }
            }
            return duplicateFiles;

            //return groupDuplicateFiles.SelectMany(x => x.Value.Select(
            //    v => new DuplicateFile (){ DuplicateGroup = x.Key, FilePath = v }))
            //    .ToList();
        }

        public Dictionary<string, List<string>> GroupDuplicateFiles(string[] filePaths)
        {
            var fileGroups = new Dictionary<string, List<string>>();

            foreach (string filePath in filePaths)
            {
                // Compute the file's hash
                byte[] fileHash;
                using (var stream = System.IO.File.OpenRead(filePath))
                {
                    fileHash = ComputeHash(stream);
                }

                // Convert the hash to a string
                var hashString = BitConverter.ToString(fileHash);

                // Add the file to the appropriate group
                if (fileGroups.ContainsKey(hashString))
                {
                    fileGroups[hashString].Add(filePath);
                }
                else
                {
                    fileGroups[hashString] = new List<string> { filePath };
                }
            }
            return fileGroups;
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
