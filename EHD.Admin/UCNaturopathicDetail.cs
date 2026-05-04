using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EHD.Model.Entity;
using EHD.Repository;
using EHD.Service;
using EHD.Constant;
using NHibernate;
using System.IO;
using System.Reflection;

namespace EHD.Admin {
    public partial class UCNaturopathicDetail : UCTreatmentDetailBase, ITreatmentDetailUserControl {
        private bool _viewOnly = false;

        public NaturopathicDetail EditingNaturopathicDetail { get; set; }

        public ITreatmentDetail TreatmentDetail {
            get {
                return EditingNaturopathicDetail;
            }
        }

        public UCNaturopathicDetail(NaturopathicDetail NaturopathicDetail, bool ViewOnly) {
            if(NaturopathicDetail == null)
                throw new ArgumentException("NaturopathicDetail cannot be null.");

            _viewOnly = ViewOnly;

            InitializeComponent();

            EditingNaturopathicDetail = NaturopathicDetail;
        }

        //Populate form
        protected override void loadDetail() {
            populateNaturopathicDetail(EditingNaturopathicDetail);
        }

        private void populateNaturopathicDetail(NaturopathicDetail detail) {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using (ISession session = sessionFactory.OpenSession())
                using (ITransaction tr = session.BeginTransaction()) {
                    if (detail != null) {
                        //Populate form controls from entity

                        //Your current health
                        rdoBodyStateExcellent.Checked = (detail.HealthState == HealthState.Excellent);
                        rdoBodyStateVeryGood.Checked = (detail.HealthState == HealthState.VeryGood);
                        rdoBodyStateAverage.Checked = (detail.HealthState == HealthState.Average);
                        rdoBodyStateFair.Checked = (detail.HealthState == HealthState.Fair);
                        rdoBodyStatePoor.Checked = (detail.HealthState == HealthState.Poor);
                        numCurrentEnergyLevel.Value = detail.CurrentEnergyLevel;
                        numMorningEnergy.Value = detail.MorningEnergyLevel;
                        txtWeightCurrent.Text = detail.CurrentWeight;
                        txtWeightYearAgo.Text = detail.YearAgoWeight;
                        txtWeightIdeal.Text = detail.IdealWeight;
                        txtHeight.Text = detail.CurrentHeight;

                        //Stress management
                        numStressLevel.Value = detail.CurrentStressLevel;
                        rdoIdentifyStressSelf.Checked = (detail.StressIdentifiedBy == SelfOther.Self);
                        rdoIdentifyStressOther.Checked = (detail.StressIdentifiedBy == SelfOther.Other);
                        txtActiviesWhenStressed.Text = detail.StressedBehavior;
                        txtStressLocalizedOfBody.Text = detail.StressLocalizedOfBody;
                        txtStressHandlingTools.Text = detail.StressHandlingTools;

                        //Allergies
                        rdoDrugAllergiesYes.Checked = (detail.HasDrugAllergy.HasValue && detail.HasDrugAllergy.Value);
                        rdoDrugAllergiesNo.Checked = (detail.HasDrugAllergy.HasValue && !detail.HasDrugAllergy.Value);
                        txtDrugAllergies.Text = detail.DrugAllergies;
                        rdoFoodAllergiesYes.Checked = (detail.HasFoodAllergy.HasValue && detail.HasFoodAllergy.Value);
                        rdoFoodAllergiesNo.Checked = (detail.HasFoodAllergy.HasValue && !detail.HasFoodAllergy.Value);
                        txtFoodAllergies.Text = detail.FoodAllergies;
                        rdoAnimalAllergiesYes.Checked = (detail.HasAnimalAllergy.HasValue && detail.HasAnimalAllergy.Value);
                        rdoAnimalAllergiesNo.Checked = (detail.HasAnimalAllergy.HasValue && !detail.HasAnimalAllergy.Value);
                        txtAnimalAllergies.Text = detail.AnimalAllergies;
                        rdoPlantAllergiesYes.Checked = (detail.HasPlantAllergy.HasValue && detail.HasPlantAllergy.Value);
                        rdoPlantAllergiesNo.Checked = (detail.HasPlantAllergy.HasValue && !detail.HasPlantAllergy.Value);
                        txtPlantAllergies.Text = detail.PlantAllergies;
                        rdoEnvAllergiesYes.Checked = (detail.HasEnvironmentalAllergy.HasValue && detail.HasEnvironmentalAllergy.Value);
                        rdoEnvAllergiesNo.Checked = (detail.HasEnvironmentalAllergy.HasValue && !detail.HasEnvironmentalAllergy.Value);
                        txtEnvAllergies.Text = detail.EnvironmentalAllergies;

                        //Accidents / Hospitalizations
                        txtMajorInjuries.Text = detail.MajorInjuries;
                        txtPreviousSurgeries.Text = detail.Surgeries;
                        rdoHadFluVaccinePast5YearYes.Checked = (detail.HadFluVaccineLastFiveYears.HasValue && detail.HadFluVaccineLastFiveYears.Value);
                        rdoHadFluVaccinePast5YearNo.Checked = (detail.HadFluVaccineLastFiveYears.HasValue && !detail.HadFluVaccineLastFiveYears.Value);
                        rdoHadVaccineReactionYes.Checked = (detail.HadReactionToVaccine.HasValue && detail.HadReactionToVaccine.Value);
                        rdoHadVaccineReactionNo.Checked = (detail.HadReactionToVaccine.HasValue && !detail.HadReactionToVaccine.Value);

                        //Current / Past conditions
                        chkAllergiesC.Checked = (detail.C_Allergies.HasValue && detail.C_Allergies.Value);
                        chkAllergiesP.Checked = (detail.P_Allergies.HasValue && detail.P_Allergies.Value);
                        chkAnemiaC.Checked = (detail.C_Anemia.HasValue && detail.C_Anemia.Value);
                        chkAnemiaP.Checked = (detail.P_Anemia.HasValue && detail.P_Anemia.Value);
                        chkStrokeC.Checked = (detail.C_Stroke.HasValue && detail.C_Stroke.Value);
                        chkStrokeP.Checked = (detail.P_Stroke.HasValue && detail.P_Stroke.Value);
                        chkDepressionC.Checked = (detail.C_Depression.HasValue && detail.C_Depression.Value);
                        chkDepressionP.Checked = (detail.P_Depression.HasValue && detail.P_Depression.Value);
                        chkAsthmaC.Checked = (detail.C_Asthma.HasValue && detail.C_Asthma.Value);
                        chkAsthmaP.Checked = (detail.P_Asthma.HasValue && detail.P_Asthma.Value);
                        chkMeaslesC.Checked = (detail.C_Measles.HasValue && detail.C_Measles.Value);
                        chkMeaslesP.Checked = (detail.P_Measles.HasValue && detail.P_Measles.Value);
                        chkHeartDiseaseC.Checked = (detail.C_HeartDisease.HasValue && detail.C_HeartDisease.Value);
                        chkHeartDiseaseP.Checked = (detail.P_HeartDisease.HasValue && detail.P_HeartDisease.Value);
                        chkEmotionalAbuseC.Checked = (detail.C_EmotionalAbuse.HasValue && detail.C_EmotionalAbuse.Value);
                        chkEmotionalAbuseP.Checked = (detail.P_EmotionalAbuse.HasValue && detail.P_EmotionalAbuse.Value);
                        chkEczemaC.Checked = (detail.C_Eczema.HasValue && detail.C_Eczema.Value);
                        chkEczemaP.Checked = (detail.P_Eczema.HasValue && detail.P_Eczema.Value);
                        chkMumpsC.Checked = (detail.C_Mumps.HasValue && detail.C_Mumps.Value);
                        chkMumpsP.Checked = (detail.P_Mumps.HasValue && detail.P_Mumps.Value);
                        chkRheumaticFeverC.Checked = (detail.C_RheumaticFever.HasValue && detail.C_RheumaticFever.Value);
                        chkRheumaticFeverP.Checked = (detail.P_RheumaticFever.HasValue && detail.P_RheumaticFever.Value);
                        chkPhyMentalAbuseC.Checked = (detail.C_PhysicalMentalAbuse.HasValue && detail.C_PhysicalMentalAbuse.Value);
                        chkPhyMentalAbuseP.Checked = (detail.P_PhysicalMentalAbuse.HasValue && detail.P_PhysicalMentalAbuse.Value);
                        chkPsoriasisC.Checked = (detail.C_Psoriasis.HasValue && detail.C_Psoriasis.Value);
                        chkPsoriasisP.Checked = (detail.P_Psoriasis.HasValue && detail.P_Psoriasis.Value);
                        chkChickenPoxC.Checked = (detail.C_ChickenPox.HasValue && detail.C_ChickenPox.Value);
                        chkChickenPoxP.Checked = (detail.P_ChickenPox.HasValue && detail.P_ChickenPox.Value);
                        chkHighBloodPressureC.Checked = (detail.C_HighBloodPressure.HasValue && detail.C_HighBloodPressure.Value);
                        chkHighBloodPressureP.Checked = (detail.P_HighBloodPressure.HasValue && detail.P_HighBloodPressure.Value);
                        chkNumbnessTinglingC.Checked = (detail.C_NumbnessTingling.HasValue && detail.C_NumbnessTingling.Value);
                        chkNumbnessTinglingP.Checked = (detail.P_NumbnessTingling.HasValue && detail.P_NumbnessTingling.Value);
                        chkHayFeverC.Checked = (detail.C_HayFever.HasValue && detail.C_HayFever.Value);
                        chkHayFeverP.Checked = (detail.P_HayFever.HasValue && detail.P_HayFever.Value);
                        chkWhoopingCoughC.Checked = (detail.C_WhoopingCough.HasValue && detail.C_WhoopingCough.Value);
                        chkWhoopingCoughP.Checked = (detail.P_WhoopingCough.HasValue && detail.P_WhoopingCough.Value);
                        chkHighCholesterolC.Checked = (detail.C_HighCholesterol.HasValue && detail.C_HighCholesterol.Value);
                        chkHighCholesterolP.Checked = (detail.P_HighCholesterol.HasValue && detail.P_HighCholesterol.Value);
                        chkColdHandsFeetC.Checked = (detail.C_ColdHandsFeet.HasValue && detail.C_ColdHandsFeet.Value);
                        chkColdHandsFeetP.Checked = (detail.P_ColdHandsFeet.HasValue && detail.P_ColdHandsFeet.Value);
                        chkPneumoniaC.Checked = (detail.C_Pneumonia.HasValue && detail.C_Pneumonia.Value);
                        chkPneumoniaP.Checked = (detail.P_Pneumonia.HasValue && detail.P_Pneumonia.Value);
                        chkShinglesC.Checked = (detail.C_Shingles.HasValue && detail.C_Shingles.Value);
                        chkShinglesP.Checked = (detail.P_Shingles.HasValue && detail.P_Shingles.Value);
                        chkCancerC.Checked = (detail.C_Cancer.HasValue && detail.C_Cancer.Value);
                        chkCancerP.Checked = (detail.P_Cancer.HasValue && detail.P_Cancer.Value);
                        chkThyroidC.Checked = (detail.C_ThyroidProblems.HasValue && detail.C_ThyroidProblems.Value);
                        chkThyroidP.Checked = (detail.P_ThyroidProblems.HasValue && detail.P_ThyroidProblems.Value);
                        chkEarInfectionsC.Checked = (detail.C_EarInfections.HasValue && detail.C_EarInfections.Value);
                        chkEarInfectionsP.Checked = (detail.P_EarInfections.HasValue && detail.P_EarInfections.Value);
                        chkDiphtheriaC.Checked = (detail.C_Diphtheria.HasValue && detail.C_Diphtheria.Value);
                        chkDiphtheriaP.Checked = (detail.P_Diphtheria.HasValue && detail.P_Diphtheria.Value);
                        chkType1DiabetesC.Checked = (detail.C_Type1Diabetes.HasValue && detail.C_Type1Diabetes.Value);
                        chkType1DiabetesP.Checked = (detail.P_Type1Diabetes.HasValue && detail.P_Type1Diabetes.Value);
                        chkWartsC.Checked = (detail.C_Warts.HasValue && detail.C_Warts.Value);
                        chkWartsP.Checked = (detail.P_Warts.HasValue && detail.P_Warts.Value);
                        chkStrepThroatC.Checked = (detail.C_StrepThroat.HasValue && detail.C_StrepThroat.Value);
                        chkStrepThroatP.Checked = (detail.P_StrepThroat.HasValue && detail.P_StrepThroat.Value);
                        chkScarletFeverC.Checked = (detail.C_ScarletFever.HasValue && detail.C_ScarletFever.Value);
                        chkScarletFeverP.Checked = (detail.P_ScarletFever.HasValue && detail.P_ScarletFever.Value);
                        chkType2DiabetesC.Checked = (detail.C_Type2Diabetes.HasValue && detail.C_Type2Diabetes.Value);
                        chkType2DiabetesP.Checked = (detail.P_Type2Diabetes.HasValue && detail.P_Type2Diabetes.Value);
                        chkMonoC.Checked = (detail.C_Mono.HasValue && detail.C_Mono.Value);
                        chkMonoP.Checked = (detail.P_Mono.HasValue && detail.P_Mono.Value);
                        chkTonsillitisC.Checked = (detail.C_Tonsillitis.HasValue && detail.C_Tonsillitis.Value);
                        chkTonsillitisP.Checked = (detail.P_Tonsillitis.HasValue && detail.P_Tonsillitis.Value);
                        chkPolioC.Checked = (detail.C_Polio.HasValue && detail.C_Polio.Value);
                        chkPolioP.Checked = (detail.P_Polio.HasValue && detail.P_Polio.Value);
                        chkKidneyDiseaseC.Checked = (detail.C_KidneyDisease.HasValue && detail.C_KidneyDisease.Value);
                        chkKidneyDiseaseP.Checked = (detail.P_KidneyDisease.HasValue && detail.P_KidneyDisease.Value);
                        chkRheumatoidArthritisC.Checked = (detail.C_RheumatoidArthritis.HasValue && detail.C_RheumatoidArthritis.Value);
                        chkRheumatoidArthritisP.Checked = (detail.P_RheumatoidArthritis.HasValue && detail.P_RheumatoidArthritis.Value);
                        chkCankerSoresC.Checked = (detail.C_CankerSores.HasValue && detail.C_CankerSores.Value);
                        chkCankerSoresP.Checked = (detail.P_CankerSores.HasValue && detail.P_CankerSores.Value);
                        chkSmallpoxC.Checked = (detail.C_Smallpox.HasValue && detail.C_Smallpox.Value);
                        chkSmallpoxP.Checked = (detail.P_Smallpox.HasValue && detail.P_Smallpox.Value);
                        chkVisualProblemsC.Checked = (detail.C_VisualProblems.HasValue && detail.C_VisualProblems.Value);
                        chkVisualProblemsP.Checked = (detail.P_VisualProblems.HasValue && detail.P_VisualProblems.Value);
                        chkOsteoarthritisC.Checked = (detail.C_Osteoarthritis.HasValue && detail.C_Osteoarthritis.Value);
                        chkOsteoarthritisP.Checked = (detail.P_Osteoarthritis.HasValue && detail.P_Osteoarthritis.Value);
                        chkJaundiceC.Checked = (detail.C_Jaundice.HasValue && detail.C_Jaundice.Value);
                        chkJaundiceP.Checked = (detail.P_Jaundice.HasValue && detail.P_Jaundice.Value);
                        chkTuberculosisC.Checked = (detail.C_Tuberculosis.HasValue && detail.C_Tuberculosis.Value);
                        chkTuberculosisP.Checked = (detail.P_Tuberculosis.HasValue && detail.P_Tuberculosis.Value);
                        chkAutoimmuneC.Checked = (detail.C_AutoimmuneDisease.HasValue && detail.C_AutoimmuneDisease.Value);
                        chkAutoimmuneP.Checked = (detail.P_AutoimmuneDisease.HasValue && detail.P_AutoimmuneDisease.Value);
                        chkWeightProblemsC.Checked = (detail.C_WeightProblems.HasValue && detail.C_WeightProblems.Value);
                        chkWeightProblemsP.Checked = (detail.P_WeightProblems.HasValue && detail.P_WeightProblems.Value);
                        chkAlcoholismC.Checked = (detail.C_Alcoholism.HasValue && detail.C_Alcoholism.Value);
                        chkAlcoholismP.Checked = (detail.P_Alcoholism.HasValue && detail.P_Alcoholism.Value);
                        chkMalariaC.Checked = (detail.C_Malaria.HasValue && detail.C_Malaria.Value);
                        chkMalariaP.Checked = (detail.P_Malaria.HasValue && detail.P_Malaria.Value);
                        chkEpilepsyC.Checked = (detail.C_Epilepsy.HasValue && detail.C_Epilepsy.Value);
                        chkEpilepsyP.Checked = (detail.P_Epilepsy.HasValue && detail.P_Epilepsy.Value);
                        chkGoutC.Checked = (detail.C_Gout.HasValue && detail.C_Gout.Value);
                        chkGoutP.Checked = (detail.P_Gout.HasValue && detail.P_Gout.Value);
                        chkHepatitisC.Checked = (detail.C_Hepatitis.HasValue && detail.C_Hepatitis.Value);
                        chkHepatitisP.Checked = (detail.P_Hepatitis.HasValue && detail.P_Hepatitis.Value);
                        chkGallstonesC.Checked = (detail.C_Gallstones.HasValue && detail.C_Gallstones.Value);
                        chkGallstonesP.Checked = (detail.P_Gallstones.HasValue && detail.P_Gallstones.Value);
                        chkVaricoseVeinsC.Checked = (detail.C_VaricoseVeins.HasValue && detail.C_VaricoseVeins.Value);
                        chkVaricoseVeinsP.Checked = (detail.P_VaricoseVeins.HasValue && detail.P_VaricoseVeins.Value);
                        txtCPOther.Text = detail.CP_Other;
                        txtCPNeverWellSince.Text = detail.CP_NeverWellSince;

                        //Currently use
                        txtCUAlcohol.Text = detail.CU_Alcohol;
                        txtCUTobacco.Text = detail.CU_Tobacco;
                        txtCUHormones.Text = detail.CU_Hormones;
                        txtCUCoffee.Text = detail.CU_Coffee;
                        txtCUCortisone.Text = detail.CU_Cortisone;
                        txtCUBlackTea.Text = detail.CU_BlackTea;
                        txtCUSedatives.Text = detail.CU_Sedatives;
                        txtCULaxatives.Text = detail.CU_Laxatives;
                        txtCUAntacids.Text = detail.CU_Antacids;

                        //Current prescription medications
                        txtCPMName1.Text = detail.CPM_Name1;
                        txtCPMReason1.Text = detail.CPM_Reason1;
                        txtCPMDailyAmount1.Text = detail.CPM_Amount1;
                        txtCPMWhenPrescribed1.Text = detail.CPM_PrescriptionDate1;
                        txtCPMDrugCategory1.Text = detail.CPM_DrugCategory1;

                        txtCPMName2.Text = detail.CPM_Name2;
                        txtCPMReason2.Text = detail.CPM_Reason2;
                        txtCPMDailyAmount2.Text = detail.CPM_Amount2;
                        txtCPMWhenPrescribed2.Text = detail.CPM_PrescriptionDate2;
                        txtCPMDrugCategory2.Text = detail.CPM_DrugCategory2;

                        txtCPMName3.Text = detail.CPM_Name3;
                        txtCPMReason3.Text = detail.CPM_Reason3;
                        txtCPMDailyAmount3.Text = detail.CPM_Amount3;
                        txtCPMWhenPrescribed3.Text = detail.CPM_PrescriptionDate3;
                        txtCPMDrugCategory3.Text = detail.CPM_DrugCategory3;

                        txtCPMName4.Text = detail.CPM_Name4;
                        txtCPMReason4.Text = detail.CPM_Reason4;
                        txtCPMDailyAmount4.Text = detail.CPM_Amount4;
                        txtCPMWhenPrescribed4.Text = detail.CPM_PrescriptionDate4;
                        txtCPMDrugCategory4.Text = detail.CPM_DrugCategory4;

                        txtCPMName5.Text = detail.CPM_Name5;
                        txtCPMReason5.Text = detail.CPM_Reason5;
                        txtCPMDailyAmount5.Text = detail.CPM_Amount5;
                        txtCPMWhenPrescribed5.Text = detail.CPM_PrescriptionDate5;
                        txtCPMDrugCategory5.Text = detail.CPM_DrugCategory5;

                        //Vitamins/Herbs
                        txtVHName1.Text = detail.VH_Name1;
                        txtVHBrand1.Text = detail.VH_Brand1;
                        txtVHAmount1.Text = detail.VH_Amount1;
                        txtVHReason1.Text = detail.VH_Reason1;
                        rdoVHWhoSuggestedSelf1.Checked = (detail.VH_SuggestedBy1 == SelfOther.Self);
                        rdoVHWhoSuggestedOther1.Checked = (detail.VH_SuggestedBy1 == SelfOther.Other);
                        rdoVHHasHelpedYes1.Checked = (detail.VH_Helped1 == YesNo.Yes);
                        rdoVHHasHelpedNo1.Checked = (detail.VH_Helped1 == YesNo.No);

                        txtVHName2.Text = detail.VH_Name2;
                        txtVHBrand2.Text = detail.VH_Brand2;
                        txtVHAmount2.Text = detail.VH_Amount2;
                        txtVHReason2.Text = detail.VH_Reason2;
                        rdoVHWhoSuggestedSelf2.Checked = (detail.VH_SuggestedBy2 == SelfOther.Self);
                        rdoVHWhoSuggestedOther2.Checked = (detail.VH_SuggestedBy2 == SelfOther.Other);
                        rdoVHHasHelpedYes2.Checked = (detail.VH_Helped2 == YesNo.Yes);
                        rdoVHHasHelpedNo2.Checked = (detail.VH_Helped2 == YesNo.No);

                        txtVHName3.Text = detail.VH_Name3;
                        txtVHBrand3.Text = detail.VH_Brand3;
                        txtVHAmount3.Text = detail.VH_Amount3;
                        txtVHReason3.Text = detail.VH_Reason3;
                        rdoVHWhoSuggestedSelf3.Checked = (detail.VH_SuggestedBy3 == SelfOther.Self);
                        rdoVHWhoSuggestedOther3.Checked = (detail.VH_SuggestedBy3 == SelfOther.Other);
                        rdoVHHasHelpedYes3.Checked = (detail.VH_Helped3 == YesNo.Yes);
                        rdoVHHasHelpedNo3.Checked = (detail.VH_Helped3 == YesNo.No);

                        txtVHName4.Text = detail.VH_Name4;
                        txtVHBrand4.Text = detail.VH_Brand4;
                        txtVHAmount4.Text = detail.VH_Amount4;
                        txtVHReason4.Text = detail.VH_Reason4;
                        rdoVHWhoSuggestedSelf4.Checked = (detail.VH_SuggestedBy4 == SelfOther.Self);
                        rdoVHWhoSuggestedOther4.Checked = (detail.VH_SuggestedBy4 == SelfOther.Other);
                        rdoVHHasHelpedYes4.Checked = (detail.VH_Helped4 == YesNo.Yes);
                        rdoVHHasHelpedNo4.Checked = (detail.VH_Helped4 == YesNo.No);

                        txtVHName5.Text = detail.VH_Name5;
                        txtVHBrand5.Text = detail.VH_Brand5;
                        txtVHAmount5.Text = detail.VH_Amount5;
                        txtVHReason5.Text = detail.VH_Reason5;
                        rdoVHWhoSuggestedSelf5.Checked = (detail.VH_SuggestedBy5 == SelfOther.Self);
                        rdoVHWhoSuggestedOther5.Checked = (detail.VH_SuggestedBy5 == SelfOther.Other);
                        rdoVHHasHelpedYes5.Checked = (detail.VH_Helped5 == YesNo.Yes);
                        rdoVHHasHelpedNo5.Checked = (detail.VH_Helped5 == YesNo.No);

                        rdoVHOtherYes.Checked = (detail.VH_HasOtherSupplementation.HasValue && detail.VH_HasOtherSupplementation.Value);
                        rdoVHOtherNo.Checked = (detail.VH_HasOtherSupplementation.HasValue && !detail.VH_HasOtherSupplementation.Value);

                        txtVHOther.Text = detail.VH_OtherSupplementation;

                        //Family History
                        txtFHCancerM.Text = detail.FH_CancerMother;
                        txtFHCancerF.Text = detail.FH_CancerFather;
                        txtFHCancerS.Text = detail.FH_CancerSibling;
                        txtFHCancerG.Text = detail.FH_CancerGrandparent;

                        txtFHRheumatoidM.Text = detail.FH_RheumatoidMother;
                        txtFHRheumatoidF.Text = detail.FH_RheumatoidFather;
                        txtFHRheumatoidS.Text = detail.FH_RheumatoidSibling;
                        txtFHRheumatoidG.Text = detail.FH_RheumatoidGrandparent;

                        txtFHTuberculosisM.Text = detail.FH_TuberculosisMother;
                        txtFHTuberculosisF.Text = detail.FH_TuberculosisFather;
                        txtFHTuberculosisS.Text = detail.FH_TuberculosisSibling;
                        txtFHTuberculosisG.Text = detail.FH_TuberculosisGrandparent;

                        txtFHOsteoarthritisM.Text = detail.FH_OsteoarthritisMother;
                        txtFHOsteoarthritisF.Text = detail.FH_OsteoarthritisFather;
                        txtFHOsteoarthritisS.Text = detail.FH_OsteoarthritisSibling;
                        txtFHOsteoarthritisG.Text = detail.FH_OsteoarthritisGrandparent;

                        txtFHHeartDiseaseM.Text = detail.FH_HeartDiseaseMother;
                        txtFHHeartDiseaseF.Text = detail.FH_HeartDiseaseFather;
                        txtFHHeartDiseaseS.Text = detail.FH_HeartDiseaseSibling;
                        txtFHHeartDiseaseG.Text = detail.FH_HeartDiseaseGrandparent;

                        txtFHAllergiesM.Text = detail.FH_AllergiesMother;
                        txtFHAllergiesF.Text = detail.FH_AllergiesFather;
                        txtFHAllergiesS.Text = detail.FH_AllergiesSibling;
                        txtFHAllergiesG.Text = detail.FH_AllergiesGrandparent;

                        txtFHStrokeM.Text = detail.FH_StrokeMother;
                        txtFHStrokeF.Text = detail.FH_StrokeFather;
                        txtFHStrokeS.Text = detail.FH_StrokeSibling;
                        txtFHStrokeG.Text = detail.FH_StrokeGrandparent;

                        txtFHAsthmaM.Text = detail.FH_AsthmaMother;
                        txtFHAsthmaF.Text = detail.FH_AsthmaFather;
                        txtFHAsthmaS.Text = detail.FH_AsthmaSibling;
                        txtFHAsthmaG.Text = detail.FH_AsthmaGrandparent;

                        txtFHHighBloodPressureM.Text = detail.FH_HighBloodPressureMother;
                        txtFHHighBloodPressureF.Text = detail.FH_HighBloodPressureFather;
                        txtFHHighBloodPressureS.Text = detail.FH_HighBloodPressureSibling;
                        txtFHHighBloodPressureG.Text = detail.FH_HighBloodPressureGrandparent;

                        txtFHDiabetesTypeIM.Text = detail.FH_DiabetesTypeIMother;
                        txtFHDiabetesTypeIF.Text = detail.FH_DiabetesTypeIFather;
                        txtFHDiabetesTypeIS.Text = detail.FH_DiabetesTypeISibling;
                        txtFHDiabetesTypeIG.Text = detail.FH_DiabetesTypeIGrandparent;

                        txtFHDiabetesTypeIIM.Text = detail.FH_DiabetesTypeIIMother;
                        txtFHDiabetesTypeIIF.Text = detail.FH_DiabetesTypeIIFather;
                        txtFHDiabetesTypeIIS.Text = detail.FH_DiabetesTypeIISibling;
                        txtFHDiabetesTypeIIG.Text = detail.FH_DiabetesTypeIIGrandparent;

                        txtFHHighCholesterolM.Text = detail.FH_HighCholesterolMother;
                        txtFHHighCholesterolF.Text = detail.FH_HighCholesterolFather;
                        txtFHHighCholesterolS.Text = detail.FH_HighCholesterolSibling;
                        txtFHHighCholesterolG.Text = detail.FH_HighCholesterolGrandparent;

                        txtFHDepressionM.Text = detail.FH_DepressionMother;
                        txtFHDepressionF.Text = detail.FH_DepressionFather;
                        txtFHDepressionS.Text = detail.FH_DepressionSibling;
                        txtFHDepressionG.Text = detail.FH_DepressionGrandparent;

                        txtFHKidneyDiseaseM.Text = detail.FH_KidneyDiseaseMother;
                        txtFHKidneyDiseaseF.Text = detail.FH_KidneyDiseaseFather;
                        txtFHKidneyDiseaseS.Text = detail.FH_KidneyDiseaseSibling;
                        txtFHKidneyDiseaseG.Text = detail.FH_KidneyDiseaseGrandparent;

                        txtFHOtherName.Text = detail.FH_OtherName;
                        txtFHOtherM.Text = detail.FH_OtherMother;
                        txtFHOtherF.Text = detail.FH_OtherFather;
                        txtFHOtherS.Text = detail.FH_OtherSibling;
                        txtFHOtherG.Text = detail.FH_OtherGrandparent;

                        //Personal Habits
                        txtPHEnjoyMost.Text = detail.PH_EnjoyMost;
                        txtPHHobbies.Text = detail.PH_MainInterests;
                        txtPHWorryMost.Text = detail.PH_WorryMost;
                        txtPHNurtures.Text = detail.PH_WhatNurtures;
                        rdoPHExerciseYes.Checked = (detail.PH_DoExercise.HasValue && detail.PH_DoExercise.Value);
                        rdoPHExerciseNo.Checked = (detail.PH_DoExercise.HasValue && !detail.PH_DoExercise.Value);
                        txtPHExercise.Text = detail.PH_ExerciseDetails;
                        rdoPHReligiousYes.Checked = (detail.PH_ReligiousPractice.HasValue && detail.PH_ReligiousPractice.Value);
                        rdoPHReligiousNo.Checked = (detail.PH_ReligiousPractice.HasValue && !detail.PH_ReligiousPractice.Value);
                        rdoPHBodyTemperatureWarmer.Checked = (detail.PH_BodyTemperature == BodyTemperature.Warmer);
                        rdoPHBodyTemperatureCooler.Checked = (detail.PH_BodyTemperature == BodyTemperature.Cooler);
                        rdoPHBodyTemperatureAverage.Checked = (detail.PH_BodyTemperature == BodyTemperature.Average);
                        rdoPHEnjoyWorkYes.Checked = (detail.PH_EnjoyWork.HasValue && detail.PH_EnjoyWork.Value);
                        rdoPHEnjoyWorkNo.Checked = (detail.PH_EnjoyWork.HasValue && !detail.PH_EnjoyWork.Value);
                        rdoPHTakeVacationsYes.Checked = (detail.PH_TakeVacations.HasValue && detail.PH_TakeVacations.Value);
                        rdoPHTakeVacationsNo.Checked = (detail.PH_TakeVacations.HasValue && !detail.PH_TakeVacations.Value);
                        txtPHGetColdFluInAYear.Text = detail.PH_ColdFluDetails;

                        //Sleep Habits
                        numSHSleepQuality.Value = detail.SH_SleepQuality;
                        rdoSHFallAsleepProblemYes.Checked = (detail.SH_FallingAsleepProblem.HasValue && detail.SH_FallingAsleepProblem.Value);
                        rdoSHFallAsleepProblemNo.Checked = (detail.SH_FallingAsleepProblem.HasValue && !detail.SH_FallingAsleepProblem.Value);
                        rdoSHStayingAsleepProblemYes.Checked = (detail.SH_StayingAsleepProblem.HasValue && detail.SH_StayingAsleepProblem.Value);
                        rdoSHStayingAsleepProblemNo.Checked = (detail.SH_StayingAsleepProblem.HasValue && !detail.SH_StayingAsleepProblem.Value);
                        txtSHHoursSleep.Text = detail.SH_HoursInSleep;
                        txtSHHoursNeededSleep.Text = detail.SH_HoursNeededInSleep;
                        rdoSHWakeRefreshedYes.Checked = (detail.SH_WakeRefreshed.HasValue && detail.SH_WakeRefreshed.Value);
                        rdoSHWakeRefreshedNo.Checked = (detail.SH_WakeRefreshed.HasValue && !detail.SH_WakeRefreshed.Value);
                        rdoSHNapYes.Checked = (detail.SH_TakeNap.HasValue && detail.SH_TakeNap.Value);
                        rdoSHNapNo.Checked = (detail.SH_TakeNap.HasValue && !detail.SH_TakeNap.Value);
                        txtSHNapDuration.Text = detail.SH_NapDuration;

                        //Female
                        txtFFirstMensesAge.Text = detail.F_FirstMensesAge;
                        txtFPeriodsStoppedAge.Text = detail.F_PeriodsStoppedAge;
                        rdoFCyclesRegularYes.Checked = (detail.F_CyclesRegular.HasValue && detail.F_CyclesRegular.Value);
                        rdoFCyclesRegularNo.Checked = (detail.F_CyclesRegular.HasValue && !detail.F_CyclesRegular.Value);
                        txtFPeriodsBeginEveryDays.Text = detail.F_PeriodBeginsEveryDays;
                        txtFPeriodsLastDays.Text = detail.F_PeriodLastsDays;
                        txtFPeriodFlow.Text = detail.F_PeriodFlow;
                        txtFPeriodColor.Text = detail.F_PeriodBloodColor;
                        rdoFAnyClotsYes.Checked = (detail.F_AnyClots.HasValue && detail.F_AnyClots.Value);
                        rdoFAnyClotsNo.Checked = (detail.F_AnyClots.HasValue && !detail.F_AnyClots.Value);
                        rdoFAnyCrampsYes.Checked = (detail.F_AnyCramps.HasValue && detail.F_AnyCramps.Value);
                        rdoFAnyCrampsNo.Checked = (detail.F_AnyCramps.HasValue && !detail.F_AnyCramps.Value);
                        rdoFAnySpottingYes.Checked = (detail.F_AnySpottingBleeding.HasValue && detail.F_AnySpottingBleeding.Value);
                        rdoFAnySpottingNo.Checked = (detail.F_AnySpottingBleeding.HasValue && !detail.F_AnySpottingBleeding.Value);
                        rdoFSpottingEveryMonthYes.Checked = (detail.F_SpottingBleedingEveryMonth.HasValue && detail.F_SpottingBleedingEveryMonth.Value);
                        rdoFSpottingEveryMonthNo.Checked = (detail.F_SpottingBleedingEveryMonth.HasValue && !detail.F_SpottingBleedingEveryMonth.Value);
                        rdoFHadYeastInfectionYes.Checked = (detail.F_YeastInfectionInPast.HasValue && detail.F_YeastInfectionInPast.Value);
                        rdoFHadYeastInfectionNo.Checked = (detail.F_YeastInfectionInPast.HasValue && !detail.F_YeastInfectionInPast.Value);
                        txtFYeastInfectionFrequency.Text = detail.F_YeastInfectionFrequency;
                        txtFYeastInfectionTreated.Text = detail.F_YeastInfectionTreatment;
                        rdoFPremenstrualYes.Checked = (detail.F_AnyPremenstrualSymptoms.HasValue && detail.F_AnyPremenstrualSymptoms.Value);
                        rdoFPremenstrualNo.Checked = (detail.F_AnyPremenstrualSymptoms.HasValue && !detail.F_AnyPremenstrualSymptoms.Value);
                        txtFPremenstrualSymptoms.Text = detail.F_PremenstrualSymptomsDetails;
                        txtFNumOfPregnancies.Text = detail.F_NumberOfPregnancies;
                        txtFNumOfMiscarriages.Text = detail.F_NumberOfMiscarriages;
                        txtFNumOfLiveBirths.Text = detail.F_NumberOfLiveBirths;
                        txtFGettingPregnantProblem.Text = detail.F_AnyPregancyProblem;
                        rdoFRegularSmearsYes.Checked = (detail.F_RegularPAP.HasValue && detail.F_RegularPAP.Value);
                        rdoFRegularSmearsNo.Checked = (detail.F_RegularPAP.HasValue && !detail.F_RegularPAP.Value);
                        rdoFAbnormalPAPYes.Checked = (detail.F_AnyAbnormalPAP.HasValue && detail.F_AnyAbnormalPAP.Value);
                        rdoFAbnormalPAPNo.Checked = (detail.F_AnyAbnormalPAP.HasValue && !detail.F_AnyAbnormalPAP.Value);
                        rdoFRegularBreastSelfExamYes.Checked = (detail.F_RegularBreastSelfExam.HasValue && detail.F_RegularBreastSelfExam.Value);
                        rdoFRegularBreastSelfExamNo.Checked = (detail.F_RegularBreastSelfExam.HasValue && !detail.F_RegularBreastSelfExam.Value);
                        rdoFAnyBreastLumpsYes.Checked = (detail.F_NoticedBreastLumps.HasValue && detail.F_NoticedBreastLumps.Value);
                        rdoFAnyBreastLumpsNo.Checked = (detail.F_NoticedBreastLumps.HasValue && !detail.F_NoticedBreastLumps.Value);

                        //Digestion and Elimination
                        rdoDEGasBloatingYes.Checked = (detail.DE_ProblemWithGasBloating.HasValue && detail.DE_ProblemWithGasBloating.Value);
                        rdoDEGasBloatingNo.Checked = (detail.DE_ProblemWithGasBloating.HasValue && !detail.DE_ProblemWithGasBloating.Value);
                        chkDEHemorrhoids.Checked = (detail.DE_Hemorrhoids.HasValue && detail.DE_Hemorrhoids.Value);
                        chkDERectalBleeding.Checked = (detail.DE_RectalBleeding.HasValue && detail.DE_RectalBleeding.Value);
                        chkDEParasites.Checked = (detail.DE_Parasites.HasValue && detail.DE_Parasites.Value);
                        rdoDEProblemOften.Checked = (detail.DE_ProblemFrequency == Frequency.Often);
                        rdoDEProblemSometimes.Checked = (detail.DE_ProblemFrequency == Frequency.Sometimes);
                        rdoDEProblemNever.Checked = (detail.DE_ProblemFrequency == Frequency.Never);
                        txtDESevere.Text = detail.DE_ProblemSeverity;
                        txtDEProblemDuration.Text = detail.DE_ProblemDuration;
                        txtDEBowelMovementsFrequency.Text = detail.DE_BowelMovements;
                        txtDEAnyBloodMucousInStool.Text = detail.DE_AnyBloodInStool;
                        rdoDEStoolBlackYes.Checked = (detail.DE_HadBlackTarryGrayStool.HasValue && detail.DE_HadBlackTarryGrayStool.Value);
                        rdoDEStoolBlackNo.Checked = (detail.DE_HadBlackTarryGrayStool.HasValue && !detail.DE_HadBlackTarryGrayStool.Value);
                        rdoDEStoolYellowYes.Checked = (detail.DE_HadYellowLightColoredStool.HasValue && detail.DE_HadYellowLightColoredStool.Value);
                        rdoDEStoolYellowNo.Checked = (detail.DE_HadYellowLightColoredStool.HasValue && !detail.DE_HadYellowLightColoredStool.Value);
                        rdoDERectalItchingYes.Checked = (detail.DE_HadRectalItching.HasValue && detail.DE_HadRectalItching.Value);
                        rdoDERectalItchingNo.Checked = (detail.DE_HadRectalItching.HasValue && !detail.DE_HadRectalItching.Value);
                        txtDEStoolFormedOrLoose.Text = detail.DE_StoolsFormedOrLoose;
                        rdoDEAlternatingConstDiarrYes.Checked = (detail.DE_HadConstipationDiarrhea.HasValue && detail.DE_HadConstipationDiarrhea.Value);
                        rdoDEAlternatingConstDiarrNo.Checked = (detail.DE_HadConstipationDiarrhea.HasValue && !detail.DE_HadConstipationDiarrhea.Value);
                        txtDEAlternatingConsDiarrFrequency.Text = detail.DE_ConstipationDiarrheaDetails;
                        rdoDEStrainToPassStoolYes.Checked = (detail.DE_HaveToStrainToPassStool.HasValue && detail.DE_HaveToStrainToPassStool.Value);
                        rdoDEStrainToPassStoolNo.Checked = (detail.DE_HaveToStrainToPassStool.HasValue && !detail.DE_HaveToStrainToPassStool.Value);
                        txtDEStrainToPassStoolFrequency.Text = detail.DE_HaveToStrainDetails;
                        txtDEPassGasFrequently.Text = detail.DE_PassGasFrequently;
                        txtDEBurpFrequently.Text = detail.DE_BurpFrequently;
                        rdoDEStoolStrongOdorYes.Checked = (detail.DE_StrongDisagreeableOdor.HasValue && detail.DE_StrongDisagreeableOdor.Value);
                        rdoDEStoolStrongOdorNo.Checked = (detail.DE_StrongDisagreeableOdor.HasValue && !detail.DE_StrongDisagreeableOdor.Value);
                        rdoDETraveledLast5YearsYes.Checked = (detail.DE_TraveledOutsideOfCanadaPast5Year.HasValue && detail.DE_TraveledOutsideOfCanadaPast5Year.Value);
                        rdoDETraveledLast5YearsNo.Checked = (detail.DE_TraveledOutsideOfCanadaPast5Year.HasValue && !detail.DE_TraveledOutsideOfCanadaPast5Year.Value);
                        txtDETraveledCountries.Text = detail.DE_TraveledCountries;
                        rdoDEHaveBeenCampingYes.Checked = (detail.DE_BeenCammpingPast5Year.HasValue && detail.DE_BeenCammpingPast5Year.Value);
                        rdoDEHaveBeenCampingNo.Checked = (detail.DE_BeenCammpingPast5Year.HasValue && !detail.DE_BeenCammpingPast5Year.Value);
                        rdoHaveFastedYes.Checked = (detail.DE_HadFasted.HasValue && detail.DE_HadFasted.Value);
                        rdoHaveFastedNo.Checked = (detail.DE_HadFasted.HasValue && !detail.DE_HadFasted.Value);
                        txtDEFastType.Text = detail.DE_FastType;


                        //
                        showHideFormControls();
                    }

                    tr.Commit();
                }
            } catch(Exception ex) {
                MessageBox.Show(this, "Failed to load Naturopathic Details. " + ex.Message);
                Program.log.Error("Failed to load Naturopathic Details.", ex);
            }
        }

