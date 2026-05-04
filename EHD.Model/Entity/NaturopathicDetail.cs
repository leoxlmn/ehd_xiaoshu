using System;
using System.Collections.Generic;

namespace EHD.Model.Entity {
    [Serializable]
    public class NaturopathicDetail : EntityBase, IComparable<NaturopathicDetail>, ITreatmentDetail {

        public virtual InitialTreatment InitialTreatment { get; set; }

        //Current Health
        public virtual HealthState HealthState { get; set; }
        public virtual uint CurrentEnergyLevel { get; set; }
        public virtual uint MorningEnergyLevel { get; set; }
        public virtual string CurrentWeight { get; set; }
        public virtual string YearAgoWeight { get; set; }
        public virtual string IdealWeight { get; set; }
        public virtual string CurrentHeight { get; set; }

        //Stress Management
        public virtual uint CurrentStressLevel { get; set; }
        public virtual SelfOther StressIdentifiedBy { get; set; }
        public virtual string StressedBehavior { get; set; }
        public virtual string StressLocalizedOfBody { get; set; }
        public virtual string StressHandlingTools { get; set; }

        //Allergies
        public virtual bool? HasDrugAllergy { get; set; }
        public virtual string DrugAllergies { get; set; }
        public virtual bool? HasFoodAllergy { get; set; }
        public virtual string FoodAllergies { get; set; }
        public virtual bool? HasAnimalAllergy { get; set; }
        public virtual string AnimalAllergies { get; set; }
        public virtual bool? HasPlantAllergy { get; set; }
        public virtual string PlantAllergies { get; set; }
        public virtual bool? HasEnvironmentalAllergy { get; set; }
        public virtual string EnvironmentalAllergies { get; set; }

        //Accidents/Hospitalizations
        public virtual string MajorInjuries { get; set; }
        public virtual string Surgeries { get; set; }
        public virtual bool? HadFluVaccineLastFiveYears { get; set; }
        public virtual bool? HadReactionToVaccine { get; set; }

