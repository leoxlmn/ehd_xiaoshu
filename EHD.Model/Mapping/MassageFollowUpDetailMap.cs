using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class MassageFollowUpDeailMap : ClassMap<MassageFollowUpDetail> {

        public MassageFollowUpDeailMap() {
            Id(x => x.Id).Column("MSD_MSDId")
                .GeneratedBy.HiLo("1");
            References(x => x.FollowUpTreatment).Column("MSD_FTRId")
                .Not.Nullable()
                .Not.LazyLoad();

            Map(x => x.TreatmentUsedStroking).Column("MSD_TreatmentUsedStroking");
            Map(x => x.TreatmentUsedRocking).Column("MSD_TreatmentUsedRocking");
            Map(x => x.TreatmentUsedEffleurage).Column("MSD_TreatmentUsedEffleurage");
            Map(x => x.TreatmentUsedPetrissage).Column("MSD_TreatmentUsedPetrissage");
            Map(x => x.TreatmentUsedFriction).Column("MSD_TreatmentUsedFriction");
            Map(x => x.TreatmentUsedVibration).Column("MSD_TreatmentUsedVibration");
            Map(x => x.TreatmentUsedTapotement).Column("MSD_TreatmentUsedTapotement");
            Map(x => x.TreatmentUsedFacial).Column("MSD_TreatmentUsedFacial");
            Map(x => x.TreatmentUsedMyoFacialTriggerPoint).Column("MSD_TreatmentUsedMyoFacialTriggerPoint");
            Map(x => x.TreatmentUsedHighGradeJointMobilization).Column("MSD_TreatmentUsedHighGradeJointMobilization");
            Map(x => x.TreatmentUsedLowGradeJointMobilization).Column("MSD_TreatmentUsedLowGradeJointMobilization");
            Map(x => x.TreatmentUsedStretch).Column("MSD_TreatmentUsedStretch");
            Map(x => x.TreatmentUsedIntraOral).Column("MSD_TreatmentUsedIntraOral");
            Map(x => x.TreatmentUsedBreastMassage).Column("MSD_TreatmentUsedBreastMassage");
            Map(x => x.TreatmentUsedOther).Column("MSD_TreatmentUsedOther");

            Map(x => x.TreatmentNote).Column("MSD_TreatmentNote");

            Map(x => x.TreatmentAreaBack).Column("MSD_TreatmentAreaBack");
            Map(x => x.TreatmentAreaNeck).Column("MSD_TreatmentAreaNeck");
            Map(x => x.TreatmentAreaShoulders).Column("MSD_TreatmentAreaShoulders");
            Map(x => x.TreatmentAreaFace).Column("MSD_TreatmentAreaFace");
            Map(x => x.TreatmentAreaLeftArm).Column("MSD_TreatmentAreaLeftArm");
            Map(x => x.TreatmentAreaRightArm).Column("MSD_TreatmentAreaRightArm");
            Map(x => x.TreatmentAreaLeftLeg).Column("MSD_TreatmentAreaLeftLeg");
            Map(x => x.TreatmentAreaRightLeg).Column("MSD_TreatmentAreaRightLeg");
            Map(x => x.TreatmentAreaGluteus).Column("MSD_TreatmentAreaGluteus");
            Map(x => x.TreatmentAreaAbdominals).Column("MSD_TreatmentAreaAbdominals");
            Map(x => x.TreatmentAreaChest).Column("MSD_TreatmentAreaChest");
            Map(x => x.TreatmentAreaBreast).Column("MSD_TreatmentAreaBreast");
            Map(x => x.TreatmentAreaOther).Column("MSD_TreatmentAreaOther");

            //EntityBase mappings
            Map(x => x.IsTestData).Column("MSD_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("MSD_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("MSD_UpdatedTime");
            Map(x => x.CreatedBy).Column("MSD_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("MSD_UpdatedBy");
            Version(x => x.Version).Column("MSD_Version");
        }
    }
}
