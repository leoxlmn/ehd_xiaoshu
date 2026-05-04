using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordFileProcessor {

    public class DeleteReplacement : ReplacementBase {
        public string ElementType { get; set; }

        public DeleteReplacement(string elementType) {
            ElementType = elementType;
        }
    }

}
