using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class OsteopathyFollowUpDeailMap : ClassMap<OsteopathyFollowUpDetail> {

        public OsteopathyFollowUpDeailMap() {
            Id(x => x.Id).Column("OSF_OSFId")
                .GeneratedBy.HiLo("1");
            References(x => x.FollowUpTreatment).Column("OSF_FTRId")
                .Not.Nullable()
                .Not.LazyLoad();

            Map(x => x.TreatmentUsedRocking).Column("OSF_TreatmentUsedRocking");
            Map(x => x.TreatmentUsedPetrissage).Column("OSF_TreatmentUsedPetrissage");
            Map(x => x.TreatmentUsedFriction).Column("OSF_TreatmentUsedFriction");
            Map(x => x.TreatmentUsedVibration).Column("OSF_TreatmentUsedVibration");
            Map(x => x.TreatmentUsedTapotement).Column("OSF_TreatmentUsedTapotement");
            Map(x => x.TreatmentUsedMyofacialRelease).Column("OSF_TreatmentUsedMyofacialRelease");
            Map(x => x.TreatmentUsedTenderPointRelease).Column("OSF_TreatmentUsedTenderPointRelease");
            Map(x => x.TreatmentUsedMuscleEnergy).Column("OSF_TreatmentUsedMuscleEnergy");
            Map(x => x.TreatmentUsedTotalBodyAdjustment).Column("OSF_TreatmentUsedTotalBodyAdjustment");
            Map(x => x.TreatmentUsedStillTechnique).Column("OSF_TreatmentUsedStillTechnique");
            Map(x => x.TreatmentUsedCraniosacral).Column("OSF_TreatmentUsedCraniosacral");
            Map(x => x.TreatmentUsedVisceral).Column("OSF_TreatmentUsedVisceral");
            Map(x => x.TreatmentUsedSoftTissueJointMobilization).Column("OSF_TreatmentUsedSoftTissueJointMobilization");
            Map(x => x.TreatmentUsedStretch).Column("OSF_TreatmentUsedStretch");
            Map(x => x.TreatmentUsedIntraOral).Column("OSF_TreatmentUsedIntraOral");
            Map(x => x.TreatmentUsedOther).Column("OSF_TreatmentUsedOther");

            Map(x => x.TreatmentNote).Column("OSF_TreatmentNote");

            Map(x => x.TreatmentAreaBack).Column("OSF_TreatmentAreaBack");
            Map(x => x.TreatmentAreaNeck).Column("OSF_TreatmentAreaNeck");
            Map(x => x.TreatmentAreaShoulders).Column("OSF_TreatmentAreaShoulders");
            Map(x => x.TreatmentAreaFace).Column("OSF_TreatmentAreaFace");
            Map(x => x.TreatmentAreaLeftArm).Column("OSF_TreatmentAreaLeftArm");
            Map(x => x.TreatmentAreaRightArm).Column("OSF_TreatmentAreaRightArm");
            Map(x => x.TreatmentAreaLeftLeg).Column("OSF_TreatmentAreaLeftLeg");
            Map(x => x.TreatmentAreaRightLeg).Column("OSF_TreatmentAreaRightLeg");
            Map(x => x.TreatmentAreaGluteus).Column("OSF_TreatmentAreaGluteus");
            Map(x => x.TreatmentAreaAbdominals).Column("OSF_TreatmentAreaAbdominals");
            Map(x => x.TreatmentAreaChest).Column("OSF_TreatmentAreaChest");
            Map(x => x.TreatmentAreaBreast).Column("OSF_TreatmentAreaBreast");
            Map(x => x.TreatmentAreaOther).Column("OSF_TreatmentAreaOther");

            //EntityBase mappings
            Map(x => x.IsTestData).Column("OSF_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("OSF_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("OSF_UpdatedTime");
            Map(x => x.CreatedBy).Column("OSF_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("OSF_UpdatedBy");
            Version(x => x.Version).Column("OSF_Version");
        }
    }
}
