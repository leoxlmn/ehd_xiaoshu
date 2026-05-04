using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilePathExtender {
    public static class FileExtender {
        public static string ConcatPhysicalPath(string RootPath, params string[] RelativePaths) {
            return ConcatPath('\\', RootPath, RelativePaths);
        }

        public static string ConcatVirtualPath(string RootPath, params string[] RelativePaths) {
            return ConcatPath('/', RootPath, RelativePaths);
        }

        private static string ConcatPath(char pathDelimiter, string rootPath, params string[] relativePaths) {
            StringBuilder sbFullPath = new StringBuilder();
            char[] delimiters = new char[] { pathDelimiter, ' ' };

            sbFullPath.Append(rootPath.Trim().TrimEnd(delimiters));

            foreach(string param in relativePaths) {
                sbFullPath.Append(pathDelimiter);
                sbFullPath.Append(param.Trim().TrimEnd(delimiters));
            }

            return sbFullPath.ToString();
        }
    }
}
