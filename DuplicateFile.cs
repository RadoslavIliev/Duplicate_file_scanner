using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Duplicate_File_Scanner
{
    public class DuplicateFile
    {
        public string DuplicateGroup {get;set;}
        public string FilePath { get;set;}
        public int GroupKey { get;set;}
    }
}
