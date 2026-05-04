using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using WORD = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.IO;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Diagnostics;
using log4net;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using System.Drawing;

namespace WordFileProcessor {

    public delegate void ProgressReporterDelegate(string ProgressMessage, double ProgressPercent);

    public class RequestProcessor {

        private ILog logger = null;

        public RequestProcessor(ILog Log) {
            logger = Log;
        }

        /*
        public ProcessResponse BuildWordFromTemplate(ProcessRequest request, ProgressReporterDelegate progressReporter) {

            //debug
            if (logger != null) logger.Info("BuildWordFromTemplate start at: " + DateTime.Now.ToString("hh:mm:ss.fffff"));

            ProcessResponse response = new ProcessResponse();
            response.Succeeded = true;

            //Verify Request object
            if (request.TemplateFilePath.Trim().Length <= 0 ||
                !File.Exists(request.TemplateFilePath)) {
                response.Succeeded = false;
                response.ReturnMessage = "Template file is missing.";
                return response;
            }
            if (request.TargetFilePath.Trim().Length <= 0) {
                response.Succeeded = false;
                response.ReturnMessage = "Please specify the target file path.";
                return response;
            } else if (File.Exists(request.TargetFilePath) && !request.ReplaceTarget) {
                response.Succeeded = false;
                response.ReturnMessage = "The target file, " + request.TargetFilePath + ", already exists!";
                return response;
            }

            //Word variables
            WORD._Application wordApp = null;
            WORD._Document wordDocNew = null;
            object missing = Missing.Value;
            object doNotSaveChanges = WORD.WdSaveOptions.wdDoNotSaveChanges;
            object replaceAll = WORD.WdReplace.wdReplaceAll;
            object gotoLine = WORD.WdGoToItem.wdGoToLine;
            object gotoPage = WORD.WdGoToItem.wdGoToPage;
            object gotoEnd = WORD.WdGoToDirection.wdGoToLast;
            object gotoStart = WORD.WdGoToDirection.wdGoToFirst;
            object gotoAbsolute = WORD.WdGoToDirection.wdGoToAbsolute;
            object pageSectionPageBreak = WORD.WdBreakType.wdSectionBreakNextPage;
            object linkBreak = WORD.WdBreakType.wdLineBreak;
            object textWrappingBreak = WORD.WdBreakType.wdTextWrappingBreak;
            object atPage = @"\page"; //For Bookmarks

            string strWorkingWordPath = string.Empty;

            try {
                wordApp = new WORD.Application();
                wordApp.ScreenUpdating = false;

                //1) Create a temp Word file to work with. Copy it over to the target location when finished.
                strWorkingWordPath = AppDomain.CurrentDomain.BaseDirectory;
                strWorkingWordPath = strWorkingWordPath + Guid.NewGuid().ToString();
                strWorkingWordPath = strWorkingWordPath + new FileInfo(request.TargetFilePath).Extension;

                if (File.Exists(strWorkingWordPath)) {
                    File.Delete(strWorkingWordPath);
                }
                File.Copy(request.TemplateFilePath, strWorkingWordPath);

                //1) Open new target word file
                object oTargetFilePath = strWorkingWordPath;
                wordDocNew = wordApp.Documents.Open(ref oTargetFilePath,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);

                wordDocNew.Select();
                replaceTemplateKeywords(wordDocNew, request, progressReporter);

                //6) Save the new document and close.
                wordDocNew.Save();
                wordDocNew.Close(ref missing, ref missing, ref missing);
                wordDocNew = null;

                //10) Move the new Word document to the target location. 
                //10.1) Copy the new Word document into memory
                byte[] file = File.ReadAllBytes(strWorkingWordPath);
                //10.2) Delete the temporary word file
                File.Delete(strWorkingWordPath);

                //10.3) Write to target file
                try {
                    //Create target subfolder if not existing
                    FileInfo fi = new FileInfo(request.TargetFilePath);
                    if (!fi.Directory.Exists) {
                        Directory.CreateDirectory(fi.DirectoryName);
                    }

                    //Delete the old target file if existing
                    if (request.ReplaceTarget && File.Exists(request.TargetFilePath)) {
                        File.Delete(request.TargetFilePath);
                    }

                    //Write file
                    File.WriteAllBytes(request.TargetFilePath, file);

                } catch(Exception ex) {
                    response.Succeeded = false;
                    response.ReturnMessage = "Failed to copy the Word document to the target location:" + request.TargetFilePath + ". Error:" + ex.Message;
                    if(logger != null) logger.Error("Failed to copy the Word document to the target location.", ex);
                    return response;
                }

            } catch (Exception ex) {
                response.Succeeded = false;
                response.ReturnMessage = string.Format("BuildWordFromTemplate Exception: {0}", ex.Message);

                if(logger != null) {
                    logger.Error("BuildWordFromTemplate Exception.", ex);
                }
            } finally {
                if (wordDocNew != null) {
                    wordDocNew.Close(ref missing, ref missing, ref missing);
                    wordDocNew = null;
                    File.Delete(strWorkingWordPath);
                }
                if (wordApp != null) {
                    wordApp.Quit(ref missing, ref missing, ref missing);
                    wordApp = null;
                }

            }

            //debug
            if(logger != null) logger.Info("BuildWordFromTemplate end at: " + DateTime.Now.ToString("hh:mm:ss.fffff"));

            return response;
        }

        public void PrintWordFile(string filePath) {
            object missing = Missing.Value;
            object doNotSaveChanges = WORD.WdSaveOptions.wdDoNotSaveChanges;
            object wordFile = filePath;
            WORD._Application wordApp = null;
            WORD._Document wordDoc = null;
            try {
                wordApp = new WORD.Application();
                wordDoc = wordApp.Documents.Open(ref wordFile,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing);
                wordDoc.PrintOut();
            } finally {
                if(wordDoc != null) {
                    wordDoc.Close(ref doNotSaveChanges, ref missing, ref missing);
                    wordDoc = null;
                }
                if(wordApp != null) {
                    wordApp.Quit(ref missing, ref missing, ref missing);
                    wordApp = null;
                }
            }
        }
        */

        public void OpenWordFile(string filePath) {
            /*
            ProcessStartInfo proc = new ProcessStartInfo("WINWORD.EXE");
            proc.Arguments = "\"" + filePath + "\"";
            Process.Start(proc);*/

            ProcessStartInfo proc = new ProcessStartInfo(filePath);
            Process.Start(proc);
        }

