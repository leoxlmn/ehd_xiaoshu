using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordFileProcessor {
    public class ProcessRequest {
        public bool ReplaceTarget { get; set; }
        public string TargetFilePath { get; set; }
        public string TemplateFilePath { get; set; }
        public IDictionary<string, IEnumerable<ReplacementBase>> Replacements { get; set; }

        public ProcessRequest() {
            Replacements = new Dictionary<string, IEnumerable<ReplacementBase>>();
        }

        public ProcessRequest(string targetFilePath, string templateFilePath) 
            : this(targetFilePath, templateFilePath, false) {
        }

        public ProcessRequest(string targetFilePath,
            string templateFilePath, bool replaceTargetFile) {

            TargetFilePath = targetFilePath.Trim();
            TemplateFilePath = templateFilePath.Trim();
            ReplaceTarget = replaceTargetFile;

            Replacements = new Dictionary<string, IEnumerable<ReplacementBase>>();
        }

        public void AddWordReplacement(string key, string replacement){
            Replacements.Add(key, new List<WordReplacement>(){
                new WordReplacement(replacement)});
        }
    }
}
