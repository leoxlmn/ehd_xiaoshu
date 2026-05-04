using EHD.Model.Entity;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace EHD.Model.Mapping {
    public class PatientMap : ClassMap<Patient> {
        public PatientMap() {
            Id(x => x.Id).Column("PTN_PTNId")
                .GeneratedBy.HiLo("1");
            Map(x => x.FileNumber).Column("PTN_FileNumber")
                .Not.Nullable()
                .Unique()
                .Length(4);
            Map(x => x.FirstName).Column("PTN_FirstName")
                .Not.Nullable()
                .Length(50);
            Map(x => x.MiddleName).Column("PTN_MiddleName")
                .Length(50);
            Map(x => x.LastName).Column("PTN_LastName")
                .Not.Nullable()
                .Length(50);
            Map(x => x.DateOfBirth).Column("PTN_DateOfBirth")
                .Not.Nullable();
            Map(x => x.Sex).Column("PTN_Sex")
                .Not.Nullable();
            Map(x => x.FamilyPatientId).Column("PTN_FamilyPatientId");
            Map(x => x.Note).Column("PTN_Note")
                .Length(1000);
            Component(x => x.ContactInfo, m => {
                m.Component(a => a.Address, theAddr =>{
                    theAddr.Map(addr => addr.AddressLine1).Column("AddressLine1").Length(50);
                    theAddr.Map(addr => addr.AddressLine2).Column("AddressLine2").Length(50);
                    theAddr.Map(addr => addr.PostalCode).Column("PostalCode").Length(7);
                });
                m.Map(c => c.HomePhone).Column("HomePhone").Length(20);
                m.Map(c => c.CellPhone).Column("CellPhone").Length(20);
                m.Map(c => c.HomeFax).Column("HomeFax").Length(20);
                m.Map(c => c.Email).Column("Email").Length(50);
                });
            References(x => x.Insurer).Column("PTN_INSId")
                .Not.LazyLoad();

            HasMany(x => x.Invoices).KeyColumn("INV_PTNId")
                .Cascade.AllDeleteOrphan()
                .Inverse();
            HasMany(x => x.InitialTreatments).KeyColumn("ITR_PTNId")
                .Cascade.AllDeleteOrphan()
                .Inverse();
            HasMany(x => x.Deposits).KeyColumn("ACB_PTNId")
                .Cascade.AllDeleteOrphan()
                .Inverse();

            //EntityBase mappings
            Map(x => x.IsTestData).Column("PTN_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("PTN_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("PTN_UpdatedTime");
            Map(x => x.CreatedBy).Column("PTN_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("PTN_UpdatedBy");
            Version(x => x.Version).Column("PTN_Version");
        }
    }
}
