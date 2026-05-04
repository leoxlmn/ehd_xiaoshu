/*
 * USAGE:
 *          StreamWriter writer = File.CreateText("c:\myexport.csv");
 *          MyObjectCollection.Export(writer);
 *          writer.Flush();
 *          writer.Close();
 *      
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace Utility {
    public static class ObjectExportToFile {
        /// <summary>
        /// NOTE: Remember to close the StreamWriter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="writer"></param>
        /// <param name="objects"></param>
        public static void Export<T>(this IEnumerable<T> objects, StreamWriter writer) {
            //Get all properties of Employee class
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

            //Create CSV header
            StringBuilder sbHeader = new StringBuilder();
            foreach (PropertyDescriptor prop in properties) {
                sbHeader.Append('"');
                sbHeader.Append(prop.Name);
                sbHeader.Append('"');
                sbHeader.Append(',');
            }
            if (sbHeader.Length > 0) {
                sbHeader.Remove(sbHeader.Length - 1, 1);
            }
            writer.WriteLine(sbHeader.ToString());

            //Create CSV rows
            foreach (T obj in objects) {
                if (null == obj) continue;

                StringBuilder sbRow = new StringBuilder();
                foreach (PropertyDescriptor prop in properties) {
                    object oValue = prop.GetValue(obj);
                    if (null != oValue) {
                        sbRow.Append('"');
                        sbRow.Append(oValue.ToString().Replace("\"", "\"\""));
                        sbRow.Append('"');
                    }
                    sbRow.Append(',');
                }
                if (sbRow.Length > 0) {
                    sbRow.Remove(sbRow.Length - 1, 1);
                }
                writer.WriteLine(sbRow.ToString());
            }

        }
    }
}
