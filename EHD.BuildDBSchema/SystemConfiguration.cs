using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping;
using EHD.Model.Entity;

namespace EHD.BuildDBSchema {
    public class SystemConfiguration : DefaultAutomappingConfiguration {
        public override bool ShouldMap(Type type) {
            return type.Namespace == typeof(User).Namespace;
        }
    }
}
