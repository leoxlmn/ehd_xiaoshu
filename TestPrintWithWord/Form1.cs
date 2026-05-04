using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WordFileProcessor;
using Utility;
using FilePathExtender;
using WORD = Microsoft.Office.Interop.Word;
using System.Reflection;
using WordTools = Microsoft.Office.Tools.Word;

namespace TestPrintWithWord {
    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            object missing = Missing.Value;
            object doNotSaveChanges = WORD.WdSaveOptions.wdDoNotSaveChanges;
            WORD._Application wordApp = null;
            WORD._Document wordDoc = null;

            try {
                //Get app root path
                string AppRootPath;
                AppRootPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                AppRootPath = AppRootPath.Substring(0, AppRootPath.LastIndexOf('\\'));

                //Generate word file from template
                ProcessRequest request = new ProcessRequest(@"c:\Test\testWord.docx",
                    FileExtender.ConcatPhysicalPath(AppRootPath, @"template\PhysiotherapyTemplate.docx"),
                    true);

                //Body Diagram
                List<PictureReplacement> bodyDiagram = new List<PictureReplacement>();
                bodyDiagram.Add(new PictureReplacement(
                    FileExtender.ConcatPhysicalPath(AppRootPath, @"images\Body_chart.jpg")));

                request.Replacements.Add(@"{BodyDiagram}", bodyDiagram);


                //word test
                List<WordReplacement> words = new List<WordReplacement>();
                words.Add(new WordReplacement("This is a test"));
                request.Replacements.Add(@"{HistoryOfPresentingComplaint}",
                    words);

                RequestProcessor processor = new RequestProcessor();
                ProcessResponse response = processor.BuildWordFromTemplate(request);

                if(response.Succeeded) {
                    //Print out
                    object wordFile = request.TargetFilePath;
                    wordApp = new WORD.Application();
                    wordDoc = wordApp.Documents.Open(ref wordFile,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing);

                    wordDoc.PrintPreview();
                } else {
                    MessageBox.Show(response.ReturnMessage);
                }
            } catch(Exception ex) {
                MessageBox.Show("Error: " + ex.Message);
            } finally {
                if(wordDoc != null) {
                    wordDoc.Close(ref doNotSaveChanges, ref missing, ref missing);
                    wordDoc = null;
                }
                if(wordApp != null) {
                    wordApp.Quit(ref doNotSaveChanges, ref missing, ref missing);
                    wordApp = null;
                }
            }
        }
    }
}
