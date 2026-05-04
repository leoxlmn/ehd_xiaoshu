using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml.Drawing.Pictures;
using log4net;

namespace WordFileSolution {

    public delegate void ProgressReporterDelegate(string ProgressMessage, double ProgressPercent);

    
    public class ReplacementWorker {
        private ILog log = null;
        private ProgressReporterDelegate progressReporter = null;

        public ReplacementWorker(ProgressReporterDelegate ProgressReportHandler, ILog Logger) {
            progressReporter = ProgressReportHandler;
            log = Logger;
        }


    }
}
