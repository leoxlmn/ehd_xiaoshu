using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordFileSolution {
    public class ReplacementRequest {
        public bool ReplaceTarget { get; set; }
        public string TargetFilePath { get; set; }
        public string TemplateFilePath { get; set; }
        public IDictionary<string, IReplacement> Replacements { get; set; }

        public ReplacementRequest() {
            Replacements = new Dictionary<string, IReplacement>();
        }

        public ReplacementRequest(string targetFilePath, string templateFilePath) 
            : this(targetFilePath, templateFilePath, false) {
        }

        public ReplacementRequest(string targetFilePath,
            string templateFilePath, bool replaceTargetFile) {

            TargetFilePath = targetFilePath.Trim();
            TemplateFilePath = templateFilePath.Trim();
            ReplaceTarget = replaceTargetFile;

            Replacements = new Dictionary<string, IReplacement>();
        }

        public void AddTextReplacement(string key, string replacement){
            Replacements.Add(key, new TextReplacement(replacement));
        }

        public void AddPictureReplacement(string key, string imagePath) {
            Replacements.Add(key, new PictureReplacement(imagePath));
        }
    }
}