        /*
        /// <summary>
        /// Merge multiple Word files into one.
        /// </summary>
        /// <param name="addPageBreak">True - to add page breaks.</param>
        /// <param name="removeWordFiles">True - to remove Word files when completed merging.</param>
        /// <param name="wordFiles">The Word files you want to merge.</param>
        public void CombineWordFiles(bool addPageBreak, bool removeWordFiles, params string[] wordFiles) {

            string majorWordFile = string.Empty;

            if(wordFiles.Length < 1) {
                return;
            }else{
                majorWordFile = wordFiles[0].Trim(); //Use the first Word file as major file
                if(!File.Exists(majorWordFile)) {
                    return;
                }
            }

            //Word variables
            WORD._Application wordApp = null;
            WORD._Document wordDocNew = null;
            WORD._Document wordDocOther = null;
            object missing = Missing.Value;
            object doNotSaveChanges = WORD.WdSaveOptions.wdDoNotSaveChanges;
            object replaceAll = WORD.WdReplace.wdReplaceAll;
            object gotoLine = WORD.WdGoToItem.wdGoToLine;
            object gotoPage = WORD.WdGoToItem.wdGoToPage;
            object gotoEnd = WORD.WdGoToDirection.wdGoToLast;
            object gotoStart = WORD.WdGoToDirection.wdGoToFirst;
            object gotoAbsolute = WORD.WdGoToDirection.wdGoToAbsolute;
            object pageSectionPageBreak = WORD.WdBreakType.wdSectionBreakNextPage;
            object linkBreak = WORD.WdBreakType.wdLineBreak;
            object textWrappingBreak = WORD.WdBreakType.wdTextWrappingBreak;
            object atPage = @"\page"; //For Bookmarks

            string strWorkingWordPath = string.Empty;

            try {
                wordApp = new WORD.Application();

                //1) Create a temp Word file to work with. Copy it over to the target location when finished.
                strWorkingWordPath = AppDomain.CurrentDomain.BaseDirectory;
                strWorkingWordPath = strWorkingWordPath + Guid.NewGuid().ToString();
                strWorkingWordPath = strWorkingWordPath + new FileInfo(majorWordFile).Extension;

                if(File.Exists(strWorkingWordPath)) {
                    File.Delete(strWorkingWordPath);
                }
                File.Copy(majorWordFile, strWorkingWordPath);

                //1) Open new target word file
                object oTargetFilePath = strWorkingWordPath;
                wordDocNew = wordApp.Documents.Open(ref oTargetFilePath,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);


                //2) Go through all other Word files
                for(int i = 1; i < wordFiles.Length; i++) {
                    string otherWordFile = wordFiles[i].Trim();
                    if(File.Exists(otherWordFile)) {
                        object oTheOtherDoc = otherWordFile;
                        wordDocOther = wordApp.Documents.Open(ref oTheOtherDoc,
                            ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);

                        //Add page break is specified
                        if(addPageBreak) {
                            wordDocNew.ActiveWindow.Selection.GoTo(ref gotoLine, ref gotoEnd, ref missing, ref missing);
                            wordDocNew.ActiveWindow.Selection.InsertBreak(ref pageSectionPageBreak);
                        }

                        //Select the whole text content of the other Word file
                        wordDocOther.ActiveWindow.Selection.WholeStory();
                        wordDocOther.ActiveWindow.Selection.Copy();

                        //Merge text content
                        wordDocNew.ActiveWindow.Selection.GoTo(ref gotoLine, ref gotoEnd, ref missing, ref missing);
                        wordDocNew.ActiveWindow.Selection.PasteAndFormat(WORD.WdRecoveryType.wdFormatOriginalFormatting);

                        //Close the other Word file
                        wordDocOther.Close(ref missing, ref missing, ref missing);
                        wordDocOther = null;

                        //Remove the other Word file if specified
                        if(removeWordFiles) {
                            File.Delete(otherWordFile);
                        }
                    }
                }

                //3) Save the new document and close.
                wordDocNew.Save();
                wordDocNew.Close(ref missing, ref missing, ref missing);
                wordDocNew = null;

                //4) Replace the origial major Word file with the new one
                File.Delete(majorWordFile);
                File.Move(strWorkingWordPath, majorWordFile);

            } catch(Exception ex) {
                throw new ApplicationException("Combine Word Files failed. " + ex.Message);
            } finally {
                if(wordDocNew != null) {
                    wordDocNew.Close(ref missing, ref missing, ref missing);
                    wordDocNew = null;
                }
                if(wordDocOther != null) {
                    wordDocOther.Close(ref missing, ref missing, ref missing);
                    wordDocOther = null;
                }
                if(wordApp != null) {
                    wordApp.Quit(ref missing, ref missing, ref missing);
                    wordApp = null;
                }
            }
        }

        /// <summary>
        /// Merge multiple Word files into one. 
        /// No page breaks to be added. 
        /// The Word files will be removed automatically when completed merging.
        /// </summary>
        /// <param name="wordFiles">The Word files you want to merge.</param>
        public void CombineWordFiles(params string[] wordFiles) {
            CombineWordFiles(false, true, wordFiles);
        }
        */