        //Current/Past conditions
        public virtual bool? C_Allergies { get; set; }
        public virtual bool? P_Allergies { get; set; }
        public virtual bool? C_Anemia { get; set; }
        public virtual bool? P_Anemia { get; set; }
        public virtual bool? C_Stroke { get; set; }
        public virtual bool? P_Stroke { get; set; }
        public virtual bool? C_Depression { get; set; }
        public virtual bool? P_Depression { get; set; }
        public virtual bool? C_Asthma { get; set; }
        public virtual bool? P_Asthma { get; set; }
        public virtual bool? C_Measles { get; set; }
        public virtual bool? P_Measles { get; set; }
        public virtual bool? C_HeartDisease { get; set; }
        public virtual bool? P_HeartDisease { get; set; }
        public virtual bool? C_EmotionalAbuse { get; set; }
        public virtual bool? P_EmotionalAbuse { get; set; }
        public virtual bool? C_Eczema { get; set; }
        public virtual bool? P_Eczema { get; set; }
        public virtual bool? C_Mumps { get; set; }
        public virtual bool? P_Mumps { get; set; }
        public virtual bool? C_RheumaticFever { get; set; }
        public virtual bool? P_RheumaticFever { get; set; }
        public virtual bool? C_PhysicalMentalAbuse { get; set; }
        public virtual bool? P_PhysicalMentalAbuse { get; set; }
        public virtual bool? C_Psoriasis { get; set; }
        public virtual bool? P_Psoriasis { get; set; }
        public virtual bool? C_ChickenPox { get; set; }
        public virtual bool? P_ChickenPox { get; set; }
        public virtual bool? C_HighBloodPressure { get; set; }
        public virtual bool? P_HighBloodPressure { get; set; }
        public virtual bool? C_NumbnessTingling { get; set; }
        public virtual bool? P_NumbnessTingling { get; set; }
        public virtual bool? C_HayFever { get; set; }
        public virtual bool? P_HayFever { get; set; }
        public virtual bool? C_WhoopingCough { get; set; }
        public virtual bool? P_WhoopingCough { get; set; }
        public virtual bool? C_HighCholesterol { get; set; }
        public virtual bool? P_HighCholesterol { get; set; }
        public virtual bool? C_ColdHandsFeet { get; set; }
        public virtual bool? P_ColdHandsFeet { get; set; }
        public virtual bool? C_Pneumonia { get; set; }
        public virtual bool? P_Pneumonia { get; set; }
        public virtual bool? C_Shingles { get; set; }
        public virtual bool? P_Shingles { get; set; }
        public virtual bool? C_Cancer { get; set; }
        public virtual bool? P_Cancer { get; set; }
        public virtual bool? C_ThyroidProblems { get; set; }
        public virtual bool? P_ThyroidProblems { get; set; }
        public virtual bool? C_EarInfections { get; set; }
        public virtual bool? P_EarInfections { get; set; }
        public virtual bool? C_Diphtheria { get; set; }
        public virtual bool? P_Diphtheria { get; set; }
        public virtual bool? C_Type1Diabetes { get; set; }
        public virtual bool? P_Type1Diabetes { get; set; }
        public virtual bool? C_Warts { get; set; }
        public virtual bool? P_Warts { get; set; }
        public virtual bool? C_StrepThroat { get; set; }
        public virtual bool? P_StrepThroat { get; set; }
        public virtual bool? C_ScarletFever { get; set; }
        public virtual bool? P_ScarletFever { get; set; }
        public virtual bool? C_Type2Diabetes { get; set; }
        public virtual bool? P_Type2Diabetes { get; set; }
        public virtual bool? C_Mono { get; set; }
        public virtual bool? P_Mono { get; set; }
        public virtual bool? C_Tonsillitis { get; set; }
        public virtual bool? P_Tonsillitis { get; set; }
        public virtual bool? C_Polio { get; set; }
        public virtual bool? P_Polio { get; set; }
        public virtual bool? C_KidneyDisease { get; set; }
        public virtual bool? P_KidneyDisease { get; set; }
        public virtual bool? C_RheumatoidArthritis { get; set; }
        public virtual bool? P_RheumatoidArthritis { get; set; }
        public virtual bool? C_CankerSores { get; set; }
        public virtual bool? P_CankerSores { get; set; }
        public virtual bool? C_Smallpox { get; set; }
        public virtual bool? P_Smallpox { get; set; }
        public virtual bool? C_VisualProblems { get; set; }
        public virtual bool? P_VisualProblems { get; set; }
        public virtual bool? C_Osteoarthritis { get; set; }
        public virtual bool? P_Osteoarthritis { get; set; }
        public virtual bool? C_Jaundice { get; set; }
        public virtual bool? P_Jaundice { get; set; }
        public virtual bool? C_Tuberculosis { get; set; }
        public virtual bool? P_Tuberculosis { get; set; }
        public virtual bool? C_AutoimmuneDisease { get; set; }
        public virtual bool? P_AutoimmuneDisease { get; set; }
        public virtual bool? C_WeightProblems { get; set; }
        public virtual bool? P_WeightProblems { get; set; }
        public virtual bool? C_Alcoholism { get; set; }
        public virtual bool? P_Alcoholism { get; set; }
        public virtual bool? C_Malaria { get; set; }
        public virtual bool? P_Malaria { get; set; }
        public virtual bool? C_Epilepsy { get; set; }
        public virtual bool? P_Epilepsy { get; set; }
        public virtual bool? C_Gout { get; set; }
        public virtual bool? P_Gout { get; set; }
        public virtual bool? C_Hepatitis { get; set; }
        public virtual bool? P_Hepatitis { get; set; }
        public virtual bool? C_Gallstones { get; set; }
        public virtual bool? P_Gallstones { get; set; }
        public virtual bool? C_VaricoseVeins { get; set; }
        public virtual bool? P_VaricoseVeins { get; set; }
        public virtual string CP_Other { get; set; }
        public virtual string CP_NeverWellSince { get; set; }

