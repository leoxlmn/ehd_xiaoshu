using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class NaturopathicDeailMap : ClassMap<NaturopathicDetail> {

        public NaturopathicDeailMap() {
            Id(x => x.Id).Column("NPC_NPCId")
                .GeneratedBy.HiLo("1");
            References(x => x.InitialTreatment).Column("NPC_ITRId")
                .Not.Nullable()
                .Not.LazyLoad();

            Map(x => x.HealthState).Column("NPC_HealthState");
            Map(x => x.CurrentEnergyLevel).Column("NPC_CurrentEnergyLevel");
            Map(x => x.MorningEnergyLevel).Column("NPC_MorningEnergyLevel");
            Map(x => x.CurrentWeight).Column("NPC_CurrentWeight");
            Map(x => x.YearAgoWeight).Column("NPC_YearAgoWeight");
            Map(x => x.IdealWeight).Column("NPC_IdealWeight");
            Map(x => x.CurrentHeight).Column("NPC_CurrentHeight");

            //Stress Management
            Map(x => x.CurrentStressLevel).Column("NPC_CurrentStressLevel");
            Map(x => x.StressIdentifiedBy).Column("NPC_StressIdentifiedBy");
            Map(x => x.StressedBehavior).Column("NPC_StressedBehavior");
            Map(x => x.StressLocalizedOfBody).Column("NPC_StressLocalizedOfBody");
            Map(x => x.StressHandlingTools).Column("NPC_StressHandlingTools");

            //Allergies
            Map(x => x.HasDrugAllergy).Column("NPC_HasDrugAllergy");
            Map(x => x.DrugAllergies).Column("NPC_DrugAllergies");
            Map(x => x.HasFoodAllergy).Column("NPC_HasFoodAllergy");
            Map(x => x.FoodAllergies).Column("NPC_FoodAllergies");
            Map(x => x.HasAnimalAllergy).Column("NPC_HasAnimalAllergy");
            Map(x => x.AnimalAllergies).Column("NPC_AnimalAllergies");
            Map(x => x.HasPlantAllergy).Column("NPC_HasPlantAllergy");
            Map(x => x.PlantAllergies).Column("NPC_PlantAllergies");
            Map(x => x.HasEnvironmentalAllergy).Column("NPC_HasEnvironmentalAllergy");
            Map(x => x.EnvironmentalAllergies).Column("NPC_EnvironmentalAllergies");

            //Accidents/Hospitalizations
            Map(x => x.MajorInjuries).Column("NPC_MajorInjuries");
            Map(x => x.Surgeries).Column("NPC_Surgeries");
            Map(x => x.HadFluVaccineLastFiveYears).Column("NPC_HadFluVaccineLastFiveYears");
            Map(x => x.HadReactionToVaccine).Column("NPC_HadReactionToVaccine");

            //Current/Past conditions
            Map(x => x.C_Allergies).Column("NPC_C_Allergies");
            Map(x => x.P_Allergies).Column("NPC_P_Allergies");
            Map(x => x.C_Anemia).Column("NPC_C_Anemia");
            Map(x => x.P_Anemia).Column("NPC_P_Anemia");
            Map(x => x.C_Stroke).Column("NPC_C_Stroke");
            Map(x => x.P_Stroke).Column("NPC_P_Stroke");
            Map(x => x.C_Depression).Column("NPC_C_Depression");
            Map(x => x.P_Depression).Column("NPC_P_Depression");
            Map(x => x.C_Asthma).Column("NPC_C_Asthma");
            Map(x => x.P_Asthma).Column("NPC_P_Asthma");
            Map(x => x.C_Measles).Column("NPC_C_Measles");
            Map(x => x.P_Measles).Column("NPC_P_Measles");
            Map(x => x.C_HeartDisease).Column("NPC_C_HeartDisease");
            Map(x => x.P_HeartDisease).Column("NPC_P_HeartDisease");
            Map(x => x.C_EmotionalAbuse).Column("NPC_C_EmotionalAbuse");
            Map(x => x.P_EmotionalAbuse).Column("NPC_P_EmotionalAbuse");
            Map(x => x.C_Eczema).Column("NPC_C_Eczema");
            Map(x => x.P_Eczema).Column("NPC_P_Eczema");
            Map(x => x.C_Mumps).Column("NPC_C_Mumps");
            Map(x => x.P_Mumps).Column("NPC_P_Mumps");
            Map(x => x.C_RheumaticFever).Column("NPC_C_RheumaticFever");
            Map(x => x.P_RheumaticFever).Column("NPC_P_RheumaticFever");
            Map(x => x.C_PhysicalMentalAbuse).Column("NPC_C_PhysicalMentalAbuse");
            Map(x => x.P_PhysicalMentalAbuse).Column("NPC_P_PhysicalMentalAbuse");
            Map(x => x.C_Psoriasis).Column("NPC_C_Psoriasis");
            Map(x => x.P_Psoriasis).Column("NPC_P_Psoriasis");
            Map(x => x.C_ChickenPox).Column("NPC_C_ChickenPox");
            Map(x => x.P_ChickenPox).Column("NPC_P_ChickenPox");
            Map(x => x.C_HighBloodPressure).Column("NPC_C_HighBloodPressure");
            Map(x => x.P_HighBloodPressure).Column("NPC_P_HighBloodPressure");
            Map(x => x.C_NumbnessTingling).Column("NPC_C_NumbnessTingling");
            Map(x => x.P_NumbnessTingling).Column("NPC_P_NumbnessTingling");
            Map(x => x.C_HayFever).Column("NPC_C_HayFever");
            Map(x => x.P_HayFever).Column("NPC_P_HayFever");
            Map(x => x.C_WhoopingCough).Column("NPC_C_WhoopingCough");
            Map(x => x.P_WhoopingCough).Column("NPC_P_WhoopingCough");
            Map(x => x.C_HighCholesterol).Column("NPC_C_HighCholesterol");
            Map(x => x.P_HighCholesterol).Column("NPC_P_HighCholesterol");
            Map(x => x.C_ColdHandsFeet).Column("NPC_C_ColdHandsFeet");
            Map(x => x.P_ColdHandsFeet).Column("NPC_P_ColdHandsFeet");
            Map(x => x.C_Pneumonia).Column("NPC_C_Pneumonia");
            Map(x => x.P_Pneumonia).Column("NPC_P_Pneumonia");
            Map(x => x.C_Shingles).Column("NPC_C_Shingles");
            Map(x => x.P_Shingles).Column("NPC_P_Shingles");
            Map(x => x.C_Cancer).Column("NPC_C_Cancer");
            Map(x => x.P_Cancer).Column("NPC_P_Cancer");
            Map(x => x.C_ThyroidProblems).Column("NPC_C_ThyroidProblems");
            Map(x => x.P_ThyroidProblems).Column("NPC_P_ThyroidProblems");
            Map(x => x.C_EarInfections).Column("NPC_C_EarInfections");
            Map(x => x.P_EarInfections).Column("NPC_P_EarInfections");
            Map(x => x.C_Diphtheria).Column("NPC_C_Diphtheria");
            Map(x => x.P_Diphtheria).Column("NPC_P_Diphtheria");
            Map(x => x.C_Type1Diabetes).Column("NPC_C_Type1Diabetes");
            Map(x => x.P_Type1Diabetes).Column("NPC_P_Type1Diabetes");
            Map(x => x.C_Warts).Column("NPC_C_Warts");
            Map(x => x.P_Warts).Column("NPC_P_Warts");
            Map(x => x.C_StrepThroat).Column("NPC_C_StrepThroat");
            Map(x => x.P_StrepThroat).Column("NPC_P_StrepThroat");
            Map(x => x.C_ScarletFever).Column("NPC_C_ScarletFever");
            Map(x => x.P_ScarletFever).Column("NPC_P_ScarletFever");
            Map(x => x.C_Type2Diabetes).Column("NPC_C_Type2Diabetes");
            Map(x => x.P_Type2Diabetes).Column("NPC_P_Type2Diabetes");
            Map(x => x.C_Mono).Column("NPC_C_Mono");
            Map(x => x.P_Mono).Column("NPC_P_Mono");
            Map(x => x.C_Tonsillitis).Column("NPC_C_Tonsillitis");
            Map(x => x.P_Tonsillitis).Column("NPC_P_Tonsillitis");
            Map(x => x.C_Polio).Column("NPC_C_Polio");
            Map(x => x.P_Polio).Column("NPC_P_Polio");
            Map(x => x.C_KidneyDisease).Column("NPC_C_KidneyDisease");
            Map(x => x.P_KidneyDisease).Column("NPC_P_KidneyDisease");
            Map(x => x.C_RheumatoidArthritis).Column("NPC_C_RheumatoidArthritis");
            Map(x => x.P_RheumatoidArthritis).Column("NPC_P_RheumatoidArthritis");
            Map(x => x.C_CankerSores).Column("NPC_C_CankerSores");
            Map(x => x.P_CankerSores).Column("NPC_P_CankerSores");
            Map(x => x.C_Smallpox).Column("NPC_C_Smallpox");
            Map(x => x.P_Smallpox).Column("NPC_P_Smallpox");
            Map(x => x.C_VisualProblems).Column("NPC_C_VisualProblems");
            Map(x => x.P_VisualProblems).Column("NPC_P_VisualProblems");
            Map(x => x.C_Osteoarthritis).Column("NPC_C_Osteoarthritis");
            Map(x => x.P_Osteoarthritis).Column("NPC_P_Osteoarthritis");
            Map(x => x.C_Jaundice).Column("NPC_C_Jaundice");
            Map(x => x.P_Jaundice).Column("NPC_P_Jaundice");
            Map(x => x.C_Tuberculosis).Column("NPC_C_Tuberculosis");
            Map(x => x.P_Tuberculosis).Column("NPC_P_Tuberculosis");
            Map(x => x.C_AutoimmuneDisease).Column("NPC_C_AutoimmuneDisease");
            Map(x => x.P_AutoimmuneDisease).Column("NPC_P_AutoimmuneDisease");
            Map(x => x.C_WeightProblems).Column("NPC_C_WeightProblems");
            Map(x => x.P_WeightProblems).Column("NPC_P_WeightProblems");
            Map(x => x.C_Alcoholism).Column("NPC_C_Alcoholism");
            Map(x => x.P_Alcoholism).Column("NPC_P_Alcoholism");
            Map(x => x.C_Malaria).Column("NPC_C_Malaria");
            Map(x => x.P_Malaria).Column("NPC_P_Malaria");
            Map(x => x.C_Epilepsy).Column("NPC_C_Epilepsy");
            Map(x => x.P_Epilepsy).Column("NPC_P_Epilepsy");
            Map(x => x.C_Gout).Column("NPC_C_Gout");
            Map(x => x.P_Gout).Column("NPC_P_Gout");
            Map(x => x.C_Hepatitis).Column("NPC_C_Hepatitis");
            Map(x => x.P_Hepatitis).Column("NPC_P_Hepatitis");
            Map(x => x.C_Gallstones).Column("NPC_C_Gallstones");
            Map(x => x.P_Gallstones).Column("NPC_P_Gallstones");
            Map(x => x.C_VaricoseVeins).Column("NPC_C_VaricoseVeins");
            Map(x => x.P_VaricoseVeins).Column("NPC_P_VaricoseVeins");
            Map(x => x.CP_Other).Column("NPC_CP_Other");
            Map(x => x.CP_NeverWellSince).Column("NPC_CP_NeverWellSince");

            //Currently use
            Map(x => x.CU_Alcohol).Column("NPC_CU_Alcohol");
            Map(x => x.CU_Hormones).Column("NPC_CU_Hormones");
            Map(x => x.CU_Cortisone).Column("NPC_CU_Cortisone");
            Map(x => x.CU_Sedatives).Column("NPC_CU_Sedatives");
            Map(x => x.CU_Antacids).Column("NPC_CU_Antacids");
            Map(x => x.CU_Tobacco).Column("NPC_CU_Tobacco");
            Map(x => x.CU_Coffee).Column("NPC_CU_Coffee");
            Map(x => x.CU_BlackTea).Column("NPC_CU_BlackTea");
            Map(x => x.CU_Laxatives).Column("NPC_CU_Laxatives");

            //Current Prescription Medications
            Map(x => x.CPM_Name1).Column("NPC_CPM_Name1");
            Map(x => x.CPM_Reason1).Column("NPC_CPM_Reason1");
            Map(x => x.CPM_Amount1).Column("NPC_CPM_Amount1");
            Map(x => x.CPM_PrescriptionDate1).Column("NPC_CPM_PrescriptionDate1");
            Map(x => x.CPM_DrugCategory1).Column("NPC_CPM_DrugCategory1");
            Map(x => x.CPM_Name2).Column("NPC_CPM_Name2");
            Map(x => x.CPM_Reason2).Column("NPC_CPM_Reason2");
            Map(x => x.CPM_Amount2).Column("NPC_CPM_Amount2");
            Map(x => x.CPM_PrescriptionDate2).Column("NPC_CPM_PrescriptionDate2");
            Map(x => x.CPM_DrugCategory2).Column("NPC_CPM_DrugCategory2");
            Map(x => x.CPM_Name3).Column("NPC_CPM_Name3");
            Map(x => x.CPM_Reason3).Column("NPC_CPM_Reason3");
            Map(x => x.CPM_Amount3).Column("NPC_CPM_Amount3");
            Map(x => x.CPM_PrescriptionDate3).Column("NPC_CPM_PrescriptionDate3");
            Map(x => x.CPM_DrugCategory3).Column("NPC_CPM_DrugCategory3");
            Map(x => x.CPM_Name4).Column("NPC_CPM_Name4");
            Map(x => x.CPM_Reason4).Column("NPC_CPM_Reason4");
            Map(x => x.CPM_Amount4).Column("NPC_CPM_Amount4");
            Map(x => x.CPM_PrescriptionDate4).Column("NPC_CPM_PrescriptionDate4");
            Map(x => x.CPM_DrugCategory4).Column("NPC_CPM_DrugCategory4");
            Map(x => x.CPM_Name5).Column("NPC_CPM_Name5");
            Map(x => x.CPM_Reason5).Column("NPC_CPM_Reason5");
            Map(x => x.CPM_Amount5).Column("NPC_CPM_Amount5");
            Map(x => x.CPM_PrescriptionDate5).Column("NPC_CPM_PrescriptionDate5");
            Map(x => x.CPM_DrugCategory5).Column("NPC_CPM_DrugCategory5");

            //Vitamins/Herbs
            Map(x => x.VH_Name1).Column("NPC_VH_Name1");
            Map(x => x.VH_Brand1).Column("NPC_VH_Brand1");
            Map(x => x.VH_Amount1).Column("NPC_VH_Amount1");
            Map(x => x.VH_Reason1).Column("NPC_VH_Reason1");
            Map(x => x.VH_SuggestedBy1).Column("NPC_VH_SuggestedBy1");
            Map(x => x.VH_Helped1).Column("NPC_VH_Helped1");
            Map(x => x.VH_Name2).Column("NPC_VH_Name2");
            Map(x => x.VH_Brand2).Column("NPC_VH_Brand2");
            Map(x => x.VH_Amount2).Column("NPC_VH_Amount2");
            Map(x => x.VH_Reason2).Column("NPC_VH_Reason2");
            Map(x => x.VH_SuggestedBy2).Column("NPC_VH_SuggestedBy2");
            Map(x => x.VH_Helped2).Column("NPC_VH_Helped2");
            Map(x => x.VH_Name3).Column("NPC_VH_Name3");
            Map(x => x.VH_Brand3).Column("NPC_VH_Brand3");
            Map(x => x.VH_Amount3).Column("NPC_VH_Amount3");
            Map(x => x.VH_Reason3).Column("NPC_VH_Reason3");
            Map(x => x.VH_SuggestedBy3).Column("NPC_VH_SuggestedBy3");
            Map(x => x.VH_Helped3).Column("NPC_VH_Helped3");
            Map(x => x.VH_Name4).Column("NPC_VH_Name4");
            Map(x => x.VH_Brand4).Column("NPC_VH_Brand4");
            Map(x => x.VH_Amount4).Column("NPC_VH_Amount4");
            Map(x => x.VH_Reason4).Column("NPC_VH_Reason4");
            Map(x => x.VH_SuggestedBy4).Column("NPC_VH_SuggestedBy4");
            Map(x => x.VH_Helped4).Column("NPC_VH_Helped4");
            Map(x => x.VH_Name5).Column("NPC_VH_Name5");
            Map(x => x.VH_Brand5).Column("NPC_VH_Brand5");
            Map(x => x.VH_Amount5).Column("NPC_VH_Amount5");
            Map(x => x.VH_Reason5).Column("NPC_VH_Reason5");
            Map(x => x.VH_SuggestedBy5).Column("NPC_VH_SuggestedBy5");
            Map(x => x.VH_Helped5).Column("NPC_VH_Helped5");

            Map(x => x.VH_HasOtherSupplementation).Column("NPC_VH_HasOtherSupplementation");
            Map(x => x.VH_OtherSupplementation).Column("NPC_VH_OtherSupplementation");

            //Family History
            Map(x => x.FH_CancerMother).Column("NPC_FH_CancerMother");
            Map(x => x.FH_CancerFather).Column("NPC_FH_CancerFather");
            Map(x => x.FH_CancerSibling).Column("NPC_FH_CancerSibling");
            Map(x => x.FH_CancerGrandparent).Column("NPC_FH_CancerGrandparent");

            Map(x => x.FH_TuberculosisMother).Column("NPC_FH_TuberculosisMother");
            Map(x => x.FH_TuberculosisFather).Column("NPC_FH_TuberculosisFather");
            Map(x => x.FH_TuberculosisSibling).Column("NPC_FH_TuberculosisSibling");
            Map(x => x.FH_TuberculosisGrandparent).Column("NPC_FH_TuberculosisGrandparent");

            Map(x => x.FH_HeartDiseaseMother).Column("NPC_FH_HeartDiseaseMother");
            Map(x => x.FH_HeartDiseaseFather).Column("NPC_FH_HeartDiseaseFather");
            Map(x => x.FH_HeartDiseaseSibling).Column("NPC_FH_HeartDiseaseSibling");
            Map(x => x.FH_HeartDiseaseGrandparent).Column("NPC_FH_HeartDiseaseGrandparent");

            Map(x => x.FH_StrokeMother).Column("NPC_FH_StrokeMother");
            Map(x => x.FH_StrokeFather).Column("NPC_FH_StrokeFather");
            Map(x => x.FH_StrokeSibling).Column("NPC_FH_StrokeSibling");
            Map(x => x.FH_StrokeGrandparent).Column("NPC_FH_StrokeGrandparent");

            Map(x => x.FH_HighBloodPressureMother).Column("NPC_FH_HighBloodPressureMother");
            Map(x => x.FH_HighBloodPressureFather).Column("NPC_FH_HighBloodPressureFather");
            Map(x => x.FH_HighBloodPressureSibling).Column("NPC_FH_HighBloodPressureSibling");
            Map(x => x.FH_HighBloodPressureGrandparent).Column("NPC_FH_HighBloodPressureGrandparent");

            Map(x => x.FH_HighCholesterolMother).Column("NPC_FH_HighCholesterolMother");
            Map(x => x.FH_HighCholesterolFather).Column("NPC_FH_HighCholesterolFather");
            Map(x => x.FH_HighCholesterolSibling).Column("NPC_FH_HighCholesterolSibling");
            Map(x => x.FH_HighCholesterolGrandparent).Column("NPC_FH_HighCholesterolGrandparent");

            Map(x => x.FH_KidneyDiseaseMother).Column("NPC_FH_KidneyDiseaseMother");
            Map(x => x.FH_KidneyDiseaseFather).Column("NPC_FH_KidneyDiseaseFather");
            Map(x => x.FH_KidneyDiseaseSibling).Column("NPC_FH_KidneyDiseaseSibling");
            Map(x => x.FH_KidneyDiseaseGrandparent).Column("NPC_FH_KidneyDiseaseGrandparent");

            Map(x => x.FH_RheumatoidMother).Column("NPC_FH_RheumatoidMother");
            Map(x => x.FH_RheumatoidFather).Column("NPC_FH_RheumatoidFather");
            Map(x => x.FH_RheumatoidSibling).Column("NPC_FH_RheumatoidSibling");
            Map(x => x.FH_RheumatoidGrandparent).Column("NPC_FH_RheumatoidGrandparent");

            Map(x => x.FH_OsteoarthritisMother).Column("NPC_FH_OsteoarthritisMother");
            Map(x => x.FH_OsteoarthritisFather).Column("NPC_FH_OsteoarthritisFather");
            Map(x => x.FH_OsteoarthritisSibling).Column("NPC_FH_OsteoarthritisSibling");
            Map(x => x.FH_OsteoarthritisGrandparent).Column("NPC_FH_OsteoarthritisGrandparent");

            Map(x => x.FH_AllergiesMother).Column("NPC_FH_AllergiesMother");
            Map(x => x.FH_AllergiesFather).Column("NPC_FH_AllergiesFather");
            Map(x => x.FH_AllergiesSibling).Column("NPC_FH_AllergiesSibling");
            Map(x => x.FH_AllergiesGrandparent).Column("NPC_FH_AllergiesGrandparent");

            Map(x => x.FH_AsthmaMother).Column("NPC_FH_AsthmaMother");
            Map(x => x.FH_AsthmaFather).Column("NPC_FH_AsthmaFather");
            Map(x => x.FH_AsthmaSibling).Column("NPC_FH_AsthmaSibling");
            Map(x => x.FH_AsthmaGrandparent).Column("NPC_FH_AsthmaGrandparent");

            Map(x => x.FH_DiabetesTypeIMother).Column("NPC_FH_DiabetesTypeIMother");
            Map(x => x.FH_DiabetesTypeIFather).Column("NPC_FH_DiabetesTypeIFather");
            Map(x => x.FH_DiabetesTypeISibling).Column("NPC_FH_DiabetesTypeISibling");
            Map(x => x.FH_DiabetesTypeIGrandparent).Column("NPC_FH_DiabetesTypeIGrandparent");

            Map(x => x.FH_DiabetesTypeIIMother).Column("NPC_FH_DiabetesTypeIIMother");
            Map(x => x.FH_DiabetesTypeIIFather).Column("NPC_FH_DiabetesTypeIIFather");
            Map(x => x.FH_DiabetesTypeIISibling).Column("NPC_FH_DiabetesTypeIISibling");
            Map(x => x.FH_DiabetesTypeIIGrandparent).Column("NPC_FH_DiabetesTypeIIGrandparent");

            Map(x => x.FH_DepressionMother).Column("NPC_FH_DepressionMother");
            Map(x => x.FH_DepressionFather).Column("NPC_FH_DepressionFather");
            Map(x => x.FH_DepressionSibling).Column("NPC_FH_DepressionSibling");
            Map(x => x.FH_DepressionGrandparent).Column("NPC_FH_DepressionGrandparent");

            Map(x => x.FH_OtherName).Column("NPC_FH_OtherName");
            Map(x => x.FH_OtherMother).Column("NPC_FH_OtherMother");
            Map(x => x.FH_OtherFather).Column("NPC_FH_OtherFather");
            Map(x => x.FH_OtherSibling).Column("NPC_FH_OtherSibling");
            Map(x => x.FH_OtherGrandparent).Column("NPC_FH_OtherGrandparent");

            //Personal Habits
            Map(x => x.PH_EnjoyMost).Column("NPC_PH_EnjoyMost");
            Map(x => x.PH_MainInterests).Column("NPC_PH_MainInterests");
            Map(x => x.PH_WorryMost).Column("NPC_PH_WorryMost");
            Map(x => x.PH_WhatNurtures).Column("NPC_PH_WhatNurtures");
            Map(x => x.PH_DoExercise).Column("NPC_PH_DoExercise");
            Map(x => x.PH_ExerciseDetails).Column("NPC_PH_ExerciseDetails");
            Map(x => x.PH_ReligiousPractice).Column("NPC_PH_ReligiousPractice");
            Map(x => x.PH_BodyTemperature).Column("NPC_PH_BodyTemperature");
            Map(x => x.PH_EnjoyWork).Column("NPC_PH_EnjoyWork");
            Map(x => x.PH_TakeVacations).Column("NPC_PH_TakeVacations");
            Map(x => x.PH_ColdFluDetails).Column("NPC_PH_ColdFluDetails");

            //Sleep Habits
            Map(x => x.SH_SleepQuality).Column("NPC_SH_SleepQuality");
            Map(x => x.SH_FallingAsleepProblem).Column("NPC_SH_FallingAsleepProblem");
            Map(x => x.SH_StayingAsleepProblem).Column("NPC_SH_StayingAsleepProblem");
            Map(x => x.SH_HoursInSleep).Column("NPC_SH_HoursInSleep");
            Map(x => x.SH_HoursNeededInSleep).Column("NPC_SH_HoursNeededInSleep");
            Map(x => x.SH_WakeRefreshed).Column("NPC_SH_WakeRefreshed");
            Map(x => x.SH_TakeNap).Column("NPC_SH_TakeNap");
            Map(x => x.SH_NapDuration).Column("NPC_SH_NapDuration");

            //Female
            Map(x => x.F_FirstMensesAge).Column("NPC_F_FirstMensesAge");
            Map(x => x.F_PeriodsStoppedAge).Column("NPC_F_PeriodsStoppedAge");
            Map(x => x.F_CyclesRegular).Column("NPC_F_CyclesRegular");
            Map(x => x.F_PeriodBeginsEveryDays).Column("NPC_F_PeriodBeginsEveryDays");
            Map(x => x.F_PeriodLastsDays).Column("NPC_F_PeriodLastsDays");
            Map(x => x.F_PeriodFlow).Column("NPC_F_PeriodFlow");
            Map(x => x.F_PeriodBloodColor).Column("NPC_F_PeriodBloodColor");
            Map(x => x.F_AnyClots).Column("NPC_F_AnyClots");
            Map(x => x.F_AnyCramps).Column("NPC_F_AnyCramps");
            Map(x => x.F_AnySpottingBleeding).Column("NPC_F_AnySpottingBleeding");
            Map(x => x.F_SpottingBleedingEveryMonth).Column("NPC_F_SpottingBleedingEveryMonth");
            Map(x => x.F_YeastInfectionInPast).Column("NPC_F_YeastInfectionInPast");
            Map(x => x.F_YeastInfectionFrequency).Column("NPC_F_YeastInfectionFrequency");
            Map(x => x.F_YeastInfectionTreatment).Column("NPC_F_YeastInfectionTreatment");
            Map(x => x.F_AnyPremenstrualSymptoms).Column("NPC_F_AnyPremenstrualSymptoms");
            Map(x => x.F_PremenstrualSymptomsDetails).Column("NPC_F_PremenstrualSymptomsDetails");
            Map(x => x.F_NumberOfPregnancies).Column("NPC_F_NumberOfPregnancies");
            Map(x => x.F_NumberOfMiscarriages).Column("NPC_F_NumberOfMiscarriages");
            Map(x => x.F_NumberOfLiveBirths).Column("NPC_F_NumberOfLiveBirths");
            Map(x => x.F_AnyPregancyProblem).Column("NPC_F_AnyPregancyProblem");
            Map(x => x.F_RegularPAP).Column("NPC_F_RegularPAP");
            Map(x => x.F_AnyAbnormalPAP).Column("NPC_F_AnyAbnormalPAP");
            Map(x => x.F_RegularBreastSelfExam).Column("NPC_F_RegularBreastSelfExam");
            Map(x => x.F_NoticedBreastLumps).Column("NPC_F_NoticedBreastLumps");

            //Digestion and Elimination
            Map(x => x.DE_ProblemWithGasBloating).Column("NPC_DE_ProblemWithGasBloating");
            Map(x => x.DE_Hemorrhoids).Column("NPC_DE_Hemorrhoids");
            Map(x => x.DE_RectalBleeding).Column("NPC_DE_RectalBleeding");
            Map(x => x.DE_Parasites).Column("NPC_DE_Parasites");
            Map(x => x.DE_ProblemFrequency).Column("NPC_DE_ProblemFrequency");
            Map(x => x.DE_ProblemSeverity).Column("NPC_DE_ProblemSeverity");
            Map(x => x.DE_ProblemDuration).Column("NPC_DE_ProblemDuration");
            Map(x => x.DE_BowelMovements).Column("NPC_DE_BowelMovements");
            Map(x => x.DE_AnyBloodInStool).Column("NPC_DE_AnyBloodInStool");
            Map(x => x.DE_HadBlackTarryGrayStool).Column("NPC_DE_HadBlackTarryGrayStool");
            Map(x => x.DE_HadYellowLightColoredStool).Column("NPC_DE_HadYellowLightColoredStool");
            Map(x => x.DE_HadRectalItching).Column("NPC_DE_HadRectalItching");
            Map(x => x.DE_StoolsFormedOrLoose).Column("NPC_DE_StoolsFormedOrLoose");
            Map(x => x.DE_HadConstipationDiarrhea).Column("NPC_DE_HadConstipationDiarrhea");
            Map(x => x.DE_ConstipationDiarrheaDetails).Column("NPC_DE_ConstipationDiarrheaDetails");
            Map(x => x.DE_HaveToStrainToPassStool).Column("NPC_DE_HaveToStrainToPassStool");
            Map(x => x.DE_HaveToStrainDetails).Column("NPC_DE_HaveToStrainDetails");
            Map(x => x.DE_PassGasFrequently).Column("NPC_DE_PassGasFrequently");
            Map(x => x.DE_BurpFrequently).Column("NPC_DE_BurpFrequently");
            Map(x => x.DE_StrongDisagreeableOdor).Column("NPC_DE_StrongDisagreeableOdor");
            Map(x => x.DE_TraveledOutsideOfCanadaPast5Year).Column("NPC_DE_TraveledOutsideOfCanadaPast5Year");
            Map(x => x.DE_TraveledCountries).Column("NPC_DE_TraveledCountries");
            Map(x => x.DE_BeenCammpingPast5Year).Column("NPC_DE_BeenCammpingPast5Year");
            Map(x => x.DE_HadFasted).Column("NPC_DE_HadFasted");
            Map(x => x.DE_FastType).Column("NPC_DE_FastType");

            //EntityBase mappings
            Map(x => x.IsTestData).Column("NPC_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("NPC_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("NPC_UpdatedTime");
            Map(x => x.CreatedBy).Column("NPC_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("NPC_UpdatedBy");
            Version(x => x.Version).Column("NPC_Version");
        }
    }
}