        /*
        #region Private Methods
        //Search and replace keyword on the given Word document
        private void replaceTemplateKeywords(WORD._Document wordDoc, ProcessRequest request, ProgressReporterDelegate progressReporter) {

            //debug
            if(logger != null) logger.Info("--replaceTemplateKeywords start.");

            //Prepare for progress reporter
            int numOfKeys = request.Replacements.Keys.Count;
            int numOfKeysProcessed = 0;


            //Word parameter objects
            object missing = Missing.Value;
            object doNotSaveChanges = WORD.WdSaveOptions.wdDoNotSaveChanges;
            object replaceAll = WORD.WdReplace.wdReplaceAll;
            object gotoLine = WORD.WdGoToItem.wdGoToLine;
            object gotoPage = WORD.WdGoToItem.wdGoToPage;
            object gotoEnd = WORD.WdGoToDirection.wdGoToLast;
            object gotoStart = WORD.WdGoToDirection.wdGoToFirst;
            object gotoAbsolute = WORD.WdGoToDirection.wdGoToAbsolute;
            object pageSectionPageBreak = WORD.WdBreakType.wdSectionBreakNextPage;
            object linkBreak = WORD.WdBreakType.wdLineBreak;
            object textWrappingBreak = WORD.WdBreakType.wdTextWrappingBreak;
            object atPage = @"\page"; //For Bookmarks

            //Go through all the keywords
            foreach (string key in request.Replacements.Keys) {
                WORD.Range docRng = wordDoc.Content;
                docRng.Find.Forward = false;
                docRng.Find.Text = key;

                //debug
                if(logger != null) logger.Info("----start to search on [" + key + "].");

                docRng.Find.Execute(
                    ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing);

                //debug
                if(logger != null) logger.Info("----search end.");

                //Go through all the ranges where the keyword has been found
                while (docRng.Find.Found) {

                    //debug
                    if(logger != null) logger.Info("------key found, start to replace.");

                    //Backup original format
                    docRng.End = docRng.End - 1;
                    string oriFontName = docRng.Font.Name;
                    float oriFontSize = docRng.Font.Size;
                    WORD.WdUnderline oriUnderline = docRng.Underline;
                    int oriItalic = docRng.Italic;
                    docRng.End = docRng.End + 1;

                    //Remove old text
                    docRng.Text = "";

                    string replaceText = string.Empty;
                    foreach (ReplacementBase replacement in request.Replacements[key]) {

                        if(replacement is WordReplacement) {

                            WordReplacement wordReplacement = replacement as WordReplacement;

                            //Add replacement text with original template format
                            docRng.Text = wordReplacement.Text;
                            docRng.Font.Name = oriFontName;
                            docRng.Font.Size = oriFontSize;
                            docRng.Underline = oriUnderline;
                            docRng.Italic = oriItalic;

                            //Add customized format
                            foreach(TextDecoration dec in wordReplacement.TextDecorations) {
                                switch(dec) {
                                    case TextDecoration.Underline:
                                        docRng.Underline = WORD.WdUnderline.wdUnderlineSingle;
                                        break;
                                    case TextDecoration.Italic:
                                        docRng.Italic = 1;
                                        break;
                                    case TextDecoration.Smaller:
                                        docRng.Font.Size = oriFontSize - 1;
                                        break;
                                    case TextDecoration.Larger:
                                        docRng.Font.Size = oriFontSize + 1;
                                        break;
                                    case TextDecoration.Bullet:
                                        object listTemplateInd = 1;
                                        docRng.ListFormat.ApplyListTemplate(
                                            wordDoc.Application.ListGalleries[WORD.WdListGalleryType.wdBulletGallery].ListTemplates.get_Item(ref listTemplateInd),
                                            ref missing, ref missing, ref missing);
                                        break;
                                    case TextDecoration.NewLineBefore:
                                        docRng.InsertParagraphBefore();
                                        break;
                                    case TextDecoration.NewLineAfter:
                                        docRng.InsertParagraphAfter();

                                        //Remove bullet
                                        object numberTypeParagraph = WORD.WdNumberType.wdNumberParagraph;
                                        object numberTypeAllNumbers = WORD.WdNumberType.wdNumberAllNumbers;
                                        object numberTypeListNum = WORD.WdNumberType.wdNumberListNum;
                                        docRng.Start = docRng.End;
                                        docRng.ListFormat.RemoveNumbers(ref numberTypeAllNumbers);
                                        docRng.End = docRng.End - 1;
                                        break;
                                    case TextDecoration.ListIndent:
                                        docRng.ListFormat.ListIndent();
                                        break;
                                    case TextDecoration.ListOutdent:
                                        docRng.ListFormat.ListOutdent();
                                        break;
                                    default:
                                        break;
                                }
                            }

                            //debug
                            if(logger != null) logger.Info("------text replaced.");

                        } else if(replacement is PictureReplacement) {

                            PictureReplacement picReplacement = replacement as PictureReplacement;

                            docRng.InlineShapes.AddPicture(picReplacement.PictureFilePath, ref missing, ref missing, ref missing);

                            //debug
                            if(logger != null) logger.Info("------picture replaced.");
                        }

                        docRng.Start = docRng.End;
                    }

                    //Start the next search excluding already searched range
                    docRng.End = docRng.Start > 0 ? docRng.Start - 1 : 0;
                    docRng.Start = 0;

                    docRng.Find.Execute(
                        ref missing, ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing);
                }

                numOfKeysProcessed++;

                //Report progress
                if(progressReporter != null) {
                    progressReporter("", Convert.ToDouble(numOfKeysProcessed) / numOfKeys);
                }
            }

            //debug
            if(logger != null) logger.Info("--replaceTemplateKeywords end.");

        }
        #endregion*/