        //Currently use
        public virtual string CU_Alcohol { get; set; }
        public virtual string CU_Hormones { get; set; }
        public virtual string CU_Cortisone { get; set; }
        public virtual string CU_Sedatives { get; set; }
        public virtual string CU_Antacids { get; set; }
        public virtual string CU_Tobacco { get; set; }
        public virtual string CU_Coffee { get; set; }
        public virtual string CU_BlackTea { get; set; }
        public virtual string CU_Laxatives { get; set; }

        //Current Prescription Medications
        public virtual string CPM_Name1 { get; set; }
        public virtual string CPM_Reason1 { get; set; }
        public virtual string CPM_Amount1 { get; set; }
        public virtual string CPM_PrescriptionDate1 { get; set; }
        public virtual string CPM_DrugCategory1 { get; set; }
        public virtual string CPM_Name2 { get; set; }
        public virtual string CPM_Reason2 { get; set; }
        public virtual string CPM_Amount2 { get; set; }
        public virtual string CPM_PrescriptionDate2 { get; set; }
        public virtual string CPM_DrugCategory2 { get; set; }
        public virtual string CPM_Name3 { get; set; }
        public virtual string CPM_Reason3 { get; set; }
        public virtual string CPM_Amount3 { get; set; }
        public virtual string CPM_PrescriptionDate3 { get; set; }
        public virtual string CPM_DrugCategory3 { get; set; }
        public virtual string CPM_Name4 { get; set; }
        public virtual string CPM_Reason4 { get; set; }
        public virtual string CPM_Amount4 { get; set; }
        public virtual string CPM_PrescriptionDate4 { get; set; }
        public virtual string CPM_DrugCategory4 { get; set; }
        public virtual string CPM_Name5 { get; set; }
        public virtual string CPM_Reason5 { get; set; }
        public virtual string CPM_Amount5 { get; set; }
        public virtual string CPM_PrescriptionDate5 { get; set; }
        public virtual string CPM_DrugCategory5 { get; set; }

        //Vitamins/Herbs
        public virtual string VH_Name1 { get; set; }
        public virtual string VH_Brand1 { get; set; }
        public virtual string VH_Amount1 { get; set; }
        public virtual string VH_Reason1 { get; set; }
        public virtual SelfOther VH_SuggestedBy1 { get; set; }
        public virtual YesNo VH_Helped1 { get; set; }
        public virtual string VH_Name2 { get; set; }
        public virtual string VH_Brand2 { get; set; }
        public virtual string VH_Amount2 { get; set; }
        public virtual string VH_Reason2 { get; set; }
        public virtual SelfOther VH_SuggestedBy2 { get; set; }
        public virtual YesNo VH_Helped2 { get; set; }
        public virtual string VH_Name3 { get; set; }
        public virtual string VH_Brand3 { get; set; }
        public virtual string VH_Amount3 { get; set; }
        public virtual string VH_Reason3 { get; set; }
        public virtual SelfOther VH_SuggestedBy3 { get; set; }
        public virtual YesNo VH_Helped3 { get; set; }
        public virtual string VH_Name4 { get; set; }
        public virtual string VH_Brand4 { get; set; }
        public virtual string VH_Amount4 { get; set; }
        public virtual string VH_Reason4 { get; set; }
        public virtual SelfOther VH_SuggestedBy4 { get; set; }
        public virtual YesNo VH_Helped4 { get; set; }
        public virtual string VH_Name5 { get; set; }
        public virtual string VH_Brand5 { get; set; }
        public virtual string VH_Amount5 { get; set; }
        public virtual string VH_Reason5 { get; set; }
        public virtual SelfOther VH_SuggestedBy5 { get; set; }
        public virtual YesNo VH_Helped5 { get; set; }

        public virtual bool? VH_HasOtherSupplementation { get; set; }
        public virtual string VH_OtherSupplementation { get; set; }

        //Family History
        public virtual string FH_CancerMother { get; set; }
        public virtual string FH_CancerFather { get; set; }
        public virtual string FH_CancerSibling { get; set; }
        public virtual string FH_CancerGrandparent { get; set; }

