using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordFileProcessor {
    public class PictureReplacement : ReplacementBase {
        public string PictureFilePath { get; set; }

        public PictureReplacement(string filePath) {
            PictureFilePath = filePath;
        }
    }
}