        #region using OpenXMLSDK 2.0 to process Word files
        public ProcessResponse OpenXMLBuildWordFromTemplate(ProcessRequest request, ProgressReporterDelegate progressReporter) {

            //debug
            //if(logger != null) logger.Info("OpenXMLBuildWordFromTemplate start at: " + DateTime.Now.ToString("hh:mm:ss.fffff"));

            //Initiate ProcessResponse object
            ProcessResponse response = new ProcessResponse();
            response.Succeeded = true;

            //Verify Request object
            if(request.TemplateFilePath.Trim().Length <= 0 ||
                !File.Exists(request.TemplateFilePath)) {
                response.Succeeded = false;
                response.ReturnMessage = "Template file is missing.";
                return response;
            }
            if(request.TargetFilePath.Trim().Length <= 0) {
                response.Succeeded = false;
                response.ReturnMessage = "Please specify the target file path.";
                return response;
            } else if(File.Exists(request.TargetFilePath) && !request.ReplaceTarget) {
                response.Succeeded = false;
                response.ReturnMessage = "The target file, " + request.TargetFilePath + ", already exists!";
                return response;
            }

            //Create target subfolder if not existing
            FileInfo fi = new FileInfo(request.TargetFilePath);
            if(!fi.Directory.Exists) {
                Directory.CreateDirectory(fi.DirectoryName);
            }

            //Copy template to target location
            File.Copy(request.TemplateFilePath, request.TargetFilePath);

            //Process Word file
            using(WordprocessingDocument doc = WordprocessingDocument.Open(request.TargetFilePath, true)) {
                //debug
                //if(logger != null) logger.Info("--Replacement process start.");

                //Prepare for progress reporter
                int numOfKeys = request.Replacements.Keys.Count;
                int numOfKeysProcessed = 0;

                //Go through all the keywords
                #region process all the keywords

                /*
                foreach (string key in request.Replacements.Keys) {

                    //debug
                    //if(logger != null) logger.Info("----start to search on [" + key + "].");

                    Paragraph keyParagraph = findTextElement(doc.MainDocumentPart.Document.Body, key);

                    //debug
                    //if(logger != null) logger.Info("----search end.");
                    var Ps = findAllTextElements(doc, key);
                    if (Ps.Count() > 1) {
                        //debug
                        if (logger != null) logger.Info("--- !!! found more Paragraphs !!!! ---.");
                    }

                    //Start to replace if found the key
                    if(keyParagraph != null) {

                        //debug
                        //if(logger != null) logger.Info("------key found, start to replace.");

                        //For now, only one Replacement is allowed for OpenXML solution
                        if(request.Replacements[key].Count() != 1) {
                            if(logger != null) logger.Error("When using OpenXMLBuildWordFromTemplate(...) function, for each key, there must be ONE WordReplacement or PictureReplacement.");
                            throw new ApplicationException("When using OpenXMLBuildWordFromTemplate(...) function, for each key, there must be ONE WordReplacement or PictureReplacement.");
                        }
                        ReplacementBase replacement = request.Replacements[key].First();

                        //Find the exact Text element in the keyParagraph
                        Text keyText = null;
                        Run[] arrRun = keyParagraph.Elements<Run>().ToArray();
                        foreach(Run run in arrRun) {
                            if(run.InnerText.Contains(key)) {
                                keyText = run.Elements<Text>().First();
                                break;
                            }
                        }

                        //If the exact Text element was not found, the key string must has been broken into multiple Runs, combine them.
                        if(keyText == null) {
                            int indStartRun = -1;
                            int indEndRun = -1;
                            if(!findExactRuns(arrRun, key, ref indStartRun, ref indEndRun)) {
                                throw new ApplicationException("findExactRuns should not fail! There must be a bug.");
                            }
                            //Merge runs
                            string strMergeText = arrRun[indStartRun].InnerText;
                            for(int i = indStartRun + 1; i <= indEndRun; i++) {
                                strMergeText += arrRun[i].InnerText;
                            }
                            Run mergedRun = arrRun[indStartRun].Clone() as Run;
                            mergedRun.Elements<Text>().First().Text = strMergeText;
                            keyParagraph.InsertAfter<Run>(mergedRun, arrRun[indEndRun]);
                            for(int i = indStartRun; i <= indEndRun; i++) {
                                arrRun[i].Remove();
                            }

                            keyText = mergedRun.Elements<Text>().First();
                        }

                        //Replace
                        if(replacement is WordReplacement) {

                            //Replace \r\n with <w:br/>
                            string text = keyText.Text.Replace(key, (replacement as WordReplacement).Text);
                            string[] arrLine = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                            Run keyRun = keyText.Parent as Run;
                            OpenXmlElement insertAfter = keyText;
                            for(int i = 0; i < arrLine.Length; i++) {
                                Text newText = keyText.Clone() as Text;
                                newText.Text = arrLine[i];
                                keyRun.InsertAfter<Text>(newText, insertAfter);

                                if(i < arrLine.Length - 1) {
                                    Break lineBreak = new Break();
                                    keyRun.InsertAfter<Break>(lineBreak, newText);
                                    insertAfter = lineBreak;
                                } else {
                                    insertAfter = newText;
                                }
                            }

                            //Remove the place holder text
                            keyRun.RemoveChild<Text>(keyText);

                        } else if(replacement is PictureReplacement) {

                            //Decide where to insert
                            int insertAt = 0;
                            OpenXmlElement[] runs = keyText.Parent.Parent.Elements().ToArray();
                            int numOfRuns = runs.Count();
                            for(int i = 0; i < numOfRuns; i++) {
                                if(runs[i] == keyText.Parent) {
                                    insertAt = i;

                                    //Remove the placehoder run. Split runs if necessary
                                    int keyIndex = keyText.Text.IndexOf(key);
                                    if(keyIndex > 0) {
                                        Run preRun = runs[i].Clone() as Run;
                                        preRun.Elements<Text>().First().Text = keyText.Text.Substring(0, keyIndex);
                                        keyText.Parent.Parent.InsertAt<Run>(preRun, insertAt);
                                        insertAt++;
                                    }
                                    if(keyIndex + key.Length < keyText.Text.Length) {
                                        Run sufRun = runs[i].Clone() as Run;
                                        sufRun.Elements<Text>().First().Text = keyText.Text.Substring(keyIndex + key.Length);
                                        keyText.Parent.Parent.InsertAt<Run>(sufRun, insertAt);
                                    }
                                    keyParagraph.RemoveChild<Run>(keyText.Parent as Run);

                                    break;
                                }
                            }

                            //Insert picture
                            InsertAPicture(doc,
                                keyParagraph,
                                insertAt,
                                (replacement as PictureReplacement).PictureFilePath);
                        }
                    }

                    numOfKeysProcessed++;

                    //Report progress
                    if(progressReporter != null) {
                        progressReporter("", Convert.ToDouble(numOfKeysProcessed) / numOfKeys);
                    }
                }
                */

                //Find all Runs in the Word doc
                IList<Run> runs = getDocRuns(doc);

                foreach (string key in request.Replacements.Keys) {

                    //For now, only one Replacement is allowed for OpenXML solution
                    if (request.Replacements[key].Count() != 1) {
                        if (logger != null) logger.Error("When using OpenXMLBuildWordFromTemplate(...) function, for each key, there must be ONE WordReplacement or PictureReplacement.");
                        throw new ApplicationException("When using OpenXMLBuildWordFromTemplate(...) function, for each key, there must be ONE WordReplacement or PictureReplacement.");
                    }
                    ReplacementBase replacement = request.Replacements[key].First();

                    
                    //Find all paragraphs containing the key
                    /*
                    var Ps = findAllTextElements(doc, key);

                    foreach (Paragraph p in Ps) {
                        replaceKeys(doc, p, key, replacement);
                    }*/
                    
                    #region test using RUN other than Paragraph
                    for (int i = 0; i < runs.Count; i++) {
                        Run r = runs[i];
                        bool hasText = (r.Elements<Text>().Count() > 0);
                        Text t = null;

                        if (hasText) {
                            t = r.Elements<Text>().First();
                        }

                        if (hasText && t.Text.Contains(key)) {//key found in Text element, replace the key(s) directly
                            if (replaceTextKeys(doc, t, key, replacement)) {
                                //rescan document Runs when needed.
                                runs = getDocRuns(doc);
                            }
                        } else if (!hasText) {//This run doesn't have direct Text element, ignore
                            continue;
                        } else {//This Run contains text but the key was not found, try to see if the key has been splitted into sibling runs

                            List<Run> siblings = new List<Run>();
                            string textInSiblings = string.Empty;
                            Run lastR = null;

                            Run nextR = r.NextSibling<Run>();
                            while(textInSiblings.Length < key.Length -1 && nextR != null && nextR.Elements<Text>().Count() > 0){
                                textInSiblings += nextR.Elements<Text>().First().Text;
                                lastR = nextR;
                                siblings.Add(nextR);

                                nextR = nextR.NextSibling<Run>();
                            }

                            int maxSibingTextToCheck = Math.Min(key.Length - 1, textInSiblings.Length);

                            if ((t.Text + textInSiblings.Substring(0, maxSibingTextToCheck)).Contains(key)) {

                                //Merge runs
                                string strMergeText = t.Text + textInSiblings;
                                Run mergedRun = r.Clone() as Run;
                                mergedRun.Elements<Text>().First().Text = strMergeText;
                                r.Parent.InsertAfter<Run>(mergedRun, lastR);
                                foreach (Run sibling in siblings) {
                                    sibling.Remove();
                                    i++;
                                }
                                r.Remove();

                                //Replace
                                if (replaceTextKeys(doc, mergedRun.Elements<Text>().First(), key, replacement)) {
                                    //rescan document Runs when needed.
                                    runs = getDocRuns(doc);
                                }
                            }

                        }
                    }
                    #endregion test using RUN other than Paragraph
                    
                    numOfKeysProcessed++;

                    //Report progress
                    if (progressReporter != null) {
                        progressReporter("", Convert.ToDouble(numOfKeysProcessed) / numOfKeys);
                    }
                }

                #endregion process all keywords



                //debug
                //if(logger != null) logger.Info("--replaceTemplateKeywords end.");
            }

            //debug
            //if(logger != null) logger.Info("OpenXMLBuildWordFromTemplate end at: " + DateTime.Now.ToString("hh:mm:ss.fffff"));

            return response;
        }