        public virtual string FH_TuberculosisMother { get; set; }
        public virtual string FH_TuberculosisFather { get; set; }
        public virtual string FH_TuberculosisSibling { get; set; }
        public virtual string FH_TuberculosisGrandparent { get; set; }

        public virtual string FH_HeartDiseaseMother { get; set; }
        public virtual string FH_HeartDiseaseFather { get; set; }
        public virtual string FH_HeartDiseaseSibling { get; set; }
        public virtual string FH_HeartDiseaseGrandparent { get; set; }

        public virtual string FH_StrokeMother { get; set; }
        public virtual string FH_StrokeFather { get; set; }
        public virtual string FH_StrokeSibling { get; set; }
        public virtual string FH_StrokeGrandparent { get; set; }

        public virtual string FH_HighBloodPressureMother { get; set; }
        public virtual string FH_HighBloodPressureFather { get; set; }
        public virtual string FH_HighBloodPressureSibling { get; set; }
        public virtual string FH_HighBloodPressureGrandparent { get; set; }

        public virtual string FH_HighCholesterolMother { get; set; }
        public virtual string FH_HighCholesterolFather { get; set; }
        public virtual string FH_HighCholesterolSibling { get; set; }
        public virtual string FH_HighCholesterolGrandparent { get; set; }

        public virtual string FH_KidneyDiseaseMother { get; set; }
        public virtual string FH_KidneyDiseaseFather { get; set; }
        public virtual string FH_KidneyDiseaseSibling { get; set; }
        public virtual string FH_KidneyDiseaseGrandparent { get; set; }

        public virtual string FH_RheumatoidMother { get; set; }
        public virtual string FH_RheumatoidFather { get; set; }
        public virtual string FH_RheumatoidSibling { get; set; }
        public virtual string FH_RheumatoidGrandparent { get; set; }

        public virtual string FH_OsteoarthritisMother { get; set; }
        public virtual string FH_OsteoarthritisFather { get; set; }
        public virtual string FH_OsteoarthritisSibling { get; set; }
        public virtual string FH_OsteoarthritisGrandparent { get; set; }

        public virtual string FH_AllergiesMother { get; set; }
        public virtual string FH_AllergiesFather { get; set; }
        public virtual string FH_AllergiesSibling { get; set; }
        public virtual string FH_AllergiesGrandparent { get; set; }

        public virtual string FH_AsthmaMother { get; set; }
        public virtual string FH_AsthmaFather { get; set; }
        public virtual string FH_AsthmaSibling { get; set; }
        public virtual string FH_AsthmaGrandparent { get; set; }

        public virtual string FH_DiabetesTypeIMother { get; set; }
        public virtual string FH_DiabetesTypeIFather { get; set; }
        public virtual string FH_DiabetesTypeISibling { get; set; }
        public virtual string FH_DiabetesTypeIGrandparent { get; set; }

        public virtual string FH_DiabetesTypeIIMother { get; set; }
        public virtual string FH_DiabetesTypeIIFather { get; set; }
        public virtual string FH_DiabetesTypeIISibling { get; set; }
        public virtual string FH_DiabetesTypeIIGrandparent { get; set; }

        public virtual string FH_DepressionMother { get; set; }
        public virtual string FH_DepressionFather { get; set; }
        public virtual string FH_DepressionSibling { get; set; }
        public virtual string FH_DepressionGrandparent { get; set; }

        public virtual string FH_OtherName { get; set; }
        public virtual string FH_OtherMother { get; set; }
        public virtual string FH_OtherFather { get; set; }
        public virtual string FH_OtherSibling { get; set; }
        public virtual string FH_OtherGrandparent { get; set; }

        //Personal Habits
        public virtual string PH_EnjoyMost { get; set; }
        public virtual string PH_MainInterests { get; set; }
        public virtual string PH_WorryMost { get; set; }
        public virtual string PH_WhatNurtures { get; set; }
        public virtual bool? PH_DoExercise { get; set; }
        public virtual string PH_ExerciseDetails { get; set; }
        public virtual bool? PH_ReligiousPractice { get; set; }
        public virtual BodyTemperature PH_BodyTemperature { get; set; }
        public virtual bool? PH_EnjoyWork { get; set; }
        public virtual bool? PH_TakeVacations { get; set; }
        public virtual string PH_ColdFluDetails { get; set; }

