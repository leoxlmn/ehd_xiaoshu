using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordFileProcessor {
    public enum TextDecoration{
        Bold,
        Larger,
        Smaller,
        Italic,
        Underline,
        Bullet,
        NewLineAfter,
        NewLineBefore,
        ListIndent,
        ListOutdent
    }

    public class WordReplacement : ReplacementBase {
        public string Text { get; set; }
        public List<TextDecoration> TextDecorations { get; set; }

        public WordReplacement(string text) {
            Text = text;
            TextDecorations = new List<TextDecoration>();
        }

        public void AddTextDecoration(TextDecoration decoration) {
            if (TextDecorations == null) {
                TextDecorations = new List<TextDecoration>();
            }

            if (!TextDecorations.Contains(decoration)) {
                TextDecorations.Add(decoration);
            }
                
        }
    }

}
