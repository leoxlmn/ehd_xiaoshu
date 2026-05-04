using FluentNHibernate.Mapping;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using System;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Conventions.Inspections;
using EHD.Model.Entity;

namespace EHD.Model.Mapping {

    /// <summary>
    /// This convention is used to convert Entity Names to lowercase in the final query.
    /// If MySQL databases is on a Unix/Linux server, the table names maybe saved as lowercase. Table names are case sensitive.
    /// It's not necessary on MySQL databases on Windows system, as the table names are case insensitive.
    /// </summary>
    public class LowercaseTableNameConvention : IClassConvention {
        public void Apply(IClassInstance instance) {
            instance.Table(instance.EntityType.Name.ToLower());
        }
    }
}