        //Sleep Habits
        public virtual uint SH_SleepQuality { get; set; }
        public virtual bool? SH_FallingAsleepProblem { get; set; }
        public virtual bool? SH_StayingAsleepProblem { get; set; }
        public virtual string SH_HoursInSleep { get; set; }
        public virtual string SH_HoursNeededInSleep { get; set; }
        public virtual bool? SH_WakeRefreshed { get; set; }
        public virtual bool? SH_TakeNap { get; set; }
        public virtual string SH_NapDuration { get; set; }

        //Female
        public virtual string F_FirstMensesAge { get; set; }
        public virtual string F_PeriodsStoppedAge { get; set; }
        public virtual bool? F_CyclesRegular { get; set; }
        public virtual string F_PeriodBeginsEveryDays { get; set; }
        public virtual string F_PeriodLastsDays { get; set; }
        public virtual string F_PeriodFlow { get; set; }
        public virtual string F_PeriodBloodColor { get; set; }
        public virtual bool? F_AnyClots { get; set; }
        public virtual bool? F_AnyCramps { get; set; }
        public virtual bool? F_AnySpottingBleeding { get; set; }
        public virtual bool? F_SpottingBleedingEveryMonth { get; set; }
        public virtual bool? F_YeastInfectionInPast { get; set; }
        public virtual string F_YeastInfectionFrequency { get; set; }
        public virtual string F_YeastInfectionTreatment { get; set; }
        public virtual bool? F_AnyPremenstrualSymptoms { get; set; }
        public virtual string F_PremenstrualSymptomsDetails { get; set; }
        public virtual string F_NumberOfPregnancies { get; set; }
        public virtual string F_NumberOfMiscarriages { get; set; }
        public virtual string F_NumberOfLiveBirths { get; set; }
        public virtual string F_AnyPregancyProblem { get; set; }
        public virtual bool? F_RegularPAP { get; set; }
        public virtual bool? F_AnyAbnormalPAP { get; set; }
        public virtual bool? F_RegularBreastSelfExam { get; set; }
        public virtual bool? F_NoticedBreastLumps { get; set; }

        //Digestion and Elimination
        public virtual bool? DE_ProblemWithGasBloating { get; set; }
        public virtual bool? DE_Hemorrhoids { get; set; }
        public virtual bool? DE_RectalBleeding { get; set; }
        public virtual bool? DE_Parasites { get; set; }
        public virtual Frequency DE_ProblemFrequency { get; set; }
        public virtual string DE_ProblemSeverity { get; set; }
        public virtual string DE_ProblemDuration { get; set; }
        public virtual string DE_BowelMovements { get; set; }
        public virtual string DE_AnyBloodInStool { get; set; }
        public virtual bool? DE_HadBlackTarryGrayStool { get; set; }
        public virtual bool? DE_HadYellowLightColoredStool { get; set; }
        public virtual bool? DE_HadRectalItching { get; set; }
        public virtual string DE_StoolsFormedOrLoose { get; set; }
        public virtual bool? DE_HadConstipationDiarrhea { get; set; }
        public virtual string DE_ConstipationDiarrheaDetails { get; set; }
        public virtual bool? DE_HaveToStrainToPassStool { get; set; }
        public virtual string DE_HaveToStrainDetails { get; set; }
        public virtual string DE_PassGasFrequently { get; set; }
        public virtual string DE_BurpFrequently { get; set; }
        public virtual bool? DE_StrongDisagreeableOdor { get; set; }
        public virtual bool? DE_TraveledOutsideOfCanadaPast5Year { get; set; }
        public virtual string DE_TraveledCountries { get; set; }
        public virtual bool? DE_BeenCammpingPast5Year { get; set; }
        public virtual bool? DE_HadFasted { get; set; }
        public virtual string DE_FastType { get; set; }

        public override string ToString() {
            return base.ToString();
        }

        public virtual int CompareTo(NaturopathicDetail other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }
    }
}