        //Find all Runs in the Word doc
        private IList<Run> getDocRuns(WordprocessingDocument doc) {
            List<Run> runs = new List<Run>(
                            doc.MainDocumentPart.Document.Body
                                .Descendants<Run>()
                            );
            foreach (HeaderPart header in doc.MainDocumentPart.HeaderParts) {
                runs.AddRange(
                    header.RootElement.Descendants<Run>()
                    );
            }
            foreach (FooterPart footer in doc.MainDocumentPart.FooterParts) {
                runs.AddRange(
                    footer.RootElement.Descendants<Run>()
                    );
            }

            return runs;
        }

        /*
        public ProcessResponse OpenXMLBuildWordFromTemplate_invoice(ProcessRequest request, ProgressReporterDelegate progressReporter) {

            //debug
            if (logger != null) logger.Info("OpenXMLBuildWordFromTemplate start at: " + DateTime.Now.ToString("hh:mm:ss.fffff"));

            //Initiate ProcessResponse object
            ProcessResponse response = new ProcessResponse();
            response.Succeeded = true;

            //Verify Request object
            if (request.TemplateFilePath.Trim().Length <= 0 ||
                !File.Exists(request.TemplateFilePath)) {
                response.Succeeded = false;
                response.ReturnMessage = "Template file is missing.";
                return response;
            }
            if (request.TargetFilePath.Trim().Length <= 0) {
                response.Succeeded = false;
                response.ReturnMessage = "Please specify the target file path.";
                return response;
            } else if (File.Exists(request.TargetFilePath) && !request.ReplaceTarget) {
                response.Succeeded = false;
                response.ReturnMessage = "The target file, " + request.TargetFilePath + ", already exists!";
                return response;
            }

            //Create target subfolder if not existing
            FileInfo fi = new FileInfo(request.TargetFilePath);
            if (!fi.Directory.Exists) {
                Directory.CreateDirectory(fi.DirectoryName);
            }

            //Copy template to target location
            File.Copy(request.TemplateFilePath, request.TargetFilePath);

            //Process Word file
            using (WordprocessingDocument doc = WordprocessingDocument.Open(request.TargetFilePath, true)) {
                //debug
                if (logger != null) logger.Info("--Replacement process start.");

                //Prepare for progress reporter
                int numOfKeys = request.Replacements.Keys.Count;
                int numOfKeysProcessed = 0;

                //Go through all the keywords
                foreach (string key in request.Replacements.Keys) {

                    //debug
                    if (logger != null) logger.Info("----start to search on [" + key + "].");

                    Paragraph keyParagraph = findTextElement(doc.MainDocumentPart.Document.Body, key);

                    //debug
                    if (logger != null) logger.Info("----search end.");

                    //Start to replace if found the key
                    if (keyParagraph != null) {

                        //debug
                        if (logger != null) logger.Info("------key found, start to replace.");

                        //For now, only one Replacement is allowed for OpenXML solution
                        if (request.Replacements[key].Count() != 1) {
                            if (logger != null) logger.Error("When using OpenXMLBuildWordFromTemplate(...) function, for each key, there must be ONE WordReplacement or PictureReplacement.");
                            throw new ApplicationException("When using OpenXMLBuildWordFromTemplate(...) function, for each key, there must be ONE WordReplacement or PictureReplacement.");
                        }
                        ReplacementBase replacement = request.Replacements[key].First();

                        //Find the exact Text element in the keyParagraph
                        Text keyText = null;
                        Run[] arrRun = keyParagraph.Elements<Run>().ToArray();
                        foreach (Run run in arrRun) {
                            if (run.InnerText.Contains(key)) {
                                keyText = run.Elements<Text>().First();
                                break;
                            }
                        }

                        //If the exact Text element was not found, the key string must has been broken into multiple Runs, combine them.
                        if (keyText == null) {
                            int indStartRun = -1;
                            int indEndRun = -1;
                            if (!findExactRuns(arrRun, key, ref indStartRun, ref indEndRun)) {
                                throw new ApplicationException("findExactRuns should not fail! There must be a bug.");
                            }
                            //Merge runs
                            string strMergeText = arrRun[indStartRun].InnerText;
                            for (int i = indStartRun + 1; i <= indEndRun; i++) {
                                strMergeText += arrRun[i].InnerText;
                            }
                            Run mergedRun = arrRun[indStartRun].Clone() as Run;
                            mergedRun.Elements<Text>().First().Text = strMergeText;
                            keyParagraph.InsertAfter<Run>(mergedRun, arrRun[indEndRun]);
                            for (int i = indStartRun; i <= indEndRun; i++) {
                                arrRun[i].Remove();
                            }

                            keyText = mergedRun.Elements<Text>().First();
                        }

                        //Replace
                        if (replacement is WordReplacement) {

                            //Replace \r\n with <w:br/>
                            string text = keyText.Text.Replace(key, (replacement as WordReplacement).Text);
                            string[] arrLine = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                            Run keyRun = keyText.Parent as Run;
                            OpenXmlElement insertAfter = keyText;
                            for (int i = 0; i < arrLine.Length; i++) {
                                Text newText = keyText.Clone() as Text;
                                newText.Text = arrLine[i];
                                keyRun.InsertAfter<Text>(newText, insertAfter);

                                if (i < arrLine.Length - 1) {
                                    Break lineBreak = new Break();
                                    keyRun.InsertAfter<Break>(lineBreak, newText);
                                    insertAfter = lineBreak;
                                } else {
                                    insertAfter = newText;
                                }
                            }

                            //Remove the place holder text
                            keyRun.RemoveChild<Text>(keyText);

                        } else if (replacement is PictureReplacement) {

                            //Decide where to insert
                            int insertAt = 0;
                            OpenXmlElement[] runs = keyText.Parent.Parent.Elements().ToArray();
                            int numOfRuns = runs.Count();
                            for (int i = 0; i < numOfRuns; i++) {
                                if (runs[i] == keyText.Parent) {
                                    insertAt = i;

                                    //Remove the placehoder run. Split runs if necessary
                                    int keyIndex = keyText.Text.IndexOf(key);
                                    if (keyIndex > 0) {
                                        Run preRun = runs[i].Clone() as Run;
                                        preRun.Elements<Text>().First().Text = keyText.Text.Substring(0, keyIndex);
                                        keyText.Parent.Parent.InsertAt<Run>(preRun, insertAt);
                                        insertAt++;
                                    }
                                    if (keyIndex + key.Length < keyText.Text.Length) {
                                        Run sufRun = runs[i].Clone() as Run;
                                        sufRun.Elements<Text>().First().Text = keyText.Text.Substring(keyIndex + key.Length);
                                        keyText.Parent.Parent.InsertAt<Run>(sufRun, insertAt);
                                    }
                                    keyParagraph.RemoveChild<Run>(keyText.Parent as Run);

                                    break;
                                }
                            }

                            //Insert picture
                            InsertAPicture(doc,
                                keyParagraph,
                                insertAt,
                                (replacement as PictureReplacement).PictureFilePath);
                        }
                    }

                    numOfKeysProcessed++;

                    //Report progress
                    if (progressReporter != null) {
                        progressReporter("", Convert.ToDouble(numOfKeysProcessed) / numOfKeys);
                    }
                }

                //debug
                if (logger != null) logger.Info("--replaceTemplateKeywords end.");
            }

            //debug
            if (logger != null) logger.Info("OpenXMLBuildWordFromTemplate end at: " + DateTime.Now.ToString("hh:mm:ss.fffff"));

            return response;
        }
        */