        private void showHideFormControls() {
            txtDrugAllergies.Visible = (rdoDrugAllergiesYes.Checked);
            txtFoodAllergies.Visible = (rdoFoodAllergiesYes.Checked);
            txtAnimalAllergies.Visible = (rdoAnimalAllergiesYes.Checked);
            txtPlantAllergies.Visible = (rdoPlantAllergiesYes.Checked);
            txtEnvAllergies.Visible = (rdoEnvAllergiesYes.Checked);
            txtVHOther.Visible = (rdoVHOtherYes.Checked);
            txtPHExercise.Visible = (rdoPHExerciseYes.Checked);
            txtSHNapDuration.Visible = (rdoSHNapYes.Checked);
            txtFPeriodsBeginEveryDays.Visible = (rdoFCyclesRegularYes.Checked);
            txtFPeriodsLastDays.Visible = (rdoFCyclesRegularYes.Checked);
            txtFYeastInfectionFrequency.Visible = (rdoFHadYeastInfectionYes.Checked);
            txtFYeastInfectionTreated.Visible = (rdoFHadYeastInfectionYes.Checked);
            txtFPremenstrualSymptoms.Visible = (rdoFPremenstrualYes.Checked);
            txtDEAlternatingConsDiarrFrequency.Visible = (rdoDEAlternatingConstDiarrYes.Checked);
            txtDEStrainToPassStoolFrequency.Visible = (rdoDEStrainToPassStoolYes.Checked);
            txtDETraveledCountries.Visible = (rdoDETraveledLast5YearsYes.Checked);
            txtDEFastType.Visible = (rdoHaveFastedYes.Checked);
        }

