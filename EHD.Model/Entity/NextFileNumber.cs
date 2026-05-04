using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EHD.Model.Entity {
    public class NextFileNumber {

        /// <summary>
        /// Primary Key Field
        /// </summary>
        public virtual char Key { get; set; }
        public virtual int NextNumber { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public NextFileNumber() {
            Key = '\0';
            NextNumber = 1;
        }

    }
}
