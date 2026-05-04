using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHD.Model.Entity;
using FilePathExtender;
using EHD.Constant;
using System.IO;
using WordFileProcessor;

namespace EHD.Admin {
    public delegate void PopulateExportRequestDelegate(WordFileProcessor.ProcessRequest request, object objectToExport);

    public class WordExportHelper {
        #region Public Properties
        public string WordExported {
            get {
                return _wordExported;
            }
        }
        public bool HasError {
            get {
                return _hasError;
            }
        }
        public string ErrorMessage {
            get {
                return _errorMessage;
            }
        }
        #endregion Public Properties

        #region Private members
        private PopulateExportRequestDelegate _populateRequest;
        private ProgressReporterDelegate _progressReporter;
        private string _wordExported;
        private bool _hasError;
        private string _errorMessage;
        private object _objectToExport;
        private string _templateName;
        #endregion Private members

        #region Constructor
        public WordExportHelper(object objectToExport, string wordTemplateName, 
            PopulateExportRequestDelegate populateRequestHandler,
            ProgressReporterDelegate progressReporterHandler) {

            _populateRequest = populateRequestHandler;
            _progressReporter = progressReporterHandler;
            _objectToExport = objectToExport;
            _templateName = wordTemplateName;
        }
        #endregion Constructor

        public void Export() {
            _wordExported = string.Empty;
            _hasError = false;
            _errorMessage = string.Empty;

            if(_templateName.Trim().Length <= 0) {
                _hasError = true;
                _errorMessage = "wordTemplateName is missing.";
                return;
            }

            try {
                //Get app root path
                string AppRootPath;
                AppRootPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                AppRootPath = AppRootPath.Substring(0, AppRootPath.LastIndexOf('\\'));
                string tmpPath = Path.GetTempPath();

                //1) Get the word template file path
                string templateFilePath = FileExtender.ConcatPhysicalPath(AppRootPath,
                    Constants.CFG_TEMPLATE_FOLDER,
                    _templateName);

                //2) Create a new temp word file which will be populated from the template and actual treatment data
                string destFilePath =
                    FileExtender.ConcatPhysicalPath(tmpPath, 
                    //Constants.CFG_PRINTOUT_FOLDER,
                    Guid.NewGuid().ToString() + ".docx");

                //3) Construct a ProcessRequest object, which will be passed to RequestProcessor to populate the temp word file
                ProcessRequest request = new ProcessRequest(destFilePath, templateFilePath, false);
                if(_populateRequest != null) {
                    _populateRequest(request, _objectToExport);
                }

                //4) Call RequestProcessor to populate word file
                RequestProcessor processor = new RequestProcessor(Program.log);
                /**
                 * Using OpenXMLSDK 2.0 is alot faster than using office dlls
                 */
                //ProcessResponse response = processor.BuildWordFromTemplate(request, _progressReporter);
                ProcessResponse response = processor.OpenXMLBuildWordFromTemplate(request, _progressReporter);
                if(!response.Succeeded) {
                    _hasError = true;
                    _errorMessage = "Failed to create Word file. " + response.ReturnMessage;
                    return;
                } else {
                    _wordExported = destFilePath;
                }

            } catch(Exception ex) {
                _hasError = true;
                _errorMessage = "Failed to create Word file. " + ex.Message;
            }
        }

        /*
        public void PrintWordFile(string WordFilePath) {
            RequestProcessor processor = new RequestProcessor(Program.log);
            processor.PrintWordFile(WordFilePath);
        }*/

        public void OpenWordFile(string WordFilePath) {
            RequestProcessor processor = new RequestProcessor(Program.log);
            processor.OpenWordFile(WordFilePath);
        }
        /*
        public void CombineWordFiles(bool addPageBreak, bool removeWordFiles, params string[] wordFiles) {
            RequestProcessor processor = new RequestProcessor(Program.log);
            processor.CombineWordFiles(addPageBreak, removeWordFiles, wordFiles);
        }

        public void CombineWordFiles(params string[] wordFiles) {
            RequestProcessor processor = new RequestProcessor(Program.log);
            processor.CombineWordFiles(wordFiles);
        }*/
    }
}