        //Save changes
        public void Save(ISession session) {
            //Assign UpdatedBy/UpdatedTime fields
            EditingNaturopathicDetail.UpdatedBy = Program.LogonUser.DisplayName;
            EditingNaturopathicDetail.UpdatedTime = DateTime.Now;

            NaturopathicDetail detailToSave = session.Merge(EditingNaturopathicDetail);
            updateNaturopathicDetailInput(detailToSave);
            session.Save(detailToSave);
        }

        public void LoadFromTemplate(ITreatmentDetail templateDetail) {
            NaturopathicDetail detail = templateDetail as NaturopathicDetail;
            populateNaturopathicDetail(detail);
        }

        private void updateNaturopathicDetailInput(NaturopathicDetail detail) {
            //Populate Entity from form contorls

            //Your current health
            detail.HealthState = rdoBodyStateExcellent.Checked ? HealthState.Excellent :
                rdoBodyStateVeryGood.Checked ? HealthState.VeryGood :
                rdoBodyStateAverage.Checked ? HealthState.Average :
                rdoBodyStateFair.Checked ? HealthState.Fair :
                rdoBodyStatePoor.Checked ? HealthState.Poor :
                HealthState.Unknown;
            detail.CurrentEnergyLevel = (uint)numCurrentEnergyLevel.Value;
            detail.MorningEnergyLevel = (uint)numMorningEnergy.Value;
            detail.CurrentWeight = txtWeightCurrent.Text;
            detail.YearAgoWeight = txtWeightYearAgo.Text;
            detail.IdealWeight = txtWeightIdeal.Text;
            detail.CurrentHeight = txtHeight.Text;

            //Stress management
            detail.CurrentStressLevel = (uint)numStressLevel.Value;
            detail.StressIdentifiedBy = rdoIdentifyStressSelf.Checked ? SelfOther.Self :
                rdoIdentifyStressOther.Checked ? SelfOther.Other :
                SelfOther.Unknown;
            detail.StressedBehavior = txtActiviesWhenStressed.Text;
            detail.StressLocalizedOfBody = txtStressLocalizedOfBody.Text;
            detail.StressHandlingTools = txtStressHandlingTools.Text;

            //Allergies
            if (rdoDrugAllergiesYes.Checked) detail.HasDrugAllergy = true;
            else if (rdoDrugAllergiesNo.Checked) detail.HasDrugAllergy = false;
            detail.DrugAllergies = txtDrugAllergies.Text;

            if (rdoFoodAllergiesYes.Checked) detail.HasFoodAllergy = true;
            else if (rdoFoodAllergiesNo.Checked) detail.HasFoodAllergy = false;
            detail.FoodAllergies = txtFoodAllergies.Text;

            if (rdoAnimalAllergiesYes.Checked) detail.HasAnimalAllergy = true;
            else if (rdoAnimalAllergiesNo.Checked) detail.HasAnimalAllergy = false;
            detail.AnimalAllergies = txtAnimalAllergies.Text;

            if (rdoPlantAllergiesYes.Checked) detail.HasPlantAllergy = true;
            else if (rdoPlantAllergiesNo.Checked) detail.HasPlantAllergy = false;
            detail.PlantAllergies = txtPlantAllergies.Text;

            if (rdoEnvAllergiesYes.Checked) detail.HasEnvironmentalAllergy = true;
            else if (rdoEnvAllergiesNo.Checked) detail.HasEnvironmentalAllergy = false;
            detail.EnvironmentalAllergies = txtEnvAllergies.Text;

            //Accidents / Hospitalizations
            detail.MajorInjuries = txtMajorInjuries.Text;
            detail.Surgeries = txtPreviousSurgeries.Text;
            if (rdoHadFluVaccinePast5YearYes.Checked) detail.HadFluVaccineLastFiveYears = true;
            else if (rdoHadFluVaccinePast5YearNo.Checked) detail.HadFluVaccineLastFiveYears = false;
            if (rdoHadVaccineReactionYes.Checked) detail.HadReactionToVaccine = true;
            else if (rdoHadVaccineReactionNo.Checked) detail.HadReactionToVaccine = false;

            //Current / Past conditions
            detail.C_Allergies = chkAllergiesC.Checked;
            detail.P_Allergies = chkAllergiesP.Checked;
            detail.C_Anemia = chkAnemiaC.Checked;
            detail.P_Anemia = chkAnemiaP.Checked;
            detail.C_Stroke = chkStrokeC.Checked;
            detail.P_Stroke = chkStrokeP.Checked;
            detail.C_Depression = chkDepressionC.Checked;
            detail.P_Depression = chkDepressionP.Checked;
            detail.C_Asthma = chkAsthmaC.Checked;
            detail.P_Asthma = chkAsthmaP.Checked;
            detail.C_Measles = chkMeaslesC.Checked;
            detail.P_Measles = chkMeaslesP.Checked;
            detail.C_HeartDisease = chkHeartDiseaseC.Checked;
            detail.P_HeartDisease = chkHeartDiseaseP.Checked;
            detail.C_EmotionalAbuse = chkEmotionalAbuseC.Checked;
            detail.P_EmotionalAbuse = chkEmotionalAbuseP.Checked;
            detail.C_Eczema = chkEczemaC.Checked;
            detail.P_Eczema = chkEczemaP.Checked;
            detail.C_Mumps = chkMumpsC.Checked;
            detail.P_Mumps = chkMumpsP.Checked;
            detail.C_RheumaticFever = chkRheumaticFeverC.Checked;
            detail.P_RheumaticFever = chkRheumaticFeverP.Checked;
            detail.C_PhysicalMentalAbuse = chkPhyMentalAbuseC.Checked;
            detail.P_PhysicalMentalAbuse = chkPhyMentalAbuseP.Checked;
            detail.C_Psoriasis = chkPsoriasisC.Checked;
            detail.P_Psoriasis = chkPsoriasisP.Checked;
            detail.C_ChickenPox = chkChickenPoxC.Checked;
            detail.P_ChickenPox = chkChickenPoxP.Checked;
            detail.C_HighBloodPressure = chkHighBloodPressureC.Checked;
            detail.P_HighBloodPressure = chkHighBloodPressureP.Checked;
            detail.C_NumbnessTingling = chkNumbnessTinglingC.Checked;
            detail.P_NumbnessTingling = chkNumbnessTinglingP.Checked;
            detail.C_HayFever = chkHayFeverC.Checked;
            detail.P_HayFever = chkHayFeverP.Checked;
            detail.C_WhoopingCough = chkWhoopingCoughC.Checked;
            detail.P_WhoopingCough = chkWhoopingCoughP.Checked;
            detail.C_HighCholesterol = chkHighCholesterolC.Checked;
            detail.P_HighCholesterol = chkHighCholesterolP.Checked;
            detail.C_ColdHandsFeet = chkColdHandsFeetC.Checked;
            detail.P_ColdHandsFeet = chkColdHandsFeetP.Checked;
            detail.C_Pneumonia = chkPneumoniaC.Checked;
            detail.P_Pneumonia = chkPneumoniaP.Checked;
            detail.C_Shingles = chkShinglesC.Checked;
            detail.P_Shingles = chkShinglesP.Checked;
            detail.C_Cancer = chkCancerC.Checked;
            detail.P_Cancer = chkCancerP.Checked;
            detail.C_ThyroidProblems = chkThyroidC.Checked;
            detail.P_ThyroidProblems = chkThyroidP.Checked;
            detail.C_EarInfections = chkEarInfectionsC.Checked;
            detail.P_EarInfections = chkEarInfectionsP.Checked;
            detail.C_Diphtheria = chkDiphtheriaC.Checked;
            detail.P_Diphtheria = chkDiphtheriaP.Checked;
            detail.C_Type1Diabetes = chkType1DiabetesC.Checked;
            detail.P_Type1Diabetes = chkType1DiabetesP.Checked;
            detail.C_Warts = chkWartsC.Checked;
            detail.P_Warts = chkWartsP.Checked;
            detail.C_StrepThroat = chkStrepThroatC.Checked;
            detail.P_StrepThroat = chkStrepThroatP.Checked;
            detail.C_ScarletFever = chkScarletFeverC.Checked;
            detail.P_ScarletFever = chkScarletFeverP.Checked;
            detail.C_Type2Diabetes = chkType2DiabetesC.Checked;
            detail.P_Type2Diabetes = chkType2DiabetesP.Checked;
            detail.C_Mono = chkMonoC.Checked;
            detail.P_Mono = chkMonoP.Checked;
            detail.C_Tonsillitis = chkTonsillitisC.Checked;
            detail.P_Tonsillitis = chkTonsillitisP.Checked;
            detail.C_Polio = chkPolioC.Checked;
            detail.P_Polio = chkPolioP.Checked;
            detail.C_KidneyDisease = chkKidneyDiseaseC.Checked;
            detail.P_KidneyDisease = chkKidneyDiseaseP.Checked;
            detail.C_RheumatoidArthritis = chkRheumatoidArthritisC.Checked;
            detail.P_RheumatoidArthritis = chkRheumatoidArthritisP.Checked;
            detail.C_CankerSores = chkCankerSoresC.Checked;
            detail.P_CankerSores = chkCankerSoresP.Checked;
            detail.C_Smallpox = chkSmallpoxC.Checked;
            detail.P_Smallpox = chkSmallpoxP.Checked;
            detail.C_VisualProblems = chkVisualProblemsC.Checked;
            detail.P_VisualProblems = chkVisualProblemsP.Checked;
            detail.C_Osteoarthritis = chkOsteoarthritisC.Checked;
            detail.P_Osteoarthritis = chkOsteoarthritisP.Checked;
            detail.C_Jaundice = chkJaundiceC.Checked;
            detail.P_Jaundice = chkJaundiceP.Checked;
            detail.C_Tuberculosis = chkTuberculosisC.Checked;
            detail.P_Tuberculosis = chkTuberculosisP.Checked;
            detail.C_AutoimmuneDisease = chkAutoimmuneC.Checked;
            detail.P_AutoimmuneDisease = chkAutoimmuneP.Checked;
            detail.C_WeightProblems = chkWeightProblemsC.Checked;
            detail.P_WeightProblems = chkWeightProblemsP.Checked;
            detail.C_Alcoholism = chkAlcoholismC.Checked;
            detail.P_Alcoholism = chkAlcoholismP.Checked;
            detail.C_Malaria = chkMalariaC.Checked;
            detail.P_Malaria = chkMalariaP.Checked;
            detail.C_Epilepsy = chkEpilepsyC.Checked;
            detail.P_Epilepsy = chkEpilepsyP.Checked;
            detail.C_Gout = chkGoutC.Checked;
            detail.P_Gout = chkGoutP.Checked;
            detail.C_Hepatitis = chkHepatitisC.Checked;
            detail.P_Hepatitis = chkHepatitisP.Checked;
            detail.C_Gallstones = chkGallstonesC.Checked;
            detail.P_Gallstones = chkGallstonesP.Checked;
            detail.C_VaricoseVeins = chkVaricoseVeinsC.Checked;
            detail.P_VaricoseVeins = chkVaricoseVeinsP.Checked;
            detail.CP_Other = txtCPOther.Text;
            detail.CP_NeverWellSince = txtCPNeverWellSince.Text;

            //Currently use
            detail.CU_Alcohol = txtCUAlcohol.Text;
            detail.CU_Tobacco = txtCUTobacco.Text;
            detail.CU_Hormones = txtCUHormones.Text;
            detail.CU_Coffee = txtCUCoffee.Text;
            detail.CU_Cortisone = txtCUCortisone.Text;
            detail.CU_BlackTea = txtCUBlackTea.Text;
            detail.CU_Sedatives = txtCUSedatives.Text;
            detail.CU_Laxatives = txtCULaxatives.Text;
            detail.CU_Antacids = txtCUAntacids.Text;

            //Current prescription medications
            detail.CPM_Name1 = txtCPMName1.Text;
            detail.CPM_Reason1 = txtCPMReason1.Text;
            detail.CPM_Amount1 = txtCPMDailyAmount1.Text;
            detail.CPM_PrescriptionDate1 = txtCPMWhenPrescribed1.Text;
            detail.CPM_DrugCategory1 = txtCPMDrugCategory1.Text;

            detail.CPM_Name2 = txtCPMName2.Text;
            detail.CPM_Reason2 = txtCPMReason2.Text;
            detail.CPM_Amount2 = txtCPMDailyAmount2.Text;
            detail.CPM_PrescriptionDate2 = txtCPMWhenPrescribed2.Text;
            detail.CPM_DrugCategory2 = txtCPMDrugCategory2.Text;

            detail.CPM_Name3 = txtCPMName3.Text;
            detail.CPM_Reason3 = txtCPMReason3.Text;
            detail.CPM_Amount3 = txtCPMDailyAmount3.Text;
            detail.CPM_PrescriptionDate3 = txtCPMWhenPrescribed3.Text;
            detail.CPM_DrugCategory3 = txtCPMDrugCategory3.Text;

            detail.CPM_Name4 = txtCPMName4.Text;
            detail.CPM_Reason4 = txtCPMReason4.Text;
            detail.CPM_Amount4 = txtCPMDailyAmount4.Text;
            detail.CPM_PrescriptionDate4 = txtCPMWhenPrescribed4.Text;
            detail.CPM_DrugCategory4 = txtCPMDrugCategory4.Text;

            detail.CPM_Name5 = txtCPMName5.Text;
            detail.CPM_Reason5 = txtCPMReason5.Text;
            detail.CPM_Amount5 = txtCPMDailyAmount5.Text;
            detail.CPM_PrescriptionDate5 = txtCPMWhenPrescribed5.Text;
            detail.CPM_DrugCategory5 = txtCPMDrugCategory5.Text;

            //Vitamins/Herbs
            detail.VH_Name1 = txtVHName1.Text;
            detail.VH_Brand1 = txtVHBrand1.Text;
            detail.VH_Amount1 = txtVHAmount1.Text;
            detail.VH_Reason1 = txtVHReason1.Text;
            detail.VH_SuggestedBy1 = rdoVHWhoSuggestedSelf1.Checked ? SelfOther.Self :
                rdoVHWhoSuggestedOther1.Checked ? SelfOther.Other :
                SelfOther.Unknown;
            detail.VH_Helped1 = rdoVHHasHelpedYes1.Checked ? YesNo.Yes :
                rdoVHHasHelpedNo1.Checked ? YesNo.No :
                YesNo.Unknown;

            detail.VH_Name2 = txtVHName2.Text;
            detail.VH_Brand2 = txtVHBrand2.Text;
            detail.VH_Amount2 = txtVHAmount2.Text;
            detail.VH_Reason2 = txtVHReason2.Text;
            detail.VH_SuggestedBy2 = rdoVHWhoSuggestedSelf2.Checked ? SelfOther.Self :
                rdoVHWhoSuggestedOther2.Checked ? SelfOther.Other :
                SelfOther.Unknown;
            detail.VH_Helped2 = rdoVHHasHelpedYes2.Checked ? YesNo.Yes :
                rdoVHHasHelpedNo2.Checked ? YesNo.No :
                YesNo.Unknown;

            detail.VH_Name3 = txtVHName3.Text;
            detail.VH_Brand3 = txtVHBrand3.Text;
            detail.VH_Amount3 = txtVHAmount3.Text;
            detail.VH_Reason3 = txtVHReason3.Text;
            detail.VH_SuggestedBy3 = rdoVHWhoSuggestedSelf3.Checked ? SelfOther.Self :
                rdoVHWhoSuggestedOther3.Checked ? SelfOther.Other :
                SelfOther.Unknown;
            detail.VH_Helped3 = rdoVHHasHelpedYes3.Checked ? YesNo.Yes :
                rdoVHHasHelpedNo3.Checked ? YesNo.No :
                YesNo.Unknown;

            detail.VH_Name4 = txtVHName4.Text;
            detail.VH_Brand4 = txtVHBrand4.Text;
            detail.VH_Amount4 = txtVHAmount4.Text;
            detail.VH_Reason4 = txtVHReason4.Text;
            detail.VH_SuggestedBy4 = rdoVHWhoSuggestedSelf4.Checked ? SelfOther.Self :
                rdoVHWhoSuggestedOther4.Checked ? SelfOther.Other :
                SelfOther.Unknown;
            detail.VH_Helped4 = rdoVHHasHelpedYes4.Checked ? YesNo.Yes :
                rdoVHHasHelpedNo4.Checked ? YesNo.No :
                YesNo.Unknown;

            detail.VH_Name5 = txtVHName5.Text;
            detail.VH_Brand5 = txtVHBrand5.Text;
            detail.VH_Amount5 = txtVHAmount5.Text;
            detail.VH_Reason5 = txtVHReason5.Text;
            detail.VH_SuggestedBy5 = rdoVHWhoSuggestedSelf5.Checked ? SelfOther.Self :
                rdoVHWhoSuggestedOther5.Checked ? SelfOther.Other :
                SelfOther.Unknown;
            detail.VH_Helped5 = rdoVHHasHelpedYes5.Checked ? YesNo.Yes :
                rdoVHHasHelpedNo5.Checked ? YesNo.No :
                YesNo.Unknown;

            if (rdoVHOtherYes.Checked) detail.VH_HasOtherSupplementation = true;
            else if (rdoVHOtherNo.Checked) detail.VH_HasOtherSupplementation = false;

            detail.VH_OtherSupplementation = txtVHOther.Text;

            //Family History
            detail.FH_CancerMother = txtFHCancerM.Text;
            detail.FH_CancerFather = txtFHCancerF.Text;
            detail.FH_CancerSibling = txtFHCancerS.Text;
            detail.FH_CancerGrandparent = txtFHCancerG.Text;

            detail.FH_RheumatoidMother = txtFHRheumatoidM.Text;
            detail.FH_RheumatoidFather = txtFHRheumatoidF.Text;
            detail.FH_RheumatoidSibling = txtFHRheumatoidS.Text;
            detail.FH_RheumatoidGrandparent = txtFHRheumatoidG.Text;

            detail.FH_TuberculosisMother = txtFHTuberculosisM.Text;
            detail.FH_TuberculosisFather = txtFHTuberculosisF.Text;
            detail.FH_TuberculosisSibling = txtFHTuberculosisS.Text;
            detail.FH_TuberculosisGrandparent = txtFHTuberculosisG.Text;

            detail.FH_OsteoarthritisMother = txtFHOsteoarthritisM.Text;
            detail.FH_OsteoarthritisFather = txtFHOsteoarthritisF.Text;
            detail.FH_OsteoarthritisSibling = txtFHOsteoarthritisS.Text;
            detail.FH_OsteoarthritisGrandparent = txtFHOsteoarthritisG.Text;

            detail.FH_HeartDiseaseMother = txtFHHeartDiseaseM.Text;
            detail.FH_HeartDiseaseFather = txtFHHeartDiseaseF.Text;
            detail.FH_HeartDiseaseSibling = txtFHHeartDiseaseS.Text;
            detail.FH_HeartDiseaseGrandparent = txtFHHeartDiseaseG.Text;

            detail.FH_AllergiesMother = txtFHAllergiesM.Text;
            detail.FH_AllergiesFather = txtFHAllergiesF.Text;
            detail.FH_AllergiesSibling = txtFHAllergiesS.Text;
            detail.FH_AllergiesGrandparent = txtFHAllergiesG.Text;

            detail.FH_StrokeMother = txtFHStrokeM.Text;
            detail.FH_StrokeFather = txtFHStrokeF.Text;
            detail.FH_StrokeSibling = txtFHStrokeS.Text;
            detail.FH_StrokeGrandparent = txtFHStrokeG.Text;

            detail.FH_AsthmaMother = txtFHAsthmaM.Text;
            detail.FH_AsthmaFather = txtFHAsthmaF.Text;
            detail.FH_AsthmaSibling = txtFHAsthmaS.Text;
            detail.FH_AsthmaGrandparent = txtFHAsthmaG.Text;

            detail.FH_HighBloodPressureMother = txtFHHighBloodPressureM.Text;
            detail.FH_HighBloodPressureFather = txtFHHighBloodPressureF.Text;
            detail.FH_HighBloodPressureSibling = txtFHHighBloodPressureS.Text;
            detail.FH_HighBloodPressureGrandparent = txtFHHighBloodPressureG.Text;

            detail.FH_DiabetesTypeIMother = txtFHDiabetesTypeIM.Text;
            detail.FH_DiabetesTypeIFather = txtFHDiabetesTypeIF.Text;
            detail.FH_DiabetesTypeISibling = txtFHDiabetesTypeIS.Text;
            detail.FH_DiabetesTypeIGrandparent = txtFHDiabetesTypeIG.Text;

            detail.FH_DiabetesTypeIIMother = txtFHDiabetesTypeIIM.Text;
            detail.FH_DiabetesTypeIIFather = txtFHDiabetesTypeIIF.Text;
            detail.FH_DiabetesTypeIISibling = txtFHDiabetesTypeIIS.Text;
            detail.FH_DiabetesTypeIIGrandparent = txtFHDiabetesTypeIIG.Text;

            detail.FH_HighCholesterolMother = txtFHHighCholesterolM.Text;
            detail.FH_HighCholesterolFather = txtFHHighCholesterolF.Text;
            detail.FH_HighCholesterolSibling = txtFHHighCholesterolS.Text;
            detail.FH_HighCholesterolGrandparent = txtFHHighCholesterolG.Text;

            detail.FH_DepressionMother = txtFHDepressionM.Text;
            detail.FH_DepressionFather = txtFHDepressionF.Text;
            detail.FH_DepressionSibling = txtFHDepressionS.Text;
            detail.FH_DepressionGrandparent = txtFHDepressionG.Text;

            detail.FH_KidneyDiseaseMother = txtFHKidneyDiseaseM.Text;
            detail.FH_KidneyDiseaseFather = txtFHKidneyDiseaseF.Text;
            detail.FH_KidneyDiseaseSibling = txtFHKidneyDiseaseS.Text;
            detail.FH_KidneyDiseaseGrandparent = txtFHKidneyDiseaseG.Text;

            detail.FH_OtherName = txtFHOtherName.Text;
            detail.FH_OtherMother = txtFHOtherM.Text;
            detail.FH_OtherFather = txtFHOtherF.Text;
            detail.FH_OtherSibling = txtFHOtherS.Text;
            detail.FH_OtherGrandparent = txtFHOtherG.Text;

            //Personal Habits
            detail.PH_EnjoyMost = txtPHEnjoyMost.Text;
            detail.PH_MainInterests = txtPHHobbies.Text;
            detail.PH_WorryMost = txtPHWorryMost.Text;
            detail.PH_WhatNurtures = txtPHNurtures.Text;
            if (rdoPHExerciseYes.Checked) detail.PH_DoExercise = true;
            else if (rdoPHExerciseNo.Checked) detail.PH_DoExercise = false;
            detail.PH_ExerciseDetails = txtPHExercise.Text;
            if (rdoPHReligiousYes.Checked) detail.PH_ReligiousPractice = true;
            else if (rdoPHReligiousNo.Checked) detail.PH_ReligiousPractice = false;
            if (rdoPHBodyTemperatureWarmer.Checked) detail.PH_BodyTemperature = BodyTemperature.Warmer;
            if (rdoPHBodyTemperatureCooler.Checked) detail.PH_BodyTemperature = BodyTemperature.Cooler;
            if (rdoPHBodyTemperatureAverage.Checked) detail.PH_BodyTemperature = BodyTemperature.Average;
            if (rdoPHEnjoyWorkYes.Checked) detail.PH_EnjoyWork = true;
            if (rdoPHEnjoyWorkNo.Checked) detail.PH_EnjoyWork = false;
            if (rdoPHTakeVacationsYes.Checked) detail.PH_TakeVacations = true;
            if (rdoPHTakeVacationsNo.Checked) detail.PH_TakeVacations = false;
            detail.PH_ColdFluDetails = txtPHGetColdFluInAYear.Text;

            //Sleep Habits
            detail.SH_SleepQuality = (uint)numSHSleepQuality.Value;
            if (rdoSHFallAsleepProblemYes.Checked) detail.SH_FallingAsleepProblem = true;
            if (rdoSHFallAsleepProblemNo.Checked) detail.SH_FallingAsleepProblem = false;
            if (rdoSHStayingAsleepProblemYes.Checked) detail.SH_StayingAsleepProblem = true;
            if (rdoSHStayingAsleepProblemNo.Checked) detail.SH_StayingAsleepProblem = false;
            detail.SH_HoursInSleep = txtSHHoursSleep.Text;
            detail.SH_HoursNeededInSleep = txtSHHoursNeededSleep.Text;
            if (rdoSHWakeRefreshedYes.Checked) detail.SH_WakeRefreshed = true;
            if (rdoSHWakeRefreshedNo.Checked) detail.SH_WakeRefreshed = false;
            if (rdoSHNapYes.Checked) detail.SH_TakeNap = true;
            if (rdoSHNapNo.Checked) detail.SH_TakeNap = false;
            detail.SH_NapDuration = txtSHNapDuration.Text;

            //Female
            detail.F_FirstMensesAge = txtFFirstMensesAge.Text;
            detail.F_PeriodsStoppedAge = txtFPeriodsStoppedAge.Text;
            if (rdoFCyclesRegularYes.Checked) detail.F_CyclesRegular = true;
            if (rdoFCyclesRegularNo.Checked) detail.F_CyclesRegular = false;
            detail.F_PeriodBeginsEveryDays = txtFPeriodsBeginEveryDays.Text;
            detail.F_PeriodLastsDays = txtFPeriodsLastDays.Text;
            detail.F_PeriodFlow = txtFPeriodFlow.Text;
            detail.F_PeriodBloodColor = txtFPeriodColor.Text;
            if (rdoFAnyClotsYes.Checked) detail.F_AnyClots = true;
            if (rdoFAnyClotsNo.Checked) detail.F_AnyClots = false;
            if (rdoFAnyCrampsYes.Checked) detail.F_AnyCramps = true;
            if (rdoFAnyCrampsNo.Checked) detail.F_AnyCramps = false;
            if (rdoFAnySpottingYes.Checked) detail.F_AnySpottingBleeding = true;
            if (rdoFAnySpottingNo.Checked) detail.F_AnySpottingBleeding = false;
            if (rdoFSpottingEveryMonthYes.Checked) detail.F_SpottingBleedingEveryMonth = true;
            if (rdoFSpottingEveryMonthNo.Checked) detail.F_SpottingBleedingEveryMonth = false;
            if (rdoFHadYeastInfectionYes.Checked) detail.F_YeastInfectionInPast = true;
            if (rdoFHadYeastInfectionNo.Checked) detail.F_YeastInfectionInPast = false;
            detail.F_YeastInfectionFrequency = txtFYeastInfectionFrequency.Text;
            detail.F_YeastInfectionTreatment = txtFYeastInfectionTreated.Text;
            if (rdoFPremenstrualYes.Checked) detail.F_AnyPremenstrualSymptoms = true;
            if (rdoFPremenstrualNo.Checked) detail.F_AnyPremenstrualSymptoms = false;
            detail.F_PremenstrualSymptomsDetails = txtFPremenstrualSymptoms.Text;
            detail.F_NumberOfPregnancies = txtFNumOfPregnancies.Text;
            detail.F_NumberOfMiscarriages = txtFNumOfMiscarriages.Text;
            detail.F_NumberOfLiveBirths = txtFNumOfLiveBirths.Text;
            detail.F_AnyPregancyProblem = txtFGettingPregnantProblem.Text;
            if (rdoFRegularSmearsYes.Checked) detail.F_RegularPAP = true;
            if (rdoFRegularSmearsNo.Checked) detail.F_RegularPAP = true;
            if (rdoFAbnormalPAPYes.Checked) detail.F_AnyAbnormalPAP = true;
            if (rdoFAbnormalPAPNo.Checked) detail.F_AnyAbnormalPAP = false;
            if (rdoFRegularBreastSelfExamYes.Checked) detail.F_RegularBreastSelfExam = true;
            if (rdoFRegularBreastSelfExamNo.Checked) detail.F_RegularBreastSelfExam = false;
            if (rdoFAnyBreastLumpsYes.Checked) detail.F_NoticedBreastLumps = true;
            if (rdoFAnyBreastLumpsNo.Checked) detail.F_NoticedBreastLumps = false;

            //Digestion and Elimination
            if (rdoDEGasBloatingYes.Checked) detail.DE_ProblemWithGasBloating = true;
            if (rdoDEGasBloatingNo.Checked) detail.DE_ProblemWithGasBloating = false;
            detail.DE_Hemorrhoids = chkDEHemorrhoids.Checked;
            detail.DE_RectalBleeding = chkDERectalBleeding.Checked;
            detail.DE_Parasites = chkDEParasites.Checked;
            if (rdoDEProblemOften.Checked) detail.DE_ProblemFrequency = Frequency.Often;
            if (rdoDEProblemSometimes.Checked) detail.DE_ProblemFrequency = Frequency.Sometimes;
            if (rdoDEProblemNever.Checked) detail.DE_ProblemFrequency = Frequency.Never;
            detail.DE_ProblemSeverity = txtDESevere.Text;
            detail.DE_ProblemDuration = txtDEProblemDuration.Text;
            detail.DE_BowelMovements = txtDEBowelMovementsFrequency.Text;
            detail.DE_AnyBloodInStool = txtDEAnyBloodMucousInStool.Text;
            if (rdoDEStoolBlackYes.Checked) detail.DE_HadBlackTarryGrayStool = true;
            if (rdoDEStoolBlackNo.Checked) detail.DE_HadBlackTarryGrayStool = false;
            if (rdoDEStoolYellowYes.Checked) detail.DE_HadYellowLightColoredStool = true;
            if (rdoDEStoolYellowNo.Checked) detail.DE_HadYellowLightColoredStool = false;
            if (rdoDERectalItchingYes.Checked) detail.DE_HadRectalItching = true;
            if (rdoDERectalItchingNo.Checked) detail.DE_HadRectalItching = false;
            detail.DE_StoolsFormedOrLoose = txtDEStoolFormedOrLoose.Text;
            if (rdoDEAlternatingConstDiarrYes.Checked) detail.DE_HadConstipationDiarrhea = true;
            if (rdoDEAlternatingConstDiarrNo.Checked) detail.DE_HadConstipationDiarrhea = false;
            detail.DE_ConstipationDiarrheaDetails = txtDEAlternatingConsDiarrFrequency.Text;
            if (rdoDEStrainToPassStoolYes.Checked) detail.DE_HaveToStrainToPassStool = true;
            if (rdoDEStrainToPassStoolNo.Checked) detail.DE_HaveToStrainToPassStool = false;
            detail.DE_HaveToStrainDetails = txtDEStrainToPassStoolFrequency.Text;
            detail.DE_PassGasFrequently = txtDEPassGasFrequently.Text;
            detail.DE_BurpFrequently = txtDEBurpFrequently.Text;
            if (rdoDEStoolStrongOdorYes.Checked) detail.DE_StrongDisagreeableOdor = true;
            if (rdoDEStoolStrongOdorNo.Checked) detail.DE_StrongDisagreeableOdor = false;
            if (rdoDETraveledLast5YearsYes.Checked) detail.DE_TraveledOutsideOfCanadaPast5Year = true;
            if (rdoDETraveledLast5YearsNo.Checked) detail.DE_TraveledOutsideOfCanadaPast5Year = false;
            detail.DE_TraveledCountries = txtDETraveledCountries.Text;
            if (rdoDEHaveBeenCampingYes.Checked) detail.DE_BeenCammpingPast5Year = true;
            if (rdoDEHaveBeenCampingNo.Checked) detail.DE_BeenCammpingPast5Year = false;
            if (rdoHaveFastedYes.Checked) detail.DE_HadFasted = true;
            if (rdoHaveFastedNo.Checked) detail.DE_HadFasted = false;
            detail.DE_FastType = txtDEFastType.Text;
        }

