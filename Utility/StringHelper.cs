using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility {
    public static class StringHelper {

        public static string ReplaceLastOccurrence(this string str, string find, string replace) {

            if (find == null || find.Length <= 0) return str;

            int ind = str.LastIndexOf(find);

            if (ind < 0) return str;

            if (replace == null) replace = string.Empty;
            return str.Remove(ind, find.Length).Insert(ind, replace);
        }

        public static string ToHumanReadableString(this string str) {
            StringBuilder sb = new StringBuilder();

            if (str != null) {
                foreach (char c in str.Trim()) {
                    if (sb.Length <= 0) {
                        sb.Append(Char.ToUpper(c));
                    } else {
                        if (Char.IsUpper(c)) sb.Append(' ');
                        sb.Append(c);
                    }
                }
            }

            return sb.ToString();
        }
    }
}
