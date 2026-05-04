using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordFileSolution {
    public class TextReplacement : IReplacement{
        public string Text { get; set; }

        public TextReplacement(string text) {
            Text = text;
        }
    }
}
