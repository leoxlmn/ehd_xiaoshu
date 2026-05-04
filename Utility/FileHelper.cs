using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Utility {
    public static class FileHelper {

        /// <summary>
        /// Check if the given file path exists already.
        /// If no, return the same file path.
        /// If yes, create and return a new file name with xxx (N).xxx
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetNextNewFileName(string filePath) {

            if (!File.Exists(filePath)) return filePath;

            string fileExt = Path.GetExtension(filePath);

            string fileNameBase = filePath.Substring(0, filePath.Length - fileExt.Length);

            int i = 1;
            string nextNewFileName = string.Format("{0}{1}{2}", 
                fileNameBase,
                $" ({i++})",
                fileExt
                );

            while(File.Exists(nextNewFileName)) {
                nextNewFileName = string.Format("{0}{1}{2}",
                    fileNameBase,
                    $" ({i++})",
                    fileExt
                    );
            }

            return nextNewFileName;
        }

        /// <summary>
        /// Save a file from byte[] to a temp folder, then display this file.
        /// NOTE: The given temp folder will be cleared before the given file to be displayed.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileData"></param>
        /// <param name="tempFolderName"></param>
        public static void DisplayFile(string fileName, byte[] fileData, string tempFolderName) {

            //Arguments checking
            if(fileData == null || fileData.Length <= 0 || 
                tempFolderName == null || tempFolderName.Trim().Length <= 0 ||
                fileName == null && fileName.Trim().Length <= 0) {
                throw new ArgumentException("All arguments cannot be empty!");
            }

            //Create temp folder path
            string _tmpFolder = Path.Combine(Path.GetTempPath(), tempFolderName);

            //Clear this temp folder first
            if (Directory.Exists(_tmpFolder)) Directory.Delete(_tmpFolder, true);

            //Create this temp folder
            Directory.CreateDirectory(_tmpFolder);

            //Generate the file to the temp folder
            string _tmpFilePath = Path.Combine(_tmpFolder, fileName);

            File.WriteAllBytes(_tmpFilePath, fileData);

            //Open the temp file
            ProcessStartInfo proc = new ProcessStartInfo(_tmpFilePath);
            Process p = Process.Start(proc);
        }

        public static void ExportExcel(GridView gv, string exportFileName, string reportTitle = null, string extraHtmlUnderTitle = null, bool forceText = true) {

            //Determine the number of columns
            int colCount = 10;
            if (gv != null && gv.Rows.Count > 0) {
                colCount = gv.Rows[0].Cells.Count;
            }

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv.RenderControl(htw);

            //only output the outer <table>
            string html = sw.ToString();
            sw.Close();

            int indStart = html.IndexOf("<table");
            int indEnd = html.LastIndexOf("</table>") + 8;
            if (indStart >= 0 && indEnd > indStart) {
                html = html.Substring(indStart, indEnd - indStart);
            } else {
                html = "No records found.";
            }

            //
            StringBuilder sb = new StringBuilder();

            sb.Append(@"<style>");
            sb.Append(@" br {mso-data-placement:same-cell;} "); //Prevent from creating extra rows for <br/>
            if (forceText) {
                sb.Append(@" TD { mso-number-format:\@; } "); //Force all columns to be formatted as Text
            }
            sb.Append(@"</style>");

            if (reportTitle != null && reportTitle.Trim().Length > 0) {
                sb.Append("<table><tr><td style=\"font-weight:bold; font-size:150%; text-align:center; \" colspan=\"" + colCount.ToString() + "\">" + reportTitle + "</td></tr></table>");
            }

            if(extraHtmlUnderTitle != null && extraHtmlUnderTitle.Trim().Length > 0) {
                sb.Append(extraHtmlUnderTitle);
            }

            sb.Append(html.Replace("&lt;br/&gt;", "<br/>").Replace("\x0D", "<br/>"));

            //Save to file
            using (StreamWriter writer = new StreamWriter(exportFileName)) {
                writer.Write(sb.ToString());
            }

            //Open this report file
            ProcessStartInfo proc = new ProcessStartInfo(exportFileName);
            Process.Start(proc);
        }

    }
}