        //NOT IN USE. Refer to the findAllTextElements(...) method below
        //Find the Paragraph that contains the searching text
        /*
        private Paragraph findTextElement(OpenXmlElement RootElement, string text) {

            Paragraph found = null;

            foreach(OpenXmlElement element in RootElement.Elements()) {
                if(element is Paragraph && element.InnerText.Contains(text)) {
                    
                    //First time found the key paragraph
                    found = element as Paragraph;

                    //Make sure find the most inner key paragraph
                    Paragraph innerP = findTextElement(found, text);
                    while (innerP != null) {
                        found = innerP;
                        innerP = findTextElement(found, text);
                    }
                    return found;
                } else if(element.HasChildren) {
                    found = findTextElement(element, text);
                    if(found != null) return found;
                }
            }

            return null;
        }
        */

        //Find all Paragraphs that contains the searching text in the doc including header and footer
        /*
        private IEnumerable<Paragraph> findAllTextElements(WordprocessingDocument doc, string text) {

            //Find all Paragraphs in document body
            List<Paragraph> paragraphs = new List<Paragraph>(
                doc.MainDocumentPart.Document.Body
                    .Descendants<Paragraph>()
                    .Where(p => p.InnerText != null && p.InnerText.Contains(text))
                );
            
            //Find all Paragraphs in document headers
            foreach (HeaderPart header in doc.MainDocumentPart.HeaderParts) {
                paragraphs.AddRange(
                    header.RootElement.Descendants<Paragraph>()
                        .Where(p => p.InnerText != null && p.InnerText.Contains(text))
                    );
            }

            //Find all Paragraphs in document footers
            foreach (FooterPart footer in doc.MainDocumentPart.FooterParts) {
                paragraphs.AddRange(
                    footer.RootElement.Descendants<Paragraph>()
                        .Where(p => p.InnerText != null && p.InnerText.Contains(text))
                    );
            }

            //Paragraph may nest other paragraphs. Only to find the deepest level paragraphs containing the search text.
            IList<Paragraph> ret = new List<Paragraph>();
            foreach (Paragraph p in paragraphs) {
                
                bool containTextDirectly = false;
                foreach (Run r in p.Elements<Run>()) {
                    if (r.Elements<Text>().Count() <= 0) {
                        //This paragraph doesn't contain the search text directly
                    } else {
                        containTextDirectly = true;
                        break;
                    }
                }

                if (containTextDirectly) {
                    ret.Add(p);
                }
            }

            return ret;
            
        }
        */

        /*
        private bool findExactRuns(Run[] arrRun, string key, ref int startRun, ref int endRun) {

            //Get the first Char in key string
            char[] arrKey = key.ToCharArray();
            char keyStart = arrKey[0];
            char keyEnd = arrKey[arrKey.Length - 1];

            string strSearch = string.Empty;
            for(int i = 0; i < arrRun.Length; i++) {
                Run runStart = arrRun[i];
                int indKeyStart = runStart.InnerText.LastIndexOf(keyStart);
                if(indKeyStart >= 0) {
                    strSearch = runStart.InnerText.Substring(indKeyStart);
                    if(!key.StartsWith(strSearch)) {
                        continue;
                    }

                    for(int j = i + 1; j < arrRun.Length; j++) {
                        Run runEnd = arrRun[j];
                        int indKeyEnd = runEnd.InnerText.IndexOf(keyEnd);
                        if(indKeyEnd >= 0) {
                            strSearch += runEnd.InnerText.Substring(0, indKeyEnd + 1);
                            if(strSearch.Equals(key)) {
                                //Found the start and end run
                                startRun = i;
                                endRun = j;
                                return true;
                            } else {
                                break;
                            }
                        } else {
                            strSearch += runEnd.InnerText;
                            if(!key.StartsWith(strSearch)) {
                                break;
                            }
                        }
                    }
                }
            }

            //Return false if reach here
            return false;
        }
        */
        