        private void UCNaturopathicDetail_Load(object sender, EventArgs e) {
            /*
             * Load controls in the OnLoad event other than in the contructor. 
             * Otherwise, the control locations will not be accurate.
             * Still don't know why.
             */
            loadDetail();
        }

        private void rdoDrugAllergiesYes_CheckedChanged(object sender, EventArgs e) {
            txtDrugAllergies.Visible = (rdoDrugAllergiesYes.Checked);
        }

        private void rdoFoodAllergiesYes_CheckedChanged(object sender, EventArgs e) {
            txtFoodAllergies.Visible = (rdoFoodAllergiesYes.Checked);
        }

        private void rdoAnimalAllergiesYes_CheckedChanged(object sender, EventArgs e) {
            txtAnimalAllergies.Visible = (rdoAnimalAllergiesYes.Checked);
        }

        private void rdoPlantAllergiesYes_CheckedChanged(object sender, EventArgs e) {
            txtPlantAllergies.Visible = (rdoPlantAllergiesYes.Checked);
        }

        private void rdoEnvAllergiesYes_CheckedChanged(object sender, EventArgs e) {
            txtEnvAllergies.Visible = (rdoEnvAllergiesYes.Checked);
        }

        private void rdoVHOtherYes_CheckedChanged(object sender, EventArgs e) {
            txtVHOther.Visible = (rdoVHOtherYes.Checked);
        }

