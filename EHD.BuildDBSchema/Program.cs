using System;
using EHD.Model.Mapping;
using EHD.Model.Entity;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using NHibernate.Tool.hbm2ddl;

namespace EHD.BuildDBSchema {
    class Program {
        static void Main(string[] args) {
            var cfg = new SystemConfiguration();

            AutoPersistenceModel model = AutoMap.AssemblyOf<User>(cfg);
            
            model.Conventions.Add<EnumMappingConvention>()
                .IgnoreBase<EntityBase>();

            var config = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2005)
                .Mappings(m => m.AutoMappings.Add(model))
                .BuildConfiguration();

            var exporter = new SchemaExport(config);
            exporter.Execute(true, false, false);

            Console.Write("Hit enter to exit:");
            Console.ReadLine();
        }
    }
}