        private void InsertAPicture(WordprocessingDocument wordprocessingDocument, Paragraph container, int insertAt, string fileName) {
            MainDocumentPart mainPart = wordprocessingDocument.MainDocumentPart;

            ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);
            long imgWidthEMU = 0;
            long imgHeightEMU = 0;

            using(FileStream stream = new FileStream(fileName, FileMode.Open)) {
                imagePart.FeedData(stream);
            }

            using(Bitmap img = new Bitmap(fileName)) {
                imgWidthEMU = (long)((img.Width / img.HorizontalResolution) * 914400L);
                imgHeightEMU = (long)((img.Height / img.VerticalResolution) * 914400L);
            }


            AddImageToBody(container, insertAt, mainPart.GetIdOfPart(imagePart), imgWidthEMU, imgHeightEMU);
        }

        private void AddImageToBody(Paragraph paragraph, int insertAt, string relationshipId, long imgWidth, long imgHeight) {

            //Define the reference of the image.
            var element =
                 new Drawing(
                     new DW.Inline(
                         new DW.Extent() { Cx = imgWidth, Cy = imgHeight },
                         new DW.EffectExtent() {
                             LeftEdge = 0L, TopEdge = 0L,
                             RightEdge = 0L, BottomEdge = 0L
                         },
                         new DW.DocProperties() {
                             Id = (UInt32Value)1U,
                             Name = "Picture 1"
                         },
                         new DW.NonVisualGraphicFrameDrawingProperties(
                             new A.GraphicFrameLocks() { NoChangeAspect = true }),
                         new A.Graphic(
                             new A.GraphicData(
                                 new PIC.Picture(
                                     new PIC.NonVisualPictureProperties(
                                         new PIC.NonVisualDrawingProperties() {
                                             Id = (UInt32Value)0U,
                                             Name = "New Bitmap Image.jpg"
                                         },
                                         new PIC.NonVisualPictureDrawingProperties()),
                                     new PIC.BlipFill(
                                         new A.Blip(
                                             new A.BlipExtensionList(
                                                 new A.BlipExtension() {
                                                     Uri =
                                                       "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                                 })
                                         ) {
                                             Embed = relationshipId,
                                             CompressionState =
                                                 //A.BlipCompressionValues.Print
                                             A.BlipCompressionValues.HighQualityPrint
                                         },
                                         new A.Stretch(
                                             new A.FillRectangle())),
                                     new PIC.ShapeProperties(
                                         new A.Transform2D(
                                             new A.Offset() { X = 0L, Y = 0L },
                                             new A.Extents() { Cx = imgWidth, Cy = imgHeight }),
                                         new A.PresetGeometry(
                                             new A.AdjustValueList()
                                         ) { Preset = A.ShapeTypeValues.Rectangle }))
                             ) { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                     ) {
                         DistanceFromTop = (UInt32Value)0U,
                         DistanceFromBottom = (UInt32Value)0U,
                         DistanceFromLeft = (UInt32Value)0U,
                         DistanceFromRight = (UInt32Value)0U
                         //, EditId = "50D07946"
                     });

            //Append the reference to a paragraph, the element should be in a Run.
            //paragraph.AppendChild(new Run(element));
            paragraph.InsertAt<Run>(new Run(element), insertAt);
        }

        //Replace the found key text with the replacement text/picture
        /*
        private void replaceKeys(WordprocessingDocument doc, Paragraph p, string key, ReplacementBase replacement) {

            //Find the exact Text element in the keyParagraph
            Text keyText = null;
            Run[] arrRun = p.Elements<Run>().ToArray();
            foreach (Run run in arrRun) {
                if (run.InnerText.Contains(key)) {
                    keyText = run.Elements<Text>().First();
                    break;
                }
            }

            //If the exact Text element was not found, the key string must has been broken into multiple Runs, combine them.
            if (keyText == null) {
                int indStartRun = -1;
                int indEndRun = -1;
                if (!findExactRuns(arrRun, key, ref indStartRun, ref indEndRun)) {
                    throw new ApplicationException("findExactRuns should not fail! There must be a bug.");
                }
                //Merge runs
                string strMergeText = arrRun[indStartRun].InnerText;
                for (int i = indStartRun + 1; i <= indEndRun; i++) {
                    strMergeText += arrRun[i].InnerText;
                }
                Run mergedRun = arrRun[indStartRun].Clone() as Run;
                mergedRun.Elements<Text>().First().Text = strMergeText;
                p.InsertAfter<Run>(mergedRun, arrRun[indEndRun]);
                for (int i = indStartRun; i <= indEndRun; i++) {
                    arrRun[i].Remove();
                }

                keyText = mergedRun.Elements<Text>().First();
            }

            //Replace
            if (replacement is WordReplacement) {

                //Replace \r\n with <w:br/>
                string text = keyText.Text.Replace(key, (replacement as WordReplacement).Text);
                string[] arrLine = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                Run keyRun = keyText.Parent as Run;
                OpenXmlElement insertAfter = keyText;
                for (int i = 0; i < arrLine.Length; i++) {
                    Text newText = keyText.Clone() as Text;
                    newText.Text = arrLine[i];
                    keyRun.InsertAfter<Text>(newText, insertAfter);

                    if (i < arrLine.Length - 1) {
                        Break lineBreak = new Break();
                        keyRun.InsertAfter<Break>(lineBreak, newText);
                        insertAfter = lineBreak;
                    } else {
                        insertAfter = newText;
                    }
                }

                //Remove the place holder text
                keyRun.RemoveChild<Text>(keyText);

            } else if (replacement is PictureReplacement) {

                //Decide where to insert
                int insertAt = 0;
                OpenXmlElement[] runs = keyText.Parent.Parent.Elements().ToArray();
                int numOfRuns = runs.Count();
                for (int i = 0; i < numOfRuns; i++) {
                    if (runs[i] == keyText.Parent) {
                        insertAt = i;

                        //Remove the placehoder run. Split runs if necessary
                        int keyIndex = keyText.Text.IndexOf(key);
                        if (keyIndex > 0) {
                            Run preRun = runs[i].Clone() as Run;
                            preRun.Elements<Text>().First().Text = keyText.Text.Substring(0, keyIndex);
                            keyText.Parent.Parent.InsertAt<Run>(preRun, insertAt);
                            insertAt++;
                        }
                        if (keyIndex + key.Length < keyText.Text.Length) {
                            Run sufRun = runs[i].Clone() as Run;
                            sufRun.Elements<Text>().First().Text = keyText.Text.Substring(keyIndex + key.Length);
                            keyText.Parent.Parent.InsertAt<Run>(sufRun, insertAt);
                        }
                        p.RemoveChild<Run>(keyText.Parent as Run);

                        break;
                    }
                }

                //Insert picture
                InsertAPicture(doc,
                    p,
                    insertAt,
                    (replacement as PictureReplacement).PictureFilePath);
            }
        }
        */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="keyText"></param>
        /// <param name="key"></param>
        /// <param name="replacement"></param>
        /// <returns>True - The Text's Run element has been removed. Need to rescan the document to update the Run list.</returns>
        private bool replaceTextKeys(WordprocessingDocument doc, Text keyText, string key, ReplacementBase replacement) {

            bool runRemoved = false;

            if (replacement is WordReplacement) {

                //Replace \r\n with <w:br/>
                string text = keyText.Text.Replace(key, (replacement as WordReplacement).Text);
                string[] arrLine = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                Run keyRun = keyText.Parent as Run;
                OpenXmlElement insertAfter = keyText;
                for (int i = 0; i < arrLine.Length; i++) {
                    Text newText = keyText.Clone() as Text;
                    newText.Text = arrLine[i];
                    keyRun.InsertAfter<Text>(newText, insertAfter);

                    if (i < arrLine.Length - 1) {
                        Break lineBreak = new Break();
                        keyRun.InsertAfter<Break>(lineBreak, newText);
                        insertAfter = lineBreak;
                    } else {
                        insertAfter = newText;
                    }
                }

                //Remove the place holder text
                keyRun.RemoveChild<Text>(keyText);

            } else if (replacement is PictureReplacement) {

                //Decide where to insert
                int insertAt = 0;
                Paragraph p = keyText.Parent.Parent as Paragraph;
                OpenXmlElement[] runs = keyText.Parent.Parent.Elements().ToArray();
                int numOfRuns = runs.Count();
                for (int i = 0; i < numOfRuns; i++) {
                    if (runs[i] == keyText.Parent) {
                        insertAt = i;

                        //Remove the placehoder run. Split runs if necessary
                        int keyIndex = keyText.Text.IndexOf(key);
                        if (keyIndex > 0) {
                            Run preRun = runs[i].Clone() as Run;
                            preRun.Elements<Text>().First().Text = keyText.Text.Substring(0, keyIndex);
                            keyText.Parent.Parent.InsertAt<Run>(preRun, insertAt);
                            insertAt++;
                        }
                        if (keyIndex + key.Length < keyText.Text.Length) {
                            Run sufRun = runs[i].Clone() as Run;
                            sufRun.Elements<Text>().First().Text = keyText.Text.Substring(keyIndex + key.Length);
                            keyText.Parent.Parent.InsertAt<Run>(sufRun, insertAt);
                        }

                        p.RemoveChild<Run>(keyText.Parent as Run);
                        runRemoved = true;

                        break;
                    }
                }

                //Insert picture
                InsertAPicture(doc,
                    p,
                    insertAt,
                    (replacement as PictureReplacement).PictureFilePath);
            }else if(replacement is DeleteReplacement){
                DeleteReplacement deleteReplacement = replacement as DeleteReplacement;
                switch (deleteReplacement.ElementType.ToLower().Trim()) {
                    case "textbox":
                        //The Word XML hierachy is Run > Picture > Shape > TextBox > TextBoxContent > Paragraph > Run > Text
                        Run txtboxRun = null;
                        try {
                            txtboxRun = keyText.Parent.Parent.Parent.Parent.Parent.Parent.Parent as Run;
                        } catch {
                            //Couldn't find the TextBox's Run element. Ignore.
                        }

                        //Delete the Run
                        if (txtboxRun != null) {
                            txtboxRun.Remove();
                        }
                        break;
                    default:
                        throw new ArgumentException("WordFileProcessor cannot process the DeleteReplacement.ElementType: " + deleteReplacement.ElementType + ".");
                }
            }

            return runRemoved;
        }

        #endregion using OpenXMLSDK 2.0 to process Word files

        #region using OpenXMLSDK 2.0 to combine Word files
        public void OpenXMLCombineWordFiles(bool removeWordFiles, bool addPageBreak, params string[] wordFiles) {

            string majorWordFile = string.Empty;

            if (wordFiles.Length < 1) {
                return;
            } else {
                majorWordFile = wordFiles[0].Trim(); //Use the first Word file as major file
                if (!File.Exists(majorWordFile)) {
                    return;
                }
            }

            using (WordprocessingDocument myDoc = WordprocessingDocument.Open(majorWordFile, true)) {
                
                MainDocumentPart mainPart = myDoc.MainDocumentPart;
                OpenXmlElement refChild = mainPart.Document.Body.Elements<Paragraph>().Last();

                for (int i = 1; i < wordFiles.Length; i++) {

                    if (addPageBreak) {
                        Paragraph para = new Paragraph(new Run((new Break() { Type = BreakValues.Page })));
                        mainPart.Document.Body.InsertAfter(para, /*mainPart.Document.Body.LastChild*/ refChild);
                        refChild = mainPart.Document.Body.Elements<Paragraph>().Last();
                    }

                    string altChunkId = "AltChunkId" + i.ToString();
                    AlternativeFormatImportPart chunk = mainPart.AddAlternativeFormatImportPart(
                        AlternativeFormatImportPartType.WordprocessingML, altChunkId);
                    using (FileStream fileStream = File.Open(wordFiles[i], FileMode.Open)) {
                        chunk.FeedData(fileStream);
                    }
                    if(removeWordFiles) File.Delete(wordFiles[i]);
                    AltChunk altChunk = new AltChunk();
                    altChunk.Id = altChunkId;
                    mainPart.Document.Body.InsertAfter(altChunk, refChild);
                    refChild = altChunk;


                }
                mainPart.Document.Save();
            }
        }
        #endregion using OpenXMLSDK 2.0 to combine Word files
    }
}