        private void rdoPHExerciseYes_CheckedChanged(object sender, EventArgs e) {
            txtPHExercise.Visible = (rdoPHExerciseYes.Checked);
        }

        private void rdoSHNapYes_CheckedChanged(object sender, EventArgs e) {
            txtSHNapDuration.Visible = (rdoSHNapYes.Checked);
        }

        private void rdoFCyclesRegularYes_CheckedChanged(object sender, EventArgs e) {
            txtFPeriodsBeginEveryDays.Visible = (rdoFCyclesRegularYes.Checked);
            txtFPeriodsLastDays.Visible = (rdoFCyclesRegularYes.Checked);
        }

        private void rdoFHadYeastInfectionYes_CheckedChanged(object sender, EventArgs e) {
            txtFYeastInfectionFrequency.Visible = (rdoFHadYeastInfectionYes.Checked);
            txtFYeastInfectionTreated.Visible = (rdoFHadYeastInfectionYes.Checked);
        }

        private void rdoFPremenstrualYes_CheckedChanged(object sender, EventArgs e) {
            txtFPremenstrualSymptoms.Visible = (rdoFPremenstrualYes.Checked);
        }

        private void rdoDEAlternatingConstDiarrYes_CheckedChanged(object sender, EventArgs e) {
            txtDEAlternatingConsDiarrFrequency.Visible = (rdoDEAlternatingConstDiarrYes.Checked);
        }

        private void rdoDEStrainToPassStoolYes_CheckedChanged(object sender, EventArgs e) {
            txtDEStrainToPassStoolFrequency.Visible = (rdoDEStrainToPassStoolYes.Checked);
        }

        private void rdoDETraveledLast5YearsYes_CheckedChanged(object sender, EventArgs e) {
            txtDETraveledCountries.Visible = (rdoDETraveledLast5YearsYes.Checked);
        }

        private void rdoHaveFastedYes_CheckedChanged(object sender, EventArgs e) {
            txtDEFastType.Visible = (rdoHaveFastedYes.Checked);
        }

    }
}
