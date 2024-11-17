using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutputNUnit
{
    public class PathManager
    {
        public string MakeRelativePath(string path)
            /// <summary>Receive a string and construct relative file path omitting runtime path attributes</summary>
            /// <param name="path">the relative path to use</param>
        {
            string relPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", path);
            return relPath;
        }
    }
}
