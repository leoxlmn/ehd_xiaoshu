using System;
using System.Collections.Generic;

namespace EHD.Model.Entity {
    [Serializable]
    public class SystemSetting : EntityBase, IComparable<SystemSetting> {

        public virtual string Name { get; set; }
        public virtual string Value { get; set; }

        public SystemSetting() : base() { }

        public virtual int CompareTo(SystemSetting other) {
            if (other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }

        public override string ToString() {
            return Name;
        }
    }

    //Extension Class
    public static class SystemSettingExtension {
        public static bool HasMandatoryValues(this SystemSetting setting) {
            return setting.Name != null && setting.Name.Trim().Length > 0;
        }
    }
}
