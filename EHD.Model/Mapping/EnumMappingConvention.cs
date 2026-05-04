using FluentNHibernate.Mapping;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using System;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Conventions.Inspections;
using EHD.Model.Entity;

namespace EHD.Model.Mapping {
    public class EnumMappingConvention : IUserTypeConvention {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria) {
            criteria.Expect(x => x.Property.PropertyType.IsEnum ||
                (x.Property.PropertyType.IsGenericType &&
                 x.Property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                 x.Property.PropertyType.GetGenericArguments()[0].IsEnum)
                );
        }

        public void Apply(IPropertyInstance target) {
            target.CustomType(target.Property.PropertyType);
        }
    }
}

