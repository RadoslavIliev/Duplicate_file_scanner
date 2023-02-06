using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duplicate_File_Scanner
{
    public interface IFileScanner
    {
        Dictionary<int, List<string>> GroupDuplicateFiles(string[] filePaths);

        List<DuplicateFile> GetDuplicateFilesInDir(string path);
    }
}
