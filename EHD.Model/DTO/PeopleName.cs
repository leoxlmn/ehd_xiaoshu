using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EHD.Model.DTO {
    public class PeopleName {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public override string ToString() {
            return
                (MiddleName != null && MiddleName.Trim().Length > 0) ?
                string.Format("{0} {1} {2}", FirstName, MiddleName, LastName) :
                string.Format("{0} {1}", FirstName, LastName);
        }

    }
}
