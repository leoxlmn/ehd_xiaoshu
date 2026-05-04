using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility {
    public class SqlTimeStamp {
        private byte[] _timestamp;
        private UInt64 _uInt64;

        private SqlTimeStamp(){}

        public SqlTimeStamp(byte[] TimeStamp) {
            if(TimeStamp == null) throw new ArgumentException("TimeStamp cannot be null.");
            if(TimeStamp.Length != 8) throw new ArgumentException("TimeStamp has to be 8 bytes long.");

            _timestamp = TimeStamp;

            byte[] arrReversed = new byte[8];

            for (int i = 0; i < 4; i++) {
                arrReversed[i] = _timestamp[7 - i];
            }

            _uInt64 = BitConverter.ToUInt64(arrReversed, 0);
        }

        public UInt64 NumberValue {
            get { return _uInt64; }
        }

        public byte[] TimeStamp {
            get { return _timestamp; }
        }

        public override string ToString() {
            return _uInt64.ToString();
        }

        public override bool Equals(object obj) {
            return _uInt64.Equals(((SqlTimeStamp)obj).NumberValue);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
