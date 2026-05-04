-- MySQL dump 10.13  Distrib 5.5.25a, for Win64 (x86)
--
-- Host: localhost    Database: EasyHealthcare_DEV
-- ------------------------------------------------------
-- Server version	5.5.25a-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `accountbalance`
--

DROP TABLE IF EXISTS `accountbalance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `accountbalance` (
  `ACB_ACBId` int(11) NOT NULL,
  `ACB_PTNId` int(11) NOT NULL,
  `ACB_TransactionAmount` decimal(10,2) NOT NULL,
  `ACB_TransactionTime` datetime NOT NULL,
  `ACB_TransactionDescription` varchar(50) NOT NULL,
  `ACB_IsTestData` bit(1) NOT NULL,
  `ACB_CreatedTime` datetime NOT NULL,
  `ACB_UpdatedTime` datetime DEFAULT NULL,
  `ACB_CreatedBy` varchar(50) NOT NULL,
  `ACB_UpdatedBy` varchar(50) DEFAULT NULL,
  `ACB_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ACB_ACBId`),
  KEY `fk_AccountBalance_Patient_idx` (`ACB_PTNId`),
  CONSTRAINT `fk_AccountBalance_Patient` FOREIGN KEY (`ACB_PTNId`) REFERENCES `patient` (`PTN_PTNId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `accountbalance`
--

LOCK TABLES `accountbalance` WRITE;
/*!40000 ALTER TABLE `accountbalance` DISABLE KEYS */;
/*!40000 ALTER TABLE `accountbalance` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `acupuncturedetail`
--

DROP TABLE IF EXISTS `acupuncturedetail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `acupuncturedetail` (
  `ACP_ACPId` int(11) NOT NULL,
  `ACP_ITRId` int(11) NOT NULL,
  `ACP_TongueDGRId` int(11) NOT NULL DEFAULT '37196',
  `ACP_Pulse` varchar(250) DEFAULT NULL,
  `ACP_Tongue` varchar(250) DEFAULT NULL,
  `ACP_BloodPressure` varchar(250) DEFAULT NULL,
  `ACP_BloodSugar` varchar(250) DEFAULT NULL,
  `ACP_Subjective` varchar(1000) DEFAULT NULL,
  `ACP_TCMDiagnosis` varchar(250) DEFAULT NULL,
  `ACP_TreatmentPlan` text,
  `ACP_IsTestData` bit(1) NOT NULL,
  `ACP_CreatedTime` datetime NOT NULL,
  `ACP_UpdatedTime` datetime DEFAULT NULL,
  `ACP_CreatedBy` varchar(50) NOT NULL,
  `ACP_UpdatedBy` varchar(50) DEFAULT NULL,
  `ACP_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ACP_ACPId`),
  UNIQUE KEY `ACP_ITRId_UNIQUE` (`ACP_ITRId`),
  KEY `fk_acupuncture_initialtreatment` (`ACP_ITRId`),
  KEY `fk_acupuncturedetail_diagram` (`ACP_TongueDGRId`),
  CONSTRAINT `fk_acupuncturedetail_diagram` FOREIGN KEY (`ACP_TongueDGRId`) REFERENCES `diagram` (`DGR_DGRId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_acupuncture_initialtreatment` FOREIGN KEY (`ACP_ITRId`) REFERENCES `initialtreatment` (`ITR_ITRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `acupuncturedetail`
--

LOCK TABLES `acupuncturedetail` WRITE;
/*!40000 ALTER TABLE `acupuncturedetail` DISABLE KEYS */;
/*!40000 ALTER TABLE `acupuncturedetail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `acupuncturefollowupdetail`
--

DROP TABLE IF EXISTS `acupuncturefollowupdetail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `acupuncturefollowupdetail` (
  `AFD_AFDId` int(11) NOT NULL,
  `AFD_FTRId` int(11) NOT NULL,
  `AFD_TongueDGRId` int(11) NOT NULL,
  `AFD_Pulse` varchar(250) DEFAULT NULL,
  `AFD_Tongue` varchar(250) DEFAULT NULL,
  `AFD_BloodPressure` varchar(250) DEFAULT NULL,
  `AFD_BloodSugar` varchar(250) DEFAULT NULL,
  `AFD_Subjective` varchar(1000) DEFAULT NULL,
  `AFD_TCMDiagnosis` varchar(250) DEFAULT NULL,
  `AFD_TreatmentPlan` text,
  `AFD_IsTestData` bit(1) NOT NULL,
  `AFD_CreatedTime` datetime NOT NULL,
  `AFD_UpdatedTime` datetime DEFAULT NULL,
  `AFD_CreatedBy` varchar(50) NOT NULL,
  `AFD_UpdatedBy` varchar(50) DEFAULT NULL,
  `AFD_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`AFD_AFDId`),
  KEY `fk_acupuncturefollowupdetail_acupuncturedetail` (`AFD_FTRId`),
  KEY `fk_acupuncturefollowupdetail_diagram` (`AFD_TongueDGRId`),
  CONSTRAINT `fk_acupuncturefollowupdetail_diagram` FOREIGN KEY (`AFD_TongueDGRId`) REFERENCES `diagram` (`DGR_DGRId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_acupuncturefollowupdetail_followuptreatment` FOREIGN KEY (`AFD_FTRId`) REFERENCES `followuptreatment` (`FTR_FTRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `acupuncturefollowupdetail`
--

LOCK TABLES `acupuncturefollowupdetail` WRITE;
/*!40000 ALTER TABLE `acupuncturefollowupdetail` DISABLE KEYS */;
/*!40000 ALTER TABLE `acupuncturefollowupdetail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `chiropracticdetail`
--

DROP TABLE IF EXISTS `chiropracticdetail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `chiropracticdetail` (
  `CHP_CHPId` int(11) NOT NULL,
  `CHP_ITRId` int(11) NOT NULL,
  `CHP_ChiropracticDGRId` int(11) NOT NULL,
  `CHP_ChiefComplaint` text,
  `CHP_HistoryOfCondition` text,
  `CHP_AggravatingFactors` text,
  `CHP_AssociatedSymptoms` text,
  `CHP_RelievingFactors` text,
  `CHP_PreviousTX` text,
  `CHP_Medical` text,
  `CHP_FamilyHistory` text,
  `CHP_LifeStyle` text,
  `CHP_PastIllness` text,
  `CHP_NumOfCareGiverNeeded` int(11) DEFAULT NULL,
  `CHP_Employer` varchar(50) DEFAULT NULL,
  `CHP_LastWorkDate` datetime DEFAULT NULL,
  `CHP_JobTitleAndDuties` varchar(50) DEFAULT NULL,
  `CHP_Education` varchar(255) DEFAULT NULL,
  `CHP_DominantHand` int(11) DEFAULT NULL,
  `CHP_ShoulderYergason` int(11) DEFAULT NULL,
  `CHP_ShoulderSpeed` int(11) DEFAULT NULL,
  `CHP_ShoulderDropArm` int(11) DEFAULT NULL,
  `CHP_ShoulderEmptyCan` int(11) DEFAULT NULL,
  `CHP_ShoulderOBrien` int(11) DEFAULT NULL,
  `CHP_ShoulderNeerImpingement` int(11) DEFAULT NULL,
  `CHP_ShoulderKennedyHawkins` int(11) DEFAULT NULL,
  `CHP_ShoulderHornblower` int(11) DEFAULT NULL,
  `CHP_ShoulderLiftOff` int(11) DEFAULT NULL,
  `CHP_ShoulderAnteriorSlide` int(11) DEFAULT NULL,
  `CHP_ShoulderLoadAndShift` int(11) DEFAULT NULL,
  `CHP_ShoulderCrank` int(11) DEFAULT NULL,
  `CHP_ShoulderAnteriorApprehension` int(11) DEFAULT NULL,
  `CHP_ShoulderJobeRelocation` int(11) DEFAULT NULL,
  `CHP_ShoulderPosteriorApprehension` int(11) DEFAULT NULL,
  `CHP_ShoulderSupraspinatus` int(11) DEFAULT NULL,
  `CHP_ShoulderInfraspinatus` int(11) DEFAULT NULL,
  `CHP_ShoulderTeresMinor` int(11) DEFAULT NULL,
  `CHP_ShoulderTeresMajor` int(11) DEFAULT NULL,
  `CHP_ShoulderSubscapularis` int(11) DEFAULT NULL,
  `CHP_ElbowVarusStress` int(11) DEFAULT NULL,
  `CHP_ElbowVargusStress` int(11) DEFAULT NULL,
  `CHP_ElbowCozen` int(11) DEFAULT NULL,
  `CHP_ElbowMill` int(11) DEFAULT NULL,
  `CHP_ElbowGolferElbow` int(11) DEFAULT NULL,
  `CHP_ElbowMedianNerveResistedPronationEX` int(11) DEFAULT NULL,
  `CHP_ElbowMedianNerveResistedFLSupination` int(11) DEFAULT NULL,
  `CHP_ElbowMedianNerveResistedFLofPIP` int(11) DEFAULT NULL,
  `CHP_ElbowAINResistedPronationFL` int(11) DEFAULT NULL,
  `CHP_ElbowRadialNerveResistedSupination` int(11) DEFAULT NULL,
  `CHP_ElbowRadialNerveResistedEXofThirdFinger` int(11) DEFAULT NULL,
  `CHP_ElbowUlnarNerveTinelAtElbow` int(11) DEFAULT NULL,
  `CHP_ElbowUlnarNerveFullElbowFL` int(11) DEFAULT NULL,
  `CHP_WristHandFinkelstein` int(11) DEFAULT NULL,
  `CHP_WristHandFroment` int(11) DEFAULT NULL,
  `CHP_WristHandPinchGrip` int(11) DEFAULT NULL,
  `CHP_WristHandPhalen` int(11) DEFAULT NULL,
  `CHP_WristHandReversePhalen` int(11) DEFAULT NULL,
  `CHP_WristHandTinelAtWrist` int(11) DEFAULT NULL,
  `CHP_WristHandAllen` int(11) DEFAULT NULL,
  `CHP_WristHandBunnelLittler` int(11) DEFAULT NULL,
  `CHP_WristHandGripStrength` int(11) DEFAULT NULL,
  `CHP_WristHandPinchStrength` int(11) DEFAULT NULL,
  `CHP_HipTrueLegLength` int(11) DEFAULT NULL,
  `CHP_HipApparentLegLength` int(11) DEFAULT NULL,
  `CHP_HipSLR` int(11) DEFAULT NULL,
  `CHP_HipThomas` int(11) DEFAULT NULL,
  `CHP_HipPatrickFabers` int(11) DEFAULT NULL,
  `CHP_HipAPSICompression` int(11) DEFAULT NULL,
  `CHP_HipGaenslen` int(11) DEFAULT NULL,
  `CHP_HipNobelCompression` int(11) DEFAULT NULL,
  `CHP_HipPsoasStrength` int(11) DEFAULT NULL,
  `CHP_HipOber` int(11) DEFAULT NULL,
  `CHP_HipEly` int(11) DEFAULT NULL,
  `CHP_HipYeoman` int(11) DEFAULT NULL,
  `CHP_HipHibb` int(11) DEFAULT NULL,
  `CHP_HipPASICompression` int(11) DEFAULT NULL,
  `CHP_HipTrendelenburg` int(11) DEFAULT NULL,
  `CHP_HipOrtolani` int(11) DEFAULT NULL,
  `CHP_HipBarlow` int(11) DEFAULT NULL,
  `CHP_HipGaleazziSign` int(11) DEFAULT NULL,
  `CHP_HipTelescoping` int(11) DEFAULT NULL,
  `CHP_KneeThessaly` int(11) DEFAULT NULL,
  `CHP_KneeApleyCompression` int(11) DEFAULT NULL,
  `CHP_KneeApleyDistraction` int(11) DEFAULT NULL,
  `CHP_KneePatellarPosition` int(11) DEFAULT NULL,
  `CHP_KneeWipeTest` int(11) DEFAULT NULL,
  `CHP_KneeVarusStress` int(11) DEFAULT NULL,
  `CHP_KneeValgusStress` int(11) DEFAULT NULL,
  `CHP_KneePosteriorSagSign` int(11) DEFAULT NULL,
  `CHP_KneeAnteriorDrawer` int(11) DEFAULT NULL,
  `CHP_KneePosteriorDrawer` int(11) DEFAULT NULL,
  `CHP_KneeSlocumIR` int(11) DEFAULT NULL,
  `CHP_KneeSlocumER` int(11) DEFAULT NULL,
  `CHP_KneeLachman` int(11) DEFAULT NULL,
  `CHP_KneeLateralPivotShift` int(11) DEFAULT NULL,
  `CHP_KneeReversePivotShift` int(11) DEFAULT NULL,
  `CHP_KneeNobelCompression` int(11) DEFAULT NULL,
  `CHP_KneeHyperextension` int(11) DEFAULT NULL,
  `CHP_KneeBounceHome` int(11) DEFAULT NULL,
  `CHP_KneeJointLineTenderness` int(11) DEFAULT NULL,
  `CHP_KneeMcMurray` int(11) DEFAULT NULL,
  `CHP_KneeAnderson` int(11) DEFAULT NULL,
  `CHP_KneePatellarCompression` int(11) DEFAULT NULL,
  `CHP_KneePatellarApprehension` int(11) DEFAULT NULL,
  `CHP_KneeClarkeSign` int(11) DEFAULT NULL,
  `CHP_KneeHughstonPlica` int(11) DEFAULT NULL,
  `CHP_KneeMediopatellarPlica` int(11) DEFAULT NULL,
  `CHP_FootAnkleAnteriorDrawer` int(11) DEFAULT NULL,
  `CHP_FootAnklePosteriorDrawer` int(11) DEFAULT NULL,
  `CHP_FootAnkleInversionStress1` int(11) DEFAULT NULL,
  `CHP_FootAnkleEversionStress1` int(11) DEFAULT NULL,
  `CHP_FootAnkleForefootAbduction` int(11) DEFAULT NULL,
  `CHP_FootAnkleExternalRotation` int(11) DEFAULT NULL,
  `CHP_FootAnkleSyndesmosisSqueeze` int(11) DEFAULT NULL,
  `CHP_FootAnkleForefootNeuromaSqueeze` int(11) DEFAULT NULL,
  `CHP_FootAnklePlanarFasciaTenderness` int(11) DEFAULT NULL,
  `CHP_FootAnkleHoman` int(11) DEFAULT NULL,
  `CHP_FootAnkleThompson` int(11) DEFAULT NULL,
  `CHP_FootAnkleValgusStress` int(11) DEFAULT NULL,
  `CHP_FootAnkleVarusStress` int(11) DEFAULT NULL,
  `CHP_FootAnkleATFL` int(11) DEFAULT NULL,
  `CHP_FootAnkleCFL` int(11) DEFAULT NULL,
  `CHP_FootAnkleInversionStress2` int(11) DEFAULT NULL,
  `CHP_FootAnkleEversionStress2` int(11) DEFAULT NULL,
  `CHP_OtherTMJ` int(11) DEFAULT NULL,
  `CHP_OtherGAIT` int(11) DEFAULT NULL,
  `CHP_OtherBloodPressure` int(11) DEFAULT NULL,
  `CHP_OtherHeartRate` int(11) DEFAULT NULL,
  `CHP_OtherPulse` int(11) DEFAULT NULL,
  `CHP_MyotomesC2` int(11) DEFAULT NULL,
  `CHP_MyotomesC3` int(11) DEFAULT NULL,
  `CHP_MyotomesC4` int(11) DEFAULT NULL,
  `CHP_MyotomesC5` int(11) DEFAULT NULL,
  `CHP_MyotomesC6` int(11) DEFAULT NULL,
  `CHP_MyotomesC7` int(11) DEFAULT NULL,
  `CHP_MyotomesC8` int(11) DEFAULT NULL,
  `CHP_MyotomesT1` int(11) DEFAULT NULL,
  `CHP_MyotomesL2` int(11) DEFAULT NULL,
  `CHP_MyotomesL3` int(11) DEFAULT NULL,
  `CHP_MyotomesL4` int(11) DEFAULT NULL,
  `CHP_MyotomesS1` int(11) DEFAULT NULL,
  `CHP_MyotomesS2` int(11) DEFAULT NULL,
  `CHP_MyotomesS4` int(11) DEFAULT NULL,
  `CHP_DermatomesC4` int(11) DEFAULT NULL,
  `CHP_DermatomesC5` int(11) DEFAULT NULL,
  `CHP_DermatomesC6` int(11) DEFAULT NULL,
  `CHP_DermatomesC7` int(11) DEFAULT NULL,
  `CHP_DermatomesC8` int(11) DEFAULT NULL,
  `CHP_DermatomesT1` int(11) DEFAULT NULL,
  `CHP_DermatomesT2` int(11) DEFAULT NULL,
  `CHP_DermatomesL3` int(11) DEFAULT NULL,
  `CHP_DermatomesL4` int(11) DEFAULT NULL,
  `CHP_DermatomesL5LateralFoot` int(11) DEFAULT NULL,
  `CHP_DermatomesS1` int(11) DEFAULT NULL,
  `CHP_DermatomesL5AnteriorLeg` int(11) DEFAULT NULL,
  `CHP_DermatomesS2` int(11) DEFAULT NULL,
  `CHP_DeepTendonReflexesC5` int(11) DEFAULT NULL,
  `CHP_DeepTendonReflexesC6` int(11) DEFAULT NULL,
  `CHP_DeepTendonReflexesC7` int(11) DEFAULT NULL,
  `CHP_DeepTendonReflexesL4` int(11) DEFAULT NULL,
  `CHP_DeepTendonReflexesS1` int(11) DEFAULT NULL,
  `CHP_CranialNerveExamCN1OlfactoryCoffee` int(11) DEFAULT NULL,
  `CHP_CranialNerveExamCN1OlfactoryVanilla` int(11) DEFAULT NULL,
  `CHP_CranialNerveExamCN1OlfactoryOrange` int(11) DEFAULT NULL,
  `CHP_CranialNerveExamCN2` varchar(50) DEFAULT NULL,
  `CHP_CranialNerveExamCN346` varchar(50) DEFAULT NULL,
  `CHP_CranialNerveExamCN7` varchar(50) DEFAULT NULL,
  `CHP_CranialNerveExamCN11` varchar(50) DEFAULT NULL,
  `CHP_CranialNerveExamCN12` varchar(50) DEFAULT NULL,
  `CHP_SystemsReview` varchar(255) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineCervicalValsalva` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineCervicalKemp` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineCervicalSpurling` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineCervicalJackson` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineCervicalDoorbell` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineCervicalSotoHall` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineCervicalKernig` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineCervicalHermitte` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineCervicalEAST` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineCervicalAdson` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineCervicalWrightHyperabduction` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineCervicalEden` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineCervicalRotaryChair` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineCervicalDixHallpike` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineCervicalSubOccipitals` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineCervicalTrapezius` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineCervicalLevatorScapulae` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineCervicalErectors` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineCervicalSternocleidomastoid` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineCervicalScalenes` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineThoracicValsalva` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineThoracicChestExpansion` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineThoracicScapularApproximation` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineThoracicT1NerveStretch` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineThoracicT2NerveStretch` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineThoracicSlumpTest` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineThoracicSotoHall` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineThoracicSternalCompression` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineThoracicAbdominalReflex` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineThoracicBeevorSign` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineThoracicSLR` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineThoracicTrueLegLength` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineThoracicHermite` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineThoracicKernig` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineThoracicRhomboids` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineThoracicTrapezius` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineThoracicErectors` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineThoracicLatissimus` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineThoracicSerratusPostInf` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineThoracicPcMajorMinor` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineLumbarValsava` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineLumbarKemp` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineLumbarSLR` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineLumbarBraggards` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineLumbarBowstring` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineLumbarPatrickFaber` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineLumbarThomas` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineLumbarAPSICompression` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineLumbarPPPP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineLumbarPASICompression` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineLumbarEly` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineLumbarYeoman` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineLumbarHibbs` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineLumbarErectors` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineLumbarGluteusMax` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineLumbarGluteusMedMin` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineLumbarPiriformis` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineLumbarPsoas` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineLumbarTFLITB` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamSpineLumbarQuadratusLumborum` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamCervicalFlexionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamCervicalFlexionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamCervicalFlexionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamCervicalExtensionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamCervicalExtensionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamCervicalExtensionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamCervicalRightRotationROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamCervicalRightRotationERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamCervicalRightRotationComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamCervicalLeftRotationROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamCervicalLeftRotationERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamCervicalLeftRotationComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamCervicalRightLateralFlexionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamCervicalRightLateralFlexionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamCervicalRightLateralFlexionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamCervicalLeftLateralFlexionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamCervicalLeftLateralFlexionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamCervicalLeftLateralFlexionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderRightFlexionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderRightFlexionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderRightFlexionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderLeftFlexionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderLeftFlexionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderLeftFlexionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderRightAbductionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderRightAbductionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderRightAbductionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderLeftAbductionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderLeftAbductionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderLeftAbductionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderRightInternalRotationROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderRightInternalRotationERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderRightInternalRotationComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderLeftInternalRotationROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderLeftInternalRotationERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderLeftInternalRotationComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderRightExternalRotationROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderRightExternalRotationERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderRightExternalRotationComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderLeftExternalRotationROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderLeftExternalRotationERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamShoulderLeftExternalRotationComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamLumbarFlexionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamLumbarFlexionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamLumbarFlexionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamLumbarExtensionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamLumbarExtensionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamLumbarExtensionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamLumbarRightLateralFlexionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamLumbarRightLateralFlexionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamLumbarRightLateralFlexionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamLumbarLeftLateralFlexionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamLumbarLeftLateralFlexionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamLumbarLeftLateralFlexionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamLumbarRightRotationROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamLumbarRightRotationERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamLumbarRightRotationComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamLumbarLeftRotationROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamLumbarLeftRotationERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamLumbarLeftRotationComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamElbowRightFlexionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamElbowRightFlexionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamElbowRightFlexionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamElbowLeftFlexionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamElbowLeftFlexionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamElbowLeftFlexionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamElbowRightExtensionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamElbowRightExtensionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamElbowRightExtensionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamElbowLeftExtensionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamElbowLeftExtensionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamElbowLeftExtensionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamHipRightFlexionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamHipRightFlexionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamHipRightFlexionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamHipLeftFlexionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamHipLeftFlexionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamHipLeftFlexionCommment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamHipRightExtensionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamHipRightExtensionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamHipRightExtensionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamHipLeftExtensionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamHipLeftExtensionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamHipLeftExtensionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamHipRightAbductionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamHipRightAbductionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamHipRightAbductionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamHipLeftAbductionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamHipLeftAbductionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamHipLeftAbductionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamHipRightInternalRotationROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamHipRightInternalRotationERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamHipRightInternalRotationComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamHipLeftInternalRotationROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamHipLeftInternalRotationERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamHipLeftInternalRotationComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamHipRightExternalRotationROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamHipRightExternalRotationERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamHipRightExternalRotationComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamHipLeftExternalRotationROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamHipLeftExternalRotationERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamHipLeftExternalRotationComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamWristRightFlexionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamWristRightFlexionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamWristRightFlexionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamWristLeftFlexionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamWristLeftFlexionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamWristLeftFlexionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamWristRightExtensionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamWristRightExtensionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamWristRightExtensionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamWristLeftExtensionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamWristLeftExtensionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamWristLeftExtensionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamWristRightRadialDeviationROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamWristRightRadialDeviationERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamWristRightRadialDeviationComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamWristLeftRadialDeviationROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamWristLeftRadialDeviationERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamWristLeftRadialDeviationComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamWristRightUlnarDeviationROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamWristRightUlnarDeviationERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamWristRightUlnarDeviationComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamWristLeftUlnarDeviationROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamWristLeftUlnarDeviationERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamWristLeftUlnarDeviationComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamKneeRightFlexionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamKneeRightFlexionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamKneeRightFlexionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamKneeLeftFlexionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamKneeLeftFlexionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamKneeLeftFlexionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamKneeRightExtensionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamKneeRightExtensionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamKneeRightExtensionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamKneeLeftExtensionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamKneeLeftExtensionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamKneeLeftExtensionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamKneeSquatROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamKneeSquatERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamKneeSquatComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleRightDorsiflexionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleRightDorsiflexionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleRightDorsiflexionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleLeftDorsiflexionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleLeftDorsiflexionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleLeftDorsiflexionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleRightPlantarFlexionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleRightPlantarFlexionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleRightPlantarFlexionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleLeftPlantarFlexionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleLeftPlantarFlexionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleLeftPlantarFlexionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleRightInversionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleRightInversionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleRightInversionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleLeftInversionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleLeftInversionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleLeftInversionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleRightEversionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleRightEversionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleRightEversionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleLeftEversionROM` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleLeftEversionERP` int(11) DEFAULT NULL,
  `CHP_OrthopaedicExamAnkleLeftEversionComment` varchar(50) DEFAULT NULL,
  `CHP_OrthopaedicDRGNote` text,
  `CHP_FlagSeverityOfSpecificSymptoms` bit(1) DEFAULT NULL,
  `CHP_FlagEmergenceOfRadicularIrritation` bit(1) DEFAULT NULL,
  `CHP_FlagPreviousHistoryOfHeadInjury` bit(1) DEFAULT NULL,
  `CHP_FlagPreExistAutoimmuneDisease` bit(1) DEFAULT NULL,
  `CHP_FlagSystemicOrChronicDiseasePresent` bit(1) DEFAULT NULL,
  `CHP_FlagSignificantRecentWeightLossGain` bit(1) DEFAULT NULL,
  `CHP_FlagOlderAge` bit(1) DEFAULT NULL,
  `CHP_FlagPriorHistoryOfPsychologicalDisturbance` bit(1) DEFAULT NULL,
  `CHP_FlagPregnancy` bit(1) DEFAULT NULL,
  `CHP_FlagFeverChills` bit(1) DEFAULT NULL,
  `CHP_FlagRecentInfection` bit(1) DEFAULT NULL,
  `CHP_FlagOther` varchar(50) DEFAULT NULL,
  `CHP_FlagHeadRotatedOutOfPosition` bit(1) DEFAULT NULL,
  `CHP_FlagMultipleImpact` bit(1) DEFAULT NULL,
  `CHP_FlagShowingSignsOfPTS` bit(1) DEFAULT NULL,
  `CHP_Diagnosis` text,
  `CHP_Recommendations` text,
  `CHP_IsTestData` bit(1) NOT NULL,
  `CHP_CreatedTime` datetime NOT NULL,
  `CHP_UpdatedTime` datetime DEFAULT NULL,
  `CHP_CreatedBy` varchar(50) NOT NULL,
  `CHP_UpdatedBy` varchar(50) DEFAULT NULL,
  `CHP_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`CHP_CHPId`),
  UNIQUE KEY `CHP_ITRId_UNIQUE` (`CHP_ITRId`),
  KEY `fk_chiropracticdetail_diagram` (`CHP_ChiropracticDGRId`),
  KEY `fk_chiropracticdetail_initialtreatment` (`CHP_ITRId`),
  CONSTRAINT `fk_chiropracticdetail_diagram` FOREIGN KEY (`CHP_ChiropracticDGRId`) REFERENCES `diagram` (`DGR_DGRId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_chiropracticdetail_initialtreatment` FOREIGN KEY (`CHP_ITRId`) REFERENCES `initialtreatment` (`ITR_ITRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `chiropracticdetail`
--

LOCK TABLES `chiropracticdetail` WRITE;
/*!40000 ALTER TABLE `chiropracticdetail` DISABLE KEYS */;
/*!40000 ALTER TABLE `chiropracticdetail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `chiropracticfollowupdetail`
--

DROP TABLE IF EXISTS `chiropracticfollowupdetail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `chiropracticfollowupdetail` (
  `CFD_CFDId` int(11) NOT NULL,
  `CFD_FTRId` int(11) NOT NULL,
  `CFD_ChiropracticFollowUpDGRId` int(11) NOT NULL,
  `CFD_VAS` int(11) DEFAULT NULL,
  `CFD_ConditionChange` int(11) DEFAULT NULL,
  `CFD_ConditionChangeNote` varchar(255) DEFAULT NULL,
  `CFD_O` text,
  `CFD_A` text,
  `CFD_OsseousManipulation` bit(1) DEFAULT NULL,
  `CFD_Massage` bit(1) DEFAULT NULL,
  `CFD_Stretch` bit(1) DEFAULT NULL,
  `CFD_TriggerPointTherapy` bit(1) DEFAULT NULL,
  `CFD_Traction` bit(1) DEFAULT NULL,
  `CFD_TheraputicExercise` bit(1) DEFAULT NULL,
  `CFD_ExerciseNote` varchar(255) DEFAULT NULL,
  `CFD_Heat` bit(1) DEFAULT NULL,
  `CFD_Cold` bit(1) DEFAULT NULL,
  `CFD_UltraSound` bit(1) DEFAULT NULL,
  `CFD_UltraSoundValue` int(11) DEFAULT NULL,
  `CFD_Electrotherapy` bit(1) DEFAULT NULL,
  `CFD_ElectrotherapyNote` varchar(255) DEFAULT NULL,
  `CFD_NoDxChange` bit(1) DEFAULT NULL,
  `CFD_NewDiagnosis` bit(1) DEFAULT NULL,
  `CFD_NewDiagnosisNote` varchar(255) DEFAULT NULL,
  `CFD_PTRDays` int(11) DEFAULT NULL,
  `CFD_PTRWeeks` int(11) DEFAULT NULL,
  `CFD_PTRMonths` int(11) DEFAULT NULL,
  `CFD_PRN` bit(1) DEFAULT NULL,
  `CFD_Referral` bit(1) DEFAULT NULL,
  `CFD_ReferralNote` varchar(255) DEFAULT NULL,
  `CFD_HomeCare` varchar(255) DEFAULT NULL,
  `CFD_Comments` varchar(255) DEFAULT NULL,
  `CFD_IsTestData` bit(1) NOT NULL,
  `CFD_CreatedTime` datetime NOT NULL,
  `CFD_UpdatedTime` datetime DEFAULT NULL,
  `CFD_CreatedBy` varchar(50) NOT NULL,
  `CFD_UpdatedBy` varchar(50) DEFAULT NULL,
  `CFD_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`CFD_CFDId`),
  KEY `fk_chiropracticfollowupdetail_chiropracticdetail` (`CFD_FTRId`),
  KEY `fk_chiropracticfollowupdetail_diagram` (`CFD_ChiropracticFollowUpDGRId`),
  CONSTRAINT `fk_chiropracticfollowupdetail_diagram` FOREIGN KEY (`CFD_ChiropracticFollowUpDGRId`) REFERENCES `diagram` (`DGR_DGRId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_chiropracticfollowupdetail_followuptreatment` FOREIGN KEY (`CFD_FTRId`) REFERENCES `followuptreatment` (`FTR_FTRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `chiropracticfollowupdetail`
--

LOCK TABLES `chiropracticfollowupdetail` WRITE;
/*!40000 ALTER TABLE `chiropracticfollowupdetail` DISABLE KEYS */;
/*!40000 ALTER TABLE `chiropracticfollowupdetail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `diagram`
--

DROP TABLE IF EXISTS `diagram`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `diagram` (
  `DGR_DGRId` int(11) NOT NULL,
  `DGR_DiagramName` varchar(50) NOT NULL,
  `DGR_ImageType` varchar(5) NOT NULL,
  `DGR_ImageWidth` int(11) NOT NULL,
  `DGR_ImageHeight` int(11) NOT NULL,
  `DGR_Image` longblob NOT NULL,
  `DGR_IsTestData` bit(1) NOT NULL,
  `DGR_CreatedTime` datetime NOT NULL,
  `DGR_UpdatedTime` datetime DEFAULT NULL,
  `DGR_CreatedBy` varchar(50) NOT NULL,
  `DGR_UpdatedBy` varchar(50) DEFAULT NULL,
  `DGR_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`DGR_DGRId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `diagram`
--

LOCK TABLES `diagram` WRITE;
/*!40000 ALTER TABLE `diagram` DISABLE KEYS */;
INSERT INTO `diagram` VALUES (1,'Physiotherapy','.jpg',250,250,'ÿØÿà\0JFIF\0\0H\0H\0\0ÿáÞExif\0\0MM\0*\0\0\0\0\0\0\0\0\0\0\0\Z\0\0\0\0\0\0\0b\0\0\0\0\0\0\0j(\0\0\0\0\0\0\01\0\0\0\0\0\0\0r2\0\0\0\0\0\0\0†‡i\0\0\0\0\0\0\0œ\0\0\0È\0\0\0\0\0\0\0\0\0\0\0\0Adobe Photoshop 7.0\02004:10:18 16:13:35\0\0\0\0 \0\0\0\0ÿÿ\0\0 \0\0\0\0\0\0\0Ã \0\0\0\0\0\0\0í\0\0\0\0\0\0\0\0\0\0\0\0\0\0\Z\0\0\0\0\0\0\0\0\0\0\0\0(\0\0\0\0\0\0\0\0\0\0\0\0\0&\0\0\0\0\0\0°\0\0\0\0\0\0\0H\0\0\0\0\0\0H\0\0\0ÿØÿà\0JFIF\0\0H\0H\0\0ÿí\0Adobe_CM\0ÿî\0Adobe\0d€\0\0\0ÿÛ\0„\0			\n\r\r\rÿÀ\0\0€\0i\"\0ÿÝ\0\0ÿÄ?\0\0\0\0\0\0\0\0\0\0	\n\0\0\0\0\0\0\0\0\0	\n\03\0!1AQa\"q2‘¡±B#$RÁb34r‚ÑC%’Sðáñcs5¢²ƒ&D“TdEÂ£t6ÒUâeò³„ÃÓuãóF\'”¤…´•ÄÔäô¥µÅÕåõVfv†–¦¶ÆÖæö7GWgw‡—§·Ç×ç÷\05\0!1AQaq\"2‘¡±B#ÁRÑð3$bár‚’CScs4ñ%¢²ƒ&5ÂÒD“T£dEU6teâò³„ÃÓuãóF”¤…´•ÄÔäô¥µÅÕåõVfv†–¦¶ÆÖæö\'7GWgw‡—§·ÇÿÚ\0\0\0?\0õ4’IVKS¨_eL¦ª¤Y•kikÚ,ë,·Ý¹¿£ª»6îoÓ@wJÂØ\rÙVA?£É=ïßü¯UïeíÓù‹Zú¿àÑ:­m£486‹‰-kŽÂ×Q‘ó µïÞßÜY\'§ô¼Ci²Œ.—úûržÑe.ÛWÚ_ú<ßá(õ¿3I ¥;¸9\'»œ6½À‡´pÒk°Ûr:«Ó1ìÇÂ­—G®íÖß´\0=[\\ë®­þvÇ+I‡r¥$’H)JÏÉËÊ³ÇcU@o¯ÐÒòç‚æÑGª×±»Y²ËmÙþŒ¯þ\nò©ŠÑNfUZnµÃ#pÝ$8\n=û½¾ÏCoèÿ\03ÿ\0pê¤tQ•‡{ZrlËÇ¸–ÅÁ¥õºö¹¶ÔÚ·Rí¾žË÷ïÿ\0\n¯ª¹@[}ð~—®^×íÇÐú~£ÝéúnüÏQZHô*RI$š§ÿÐõ4,œš1h~FMªš†çØã\0UH9×õgÔöÅxu2Êÿ\0”û¬/ø*éØÏøëUp-.6wÖ|,ê¬ÄÆµÔÕ}Nœ‹qï$´ûèUeURïk¿œºÿ\0ýµ+¯ú¢ÚŸ‘SÙE®×}ž›¥»mf›e5þ‘¬ØöÕúOðv-ì³—…’FÃ{`g†á»n«?§œì&—?Ò§í^Ç:H,³Ñ¡¶º+ÝéR÷±¬ôÿ\0ãÅWjS/­}\"ç²›òñëºÂQ\0Ûï¢Æ¶ßNÚ®wýÇµŸñ~²ÚUò]SŸV5ÕY~àC€-Fÿ\0sô”z{è¾—8ãXêCÝ©s[î­ÅßœïIìkÝþ‘4¸Si$“9Á­.:\0$üR¬Ü\\F´ß`iyŠëç¼vSS7Ysÿ\0‘[UL|·]—“akk¾ŠZŠ^\r»I{Øü†×½”ú›FßÒÂ~âé”õäd0?*ðÛšò\\ÂÐaíÆeµm¶ªv~‰û>Ÿ¾Ô,\Zñ[èÈ¼ÐiµÂ›M{joµ¯¤9­©û½Wn{­¶ßÒSüêx”Ù¿&Ü|Œl‡×ºËê-³¯À[úg»ôÛ“éîý6ÏÒìþoý\Z¹™‹–Òh°<·G³V½‡÷m©ûm¥ÿ\0Èµ‹#¨b¶üœµd[^#)ÇßNÏÓnu­ª·>ÆYc,vG¶Ÿ±Ýnÿ\0ðÈõôº:n32h¯fMok®v÷=ÎaŠŸE—Üç[{*¥ß¢õüíUÚ‘”ë¤’IŠÿÑõ5R±þV¼—\rqé†N¿O\'s¶+j¦-eùyŽ¶Š¿©Q~çë_m¿Øcê”8\rÆ;+%µ‡b1Ä\\ðßÑÀ5ÞæYþÚßz²Y}Èaf)u¾ƒÈØéÈ>®;n©ÅÎ­Õ·Ô¡þ¯§ú{hýn‘…eôúÙW>ÚM¯¶¬g5`sžnÜïI»­ô.sÛNû6…ý#ý+V•ƒ,_‰kÚØm¡ÍsZw\ríôìpklÛûô¿ôOÿ\0„Nµ5EtgáÕŽÀÊÿ\0X±Í\Z	qkì~ßß}×nEéâ+»¼ä\\f\"}îÿ\0©ú*¦=Ytušª¿$ä°cØ)cZðÒúÝcí¹¿Î¿ÛM^ÖSÿ\0Vðƒª·#Ú€óugMYqu†©¬Ïêl@ì¦ÚbAÐ§AÊ¼ÑC¬hÜý[|^ò+©¿ç¹4n¦µ6Xp0-®@>Žöñ-{vAþ«ž×¢7œfeYVí×nq’¡vÊš>ƒ=GYoüeŠž]´Œz°ØýîÃ»\\\\×4{of×»Ó¯Þê~›eu+vÛƒÔº~Cj½·c½¯ªÇÓ`ÓM¶3ÕoónÚœT•Ø´Üúnµ¤ÙV­‡86—XvËv}*ý]þ“ýõª–]mý,ÚÇÇ­k}7?›uÍc9ýúUœŽ¡ƒŠöSmÕ×kÄ×Sœ\ZOæ·é~ó½ŠŽ!ÈJÆÄ{½LÀÁc_é½µ“KÚöWdîô}M¾Ÿéê9bANºJÚË©eÌ–49³Ì:©¦õSÿÒõ¸1Žyá “Jƒì^‘ŽÍŸ¦²¶V+2=ïlØ^~“[_é.·ù£g<<3\r¤ú™&ŠšA½îýÖìýüe¬Uº‹ÃèÎÈ>ËM•VH:Xæn²ÎíuLÿ\0·T %¯Ñ_•Ók«9áø—‡\r¤:ÏwØrô+s71˜Ÿé¿šþsÓõ6Üæ±¥ï!­h%Î&\0’â üz, ãXÆÙInÇVðÒÞ6¹®YX˜[—u/ÑŠí¦¼‹ŸeaþÛ*­û¾ÐÏGÓ¿ôÖÛè½ÿ\0é½OIho¥)³-½J®©sãÝÕUA;vã»íNô¯éígæbúá+ZO;z¥#@,¢Ù×RXú6éùßÎ½,ö1ïÅkØ,c­,x kê¹‡Ÿë*î±ÔãQuÇ\\½;ÞâGèàÐoýjÊ²¬ÞŽêtÕ\\ “‰\\:Ò#³öî?õËjV•åV-ÈÌ¤¯†–måÖ7ÔÇ]¸ôÿ\0Çz•ÿ\0ƒMˆÕHs˜3zž&+½ØØîu™¹¡Ì{Ã?A[÷¡ßöøÏ³«yXï7W‘@ý(Š­×nê\\}ÚþýÏSÿ\0\\gøgªôÒÜ\\–Y{˜ßGÇßq07ÚöY{÷:Úÿ\0CþbÐ{ÚÆ9îÑ­¸ùN•Jkâb\nÛsîk]vS‹¯<‚>…u{‡ºº©ýþ¬Uú;™H·\0CE A¨<°mô.ýŸ¹ú?ô•­9¬isÈkZ%Î:\0r³\rb‹/É­»~Ï’_``ÕÛ]_hÝûÛ\\ÿ\0´è:C[µ6ºx\rÇ,ýË-lxEÛÇò>Š´ªPæÕ~1M±‘_ò„6«¶ÿ\0ÅØÖzŸñõ«iVªÿÓìïßgQÉ¯=ÏÆ¡íi,©Ís¯¯Ô²Ši±Û=Ziöïôh³Þü¬Wý\Z¹\rMÍÊl†ä—\\<a¬e-ÿ\0£Få>£ÑÙŸ“U¯¹õÖÖ–][	i{~“mcšê6»ü%¥ýÇÔ®[C_Œüv€\Zæ\ZÚØÐ6º¢½’•WÆ Ý—¸¿EGµÊX–úØµZD±¥Àv1îoö\\ƒÓÜ×?4´A.:jC*lû“ûÉ½ÔË1ù^â./€b@ªö{ÿ\0y›¬ÿ\0=gäYé[ÖÞêm®Ç<û=J¬m¡Ÿ¢¾¿ÞÚßß«Óÿ\0ŠZ.‹:‹1Qq>v»c?èÓj­ÔúAÌµ·QqÇ´·Ñ¸Æàê‰:nØËëÝº‹½ÿ\0¹mvÖˆ4¤8—fŽú›‹eVÕ±ÍÑk,ýZç9¬±˜¬·Ð®ïøSþ\r¬¼lŽŸUðË©†šäúd–—mÜÖûj`{÷ìüÅ¦Öµh†´\0T0:6.\rž«ûÐæU¼é[ÁéVÆíoÑª¿ÒY¾ïøD¬)1k]Ô^ Ó¨#s§þ©QËè8#ìk_mD5¬Ä¶×¿¾àßn-u;kúUÔïÑ3ü\Z¿iÍÇqô² {Ï¶æ·üÚlQêv¶¬MÎ$eM%³ +w·óZßsÿ\0•<”Á¦×c^ÚÜC¾ºeŽ©®\ZµÕã>Çc×³üÊ¿F‡c½L<û6º=GÈîáPeoÛ½èíZ{kc¬y†°8ø©AÀk†EÂ`6¼Î°›žßóž•õ*iuÁc1ßƒ¶û\Z÷=¥º¹»k{ä0ïßüÍ•7ôžÞ¥Í­_’ÎÄèxØ™,¾»msjmU=û€õ&^ïÒÙéý\n=K?EêYÿ\0éh¥aOÿÔõ4’IWKœÜÌ^žÜŸ´Y¶¦^v¥ÄznK¡µµÎØ×:û7~c=OôièÊÇÇºöX~Qk6°Ä¶–ïw¦±»žÊý[v{Ñ(¦œŸ´>ö6Úì¿Ú×€áúµ7Úéú7ÕeŒUzM5ÞÇ¿\"±m­¸än|;eÎÝêWWî}Ÿù¤ý5Swnõ¯&M¶»äÚÏ Æëÿ\0»ûjÊ­ÓÀn1hüÛ-åež*ÊiÝJI$S[<ŒnoÓÇ\"æÏò5{uÿ\0IVúÔzl³¬x–›¨‘ÿ\0^©O¨k‡k;Ú=!ñ°ú-ÿ\0¤õ_¬ˆÁQ ¶ÆØ\"$šZe>éþúGýq8tSc:Sh<^ñ[¿ª}Öûi¯Vl²f3ÌCnƒ?Êe•·oò·½Y@ì¤’I?ÿÕõ4’IWKW–»&—ƒ¹Å¿Õ°ú»^Ä[éå86r®q. È\'s^Í¿F·Wµ±_RxçêÎ§lýêTk´·¦äµëž+nžÑ¾úé¬Ó³ríS{¦û±EÑ·í}ÃYÑîsë?öÖÅiEŒk\ZÑ\rh\0!¢’iÝJI$S_=Û1]gj‹,tx1Í±ÿ\0ôZ«õŸ@×Œ.vÐÜª^ €}ŽÜwþŸNßø5o&¡~=´ž-c˜´V&oPªÜL{rŽÓUVdÜÆ‚t¡Ôµÿ\0ÖþuŸ¡ßïõãù)×Ënû±kß¥Þ|ƒ\Z÷nÿ\0·=5eR¢Ç_Ô²þ¢–Gïý-ÿ\0æí¡Šêiì¥$’H)ÿÖõ4’IWKQÓgU¬	ÛC‹ü&ç0Uÿ\0G\ZõŸ†ëI6½­te›,“ôjmáÌ·vÜV×wòÿ\0Œsß…›’Ëñ¯±¶?}wSKì•7iô›gÐs¿‘þôá,®ÜÑÓð_‹‘‹}Ö½®&ª©±À‚ÐÐÀüf[^÷~åný})ÝI\\1i\0Åm€díö»ó½ÈÉ…JI$’S,eUºÛ]m.{­œåÍ<TÆ´½–äf\ZöŠp¯Ó¹ô<ôÊh¾ÆÑöAõ3ô~ÿ\0ÑïºÊ=~ßWn[ºußcc-½»^Ú¬×†9¶YOýv¶¾¶,‹ºÖNc«¿§Þƒ7¾»Ò‘öGe\"ûØÿ\0Ó:úýLSôÍ\'Çe:˜õ†ãXlÇ¼×•¹Ïs…vVÊ¢oÝgó˜ö?oæoZk#¥du.¡–s²±Ž;*5WEšØ÷9Áïµû™[ê®¶ÔÖÖßðž«ÿ\0Ñ­t%º”’I&©ÿÙÿí–Photoshop 3.0\08BIM%\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\08BIMí\0\0\0\0\0\0H\0\0\0\0\0H\0\0\0\08BIM&\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0?€\0\08BIM\r\0\0\0\0\0\0\0\0x8BIM\0\0\0\0\0\0\0\08BIMó\0\0\0\0\0	\0\0\0\0\0\0\0\0\08BIM\n\0\0\0\0\0\0\08BIM\'\0\0\0\0\0\n\0\0\0\0\0\0\0\08BIMõ\0\0\0\0\0H\0/ff\0\0lff\0\0\0\0\0\0\0/ff\0\0¡™š\0\0\0\0\0\0\02\0\0\0\0Z\0\0\0\0\0\0\0\0\05\0\0\0\0-\0\0\0\0\0\0\0\08BIMø\0\0\0\0\0p\0\0ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿè\0\0\0\0ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿè\0\0\0\0ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿè\0\0\0\0ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿè\0\08BIM\0\0\0\0\0\0\0\08BIM\0\0\0\0\0\0\08BIM\0\0\0\0\0\0\0\0\0\0@\0\0@\0\0\0\08BIM\0\0\0\0\0\0\0\0\08BIM\Z\0\0\0\0I\0\0\0\0\0\0\0\0\0\0\0\0\0\0í\0\0\0Ã\0\0\0\n\0U\0n\0t\0i\0t\0l\0e\0d\0-\02\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0Ã\0\0\0í\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0null\0\0\0\0\0\0boundsObjc\0\0\0\0\0\0\0\0\0Rct1\0\0\0\0\0\0\0Top long\0\0\0\0\0\0\0\0Leftlong\0\0\0\0\0\0\0\0Btomlong\0\0\0í\0\0\0\0Rghtlong\0\0\0Ã\0\0\0slicesVlLs\0\0\0Objc\0\0\0\0\0\0\0\0slice\0\0\0\0\0\0sliceIDlong\0\0\0\0\0\0\0groupIDlong\0\0\0\0\0\0\0originenum\0\0\0ESliceOrigin\0\0\0\rautoGenerated\0\0\0\0Typeenum\0\0\0\nESliceType\0\0\0\0Img \0\0\0boundsObjc\0\0\0\0\0\0\0\0\0Rct1\0\0\0\0\0\0\0Top long\0\0\0\0\0\0\0\0Leftlong\0\0\0\0\0\0\0\0Btomlong\0\0\0í\0\0\0\0Rghtlong\0\0\0Ã\0\0\0urlTEXT\0\0\0\0\0\0\0\0\0nullTEXT\0\0\0\0\0\0\0\0\0MsgeTEXT\0\0\0\0\0\0\0\0altTagTEXT\0\0\0\0\0\0\0\0cellTextIsHTMLbool\0\0\0cellTextTEXT\0\0\0\0\0\0\0\0	horzAlignenum\0\0\0ESliceHorzAlign\0\0\0default\0\0\0	vertAlignenum\0\0\0ESliceVertAlign\0\0\0default\0\0\0bgColorTypeenum\0\0\0ESliceBGColorType\0\0\0\0None\0\0\0	topOutsetlong\0\0\0\0\0\0\0\nleftOutsetlong\0\0\0\0\0\0\0bottomOutsetlong\0\0\0\0\0\0\0rightOutsetlong\0\0\0\0\08BIM\0\0\0\0\0\08BIM\0\0\0\0\0\0\0\08BIM\0\0\0\0Ì\0\0\0\0\0\0i\0\0\0€\0\0<\0\0ž\0\0\0°\0\0ÿØÿà\0JFIF\0\0H\0H\0\0ÿí\0Adobe_CM\0ÿî\0Adobe\0d€\0\0\0ÿÛ\0„\0			\n\r\r\rÿÀ\0\0€\0i\"\0ÿÝ\0\0ÿÄ?\0\0\0\0\0\0\0\0\0\0	\n\0\0\0\0\0\0\0\0\0	\n\03\0!1AQa\"q2‘¡±B#$RÁb34r‚ÑC%’Sðáñcs5¢²ƒ&D“TdEÂ£t6ÒUâeò³„ÃÓuãóF\'”¤…´•ÄÔäô¥µÅÕåõVfv†–¦¶ÆÖæö7GWgw‡—§·Ç×ç÷\05\0!1AQaq\"2‘¡±B#ÁRÑð3$bár‚’CScs4ñ%¢²ƒ&5ÂÒD“T£dEU6teâò³„ÃÓuãóF”¤…´•ÄÔäô¥µÅÕåõVfv†–¦¶ÆÖæö\'7GWgw‡—§·ÇÿÚ\0\0\0?\0õ4’IVKS¨_eL¦ª¤Y•kikÚ,ë,·Ý¹¿£ª»6îoÓ@wJÂØ\rÙVA?£É=ïßü¯UïeíÓù‹Zú¿àÑ:­m£486‹‰-kŽÂ×Q‘ó µïÞßÜY\'§ô¼Ci²Œ.—úûržÑe.ÛWÚ_ú<ßá(õ¿3I ¥;¸9\'»œ6½À‡´pÒk°Ûr:«Ó1ìÇÂ­—G®íÖß´\0=[\\ë®­þvÇ+I‡r¥$’H)JÏÉËÊ³ÇcU@o¯ÐÒòç‚æÑGª×±»Y²ËmÙþŒ¯þ\nò©ŠÑNfUZnµÃ#pÝ$8\n=û½¾ÏCoèÿ\03ÿ\0pê¤tQ•‡{ZrlËÇ¸–ÅÁ¥õºö¹¶ÔÚ·Rí¾žË÷ïÿ\0\n¯ª¹@[}ð~—®^×íÇÐú~£ÝéúnüÏQZHô*RI$š§ÿÐõ4,œš1h~FMªš†çØã\0UH9×õgÔöÅxu2Êÿ\0”û¬/ø*éØÏøëUp-.6wÖ|,ê¬ÄÆµÔÕ}Nœ‹qï$´ûèUeURïk¿œºÿ\0ýµ+¯ú¢ÚŸ‘SÙE®×}ž›¥»mf›e5þ‘¬ØöÕúOðv-ì³—…’FÃ{`g†á»n«?§œì&—?Ò§í^Ç:H,³Ñ¡¶º+ÝéR÷±¬ôÿ\0ãÅWjS/­}\"ç²›òñëºÂQ\0Ûï¢Æ¶ßNÚ®wýÇµŸñ~²ÚUò]SŸV5ÕY~àC€-Fÿ\0sô”z{è¾—8ãXêCÝ©s[î­ÅßœïIìkÝþ‘4¸Si$“9Á­.:\0$üR¬Ü\\F´ß`iyŠëç¼vSS7Ysÿ\0‘[UL|·]—“akk¾ŠZŠ^\r»I{Øü†×½”ú›FßÒÂ~âé”õäd0?*ðÛšò\\ÂÐaíÆeµm¶ªv~‰û>Ÿ¾Ô,\Zñ[èÈ¼ÐiµÂ›M{joµ¯¤9­©û½Wn{­¶ßÒSüêx”Ù¿&Ü|Œl‡×ºËê-³¯À[úg»ôÛ“éîý6ÏÒìþoý\Z¹™‹–Òh°<·G³V½‡÷m©ûm¥ÿ\0Èµ‹#¨b¶üœµd[^#)ÇßNÏÓnu­ª·>ÆYc,vG¶Ÿ±Ýnÿ\0ðÈõôº:n32h¯fMok®v÷=ÎaŠŸE—Üç[{*¥ß¢õüíUÚ‘”ë¤’IŠÿÑõ5R±þV¼—\rqé†N¿O\'s¶+j¦-eùyŽ¶Š¿©Q~çë_m¿Øcê”8\rÆ;+%µ‡b1Ä\\ðßÑÀ5ÞæYþÚßz²Y}Èaf)u¾ƒÈØéÈ>®;n©ÅÎ­Õ·Ô¡þ¯§ú{hýn‘…eôúÙW>ÚM¯¶¬g5`sžnÜïI»­ô.sÛNû6…ý#ý+V•ƒ,_‰kÚØm¡ÍsZw\ríôìpklÛûô¿ôOÿ\0„Nµ5EtgáÕŽÀÊÿ\0X±Í\Z	qkì~ßß}×nEéâ+»¼ä\\f\"}îÿ\0©ú*¦=Ytušª¿$ä°cØ)cZðÒúÝcí¹¿Î¿ÛM^ÖSÿ\0Vðƒª·#Ú€óugMYqu†©¬Ïêl@ì¦ÚbAÐ§AÊ¼ÑC¬hÜý[|^ò+©¿ç¹4n¦µ6Xp0-®@>Žöñ-{vAþ«ž×¢7œfeYVí×nq’¡vÊš>ƒ=GYoüeŠž]´Œz°ØýîÃ»\\\\×4{of×»Ó¯Þê~›eu+vÛƒÔº~Cj½·c½¯ªÇÓ`ÓM¶3ÕoónÚœT•Ø´Üúnµ¤ÙV­‡86—XvËv}*ý]þ“ýõª–]mý,ÚÇÇ­k}7?›uÍc9ýúUœŽ¡ƒŠöSmÕ×kÄ×Sœ\ZOæ·é~ó½ŠŽ!ÈJÆÄ{½LÀÁc_é½µ“KÚöWdîô}M¾Ÿéê9bANºJÚË©eÌ–49³Ì:©¦õSÿÒõ¸1Žyá “Jƒì^‘ŽÍŸ¦²¶V+2=ïlØ^~“[_é.·ù£g<<3\r¤ú™&ŠšA½îýÖìýüe¬Uº‹ÃèÎÈ>ËM•VH:Xæn²ÎíuLÿ\0·T %¯Ñ_•Ók«9áø—‡\r¤:ÏwØrô+s71˜Ÿé¿šþsÓõ6Üæ±¥ï!­h%Î&\0’â üz, ãXÆÙInÇVðÒÞ6¹®YX˜[—u/ÑŠí¦¼‹ŸeaþÛ*­û¾ÐÏGÓ¿ôÖÛè½ÿ\0é½OIho¥)³-½J®©sãÝÕUA;vã»íNô¯éígæbúá+ZO;z¥#@,¢Ù×RXú6éùßÎ½,ö1ïÅkØ,c­,x kê¹‡Ÿë*î±ÔãQuÇ\\½;ÞâGèàÐoýjÊ²¬ÞŽêtÕ\\ “‰\\:Ò#³öî?õËjV•åV-ÈÌ¤¯†–måÖ7ÔÇ]¸ôÿ\0Çz•ÿ\0ƒMˆÕHs˜3zž&+½ØØîu™¹¡Ì{Ã?A[÷¡ßöøÏ³«yXï7W‘@ý(Š­×nê\\}ÚþýÏSÿ\0\\gøgªôÒÜ\\–Y{˜ßGÇßq07ÚöY{÷:Úÿ\0CþbÐ{ÚÆ9îÑ­¸ùN•Jkâb\nÛsîk]vS‹¯<‚>…u{‡ºº©ýþ¬Uú;™H·\0CE A¨<°mô.ýŸ¹ú?ô•­9¬isÈkZ%Î:\0r³\rb‹/É­»~Ï’_``ÕÛ]_hÝûÛ\\ÿ\0´è:C[µ6ºx\rÇ,ýË-lxEÛÇò>Š´ªPæÕ~1M±‘_ò„6«¶ÿ\0ÅØÖzŸñõ«iVªÿÓìïßgQÉ¯=ÏÆ¡íi,©Ís¯¯Ô²Ši±Û=Ziöïôh³Þü¬Wý\Z¹\rMÍÊl†ä—\\<a¬e-ÿ\0£Få>£ÑÙŸ“U¯¹õÖÖ–][	i{~“mcšê6»ü%¥ýÇÔ®[C_Œüv€\Zæ\ZÚØÐ6º¢½’•WÆ Ý—¸¿EGµÊX–úØµZD±¥Àv1îoö\\ƒÓÜ×?4´A.:jC*lû“ûÉ½ÔË1ù^â./€b@ªö{ÿ\0y›¬ÿ\0=gäYé[ÖÞêm®Ç<û=J¬m¡Ÿ¢¾¿ÞÚßß«Óÿ\0ŠZ.‹:‹1Qq>v»c?èÓj­ÔúAÌµ·QqÇ´·Ñ¸Æàê‰:nØËëÝº‹½ÿ\0¹mvÖˆ4¤8—fŽú›‹eVÕ±ÍÑk,ýZç9¬±˜¬·Ð®ïøSþ\r¬¼lŽŸUðË©†šäúd–—mÜÖûj`{÷ìüÅ¦Öµh†´\0T0:6.\rž«ûÐæU¼é[ÁéVÆíoÑª¿ÒY¾ïøD¬)1k]Ô^ Ó¨#s§þ©QËè8#ìk_mD5¬Ä¶×¿¾àßn-u;kúUÔïÑ3ü\Z¿iÍÇqô² {Ï¶æ·üÚlQêv¶¬MÎ$eM%³ +w·óZßsÿ\0•<”Á¦×c^ÚÜC¾ºeŽ©®\ZµÕã>Çc×³üÊ¿F‡c½L<û6º=GÈîáPeoÛ½èíZ{kc¬y†°8ø©AÀk†EÂ`6¼Î°›žßóž•õ*iuÁc1ßƒ¶û\Z÷=¥º¹»k{ä0ïßüÍ•7ôžÞ¥Í­_’ÎÄèxØ™,¾»msjmU=û€õ&^ïÒÙéý\n=K?EêYÿ\0éh¥aOÿÔõ4’IWKœÜÌ^žÜŸ´Y¶¦^v¥ÄznK¡µµÎØ×:û7~c=OôièÊÇÇºöX~Qk6°Ä¶–ïw¦±»žÊý[v{Ñ(¦œŸ´>ö6Úì¿Ú×€áúµ7Úéú7ÕeŒUzM5ÞÇ¿\"±m­¸än|;eÎÝêWWî}Ÿù¤ý5Swnõ¯&M¶»äÚÏ Æëÿ\0»ûjÊ­ÓÀn1hüÛ-åež*ÊiÝJI$S[<ŒnoÓÇ\"æÏò5{uÿ\0IVúÔzl³¬x–›¨‘ÿ\0^©O¨k‡k;Ú=!ñ°ú-ÿ\0¤õ_¬ˆÁQ ¶ÆØ\"$šZe>éþúGýq8tSc:Sh<^ñ[¿ª}Öûi¯Vl²f3ÌCnƒ?Êe•·oò·½Y@ì¤’I?ÿÕõ4’IWKW–»&—ƒ¹Å¿Õ°ú»^Ä[éå86r®q. È\'s^Í¿F·Wµ±_RxçêÎ§lýêTk´·¦äµëž+nžÑ¾úé¬Ó³ríS{¦û±EÑ·í}ÃYÑîsë?öÖÅiEŒk\ZÑ\rh\0!¢’iÝJI$S_=Û1]gj‹,tx1Í±ÿ\0ôZ«õŸ@×Œ.vÐÜª^ €}ŽÜwþŸNßø5o&¡~=´ž-c˜´V&oPªÜL{rŽÓUVdÜÆ‚t¡Ôµÿ\0ÖþuŸ¡ßïõãù)×Ënû±kß¥Þ|ƒ\Z÷nÿ\0·=5eR¢Ç_Ô²þ¢–Gïý-ÿ\0æí¡Šêiì¥$’H)ÿÖõ4’IWKQÓgU¬	ÛC‹ü&ç0Uÿ\0G\ZõŸ†ëI6½­te›,“ôjmáÌ·vÜV×wòÿ\0Œsß…›’Ëñ¯±¶?}wSKì•7iô›gÐs¿‘þôá,®ÜÑÓð_‹‘‹}Ö½®&ª©±À‚ÐÐÀüf[^÷~åný})ÝI\\1i\0Åm€díö»ó½ÈÉ…JI$’S,eUºÛ]m.{­œåÍ<TÆ´½–äf\ZöŠp¯Ó¹ô<ôÊh¾ÆÑöAõ3ô~ÿ\0ÑïºÊ=~ßWn[ºußcc-½»^Ú¬×†9¶YOýv¶¾¶,‹ºÖNc«¿§Þƒ7¾»Ò‘öGe\"ûØÿ\0Ó:úýLSôÍ\'Çe:˜õ†ãXlÇ¼×•¹Ïs…vVÊ¢oÝgó˜ö?oæoZk#¥du.¡–s²±Ž;*5WEšØ÷9Áïµû™[ê®¶ÔÖÖßðž«ÿ\0Ñ­t%º”’I&©ÿÙ8BIM!\0\0\0\0\0U\0\0\0\0\0\0\0A\0d\0o\0b\0e\0 \0P\0h\0o\0t\0o\0s\0h\0o\0p\0\0\0\0A\0d\0o\0b\0e\0 \0P\0h\0o\0t\0o\0s\0h\0o\0p\0 \07\0.\00\0\0\0\08BIM\0\0\0\0\0\0\0\0\0\0ÿáHhttp://ns.adobe.com/xap/1.0/\0<?xpacket begin=\'ï»¿\' id=\'W5M0MpCehiHzreSzNTczkc9d\'?>\n<?adobe-xap-filters esc=\"CR\"?>\n<x:xapmeta xmlns:x=\'adobe:ns:meta/\' x:xaptk=\'XMP toolkit 2.8.2-33, framework 1.5\'>\n<rdf:RDF xmlns:rdf=\'http://www.w3.org/1999/02/22-rdf-syntax-ns#\' xmlns:iX=\'http://ns.adobe.com/iX/1.0/\'>\n\n <rdf:Description about=\'uuid:0620b836-2118-11d9-bb9f-ddfb0074707f\'\n  xmlns:xapMM=\'http://ns.adobe.com/xap/1.0/mm/\'>\n  <xapMM:DocumentID>adobe:docid:photoshop:0620b834-2118-11d9-bb9f-ddfb0074707f</xapMM:DocumentID>\n </rdf:Description>\n\n</rdf:RDF>\n</x:xapmeta>\n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                       \n<?xpacket end=\'w\'?>ÿî\0Adobe\0d@\0\0\0ÿÛ\0„\0ÿÀ\0\0í\0Ã\0ÿÝ\0\0ÿÄ¢\0\0\0\0\0\0\0\0\0\0\0\0\0	\n\0\0\0\0\0\0\0\0\0\0\0\0	\0\n\0	u!\"\01A2#	QBa$3Rqb‘%C¡±ð&4r\nÁÑ5\'áS6‚ñ’¢DTsEF7Gc(UVW\Z²ÂÒâòdƒt“„e£³ÃÓã)8fóu*9:HIJXYZghijvwxyz…†‡ˆ‰Š”•–—˜™š¤¥¦§¨©ª´µ¶·¸¹ºÄÅÆÇÈÉÊÔÕÖ×ØÙÚäåæçèéêôõö÷øùú\0m!1\0\"AQ2aqB#‘R¡b3	±$ÁÑCrðá‚4%’ScDñ¢²&5T6Ed\'\nsƒ“FtÂÒâòUeuV7„…£³ÃÓãó)\Z”¤´ÄÔäô•¥µÅÕåõ(GWf8v†–¦¶ÆÖæögw‡—§·Ç×ç÷HXhxˆ˜¨¸ÈØèø9IYiy‰™©¹ÉÙéù*:JZjzŠšªºÊÚêúÿÚ\0\0\0?\0ß{ÜÑ—^÷î½×½û¯uï~ëÝ{ßº÷DËså;Ëä.çÍm¾šìTè®”Ú9œŽØÝ=Í‚ÛÛsxvÇbnüKønéÚý?E½qyþ»Ù{ci×RTãr;“-ŠÜ5yhª)(¨)…ßÌn‘ÚYF¯r¾%Ã%p**S>”¡Î@n¬Ç&ŸàÍf7ŠCæoÎm·¾p¯÷T½…”ïŸô&O!á©F}ÏÖ½›¶7oKæ±µST–Ž=·KˆÕi5¯íÃ¼™–{ekvÁ\\à|ˆ¡û*z×‡JÙèPèžÉíwÜ»Ÿ£þBâ°Qv¾ËÆA¸ð‡²èê±}{Þ½kY_&2“~m¬B»%‘Ù»·o×ˆ¨7^Ý–ªµ15Õ4µÕSÐä©\n¦¼·„F·v‡ô[ˆóSóûOò=YX×CqèÑ{-êý{ßº÷^÷î½×½û¯uï~ëÝ{ßº÷^÷î½×½û¯uï~ëÝ½ŸÙ»W¨ö}~ôÝ²e†šz,v7·ñ5Û‹un­Ã•¨J,ÒÚkùMÇ¹÷BT‚’ŽÝØ³$‘ZÛIs DÀó>@u¦!EODò¿z2íõJû‡®ºgâŸIaç¡ið›7ädv>þìÊ¹ÌÕFšê1·àØ]Q=(€É71¼Ïyœ®f‚=žð§w‘ \ZpŒ+C^!}:¥dlAÒó§¾Avì;³\rÓ,º‹Ô½­›¢×´wçZîjýÿ\0ñ¿·²4ôu™¦¯÷¶oµ7–Úßx¼v>j¹¶æåÃcª§¤ŠI±•9Hiê¥§fâÊÝâ{»uEš©Ã_JŸ—™<+J¨>k¥†z9>Êzs¯{÷^ëÞý×ºì{qÈõî»¿»k_^µ×ÿÐß{ÜÑ—^÷î½×½û¯uï~ëÝ]ßÛ» ú‹±;“²÷~alnºÚ¹MË¸w~è¬€ÁÒÒBR\nœ\\¾„†Zùb‰E‰y$U\0–Ú»8e¸¸E…I—ˆ§òþtëL@ž‹Ý×Ò¿\rþü2‡äÇyuÞÉÌoN”Ú{Š«sn,¢á)7ÆôÜ{Z—³»#?‰¤¨iªžœå75E}\\|4Ë8.QYG³+Û9ïoî…´DªŸBh	í4 ~]6¬OF‹!ó?â¦7¬zã¹æïÎµ©êÞá’Xº«zâ7ì?b55&Bº°ìæÁŒ…NàL}&&¦J–¦ŽE¦X[ÊRÞÑ\r¶óÅ–œj|½+ûGù:¶µ¥kÐ#òµöðøò7\r¿v¼}}»{Åº®ƒter2â6öàÛß\"z³wãö²Qdf‚\'|–{~í¼.6’¤Å]eLJAiÇµ–pJ©lÑBšþÐ	Í)EÖsè|«Õ\\ŽÆ¯Vì§z÷¿uî½ïÝ{¯{÷^ëÞý×º÷¿uî½ïÝ{¯{÷^ëÞý×º\"Û;mKÛ7û‹²·luûoâÎauHb%ËÉ>Ù±ª»»û2qGF›¿-²;mízjéžzŠuF\ny\ZÁ9ÌÒ]¶+t?©/sb˜àäÊkþp¤4;¤$ðŸdÝ;Ð#ò+kdwgPîˆpµòb·Ü—¿öîRŸM’­¢ÍõÖâÅïz6ÇRUþË×ÖjXõŸ“kû[a ŽàÁ^4ãŠþÂ>ªÃP§CL2y¡ŠcÅå%ñH-$^Eãp³%ìÄ{K%5¾‘E© ôêÃ¬¾é×º÷¿uî½ïÝ{®^Ôu®¿ÿÑß{ÜÑ—^÷î½×½û¯uï~ëÝ\"{#bá»?¯·¿\\î\ZzZ¬úÚ{ƒiå!­£ƒ!Li3øºœl²IET’SÔO‘U\Z”{~ÚSñ8jxú|Í<ÏZaPGUç…èlÇÏ‚ý/×ýÛ=ÉÔ=­²êööºwgÅ®Ì®é½æý»ÒyŠ­“ÙÛr=Ñ†£ªÈcöŽïÜÛz©êé)Ä^z9âx%UðËìÖk…Ûïåh-Ñ¢a€ëPÈãŠÒ„œÐÔSˆêŠ «~]5|‹¤ë¿˜3T|6ë¿’×ñóyv—Aöcmï>ŒÏäv_kõÝ\'O÷žÐÚ[Û;SM6×§Ëí˜wã…ðsTÔäè—qQ\nèiR_ÕDü\Z­oeÃ§P$~*\Z(¯¯ÂOÔ÷vkÐØ¬]‡ðÏá6ÕÜ“o¯´±÷_{î^ÛËùÙ¹Î§øî6Ö#­?Šæª>Þ¶«~ö?~åðóe¥‰R²ƒmæ¢Óë)í˜o7	bÐÇµtŠêƒO–š¯ªÕO\0k¶T«öEÓ½{ßº÷^÷î½×½û¯uï~ëÝ{ßº÷^÷î½×½û¯uß¿z÷D£â–cõÆíùÖôûr¯föžs·wmoª©ßy6ÿ\0Çî*ÊL&Óí¬KvÕåñf§rlüN*›5G‹­0QeéemI°ÆN7òÃkp$\r@¾UÏ%ŽšÕ¼ÏM%0óèë{\'éÞ€Ï‘9þ®ÇõNóÛ­›ÂQ`»jî½ŸÛÉïZ\r‰’ß£#·ëãÈìÝ»˜ªÉc*“)›Æ<k¦Mr5 RámŠÏã¤)Áã¤°oíóõê­J\ZðéUÓ›«¬ú›­zî·3—Ü5{cmm«U›ÏdësYŒ”ø,-6Z¬†c\'QY”ÊT;Ój*§žªPM,²‘›»”Mq4©c€)O•>\\:ÚŠ\0:}¦ë}{ßº÷^÷î½×½û¯uÿÒß{ÜÑ—^÷î½×½û¯uï~ëÝ{ßº÷UÍ—ïM£ñwå7yá·Æ3µ_«»/nuzîÞØ†…7ORtŽèÊã+:rŸºqøš¼ŽöØøMëŽêÊIœlcmz:ˆ§ûúÊ$CP|¶Æ÷o·ÐËã) )­HÍLR§PÀÏÂ\09£:´¹¯‹Sü¥øW±þMv7tSÿ\06Ê/˜žíÃGX|\\Ã÷¾ÞîoáYóZ²°u×Ju´yÜõu}\re=>3	G‚ÂÓe7ž*÷ÊTÌ’ÄªæÞúk8í¿v´jœY•€¨K\0’*q\\\ZéJ+j×Zôs¾\Ze2»šïß”;§¯ëúß=Ù}“’êM³´÷-:Ã¿0]Uñ³pn¾¿Û”»æ(êki1»ƒ=¾ª·>tÐRÊðÐÒe©áv’¢9¤d;ªÇl\"°G-áñ4¥I€©ü:Aù¯ŸVŽ¦¬|ú=Ézw¯{÷^ëÞý×º÷¿uî½ïÝ{¯{÷^ëÞý×º÷¿uî½ïÝ{¢å¿òYª’Ÿé)kª¢Àf6~Ðf1±©jJ¬=[e0•³•ÿ\07%4ˆ…½?å}H¹¤\n­¶Üñÿ\0!áö“Ÿ°tÛ|iÑöWÓ-ÛI>çù%Ö;~ª»É¶v—WöùÊm¶ûj¼~_qVî­„ÙÙÖ:¥dPøµÈÔãg\n®•K##‡ÙœÃ°ž@£^ §‘_Ì—çÕYGF7ÙgWëÞý×º÷¿uî½ïÝ{¯{÷^ëÿÓß{ÜÑ—^÷î½×½û¯uï~ãÇ¯tVûgåNÙØ;¶£©ºÿ\0doÞÿ\0ï…ÆÓäGQõF2Ž¢m¿KæÄäû?°÷n¬z‹	Zº$GÎå©«ê©Ü=s”‰Í­6™çYˆuPÖµ \"´\'Î•¡\0ñ§Ti\0ÀÉé7Ð=]ò\nîîŸÝÿ\0ë<E_fl>›ëÞ¾êN´§Êf¿ÑvÑëjžÇÜÙ(7Ÿhæ©0õ»3›¯´+MI‹Åbèéi\"Ždvš¢W/\Z4‚l£“ôËc^&ƒìPxž\'‡¥­Ib3ÑžÄì}•‚ÉÔæ°{;jásÆV­Ëbvî#“¬iß\\æª¾ŠŽ\Zºƒ3›¾·:&þË\rÕÃ\0ºñò\0€W«Ó¢ŸI˜íO‹y]óQ¾1[Ç»ºCyö?bvV?yì\rµ6g|ô=&öÜ´™¨öáë|TÙ]ßØÛ_’ÉVÅ¸0«WÇG*ÒM‰J\Zu­ŒÕá‹pXÚ$ð®@†´j šPz™$›¥jj½\Zn½ì]‡Û?Ø=g»öþûÙ;’—ï0{£kå)rø|ŒÚ)DUt’È‰SKPð¾™©çFŠTI”Ïo-´ÉF8\"£¥§¶zß^÷î½×½û¯uï~ëÝ{ßº÷^÷î½×½û¯tÕšÍa¶Þ#%ŸÜY|f†£Ÿ#—Íæ«é1XŒV>–&–ª¿%‘®–\n:\Z*h”´’Êèˆ ’@÷xã–V	\ZÄûp:ñ qáÑ:Ù½•¶þRwW^ïŽ«ÅÖn>œéz-í•§ï	±’c¶®öß{³Û6Ÿkumf^ŽŸ%½væ;oVÖWdóØÔ8¥j()k*åZ¨éä‡èleŠi!øsƒ‘äA®xãfÖàÐÛ»{ÓkìîóéÞ†ÉÓd›r÷NÖímË¶òøìÍN*õL[>¯!ŒÊdhñX\\e^gºfžŒVÖÑ½Bãj	9I<H\"³ymg¸Šäp!½xðòÿ\0(êå€*Ÿ@—tî,/Çn÷£ù;½°Ùyº«põ?Pvfÿ\0Âíù·úhnÌöûÚ»»wSâRmÅAÖY†ÝJ\\ÆJ\nZÚ|UTÕ\nZ?ºªEÖq}e“Ù¤€L\Z JÔ€e8ñ¥MQŽ–Ltp6îãÛÛ¿‰ÝK=†Ýg=CO“Àî=»”¡ÍàsXÚ¤Òdq9|dõXü\rLL\Z9¡‘ãu7eRE$LVE ƒLúŽ#§*ž½·Öú÷¿uî½ïÝ{¯{÷^ëÿÔß{ÜÑ—^÷î½ÔžO…Æä39zÊln+CY“ÊdkeH)(1ÔòUVÖÕO!	5-4M$ŽÄU$ûq¥uEÄõâiSÕcwE|¦ùÉ„Ân½›ó7µ~ôŽsír›w­:«¯vÍ/cv&ÎQÕâóÝÙ™‰¨û\'bÿ\0{¡Fž,&Ú«ÀÖÐã*R:ê§¬ia¤>‚çoÛÇ%·‰p0I\0Qšw\0=CÐÓ‹$Hùƒ¢í/òZÜa°¬óù„Çb2Ù\nº¾°›uE¹¾:Vbë¤«s‹‡©÷^C3¹¤­’JŸ¹¨Éd÷^Z¾|ƒIR$ŒÉãW¿Z±(ö(SÈé\Z«@8\0p¥GAALûÂ>½&{Cùxçz‡3×y:.ñØ»z—xï\ZNº‹°°_{—kîþº¯ÜK<;?pb»3¨¾QàNÍÎÒî	€£Égq9|%VV¢(kÒ(&r]·¿Šrí\Z»RhÌ `WN8P­)_.ªcaÑ€ùÓ¿\'ö?_SVoOæ¹ó»–þ°ö¥\'Iî¡Ù=£¹³ôÉÛ8}ÓŸøŸ¿zÛ´»7ybcZÚºLfÒƒjãr¾l¥+ÓRÉ<LÚ\\YË$‹oc¦B2I\0Jš–H¼05âµ§[`Àe±Ðq´¿–wÌž½ÎSî­™üÀj!ÎÍ‘+š9LÊ¬µ.^©qõpC-~?y|Þß[o+ö\Z•¨ŠŸ%‰®¢dO°:C_¾,Ž¥k:\0)‚ƒòªBì?1Õ¼6þ/ðÿ\0Ð]$wgÂoçÖLoa|bù£ð÷vö­^Q2©”í¿[‡¥ð÷åÆUQLý·ƒéœöïÙYÎ6h(d Íâ¶îsGRRË”’‰Å?»þõÚ&ŒÇsfú\0íµéM@y‚H©­+ž«áËZêêÛº³÷¶õÃWí.æÛ{[cwÿ\0_Óái{ShìÅ_ºv<“æie¨Ão^¸Ü9œFÝÏæzëw¥%A “!Ž¢¯¥©¥ª¢©‹ÍJï!%å²B|kvÕjÇ´ð#Î„TÐÓæ|‰¥GN©\'âè~ö‡«uï~ëÝ{ßº÷^÷î½×½û¯tY;³»÷ÖÙ«ÊußÇÞ° ïõ§ÚyØ»K5¼ß®ºókPÃM3`Geö\\[gyt²Þ¾¥ÀcÓUY”–)¥\"š‚š®¾˜ÆÚÎ7Ušê]…IõÅGøF\rk•\rFcÁEOTá€ù…Ö‹°ª$ùá¶7W{|€Ù;š“¶z§¶mVt^nÜµ]M.ÐÚ8žŽï\rÓ«í×ÇÎ*#Ü‡ûËç%ë°õô\Zi©D¦É’÷\\ë±\Zµ\ZpMSIù«âÖ¬÷ŠžvÞù¾ŸtlþöÌü…éú®¦ÏÕQlíÝñCb×uvúÈõ~¿\'Yˆ‹±$í]¿Ÿ}Ë¹÷þÊÎTã×vÐÑ‰¶î?µéG\rLôqä*Ëä²áxƒµr()Ž49#·$ŒÚ6†­{z>ù³ÿ\0KM^³(Ùô}¿žt9·*A¸ºå*²¸	¥Mß’ÊK,±EKYL²ãh¢ZˆêLSURùHr u(+¬zú7Ÿçù!î€Ìì>ÜÝ›ÿ\0zíNƒÞ½´ŸãÆ;nî>Ôÿ\0I{x×m]õ›ÝxJÍÇˆêÊ­ÛCŸ‹-Ö˜êY\\¾u1ïF2ØÉ)ã¬H²dÎÒÒ(áI.\"rÒ|:i€H>•ÈªšT0m˜–¢žSþãù¥ðÿ\0cv[¡r}ŸðÃä6S23µ!ÙÍ/Ã¾ÄrT8œ®wyšYÐgpá·.B¢SÜaX½—X*\'¥û£\rM‡¢Êéãð®4KoP\'¸ùŒ‚1ÅB!À–‹BV‹ñ7åOtoýµÔ4Ÿ.:sjôcwžÛmÇÕt{;uîýÏ¶7d´8JÅÙ¹\Zmû°:ëyl.ÉÄmêGË.N…þï$–ž¢I¨òô„7Û|ãÉe!hã9˜0u5rx×5«©åšÇ£ÿ\0ì—§:÷¿uî½ïÝ{¯ÿÕß{ÜÑ—^÷î½Ñùs¼2[ƒ³>,üKÆâ~ïòS{o,ïid«(««0ÔýÐxL^øßûf´ÐO‰û+?—ÀmÒ•7¡Ÿ‘­Ža&¤†S¶ °Ý^±ÃZ]\\E1ƒæÈ yÓRªúô3üªîZŸýØ±›lEšÛ´˜:\r²›ÎL”[b»wîýÓƒÙ[?–|;˜‡3¹÷%\"J»K2‹‹Ü#²….®V)	¡É§\ZŸØµ?—Wc¥kÑ„µ‰’8?ì=¦‘<)^3ÅI³XdW¢?üÅèv¦OâW`c÷WCÛøºÍÉÔÐQí3uÛr¦“pIÛ{ m­ç‰Íbqù|Ý.àëìçƒ7Š‚Š¶¶º¾‚\ZH©çiüLe´©¢P‡KqÏá8âxdÐW8¯T’šr:jù‚ÈT|‚øE¸ó•[‡ìëSÛ“mÔýåhà:WzUaû.»{ãúçpQáäÛÛtf lSd6zåRR*k#†LTê`•|ÊEJLxµGñ\n%É#$\ZSÈšŠ‘RƒË£ëì‡§z\r7ePíÞÎën°›˜«®ì¬/af¨s”‹Hp¸Xºî-«-e.]¥©Ž´TæëŒRb‘OÛË¬¥—R´¶i-žãX¢µ)ùW­W t÷`£ëŽåèní¥_¶¨Ü›žãfüÕ\ZZžÎì¹êò;\Z|ŠËWä¶¿kchbÅË\"I2Gž¯¥€¯oj­+qkqlíÚ£RüŽIÿ\0kJ³z:£a•€èØû*éÎ½ïÝ{¯{÷^ëÞý×ºCöfûÅuw\\ïÎÉÍ„lFÁÙûxd\"–®ž€TRíÜE^VJE­«e¦¦–°Rø‘ä:C¸¿µ›‰â„~#Oõ}¼?>´Æ€žƒŸ½y’ØÝkA–Þš»¶»:JnÌîÃ-ÍöNäÄãW%K•#¨M¿³±””˜$\rt¸LU$-wFfzþmÿ\0b˜_³í5©¦*Izª\r+óè?îÝ£µq9mÓUÙ{[Ø	òŸhu_s`3”0ÖÒmŠêÉ\'Ú{Ks×Âézí•š«ÌÒc2m©%ÂT}¶IiÖ®Ju–w2´Z!”¥Äyø€?.#çÃ:GZePjFE?¼¿—ßÃþ®ÀîÍë¶v¶ãÛÞY]Ñù}ã™ìNÉìÕºò½©¿¶.Ð¢¬©Ü}•¸7ææ¤Ýpã£’Ž,¥-U,ÂŠ¦¢:©%€ÙÙîW·@âª• \0\0AjPÄŠPS4ëLŠ3åÕ•äZ„ö®ÓˆUQÅ“Mƒ¿¥ZI6UUnB³\ZÛƒ®£’jNÄ\n(våXŒTa˜´Ù—ž\Z•qŽI)jC(Ø\\S#Ñ©Ž\'íòüúsª¥“ù||}ùuòåžcåÖ¯Ýù-ÞÕØø¯¸³]bô»sruQîM¹ŸÜ[ãª³[OölÁ*Rc7B«…ð°¦¡Öb}.áqekf-M”œ€x–¥+ZTPš2H„Ï«£—³6VÎÆï<OItN)¶ÞÀé}Ë‰ÜË»âÍnÖàÉoú<V3²ú¿/¹·4ù\\öüÎd¶æ~®r¿!“­©Æc#ÇÒZ¾6¤J×3Çnn.^²¹\ZW†;†ª\npÈÀÁ\'…6¢–ÒBÏÉM‡˜ßÝA¹àÚTÐOØû@ÑvOSÍ1Š&¥í½ªMÑ²ÄuSTÑÅK[)Ú³$‚	(+gŠu’žIbt6ˆ®ŠäDØo³ìó4&ž„×«8ªÐô!u¦ûÄö\\ìÍÀÇU±¶NÔßxXk©ç¤®‹»°Tƒm%TPÔÒÕ%A‘Èˆèà†PAÓ\\F\"žhb?aêÀÔ:[ûg­õï~ëÝÿÖß{ÜÑ—^÷î½Ñ,ù)/ßß7íY¯5ðwŸ`õm%6-ê\rem\'itgÔTAUOxëp4Y]A_X“:E¢J‘®JxÑŽ6æ/ouÏ\0u*×þ5ÓoÅOÏ©ß?°8ýÉñºñù&	MO†Û¹èÝ®,†ÖÞûcsâe!Ckå1±RkXðO¶v¦\"íOâ¡¶ƒü­Éðž†îìâ:û3/ÇOôRý±F>L/uÝ]×Ó-t_Å¨³5ÛM¹qsMi>Ú¦\nz±áuÁ\"¶7³ýZ¿‡­¾\ZW‰õÿ\08ûzßv‘§@ïËL™Ÿ‹Û‡¼±}Í»%¡ÙØ˜êöúg0¹<N÷Üù|6Ê©Ý½;WÉ®ïÛ=Š“rUO´¥£ÉÒdaÉšh¾þÏP®Ø8KÚÄÄE“šh+ÝŠõÇÃ\\ƒ§S^=+;c#O¶7wÅèàlæzª»¶fÚ44‘îÈ±ñÕÑä:‡±š¿tg©*·FÞ¦Ý§G‹û€†“30š]pÒ$Œµ´ž…Cý|uU@¼@¯P(@?·xæ‡Æ§Ï¥onn.éÀ/^ÃÓiµ;|ÿ\0emlbVoÃ}ƒC×½_SS$ûË~ã §Úûš³{çñxús/²ûªÊ˜ÚJ˜¡ŽCí%²[?Šn%+D$\nV§Èqf¿—ÕqAÐi¿ò4•_0>7íÔš4ÈÐõ7Èýß$4´ØÈò/¶˜Ñ™%ZŠ‰¢®ÍGäH‘ÂFC9_F¥°ãl¸4Çˆ?ãJÔÿ\0Žª´üºò¦83•Ÿöøê|Ô{ËåZWÕbê¢§ž\'¢êª\rÏÝI–1Õ#{s3×T5Ñ²–9à”úOºíúU.Ý¼£ãò\'AþOÖŸðý½?e}9×½û¯uï~ëÝ{ßº÷E·æ\Z£pü_ï<E,uSUõÞtø()!¯«x©¡ZªOGT\Zž¢E§Î—\ZxçÚý´Òòñ#ü#ªIðžŒlSÃUU4òÇ55Di<Dë$RÃ2‡ŠXäRUã‘A ƒí=ÁÕ<Äâ¬Oí=XpÛ…p=ïÔ‡·p5qÉI¹ñ“Ö²Í–ÇTE7rb*óÛ/~>®#®rã¥\rèd”C©5£)/Ä\rÜFUÈ¡ý ˜=h÷)¡è­ôu¯ÉüŸTbé’º»\'ñûK‘ù%UQIS3l|“‹hÕlÉ:~zÚº:ZMÃ»¶nK\'šÊfié£1â\'‡3È’TS«/»…mRæMY”ög%+Pßí¨\rkB	E:ª6 §GÖÖžÓßï.tc—¯šµ¶xÙ¯ýÚ’¶]È°&ånÁ8£ñØ ‰©FkAûw5-\ZdgOÒÁ¨·\ZçÏÊµþ_:ç«ùôW{;+œøãÛƒÚ»\'¤ûºk>Cm­ƒ‚\\XÓÝ?¶zÚŸsapUÀåóÔØŽ½Û{ÓkgqÔµ;©áÆP&ß&¶V–jTsp/à$¹T1r+ÛALb Pú“\\CJ\Z!$=})²(þ6ô^ÐÛ{ûxÁžÜ4oM[ÙYE=·çovNæŽ§wn©¨è T¥mßØ›šAK\0R)¡šÑ>ÒÝÈ÷×<Qö\0h®ÏÎ‚‚¾t¯¬£J€z²¹Lv’Íf+¨ñxœ=nS+“ÈÕAEÇcqôòU×WWVÔË5,-$²ÈÊ‘¢–b\0\'ÚFvªKzõn¿Œ	GÆŸ1ã«¡Êã×£ºœPdéªàÈSd(¿¸xI[M_K5M5m=M>—Žhä‘$BY¨¾ë.ª3â7øOZ_…~Î‡_izß^÷î½×ÿ×ß{ÜÑ—^÷î½ÑPù¶*ó=Ñð›=MF^-¡ß›Þ¿%’v¦ûlf?+ñ›½°h“%D2ê—+—¯¤¦„Äc™fuÒúK£™ÙSÁ¼RÐÿ\0˜t>^´§M¿áûzïçN7/”øyò6\r4Õ™Z.©Ý9êZJf¥Jš‘¶i¼u0S}ó%TËIŠF²¬äÏ¿mt«üh“¯Iðž>¾­R—Y¥ÈÑÒ×Ó£šzÈ¦\"èyG1È.ÐûI5|iñ$ŸÛž®8tUþdæv^[¨³?³Ë6suü«Äîž†ØûŸ¥Û»§v\rß¶rT[ç)ŠÊTá÷X\\W_l9ëó™LœÔTôT´|rÔOMO:Í¾)|ar†‘ÄCEF Ž&ƒóÍE$8§™è¢`±½×Õ;Ã«»G»î‡ª>)o-ÙñÅrý•è=Ý›ì^¼ì©áÄíŸ—XÕ°vèëzl\r$Û[oåÒ:ŒO‹Žlž ÇMAY1«5µÌRC	Y6…`‚ÐðÅ¯`¤Ž©F1øGÙÕ¸{¡ ñéþ‰Žül–Oç§Çm2cµþ8|žÝ™YØdI>_züvÚøªHZ*gÅK¨–a4©9ž(ŒJÊ%*if×9#‹¯óYò Sí>£ªíìÿ\07J‘ô˜·ßßòY\nÜÆ*£ò^5ÄWâL¬’ä2)Ü˜³ƒÌAB‰dÂçhj¦ŠFxê–)V6ñ‚‰M¼·‡zª*<>þjG×›Š}¿ä=\ZÏe_¯{÷^ëÞý×º÷¿uî?’yÃütïì´‹TñâºOµr2-@µmÅÏTºÒµTSÒ­IXŒÈ{jR.\ní¸½·ÇWòóþUê’|¥·\\TCY×›\n²”ÊiªöVÖ©¦3øÌæ	°TÂf0ªBe1¸Õ¡BÞö\0{jä<•ãþÇVèøÛO>»5Œ¯O´þLv]UL”K÷Tý§Aµ»ê¤Ç¨iªŠ‡1Û5Tbe,íH¸`UTî\Zdú[´g#ý)Ð?nž©5/¡èÍ{.,Æ•$Ó§:\nêäû¯oÄ•3}µ/Vï	*èãìO¶§×îíŽ¸úš¾¥Zv9¹ŒxÚ”§Üo*Q5#ÿ\0s\Z­\'èÙü¼Aåž\rçÆŸ•<¸Ž½çùt)ûI×º+ß%ò±d%èî¦†]‘í®óØÉ-/Q]>Öêš¦î­ÏÀê”T8ÕëúZydœ&’¶*OóÕP‚g· æbHUBØp1PGÈÀuGü#çÐ‡òg~†îä¦aKõe­<…Ds¶ËÍˆœ´_¸HAºú‡ãŸmmô7°Tb§ü¯Ið¦ô~©ÒÝ?µéâZx6ßVõöjŠˆá‡´±èâIêÏÝÎ±¥0åýÆíê\'ÛwÍ®êá½Xõ´øWìèQö—«uï~ëÝÿÐß{ÜÑ—]û²®¯³¯tH~Xc›uöÿ\0Àý™Ž«Aœ¥ùC[ÛQÔêWeuwGöÊn\\Ì±%Tÿ\0Â)3{¿–KÃ¬ËRBòƒ:$‡mbK¹éúa)ò\'R\Zyf€Ÿ\\tÔ™Ò<ëÑŠï(¶lý-Û”½‰–8…_Ö›ç¼s*$iq{c#¶rtªÊxâWžj¸qõ2cZI%Òˆ¬Ä’ÈÈ.¢0­d®åOõ|ú»SI¯«×ã7ó1øÛ»ºãæÜÀv÷Él·Zí­¸>:õÆ#%šïLWkí,ÝÛ½™·{`ËKK‘êv^ö¨’7•Ý¿ÁpØ™ÍERäÊëj›ê®¨–ºªÊ†¥tžÊ¿ zm\\i‹tr:k¦÷6/uç»Ëºò8}ËÞ»ÇßŽ¨©Ù}7×ÉT™:Ÿ¬\'ÉSRd*1Õ(Ò³pg&‚–»tå!†iá¦££Æcñèï.£e[kPDôÏýGÏ9bÖU?qèÄd±´9œvC•£§Èbò´UxÜ•\\k5-v>º	)k(êa`VZzšiO¬G´Q±ÖUøÿ\0W®EEˆæÍíOöR²Ûc þEï*¶Ø¹ÌÛíŸÿ\0!wv¸°;‡$/S·zO¸·œºq;{¹öý42Ðâ+²-IM½qÔôï²æ¾ö—Ù¬¶ÃpSujƒÅ\0jAû*8c…x_ :l­Ã¬]WÙ}Û?~HÑmNÅÇn¬Ç½	×9M±†«¥«¡ÚÛ¶7möïžª¦\nF¹žiíD\":¹RÓ<rG®uzh§¶ÚâI#\0Jä×‰Ò•à|3C×\r!¡áÐ‰ßù‡Ãw_ÂÏº­–ƒ—îý÷ƒ®w2&6¯/]ñ×¸¤ÚøÚÙ/öâ¦³/J>Íd±z¤P‡YPY±BÖ÷ÔðÏíªÓöž¬ÿ\0‡íèØû(êý{ßº÷^÷î½×½û¯twÌØÈ:;¹$ÍUµ$u_`.F±ä–žŽ]§–Šy!Š9iåš£Chèò=•Hb=¬Ûõ}d:FkþCÕ$øO½]G.;¬ºç5˜ÉèvÐ£›*I¸ùivö:	(dŽP$ŽJGC\Z[}Òïºær\rAn>½m~û:/ÿ\03Û·7Þÿ\09)²™UNÎÛ=ã×ofcé*ðµyLmJ|gémÁ»N]q•ähEnOpÄôôùŽ \"´ˆÄ}©ºX–ÊÄ•>)SëÃ\\Ÿ—§ZZêoOöFóÙoWèŸííÁÙYï›Ûÿ\0ûcéí…ÐûsE»+öfÓ Þ²7&ç‡=–ÄâweVö“å:ß¶’‰âjm·£;=|\r’’³\ZÔ¨i\"Â›d4rdf­*h\0¨áJq­jkò¡© ®¶Æ:8Êú¿E¶ó3òÿ\0áå&ç—KœÜ[äî®¦ª4)š­ÝÑmÞ´Ïd0x—,¹V¥ªØ˜LÕm\\iz6lt-8ó%!ö¢S¶Þè®K_JPþU©×=6ÔÖ	?(YWã7È¦o.ôÛ¥ÌGÊŸèÿ\0pkh¤ˆã‘ä1àiöÏ÷>Úœu«öqër|¡;dÇ;3hÅ\r[_,°Ñ&&‘c`Ö]@ Ø_ÚiÉ2½z¸éQíž½×½û¯uÿÑßéîø>uèË^úûõuã‡^è²uvÚmÛÝ}·ß9ŸáUëNbèî¢ž’G©—×›BjjþÂªyøir›Ë¸>ú*±+>;nâ™œ²•C+™|+X-Ìo´ð¦8R„ –+Õ÷ë¿—ôÙZÞ„Ü´8Šb¢·tuU%^ÜÏ}è¦Ý¸JŽÙÙn-“$uî}Ùjœ~DãÊÔS³#¨*[ÛÈ\0ê Òßà4?`4\'ä^“á=Kø­ˆ«¡ê8sRÕÑÖbû{ö—jlÅ¥ÅÅŒz¼í~ÉÝ=²1õ–\"ª³ vþç†¢¦Z•&¢¡ÖUWB=Úú@dXÖºÑ·Ú >Ê‚1é×PŒ‡´}[ ©{{Ü»â·±»[dÒìŽÆÝndín¼ÏuÄÛº¯hUGC]»ö.?rEM“Ü]w”«.¸¼ÒÃ.M\"iiŒ•Ý¸·x J­©`ƒJù`œ>Ôu k\\t¿Ü{kno_kný¿„Ý[_pPTbóûorb¨3¸Þ.­uXì¾)OUŽÉPTÆJÉÑ¼n¼G¶¢–HdŒÃ­U[ñc¯v\'O|µ¦ê=ƒ´vç^É²¶7Í–æÛ;OkÑì³[IÙ&:ƒµ6nK·1ñ¾>Z8v¾û¡€ÔÀ`dŠHÖ(ÓLhxí-ƒÈI5\nÖº¨æû½4¢„êz°®ýë)ûoª·FÑÅU®\'wEèëÅ«D›W´6fF—tõÎçô¸ñá·†&’Zˆ™^\ZºA-4é$K”YÌ\"ký›`PqO•kJñ=]—P§ŸJ>ªÞØ½k±wÌÔÐQÖn­†ËäñôÏ#ÁÌÔÑEükMûÿ\0î+,³S‘%¤Sœ.âðn%E€ãýŽ8ôùS­©¨¡Ún­×½û¯uï~ëÝ^åh{W´öÇ\"c¨ÛG;§º¨¤‚¡ ÈìM±Ÿ¦ÇõþÉ«œRMJ ìÆ¥5Ó¼‘\nìFÛÉS>¸e’)MmWéí¥¼eî=¨h4§	 ŒŽ›næTòéNÿ\0\':®>ü¦øã$›Ù{©%‚<¤o¿!ëFÜ©´GaÇ°cíYðì\ZžÂ©ëÈçÏG‡‡!-_ðºIåeS“Hìe’4º‘Ô«0$j\Z´“BÄWU+ŠÓ‰ã\\u²à#¢‹ñƒtm±Ìö×cïœ†Ëê­µ”ë£Ù]á¼·IÚû?kï.Óï^áÞû–¶lM,ÙI6JââI*ˆ(–}(±¡üR]Cn«;†+æx@>ÑZ’uPV½6ŒOV×]Ö½¿µé7¿TvÉìí›sìëýÕƒÞr®UŠÞ3[z·!Ž’xá©™šÕ]IãÙÖó[6‰ã*þ„PþÃ‘ùôð äu&,«?`Wàÿ\0¼»bE§ÙØŒ·÷::{o:6¬Íç(Ææ¬«þ*ú¶¾LPý­4b–«¤¼ï{+ú\nÞq9òü±þ^={Ï ç¸¾P|qø÷S…¢ï.óê®¦Èn:jÊÜ7o¿¶r™ºs$uõØ¼nR¾ž¾º‚†YU%š8Ú(äuV`Ì´6WW\n“ÔÀ?ÕCèzÑe^\'¢\'Ü_è—¸þNtGyÑ¾3´ñ+ftÖþøÏØØWÆçúâŽ£uü‚‡¬û“%±wý.Ið‘îÜîÙÞX+ÒšF’£BCëñû:€Okdb#CwëxRÉ¨R¼U¿*W‡M\Z;ã‡G»§žîœÿ\0AÔõvüÈ`©¶êÃœí\\†/jKÓu››+ŠL¼HòVnoï^SvÔlú±‘™	.\'í¢z±=áöV-\Z+t»Y—]kLÖ™áJãRj)Lôîª’½1üwŠ¿«ßßâ9ÆtŒ»Zn¼Ÿ%WU’¨—¦·æ>¾³bcfÈUI4ŽÛ+3‚ÍíªxžG™q˜J9¤æu½ï¿]b½Uøo]C¥qBO©¦\0­.*¾Ïe_¯{÷^ëÿÒßƒÜ/ÑHNÏÞPõÇZv\'aT$/O°ö&îÞs%D“ÃNðím¿ÎJ•RÓVÔÅ%	ÑÃ4Š·*Žl¥ËhŒóÇµ	<ŸZc@OIŽ{\"£­ú§v=vA³}»×Jpç¶¿#.áÝ2a©j÷^ãŸ!”c’®©Ü;Ž¢ª¶I§´²I9f\0’¯%ÜHÃá® ãOË¯( ¢yüÒ»§`üx\\¾?1™£¬Úµ{·¹j0{nŽz½Å¹©:¬÷§díz,A‡	œñÔR÷N3gJ*jˆðy%”C\"ý¢ îÄ¨ÔÅTÀTÑ¼ÿ\0„ÿ\0„ùuI:]|	ùK°ûë©v¾ÐÇã±»3°ºÓcí\\>äØxÝËI»°G†£M¤w7^nÈ)1Sní‡çÀ×á^j¼~#3‹Ìc*±Ù|f7!O%0¦çha‘çV,ŒjM(AÇÀV Š(E	\'ÈÕ\0=?e½9×½û¯t—Þ{ÃoõöÒÜ»ãväW¶v–#¸s¹ŽIE&/K%]\\«(óO(Š\"4Vy…PIÞ ‰¦‘\"^$õâ@=R÷Å¯‘ùîÃù·»w¾ðÚ¸½CÙ»ã9±6kÃŸ“qPîm£_Ô¸:M¯ŒÆfÌT˜Ü®{iö7Å}åŒÝU8È¤ÀÐçå¢ÆÐWåËU0šòßM‚*Ô Eò¡:IZÓ¥#D¼:`ÿ\0•z¼Ÿa®Ÿè°|`’“AÞ;§Æâzóä§ocqEQ=Lò.ýÈc»»+UXÕ´O>ãíZïH\"Š\Z1hT»qNÛY*K§ä½£öÐþ}Q?ÛÑöSÓ{ßº÷^÷î½ÑTè§]ÅÝŸ/7³VTÕµgì~¢¢‚¬ü+×}I³7$ÔXçt\r2§pö]}V…bŸs4¯`ÌÄšÞÕ,ìP¨þ`hÓ_R:¢üoùt0wóÂõŸWïþÕÏS}ÅUlãØšâÅœÆB—û±µs5õ2áè#sU”¨Ç¬ôñÅYªí\nŸÜ £µIqHô$€=8Ô:uf OU¡ü¼þ8õ—nõ&Êù[Üküg±;+„|FÎßÙMÁ»öG[ã6v16T5[7®7ƒË·6®âÜ¹,NC/U|yÎGüY¨Ú¯í¢HTët½šÖG³·p\Z–\0wV¸ùR3^\0àäµ\Z\ZŽz7]§Ôy±«¢îÿ\0»k‚Ý›Z¢*¾ËêÍ¯CŽÛÛ{¿zâ#[&oWc(©!ÇÓÛðWO“Úyr!œäâþUP¸êú¢ˆ­îÖåZÚð$vµ\0Ògg¥xš®VËÇ¡\'p÷NÛÀõŽkä\r$˜]ÃÕ	ÕXÝû¶sZš™w>ð–¾\nÌžŒ¢|w‚dÜ‘Vãéñ1¤òUTd«Z\0mFRÐ™ÖÑÉ ã€ÆIÍ¯ðñ={PÓ«Ë¤ÆN‡¨ë|>K³;6Ÿžù5ÛëåîÝöþ,•u-ut’äq½G´3	*ñÝAÔÖÛØÈdþ*w®˜K­­ªžû…á‘…¼-KXðØ)Sè}içò\n‘)“ñŠWËžµÚ_wvÛù²¦Ý›\'ie¥ÜËÙ;`KŽÇlÌÖïÁWlñ¡Þðí“Žx¨·ööÄüz›fÌôRÑÇ™|õ<•ÉQ5$\"ë)ä½‰à”VAÁ¡)ùkÕö.)šÑ€FR:µtX½RD#´ÇÈÒF‰nª«!eÿ\09©\0yàd/©žçÓÝ}Â­†ùÕU”´ÕŒ»÷ »“ªýÇÇCþŽ·ÇPæv´$)Á]Qýÿ\0Ì[Q’8ÈÐn½K6× Å@>t “ûNŸÙÕ?Ñ?.Œï²Þ¯×½û¯uÿÓßƒÜ/ÑE·åÞ7=¸~8v¾ËÚã&ãì}º:Ã\rO—¨†–Ž®«²2;&Za,ÒÂ†ªJäÆÕv”(ÕíºEÔnÊI^áOU ÿ\0€©\'Âz14”±PÒSQAþbŽš\ZXuŸ<IzÏ:_ÚW\Z¦uNêšŸ§íêýUfë?Ü;çÝ“ñáûä¯@ô—Ç*I²TQá¥éŽ™í¼/ö¿aáÞ–¤6F¯½ò};\\°‰	Z¬ÝÃÏNu	ãX¡cðÑ‹ã‰e* ãËQÇ‘,F\rI©xt6v7ÅfØ=á–ù—ñ¤ïl¶Þ_·ºâ¿1‘ÆlO»2™œ8÷ËRí=‡ß&§Jq[Å©­\\)ãÇfÌÔF\n¬j8¯ÍÌ_G|ä¡8o0|ªx‘æ~yãZÜ©RqèÈtxõÿ\0È»ã¯«ëŒ4y|žÕÝÛ[pcåÀï®·ßx	R›tußcíJÂr[K{íŠÇÕÑN,ÈñÔ@óROOQ*»Imd(â«äG<ˆ?êÿ\0WV*:=³Öú%ïC…ùW”ÊüR ›‹« ÈQEòã#K[ªÄGµ©“¸\"øÝ“‰$š¦]ÁÜ”ÔÃ=H~ÓfIV³Iù,qÎÝ\r„fîPD§à‡9ûAô q£\0Ù:Ø(áÐQò¿ª6ÞG±þ:uÏ_®¯w„½aÛ¸ÞŽ£Ädé6~ÙÅnÎ‘ÉôÇqõf\n“lã±ïMU‚Âîƒ–’Š\0i°‚µR2ŒJ,Ûå‘á’âBÇõr@©àF‘%øúÐñ§Uw\0=:=YØ¸>ÚëÝ§Ø›xTÅÝ¨ëM\rt?m’Ãd¡’JæÝÌQëÑföæj–¢‚¶Máª§‘55®Iná0LèÄzŠp¡ôùztêš€z/ÝO‰Àüù¿¶é_1S/ØÝAÚUt9h3)G&;xt^ËÙ´y­¹S…1µx:ìÇWäi[ìâ‹!AT’i—Pö¶ô–³±m@4òã©˜ËXý£ª¯Æý/dÄƒÓuï]{¬5ÁKÕU2ÇOKMµÊÁ\"‚Q¤–i¬8ãRÌO\0wD.Ê¾Dÿ\0¨õî‹ŸÅVÉf:²nÅËUPVUw.ûßý»ŸYK¢;+ynzùº®khè1ÐUIIÔ´˜(f,ê#r%•H‘—_šÊ±DE™­@îãý .©Ã_3ÐSòÇvÉ¹¢Ïõ½-N£au~ÈÊ÷çîLb\Z:¼F?¯ ;Ó¬:²\nH§¦ÈÖe;;pmé+ëR\"A‚ÂMAŒåi$ö¯mƒÃOš’Èt(§¯öŸØAí#ªÈuT£ñïod¶ŸBô®ÚÍAKMÂuG_cwt&¥;†›jb“?,RÂ«ÿ\0q˜ÈÒû¬åÍËíó$·sH¤é\'åþuêëP †ù´kÚQ”ä\Zõn‹ÖÐøÕ×›W¯p]#f·ÌÚ]­í©ˆÊä\Z*LLvV[´vŽÝŽŸ\Z´0äv¯^î,Œ?Áé+qÆÒ<†I`Gö²KéÝÞLj(àp¥\ZüY\'Ô“ÕB¨èÂ{AÕº)6¥ÛØþ¯Ü;¢º›ˆÚÝ“Ñ¹êÜÌï[ãq©Ý[¸~Ò«EÈRUeö¶N¿$†0á+XkŒ\"™mZ¼iB‚I©öÒ€þUêø~Þ¤ü_Ý;Ý?{ä¨ì¿M„ÀK›û*ŠZÂê\\áÌÅÒýŸŽ’XRžJÁ¶¶äøìÔ1<‹G¹°Ù8Už‚Ym¹B¡’â6\Z$òÆ¥F==<…+š¨Ï>]9wT“à;sâÞöl˜Çá“±·wZn–iã¦©¦ìÞºÜmÅâ§W5Uu=…µ0´£¯‰~í¤b¥ûÕ•f¶¼€GRV£×U@öWöu¶Ã!¯FgÙgWëÞý×ºÿÔßƒÜ/ÑE·~Ï_»~Ct¾Ã¢–Q„Ø8ÙÝÛÞ8“ÉNõ_cU×eŠÉÈŽë\Zdò›5“¤†hÂÏQ·ŒÑ8’Œk …Ä¤QØ…û>ÃM_ÑnªjYGNŸ!«·\rvÒÅõŽÏ¨žƒt÷Fz>º‡5MÍQµ¶¥unG±·4Ñ¡¦¤Éíý‰¯8É*!þ1-\Z+:Á-,Q5´ÒÔùóõ d‚iÃ¯=i¤q=}£OƒÄwßÁ^ŒÛQaöþÝÛÕÝ¯Ù˜Í¹L´\"jm»ÓE\'[a1˜z9ätø¼mOrÒ	g€;F\"Ž\'ôÌX)†Fšßp™ØêjWþ<?ã¬?:šÓªœ2.Ž×²¾œè¥wÅZ-ï½[¹º²wgÇ.ÿ\0þCˆÈvvÂ¤Ãæ0Ý„Å¤‘böçuuŽä§ªÙ«†ÅE+%\rELt¹üJ˜ìrJ’Zî-\Z.#[ùÄzSüÜ=A Q’¦ ÐôEÔÿ\033ƒlõ/wÿ\01~¶£MÍ€ÞyÁü}øÑèîõì\r·ˆ«ÁSfr¸=Ù½;Ë»¢Út{PçèéjòLU0¾FJšiÚ\nÒîÑ®âÛ\r’TÓ­+CçL|$TuR¬{Kÿ\0«öô|ºÛ¬vPm*Öûfƒjíš	«kV†ªjjkò¹J©+óüö_#=fgrn|öFi*²9L…EVG!W#ÏS4²»9&¸¸šáƒÌåšŸêÿ\0WåÀ\0 »ºÍ}¡ñYqÖdO}nªj:æôTb)j>2|„5ÕÌ\"r`«’ xK\"ÈÒ#rÑ¨öý¹§½»t·N´ÜSíÿ\0!éŸ­éáêß=ŸÕ4ô©Cµû^‡!ò7aSÄ…iaÜRåñ;o¿qÔ`ÌÂ0û»7„ÜU\n#@ù\rÕS.§22ÄõÁYì¡¸-YTé?Î•Ç\ZyP8žª*ÃÖ=Õ8Ù/º¯7&J¾<uõ6ôêºŒ`ƒ\"}ÑÖ%ìíS=q¢\\VYvæsu­5)©xÄq,q.™Ý­l•HF¨>b´¯ííQó?gZ?Ú³£Wì»§:÷¿uî€ï‘ûª]¥Ò{ú®Œ§ñÌö*-…´ÒHk*a“zöNB`láWO’œ˜Øw&ä¦–°Ó¸ŽŽ9dOR{Qe’áN4¯qû>t¯Zc¤W¥]^ÊèŸzŠ‰*qû§:ò(ÔË;Wä!Úûo¥5-:ÍU*I’É¾?\Z‘G­ÃÔNÀ_S{Õ%¼»¡#[7ûêùõ¬\"ôB{§¯ Æ|)Þt}±-f~ü¬ìþž›·h–,†âÈÔnžíí~­ÙMÔ´íAK%en/mlF£Ùtó¬)XÌp¨‘#ŒHÖ)ƒ^Ò\Z\"€4„¯Ê´-O#QçN¨E‡Ìõh\n‰\Z¬q¢¤h\"\"…DEPªˆªªª‹\08Ù¿ÚIOSÓ£¬ž÷×º÷¿uî½ïÝ{¢Ñó#ea;â§È]§¸väÛ·_Ô»Ö´íÚZÚlm~F¿…©Ü8eÆdë(2´ØÌ½6kO5%SSOöÕ1Ç F*·¶ó$w–ìJ°ì®zÓÐ©¯INÖÍÖc7‡Æß’	‰í<ö[Ö]ÒRÔÓ\ZŽžîôÇÏ´÷kÃWWŽ’†¯gv}>ß”½E4ÒRb²y0Vò¸[ø‚öÒsIOr×…~/Ëž94…\r¡‡Ò«å–5Ÿ¦2{Î\nzŠªîÜÛ#¼i ¥Š¦ªªh:—ub·–~š’‚–9\'ÉWÖm<nB\njp?z¦XÔ‘{„»s„¢cM`¨ù1E~Êõg¯˜èË­ôü¾þžËÚš8W«õïzëÝÿÕÞ_¶;SitÎÇÊoÍçSS6†£‰Æã1´Ï_ŸÝ;§qäé0[GfmlTDO™Ý{¿qä)±øêHýSUT %WS,9oo-ËhA€	\'È\0+“ù~Êž\0ô`H\\ž‚^±§Îl\nlÿ\0n|€¯Ã`;/»·nÃÛ­·0ÕYÜFÀÇÕ×&×êŽ˜Äf ¦3îI±]ÇWU‘É$4ôSærù\n¨Ò\n-,]i’ÖÎ=I9	¦Oìkš4«QqÜÇ\'¬[+7Q¸~Z÷~#qå1d:»®z®»ÛxöªjÌnÁíOã™ÃºóŸt<+™Ý{÷®ªqê”Ö‰1û~•˜™%pº”\"m°hSÜæ¿hÿ\00aŸéSÒžÚ7ÙÓvã“;Uó›©iéiqU›wñ‹»«ròÖBÑä°¹,ïeô.¯Sâ*Û+®\n˜Á§0CbÒùBÇèV›lì|Ýo³Ìç‚qÖÏöŸ—FçÙWé)¾6fÞì]›º¶ì£9\r³½6þWlç¨Öi)¥Ÿ›¡›X)êáežŽ­aœ´3FVHe\nèC(\"ÐJÐÍ‹Ä×ˆ¨#¢yðko<Æt÷Gte±;Ë¸«·6ùé~ë¢¦Ò”=KÐÝ‹ºzÛkABµÑÕbò‰‘Û5»rÄ²J²grïË-=a1Ü§\0Go+•b>lªßÊ´ùÒ§$ž›@rÄdô{½–tçESåf‡/è&©ºVâ~Vt&c\r2[î!®¤ÝM`€•u«S[O5Ç4ÓJ‚¶†\"Y‡FÕöôÜŸûzwù\Z´Ø3Ò½‘÷ñc+v\'zõÖ5&ð\ZŠÜ¦/¶²ËÓ-¹Cšœä¦ß´ÓÉ¯XŽ:3*©’4!«ªÜÆTPŸÈOí¥>ÊüúÛþ·©Ÿ%¶èÞ{œØ˜ó½“Ô}‰²»‹`á›/Þ›påv^EÆâÙ”{†­ãÇ`ê»¯²y­·÷Uäq¦]¾ãL%Ýu·Ì‘LRSú.(j*¥~ÃCùuçZŒqésÔ·´{¿`â;e6b^Jl¦3!…ÜØzÍ¹»¶–æÛÙ:¼êÙ{ÏmdR<†ÝÝÛKpcê(rsÅQigŒ¤ŒžêÕíe(ùGÔyòõe`Â£¡?Û\0\Z€Xõ¾‰Ö?tfþFw7û·…£ãŸGn¹ò•=‡xª›¹û£_ÞëÌx´ì¡ËÔ=NKp´Š2;––*\ZðPä%ÛBíöä³¬q€Â1“óãò¡ùá¿íú#§ÎúÅÔïîÏøíÔµÈÄËî­ÇÚ{ö’GÃ¸¢éšL6oemJ‚åâ¯¢Ÿ±sx¬µM‚j Ã0f¬É%,Yb‚êôP(>Uù`ƒöƒÇ­¾YWË¥È·‰±EM&]qV|‰éD¦‘…9ûù¨·}>Yñ\0Ôdé±ÒF\n~ç>›}ëm©–c¦§Iÿ\0åëRpoO½‰ß[ª7ïYl}ÿ\0smÐöÝfComÆ­ §^³Mÿ\0ØäÁõžáÝ´¾ØÞÛî\ZÙ_oE[O\r^j)é ©5íMKPÄ6r\\E4±tqtÎ~ÁLçxT‹–\0€|ú,oksôµ¹¿´Ú_‡ ë­)L×ìëukÑ,Ù?9>5ï?“]«ñ·ò;¦ò‡°—jb¬éêjq[®›vOj¯uâ)·NW$»S°ò¸ø–’:¬NeÈméáš<H©ƒí—+i×Ó¸SšÔR˜Í)QŸSšŠzõPãQZç£‡”Éc°˜êìÆj¾‹ˆÆRÍ]’ÊäêàÇã±ôTÑ´Õ•µõrÅKIKO–y$eDPI {Gô—D…ð\'ÐûN:Þ¥õºÎÍÚ=íñÿ\0·7FÃ¯—#°r{;°0»SÒŒu^ÞßX´Ù’¥VóÙ3-EXËíXsUTõUÃ|Øég§Yè%¥ª©SZ]À²¯\ZzÐŸ?,\ZV!‘ˆáÓ®ÈÙØnÎøÃ±¶6öI3]ëÑÛ?¸Ìñtù‹¦­¬¡’‹Ã6½^S5<’’`­VE#w4;ƒIóòñ}yER‡®ºB¶«´þ;ìø»¶\rãQ¸v]vÒÝÙµ¦ŸI¾’„ävv_tEHÐR=%÷‚…ò±ÇX<.ñè‘›º¥­÷‰À:©\\Œð®r=x×=m{Ó=!¾1nMÃ´iªþ0ö\\QïÎ–ÄQc6>~«#t}ÑÑØ¯¶ÂlžÔÄ0ñÔ®^‚™`ÄnªCÔbó±	ä¤ÈcªjœÜ#Y_ÂÕGøO§ Cä=u¤4ì<GFßÙWNuÿÖÚ#äÇÊÎ¾ÛÝõÃœ¥.îÞ7¼6WWu¯RáæªÏWMòGäG]í/mvÖmÁ•ÎlŽ‘é\r©Ü˜4¬ÜÙ#ÅÁ.ñcYòqbã1­•«-‹L\rƒq]+\\\nùÔ`q%{MXÎCSË¡Ï­><öóîÝµÜ4;Ïdw.êêš¾ë}¿×]iWÒÝ#Öû„mœ–ßÝ».ÜÜƒÙ[ƒ;ÙÌZ¾•+òY¦Çàquucé zŠª™ÓOy	…íl-ô‡4bMI¨@ýžU$ãM‚š†cÒÇ¨ðÕü¢ùcØôÍU£«¤èž°ƒ-Y›¥ÉGI“Ù{75¼3»ooã)i!l.ß¢§ìÜuc™ežZœ­ui,©q£WÂÒ\"{ÍZ”ò8$ŸZ¨ü€ëÀw¹éO‘¦x¾[í\nµ’ G]ñË²)¦ˆÏ\'ÚHø®Îê™i¤ZA ˆÔB¹™A©`²\0\Z¬f»mÂã¿ÌŸóu³ñ¯çÑö‡«uáõÿ\0cî‹ñ¯úc×¼º-ÿ\0ˆž™¦“)Fh+\'ìÞþ©Å£fZ:ŸŸQ‘Ú)ª]F6H¤aªà½ˆSu·H@5ý$ÿ\0UOÅöžŒ‡´[¢]òÛbÉ¿·ÇÁê¸2˜\Zm»ó+oï¹“D²gæØý\ròrRmì–’<kÐj{¨h#Æ;í³ý:_8@Lqÿ\0L£ü½QÅtŸK’ð7¡2²ÂÍÁ|—êŠÜ­YTû|l6ÎílmUd²K\ZAûpÐÒ£z‰ž¢5\0–öÜÀ-Êê¡*ä*OìjN)öôÃÙ[äþ×ìì¿j||ÝÛ;c7–#\r…Þ	ßÛÇxíM…„ªÛ8ÌÜMãÓÛßfmÀ¯ØŒ¥^F%Üxê­¿–£ÎÅO‘M¨…ä©ÜRÚK\nÚÜÂVPpÊ3ZŽ\"¢¸â8’ji§Ä0%”ã ã?È-…»û“1¹ö–G»{¿²y­ØxM½’Èäðeó—ã½%VÞî>µ¯š··¤“/ºö6*9¨2ïIOá¡Ú®0©SHj•ÞÚ¹´P{¤@\0%Aâqšž$±ˆaÕQ†¯—ùz2Ÿ(7­63iRu¼[’§íó49ýÝ6]vüuÓÛ~€e{›³+wbÛë·vk=c<~,æNƒ×\Zy&‰ÝHí>A8\nT’xcÏç^\"´ÏW‘¨)æz_tûëžÎé¦ì. ¦û>¨ÞlgúÖœREDØµøªY¶£GAÓý„á;	¡V	*¤Šè©oc–;™RcY\Zñã_ŸVRáÒs}<ù½‡¢ÌMG>\'n÷>úËa’vŠÎ‹Ú;(É4b&J—Çg÷í¢–Rº‹sk{~Ø¢Ù]3%|¿Þ§ì§Uo:jù+²ÓôTñTGOö_&:ryÔ\nv¨Š|µv9©¡ÔÏ#šÐL|Eknme|I†ž)þÏù:ÔœÛÐÑ¿6Èí-›¸úó²6–ßß[wâçÂî¡ºñ4YÍ½žÅTÛÍC”Åd!ž’ªJ¬º””uWRA!ž[yX\\«Nœ CÃ¢-ÿ\0\r—Ó©Røú^ðù¿ë&Æ¾	:?óKäUÓíù\"Zy6õ,Ñw•Òji\"Í$S¹†5H‚¢šêsƒzÿ\0‹Jú×…)ÇåóãÓ~õÇNû#ãgÇ\rû”ùÑÙ^‡ØnƒÙ•ÝCÕÔ½]¸6î+Ö´yí¹×ƒ±j«vžÇ¨ÅÛUôÔÃE$¹8[ïj²,ÚÒDÖö–þâ8­îDô¸~ì\rÓBÕ¯õ©ó<zÐ@Y½gÄÿ\0+ß‚¬ñ	>?ã7$\"¾šíþÀÞÝÙ{%*éj)êé4uÿ\0aomÏ±Ò–Ž¦’9!§ÿ\0·…ÖèŠI»x½¡£ó\0ûE	üÉ¯z·†¾/Yˆ¶oÇÎæÍÒIGŠ‹nuý«£wÅ•\rðý¥”û@0pÔãÆJž\Z…£Iaó\0#V]@„¶º¥¼‚¦¬ÎüÍ?ÕéÖÛ\nz_mÝ½ýÙÙx-§Œ•c;{jâöî:yÌ±œN\"e$²Æ¯‘SíÔ°¥­ktšC%Ã»ŒŸ/Êl\0\0 ÃâÄ4”¿>;ÑP³56?£ú§}^X¿†ì\\\rÓÔ,“TËU4”æ9Qäy#‘YX–Ýïú»C:Ûü=y~û:.?,ûQö§luþÞÛ_™ï\r£Õ=Ÿò7£±ÐæèeÝ·Œêœ–ÕÁü‚èLÏ€Eº«r›×®÷Ö>LDÔb¶—øÜTÕpiÇGRý¶\rvóVR bðhµ­\Z¼((I®h\nŒ·MÈ{–ƒ=öez»þWòVÿ\0@_ìÉßøtÖÿ\0F7·Þýâé«¶ýwö_ô\rZPÿ\0k£Ë+N?—¥|úsXôò¯_ÿ×Û×æ÷Iì}áÓÛ®¢›¬¶î[sö/hüTÛ½•ÅÃE´·NñÙT%ú~Ÿ%ŽÜûÿ\0ŽmÆvþm-DÌd•Öž–¡nDS·\\É­ÊB¢;(ã\'Ëó­<È.p=8ž«ÿ\0w|‘©Ü¸Í½`öNØÂm,÷Qm6ÌDÛ›%[vo/™Ý½û‡-Õ;­®Ùù}ÓKñC§²¸Ý«CWWZµYmË¤¤†&Èh‘ešÉ–¦§…4vêÆhY†¼R‰áÓdúü?çêàúfg¶WZbaÞQãbß»¦»1¿·ôŠjz|eñÞù÷WŽzx)n3hŠÈðôuR¢ÏSIA’v*¯¤F›L@øhŠü±ÃËæ=kÓÊ9é#½ä8”Ý”ñTJ»›­;ça>žXi¥5}U¾á¨«É\Zw«¤I#Ù’Æ‘,±Å3°,‘4»n¥¬n…@îýä1?ÌŠuVøÓ£-íWëÃê?ÆÞõ¬‘Šñ#¯¢“ðgr×oO‹}e¼òj=£6ð¨ì\rÛ))ç†Ÿ¹»?zfñ°UÒE\r-}>g_lu1¢¥JT	Gí~ê]«J\0~ÑŠuHþ·´=_¢áÙ5Ã\'òãfÏ	U\"b×·»ni(ªž1IQ¶6dgzxÑÒlE|Ç[ HP}å<,¤”*VBŠ,®ä¯q¢þUÔ)ö”§U,uªô§ùµ÷6ðé>ÉÃll^33ØPíªÅÖ˜üÍCRc_´6lÔûÃ¬êjjÇ¦”ã÷æP’8hã’%gV@Ê[±%n¢RHF:[æ§~co§[o„ýTîÒízŒ—QdJv&÷Åíc¶76kdÐewç¢¯Å¿ò7©ûib1pä2•jOî§Yü‚}«”…Ãÿ\0Çâ`Š¨\ZHa‰D>z¤iá_Uhq’j¤š\n…˜zŒô›Q¡ÇF\'ªþôGwmÎ»íjÊ^ÈÙÙò~ïÝ¿E²{Szí:Á\'Yüœì]á°qÝŒÂÖãq›ç·÷/ÜOFµ´íQM®¨Å¬íŽ¨©‚tw;„¶å¡Ò\n4Bœ**xxg\"„œ×…T5óèêößAuWrÉËoÍ··neÔ¦WhM¸NTâáÊÐTÇ–ÆRn,~+!AèÚ‘î\n\ZJùñ5â§5e5KBjii¥ˆº×p¹·V‰$¤,r(8úÖ•à¥qÓŒ€š‘ž©Û¡¾Tö¶Èë>²ÊRîÊŠÎ¼ ë•ìéE½¶vC¹ñ}SÓè>Hw.ù Lö_õýÛÿ\0 6~.\n=]=!#µ9cQ4\'76°Ë,‘\ZêhÕ®K•-*OÚ8Ó4(ðõf²´•¿$²yÚ-Ìw†ËÙ}s³sØ\nÊ‰vnáÚ»\Z:êÝË¿ö•‘ÁOKŽì>ËÎå¤§©Š;d°XÜLÞI\"ˆÊïÊEv±©R	b^\0Ÿ:÷“Qž®™%ÈéQò²±pÝ³wTòÁÑïŸ¹¼“ÔT›¸6~¥bi…–¡ÿ\0€–!‰à}}Óm$K(:þÅ<:¶ÿ\0‡íèÌû.nÆe<AéÎ»÷~µÐ=ÖPà£Ý½û.|DµÕ=½“s&.yæª¦ÏEÒ½;M9øæ¤¦JlÓmøh$o:\Z¦o fhãvm^¥A¦ƒO÷¶áù“éÖ‡ûÉÐÃí®·ÑHù­„¢Þ} iYO%Jö¯jô†ÀEU=$‹O¶övK/V³RWã+?Ü~WQ¦ã‘„Vñí~ØYe–U\nÛBGóüú¤œ|ú6÷æÿ\0ï¾¾ËÌ‡Ä/ó¯óêþTêŸß½·‡ÅëØÝA¹6Žã¦ëÈûk¶0;?ÁÇ¾«z†¿xm>û}ÝO›*ûÊ«›?Ù;N†¾T8ÅÌlXL®²UÅˆZ¯ã†jU­8\ZBiBucçF4\0ôÀb„¯K­&Õù7òo°ðû˜ÓRî=ÓRmìf÷ÙôU[g°öøê”?$zª«vuþì5Yj¨ù8±FW¤2TE]K9§¬JšGžõ)6•¯käWPWôò®hr3ÖÅÍz6?ì­lK[ûÏÙ–ÿ\0eÃý–}*ãÈÕ¯ûÕÿ\0\0ÿ\0ægy9þ5úÿ\0æß²Ÿ­o÷Úÿ\0¹\'çéÃ‡òùtæŸá§_ÿÐÞS¹:›cw¿Uïþ›ì¼3tlNÈÚÙm§¹p™Š²5tJf„I%,¥Uª(j4T@êÈñO:2º«nÖá­¦IG¦ú‡JŒô`ÃP#¢añƒà3ªk£ß}÷¹¶7}v­6Onæ0scz[dl°Ø9Ý™´öÇ^ímí×ûJ¢-á½(û*=‹²ñ´sg2›—+QF¢¢Bâèªg¥ÂëtÔ¦;U(…hI%˜Ô–\"¤Tù*ŸZñêŠ”Ë|]Xß²¾œè·|€ÌUm×ÇÝógøv7¾6¦ÉÝ	³Ë¸q™¾®Ä,TðÅ#H*{7sí¸årQiéÌ“1ÓuˆGu=å*?.æÿ\0ŒŽ¨ø*Þ]hz¿MÙl¶7ŠÉçs´øÜ>[–ËdjÜEKAŒÇSKY_[S!â:zJh^GoÂ©>í\n³Í ÔÕëÝ…YJ<ÇÄ_•´9WÍ¢t·^cêòRá+öÕLùŒ>Û Äg£®Û™JZ,–ßÈÒg(j!©¢¨†)é*#xC)Ôn\në{riW$yà“LõHþÑ ö—«ô[öÓRn¯”››\\lª½MÖ[¬èóW¥Vw°rÝ‘½pÏ©yãÅmünÐ¨†d+ºùã:Ù?mtšá°IÖŸÑ*n¯ÙÕ“éÑö‡«õR½ûü¾÷ÐÞ›“süNÈu6ÅÛÃ]ÚU}É×[–Úÿ\0Ã»½1»{ooï“½m¼ð8­Þ)»KA´ñ™·êðËˆÍæ¨\"®’²Ž Ô5QÌ¤M\Z ÆDÓ¦œ\n¯ ‘çZ‘œð#¢S¤àõf=m×ø.¬ØÛo¯öÔùÊÌ>Ù 4päw>s%¹·.^¦z‰ë²Y½Å¸²óÔä³yìÞRªjºÊ™œ´Õ;X\0)¸›Ç•¤ü>B€Pytàésí°<éž·Ñùrô4ý™”Þ™¹w6ga¾ÞÜgltVQ`:kka÷Þø¤ì®ÍÄ6ÙÛTX‰7VÝßÛë‹ÈUbòòÖcáþ<q}£4ÌÛs˜Æ¢øµµ*j¢ŠjI¥#9ã\\ôß†+ÇH)éèéà¥¥‚\ZZZx£§¦¥¦‰ §§‚	0C¬qG\Z(\nª\0P,²¶bÄ³¶ONtüˆ¦ÏÔtgkM´éòU»³²3»Ÿjã°ñÒË“Êî¥HÛ£oa¨â¬Šji$Ìæ0ðRéuåf6 Ø…V¢æ!%4±¡¯\'?Ë­7Âz0Yª\rÉƒÃn,TÆ£žÄã³xÊƒÐñùJ8k¨æ0TGD&ZiÕ´:«­ìÀi¤CŽ@Àõ¾ýï¯tY¾9MK\'òK<Š:œ×Éñ\rdt2G*,›3hu÷YÒ=D¨ˆæº|VÈ§’e{´R9Œ¨§ÚýÁki­~2Oùz¢gQùôf} êýnÊ«ÇîÏ]	ÖbI§ŸiC½>@n\Z#Ž·¸Ý·Šn·ÙÐe*¥+ü3)[»ûä±L¡šFÛÕ\\\r\Z½¯·\r•Äÿ\0ÅE¡¯_`ÒGçO<ÔšºŽŒÏ´[¢¯òá·Cü—Èc7>ÿ\0Û•¸¾ÉÀl½ù×{c·v>JM«Ù»{cön¯oo}£M¸ébš<ÆÒÏÐÖ\'Ãåiò–«Ž*“LgŠ9]¦ãqf4GC hxT«íô\"¦µd\rÇRz/âÎÍéûÝ}·kpï.ÕïÍÍk}nÌíuLXú\\6l÷OfìÝcí}“€Ã¦R¦ª±q´ôòfsuµ™*ÂóÔÚ?^_=ÒC\"AZšV§Ï#‚€PuäM5ÎOFÚN­×ÿÑßƒÜ/Ñ^÷î½×½û¯t÷~ÆÈöWQöÈÂÕ\n\rÁŸÚù(6ÍsUÍAé¥‹ø†×«ž²ž)ª©)©óô”Í,‘)•#RÍcíû9V+„wNý_>a=UÅTÔÞ ìJ>Ùêý‰ØôTïCýîÛxì¦C1O¼Ûùã§Ü›_\'\Z<‚›3µ·XêØK‚®–HÛÔ§Þ¯\"h®%Võ$#ž#ä|ºÚš¨=:ö.Û©Þ¾¶}D•›¯gnm·IUT©iês˜ZÜdÕ,i$†ž)jƒHYŠ`Oëján!và‡ýUùuæt\0ü“=]ñ#¢·&é¡Ç¿¶\\}¥šÁ-m~F-·‘í|–C²\'ÚôÕÙ:j\ZÚº]°Û§ø|RIMJdJ`ÞoâU“+]Ê¨p½¿nžÚþtê±ü#£K__C‹¡®Éäë)±ØÌu%M~G![<TÔt44pµE]e]LÌ°ÓÒÒÁ<Žä*\"’HÚ8Ñ¤p‹ÄŸõ~Î®M3Ñvø³ŽÈÔõÞW³sQUÓå{ß{îNç\\}}ÍnkîÇ¥¦ëŒ%V¹j$†·Ö˜œ@«ƒË$Tõí<pŸ\nÆíÆE×¸ôÅ)Ÿ:Fµ+ò?ŸTŒb¾g£-íWëÞý×º÷¿uî½ï@P×º÷½õî½ïÝ{¬rÅñIÈ$†hÞ)Ü‰#‘JH†Ö6e${ºWVuªƒŸŸ^è¹üS‡3ˆéÜ~ÄÜY*Ü®wª7Fûêšºü¥p­ËÖâö6ðÌâ6fO&Z8ç¦›=°‘†9CIö•¿’Uu•Õî+ªãÄEe£$TÓËÓQ>ÑQþÂÿ\0ë{B€	WÃQ_³«žªÝþ_8™ñ™–MG­Åc$ùœ¤Þ¿ÄkêkçÜ!1ëSso*Ykp˜‰$Åç±²m¨ –œ¶>V£“ÁL²´Ç{¹a°”ÖZcä¹ÁÏ\ZùùúštÔ|Zœ:²/d;ÑeêšJÍÓÞ? ûN­™ñ4ÛK£6(’Ž¦˜œG[ãê·øÊA%LTïQweoœ†-ˆWŒ¶\0<nÉ\'µ÷L\"³¶¶§êeŽkJž¿3ëÕ,Æ½½¯h:¿^÷î½×½û¯uï~ëÝÿÒßƒÜ/Ñ^÷î½×½û¯u×¶×PøºßE‡ã~3´ó?$:ïf8]›ò#rdñÉ=_ÜÉM/ol­…ß9ú4NŠŽ—uv•zÓ@\0ÑM ó{“+æ2F*hÆÈ•ýåGç^¨¸Õöô;á÷ŽÒÜËŸM±ºöÞá—läªð[pyÜVXíìí~J¼>tPÕTÿ\0ÊÒ+†–ž G,`‚Ê´ž±”Õ…GÌuºƒÀô\\þ\Zo<oÂï‹û²¿9I2£¡z•*3YŠZý¥K-d{7Ž¨‘¨÷M6\'!CNF‰ãC*•dÔ¬¤¨½‰ÍýÒ…ÏˆÞcÔüúÒ‘¡såÒ³äí4û—©+zï”¥ÆÖ÷.{ju<u²TÐÇ\'ð\rï›££ßsbÒ»\'ŠJì¥Z&f®–ä’Id€¢èÛ±VŽs1BD`Ÿ>4ªŽ	 ùƒé×˜)ëÐýIGI¤¥  ¥§¡ ¢¦‚ŽŠŠŽé©hé)bX)©i©¡TŠžššTDP\0\0ÚG¬ŽÎ~\"Ô?.­Ã©V÷Mé×º÷½Ðzuî½ï}{¯{÷^ëÞý×º÷¿uî½ïÝ{¢Ó†¦}‰ò“saè•Î¾úÞ£³§‚8œAßý=]±zçrä§‘^*dŸwlÍë¶¢ŒxäžOà\"ÄžÌÖkþ8ØÉµOØÄüÏTàÿ\0#Ñ–ö_Õú+Ÿ¢¦Nì¬¥ªû¨²?)~CÊÀÁ=;SÔãû%­¥ežIž*Ü<šdM1º@›šî†²Æ´à€ ËÓqð?oC—an‰vFÀßÒžrÓíŸ¹·<Ç¬‡¹)°ZÜ¬X÷ÈT«ÓÐŠÙ)FiHµj`@>ÐA\ZÉ4Q–¥Xf•þ]\\šzcéÝ›ýÃë=Ÿ·&ji²°âS)¹«¨Ñã§ËïÃ,»ƒyfãŽG‘ã\\ÖèÉÕÕ,t	BŽ{ÝÛøÓ3-tŒ\núòû:ò „Ï§´ŸÎ½[]ûs­uï~ëÝ{ßº÷_ÿÓßƒÜ/Ñ^÷î½×½û¯uï~ëÝ>»ÅE›ïo˜G>J[/Pll¬ôóŠG‡xÑuBg3ø¤HQ–©6fûÛË÷„ÈÏ$\"+LLå”Gk·ƒBâ­óÓ‘ÇÖµû: i=:+ýñ‘~mçîžÅÝ]1E‰ÙK²#›¨:‘zºöWTÒîËMÙýç÷;‡wÔvuæêr3G-Z4ttþZ“M>FeU—ÿ\0½ÚÞ8¨Á_!ÄÓ<XÒ¼U¢¬OXp½%¹~Aü\røH´ý}Ó»ûwl·éìþw§>Tm¬´½o—Ê¯LÔuÎöÚÛç>ÍÏg6žöÙ5{‚±á3`f¨£Éãä¢žš4’ÓÖ;”³ÜïL¤ˆÝÚ…}5TCiŒùÖ¸§Z¦¤R½	mÔùN˜ëßå÷Óe¶²”ýyÜûW”©Ùô5_fmê]¿ÒýÉ&>ŸhíÝá»·Nâ¡Û8ÚÁOÆPÿ\0ÊWAH±†vHäu¢Ü‰Úò@\Z…|òpâ„Ð\"|¿È:±Z^¬Ù/Nuï~ëÝ{ßº÷^÷î½×½û¯uï~ëÝ{ßº÷^÷î½ÑiìÜf/äwÅÿ\0²Ê=:î(;Ëjå±¬h²˜fÙXß÷e`¦–•\'ÅçvmI\'’%	Rê¥Â“;TW´¼.µ ­}\nƒOÛSû:m:2ÞËB’@ONtMþo]¥¾v/tWmö?O…ùòïlf&ÀÖRÖCC¸0½ù½á¯ÇWµ%MJÅ“†\'ŠIŠ³$©&®¾Í7eu”€È¤|ûW#¦ãà~Þ„?””9lçKn¡ƒ˜Ód»9°úî:¦¥ª«¦¦¤ß[ómmŒ¬Ùi ¨œãF%P*\"\"u[°go*·\ZˆÈ´w?e?>¶ÿ\0	èÁª$j©\ZªFŠ@UDQdUÀ*`=¢r…Ø¯Ã\\}ž]_®^ë×º÷ºTqëÝ{Ýú÷^÷î½×ÿÔßƒÜ/Ñ^÷î½×½û¯uïtÓR7^è±|P§‹)ÖùžÔAU«¿;ywE,Õ“PUÏSµ·MlXî¶¨Ž¿,ÐÖãfêüèKi–*&Š)2 Çq`fH…*‹Bpk•üš§óê‘Ö„Ÿ>‘ß:12fú—`b²1á*:þ»åÄúnØ¡Íø¼y…7ÈN¼ˆPBÕU4Øÿ\0·¨Þo‡þ&•%âŸ+¢ï\"nm\ZK)bÞ/†úiþ‘«_ò|úôœxW¡ã~^®ï¤ÈPÉAÃüí¬~ØV®ÆÕÁS†“#ÊÖVRAŒ§1ÔnŒ¦H¼jLþIe}Ré]îJŠÐéþŸ–*Rj>íëÉD¦¢‡¢·ux‹ø~Ëù\'Ôõr»0’	wíN_¦0ÒS(š’I7gÑFá‹…Ý‚³ªƒKEIæc\'ò^óÿ\0ëoø~ÞŒ¯²þ¯×½û¯uï~ëÝ{ßº÷^÷î½×v÷m\ré×ºõ½ûCzuî½o~ÐÞ{ šEÜ¿&rR*j¦ê^ŸƒDU®øçqîŠ|ÆbŠ¿\ZÃÆóã°M‰–’¡lR<LdvÚ/Èq«µOûQŠ}¡ìê€êo³¥gyn,ÆÏéNáÝÛ{_ñý­Õ…¸ð~:sY\'ñŒ&ÑËä±ž:EhÚ©þö•-e.}7öÕœk%Ìüg­±¢“ÑhøË·±¹û[®úÛ… ê<wJüfÜ©-!©jîÅËb7ÞÚÈfrsŠÙ1ùÚ¼ï^ìÍ½%NOíÒ²®¢ò‰§ÓvàÒËn’N”“U0\0Åò­XŸžxñ¤`!xt1ü®ÄTe~<vœ´U‰Ž®Û;y;†½’¡Þ’¯­²t´ÿ\0iþTµe¶Ö˜š?ZÈÀhöâÒ©Ôý¼•z³ü\'¡ß“£Íâñ¹œt«>?-AG“¡2MG_OU4¨ÊJ²ÉªÀ‚AÚYª%’«F©Ç§Ë«§ûo­õï~ëÝ{ÝYu}½{¯{·^ëÿÕßƒÜ/Ñ^÷î½×½û¯u\Z¦UO=+I4\"¦	iÚZy^\nˆ„Ñ˜ÌOY!™]]H*ÀÏ½GRëEîÕ×º.ÿ\0kà­ø·Ð”ÑCOKQ¶:Ãjõþf†’	é¨ñ»£­1ÑõæîÅÑCTïP”x­Ñ¶+)¢ÖÌZ8¹½Êëô)w;‡bÃæ	4=U\rTuÏåþÚ¦Ý¿ûûU*Ãêå—<QÌ«>ÚÃÔîJK¤¤*Þ«ƒ_Ö;êïÛ}~®0ògü”ëOðž±|C‡)7Çn´Ý™üíÏÚØÊþòÝ{jy\"©«Û;›¼óY.ÛÌí:Ü„IÊÔìú½äq?tULéB¯¥A\n-¹=nš1•Œø´à7æ)×£økë×,é3Ù‘ÉâöµµÛ—-¿úC’*x*¤Âerã×T[ÀCQ\rBØ³TÓ2¯’4 /#ªºûo(·¥ø5~}§Ÿ¼õ¦=z2gê[{Däb¢ŠI§Wë¯uëÝ{ßº÷^÷î½×~î	cCÃ­uß·z÷^÷î½×½û¯t]ºÓì¿ÓçÉ\ZD2_qÓ†®D¬Y¥z°çá5 ™ÞŒ$â«MÑ€ê\Z¹!|ú¾†Þ¼*?ËOåÓcãn‡úÚHëèêèe,\"­¦ž’R¶Ô#©‰â® Ë¨+›\\ì¾0^DPrH·§:¯åu&ñÜ°Øs½VðÞÙÜÎæha†îHÊŸ6UQ×dš’víž«‹sÏNge‹+¸+J¬aôMá¿ÆUŸ•ûk“éÀ’£ä:n1‚|ú3¿)ª*©~3ü„|}}>/)7Jö}\Zºª« §Îd¶fgƒg£M/Z_/S\n¬*uLÌrÃÚM½iyÓQ«ýòõgøOB–ÊÃ¹³vŽÞÒ«ül`0Ú#§‚ø^*’‡JÒRÅ-*¯ÛØG\Z$h=* \0=¤™üI]Ïz°\0t§ö×[ëÞý×º÷¿uî½ïÝ{¯ÿÖßƒÜ/Ñ^÷î½×½û¯uï~ëÝ?Ž4çhïo“ýWãhhv¿xUoí®“U™ªj6·ym|fÖ×%6³?håwN6’4TC4µ‹–$Òú²Ek>‘¥O\"1OÉ@éµÃ0éUò¢¯7ÇîËÃm‰h©wFýÄQu&×Èdâ¨¨Æb77sfñ]Q·s™:JFJÊì~3¼à¬žžYªb¢FWpCEÒ3\0ÄýI?+O+Ž¶ÿ\0	ê?Ä¬öpüoê·YÇIºp[R‰¹«°ÔŸÃð¹ÅÖµÕ½wÜXLQydÃá7.WkË£¢‘ÞZ*j”‚Fg˜ïqˆ-Ô„Ö£>GÔúŸ^¼†ª:Jwt-¿{çãVÐÖÆiî½Åò;|ÑÄÌ›[¯6ÎWeíjØ«ÒÀ™ŽÌìL|ÔÑÊµ+ˆª’ŸšIš7ìÿ\0FÎêb(\r\0ÇåO—Ä­öŠõ¦Ë(=_e]9×½û¯uï~ëÝ{ßº÷^÷î½×/j:×^÷î½×½û¯t]¢£nü¬ldqQÿ\0\rí~ˆ­Ü\Zzxb­ƒ9Ò{ë„¬©ÉT´þzèó~í Š•R=4Ã6¶¼Ñj‰I,²N¤zqÁÔ\r)öþ}Wƒý½\r[³uíÍ‰µ÷õÞœ~ÞÚ›O“Ü{“;•©Š‡Âa¨æÈdòUÕSºEOKGG»³\0ÓÀ$ÑFªX–O[&€žŠÇÁœÞV££åÙ›¢‡‚Þ]Ø]‡³·.ÓÀµkµ(2[ž·°z×“Åã©é`Áeëºs|mÜ…E‚`zËk”~ã+ÜTx©\"±1²ŠZ\n7ükWZNóéaò¢’£sl-¯ÕÔÐÁ‘îÕë­ñ\n1]`±ÙØû°\Z(¬}OÖ{5-+JB\n¤ŒrÅT×o!$’F:bT5üð?ãZzÓäSÏ£.yçýhd>#´”âIêã®½µÖú÷¿uî½ïÝ{¯[Ýt/§^ëÿ×ßƒÜ/Ñ^÷î½×½û¯uï~ëÝLm!Û_/·Tæ\n‘ntÐ¨Ž²z˜~Ñò]\'¾·UU2”i›îÜ°MVçRéðnnaPÛjyY+O™À±IêŸèŸ—O!2X•¡ê\r«•X™·×ÈN ÆâÖx¡–#–Ù„÷=1d]D©WHÐ²i8FR=a&i¼‘iþö\nðõç<¯X~+ÓÒQô¾6‚…™©qÛóºñ±3\"ÆOðÞîìJô\'¥TIN@ño~ÜX¼Ñ±âc_ðu¨þ™ú¶™·Èÿ\0’ý€íOQKƒ©ú+4RQ¼Ðÿ\0s6æK³·\r4‚–i¤1¯r q¢Hä@UYïpU,m„Iÿ\0!þjûOŸ[>²Î¯×½û¯uï~ëÝ{ßº÷^÷î½×~ì­§ìëÝwíîµ×½û¯tP~Aæp½yÜ\Z{wrfWlm}½?ul½á¹*ä¦ƒI¶7?YTïG£ÌO,‹R´óçú³S…dc% ,ºAe2±A-½ÔAjÄ>Þÿ\0=6øe=wó¶\\eoÃ¾ö¡¬ÄmÏŒÝûl±Aºi1™}³P;+‹Ù”y*ª£ŒVOøTÛ…+i¢•Ò9ª u­ÃöÕ\"íX³.Š61Z0Ç[“áêÅM£.Þì›9|»aò;Ÿs|¯¬¨­ÜX6+3Yµèú3¤?¸öOCK]¸[hmšÈèég®’yãƒöÕôQmÃJÇi\Z !õâ…iÀf§ó=i2Xùôò7¬‡òß´6ÆrivÏEu–æÍöE%!ž8$ßý“›ÇmÍƒ‡ÉE<IßÃööÒÜUI$wÒîª†‘}ûÃHlY¤ÄfÇ®0eT˜?—ƒø8èÙ{/éÎ¸û`‚0zß^÷®½×½û¯uï~ëÝÿÐßƒÜ/Ñ^÷î½×½û¯uï~ëÝ¿\rEµ»ã\'iÏ÷‘Ííçë„Ô,ÀÔmÎôÛ¹-š¸ÚàÕ0S\nÿ\0H´»c!3H²ØÔÐºÈöaaªX®­ª4¨¯“ä ×ª>¥Ÿq¥#gzêé£¨1w–1éP§ìë]vdqU!b4É¢FŒr|–_z±Ôc»Éà¿ñî¶ßþ}%>*gk¯÷ÍlqTA·hû÷ä>;kÕÕºøkð¾àÝô³ä¨™ÏÜ®9³±W$fY’¿´c>Ý½‰¶ª[Š%~Í+Ÿðþ`õ¥üoL¿	)qµŽß˜ÖIGtïîÝïjº”ŽZ‰û´wnõ¤Xª\"Põøü~+KIC<¬ò½<\0›*]Ì¸¹Òìu*ö4ŸÚEOÛ×£øz6ÞËz¿^÷î½×½û¯uï~ëÝ{ßº÷^÷î½×½ìR¹á×ºåíþµÑQù•€>›‚ig8Ž¼Þ›G|oe5µ•ƒ­ikßovÎÜ¤†	©çþ#˜êÝÉ™Š£f+ýj·n,²!q*]?ñ¢:¤‚¢¾ÎÜìmÏOñçãwNf6ínâ·å—Y|<í­Ñ)VÚcvU;°dÆÒKGEØ1uö>x\0‘ã¢þðSFÆG¿³8-	º¸“Ä£êÌÓ¶¾z|þÎ›\'°ŸB/\\÷>gk|°þc;o!‹™ºÏ§¶çH÷Ume6Q]˜Ýûë§\r.cCaäf Úý=Aha†hÒJÔýó!xafHkmº_ÆÌÕôËT|µTŒúÃ=YN–qÒ§à†ÙÍ&Ýí¾ÈÜÓÔ×g7×gVíåÉ×š…ÈÕÑõM;#qŠúZ¹ê§ÄÖ7rA»ê\'£GûhjjehE ºmÁÒF¼\0­<…rûTþ×‰ëqŽ\'£çì»§:ëñí£ð·¯yõ×ºu¾½ïÝ{¯{÷^ëÿÑßƒÜ/Ñ^÷î½×½û¯uï~ëÝš97Ú}·hê)©q{Ëå§ÆÍ¿ºe–	\'¬8;#¹…>\'Æ’šzúÌÞÝ¢…¦(V*i&bRÚÔËhDi&-_€÷ Ê‘4ùÓ¦åà>Þ¸üñßî·øýùÉæ(vüo½¾*ä¿äL1Ñc¢ä÷Qzª™Þ(©)*p­UÎ]/®—:ôµvÑ#ÝQ$«à|Ñ¿ËÖä )écÒ1Plˆ=^ÛáÃ¡Û?ö¾O}¾qÀ}¬Ô»“)¼«sóKWŽŸî§®™¬òJ²;LO½\\–þmÌdm?1SJ|©Ã­ŠiáNŸ>+UÑ×üeø÷[JÄTô·YK„|d³ÍªÀ6ÎÃÿ\0ÈQš¨ ªŽŸ%‡ðT,r¨–!.‡%”“­À¼œ5k¨ÿ\0±ûEçÖ“ážÐõ~½ïÝ{¯{÷^ëÞý×º÷¿uî½ïÝ{¯{÷^ëÞý×º/.38=¿ñoäFcrcëóŠ—ì‰gÂâpÕ›‹5œ©m©”‹ƒÀmìtsd³û‹5“’\Z\\}\r2=Mel±C™A_·w^B gWüU}iRp8œuGøOD°>#nýçÓ{+¿ûkäÊ<Wsa÷§H÷Ô[Úµ[{«v6îÛÛ·bäð}{?SÁM6ÍÍãpú$Æe¾:©²y«ÈkŠ¢tðœÃyI%´.§¼P“_Å]5>DPã P¶ÊiRsÐ¸>2uËìßhvžôÍ÷\'Zv~µ÷ŸOVg¾?|ïNyö¿Fv^è ë¸÷FÙÙ›û³³¹:Ü`©«9]@©¥É4V4Î‹í¹¦6S­¸‰Zq©PÒ _.G ç¯Ô	®zSÿ\0,J¬Ðø¶ð¿så7bmNËïŒ_eçó{wum<¦Wzf»¯~oº¬ÅNÜÞ{chçð±n<Fð¢ÊSÒKFV––º8Vz‘žD{²¨VE¤e€áPMiÂ¿.¯öõ`žËú¿]l¿Äzß]{¯^ëÞý×º÷¿uî¿ÿÒßƒÜ/Ñ^÷î½×½û¯uï~ëÝ¼ö*£±þ`m\n,‹\nÍŸñ×«¦ìZLj¼jÅÛµûƒbíœöB—É6Clu¦ÝÜtøò©ã‰7K92x\n!m¯\"\ZI#i­3ACJúµzlæ@<‡I¯›Onö§Uìž†ÞqM>ÅïîâÙm¾© ¦Îë¶•->s±3™­½”Ädv|ðì0Ðå<â:FK²Kq›ÛÅã\\¢þ¤kŽ©¯\Z×ù½&t¯‘èwèÅ’ì>‚éÝÕ»\nì®öêMƒÜÂÍO[Ü[C]–Ö¥¥Ia©¨«’à–ê}§ºý+ÙÂŒ,„~ÃLÿ\0—«/Â>Î¿iÆÊÚ»›¡$z_7D›m­vZ¶F¯ªªñô›“¨ëæ‘DU)\rËÊÃ…&h¢-Y…©Ð\Z%GgïÁÇv>	qLæµòÉ©*uTÅWÓ£9ì·§:÷¿uî½ïÝ{¯{÷^ëÞý×º÷¿uî½ïÝ{¯{÷^è¡ü…›-Ø}•Ðý¶éh+é¦ßû_½ûŠª®iÌ[c«úgqRnÍ $‚Žª–FÌo®çÃai1±LÆ\ZŠLnVCÉG4~Íl•\"·¸¹–¢ªU~u\Z~UÏ‘Ó^5\r½IP§ /å&Ûù\r»÷¦\'wnšÃ|rêîøÅ”Ù{®7®GûõÛ_ôß´#ìËîØ©±ØìRu?_íº øŸ^Gø¬«S’Êä¥ÇSDý›ZÄšc!§dz’0“@>lh§Ä“§kü=3t~Àî|olÔ|³êºJLÆÕùAÜ[ê›äwZg7æ{ŒŸ®öîXõ·Ç¿Ýk‚ËSä6Þ³vŸXìLF?sâhF2›uaªESÍQ”ÆÒ\n·n^ÔÂö’ÔZ¡Á<*TœÜ[O\0EP5u¶ÅËVí/ýµÖY(h×¿ðØŽöØ”²Êõ5+>®ûG\r•§xÛÏÎÐak ŸË¢ªÿ\0‰#V£•ãÇ´ŠáI%pÕùš×çRHõsÕ×Ã£\'ì³§:÷¿uî½ïÝ{¯{÷^ëÞý×ºÿÓßƒÜ/Ñ^÷î½×½û¯uÐöÚV™øzßUññ÷¼¶¦uü§íƒhojÞíîÝn~|•&nuì.­Îlž¢Û»™?»ø÷¦Ùxz4©ÅíœV!3f2Y,umBÇ#Ë\'³ÛÛ9¤X„=ÊŠ½§Îµò©4<(@\'¦Q‚Ö¼OB\'É½å8n Ý›{3M‘Ú»K½sõ}6ì33wºë©»Êmí‚§A#1Íã3ÛtRMM‘T­LNä*¬¢tŽñ–‰JÔq?ìƒþ¶Ç(AÇJ®ŽÝ[O¬¾6ô.?}n¯µê1=/Ó5±×ç±ÑC¹LNÒÙ¸³²VÌf£Èn¬­.>žpòE5UDq¬ŒÎ·nâ	g»ºdBT»Ê¤Ôúcx\Zu°BªÔôõ÷uì]Ñó›tl…†Ý_Ä7/ÆªñØÙŒ®9ƒÂ²lžÊ“dõ½}\r=}ÿ\0Èd2ûÃ\r‘ZÆ§ÉÅ6ÙŠ4¦jtiý¨š†Ú¦g¯@<øf¿`Ò1ŸZõ W_o§GÛÙ?Nuï~ëÝ{ßº÷^÷î½×½û¯uï~ëÝ{ßº÷^÷î½ÕPæ>lìþ’þb}µÕ?!\'ÙÝ_±ww[ôŽ¥»;uîM³‰¬Êdé(·ÆãÜôÙºIiirØ^®Èfwðü^á®¬|BnŠ*ÌdÆŽª³\Z¹Ð5ÎÝnÐd$fžBž€ÔöNgV—5áÐÇòƒäOÇ}ãÕ›“«6ßxtÆíìç›ë­§·öµ6~Sxe2û—°¶­8PmÌ&O+¸*d¦YañÒH\nw‘ôÄŽêÕ•Ü4²BB>‡çÀ\Zù“Ï­»)Zž‹¯Kÿ\04?…!Ó½ÔýóÝuWbuÈ}½1›³­û›‰ýE’¥êÜ¾K¸3=yG‡ÜXœžjŠ˜ÐÏCQQh®€@dò¥Ü¸Ún®gi PÉ!Ô2	î%€ A§­)šÒidP(xŽ•3òtü¨þ`\r–ë}³¹v_I|xèmý²û2“¸º¸:«µ³û÷¶÷/Uî®±ÈíÚþÆÚX\'ú<Ìm¥Y_4sÉ—wRÙ*Z4û¸ôö«e·”ÖWe\"Œ Vô\'Í‡•<zðmoŽ«]öAÓÝ{ßº÷^÷î½×½û¯uëû®µõëÝÿÔßƒÜ/Ñ^÷î½×½û¯uï~ëÝPçÍ?öW¿ÒNóþãÿ\0³¿ýïÿ\0fwâ—úzÿ\0d{ý}‡úqþ%šÿ\0Aÿ\0ßôßþGö¿ÞñßîO«øçØÿ\0rz½‰ì¿xx+ã}6/Oº©C«†iJ×V=qN™mÅkòéuñ3ý–Ïö`ðÞ¯ö}ÒñÎÖÿ\0eçý¯ã?èçûÍáÝ_éçýÿ\0ÜÏô‹¯øïñïWûúþ×ï¾ÃýÆy=êüßøêX)Q¯I:«QMU\0úG…{éÖ“N¡Jüº,ý¡K‘—=µäéÜçó( Øëñ[n¸¤ëÍ«ü´rÒKñÅ;l¶Û]™‘ïÝã…íè7\rF_ø\Zá[1K&ä„ý³ZZÏ*{[“ÁŸÆ[}Y×S\'‹H§ÇñiÅ+øz¡ÓQJÓòèñÿ\0,¯ôAüWä—÷;ýô‘ýþïïû;À½ßÃÿ\0¾]—oôqýÌÿ\0~w÷ý*ÿ\0|¾ãGû–þ3÷_{û?aì³{úªÇõ\r*~\nÖ´Õ«»U4Ö¿/:ôäzs¦¿ŸV½ì?ÓÝ{ßº÷^÷î½×½û¯uï~ëÝ{ßº÷^÷î½×½û¯uS_Ín>—¬±‘v%WvPï	6gfG-oBPuW1GñùÆÒ^õªîì_|d°ÝQ‘ø³iÛo¼)ó”¾iRMQGû/ÔTøz<*ŠêÕÆ½´ÓÝªµÑåª½5%?Ûu^01ý÷ñv§Û·|cæ—h¦ÄÂ|Sø÷ñ®wDæýCÜ¢j­éº:«äçid¡èê]¬¹#,;É>EñÃ\'4*•EÏ.ŽáàÝ\r«Œ³yê_„i\n¶Ž:xŠ0º*µéßäGû-?é{t}çü=/ú=ûíÑýÐÿ\0eüä?Ùzûô‰ƒÿ\0KèÏìÇúTþâÿ\0¤ÿ\0²óÿ\0q>÷gü‹Çí<&ûÁRÚ×N5ªÔâ4Óâ¥<ë«º½\\è©ø«ÕÐ|#ÿ\0eGýÃþÉÎŸôWýèÌû¿ô‡ýêþý}¦/ø÷÷ïý-ÆMþø}‡Ùyÿ\0ÿ\0–}¿‚ßµãöÜþ³Ä_«ãAJpáååöÓ¶µÓÓÉ¦½ße_¯{÷^ëÞý×º÷¿uî½ïÝ{¯ÿ','\0','2012-10-31 20:49:39',NULL,'system',NULL,1),(2,'PhysiotherapySpine','.jpg',250,250,'ÿØÿà\0JFIF\0\0\0\0\0\0ÿþ\0;CREATOR: gd-jpeg v1.0 (using IJG JPEG v62), quality = 70\nÿÛ\0C\0\n\n\n\n\r\r#%$\"\"!&+7/&)4)!\"0A149;>>>%.DIC<H7=>;ÿÛ\0C\n\r;(\"(;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;ÿÀ\03\"\0ÿÄ\0\0\0\0\0\0\0\0\0\0\0	\nÿÄ\0µ\0\0\0}\0!1AQa\"q2‘¡#B±ÁRÑð$3br‚	\n\Z%&\'()*456789:CDEFGHIJSTUVWXYZcdefghijstuvwxyzƒ„…†‡ˆ‰Š’“”•–—˜™š¢£¤¥¦§¨©ª²³´µ¶·¸¹ºÂÃÄÅÆÇÈÉÊÒÓÔÕÖ×ØÙÚáâãäåæçèéêñòóôõö÷øùúÿÄ\0\0\0\0\0\0\0\0	\nÿÄ\0µ\0\0w\0!1AQaq\"2B‘¡±Á	#3RðbrÑ\n$4á%ñ\Z&\'()*56789:CDEFGHIJSTUVWXYZcdefghijstuvwxyz‚ƒ„…†‡ˆ‰Š’“”•–—˜™š¢£¤¥¦§¨©ª²³´µ¶·¸¹ºÂÃÄÅÆÇÈÉÊÒÓÔÕÖ×ØÙÚâãäåæçèéêòóôõö÷øùúÿÚ\0\0\0?\0öj(¢€\n¡y«Ãit-D7”R‹mRHžƒ$ýaÅâE¢”^]éz|Ñ¹I `ï4G°	‘¼FàçŠ›@™—\\™n ½ûUÝªÊ\'ºe‘Œykþ¯—$ù9ä\Z\0Ô´×,n¤xÚÖæ1—·¹$QëŽãÜdTÖú¶wj÷v÷ÖòÛÇò¬ ªcÔöâ¨x‚âÁ„vYÃ{:9·¶‘Q‰ÀäüÜúš„¶‹&»7š|Bâtÿ\0E’k@»¶@\'ž3;ã¥\0\\\"ÓwF\ZIÐI¯%¬¨‡\'\0î+€	#œÖ¥dx‚{y-—I–Þk‰5tHá*­€2[scßŸo¬]YØÉ ¾³¼‚Øˆå{é\r¼ð·÷dX3tÆ\0ÎxÏZ\0éèªÚuÓßið]Inöï*n1?UüÀ?˜AVh\0¢Š(\0¢Š(\0¢Š(\0®hxÞÌ7˜úv¢-%m–—)lÒ-Ów\n«–7	¥©ÜËªjQ/ˆm´h,V8Õ¤XÛs²ï%ƒà\\cú×7i¨G¥Zf&»qx—EåÇ`À—\r¹ž6w½G\n\0uéã˜ËM«ÛÚº¯\rÓy2¡ÆpÈØ#ò«±øC–Þ[ˆµ›	\"€fWK”`ŸRÍh)ðL#|w¾MÝ×2K¨©YæíË0äv\0p;Qøƒ_ø(‚K«˜Ä/›i¬áf‘|¬ªG_^3@“xßÃÈªu$xøß<j^(‰€îœ	Ï¼¬C)GC^aq¨½ö‹y¥Úê’Ù[ÈY.î®ôçVfeç™	L•#9ŒŠéì5mFÖëI‚Yô»Í:ôµ¼sX+(Õ(Îæ„vÇÔÑE\0QE\0QE\0QEWPÔmt»_´ÞHcr ÚŒå™Ž\0\n ’IìRÿ\0„¯Bò!•u8\\N3\Z&YÛ·Ü7^:qU¼X×ÚÛéÖÞ}üò	`%Â,^[+\'ž‡9ÏÖ¨Ùj\ZÖ—s¨M©xZ4ñI¦(ÊÜü¯˜žŸ1½(£°Õ,õ1)´˜¹…¶HŒŒŽ‡ÁV\0Ž=ªÝqšlž$a&§ŽþxT\\=üŸg\\‘h¡ˆPXüÍÉþVô¦Ô Ö­ìN»6 ÑF´TËWÛÑ(mÛˆ;rp½q‘@EQ@Q@Q@Q@5­»\\­ËAÕ” Ü£Ð¸®W\\’öãÄ0§‡5†öu6×2¼&hc†`\r–nãÜW_\\Š\ZÖ]V].\04§’hÍÚß-³<-ÌŒðÃóÍ\0lèžKh..õ=bMbêiVY®8E>êª¯@98Ï\\úš“RÑl<D·/½µØØÊç?t2œdöÁÈàÖm¦-nž÷E¼Wµ–iuoÅÕXlP¥‡#žÍ×Žd¼ÓuÝjÚâÆX\ZÅo<µžõ¥Mâ59Ú¨¥\'rqóa@Bjú6±¥Þê÷kàÉKm·ÙÁBK•@K´/9êk©µM\'VK}^{{‚ê†„o¶	ÄêZch×°_,ŸÚZRG ¾‚Õã·Fq´Fh@v\' üW Y”6P­ÍºÔ¬,LctÐŽ˜ 	¨¢Š\0(¢Š\0(¢Š\0(¢Š\0­&Ÿc-ÏÚd³·yðšÑ)n=ñšáåßÄÛ\\´Ìé–Ö¶_c’FtG²åNàÌxJô*áeÕoí¾!«_Á7·[ÃkfÎ^=ñ•mù “Æx\0t=hÖ­w	½°”é÷Hír»#‘cÜB)uDPr	e^[•7@½´h¯ž=î@—2Å)ŽýàÝ¼RFYK<u¤Ô/%Ó[Oh—‘Ç-Æe”ìyelT$1åŸ±\0\0sŠ]â;Ë§›A»eK©6\\A#\rù`îã;r3÷xÈ ßjv|@½·ÑÑ~Ï|±‰àòäàtG%Ê‘Žrªz»]røGME5Óm’î2JÊ‘… Fxï‚Fz×9 ê—7^8Õ~ÄžuŒóíœË‘=¹Ž$QË6[#zž•ÚPEPEPEPEP3âØ~×s§[-ãØ4^mÛÝÆÄ2EPëÁ†Þ¹ç·Ò±´½Nñ+õÌªÉ¹ä—MŒ8SÈýë©<ƒ‘‚OÒ´üsc&§ö”Z“,Kx–I\nü±ðxWëÐwÁª6zíÕÔ«äÝÑa²Ûh­e+ÛÈwãG“@[xcó-ôÿ\0ë	oì’¸/´à˜ ô4Ëí\'ÃöštñhÓZ\\k–&Œ´Èn7¡ËÆ1 \09äSe´¹Öm5ëÛ‹£¡Ý«Åæé‘‘A%ˆ mpÀ×n^+_FM?S¶—G¾Ðì¢6)a]³ÂUÁ*ÈÄ¸=@=ûÐýµÄwv°ÜÂsÈ²!Æ2È©i¨Šˆ*¨À\0`N Š( Š( Š( ¹ßkmorš\\3-£8æº’TdEˆm›¾óáO@q‘ê+¢¯=ñF­nuÖµ¼´½ÖcŽu’ÚÒÞ(Ú\"ëFòÀ‚Çnq‘œb€4¦Ñoaš_@Ôšöñ×eÔò:3OAŽŽ£¡Á<óR]Xø‹XD°»-”ä½Ê‘“ñ*”v9=;u¬¸tö¶¾‚+T‹Ã7‚Û‹Q>èn#Îw\0eÕ˜‚\'æÆ§šÒëhÖüEØ!ùçŽ²Þr÷6íN8äžè\06ÑøoÄVæh­õdŒ q\rºÏŒê±œ1àœ‘×=»÷5åW‡KÓoÖTðî³¦ß$‚kk§Oµ9Qv€ÌBüƒ®6œô¯HÑÚñô{GÔ&†k¦ˆdƒî1=Ç·½\0]¢Š(\0¢Š(\0¢Š(\0¢Š(\0®oí(¾#ù—qÞ]ÜÅf¶l«ÜmY2Üm9$ØÈÁ\0W{#¤Q´’0T@Y˜ô\0W›èÚ¤mâû¶× ´·în®£>mÌw&ÕfÚ e‚ñÈÁÚ3ÈI«?ˆd¸ÓQbÓaßpqØîÅ…w‚Ä´`Ót	uûk	cšÂÊhã¸•@µ¼%Ðo<a”;e‡ª\ZÁÒ/¤ÓÔ[jšÊ,’o’›*\nÿ\0³€A!{…ÎÖ—ý™({\rOM\"gùÂ\\Æp®IÏPRGá@x+í£XÖcg¼ŠÖ9ÉkkÝ/˜ø`ÀŽvíÀäœöÆ+²¯9Ñæµ¼ø’[íñNÛ|Å»·á¼½« qòª;tçƒÆ¯F Š( Š( Š( Š( 9øpÓßK§{›·ŽÀ]ZA˜Â¸gó%ÈêUU@ãqãšÙ†öæþÜµ¬—Ú[D¡4±ÜÖù*$W\'iÎÒBäU6¤ºÜÙà‰Û[l¦5w,yþ6€©.tö\Zå‹]j×Z^³uâÏe…at\'’ÇêsŒ`PU¥„³xhÌ¾½¹K«ƒr’µÚy° lÃ´Ä…P€)§Cß_ÁÚ¶—b÷_h·µY¥Ar’WŒ6vp@=F:V%µ¶­¡y¥Ýx¶k+”¸yM¾›ló9ä7–ùjr9ëÖ¬Ü\rZä&™yâ%¾¶‘Ôùw±>IpÀ…É„úv š\0ôŠ*†‹ªG¬éPßÆ†?3r¼d‚QÕŠ²äuÁUú\0(¢Š\0(¢Š\0(¢Š\0+Ïa³µ·‘õq©G§i¹ÙÞ›57R+–b¨Ç<eŽÜœ“ï]dÞ!‚+©âK+Û„¶m³M;Ñ\0‘×$€Fp\raÜ§‡#Ò.æ´ÖåQD÷a·ž2\0±\n¬	çŽ€ž\0 ë»í6}2ñ^›q§ÍrÍ¦Þ2å„cK‘´¡l=AÆ)mn|m}m5ž=íçœ¢ÙXn\"Bp¤<uûÇ§Z·áùy¦êÚ”Áe-¹\r\0?2›tîk?Šô(¥•Rå|¥,á4é\0óò{w vK‹Ëèe³šñnuëZSauWÌp0ºì m9œŽ½ë®ðÙ¶Mki¤”Z%üØÊ:¸†_áëÓÓâ¹=\ZþË\\º½¿Ô¯µ=2d–CÜ\\-¸X·\'l\r¹;Öþ™yocÇO´Õ5O5ÌÓÞ2(2¶\0ÎX n\0\0 Æ\0 ŠŠ¯c{o¨ÙEyjûá™w#`ÌAö«\0QE\0QE\0Vg‰%¾ƒÃ·ÓiÙûLq]£-÷¶ÿ\0µŒãß§Uu;·°Ò®ï#„Îöð¼‹êåA8ý(*ÎÆY,¢¼ÐõéÞ9T:‹¶ûLn:òIÜ?ü*MÖôßJ†ÏL+9nCI\'fÎ	\'¦êÎ>¼žÞ;±e¡jÑÜ(•¶DÖŒÙù]w›€>ôÝ>çQðÅýÓÉáÍR=*HSdPL·b9AmÌíÊ¤íŽ	â€6umf÷H»Ó~Ø †Îk‚“Ý;lbd|¹l`ç´Ý\"ûWÔRòò¶{v»‘mÌ¥°ñ®ÇANyÏ_JX<Wi¨ÝXÛÙ$…®.\Zâ¸·xÚ0#vÏ ªpsU—ÆúUî£¥¨Ä³CvÐÃm—¨Uþ“’yéÛµ\0IªhÚ¾«{§Ý¤z}…Õœ»ÅÒ³LûpA@6§>´íGKz|ú†«âM–‹3E0Gûª€sØg<ÖYÖßÄ\Z«Ë¾¼tÈbAv°Ilg–ÞYÎÓ€6Œn“RßÍ7öœ±ÚZ4¼†-Bi¯J‘Ý™¤Ú?„uÍ\0tZ\0Ô‡ì¬Û¯þÎŸh8þ<sŸZÐªºeÛj\ZU¥ëÄakˆS9ØYAÇëV¨\0¢Š(\0¢Š(\0¢Š(…ñåµæ«n·Ï5”«Ã‘žMèBž3óääuª:g‰4¹,mÜÃm©ÝêH³jÅò(@§?t–ÂñÂÜ\ZÛñ¾œú–qª1[Xš`Á\ZBTb>9`Ý6Ž¼W1u¯CâXÜ­Œ6º}Š•ÄÎ©-ëÉò\\}ì:9\"€7æƒE·°:…Öˆc·Á”ßZ»´ñ9,0üd”ž0)WÓ£,>3µ¹¶#èRå€ô!J±ÿ\0dÔº]–—âI¡‹[žòÊ\0±IÄ!gA»˜(fR`®\nŠÆÖ[«TÔmu]U#—Ë1¥™Êgz•\'eO¨æ€k©Éå´\Z‰lDñ®mìšÀ[FþŠàž2Õi—ñêšeµü?râ%çÅc}—^¼°Hå}#S·‘#Ý[²‘ÕdÏlTöwz†­k¥ß¥‘†â05ª4b2›r…I9[ Œ}ÓÅ\0nQE\0QE\0QE‹ömSMÔnæ±‚ÞîÖíÄ¦&”ÄñÉ´)ÁÁÐ{s×µ=Fä,Öíâ/Ù5¤Ò¾Ó½g18_02xÈÈŒúÔ¾+€8°šæÞ[­>)ˆ¼† Ä…e Iµy`§@IíY²ÿ\0dêVçO__¤7\na\\¢ì`ÜmÌ‘äŸNI 	,ÿ\0áº¼¼´¿’nm¯&1!Âñ)#8`AÃcvî\rh?\r<×‘Ì±üØ¸ÔUîC9»Öu¶™¢Ýjš†‡r÷sC$S¡¸Ÿ÷û„J0x\\dSš¾ÞÑbÙ%ãI,P°p—,r28gŸJ\0ËÓ¯ãi.ÎÖÞþîÿ\0R¸–Í¤oÝˆÔ*´»°~P@u8Ôk=î¼ö&‹öÙW1þýÔ¯-åKõÏh©§eÚj‘^ß®¡rg@¶\rç3/œÅ€RBƒÎp:ŽzVÖ›b“^Ú¤6qCo!ºžæù{4»J¯9Éá›=†\0\Z\0ÛÒìKÓ`²ÚA\nà»usÔ±÷$“øÕº( Š( Š( €À«\0AàƒÞ–Š\0äeŽÏÃ¯m¥IâëÛH,±13™gjölf´,d¼ÓüDºlúŒ—Ö×V<: ude7(§¦F4ÙÖîÃ[Ô%:DÚ½üq€ahøÚ¥J8v^9ÎyŸÇ\"\r\'@]bÊt«ýQ%šÍ–s°¶Ó¹P«2goð8í@F°¶¯iwq	VIãEŠÅ€È#$ñéI¢Åmš±ÚÚ¥´q¼‘„^Øv“Ü’2}Ídê\ZoˆRæÖt¸‹T·³º[…ŠEX§a±Ð®Fãx`p:cÞ£Ò4ß\\¬¿l¿†ÆÎIæ—É²;¦Ë9;\ZCÀÚrÑÉï@%¿ÔïõÝFÎÖö\r>×LHüÉ^0»²–9ÉT.ßÌÔP%çˆˆ·¸Õt»Í:)Ÿì%ƒMŽˆÃq\n¤òFN@ÇBjœ~‚æSêú”o1iÂ­ÅÌ/ À%ˆ9 c§}5Ø^éÖ—Ç­Ôò[<!”íØŸ0ˆ#>ÜúÐF\0KE\0QE\0QE\0QEƒã=;ûGÃS¦Èä2\\}õƒ2ƒØÏ¿¦j=væÖÏÃ°AlîZà ¶H2\0Çîü›²Ü9½<ÜÛÉÈ)T£©îÁÃiƒRšáoçœÅk+[=¤’;Tæ0ÌÙ,¥66:ôâ€+hžÔn­–{mMìd²È¹\nÖä³»‚È¤íV<¤ãœÖÆ•¢ø›KÒc´´½Ó$òâÙÌ$ÁÂ’ à03žp*¿‹u*Þ/ì´6þ\\/³ÛD3æ G…P£\'w^8<Ötmâ+v´Ñìô‰ãÐu+¿5\'‰vIkÉæ26Ö;A@û¸uè§‘¦i¶ë­á|l7¶àÜîÀä–\0H?,Tö·ž³»†ê+÷–XƒUî&ŸÉÈÁÂvä{UODš‡¿µaSiyjÌC¨Úº„“ó+sÃÆ8ÜMwQ¸n­t	ä´Ø³O‘Áþâ‚Cs’Ã=³@OÌ	<¬±H¡‘Ðä0=Á©+#ÂÖ×~³‚ê&ŠUVÊ62bFqÐàŽ;V½\0QE\0QE\0TÔï×M°{¦C!Q7;0U=2Ìk6][O¾¶¸Òõµ†ÂwB’[ÜJ»]HûÈÇ×ÜräZ:¶Ÿý©¥Ïeæ˜Lª6ÈvÖqß+P›[ÓíZ÷T‡MÔ¬ çŽwIGVPÌÁ°9ÇŽ(Ãú5¯ŠtË¸õ˜EÇ‘<HdÞÄp¢<€ŒðÀ.3Ôu©ÿ\0\nïÃìèe¥D*|²±…89Âƒ×Þ³#ðñ¿¿Õ§Ñ5°ºKä“|,ËÑ41ŒªG®r9úæ­øŽã÷W:ÜÆÄn’¹Kc¾Ð@ÁÇ¿ç@ü{¦i­]ê7v–×³ßÊ¯\0”4Ÿ+áG<±$(1]Ÿ­^\\ê‘ÚÝé¦Ò;¨{bòfM¨ÊuÇÊNõ dúE`è^bBšfŠ-à’YîeG½­íÄ¬ z³’;žsœ`ôV\ZEÜ:™Ô5\rGí’¤&vÀ\"¤†làœ’Uyã§J\0Ö¢Š(\0¢Š(\0¢Š(\0¢Š(RkÝC^\ZU¶£%„pÚ‹‡xUK»3Qó6¤ž9Èürµ[­rÖmãTÓ|è4û³%ÕÕ£BžS§™åýñ‚áˆÆ&´üCÿ\0è»€êûÅÈ‰ö4>nõ#vL|…Î:ñš‡K¿6zÅ¶o|5-6ö)$¶›Íó˜%Y¿‰Ha‚yx ÷º¤ZY\\XÝ¤±Ïw,‘8e`[‘‘ê2*¿ŠËÂSÝK2ÛìY°îØùÃ7æIíPkZöû6X	}?“r &=ä+:?ËŒ:º®¯${RxgD´}¾T2¼sËªÊK‹hÑÊ\0¹þ#·%º’O4_Ã÷\Z¥Ç‡4»\r6ÂKã¶‰%»ºfÜ Ý²3ó1Ïv\0wæµt[Ë¶¿Ôt»É~ÐöN….\n…2#®Fà8È!‡\0pWí²jQM¨ÜjƒNÒ£‘ã‹ËeV—k,ÎÃ€H8QÏBO8ô´Ô[§±¼’îY$\rq$Ï™3´ `mP½Q@Q@Q@Q@qþ#±¿MX[i×2FºË—$*†DUÚÃÊÇž‡°¬ŸÚ‰´³r&’,íq¼q‡9E<m$nÈ$c#¯Z\0££xBÛMkÆû\\±ò‘í8Ïû(\0\\ûã5©ø›XƒVylÕÞÞ+vû\0@Ò\\]×óò¸Ú:çÙ¶úf½­¥»¸›c¨,dºu‘û¸vãý¦ôí;Ã6\r†ÿ\0Z»µŠöçvñ*F<Æ9Â*ÂörI$š\0X5ËMgPS¡Àg½+ºI¯âf¹Ïî‚da€É Ç994»]BÇY¹Ó,¯\"³”î¸ò’ö²”6ÑŸ™IŠÙëOº·ÔínÃÚ5ŒÐLÑÎ§nRB€–2ÊãŸzåà°ŠÖkÝFîæ}U¢f#RÓîTIo\'iHrO\r»®;\0‹L¿Õ×Y“KÕÅ›¹·ûDRÚ+¨vÒ¬žy9çž8­ºÁÑì5©\rJöòÖé>Ê!†hT«L¥·qÐ÷xäôàVõ\0QE\0QE\0cxµTørâGr±ÂRY\0r¡ÕXRA–E¥ÿ\0›¦i,pgGEŠM·	Œ„c‚p2~ñé]&£}o§XKwu»ÊŒrKÉÀ\0¤’=ë›Öu6öÚÝžçû2òÂu¹Š\rB3ò¡†Þz‚	åIÇ¿J\0Ãðü-Ò!¸H\\Ù½ÄÁ^4v’4gpyPò‚zãŒV±Õ|U)1Çow»cLŽ1Ÿ÷žàù\ZÍð§.âÑ ¶ŸC+q¹ÔßC±r_~ÉNvF2+¦)”&ù<?©*’ë%» »„½(œÑ4›Ûi5-SV×þÂöÒ¼7ÛÅ\Zî.#våæl\0O^­ZZE¾“uâh%Ò%–o²Dò]Ü»ÈÏ#°Ú¨Å¡fÆ;/JÂÓ¼Q¹âû©×D¿PIÇËnÒ´²*:!ù2£9È$ã÷cÓ·Óµ›‹­JK½5ì¤‰ã\r*¹d,G8û¤ÜŽzÐµQ@Q@Q@Q@wÉ¨Ùx€ê6š{_Ã5ªÀéˆ+³ó!Ï~Â¨ÚÙé:¶¦Íý•6‡­ÚæEq\Z,…Oƒ.VE=søqW®ï5‹½bk&K8Ò$yžæ&»¶p +\0Iç¨ãƒYæ][Lñö¾»oÙ­Ÿ“X†u„—ÜÅÔüÀ/# mç 	o5¤º¶´ÔPù–W)<’Â„¬ÐíudŒ9PÃœuèEI¤ÝêØý’Ú\rÃšY&âGgE+ÕŸk·Œw<€mßKš–‰}o2H²Lð‡FÜ&nëÌj\nv5´:<—²H‘¤“Í,²3`gÌaÉ>€øPM…¦‰áÛ”²Œ_j·ÖÈK6Æ¸h|Ý¾Xóœ€0O¸­\r\\]êú–­-¤¶°Î±As®ÙcÜK•í“!<ák7C“\\\r¨ýKŠ8noåž+«¹6CŒ€=;íã·£êSÞ5Õ­ìqE{e ŽU‰‹#¡•†yÁ¡èA \r:(¢€\n(¢€\n(¢€\n(¢€\nB<ŠZ(ˆÑõùt=*.{Uó­G+Ë8©ÜHU(à\0ŒqPjú¦¡¬[›XfŽÙ§Û)dIYB«TÈª#¢«ÓŒäjiú–…k{«Á}ycÅ~åwUp\n#ƒÎ7æ¯Oâ­#Am{k3nÚ\0“„÷8Š\0Â»Ð5[+ÓÄí¢\\],÷¥•K }å\0g•PØÈ gh “>®–ö	²¥´–o’TŒ\"JŒê®€¾Œ¥ˆÎpT+7AÖlì¼R^êýìb¾IçÛw9Ä™pPÇWc¦åÇ¥]¿Õ¾ÜÁ6Bu[i”¸¹·f;#œ²œg£zâ€4V	¼?öt»ÿ\0?Ižh¡[I~pŠä\0ÑH9ÀÈàäc¦+¦®7IÓ//­\"ŽØâKKåûeœ‘dE,n¼¢Ú­Á\0ä~1Ò»*\0(¢Š\0(¢Š\0Íñ³]è—%ÄVì¡dYf8E(Á\'°ùzö¬{ŸQ\ZðiwsŸ-–ÊøÍ)È<…Ø7¡ãð­û\r©0Ë-ºÞD×+\Z>Xl’Tr@!IöÍcêóhz•ÌSè’[4ü[ÜÜZ¼÷=7Þ{d{f€&ðüþ¿ðÖ—\rýÞ•4V¨§ÌhÜŒœóÇCWÛFð‡ún‹ÀÈ&¿Â ›ÀºA•ÞÑ~Æ¬K’dŒrHY¶þ¨`Ü\nÝÃºé–¹üÌtNëÄvÖž#¸MÔldûdpB“¼ª`´Ø$,\'‘ËvE½³—]óçÕZòîê*Ù¾ÆðDè¤³cÃžç ã½gÍc£øGZ³Ž×Mó¥žÊH¢Œ2^Kæ)\'ŒÎIì ­In5=RãO†ãAžÒX.–g‘¥G‰’’sŒc½\0t”QE\0QE\0QE\0QE\0rºìš\"ë›¦Õ5[±ˆôñ#o@IO3b’½[*pOj—ÃZÜ7z¥æ™¨5a†9á’N&PÅFãh9#?7>´’kKÖ¯ãÓôµ»\Z”É4w/ HáÄjŽ$?{€€ÎìqŠrÆú¦ tívÎubó­¯,¤9Ûœ¤€ÈA# ä÷äP]JÆ}+ÅZuÆ›k&\\ÜÙGŒ™vŒ>iÜ8Îß^³xcL¸šØÉªBŠ–÷2ýšÔÊ½‹9=Ûq v\0qÉ4žuÌž$±Ó/nqs¼ãÏä3)òÊ°ÊÙºúRxzûPÕ´˜\"¶•!ZK«°7îÅ¶ <g99\0Áì^çÄës}}¾!¶Ñ¡´ HÕRIå+ÎCgž\0¶sÎ§…b³xnõ}`jÒÞJ×\0*€UB…Ú¿w\0t<óžõSM»k!q&áÆšÍ\'t–èL¢{‡V\"GÚys¸7VãŽÕgAi.õí_R[I­ígX#ŒÏDÒ:ÞÛX>ò®Hçm\0tQE\0QE\0QE\0QE\0QE\0sö/â]Ug¾¹Y-åŒ¨ŽgU²(\nFàÄç\'j]oÂšuýŽÙç™Uæk‹¹™HçƒóŒxôª\Z¤š¼Zæ ¾GÌ¦3u#¢’l	Á9Û·9zw¨æÑ¼]4I5B[‡¼±È íò8 f€\\ÙhÖz•½Ž…e6•¥ì{·ÞNåòˆ‚@+ÔŽNJŸÅÚ¤úXh²K&¡nþXy#„Œv0f8#Ÿ»ÇJ©§xæ÷J¾¶×µ[©Mã³~å˜€<Âœœ\0 \0Œš×ÿ\0„rßLµ´A>\\­¨Êòìˆ}î§ÛØúdæ€*Ãtºt¶ž!†ån4ûØcƒQq±Ôa&#¶	*àò8ÏÝ5Ö+PÊAdÐ×áÝR×L´Ší–H4Û—0^EsÇÙ‹dÃ+gøZ2ŠOû¹èÕÕøi<?k˜ü¤`ÏyÎÈÙ‰Eü¨ü(VŠ( Š( \n:¾£ý—§µÊÂg”ºGA¶ùŽìF{rFOa“\\ÏŠõ\rfM%leÑžÉîbK;»{„’›x(ÌX\0ýÃé]&³¥VÒ8ã¹ki¡™&ŠUPÛYO‚:×+âôþÇÐ!ÔgÖ®®áKØ>Ö_c‡Q $¢€6° ·Ðõ 	´¼k`f²k‘ýój’Î9Gþ€)á ñAP\r‰ßÜÒ¦lßR(ýkq|W£G{™ S<F=Áe\0~”7‹4RF«nøë±·ÈP.¯â¤ñN—y{l²‰Vkhå£ˆFì»÷…yll2XžGNý Ôõ‹]VÊFÎÒ;ké\Z0LÎÑ8Vq»*2Vè:æ°µ\rrÓÄž%Ò,ôké£Ä—Or°2ìù2)`0äÏ`IëŠÝ¶Ñ#þÒ†tÕîn­ìÜºZË —d¥Jä¹Ëtfà“ÉÍ\0nQE\0QE\0QE\0QEp—š¾»udš•Í…½”qœ[l\r+¾NK2ž\0ÀïœÓ Ñu‹\rbMPjI©´,\rÌb&\n¥ˆÚÈ1’XçåçŽ˜©5Ë}ãP5¦šëÊÈKu™˜ÆñºŒ“ÝóŠ¯àÛ Ë©ÚµGÌ-a»GGXv®0+»yéÓµ\0Pñv\ZÞ±£«Ã:\\Èg·Är˜f‚©e,:\r›³ê#±«ž¼>‚Ú\'k‰nn|»eJ À,z*ž{c4ín)áñE•õ¥ŸÚä†6‘ Bªî0PX‘½:ž•\'‚mf‡Fk‹™I¦‘Ôí9U\nÌ6ƒÜnÞsþÕ\0%Ž‡¯Ç‰¼@¶Áå’_*ÒÕÜ±¤O,zW¼;u{gp—¥âÒê[f•h—iá±ØFG®k›Ô/´ëŸjÖúŽµwllÖmö9¤\"U‹ü«Iã9µÒøpik¤*é7k„;˜¾çy	ÜÅÏ÷‰99­\0jÑE\0QE\0QE\0QE\0QEÊK«[è#Õ¥¾IóvÐ˜B…ÚÈ±IîÜÐT“øÆ\'EA,Y ùŒÐH1ßeÎ•\ZN“e«]êZ¥ÌI8–ùÖ<å*c>‘ñZ7ú.öô­6Ì)`ˆvìñŒç4Ä]É%Ê^jv1ÞÏ¶nnmæDµ\'hˆ¶Ho íáŽÒsQk\Zu…Þ¡e­xI‘ã¶¶i®!x¶#k!t|oà8=ÎA‘]F‡­-µÝ¶Ÿ$Ö–·RÜÅhŠÇxx¤ÚU³Á-ó71Žj¶µ|×~-—AO²ØË5²˜n.UÛÏ,X¢‚²SžhÒ^¦£omâ[kf2D«§d£Ì2@Ãpãø¶î§*HïŠë-§†êÚ;‹iHdPÑº†SÐŠóÛ-N]ãû(Ê!Õa°\ZpÊdM\"2ˆ$ø·#–ü	®ûN°‡LÓ ±·Ê.zŸsîzÐš(¢€\n(¢€1¼Gûè¬l\Zo*ëÅ†bnäÚÎS=·lÛÿ\0®vþ×Â¬éö:/Ø×P‹Q‹Ì¶´“hAf,Špp òGZëõXtéôÙ†«/f‹¾_<€rsé\\V³ªZGw¡CákX­õ™Å¬7VÏoÄÑ³0\0¨Îí 1‚Fh©O\nèñHd··šÔŸáµº–EF\0~Tÿ\0øG,;K¨ŒœŸø™\\sõùëko{ô\Zd˜£}\"æp§¸ÞŒýj9<_:‘êºtòg…þÍ¹Cú 	õí\'E±¸ÒòÂ3¥Û™cÙ°°IiV d’v°ï’ÞôY¾‘\'‹töÐ¾Ì«ö9þÒ¶ªÝÀê1ƒØÈÏÞ÷¬Û½{X’ãO¿Ö,íl´X/Q^F‘ãy\\Œ+íu\"“’	Ç ç­Óu=\'Pšca,m1ä\n;ÌA\0‘èzPQ@Q@Q@Q@ýÜ—ZGˆno×K»¿†òÞ(ÃZ…f¿ÊC0àïÎG¾jÝ¦¡m¬É-¥ÅÍ¥Í¸Y7\0+€ÙÚêÈÄv# äb¬5µúXZØË{rÑyÌˆèS8ÎXŽsÐ*ËÒîu	u½KV“IºKi–(\"Y\0Y” bNÒq´³œ`çÚ€+j÷Ùþ&Ò\"Ô.lÕg†IålnGPÉ¹ºš\"©ÛëOðµ´·Ú%µ›Í46–q,o\Z9W‘ÈC6sÂ²ƒŒIÉâ¦¼¾M[ÄV¶–B	Œv—ê˜ùC˜¼°Êz|Ø#ØzÒxoY²ƒ@²„<÷w­oÅÊÁÈÞdƒ{ aI,N	PŸí\'Eº—Lµ°¸Ûl®\ZÒÑ\"Ü27m\'œyÉëF€é­jÚ½¼n¶—\"£wÊP6çÃ\0qóÏ}•‘¡ë:£ÝkOeáéæ’kã(ieX¢)å¢¨s¹¾^@à	®¯KÔbÕ´È/àWD™s±ÆOB§ÜGá@è¢Š\0(¢Š\0(¢Š\0(¢Š\0(¢«ßK<\Z}ÄÖÐùóÇ4qgØBþ\'Š\0âtß\rÜë¶ÿ\0Ú‚kkH¯L Ž9\n)\'P:ÙÎOsS\\øJæÑãò!Ó9>lzt¹tÎ\'A<ûV®…©Yéš­©‡Q>TC{ÿ\0gÎw1å\n{“QkÞ*²:êi×.nÞÞA€ÑÈiÆÐÃ%³Ž\0Í\0S?,uø™ÜH·[‘¢–ÍDe\0îÙËc,I$’I9ÏJÏºÒìÿ\0á#Òì.oî$¾¼Y-î­î/<Ò±ló‰’J0tM¤c<œqÅûmv|%â	­o]’ÓwÙÊÉæ´;¢\\\0Ù9Ä…Ç\'ŒÚ«iÉkÆ­YÛsv¶ñÞÛ)I<íŠÈÎzîmß+úðy# Öf¼>‘ß:ÄwÏi,è0]\"Þ%ÝìÁ3VWg\\¾¤Ekâi¾Ö†[èa­Ê9T˜>P»G÷V\\&	G§AÔPEPEP=[O]WI¹°vØ\'Œ¨oîžÇð8®e|Egâ)4åS¨»ÇyqDX%Á=Žï—y®ºâxím¥¸”â8»Ÿ@Mp·º6¥«Í¢]^jòØÍ{,®‰i\ZFÖÅ¡fP\rÍ€ 0$†#·ßQ\\gü#úô*«,“^ã‚é­Ü@[Ü®ÓÁ¨DÔ¤“-¢Ißo]gëÀ4«âé#´Óíu+›m>ò9çw|œ©8ïÀþXÝÚk>&kÛ9âž;1\ZÉ•·‘è#N?Ú¬\rKÃ:¤v/wy«Í½n-þËhnx\"s*€Î_™:ô8¼ŽG’âÏRºÒo&Ši­Ì2¤B6‘Û€ã!‡$ve \rª(¢€\n(¢€\n(¢€\n(¢€0u˜îìu›mnÒÏíi´°\\ •#`„«+ä.SœŸâª>×µMk]”	­&±ŽÓ}›HÄŒJïnK``d\n½®\"êú­–„~hsöËÅÏ4?\"PÏŽ=Óc‹þ­Jå£†VÓ/	›dæCä€@3†<Ž¹çš\0VÒ%¹ñlVw­e<ÚtÐÈê· tì{‚ù·=sSx>Þ?Ä Œª•€êÄ`3êv€?\nÏ¸¿ÕãÕ ×†‰w=·—5¬v±¨(%d`Ä`1Lc° Ÿ@ýUÓSK×ˆ†xx’]£unIÈÄƒô½\0X²ñd×¶ÜÇ j®d¶T\né*·ÝËƒ„÷ÝŒ{Ö‡ôé4\nÖÊwW™´¬½±,ØöÉ5ÌÁ¤Km¡Ýëö©uip·ymhfp©\n¶|½™À(f+ŽûWk©<)4g)\"†SêÈ ÑE\0QE\0QE\0QE\0V\'ŠO›cm§ï*/îâ¶’“;œzª°ú[uÉk÷lþ%„éÒya	WF\Z	0xéóá{‡Þ€/\'‚t$ºÚ”9ãÉo$Æ=¤þ$Õ-GAÐ,nCj:‹À—N15ä¡ÈÆ\n«û§<ñü_JWñ«l£ÍŽ6oîÍasÈšÅÔ5c¬M¨J–‘^É6œÖ¯ok+KöXòÛä2©%0€n;N:PÕï‚t˜e¬â¹·‹Q)\rÜÓ‰“g`™c®}k2h´ùõ[-G-¤½ÏØæHXÀŒªÐI¼ó€]—9ãåþíV±²Ó|Màë›©.ÿ\0´5´µdF‘ÔÉÆ¼öôŒîÙ>ƒwJMµ­Ô­Æ—¯Eöˆd¸s!vÚFY‰,\nm Á‡A@tk=^rGÔ\"VX­Ü^	ûVÖ%¯U`·vÉãŠèkÂ<øvI${vy\r¯˜Å™aÞ|°Iäü¸ëÛ·@Q@Q@úíŒú–‹sglÈ²J bBB¸ÈÊ’9†T‘ëX—oq§kún£âëxíJ‘„M‘[LÁB‚äó•Þ2qÏ¦q]]p§\'‰µK)5[‰nlïÞõE©såFcm‘2‚†9õlö ¢Oxv&ÚÚÕ‘>‰:·òÍ0x¿ÃäãûRîrçŠd\ZF¹gŠÛ^€Æ½<í=KcÜ£(ÏáSý—Ä[6_NÏ÷†œùÿ\0ÑÔ•¯ø“@ÔôK½6ÓU·º¼ºŒÇo¼¡¥iÝÀŒ6{c5£k¡]G©ZÝ]êot–k €4@Ió€÷æ\0AØœâ°|Y Ü®“u¬ßjFïP´E‹$Bµrëó€	$ä¤ñ‘ÜÖö€\'¶ŸQÓg»¸»6³!Ž[ƒ—*Ñ©ë‘»Ó¥\0mQE\0QE\0QE\0QE—ªxsIÖn\"žþ×Í’%*‘—ržv°n\\óƒ‘Y~\n‰¤ŠïR¶wJ»qöF‘œF‹\\nû»;G\0ølë—Í§h×71\rÓ„Û’ò· {±¹Å¶ºðD Ù[Ý_[ÜZœÛÆ\ZOôÁÈ<gh““÷A¦hoV¹›ûcKÓÒv‚;£1‘“ï6ÔáAí×?ð\ZŸBº’óC³¸•‹»Ä79þ>Û¿¿`_xo^Ô<¦¿¿ƒP\nw½ …È*Û^<±X¯?ZšÐê~KHõƒy§H¾VØmÉ6d/ÈQ—C‚¹#9Ûë@·°:ÕÞ­k©j›£¸1½´rìÉa”\0pTàœç;«¤UT@ˆ¡UF\0\0\nã`Ó¥Ó¤ÓüI}&¡w~\réÝƒr«GGý•- ší(\0¢Š(\0¢Š(\0¢Š(\0¢Š(+«˜lí&º¸}ÁI#áP2Oä+\'ÃVóÉmuª^ÃåOªMçùGÇUXÔöÎÕû“Nñ-ýÕ¥´qÀó_\\}˜}¡w Ê;r23»@$°Ía§…/moj›0\0Ynm\nb’0 Æâx­må¸ÄqD…ÝÏEP2Oå\\¦›/Û®eKmRöâÚê9Ú]Äc’êZ9 m\\dÓ§B\reë–úî¤M%íáM>}¶óÄ/òFYC°<@’7Œö«Ú>«cgöä½†åïÞUµMd1†1û¯—8åX±bq–<ŒPUb- Óç´ÛÅŒËe\ZÍyxVÚCeF@ Ã<Öí®œš;ê\ZcÅy¥K8ó´ÙÓ|E™Â°@Fb}ÌxéžÀóV>Ù\"êÛZ~—3y ÙêVJª\'B0ÊÀ‡Àn€òJžHÖï\"×4ëÙV™eš(ç’3ò—R2Hç¡àÒ€:ãHbX¢EHÑBª(ÀP:\0;\n}PEPEP7‘I=”ðÃ1†Y#eIT$`Ã­pÂõ<3¯è–ž\"»°´‚)Î;Tr<Ì¢à“’~\\ã’OR+¼y#ˆfGT¬q\\¤siþ ñ…å¿.l¥ÓÖ31$¤Œ9ùL£zã¸ \rEñ~ˆä„¹•Èþí¬§ù-ð–éw½#×û>ãÿ\0ˆ¡|=<\'6Þ Õ¢•¤ŽQÿ\0‘ëO\ZF¤\0Ï‰oÏ¯îmÿ\0øÝ\0bxŸÅzï‡ï4ä¼qwwCmÀèÏ)á\0Üñb·4ë\rF-Vk»éà4	˜”«I‚N\\g\0ŒàcÔôéYºÖ‰ce¤_êZ„³ê“Gi\"EöÆW¸tU\0(,vŒã=9­Mþ+­ØOŸké!Ä‘È\n°ìr×¨â€5(¢Š\0(¢Š\0(¢Š\0(¢£žxí­ä¸•¶Çsè\0É \nú¦™mªÙýžäÈª®²+ÆåNC:Edx*ÿ\0íºLÛï¥¹uº›jÌItyòù<°*\rß>ÔæñV›wg²[\rWü°>\\È¬;1È>¢©i¿Úúv•£ÈÚEÄòÚÇ%µÄhcYcˆú¢çŽhkT\"ãPµÓ¤”G\rÄ3<ƒ Û´ÄÝ‘ýÚ—B¹–óB²¹œî’XUË‚ÀŽâ0\ZÅ¾ÑuíR÷²XJOÎ–ä2v Œ\0³»ž{\ZŠãU×4K94ëÈ\råÜÐ¡M§Y?–¡B2Bíà‚H}([k{ŸkZ‡Úuv6Z~ ˆ¶1GÂQQ×scqùŽHÏQŠêk’µ“Nð×ˆï¼È\'³¶šÚÝS²Ü:™1`Íó¨9äûÖîŸ®Xês46Ï(\'˜X^2éœn]ÀdgŒê(BŠ( Š( Š( Š+?^ÔdÒ4íF(ÖI- i[¡ qŸoZ\0£kw[{É~Ç¥Îñ[Ä:¼Ã*ò7Ó,ª>§ÓÕÆÍá=RéÚ{™´»«ƒóyÆØÄ[þ+ÄµÙ$ƒå·3Dq€bÖ\ZEü¦‰ˆús@\Zž$ºŽÎæÆT±kÛÀdhc\n‡h–o™€ryÅ6öþòm\ZÇû\"ì‹èŒI,`´Ÿ&ð¸<.GsœVgSÔ®ä±vº“YD’Ýšî5ò,âpH\Z5Q!a´/Cœô\0ÕƒzúUÎo«b›L¸[vœ>Ø¥†H\\,œýß™0AèAç‘@Òç´Òu«[ëMBwµÔ^Hµ1zøxîq˜ÙÔãcaY0\0\njßð²oÓeÔ6ì\Z•ÃÝªãFÀOÄª©úš­¨Ùé¾!Ó¯\'º´û-Å¦åóØ®äÂp$2áÁÈõ«¢\\Kw é÷3¢Ç,Ö±»¢Œb j\0½EPEPEP;®iÃûb\rN]1uXZ³½¹‰]¢9,®»¸Á<7à{\ZŠÎKÇñ•µ¥Í­ªZiÒH‹¥¸w@F1°ôã‘])u°ºy5ÇxŸB_x…í|ÿ\0*ãN±I­Fæ@ÎîÀî+É\\Fã¦ãŽh³¤È$€FGQ\\dz(JËà;gí¼]G&Áüé_E°xÕá]C‘Óþ=Gê4½âh„Þ¿Ö3&Uf8PÉóûeFkîÆóÄAæƒmo=ÌCÎÔB¤‚(Êç[$Î\0ÈÂõÉéTÛÁpê7hé ÛBÞk¼W^dŒW•FPòsœãç5ÒXkÖƒÃv\Zž£}oŸlò3…RåA ~9â€5‘v\"®IÚ1–9&MGIcY#utp\n²œ‚B\r:€\n(¢€\n(¢€\nd±G<O¨9«)zŠ}SÔ5KM1b7R0iŸdQ¢3¼‚pª “À&€3aÒ5}=>Éa«Ä–	Ìi€Ë,+¸pGbÀ‘Óš«áýRœé£SòdU‰äµš(Ê´’‘ó&èG¡«ïâ}\"HÙ#»f˜åD+´ ÿ\0×<nüÅs^‚\"ÛH¼¼±ž[h?0±qÁb’;ŒüªBŒ`aA9Å\0vrÜ±Ôà´‰—î4³gû½\0üIÿ\0ÇMúŒ3iK¨¸1Gå7ð`rÓ°î5©u¯í-OmJÝ kidŽdPïSnâ;ƒñ»ŒàÔ\Zeí½¿…åÑu×6×±Æ`¹NæÈ\Z 2Xœ`g ŽÔ\0û=_ÄZ­ø†?°iñÍf—p‰!yŸk·Ì `œg­­3L’Öi¯/nêú~UMŠˆ:\".NÕzäžOlrÚø±ñ²êÑ\\Á*i¶ñI$ˆí\Z>X1‘FÒGLçÒº›_i7·Ikm²ÈÀ§†ÀÉ\0ô$Â€4h¢Š\0(¢Š\0(¢Š\0+žñ>µs†âùbD×Ïé\0o¸=Ü‚=€ojèkÔ^][[žãOÓ®sg›W»†ù iJJmU`@-Áls»à•Ct‹%»oàP7¶•Þ¹x£Ö ‹r\rz2ßÞ{k…ƒß•VºÖ/fµ{ZëìÖ·@Ã$—:L‘`¨må2FFN\0^Õcq§ö6§wq¨]Ì­nÿ\0j-Å`dfå)· ðNHÇ5åí®¹â}>Ý-BGm4Œ·“à¦äxÉïŒm$g[H¶¼Ó¤¸ñ`ÒíåMBÚ9\rµ®RX#œ<;w?t’03VtÛ>ïNº²ž¨iæýü¹â\0¤k&ÙT±È ƒ.29ÏhªézœZ4~]6m¯$qÏx|–·V\0ž¹ÜcP»qßÒ»`\0\nÅ_·i\Z­•¹»{»ÇhBÏÌ°¸FpCÿ\0„Ù#Žkn€\n(¢€\n(¢€\n(¢€9G³ÒµmcZÔud‚K+_*Ö9fm¾K ,å[ø9p20r´ÏÇ£¿Š¥“C)$1XžQ#;3¼™PÅ‰9_ZÜoé©I¬\"k’Á‹œà°W¡`?‹÷®^Mnk/êN’Õ-®æ-ÕüOBhÑPÆ²¡Ç!±š\0î¨¬8n|M$a–×F”y/$Áÿ\0ÈfœfñNN4ý#¿Óeãÿ\0!Pí^n4kØ Ï›%¼ˆ˜þñRëX\ZN­áx’ÖêÞÎ8ZUHþÚ–Ec@L»qœñ×ã­?TÕ|I¤é³^]¶‡IPd”îlp `Iì*o\n[ßg\\E#¼[£¼·º€FC·ÎÀ§ )ß‘Ô`Š\0»áÆÛ¥›LäÙM%¶qü(Ä/þ;¶µj++]:Õ-làH!LíD<šž€\n(¢€\n(¢€\nÂÖŒÑkÚUÒXÜÝ$)><„ÛA$€ ‚Ç\'Ž+vŠ\0çµ-kZÒ¬S¹ÒíÒ¢[²Óm\'&ÒFzgŸZÙŽáòkB›Z5Wí†VÏ?š·ù5ã&•´ë;Hcšîþã–@Y#`w‚Ëü@”Æ8äŠ›ÃsO{öëÛ¹`{‘pÖÎ¶1+ŒA$³}P‘¸Hná²‰sFÏ€@\n«ÓêÃõ¨¬®¢jfÒ5¸T; /8e\r×¨5ÏÜx†Å<^eYâ\"°–/2yQY<Å\nªIÏUÓŠ“áö±¡áØ-12^[D­r²–g$–•,zðs@æÖ‰šÑ¬lb’ïQ/-$»Ë\'s	êN×¨çõ©õ+‘ekq¥Ì/a¾·šmTË\0à9-€Sä/À{Y×SÍmâ˜ìÇmtÝNSù¿éA×`ì\0ff#¸ÀãŠï(\0¢Š(\0¢Š(\0¢Š(²H‘FÒHÁQfbx\0u5á•i,îµ+ÊQºk˜Œ„*©#±`»±þÕYñ…Î©¡]ØÚJ‘M4{UŸ;O<ƒŽpFAö5Fg]A¶çÂò8‡²¼†D?÷ÙCúPýfkÖú=åˆ·Öå‰-Ë†Yü H÷Èü©£Y»Æ[ÃÚšsþRšæo4[-T¼Ôntãe\"Í5Ü¨ò¤ƒFÈ6Í 7<gÐÐ¾±u¥ˆÅ¶¯0•€Ž{+Åkœ*ª¯#®;`u5• é³iÏkkuu¥ë1Çþ›nÍµfŸ4¯ÈucžW‚8ì1¾§dë¢jé^V¯9–ÎK#“£*ùžI`0#p\0òõÒ£h¾)¶…î´å™^1$\"îÜTàåIÏ¶@9è­ÝÎ³¯Ø¼ÚuÝ“éÑHnÑ˜À(Ý£ŽÞ™ÅtµÍhö6–+½µÒbû5œ©öˆâ33œ©UèPsŽ»—ÒºZ\0(¢Š\0(¢Š\0*¾¡xº~›uzêYm¡yYAêý*Ågëškjú5Å‚J\"3(™r¤	R220}‰ \n6ÍâÍ‹<¿Ùù€7‘y~Ûþmß]¢¡ðåµÊ\\ë\Z¡·že¸xãOÝ\'˜¼¨\rÉRrz’xat}Nô4ºž¯<“òE§¾ÈâR2Ç¿<vÇ­™n4¹õ»¡syupÞkm\nÊäUeÂäRh@øg@,Xèzq\'©6©ÏéQÉá/ÊÅŸD±ÉôGò±E\0fÚxsD°•fµÒlá•NVE…w/Ðã\"±4F×ou4Óîb¿¼’EÈñ<aO–­»åŒzW[\\Ž“§K}{ªZ.³t–6wŽ°ÇfV5ùÀ‚àJ³‘€F0(äw:õ–µ`º•åŒ_»Åöx`e0°FpC–ù¾é u®Š¹ÖÐ59nmbŸSK‹;;¥¹†IcÍÀ#?!a€F	±œ=ë¢ Š( Š( ¹x\Z…­Ö·©jsÚÀÓ0´•.šHAÚ¬pK`œs‘]EeZøkH³¹Kˆ­	x‰1	%yœŠÄ„ü\0 \núuŽ Ðj_Ûk	lÄÂYÐ¤o‚3¶5\0°ŽrFj)<=ßÉ®èbÆúë{2Šp0ñäa†:ŒAÏSUüJÖ¶ZÎ!’M8]9Þ@2íÀXHPrÌ[ŒŽŠ@äÕß4¡%¡™­šùü¶›væ!U\\üÜýðôbÇBÒìmmm¤··žxáòüéb_2N›ây5RãAÞÅ{ Ý[Ø$–ÿ\0g”ÇaåîÜ¬€pe€È#æéÇ6-¯â»ñ]Í¼ ¸µ´Q#í8Vgo”ÿ\0pçžÔ¾\Z¿‚ÿ\0K-ÊRiCFF\n~ñ±øz\Z\0«oáÝßíšlkq,×¥f¹™¥f2µ‹ÿ\0dd{ûT¯¦YÜ]+ÍrîåÂäÚµÊÆXz‘V#ô¬KÍU#‡TÓ¦¼¹Òïí¦wûQâ)K’ÑG\'*àr0@äVõ–¦_hÖÿ\0iÑ£‡ÎE•àwI°å;‡Lõâ€£8·¾Ôt±,’-¬ˆñù²eG\\…Éçƒc=±ZõGLÑ´í%[U‡ÎmÒ–g `d’O\0`zUê\0(¢Š\0(¢Š\0çüej×:Dm#·Ø`d¾‰N°\0C~\0Ät!HïIôp[N¶ t	\nÇùì5\'Œ$ÿ\0‰	´ÉU¿ž+7qÑG\nÄÿ\0ÀIR+p\0€1ƒ¼:Øß¤[ÈA .àj†¡à˜\'ÔáŸM{m6Õ­ÞÚî{P¦hÙ•ˆ»Œpâºšç|Es=õí–…§jgqrÎÓÉoµ¥Š5BAÁÎ}ƒ?€ k‘^¦™|u4Ón¬#vœIpî†4…SóÏ¶k/ÁÚUõ¿…ô½CH¹ŒHèÏ-¤³³C\"1%C²\0W,$H-ãÖo´ÑöÍJ]Aô›ÅÚjÄ›åòØ†,1µÔ`g€rkrÓJð¶¸Ï¨é2\"ÊüÉ6ŸpÐ¸\'ûáç®Cõ \r\ZÆîÙ¯n¯üµ^Ïæ2ÀITPªŠ¹ Âç8“Zu¼Ñ5›+C}-íû<j·,°º¡|†,¤)<‚Wžq[ô\0QE\0QE\0QErWz6ŸsâéÓZ²\"ü±œÈß»ØŠ\Z ùOùr}(oÄ\ZÄ:‡u©LÈ<˜ÉEvÚÿ\0…sîkJðž‘z×WzÅ½½þ­,ÄÝÈWh`(Uì›UHÎsœÕkß\rø:mEô¨ãŠ]\\ x’êI.6mÃó–\n `jæ•£‹í\Z	ô­^öÒÖx€û4«êŠ8Ù—RØ½@eð§…¡It›Àþ&@?Zbx{Â-±4í.f=‚#\Zl~vùõK·úd±BÓb?:–O­åj÷àž‚w[…„Šh\0>ðÆíÃC´ý˜ñüª§†eM.úM\0O%Ñ¯,Í°Ú¡ZFÞ˜ïµ¸=±SV6Íh5›e·s—Q§ŒŸüooîãÚ³[ÂºÞ³\"Þ¯Ú—N‹}ÕÍÓ|ÌÎ¿(Ü0UA8\\¸c½\0v2K+¾Y5Î2ÇŸ\\„ú_„­t…Ô,ô›M[íDEj‰ü÷c€ªîN\\žÀJßÐl&Ó4++‰|É`ˆ#0$€}<:ö¡EPEP3RÌ@\0d“ÐVKx«@X¼Ã«ÚN\0Íô^§ð¡{iýÅœùò®\"hŸkÐÖŽ¡¬Ck\r¤š³êp%®dp÷Œœƒ´äô\0Ë­MuÍ{H±Kkˆ-D¯wç\\\'•ç˜ÀÚ¨­ó\\7 p§ß¶ƒªi‘I‡µ(¡·“q×‘bId`Aœí9J¨|AáÝs@ðMgk*«4°¼øxYISÃã#a‘Z^Ô%Ôü9Ì²I0ß\"E<‹µ§\\ª9‰Pë@ÃRÚÜHF²öðMH¦1²Bè›GÍžW«íÇ$œäqN·Ñu{%†k+Û7ÒUÞ&Xþy<ÀÊ œà³pO;ºŠÓ‘ÒOÇ€7—ldŒÐ–ÚO×‰õ¦x~Q.œæ<yq2Aî+1íÇØ wMƒFð­õ×ÚÙïÆËµÜÏ4Ñ°x×9#hUøÕëè²\"‹«¡a9P^ÞñL.‡ÆŸ¥P·½Ó‡‰µ9µû¸!º²™Eœw2Hà(¤H¸Ë6à[®WVañ<÷×—ŸÙVêV6Ž‘´Ö×J]ÉPÄ¨?)8ûÙÏj\0×ÓõKV&–ÆáfTm­Œ‚§Á‘Á­Ö?‡¬n¡ŠçPÔcXïõ|Ù£S‘€#Ï|(=É=±[\0QE\0QEå¶¡i-¥ä	<®×ŽEÊ°®nx|)lÂÖmbvê²ê“¹íµ_?…[ñ”ltx®^7žÎÒág½·BAš\0a÷±û{ìÅ_ÓôÛ8­â{IY e\rˆ„B á\0ñ iô½å‡Ù<\'|ÀepÑ¡ú™XÐÔö³/„ç†KÍ/FÑôûÇhÙ­ÛkDB3ƒ#í³´©k¯¬Í_WÑ¬-õk˜N@X¥‹ú|¼ç¡ü¨‹Ó%ÒgðñÔæÑ¯\'-$·w\Z¤1ˆ¤‹s³HvÚ¤p¹ç¥O¦ZÞèzÍÞ Žukùã¸ž8É¸Û\"FÌJ¨Ã“®ÑŸ¼qœÖïö•‰µH,,f[«+_ß^mS³pÇ—=3Ÿ›ìðy«á´Ö-t{›!¾ŠmÒ½µÃyn²3ûd\0çæ\'†\0^Ò.#×5©õˆ’Sg+og$±²$î•”0qsá5¿XrxŠâÒ-þ¨ÛÆŠZIÊ•QGSò¹b?ûVÒ:ÈŠèÁ•€*GB(ÔQE\0QE\0KTÒâÕ!‰^i­ä‚Q,3À@xØ22ä ‚	«µ^þú\r3O¸¿ºb°[DÒÈ@É\n£\'Š\0ÁÕôkMÃ“ÞAss\rÅ‚Ëv.Ã+K#”!‹Ž:qŒ`V¯‡ôó¥x~ÂÅœ;Á«¸þ&ÇÌšË›Æ:tö[™¨Ï-Ì¹µ{h\rÀˆÙƒžrpZÆðzjévñA®=´Ùe67‘	bæ$ä:ìû¸,x\0ãP}Es×V×NÁ5O4¹Ø¢ÂOç¹ÏàEWºSÈD:þ±jéÕäº”/þEh©®?YÒ%¸ñÙ£Ôg·µÖl.á‰UƒydNAÛ•“náè?ïi­ZÂ×?ð•BÖÀn/sb…ú£(ý+\ZÒú=ÆWW77Wz…³ÚEÕôÈ1g)%•\0lRHÇnO4Ò[øoNµÕŽ£\nÊ­¹a%”+:§@ÄgÜúšÖ¬›hº•âZZ_,“HÆ60]¤Œ6=kPEPEPEÎëš¬:ý¼Þ¥´Ê¢ÝÞÜKòå·Fç ©Æ6ãç©â€)xíµ˜NŸe³1³šèy±oy<²‘ð1Ã`óŽ•¨ší…·„¥Ôìã	\r•¹ÿ\0FoÄÊ¼FÃøHà~G¥dê:í¾¹¦ê×—fhÀ’¥²µ,‘ÄÀ;™²Ê¹nqÇSZÑh¾ÖUuHìm.ÒáÜ®\0ÀÊž2\0Ç#<cµ\0aÇ¨ßé\ZõåÎ¬$»¹Kh•~U†§,ë#ç9Ú\0$“·µKá™®té´øRk‰4ýMî¼¨nScÂUÙ‘€ 0R™Èn‡³k£èV·¯i\rš¼Â˜H\ZP‘±ÆÜ¶BƒƒòŒg*³x[E¾t¹µ7V×²²¤ðÎáÐŒ«(ÜOËíŒt ïßA¡g=´7Z–âârøÄ0¸*Tœ3ämxÏÍv	\ZD9!F2}k•ñŸ§Xèï¥CpðO¨)ÞÑ[›«‚§;½ÎOVàg,sxžÛBûVµ}aj\"·ó\'’vyT’\0-·wn„ÐPQEfèT:Lm«œÜ³1P!c°>Þ7mÆqÆkJ€\n(¢€\n(¢€(k“­®¨Ü:†X­erB“Mðí±²ðÖ—jH&8c$t8@?¥^–(ç…á•Ç\"•uaÀðA¬|dˆÜßêW–±¨X­gº>ZÐa@-ŽŸ1n”£q¯hÖŒÉqªÙDé÷•î0ü3šÇÔ¦·×)£Á,—ò)þ×ä²GÂ2Å˜\0ß&W?{ñ«†]ÃÒ-¥Œ+tË•µ±L¬=H»=ê‹ê6òOq~š**“6åd|ã2FqžÊ?àF€\"ŸP’ÏÆ×m…ÕÔ_`ˆ\\eSµ÷¹]ÀIÛéž?\n¥¦ko§êwvð^ßC4’\\ÙÅh›9–6´©Wlã®p8§øcT»¼²mFÞµ\rFÿ\0kÝÜÅ¹U³q–€$ôË5+Ý:ò;EÖ÷öWKí\Z—òÐIåÎ®Wå*\0cŸ`hPø‚\rCÂz…ô‘=œC\"\\[Ü|­~éúä{‚=kWJ…íô{(d<vñ«Ô \Zª–\Z±tš´PÙ^Ê¸Ûp…dtäpHì{V­\0QE\0QE\0Š¾+ðýÕÇØÅòIæ¿’ÄÆcüLl$ôÆy¦ø­ØØÚZÞMíì6óí$˜dv8Ú}‰­I¬,î,\r„¶Ñ5©O/ÉÚ6í\0±ÛÒ€9ÛkÈ¼%}u¦MÏö{í—NX¡yqwÂ»AÆÙ¸àpûK;?3ëbÿ\0H»YLs+*Îœèw!#³u½ª+ŸíÍöÃHVìWŒñÇyqù¡ ecÉm¬HÎŒü¼‚k¡Ó4è4>++rì‘ä—‘·;±9fcÜ’I?Z\0Î_)s$º¾¢ÌÇ,!‘`}üµRO“ÃP¶:¦­åoäoÑË\nÙ¢€9É|3¨8HÓ^e2Í”&dçåp\0ßm8êþð”±èòÊm—ç4Ò+%‰y?¾Å[©çº\ZåüRÿ\0d¾¶ºÓ$/­ºˆâ²2ÝÆ$8þ\\“¿¤÷ÎŸÛM~çÄ\Z›Ï¹ÝöX%°“ò€½™€RÄóÛ€+x2¶vppp{×>Ö\Z¾¿µup4Û÷¬í§.óû<€/û+×¹íPjzmŸ‡d²½Ñã[%»†ÝíáP±\\«°RzdX7^=	 ¢Š( Š)R§¡4‚5­KW‘Æk¶Ê5õáa„\Z¯,3ÆìÁÆj­î©	²›KñŒQX	Rê)È“!–LŽ‘ÁÕï“ÿ\0Žš½U!Ø‡ûÈ	\n\0þ5§h—ú}Í”ŒU.\"h˜Ž 0#úÐ%õî±qc¥Áâü]£y:‡ÙÚÖ6\n(qû×#$r\0œt®É[MðÖ—on\\Å\n‘K‚ï#œœ\09f\'$÷<šÎ·ˆkÞ›J¤öBÅåŒèG\"û}ÖÇµ\ZišÎ³©[jZ].]=m£†A0yaÝ²1·×“È \ZmñÞBmg…nmcÊ’¬Q†3œr§ŸSPézÕµíÍâOúàµÑtX²ƒ‘œ\0¢Õ|+ªkÓ+ßë[2!{s•8Èf,NÓ•qÖ®C£j‰¥>–o,a¶XÌQýšÓi+‚9ŠÈÐõÍ}HÇ¦jÏ¦_EF—(‹\"26	­ÁPAàŠæt­Oû[HA­ø‘Ú&ógŠH™Ñ$Àd—\04lTŸ›­-7O½ÕôÍ;FÔlî-,¬-£Žõ$Àr*…Ø?4|\'ø¾QÓ\"¯ÙÁ±®ï³F,´½öÖyQóIÀì¦Ñÿ\0ö ­÷ˆoÀ¸±°´¶¶\'äKçu–AêBƒ³>‡\'Ô•{HÕWU¶vh^Úæ1\\[ÈAhœvÈà‚ Ž ƒWëKÚÞ.×X(R©mÇñaY·ãØÿ\0€ûPåQ@Q@r÷M¨ëZÍý”7“ÛAc$qyVò¬M&ä½œ†`¿6Ð*yôê+\'WÑEä‚þÅþËªB˜†áz8„CÓ94\0ºVƒm¦¨*£y;˜)8-êÄå¿ÚbO¦+JX£ž3Ñ¬ˆÝUÆAü*®‘¨®«¥[ß,f32e£=Q‡¿~š†¯e¦l[‰}/A/$§ÑTr~½=h6÷ÄBRÃO±»¸–à<v¯AbgPxH<ôàã4ÍS±Òt‹}/Q)¥ÜÙÂ‰$w2*‡$}õlá!¹ëœä\n¥ge‹µ[·Õô÷†ËLu·¶±w´1vq®ªHúÓµGÐ5-^;D[`~Ët%Ì‰oœ0Ý˜p ‘‚sÒ€4Hð¦¥p6[éÒO&XUÜwåH$TzfŸi¥x¾î8>ËÖ18D?$¬Ã6;\n}ÕRãÂúx²8bÓmáÙI$þLb3¸<b7R¸*Ã/Èçš×Ót&±¿7³êwwò¬>DFãgîÓ ‘ò¨É$““À \rj(¢€\n(¢€0nÉÖ<KŠÚéL·-Ž³û´@wŸø­oV€âßTÖtù²·?k7C?òÒ)\0ÚÃÔ\r¥}ŠÖí\0`øÎÒKïÏl°É,nñùëE¼o(ñmÎ;úsŠÏÒmí®¬ M3Å7ÂÂrV%GY\n‘üWBÀñ÷OÌ9ÇJë«œÖti-ïUÓ¾é€[Ë9ßdW@tlÿ\0ƒ6:pz\0,h²×pKzøû÷WJOýôØü©„4˜þk5¹°ïÚ\\Éèâ*”M¯ßÃvö&Ú Iónu-å½¿v	8ÿ\0x~5.5í9™æ®b+÷ì¦.ËõŽSÏü³í@ÜÛê:jÄ@®0©l²n>ƒfÆ\'óªÞ3IwªÉ}$w7âeIn£]ªWh+^vÉry9\'štQ_k[#ò®ìíF|ë›•	s(=QýÅõ<ÀwöÖ°Y[%µ´)1Œ\" À€%¬_+%…½øBë§ÝGs \'`Èr=Â±?…mUmBòÛOÓç¼¼uKxc/!o@?_¥\0NŽ’Æ²Fêèàe9„\ZudøZÖ{/iÖ÷(c• Pcn±Ž¡O¸…kPXÞ\'fm: ì‹s´Ž‡¨ÇæÁìHg¶kf³õÍ>MKI–Gp¥eÏðÈ„2Ÿ¦@Ï¶hì0Ço\nC\n,qÆ¡Q`(\0\n«:Ûê°´0ßºª6$û,À7Ð°ä~\Z]\'QVÓ ¾\Z1(;£n¨ÀÊ}Ã?\n¡®Æ–×zv¡„¸ûdp3¨åÑÎÒ­ê9ž„\n\0g‡4t[­Qíãh¡¹œVœX’Il’y<oUORƒLHä»&ÚC±î	ÂÄOBÇ°>§€qëLÐï_PÓ¼ö5|Ù9°œˆWõê84\0ø¥kwû™£0$¸<„bYxú…éì}j-w½Ó†¡+¹k¦.žA!TÜuõ9¥¼ƒQŠõ®ôå¶‘¥ˆG\"\\;.Ò¤•`@?Þ9øäwŸK°]3M‚Í_Ìò—æ|cs’qÛ$ž(Ýs¾ðÒé3‹Èe7»¨›)&\\àì9Q‘ƒÀš|ºùMJòÙeCqñÛÁfF÷c\'®0[‘À\n>\"¾žÎÊÞ;fòå¼ºŽÔM€|­ç°zœt÷\"€5ráXšäÓRÓµ¨Ù’xígÇlR°@®•‡§>¦µ,¬ °¶X RrYŽYÏvby$úšÊÔ[û_[·ÒáÉ±–;«¹;4qRNú\0=E\0nÑE\0QE\0QEÏ­‡ˆ4èî-4ÆÓÞÞI^Heœ<Ø±@!ðIÇ+Æ½Y[{\rØO1yç y·7MpÝþ\'\0(àd\0+^¹‡»}cS[¸pö°H`°SÊÍ8Èyª S»ÔP†…Ø`t½š¨ÞJnn\"W+6Ð;€¡W=ñžõi¿âaý¬ˆ¡rÐàóPuÿ\0¾¨M*Õ,ÚØ¦íãç‘¹voïëß=«&ë]¿µ:dV:\\w·WÈï6éÖc\nAËsÀôSÏ—âCPðÄš&³œnÑmZÒò3&Â€„`ÙéÁVÏ¶zu®‹Mñ¶¡qöI`¸±¼ÚX[Ü¦ÖuË!«ŽG*M9ŒZþ‘º%hŸvBÌ˜h¤S÷X}F¨\'¨5Ïj+ÿ\0ïˆtv¼ž8´H\ZSÄ­³»!Q	?ÝÁ%IéŒvÚQLŠXç‰e†E’7WCG±§ÐEP¦<a¡ÉHîbãï&ÕlßJ+v²õ½&MJ8\'´¹6·önd¶›nå‚\n²÷RúÒ±EÎ½¯ÝM _Û[iéD÷Ó[Ü<ä}ß$chÛ»až@<rr\0,Ë®É©ÊRÆI!´çl°GæMqÛ(9¿í·^ÜsToüCo /™sáû‰\"R7H÷K9è3´¹c×¦k¨ÿ\0AÑì€`8\0r}\0êOëYWþ3Ó´§C¨[_ÚÀî\\ËjÂ0IÀÉíøŠ\0Ý„«BŒˆQXd)]¤gžGcO¢Š\0å®<E£¶³uaka}qwnû\'{5UˆÏvRÝsÀ5<~$ŽÞQ4ƒê§„Åp?Ý\'üŸ­K©jþ¶Õï“%ûÆ£[V•ööÝµNÖ¯ÛÜéšœg‰¡ÂëÈ¦Tò?@mî!»·K‹yH¤V^†±üF¯4f¸¬VøyËÛqVöWñ ö¨n­ÃKOŽY,$|ßÛgØ1þ¶1ÉÈÇÌQÏP).õ+?@š~”ÍuË“] \"(Ñ]_†#Ç\0gÉé@%Q@Q@Þ+mæ”FÙl®¤8õIÈôÃcê¦­êq‹³[`Ó¤œö°|þ`Æ©kÖ71ÈšÞ–…ïìÐ†„.¢ÎZ#ïÝOcìMiÛÜÅye\rõºoYaEž	V\0íž(r{P##‘Y1j³_èÐê\Zb‹‡Wtu¸áÓœm`{tç­7A[¶ŸP¸–ÒK+iæV·‚\\o(Ä@ËgŒöÏz\0»%ñ\Z´v…,Öï3OË†P¿ž[ò¦è×sÞé0\\]*,í‘\" ;Uƒ@Ï`EC¨i7ß­ý…÷Øî„&f‹ÍVRr>\\ŽAä÷9Í[Óì£Ó¬!³™Ö%Áw9g=Krr\Z\0Ÿbïß´nÆ7cœV_‰!wÓâ0XÙ\\GtPueF€÷Ûœ{â£±†í5›©nì¤’F˜ˆ®w®Ä‡(9 äÔç¦*ÅýÊ¾£m¦edQÉçÅƒ‘\\nÈ<|Ä}ÇÒ€4ÖDWC•aGqXÞq4z•ÊŒÇ6£1Gþø\\&~™R?\n]nêuú.˜Lww ¨•Gü{B¸\'Ôÿ\0´G`kNÎÒ(lí£ÃE€=Q@Q@Q@µy.ôÛ«hf0Ë4.‰ þA\0þk#Ã6ÇÓ¯, ·ŸGD¶W‚Rèà¢œ®@ c¯A\\ÔÚŒñ%üº™¦¢±IÀ‰™E]ŒŒ@;N‘ž>”ÒÖY¶¶[‰l¯!ŽKyäó¡¨eÜyaÏCœ·üúTPø»ÃóÊ\"MZÝXôó`?BØµeŠ+˜Lr(’7ƒÐÐf§áøïô›Ëæ0‡I•B¡pã*vŽù5£Çs¨CªøgUßÖÁfóa³µ£sËª²dg‘÷NqÏDd›O½-5¨ëO/ÿ\0kûÃß¯¯­fø˜O`!ñ‚,’Ù\r·OÛ±ù\0ò¸±õ \n~/Sh£Hà2Àÿ\0lµ‹„Šæ\'UfQÐ\rž:€\ru•É®§¥éž&ƒTv	ˆíãHçê¾b@\' Ü¬0{ì®²€\n(¢€\nÄ‹}·Ž.nc½ÓÒMÙåZ\'*GÐ‰Gäkn°õi\r—‰t{Öâ¼Û\'lðö²~f<}XzÐ¿ÙãûGÚ- RNvŽø³Tï®ôHnjÌ¸Ú.Lgš³uó…Ž9¼”\'÷Œ¿|@{}/QVçAÓ®´ÙtæRÞe+\"ª‚\\ÎIçß¯½\0h‚ ô\"–¡´µ‚ÆÎKhÄpA\ZÇ\Zª£\0sì*j\0Ê´5‘qq5ŒZ–À¹yUd+Øœž¼Uë‹h/#`Î9GS†Sê§µeiž°Ó5;ëèÿ\0z÷³´ïçFŒÊÇñ»hÇ<f´%±	ûÛ-°L9ÀáÙ€þ}Gé@5›™´ïêÏ\'šÉ,,«‡rF`q¸±Ò®iv†ÃI³³lfÞˆãý•úU|ù²é6a€yïãm¹ê#Cÿ\0 Ò¶h\0¢Š(\0¢Š(\0¬?)O[BI>L“CÏûºÿ\0JÜ®EoìVûF¹ù÷]Ù1YQÎçP¼¬X‘×Z\0µ¡FÑÅ¨]ÌQ\ZæöYGHÂâ1ÏÒ0O¹5>—¨›ç½Œ”ciraßÝq±\\îàûƒO„ª_ÜY¬H#Ø%8K–?\\gó©íí­í!ÛA/D¨ü\0dÜ»×o¡Ì¶¶1´j‡”‘Ìœÿ\0½…\\z~5G®´k+†1¤·Ff=ÉQŸÖ©.€é¨_Ü®¥:Çzâ_-UAG\nªìd°½99Í_Ó¬Í…’Û™L¬™Ÿh\\–bÇÓ“@éíªiÑß…ÀßïåŸºO¹ûgJæ9!ñ”ð’ÂêÎH§_îª0doÍÈÿ\0JÓ´°´°W[KhàY»Ô\0IïU^îÒÖÞãX¼‡|Làv+\0É=‡R@ \nñ¡“ÆóÉÔC§F¿Bò9ÿ\0ÙmVV‹opd»Ô®áò&¾ua	91Fª«µÔœt-ŽÙ­Z\0(¢Š\0(¢Š\0(¢Š\0(¢Š\0d±G4m±¬ˆÃ®2÷‡.‰q£æçÃ˜@é4é$\"G}™Ï–Þ˜ù}Gq¿E\0RÓ5[]VÓíå—i),R\r²Bãª¸ìGÿ\0_¥\ZXÎ›\Z™å_Bˆ_Ã¬Í{Kx.·´è—1¦Û»ld^Â?„ï¯U?‡CÆÍÌ–PÝZº¼ xÙz#Š\0ã/O–	‚4\rDÇ,dp°ïÛ§“%u>yäðæš÷N^fµRJŽ¾õÉx‚Úa¦xÞ\0¤Ç\"!‡Ÿ¼ÏÈÿ\0¾º}k²Ò/ÿ\0H´»6,°«lþáÇ+ø?\n\0¹EPUu>ßT°šÊè£©Ã)!ìA\0ƒê*Õ‹¡_Ý‰¦ÑuW¨Z(a0EÔD²Øñ†±{SÔíô›O´Ü‰X‰eÝØœU“Y~$Æ{§x”˜lâ¹*>ä2\0þŠÊ„ûdö­¹a†îBC)¿PAÌPm+W´Ö y-ŒŠÑ6Éaš3‘7£)äzûÕÇuÝ‚ªŒ³€­Aoammq%ÄH|ÙUVG,I}¹Æsõ4û«Xo-ÚÞá7ÄøÜ§£çÚ€3ìüGay©-‚-ÌrH…ày dIÔc%›žkBîîÞÂÒ[»¹–!RÎìpTiV°\\Gq‰$–$1ÄÒ9o-N2=3Ï^*•ücQñ-›’ÐYÄ×r!V|í‹?Oœu”\0Í2WRMvò¶\"hì­¤tV#tŽ;3``vy$\rÚ( Š( Š( ²¼Ki5Þ…p-cßwÛ×ÌC¸cÐœcñ­Z(íkq‹Íÿ\0È!¸U8tCžªyW\\’2;ŸlIe¢ÿ\0djq6œe[IQÅÌrNÎ7ñµÆâHcÈ8ëßµG¬ªiÚÎ­`${¥Ûü÷{	ŒöÜ}êm>á®<G«Fç›ah¿ì•ÝŸÄ’?à4¯Tµ{‰­4‹©í€3$GËÈÈ\rØ‘è:ÕÚd¥V\'.Û)%³ŒZ\0Ë‹N¿°­µô÷FE),—O»\rD€vÀÝòŒG¦j²5k›k2ÒXiò‰f¸ÎVYTåTâ!¾f=ˆ©8hÕåŸÁ6—*ÍöÝBÕÜåtàÿ\0ìÇÐ{VÝ¬V6PÚB¡c‚5E\0c€1@ÑE\0QE\0QE\0QE\0QE\0QE\0V\'‡ƒ[Üêúü²¶¾&.0È‹&Ñ«i˜*–b\0$ž‚¹	uY¬|36£j7j:åÁ6Q·–c?„jð \n\Z…æ|;­ßLí-½þ«Eå¡c´IX©á2=Íuú¤¶zD1N¡ebòº/D.åÊþ±øVz[As«Yi¶Ãu–Š¡ß¡nÝ±©÷\nY¹Zè(\0¢Š(\0¢Š(UÔ«(ea‚È\"¹¯±j^ËéHú†NŸÿ\0-­Ç$Ÿ¼¿ìA]5‡oã/ÜC©GˆpñÜ«BÑœg\Z²|K ¨ÉÖôáõºOñ¬;É/toOWrAkª4#ìfåLÊ¸|¤2ü¨‡:ò*=CRžiäíÊ®#ÿ\0‰á‹ž|Í– r{ÐÃx¢ÖàìÑá—Vôkaû‘õ”ü }	>ÕkHÓçµÜÞÈ’_]¾ùš?º p¨¹çjÌ’{ÔºMŸö~‘ig´)†F\0ñ9ýsW(\0¢Š(\0¢Š(\0¢Š(\0¢Š(;‹xn­ä·¸e†U(èÃ!ê\rr\ZÎ“yáh¥ñ‘¨O/Ù¡X¤´»j4;ÿ\0¼>1 ’Ç\0ŽõÙÔ7–±_YOi6ï*xÚ7Úpv°ÁÁíÖ€1­õmbtW‚=ñøu÷ìÿ\0:ËñúÆ¿,~µº°´±¿Ú¤·‘®(€a0;sîqí›£DE¼š}î—¦O{§?Ùäß¤Èî\0û…™ƒ¹pÙëÐVçƒ [‡¾ÖRX¡ºÙ·ÙmÌ(ñ $6ÓÏ,î2{(Ç©¥è1iî—Ì÷wI–’º…X“–4 àtäã’kVŠ(\0¢Š(\0¢Š(\0¢Š(\0¢Š(\0¢Š(\0¢ŠÏÕu‹},G÷“ä[ZFG™1<ÀwcÀï@~6Ô­­to±Mq±®ÝâGI!Þ¢M¹ï´‘øúâ£>#Òî&ÿ\0A³ž]Vò­­ž	éÈùUzdç öªZ^·jÿ\0iŽæÊóP×®EÍ«[ðƒ$÷}ÄŒòO<žI­­@’êÃgÌ«åˆìâ	iœã<9,qìp\r\r.Äiºl6¡·º.d“¼Žyf>ääþ5nŠ(\0¢Š(\0¢Š(\0¢Š(/Ä:$zö˜mZO&Uu’)†sÛFTàŽ	ªV^¸\ZŒW÷1¼VÀ˜í¡2ì/‘‡`îÀ•ÁÇdŸJèh Š( Š( Š( Š( Š( Š( ÛÍL¿¹k™íÏœÊÞ9^2àt\r´Ã“×5~c·…!†5Ž(Ô*\"Œ€\0ô§Ñ@Q@Q@Q@Q@Q@Q@ºæ‹¹f¶ò\\Ü[˜ÜH¥áêÞò\r`éÿ\0lm¥ó.ïg¹\"V•V0!Áläo_Þƒ€à\nì( \nöVšu²ÛY[Eo\nBŠ±E\0QE\0QEÿ','\0','2012-10-31 20:50:25',NULL,'system',NULL,1),(3,'Chiropractic','.png',600,300,'‰PNG\r\n\Z\n\0\0\0\rIHDR\0\0X\0\0M\0\0\0Ü•i\0\0\0sRGB\0®Îé\0\0\0gAMA\0\0±üa\0\0\0	pHYs\0\0Ã\0\0ÃÇo¨d\0\0\0\ZtEXtSoftware\0Paint.NET v3.5.100ôr¡\0\0£#IDATx^í¸%ÅÑ÷_’ ‚&°¸n‹kpûp²¸CÐàîîÁaáÅÝ-@p[<‹-aqX,HXèßä­›¹sGZfÎ™™Sõ<ç¹rZÿÝ3ýïªêêÿ1*Š€\" (Š€\" (¥\"ð?¥–¦…)Š€\" (Š€\" %X:	E@PE@(%X%ªÅ)Š€\" (Š€\" Kç€\" (Š€\" (%# «d@µ8E@PE@P”`éPE@PE d”`•¨§(Š€\" (Š€,Š€\" (Š€\" ”Œ€¬’ÕâE@PE@P‚¥s@PE@P’P‚tôèÑ†Ïwß}W2ÌZœ\" (Š€\" ô=K° QC‡íû¬¾úêæþç¢Ïä“OnvØaÃÿøIº×_½—æ…öµÁ|ÿý÷ÑF!þipw´éŠ€\" 4ž#X«ûï¿?\"QB¨øù“Ÿü¤ßgÐ AÑßcŒ1F_:ˆ‹—Š\"P\'˜Ól\0˜Ÿl\næwÞ~s›ùÍÿ:ê(óÆoÔ©éÚE@PZ‹@O¬Ë.»ÌÌ7ß|ÑâZwÝu£Ï‰\'ž˜9À<òH_:Ò²x©6«µÏC£:††\n\rkr³ß8$\'-E@Pjè‚ÅÎÅÔI\'ä*$‹Em€Š\"Ð- ùB¬˜Óh[eÃÀü¾úê«û}vß}w³ðÂ›ÿøÇÑü½ôÒK»Õt­WPž@ \'f=4W,DW]uUðÀŠÙP©`(µ\00B’˜‡*´¬¶òÖ[o™)§œRI–-`šNPOz‚`áÂ¢â²åá	IûÑ~-RêÓâ9ó4›¢‰Eå;Ÿ!Yë­·^4Õ\\è5šIPBZO°|ðA3ÅS•2EHš1E ˆY°¬ù<xðàÈÌ¨aI:1zZ‡\" ô\Z­\'Xh¯òœØCœrU‚ æuA\02æ´37õ²é <ž%Y.#¡iE@(F Õó»ý*…òÿßÿûUV¡e+‘)2üñÇ—ŠÎï”Ë	[E@Pòh5ÁÂüQ5ùaÁ£E J˜Çh›Ê6uÓfÊ]c5ªl¾–­(Š@Ï!Ðj‚ÅÎü„N¨tPÿ÷ÿ7Ò\0Œ1¢Òzš\\8æ§ÿûßæÎ;ï4_|qß‡¿ßÿ}\rÞZ0¸à7Ùd“™]wÝµ’i\0yc«(Š€\" ”‡@kßª\rP•òðÃw¤ž*ûPeÙ/¾ø¢Yb‰%Ì¸ãŽý\\ýõû>ü=öØcGÿÇ¯H£ä§æ;æ2s­\na¢›„*Õ2E —h-ÁÍÒÈ‘#+_§wÜ±òzšTd‰…{žyæ1çwžùðÃ3›ë­·FñœÖ^{móÍ7ß4©›ikÕ\Z&yVªÞŒt,­DPš Ðz‚Õ	œ!XUûzu¢eÖñ×¿þÕ¬¶ÚjN§Ó®¼òJ³ÓN;•ÙŒV”U5Á÷Nh{[1Ú	E@P,h=ÁêÄñó…ZÈì¶Ûn–·?Ù;ï¼c–]vYóå—_:w–|O>ù¤s¾6gÀÿªJ)ž%XmžAÚ7E@è­\'X0{pt\'êéÆñ©óðÃ7gœq†OVóøãGæB•ÿ\"\0¯’`©‰Pg›\" (å#ÐZ‚…ï‹RÕ>X<ð€îþó“VžÏUÑ4^guŠ’ôÔ÷U›•`õÔtÒÎ¶o¿ýÖ<÷Üs8Àl½õÖÑ‡xwøÄÞ~ûíæóÏ?×Eš­%XÜÙÁ\Z=zt*”Ã‡7^x¡áçW_}…H¾“´÷ÜsÏ€òþþ÷¿GõøÞ×¡qîh5+¯¼r„©‹`VÄÿ\n¿­µÖZË%këÓ*Ájýk 85¿Ï>ûDïÏ=öØÃ\\ýõæÙgŸ5|ðA´Ùåš­{ï½×œtÒI‘…`£6ŠÈVÖúÔÍÜ‡@k	„‡q³®á;ˆ‘|ðs‰O6Úa‡Ö/\riW]uÕ~ÓGvÿ:§þ‹\0ýë_ÿ\Z\0	\'Ï9çœÔØd\'Ÿ|rtó¢‹.2‹.º¨“s|Û±—0\nU],®\Z¬¶Ï í_[5jToÓM7uò]ýè£ÌÑG²ªÂ¿´s—~µ–`w¬e-Jøúl¸á†ý>qs\"‹ÚXce_|ñ¾4ÓN;môw\\Äyß}÷¹àÞê´l°yê©§ôñ™gž‰H¯ø}ýõ×‘Ï¤jóÍ77ÓL39ðÀÍ;ì=ü*ÿA€ÍB•Nèi¢cª(å €{\n‡‚BDk›m¶1tÁ¼¨R.­%Xâ•ŸŠxK˜±Ozè¡f¥•VŠ~GÐdÝ}÷ÝÑ÷ü_¾{õÕWSÑŸrÊ)£HÛ*ÿA`èÐ¡fÏ=÷\0*êå–[ÎÌ2Ë,ÑwM4‘™~úéÍ5×\\©¯ãfZÔØ/¼ð‚Búàè^U¬5!XYó[APê…ÀW\\a6ÙdóÙgŸ•Ò0ÞÙ˜\r]];J©¼Å…´–`a\Zd×ŸE|8ù7ÊïøT¡=Iû.+Öš²*Oy5mþA”æ›o¾ÐXM2É$fÖYgÍì¾úÓŸ¢]~*ÿA€9VU¬5!X<*Š€\"Po®½öÚHP¶ÿW—aÕQMVyãßZ‚DƒÎ$>7ÜpƒásÚi§™½÷Þ;úÉ•-1­ðâ’vÏÄNŠ8ÓcúRù/8P.°Àæ“O>‰þÉC‹Ö°aÃ\"‡Ë¼‡x—]v‰®ÓážB™¹!ð“N:ié/UðÝvÛmû6Š·\" Ô××\\sÍÊH×rá,¯R­&X\'žx¢Á|g+˜©Ô¥¶>@²û×ÐQ¾ùæ›Íb‹-œcŽ9ÌÒK/ibÆcŒ¾l’ß­½öÚ+óp‚íX¶)ÝÛo¿ÝG€Ž8âˆÒ»6õÔSGå»<+¥7BT\\°¬¸âŠæ½÷Þ«©í·ßÞüío«´Ž^)¼ÕòÃ‚n+b\"ÁckŽŒ±8á˜­2%—\\2:IÀäwÝu×‘ò¢øâ‹/d8ê¨£\"í¡šûC#ÚØ*HcC¹z>ÁŠ@}À¢Ò‰€Öøu­°Â\nz’»„©Ðj‚%~X÷ß!TØ³ÅŠ l˜clDbÙ2›2Û”†S—É £8hJð»´øc;ï¼³¹å–[ÚCp_˜_ƒ\r2O<ñDpYi€9þ‡*Š€\"P?8”µüòËw,@è)§œb¸£T%V, Á7\n-S‘ 9!L\0òè£Fd_¡\"ACFi§æŠòöÂ÷¿ùÍo2»9dÈ3ÓL3E\Z®óÏ??\"b|>ø`óÖ[oõ<Ö}ÄÍ\\c#\0VÊ ,>l(:±;m³æWz«¯¾:Š!Ø)!Ž!1²TÂh=Ába²Ñ.%L2EÄL´^h¼TÒ@ë’\'D\Zf_{íµÍ˜cŽi~þóŸõgˆ˜hcçœsÎ>‡÷-·Ü² 	Qzé¥—/æ)\'„$\rê>Ìy9-Ë&S¡Š\" ÔYT´“BÞÏ*þ´ž`±+Ÿgžy\nb¡añŠ/>EÄLÜuaJ‡—Sƒ¿ÿýï£/1òw\\ÄtøÊ+¯D1]ˆ{•ueQá\0ö@ÈjZøäÿ’æm!eñt?ûÙÏú‚¾òÕÀöÀÒ.6b1vZ.¸àƒæLÅž X61ªXàYtdúÉO~R¸èpç^‘†ÆhšŸ“äÛm·]Ô|®Ý‚Œ‚€hÄ\Z’¡x¼9 À¼dŽ2ï˜³|f˜aó‹_ü¢ïÃßqá ßó“ô7ÝtS¤­ÂŸë·¿ýmô?E@¨\'ÜÓºÕV[u¼qÜ¼A„wZO°ÄŸŠëXŠ„EG®\'Ÿ|279Žó,t§žzjQ±=ûý\Zk¬ÑwÜ—ë¦›nº~qœˆ·BPQ|°Tì’U¤•åÍ¢9Ï†°©(Š@}àÝi6¨Ì^pš;Uüh=Á\ZHA/m¤ˆXI„eànB•tÐ^-µÔRý¾d7„¿Úwß}ýÿÓO?L²¯½öšÂè€\0ñÝŠ´²Ä+\"ÿÌaõt\0^“*]@\0ÍÿYgÕñšyO¯µÖZ¯·MöÁ\"¦RÞ‰ˆáÃ‡›óÎ;/õ“qœÿáç¢¾WéæÜsÏm^|ñÅ	Yd‘È\\xÜqÇ™¿üå/ÑÎìôÓOïØñã6<¼¢•½çž{2»öwÜqGæ÷Ìa´`zr°\r3BûÐfîºë®èV‘Nïµ.„¡ÞˆÐbe]ÏÂýxYÎÃÄJŠ8·‡AßÞÜÄOÙu×]S;Èíí[l±Ed\ZDƒòòË/G§<5î•Û|`¾fÂ \nsünÍ´’e+ÁrÃ]S+Fàé§Ÿ6x`§«Nr\'¬Š?=C°`âÄºJÔ¯,ø|0û5ÖX}ã`˜®Pß•ìI·êª«šx wV¾ùæ›æ¶Ûn‹Ò|ùå—æøƒ|©Ø!G°˜³3Î8c4‡³„4”¡ÁEíðÖTŠ@·Àjƒ\r6èxõøhbiPñG gÇMÑTÝÎÂ“FªâS>.*ÀŸ\n3`‘pç`ÜlË‰6þN†r(*§W¿Ï#X`BÈ‹Ï?ÿ<žÊP\rV¯Î íw“àj±NË1Ç£w‚Þ3œÐ:]xá…™Bãà\n\Z\\\0>UgŸ}v!‘¥‹³Î:k_:ˆ´î˜ì¿ˆ`‰‰0ëÒÖþóŸJ°ì ÖTŠ@×Øe—]Ì?þñŽ¶KD7Ft´Ñ5«¬§\Zžå–[Îy²Šæëî»ïV‚•3‰×_}3Æcä:XKö7ÞØ<þøãfï½÷6K,±„¹á†¢ˆîýë_kö˜Ô¯9Eké¥—6ãŒ3Žyì±ÇR¯>XõSm‘\"…\0ïÉ}÷Ý·c\0qM¾²*aôÁZxá…ûœ1óÙ.äìðB —;‡ÁÞÞÜ;î¸ã€ðE½% \'e¾úê«è.ÂÙgŸÝ<ÿüóEÙzú{\'5³„9‹™0KÄëÈ#ìiµóŠ@SXsÍ5\rþXm·ÝÖ6¬UµºŽž\"Xh°0_ñ‘ˆØ¬<¿,*ÈØTSMi XêàžýL`‚ÝpÃ\rƒ\ZNÎ2Ë,…¾pA•4<3ØóbaÉÉØ,!Ý/Ò‚5¢V7Ÿw‡Cð³cÑÍûpq¯šzš?ËÒ	ß_B¼`]P	G §Š,8˜£fžyæh‘A;ÑJ~6ß|óˆ\\‘§lä¤“N²º<:|hšY¦§¼Ók¶½Úo¿ýÌ!‡b›¼çÒ‰UVÇ‰À<î¸ãfšÉ‡F?•z\"À½œ\'ÌCÄé;üðÃÍf›m™n†-¶\\¡Â¥ßÜ%™ü Mfà¿C:BÎpi0!T(ƒ?o½õVDÔTš\0c]åÉßo¾ùÆ¬¾úê\Zã±¤éÐs+yjŠÖ˜cŽ™\Z‹ÿó}\\È_t	tIcÓÈbÀçÏþsaÛñeË»Êå‹/¾0sÌ1‡á\"h•@° HYBŒsÎ9\'÷òlÆ‰rô‚ízÌ0æ<7I°‰ƒa¦á.OñÞyç¥™Íß}÷]Ã|žÕÝvÛ-ŠuD}ÄZºæšk\"R7zôèz€¢­è‡\0\ZK.~&ÌMÙB€hbjLÂòí9‚•æôK ·ý÷ß?\"SñZ®¤ðB«¥’Ž\0»h|ÖYgC@Ò,ó,‹>ÚF²ÖkcA(ˆQ6¢áÊó±âY j\n»3ƒ 1,”l4Ð–ó9ùä“+ÕPäõ”‹×!];í´SDºx\'>ôÐC‘o¤J}`œ–_~ùRIs‘ÃFgžyf}:Ú‚–ôÁbÇº[\'?‹~\r*ýÀG\0DRë—Äé’K.1ã7žùÕ¯~½ÈÓb_Í9çœfðàÁf‚	&ÐØX1\0™ÄÄaæÅk;à€\"?¶´MB|<Æ{ì¨,Æì¹çžÓ)Ý UÜ\'É‰[4H÷Þ{oju¯_ÈN8ÁX2ÿàƒª/—;Œ•ä€d¡É*Ã\\øÉ\'ŸDä³±J¹ôÁâÔ¾!ÂnŸ	-\r&•ÿ\"0ÿüó›‘#GfBÂNÌðûAkÈ-ñø’°Ð`¶ˆdíý÷ß7üãƒIq[ÆèÖ[oNX2ÿ–]vÙÜn±8’æüóÏÏMIƒˆ‰Æ‹<E¦Å¶àÙÉ~0÷¯ºêªèý©‚¬4I8¶ÙbE¼º#F4©ù­l+ÄŸ,4üî*Xn¾ùæˆ¨ñ.V)ž XìúÙ¡Ë\"â«Å\"”\0eÄ}¶˜Ü¾å•?œÝ+‘«qŠn^_|ñÅÍ~ô£èþÁ¸\\|ñÅ†Sq8Çãx;jÔ¨¾¯‰çÔë‚¶”y&sÏ†ØË)C~Ú;Øø¼†È©VË¹ü4.5Ç—\n“x„Sfâ`ß}÷YnC¿ëÚ6^lZ>ø`3|øðÜõkdlèÐ¡†°tÁ÷O¥\ZZO°XÌY,0…`–b·žÔ–ØB‹sûôÓOi_8q(æÊÇ1°—‰+§ò„“˜Y$ÍþB-´Ù}÷ÝûŠ!Ú{¯Š«‰\'ž¸\\í³Ï>VpÜxã‘ÚŸŸ¶Â¼Æ´Ès¿ür§&q[ÿ“ŽÃÌã­·ÞÚ¼ôÒKn™’úÓO?LhåpÂïå÷_·‡â„Ï*Nê¼‹!ôÇ|_X¢3Î8ÃðîàNÞõÖ[/ÒlüñÇÝnvëëo-ÁbAà¨?‹;ó¬ˆÖ.#Ì\'güNTxÙù¯°Â\n=¹tÉ%—,„ó‚ë.R›w§^a¥\rLŸ»Bt˜gÌ?[Yf™eN‡ù§ãs›çˆçI‰V>šB¬Ž8âƒY­WäÒK/ÂG å/ºïµW0éd?± °¡ÂIƒ\n˜s	Ó!‡\' úh¸8PÁ÷˜yù_žKG\'ÛßÖºZE°ØA=ûì³Ñ¢\"»~´L.»xŸ¦üf˜!ÚõS/Ú¬^Zˆ0YaæËœ2§vZgxKbá\\AÍ20wdî2Ÿ~ÿûß[ß8ïJÜÜÒE¢ì\'‰V¯kkÓðÄ—\rGpB+¼÷Þ{!7:/8\'„1rì_¥zyä‘H3w­°©•Û2x¶}ü·lÊ×4?sn+^ú³Í6[Ÿiã¿øE×Å×èŠõP_œhA\\\'½k½uHÏýE8o°ÁæŠ+®pn.GÆ“±Ëœi@æ0\Z\"ÑXqÂÒö*§´îAú§žzêhN–!-Èž´mm[M_®x]pÁ±ê6hŽÒNäºö\'4=q´Ð AùàƒB‹Óü¬±Æ\ZÞë‡‰Ðl©Tƒ@£	Îyœ‚ &ˆ8\0³œ{î¹…~5pš¨^ê/F8o£Yk£ðeÇš\'%ÁÔrùå—ûdmLæ±Ìa6ÌŸPa±GVFTýx[hmm-Ï›u”\r±ðüøCqµ—Jo!\0Áò9>Y*Õ ÐH‚ÅËœ“9bJá…ýŸ]v·\rDR[ÅbD˜Yh³,Hmq%v\n‘§³GXÂcø^JàÒ\"óc·Æ¼ŒzÏ:ë¬¾yÌ.ÒÚÖ)—’c*,[h#‡âŽð<‹ô¥-óºlÌ:Q‡4–Y\'®W¡7‹„æ¯\ZõjM£§’ÄŠc¦E‹÷+ñ‰$-ù¿ø÷,~*\0M@<=\Z¾“h ^}õÕ#Lû Z?ýéOû%Ì™”	áÊkG½¦ËÀÖà¸Ž@–@¸ÍWxø	ÝÐ6an1¯ )Ìæq™ÂfÂvÓM7•Yl¿²Ø@Œ?þøÑGÈóÚ—LWÖÐ)X	Vt¢›œ\Z‘\"DHÙ½ž·öòÁb%/q$Tf+H§)äÃKŸc«\"h]Øqóÿ¥–Zj\0y\"|w.‹`<Á1“iøûÚk¯ÍœS,vìüãD‹<M^”ð‘BÃ¿0µ3þ ˜CãX¡þþç?ÿÙšç”¹±–¹ƒv³*D].d‡gMŒá›é}¤nŸ²Z3Øîˆ¬^“êV^yeïÄi#´ƒJ5Ô–`	±Š›\"XŒ VE‹D(Ib h\",hb¶“…ŽrêE…ƒ0DŒÝ9\'AÈß“O>¶ZÚÇwDZ‹8á‚ìA\n›²(ÑW	Ù¥íU_`Ìþð3ÑDy‡Y ®ÎÞm8î.Ú×8±ÊÛ$$wÙ4Ø’1æ¦ÔesãF{ÈßŒd½vÒÈs€9¶˜ß¦T©%XÕc\\Ç\ZˆqEX×S›_ýuäÕVÿà:ŒUí/î}÷Ý·Ÿ\Z‰\"S`P\"!>i§$(’ÃGÌ{!t\\ºÚ)¡­q3mhÑâh0G|9Q&7§R ESL1EtÕÍ)§œâ¥¡yûí·£r‰¯Õd¿ÈÅ&›lÒGv~ùË_F§v\\æ2óÍ¨¦43tr®î±Ç}éÑ¶	‡	¤ü¢ô˜å9I#côgO69«¬²JQõú}	(Á*Äqúé§›i¦™&\"KqËMÞï¬ol‚YûXUªA V+þâæe/‹‘M×!f}ôQôñ½_ÿú×ÑBçº\0Æë$0&$Âf³J^H$uÓoúßVµYø_!ÜAH˜Ú±Bpz†ˆ¹\n¦E‚’(¯‰&B0€XÅcZ…Î+îc^@†Š„ùKz>6s™—-„¨(=»å¸©ž—x– á”6“O¥Z”`U‹o]K_d‘Eú”¢4pù©NîÕlmÖí·ßÞçðËK™«lö±˜ÅM‰,ly‚ˆÙÑ@¸%Ìb²ùÄp¢ßì,Xð Yu®½Ac¿ÚFÚJl,È&‡0\'NNž\\xá…}ñ›¸Þ¡‰&BÌeÌ8ãŒÑ|Ž>KhçÀÆ%R=wVñB¤ÌwÞ9sXD³LŸØ„0?9 a#âs¦$Ë-ÿ4J°ü±krÎÐ÷Ahþ&cWuÛkA°xyÏ5×\\‘æÆö¥†8Kq‡s4yÂnô,‚qL±@öHd	¢“ç¼ž¬‹{žÄ<âC°¤<° Òžª\'ƒkù%b=æ¼¤ðÐâ‹…`*œi¦™\"2qöÙg›	&˜ ú|ÄîA[…VìW\\qÅHu-×m@°¸¢I\"ZæaÖF!~hƒ9F`Pí+óôE›!D¸\"(ÒR&Ú/›ßiDŸºÙü¸\n>‘ò|ºæÕôö(Á²ÇªM)C	Rhþ6aYv_jA°ˆ6\r!)º,8«ó%òrÈ!4iyÐ¦ŠÏÿY@„äg÷\"§ú´:Ä<b«…Ëê»>\rZÙ“$«<1¦}ÏCËåÎ\"h¹ˆîÌeÙ8B3føqQ6aø\rÊõ×_ß/\Z5c\0AkŠ¹²	ð	ÇÏ,~ cã7Ž®J#PñÓ­EfÂãŽ;®Ôà•\'qÿ+ˆl\nh‡+þIö	IQD}Ë×|Æ(ÁêÍYJBó÷&êv½î:ÁÂTÂË[LHvÍîŸ\n×Œ¸õQ¯hYÒòâœÛíÉ÷ÄODðá‡îÒ½Ž¤Ís†&6KÜw\nŸ¬|°_»F]¨‰a¾úê«Žô§ŒJ˜S8œB¬m}Ç8Å“$ZD¾Çy•‹´…”SŒ¹Î|(š—ÔO™Ø\"’Æ ÒnµÕVæÞ{ï5=Ñ:A¬Ø¼•“‡¤òê8Ë÷:”¡«£Ðù6½ŠZš¿¨ü^þ¾ëíLÈÎ8dðò^ø¯PâÒ¶x^Ö9çœ³¬âJ+‡ðY’4Ó:Ô‹DÏ2Ë,†‹t› ²Yp%ûÒ·4¢Å„la*ß5Èj™/Eêò„ÖJ|Çgœˆp…+ée@mÌ›Më:¶Q	VG¥ú6…¾BóWßÃæÖÐU‚Åe¶¼Ü]ü›Ê„š];/ü4á–q4u´XàrùoýÈºÃŠ@£:ˆËe—]æ­-Œ¯é¸Š>ç•É\\*cÎ@´ÐáÉMåßpÃ\rÑ€¬yëÓgÊ‡XAâø:¹À¸bo\\ßâ—èÓ¿^É£«WFº?C	RhþÞDÝ®×]#XâX»Ì2ËØµ´‚Tb¶ÀI>),4j ‚ª‹d÷_·xBYW4¼ðÂ&éŸ…ëšk®‰´0˜½î¾ûnÃÿø=OŽ<òÈÈTÖaÎ m,S 94I˜ã‡9ŠêŒ]‘ßSÜ¬,UZÛ8à÷ñ*j¿~ï†€,7¼Ú’\ZÐa‡æýQ‚UÝLè\ZÁÂƒ—-Ú™n	møÙÏ~–Z}ÝüE {´©N’õ`ruÎÑGÝ¯©h]ŸA\0Ò	\'œÐÌ1ÇÑ‡ß7ÜpÃHK…æ+)DsoÂ@LÊUkg6ÝtS«yøQ	+ò{ÍRÚ·¢ùI›8¸ R>J°ÊÇ´	%råÉ\'Ÿôþ4áýÚ„qHkc×VlLÝÔ^m $@š@¼ÊÖF„L’ë®»®Ð)?¤|Ÿ¼<˜>úè€¬œ¤‹\r	ÃÁÉº—_~y@úÛn»-ò1ãaÒ¡\rW^\0²a(Û¬–‹òmBˆYÙF«&«8/ºè¢OŸç¡Ûy”`u{ºSèsš¿;½nF­]!X²Û‡4tSX|²&ÿ¿è¢‹ºÙ¼u×Í¼B,««®ºj@;ÁîùçŸïû?áò‚XJBÂ°\0sGV\\æž{îZCZcÊò¿²éèÌ3Ïl¥Í<ôÐC\rŸ\"Ò×I‚‘ÖºÍ(»§Q‚åŽYr„>O¡ùÛ€aU}è\nÁÂDÐ	sDhyQ±™tu›x¬:‘>×?þø0¯ºêª}ÿãªœÅ[Ì|ùå—EÃ}ÅWDZÅ¸ÿ[–¯—UH$æ8¯vBÐf™¶}êgžS>RUÏýÊ+¯\\u5=Y¾¬žöàuªnë\\›F±+«.»Ø<óI	VÝÌ+ÄµúóŸÿ<ày4hPßÿ¸OÓi.¹:õÔSû²¬´ÒJ.Ù;žVKTí%£›°hÁ1©LÄ\\ïé2×ñAê\n•`õÀ §t1” …æïMÔízÝ‚±éÔn?†<“[	VÝÚ„‰w‡v\01ÚE¶Þzëèô ‹\\~ùå}ŽÐŸ|òIíNO&û\"Ë6°¨iiÅ¯¨œßüæ7‘)q›m¶ÉMÊ³Ø©u›ÃE6é{%XM\Z­òÚ\ZJBó—×“ö•Ô5‚Õ©Ý~ÁÊ:aÅ¢c£%ðh\\}Ðê¶8½ùæ›QÄï¤L<ñÄ}ÿâ„‹Ü+˜†×äp=N\\ XœDD>ýôS³Új«ùÂÜ‘|B°\\Ç3¤q6„HœáYxó¤“MY§´e!ø61¯¬&ŽZx›C	Rhþð´·„Ž¬{î¹§6±pòœÜYtðÑªJdñË»ª\'YwÝñÃÒo‰×…ÿU^ì.jrïc2ê;~X;î¸cÔýÇ{,\n¬Yg‚RdŠ+³6=Ÿ½N¶Ÿv—é?V&¦M/K	VÓGÐ¯ý¬$â>W×Ïí·ßìÃå×êÞÈÕq‚ÅÅ½.§áX´ªŠ•E;â¾>ñ!—E§ªi Çú‹´ñúëF° GiwHŠ‰qÛ~ûí3!„ =÷ÜsÖ¨Q£\"Írë­·š“O>¹ªa(¥ÜNû`\r>¼T“ž˜}µÊŒ•ŒW ÓN;­¾Ð‹@òü^	–\'p\rÏÆºðÖ[o™×_ÝùÃ»V5XÕM€Ž,W\rjmvë®µ£ªzãíä´œø£±hIÝÖ%—\\úKŠ¬;î¸ÃuÔQ¹Ýúî»ïLÜ)>™Ww‚%›‚ýuBlÉ?§9‰-ßªl€ðõ\"h¬äÜµÉ¯i²P‚Õ›³#” …æïMÔízÝq‚å²ÛGÂË›cþe‹až&¸Mì¶«½øµ\'EŽÈ´£n‹ûòQ”uÖYÇpzð®»îÊÕ`‘uÞâÌ|¹ðÂ«\Z†ÒÊe,Ñ¶uB0ûÙl:ÄÉÝæŠÙÎ&]²¢ý²5ûáåSO\'p­ª6_|ñEåŸ½÷Þ;2©w¢.©#¸*µÜlBŸ§Ðü:6ÙÔ–`É½e,$¶»c×Î;aU%™\\±øáŸDL0lÆsfõ£Ê6¹bGúgœ15êj.Ò† §…qˆgâŠœO<1³zÖæ›oîÓ¼Žåáô sé¸ãŽëH¶›Hí²¹$Ü†ø0VœêŒ‹˜+mê _žßcUàuêtgVûy¸&ªêÏãüóÏ_y=ñ~tjÎW57ÚPn(A\nÍß«êCÇ	\'ÆxÉi%–[n9Ãi´ñÆ/J_EDLtY“«*2Ã‚Ä¹ÝvÛ9)mâÞ¾:‹Æzë­—Ù”=öØÃœtÒI†¨ã!‚V¨îF]ˆ;þwß}7’èy°™öûî»Ïª.´Äy1©\'Ÿ¹Ñª’.,Û¶59¾4SN9¥YpÁ#÷7¹ŸÚö„¤Ðü:&Ùtœ`Éb´×^{Yþ-HIž:šï’»ë¢\nŠ®ÊÉº§0¯\\O>Ym…XúšÁÀ×¹×ï¹\"çôÓOOÍFÔvØFmd~÷»ß™ýë_®Å÷¥GÛ·ÔRKyçïTF[Í÷oæmn¹å³þúë›<Ÿ<ò\'C[¤õ“9H2Nk	‡=(7kÞB¤Æ\Zk,Ã½‘q˜¹ÌgÒ3m|‹Ú¬ßÿ\\\ZVXa…È\'’ç³¤Jo JBó÷Ê~½ì\nÁÂÑÕ•(ðÏÓzÍ4ÓLÑ‹ÛEÓÅ‚‘5¹(ËFK‡ÅQüªXHÓ+êË:¹˜7„8zS6„³‚ózÖž{î¹†C\\‹²Ë.»D÷á…È,³Ì’½#ym	ó8/-sŽï!4i\"š$›N	™c.‰h–·¤@Ð²NÜ¢õryN0%Úh°‹Ú«ßÿLñ¸\Zˆ“;šãóÎ;O!êB	RhþÙ«›]!Xy¾OY½à%ž7„Ø¸LÒ¦]ÃRtÂ0«Ç¸ãzRË\0ábA*2&Ë\'œ”ë5Êdb—Œ{š:4\"VGy¤!Å\\sÍtpœqÆ© å™ò#^Ó/¼i|²®ÿ@ÿþ÷¿Æ;-¤–`f§[\ré cq—ÌYêL#÷yE²ÚX¥_¥\r.mJ3zôhÃÝŸ|ðAÁâk¬±†yõÕWÛÔUíK3Ì0ƒÙm·Ý¼>h¤]ÖL7ºF°\\•…eÂ	\'Ììñ–æž{îèTš­œvÚi©§±$D/-aw>É$“DŸ´øOâ¯âÚwÚ°Å[X/”.möM‹+K›öÀDŽé8¯£éG×æ¼óÎ;ÎUB\\	ZZW¹ûî»b»‰–*Ë^,æ;§ÂDHÏá[å0mŸ	Ñ.Q/‡Ð„ Îs”ç;×ð¢ÅÓhá3úŒ3ÎèÓVÅÃ4¼ñÆ‘ïbÞ-\náµk	u@\0Ó0ãíóÁ­Æg=ªC¿›Ð†®,^°¶‹„€ÈbÁ.š—~Y/f1‹$ËcÂ-¾øâ¥ŽíþÕ¯~å}Š\n‚YÕiJŸŽâàŽ–Š+m ¤ø\\áþüÃp\\‡Û-·Ü2Ú]‹pÂéé§Ÿv®‚õÙgŸ9çëT	=bãE›ŠB„Fæ;fá…Žæ=äÊGcä‚„,®…åwÈ^š±t)Ÿ´²±qÕäºÖÓöôøz¢©«d,âÔ¡EVi7¡)4»Ñ\rë]W;dW,ºI>^ø,4>úhXÏÿ/7do¿ýöëWDÆÖ	ß¦,Ž´›EÓ7Ð¢mpI›ö”‘†;>úèÈ‰\Zs!‹-‹3í„<?õÔS‘ÆŠ¾‹¬¸âŠ^Ucb¬3Áò	JË|HówŠ„æIHyœôØjŒØ8¬»îºÑÇeSÂ¢<Ï<óôib³4¹$Úå*B0õÅîŠÜÓÿý÷fÈ!æ™gžéûg’`‘æOúSô,ª´Ðç(4{‘\rï™ûÛ1°N‰›ãJ°¸–%¹³fáˆåB©Çµ}ií ¯´‘ò¹ô±u†N–\'KÚÚïÐüØüÑN¡À©ý½÷Þ‹Èf¦,ŸŸå—_Þàâ*¼\0â¦2×üU§‡»\r0²ÑHBn0³æ™ž³úÁ•g¦Œùœ¬‡q±õïJæ?,âWõ86©|üÑÇ%-’û‡~hØØp²W¥„¤ÐüíDµœ^uœ`É®×Õ¿‰…|ºé¦3÷ß´»æ=ë¬³£ ä²\01ÙDÛZ8~GÒNÙEú,ò×éô•Dg?øàƒ£°7Ýt“¹úê«MVx^ôñ·-¾hÈ~øaÛäOÇ¸¸š”Å«ÊÆB|“ó¯Ìúþð‡?xûoð,Wmî,³¯u*RÊfåóÏ?/$X$à¹ÜsÏ=ëÔmK‰„¤Ðü%v¥uEuœ`	qEbŸ-W’–V§>ñ‘öç)TXàvØa‡~íd!öÐ!yCû’Ìÿ·¿ý-Š½ãŽ;F7¹¿ôÒK‘MVÖË|Ùe—5ß|ósSpÖ­«‰Å¢ÀÀE:¥‘äDgÏI²o½Þw.£})k#ã‚{Òró™¤äÝEÈ{ˆÓÑ*íCÀ÷$Bó·ÑòzÔ‚åºÛ§»L‚²\'Ùâ×ÅnœSoò7/ÿ3Ï<³<¤ÿ¯$ß~d…•(½‘B²æwÞèºß\'tÒL\rë¡‡r(ý?I	4\n¡«£¸Ü­o?¤‡9†°Jùøãü¯lÛ\"î¾sÙ×Ë¶}mM÷Úk¯EnøW¹,6(h½\\ƒ1·Ç6õË÷T‚Uý,è8Á‚À$}lºYÁ‚Œ?þøÑ\'îßaàoHWÙÂ‚ìû@`*sõõ)»ýÉò®ºë®»šé§Ÿ>:|°úê«gàîE‚µÄKÔVƒÅ]l¾¦®ªæ˜Œ‘l ¸£®lœn¸ÁÛŸP6Lôó•Šœd“ß*OƒE\r<{œìM#gv-ÐTuD€õ„Ë·!Ñ>ßõ¨ŽXÔ­M%X®ñ‚â`AÌÊœ\'œpB´@°P M€L6Ùd‘ÉáçÏþsóÈ#”:fh,|OÓÕí$aâ[92+L}ÄÈr•Áƒð7q-£ªô¾\Z,ÚƒF9ªB¤m¾0¯M´Ù×Ÿr%T›;0¢)Î’\"‚E¾8À”áþ`×bMÕ	X÷ÝwßèöŸO™ëj\'úÛ¤::J°B#ÌŠÜ·U†°kfq`Ë®›(ÛTèëƒF;ëL°lÆ…à¤>§ÙmQ×»ÕdL ê®ò<ØÔ%#Ø(”í‡%m÷}9WÝw|š”‡v6f_ýuÁ\"?šä¢ÍP“°éõ¶ú>ƒ‚[hþ^Ç?¯ÿ]!X>/{&i1­ó˜þ8ÅdÓH˜¯Æ)Y7f\ZññòÑZˆßŽOÞ:<ÜÁÈ‰CX¡Y¬«H(Ÿö	É½«1¯îË.»ÌÜqÇ>ÍËÍƒyÍóÙÇü¨ËmH88R¢ÅFƒE­\\×„©±®›7d4u(A\nÍ¯#@G	‰¯¤¨Œ…HL¶dMŸE$	û\\ÐçëÅï>R…¹Ç§>y0qd]bœUÞ°aÃÌB-äS]Gò„h$…0WáëWuçÑŠ‰ß¢Ïs©Ë~„x¸*«Hl	åpy|Ú]—Euè÷õC ” …æ¯\"õiQG	É‹ÙUÄ¤W†&‰Å€6€ÏFHÇB‚;Th?\Z1ê÷ÔM&XøiÍ9çœN0âç‹•SEž‰C–Ìë*ý°ˆÆ§Œ\rB\"™Ë¾ŽúJ°ì&yñ]´¹ÃÓ…`¡½Zk­µ*óÿ³ë¦*Ð÷chþ2úÐÖ2:N°|“—¹ì–Cb‚	&p6Sq™&õûÉŒ·WÈø’Å&,.lwÜq†ðòË/N)ÖUBV|^ûø¦aB´oyn–\\rÉ¢äNßË<”€¡®fëß5§†6<ñùçŸoN?ýt«^¸,\n$–Ùj«­æ›ÎªAš¨#ø¬©ñ†…æïH\'ZIÇ	–«9$¾HðRÙ‰só¼‰Rœ…]ÛŸbšÄ:dQn2ÁYf™Å¼þúëÖ÷â,³®â:Cæƒ3[&J9ã£9Î*ŸgPæ¡u%ˆB°x¾UÒxÿý÷£ÓÍß~û­D®‹By¶ŠîÄ´ª\\u\rP‚š¿ko@Å\'X®/z|•ˆƒtÈ!‡D/uß…ˆ\ríeùñc¨ÿøã÷Éµ[È,J>‹KÓ	;æÛn»Í\ZC‚*rQ]Å×¯P\Z\n_3›\r&Ç{¬Ùi§ÌwÞi“Ü*´‡¸­¶ÚÊ*¯$\"}Óç²S‡=s€ÂöboŠ÷!Xriô£>êÑÂÞÈò¯ýË|úé§•òNˆæ!J|ós²µ¸øÜþQ—™Ùq‚å:˜˜QÈ#¾P®ùš7&ÈšP?Á4}µhôƒüˆ,J®»~ò6}Q\"þZ)[™i¦™l“v%¯6R62¦>šÕ®tø‡Jãm—ö³yqÁÍçp©§©i¹¹`—]vqj¾Á¢¢ý/·Ürµ5çBÉ‰! Ü$Aˆ™Í6Û¬²Ïb‹-f.¹ä¯Ö/²È\"Ñ¦Õ÷ã³¦ÒPN¢®½öÚ•aÞÌK®bkªt”`ñvLñA€ ¸æg`Ø-C®|4FñmƒÏ¢ïeÒ×r^~ùåÆ,4‰/¼°Õó‚iÄÕ)Þªàù,ñ¿¢)BÜKlV_QDï&Ú¼í¡›6$ûŒV˜ùíò|ñL7}³`ƒ•O\Zvìø}º^kãK°h#¡<vÛm7Ÿæ¶>ÄW\04YUIÈØÍ=÷Üæì³Ïöþø¬©àÒfŸ}öÙÈGðÝwßµI^Ë4%X¼ˆ]óÆoŒ^Â²80ÉgŸ}v\' Ñ^AìöÙg§|Y‰iÏÆoìTV\Z1×rÄ4ƒsj“e¢‰&Š®v(’{ï½7Ú%ÕY|	VÜ/Š¹éJPl0‘yGÙ.Ï]QÙ<O¢%­l<ÐlÙŠž\"ÌF\n³.ñË\\%tÁûóŸÿln¿ýv×j{\"=$‹wQU$+dìBŸmßü!m.š4«•VZ©ÑäŠ>v”`1.ƒÉ..þ\"÷‰dÎKì±Ç.m/š‡§Ÿ~ºhŽô}Oú8QäYT­ù!aÜoÇ%_ÝÒb®µ‰$ÍBCpÒ:KÁ‚]Ù³6óß…üá¶É —g›ç‡r\\}·ŠÚÖôïßxãÈìâ4tÁc3ºòÊ+›Q£F5ÆJÚ_%É\n;—ç.\rßü!mÎ !Wï½÷^%ãØÉB;N°\\|5xÇ5OòR¶5E°Û(S{ÅÀÐM\Z‹Hœ(RŽø”Ùö…<âñl²àWUD°pø\\vÙeÍ}÷ÝWë®\nÁrÑ*ÊÉX1Ë¼.ë^>ˆéüóÏ™ìÐbñAøÉÿÅ§ÑXž)žËäƒ2]ž	êv%e>ímZž\r7Ü°o¼\\Û^Æ‚÷Øc™!C†è…Ðà£á«B“2v¾Iºè›?¤ÍYs› ºü6+úØQ‚År¶Î¼²%}Gx)ÛîzY°ÊÔ^É¤“c¶äˆ…\'Íé²(OÝ²˜Ûbèú‚îTz–ØÕ9…’&eìê|‚0>&.Ø	QÆ.\"ñª\\ÊIK¹’²(Ÿ8büçHLî|Ïœô1wdi‘Å§Ê¥ýlž|ÛáRO“Ò>ñÄÞÍ-kÁãÊ+¯¼âÝŽ¶g¬‚d…Œ/AªÁj¹ê(Áb÷Œ6ÉÖ×“FRëCƒå¥lóS—ï•4EåÓ6ƒøÁ¤9‹™Ðv¡#=NË[rWÔn|ã:/)œxgœqÆÌ&H´/Ýh§M>&Â´<bzW!WÌ	\'\"dK^ÂW\\q…!žÿOÎ_æ!„,o>2\\pÁÐˆF6o¼ë@ù\"BÖ|C§ØŒO/¥	Y¤{	§2úZ6É\n;4ý×\\sMßçÚk¯í÷7W”Åÿÿ›û-}	ZH›“cÐFrÕQ‚%mOaKÓúÈ.¼è!!B•/n¹WE-Op\0Îj‡,J6§	YôJ‰,°ÀVä®£n|æ\n¢=hÐ (È!Ú,0|íµ×ú5‡ãÑ¤©»ðrJÛäµ›<É9!óÚf.@T¸>%N„$.ä*¾©2Ç¼BŸ¿¢Y[|ñÅûÊ³\"±´FŒ¿§	Ïvž¯W’XŠ¹|§vZÝ‡ºöíc¬>ûì³Ú·³-\r,“dù’6d³Í6[ŸÖZ6T¶?úÓŸv`µ•\\uŒ`ñàË®ºè%L£Dë7¡ÄJÊÈœÈÂƒéÈíUŠ,^yq:XTòN0ÚN^0D žM5žwÞy}fÑ¯¾úÊÌ<óÌfŠ)¦°ÀâPêz1t•cUvÖF ˆ`¥í\ZmœÑ™Û¿þõ¯û…8ˆÿi\\>úè£è.Â¤ðÿäü…˜Ñ†¸v‹gPþi§õÃô‹öðL$µYÌÛ$¥½”ßt¿ÂnÌ=­³û”E²|	æ\\žŸ1ÇÓ›duSƒÕfrÕ1‚…ã¦h£x	sª.Oâd\"-/é¼€¡ÔÇâÁ\"R¥P¾,>×_}jU,†y\'m	uÏáw0@kÑ4As\"x<÷Üs©AäˆEp¿º›£è²×:A.ÒLl|ðAßÿ™i/5ž¾ƒœ¬±Æ\Zf¾ùæ3ñyE¹h—¨“ñ—ùÍ|ç®w6ÆIÖƒ>µÂ#Ä‹6Ðüà8Ù&sãÓi\"§„ÅôÇíq¬Ñ¿$>b*oÂx7íyÓöV€,›Ð3Y­ñ%X”Çs‡Ùï¯ý«×§[«íäª#‹B!b»ð‚-Ú­bfÈt^ÈYe° °¸„QyYæ˜cŽ¨_ÉØ5ô½è’]°±qä-‹ž<TÔÙ¤E‰“ƒ6‘Ù8à€(‚oÝ%-¾d\'nb“> Q…p yeÌÓ´«ž¤jÿÂ/ŒŠˆ“«…Z(\"Bˆ<_Y¤ÓD-ëÊ9ÁH½˜ ™_œˆŒk­˜gˆÜå™eÆ‚%eÒgžÇxÀ@0…îµ×^ý†VÒƒŠ\"ÐDÔÊéB_’J°B0ëÁêrÕ‚…}˜—õ3Ï<cXdÉs¤Ýwß}\rŸ,‘²’ßCp¨+i&	™|6yY!Yôíè£Ž4¦¼~P6,¾`‘—X8É‹6}CDL+MZ”.½ôR³ãŽ;BÊ]ÝEÆ„ù(Âx2âÿbA¿Šb™‘_ÆùÊ+¯ŒˆE4W\\5$\"sŒ¹—¥­¿§¼—(eN8á„ýN2¿äy•9\'ðàƒN\Z6=Rà Ï;ïÁÿ%5¾DþÿÒK/Õ}Øµ}Š@*!$«—ïÆ6…bÈ{*\rÓ »uÙ…Ó\r„i§6Ú-ó™wÞy£ŸBºØég½Ä¥3¤å¶y„…†…›´ktô²Þq’±?™äB›¬O]ù¿äKj2Òˆ#¸6É2xÓM7B&6é\nª0D\Zìãs›êâŽÜq’,sSVá-eñLðœÈ\\«Lä™a®Èæ%««%Ì{Év&Ó3)?NàHC¸Œ¸@œÄT,ƒvJ¿â!EâÚB:B¶ÄEœü“fÅ\n‡P‹VJGÀ—dõ\nÁê%rÅäª”`É\Zi/ó¸6Kqj/2YO\0äLHIÞN¾ô\')¥@©uÖYÇüæ7¿‰qJÏ«›¾ÆÉ¤ä›h(ï°ÃK-†/Ëÿ«}¶­ƒÈÔ“M6™Õ…²`òâ‹/ÚÝñthY_æ[R øŒl.âD™99É2\nAów</óÁœÿÖ¼ˆ·¢\'›‘P°h[–%ý-\']™›´áÃ‡÷«–¾¤µ{†f(tm¿æWªF\0’µîºë:™{`õ\Z¹ªœ`a@˜%¼|Ù5ó9ñÄû’áëQdZc°d¡áÅÎK»j§vÛ“vl³Í6VÇ_!É‹ü,JàÃ\'¯_`%æÛöu#í\\Jj#`‚Óu]BÌÜK¾£ýÌÏ)§œ2úá†`]‹×€]uÕUÑ³‘Ô\"1×Üï™ÉÂJ4mlBÊ0¿ÅµTÉ:ã››MR2¿øx•ÑÎºÎmWo àJ²ÚN°„\\•µÑkÊ,ªLƒÅri¾(6À°HÅÂ—/Ä,¹C¶©£ê4²ÐÕ‘dñóuŽ¿˜º/J˜Ün»íŠàˆ¾¯³‰œ/tš¤Æˆ¹ÊOÂTä=äOšÏ¬@ËHD½²)2WÛÔ“G°ÐJ	‘£®Ä³0å¹Ø´SÓ(ÝFÀ…dµ™`õ*¹bþUB° ¼hó´WY“_ò,L0¾§:ñàÙîà… …øž°(áÕ‰>çÕqàZ·ìêjöíUÞŠ´y)Äƒï!=Y×`•1f÷ÜsO°5®!)—¶ç=w¿øÅ/¢Ó’l˜—®W¿ùå—i¢æUjƒ\0ë”¹°­«—ÉUeKÌ >ŽÊìðyÉ™û¨£Ì~ÙO¤Ë_•E…ü!­ì¾givlçØ]wÝuh–s´M´QÉÌ´RqÑEõûJ|¶¦™fšè{HO§–s2l²É&Ñæ)¯ýb\"å9†Pºhg…˜Õ]#[6®Z^{°!Y!‹ð*òòùÜrË-ÞŠŠ¢6~e•UV)Íÿ³‰3¤\r‹½ïé6[Í«î\Z,[XTŠâeåM.pÈ[ôê01i£mè…:›³‚„‚1í†T$eƒ\r6ˆ´¹º h,mçÍ¸BTV[m5³ýöÛÛ$/L#›§¬›â>1ú²è¢‹–+	²‚”Z 	\Z\"PD²ŠÈJ^—ÞLŒAŸW+ù®£ymVrõŸ«„`±kõ%6±£h8š.WN>w.¦4;!I²NöÏµ.ð`·d#¤Í»\nÉ¦Œ*Ò ‰0dÀxå•WŸ¸HÆ˜ùšFÀâéé»ï³“ÖgÙìÐî20-rÒ—Ã\';~§¿y&Ñd›ÅË÷¥_Å¸k™Š@ðüe#\r!X¡ÏŠoþ¬6óÜ÷ºæJæKéKŸASŠ?‘˜jÊ˜øU”ÁÉ@MiqŠôYô|òv*ãuóÍ7[UGÚO?ýÔ*m\'ù˜¾‡Ò7òÄ¥Y>\\¾ý¼øâ‹#’3çœsš?þØ·˜¾|B òžÑ¸O`\Zé,jùë¼y*j¿~¯d!¥ÉjÁRÍUÿ‘/`É\"„VÅUŠvÇÉ¾‰ËjÓ{ï½g^{í5\'‘¼þyæ™N‹EÐ÷ô”àVg¿æal„XKuŒƒ\'K6ý \rs”ÝbC°lÒØÖ-é 9e+)¯èt0ß£aöî¡tÙœøÖ£ùn F²Ú@°”\\\rœM¥,HdÁçH¸Q´YX¸ 1eÈ)§œbÆüè¥¾þúë—Qd_¼#ÛÂl}ÏÒÊs!¦¶í);ÝÐ¡CÍ{ìaUìÏ~ö3«tNÄ±ø»HœÜä™ãä¥è2t—úI{ß}÷™#<²´Í&¿<Çõ¢“†Eío‚É»¨ú½\"‡À]wÝ.üüóÏ£dM\'XJ®ÒG»t‚Åi8_Í’‹©BTÖBÄiÄxtì2^\râKb{‚\nÌè“@HëªãúD÷¶‘±Æ\ZË&YÇÓ0>¾¤c”·yÀDL\Zß:Ò\0‘2)×öyyõÕW£˜e†å³êª«šK.¹¤TÍ:ë¬¹!7þò—¿ÒøŠ¼ê¨Åôí“æS’@²\ZÉj2Ázê©§Ôç*cz—J°Bü¯hŸ‹	¦LBÁê©§Ž>\'Ÿ|rioS	‹RˆY¤L<J VÐ¿ÿýo3î¸ã\ZÄæÉèÑ££‹‡ë&BTlüÓÚ.Wääõ+‹(Cx˜£GqDDvøÉß6\"DŸùQ&ƒ2)?¾Ùˆÿ¾È\"‹DÄ‹ÿtÐA™Õ6,h.K›y¨(mF@H76pÛ…ø*4¤.ßüB6^êÐž=j¥,ÙyúîÀÑ~ÙÑø.v>“Ø\'‹?,*¾»vÌ²u_vß}wCÀÑ<yôÑG½5 >cd›Gæ6äÁG0=Û˜™3˜R…PÍ5×\\™„‡›†”ºó6”!Ä‰ú\'žxb™¢äåÃÿâd‹\rAžÐ×k¯½Öª¨OÔÎ«bÍäŠÀ×?dùÃ‡)ïÿðõ?ßýáç[ÿ÷;ÿ3ö;Kz~çÓÓÉúýïoðöâ`í°Ã^n_ð%XÄû[zé¥{:ÎUÑxUB°ŠvÊi’—ªÍ\"$‹¯V\0U~ï\Z•:DÅCâs° Êþ\'ËæÁÂ/œ[%±²ÖZk­N6Ëª.óu²@Ín–öH4¬ò“¹EZþ~üñÇ­ú–ˆS†”AY”	©J{®ø„‰ïIWäÄÃ¤jC\0“íMÞŒ3ÎèÝ/Í¨4	®{ûþûï½š¼ÒJ+™·ß~ÛëÃÝ€¾‹çôƒ>ðjs¯d*•`¹˜ø’\0ËV´3&_Èb×Éu‰…E»~ùË_šsÏ=×«‰ÔUw\r›yæ™Í[o±¹M|_\r¨p–™Ê˜Û6ý@æ‘ß9€‘vÝfC¾ƒAdòHÖ»ï¾; >ÝM0Ú©=÷ÜÓP¦ØøX’£È”˜WäÊ×\'Ñ¦šFh¾Iúš¿-8VÑR	å»ëtÑJáÿacJ¬0—2ÿô§?9µ“ôüã]ªèKö!ŽÅ^•zd¢÷Þ{ofÎÍ7ß¼´{ó<š—™Å•,Ç\"¸`‘ƒ»¤\'æ:´}6‚V	2I^Ñ#ùå„l2Î\ZíâyuÕ‹U‘9›v…Ìg°SV6XhšZ!ðm­ZÓâÆ„¤Ðü-†6¸k¥,^ä¾ƒ…†À–œQ‹PÝEvñ¶í9=B\0lÛWF:®R9úè£3‹Úh£¬/….£=¶e„l \n6¦ïk®¹&\"b®~^øk‰i1ô@tøÄËí•ïójcÎ–9éC’äÐ‡¯F×v\\5]iŒþ¡¤×øà“…ÝèË>Ÿüðù÷È–Ÿý«´æµ· ßgX	Íß^dÃ{V\ZÁ’¶‰/­Ù3Û¼÷¯»ƒ»ô‘…¨èäœ¤ßSŸ6õYÌÂ§‘}	h¯–]vÙÌ8Tû:GÛ·Â=¥oœ29}Èi›\"¡ˆHpÎ´Ó}hÃ’\Z1æ\Z&HâcùóºhžÒgÒ¡)sÙlèËß¹Ú¤RÑzç‡Ï‡?|pŠ\'ðÓWµie\ZúŒ„æo„•u¡4‚%/DÛX;ñÝ~ûíNqœ˜MØÙBvX`ÐLØ\nZ<Ÿ	ß„`£`€Så¼óÎ›	}÷qŒ¶Å×7Ý¯~õ+¯qó M½Ôa+,Yžh|ÀyÇé >÷ÜsÁ–¿o³Í6ÑÅÓEæ½¢ö2¯mÌÌgs¾¬¢hÜ÷+Hë³>ÜÛÑú®q=©Yƒ}Ö‹xBó×ŽZ5§t‚e{ß\\ÈRÒ„‘‡þFuÖÖ<öØcÑ‰9Úk+,”¤w]\0G\ZåDRmÛSEºAƒå¬o¿­Ÿûãh«a•Î‰V×V+åÖ#	 ãÝu×Eš*1MgHäÿ>Ïi¼Nú„C~‘œuÖYÑ¼då\"2Ÿ›àWèÒ/M!€9ñ‹>˜!^*„¤ÐüMo}ÖÒÖ[láµS…$ñudY8 0u%Yôi’I&‰ˆ£Ë^–¼@ŽY³’˜$>ù:=ËgŸ}öÌØ)Ìƒº,ÑDºbËÆ±‡d‰1›´Eeñý©§žj_|q3í´Óöûð?¾[Ÿ?æ3Ï\0ïWq}v\\Ë×ôµ@à›Z´¢ÁàYüä“OÌG}äõqY{SWš^\ZÁ‚PØîÔã=•Eè¨£Ž²@î+¬3ÉÂ4#ÎÊr’Ð…®¶Új^\'}ý„¬€/1í|ã7RK¬#Ár	!ï”<6NëR‡‘:1¾óî.ÅrÒI\'E&D_‚e£™â@+KY±%q.ejZE mðœpÀfË-·ôú(ÁªnF”F° 6&ƒdWˆ`‹æÅVD›pýõ×›\r7Ü0\"2uÒdzè¡Q›¶ß~û¨K>¾QbâatYÈ\\òt#-ôÕW_Ý8‚…6ÆV;æÁ~ûíg•ßE[mWV.æh‹f5Y§B(ó™gÝE\\HœK¹šVh¡)4›°,»/¥,!=®Å\"ë²	a‰;ŸvÚi‘ælðàÁÎ;ä²Ár…JãgŸ}ÖË7Š>¹â)dî…^(»k¥–Çµ´õË/¿ qá†ùÛn»­ÔúB-«K98€3†Œ¿H!§cgši¦h®ÙhKI7Ï<óØ4-5Í¥—^\ZÕe;G¥m.sSü·Š®Wòî„fTZ€€í3˜ÕÕÐü-€°².”B°daGƒâ\"ìhyI»hxÙ&wÞ÷ß´˜±+·Y\\\\Úh›–…ƒv±$ûc3(Y‹‹kDS²HÛö7$šGÚÁN^‹³é¦›Ö./ ÆÕVd.Øj¯(W´5!Îç¶„DNí†ÔE›]–2î£´ÙœØœV´-SÓ)mC ” …æožeö§‚%î®/l!$.ÊòË’Õ-r!¤9)cum—,.hæ\\Ä‡Ì¹”_FZH\'ÞÀ²þ?Ú­ºD×%üˆllµWô“Í†‰KÃFæL1OÛ¤ø`íB°(ŸçÀVÃ&íqÑÊùôAótN®A¥B	RhþºÐÚ\"J!X>Î¨\'žxbô²…¹u3Î8©Yø®[æÁ MÇw6¾*ÉNÉâbãH,yÁÔ§.—1M»öÚkGãÁ:øàƒ·÷Þ{›¹æš+\"5/½ô’÷%¨¡íŒcêrºÏ‡,¹šÊ³ú¶úê«GÏU–¦GsX(™£~Ÿ/Ò£Ñ#Ë3\noÂ¦¡¬¹¦å(®„¤Ðü®íí¥ô¥,Gu\0æ%Ïbà*y\'åxóé†ä‘LÚä3‰‡ê¼¸àßäêLÜi¼¶Þzksì±ÇF+K³÷úë¯GO=õÔŸ¶‡z¨ÓÍŒês\r‚+æABvÕUW9,0D‹)9äCú%ûì³Ï¢çŽï˜[¡ÂF‡²l7\0‚„6Øúb•qº2´Ÿš_¨3>kK¼?¡ùëŒM·ÛV\nÁbAw$YPn¹åçþórÎ\"Qýë_\rŸnHÁb‘Xn¹å¼šÅ\"æ¢‘ª»£ûÝwß]Ñ2Ë,³˜»îºË,±ÄQì®¯¿N7)8ÿüóÍÌ3ÏlˆŸEZHÄwßu&\0´àië\'ˆv†1s1ÊÜ)ƒô0ÉöÙgŸˆøˆ¯âÛo¿Í=Ú4ýôÓGß—!¢]²½\nŠ:ÅLhk2›=Ëh³–¡´\rUVYÅ|øá‡^°pY»Û†]Õý)…`ñ\"wÑù˜P\0‚Å6Ï\\pÜqÇy™P óŽ”Ë\"Aû]\\ÁË6¯ï©E×vù¦ŸcŽ9Ú)®ÌA Û„´(\"0ødaVÜm·ÝÌb‹-fV\\qE³ÓN;EZ\Zîat|oÛ~×\0º®›\rÚAž,³·m;“é6ÞxcÃÅÚ›p+[|B<ðÀÑ3¼ÐBYÏi1{Új½Êî§–§Ô6¬[mµ•×G	Vµ#L°Ä$b»#•ô.„L (z¡É±%#eB+»ù,­œ¯‰&¿ükÖYg2»WJYœðüñTÖ+¯¼bŽ9æÃwìÜÆcŒ¾;¨àŒÌhmý•/Á8Ÿ}öÙNMquw*ü‡Ä”_…Ù˜¹î3¯wÜqG\'“¨<÷ÝÒN»â­éN\"ª\nÍßÉ¾6­®`‚ÅK—,¡úmdóÍ7vë¶éãeÊ‹6+/»ãf˜Á«l›¶ç¥¡M¢1HKç³I9,ð.¤½Ð>•‚Ån+DÂu/\\¢ŒV†òÆo<óðÃ‡›šW6¶„‰¹Í8»˜¾E+[åKŽ6-³Ì2¥ãC”Í¡‘Mƒ­Ùœg‹zlÓ»´EÓ*MG ôÝš¿éøUÙþ`‚ÅKÏÅ¼Ábè£½ño©ªÊfðÝËé+Û\0œ²€ÕÑ¤²üòËG¡KNò(Í6Ûlý>¤ãÿ|è?éöÚk¯èoòl3Ú —ñu\r·ÀÜ¶ÕvI;dÓ`Kâ\\Ú\'èU½DÅ×Ëu®É	Y[m³O R¬4\"Ð4BŸíÐüMÃ«“í\r&X.þWbBqÙáÇÁÈ;Ah\Z‹49‹7¿ßqÇ‘ïŽ|FŽiøÈß¤2dHô±]²ÚÁE/G\Z[ôº˜mË\rM¾8ªãG…™o—]v‰â^Ýzë­ý>ûî»¯á³Ç{Diøð÷e—]¥{íµ×B›R˜?/$H2³h¢\\‚‹Ê¦ÁE\\Øè”?ß¹WTóÚ§ý®áwñÝ:ï¼ó‚ŸÇ¢~é÷Š@S%H¡ù›‚S7ÚY\nÁ²õ¿‚ p\ZÌGXÀX(B&Ã|óÍ×wºJvÞñŸ”Ï\'í;_R(}õ‰Ç‰…Œ¶ÙŠohÛò{!Ë|S¹í³PÖ¼°‡*M„¾~â¿å²iˆ?¿h/«0Ûà©i:!²&ÒÐüuÂ¢nm	\"XÏ?ÿ¼“Ï	Gó}ÍƒbJa\'ë+ørmþ/ù‹Yj©¥\"\rÊä“OõŸ|ø¾\'N©…ÊþûïÕãkÎÂ„ä’ÿ¹çž‹B!¨ø#\0ÞŒ›Ñpõ-%Þ6mC›Y•ÿ’«&*Þ^ðu™£`Ëó(ñ·ªò½³ÁTÓ(uA ” …æ¯ulGÁÂ´Ã®ÒF„Œ±ðûˆ,Ÿ¼yyxi6,rŒw]]Ú\";vpðqôMóÃÚu×]Í¬³Îj¸³-¾«gKþÏ§î^Ì#&?Û9ÁKÊÇ‘’àâÃè3‰‰å“?/OÑÉÞ¼¼¢)¶õ-”²FŒa8ì@~%Ye¨–×4B	Rhþ¦áÕÉöz¬O?ý4ò£áåaÂÁ•¿‰èÌwiÂ‹Øg’²ä„–/8´‹vfµÏ·\\—|,.&‘dÙà™JÊ²Ë.ÛgÖÜl³Íú¾– ŽŒã£bÀ‚.èDJ 1¿ýíoí+ø¿”U‡h \Zñ“rnœE!Xl¶\\wÓµh³Æw\\%Y®ÀkúV!\0Abí½òÊ+?\0¡«ºéàL° (²ø`RÃy6î³4ÅSD‘Ç!3o½õV_ËqRH0^¦¾\"ípÅÑ•!,t!_<õñÇßW„à”ÖIW¶Èn¿ŽNï¾XT•ù-ÁE×\\sM«jD;\'·61w‚`Ui†”çÊçÙ–v¹ú­Å±Ó;›æw77P6c®i²à9Âõ÷×¬²G£yN+N®ð½`1gQ‡h±ÀËQj!\\¼ôX| ¡‰‹?L\ZdhÚ h”ck¦ãþ;Òû:æ\'Û!‹ïrz.‰#c`ã€ïsÑ®o;›šùý»ßý.Âxþùç·6¯µÖZQówèsaƒu]	–ø†,æ?šZy¶]7P6øi\ZE Îølnâý	Í_glºÝ6k‚\'WÉ£èq­	!øm\r„‹EçòË/ ×~ß,^†´ÃÖœ!Z³|°”q’Ý¾«ÏIßBHÖW\\QJÚTˆÌojæ®­ï„œÒkÁÚyç§ƒ<çœsŽsÞdžQ!Y!ÏWpC´\0E Ãø®ÒÌÐüîn£ª³\"XhrìÈbàó\"×E+Ž\"‹_YNº´_4ky#	á…ýæ›o–6 !ÁÒÚò@@²è¿oÐÓÒÀ¨QA¢¹Û¿ÿýïN-ó ï˜4`±òíƒÜðÅ.9PŒch»r\ZhM¬ÔÐç\'4Ma©E³\n	‹hrPéûú8@Ð0qù;Ü²íç%ŒY\'K¤Ï¶Gômû$D1Ä$ÂÃú@`þOÕd™h>ËæáØcµÊ¾t2¦¾c²i±m,m+ËÌV§/Á¢,žÅßÊd{ÐRS›BE ð}÷6¡ù{cß>æ,Y|lwöyäçn_bÁ`,KXHóN/±.Ã$™ÖÞÐ6\'ÕBÆ‰2h‹/a.k,ºY‹0>WàrU/Á@ãBÞƒ6ø±A)óùIÖéÛÊ‘ƒ¾›/ò±”Ó¹¢QT’e334M]Bó·ÃªúK°0§ñò<î¸ã\nëçÅÆb•µ`‡¬²!vô¼˜“í\rWU“Žr]‚+&\'8øò\ri’U†ÿKáä¨aÑR†„	n…ò–©ÁIƒš1¶Ý$ùUˆÙZnøúgŠÙ]¯Ä³„<g>8hE „®U¡ù»Ñç¦Ô™I°\\âÛ°3K––Š—°¯‹¼¾/à¬È:^.ý°!)>ƒz¢+/¿”DËe£èåhïl˜W¡3” d‘3B8øFýÏKÚ\'Îßü´™yó\Z¼’~jú—4íò%CâŽâ{n¢ó}çø<ÛšGè¬YÌsŸÏOÈ³Ûþ6©ÎT‚%»@‡vÑøˆŸVÖ‚ÅÐ÷å^†–!mP„¬ÈKXË²µeñºÅüè;Iòòƒ“Œ¿ÛøÉÃéÛž&ær]i=¸e\"DÃ\nñ°Ã´1àpF<\0-õÈÇ·L	3×¸…ú Ñvy}	¥ôK6b<ÇB¼øŸOøŒ&Îkmso\" «¾ãžJ°ä…—Ôâ$_Tìdq˜–Ø3yÎ´!Nê¢þ/Fñ×(ý…ÀTé zå´9‰c#ÄöËâ\ZwÜf¼X$ã‡ÄTèzz®ì±èTyàÏ¢ËKIæ7Ùl&ÒÚ(ãé«ñ”qJ–-Dš¹t9Í*Z+ñ÷\'‡Eøg{h…¾I4xòÆo#H‹Íæ:–ò¾ñ%BYDRˆ—ïØºöCÓ+Ý@ Tš¿}nJ©‹“œ²“EY®µ—µœ\0’À,ìiÇÿe7éŠì}_¾EuÚ-*ÇöûÐ™zÒ´ŒGR#ÃXÄOKrjPÌˆB\nÄ\\VÆ¶Øt\"ÌÙ8!\n1q‰VÇ— æ™|!YqóÁ}	*Ë8ñ‰“.~‡0ÉåËŒ³œ›!‰%fpþÏ< ýà/ŸòøiH‹eÚóâƒ×`ùšó ŽŒ!Ï2˜¡Å•€±üÞég¼óXëPP‚š_G\"Kv¤òRb%³¼´ÙÅò!}rwÍ|¼¸YxÄYÞg BM0>uV™§Œþ°˜$Éä6N\ZÐ80IÒÄu\"²øH@FòËwËÃ‹XG´{²N;í´¾Ï=÷Üc¸P¹“\"D_40	¹„Û7ŒA‡x¶²›26Ë-·\\4>q3Ÿ8ÇÓn1\'Ê÷¤ÛDÃ\rÅ<(%¯¹J–ÿÌ²´t¤ñem¯é’¾&Ç¶*±êäÓ¥uuP‚š¿[ýnB½/9q8²%÷Ýñ\"eá–…IÌÉâå&/o®Éá…ÏÇGDCàk‚±©“…W¢ÎÛ¤ISÁïøÅÙ\\Æƒ]Õ¬€ª,<Œqœ³€†„*HbrçwFw þáˆ4&Ûm·]tWœ|øßJ+­dN?ýtóÕW_…@j—»ÅŸOB$Ùôõ»cB^PI?À¬Îä-!Bô|“¾LiÚM©¢ÅØg‘-È]QPZÁÔz 	}‚µ&	)mè\r¬/Æš¯½„¼@%4{‘\rïÙ\0‚Ø|xí´ÓNÑB?©Ã‹Œ…;þBŸ%òpU~çÂgq´-¾Ñ•Ë2šW¤Çv(d1	Ñ‰©¨ìñcé¢Õ -`Žy	-bRÆ(ZñÑÂô.WÄ¤õ­ÔV[me¶ÜrKsÉ%—˜÷Þ{/š/¿üÒPÿb‹-f^}õU[½Ó„æ ŸÌcˆ;ä*îWäRAÒ\'É%/i©×… p:Ï¤\'NXo¿ýöL\rÏ³ÍóG¹|(K~·éKû³ÊK^æÎ_QÅ¥å²!`ì¸ÿ”;e¾ÒÛË¹mú¤i¦ JBó7§n´s\0Á^`¼´x1³Xó”—./y„¿Yœ b‹´ì¦åhñ!ñ=±%Ç­«§“K´‚!,ÑPAvåÃ¢‹@zÿøÇ?FŸ¤ˆI†<€²Fmluùt¼M<ð€Yj©¥Ì¿þõ¯*‡¹ïÎ@6\0,Æòbñ\r!\"±,÷\"Ð Ó¾æ·¢²ËÀ€:dÓ Z(1qó]Ò‘R,&SÁ~ÁŒÆ7tƒPÔß:|ÿõ×_ÞÁÿøÇ?ú6´²±Mþ|ùå—£´äQi\'¡)4;Q-§WVÜ×Cvøy;`^Œ?üpÔ\Z^|çž{®Ùb‹-ÌAýœuÖYûˆšO“CãFÙÔÙ‰ENÚQV]àâ{:J‚l²aÂÅ”á°¹£1Ï{ï½×¬½öÚ6HsÙe—™]wÝÕ+¯M&¹è;îW$Z_\rŒŒeÈåÂRF™æÙ$bÚ·ÁÉ5´ß&qQÙÌ?Á÷í–÷Šhg…@	!­UYÏUQ;õ=„uÿý÷›Ã;Ì ­Þf›mÌFmmh>ø`sÄGDïÙSN9%2µÇ?§žzjôÝá‡¥Åe`“M6‰´Ë”uè¡‡šn¸Á¼ôÒKJ¾:5¨ÕJBóWÔ­V;€`±ÛÁD\0QM»É‡dÐ7?y9ÅT¥tòå\\V]˜–ÐLøîØ11ÉîŸ±~ñÅûÆÛk´eÏ<óŒov3dÈóÄOxçÏË‘Œ“+ÚÊœ_,™Ÿà™ÄtØ°a}E1ÂðAÀ/tnŠÉØÆ„çŒœôÍŸ—¯¬ùL`ŸÓh¥ ÞÄ=I„%°®Œß§™Ä«èwÙe~ðÁM.„ˆç€Ï‘GiÎ;ï¼èÙÄ¯®-Ô7ß|=ç”yÑEEõAº°ðûßþö·\\³~ÙýÖòÂ˜fšiÌÖ[oíõ¹ñÆÕ+|2K(¼ì™œ¼¸|ƒ\0²XÉNÔ§ªÁJGMf1ý°èCòiä{Æ²#ß“xÒCN\Zžxâ‰>ÓÄ*ÏwÜíêY\\âŸŸ`‚y[p:æ˜c¢ùË!þ7Î8ãôX›o¾ù‚æ¶´¡Jb÷Ÿ´É1Q™’Ç‚²å0‡ÔŸ³ñwy!dŒOSóÝ	\'œi¢Ñ0qÊ–ÿuêÐG\'Ñ˜yæ™‘oZmü#Ñp©Ô•W^Ù@Ð}>¸e¨«ºñ-$XrŒ]šÀ(;xþ—\\Ðù;¾ãßqÇ½ÍF”ßI‚U•ŸJ|øÊ\\ØÁË‚\"Z\nY€Ø\rÏ8ãŒ‚ ‚¿Õ\Zk¬ýI;Ê^ŒBÔk®¹Æl°ÁÕÍöŒ’ãZ(H\"¦*ÑhÑ\'1\nS¶â4Ú`Ê	Å.«\r2ßB69yý“S¾¾\ZêdÙœZ¿@9½,ó5‰“-—ôµÎ§	Ù¨bNÅÔ÷—¿ü%ò•ä°GÝ„6áúÁûƒ¶ò“¥Jý}w„æ¯\"õiQ!ÁbÁæ¥‡Ù’Á¡r²É&‹”EdW/Î–¼Cü3:I°:1ÑÊ$X˜ùÄ±W~—ÓpìŒ“;Sþùâ€‚hfÊšŽ„ºØ}÷Ý£Ýø£>ê\\,kï½÷vÎ’,d~S˜ÈAþfNÄO²øË÷¤-\na`Ó6\\ÙdWêÅßFL£<¿eKÙÏ\'XË)fÑ~ó.À%q’gIÆ@‚¬†:(ÊCK€à;ìÍˆKæ¾*ÚšV&måy†Ò‡K/½Ô|þùçª^ë)@ tÝ\nÍ¯”@!ÁâÅ/ /4L9HÚîX^æ|\'/÷žhªD!ñ…µªúÊ$X²Ã§L)ÌEZh¡~šY¤d<!eš	ÑŽá¤|ÔQG™÷ß\0„<òH¤¡:ÿüóSáe^tÒIUAŸZ.X² Çµ1˜¨âNìq? ~qp—‰&¦ŠŽòuä/j“ÄÂ+Jgû=Ï\"ï6n|h7Zp„ºâdX|àÄŒÈüªªŸ¶í§{ûí·#B…	§üªOÉú´Ñ5š-6B8ÚC¸ÄO×µM_¡)4y=i_I…+¾K—E\\`“	§TDØásüŸïäåÎKÓWØÆƒjú–S”¯J?˜xÝ‚a&ñòØÕC’„lÅ5!ÔùëŒ3ÎèËžò@AhÐ:ù:ÊÇÛÁ‚Ýùì³Ï}î»ï>óïÿ»/i X«¯¾zêp`~ö½¼h|³¾\nwÞyûùTÅ5(ä\r.xc–*Ëì)-[›(ý•[ªšß”ëD8kLÒ¢ÖÏ0ÃFÌo~y– ]ûî»¯¹é¦›¢ù^…¦Îe~aÜk¯½¢àº=ö˜KÖF¥}ê©§Ì>ûìi]îÊlT\'ÐØP‚š¿u­‰¹+¹£gaŒï QÝÇ_®ü7	2pñô>½”—h$ «~ég\'&Z’¤ú`Ï×À€=Z«¡C‡ö%³ ,:Ô©‰÷m&ˆõ;ï¼¹†hqÄœ¸X8#´kùå—7‡rÈ€ª(cµÕV3ß~û­o3¼òÅM‚Éd¬DK%Ú\Z	òY±1ª\"Tc-Zå*æwþc‚es#€ÔÁÏ)§œ2zçÐÑ<Çï_$M\'6di“mäÈ‘ÑC³óÆoxÍÇ&fâ¹gÌ Z¾‡¡šØïº´9ô¹Í_êØŽ\\‚Å‹ëW¿úU_»“/SvüñÁ€ÅÓÈBÒñ2MjYíÀA»‰]µ‘º,ÍmcÑQ£FEåv ÞÁOÌ^²(		ãÅÚoÌzœÎÃ$’4\r~üñÇæ•W^‰`åø;IÁìÀ±ôNŠlÄ„Í\\Ž›‰ù?ÚX!+G}td¶òC–ì£ŒI/:æ›œ*ˆhôˆwW¦€DjuÖ‰Š…ó¢¼c¨MšYéDLÒÄßYe¶+«,âÊáìÏQù^>uÑÚ~ûíÍž{î©¡:1ñþ¯ŽÐ÷FhþvµqUå¬¸ÿ•,àW]uU?Â¹¢žçå†/ÂË0ÄÿŠ2„ @LªÙáb†L?ýôÑGüB\\Û$fSð,CÄL+þ*‚—(Y¼!\"1%Ê÷!íAÃƒ9„+oÄ?/­oßÿ}ª\n§N™G>ž\'ïhÛ P8ñŠ0çäåÃ÷q²R6a)»<ú Ræ\\\\sYÆÜ+ÛÜoó:îÇ&@žSâôÅŸYéïÈ˜¼ÊècQ`Œ?’\\V”¾ªïÑüÖÅñ³(±µž|òÉªº«åÆ xû|(F	VuÓ)—`ÅÍ\'i¦-È”øÅ_æ4—Å‰ïCoËË´ŒÓZi0Ê.\\^Øqÿ\rÔüO?ýtäg\0©’4+¬°‚÷K\\ŠP\\â}‰ß	îB „41q‚\n™!\rcF\Zˆq(Åç$ž#‰5š,HØ´ÓNkæŸþ(ÂÿÂ/ÍÌ†q_­ê¦{ÿ’eN£õƒpÊø’\n\\X¸ååÃ÷`w\Z/sNVA°â§ìâ}+_ÑXwÂç‰ƒÌcž=Dþ®¢_eáÓÉrÐœuZÜÉþi]Ù¬²Ê*æÃ?ôú(Áªvfe,!Lìè1éÌ3Ï<ýL[|/ñjØÝó²e¡†@@HH_3æ%\ZJ\0²`”EHHJ\\ã&±¥ä%ŽãöŠ+®4\"¢	*$‘™#Ôƒ\rŠÆ‡@|â˜1qçwÆŠqbŒ&tÒRÌGwß}wÔ†=öØ#µk8·O2É$ÑÂxõÕWG1u6ÞxãHCÉUÝ{ð`üYÀÅ$˜$ö|1Å\Z×¤”ÕnÊ,;TCÜ<,fÂ²ÚK9‚CY6ŠÚöqA›Î3™æÓWTVÛ¾\'öW\'âøµ\r·6ô\'t\rÍß«êC&ÁÓQ\\ÏïbjŠ¨_þò—ÑË–Vü ÊÜ‘SV¨ŸP€âÅu1Ô\'X¼Ð!ò)câZÁ2Ê“20Äâ¿\"˜‰ÖL4Z¾‡,2føÃ¼âV%ÛO9YWå,²È\"™‹×âà—ÕéØWÒ¢UËüæ%×X	ÀÔÊi)æ8¤;œ÷Ë®î)2Q»Ö\'‹q§¡×ú$ëWí‘ëˆT—^	VuØÖ½äP‚š¿îøt³}¹&B‘8Á‚<±ß|óÍ}§_ýõèwÒòÚÚ²mÀaa`ªB˜\\ôK4ñ8Re×\'ZÁª´qñö2ôkºé¦ëCd²\0¹@ÊòÛ°i˜q²ˆhÐi±±p®ò’ç¼1D#%ó2\r^`FqÜ’DÌÊÂMÚW¶oåÆ}Ì¤ü¸/^èüV‚Š`yù•`•‡eÓJ\n%H¡ù›†W\'Û›K°D+‚f„Ý²øˆ¹¿E{U¥™\0rU¶ƒn|aMFUõÄ¼N4¥>Lq²Bxãd¹lmäIp„P±_rÉ%‘£ëèÑ£3Ø¿øâ‹Ê´“Es\r§¿xx€¤æ¶ê“iRwè¡xŸ%Ð/›!“e•/›…ª6>Ec§ß÷G@	VïÎˆP‚š¿w‘/îya Q)\"yr\'¾\0­»îºÅ5¤Ýw@™Y!T²xŠy³ŠzâKLMUÕ/ß(1òóÐC´‹¯²e›m¶1˜ÁqÂ	\'ŒNZb,’n>àqM”hý˜Ûà^^xaô»Äø*ê‹ï÷Uœ–?7e‡;‘òÊÑà‹_¯çS‚Õ»3 ôýš¿w‘/î¹5Á¢¨Ã;Ì¬´ÒJÑOù°\0•m.I6[ü£Š»ãž\"î+V%‘£eâóãÞÊæäà´àðáÃ\rw!^pÁ¹\rÇ—éÝwß´+÷ÜsO-:Éæt§EK™/»ø|–\rRYæiñ],K#Öi¼ÛVŸ¬¶¨}Bß¡ùí[Ú{)V·àá=Dß¯²…r!pHÕ«êòËÆ&´<‚;Œ×]w]¿âˆô¾ì²Ëšï¾û.\nJHè†ª‰zhªÌ/¨ÌÃIÇö2‹È\\ÖÈÝUÎ\nû²•`ÙcÕ¶”(=ð!õùÈº×6LêÒŸF,Y|¸‚¢L!nS<œÜÛVfñ²fœqÆ(þS¯Èˆ#\"…s}ü„#ý\'lš.H.7t#V]Æƒ¹QæIÂ8¡‡QvYóO|/_|ñÅºÀ×ÓíP‚Õ»ÃOlA6³>%XÕÎ›F, (3ìƒ@*š±Nh°$~¯©c¹ð9é£wíµ×šãŽ;nÀÌ&ñÉ\'Ÿ\\íŒ¯qéBˆò¶º4?©1-ó*“¬¹ôIÓ¦# «wgFèšš¿w‘/îyO¬¤ão•¾^R×!CŠG¥E)®¿þúèdæÁõêÍ7ß4sÎ9gª¦ê_ÿúWä$_å‰Ô:CË}ŽÉXl!íM#XhžBE4¿úbE²¼üJ°ÊÃ²i%…>‡¡ù›†W\'ÛÛ‚…i£ì‰€…MÌi×•5eŸâ*«]U–ƒiwî¹ç6§žzª™`‚	¢Ë‘Áš»Ê²„¨Ügžyf•ÍªmÙ2GÊ2…§¬2LŒO™Ú°ÚHƒ\Z¦«AƒUrSC×ÅÐü%w§UÅ5†`1	Ø}sJ­,I*1–Y‡´UêêGî¯¿þÚÌ5×\\}~Vøc=þøã…CÇZh¸@Úkù,“¸$	Öøã_Š—”+¦õ^§:öW	VG¥3m\n%H¡ù;ÓËfÖÒ‚%æ®V)K„ô¡ªRËÔ!\ZâãBËsÎ9§ï_h³vÚi\'«¡CƒS—°\rV\r.1;9Q†$	VòT¡oR®†hðE°ü|J°ÊÇ´)%†¤ÐüMÁ©ílÁJ’¡2À’2ÅD(§Ë$qÒÎ¥–ZªíAý®ºâH­°Â\nýª9í´ÓúÝó˜ÕÂ6\\qÅÖd¬ê¾tºü2Ý¥,Ù@@ÜÊˆà/åê	ÂNÏŽìú”`Õg,:Ý’P‚š¿ÓýmR}#XeúçÁÂ4ˆTAâd2`žY~ùå›47¼ÚúÆo.“þàƒúå?æ˜c2/‚Ž\'ÄŒVk¯½¶WýMÏtÞyçED|“M6	î\n*âNóe™Ù,ôÂ\\€ «ƒ`×¬ªW\\ÑŒ9ÒëCW”`U7 !Xeû§Ä	•À›$\\eÂŽö íùûï¿7ë­·ž¹õÖ[½¡ûöÛoÍÄO\\š™Ì»!]Ê(ó¼3aÒä4útmX™~b>mÐ<P‚Õ»³‚‹éwØa¯¬jçMc0”ýbO:¹WéƒUvÛ«~¥Ë¥Â~¹ÿ›kÊ)§ìi\r‰ÜóŠ#¾lq\rÖ/~ñ‹`3uWú„öSó£«wgAèÆ=4ï\"_ÜóF,Ì,e‰,7ÝtST$f¤*|°(óO[…S\\\"ýüóÏuñ«¯¾2cŒ1†YrÉ%ƒÊiræÙf›-š‡Ï<óLP7ˆ=\'XBÜB®·‘gD_ÊACSzf%X¥CÚ˜CŸÅÐüª\rmÁÿ”²pJj¬Ê._ÚyñÅGøz•Õþ:•Ã‰ÁC=4·I}ô‘U“Ÿ~úi³Ùf›Y¥mc¢²Né}ñÅý”Ð8XÒ¶6oš8§”`5qÔÊis(A\nÍ_N/ÚYJ£ÖK/½TÊÎ^†R&ŠÆjß}÷\r^€Ò¦‰,J´¿­BìªO>ù$³{8½O3Í4ÖÝ\'¤Ã•W^i¾M	¹J¨Gw4³q\rV>X½0—›8—”`5qÔÊis(A\nÍ_N/ÚYJ£CÀ‚*Cd±ÍJPZ»0kböi«üío‹,óäÉ\'Ÿ4œv8}óÍ7…P@F1Ú¤-,¬	ÊðÙ«ÂÉ}†f¨dÒÀ!ªU“•`Õj8:Ú˜P‚š¿£mXe#X•²ü°Ä$(«\"X,–r_Ãæ‡Us:ê(sÑE™ã?Þ¤™¹*gþùç7{íµ—Ù`ƒ\r¢[ß¹üºHŽ8âˆ(m/Jó\\4¾²!)ƒ1—ýë_÷âÔºÏJ°j=<•6.” …æ¯´s\r/¼qëºë®‹vÐâ˜‚Ú¿lMSÒLÒÞºæÝj«­Ìa‡f–Yf7ƒ¾÷Þ{fË-·ŒNr‘3B(‡k®¹ÆL4ÑDfðàÁæµ×^ËìÖ¿ÿýï(¦ÖÛo¿]×®WÖ.!û¡Žî!y–áƒÕöÍBeZqÁJ°*¸ÆÅó|ó®}ë­·œ?tK	VuƒÛ8‚eø§PŽ\\ö×`±Ë/SÄ¯‡ã¶ÊvÛm×wav¼\\Ü|øá‡N&eõÕW7ø\Z¥}OË8 ­ÐeöËÕÑ}øðá©eQŽ¼@³4´h\r\Zi!ó²÷éê¹Añì°ïâç²`Þ}÷Ýf—]vq^`]êHKË†I¥»ð\\óžÝqÇ?J°ª»F,ÌLeãOj°Ð¶”¥“a£¡\'·ªá¥ó`ßvÛmNí¶Ûnæ¡‡*ÌÃÕ9h±ÞÿýÂ´mJ æë3Î8£°[h1›§™gã+ËD(Ä‹ùŸ\'¢áÎ;ï,l“&ø/,~øVùYe•U\"Ó-&õ*ëI–Ý«>’ušß¡\Z¨ÐüuÂ¢nmi$Ábj>I,Ž²wè”W¶Ù±n“‚åz/Ý«¯¾\ZÅÍ²‘¡C‡\Z´d½$.=%¾UÚåË¼<Ç{ì:î!L#û¶Ú²óÏ?¿õ›…&Î1´HŒá¬³Îjlyû¨mÎF ” …æ×±ÉF ‘Kœw7Þxã ±•E,î7T&ÁâÒcÊcaj³p=Nž/UVß·Øbsä‘GBÃÂâÑKæ‚Úú;m¸á†™ÑÊ2\ZŽ<‚U¤™“zá€i‚Ž\"€O#ÚE\"÷£Ýùå—;Z¿VÖ]B	Rhþîö¾Þµ7’`é\n+¬Ð·3÷…˜Ý^rÁáï²vL\\5qðömgÝóÑÏ7ß|Ó¹™àÂéB|;ŠäÀ4pèqÙDÈF!mÞòœúËòÁâÿ6ZV–Mº^£:ôóóÏ?ÂŸ<ñÄfÿý÷„¬¶Új=Þ¤cÒé6„¤Ðüîo“êk,Á’yˆ™0mÁa)cÂáƒi¦È¯¥I“%«­h°î½÷^¯®pµÎ\"‹,b¸D8Oüñž»Ÿ0‹ù”	¢\n‰J‚–WL„Bø“8óÿ,-+ÎÓhbÊ‚d©ÔBŸÜsÏ=ýî\"¼âŠ+Z¦>è×£%¡ëUhþz PÏV4–`É®=ä^54aqa²M?ýôÁ£µÏ>ûDÚ±\"³KpE5(\0,_‚EóO;í´h×Í}†y²è¢‹šÑ£G× ÇiBœ`An&œpBÃ5BiÂ‰Mæ\\Rø\\ƒ•v8„9Ÿæ¿EY¤—MÏ‹¾Œ;3ö6µ<ûì³3;Ó€)1{ôÑGmŠÑ4\rG ô™Íßpø*m~ã	VÖÂ`ƒZZ´ì²y!i½?ªsÏ=×òÌ4#•Å\"+Ñ¥—^j.¿üò zš”™ù#d¢Ä|Íò;„È§mx>ŠÂ4,°Àf¥•V2»îºkô™bŠ)ú>Ô¹õÖ[G°ñ;Žò*ÝG€ÓµŒ«ÄˆKÆÁB#Ìæ¢J»Xl±ÅïOù°aå#ŸrÊ)Ñßò¿äßJ°ª›=K°Ð°`$M¢³ˆ/ô”]F(	ßú;™3íÌ3ÏlFŽé]-Ú«1ÆÃ´¿¬wß}·ŸSû¦›n\Zý½òÊ+{×Ñ´ŒñÄÈïh‘„ñóá‡î3ò}ÒŒ!+\"Xb*—Óˆi?!]ü¿ínÓÆ¡Ží%ŒGÜç.-Ð¨ÄÆªcûµMå!0ÕTSõ`áå=Ê\'þËÿäÿük¬±ÌK,¡Zéò†b@I%XwÝuWPH…¬cðò²?üÐz&wÒüè]XÍ3â`Kwâ1TÑG¸x›@—×_½üñÍ$“Lbð%a1!0)f°4S˜OuÏ³óÎ;GsüÐCæbÂt((‰\r„ÒŠð“2âóšß™ëL0ADl‰stì±ÇøðygŸ}öºÃÖúöqqúª«®j¾ýöÛ¾¾fErßc=Ì­·ÞÚzLz¹ƒ<ûãŽ;®ùÉO~Ò÷sÌ1üÿ¿óùñ¬«ÂÉÓX‚&¼ðO?ýt/xD# ‹\"¾S,8!§ÿhf^Héž{îiÎ>ûlƒºÚUÐLÍ5×\\}QÝ×_}óÆo®Êáèùºë®k†\rf–Zj)×*\Z™^4Kü”\r…!!<IÒß)3º”\'aœ@ƒ4±hÉÞ{ïíxUº‹À6Ûlc{ì±~È\"X¼ÃØèÙŒow{¥µû\"°æškFWÇ±²•Ë\'ù?6´|xg«‰Ðùâ|\'X!“#-æå±à„h¯„ü…ø‡]}RÐO>ø…l¾ùææä“OvjÜ}÷Ý×ïRç43àí·ß]2bÄˆžy!Ázê©§\"<ñ?‹ÏKBcðAã„¶kºé¦ë·éHÒ¥æ¶<3b&·}†¤¼ÐgÃirhâ~<øàƒÑX\'%ï.BNàþéOê©8r½4mlŸß,LBó÷Ö®}m4ÁŠ/®”4©”uŠò†Æ D}EëDÀC®!–¤‹O<H(¿ó?®ùˆ{]c5À±â^GNâÕ‚6*-0hVßÑVðLˆ–	ÿ«¤&­ªh¹øžòmá†šå{aÌªì#×ÒpaÔ¨QN‹Ä<Qi¡)4û-¯G&XLEø=þwLi‹‹œÖ*Ê›üþïÿ{_eÑd©w-»îé!XøzÄå²Ë.3sÎ9§™|òÉ£“gBd‰øNäñã?>ZØ9J?m‰³ûÒK/Ùå/¿ü2ò÷êÁ´ãB°ÀDNÁ2÷ÒÂ*ÄIþY”ïb\n\'}¯øÀÕmŽq ‡ç*Mò4X¤gãC(6**íB ” …æošåö¦ñ+~ªIüQlcc¥™ÅâBÚn¸á†~\'¬Ä«WL„œ\"L{H¿úê«HãÄ¿)È/7c÷ŸrÊ)#G]îäš¸à_”\'¶w–û¨t¾´ø)BÛÚ! h­Ðd1¿/¸à‚YÑpÉ÷¤u=Iè‚Vyi1¯½öÚý4¿ñÒ‹i‡=§½K®¼¨oI¡)4}‘é~Ë\ZO°â‹FÞ)\'võhSø)Z¦¤“¼,HBù,*yÚ(É#;{ñé•;Á PóÍ7_îlætà-·Üb>ûì³(~<˜\n9˜4]ä,òô\nÁbá„î*rZ0ëóR6#®1„˜¹¶IÓ‡!€97nFO–fC°ÈsÎ9ç˜“N:)¬1š»V„¤Ðüµ£fi<ÁŠOŽ\"ÿ)!B˜IäâÚä_Ò 	@ø¾è´bÒ™XVÍÆºÒæøÞQGËûï¿¿_ÛV_}õÌ¶bê „C/ÚYŸS{(Ù$OÉ\nn7æµÌs[<…¼ÉÄ6Ÿ¦óGàæ›o6p@n¶ßGNérE•J;%H¡ùÛb5½hÁ\"^P2ÎO6ˆh?8rNÚüãýåoþ/\'·äï4S‹d”²Ä$(«—N[áoå#Üc˜ô`‰¦+Y&ÿŸwÞy}ªj\\Ñ2ù4\\ž…,35sÔG˜v2Ñ§}šÇüã ÃE~r¶‹Z‰þÎIÝ¯¿þÚ®šªÖð,s°h³Í6óú(ÁªnxM°8–ŸâÀ›uzo£6*Œj\rÔI“7KØÕ£1²&ËU;PÝ0W[2×røÆ¦âŠ$ÁÂ\'Kn²å8Ás9t/HÁ’g!+NsÔçÅ*+ïyè…±éT<ðÀÈ´^$.‹²®¹æšB­XQú}=à9þâ‹/¢»\\}>>ïzô¼þ­h4ÁbJNŽ·Þz+u;{$ÏŒ—,—äÏþóLˆTüŠ1+öŠ?ª\r6Ø w¶cš â;÷c-œŸ˜)X°YâÂ5Eñ(îñïxp\Zª„yè£elÒžùŽùœ¼&ÊSÑè’_¥Z0õâ{q’U£+Á¢LÌóÄÕRi6¡)4³Ñ«¶õ\'Xy¦; Ã1”pqŸ|[²/U|Ò%—¼,*”Cyy§®\"¿­j‡µ³¥Ÿxâ‰æ„NÈ¬”£å‹.ºhävŠ8Vü$zû,³ÌbŽ:ê¨~yxà3dÈÔòXà{á…€/ ÙŒ0ZU6I‘S¯¾8Šf­W66X—†\rÉZk­eò6Œñ:]	y?ùä“(Ê{–9¾ì>iyÕ àûKkBóWÓ«v”Úx‚UdªMUÒÜÂýmY-Wr‡2l>›4í˜B&ºŽ!írl^Ü)®ràRâäñp|@Þÿý\Z)üM¨˜&wÞygägÐvSœh\\}ú‹†\"-ŽVhÙâ ß+aH|°ÍsÉ%—6.¶âC°(›ÍÌ¶Ûnk[¦«!¡)4\r!©M“\ZO°Š4XÄh‚-´ÐBæÊ+¯ìžI•µx‰$yRŠ84”Ã­Vž$µ`µñ\n\ZÂB{Øa‡õ+ùÉ\'ŸŒü²0Æ£¶§UÏ	)®{‰1 â—ÙÊwœ8<å”S*èE½Š,Ã™<rDzxÚi§EÄëî»ïöê´lZ”`yÁW˜	wÞñ»8‹2ù,Ê%¼f~•f\"JBó7µÎ´ºñ‹|>Â¤b¡ÉÌ‡yßÕIùiæ™¢|MüþÆo4[n¹e_Ó1o-¾øâæ¡‡²êšVÈ°Èî»ïn¸C-),ê®wZ5¢f‰Ê X²QX~ùåûõŽ±Aƒë+âc¨ËÁü|;ì°ƒáf	!X¸4 1F›¬Ò<B	Rhþæ!Ö¹7ž`i°² Ä˜7±X„B&^ž}ç†·35½ðÂý°â”ß{ï½çT9&ÄxPMÈmš¦ê¼óÎórÎvjL\r\'Cø6IÌyñü¡æk!¾›ß¾ôB¾GyÄl·ÝvÎ]\r!XT6lØ0k‡zçÆi†JY§hXhþJ;×ðÂO°â»hv~ÄPúàƒ\n‡…|ž†‰E(Äÿ¥—÷Æµ$EQÝ³‡î”…àŸv©3ã½îºëŽoÓˆÿT¨–ß8æ2‘ôžÐ¹-K_ÌåÎ2L‚Ä§ò9<J°è	÷ƒ^tÑEåvJK«©¦š*zWú~ô9®nˆ\ZO°âGÍ×Yg3xðàÁCÓà“@Œ²ðÄÓ`â‚€%ƒºƒ,¶÷\"º”]·´ùæD HV¤õo¾ù&·éûï¿Ÿù³§¨’‚ƒqn»”¥ÁJj›Ê0=*Áªföyæ™ÑeÝ>RÁÂç‘)o¼ñ†O4O— l\rŠ|?J°ª¸Æ,ˆ;qß>“Ã\r7€.ÿtâHl£M«nx;Wò3Ì¹C [øUÅg÷}÷Ý7·Aï¼óŽYc5úÒÚ!)ŒÛ‚.Ø¹Žu©¦²ÉežË|–yéûÜ\0‡øv…>#]‚¶¶ÕŽ9Òû\"æ2À¼úê«©„×4mXðZ¥Ïqu“¨±KvÑ!Z&ÙÇ\Zk¬èò¿ÐIWtUIuCÚ’‰fß}÷E•_~ùåN\0rÿ™Íé¿Aƒõu`é¥—Žbõ$Å÷Zžî ãWkY&Bjg#!æð²ˆ[¨—*š+²–\"Ü<B×ªÐüÍC¬s-n<Á\nÑ¥Å	\"¼\0‹GZ\\\'—a˜¦!s)§)iéoÞ…´„Ê°9•¹Ê*«ôuy¹å–3ifÅÿøÇMÅ»e˜ò¤rÌÝ‹ÃüŽf+D”`… W~^ÆóÝwß-¿`-±ö„¤Ðüµ¨‹\rl,Á²åƒ}šûîÒD±8I³YØ0OâgË1Í„µ.Î\r§ªo½õÖèR–ðýì³Ï^háaÇT¸ë®»\ZîaKˆñÕW_íÔ¾¦%–y¸×^{7=~à\"ä~ÃxC”`‹ ”‚@(A\nÍ_J\'ZZHc	–í)½¼… ÍKˆÛðáÃ3‡Çzÿ/›4m™W(ˆO– ‰Â«(x\"ãÊ5D›o¾yf€RN\Zˆ´Í\"+ÀÕ§Ïñg…xGiÑÝ]Ë¥Œ4óºk9š^PÂ%H¡ùÃZßîÜ%X,<6ÅsÌ1 Êx|H“Wæ`B!OžÈâ—GÂBð›6í†jöÜsÏàfã_EÄüW^y%·,ü³¸j§­’wÃµÏñgÅvcRTÏ^ÑsRT†~¯(á„¤Ðüá=ho	%XBrŠ††É³Å[d&ã{vâÅØÆô_Õo~ó›Üªï¹ç+-WQû›òý{ìá}¢3ÞGÀBÔTÊ„YrðÁ›+®¸¢)ð8·Súb‹-æœ7™AžÊd®ÛlLŠ*µyNŠÊÐïE Ö0®;âÞW×W$)Á\nƒ¬\ZK°ðMá%_t½CÑŽ]1²‰ƒ{Ñ„›vÚiMÑÂ\'e…:W7ôå–fEZ§¢\Zq’ÿå/ÝÉE·˜³„èñ8Á·UDƒUŠû–áƒ%ä¯WüÛ:Ç´_í@€w/›ÑÏ?ÿÜùƒëFÑz×”ºÓ‹Æ,1{Åó)2%ÊBF9¶~/h¯ŠÌ#RVìÎ°—_+ñ«^ýuï‚?þøãÈïŠHü<ð€A˜G°¨3a›…yVÁ*Ò¾ajs¤¨ý^PÊA ” …æ/§í,¥±Ëö%/éòNüáøËb.iYÜó„	Y´{§¼^rrG£Çé?5jTt!—=ƒíóÏ?ùaå]‰óÑGE„¬ÍR¤}µí»Ìk..Ã´‡‰¼—æ¶-ÎšNè¡)47úÜ”:K°l}œp„f1Èsˆf‚ñ³cÑàa\",Òœm³Í6…õÕÓ¤ï‰ºžŒÞnÛþcŽ9ÆàÃ…êášk®1»ì²‹!&ÖèÑ£Í\r7Ü™‚Ÿ}öYó×¿þÕñštm1ÈÊÛØ`œ42CDˆ_›„à£yN\"úÍßÉ¾6­®Æ,€†8™‘$Ý9çœ“96B°l5Ô[ŸÈ–¬5mÂdµ‚…™ÏGÐD	aeœÆ{lƒóåFmdîºë®ˆH]wÝuñâŽ>îL#¾Ùn»ífn¼ñFŸ*‘GˆÑlÔ^)G6¡fGZhAÒÌŠ€\"Ð‡@(A\nÍ¯C‘@£	7Ï9›ÓuÒåM\"ü´øž›ämÒÅ\'²%km™œ¬#Fxu¬|ðÁ(/\Z«?ü0úýþûï7\\€‹v%—Ðn»í¶ÑwÜ}Èx™j½\ZT“L\"ü¥~ö³ŸµH$•gù¢#¨WP‡4³\" (ÁjÀh4Á²%1¡<³ßK\0Ñ¼qc‘b±ñÓb1ë`ù:¹3ŽwÞyç\0¨(ïŽ<òHÃU;ˆ„Ü˜uÖY#-âé§ŸÞjˆeŽ‡–ˆÇm³c‘‰;TÚÁü§E@è>¡\Z¨ÐüÝG ¾-h4Á:üðÃ­4N;ï¼sn:\\ÚÄÖ²IÃp£Yë¥‰»êª«šaÃ†yÍtb·üíoç÷?þñÑ¸qÄ†¿—Xb‰ÈkÓM75Œÿã?îUgS2	Á\n	â\ZŸ³¶‡C²ð¡6Œ¦à«íTšŽÀòË/mn}?½´Nuz¬M°lÉNQ:|\\ DEé›4¤ëµ]>\'Ë|µ\"Ûo¿}äc•&Ë.»¬a|–Yf™(Í,³ÌbÆüÈÿª„+ˆBOþ	)ŠÏ__¿.Ì•6fô^í£\"Pp—Ø}÷Ý½?J°ªÅF,9IX¢\'žx\"5©ø^Å}UŠvðyuŠ%4ðfQ¿êô=¾Pûî»¯W“ˆÊžEÎpv4h™{î¹£ ¤˜÷Ûo?“5–^\r¨y¦Í6Û,òÃò5N3Í4}>Sâènã»˜‹ú_Õ|²hózP‚š¿ç\0wèp£	–hŠößÿÜ.ß{ï½¹q{XÜ\'šh¢>íTž&†Éˆ91Oâ\Z‡±htR®­Ygu¼úpè¡‡šK.¹$3/QŠ¹$\Z\rÖ|àUG“3	ñG‹ç*\\9)’Ãägþò?¾sHm¨6Í¥>M«(Å„¤ÐüÅ-ìÝ\'X8’Ûœ²ÊŒÈÆ$“@ŒE«hBâðÝkfˆŽç>Á:ãŒ3|²öDžð\nBöãÚÔÛn»-šŸhÆ\\Dˆ^^È—ò4­\" „#P´Õš¿¨ü^þ¾ñKÌ{E&#”,GaüQ˜d²åù§`n)šÔÅ½z½&SL1…ùî»ïœ»ÙqTÉF€XaÌ«³Ï>Û	&æk\ZÙ—;	³|ßÒ*á¹@Ó«¢(õA h=*jihþ¢ò{ùûÆ,væ6×v`ÉšHü_¾+2$Í-ÉÉ#&™^ò¿æw^ƒ©ÐUÀ3®J6bæF;j+ÿüç?£g#`ÙâÎ¼FS¬/c[ô5\"ÐBŸÉÐüée3ki<ÁvÈÓL3Í”;ø dM$Ñ ,°À&oc±:å”S2ëb—ßKñ¯â@JsŸ«pRÐÕÈµŽ6¤Ï\"Ky}Cã”F°ø¿Ë<S£¾ŒÛ0“´mB ô™Íß&,ËîK+–ÄÃbÇž%ø`.I“¸L€³Ê)Ò`Ù˜ËÄº”÷â‹/\ZÈ’«L7Ýtæ›o¾qÍÖséenr²ÒV$ŽV2}\\kkSÖOúÓˆ¨á¿¥¢(õA€8X¯½öš÷G	VucÙ\n‚ÅâÌË?ÏKtÓ4%q‚•µ 1rŠ*ËüÇÂGY\\HÜ‹Â57cŽ9¦ùôÓO­»Ïµ8ã7žuú^N(‰¥—^Ú\Z†<‚i²æu/ù_}ôÑGæ¤“NÒbPû9Àá¢ßýîwfÜqÇbN6ÙdfòÉ\'7“L2‰™tÒI£ç–ßåÿü=ñÄGŸßþö·f¥•Vª}«~«º¤‹E\"Ï‰ï³\"X)_”‹/¾8ó`QQÌ(E!l´¦¦[o½õœÖ¹¬ÙÅ¯¨©¸”Ñnñ7t9¡šG°lË¿Â^Úé\Z„Ûô£T9xÿ±q’{uZh!ÃŠ—ÛÕMüÁ¥–ZÊ,¹ä’}n»ÈûÄÓ’—»^‹êáûõ×_?je/¸à‚ÑïþóŸÍÐ¡C­òÛÔÑ­4<ð@¯èe´†`qGæ¹,_ž¬XXÉ˜Uy$*ï®BêE#7TÉˆÕ¼P^\n‹/¾¸u+!´½•Ý\Z”œ„«¬²ŠS!XIí®üßÆ÷Mž	ÆJEPü€¸ßqÇÑ‰v×„m½õÖf§vŠâü]uÕUæ–[n1Gu”Y}õÕÍÚk¯m8<„oïFmÔñ>Ék®¹fÔÖXÚ)ä0Ói§ýŸ›8øI?xG¼óÎ;þ\0µ,gk–Íki§\rYhX´D^}õU/\rŽï”o{*«eó¨¯;˜	gžyfóÆoXu‘+Ž=öX«´šÈD5˜ggu–þ´y/šZ›ëX(Cýä¬ ×DŠÀ\0ðFÛ³îºëšO<Ñà¯\nÙ²B³ðùòË/;ú1b„Yc5Ì÷ß_ØLÞ\r«Ë/¿Ü2Äl¼ñÆ©÷ËÔ²­!XBŒX²$‹`M=õÔ}YÄd˜æHÌb4×\\s¥™+ë»–Í™ÂîÀò„N(LGÃ¹çžk•Vsûí·Gd\'>góp‘ÏG\\l6$’¾çê\\SÊ@\0r9â–´Èl@]ò‚#û·ß~ëš5(=ñ		¾í*ô²µ÷Þ{GQ£F¹Ñšô­!XŒ*Ì¼…\'‹`Å}QòÈ[š¿ø¨°ø©sÙe—E*c`‘^Å—p\r2Ÿ“[‚%s»—ÜíGBS*Ù|õÕWf»í¶3Ç|09:ÿüóƒ‡ŒÍ[o½‘BíUV=ä}ðÁ#ÑðáÃCšÓØ¼­\"XENè,LI\r~Sqá}÷ÝiN>ùäƒ\nyKsJcgAÉ\r÷Ýw\rQÝm‚ec¦²)«WÒ )eÚ„kÈ\"R„4mžÇq”ü,*Š€\"`‡À_|m2oºé&»©Ð|¡Åâ¶‘N>aO=õT)UAÖV\\qÅÒÊ+¥Q*¤UKÌ„gžyf*|,J8íÅr•<•Í…£­I!Úÿ;4†µ¬†Ó0-ÌƒìðTì€ìdm’¥d,[ÇuIÇE@(F€‹éñµºçž{Š;¤à´^UáâÍ`ó•çjãÐä¾¤„ãYa…Ì?þñŸìÍÓ*‚Å(@t²Ž“6,r.ŒZ©äé(¯´;»>IA¦;üþ¨0G}tæƒÁ©|\n8‘‚­^ÅÑ².²È\"…™„ %_˜;®H{˜åÃUX±&PzîpšºlA‹Eh„´5¨Ìº¶ÜrËJ´Møe¡Ér‰“Xf¿ºQVë;„3•$Í†äKóÕÊ*ëñÇwJoÓ¦6¤Ùo¿ýr	ñˆy²ÜrË.2VqCÀÖKˆTZˆ›yNŒÒ1ÏUE Ç{Ìì¼óÎ•ÁD(‡*O]koÓM7­¬ýl÷ÙgŸÊÊ¯[Á­#XÄ±5Ÿ0Is ­ó¯¤kúºM€ªÚƒ:;M“xÆg˜Yf™%º8xäÈ‘çMâ¬¨¸!à+Í÷Pæ~‘KÂBðbTQ|ˆkUe¨ï¾û.ÒU¥ÅB{õÒK/U:ÌÄ÷²\rQQiC:Pxë˜å™	“˜®ºêªýˆ\0ös›½”Cärê#ŸÊxþùçÍì³Ï>àEÀYœ¶<òÈ#ûNÖ°È9[+¶ý?,\\²|\nùÿ3Ì«íUÿB„5\"`¢«­¶ZåPÜ|óÍæC)½žçž{.ŠaUµœzê©=s§i+	ÎÕÜ‰g#ø±p“¶s‚E=½t…ˆ\r¦’mç~øáÜlgžm¶ÙÌÇìR|O§Íi‘¯Ç¾ûî;@K+ÀÉiÄ<-–ë¥Ð==(ÚùžFàå—_Ž®‘©Z€b ìøR¼«mD‡ôGý*bH›ªÊÛJ‚%‹ßHRÛåbòÿ+upOŸž¨³9îkóÒC\"«Ø! \'f¹W,OP÷gitÅüGYøGŽCqÑ\r„Ýxh*E\0ÿ(Ûø¡hÝu×]æ ƒ\n-¦/?Ú«N]óvå•WFWÿô‚´š`q5A‘$wè.KÒböRIG\0Óé<óÌSÏ/¼Ý%Ø®°’–%˜{î¹#íT\\‹QâÐÀõ–¿§šjªè“´·”È\\N¦Ë2/¶JíŽ\"Œ\0ëÀ”SN\\ŽM¼\'×Zk­Ò´XÜ¾ñæ›oÚTœ†»gqéi%Á?*ÓiâæDYhl®ÿ«^˜(!}œtÒI­²/³Ì2æþûï·J«‰Lô’‚\0ÅcTIþÏ‰&æ¨œ8äo9-+ÚW6#¤;øàƒûA³yŽt<^G\0,¢Ÿ_tÑEâ¡‡êÛH…THø\"eDHñ÷®	É«»Ê(»Že´’`´­£»˜Idpp¶¶õÁRÿ»)fÊF:ÊîFÅ¬  ¢ÙbG}çwö¬øK4©©•ç yXƒÿÛÄÛ²k±¦RÚ\0šx4K’\r7ÜÐP\'á|?h¯ª<ùÇ‚w|/-n-Á²]lÙí3²š´sŠîî‹_%ã7^q¢Rà`9hÐ «´šÈÑV%wŸÉMÃV[mÍñ¸ù¿8þ\'„\n•v\r”hÉlãÊé¸(½ŽÀFmd¸Ã³‚B€Ð\n{î¹§÷§“kX/…h`ü[M°ÒŒä¤¿ä’Kúi¬^xáÔ…&íaQÿ»WÈL`—ð‡TÄÈúàƒ¬Ó÷zÂ´9(súÖ[o5| Q“L2Iß¼–“²ä…üò3br\'ëâ“Øëc¡ýW@\0ÓÑÜ;!Gu”yôÑGƒªêÁê$.A€”˜¹µK™\"¬’\rG`m„´Ûo¿½MÒžNÃÅÏO?ý´œd»ôÒK­Òj¢ÿÊMÞ¯)sZH„Š4ü}Î9çvØüNœ2>’.Í4Ž™‘ÿ“GEPìÀìJ|lj:å”SÌ½÷Þk“43M\'E¹¬ú£>\njkÓ2·–`É\"S4 \\l)»w~‡œÙi;åhÓžº¦!R»í­òùË_ÌyçW×®Ôª]2_“/È8ÁâE‹=q2Å&‚ÿóán0>ønM?ýôü1Ä§«V×Æ(5F€U\\nŒy•BÐÎÐK¥«&X+œÿC‰`•8VUvë	VüäT\Zˆ²CgQr5‡¨‰ÐnZrR­èJ)é²Ë.3»ì²‹]Á=ž*ë´,¤R”¼T•¹Ž©OÚ­öü9}ÒI\'õCí—­V·Ç‡D»¯ô!€«‘Ýo»í¶ÊPa3j»yÍjD•ë­·ÞŠ0èErÞ­\'X\\(\\$¢Á’#í¶×Þ(Á*Bö?ßŸpÂ	fÿý÷·J«ò·jDƒ¥ÍÁ¼à ˜´óð=ôÐCÄÌÂ/1-ŽVƒ`Ò¦*]AàóÏ?7[o½µáv/—-¸S\\}õÕAÅVõ¾%äfÁ_|1ºF¨¥õËfPaØø˜üä\'?qºKJ	–\rº&º{p×]wµJ¬Ë\n¦¾DÌAæ­8§ŸvÚi‘êî»ïN-|óÈÒ·ß~åß{ï½£üb†´90âÖrM­ôlØ/¼ðB³þúë›·ß~»ÔN‹å%¤Ð²	ïÃ?Ü`¹øä“Oz:xtk	Öºë®k}\Z]€8úî¼óÎÖs•<½‘Ö\Z””„#FŒˆÌR6Âc›m¶±Iªi~@@ü£À\r2yÊÃzñÅæ:D,KØpi³¹íøé (Š@:\\[µÒJ+™;î¸£4ˆ®ºê*3tèPïò$8ªw‰ŒH|nqõ¨BcWV;;UNk	V2P q§`6Ÿæ¯Ò©kR=\\ë0ÆcX5™+^.¾øb«´šè¿ÑÜã>„YsDÙHäi±dÃ/–h´ÔD¨³MG\0‡ïvØ¡4“!þW!‡‚¾øâ³É&›„wì‡|ðA³âŠ+\Zˆ¤Êh-Ár‰gEÌYxØ¹ÛHÖ	.›¼½˜†kppx,’M7Ý´Ô^Q}Mÿ^4µ¯¿þº‘ß‰“&ÌsqrÏ3ùÉ†Cˆ\ZwI’¾ÇÎ›>Ú~E 4;gu–!\0°­¿oV™u!X—_~¹ÙxãÍgŸ}VÔýžú¾µ‹S‡@ÆH+‰MœçÕË©ÿ¤á˜.¾Xyòþûï›É\'Ÿ\\UËö°šøFb•ËJŠcó@\Zü³H÷§?ý)µ&´V1!X¢Ñ²=	êÐ|Mªô,˜Ñˆûâ\0Î³œgî/—“Ž8á‡ÈW\\aØ‡ô#¤þ:çm5Á²5÷A¬¸¢…ÈÅ™—´,p*Åp%Ñgœ‘›{ªØ©Ø#Àÿ¨¼0#hŸâä‹—*/½ôÒÑn:)±¤–,ûqÑ”Š@h² Gûí·_QÒÌïÿþ÷¿ÝíGH¿ãdC0n¾ùæF¯ÒJ¢Ö,H“-Á’?Nˆ¶‚36y:uQ¦m»ê˜î–[n1C†Ém\Z—†Æt©cß«lóOL‚‹-¶X41Æ…`„c5Vô„-AK%\Z[þöqß«¸oå«(Š@y õát¡¯§,—\0›Vò®ð½ÖçÕW_5-´!¾žJ¬Áƒ[¬¸“û‰\'žh=W˜œ,N.y¬oYBìi¦™&·WDWIûçÄŽh¥DCuØa‡\r(€ÿ‰!;VÙXÌ1Ç©Ñ Ù@L;í´Ñü–üö­Ó”Š€\"`ƒÀ7ß|cV^yeóÌ3ÏØ$ï—æúë¯:É9Jn®l\Z¯Õ*«¬b^xá›ä=›¦•\Z,Yll™=kÒI\'õò©B\0™S)F`Â	\'4ÞKØ8ãŒS\\ˆ¦èC@6{íµWD„Ò´W’²ôÛßþ¶z’ìÓD¾Ó¹‹ù\\‡IPì9rdDX0rêÚV¸[ÔvK+‚õÇ?þÑ¶º¾¶sÌ1zg¬j­$Xì¸!LYG	néã´.À,r*ùp/Þ•W^™šUùrË-§: ×,1w™óY\"\'ã%VðCžŸ-·Ü2ŠÆ,‡æiRE@p@€KÙ9ä(‡m)4X!amÐžÙj°¤MhÚð»r!‚0´*i+	;n[ÿ+F4éYD\\µQ,BäÃI^%:Ùù¤ÉSO=eÖXc\r…Ð‰õ&æ;âëd‰¤o:HO^®´(.”U\rVJú½\"à\0„…MèO<‘Áâ*2_AsfK°hÏ×_]býá‡úVÙSùZG°Ä<hÝ–Éüãÿ8\nþ&>)×]wÓ$|z.64…ûì³Oj¢\'Ÿ|23l€Ó`ôPbæ¸«9çœ3÷˜´,\"?‹Hü·øÿ²à/…ÕCL»Úq>úè#³ãŽ;¸¨=­!\\¿Ãšãã»%åq•\r§wÚi§ÂV¬ÓO?Ýp`IÅÖ,©Ñ(ÙÄä8å”S\"r%»xò—ÿ-¹ä’Æ–h‘O€‹\'Ü—_~™©Y„`qÙ¶Š=bâ#:tÑ|2µ ¤Ë&ü‚øcnCEPªC€M?Ññ¯\"Ò:¹á9GÐ\"tÐA†‹Ùm´\\E-¥Œ‡~8Š‰Fá}B=|‡iÓÇ«¯¾º9óÌ3‹ŠÓïc´Ž`ÙúQ±#rÅ	‘W^yÅ \r ¾·Ý±s¹%yÊ¾Ì³M³óÔLÚ%Öª«®Ú¦îVÞ—ë 0&ƒŠæÕÆD˜–¿òjŠ@\"€oÔE]m:	ÒŒä3ÌmP×^{íè®¿2ÈUÞgŸ}62Qâ¯ÉÉbB0P7u|ðÁ\ZŽÈc.¶Š`¹˜eG1Jîþù[bisÑb-ºè¢ÃÐ;YxI¤ÉÇ¬Ëq\Zà/èâEÚxÌ^ž.ùÑîêüv$M® \0‰bcÊ•:h³øY6±J6ú¤.~¯º¾\0xjŸµUKL&o¼ñF!ðB°²Ò}ôÑN‹êI«BØ£ÝÐ¨Q£Rª«¿x\nÈÑ¼óÎk	ÿ”SNÙ—ÞEF&qŠä‘G¬ëÔ„Š€\" ô*­\"X,¶Ž¬,_ùÞÅäçCÊzmâ1FwÞygj·QI«Ø! Nç.§e“ó8¤ËVÄL¨umÓtŠ€\"ÐË´Š`±;·]pDÛ•e”ÅÄF&HÌ„×^{m/Ï©Ü¾§¬6Ú(ÊC$wNµ¨# ó×v¾Sb|SÁÆ\"î¯¸÷Þ{›Ÿÿüçf‰%–0ÓM7]ßÏä\\ßÄâj\nE@PzV,^þy±€âC]d‘ÈÕ‚™¢÷øã÷öÌÊèýšk®9 ˆž\\¡³ÔRK©#¥å¬mT’`A” ¬‹/¾xD˜øì±ÇQ©B° gò»</bÞ–Ó°ò3Y¾šÁ-H“)Š@Ï#Ð\Z‚uòÉ\'›É&›¬ð¸ºŒxÁ’Èæ{\ZqÃüâJÎÚ<qš<þøãSc]Í7ß|Q×¹Êå¤“Nj3¥õMÔ“ñÞx’$‰¸:!øŽ¿wÞyç~§\n¹¸•cßòáÒsâìpš)9¿mÍð¥uVRE ´‚`Ad 4.æ’\"-!Xÿûß†•…jë­·Ž/Á^N pSü¯~õ«Èì4|øðìºë®ÑÿYd#¿÷:nEý—¸V<ð@¿¤2ÿ HÌCH’˜ÁÅ„ÍÜöÝ@à\'§¾rE££ß+Š€\"`L+3„³‰­@°ò\nY€|IZL…¸Í¶]mIÇ¥Î§žzª™zê©£ð/¿ürêqßóÏ?ßÌ:ë¬æ®»îŠ´[³Í6›!™J>B–l¢°ÇKÂ©=N°\\æ7\r¨«3SP;ZA°xésWšmÌ* ±5rÛ¸‹n¬±ÆêbºÛn»¹do|Ú‡z(ºSpüñÇ4xwß}wnL»gŸ}¶¹ï¾û¢1¼é¦›ÌŸÿüçÆãPuþùÏFdgª©¦r\"ñhzÑzqxƒü.Ú(ÙÈ¸ÄÎª\Z-_Pº\"Ðx‚uå•WFÅ´ÓNëäódK°\\ý¨ dq?ˆC/aÚºÌ2ËD·»ûž¤œX  î£h“ÆsLsÍ5×–Ç\\ŽÇ\ZÛn»í¢gÇFËŠ62îÛUX™&PE Çh<ÁÇ]^þ/¼°õpÚ,[\'w)Y¨D;ÐæÀ™Ü‹µÖZkEW* ±ûÝï~g~ýë_›3Î8#ºÃñ«¯¾²‹xBˆ\'àTŠÀÙ\\HÏ~ô#ó³ŸýÌp\"sÏ=÷ŒÆ€ÄˆÿCÛŸÏÜ1F¢1|ì±Ç¢<2ùÉß‹-¶X?rµýöÛ7NS(Š€\"Ðã4ž`	™‘…†ÆfGŽTž©CÊM,Êf±â\'Zƒ\r6Ø šB¤sMQ”ø6Ì¹_|Ñ 9!\0€âÈþàƒFW¡ábç,ùì³Ï¢àÑ>^~ùå}d€Åœûñ.¸à‚6@Tyd“<5˜÷wr>s‰+é!g4~g\\ù›ŸÉ²Ø4@ÌTE@Pòh<Á¢{˜ä8â/‹ÁO~ò“h\'Ï‘vå äñÿN8¡0ê;å‘..Bž¸pSH\ZåCÈæž{îˆd¡Ië?•×_=ºý§?ýiäËóË_þÒpÀÑÅ |RÉì·ÜrËH!#=š/â^­²Ê*‘YM\n.\ZÕÜîµ%ä¨ˆ`±˜­“§©©(¿|ÏXëØØ¦RE ë˜cŽé[$X¶ÝvÛèoH—/~ÆeŠ)¦(ë@|&ÒÅeÄˆÑb¡`±¡>êzøá‡\rßÉbDš^Â0ãjØ°a‘ï7À\Zà¬³ÎŠÈÆƒÿCÞÿ}ƒy‘	r‰(?Á’^,ê6k8q)s0I”˜ƒr\"ó²Ë.Ë$ýÌóK/½4ŠyÅÊEçñòH£äÊml4µ\" ô6­ X¢UbA`¸úê«#„†EÈˆæ=Ò²èä	yH·è¢‹šSN9¥/)åòÿÝwß½d\r\Z4È¬¿þúÑÿ¥îÞžZÚûN!Àœ‡%ƒŒÆãÂ_Lº&Û…&Í¡\'ãÏs\Zr¬¢(Š€\"`@+V\\“DÜ%|IdA€`±øÄ³‹‘ÍŽ<¾hA¨Dð’,©¹Š“9û¡Ð”Š@ÿ¸ö‰ç@_ð{V ^!Sl}ôÑ>7Ä\n‡öd4÷°VjnE@PzV,†\n²Ä]Ì\ZÄWÂ	]\rN´[,6,F6òí·ßš#Ž8¢ßÅ¸,BƒŽÊ¡|Ñ\\aª±-×¦nM£¸\" Ú,!Zø#òá¹Øi§R‹cƒ0ùä“GiÄÑßÕçÊ}M¯(ŠÀh\rÁ¢KbºKú¢°h Ù\"P\"¿³˜¸Šøcák_„âu±P©(u@\0¢…–Õv~&µ_D|·ÑðÖ¡¯ÚE@Pêˆ@«\0è3ÍI7¾Ð$/Èµ˜ûï¿?Z¬Ð\\ñ3®-ãèz¯µÁJÓÔ!3øØÌO6—\\rI”VÉU=ÆP[¡(ÍE u‹¡ “*vñ,\ZìÈù¿û\ny¥Ü¸Ë·<Í§(Š€\" (íD •‹¡:òÈ##S w¯0‘ŸeÄ¦’r)‹òC[;§”öJPE@PZK°Z‚â¨+ÈQB\'œß].—.£^-CPE@Pš@«	V3†@[©(Š€\" (mC@	VÛFTû£(Š€\" (]G@	V×‡@ (Š€\" (mC@	VÛFTû£(Š€\" (]G@	V×‡@ (Š€\" (mC@	VÛFTû£(Š€\" (]G@	V×‡@ (Š€\" (mC@	VÛFTû£(Š€\" (]G@	V×‡@ (Š€\" (mC@	VÛFTû£(Š€\" (]G@	V×‡@ (Š€\" (mC@	VÛFTû£(Š€\" (]G@	V×‡@ (Š€\" (mC@	VÛFTû£(Š€\" (]G@	V×‡@ (Š€\" (mC@	VÛFTû£(Š€\" (]Gàÿ•¼ª½uš\0\0\0\0IEND®B`','\0','2012-10-31 20:50:46',NULL,'system',NULL,1),(4,'Chiropractic SOAP','.png',600,100,'‰PNG\r\n\Z\n\0\0\0\rIHDR\0\0ë\0\0\0B\0\0\0@I\0\0\0sRGB\0®Îé\0\0\0gAMA\0\0±üa\0\0\0PLTE\0\0\0(((***+++---///000111333888===@@@DDDHHHMMMOOOPPPTTTUUUWWWXXXZZZ\\\\\\]]]^^^```bbbfffhhhiiikkklllmmmnnnooorrrssstttxxxyyy‚‚‚„„„………‡‡‡ŠŠŠŒŒŒŽŽŽ‘‘‘’’’“““———šššžžžŸŸŸ¡¡¡¤¤¤¦¦¦§§§©©©¬¬¬¯¯¯³³³¶¶¶···¸¸¸¹¹¹¼¼¼½½½¿¿¿ÀÀÀÁÁÁÄÄÄÇÇÇÈÈÈÉÉÉÊÊÊÌÌÌÍÍÍÏÏÏÐÐÐÑÑÑÓÓÓÕÕÕÖÖÖ×××ØØØÙÙÙÚÚÚÛÛÛÜÜÜÝÝÝÞÞÞßßßáááäääåååæææçççèèèéééêêêìììïïïðððñññóóóôôôõõõööö÷÷÷øøøùùùúúúûûûüüüýýýþþþÿÿÿ\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0ÒÀk½\0\0\0	pHYs\0\0Ã\0\0ÃÇo¨d\0\0\0\ZtEXtSoftware\0Paint.NET v3.5.100ôr¡\0\0ÆIDATx^íY[Ó@†ãZ­E¤Šâ7DPÜPÜPëŽŠŠ»(TAÏŸ7iÒ6mgæÌ¤é<X¾^x!“~Ó÷Í™œL)u•BÀY)/¯“Ê®çŸœt“zô¼˜­¡‹„*¼–)]OžY³!s()ÕîþÌÚõ½ß*l#¡†®eJ¡ë‡;¿ù“ì2÷º«ãIä)‘ Äk“RÁõÌ…í·“5í?Ûð–ë¿ÃgE‚¯=J×wö¿Ó6ÕIôyÇã¥`8äØ¬Qò]86¤/Ð¨q¿}àká™‘ l‹’§nöZ÷Bƒ\\ÓÁ+þ*Ž5_K”<×o÷>ÒW¹IÓ9èË¦IoÔ¬,QrèÏýÖymá£5œhë½%$°xíPrhfð(;—È\0C×nß\"X¾v(9ôãÜIv.ñ]÷¹‹H`ùÚ¡äÐ”{‰K}®‘Àö]7ž\\sˆì˜øÏ\\ÏÕrK˜ŠˆU}“„’¸®ƒùŒ¹Î~}×Hˆ²’UD²”Ä®ÛŽHW6éù4–¨–®HÐq,%Éõº\'=&±-s=˜z&:Bº†#!‚Ë%Yo–K‰ªÔ›žØu>›¯7òë5Ê²íP’öásÛò¢BºÎ¥%Ë€¢7CB‰™JŠ{®ë«s\"×=é²ë»²GBˆÍ%ÕýõDZÐ¢ÕºÎ·È;9æÞ	lõiR”Ô{)‚‚­q[}S±ÁÝ_#wM”%fß,—ºZ%²Úµ›žPí<q®		l]{¡Äí‘æÛ²•-Z¥ë‰´«Þcd]¸5Ü\'œ%Î5Ñ`º¢E«p-n®tv	¢cÀWDx×4–é‰˜‰¸–Ý4™ºF‚Žëú)i¸¦¹#-åkrÙõ˜l»ÅØ5´ÞÓ¬×ƒŽk¢›å^»äº?%ÛF5w½÷¯ëó çšò-áhè:ßVüz{³ðx$èüÎH]”4]õ§‚-pý,]}/f¾GZ}Ôeü´JÚ®‹­AÁµüMªêéjuáAaˆ¥óø”ô]{\rTÆkÑ<Þ¦¨âMôÊ‰š¸F‚NaÇ§dàšhÄkÑœÂ¿Ú#×HÐã\ZÓƒ‘k¯¢³N¡ºµ†®‘ E6ž‡ÀuÆiÌÃ-½ƒƒ9a[”ÌêºÔ‡k}þ ÓºF‚&ZÃÏßøàšgÛ,ç+\\Ãµ”@Œµ£ñŸh@wÆb\rç?Ç\Z¾²85ÃÊë5Î¢®yFÍ´þ¡®yßÍRp\r×Ëòn×ë•s¾Â5\\c/¥L YzÔ5ê\Zuº6ü•M³¿Ø÷\\ùÓ½ü2Vãn	`¿#h<%‡¦˜ßUNÔÐõ‰S‹HàT“J-·³s‰0t½ëÆX¾v(yê^v¼a\'w\rÿ½ñÓ_$px-Qò\\O;ËM&v]¸ÓÞ±HP¶DÉ_’G»\rþX¼Ñ\Zþj×¸WÖHP«¶EÉW·0ÔýS»²Wi$šßs#ø\"$(¨Y£T(ÓïÇ;Çujº‡èýî¾âßß@‚”¯=JÁ’üëbëpÂ_ÅFt·ýòLé\"A\"Û\"¥âå÷iKª+±oXôŸhßºÍ*N$øZ¥TnµF{u}~´æ›üPCØ*%£¶Z÷šŽqË’\0\\/K-\r™Ô?TXÎÔ>ý)\0\0\0\0IEND®B`','\0','2012-10-31 20:51:08',NULL,'system',NULL,1),(5,'Massage','.png',600,300,'‰PNG\r\n\Z\n\0\0\0\rIHDR\0\0\0\0X\0\0\0§1£í\0\0\0sRGB\0®Îé\0\0\0gAMA\0\0±üa\0\0\0	pHYs\0\0Ã\0\0ÃÇo¨d\0\0\0\ZtEXtSoftware\0Paint.NET v3.5.100ôr¡\0\0ÿIDATx^ìÝ÷·dG™¶iþ±™_fÍ¬õkC{úk‡i ñÞ;á„wÂÉ¼HHä=ò^È\"oJ¦ªd˜¹vÞ§‚ìª“Y•ÉQ©„´W®<ûìÜ.\"Þxž×EÄ‹{tç¦ŸGÙ±è³è’-?þøcO¬ôÙÚXRÏîOk4Ù\Z/¼…OÙöðã«~Ù¶}ßy_#IÛþø“}V•ÿ5ÞvÉ%Š¿è³êƒÜgQåWÉUÎnÅß—\Z›?gÿ”eÕ·rþªÕåü½ÖØ\Z÷Ü,ÝÂçŽvß³J_´µÙÂ»\r1Ý–ˆÅ>=ÉØÂn¹U·ÚÚ2®w·UË²*8=JX¯8›^µj×xôÛZÙÛký÷¸Q„õtQ\r,AØ%—ì‡ú_ã…‰åò¦\\C6¶ð’UkreJX¯ðkˆøj©ëõ½åºÕJm¶…·Zé¹ÏÄÉ‹zøß+$íyÂþ¡„%‚±šÌÓ~è±M?KjlÕ6]‚Èóî‰»qÃªÏÚô|·]TÌõ@yUŒ[µkX	k\\²ê[­}þIÞ´&_´*X/)ü©=ï¹DÄ—4ðª­E·Z’–¿ðªÍ³µ=|Õ&[õm—[Hk”eú_ƒÖ þ‡|ôÁñí³›i²jGZï…{ôžºèVmÍ½RÂ¦d°Fe.z±5(a\rY>[XcK¬„Uuˆ5jx=Â^µ2hJX£-7­èU+eùùktþUÅeIÁ×³Ò¶ª&—+K@aÕ&X£’×ècÜ¿íþûö\r”¡°;ä®ô½ž³R={ÜJXU³^CãÞÍ,A;kTæRÂr1[•­Wj”ô¡U;ìz—¬QÉkô‹U»Þ‹¶°ð«>{mä]äSÚÂØZäÝB½~\rá[µKìµW,1ì¹Vmšý ú^i rZùüC×°8W­gX„ûKŽ¯ñ”E•?o‹ìVá«¶—ó×P–ÐØ\Zõ¿ìª%VÂª}|mÚ[C\0VªÌ…”°¨—”|¯¿çµËu±E…_”†´†f·jC®Ú[~þ³N	k”h\r|Ù?”0Ê’ôžåÜà{v)aÞo–´FKKÖÀ÷5di%tëÝV}Ê³ÞÅÖCÅ5Š¹ie®ã8ZÏÞYõª!£òò}×…Ö¾-”¤%\r¶j«¬qþzÊÅ=qT©.”*êî»î·¯ÎC\rGì,·þ4éÚ½FtÉª¶Û³v»|\r[ƒEöÚ.»qÆz\07oŽTL·Õ¬¿>ãì³Î<O[»­æ.¬Rs¯Ñš[XcKnµÆ‹má%{µÆ³:dÝ~Û]—^rÅM7þî‰O¥ËV:U½(ý)dÏZÝœÖîÏô…Ã;¤ï¹ûûîE1lís×À÷5.ÙÂw^¯».zçdtþW¾õ‹.üí©§üêÞ{TÛÁ„÷‰·öéóýgž¶6	r+½[m!$­A	µÚ –0%ú?ñ„ŸÿìäSï¸ýî(¿÷ÜrJX¯Ò6½ê¿}o 5(áÊ+®=þ¸“Ï?ï¢G¶M5œ^[…/¹Û¢WÚZJØ´’WNBÝÂ^~+…\'>°éÂ.½äâËïúý}É÷¾Ã\Zø¾Æ%[øÂ[Û+útFß;¶?ñðCœò‹Ó¿óíï«ml‘3¡,ûÞ…öñÌ¡­TQ/PÂÖÊØ\"J\0U\'ÿ3jlÝªjªö±áæOÛB!ÿs².8ÿ’C¿÷£3~uÖÈjÓ×ÔÛ”°p,ôs›Æªâ˜?ÿÙ/áÔ­·Üùè#’oaÏg­Ñ÷Ö¸d{ËzÝuÑ;gµñ±GÑíãœ	?úá!Ej)s!nøÓ=Î›¾É”°ï¼†,-±N>é”¬„(?=l=[ãÅV½d½Û÷ºÝë™kX	ÔY½éœ³/Ð‰ò\Z±ÏzÐVÂj¬P&ÜÎO«»“NüV¸óŽ{vljkUÈ5ð}KVý%çom¯(BäMö}æoÎýñŽ¼åwwô¬(!\'ÿÃ^{Îª\'¼@	û^ckÈÒ\"Jà5â!üý÷zz™¸ÏD,aþs²¸aûñQçžsa| ªiºÃ&[Õ?¶µ>’ç˜ã(Rpê˜£O˜©3Ô[·JÎÖÀ÷5.Ùª·ÍÀßwøØë™îF@Åi²\0‚F.S÷w7ß>t´1T›½Þs^ „}¯´5déJØ÷ê]~æzVÂp85+J/Óã–{G—ôñç5%”ý¢îÄ~ú“ãh4¬„„{^±Ä=µH¶ð’-|á­¥„t¤ë\r‡ð›_Ÿƒn¾é¶„8mÔ[Õ»æïó%ì{­®!Kûî8J3XOÆÖx±U/YïÅö½n÷zæ\Z”ÀJ`sŸ}Öùés¾‡kîÇÑ&Ž£e|øèŸ>÷Þ{ÿ‘Gõ³Ÿýâ÷wÞõØcÛyä±mb÷›~Ì¸YÕÜig¯Í¿ÄzÝíV»!Úª‚¾Òù‹½ÞñGåÓ|DeªØiçáGÔö)§üòÛßþîï~wëC>üðÃ<ôÐ6\'´?;Ï>¾ÞJU±ÇÉ|\\23¿³D`Ù¶rÒºXË¦OYrÐÈ6Ùó3Éð¬Î7û¬îÛ¶°_h8O™ÞÁË<ò˜|ðá“NúÙ©§žvÏÝ÷:¢•l;1xðí½•/¶ð­\'c«_µ°˜ZsClfU7Õ^U·yfÇg9E­i_gQŸ^pÑa‡qÁ¹çêvW‡êžplaëÿYY	›9^J	!õzß}÷~øá\'Ÿ|òwÜáßGö¢íáeU9ÏóéÞ«Z	\Zl­Py¦ŸõØc«Ë	ìgµêûž{îùùÏþÝï~÷öÛoŸ¨àÑGzè!Çmíl[\\É‹jr?Èñ¬¢ó¶{nKf-JØü)›>zùÁ¥’¼‘º\nÌmÞ/4®†ó]ûöPG|ðÁ“N:é´ÓNÓ¿ôïãÜ´­ö¯IVV~±˜\"‰zP£êê {ŸY­uò¨m—œsÎ9?øÁ.ºè¢íÛ·;®’ÝÊŽzî´M¶‰W6W¼öCWz–“P—P1&¸êôþûï?âˆ#~ö³ŸÝyçUúª”[áV.ÓòþöLÃñpÿíoWÃU©Úö\r ÀÄ~ô£»îº‹ø’ãˆáJØÕ^Ï=J U÷ÑÐðH·:î¸ãhZ¿ÿýïkÜ7l-W=)aýç!~¡.º‹	TcÛ`…_ÿú×_ÿú×Ï:ë¬î©Cá;:Ú”°šã¨jU}÷Þ{ïüc²»6%Œ<Š¦3Ãâe/ùýL¿Ì”\0Ô9+á—¿üåøCH1™¾OfÄ”0×Ï1J˜G±ø@CG	ÇsÌñÇÏLo	^ „E¸¿„)c„ê¶m°ÂùçŸOÁb+T·Ý¼nµXµ}X	‹\\ÉË@yfæ#LÀqÂ	\'\0)±´*7w½@	‹ã\")*™ãè{ßûÞ­·Þš9N‘|Áq´†hUÖ\Zþ™YLh/[°5¶gÛm·Ý6O	îÁ…úðŸ¿ãH-íë*cøÜ6÷ô,¦„k®¹†Žuå•W>ùä“©_j%Ì›n»ßóùà8ZÅ%ºKsŸ)§6ZÌ·¿ýmêWÆz”ð‚ãh%ð¥Î$¬z‚J>õÔS¿ÿýïßrË-þÍé™È¾@	K\"kðÄÖúg–SÂ.ÇÆô7€óÂ<„Ú:JÛ2Çìäxz^P¯±Oþ÷Ê¹ã†¡°›ãè²Ë.;úè£Ï=÷ÜIÚÅÉþ¼v­A	°8@T%° UÊìbƒë…ðòjáëA	é‰ÙŒÜŸüä\'ªÝ¿ê?Âx!–ð%ÔYfÙÏ¸cÇ8uæ™g²ÂŸÕÜK»Øó‚8«±Â¾SÂ@¤ùzŽw/¿ür½I=½JõŠ(Œ›o‚f •Ÿg>qsÓ,ò=ÝA«BÿH{Ï·ÓåeFcép\n6QZ‹%k+\Zâ»¡pí-ãh·×[>w$Ô%©–ÏtT`Éý‹á;aÔ¶#…UXù…J±ðí_¡°£Ž:ê¦›nJ‘\'TÉ?´1Œ¹fjðs_ŸÅ4‰õ2Ž”{õ|âýKX9	õá‡¶\rÅv8‡\n•ê\Z7hV}JûÒ^¹¹\n¿ùÍo`Ö…^xíµ×2Ê€3´»täÔÛÉ 4:eõ,äuüÆ«?eÕ›e—n¾áe/$0ÙãìñSñƒ¨tXZQ¬#\"srÄlÔ°:gyûÆ7±…;÷”a=ì25ô²X+T³½íyÑX{ $Ñž:ß„^¨÷«Û/™åf·‰|÷œ×wÏ6k…h JðÝ`Z:+.Uãš‡ÇNqs^i:QB:lõ»†›{ðò\Zò½RÇöJ	šCÅ:­á1Ä£“dNUÇ{\0Î8ã‰‰_øÂÞóž÷|ík_cœ	* ‡.¸\0ÜÐ+g`š\'š©Õú<\')a±dq}îJxdÛæã1—fèm$ãéqÃPKë/^wÝuœEr\"?ýéO¿ï}ï{Å+^ñŸÿùŸoyË[Þüæ7¿á\roxÇ;ÞñÑ~ôË_þ²l@–60ñO¡„UÁzÿœ¿ˆv‹çY6tÿqÑ#JÛj“áÕ7ÞxÞyçIèÒƒ:è 7¾ñï}ï{?ùÉO~èCúâ¿¨zš>…}Ë;*Œ÷G»ááiíŠ ·TÉprËçÞTÈ7§„Xa4Ì3G	Ê<¿ØaUÐÄœÂ›7ß|ó\r7ÜpÕUW{ì±h¶LùÝÂ	µŸãSºä–j«Z	Kžþ¬SÂ®ØþÄÎ§Q‚:Gÿ;w<I¦/½ôRFØG>ò‘·Î¶w¾óÿõ_ÿõªW½\njà·¿ýí‚‰tü³ÈØ4¹vÂº×ŒÞýUü•Ç%,ñŒ?)aÒh÷êÑŒÀVY_|ñw¾ó­¬}AØ¤‰¿ùÍoJï¦\nüâ¿øéO\nÂ>ó™Ï8ímo{àì³Ï.€—f6ƒÅ•Í—­í•[x·5(!£Ym”HšUV¤zfüêW¿R‡üàQ,¢µóþ÷¿_Rá*öÝï~·å¸.†\'>ö±©sìÙÜ¤šoûöI5×kæƒ ©Ëû¡+M”°©}0Œƒ¡ì?VBž‡&„ò ¹¡×\\}ƒ	{¿ø…¯Ê×½îuªïMozÓK_úR5‹]‹ÝðTƒ±tí4qì–ÎÈ¶ÖÊýÐËJ5lAß=8©óÔ\r¯tã\r7~Øádô•¯|%‘­zm‡vËàw¿û>¾þúë‰ì·¾õ-L ‘Çu×Þ‹k,U1»ç4mËÀˆÏkJxàþ¡v•Ï\'g _ÐW¿úUZ*2€VT«ßþö·ZŠI™·1¾õ#›k\0\r—– Çïõ¯½ÞÇjäýÈÝ4¹;Ygìú\Z]f?\\²%ŒÄ<¬ Bb4¬>û\'>ñ‰×¼æ5ê\r×r1€’K°÷ÝwëS\\>º#<òÐC…i/ùËõ¾ÏþóX9•wr\"=øÇÅÀ1\0·æË{¦ÝGCÕ†+`O7Ñ`‚g‚ò<Üwïƒ7Þp‹9™MÓÿú×½ù?þýïßAÌ[^8E?E³ )’@°ðK-«\\}`8I\'ëaõµk8ŽÖÖýÐKjoü‰Pû‰àÝ|ÿ°×¿îM/{éË>üáó&§§ìÜ¹Ý\n/‹ÜwÒIÜK˜ãs f’à÷½ïƒ¦ 4}fÑˆLÚåãþöWñŸ×”0›€aò¥Æ/X¯‘.€®¾újà®}gz¨‰aÁ¨<‚q1Híu>\'’®g\n{}#¨°zÆÑ\Zýeÿ\\²%ÙÃq¤\Z‘(Ö¤-½ä%/Ñ•˜\\zŠzÎ’Ð¡da^Ù.œ­Â¹ædÍ|üãÇ\"<x.§©yË–¨Ý*§zàüL, ¶¹ãh7®á^hÞS<¼:‹”ñ5b	35s§YV?ì\'/Ù«Þø†·ZéÉ´ÌG•ßŒ)¼\n§È(ýÁòtsƒ2‚3ô¶f±¯­´j&nîŠüìYÞE¢¼¿0qaº‘˜ð=wßÿ‡§ÿ°cûN+O¼óï}Õ+_ûîw½ÿüó/ ‚LcŠoHÁSWŒQå\\mñôÓO#`­ð÷ôêW½îóŸûòå—]Mvßå5¿¿Šÿ¼¦„Ùô_„øRà)ûßøÆ7¨PTQvž¶{ê©§üª³€*\'D	Bt©ºE R°47:!¾q	3ýsŸûP›¬ç7%ä@SQº†o>UzmUf‘€œ_Õ§­„IÀe§7ùÎ§[gÆ5ÐÊqŒ‹w™`*|ë­·/M./Ž.¶µë,Â·ý}<¾©’ûôïX”õ™ ¯e¦@6Á[Þü3`c‚ðšæÝÜ5xŠË•qúé§«Gæ-IUÑ¸ÁÄG–¼bd»ßÂËK˜ïÀ¤„lOìn*YS·¿ì¥¯D½&ÊìÑÇwîØ™çÄ$ ´¦®útÜ‘\'žx\"Â\nÎïï¼‡…áÿØÁç{QdP¼úÙ.þóšøMë ð]è’›âÉÑƒ´`Šm3”Ø×â¸Ÿ\rÑ<1\r¦ÍP(È‰?‡Ö×\rõ>žq®p¨Ç§±Tøýð”5¬„ <b``¡[\n¾úÑkFzìdo]rÉ%Ü°G“Ghva¼‹Qüªbß?üá¼L<Zí“Ÿ<ØrÍq@a?‹Ÿ¶¿gÜq´xpŸå”°Ü ØT}.^ÝOvÏ:à¦¼~Ï»?ðÁ|ä??-ÇtþÉñ€*xèÁ(,w}ç;ß%»÷Ü¹¦´­ûïŸfŒJÓ9ë¬³?ûÙÏáêO}êàk®¹¡Ë{ÄP]×³e­G	«&;.:­ãV|•,ôèé§ýúo|ëÛÞö®Ë/»\nÜ{Ï½Ml0’(@?áVÕèv8£KÈM¦¼£íâ7;N=åôƒ>ôÑo|ýÛ×_w#ÿòƒLis3ÇÝ&Ÿ%ï¼ji—XH	õÒÍ¶t†=%d+ÃËÒº6ûðÌçˆØs3ß¢J[Ì»mëªrÆ¸2ŒBËSKµc!âš±°méªTÄï›°sêhD‚\"Ìtå•W×ÄìÎé³«¹g©ÉàYüÉ.þüq¸Æ¼lŒüŸ\"”#ã¨srHððÐDU²œô9y{vì(2Õ*¶ÏD;ì°ÃÏ=çÜYsO°VjÓ id›¹:¶ß}÷=ò”Þóî÷}ê“Ÿ½ì·Wµ.úX ý‰x¦ùÀý\'+¡Â&ƒ‹	æ\r‚UÑÓóÇ²®3×Øã÷Þó€µ”¿ý­C?ðþû¾îÚ›2<×ŽÎàF/e:|÷;?øå©g”œšsm˜QþUwûä\'>ó¾÷~µX˜³ÓÜm6eñÆâ0›ö¥E3ŸpõŒ*,[ÄÞ¶x>@ÛÀzüêW¿š\r«“ç%#ÔRa)Ï²þ/T@Ì\'ûp)ÿšÝFK:äC8OGžõæ·8Æ³FñWë‘¬¼	%<4­8=Ô”á·]ƒxÊ„Þ|›æÓ^má‘[t+%å/’@C:å”S4hº@o¯WÊ2jôò¤dÍä!LQ(©¦Tw7	}\'ƒ`júroÀm±6°ð—j`ÔFà^5F–:ˆÌÝ|àà¾`[¥-# Î¢fL‹m	mæü)Ç2­w‘ªj½Zò—¾øUJskÜ‚¾ò€V¤5ÎQLÀ,©„Žôï–A7¡¥F6âœ÷ÝûÀÏN>å }Œ¿èèŸÛ­¿·¢²ˆB!JeµV”0ÒZ:Á™êh–?£²vÜtã­ßøúwÞûžþúŒ³Í»üþûzàþýç-%K@/VÏü®OpÇx™€#ÜôÍP>â\"JÈê}CZ…pŽèŽ»í–üßÐáJxæ)nJßç¶–÷ÂSQ€\r\r§_­Ç”ç;’í][îôo1‰”\'k\\a$míþIÎÌXß°/süyPBê;\ZFƒƒŠÌö¢ZqY›Ï.Š-Ñ«K6¥k/[hsž ÓJ°ÈüQGó¡~ô\'GÛp„Û§u›÷“ã(âŠ¯²Uó;o­¡€îžØù”RÝ}×}bJkÝ®«¯º.d÷è\"#©©1±ó”pÚ/=r°o*S¯Ê&˜1\rzØÁÍýµ¯~ë+‡|+xâ,·—ûhµNV‘Ÿgø|ðÁÒà8ˆáEVm›¡ãç8bÃúE”èù—ˆ-L\Z;óùJx–­„GgÿÅmdrnÕ\0}lãHÈeÞ0Z?J2¨ì#’“ŸD[Kî€ƒ<ŠÔ‹Îit•-[ä\0äƒÓC…ßÇúÅ(Z ï_!•ÀZoÝ§øü¾SBÃ–P(»êÊkþÔç¸=ø?„š%q”à·?b	óL\"§ªo9&>þØ„Ú‚>èã,)Lè)¹­ {EÍÛ“û¬wð}×ïïûÞwˆZzs—ú?ñ‡ýÎýˆÁ#¤Á|úàÏû\\xÁ¥h£;,I‘|>8Žä3HH—1-\'Ç“÷à»Q¶GA\\D	yê0z…ÑL\"™_ùÊWpÉBPx¶­„E/6sˆÿ™8Ž,{Ç¶£Àj».ÎPÈ>[|à¸$>é(GÒQš²à§;äDâx4W= ßH|0äáÏ†*RÇÅÒˆ:ÈX\rnUHÁÉ¼µ[¼a7ÇÑ…\\l¡M‰R¸¥EŽ£0>Ì.3ð›ßøn^¦1~k\r_ÐJ—LCÕŠôòÏÕ{pÃV¹ÑwlŠ•ðÛK¯@}ßúæ÷n¼áæ\"N=º1™Rž²Ø(wÐ<%0)N?í7™yºÔæà-†•OÔ‚ÎøÕ™Ÿøø§O:ñ÷Ü}ßÌJ˜øfqPnóþ–3â¢XÃ™¾¸Q6ï•W\\qÕF‚9Ç–WÚI¾‚=­þååV‚>R¸ 	Å)§bóíJxæG·ßv‡¸ŽQRK­6<Eó”0è\\ó1„š\'&÷Hs½å\nw0Q /	õ]ïz—!lÏJÈ-–òÔ·\Z áj€‰`¨Á,&<%mëËGƒ`Ô\\Œs%ÌÌˆÇn¾é–/|þ7¿éíB­A_\nñJø¾ÆÉ/ÖÞ\0ÈPñÛ”÷µ’[¦—Þó3Ë¦zXX_yÅÕ\nœoJÁ·ç6Ü©‡†ïÆ…è*Jì:vTèVí<õäÓ|S_þÒ×xŒÎÅKØx‰!öçD	:3åñSŸúTÆAnâá>ž%û½Æ\n¦ùîVz…¡:(ÿú€¥„yù¿éËFVÂM7ýÎ YŠ<w\rß7*m7!ô—ÜRj¿:9Jh+`0Â¤lG­,kž‰ðçê8ÊDè[1ÙÍ*AÍˆÃI9e(¨±<E6¿æD]ä8ºðÂ‹e~ŸÞÅM#¤¸Â\"+avþ#i½ö5oQ©ö“ã%Ç=.á©ÐöRÂÓOý¦Ë/:ö˜ï¿ÏüveÇÎStaž\"ÀÞ$Ã*­¿w”ð«ÓÏÌzÚ*;­úeå˜òÉ\'ž<ö˜ÐÏ/O=ýá‡ôØ^ˆÿy^æ/X–¢^·å%GBHLO´¯Ã/Ï8êäÒmî€ré/PÂ³˜qtë­·a}ÓæHoÏ_ªöäÂÂ\0yE8@°B×H5øÀ	ÃŸÞtL\n” –Àª(˜4¼LÎüsŠ%§hµ‘G”mmd†\Z0Á¿åheB	ÛT\'›†—%À·}¤„¦q=dt‚}‘×|G¹ÓŸéÏ¯½<Ø%ŒÎÌqX+ØW¿òÍ«¯º^r‹¡,Ò%vP~$ßwÜ~÷÷ýñÙg?&N¨fç½ÀN“´;üt¿»ùv*é­·ÝzîQ›[åÛŸ÷Ù”’—¼@iˆäU÷¶C@uu²kd9sˆ¡!K6BLÖKN^‚~läŒ6–2PÖi¾‚($ƒÃ&QMó´è69^#Œb2Gfói?óŸ…ËŠmaàwJk^qÛÚyVUãõ×ß¨	>ûÙÏÊŒ¬í|ƒ³,9-¶ø:´Kn%ÿjå+L?ˆÎÉÙùÊ]å Js,CÉÁœKY	UF\"±Ò¶<Ò»Ò­††¾Ñã½ž¦DÔeüÔŒ¼ÞWÙ\\ÉQ>·âsõ»¢8ÃçÂsÏ=_(Ôl=eî°\0fé0O:Ýô3’}®¸ü\Zn	©\\8)ëÏ4¸ÿVRÂžHÙ•í]ï|ßñÇ\\¦mª}Õ±Ä™î4“µ©uú›_Ÿ“ãH}¥õ±QÅÌÎx²qv*ýˆÃjî¼³Ï:÷Á–%¡>ó8µ>®J	Óà¾]^àvt~ž_!A>P£(Š}e(¤Ùñ#´@gläE”PºúØÐJ ŸŠ0çznÐæ°?&\"ÙÒ¹§7Ùó›®»‰À\\:ÒÐšÀw¥qíh©¢g%h,*¿¶n˜Un@pßO5e~§œéGqJ0¥ÐBé	‚Žu¶Ä·ön{å€ù²­¦å˜ÿ‘ÞÃP0 ª¨^rL¨“é;vC;]xÉÌqt§È€¯áYäº¨ï‹\"|ö3_|ë[Þ)ÃÜí§É±·vvÓÜ³\0¢þ\"#~{é•5ßÔòPÉ,¿h\"¹I¦ÃC	ÅœGÕD¹ÅâO£f3}N	Ž&†‚ ó£p £ÿ–¼ÑM7Ïõ™êG‚E¼bbà`\0}Ê]gê\0ƒhúôAcô—PBþ¢že“á\n‰8¦Ü$Ë -2•sê4/PÂ3o$Ýu×=XYÐHsÔÐ‘÷˜Ç|;ýk÷†•ƒÂ3ÈÂý¡ò×úyaÅBj™›H±pgn4ô*@~ÀZ	ÊÛ$EUÄWL”@ïa%˜JLÞ(a|‰¹Ã{¦8dýÎ¿u+Ž#ÀH‹Y3Å™ó‚,¡€†	L&öŸ¯ø/Î• oázÑ­¶ØJØT±õlèü™O¸])ä»WÞÃ«è„af\rÿËžšÙŸ7òˆª,?ñ™Aï‚ó/Æ®W^q­á\"3ÓzÏMÇÑª<A)IC×1‹Ù—jN|”@yìøp.Õçõ\Zÿ²¤«/±’øÔO@\0Y¬Ì%V!ðèxs©6q‚-lÒ^«–e­óŸ×VèçÇÍRQ‹‹Ú†ßßƒCIäÑô` \"È0%_98sSRdH4Ø¹I&…v$¼Rf .¯l¸ŒCsa­ÂÏÈ¹+ÙËO®PKr¾9_åX_È°p\n“”nU¡žiNBÎ‰»dt+ûê%ð{ŸsöùFÔÂ. Ör\"-\n/çr§æÞpýï8Ž^ñòW_ÝÍ\rcÞG[K; ¹OÑé\0\"›òúë_û6tž1Á”ÔPä%Oã“yäG£ÙÕÖfU9»pbØ˜ŠÃî§?9Îøç›ÿY,Á @cÙö[úÐZ@¶ÐŸ8‚%»í,tAÎâÄÃÑŸuO”MjmQ(ò’]<œ¤™·ýdg¯±„ÜÇu\0ç›)Þ4™÷wÇ7e€ëP‘Êsí4ÚçÖÌŽ£MkàñÇ¶Cyš¬¼#˜šk ßô\\C«ÌPô¥/}É@63 ™$ÄãcqÁ™)Ih¾üH´óa`w)ôâ|#èß¼I@Pþ¥ÓGÂ“Àô”5G[Ë[K	\n•î¯ËÀ\\sâ¶©+µÚj\"MV¨üNvøî.½ô2VÂi¿üÕM7þŽÿÜ*\0¦ß—deF§fºb.wÎs—40kk{Ï»m¥•¶>Ï\nQ‚’H¥[6?3Õ[úô#—tÓBn7Mçö§ºœßÉ¼û,Ì29\Zˆ0ËXÝvÁù—þfà¸™ŸÍ…	X	Fy˜s[tzQÕïœZ3–°ˆ–”…·&YL=Çz{3™$ÙhƒHbnjþ?J…Bñ_ä8\nô35x!œnŒ‹6„G‡1ÇŽòZŒ7Ù_Kq=¯­„»îº[ì·»$¨üVÅ	° šüœÁ`È(•ndØ9Ç¥	Cˆ6kJì˜H©sùšxK‚ÿñÿr˜\Z©Ãý:LÆ5 ~>(5¿¿Æ­¶œÒx†þ„éUø’ë³RƒèXYÃ™ÅyÞòÅ—õ‘³Î:g–ÿÍïú#~aÑ_þ›\"ÌKÂËð\rîýüg¿|ÝkßÄmÞ™ûiB‹-äœ‘9;Xaœ¥ŸŠ“ðê ƒrI•0|_ôFcÞpýÍP¥áeÍ‚ÒÓ,ŒÃòrÄ\rÍ\ZxÜ±\'±³œ£Y	†rÃÙ¿ý¶ßs(Èè¿é»­A	rX	x€Ø_ýÕ_½öµ¯ôŽP[Bê|ó;	1ÁòXBq‚Ä½›ØaÖILVŽŠÒ¨ÎLŠ(a?ˆß/O=M ÷z!Ãb51&%ËŒe£\"hbŠ¿ð_W’Á†šO¶RãW‡w)ÈE¸Gw9[ßhV \raXÇXJ)s¹ÅdYv´qËì*Ú?sªÕD†ŸV…ätÊJ3³È¡ßû¿6¿…)Ìê6›ËgŠ\r,‰%øõöÛî¢ãò\Z¡¾&Y3Ï½XÂ¦èásŸûü\'?ñIúëÊ°ÂQ³	zi1Shê±GÍ%kÆ1Íë\'§~ØQ²†€;>øØG?e~ÿF†Ö|Üª2kï˜@‰E\"ta“a…k®¾>BÎ‘g§É”¼Õž1†y ^Ô`®jpÜŠ<VÈ\Z” ß!¥fØ<ÂÜø€ö\'nÜqç”V^¤Úänr0Ç\'éðäV\Z–AÿÃo@s¬‡ä?õ ÜåÌ¶­tm\Z¯šNò³ù®m²­>Gé¢$Ô8róM==óŸÖº‘ÄÔ|BGyx¼’™xŠŒb£æ7¿?‡ƒÚKÆŸ¸`)Î@\\C„‘ÀRˆÀiJá%†#¦…Ä£–)C/^¤ª¯Á+_2ÉØÆìÜÓlØÍÝ½—ÏÂÉ±GÙ“g¦¶Áÿ˜àÂ.Ð>ècïzç{¿ðù/ÿüg§Þyç=¹F±${lú¨¶,r ®y¿D£Ï~öóùÈGÏ>ûýæÞ{ï;õÔ_~ê“ŸºîºëíS®fxh­Ã\'XõSG|ˆ‘1õHxùåW|à|ÛÛÞ~ýõ78ýä&›~þÝ*å~‹G{ÎàZ:-½ƒ¼Ò%‡O!duÚJu(b)eÜïoûPJ†ž©Yq¾ Ñi8¾Ä°@\0/ýÿüË¿øi[¦DL”Pôp¸CKD”\0±2‚\rûN	ƒ–ø÷ƒn8ÿˆÒ|ê®·Tº!(/‘ç%dFŒŽ7(Á%ÜP#k`žB=æRðoX€l ‘X]r4h‹+ÔÄ[›q´XôW1 µzGZ}¡ž•ám´š.Æù#µ¥Ïç<Œ¶1=ŽÓÃqP®Qü‹	¨ü.äg:HiÓIý„0’¥B¬ÂQø@¤%p:õSªqž“ˆg“xtäÅÚÕBJÈºM€ÕŽ4«<R¼ãŽßß|ÓmR]¸.ŠŒ,¼Ó­6–Î*ê>8~Ê)¿D½t&m¡bõÊ“t²Y¤mÃæÎLOqqª6œHƒö\ZC‡[ S6æ–|ö%Ð\\þõ_ÿ•ÍŽ$¬öÙx\"J²ÂÝ“e\r©Ó§ž|\nëšÉØîûï{P€aIPZ†I\\ax¸÷ºkoæ$†¡ËqÊU•¡°›²¶î%û {>&Âz\\2S4&°Nhòæ«yê!³pçÌq¼é(ªùNÞwJÐjkEÏJÙ–¦)ø+àN£{n?5&öJXãW¼†«§ø§PbÈIHá­Å­Û5%tè#~adì\\®ëQÈ|×7³4ï‡ 4òà„´#ØÍDyÒç,ÂMxaÅ¢¬wú–QBP¢Ö•YáŽ³ö;CÊRµÍèœbŠLÊYÚKÎƒÒj\ZeÇ$¦—^ò[” £&y™``S¿…,f\rñë¶ö5‡nËh“DN»\Z“.®šç\Z%p\\ÊƒÂ\"ˆ%Æ)Û^Ìv‚¤\"¢búÜ˜`6PéÓÊÌfBe.˜÷£Ñ	›~ž|â)3g0Ù´Ð´wÜv&^5FAf+DÅîCÞU­„yÑ¼k=ßÂ«›õÏ°>ÆUóÜÄ(–ö×0K²>x\\•<¢Ñ­¹¡´y£l=~rNì¼@	ëÜJW©vÊ¾†ÐÜvƒËZÍ]r½^6rF!‚¨)‘ó±HÊþÐœÙÉú¬l%\'c}kcÐšÓ ‡ûh$ç˜VÂÂ(îÃíí¹¥WlÆ…œijUºù¦›o½åŽäÙ•`G\nLi/ÍöÏƒtæoÎbc™=^× û³ÂE&Ü¶TIEÎäÊVÃ\"Í(5R¹Ê	þó¡…\'Lì ’\Z^4ø{0Á0š4a¥ì\\xÁ%–\'d%Èjç:ãWg Pc,¢M2›-cÛÎSR“¶”¶D0#ãÞ{î·}ËÆ¹C¼²žãh7KbÞÚØB|_ãVª«éìzßäŒü,j¯NLÐ4^ÕüØöÝJè¶u›’óŒêu¤Kf\"„#¶Îœ±¥CÕ^pmŠÀfimmÌšŽR7U§†}4Y§¦os²T\"8%ÚÖVF€­L„d”Y˜oPPj8UÒ	Š¦ÈVÂ\Z”PF¡d^„=‘¹Ã;BÈ·©IÁQ`’ãÈ>à¨€Beµ\\±UÕ¸Á™b*_]1ÖÓÕbÜV’ÏØÊA¤ÉÐ³ªÏ§ÏE	éÐ#€·™\0<×¬„§žzZDËèG†Bùm	hR5K`©!IÂ*mëª+¯Íc.ˆ×Óôg+\"LËp.NRztç³e°Ý»óŽ»Q´€³¤#Ñé_ŸqÖ†L=ðˆFŠÖŽ%Ì;‹æ)j˜k ùÖ\\²mc2ËÔ\ru[œ†ó÷ï|\'-O†¨u‡[ ÕbWµH­¶›¢3•šHbáyHÙa†Ü€…^ „•ôýõN†0]ÐŽ‹špPd‘×L{mâv%è ¾)Lè\rH=+!}‚å{tœü¼øÅ/Æý\rxìn!Ôp-2†sr½ríóUG‹òhä‹¬„æóPoL„ù—‘ Áó£¢~ô£Ã$’Z”¥ŒÇ2VJ”o\0VÈPð)CòÒK.—nr$i`ò€KôskgÙû7®¥c8òŽw¼ƒO¯¶hÊ²Xdqe>×(!2õbØ‚NçZz2fûi”\rq¢ÒZAâÚkn»MŽ$ãˆãh9%Ä\0	áhºþº.¿ìJ¡é7¼þ-‚\n\"\n™xE˜}ïéöÙ—XÂ<|h”T™‰;!SÛ<RèŒI.– 3Ó\rëö«R‚KØ™\Z”MÎeÓÑO³jã[éŽL½mÍÁ›2åVÂ¦(4ë8Ðä•¯|%XáûÆú1ACú¦#N¾©p´æcH·ÏJˆé%d»C1˜ÈøàeÊøC¨ô¹èaÑ¶Ï°þ§œ¸•”0tvy\"4/}éK¤vÞ¹˜ÑÙ<¹ )©E•Àl\Zæ)°ŒÆ;çw€a+h·Ú\"ÇQ.¸Š­§¨^]•›WVæ¨n;gÔí‚::P)adÎîÆÌÀ‡úOÝ\0XAº[˜RuŒé¬þµ¶\\ãÄsŽ9úxË±‰ä4 a(ô«^YFÖa>÷œŽ8ü\'{}ŒaîÍÍ†4\r”Û32¼*%ìØ\Z}ÿ¿ºwK™XôI½uþ¼ù\njÒAcŒÙgVã.@Ér\neXKi\0\r<Ä¸Á²Š\n œa£.¹¡Æ¢¼è@‚É7*zÙºäÔ7Éá0\0%ïÄgêß¶°Y˜?ºÙ\Z‹NÞBJXÄUK,ÑùT×?À¶üÚI† ¯ÑK^ò24°ÐdpH‡\'x$¤¥²\'x„4¢L$Aã Þ%# ” ‘ú„ÔºEvgL0œE™û«RÂ’ó×1,þ{¢ó¾täEVB/–òDòù±´ºÂ²—_~%”‡û­6æÓ,ÑÃ0\ZŠVâJíqÇ ÅˆÝÆS‡zŸ6Š›s\n©UÍÁ‰ÂŒÐï¸Œ<È\'ÒÍ5·TfTJ¨»ÎlÃ/•›ë¬ð3(6V€)E˜Þ	$Õ¿µ«â¡âý·œòyçžÐ‡>bž;n¿«%w–P‚(Ç2@	–­0¿…éöfî¿5Û˜²|}[…à[~Ÿ5Æ%4T­.]–B™E™_dŽJˆà÷L§õ âNLu{\"‹*üËk×.6\'ÈK!©¥¥ïhl!¸VdÁ\"ÅÇæŸ˜‰p0¶$=no7Y9	u‰GkÑ³%Ô?çqmËQ~¥¦`Ù7•¿¹i©ðº®ÌÌQ­Æ8@ÿ-´™äDêíøÖ\r54aÀ@j$ª­ôb‹N~F)aßúãæCIFMÆ¦JÍ•ÍiÆ{vê)§U.Ç4V\0wb	%žb‚’SS7·¾µVö}KY?b.4kˆ¾¦\'êD:¦q‚Œ6i™bøúfMd´ÅÄ‹ÉrJ˜Ÿlbìoqêž”°Q;³yµr&@\"ÅO@²²(§\\l¢©\nò<LºÌöF™É52fÍz8F˜ÕB¬‡%±«ªIüb^\\{Í\rÌ—°vùeW!N§ÚÌŽ9·â°o¢³•~}|â\Z”`¨Ú4üoÇU*‰MäPUÛìH‹¦RÙ\nÒ\'ÄôpØá×¦7°ñ>;Ó¿T†‰}çwÜAw°q=»ÄµîÆV`áh)+çkƒ“ÁŽ#t+Þ¤²Sx°÷†æ[’Rý¼¦½f$±èkè\\3Æê©§žÊì.dêºgGŒKÈ‹›îÃ=ù4ä­¢^#]5‹¡Ó¶d;)!G¿\ZSj}JT†ãHÇùÁ~Äÿ\\TÙG„™¹À÷PÖiÞ$”éà_·üð?–­¤ï¤«5BZl>¦‡‰<ë/Æ+pÍñ èbú›òÁs†±¿[ŽSGå‚´5S¯zÕ«DóYIpJSF ‘\nÊŒ:æèc¿ûïÿm¢(áe£ÏÌYtë-·/¡M%i•%ñä1ˆ-›Q„‰`Nú`|à\r1A†Þ5ª`7ªX‹¦YNÙ^Ð\\oW±ê“ªˆ\0è)<<¤bÇÿ÷ÿýç!5¡£ñ\n0ÝD|ÿþïÿ®QœC²Á×‚©&æ·qœ­à|\n#†øþó?ÿ3·5ùv\\ÆºƒR‡=Ô³ÜÓq‰tÌ™úù%l	Šîå&A68“éÈ%­MA9‡-)ücØ€Ëè4mÄì‹$t:`ZüÙAÊ™ûÐŽ‰Š_ÝÙ…Eª·d;)A]r‹De¨ðÆ~Šl.5ÃfMŽP:;ov®$ûÈ@ Ó\'‹8óo|3§“æ`\"ð»ò²êî\"*A«i#Š—ÞDÍRçéÄ{ÚÏ=+a>å¦@üøäÚÎ}”ÛÑÌ*ÆÖ#4æ-u’%Ë­	\\äªòo`ÐÖW0Ôà§?9öox«Agf1Z2ÑÇÓO¡ÛÍ™Ê8ÐÈÀ‚D2PyŸš–®\ZZ2eù>êòÔiìËr\rUoC(&¹ãF<PCH\ZÉq—&ÈÿcPŒ¶¢òp¯xÅ+^ö²—ñ< jD\"5aèÆ4¸„÷æˆ6è>9‹r;_ŠÓjè]qæ(aKPtÙM\nÎÕÅì3ì@¿Dñ9ß©½úVð¤‚9,N{€ûŽ`’±œŽæ^ˆL„Äi#«x+Js SòëM‘ar”üé§aò4Š&b€N-\Z[ž|Âp¨f%jf„Æ©ILúÂçarS­ô8=K%S˜Ð…Š%®£Ÿ=U]WÚ7gÑ|3Ž£A	i¸ñAî6¾Êà)ÛV-3Ì¢Å\\\ne\ZWIàVœ´Ñûï³þš°„‘ÆšK„‰\0â#‰E^ˆÛn½MÊ©ÅP‚ö°úÄç>û¥›oºÕ}´SVB¯ç&8%l\Z_]BB,þ‘+¢†Ò¤é8Þ¤CA“eòÚV\"Ûìù@7HG\n£B²m0(û°p%:a708@OþŠbùšË8*SÂ%~j)¤gþó¼vD zM+.ç›­ulºaàÜƒJ²*ñ½tó§òûI\rw¦†Ódµ uð\"¶ädy™ÏJÈ«‘«Í+\Z+œ~ú„™5pÛ­“ãAæ‹þ8‹Ü“³ˆ®‰Ì³é´“O:ýô§Ç£{ñ¬R­Êã¨è}66Mmk‹ì’}ìFË0%ìÉÓh€Ù´*¶f×±©\ZJ(¸é\'UMê\'Þ†ÉËvüÏä¡\Zkfv\nš>ßQ©¨ÜAKÀ…AwñE—5ZÄ%|vŸæ¹+.g\re+ì™ttà(þ«R‚ønˆvý}x‡Ë\"åªO‰*Tçù¡Á}^;ã°sÁÎh<¹yœã÷×:à†Ó3ã/¦±…;m…æz®¼gžPÎóšÔÖaV8×«&ß¬€Ðˆ%$5»œfÀÑ×¤§š²VƒG\rit!âGÿÉL\rý<‰%SIU\'jU`Iãß16½?”HÙÍßÐä}ÏÜÛ¯¾ê:YŽ4ÔÃ~|êN cét@OGC\0\r^ÃÜ¼Ü$~E9\"7y“F0yê×‹·gž–çnÚÃ³†|Þq¤f‹ÕÄ~6p£¦Ò4c¿RLTÆÉ—_~ÕñÇtÂñ\'ŸqÆ™†­ñ~ú¯O:ñç—\\|™E½¤šiŠÍm€Ðª¤†•OÉHÃ°+ÁpT«P+i\\ÂL	+“Ó®KëÃqðèÉåN8¢ž‰#—qœùš¶ŸsÎy_üÂ—¾óíïžpÂI‡~ïûùÈÇ>ûÙÏqÆo~ñóS~üãÃ¿üåC¸P¯¸‚7™¬OÙLº§&2èž~¦Šæ|ÈP˜vö%°E<k³Öž§æîúPL¾´ðc´ã¦ŸÙ<”Üæ½»ë4\rªñz+}þ$×L\n–®Ä‹M½ÕÐ¡Ì\0}ÿ¢ØÄy\r8k¯ºa=QJ‰“°q¶ñD|³é¶è—;ˆÞlµêR·kÌ¶»0ã(GkHÙU«KLå£ýØ%ÿÖrîôxC_Íš#áÅ,;ž.¥…?ÃT÷ÝûÀ%_zä?ùÒ—¾rÒ‰?ûö·¿#JÊóÁ8ÀÓô]‘	&šïš	ñè5T±–&tšys¹Uúlˆ\rYí³°Z¦\'ŸMq{%…laÆÑ|``7lZäºYrIù‘Í¸bG°*[œH¥¤ž$¦4ÆO…f Í’‘¨±Ž¨JAKž¸ÜÃ¶(—IÉƒxfÄ\0IJfÉ6Þ’PíJXT+#õV¯»»ðÅþ;Z.\Z7ä «Þˆ#•„[™(#T3¹ó³YÍ”©+m•šSÖ\"VðÞÿâ¾âWUº1Þ{Û#ZWT¦cŽ£DZ0ØdÛO”°Ð7µçp™]¸\\óú“pyŽ7¶­–¼PØZCä÷.ÿþát(~¡Í6»$j3¨*Øàª43?é_Êµ’ab‚e^{Ý\rÍ·”žÍ•Ys·VDX‘\'d7>ƒðGŸ£ÞêV´Xð¸ØÐ\\âj^n@³a.º(“„*~c«EÜÍ­hiâsh˜iý«e9î9cžDJ&÷Ú|çÇä­‘ÏäRUrJ`K¾5Èþ@Î@\"\rê12¸>»­–K^§îä še=ÊjûÈ‡?a&ÛÝÒŸöqä×Ÿ%$åNÒùÄ²4ÁÝwM9¼¢ì\\Ã8Ì(Ž„ñ}XW²~ÅêÃ¥—\\ÑÒ€S*Åƒ‰qR“iÍQ«å5z6‡Å­S`—PBÈ•Å–bqä‰Iu×´µÐeøž·~•—ûFœæ<Ez\\éó\rŠ-y®Êî|æ­„g™Tf–“m\n†=òJ…Q¸dð§H>U•-ÕZ„B÷ÈÒ‡ýj¿!â”`µ-ƒ†A 9ø]Ó†Õ­o&æhüÇÌøs§nîü˜Q«†Â¤U5ö›3‡Dª\\)$Öh&ôÑHbdÈh	ž%æA§çNvÖ®q³üzáølyœmßûîÌn$ñ<·†C€K\'=‘nÈ\' N «p–•;­U—Õi°>CÁ²EMåè§rì8f9¾2š¦qÔÒß<Éù¯µà”ð,RBx†T£¢êbtO^ @âaäÒËj/aOÞ?Àä*Ê©yê-i>}«D/k¢—”/ÿ| „hrP`™»Üýb3Ò¯å[—Œ\'6€\0äæ©[=(A$5YEùF±t\\ÕHµ-¶l°¶Ð4èAlYåçÑåPâËµ·ç…•r¡f#XßjJU\Z§—•\0VÔŽ9©	.ÌrB9õv¤ÄeWejØa£-!ŽÜ«qðd\ZÂ’8‹x<d òå¯óŠð¬‘æÿgc%DÆ¡•zf+ÐS7Eœ³bNA Æmp¸Y²Td>ÃS$­ª›­q=Ø1K&ÂÐÆ”Ð}Ü0ôVÂB~?Z	š&b°±	$’ñu€]O‚\0ËÈ€u•CÉr\\$ôg>6Y^YjT1\'»\\Ñrì¯XÂÊjòªjõìüe£—yætÍbhX8,¢Î3ô CyZÅÜ«·ôÝœ´í@n5l†êÅH·¥›¡?sIí…HÜ™³nš+úùà8RAÃk–Ñªv0­ZÎ}¦°ž19`XÉ…0X¡X‚‘S”pVŒÔ˜Lõè\'nqÚ®Ø\\o|Ã[þýß^nF£7½ñmï~×û[æý˜\rSŒaÑg‘0ýÙPÂnj÷¨\Z&ÖHt¶\"à4Š[:’ý+<PüF`6³ã;§ÌkvÃwN³½Sj0:×ŸxOm‘ÿzóí…XÂŠ~€EhµÄqT2X£RlMdÄãŠ¹›9F31Ùâ4YÀõÔEÔqüËõQd¨4çËZ6ÈV€šbÛtUjß?{+aøÖ”´°#ÍÙe_eB-hŽ(Fº€é¤T¦zSÃ\r	TùN.¨Ó$Ñøv7•˜ÙÎÑƒèdÅ–Ý\"L3ËŸ|à+}ž{±Õ‘c!âô9Ý( M²¯úð$ÜŸÏ¹ï>s8«>a.ˆO¡ªÀ}•îrúËÈ[Å\"šOh!Ãjþéÿçüû+Þÿ¾ƒ,ÚÌP;}êÉ?pw°žÏ”Žd“?u%šÚ‚PÙOø`Wôe\Z«aÈ%A¨¹œ]õ&£·©áK°»ë÷ph2–œž Ú	wHñ%<‹Ž£F<¥{åžýŸ#H»ø–y½ˆÁ™ºË€ÿ¶	-Êa\rÅ‰}\Z˜´Ë¿þë¿æ¹ÍÑÄg[ªþ¦Û\"ºZtþÒãÏ¦•I”½U¡”=Vè²T‡N(`#ôòÿßÿ·EÓU½ÍÐ9|1´‚þ¢¶Åøëà>O÷¸“\".Í+åLs4¬Ä»ÅJ÷L=Z?¼<¤^òN‹òS—\\dd…Ù¸Ò8|°b¹C±Ë‹mÅÎmÒ.¶Ãs°ËLN«fÉñðNŒ¦Â·\'#X)ª”\\¯aF#”`‰,±áýØ”žsVÂ¦¹Sº×¶Ð_•3ºœ>	Îæ|ÿ‘™ï¦Á€ì\\?KÓ*‹LUHÞe\"`Ë¼.ÆgiZ ô‘É 3@í…]|ƒ•²QÛrÂfÏþŸî0äry~óøu%9ÞÇ“ÇÍôŒ£=R]àjÖùöûU=÷–&äìþ§ú§0—ìx“UØ4IülZÒ’ÒÖ¦[±0L[ì8—gtfçô2ý»ô/ºhsJX/(¸—:ŽHxbœ(V°_†nÄiS\'¸wˆûoÿöo©G¹=\\;r–$Â@\'Ê“©¶`18‹àãf/×=˜oœÑÜ´Ö‰	åÇ·´<]~O+a”zIwÛ´×¼h·ü¼ùê[u¨ÔJ˜f6mI°0€:2.#oC•ŽÔ”À\0{JP]†0@§Á´¢1…1Sx“r$A24ßÝM¦=ra—Ðøõ¹E	ó#ík¾füVj1üúvˆ\\_m§Ù­©!ürì\\âØ™œóƒ3˜¦^ffe\">H:§uèfõÄ\nšL8§€7EÖ›Ç•m\Z5‹MCÉßxÕÆ¬,W;öåÿ”Óvd°HÏýS¯KPs4_z(V›æÍÈ\ZˆzÓ«œ£Ú%eP?y¥54ÕJŸâƒ\rzçb¢Þ©(!Q7©}eOÒj35hfŒKt2¬„ájÏïÑËŒm@§%l±°bÔØþ¡„5š[µKQ¥¹òg€¬òˆÊšQÃ¸™ÿC¬³ŽÓSœY]9SE°îÔSOk8­N=m˜ôËûËªVEçoN	»én:°`OVXïÙ\"Ùb…ˆåçòÁ‘¶´¦“\Zd=5ÝŠ\0K33Ûð*¸gyÉ=eÉæ†Sƒu´ÑÌ!eM¤þySÂýsË½ÖI4âlV×)¿°.šH­‚~õˆ¬šÄ\n £âˆL«[oŒ×«ÞL2(–päG/¢Œ“abÀiø1ýäŒšÃˆÒø¸§<¨Ùä[Ê|É–aöþ)X¿×°” ÀF§¹Çôa«ïæ»µô¨áœ×z‡&\0ß`6QíAïFE…>i¾n¨Ë+aSJ ü2eµÓºtLŠZ½¯7É=ÍxÉrlòë&ƒfÙÏ.%,_/a%bP@A‰$«.àÞ¬ªEÄ.o‡Á|Ç‘\rÊaYN×&§R™ØBp‚Ù}È!_å¿m^ÎÙˆZÓqul,¿ozÕBJ˜·¦— Ê«’F–‘!UÀkÑüÌ1dÄà±J˜%†! Ë\rÔFÖ‹ç°g³ËT(%ˆmKâ#äTÚ?oJ¨^·ŸB{g*ùF}‚~œŠ2åÀÑøÔjó’––%¶q+òá&dÑÄ!GcÚ¡%4cÕ=÷Üåä¼v7Œ.m‘oT¶˜^!—‘ŸšSÕ›”¸bpzÆ¼ñ;ßR{ŠÖ¼£iñ}ÓXJØ¾}šï+ƒ¯öÊ>.slxç†¦Iý”_¤O¨	t\nV—7„ÊMâ±ß¸„M­„a8Y ÎùÚÔÌWIÇÄúq@¶`FLð—	bëAØk%l!%¨¬ š9€)¬ NTWó\rZ†c|àh£AWâ4`Pu¥+Sà`ÚÛßna†_ék&Æ¨NÛÙŠñËÝG«Bôæ”°)lù³“¡&8¢kˆ!ãÌÜšªÃOvüË ›>sÀ©Ag²\'¤C8Ÿ*êˆ°9vôÊ>v‹Nþž”•:ÏŒƒ)¿¶):}drÔ\0hõVz.¿$ÑlüÁ]è<ó5çj˜isÓ=\rãC0Æ¬P|Gó”0¼UStáÁIÌJJ\" Íä›-¢Kø†5Ž8a‚¤ððätjaBÏ\Zß	ñóœd…¤™}öóš–”Ã!b œÒ“èLf©âô×_Ò´rþ¤\"d$\0£ÕpÿÒrJî½ƒ\'z\n…—ÉŽQŠBÓðr=\r®J½+si‘}°Ña©âûÅqd\"ŠkÏ-6Ý÷-.Td’/ú\"Í´eÑTõvé†t\\|€S)g_ãÃÙî)¿Ònþk#-øšÿzíG?òÉ«®¼® h°ÖBžû‰†\Z˜³x|–xîV¥£ª89V\\fð}èªh@5Ž\0úbî„O¬†‰ÀÂ\"‹,\\d\0b\ZqƒèA\\Oè¡8Ï†8>GÍãÔZN³y˜‡Ë¤íZ„äq1ó¹ÚÔ«‹ª’ÿ7[>5“üéÉ7lû£Á‘•Ð¸æž’r`4.R¥	Ö¦Ý¹V°IX¤q\"ñ«ÊÐpSŒá^ËŒÈÒxÒãÊ\\êYKÜ£ÏsJ‘\nÆ¤}ëe`;¨þé¤4M4 ¶‹Ì©|ÍZ®¤K¢íZ*˜¾~ÿ.¡¿æýó ®ÊLÛ£É±;¥rpRuioƒxÆ¨«œHÐ­Á%¬°s÷%è[E	\nÒÈ]OÍ´`N­\0ÜØèÚX>4¬ƒ ÁQÑz…ÎW{™VLy‰52é-\ZoªÙá­å¯Šº{=s+a\\6·œÇ‚M‰a¯Ûý„Yã+0&€Y\ZˆQkªRuˆ€5æ[\02¿¶Ú\Ze„dÓeÔ#ùs‡–SÅ6Ä›}\rŒ¦Þ²k	ßy’ÛkyQYÌq	ÆX˜ã°Je\"ÀYêƒ9Ãuú™Ÿøø\'Ô	Z…€x¸}«ù¼m¶<Ñá8ÇÑ|,Á¾X‚Q~ÂËó”–%P:ÓOóÏr7$ÖÃb€\"ÏÆ¾þµo1„­uê&­dG¾½ö)¾S\'èl&4Y	\ZúÜ‡,´u¸Ì“Cm¢31¯U»ú×\"ièQH­3 µ¬6¢½Ò®6µ<±µ¸¿G¶æ	iBKôˆ`S>¨qt[AÚ.XLE³\r‘XÄ\nÏ:%ŽÜmgU+AAÔ’â—2©ªjµ‡à>W‡ƒµK|éþå\Z¡ŸÙoy†zÖùç]h†ÿ7¿éí– ð=st.£„Ýø`c‘ƒÕ‡w-‚×’ªƒî/ðeH=2PkTÀ¹†*ÙP\rWæñ°ñÇ1¬X*º%É=Ü/CéGzÈHžDÿyKxèAÓKèú“ûE‹Þpý\rBÁï{ï‡¬qÈ!_³ÒÕ¬„/BœþUcyf¤Õ#FˆB*¯‘5ˆ%«1J0T­»=mÍ®{nwò8ƒ\ráÎË_öÊøû6~ðÐïýÈx‘&ÆXîœ|¾SÂ,a;©oÍJìY¦¢3õ¢×PÛÓâ#ƒ¬ð²¼RÏ\'Öøá–QËäH\n,e‹‘±ÈqDŸ\0[`Îåiýé¼Œo—s—ë¡ôbïcï_þå_úÖ+Yö:x/Ó%(%<8™>›n«RBÅ”EÊ8¦òRg«í¯Æ—:fíâqè6¶®VaZ1ùüoS?½ÿ!õ¼ãíïÑ©yqu–º^iâ[øÙKxy%Íz‘ê]êK©îM’Sö!•ö©\'Ÿ:êÈ£#£0¶.3ï¿ï!3nšzóÓž³â”_œ~Ò‰¿ðqÎi¿ü5†3R`Kª«Èg™µ¸Xg—ÃMükHÚz5µ…Ð³$‹¦áè¨ÃåRKzR	Ãñ4¾\rE~¶ˆG½Ë¯áo¦½dVêßÿýßãÑUïnƒ…¼ºí$”þÍB×ýó@‹­§ÄoÅñÀ=÷>xï}÷ß}Ï#¦×fÑÞo¾àî»ëîù·?òèÎÇßñèc÷þþ®³ÎøõÁÿÄ¿ÿÏñ±sæ¯Îpðñm¸Õ£óG\\îÚ\'·ïð±ïu½R>=*-”¬JG¿š¦X}9Ï­\r/ÇÄ›n¡p˜j<NËÌm|Sþ‡áäqh¦3Q3yá4´t”æÆ‰\'Úžn;‹¡§«\"7)©OÔÍ=»Eš¾-wB\ZC8ÞKÚ©žÓ*2\'6Ü’ˆœ–<$ÍŸúoÿöo1t“ä¶¨C·ên½üè£…¸ôhÐÑ2ñö³¨›ïÕ°ê	›>á–\\Ÿ­¢òãÙ´‘á\\ö«põ£fçù6AV^¿Ž…6û7âÌ”WÉW¾†‘%ÔbAþ\\‚™øc˜‘{\r^Æ%™5K\"v{æ”nLŽ½F¢Î¦Uœ{¡˜üÅiîfÑ¹ý¶;>ù‰OsÝv«ÑdS¤gÔê¥\'Ž¨é®)þ°kæµ\'pÃ±ÇœÈ§!P9[gíaäñÎw¼—>ûäOMKôÜ»æb8û‡f¹9ÓÈÒàŒp¸{ý9!k\'!«CºÄ9ú-\"„Ã9:í›òÑÄlÀd·Í{@Pê·…ûw˜«…úÇsŸ7“[€[¯sCaÙ•=2àæÑ‡·áƒ‡î»‚÷™üž{·=ð`1„}}ƒ{Ü¿þêk~tè÷ßüú7¼â?^úþw¿çø£Ù¦OÝwÿC÷?8Í`‹\\¾kñÎQEû¼üèrÍã?UïlØÝJŸ­¥„šoÏ­Í;_EÉþmßŽlRÂÔs§±€©–f–ü¥†Ëm§Ñ‡ªARªeÆYÍgËVh‹GÓC/£t~½¬¡©´‘Pëy…øÝ\0¯ño\"ÔÉÃ:Œ\'\\û‡?üÁ¯Yâ«|JÖ¯ç_‚hòAšTƒ{Äû¸6ñs·Á1Ž§ÌDuZýpžJ X¢u­Šø{=skÎ&(ñW;*—Òñp(rÃoG7÷“®ÊÑÇ€¦R.!©¨ÒÉlj2I`êãô\0©J5â ¡š&NÝmY²å:”0àûO±j°±@MæBÆŽèâ·¿uèÞÿa.ï‡DõsŒ ¥×úÎª°?[™hÊ«éÃJàÊàÓÈÛ€8¦­ Éæ¸ãö»˜MY±†¡°(á¡YÿšW¸êxé_A^Ýrt³”G=\'ÈÐðö¹ÎüÖ9‘åž¹ëØ)iôõÌä&°è†=hhÜ~\r%$”Žˆá³seA¤9öžÉ_ç¸Jñí€ï»ï¸Ž‡þø\0¸#€a:@ydÐ¯~rá•—]~Ä~ü÷¼÷-oxã×ùÊçžw×¿G÷Ì¾Œt—ž˜ö¤r›©þSagVÂj|0Iæ‚&û’ã£Ãï¹S»Û´)Ü·•«jGÌiîvhÜÌ`­}9¦YöÅæãÌÃèŒïç{ðhzkÏ­SJRØ=\ZC°D8“„/¿ú)WFC«œFêl$º@çå	i“}o³Ã]}{yº‹›¸•ã´I™gÎ%$Øµé.iÐ‘eÜPè§Iâ&åoƒ<û”0›)oþýkVMöâ¿X¯ÔRµcšŠ§Ã\n,£„d¸¶ˆæ»jøOtûZJ1¹T è`*Å•î³«§äûQ?»ÞJÀøÇÑË»±ÂJwéäX}X|ÙÒ\r4á¾5/O<áç<Eø@ï\r‹wîxÒwž²Ì‹(ÁU]>åô8Jðä8Ê„tÐ4üKïzçûŽ8ü\'³E»&6Zã³(¡¹)Ó)’˜ô¸zB:Eí=$¦~•A‡”uÎü‰1º[ØVÞQK&µ:|÷™uªIšièà×lUÊZd3h<ZŸç¦à8\n‚­új¬C£G€>JÈbÈíÓÁ¾Œ9¢éßÙimã[Üþð²N8æØ¾÷}¸áàO~êç\'ž„p‰sŒ\"õÄ\n2Š9Š6\"êšÀñ@¥„±ÆœÖ,£4}0€ˆfBvŸt;¡c£mhpD´¶vLEè;ý\0Xó ‰´•±m“a³#Òk‡¶N»ä\Z²C~À–»\ržpm“9W½nX¸D¦†É]6C*å€°µxó1xCN!ù‚‚<–¶¦öôÂbÝ9I¨Æ¸Á\r½U—C‰Ð¢‡fÏE$~MS‰ÔSez“-¨±,I\r3Â’ÅÒ÷ªõ¯zÂ¦`2†2ÔjÃÇ80IŒ¨»wìë2Š£Ÿ²Zx\'‘îø`Átš¡m$N+\'P+«p!\nÑ#m ÔˆQN,2[pë(!(O£Ÿ¿ïªG•™¹à¶æ\Z2•¦4Ûï~ç3LwÂdçR¾£œ†˜ÀÎÈoÑŠÌGÌÂ&ˆêòîÜÛ2\Z¬ÉÌ}„ož|bºpÕÞåÔZY»ÜÔ;±D…ùC†ÒÖõ¦æO>†ö‘T%\r7#\ròe2<–!#.õ%7	²í´ïÚÔyòJk£¶ÇÎtÏTï’Sû7TÍœ§Œ°óë¨i‹y	šŸKÀ\0dƒoöøÎ`(Ü|ý\r¿»áÆ;o½Íqçøð&õ±?ÑÓºö®ÛïpyÇ¯ºìò_œtòG>ø¡×¾êÕ,†k¯¼*S#JèÅ†!58,rõz)VÎœqáÊ+<ï7+AêÛ*0ØÍjRð+ãÏ4sœ-ê€8­\\«yØM=wÂ¸J›‰ÚÜ†º,’#ÞÔ¶¸„U|eÉÎ\0Ö°›ëFÔ÷e/{™›»\"ƒl\"$ÇÏ9P¾¥m		IsÄ`7‡•×p¹\"‚pƒÍ9MuuH\'+„\nì&¥†;S’×0ÞŠqCŠEyfB÷Òy… ]È-fs‚ b¯“pŽ!•£>–gXb>·LÈmÐt[Ïn,ÁP†ºO6EÑJÝjÙºX}0\rÅ¢^u>Œ°4€z}½là@ú7…¬’€}ÿå_þ…™EÔé=7û`—#krµ­d%,âÈ•ôº\'+ìvØë‘h Õí{¤ù–¥ÌŸ~Úo¬nÆÕ3óY7yš­\ZÖ·ªµV¿õ–;/¿ìjYK¸¸dÇöé§Y€aÂk&‚à³ŸˆT{Ì!.ÿžwÀ‚š·Ýzç\rb‰$í+!¸O·¯9uBÊ&™ª>fk(ët.&z²Ždì¨Õš\'”çt­^§+Ú\0Šk	ŸÎ¦¯ºƒ Àª¤2¦®Ñi2U\\n¿áõîã_wöíW=ˆ4Ù‹µ4 Í=}ƒ>ŸÃø£|÷{‡ýà‡Gv¸ïC¿ýï~ó[_ùâ—>óÉO}ó«_ûá÷õqŽPÁ‰Çwô‘G±|þ£ÿäˆ#ûéÑ?=âH\'¸êÇßÿÁIÇòñ\'¸ð__òÏ/ÿ÷ÿøø‡?ò½o}û˜£~røa‡y1oå¡cÊæöÝB`ð1\'X:§^ºmó9[–D¶†–·çôo¾æh¹ã*_¹ÐŽã ˜s@Ø@xÖ™U»Ÿ”—bp]H\' ŒƒZ0ÍG!Á×Xeþ%Öd˜À>¨Ð¹ñ±.‡ HñÂ\0^îì)®rÐ³‚r*¼7q\'8Í~–;¸\'”÷POiîúà›6Ý@ƒr}“\"2iÇ=ÝŸ,¹ƒÇ9	]¯¤PXQá¥S9¶F¡ÚI8t¸Ø!UOJjâ‹óìRm>ÈÖÊ!>{HÝbeÜ–Lf³fÅ:ÂòSgœqÆ0ß³„h	C5Dü¹†d¿Ð}Qe7â!í˜©×£ó1ŒÀÆô.[H	ƒFçÙ+ú/:Aƒ•bTˆèz&‚Õ.l3òOó·<4„¯¸ü*)Vr‡¾ùï\n;Sù­ïß·ÞrkÙ¸ÙÍÍ)¿(;Æñ^Õj9ßûîÿú¯þNzR	ï«~ö%4{(9H±µCo*WÓ÷\Z—§sê`6HÁ,²ä¸ÞEÇ\'Rz\Z”ô\r¬ã¾£WéußÝô@[Ïw&òÐKõ[½W‡lX“rG¸Ü¯î ‹Ú¡æ8èA6†;NãŽ ÚøÉMº•oÛ/N>™“\'(ÿÉáG|ésŸÿü§?åÏøåiŽà\0ôð¡÷½ÿo}Û\'?ú1<qÈ¾è;ßøÊWÙNöëë^ý_ÿø·÷?ÿñŸ>wð§]{êÏ~þ«SyÎoÎD0‚Ïó—õáƒR!\0Bš#T@ì¼à‘	êi“1Î(}VÃËé}{nzos±€NÕ¨9  ‘ö5\n74&ÐÜ;MøG¨í\ZÅiöùp”Ý™Í¼ÏJ\0\rÈ†à¸÷ÝÈÑ \'h>¦€_yù1¨oÊ¦ó!/a`Â2­¬¹]È…Ú Mõ	ïÙ<ìš/?[¹¡× Wµ‹·¥þ3\\²GÉÊÖlb¥–dàSòÙP¤œ-\r°Ç=º;ÍýóI²™ÐçäqºSŽÔ4{^èy´yÖ)!÷]F*¼V!ø\0‰*ì¬çQ[K¡-žã(£\\uiÂ€[T§„`\r§ƒˆH\"ƒ@aÊÿñ?þ‡Î®–œ•U1ó ­^^h%,êEKXaQ«¸$Ÿt.ŒçßöÖwx$_x`f:ì¼úªk¡ÿG>üqË#³åªË&2\'_ÐÅ]f˜Æ¾ÿã‘cÀõDeðá8Ê;Uø&4W\\~Í{ßóÁ·¼ùfbpD¨¹”¤YÈa2€öJo«âÈ\Z5fª‰”ÇYÒr4‘ŸÜ8\rÍÓoe/@|ÿ’	gÂŽ\\·d8\"XY¯Cú›>,`JQ¾bù S¥ÔŸs:åe\Zöi’êèÐÔ+ÃoSŸŸeµL›8×GÇ\nŸ/˜‡ƒ~½úò+~sú¯Ð¸‡ò,ßXÈ@EL\rú°$|Ÿ~Ê©;wìäMâYúíE0á¤ š\'*NqËÊåeŠ¾wê.ÏÛÂü‚…MfNg~ËáXË!¦E\Zd\0CsGT([éCâMf‡QàEÁÛi¸¤ƒ­ß¡Á *x•f.D¥D…=ÁÚCžŽ!ÈèM`Þú¥zîAaS›Óˆ\r#l(!&z=ÔE ¯À,ãþUöîiŒ¼X¤Ô£G8]=ãmÝVIÂ‰àœ|ËŽÚmüÊª!µÏßåæ<“Ã¦‰•Ôìa‰RöA›Âê’:­®œ]mŠeum~9Ž}©Y¾3ˆõkÊ_(_w\"á>ÜSÜƒê¿‘\rÙÇ	•\Z[»¤»]¸‘„ºï°¸ÄcLïRáe<n=w£¨ öpôÏ\"\r\Zžg,”7„u <Ÿ1ÇvQèƒ>ô1?ep ssúKÈÇå)C\\8¦<E’‘~÷Þsÿˆ‹ ‡|Y#\\±ïÅÜê3ƒ…Ý7í:cø)òL€\ZD+d¡ë®:§&÷5J÷Ã\nz‘ƒô*žƒ:]’0¡BÖ€ð¡ªˆÉgEtÜYÏ$¾ÂËÄÎÁ²wìC®-ãˆŒ.ò„¸ dÓ[n¼‰Gˆe ~Pþh±ågžà‰G“b„!S@Tù{ïÓÊ;äImß±í¡‡/¿äR¤ë®½ÎåOlß!\n}ëM7áóŸ‡_ùô’aSß›W¥\"Ì’›~·æ³ûÝvKŽÇÍEkn½ òqëiJ-âœbd\0‚ÅçPRÜ8l#†ÁÔÖX°XëÓ¸iîˆœa¯ùèõš-*¿ËýÊ‹è4ö€‰~à\'râq~î#mÐÀž;aŸ›¤4$T U×k3pÙ4¤ˆmA¦ñ8™U¤ÈÞÍåS†ØlýÚ±8R)õ6½„É7ø JZàVß^ï³© ñÏ(~ì¨òÈO1#€ÑeŠ-ûWÕ\0ÛN»„ÝêMÿÅÖÚ‘çJSå€éæ©ÿy‡º\nçåûŸÿóªóá2­º&á¡)sK>[L	sQ„)ªÌ>ð™YO¥¹Ïù\'o¸þæ|ÿ0î ë®½iÄy~&Âð4#×æÍF7Ž£\\RéþºúÎO;\"ûèoÿæq	Vàž\nœæqÍ1»Õ¿jDz!%$=¥\0™=TÏ3ä„¥­gŠ’‚\"èGçÒùÉVš29p•Lp0A$—º\"Ÿ\0¶pC·²ùÉƒ{„ÌwâÈÂ sE¨b#2—ÆÚÒA<NÇ^D	÷ÎbÅO<¾ý¢óÎ!¸ôÂ‹@?4§ã³D›5ûÿþðÿùˆó&}ëk_çJ:í§\\wÕÕ<¼Mº.1rMÀ@8Á0á‡W¾ìåÎy~zçnå7ÞpƒnÃWqP]`áUÕÉsŽÒ‹³ÚÑ¬š‰‚)8¸5¢s\ZzÒÉ^KAXÖÔ†¹jƒb®õýË‡Ð @LëÇôzõÔ¬î“ZZÖ™‡\0âáB\0í DC$\ZÚsÝ )Ç³“™=7¿ÚÒG¹†éx^J&‹VÓ^´œÌÓA[+tÜ}ì8M”WáÅÚw¢œmœlmðA½%¨·/7Ù”ørÒ´ ¹n‚Ñqv¨ºŠâQ;~¢Ý£½(!¥^¤ìgœÕ4Ž·úi§1è3˜†¹àÎ¡Vå0øŠc»ÿœ]r@RB­º¹n<dÿØG?uþyï\n-L>œ(AŒá‚ó/6øò—¾&°Ü…ÔyÞƒë®½žqî9Žfk\\V(–oªô$\"3CÝs÷ýŸüÄg8©®½æÆÙÓ§wLPØùÀ¤„Úud—ëœz>—Q)kÄ¥¡DŠ èiúè/ö`‡\0ñBÂGh|(A\'·ã8y\rAà…Î–bHÂœã§Fúƒ’u”€xHíb+É<¾‹-ßú»[žzâIÖ@®üíeì†¯}õkê`¢#|Ø/þ‹›®»þ’.dR|ý«_ûøG>ú‰|T<ù½ï|×^óÚ\"ÒþÀÅœŒ`˜<ô°•É=õŠ[Œ†›ô÷JÏ9J¬\\5“ÌBm°*¬Æ—Ð%tJ(à$d0ø‚@v±Yª¢ã$Äi	ûW?aS40Å~^l#“ÑãáIÉ%EXG,}@„ÜÐ ¥Œ•M· ÏOõF‹Ø‘ùÊGÔ’ÂþüÑËø×kkSj²›äD‡È5\"?·-­Sžú0²†ûwÕ`áÖžoÝ‚ÊÂüïµ¡vl¾OœÙÖ9á8J`™AóÐ_µ¨êbÚeòÄîÚt¾µœÝ¤ä¥R’ôY\'È4Ñ#\Z\r——=ñ0CbÅHªŠ¿æw¶ÒJÈ4›æ{§ì ÏîËËÔöÐù©\'Ÿ€Î`éˆ-€vûmw•zÄDN\\”a1|M¨‚IÑ¸„q°ò»³ô$\'¸9÷Ôë_÷fÁ	Îw?Ò³Ê\n›ãXlÄh<°|DulÒf‚ôé*CÕršüxÂ>µ‘~GÝ+Ì`³CÍ,7©Ü•7ˆ\0k\0	KÝ¡²ö‘Š qÑ‚ÛnþÝ5W\\É#ñ¥!á	;òJ}üÊY”5 ¶üÓ#úõi§sÝw÷=W\\ú[Çy“v>¾ÝŠQ|M7ßp#›ãÛ_ÿ†|V¦H£Hêx|h@d$Ý|›n¬ã¨ÖÀí_.® Ï‡Þ\\\r…åTÑÜDã–ÖÉÝŒ(†ˆA…83_A5Póa…J™£ŠæI4Åã0\n‚2œE<×\0äxlì˜›ä{Üt‹	&Ôžs“¥ M,‚2rizÏRé`Ÿ‰|r}Œ-¤SLê6õ¥|™®ýGwÃË«âšçÏ–6Bi,š¾ÚÓ|•n×›o$O×Lê“ãHSâBÿÆ”¬%Ð/\r¬HaV‚_!>nÖ:-ÅÜñA3jÏSX–\\(¡”Ö»éA³%IVú,2•¶’‚þèý¬3Ïõ5á]¾ÑjæïôÆÅ{[ùg\'ŸúóŸý’š/3Õ%wýþÉ>ºû®û¸ƒ†^%ð2	 ï28LŽ4M¥9HHÒjÆCÁìwßu/[¡$wã¨Ÿ=VX€b»&xÑØòLZ1T¿ÕÌä4f~´Öüú Ô‹ˆŒHòœ`_ß#CœÈÈ 4a¤;Ù©`ê¼RðÅÉD*WL2È‰•€f\\¸ÈJ0JÀ_ýÒ—¿øÙÏI1îÐÜ3Xæ/rP\0¹T¢Î9—•À‰Ä’€õ¾o¸ö:Ìqü1ÇâÁý÷N!ë§ŸzÚ°7¼ðÜóüc‡‘$8	¹h²iXOÏ9JèÍƒl\rŠ•§¥ÂO:)ÔÈ•DÌÔ^9UÂzÍ\"´9@î‡1Æ]Š}­9oÚO+—ÉêVv44ËÃ;H@	Ú†<ìÉ\n	L[ôà_wf¶æGòˆÆåÙÏìp[œÇ¡4»juâ¸È¡$ÉßÀ¸™g|¨}ói{ÍùÓOX\n¬ª.:Š±fæï…ŒF‰5/ëÈ(Á9U…KÜ¤´@ö¢\Z¨\'ª±òË™õµÝÈGè.yâMeá\r+!žâª_TÌ-¦„b	Jý»ßõ~\n{‰§b	c›¨¯áÇæsf\rœuæ¹ø\0šË9%vÕ•W\\½sÇ”)4s@m,óÃ.– å4ÖÉ)Am|¿óŽ»¾rÈ7>øÈb2G¿§Õ¼Æ Ög€96G±¡ÇÑÈ˜à(h\rE?•ŒÏDHÃJ+$jðâ;8	ÂlKË 7EíüCúž®70T‰&ÇßQj`}²;×«=š´9Ÿª²ˆ€¸1ÆrŠ¤‰\\ñÛËn¹éfº¿ÐË€‰€°Â×^g ›3YMq1…Mzÿ~e7œÎ¹ÛgAiƒœ†¤,Ìñ©}üÜ3Ïr¼hé¹v*à\">˜Šp †—kåA½\\yŒ<1UXä¼´\"–Æ¢\0Î“t*¶v$Ò¿‰AÍW\rbÈÝåaÄðEãr;pì¸ÊÝ˜Œ@œ®J]¨’º-jýnUq1t2õ…—5WKå\0ñ&)¹Äs%{µ¼†Š/f$â#¿;Ï­—0¢†õÊUoó—P‚•¨t\r¸¬˜º˜æP«xºèË¨–j©\"xm×ò×UoÍä¸hªPo*\'ƒ€ýÁ¤`|¨´ø2*MoðMkd(xt®…AücÚ»•Ê»)Ð­C	‹žZƒ©Ð«¯ºþ3ŸþÂ!_þz˜¬Í÷þàSæ8Ñ£pž>ú«_ýú˜£½öZR¾ÃlÏgžy¶‘I_|i11bœ‚ßå¨‚îoD[›É~™jY@ö¹¡¯ºêšÏ|Ú@›]tÑÅÜo*7ƒ°ÓC|Vòúß\r´ùÚX$LdãÑ¥“Ä¼óïþÔ\'?m¸†ZbQñLÒ›øxFW!d%S4±(\0ÌGü©×‰ƒéüD×ˆ‹‰nBéfí)}uo’§²(tUÚÊBJ\0o»öê«ýÞ÷N<þ„«¯ºêÜsÎ9ÑÇwþ¹ç™éÆˆgC3wÊ&b)tÂ½í‘¦µ°Ã2àP’b„H¶Ïc:þð¶«¯¸òŸù,Ž1€ÙµÆ›‡Ñ¬-b($™mÓÂmúQgËÛeK~µ&vï\\¢‘f•¶H¬tìì!¹˜9ÙÃÓ”Ä`EÛQ4M”¿[\rŒ¦Ççñ:Pö­JÁ™úô,Jº»±\ZÅi<…WŠF\"µÁñœK¶÷Cx	!Ô‚Ø¸ËqÛðl$¥°Ïð,yô<Ì9Èþ1•s×ô»ñÁVS‚ú\'ª›|`„ü±)mÒœl³ÜSƒp<í´ÓeÍ¢UÆw„7€~XfÃxªK²ôV”P‘ÕÃàH•¯ºtçR~³4ûC5KRÔâ§L.ég„GîÉ˜«fmÌ„ú,PÂr\r:ýý¢k†;i£#aTu¤)ôM\\¸Unä	¡7±—}ç‘Ü ÄÙ(‡âÕ¼FrŠ8Žr,.ò‚!\0÷§hç!yKüýgšiµô¡ù§ÏÛ¶K”‹yËwþq1¨b\ZÐ÷_¯~½y;ÔX¥~ü±Ç©T²Œèt™–T¼äS.(I#SW\n¬Ì§9õ-yíR¥ªõ|[“n/!V!îF¡Õ+Á9žå\ZbÃê÷&­çå&ù£Ý¼šÏÛ£çþ¦½3j%p‰4ï©1\rvøÌ‚Çk4e¸š$cö2(s#ÇQ¸“Ò´ÒV?Üt[cÀóªÒ2;\"ëÀ]‰xNôjšrþôÈp þ„Š9\Z®E¨ê¹ðï¢²/*c›s	kYÀ­™È’ŠÕvªT×cAjÄ–SÂ¦¿z„Î«€ÀâÁa^»eŸÃ5B¤‰®ì‰‡kËµ-?uû\'ds\'ä ÔØºnâ%a·l`z:ÔÎFœ·w~eaÓK¨;t·:ˆ›°Ý@#ÔêÅÚ…™.²2ÌŽÌM¯Þp§(çpbðL}ÿÑ-«±•­„%]\"ž—ßpóLüâç§ÅÐ0#:u% EReé\0Ÿ\nªÛlD>w¥áV‚Ó¬„ž²é\'J°©Sç¤°{.‘g•âÑ£¯_ýª×©±\'v>Ý=Æ\r L˜@F¦`ËqÐS¸toÐ¬\'õ®Q^Õ8?àÈOþÅ%j;A*ºª	_I¨KÆ%8Y}–#òI:IêxhI‡™½E,&²yxðýô“OÝxÝõ¦¬0ŒY˜ág\'œxÇ-·\n0 £Õd\"	QÈ@54Á;)T³»ðÁÊZð¹H	ÛŸÔ=uâýyóÀ\nøKSNû³\Z4M\0ªs\"³@aÜMmlº-Áqç»VK‰TQoóCzùi94štäy™4úU77)\"¹\ZzénÌ…Þ<Ø²Cb)\Z±bzJ\rÊŸ©ÃRæRMfÖÛjŠÚZç/¤„DÁº¢5q…²P<_[ï¦ø·\r\n”À\'Ælr\'W½XùÐ$…Xë$!jOªN2Ÿæ*(‘>6ê¹ÝÊU(%ä½8ù¤SÌFg!„<<J^ÁÔóJÒšJi*§…V“±Ëq\"©ÏÒ“öJ	¸Ù=¹öH$Ädâé\0µÓL	b$ã¯üÏ×°®ž~ê8>ñÄ“à€]IV\"KÚ¢,LoB±V££€”ibÖÇRÍ‘öí  >“3w”à\"ôZd(d\0kP…œj¬ž;\0k—®·±˜Ñ;vÞyÇGqä‡Þÿ£Leñ¶7½ÙXQ„§vìd(\\vñ%bË’P§@!Ÿ	:ã€§%P®£½eM¹ Ÿu+À_Å‘T]\Z£—m\"N@©Yõ‚\ZÅÑ-E`O4¸· Ó\Z”lèäg8g44áá2ò¸rÛÀwÂ°*Ô.¹>lÄL¡tCÈ•9›þë‰œfD(ÄOÎTvsyøôî5æ&ÙrJH°u=eÑ¿–Pý±PðíÿÖ}QºMå\rßâã\n^TY‘›î¬áþ¥%“ô,—4¢Ô, &µÔ^4%Xº@âJ0MÅ å/¤©~Á:R~šo#¹uø2Fuêÿ³h!GŠÌ\"áåã;t.·jÅRqT+OC¯œ›oÏª•ãÏd†^[TOî;ÿÐŒrˆØÑ:­~EnÊ<)ÍT¯VR	ÝŠÙ@ž|e46-F=O®Š]³-:˜Ü_^‹áÖÌxßòé-ª–‹¢5÷äJÕmmŸÌÁîœê4¹¿·=\"t,ø,ªì[hZ\Z’$%SY˜•Y –€L…Ä§$Ú<…ž©Ã^†ëŒ0Mó9J	,·4hÕ¢¥$ð†LVý(V·O÷´A5,i<êm¸À{w	ŽçT£Ï¬;htt;&Ç–âBÒhëñARÙÓú+¤Û’%ý½Å_ù”<‘ÌÄmI©ó˜Ô«°ê@	Ú«\\ Ì\\æõœYœ2‹‰}¦pe¯È›Z	z1|çš+[¬«ò…”ZM\0Ô¤Ò@îìõFNRšëž.ñ“î`°´a}-¡˜â1ÃÖ\"ÅML±-v•n$•ˆãÈ¨±b	SjìÃµâ[NFyôJ‚?õ –ŒVY•pÒ9g	E\\Oü*ß?ôÇÂËQÂ\"¯Ñ4”a¶c9ÂnHÊ±t+Oã\0¤„êÇJ¢fŠ}ÝkßdÅÐÖá	&8P  …ØÉñÃÒc¾œh€#R•RCªÀ©GÍ¶v¦ŠÜå±Uù”nÞOH„1<{-¶®s.‚˜Pƒ@ëíÎÔ7†\\jâôÄZÐñjþ÷·ßaÈ‚Ê7\\sí´BÎãÛ­ºzíÕ×|çß<û×¿)ÉqÞ$”€?œéÏ.¤\rÉ£G	yTêr›ò@µf0¥“Òòç”Œ³‡ã¨é§Tf}õìœ–ltm¥nló¢â/¡W¹T‚&4ŒZ\\ˆ’†‘•@É ¥-1\r÷j7Ôâµ¾÷\'iîŒ|ëãÒy\nYò<d%íª¥ìÛg×JP?\Z«‘ÆpœuÅ°ãëó’jÒÁ^²ðLe9‹¬” {ÒÃÒ	‚òZ¤	lÒƒÙR*JCØom<„¼‰wðí|}“¥Â¥;DEC<@)¡|\0” \Zl½3:/t3(aZ\Za6¿˜RÁ\ZÆ£²)mótªßÀ\n™ÿa2 fë. ž(Ó¢wÞ%lÄ[È¥J\'‚ÜG@°Á~ %Dì¦—8Ë®j^—bÎ¬=\n@ðtCž\"ÿ6oªzV-¹ä;bøó,¸DêrAö)„•–î¶áÎ.ýbcé§¹la%£—Çýé°¬]â›\\–3pÁÝÐ•7Ô£¾û­oÃzÃÀ½q	­Òl~†‚ñÏM–‡¾ÿïû“Ÿ¶<\'Jµ Ë&¬/¼+òÛ×öOx™QßP²@™ªN Uõ<+ƒl‘6­t4DÝDKù7\'=4¢Ýt[Bá™#lqY=T‡P[ï(Æ¯Í„ªo†Y{%€=OÝ6ˆRÝMO§™8¥°O_–f8ö,Ow2ç˜Ì\\ê\rÊyvc	ª7¶ö\r|ÅäÍCÎ™‘HMúÖ|Õj`UxÛ¤ÎÎ…—b×$yýZ¬¢|ƒ7¤4kíâÌ‘SÀFñ&²Ëæ\Z4ìžk,<¾ÈªØJ+!J ËG	%LÚîãONá„<éØÅWæ–Q_ñAà•ÄÛ™²t˜æöŽÆ\'£áe.©â±}æÍ…Ž¨¦$¬Õ`DP\rÒ>Tß è©g˜8=K:ÌÌ{³ïŸ=3Ž–ä\Z”¤E\ràóÅZÎäÎiì·ƒ8‘} ºØR	Ÿ¾ÄG”/xkTLÈëWý™ØQI2½Óúõ½œ!‹š¨«´–Á¾NÒìÌ½RBîNð>Zjw·¤<÷¨gåûjb˜?;ñDáe©¥% À5tûïnA\0f³(ãÈn%«,HIâJÂc^ÆÍq_™ÞøÜ¥„)ø7›I¿‰?AdB^%üˆ%4©#tˆàk©€#ÞL?:N-¸)”;|¸•šl¾wà¬`—^Î¥ÓäW!ûVðk÷ì¶ãÑiîœñ\nÚ„( ×\ríØˆ(zpÎ}%9ô†W¾ò•ôâ`×PÖ%™î[å!™¥Ÿn²Õ¿\ZXîå9úôšÐy¼¶”U¬ýjcLµõ“–u¹šç\'O-RûŒ¨:T«%ÒóT]Ó`Ô¡j>2 à}„îëk=tVŸ[ßzJ0í„`©©LqƒXÂÆÐ„Y‘è>Z¶¢º•Ÿ86…z½=¿Gudká¸ì·W\Zâ`Æ$á•:æ@èÜ\0U”ª[5Ù(_ì,†BØé¶3O—™Úrñø¦½ýž\'¬*¬ŠãµMè$ÝÈlác-£É<z|;ƒ€~Qj`ƒÝõá2ô¸4¥PBÜ”¨t±ô”j`tZòÇ&Ð`%98Ê¤<º\\7h–î0Îì‰	¨ã¨^CvSãã^,h añ,y¥;n½m,¿Ü<©ÌÉ¦RNÍ`ÁzÈwd8´ÏgÀ$¥ö2t4êRIM@{±-ØöK²£¦ôòŠ ù${5–(\n‚@@£ŸT)è¤Å¯µN5‡A\Z±ƒÝj	ˆ‡SnÒò543W‘+8åeì@úÝ¹ÇmºUçé(	L\0Wÿím»	J“ËÀr…kø†òGH ßÑ`\'+¯çi2+×NB´ò¤¶+câl¨Êæ›CišÌ|*^LwÈˆöÂbç„TÅ7fgš›N¼šTLCƒa}XçÂFê›Î©Óçô÷¦+×FvôûÀÏú§Â+Ýp¶­œL¿Ÿ¬c•¯¿î&èf=œÙ„tÈ3®{7õ+ÉP˜²eÊÃ+\ri&ÖoýÃÓèe’ñÁÛßön”09 v^.»i¬Øœ=¡ŠT\\ÙvÜÙCUV`ˆ%èµeÄ3£„Õ&Y~þª”YpæoÎ[¶ˆP“¼6KÇ‘NÅS”÷@]µ\\Z3æ×©*E%òMïcF`AW•&7!5\Z&aî0$,}°ËP7lz©D‹(¡ãNÎ‘•3zºçz¥¢gn§\\º‡Qœ&Iõ±3­£	Cî»ßâÖÚlÞ$ó 	87ó¶—†›QBiÁ_cË¶ýB	FÏUN0.i*Þì:dí5Ï„æ ÈÛšÃc lø’šY%«aÕ®!Ê‘_‚ãu.çhYCI\Zùˆ@6s‡’ø”R§Âô%pY+d€vZ;Ã¬AŒXÙ¦D·¹ðˆ„’\'ŸgMlP.pp“YöÊ5ºØB}%Œ.ƒ¨„s%8acÁ~Ê{óZ§	ÕNS¬yýÑ¿¬|ñ0S…ë}@hN1k’\\ZëÀ+íþºÉá]T:ŸFÈPÀ1Ä#K+ŽÙ6Æ]™·fôòòq	”îsÏ9ŸD¢}Áa)•°›	F&Ø0ºdP’-<¸7¡÷­:¦¸ï!Ï2	Ò—¾øUÑ×_Ÿq¶Ù)øyZÿ J“ªG<ÆpðÕ~ž«+RˆŠÎÅý>»VB”ð«ÓÏ|ÙK_iÂ¾¦wÝ‰½cJÍ\"M\ZLÅ€¿cPqh>€¸}ÓÜéŒÜj2ý±údé¥\0æèª¤y’ªYo,¼ÜÄdÉk@Ü	ƒS]‹«¨œÍ¡æ×aÆúÉ#È.Pà=ŸÀhÆ‚ +´²‚X‚ðòG?t©R;ÞÒ›(Å “¹IG%ì÷¶[FeÛ²<eòÑÇDA°I,J`+ó“”ÐR?×¼üf‚×ÐÝ6	WR®äaOÐ®rd»<`MuMg\\´9ÁÉ-ÿ•D¹ÄýU,+¡X”‡jÐî¼„Ò‹“½:o¼•·¶ŸÜ_F\0I£¾hDm¢‹Ü¹åáJ)v!¸T@%Rü†ªmX<sZì¦„mðMŠ×bJh@, «Ö¤ªk5Ö0OÚ«:Z>ŠBdR~››’—ˆ38i”Y}ÍbÓú£#š X´sÔ[ž=ÏrÃ25Ô³ƒV£k¡‹a–ma@~+Ge`a.ÝH¥‡n&5\"ª†(¹jUË`+^Mˆƒžèw£ºÙn$c,ªf$k®]uåµ¢ÖÙÑ@ÁØ<HªÓª)—ñáÁP=AH{VÍðìR‚\ZC	¿<õ”ðã%ä$`$\rm§Ç1*õ+äšp¨ºP²\nâUŒ¦k¤Vä£ô‘E×êNeŸ‹çw?é¢\r,_D	Ã­ªÉò+ÿ¡ÎÐŽG7+Æ†ËÄ§÷ÀƒE’}ÿþ¶ÛÅ–ßóŽwZjMD¡©˜&´ð“€œðÎ4,Êl\0ú\\¥„G&Ï,ÀT<*³Ê)’\\#j5òIGv‰6\0°jýÖ§Ì5§Vsùnü9š!	ÀJè!ŽÐšù×µQ‚ûS‡½ÒifmºEcCP®<?#åF«}ÊGr[#ú—¨úE¡QÊ`·Ù^½‰µÁÁeÚÏT#<ÄÏªãHÙA/ŸÔXïL°}›‚ÔxP¾Š\n‘©-\n§ë›*Vý¨jMÀsV:‘°Ôò×2YÕ³KÊ ªêªç¢ñnÈIÅsëúZÍ1àÌx×Í©lÖž®:TÃ4ãˆ×èæ›n\næ5©ÍiúÔ“°v¯²)¶â±ÒMò¨¦¿”B“?ŽJ¥r“Yo|Ã[KÐ|Õ+_+dÍ‰dhtù©(\'ç{Þ¤‰{fI]¾ÝŠôc ìêþšGO€•i4‰u½ë@ >1Ž£æÿhi9%ºïÞûèV¬\r¯BÈ¤ 6ñQÂP.êÃŽÔu3~¹¦¢LFýÄ~\'F:jšÝ@ØP)uÃùº%”_b%x§d®òo\0¸±ð™Ÿº\n7YC!ÏÆŽÇoô²aÉŒ\0‘dksŠ˜BÕZ:’Ž¦iïzØ2;¢ÍÂÎ,«­ymzƒ¾äVÅœ2V¶ÒPØ/Ž£FP²xä\ZYƒÉiM\"(ŽvD	Xßåy/%I7 ðºÔ²ÈÀ%Z¶‘7†Rk‘E ^/ó ½Ïùnë†p%p¨ê)ˆÇS•\r=}%„J´Qì2bò`)»á¡Y“\nMqúá½ÞÓ‰\n\ZðÐ‚yš8¬üÇüG;~ÍþX2Õó~°þ¨Óª.=Â«ú—Á*á\'7¦.™þêä¼miB©G6n·Uí°Cûj/w(¯ÊÉÑäÂPn+ö n›ÆF^\n‰ÓTŽ3ÍÿŸ`‚«I1×­ª™­´òXçì³Î\0µ×P¬R$½½:&ÍDM‡M¿HWìôhbô2`\"ºüÚ×¼‘m†+.¿z×â\n“‰PŽS†ÂÌhØP™ÓÈP:Õ»Ô©ÄS#´²]XƒcK‚\nk´\n`%/›*¼¥¸Ú¦Å«zX(¸Dq`ë€²(ÕUvz4­„)\ZæÐ\'ILóÕ¨[Ç]H}†J}\\d„¶ð¥aÉ‹¬„ávq×C\ZYÒ­šeO\0@sÇ^Îjçßp†‚T oêleãÔ¸Zž\\{ð!+n¢ÑfœÑèe/£™GEDo+ìJ°¼‰7§ô¡d±Á«!‡ñ¥ú#˜”:b¶Ô\"TR™œ‡€›Ã§öMÍSt¬®aªä >ØsKÎz¡bDæâ·Ïe}è¬“¡XlhKîæma–õl\0Ð;1w^¡ÿíßþ­¾Ö:0µ\Z\rƒ@’[íˆHs¼2’4÷ù?þÿƒk—6“L>ë”€ç`Hƒ\n©•u%õ†Íf‘(Ö‚~\ZA/Ö…‹d=8¦Yµ`¹¿j^°úïÿþï™„¡µ6‡ÿ<»Áý)¯SRÆlUö4¼OF‰JSÕ­Ë¦Ÿêà%€ãÉ³ï{¶£Žx~oùÎ·Ü·ßö{K§QÇ}<5Ae)-> Zªˆ@9AOX³¡œ†ñê;ßù^Ž#YF ’ãÈ\0f.©ãŽ=ñ·—^1ï,*5zÀ¸xTõ¹¿f`ò´Ìüã7ÃÓƒúð-·Üjá¶»î²ç4~R%/ø¶êOÃ‘KÍÓÃ4íÛ¬¢æ?³#b	%à<ß3JØ®¼;vLJqToÍH¼èªŽûž$•å™žRí…³\'×Í‘GåüÙÄëÓq5ÒIÓŒcäõA>ýíÛ-Ö™VèÂ¡ïØåž¥£»ué¶‘ÎÆ”^áŽ¤ªy2í©61\rË€_È÷Ã³Å™Ï?ûÓ™çŽ¿È¼§;èÃâÌVî4Ÿ6z0³…3§inf9¬om§7¦ó[ï6{Õ5>+Çö=YyœiBMo¨ÕDÐÉG÷ÚÚ‰2 µŠ‰ÿ‚Z‡ûBs€ËáMVÕ\0T—”…[£»FÙ”2\nCÚº<x…	Ù`Ý[5ÍH×#a:¬,”\0*3sg$‘ sNþë¿þ+bóˆ¨E)ÊD ’\n²áqÝŸãI_üâC†ÖKð¶/[¤¢­Qÿ‹.Y’qäÝ”Ñ*ôq°®örì´D¶ZJ­Tce\nää°£€z†nv2ég*œr“2ŠþáþAy±&Áîæ~r‡ªÝàSÑy=Âk¨ºx¨žî5üX	X§ÉŽ)	u¾¤3\'Ê:çu¬ó—¤­¤ÔG•6ÜÅúSžmõaªWo¨)ÄlŠ:<1A£Df¨‡œ¡¹UÒ\nMßÀ_$Î,–`Äƒåt÷­Ÿ3ù‹hÙ€šû·!Z¢êD÷>dŒÛl}žGÌÚøðA7á¨3ÝcYî­Å™·ðÓ°‰æëöîŸ_+‹Oµ¥SëZv´(Êœ_z”Jp<Ñ7 \0„Œ^F,¨r*“{OÄ£uàÒT&ä¥íæC»òŠk¿ý­C?ôÁþô\'ÇÜqûy­MÄ†kp–÷¬¢Àh`áq9%tr=ÖËŒGÓ	¤¶ÓRm\nQ2eŠ­Q~½dó‚ùuLº7­“s÷=–á”`zç­·™Õ¨4®¤¬I:BÜJ¦ÀûÚ—±ëáÞ{¦By‰ü-¬˜¶RíxbÉãÍ0ó6¬œ<¶sï‹‘Å®´ÕçÕ:ç:ÐMÝ†&˜U’¢V¦ªƒTý<öuš_©œÜžôÈñ¼45DS	¥Ý/ß´¦KœìêÓ#\Z4Ö©ÉBºlŽE[ïÖwBØô|È›fM$Œ0à‰j¦/é„æSbR¸3ˆ„\nëqA-Kèoþæo¸h\n;O\råpëR×£ýNÐ›VRVÝaèõßQöI&æÓ`½ZŒÇi¬sŠrÛ5ßû_ÿõ_«±Lö°´n˜¨pl`‘fþPQèÁÉ‘qAí¨>pì>ÁË£.“\"¸ ˆ{ÆZ“{ª§{ò5(aâ€ô²¶ÄqQ:4u¦!µ^]µziMžwÀéô‚XÄ…qƒÓÔ¦’Ó•þßÿ÷ÿÕ!7–SŽ÷ZþÆ×¿ÃáÊo¾éV+µ÷ð»›oS°Ï:9OìœVéIIŸ™€¸`Cöb0E’ùº/¼à±înNÀ=âÒ[Â\n{f@¹-äV¹âŠ+©	ÜeèPv\Zd×\'õ\n…¯u\rÝT9Ž\0AœJ5 Iœ]@„nE È\r¤v²†‚3õgßêŠ®çˆõˆ¬tmQë/¸TÄEDç-o~Çk_ó†ƒ>ô‘Oü9‹žòM]sõµ&×3âŒ_Å¿wÖ™g»?˜°Ù)+Œö§÷âo¨áˆÍKÚœcßA;9¼³n­41ífÄeÔ¼ÞßAb­ÅO;å”ŸŸx’¥uLpôÃïÊ,øòç¿`_®ÒûßýFÃi§œê2;b†Ô#gë‹y:ò/Ë#t6Öñ½Ò9\n^¶Ÿ6]Gö%ÔAtmG¹£Ú¾LhD’C5(N-\0–¶¤Ã«d&\ZEa[…ÑÖ9JQ€…VÙvõÊ¼ùÚÈŽ›—3ø{ø@6¥„Ê¶róú×û\0MFCãª6‡­¦e)°i{T™kaµfU‰örÑœ¦@ŸáìÊM¹¤éW¥„jÕ‹‘.“fÇSÒ‡+¬ÖñªÍ\\¤G·>–Õqª¥\\=yq•Ñ‘ZM§à+A9ÎŸwF9NÚËAps-¥HB—§y\n¦+è˜“8™`¦\nc‚œÏDÃæ‡e¯L0NX™f>mÅß•X44q˜Q°i,…\n–‹¹ž•äß¢L…=ÁXkÈûŸŒäìƒ3åd\nÉe¢ò›YÏ ß™¡ð¨ã3‡Ì¤üª—óvRÛQ‚KÌ4ñG£2)vý§pÃ\'Q3øx\n>0+Ý¡yÿIŒJ\0ô6Òw.¥nö:¼è8ôçjdPS(`ŸŠÕüMbê& ²ÅÙ]Kj‰™sÉw¿cÅÞ3ül,*YLŒ*\\#Qýëø¡ßû‘ÚÃ¬qGõJ¢ÛÚÜÊ=µ9Ó\r<{ßÀÈ¯Ñ¿pÙœ?ÚV‰œéHsÙéB\nãq–ö-Æ)Íèäƒ²\0@¿,£ƒÞÿÏú3Vo6?ö‡Þ÷~áüqÂñÇ·œ÷¡\ré?¤5b\Z¸96ýMg4“Á´ú¨ÃýC	õO—})%8®ÿëÕš;Ç±\ZNï«—¡‚Õgã<€©KÊA\n@¹Œ\"	7)\rÁF%‡Ú\rÇ\n­ÝMºUÓÈwÎƒcàZEÐ©ÝÁÝÊÝ€Þ^ÎU·€ÎGBüZ‰I K}T^	š€§>]X°tUv_ëüÍ­«(AY¨\ZœþXœ\Z¤àÞMMôU»M)šô[4ˆˆªÆá5Qµ/+øðË)¦œT]žŽ¨ø™ÝvR¯Ó¶<Øš(Œ05Ü¢=µxßj^B”Þ7YWL@yÊ¸™ÆíÎ’õ÷	Ö§„iéÊ™|>#Rw¤Š¼¢µê®$9>ˆ¯sˆ{C—]˜¤VjÄAU†-tÎ»–¾IéÓiyx(¼¬…Ì;Æƒæ GÐý÷=(Åi`¤ô0Rÿ«;T»ƒñn¬V†è×S\n\rìví˜lc˜,|V&³»îºë±£ZbA³\niCm\n.ÊGë/GVß [å†øÃAç4w®j¡ #XÜ4Â9\"LSsám·ÞÉW†„¨‡¶îBŸ^C—P¿•ˆÜiÞk‘ËÙ–C†@ƒ\'PÞˆyèà ­yWêzÅ¸¤ãþõNK‡ršã6gv²k\Z€f‰M‰Fgˆ\"<>M*Ú,åô¸Ÿm8‚,£§žxUø÷þ{&ÏxO,w–^9¦Ýï)„ª\\—¼p3ÜYyàáþ¡„^¯XJ¡û:¿bjM}^]áŒ&ŠÞ£S°!š™<PH5zÑˆôª´×ÌîM7O	¿ìP99¯ô²üEŒûÄÖ™â_ÿ]´…wy½í°}Q~Ò˜Óƒ<ËªJL`|{®3‹œ3†ð\ry=ŽÀã<æfGÓÑÅI¨kAÿ\"ââµ•fìèåÑ\'ß½>zË²±?È¸)œÕžÞûº@3\'·bjQÒ|¼ŽS•þâ/þ‚±›?ÙÁª=Ð÷íVgg9©7\Zá8-*õÕgUýO“sìJ¼œj–±²Òge+ÁÝÇü<›:Þ†Ä\'©Ž«#¹5^\06£à¦(aÔo•KÁÞ‚Ÿ<¤Ó²ÉSy@@7Tm(þ$æ¶[ï0\ZŽCI>Ò•W\\EC+¸AÀ¸¡\\Uß®}ï{>ø©O~v6ÇÆ“ŒP]²…|PKD6=:·•!Dõ„¨¾Z¢2è€@©49\Z(@¿Ø:pº!d$ þu~=j¨-jŒÙè3&ÆH\ZÝOÞ§_güj¹›\rOÊ=áOûàÊh\0D]41MÜû·(H\rÝ¾…¡I…\r‰ŒU–q$ÀktðÇ?qÎ™gYiùÑÙœf4²–?Ò÷Þ÷‡§ž>þècXwÝ9%Yu]‚ÅÆßmÜp,&•:Û–¤©,‰U®Ô[&KtõXB”\0\"•‚EÈá–Š=´û2µ§Û7‘\ZÖSà‚î|WóESÊ;¾œœ=;ŸÞf#QŠ-3`Ap\Za+mD2“Õùm¼³[•ø\0Ð‹=¤&2Úþå_þ%ëÅŠô42I1Y	HQ\"YC‚mÎäŸQÌÖžäyëb	Ë:øÃ)AõæŸA]¼våh)Eº|$—Ø“=¥jQ|ÿÚ/ÒS¯aÈâÂe¹ÚÜÖÉ;2¤Yµ4©xí˜`ÔÅX`j¬Gôz\0ÛâÛaHµÊöbÄÌÖ.ýã´Ó\0§^ú­ª÷¬A	Å®À6}ô“*;ƒˆK3êÄê´Ää\n¬â²|G]`<\'£;i\"tš4\\cÓ¾öÕo	æý§æëü÷¡Û¶]{Íõ\\\"_9äëæ„¨ä]²Ôƒ… ]n²îS~qúl,ØRŽ¶œŠíÌ\"Þ“u’ûhj˜i‚†m l$Á/¯Ô\ZUïJ/V3\ZžqÝ`oÂÔÄ®W3q§.\r»‡ƒ.aR3‘hã\"EA&d–“oæÔÆô´³™”²ëã\'7¤‡ê·¬Ôy¬™n¾‹ÕæA!‰ºÉÀ¦° $2Ûä¢+.ýíg>ù)ƒÑn¸öº²Q§I°ï½Ï²9Ÿ;øÓÖS{ú‰\'ùŽ¬«cN¤îß\0;w&HúCàä±“J›!¿‹P+!®Õ;¸¿ \0;/~\r5TTs\r)KCOŽâs€›\ZH¢g…Õ³ºÊå¹õçõŒ=µû*-¢ÌÂ¸ÆÁy>\råƒ2WNûGH{’AGjîöÓEº\n2Ybë¿ä%/-o~ÆNóS€ÀNRŠÃ9™ÏÝO^†¯‰b4ÂË[h%¬G	Õ°×£†ŒY»ƒ¯zbÄ©b“CmQ=;\'p«;Cm¦+!XKñ\\ÁJ=–ËtšÙ–løWCyúP-®WBQ¿F<|Fê<É?9êhvÝ¿þ%4³òJzÏÊ”0aÜlSþÆkPdžeÐ÷*¢U·ø+®I\'€}ŽcHµ–¹4ÀÑq¿2Q+ð|ŸeÎ(Õþ»ßùphéC»VH¾öù—¶I@W¨ð]¥.\ZðÕhjóŒ²\'˜M°áž¼(O>1­_¶…Ž£á,šŸy)÷ÔÃ³NTˆë-8’Í!Èe_eªÕD\rIP;SŽÝ^I(S4;uë’rà²R”2P†Ä\n½Ï°œc!†¬„¶^O—n¨Ì¸^85<­?ú©c$Áµf\'=QH£:§Ž1ž&rðá|ðäãO`1ø4%*oÒõW_ÃtúôÎ\',Ÿ€3\\Ð·ÔCýD§b¿Ó˜êfõ´h`Ã>Øð6¬ì€^µ·¬g%xUõ\0šÒ\0ðU«ÚÚûS?á>ž\0SC×ú<¤V«u£D¥¦/wcO2bÖÉž˜ß¿5œK$U«dŒ}à\'ížª;õ¾Å[õŸlˆ‰´ƒÊB™0öj>«$™Y tyzgZ,Ì\0çÈ¬,+7å\"Cp=Jðæ)ïÌwÀÍ¬Ià£·*¶F}§\"’úEò©+AÂÂËÎÉ§ªÔîùw÷w<Íª³Ôñ«ß\0k¢jø©’Ý•Ù¬—õM›gé°¬O~ò`K–M‰ÙX×²lú¬ÄN^H	©·{~Tq¢4xE6 ïåše$á`AÂ¦äMš© Æ*FÉŠçÎ±\"_PˆÆq„X	°5Ø-˜<¿óG}|f\"øD(Áµ†@[û³`û |,î¼èdd\r´]tÉ’»Q§ÂÍª)ê‡L¼ìe/Ó[ò3æŠi¡×¢d‚vPo©ÚÉ\Z&jàVÉX?´™oµQ´=_ovdîÈ&œe‚”B^Ÿ‚>ÏñÙž0´Û9’¤ÖÊ+åúÔÇ>.Ç”Å€ZPS .#sb-|ò£““Ê[ñbÀTì‘ª54©zc½bð;\'1Xé³j‡Y4£ò@Š=¡¹6ElœøZ­öªö@*Y[ã	XéWÆ_Õè\'=ˆØÔ˜¡É¨óEˆ*­óóñê¡ôÑû8®‰Ùš§	@+êùÊ­lãª,ûîÐC+»Ïü~€ #ª†Â!CJCsOÞHOTÍïÆµà|E#Û’°¹Ô\n“L”ƒ’¶4;|ó»=¼Ðªi`1¾E¡ý;¬¢M+¿:ŸïI)Û‹è687ÄOTÜPìÚ\0i=nA¶x@ºª1šú‡4fuJøµÁ€î)œóæ7½µqrt!áª¾2%@êÑ½7ã€-Ì°Ê·UÛ+	Ý–’«ð¹PóˆZ‹}×±),ÌF!—ê+5`åš§„zû¢BF!yN¢,\"Øð¶·¾KjÞ¤(aPÈåb>?*_Ð¢K–W+©ƒÂ÷ _È‘•g@9Su9ø[y~Ù¤a·^ÊÇÄùŽâà(aÕwË|Ùå©›:péê{RÂnâ¾\'ð-;bî©;î4Ñ§?ñIn\"þ\"Ãš%%ð™›¿È¿â\ræC•qtÇíwµC1‰|$u•Í·ý2™aºéÓ—PBm*ÌÈ†ƒÓ¬>µe™*\nî©„ÜÍ‰­A¶¦•¡@áx`±é©wÛ8»#¡¶aƒ:©¹FCG	Ýa€à‚€9Ç½O¯Bê>Ù\"¶ iÏ­âPìþ×ÿõ…t.{¢wð\r©Z©Uð`‡ùØCÍÇ7•âÁÉïúÌRB¸¤N„ÅÓH‘šxÏ¯B³æ¬\rV°#¼,¢Ànk”†¾\\RÆ\0Fûœ.OfÐ9Æm:ƒªÔMWÕŸüÄ§d™[Œ«P¢i„FluUSxMJðN‰>Ð]5¶#©™Þ1òT6úŽ8§	O´wúNT\';ØœÅìÙâ«î3éƒ³AUƒ8Žàû^‹7òpóŸø×Ú;ï{ï‡ddfadUí:ÇqÚnºö^/ßíÑÔäÉw}µm,°†üõ·ÒÑê‡ÍžÈ ÖØ,°VaŒBnÈ|‘z”âzÂöU_¬5ºs €ÐÖR‚AË(‰ð¾w½û¢óÎgd\" £Øìq3ñËæ`æëÄf¯DÔƒ*²5wÂsŽBˆ,«Xÿo8XýÙ&®«‰Ã¿¶ Qj¶U)74Pë÷…ºs\ZX ìþÜDtó²ËÔ*‡³`P‚3….$S ¼‘Þ3Ùðçj0MwÞmsq[þçþg!Y·j\"wo’‰¦Xâ²#:;Žç­ªS°ŸBÞ]o>\rö|æ?›ã{åRp\n™pøÊ	Vý¯J	º’îÜL…¾¢\n­L]Q`!cODÛŽwÂ¤ÏjÆ9ú£fÂÓÈ\0,¸•ŸrÕŒÎ{ÌÑÇI²—‡IåˆS$Íx¯˜¹›†½2%L5ô»À{îQ™¦™´yóKR7üKªèŠ4*bÝ¬¸	3³i÷‹™„MJRøW^¿©ôP4ß«îG™máOÉø1ÊnËïì#†þ‰”Ài¤tÁ};**¯‘7X?Il\\®»N&˜–)k‰£3Ñm£‚]bº:%<0qB”`³ÃbÃÐeÅ«“Œ§/¦M22™Y€ÞøÚ×ñËÓÄ–Y|GÌ…‰{\\äÙˆfÓZœwÖÙì‰+¯ØÁ¼@µ£Ñ­Ã‹º	1¨VBÕ¨G˜ˆ0{zP‚Þ=‰}«è€¡° eb_ÿú×ãŒü±5Á¢Ê40ô÷Îw+˜ 	cÔA·r“&ÄnúÕžèd–\n/«´ë;½u4ý`š=)Aç%9†.Ã}H‡ZçÙ}òºo¢î¡®U\"§)`ŽuÂ,¢Öz	3\ZÛ2ÇQöýæà¹ÙVya¼ÅJ\0qÜà5ÁhµåÄPýÍN¡b3¶l£#“më0©uÀÑ¡„‡/€HÇ®õÃÖœ¹š¯úôÁŸ7[I€Òe}7;Ã3O	æ6ž©ùbDJBwÜž[£á¸ôåa’ŒùÐ;]X1Š¹¹¶LçÉfœEf•¹A	ÂË(A\"iãË–³BŸ{§L,ƒu¥ŸZÕGD>ÏR!èv–SÂžL°›õ°Œ%äK˜iehg¥ú©û©uÂƒ©bq¤Ó8ˆà~¹¶AÕá]ž%T:,³©ÿ¯î8\nç#ª$Ò³UC\"·Š¶?úØ-7Þ$¶üòÿ>¢f¶˜ÖV›-¢`ç†k®õ«É-„®¾ü\nQ‡Ë/»¼òz\r=“GEyÇ<?Ï-ÇQX@Èy¥éÈuõ°JÒ™‚BÀA‹oØt\rV…ìu×›r1‡›nýÚÂ‘ð+`‚ òRP‹wÍÝØ±D+¤sÜ¯LR\'7­¿\'6rÍ~ÝvºÂ6ÄšYCŒñ\nþ#½½Cß7ýITþ´R;ÙƒÈ<IQKæ]òÀý[6ùØ\Z”PU ŒŒÇŸ’>½Õ._Ír>´=(AÓ§Õ%ÒvòæÙQWÊnæ«1Éü¸¼uŽÈq-”PQ­EQªŽû×”ÝþþŽASßŠ,¯’ÍPØ|]ahÂÊVÂõ%Õiï–5.ÑÈA%ÄÈŸ?DQ5|£Øˆ¤sÞÆƒæ×Ö’LÕÍy%ŒÔZ”@ÇßJŽþˆÑ7J0W’EÌ«j\0W\'$%ÍD´„25Fa\\»W\"Ù“\'‚€Z±ºRÔ4ö²î<‰Õ²k{RRŠo#-’#2)X€k)/„UåÇ¯êm\rJ˜/gÄ@\"ý¹4ê­£„GO9ùg¯}Õ«ÿñoÿÎP5ôÀ\ZAf¶Â­7Ý,ÌÀ§Ät¸éºëY	—^rIåõÞ,o]IŸsŽ£ @„ýÃåQ.ò@- ¼£:L Œ4Ênm¡q¿ãÊÏ>?ŸÁ\"JHBf*öÆ‚ë†º*»²x“ª1ÕMCwU\'ç\r¦ŠB¨¸~í¡iú‹Ðsèõ&¼DÔ@ŒQÞã‚Ìƒ7‘fƒ›§Øëûže9IìHï)Øg«¼FõôM?SÄbÁÎ¢R¬¬—©¥aí•v£„RÆUÈpüJÐ\\åz·G(bD­_ã…5Óà€ŠÊ}§{65ˆ_sTc¦±1uô\'>þé[~wû“O<¥È­süÌ[	“ãhÒå	s^yú×¦É•Šã¬ßÁ–µ	è½wQ6‘MáéDÍ;ô&…,^º\'%´LÂòúÀâ\\Fþõ-äòÙÏ|‘¡À5|Jc9¶å”0˜£jNÖ Ý-!S®WI¹hR<ªöcÄÎi–üIÍß‚‰Hv˜ªC\'ìMFºÓá…œü¿Æq¯8YØlöÐé¶)AvorvîVQ‚X‚i*^÷êÿzÕË_a‚£ßœþ+~$è?Õ1Þ—>÷ysZ “c£„/¸ Š²é™0‘“ÂÑÛ>·¬„€˜v_\Z‰YgVáüÌh§zñÕ:9<\n®£qgÓ¦]5´«=‰¡ty<ÚSie:“‰ÐË€~š¦›—q4,Œ.ÙÍxAáÈ•18c!ÁwÖ/hºþÃâÊ5ôkÇÉ¶—™MÐ;m’A\\ºmsÄ~ØÂðòRJØÞ³´¼†‚.V&dp¼ÄNš¿×Ð€UcH/ÖÐöÝ3J-åˆñ¨UqˆG³Dw«PÂ·K\\ŽJUŽh5­æ@í„©Ÿ>4ÍsäG3Î9û|“ï–q´WOûž\'¼hxZæ“p¢èà¯oY:3“dJf\'gF£ðj…áÒJÁd¦›€<¯ž¡jp™$#ž	ºìöÇ½èã¿ùõÙ¦52oöÝwóçðöÀDÊ;ˆ^ÞXÙÆ\r6¾õ–;,B¹±„éõv¥!V4©\Zü|Ð‡>ÆË£äh«DûÇq„Ëë¢,!P?4#}Ft0:$+ŒD´ÚÜÂpA¿s¿ˆú“Aâx8ØÁ¦YÇJØÆÉ†851M\n4l¿ø¢ßþð‡ÞEŽo´Ë”Û:÷YãÈRI‡™|ó\r7\n˜øÚjrL¿ð™Ïš%L“`ÏˆAî©…tLx\'‰OÉœ]xaÚ¥òò¤$NªFw÷%d&æ­wM;ÖrØ5«A\nÝ<\noŠ˜Î×šÆˆ	$‚fý%°Fí@\n©ùØÍ^Ñà\nd²èø´Hn¥¡°oú”ê<íÊÂèøÍ Ðäû%2:-*¼\\,¡Æª8Ý$\Z‹Ùù5 LP{þmÖ,× Óû«Ôì@‰wê e98«Þ@-†h’Gt¢ŒÍŒôYë%œ¯ôyhóˆô\ZŽ#/–±Â‡&¡R{ÏHB\'ÓM6ýÌæ!ž:Qí0›¼O=Ÿ¯žÕ¶z( Tíª³¯Ô€Û\r‰’»<,ðt	hÚZmêW¢‚<FkNÍ7ç£çœ}1Xf9#®Ö`…Éq2Jd0tä†\r;a6ñÜ4¤f3 þeþ4Ññm0YlJ“2‹¢“](üØE^jY#ŒL^Ê²©$¡î#%D`õáa1•f6$IG¦ÖÃ „qæn;«ªÛËÏ7Ètè†vÔ˜êÒ7hRaºV)´7W’ƒõªeKr×¯|ÃÖ·þÆ¹ärìnÝ|\ró¥Kb};_t™ê2£êž*Â†-¿\n%TØŽ=îÍ¯ƒ(‚ñh²‰Ìsg±„¬E	Œla¦#Gþ5Í‘iï\ZÂcÓš}héûRætŽhJØ:¶üûSÝ/X® \Zàg‡}ZMºH	Žƒi^A:\0:”>Ÿ†è|-.BKô#:.ap§Ý/ÒÓSö]›^oÃC€±×Õ[^ÙN°5.wr£ŒwkÁä¡5fPBç{ÎÎkÔó °Å&«iá\re´Ä|~pÙ¼éÞG,¯ÌEÅ§&¯îkZ^ö\Zj¾)KùÜSAÌ¨¶È\rUÞÊøu¸Î;÷‚<±Ùg):6=½{¦#ªCÌ­G#\"Qµgä5A*Â®ùÁ,u\\¿(SìÜ9¥]wíM<äÿØÁ¦2kÒšuF/—Ç:/úC9ÊOÒ}mVµ	>z¨î™ÊÖpGA.É!å•ú·)ú¢ò¸Ã;ŸFž&\"½òŠk¾ðùC|®¸üªÛ§Û.ù “Öá½b	Ëí ‰×ð¯¢¹JnÖ»ßõ~0üõÓà¹MYak)aû.J¨»¦éƒ9ñF®Æt¨Æu;.æ4ç5RÏ¥óN*ÊÃÛ—› ˆˆ¼õzàÌŠ\\#ãhj\Z…M U—™UbX•†þØN’ª¼t™ä£ïxË[ÏùÍ™Œ\0Þ!K.³X Õ˜G¬„òS{ÑÅßøÚ×)°\0oß/ðgZYûÄŒÐÔLl¦¾ê5­;4èEhåL ˆÒôšØ]\'GZS\"	”@A†Œ\n˜ž[%3Õž†æ8·kâ¿%”ˆîN+P\\>ñþð·%`lŽ“.Z]”P)*šóU;ƒÆ…µ‡M0\Z:h‹Àø¼pÃkzÎé:Mè¯¼.·qFy¥LOT	äŸïÈË@C¼Bþ‡½¤¤{þ´å” à¸<(½,/®wà+SÂyhÜÝ(AU”Pƒ³	\njwõà¡9`œ@\rr\\®‘º%B\0–þÐ6m$ü,+L`äqaW«½ùMo7°Þ¦ä­úyQé«{RBÉûi÷a«þ)ó@QqQ¦%y%Ú›E£í»ŠÓð Í‚—ÀÍ@bçS·ªúÍïâSPXð`¯³àz=”ÀJ”°$ZÒì=^µP{Jm$” õh(ÅËË[Ë©–u¤Õ´z‚>“37£2AldŠ#ùšY·ž¦óè~jÛ>‚¥ùÇŒõÕ)!‹p¨ÃY	¤Jë¬A	CSªRL3¿ýBœE¦µ\0÷æÇf()7¡…|¤ÇyÔ’	‡ÿðGg6×…ø&¥Éµ*²F_Ö¯Â‘ýC	óŠÑ¨\nEåMÇOÑ+… ä¦(¦6´ X×â:ö¤lÞ¿n,ù‚e\ZjÓ\"mî‰ÿ@[º4s_Ð\nÃîE[˜^®Q«ÖGb/è“¨„û%Gr‰b›/yã¦ÃÄºv†…_s§¤çvŸx¢âéø5¢)| ø7Fäáè¿MÄÝieW$Y©[yfmk)¡lxEá÷V*sÃç3óÏ,ùìi%œsö³Á|µlE³_5V¥—–`‘3\nMßô«5Ÿè¯êD€Ô¹Øaž=ÎyÇg?Mm$œ\0\'ÿëÕ¯gè[Æ‘–OXéó¢¼F»È¦<FóÎwz=Iâå¤®*ë^”¸´ÅB£ŠO:¢0N¨‚^ú[\"í¼s/j¦Vlú)œ«H‚(ÁÄvî0ÜA›’I1fýìžNÃ(–[±k¡±\n2Æ6‚ojrn!1<FžmÃJÐßhˆ\Z^—ýs.3XCY#üKÅõLf5õAåÇ¾@DFÊHQ]oÁ€ji–{• î²%„•½ªv»9ÁÞöæ·X/Aâ‹•öö7¿Å‚š¹‰d	*ˆ4˜[\Z’ƒ˜ãÆ¢•Ï¦ŠPBKqì+¡²½¡S‰P‚Æjì}m\Zb.A1ç(B3V–Æ£­õm@yÊŸAjÓbKCr2³ ‰q\Z©Ç	ãßåOQ9½RÈ˜é°,ß‚-Ü‰3œÖ*÷zÛ0jG§©ÿ0Ý‘¼»\\ê”œ!o„ˆÖîqS\n¹ReÐN~‚Y´Ö™|	\rîé[½$½çÿñ³ã‰UYak)Á‹QÆß|MÅT6øoo‹M)¡!QÂàÝúø¼•ÙçuÈÀƒD\'–9)…·HL¤ 1ì½ë÷w	){ºžkŠO}êÔaìzS}‘Y€ŠEìæ?É8n¥1	ÄM7NÙÇôS\ZÓíó‡’ïFÀ\'ë¢Lú@ó¢T÷ß\'˜¸Í‹ZÚì¼s/ì‰}!o¶ˆ”W7[ô‰{6\"3áôÓ~óÁXVìóbÝ3oÒ|·¦)!¥€”§mù¸k]\"ž\'|-$¢éƒz…^(Øoœr?Ht`?¶´–N¨+¢äì÷5(!2¨ºª[” oAæîª”î3ÑÿŒùòQx1ÈŽ¬”`¬2¬oŠl~$Kª!ƒæ´ÀbÎ<KOlßáˆã&Ìjtž\r”»¢†¼ûÁJ¨*Æ§õ.åæ›~×zaÖËó’5¡˜JÓÉI˜MÐL÷è\\ËŽ™Á¸Y@Oíin\'#	¿ºgãW‰Šæ^â8J®Š\0Y9‹e7é’ñè\0 ¤…jdîI	ÃÚãµðÎ\"‚<\'C©†­àõ„+^ýêWÎ°ÞO€žÄ¢@Ç†æ*8äÂð¡kçó›¡FŽ25à`qðY¤„JªMU J¨KÆ‘b	‹\"¡Ù&±„ÅŽ£´\nz@õgë8Bò¯þò/ÿ’™¥ûŒÙnÔ•‘CuÌVËúÔ:Úñ´_žfh‚°ÂÜ±€ç:±@éâ³£\'„‚+­ä½â??Í z+ ±’l¯ÕLjÍ·Ömxš’4%º’`®¡Wþçk0ØÅ]bZ«†ìÖ÷æûá€o¡VÂYgž·WJpù“Æ®ýÒ¿úÿþŠ¯ò\r+w*B”yMð™¦ZW–òÀzµ§!)ƒú¿ú!\nêŠa˜÷ >“#žLÐ2tr×Á\0‘º§þÛ¿ý¸qò,ð¸òâÃ¹×ªöÚBv³×L.»*%èÞÅZ½XyþULÂð‘tõW\ZÃœ§È›|G?:ôû&o’,Uk/›[¨Ùiì&´‡/iLj€×…°å(¶(z!–ö»›oi’™Ð6»>¦_„bNÓdÚ:VÓ±ucmÊžVM¬¢\04èÑ J-Œë€ø:’ÂÔ¸ÜIfZeÁ%Eã{å½IÕ@KÍ½ã‰µÚ0¿F¹jG8Nwñz^#xŽ)[d£›3¼0+C&@W!lLæM¼ˆS”¬¦Í¸¹÷¸„“ÊŠÄ¸ŸU1Žï;1l­•!¥»é_<Zd/ž%Cn>è\"J8÷œiüW3Ý6£mØIù«X;Ô6?!€§+`\\¿zÊ‡|$ÚƒúŒãKÙ§6|ÒÏKŸ±à+ÿu¯}Së¤ë¯æ8Â-0<H*|E…¡8G‚ÔÏ|æ³¤ÍÆ\ZÀ\rz¤¡ökè<SèÎK+Œ¼F®#ÎX	ß?ôG–PŒ²E6ýIà[Ý\"‘JØK.)á,.Ypº\nú‹f[fGáe/}¥;XbÁ¯‘_A…õGóo»Î¥a-5¤®B‡ÒÀ:žÊd¶Õ˜ð2T‡à£ä. ÐzîÕ¹ÞH·¢]”kX	µï±øWþ\'G«ÎW¥„”¾ô¾ü`Þ\nLLÓ~þ·Üt³…t²	.>ÿ‚RQ¡ÿ“;v²ÌktÒqÇO«íØih÷DU\r’`É@E–Uãþ¡„ÝŒæ´‡É7ÜzF~±û\0¯”ô´KMS–È°WD×(Zd›Û<˜\0Ðœ*ú?÷‚_Eœßàž°ƒlÀV>œ\0=8îæuºa8ˆ·d·gÍIKP^zt«7J´ûÌ›­\"ÐZØÅ!ZD˜Vë/,°¬ËG5}láMø»øÇ¤)±‚Ÿrš…-Âã•”‘ÉKÝ„zQK6Xa	éÎŠ¹•G½\0ƒF,(ë˜šfæŸÒRRgsH,ùtŽXBZÄµó-¢€)Ó*­AjUšÌ4ƒT(E$hzäš~ÀªC\rÐé7´ó«ÓÝRÄDT**°5>W\\°I£WúlŒKØÚFt¡’ë˜ã¤ñŠ—¿:¿w60ª¦4j•¨ÙÔ£\Z¤yõœ§°ÏrðÐý]šq¨™®»ó\"J»k&P,a¯GFrg^ýpÅåW{œ¬,KªšÀê6ç-yôàŒE5ÓµC¿^bîLçlkøÙ\Z ZøP‚¹k	AZd*ÑÑäê¹)…Á«®Y\ZGF’àmBh¡ Õ:ãveŒ:‰8ëV¥„,›0n¦OMèCvõ¨o|õkæ/j`Ú\'¸öºC¾ðE#œÑÀµW]ýµ/bßB›ƒ)n¿í67iQFÊ¤Èm²(a‰˜M	Õ¶äz—Ïoæ[7få˜Øªr:éÞ»Ù‚î\Z¸h´²ŸÔuJ?O`’¤H\'Y$áòÀ—aœÑÈU1˜0h¨¥î9œ!k…‰?(ak4eù#ÔÚýÈIØÂ}‹Ä·rÛH\"ÂŽÖgÔÊ†ðþ‡á>|€	a™o—ó,yIQ\rD‡Ø…*PÙÇ;’.´,×¼´/½êh(ÑÆZÕ.ÀšãæZy%½ˆ•ÃÒdš›>ëì³ÏÕ\"ê¹†è{˜Y*°é=ªU}Çƒä“öQ\0û’_NäVâK`¾å =ë¬sÌïÙb0€ŽÅð±~Ê<@­L¼Òg!%ÌL§¶ ÃY(Æ\0óÃx-zM)tšYiæ™P„»Éyp>{P\ZPÇYÄÎ`×r\r ­Ÿ¼W×Íª” m²\0W•é>¬ŸóóŸý’gÍYýÃ4YnÌÃý–P‚\0d¢P7Hh^\Z›âÏD øKlh¦OßˆVº«)cÁ	Š¤@kË±º›‘®H”#SÌfõ9Žj‚ù aRiÜ‰«RÂPáC+Å$$tCê‡‡~ßz	¹‰P‚ýïç»\"ÌRzàAs]|ý¯p™ûÈ2;R,¾vÎÙg5ª78N6 “%zÀR‚F)¸\nF[€¥·åûÂýEõp],~¨ã@§iee4*\rÍãÇs1;èS”ñ¦ÃŠRÕ§H¨,&˜’ë?ÈÎa½¼zz«}-¡„dÕ…¥ŠÒÿr÷bé¶\r,—ã±;|ž)åJò)iD(¦\\(êAñs*jâ‚(¨ÎŽ7×ÐY„ÀÄýÃÐ¤½ïýC	ŠìÝ@+œjK¹’^~ù•ÐLÉÜâ‰qÃ\"J8ëÌsX	ºsïoæZ>ÃB~U±úxs—Q#\'NhU—·Ø[M•:\0\rÑûÖPqÄÉÇ}ìÙg_*ÐŒ´î§Fâãâ®ì8Zäú¨„%ððÀÜyÇ]’…d¼Šhçû&š–î£\0=ÑÑ`MÞÉwF€X‚\0kš!dºÛ¤ã»\'W5ûå©gxtsõ-ú¬J	[<|f\"<i´³–ÚDí•ójdòÌöÏî—<}QÍìé8šGÏM¯jÚ»löº™þ o0«™ÒZ¹jiÙÇÀó^¬ÿä4là¢_$P‚B2uc˜7Ò½VOBÍ8<rî9²Ä`Ö ¯4tX…-lhÁ¬ã9Ö\ZËÜAS€–ýÀƒ\'Î¯ýà{‡Þ~ÛíGv¸|$\rM`0Ó‘•×¾÷ÝïÂ5uãüì€ƒÑÖl¾í—™P—X	£ìYšIšüã¼gú[P›ÚJÃïòŸ¨«¦ˆ·–={Ñ™…Êë/êJÙ‘%nwaÈ³LB8g |–J©î…£4JÿúI?]D	ƒ`²{Š5‰)p,c*²‰«h~Ôoa$áa·§°b£øø€\0#6fQ¯h„ŸŸD¬E-‘:DJäXs¦×ÎÞÊ¶Ø|Û:+¡ºÊpÂÑSàg–(üSÚRëÔ6H\n3\Z–øÀóë³¼iãf•©–„ˆt¨§°*r(9E¥6Ä3˜Y-‡®JU¥v8\rµ»ÊB›7ßt[~ò{î¾G.U˜kgË(axE\n?Š\"Ðî­E39^vM–¢ˆT{ã1¯}±ñƒJ+rEA\0aÄ]2k©M^‘SÂ¤CÂâîÙ¸E›U)!Ò6å“‰Ÿðû€«\nÆ}òŸ1Rº„×±\næ „}ŒŒÓÖ „¦J¿«C¦à›\ZL7F<†Ô”„ÚlQd‘jÐŒW~\"\n(¾£¨%ÿÞµ\n‡&”|dåå}jåœiU*ò‘°»%ôb\nHcÈa\rtØ¼gžñkF\02\'\0ýl…sÏ<Ë€5î#™©Gþø0Ëæ°¬­&Ì°ãñí†,|é‹_ä¬ QÌdá50QôµõÐ@JH_«‰õ[ìeß¬¤ibµ–ÑÖ½¡¶šA@¶œ\"åªc+©ÎÊÓËDH»$\'M$çþ4r|ÛÉ=E“(Î[dˆ„ÎÃ’p$¶ N jS+¡“³]l‘X²7?‡;;¨h^¬ûD	Aj¾#ÏîaÝr¨Ê67—R@üæØ çNp¤…=Ž¡@S.ÿ22Ûÿ”@ónôZ”ÀéçÍ\r óßæ²7Q+¿tx’×h‰fyÆÓ$äÿØ”®Ñ—‹˜RUP\ZŽã|7ÌñòÜHv!EäÊë‘¥f£ºûî{ëË-žcÌ\Z=ØêÂé|[ã8JÎJÂŠ¦…øò—¾f•žK¥§(a!,ï4¤LÔÒ, ˜tIÒ0“ÂÇ¼kZ<\ZàšøðA™™?›FóV¥7×NÛßqÂñ\'žvõU×;\"a‚#Ñ³æ£Có‘€Ý^`	O¬G	iÐ¶¬?èÀÇÊiÈ0ÔäÞ©+\r”®Ï¨:ý zu6Ö7}Êwî›&à;æÉ¥]:a6%ÑjŸÄ¨°\Z(g—Eµö¸„(\0B§úQ.¿ô·ËMjTôøÊß^fù\"Á+(4ýÑU—]þäOú˜&ï·—^\nU%ƒÊ ô\nÊ¹:0)¡FÑ\"½ž®D:šÌ›Cj?éÿ8RCƒ?­¬q[{\n b}‡ª5|˜îÖLFÒÅ\nÚ…×ä$Ç/‡êbw[óK©±Bc‡z‡E”à×­<|÷Dš>[M‹¤äzð2~BVzzîÈ»÷ˆbÑT™²]‘Ÿ÷l^[½°&nº*°žgôF¾µŒêÝ?VB/Oße™1kZøAé$öÐ¾Ï:SªË¶;ž`è;O=ùô’XJÐ¬…—ãÚl¬Ì·ÍÿÓ€D€@9¨ÑóÎ\nÕ«Hð­¢p\0%²¥³­²cÇÆúš‚0PÆ Ì{95Æ0¯ÄN^^y¢|>ÆñÕ¾áõoðN½®þ™»3b ñ¥Oh{z®­yN(DªL\rrô `7±€ÔÒQØJ˜‘öS2Ðs%­Æ§üâtñwá\Z¤6”â‘¿çÓ·–(vCãÓÞj‰ú)ÈŠýØ‚ØÙšÄßd¥è‚}•édúÂé§Ÿž2%»œ1. 9MI«Ÿ®H	‘nzD|iÆYô¹¶•âèÆ\0‘Í«GÜ›®¿‰ÐŒFvX	–F°(ÂÇ?üIGOìØiÚ»–Þ4\"A¤¡P,ƒ¥„\ZtcæXJH%·ik}DÃ	j\ZæNFƒ²Ð¦©xbHZ­u•©ÉŒìb°šXiâP¸&ŽÊQæU C¸O˜5l2×rXiýÓ¯èª+T6‡#ìË%”‘Q7xP‚ÍªS{PoÈ›a>|oQGœ#Áneid†\nznªì£Gr…¹mÙLÎÄ*D-µsÎ¢XÖ¶(¡§x€£E¼½ªgŽ²ÛŽþéq ¼”Q2Ërœ\'Xä…ŽZ39zNBÜ°Flq£ B¢âéá§3q‰¼DÎ·þÍžPNk,z—V«îî¾ë>©ä<:òþaÞJJ˜a(ÈØ1s–=L©7T\ZˆÓNL¢5sª²XeO5ælx‚~÷ð#÷ÞÃO*C‰\"sï×¿þ7¾ñÍwÞñûi&€û2¡Å]¿¿wZqaÛã¾#ŽþùqRª¸(X+€ž´¨\Z#ñ„Í›EöÑã­·Üý;ö$#¥\r´]ñÚ¦ÇgÝ<ž÷’&´Ø£5…y;c	«±If³ ««k®¹ö+_ùêG>üÑsÎgÝ¥¾¾\'Ð+¯×}÷šÜbr×¬É©c4]‹¦6t¬4Š§Ç&UëA*—`U§’Y\0Sg6kˆµ1›é,WuúÉgšÌæqPî,†‚<KCÎ9ìÇGÞpýÍö§;lóôiŠG—ÏnèóÇlÅúg](¸±™TG¬Ì‹ñ‡ÜyÛí³–]üÈãüÞo»áÚk¥!™¸ç¶ßÝ²cûÇMkñà}÷O9©üì˜c¶…2=E·Ô1˜Õ©É¹eÂÙ c|»U{‹óÅj”*ÁÆˆ Ž‰Æ\"ï0ð1z°ª\0LŸF·*¯ÒqÂÔÄ±K¬à*ˆ\0Žý*ø”án=‘-PIó Ø’?%EU×x½8F“œ|ÜC­);a84‘¤¸°iš²T19.˜5´~†lü1ÌšÞ—3#h?­ú²‘\Z7“ê¸Êã†¯ÉAÏrg|àªæz© QÂ†MˆaXÂÂµ—‡£až.O5pÅUßøú·¾þµo]tá%–.e0}­ù3õ¯ÙtÂÛ”‚3V¹¦iSõ«_£œ­øÑÛ %Ò¬n«‰J&Br¡ú	%¨=ÊÑAVT“O6ÝUÓüØÇ£õØ£_zÉezøÙÉ§LP<ÅØ6ýlîPZh%äb†})°@@øMo|oÌ\0îÝzT8îüß¼Gÿôx†…àAw3ç]~º›ˆÞÉ±BN”Û »q+NŒ]”€\"¦]â†%¥êú÷²ß^ú‘ÍˆŽbK8þ¸“c\Zôæ)bÝa„Lö]¿^ÃqTo•)*ex0³î˜£OXê7ÛÈ:¯›éŠ„†[Sß#I9\"?‰ï1À“³ëÃÊNë»mtÅDsCËØÆ„V”Ç¦³­Çvíð9ô>Ž\'ßi…vÀV8	 É:“_\0øRWsqt7}€ã[qXE.ìµi‹¡|§å#êÑvÙ\r)¤À%ì ZF	Õz[âW½d	%Ô°ãÝ¼\'4—eˆí¹yí\\Yô>=Ÿé+`ÔCÅW!@¿4y~ƒ\0Â\nYS·Œ¶`b<q þÀÖb	\0½•šR?GE6›nÎáäD]|>\\¬Q°øÁ\n…ÊkbÕdÚ—‹ƒþËÝÔq7w“øcdŸÂ”pPswN.¬¤kÑ%{-m³¾y>ñ¨CO;Áy\n›{\'šä\rSu¬%Fë\\g©D~íõ°\Z}HÇDÒª%´ör:~5Pm\'Þj’±Õ8¾nU(>Jà5RÏåVŒmÔsU=ÿ¯}ÍÊÝT×è›l²g7ËO]F	Áå.ÿû6ÑZø¾„†Wº‰Há5mýµ¯y£qOµ…4ëÆ«ˆú¢ÙGÁzQß	Ùuþ“\"\\cÌã!ä‡èÂ½ÕŒ{|&:Ár°Lö„in»õÎ<T}·6[,[!Ë ñÑCq¡}\'ƒÎ\\ƒ<Îs«I$\\!9‘Ä¦X¡-ÃÊòÌ !ockó&äCKë‡ô¾’šÍÛÚõÃq$ùHËH\\Ø­äÕýí\0eý%P^Zƒ›‚pÓõz¦-Iý,f™\0:½/¡÷zÔPÂZ=qhyI0J lzmˆæÌÞ\'x`ñÅª§ðBÐ©qIþI¿æ±ÜúÕGå¬ÊË­„ÑÞGY„\n@!ë­¯{n\n˜s&Š…BñJWòq5–ÂXåã|\0;XôtQ%5<ŒŒÒÛœí%pëƒ­ ª­š_B	Þ\n6B9rÌVJ4\Zš^)â’™Ô©\';‡âïYCÿpZ/6Ý„^È†¢yV] º]Èé-œ\Zgq_H	õoË½Æõ­)9˜Ì{% ë¹òµÄ×,	¼HÚ)U*ÖAÿ–TxÙåƒ«äX_¿S9­ªV¤(Ô”ÜtAíÞbY‹(aô‚êŠVa€[‹ºo%%×\n°¦\\S´_ÿº7\\,²vÁ´ÁöÜ“O:åU¯|mæ_MˆŒZ¾ø…¯\'ˆW»‰½™CÈëJàbB	r–ŒÀ\"|d€Þ\\IœÁ™¿9“À8^—Sïn\Z·ÊÇ‡E¼¶P6>Ë.¿‚çû‡D¹ãPÈ@lC„Š‰·„ ù$ç3Mh¢}º˜Q<Í{7Eƒ‚©ó˜ØddoÖ3Á|þHÊ,0rdÓ=‰5—®îçd½‘NTÃiP˜\nÃÃ€\"€Ð?ï„oÐ<TûÁUq\0j!ÊÞÜqgŽ×ð/AoI\Zôðrôªuþ ½·õ¯ÒéoÂ³’n©áùaœà§©¼Û–M~µúozÉrÇQHçÝøŽ9y„ý[þhÓ­v\Z¸qØx}@s PÎ¢ƒ3Z­föuûfà‰BV¢$Ä__WÕmÉÕ¦[„í•4À›Ð£›Ì9MêR„“:g\"íæ9@’ÕM·–%\'ð4•‘qÛÍ²Â–RBB2[¸B;VQ£\\!/Ä×AÐ}œƒT÷¤´•r©¿´\0J(fãÍ]@;>¢A´.\Z½§Ä®z«=ûz\"Þ¥Z;Ì“÷|sgJ ŽPïR¶¶ÌJâsqwHWG	b	‹(¡.ÄC€kÄ²ÑÎ³qÕ;‚Å°Þ¯°Ûð1ß¤¢öŸ&µ÷wûÂÅbÑŒ\0\'Ð¬Ý“?Ê‡MpÈ—¿öÅ/‚œsûmwD!a}sZ`^\Zã¥%Ò9Ï\Zúþ~°ª¯ÇDþôƒï&¦Â¾YòèÇ›\01[»,=ô§§#Ú	¼ö\\É&cáOhøè”ÑÉöâ*håWÿ²v	L‘ÑÄ‡@[\Zˆ†Ð£\rýD!B^ø^º½ûôÃÐCCçDn,šÛ2.©WûWGâ|\0”ñÑµú}¦“Ý?JèWô“yÁKæ5\ZSý ”,ªŠ¡ðºÆZd3˜P‡@Ç¤oø¸Â:|ä3QªHˆÍøæ”sÄÉeÑüóª}vI÷´\r<ÊqÇ£“ê9\ZªÆ¦”ç r&Ý™ü *oÕ;wU‚‘ôúbÒ‚ÉCQè2m–Ô‰Âª@a“!9½áþ¤„Ì	Ðð\ZÄ{ô ´Ô—ŽtP7!Õ¶V8\0Ýˆô–O¯Þºªì	]É5=««µ\"Ò\0ª7·õl²Ôz|‹¬„Ñå“ý´1OMš²e”0\"½©·fSKh\\ë^u®ôq8n~!1ä01¬Ï±Žfpß‘eÆ|«ƒº<MŸ‘!ñKx™›H\0Ý8êGyŒUTî¸ý®î·âðtÚ½&iŸÅ	vù¦ðÙãÍŒ=›~ÕÊ‘Ó=‡ý19VÌÏYÛq4Æ@`Áƒ?õ9ù\0KvsCý7—”	âføBhEƒýØ*WEûNÐ!I¡)ôZßNªì¤zPWÉ%ùkö1–>`Å%@V¯Kã²hš¹ò²}ëœ\r!ë,	þ\nœÁiNÃÖ‘&ð 7t‰¼4¾áë=ˆfaø•ÔÖU\\Êó+Å0w\"¡ly†.ôoöÇ³N	A•æð>€+Ü¦ÎãƒMá5ŒVÅç÷“¯6òÿT¢+ö‡,‚‚Ïåç„ªA@jþxÊ>Rµ=#f¸)†ºè…{¢_Ñs‰pœW¦Õš¥Ðô&¾ƒ¼ÈFc1€`kKlUä\ZwÑÆ$ÊwÔHIòŒM®ÚR+a\0·º¿¼2_»„ì\r9–Ð€{MSÕLž1e”0®J(K˜`äJ+ÊÅýuðÊëYz@óc.¢„ê<‰è[zúÒq<«Ç%@.”À]óÖ·¼óÎ;ä¿/¤„¡‰§ŒÃ¾—þÇ\nPd7œzÊ¯à#—JfhÏ~ò]Î/’Ø-ã¨§p…,£<N]å8Xf‘È¶9ŽLÕ×…A	ítÎªÄ°F,!¼ü²«ßÿ¾ƒT£É6–oñ:–ÖMEâdÔ£ØzT­®“D	9(û	7Âp×¦$:£\0z&­„¥Æ:ÐÚ\n€ûRŠÃVßÍ²ç’Ñž©i¦ä›\"Ãmêª:o€[ù7àË‡c\'Ÿ²K<%4±eòÝûØ×aÐ{™å1”èA)YÙ¼•¢.r¡ú£\"÷S=pÒŸÕXBú#-CÊé¹cž¢%\"k\ZpßL¨Žì¦\Zû[óáB¤^$¿šÌ¸Û¶”@Z/løp–›_žnpFc \0ˆFÉPüa5z1çt¦{òº<~Â¦çr$æ[ÄZ“Ç†iìÂ€wÿ…²u”0‘ÈÑßµŽÎ¥P™YµN<ª:9Lý¯v&— þÒ„‘÷<¬;‡QÎÌ‚øÍ\nYÖ_º›G‹Ö`ÇpÐöP¼C—ŒÂËt¸4ª­·ÀJÜùŽ·¿R/¢„ø`¥íðç°xKš¸\"Ä\0´ã&Mb|´\"<\'Oèœ¥ó73Âà)ß)ø…£Ý°¬¡{ÈÍU¨`G“i±=Åxf#FæOdÐ°:%ìËd[™VŠ|ŒD£E¹ÌÞªÕ»4\'-‰nNådæ\'U!oÝ˜ ÚtóÌ¤ÈÌSBêUPE…Ý9g~å±	pë¨(Â2¿{};À³2\ZÜÁÁr¥[Kµ}Ç“lOaÖ°˜&õ÷	Ü;MÀá¹Üa\\Eîü!úC_.zÁ…­—R!½ä3ólSB( @À‹_übÞ¶Ñ½7¥„º}1Å‘6Ö‚6fê^,èˆ¶Ë#jÁ\nä5JCŒ$æ¡j\0V?…Õ\'Þ‚æ»Q‚\'&`nÆ=;’Å‰Bg;Pš÷<Ÿ¤\"÷2½¼ý4	ÃSÊ -Bë×¤hÓÍ{º9”iÃšˆ[úS(a(s»:ìÂÈDB˜„3v\Zì‰®ïÄ8ÜOs¯ƒ$á£ª °”Ygqa/_õŽé&´ÆëUÒúã¸§k©VŒ•‡Ç+Ýd´ug(%IÍ¸IeÙl[`%,r\r‡~¯9=¬vªnzU <`ºiZ_ó_o(0›“ncF#;(A¤šGH„ÙÍ‹ý(Î%WIBõh7ÉnˆW–|JKuñÛ+¯¸Zªè´Ñ×yo0J,3íŸòra1ELìZÝ& ‹^àA?Ì\\ùV$¶QÍÉÓ—!d\\@T0:æ†¾<—¬2úv€ÞÒ\Zì4©‡»³¾\rÑ\ZZe˜jç¥I…·\re¶Û&åö38¨?b\\½v«Wgß (f„ôs—¶>Ú	Ýu~;²ŠèËã†Nžå_¯6éã\Zç/	/+\ZŠRuJÍÕÀšY^5YE)‘æ3€î‡zÕR¨\Z+ü\0&ªâà×è?N?P«öÇ¦À=°°:”ò[xŸ©×Ü	ƒ~ºU “„w}\"Qëô \'k|Ö$9Ú®Rä-á¤N`èt«\nµ¨‰St 0¸”ÕãLïœSt\rJ.„A	AÍlØÍ&[àž@Ú\'¥²(¡²/ÚfB¸1P|€uÉ¾zÁ`”a;:™Zct7æke¡ºäh‚^¡ò¹ñÎå$ZB&EÕèVuF›ã¼U˜Kjikó‰¥_´„Fšs„—©ê{¥„yL‡¹BÁÿùŠÿ’&TcäMê¶t|^÷÷Lq´SŸÓ¦Ù²tø Æ	‹`´›—ÌêCÕ.ºðR±eá„–	êž]>\\7ûš†‰ ]J$·U¼µèéi‹6¶ysf{ÖÆ8œ@DØ×<Kå§d^ÑKsàªÑ·»gÀáVŒMùs­¾’ëf7\\›¿6n¨SWwÄ;®3n>+2\Z¸r¾·¥ö‚‰P)YwU¯º/@Yç]m•1göJÏ:%(‚`¥Ô’Ö)˜#°ÈÂh¿ªG­Ì<ºwÕUmP–¥re|´W\\^¥Õ¦!—û)yH»°¼˜Ä-ÞHÔ°¸ÁýCWprúïÐ|U»c‹¶î°ø±s6ŸÉØªM/Ñâ<cé:Á}öƒKdUcvd³w¤•¯J	ónÞy\rl‚ˆm›Œ\n©¼ƒRKþFÆAö\"2^å×\"ª®ªF	Zv¨;Ñm*¿Ú3:‡9†MYÛ\r~u&ƒ‰€Ý9Ú7Æª	ªÌZ¿ãÞX	ºI]°­H	Uõ¶ô!J·ÍüèÝè$å7S`Þ&/%È)\ZÜ9n%ÍÍdæ	x->18#ÚÜs\"‰NSó$QÙM‘tDzŠk-NmPXÓ vÃaŒ1èû¼‰y÷Þþ¶w³uz/cðÄRJ˜:¤Î#£™*Ízå­W¶\'4NÐ[¨Dg«çµnˆKB–Ú7D*õðv!Áœ*ZnhP;äÉ~<¡ïŒ	êíuƒA-½¤Ë¹ª7ëSR‹<t6œÁ‹ªç7È@\Z^¯%”à4—HËsçðÍ^õÙ´FU‹%ò\ZéU×à«{+2U‹0!˜¢7¯MkuÅò(2=ÑNJ}ò°Xwyšc\0w†ª ƒ·Çä\"ÂFwa¹m†a»–AP#Hª){OpÏÛîÝ’„¤«÷ç·ä˜‹)8Ûtë¶#bÏPË|®G	ÓŠÙ»&œHé\n¦¡›m½smÇK©÷aÝpt¼¾0p<ruœ$ˆ{å>J£Û|&2ÚÓ£œ¶è$[ÕÞB¤P£ªáúTíÒµõe¦[I¨K¼s“U¹éPµEVÂ<%Øg%˜úæ]ï|ß˜+|Ïƒò1¹é½÷L¾&IJôâËÍdË%ð¥8ç,\Z”Ð¿Œ	ÉBŸÿÜ—[Bg®97ñùggc2Ž{±eKI‹¨èY¡i‘#ÃdO©ŸðXâ8Òy5°%Rº®, –_ž¬ôPÞâÂKSLx()ý‡Z×OóÝ5©r°Þ<Žu†®7ä8ÔÛ¥’oPÅný¤‡ºÖqÖ+UEFŠ7w“ˆÄNÁ³X$ÒlD&ó§çPêqK(ÁÍõOÞUàÛ\\i»ØëÙ§oŽ›ÿáþ[,5s‰:\\7VvöŸ	ñó×UŸÕ@½]éZÁ8æ<éÎÎQd€*\0²G;&Ýd(’¡L­ã.áÉA3¥—£lÓ(öûFTNsØÊ‘h´ácé@L4ÍÔs8@šÎëÐ‡¤¶f?U:·ò/c\"»¤³å”àå›yN³ëÏB6§‘ÅZp8|“].£(aáPµjI‘Åi7üø2îqîÉ\rÎOª5Ö0y«p¹!:y¨ó:’œ×Lj˜@ô¸qjŽD¥ƒì»­Ë…‘˜b\r{¬ÃF	Iò0(Ák«jêÝVRBºsÉ£ LµŠ£Ü°(–\n?’‹ø…Ì§Í•OCÏtØEÎ³1;å±ÈJÈ±Ã˜@	²†,(–0n²§vŸ•ÐñËˆ6KªE	‘D\Zz;Ë½ù[h= ?ó,šg±ì1VXòˆÙäQ““W¯ §Æ&1)àmu*Ío®4îu=ÙÁy‡Ï`…Îâ;?9ô·jnŒa(¤ºv9ôÅ¼-[¡ËýhÃ«‚NXª»9ÂÈu:_Ww‚AIÃy\Zá\rbÛ´óûQK¹<=`ú¬†—ƒ	ÐÂÐ¬Ó.¡„ú³—‡ÎõW…ŒUŽ5(äµa>¡EÊù ¼<þ1‚ªðWð)ÂY~EÛNËÌO¸Öæ¶š²}ÄìW—´>šËçq·¾i4Ÿ¡o¦Õìðóe£­{„€T‚5à‰‘aßÌWá4‰óŽ»¹; EoÅuæ\'ëæ­¤&KBº×Ht÷Z3ÎñÂcÙ%öÖôÓbJXÃJˆlü9Ó²€ßøÆðÊîÉ™‡æúi\"Œ¯&}³¢´l×ÆêŠ1\r¾Á=I7ÊÈsÏ\"ùØÑ°$rÕjöC/¬¯ÍSBF!\nQoZy™]µª•ò¦Y7E6?8J Ñ/¢„¡éC[û¼ÿ\\%Æ‘…Â!àÈûtg]r\\,¡Ç\r+ad‹âáe^—Æ»õYK˜ç×\"VÂ|»;—Ô´„]¶ÜJIùÜø¯™{è¼slÓgeã3™­ôjX\0¯ig\n^Eš\Z¤ÐÛu9K&,Iò×Áf„M§®ÇêðÎô«Ÿ †_æ`¨ûØ±‘ËÂµÎtÄ™.t¼Í¾ƒ®íü\Z0Ñ7Áç¸Ö{r&€C«Z9Ö™ŽøÕ³Ê…W@Z$ø`5“ø`=ÝvIç¯ûéQ7Ùºäõ®gAH™+F>ß/*‘×v¦H/O1¥¨aõo„vR[·ù%/y	Í\0ª*8½Õ˜ªT ä;À}<\Z\'¹X×4\0šù¨w\"àØ·5–û{Š†#~ÕXÍÊ©ØÑd0E{qdÃÁ*jt¾&~	éW8¥¼!\Z‘õ˜Ög â-LÀ¤à¤b=¨;žH\Z‡³kIµ¤g4öÎÿ’š\\B	óá„Ñ×–ÇB/ ¸(UcI©eË·T+V…¤ßi,‰Ú®,p¹àXV`•ÏVÖ5ÓYuQB¦v¦mOsŽªz‡1=L™¸gX–Nðtœ­½–òîŠŽ£È ð2Ÿ((7a‘õ–UÓ}g©™nÈÌ©†˜¹USÍnµ1qEGL„EGF ž1j`j!åiÎÎ½åå\\ršáå3sŽqj#–à§œK™2{M^ÚëãöñC+üæy“ø4(m¹¬0ž^$E/Ò»ÀÞ®&\"eú˜>ó—ù—B\"<‘Le£w0hs6n%iòz,mÅiÎwOˆ@ûƒ&D\ršØüÛTt­L€Kl°ÆfñtÉöùxHòœp›º–Jåé8Ì%œ]Ð5ƒ‚6ïà8@Ñá)øÔ\"çxUP’“Y³ÜÙRpCÏ\"úeaNólÇ¼XW3\r°RÆ”eÎoºå²Wue ªJ\0ã‰\"‰]ì³´È€–ÕÜLJ\" Ñj$wsã›Ô¤\':‚e:”i©ÞÆ=+©Ÿ¥»~€3†ãÈO™5máo¯çnd`š}}WØ©@¨QS´5âgåà\'+‚_ýÔãÂ©òe[š&66¾4„E[/P†+™é†kPBÝ<4Ü°Ë}´0ãhXähO˜)Ö Áôž[?eýoM¿P3ú‹\"ëtZMjY­b_=Nß´£t8Cƒ–*¸«Cmd÷,³Ùå9ªŸùžR‹øIEõ•.®ãÕ)a¨äyç%™öŽÂ›–½›¶®–+àL¸9,ëC5ž-¹35g&LoìE‰=ÀÁõøÄÎiVK¥Ò@ÏÛÞú6v(Æ5š¦VQ§M&>óWL©ÙÉnÔš×L\r88óK0©¦¥¦>û™ÏÒž´hÕç|Õ]fËtÚÞ8fÕæcàÛŸzòt\rÍO¿æ³m¢ÙkoLx’=sªLã×kÚ&žcðÒ’ÿzí¦Ve’^f\0Mø(›t1‚¿”Aré\'Cwœ|ÐÓQ‡0…1­í¾íËø¶²MKÜœö9Ç}ÌÍ×ß`U-o`IdÐâ_ËÝœtÜñÇw§éw(–ºþ æA}‚0½³·:w–xˆ“˜L­(à_-˜”-¼FU=ß0Vb—„êQm¾Çg	Á¬1-Ò¢$ÔÚ”\rG§é7J#?Þ|‘èüxZÜ¦Õ€NYç.qB¶;\nIÁ‡žÂ@C‡¿º†å4s·¿¹´/MŸÌ¨Þ;³iJóÚõªnÅˆ†³¢úw•—)šÒPp5Á³©T@Ô¥ÃâuûôšHz×¢lÎt7oÂ^”XÅŒØÕ[7W¸ë§j€‘Bˆw>ô%‚ajèÝ’‹Æ¿£ïqdYÖ“2ª€ÎJ\0ÜÊ›»&ß©‡Of´¦N§®2àÔX5P,aLù7êÐŽs€ÕêoþæoôGýTµÏÄcŠ‡ç›!`„’M»}ñ_ÿ½„üç˜\"õ:6wèï[]5”¡nµpJÙý&¡Îã~>ƒoÅŠ}/¢„Ô^o©0¦&5ß§1b.Vk¹M•5”H>è>,ÓêätFù^hS7Pãe&`”Ë¡Aù\" L]dÛÜ8ÚÌíÈ‹hú”xbx´Qyrm£wˆŸBÞÙ\0[C»Idn´™’²Ý›k!”P*wbJÎÇ&P›	JMUä5ÊŸQô/’ 6BFv\0PŸt\rréxÊ2BëÃj]R¨glAÿ¢ýÝyûí¿¿ãÎ»ï¸Äž{ž•-pfA›ï½ÏšÉOí4ˆdû)\'ÿìÐoç„cŽýÉQGQ@Rk#*MÒm¼3AþB_žLEHjÃ—è*9:’Ðj©j³áwac>]ìÖ÷Üö%„Ëê¿dÁ\0½­\"\'oý;”qò á€ÐåóÀg_MºPƒ2#ljUe2¶¨M`·À…|’Šç>*\\ã6RÉã\"ƒù1/ªøCzsŒ^÷“Îè&îì&Ð\rÐëhEò=¥3SjV$D´¨ºžèÂZ™[ÜµvÒú!±\\´\r!¡‹XÀåî¹¤)÷…öèé{™ÛK¦ò_æM2‡r£ted©F•ÉPúW\Z^¶‘V£<e>üª øßþ·ÿ\róé¡ú=wß—[¥ï”lszþíßü#ŸJlg)\rLù]­äÐãhZ\0a™vµ%+!Jàç±RdPû›Z	½eß\\%1ˆëFõX.{Á©¥B’*-V %È\0ÜSšZª;1\"pêúˆ«ZÞYE7®\'cM-4›GúELnk™¨JÊKUUÇôj[H	¼d*4|e|ð§ycÈJM¨€z%o›>Ø~Eð†`¤ñVŒE†ùœf§+þÅ_ü5SÕµfgw®gö¬úRç«7*|NLøe©Ëï~ó[\'Âé§œzõåW<pÏ½gÿú7?þþnºîz|àÈe_bä¯}ùŸ~„}¿rZkTäefY*S‡÷Mú{nÚ%%Ôƒ˜º´B­VÍ‚öEäZ“‚Ù„©ÝaIÏº§:axyD\\RŸ]JÐ™u~_]SF{Cê¼¤Êñoíè­‚<ŒN­Svñ+K¢â¨¡õèìLR†Ü\'=Ý™ÍO¥[å;JéÑÄz7©ƒtÃ,Ë£HÎ‘¼I©“ãÝeYªá­ÜßÉUµÓ”·•H;5æKc²Ò?ßƒôe½^Ã5<~9+xJ·ÊÊw‡ªe‘Î»µ”àµ1(ŒnŽRµT)ª[;é7Î¡›²!6½³5†ÓG;!-a~ºzñ¨\"UÇ¦o°ìý¦…8`ž`‹!®ÿú//•°#[gä§Œ1Xð5dIˆ¦ÑO—e­G	#\0k‡â/¼l5êáž›gÝrx\Z¦<Æ-ã	?Ècâ‰Ù¦9‰‘o•Å¡æ¥ù¸ñ0W)Ï†î‘\"“$…bþe/«,ÕíªRV\\€’\'¤hœ¹4„­iL‰`Šj½1),ûeÂß-µFþk• å_|àô\Z°H²ë0CïˆásÔêáh’w\"¨´\0=yò­ a±šùþŸÿ‡WÕ™Œ¦êº£“»0¿mˆIåáˆP‡Sö³Ÿûþw¾{ÎoÎ<õg?g@k ó«_³sÚ/Na|ûëßøð>øÞw¾ë¶›wÑyçôþwì±Å‡S-Ý<Iq¼	^^y	và)yºá ; ¥)H-£/éü	½ÓøáÅ˜Ç{S>HáÈ:Ž<Kn%u)%µoo›VâWLÿ`B6Bídc€²rå¥Ðˆ¡ôVõœ´7+œK’í¾ýªZÐ?Á€Y=Ý	žîª1ˆŠbÓÝëqènÞŒ>ÝÆÀÇ Í7n@‡bZ\0.N¹qçú~¥#ÕTWeaúç¼]´Õ‘Ç#Õ»\Zë6Ý¶–”ˆñ¤ë	ŒÁzzêh”:oß”0\nj¨z!OMï›.ëŽ*6¦¯ÞT¬JpÐ¯U™Óu6äá¦í ¹à‰e\"ßü¦·7ñhš÷dìš:“ã(«Št=q>³oRi\"%Ð|½S³¶-¢„¸7¢Z¥@ªd¡ôå*1™ó–¥šð 16y!ø1R©&£i&¾\n–îãè*]óÍË}  R!ñ(ZÃø):©äu\0ß%tu4²-yy&è\Z>¥h Jh¶´]½‹¿UÈÆŸž;Ç\nÉz€˜Ž¬ìú³ °PaV|U¬ÔÁRÌy]àJqK\n«í˜ ‚éþe#…ú­LøÐÀ/N:ùžßßÅ>@\\FŸÿôgÞýöwà	”pÍWÞrãM‡ýà‡¸Á’ÈVKþéGÞ:ËJâ³Ò›ú7ø‹ËÁÜàž¶’\rÑûwNßN\nS\n’àõH±¨óµZ³	3\\^ƒ>»” 8äÐûÐøZyb”´†yk—áÏaÛ	G7ÊTui÷á–)´À\Z£ˆOˆêãNE©Ÿõÿ1¢8áàè§üÚÙ(Y\'r«À½—Ñ}üDUÊè8‘ +h/Ò(¼è$µH­?þmÇsõÄÿñ?þ‡{v~±“Ý9ÅÙqá¶¯Bõ\Z›nU(‚-¿“—l¿Q‚â †å½dè‹wÃ(S®GËü9¡îPÓ×#dÇSy[í`¾êAµ£Ÿp¤j¡åh_½¶üœðéÙüE²Ø_þ²W‰éNˆòW{æ ·õÐVl}F(¡J0ÆØ`+9B¡Þžáe¯Ø‹\Z®Ìçeâë?Nù9kù¡Q’Ê#¯+ÓµPþ¶dºMýêù4)üÁ‹’^_òMJt£mu*ërC‹tO8J€­ÑL*U2=µÙêÓÞ-â‰‘é42wÅ–Ëµµéi<ÑÐ°¼^êp²î[‰ì°`è›Â\'TNxç„dË·ÎV×MÑã£YðŒ]+³ÊB‰]ùš))¤™1‹ezä‘|A÷Þu÷ŽÇ¿ÿî{®üíeÐÿŸùì¥^Äq$´€*Ž:ìðo}íë\"Ï‡|á‹hCíwçÀ«æp$K¹Qi9÷µ—ÞÂ×Yps´4töŠì’¢Ž–äDTJôž¬ºs8Ÿµ}ž]JP^Ý^âP³ý„¡¶ææ•Dú>ØåQlÁ,UšÏÍ9UŽƒ\0‚×ˆ!(»˜r¡Àë$9ƒ»n\0Í+h€Â„rt\n\Z‰oÍ8€³n«ë± †§#:óQÇt-MY\nH¸Iâ—à\r.O\Z‡Î‘Óìÿü?ÿO\ZX/âØ¿:‘£<ÝÍÒ¸ˆzœR0ŒHx.µýI	d˜%øÎMÚ‹Ø!xì¶TKà6š8æp	U]UmŽ³ÄÙé` ÀÊO¥Îo»m²BüÆ]÷ï‰\'üüßÿíå&¦sÏBÊS´²NÓÂË³Öq+uîP‚f2hmk[	c–ñVòˆÊãÜ“FÐC\ZLàBƒŒ>÷µ—ëíoÿöo5¶jÐNµSÂ†¢OtôøUÑÐ„Ã·.„	.ÑOKÃk!\'Ð†¸_š©8TŠ‚°-tÅˆŒƒÂïZQÁq;\ZkcÍš:Ø(lÅô­-›ªZŸÌŠœ_z;È+ø\\Oƒ‰TÁÝjÞª€™òÚz_Q•Ä†Ûw‹ØòÎí;žzâÉGzXÆ‘H2zØñèc(Aù—?ÿÅW¾ø¥m<xÄ~|Á9çêƒÉªíÁÁÕ|Gì(&€zTB@0ÆµUù¹\rE©\"ú`ÞêÞpÓ-e3|gø†o¡c‹=·AB›Ìê±-Ê8R±Þ„:|<QÙk÷ê¤W;I¦Ô€o˜#ßÎ‘Lll3MY êQw®YÓøÚb,ÈLæÝ™ù\"Tƒ(OÔ#ÍQ¶13ÔÛâ	&ˆ?¾!Q˜€ö¶®ò8G†27Ž!Éç)ò8¼§0i©™;CzÉA­\nO—PBjA‹	¢÷,¾ßö!ãhßÃË*Í³d»P[a41öÜô¹jÀ5´(Ãk1Ôv•·Ó8E5Yl‘ô“-Èî\'§hè+®¸*wKá;-ÓklÖüû+,3cèkÞ—<K3\'ÒgçA š#æ3B	%ò£³2ø€¹’|{ïÈ™\ru‚6\Zà„ã>NÖÍÃF“ê+júý_ÿ}ª‰£&âz¸ HÜšTfŽ”ºÙècí;Çù$’Êƒ]^2x­Æ›\\…¤2¥#’îqC^¹]LûÇì£BëÃpËsè/úÄµb¾£Ú5r¥ÅR=z\0_ÜPÁÕÈ­Ç\"yÀáýIÚsúÜ¡:ÙxI¤`L‹9™f×\"g6Pˆ :âLû¾FÓ¤Š›é²ëŽ>ê¨¯½îº«®–häsÉþì„™Ÿ¯‘¼#vƒœT®¤ŸŸxÒ×ùÊ¾û½_Ÿq†›{¯DˆµŽý^ÌÍ´ïÇ}£+R®þýëéíW§u‚Ë½•‚k&éIæP–A`ÕO	\rèï\').\rÅR5ÖØ~(\rëí¾+c½	6ùX¾©þ\\ÏBhŸþN©T‰ßÐô3àœÛ)	ÓMn£Šï×àÛ……\rÜÀ#L©¡ÁŠ®èí2V}§rVE½’›G	tRÚ:}?¬é4ïÐšÉ„j0\Z2¾¹†4S‘9dPÇÉÐq[Ï¢Í‘žÎ¤°Ù)2\r»Ðrš1h$Œ¸¹›Ô—kµ 2”wÂ¢%nz%Ò[ç!²WõÕá”DþÙÏ¢±1cÇBJxx£)÷ìžÍoÖ¬Ãé=³éw_š´›G<x½yUTšÙ¼dæ5Â¬Î°pù®¶ƒ&À¥-ôÄQ®ÜGd†}RÑä§Ÿ~FÐšqyyÞÃ\0ÌöÏJÅÌ’˜\"Ò³Í=Ý÷?Ø”Y[O	#å˜~NjisZx§l¼qù—Ø²)çü»sÇÓ’Î6ï§i¡3÷7Åg¬hZç©‚’i[Zˆ„#.#‘Á¨ôJëß4&øâ4Ò9ú€?•Úd‹êã•œ¹³îôG>HbvAV‡U…lÊ\n]%ìJžnnJƒúp‰Éóe¬¤\\ê0þÖ”_ýÐÝ•×EO«¿5ô‘«ÁŽ~ÂÒjåEçPù›—È‘°9¡ñ2 ÇñÄúµiÜž~åK_>ü‡?’t$„ðÙOüÅÏ~î]o{û_ûº/}îóüŸú˜\0îGüô¶7½ù-oxã«_ñŸÂ	6\\î¶t7\Z\\O´±ùh‚Ì;˜Àx+Ê{z´þã»¹Ïœì\'©¦Æ„ŒˆÓ­0RP£§û×Ù\nãxJJnvý\ZÛCþQ!ˆ\0ö‘êÏ1S;ÔX=Ÿ•€ù†ÂžU&dÔÄ¸úOSô~jjFIñ€­î	¸ÁŠ3¹×”ÔOvˆî	J÷¯¨T~W¹m*}AïpÃR9ôb€ÆÜÇ\rË®,î–c0JˆtôMó ÉÎÌÁå ½›÷Ñ‚DÉ9Ò¯•Tmø·K¼›×°Ã–z‹‡âŒž;ª4®õÝyvR1¯EmÒÖ‹—Ðiª½R4Tt“®ÍqÙo¯ºä’I…BŸ6êÔØüë¹ª¨ž¨PžN9 v890UU¨½ H»8Asá·ª¢!`Õ;±êãytÛiÈ”:¡7Úù˜£KÍe4S¬·?ùÄSŒÓóXIÞT@Ã§”ÚŠbÜmP€>òò7ï\Zk8ŽrŽžÝÌwÍi‘«kÐo•îãWËD›Ï‡±ƒ¢qIÑ!ø®ÞÃ‘R­	býªl¶<6ç—£íÙg,\0µ–-·ñÎ±æ¢â’&yÝ™:VêN~yM¢£¦RÍúóîZÿðÐeÔ*Ñµ×O>ñ‡å¶ÂøØ[Æçfõ€†˜=42¾Ñ<	u0¢ÃÔv­t¯fÈJÚ™ƒþ…¨Bµè~Ü”®\"U„µß¶¦BËÕë›;ÇN§y–~N%¿ÿ¾û¼ï~ƒDNðùÕ©¿O¾þêkŒPsüÖ›næ2ºï®»¿÷­oã+_ušþ]?ÊÛº¹Û6Ö¦GØ\Z9˜„·õJÞ¿µ9½›K*Tˆ Bèª´lªY!/™ŠPÍzâ8rÂ1n;Ðd%b`%qPÂÈ²ÛÜPØ¶1™]¹†C•G²Ö¼wNÆ¹­\\ÑT™DÉrN:³RWçS†”NkPæ°ÐÕÝM­B+ÁÏNîÛñ†2x\n)’¥Ã’pó0(ÍÝOîéVƒ’Q \n.óGy¥²„hµfò º^Ý-Ó¤{úÖ”Â,Šï*\'g¶FÒuÒapà§tm¶Ê‚wðÜ¡)g¨ŠÎq¡_¹¶è\n`ºk¶ïâ9ŽJÚÑÐæT¶òù?¿äß¸_¬ãòÚ×¼žÃ]“áfÃ†lvQ-bT^×èù_þv1óíL/LÝ);ˆRëúvq„ÆFÎÓÒáèéŽH\0ÐM²í¶NÓsÝÁ·ã™V]Î= Ýàíoç—¿ôµOüy(jF}Óû‹äná·7‚iß”´V®=üqêYÝ¡T\"ò:×f$ºâPµ&‰ƒ‰)¼fº6aÀ±Çœ¨*MÔãø°k:­ÅÑ~wóm_úâWá ÓP‚—~ú©§“¯Å? ü°{ŠmÎféj¬cbÝØ‚–V˜D*+¡Éñ[ÚtXpõ7ßé•dÑFL«‹¬ZÒIdu-F¨§d%lçøéž»ïW³ìS-Ap†ŽåØŒÃ@Ë…û\r±Í&êx`‘•0È#[AÁŸzòiã3L~â	\'zt/YçïÝ†YW\rxóÖ+.ë?ØMÓ¸ÿºnLzš—ß#˜îÜmS÷Ú†XØaûægSÿwÜ~‡qã0ï||»àJà&a¾óÖÛ{x›Øòorûc˜ø½Cc\0Z4ÁVrOé_5ÜÂ	a7äÒÍh¦½FXÙµÕ‰·­€OŸÑ‹\ZÇPÆw…ê&Í¿ñ’íxÉWb‚?ž¼úêŒZÏÓÃ÷À+Ž÷z5€pd€ —LÅ	RãÒ1á;	µ»U)\0U…3Ë\"Ÿ*2ÙýÌÜŸ$ÓhåÊ.×½RMÀ!£q©ÿ.Œ™\Zµ·ÇifnBÏeÓxVV§¨Pég.‘AÄË×0xßÞ\rOês¾ËùO¼¡‘ø`·Ó‚ž›|§Äí¾ËšuJ	—_4¬OƒqÛžž8âþlÁ&B|¹À-^zdæÅ}bç´#ßû@Æ‡”úë¯›æÑÍbG½iGVŽÍ}DS.NQœ¤°Þ–ˆâ¯Z\Z´Á k–0•à “ŽH,fÓo°¾\najjö–_ôNÓ”÷g@h)¬5(N’Èkí‰\'ø‹£Ž<Fž\rŒ<±sr·Èä´t±838JÛNm%Ã†¦ALOÌw÷ÌQgÈãÆX8óúën”ÐN|S…QÆ´‘›”Écž\"YS9»UŸJÑÀå¨:È%ÈPÑ´\ZSºÙiV5›j#Xç†nN7¸O7±i*¡Q¡2;°®JWÅ¾é*]K\0Y~s<ä&\ZÒ;ð{ kDê=)¾ûX\\A93†ØhÚ!£\nú¾O,¸œüJò4•²“Bô®ÆøÍ³]²Ùq[0á[fM7]g2E³Äã	ÿêlDJ‚‡Òè¡|\r¨kFñJpœŸM‡äMR½tBÖ€TT£ÕÐ€x²œTCÕÌ]!€ŒVÃ6—wÄJ˜²¾f[:²uÛà)êr<=ÑiZYåC0\ZªŽB¹6¦o!„²¼*hì7Ä|u\0çsI·c,²Úöß)aÞ¸(–€¤Fëõb²M$Z5“s”(òîHZŽ}|Èé¡¤7§)ÒšÓP‰\0GÈ*)%Éú…_YQjüõ_ÿµ9…XÛúŽ›èJº€\Zö¯}p#w…n¡sibÝÇ¦Jƒ¡ I¯áJ¥·êþõMt\nßêZ›3)›4Ù¦Þƒ2~Í?éLŠ3Ç¯wð> vÜüw©Ï¨K×sšòÚ\\Û4ÑUÚP	Žž!ƒj_ßÐ¶îþ]Ø¾‹­}–2§—ÎÖßš7â¿Ç„ê>žR‡R¥UÊÆÒËÀ”¦¡¡ªUPÃÕá\'-Ò_\në 2ã©Y± þð/Ž©õI‹×»óä0,ÒYoäù\rox“µ\0,\nimà¼Ð­OCÅg¯üÏ×XS@qÊO§„´¥A	€b+)aø‹†o]ž“™ìdA™.à3´ã5n4j*$f_øÔ~éK_nÆ«Ý0Tá³C³°¯I0±S§Í¶FLI¤Ÿœcƒ«È\"M#é{„¨9¿¹yU¥;Øg|•x7fî¤_4×›†¡ïÔñbÒfMÁpIA{~Œ §MŒØ”’©æçXaÎQ†ÀÌ×ô¨Æ{íkÞÈc™Ç >\no\ræ;Y‘õ.š”Ó‚›,èÁ\"$RÎ!‚\\L:¹‚äÛÍ,H>BÒqŸx‚ˆë¨D\\Š‘ð²ÁÉ?:ôû·Þü;‰§èAhÁÁ3uÆÞó^ÑfæÂÓO<i›`Ã”ž´Ëéq=\"8NAî‰íP‡›\r‚4oÚx“¬ÆÞgxN\rvŒh|½È­t¤ºkûÃ\\ÐQÁ\n×vY‡¶MB;<Eõ®Úw9%TêA	!2µ^£†Žº:§ÍAL‰ò™M\0E+8Ó\Z&ÆäÙ1Ö€²¢{RAï†°:a¦$5•©ƒnÒêI\r˜ª/8ÍO:MHoòJÀxÙA.´Ùq2àdý‚ÂîÎLvª•Mßé ½IzØ8Ç™Ì¯¯¾¦e½ÔÖm\'Ó‹=×¿a¨NgÇ†5ñAmMÚQ#ÕPG—-ßÆ0H´}	ÅÓ”mÎ\n‹\r>ØZl¹.OõkîOºQo¾é¬,\0oëµˆzÆgÒÕÜ‡H5ÚÆmLF¨5x~Q†NšÙ.3Ð~ã”‚\0Ò´´»è§¤•ã)NVu¯~ÕQFï¸ý.ÛŠôd²)¾ù0ÞòæwX?†#qÝåÜÞ°¿Ý\n8xUúnîú¡Áì^oëÅê¥5¹)7+¦¤šœìƒf³o§V£aqÍ êñÇ—_¥Òe$Œ6AàhôÍ´C\'R®i¹òùÌÆïmÌäã¸3UzéqDÊ¿MþÜä½Ì.\rC4	1VÐZ$¾)|Õr±#HÇý›“´€Ä†úùè:öáãêƒ}ZN®¨ò ƒ\n»ˆ:^\\(wg”-ÃtX÷Ã%Ö‡Œ^ÆëQ¸@üpÅ„)Án°žúfT½±tu˜[,8ÒÖùmãWU\'ò”ëòˆTÓm¿»áÆ»ïü½Ž#”À8Eà#ºì’KñÄÎ«íÌ”`:¼6R«ÿÈXCæz·z’S\"€’‘çU9+Ž²¸$ÿX,¢é](¿õdÒ²CØ2Bl\ZTç…yðW3¦|ÒPÙH\'›$mÊ\n9f=k´£hEê@!Áæ—ë\r+×|‹8žç\\g½3¿P.#%*RulPn¿€Ÿ*r÷w-Í¦¬¡D:¬	h¸ô\'@è›=¢¼,5œfê*MS£tÃ¶‚sî£oœ‘Ž2Xß­2›NQ¿³“âï›ŠªùPÔ4©âlKŒÛ©•}§!éÔ,l\ZÆ ~õ=„–”Rþ0ú/WpuJÐg§¼yý—–Ípç“9ù¤SŽþé¤ãÃk&ˆŠJÙ5öuC&Æ2#ÅT«/®$b¦­Õ-“l7Ï¼3Õä@/?Œf¯JžÕ¡¾ÌÛjÐÄÃÉ N=h/|@\0`×ßðfj4wK¡cVNZ¦oõkÀ+Lžg ö¼¡R4drYXN	óºRã‘3S\Z†§z3”`ž\r”à;˜Rì3{³iqMsY›C[ :fÖ;w<Y:Jhž;õØJÂª¢Ñ_cø=–B*¬—ýøc;®¹æºï|û{r\rÚ6ö–tMëh?ìYß~Û\\”¿»ù–+¯¼êòË®8áø“Þ÷ÞuäOO9ÅB?¸ÿÉQGÿû?|Ï{Þ÷ú×¿ñˆÃ:ãW¿ùéO9ü°#ü£Ã9æ¸_üüT\ZC]rñå‚â	 9ûhƒ†›H˜±ã2J¨	ÇœÛ<lo}Ë;\rÑ¤²%z^Å&\0z:iL%¥84°ÞN @SkT°3õš]I„ÎÑ»Â…ºV¨4ÔÕn‚Hˆû´r§Áä“ƒYãŠ¦Üuçíwüâg?çé¼é†n‘ãánÛ¦	¼÷~ðC†ÞÍ7ÞDì†ª>ˆ*Vè‰õê&1ôžÞV»ÓYF.cb:Ïsù²»39önÊ–öÂÃÀ\Z°¾TEzæ˜2,tëÎí÷ Åƒq3ûL:¦õaŒ³ð6ÿ˜T¥æú÷zv`\"à\0…JÝð‹°µÓzUWéüz>> ðY?áÂ({§Õ^\0‚gà:§`¯ã£eõí¢f ßFì›ÀýÿÏÞ¿MZ•iß÷üY÷OÏûÌ3yt3¦uÌ	³¨¨#f1‹¢˜@E1GhÉHÎ9uC“t{?g}¯^ÖtW]×\\w74ž[oÕuuÆckß´ÖB\'œ	}\r¹âpVQ@AsÇ€¡êåØ¢x5«g\0ëÔ)7‚yÎðòä‘}ì0€ÿài ¬Œ40b±»#Î˜ W›ï~õ$Âî\"MKVvXÀê^dë99LŒ¼rTqORu‘<ÿÔdŠ†AÐ-7Oc}ïÙq¿ÊóñXÆñcý€ˆ sw+Û‘(ÜŽ–Sá(4ÐrÌ\Z’ÊAm±\rD…<°·\0kÎ-’Ê¥ÞÂÅq9{Ä¾ÞY¨ç¢“|vs†Ô~Ó2\ZNÓ Ó´[\'x¬9˜›yË[MoÖïÏÉŸPÊD¨æÀÐ@·É3_uå5ýèÇ_óšƒ.¿üJÇÌt{á¿\r9\0ÿ¦™P‡™ŸO0~+4(høƒïÿä™Ïx.èšEŠF3Dˆè÷•¯x•s:½ëk6ºÒd×Ú€ Fp­a–’\r©ŽÂM¶[7\"eHôÉb8ót£ö¬ûÙ<Pwþé?«0c•K]L5N×ßbì4%øãE—Z¥GQ¬³ú‚ÒXÄf9 ÓÌú\'aî»ã¾xìWýÂW(Š©ú¬¯Àqó\n{p‘ŠØ\Zrã\r7©\\\\è(ô¾k	9/~Ú©¿xúÓž%;¤>­WÛ}£@N§Õ‘¨Z0±\"â\'’¤F”­º x4U¸€&_!(#tF	VßkB4jšQ4²èB&†‰ªQ\\ÓõA	È¨¸PñF“Ô½ÓûÑÛÃ×q|àÁh¹çÓðç2T‡ÉÙY½¯/\Z‰íW]ÑÛ;7ê€z¾Ÿº¯»T¨ãDH¡º;Žšú„ì°œU†Ò¦~«·‡w½>VŽ\"Œ£)Ã¬ä¬ÂžÁbôÞC%zàšÐ8†\Z5Apc\'Fí$Àg2‚!;É‘ÀYwq¤ŽÆ¤mhhMCO€>ïJ@C+C7Ê€ óÞ°>ÒìRºwI£0ÊÇéjO1@7-‚Ñ3G–5ý^ÇÅÁ½{¹B¤b’^ìî±5®D}p|£‹rtüšžOÜ¿rUµ*\'›´¿Ü2d¸þº)Uî¦)ðÐ…Z„‚éÍ[>¶ßh\ZmMKãu“c¸}±ræWàsŒ©?ök3ŠëP[Ós‘Jó­2Ô\\ÜFwÉÛ Æ¸ÖÅ}:‹\rÔ4‘)ÌŒ­\'7\"öv\"KâK­m³,šLvÿ÷7£h·LE|äå.D÷Þ#_”|ÙK_Å”n&¦QŽ™ÿRÀ‹Sfn&¹ò®Óg–&S‘º´ü#\"\r£[¤Cõ«ÂŽ¥;ó¦\0D	\'ýìÔp¶{±ÙËg Yøýð§gÿá\\ÊûÞû!uÇ#œåíDß`½Œ7ªëÛ‰3ª ÝR°¬AŒ‹;ætÅjhÃ’A0ÝËŽ€Ýì¾K+Ž†;åî7ßdÌöŽ/|þËR/rÜÄe| ])Î¯†^›ä2{pÙF±tZÙ)­æ¨ó°žt$ê…kí¬G… Yv¡\'à`9:LOfÕÒ¹&o	BŠV§²ì&™1\r§E˜íT¶l¥ÃZÓÅ¦ôÒ-<s\rÍãÍË’u_/ˆwß&/©cÔ	ë!¡Œ.äàÆ.U%év„ëŒÈ¦öÊ³_óÑIzßÁÿ7(a\\?<ò\0b/l˜Eb *ˆ)8^!µWpPZ…Xî~?µ¸ñÍEP²÷4QBpÙY11éÙÉ‚Ö}`Öà	ÑÅ€”;–ÉÔîÚ—áyØ GÓ8†õŠ0´çÆò8‘‡æÆ‰±J\rÜ‡¿ZÊËÒ}ÖY=O¤ùh—´\n+gõx‘Î¢r¬\rª1’´°•Ÿ’¡{qõID6P·‹Mº´²µÑB Oð5K\'L\"\rRÇ–üí÷î„ƒ®P8ŒJiÝ— Z…°›ý´\Z;ïh;Ïì‚Y(AoåOä/ÚãMuC°ngóz9¾…ª¸\Zš\0%Èžâé¨(õ¨»¹E#{\ZèòÌc\niµYjXU¯V0¹\"1¶”‚ò§#\n4Æ„¼(áª+¯F>Ìðfg|0LæZT”PàÈaSÒ|û]8€e\nw2.¼m\"¦UtŽ8˜-”›ÕCˆ¬Ei‰šè«j÷®_p²+æ¹û®{Efø€RÙü\0_Tò„XP¡ü”@Æ¼LJ‰øƒ¥z»YÎ€«±«Ÿ4c»dT¯+”]X–KÈw™ñÁTvõU×XAèÝïzå»c#°€4§Þ%ßëÚ~…—àº«Ðê˜t¾îAƒA¼¸DC½2ÙRwßé.åƒ2ôŒ\"º‹Ã |åžŽi\\ˆ[#­¦6ÜÁYÙ¿E*àE+@…PC/ïÙìÄëŽç7Ã¾ú6ÓY£Ýhx„áBç¦ñ ï˜£Ð¯	n\"Ü·pO/Kt]¶¤Ç¶³­‚Ÿ€rSÎÀâ“zŸ»¼&rÕ|:¹Z¦Âó”wô}à8WId‰õ³{‘A¨ód¬@\ræ3Q7SiÙ¥ª…ÔÑ­ŽãHÀÚÐ\'WÆ>êä\"þ,@Œ`—†Ä£F	Ð_cqé°~YV\'¢|WÓ\"EipÂsLmÝ£R˜èt$ùLoI\0‚K«x°CÌàó2\rìP3‘d¸P™Õ‰7´-Aj\'é¹&íMÛ»ìP‰ÙŸK‹PçÍä:iu\"L‘¸ƒŒÇàí8^>Qç‘¼W¥ó£¤GÐ²%„2\Zšk™¸\nÐµŠIýèÓ­F=2\ZÉ ¡í¨×SÕ–Š˜¦8¨1´¾ˆG\Z‚õêY&Àdï~é‹Ç‰ÞCÎÕµ‹)!Ã¹¥1CX9û0ÑKšÀ]À]dœI>ÌáypLîØbPÂH6LÏzç6¯ÇîÓ{Y‹úý#5ïùMÑ2õÅŒ)Ý;{Ü5Yî¨HýkÓcø,lå²Ò\0°[8Œ»Ä*çOÈ\nô„>¥1.¿ìJ¹nƒ®9Œ%šucg9éHÙÏÆ…Ïþõ+ªª¬³òÎ²Â „Y.ënQK©xqª‰Tf•‹»oT™n1¨Ù˜ú˜îTj}„Ñ	GÂ \Z†þ™Ò¨”©’¹GºPÅ?,DwÉª\nÄüéOrPh\0*ÚGl®Éˆk¥í¥é+s|¬ãiçß«ˆ‡îášAà…péñX8aØzÞÓö\"]Óí$NÓºW‡9 ¹=abõW\rÕæÖd¹×3pl!%ä¯ÔLÞ‹	©î8Ó\n@›Gâí5þ#’c›+œëuzÓÂJ€óx;jˆF›C¥Ñ¾CÎÞQ8%ˆA´ð…W’úÉ³AT%ÏÀY\nÐÜºöØJæéƒ#Å´)Öég¾xG×O+ÊUÐ”*V p¦ƒýÇ{Éæ\\qBðM>5î2ù”»x…¦\n¶\'{ˆIÃ…²É–>X\Z„qÍfO‡=I\Z>‹-ŽÔU³#+	:Õ”‹³ï\"üÞntÂ§ü*bÜ±¦ì^=¤Ïñîü`Þüž\"	@§kW	æuÒc¦›V+iÑ‹dÒq¼`#HdBAK½Õþ¾\r\\øIó5¢eF³Ù‘oÛpƒ²_™ÅBå?úáÏF^saøhUàh@á\ZQ{œ!So”„ Jt&ñ°Û?‡™ÍÂDå‚ÔÉùØ6MÓ/±&ø@!—÷±“bùžÔº\rEì+<%€¤1`t4Ø0ºÞ\\òÝ0i£á2á…€<XÌ1ûr÷ï~{ïŸÁŸªþ£TÑåj£Ä(ÙˆÙˆžõšy\'½Î²I`öëö/¸ÈÂy’Õ×.ñ¼µÞÕˆ:pXF#d¼{_ÒÍ0PñŠt\"ìËXæiÑ•°#€¾‚®®—Ât°!£Ô±rFôì,±8Bõ=—RàÙ€;†pŠƒ=NÓT\Z„ÑÙ/uqŒ¾‘]é tàQÊ¬od\\»HåK¥{=<xåyžÊ\rÏùµÐbŽáÂ_sDZˆÅ~Û€Œ.×&¨ÂÝcbý_oÇådB8µù3*IO3Å÷¹VDŠÅD‡»(Ù.Vjâò+ø†D\ZºÕ“ì‰ÿ»»{ë‚T¼‡Yåiº¸Eö:šžE8ÄE†N×ïÐ€–ÕïPŽs‰Ë½´Å(ð˜ú9Ë­+¬¾Þø)FÄX.®çs;ëÞ÷gÔÂ)ñTƒìŠ:\"q5bdJÃ}ÉnðœUÖb‹F·¹/&£EFr‰ ±Šéôî;‰‘ä–¤—çì¿ÉŽlÜï:¾a\Zî3:H„W_ueìŽ:\Z«œIž”?4¨±æóÉÙ¢«~õ\nÐ9K0S¼K×&s¶oŠÅÃë\"XX~d>·côPvƒÇ‡@«&ƒ nâ`\rŠ¥ÜõúQþtÕÕÁ+Ø%|Œwk>¡eÿSBnÁ€¹ìëù¤DÄ@Ž^åF†\ZäFdDÏÇUì·%ríiœ5\\rŒäîuHÔ,\"‚H¶jï-M^úU0X,›-4†<CéØ^£ò>ôÁ#¯¿n\Zoì`>Á­·L«OôÀSã®û80\"\\d©\nßH%ß(\"ùj§ìO0¥+Fº¦º#·^á\"Ä|®æOnª;Y¼ˆ#\'RYB	ÞÓ~YG=°þœ1RÛ}#1mkÈ«mšt¤*G?¦M.*±ëÆ4²îäW<d\'Íu:z\0Cú3BŸ·1!=aæ˜+³wd{Ôñ„ó”àa(4d&Â8ˆ>)«h}•´|€HÝ²n–tq¸Ö”g|O›d2EÃ¦¶‡Í¨cxZ·Ðý`\\È•·ÂJÝ%xÎ†¶BypìÏ\nê2è8U(Žaùð.€H,¢!WÂé]æ!&/G7áT5d/\Z¨ÉœR‰jõNú8Vt˜F;\0ÖkD}\rt3¾ÄÄìn&ˆÐ‡§ÒÜð7Â 9Ú¨D”‹xA(\0ˆ$Ü¨rYÝ6˜&8¾ÀNA©#{6æ…ò¹*/—iòPN×”MsZœjy0˜Pr;vOŸ]œH=\'î\'OF€÷J†“mëFvsø«cŽ°Ç0(õÊYÏÆLMóÜœŠÚ<;FÓ¨áO8¥tQòì`Ý‡ûÂ R†‚‡,‘ã±õ/\rÄÈˆ2}!CÆ¾âˆ4\'bÓ€Tá¬amÔ¦>	ÖñºK×PUÕ°Ôå_£áµ)aèüÝ)L¥gy	P¸ Í.”Ô4s^.Á­¹t‚,—×à\ny%mÉEB¼:Õ!ÍÑ1ÂzLÛ ¸¶”Ã“Àðz’^U¸¸Z ÷R\Z$¯À¥°ê=µµwÜ\rÁ§ñ<Y$ËmŒ8˜ý¥b»ž°—Ê\'ö~äWè¬ïyHãÜ,šM²}×\r×ßì	•6‰°ð¢—áj51îÎÀQÊ=¶ì…ÆÜåØS:û7pL_ûS‡¤¦ŒˆB½¶LH§S#a7ª3:Û°/ê–,Ax\rGô¥¹p\'}Õ\"®Ì{XÐ!Ùw\0…tkðÁ´í¾õö\0kØ2ÀEÛþ,¤ð¨,Sž5Öq@¯Öé¶¸-ŠŠ¦>£‡\n±¼WP(’à\0\0ÿôIDATÂ‰Â¬2êy>9%ö0éáxÔ#8ë9{‘Àe8Õ«%ðŽ	ò¯xþfk×cµ\ZpÁ^³Wö§»Ãµê²ì!O!ibqnþ\r¬qÍæ°ËC\Z	ä0Ž3B´E”?X³zÜj\"5–+£€åŽÈ‰¸ìt×w¢FÔ¬\n¾ãKó±ý.ÄG¢Î…DnggÅ¦\0Ú¯Â\\À~§ØìÇg:/ÿÏ»WÝÀ”úîH|ÚäŠÜÅŒ\\¤?.HÉÝK!­æëõÅaÞ{xQ¬ä£éƒëxÎPO\\H¶ZnÐ%|ŒÞ*Äšt`ª¶Ü(Ò¾~½{PÂ°ÿvîüË¼@£¡x­ï;R¤QZ<§s(áè˜i—‡GÉú—‹È‡£¯Q°Qt…L{›Vv$I’a£ðZBN;z}¯&EÏ$jF2\'zSç¶Š;¢ºÎôT·L”3ÞfÊªª¢ÜLÅÑ\n¶¶c€X<˜¯ŸwªV\n1E–sTþdqû73í·37q„Kp$¨]¹«T‡A0Õ §MUÒwBØ[é>óŒ_I•þýýûÔ\'?«f3¡(” fÔ38ò®í´E„ú´—È7Œy£\n+¥=ßW„ƒvÿ©‹OÑ°¿¬-ŠZÔ·]ñÓŸüÌ‹[ïrúi¿¹óŽi<Ô<²s#Ufp9¡^]½)=|6½-iÒi‡Ù|§1F´µD\"² óú}‚Býœ;¦ÈócÛã»Ó©šÎ:íqVÍá:šaëW!]4d×ÃÄ¯;‹ÊÂ—©ò0=\\‡wÏSçoµ–‚Qˆß}^Çðšâ§.ö‡-Þ2¸º`ËÏÇ@ô¡¤WCy!\ZÝŒšéKUéu©˜ lvú<èGó[¿F?¾Wÿç\nìVªË07=i\0Á˜`pvÄÓMÛéWþ1X!–Ð“ôœËBÒ#ÆÈaèß˜árB[âr\nƒ‰Àíw°/ã»¦aìãKoÚä.0	²Ù(åbGxNyà¦©à3)lî;ÝNÀ\'ÄÁÕ`¢•Âè2(G¥.¢!ãE\Z¿æ`‡•êäÉ5ë­ë@jòiÎ8×\'.oÁè©\0ÝÅ›C›R‰Ì-.uL7*%¨Õ7ûøòbP´A{^<¤×l¾O¤¯l½@ÂÈÌ·õñÅ¾a@h}Âa•gË»uTÑ—”Ê+478e¦®:ˆ>H«½KÆ„\'$íR¡p®d6uÕ½¸Žcöj1W¦âcÞ1<³ÀaÆIx$?®“ú¡/>Êß•Ÿª±„B+à=#~÷³\"ÌäÅ±d-mkz\r&ðBJ7`Å«ðá(`ªÖq„O¨¦Ž\npÚdƒBËÔÕêM´‹\'ê›â0·_yÅÕŸ=úó@ãI »¸@Ø”#u<›ên`a¥H¨¸?OBÈHåÏÉ\'–¼zøáH®ECÕZè®{)X2`äŒÓñ¾÷~àU¯|µõ²=?æžÓ»_Ý3/a£J/¤H³µ%¬aÇeïxe*åõõàîË˜«CŸ´ÇŸ4¯‰õ({Øžº¢.7&¨q€ÞØº(\rÈt.µ+Ià³Zý™¶ù„#6?5Itz¥>Ú 8Åu E,®\nlîn¿/ü}jƒ ÍÝíXðâîÛé½¦oÐJŸé\"náúã\"† ºŸ®\"\0P¿BÞO0^¸åä’{\0/ÒYEêTiZ~˜?óERB6JåK¥œÞšíàôÛZ0Å—\"ŸtXÞ%VpÓHE³£C¬@óÉªÙ„¼¾W&¶<iGµGO¾Þ]¯Ñ:¡ÍMÔF¤®©™ðMQøâOn\\ó†5÷—Ó›£ÂF,n]Î£h{-èÖM²Ä¹FM.àÏV¿hü-Õr–oF²&«ðØäŒ½(Gˆø˜ÿä¾Þ×ºT“›¹ ×‡›NôÝFCÝQL¥[C°F”Ü\Z+?˜<L5¤‹bÐIÊ9\rŸÞ6•–ffóÕÙG¾s-J¨Ñ=¼F	\rAhKýê©(›gnN’µG“l¦ÂD~BÀD¡Wj)Ý`ï2ó¬%}YÓF–Vuášƒå§»Uû—¢êªj4e®vþƒ\'VÐ3v®	á‹@ºükÍ-Cø„ß…–RBvt®Vdk°˜$á#Ëš‹0„Þwp©¨%0Ï‹JÍ‚é·g+yg=‡í¬Ðr4P¢gð‘tJÁÍšóöÂP(¦»¸\0‘H½Ÿø\"”¬pÂ7¾ýÍ¾}í5SV¹ÛÍ‡wT Z°Tò]mR”ÐÓŽtú&(¡ICÌ\rß»îZC\"„“O	ÕÕG•´ð\"9¤Þ´PF†$° €R·‘ÐÓÒ;)\raÐ{¨%(Š¢ïµ\"Mµ¡ÄHk)¥=\"´\nR‹Ò8ËŸ|s&˜K	\r»¦ïÕÏéNG\0:gþ¾îªëóðš8ÝÃø©S*Eõ¨Ž÷0®Öä\">=˜¹ XÑE¦Wx¤ÂDš»ÇÖ+*µövžÇ‰Œ?ùÓ§könÇ,*nè\0:S+O<ü]¸åDçq² J‡¬÷æ­çjÄáÑÂK9…KQ†ÀÁ¾\05%Øõä=[1®Aö4…m×´?8s0Êlü¶ï•ö–5%äâfÎŠ´œ‚£sZ8€‚=„Ãß‚M$œi	\nt5]=\'©ù¸[‹5å¥x	Š)†! %¡\0Ž±ßó¸N£Ê|²pM7úa-	P\nP†Ã{iJlÇÚó„Ì‚Qa•æ·ù^ûÊ³£5-êâuß½»Çûj‘è6÷+\'t²rÌšSï®&Eè8Cv>Qº[g_ê%J€ãR\rÂOÝ=ñjntNæšÒVñ[cÐôÄ8Lgd{5ô\nûÚt±ÂA½&*uM\\[MFUš.²Ó•íáÛé›EÒF_˜„†CwÎ5ç5½¸pØÄ\r%þ[ÏKˆc£Ù™ o®19hˆ<È`þË`i°ˆ£\0±Ì°	¢\'³}Û64€”\nøNu˜0ô YzH§12ú?¡/j~“U	¼àA1ée3Ù2Xý®i4&Â5.A8Ëðà¾qbE´éÄŽµ&<‰™ºË¨ï˜Xm¥º¬ª,ªv*b»õCó¦5¹Åpª\"Ñ$>iêÎu3êÞ7 .Ì†óp$[´Ï1õáÑyrœ!l«âˆ´°¿€²Îˆ‘+õr.­Ò{[#eô½¢Y^6ýÊ‘ m\nÝ€näDj…7ØS]¿¦Ñ%ì‡GõáÌ´º´‹ëÍmÀ\0lv)¢êÊ³Í°¯µße³ GòÐaÕöÕçq¿¢:D½é¥n•ùoÃ4ÄBåtÂð“‹¸¦ÈÃds,Ï|ºK&*™;Ô6Ó\'3­`Ñ0f#€œÚjÆo°’ƒO¤|ä|„à]gˆ¨NîRº\0\nsÅ\0.yBØæÚÔFþè¬Y»fKijV-BÎ(¤åÒà²F¯	¼â1.”>Ø°ä&WH€1Ícç>wîËH>\'áÀ±H­ó\"ü¾\Z¥rä ¢ñÝ—i†‚¹£[ˆ“€QÖYìt\0;’Î F—íÉkß¡„Þ‹ÿ„ÞûÞ÷«|)y©ë¡„qÅ@¢iæ’å[¬C<˜uö`	ÜRÂ|/ól¬)§r‘™Ae€5‡¦dK9Þ†ƒ)$eôÀwüÁ{ð²)R]ƒÜ\0c<–¬r³mk_\"J9³62q´Þ@3¯Tí—Îè¶€r+út« @=ð*$’ë0œ²¶?{,¶¼eÂ\"‚éÓ4G÷Þ‡8•„XX ñ\ZT\\« ÛÛYr\'âw›Üü2hû˜ûw%C](U«ÆéÎ+.¿J,Ê±|ÏŽi2>Xle	ÜPÆ»PU˜\Z:Ád¦5-ÕåRl.—àD·ðx7\\ãW¾|¼¨”Áçœ}þm·šEãfè„Œ\\¿ÙkóHn»ucú@-\\Ð¥á,ãH÷Î2²5;…?³gë¢!¥dMˆ2é?\rT©ÞCç‡}à†Þ4«ç¹—â˜¬?;óLËã1Xô(úMxlHš›L­=›#Å\r4\n¥×1à‹³œÛ…‰¹¦Ã¨;¬¤ÖY£ DPÂ¹ÍNá,üá…³kó$„\0ÅãRÃÚ6¸_YŽúcÐk³h!%Ä¦ãš¾P\'/Êbúm…ö_`ºŒ]¢ïØòÑzxÁQ9\Z’¸…Ky;Wó¾,8õõä1Û“ø^=\0ˆ¬nxPW„Ña>k÷™#ˆBà!kM\0ß\ZuYBu<¼z*—bZrÎl˜©U<zTÅ©¸æNÆ–1šwßí¡<d©(Í¡–h…³6Xr-‰È0Ã6]ê]D&1ïîÊîÛÂ2éöØGQ…Ki&·cÊÄ^ŠL@„vWŒD›û«E¥\\Êx­è¯|åÆ©@©Æ¦PA1äå‘€Å”àíÚÜ\'	ßë#Ô’÷0ô\\¿kl?Ñ^;.Äµ„fƒ·!žNÄËô²„£yrR+ÔdNÉ‰›ÕØ#i÷uÍÑ\"ždÌc1LžªôòxSo Q‚)y†mºÐ\'X/—0–dår¡°IÂÓÝr÷CÛ‚]Çˆû\n\0.…þ§èÞöí^ü&49YÐB²cTR‹\0‘tˆæ§?ýù\'?q´drC¦½á¬Æt*rµ¥‘L8šÞº¡ËÍ!ž6HÞ€a\n.Òl…°\néDóBÜó’Ó…ƒ®½æódˆ¤I˜K\rô¯ªçšÁ“_â@†XPâS‡g>@\n`(TÜVŸfTVƒ”Xâ@à6„53“’ÁY—ÒC2=Ø Ü…> ‰=z;¿B8F½‹àçêüÁ\ZHíÇ´Ôß	\'@ÜãSÑoÌ{¡*±eÒjY·Ó¬Õ»8WGª6È~°&€”Þ’ÓÖñ\\§HEü×ûv\"«Ü;W§Ê6_†ãÞeð(ô·\04ñ(}S‹éúní\"n·ìjn]Gc¼MtS\r~˜Eœá‚3Èh,ìfâp˜\"r‡¹‚c<¹ç‡Ë\"ôS2ô-1:8>	­ÐÊ´Œgv!»oN‰/\rˆsgi\\Ä¬½Þ#yÝ*óVóy<ájšÀ`´ÂOhéxŠá9]¬3QQˆ›º –Â`4÷Tt)z‚\r7[bÉ[88³ …ñ%2H˜5±×ñHð®é/S}nŽÒ&+õ„,±“¶•º«sÍ41‡©ÕLi\'0 _Wqž©º	J]UkÆIùÍž¼¤‘æÐ}B‰½&@	,pÉõšŒúaŽj–\\¬œBÎuÐûD“R½>È«Fí®Óyag–Ü\rH™\rËüç{5‹¦ÄŽ{þïo–É«\\Dë¶!q˜ï»÷Ïˆæ½„êñ³ÐgôDÔÂ;Ö¢=^€¡B€ 7¯Ãdñp½?…¨·ÐÅƒ:æÞzËm—\\|¹²\"ù		d±\ZUž²\ZR8ÀŒu8žO \r”5®§B	RPÛx·†{0OjWØ]ó9’~{\ZÔöÅc¿Â\rrS®ÙlJBD&ÓTî;[OaÜ”K˜Ò;\'7|à=ÐÍXs^?+/¨jöž€nù3”d!ê\n9\Z[Pì2\0\r%ÉM·q\0“<xªÞõ®kqÔØ†Zš–av\n‚¡vÃ\'z(9ÖÌh-°l€£ÐvvâäéÌL`6”iq;`\n +ú!Ñ]›&÷×Ö =­kú³ä›\r¹¼ÎI?ûüÅQ˜u…¤4lO×LVé¨™½éjžÊ,Ç².î‘\\–Äj‹…Ó•ýJ†~ô£…ïÓX§G„x)	ï	Ö¡;æ˜©ðüÎ;Á®§Õ®ïç\ZôøÙ\Z™¼‡/×Áƒ%™P5‹Û&€5®	†À‡fµò\Zn_¢p\np:RJªBm;&Ø¢)i…çÔ¸ÐMµ,9Ñ\0S‰J	ß©„ÀÚóØ]#R·vmú\rW¨ö”zM¬«àÊ¼ÝË,,ÉŸp¤&£ÛLéäÜ1£uzëq›äyeJO7@šä3˜Äd*UjºP+žó¨G=*ß‹œ¯¸ÂbŽ}ñ/Wt®«ÎÆ¢Þ13È6âÉbÝ·o¬]Íoƒ˜°Œ${˜~FZ4Ÿ´=aí©ð%gékÄ’î)·Ûu†WÄãáw’	T¤Ôj\n±,`%ó6Ta¹¸ä u„@µK!£ô3AíÄŠ	Ùfaé‹O~¡ûY¨8á½fXÚ †?—ðÄRJÈ v²+^wíõ€Õêc²µ+êº2º#\r€	ÄÙŸú_Ïà[x2å\"Q_¼L1Öì\\ê˜È=ýÓ%X(ø-{Ü„x\'Eø®,©{ËÍ·ŠMÙ¯í	BžY\\%Ì7¼ãŽ”?Iüâ\'—\ZLÖ¨´CÏÙ`éþ¹…?åj\\äÊ+§„’Í.™®/Ã—½°@m™bé@ÿÿéô´­#ÃŽð®NBDlô™7 \'@^½ˆ::¸¨%hÐu]5¿iœÇÖW[ÇÔóÐÑN+ÙéÈ…Gô•±ï\'ª\\î‘fãÆœ§…î°t¶\'\'Ã¥\Zìf§[x×/TåZ®\0 VÜÑ@ó]¾XÛÇšÏt»ýæ[N8þkïxë¡–{Û~»JCü,2\nNî°œÅA\r\\·|´Å˜îPÍ„	ìQs<žyc¼>i×¯Ò½e›Ãtx³ùãÎ¾3YX‹GêÓøíï<•¥&nºáFKY€è×gþÂ“øgºù+ÐMõ7ÞäQ­0qðÞÀ*µiŸÉ\'›·¾]sû³Ñˆ\Z¦4ÎÃc³\0R‰&wŽ\\!U=%ÂFÿî¢Fs3½=0~ª	ü	hx: ††Sˆ¤züŽ—ËÕâîîš¹DÄÅD°‡«áW;õVO…Ãô”–óLÈŠV^9ÏÙó­œÔ\nk‚èáH}à™‹3‡§¯Ð™Ûq\ZP\ZÂ¯«\"ˆ{ä¦Ü ´W¹r¼µl«\'î¾y¶l|AiIÆ5“]o%·8*9LÛñSÕz\rè©‹iôîÉóxGÜ\\/&U]U‹8Xç7ë\0i,dI£\rÇdaø,ì™.mY–Ñ-Y5Ê¡ÌøŽÏ:Lònqzy™—0°pó‚ó/Râ*Ã$Ð`K‰~ä|ß½÷_}Õuêô-×LM=\rÑQõTÍVìŒ¤(Y¹G{x©¤ð›_ÿ¾¹¯¹;ÐdcžÑßüúw²Ê&ñ˜=ÿp_µOÓ£æ-æ %C$$\\$×¡vqt–y	%«GšÄ¼(n¡½YUôÕÓÖÙ´…ÊnucÄ§ULuÎ5\raÇOébìt€Î@Ût\0ª_ð=fu_:Ó*6»B”WÃaìƒ\0@ÉË¦¾\'sÐ$½úve0tBé«{Á‹|—r¤ðE$T\nÁƒ6µ¯ŽáDïâR9%Á\\*ëx{´¯\'gÆ¶\0@ ’Õ¼p»“êÞ|Ü÷¼ý·~í+_…û(!ªxà¾ûƒ?w¢._5ÿØàæ¦2ó§pP\'o§àc¡IoäzÂ…”@>d¥ÔÄ{“öü¿³ío{»%H=Ãí·ÜÚCZkÈóxNŸã÷¿þeéŽüð§üì$÷ƒï|×l5Õp¡d] _aüäÏl…¦¨s	#iÑ§’%„9ä†¹5V¸Y˜Kl!Î]|BÝJèô@./E\rªÓ^š¸ô€ÍÁøÀ¹BËÓ¢Š¢UžÁ¹©5µqÒÜé©n.‹OÊï<³ ¤ë4/ÞÂ&¶?g.{Å+7†?}â`ŒÅ|vÍX\'¥D©º—Å…(AC³Í=¿&ÆsJ¨™DZ©{n‚œâ\"º\0JpÙ¼L\\±‚c(¿.ÌnÐ\n°ÞC2Å¨_ÕeîNn(A›f¬8…ô<°+ë^Ù˜k: fÕ:|bÌ2ƒ6Ô8y£&u\ny²‡ÚI>î…)‚÷ÓˆÊÖd“Ðo_PZê%@C@‰\0Š\Z1´…nJ8/c‘Â2Ž7‚Al+˜ýM¬éE/|©¹¹<\"à`*–K	òA”@tÊA¸r)ŸÆ¾™HÃ|¢c½¦:z52úLÛ-ïìOÙ‚ù‚Ÿ¾73¶ú\\³!mÿìœv¢§]ñ.¹Ÿ%«b’RFB/xÁ› 9ÅØÚU—ËŽX¸Õù¦`‚«ÞôÂõ« `ž‚>Ñx{%Þ5­B¥Â8·Ð{=L\0D¶¬•f¶©ä_/Êvã±ºM¥¾@„•™)WIÙþ\0*¡ŸF5ëä@DÓ8‹¦–“ðg“çøô½ÑX%W#Ë\07¢’¸fø€ŒL¡–Õ”\0páþé??åmo9äG}ÔÊÏùÖˆ¾pI´4ô´óöÉ\nhH€	ibÞ7Žjqwà;ñ²Aê\nA³2îÄX2åÐ˜ëàñë¯wüõ×^gÑ!w‡øî{óõ–[ºÉ3ðü™gpæ©§ýñüì?æ3GôÚ×¶\ZÁèÃahBˆFGÕj¡•üê\'æpÔw‚¡ðÑñQ>´‚¾t°×7¬–€@cÑ‡rËl|’—X„>†Î¨]`VkµBU­Œ\'ÜŽvåK¹)XwºK¹¯Æõ\",º1ò¿ÛrOÞbm´ˆ’è+(!‹0‘:éõ€2oÀ-ÜÑ´¥‰˜\\<;ÚÁ#¶µ©â`î©4¼|	tÖˆ•unŽ<¶ž¯é­øaS‡õ¨¯/>£/{lræLh&\næø„ãÖLmÔè«…:éÅÄNK)˜~­Cé’~ÊaÒ\r\r9\"aûµ&±4ðÍOZ¶\"±0aXiQ\Z=hk´ý_ÿõ_hµw™·f”0\rÚ]^^‰~rÏ@äÇj‘f—c³¯¶”Se˜«e×Ú˜ÄÜñ2/~ñK\ZÈ®µ\n’Ö\rzJ-ÇP¥„B@„8ÍÆ<+mÂ\n(AÅQiÀeà.FtÜW¿.µ`jk!H\nŠš²;\'`;ðð&\ZB	N|VÈh>³ÐK(väHÎÁÎPØuQ?ñ‰OÔ¯4¶^Gµ®ð·—Z¬ ü…D”ž¢ðú5se\')îhã(sP‚9§0—ùÀ½@$¤”]¿M_=\0,è…2©h\'%ÓÕé\"K²“6†`ˆ¥Çú°f<’<»²bóæ(tAºë59¾Sz×wA0Ñd×ulò6û&d›´Âš>ß4vºn¯¶ŒìG’p¤B|šw½ííï~û;¬}ßÝ;`.ä…È”³ÐÁRòwñf¦ô¦ÐÍ\'xò…ýËMàÎ¥+p¿è§|ÌcãeõLíjøÀu~óË_ÝrãM7ßp£ÇsÓ+.¹Ô¤<˜ßþê×—_z™\'l¿gã¾X¯ôÈþæYáÍPøìÖ$’Ž†hˆW ®%Yü	A6X|@žYo¯ÅO\r<*©¶¬±VÃZì0JRE²ïÔUûê€:£ï¬Ú—MCµÜQÓT2ïÊî+D?ãŒbú®FIÜ®Õ=3J²Z<*=q)·C4­)T{ñÝ·^ßV&’ÂA7*9Ñ§–b\0Q]ÏI½]n\nÅ£Z>Ñ›fBÿèQ¸sâËÜt…Œ­µ¶”Ó¼JŽC™væø¬ÉŠJUÔ ËàÏ×©¶>áë­‚ëfÚ¯uðáhP¯I°ú ½Žãu+~‰6Õ¡¨ŸR7ªë­5¨ÄÉSŸúTÍQJÚž9ø ?se´É ùáí9ÌÓz¯CLð¾%ä\"8²Ã\\^BQ£Ò¹Ëˆ‚_qùÕÖ0°ú˜\n\0m˜˜\\Ñ S‚€ƒžu¿Þ–g áý”û©9e\råÇ†%«Yâ%ð	\\J¶@a«¥+/¼à7Zø¬³=ÐÇ9…Œ%à	%” ñ0ªNË(ìIÅÑÌÕ˜Þ¢÷5LÏdvŒÇ¦Ò…øeÖšW/¬ÐÈ”P}‹->§=ì)}²ùuGCæ1dBã1£@ïÒmÀSË\"†ûöW°_0Äö>üEÝÎ‡ÙCÂú*!ðÅF):šMõ1\Zj‡ð‘R–—n]FÔZÓÍM=ž·@“`¥©Ê†ËÂbmJé5=h+>^p!^°¸¡jáøK/úãüÐ_÷úßüâ—(¡hÒÍ¿s[‘»p³°Òló\0\"$°£C<³¾ÍuEõT¾ÇÃžÚ¸Pzÿðÿà­ñ=:UMli9>=Û—>ÿ…·¾éÍxÏ{9?úÞ÷ít€\'àþ¸5‡¼ñMoÛÛ’U†|ÆãxëA		S;BÕ&úp¢™Ð­Ö÷Ý1ó$y ˜Õ©±üê^âÍ ÛÄwÐ€i\\ÜÕh£ˆ$Ò”\Zýhv¶40÷#jý°‡vÑúO+jú(Á-X¯€Œ´!UQåÝªC[Æýi8€sº¿¦-ÊuÈrGšã9ËŒê†Bš^AŠHãêBO\\+Êæ:zVSs»ÅZdÐÁqÕî^‚ˆ½»_‡µU´Ná°~t“^ÍÃ·æZ}ÁÝDã6V‘’-?Àa.¢ÛV˜Gß4\n5à±¨\\GÛýó?ÿ³ñžD4T75ŽÂ³\'†3šÃ¨‡Ó‹þ´m†ÂPÓEÀw)´aË(Á)(D¬ÆP\0á£æ%å^ðÅšëŠ]è«^Øh6šž@Œ4À³‚<ØZÜ%=q„­ÜÝ¥šéèì?œ÷ÃüÄ¸e| é]Vy0A¬àI9È%Ý`ÜˆŽ©ò‘¥ê¯ÐÒŸðŸ/zá‹šŽÊsÒ{MÕø:½Œ¨WiýÍ‹—HhàI¬1¤7q~&ö\'é5ŽÁ}	Š‘é‘2,…FÐ°’)+‡\ZF‰ø‚X(4Æuò…}6›ˆØÅ\réËÅ‘ðÈ)Í;†` I¥)Ï]Ð\0wÓ–«N¿÷ò\0úñ“î*\"__½V€Åv^Ü-f1œDlí/óù×½ú5\0ÚÞ·ãûm¸ç^A›®¹ÖÎ)Á0[&ÈF\ZÞ…7\0/zF±AB	p*Û¼Ÿz	@Së˜ŸÝŠ ›¾ëO]ý¼óï½{‡gÀI9W]vù/O?ãÔ“N–jþÅi§c)l1=ùÛä^ùÒ—}äÈ##ïÚ(è¢š;%Ñ{Ý‘™9&?w|Èbg‹s\rÈÖ|åiP—F)~è:\ZÚ»Ë@\0zàRi)ëœ	sP.Ú¦QUø¸ˆ\'©~1ÏSØÄZÇ=ÓªÝ3ée‰ûN\rÜË`\"ô¡cÄÕ ¹e|0öÇÐMáà\nZ0¥M=¼”kº»Ã<’wÑô¾~õ0ú`S¡p€©&0l)ÊQ\r•xwÙVP…#Ç+³Ùµû¨8ªKö<Þ®[x`¯Ðcƒ8nM#@C†ˆ{`Â\'s¯PWê\0Õ2`Ð‰ú¦0ìææ¡ª p„ÈX*cIÔÈ>³Ížt¬’nâj&oÏxòžt]/!L”˜-1\Z+À]=â·\"ó=ö1ÿa®:¾ÅÑŒC¦ÍÂÞÓ`9ý3åƒ,°¦Iá,ó¬Ìô¥—\\Nœnþ8a(á#ßÿpÖ¹†)pYä“…³d\nUéTœ\'ºŠx¼\nÇÏSBY/R:z!½åmä*uËXüÃßÿâÅØºñXÇ:ƒä¾¾·Œ2¦j3ß«`!ÝXÆíY=x;ƒyJ +J¯‹:Ò}ÙþqÉù®?S\Z¦JÒ®ë6À¸\\EaêWMF¦iè(v!ñßýÝßqzhzpL“ùµW\0µ´6‡¯çt\0Á4kš{Juú’6?j!*î.Ú=ghÞXÞ8f†?o cü»ß:ñ _->sÓõ7\\zñ%_p!JpÀ—¿pì>û9ÑGžý‡?”ÿðlÞ=l•yÏªeiR3ðZµÕ2>ÈøuMœuìE\0–Þ8Î_}ÍÍ7Þô‡ßýþg?ú±ˆè¿W~ûÎm÷îàƒo÷HbG	SÞ{ÛvýÂç>ïKÇ~1ú¬-zSßuUÿžJ±\rÇ,i™ê\0\nÞÈOº7k4L´‘d‹#éJAR …ï=­0c	ÑS >íò«¬ü\nÑ²	47‹\r.óê“;¦‡.h­«>¸w	d]Fée°H÷À2;“óBVvýTCh\Z½I¿`>\'ŸA¡^íBâã€Âk—/z„þèñlZŸxµ¦ì»	JH½w©8ªlˆ«q¢ižW5­>N2K QèÜÕâÙÐÃK\\åß r2/›…ítO¡º¾3P(¡¦ÉSÁ+¥Ðãøž¡+è ÁR|“´#û6~ÎbJõ9»iÚ#\0$,¦=ÍN‘1Þ8¬˜ÅaúÓP\0ãËþó)Oó›U·Ú$ÄbøxVÝ\rbHAùÊ…‚˜EGušG4&¶“\0¹ïÞ?‰;)ÿ—Rv•QÏ@Æ¢õ,Çë÷T½‹(–Gu0—%td®OEG+è­+DŠ¾´ÒOÅX„ø‡ü·û7¸É4óÌì,	}u\ZºR\".<‹£˜£/\0šfè3\r¿lÛ¥çÔÆm©`!^Bë°)\\„®ø:xœîH†‰ŽÑBµi¸8hT\ZÍsnñáù©è\"c\ZsÃaâ-ÁèéšZ\rC„³ãÁtE\\¥átfwSµ€<ÊuôvO¹Øq¢š;•]ˆíÌKPÌSÂV•ç;}ÛaïxççœûéâEÏ{¾rÏûï»ÿ¨ñ´§üçoù+lFVØ2ô:‰žFV ¬ÄšÛ±^cÜÈÃYYÈåËúsì¨ONcïÅ¦o¾åèO~êµ¯:PÁ}¹—_|	V˜’Û¶þþ<óiO?ç÷g¡‡O}ìão~ÃÁ¿<óËZYÓÔ:ó­A3k=\\óÅ“p\0DI‚P2t†å,GÖè~ÕR\Zäí)dßÌšÞ¨IJ´~Ïà.e’­.¥ÝKåÆƒñó´]M\rCÇ£êOÓ»»¾ŒWß¥ºò<vb70ÊEQ\n˜²0:‹ÆÒ(¿²÷½5¨u_wù§ú\'úét4)BÈn²ÒLo‡•[NK—m=Éî›»WA«,²kÞ²Sz£B^LWÒ(MEÎ^Š(H©„VÄFûä¶²Àˆ¼ØQ¶K2}<*-\"í;æ+&ìeÏ¢=ïŽr2Âj÷Š\Z¦®ç\Z÷Âóx¶L´¨h¦ùK¼„e”O LÍþ¯àgi™Ìp(Ùln7­jà×ŒzÓÀñŒ[fS›±î²K¯T9Žñ¼üê¡=.ËÔ¤G\nÔ´ù(\"Q2f™“ï9ìýR.ˆ“D®¢idQ#¹âÕi”\\Ÿ#õÝƒ~ûŽP‚ñAþAÇ¯®ž\Z±£âKÈÉ5e³Oùù)°Ò†á´¨gÆh¬ÂäÜmT¡Íê*¾ç\'âÆôRz®¨OÏn)ß<X ÈË’¢\n.îFÕ€Ö\"›:–Ü€z”ýAEþÔáô=XÓp6)ëLŸoZ=ýyÌ\na9@œY=\\»à°‘ÚÊÆqGOÂ)©[FN>S\nGPŽÃ|:Æ‹—í½ðõw°ff¬\0sg.:ï|é„7¼ö ñ™¯ûE4 éž÷üøû?€¿JÛïÜöÇ‹þ(\\ÀFQn­ü‰éU¦k®Iìë	CÕed0°Ìkze!Lò»îBK€Þ¿ßýê×HëäŸüôý‡½ÇƒÝqëm—]r©\0×/ÅÕ—_qí•WÉŠË1œ{ö4Nu÷-	”	ÑÜKÓ´\0rx<d+ßiô¬ðZ¶až\r\në—-Øí€&wñSw4Z¿ÀQ;cšt&pvŒýÞQAddœ‚h£>Û|…Îe:°xªuYH	ah^K å:¸²Ñ\"g¹‡²\Z6LÌÔÙ1“e…ù\rÄE9½N/ÒÖ[ŒèÓBãc%x¶4™ÍÚ\0À†Ug/d‘<-?á6Íç™c­£3òÀ…¾ÐX±`m„$e[uú¬ýÕ†©¤2Húâ•sNæ^ÈF©0\ZþF6ÅôxŽ!cø=³›*Ž`G&ä3óºÖôZŠ%2fžYbàN#‡‘k00p¬@“Ð¢fc„\Z2À\"–„6|N ßX3o^¬²hDoš\\+èÃ´?Ç1P¾tÅ¬×_;þ›† œvê™¼Ä\0£ÝT H¼HPÑ¡/\Z.Bq?ó1Â™¿bö‹È ¡g%V¡ÎrË…ªIƒÛ1-Â|×dì§4\ZoÓHªã;%€ªÌI=Ä‹ e*-Çþbâk€”å*)J;Ë( Òò¼q©GÅë)\r,(ƒW¯Î-hu”ÊæüzZ–åöÌ.¬ì0VuANNÄ^ìM{Ìþb]zT°®á´‘¶sßÆý„í¾E¢]ÍÕ‚€š¸T¤Ã ²A9xª_-{}FweEXAFÎ@W\\þ[_ÿÆ¯Î8“— ˆd?äµ\"«û4_(iÈBÁ#pÆ`êvµWO”Ä_Ö=^ôvpÁLQ”Ûï0íÏy®¢XIŽë®ºšÇð¬§ÿ·D‚§=ûw¿)ò<\\™çW¿â•9üÃ¦±]öš5Ü¸€ 74\r1èxÂ^Ï×ç5!×G&wjûö$¯¹kå^Ó÷ŒAïÛHr_Ø$Ê¤õLŽ{á¦4ÍE¨ïKÓÌíðÚ­‹¡;s¸‚@	Ö½ 3Ë\Z‚çMFÿm¯=¤ëÄ+©·ûÂ2%ô\'õÛÊR)+rÐ¼Šº•Ÿ@ŠŽà\\È/VÅV—o_Èó4¹;Ä÷«g¸Œw=B×ögd¼’­Ÿ¨ÅkútOÂÏ`üéGM¥!bŽü$oªùM÷Q²˜df38$¥üÿø(AÇº­ç)HèÅÉÇj…&Mq¼/üBãoÀKFÃ8\nNñxl­¼6%ÃD…ÿ†D£S‰æ›|T(	È÷¾ç°È<ûn¹ ªP8Rä#Ö‹MbÊ¢ÇÐPàˆ®PëÔ® üTA[B÷ª>5äI?;Åàä?^t©ô²	®››— ÂÕ½ÆPä\nOçóƒ\Zhf®¹¾B ~PJè‚cp†ã	$bÐ=S2Ï	[)ŽäàdðAY‰^×÷Ô½ŽwÔ~¬ÀJË…PÒ=¡„X|C´:ÝªöŸ‰‡P=C“ÌÔ»H¸’©op€¯ïT-êmAóº¨=-R¦9:€w)ÞÒOMÅ3zBZe†¶ït‘ÙP#G:žË\\¢ÄÁMA$[À0Ä–Pâ”*\0¬\rMà1À\\Ö·<3¨5˜ù=ï|WÃ˜!òËx1Sª¹ûÔc¸uµ1hhU\'ô^J	=R]×£BgÔ®¡§Æ.¿‚í‡~òƒº»Áü\0y®‡FáÙÿýŒÏ|â“˜ìÂsÏ{ù‹_b0³	ÊWSB­PCP	q¢®˜<}Ñvš•æ4æ€>Øé5½/[¡a1½c˜Rb6pp ¬Q­ \"I…²IõJŸm»éjàåI´¬¸G1ú gèÂüT†ŽÓU¬Æa\nct„â“ÑÞÂ-¿¶®ä;Ld#¢7OKÒ\r#r_”,ûE]“³OxçnA¯øpÄÒp°®–‡Ô[¯K		0mçOƒ`AŽ$¹ÂK 7uÇ–*É ³³q	˜6²á\ZiØõ‹a\"3.Z- j§³Ü«µÕ ;CG»» n.†ÖŒ[nå¥¾I›ØÉAz\ZÔæQ¸N!:²ò\n®Ù0Òº€ixÖË%TYdö:Ö:d—(6Û(«â«µÇ,xçŸw ÄÌpÕ8A°I¡6–xãÁ‡8Åèe\r&B\rkjû¨RGÕŠ(UÔr¥Mê×\\}=&¶22ƒžO¸ì1D¥š\"ˆÐ*:Ê®£Ž!¸òSGˆ­	*²ýGÝÑêaE–æµM¤²³žÞóë·¬!AúM=¦³ñ\0,ä®mä¢é7¢\ZŒIì ìË¬ÔaÔ+2SŽ²%°¾Ùúª[ç£ødšav½e1ù2:FV[ý<C’¢Œ@ÅÕ»zàarúR×®ñ€Wðšý¤?SßV¸ì±=\'ô!“NoS#T»¸Ë°R.!¡Àáç?ýÙÁ½Îì<YåW¼ä¥&´¸Ç=’\n÷ØÏÃK€wBþôÊÅ}R<ˆée‰(”¤æùl÷gTêíˆš~Â8R%áßýú7HÔH¶@bùŒSN)òTžVÉc<å	O<ñ\'x`ÕGR˜ëª%¾à€ª ™{À×*]µ`A‰(¼;\nHšÖjh(ÁÂ¬¸^»Èc€è$Œƒ Ö˜yÝèIÒ®”ÛÙøÄGÉÇ§æfå0q\ZY]ô¼0]òl¼ö} {ë:¾Ÿ\\\rîS’’u÷«×„³fs¡Þ¹(­å\'ý÷ëPµ¯úL>ó>Ê.Ü°ÐÞ¯kô<\0OËè†K·h²¡Ni§ÛÉH× ”úÅ‘½o2ô\'ô€àN“­’Nw‘Q«ª¯M8ã`­éX™¸ÄMåÏ\0©°{KK[Ž¶.”M8š©ÙwÜ1/Ð´@ëQBqy‘\"ƒ\0Œ36Ø\'Û?ûZ\rÐ·\\ÄTr*s Xæ6Óœ8ÖÇjùÕÓ„Ö”ë‰ÕþHÕöõ‡œ¯šÖ§„ã2Õ\"W&¸>íÔ_¸µô²À‘‡KÊ•0èiK!„øcöî…™Pe \ZËV%Òàe”0Ÿlp¼÷²§\n%ëáÄóÀ{óa)Vñ\nÙ2^³·àhèLSMJZÑ÷¦ ½õÂ­Ž¶f$:Ñq‰Ï€˜Š”ÖKb]‡ÇÍØ×Õë\0©`¿4lOá‚F¾³>àiÅénC+wJ};Hí©ìñ$Œñ‘aö+“û<*#íÑÏ©lA{¯eï>Ioç˜äi¦ŠÙ$gýæ·F«É0‹ÕœvòÏŸ÷¬g›1Bù)7BŽÁÈ\0éeÍ\Zj}GÝ	JÂ‘Æ6eX°˜`Wðqê˜õhPÑ^F™Œ½ç<ã™<)eOõãŽGHòÌ<]ðcÿoQ“êOñ%”pÒBÊ+ÞtˆŒZLÑYCÚñ®fesdÚ×‰ °ƒó\\*ó3¸ñ=!=¹¤0TOêÔÖ8{äu¢ín—¹0ÕqA]µñ.IÏ‘°¦šùª{ù‚•Y÷­Ž—E2`tPBGŽæèQu\"È%\n$,.XçvO{ÚÓÀY«ã!xè,<BD\Z…=$vD]! ËŒ©a+¡’jÛ…öœ¢[ïÂ/ïQWPB6.M+ìÑñž%4 š\0k‹šÒg6œW€M^˜nB¿Nùèe?+Œnd4Àš^[ÐRªNX“ÿå_þÅa\rcçå:çD¦–X\nµ›üo?ÌH1£´¸þqTvJØª1•$pŒˆÁzˆ	Ç-r`6$uAÔÁæ1e§ó„o\rPÈlI3<Y”ÀU\Z9,‰e•ùrJN%3J/»/¡	‹J”E(”4¦$ŠÀYAfMÐ´;%,+B‰‡‚EOÓêÝjr›ˆ|Q\r®a½?ìç©öŸ¢(5$\n¤¾Í‚²,úiˆÅwêÅ‘Ô™‰k(\nÈkÒždÐKMUŽÞëûdÌ>AFÇ·¿þ£»rÓ vß`h\"˜â0°ƒ\0à+24SÔíÑÎz¦«¹¬>Œ\Zuæ:gY¸™A¨1ÀÓlB7£ñÛ¯¼ìrÆ8GA*Jxê“ŸòÁ÷¾Ï¯Ò\"9‚û?þá\\:0õ\rê¦¤!|úÞ|õÉÞ=ùì¾\r)Å‚PXp×¢™£Ž8R†ÀL|£	\"IøSA”0—,÷¿?æ±ÿõ¤\'\"ùS>\\|IæC_öšƒ¤{ª€›kå³ß‡ü£^@Ùäµ=y´\rZõhôÐ|à»Ç.Zë‚Æ¤@0È]ÒÆÚ×gÖwšæaJûm>õ_z.CÓvjb®@“V¤‚8‰‹Ú3WÝ1J¬°‹ÌíÛïxAIÖ¿€{U‡Óp\n‘.Æe#œ½=çFÐ(F†\'¡oäPðªÏ^d1,Ã÷:ˆO=šO&8âÛS+ì¾õŽîÈ·Ó@­îÐ¦{òfÐ|9-[.BmâÙInAE5Óè›u´®i+Þë]Àº«ñœš8žs2\\Ù£zw †öØüZbÔYŒQÌÝ”Uà¨´ðîÿªïd•ƒ~V¶”€É±ÙþøÀ¢nüÀ‡:èugŸ}Žÿá-3iM]A!3Ä\nž\\hF£Ï~ö&æ\r7¨L\Zã\0ã>uŸè ©èþ¸ëw¿ýÃÞøg>ý9N•‡f5½Ó¢êÀ~Âza+Ô\"i!x…œÀ4f’¨à1 „1S)˜nÂê‘$eTâ<N€2ÈyÆXÓ2œÃX‘^.$UÊ¿ù1n^³ŽÚÄ[ˆšý‚´¢ÖZÅ:ŒÆ“0Ðª°}ë;žûœçŸ|Ò©æÐ6Çaï~ï§ŸiÒ$s~,þ\'Z²s\ZÚ“EÏPÕêzæÔ¢³Ð°L²~’’ÍÖ_½û?ø±uv­†i\n[\"Mìwš!B1çÿ¸×´GàÇãeéE0]‹£\rš©ÓŽ®£ôy0F˜Ÿ^6ãšf³z\Zœ8:dkêêùÔ«·[n¾eÐã\"ÜÁ«›”Æã}þ˜c^ôÂªýÎ‰ß~ô£õ’^|ÉÅûé[\'|SM†à‰ÛécŒ#7BÌ:\0†Ð%šw>_ªÑM€ËY$’$´o¸åî»î¹ìÒ+^ðü4ßå—]ùÛßœuà«^ýÇ¿?KPö¼sÏ32ùVC¼ôRKYmàïþ¿ÿïÿýß7\\Ã…\\pðëßpÄáþãEi†eÄ3 Ò—ú?¶°³`–#äm6Ó¼„ ¶9é\ZÐu†m¦CL’qŠïpS2Ó \'¦«Xß`Çð®?ûî”PÕgelX…>2I 	Ð0KÃ¾îK\Z(\0³2ü´.ï\"\r¯œÚxB7õv¢£¦1JF;â½Œó§áh¦°	«¨H=Öá7°´DHN?ýÌ}ðˆþó\Zì¸{‡¶£ù=œ–F,ÿ-Ÿ;8ö.,<<ßh|—xB=´	Ð†1çÉ	oH ŠÌâÚÎw=:Ö™÷ÈëkCt}q\nùx}dI¼<ˆlðåÕW_ûùcŽ}Û¡ï<çœóMÕü®w½ç­‡¼ý[ßüÎ>å©ÿþïOüï§?ëë_ÿ&\\°3K¸)¿\\¼\0çß,£€Ë¬6Ë›¥y¤—…ÿ¸\'šbš	îÆÉ0!)MB	DZ½iøaã€ªÄZ@³øLÕAEç+feï«&h\ZkÚ4‚¬Ò ‡I\\KH¸;2€ÑÖÇ0BÂSUó:êDý”½_­Q¡¡’\rràÆÙ	@-¤„\nþE«»0G~°ægd1HÁM3 V!€óÃÁfZ\'myùeWóTHÒ“‹§a5ÞÏ¯~ùÛ-y€Ù¦T!m°Uª„ì¯ë–€ª˜¯©Òë¾ÿ½7¢»\'Ï[Zøïî»6å»\rÂvý&ÃØ…²÷SPÕçô‚—_.FÔtÊ±…‡,1sÔ[ÈGHgR»Â2¸Ì­ÉŠôî(%hV>ñÊÊ0à\Zñ\"czh?Ç«L†N¨ˆ¹%h„¹‘œžC	+¡Éôî9§\0ÕÉÌ¼ÏÛÄ[OùùéÏyöÌÓea…ÏyÎsõùÂ²À‹4s5x…MÿçÿüîQßš=Ð÷eÎP(3@yPbŽú“$ìW¯æj~õ‚½±‡Ø©Á@œXˆkJ§;È6Š[–…ß“\"ÕU3ûn£TCPNÔ U\n¤\0îKÿÙÍ¸×Cú•%$ÒBD:Åà³ÕŒØ»lc÷tLƒäášn4\0_SÒDã\ZKOëX]ª3½Ô›Þtˆf²¤ÕÏO>mû¶f­$diß±jò%p}ŠÜFAð22h?%×ª\nf>/Áô…Ê–7%/VEP«\rŽ^¶ìF@êô¹`QÉ9wÃõ7I²êŸþÄd ×2‚a&“ZŒç_þùß 3Z>yF“Ø˜¥»û¿¥”P&VvWþ@åkÝ‚—D“%~Û4A4J`Èx7Ø§aDÃëK)62¢S—˜¬Ô)ï_ôPÅª9V-\'=¸û,ogì´ñn^²sä jK)äçÀ÷qåyJÀ%ãBO./a¤:~]Jà	epéœšG<ÄŸÒ;ô˜ÍªÞ}<†&>õ”3žÿ¼þõ_£©¾´SË?Y•s%¤@”`\nüÍ6çr34 [Ò	ÝrrðoŸˆ–pˆ¨9bGXlÙ]x	®_ ¨¸íBö”H0á^:mÞÉ`‚4ÛÁú?¿UÜ¬ÎïñØ‰-0Psƒoìì¬pjô]¾t/ŸQBðÂhÝßþíßBCê\rx\0Fb`eÐ9\0Cð¼…\Z!1IŽ¡Ÿz©\"+òÖå\"Ñ…ÿ([ãW(ÿÿûÿû{CÕSüó?=úiO}zÅ I/ÐŸÉ-·Ý\0FÞº°1‚T4âf+0q°~tEìÅLèU2ÏöÏ„.Qc¸ì]šýÞøƒÞ–xk,¤å\0dOPdâÉ+Ë<ïRó”àôîè\0§©S\ZgàH;)¶Ç`žwz›³tA¶ÂƒËˆ?î©‰;r2_wìÐ‚ˆ¼¥F‰†ƒ-è¶¯6ýþŸÿ‡¯àa„ÑhTéÜw½ë0K³ öõnÙˆ÷ß÷§ªÆ÷\Z%xj\0ý[^«i\Zx8Æ¦É5çÔ2qlÉöA)è}®(\\ªJSÄÊéav¯Ï’›ÒºbïÿýôgóòÏ\0v\n¨X‰’pLÝ¶˜F¢u—/³`ý2/}É+Å‹¤5@	\nŠ¡îÍ¢RZ‘žé¨˜¼Ùwn:Ce°6­>éÄ¬ãŠ Ð~µ–½Ïz Z†,_ÁŠüw>@—èÎ3ÂòÚ¦Î®Çë¥BÀyÐwG£¸BËpÎ\'ú¾.%ÀIo\r‰8èº®ëÝ¹·ì¯V ¬ô€X„–ógõ¯}ô£Ç9P ¥…ÌÆáî+ô\ZÔý‚ì6=“lõŠªu$šÄÜó0Ó‘·O“­m\\áp‰²VSBAvÆ¦lž>¦·{A0+ßuìÉsÙ	Ri¼ÇèÁœÎO*XéyèCëF… ¢‡µ—»à<\ZX_©a#»/Æ:„Ì\'h*S’s“\0û.~Â¶EiÞâïÿþïÉœ„}M/kzæ¢.J	ÇTK­>Âr]”ß\n¯ðâ&uhf=”n«*-ÏCŒ­¤H°#@¿ÄÚ(M Qˆ¯èÞFÀ1™cÜH#S´¹¤üJñ*{Ÿ|ñE”@n-š¤™˜¢’KÑüÌÀª!òÒ·ÁÙí·Ç°÷Í	K+Ð0’ÀÁ.^;f£@TÝ|eÃzXÁˆ)UÜ3\"\'ö°rÄC*aÒÅDÃ=yòÃd¨=îbBÉ³¿ÝÒ’æ@ÀD¼HÐ/s¯QÉPFáè\Z½¬È¿þ’4\"cŽ¬† ®£$VØ½;´§Ô‘Ê+ª^1‹>5Í(~ó­Â°ší.TÎc}	UiOB(!Z„†WqËÍ·-”ÌÒõª,’KÀÿþø\'©/b{ÂVeþt‘ÀÜÐ?µÕG\\­î¡‘þÐ¤]w“ºí/¨\n»~Ó”â#b‚(¡™\'\n0dŒEô¥‹QBƒæ†gÐ(ŠB%…Œ‚{×‘‘6Š¯4°®cæÇ-¯K	ô_›iÐuùCR¬Bæ!Cµ¦Õa4Ö$pÉÀ8ÂÛ`…$¥FUa®^BÝÌg†žOÖ+H»ùÓÛC-J¥\nšçƒ6gc³Ó€’\"‹má™—Ãñ6ÌÒÑD®.äÞÎ+èø3;(å–\'@	¸%8\07ä»hú0€ê½Õ¤ã7;táE	a–‡tqòd‡Ba&?Á‚‰fQ„,t8Åsç®i!Z3;fÌÑ”$#ªYÇ\\jÌ`å^bäoQ!ú†n_¯zÕð±T¼Èf‚Å­Dd+7zk¤\'âYƒ}ˆaR\'š£Ï¥IÎA	Âã}2ÈZ˜Þíð.•€õƒ>wñ\"w©ÛŠê@X–ÕÕd)Ò „|—êù{xŸHH.·¢¦ÒZ0Îs¶\"wgeòC@má	KðÖ|Ëè0w- ì»õ<Eùî­RóÙib<Ô*ö<E•`\\„ýìh‘ÝºhXa¯Q‚ÇÐÌ‚ˆ6žÃ¾éŽdRw¨Ýk¾&2jæÔ=Ùh\Z´õãtXªN½e+•þ?ùIO…Õ\\%$Þm™ò¬A.áôç(ßOPóÿþf™¼B+”‹d¾ü¥ã–±jþ 1Þqæï˜±¬ïQ8ßuŒ&ˆöÉe\Z(ÿ?Ÿ#Dn}^‚‹Wˆ÷7\0úwß\'8èõ0AeEN#\\5<ÂAiCWôàÏÔ³’NL“DVØÎ]yY.AHÃ{éöâÈž¯ÀSf¢rfy»¨T‘Hw+hSâCÞò6öýàû?6DÃ‚B\'|ãÄfó^¡¯y	©ˆ/‰:0³×#{Z#·U5ð.üB	ÂqD7ä°âMÙŽ4•jêá`H°¥1Õ]|á6H¢g£Ü\0Z·l-õüBšà:ÙãþDÍŒ\rV:&¤ëR·ñâ¡ÉÀÍÊ•3ÀÔÄ®›(bFn„&‚e•®{Çg·\"ŸÅ+ãEVÿšèò_}÷E¥ÃsŸ;-ÇbÉÓr™óCÝžJx¶†øyÎÜ¬\Zq&öjƒõ/€Ng˜\Z¡j”0QþÌO*IS<ÍÝšÖ…5kÛÐŸØÂ):&«…~êÂ¬Ë–áL»Úzàzn(aY¦È¬…;Hµ)ê¢1ûûÔ\"XGü ¯e5%\'Ò]\ZEÕËf\rLŒ={Í˜z’³7u$×¤	GácóÊ86˜+±\'6å]Á	Ö3¬•Kp)Ý“?Êk©Þ×oÚ[UÍ+4 /¢uïËxb²x‘fZ}PJðTJû£n$aCOødàßø¦Y`‹XáéO{ï6ËÚm‚þ‘EØìù›…á¤Ù%Øõ¡vÄ`’êë®½Q^APU—üt—Jµ¢´?ÙkÞßƒ\"=Ñ4o^œa…{¬Á\rž~É×‰¢TMÔ“ô”ƒÄÓQBàõ„ày*f ëÅïåôæQBP(¯Â‡2zÙ÷áXÄ@«Ya%èàZšÆcD\rƒ\ZƒVa¼k*2áÙi\'PÅQ\0RÈò›ßüÏŽ\0\rè“ž¹òŠkÊ\"¬ðjÛÔÂ—L0Ÿ0Ž}Ô²·$,2V$ç,c\Z.”\0¢ÄÉ ÉÕÄã²:÷Ì¬0†|óO´-ë3=RO¨¯²7™‡Ã¸kAÚàÌÎ‚!¼cQµy«p¿v!†Áá©#É<Ç%£R¯Ó£Ø­ “ÊÁûÁ–à>Éc÷\Z€uE	½—GZA	£/µzUñ…—IŸõÌggˆçj\\”À2`y_:?Ã¦‰Àí%ÃÕ”{0_8ßºzõ”»Ž€2tF	Þ=\0‘ô­eRB@ãÝÛLA(%¼8±\\×ð£„¼7\'&jB¢açú~Êò“S©Ç”\\–|\\?cO^?Ýö¦Áe²\n”Ó(r¨ ÞóDç¶$àÈfs³ V¸1ŸS,H²w¼OBP0°$ü0e@¢Ð%æNëzì^·qwŽh~ÅtgCyêŒeP`.Á¾F>û|ûÛßÉ^awªÇÚ)¬Bu³Uô%·i„dÖ£„{vÜ?[õ~ÊâVzùeW2&­ÿ›ßüVOà˜ëx¼¤4˜ÇAz˜uWS¾(Jð(­¥\\ó)Ê!°.âéFç÷5ì ìJ‚÷™É&g\"ƒä_¦Ü¼o1œŒvv€Àúaï~¿¤ë¼WÑd\0®BÌ^ç¼ìt½”ØF	¼2ƒELGÏ‰ã(@%&Õrà‰­âHq\ZèÁ`7yÎ“Ù¢gØX>%J O¡â32·\Zö/6ò“Ù»ýåL/,ØøŒÑ=–÷Š’!˜®Æƒg#ÂÃëŽ­¼ž§Þèë#†šÞ#±\"l±žÈX´Ã\0‹Õ”PÏ	›º]–lp.Ô\n…e£æVÆOMÁD	Ép<¼ïZa¡dÒ´´‹$ÓCí%G÷Ñ£>FÛ¡^sÀiz–w¤GuÇ£Æ\n³–mCÈLxN¢s‹ÖÀT‘4š\rWc ó+÷Î“ŒHt¼ÒÃŒìDùÙmÍO×‚Ú\Z•&Ûä™?‘…^3ÙÀ™öEã‚ôÍc4?]·s°­y7Ù(Z\\£d4¬`Ä 4qí›X¢„Ø¥r\\ìOÍÒ½Â‰3)¿\r—n„²÷„ºãxžœ»Ìö¥ºc¾Ø]—ù?=IÌ4ô:–õÌàQ.A$9N+jk§PžÖðh¦²yì®6©®—­Öà8Ý\'£ä€^\"Ý+b|Ó·‚J	×¸°PAæøÆL·7ö/-À6oDñ†á)bÃ+yÝë^•˜HšŸFÚuH©ÙÙê,ˆt‹¢WùJÈB‡_(AœÝ›Œ€WÄ^ÝQÉñ&ÇvLüæ,cß8\n½°¾ZDh\\¡Ã†,‡`—‰˜:>éôëƒ¾ûBe*Ú‘‡Võ\n¸Ðl\nŒ¯ÆÙS)ˆXq¯…?‡…PÒ\ZDPÀ¯ÅÖUw¨gð\0(§9©PBîóH¿/¥=uÿ³i‘h-sÃ–ÏQ[¸vuõRô’‚fæ8èÞ=àv<4¡*‰Ú‚×çëlÉ—\Z\"ÚŒâÊžaáV!ÜÂËÚë¦§¹šÙæYë\ZV¼ÂBI†ÅÌg’ì!Et.–AR”K°ö‚™¢­=U†fá$Ï©™Tj1í9¸ZœíBW[ÚÅ1‹RìÚ¥Ù,üê˜U\"¡#i5Æª¨¡-„†\0ÎO®ŸüWóÁ2ë™nËNYUÏ½4ð—Ú¤?©(•æÿ\\Îž|¦e›†“djp¾ƒ™ì/Ao-ºeë\"ãvyç\\Ò®ÓõÓÒ×Ü9ÝˆŽïN¡‡þ–7¿U£ úˆœwáÀBo ‹y÷³Lõ;´$[+á0çeƒŸ÷¼il[ÐNºË=o˜\\<ÙìTMž»8I#Ÿ ÛñîfKñÁ²Š=ŠášöNZ;JpfÂUìŒ÷4˜†8\\ê›\'|%ˆØÄ~Ê».‹Píþo•÷0ƒÑaÅdPÔu¡¡0Ú(¿«“OÄ¾ä.+xb%pÉÉ™}ç¦\\f]\"4+!«ªRqDVÿxP/a›YþgKý‰„P}—E0:ÀŠžœé]ÇÐ²:¿@b+z$¿ŠEÐf­ˆ6¨J#ªÄ·\ZF×%‰<†ydL÷–ñÁÌKXÜ+V´×Ý–öœmµ¬Wž®3‹ém‚¢«$	>zZ6VÃw6®ÃüM’e€øqÊ›mp%„ÎFIzqé|vÌé\"N‘h[˜ès€O0 °˜ŸÄŠål£ìÕþZ_àë”_oÜ\0·ð§‡%&/!JÈ»M,Ë6Ç£g}³>{}§ Tžc(=éôaG²¥”ÏU_;‹d.5ÅTJžYVçò§Ö<ï¼DÔ3£çÉ ãxk(¡ Ê(ÝaOÍ†Úš¯âòÆO{ÛfŽE-‘F‚´™ø˜$9Âi•‡ž…J6–¯i9šžÕ¸3ó#5sul6GÁ7J|6°@Ê4Üå¬`¹*ˆr)FÂ¤ÚÖ3Á: %ÌeÛ „¨àr” E2ëBƒj¤‚žiOØá»\0ÅVy	e˜‘1=hò;œ†\0(ðŸP&_jPÂRÌ-Ps×]¤\Z‘5P‚â6—]Ö§Öœ5®~ë»K§šãö° ¥XhˆGêHq$”ÃÛí€Õè¹.ŒãƒþúÌØVðÁŒà\\A	ø=À-Âh/¨ÑÙ@ëRÂèä‰….%[=ƒÊINv¢N»ˆŽË¥¥h{HžMP}Á²hØªœÆ!õn¾ÕŒâ oØÂ¹wíqý±\"›=OÔ¨µ‚ƒ6Ÿy	ö‹ð<\n:í‰Í»Œ2–wßöŽ—à]Ü\Z e*yå,Ê;€óÄ2C—=*¯NŒ·õ®k…hu´vÇÓÍm£é3Å–ƒ_ó?j¦®“Z»éÞ{„ú\'Ðq³‚f[F	r	 fF;\rú˜Rîg%™V§Ò˜	¬\ZYa\\jí¹[V,›ŸÞX¾&JÈÿ€Ô†ª1ù{™ñôè¥µÏúý9r(apVZrÙËA©¿è$¬3ùSºUÛ ëRó2Ê]Ø%Ôœó*R¯¨XN `B_òî\rÌÜ*J dÎáM¶lùU×^ä w\"ƒM8ƒeWPÂ–¶ß¾½Ì%-Ùû`C¬°Vâø>m\0%Ð,šê\":†Ÿ<j«©ri7;Ãµd¸®ñ¸‡Ç>Ç/#îk:°û¿eíuÏŽ{c}Â÷¥^\ZýïáãÃÒ¢  ñ=g´Þ@c):,ñ¸B†°‹Î¥XîÉvá6uÀíÛ¹ò­/„Dì”s0\0“…1KW¸‘â±¦eµy~	‚ïÝ3‰<\0•BSaÑ`þÿ\r%$™Ý·U:³b\"€õG™twDÍü’	èaz¯eÍ2©`/ÈF¢:È˜ë<ÎšlÄÞ\0¿œÄôe‚Í_m½»:\rÉ$õgnj˜[D6bDóaÿß°Fà¨I0f\rm7R4Gú\0\nž=üùÏ¦‘•ßµ\0dÔò†ø%\'ìñ	Ü…Œ^.]Ñ1[J8÷œòG	á¸\"qY\ruJˆ„OPê/¸1ôøÃˆ6c\ZŒæjäRäˆtýÍQÂ0\nRÐyzÐ4pÐç&Ñ„ [G	TÉ&åÅ¢D˜0:ílýg“8¹àü‹£ÕÕ\nÖ\\Êª‚àÀ2…&bý…[¸QCüBÑÒ1ë¯þÀ&Ò7¢CŸ®Ié¿“*\"IïÚôÿ«¿ê´ó¨1e©¯°dÊøüÍïí.õù¸¡-{úe’ì,Ÿ®Ð\\Å£ÈPäMí¤ˆuÆ{¹)x‚SÐªªM”À hÙB>¯uI|€½‹”7\nhj—š&ìëuPí\ZëóØƒPa‰hgP;W)¯P±6 Ï³	²\\FoK/µu”0:8&\"\rÌŽYÁ\n~’IæÁ£ß3äõôÙÄ|‰×ÅóÀHÒŸò¯\n[£w\0ý\n…qº9º§ãë‰ñ½TJ\08¢;º?`\\fñ¬Œ/Í%„ÎÎœÑÀ4,Ôžî4ˆ<×óò1[Ý#ÕìÏ´mb³Y!8.0«‹ºWµ>JdnÁ „àÞúºœÿG™“(¡G¼âX2\ZéâÎõçàFÆ²qË*Ž$^ì/_Ô(§d^*Œ‡ÖŽ.”aåÝs‚‰©Ù¶.—à¦ŒGQ¨1òK\"	Œq·¥f—>iüz”°¢ùQ“ÁÈ&eé³÷yÊ­Ú¶L/{© Cçç,K,¥åvz¶FØ,ó\'¾iAðÑ‰nŒn‚ºæè$I¾ËÃGSUÂîÿVP‚;N«/Ä÷yÆ+¶€ØCfï{Îº·…Å+4D¯3dEÂ`o-wÇßn\r¯e”à‚°I†¿é0]ëH/kôF˜ªN€ugmÚü4²\ZBX#=—ýn$sÁ€Ñ¬öTL\Z‘U¬K›?~«)!9pš×a÷„r+(ÁñºW;/!ÜC‚iÄ>&%L2ù÷ÚB)3×\nïv|\n0|ˆ]¤á€RÅôB!›?9¯Á`wÖ³ðI±ðß2×a%Ì.zÿ¬\ZõFu¶?Bu¶Í²Å›(39‰Ç2hÜNìàíôéÈyJÈ<ŸqÃ=ç{á‡>x¤iéf£m7Ò	ExBp^‚…ÕLZ7ï%¨C5¤YxdäRæãæñM^BK\0½ï½2Ò½ýy\rëˆÝE¶2Ø\"/M] Ú_þM{›‰búrßÅTû^sêe[H	´DúšÊx)zÀ?À³<!Íx\0ùáKs‰÷R«}F!HU›ÂPT_’™-/dÑJ‡«;g”€Ntƒ±l}5\Z\04™\r”@¢÷¿ÿ—\\r)•˜\0g\Z4Í\rJVéÆþÛ6­0®ÇHe1—èÞÑ%NáÂkô¼Eæù ·juVŠkñ;z6Ygñ™4fË¸nƒ¹Ò	þgfmð§öÒF›AÀ€Uá6Mi°s¤w01¶à	²«o bv\0úgìwýð%b‹D½”7m”rë¸9€Ì)”¾¢„ö¸i¼ëñ<³:f¹Š‘™È<šÚWç^ïßúÔð¿£„Ñš¹IÀCHëWï˜ŒýÝ7r€%™&8ê\nz(Jh-ÅXS&s<ad%¿\\?JVŽ™±Ó\0ÒÅ:]ƒÜ)8Óó¬ É¬`Ó\'¨ãg|¯5rðRJX&›°¬—¢`ßâ6Ò	¾HtS¦nÃ+ü³ÓxC0¬ªÖ$\rPt„!„›FqÑükç”|Ç&ý–ŽÏŸ(ô„B*]]W^›H×tÇ­Ê%h¶¹\nÕ~ú3/’²òLe¨n½eª ÷Ê¢jÂG£ÊvwG¡ç™ÕïÐÃ¡¶0÷ñ(‚´¢G1Ô\Z^€\'\'b£ì\\A—À+i<ýöœ\n[OüÖwAé1ú7*¾6!ÿe§lBÈë6ÊìøÉFÛ}[A¢›(.8åçSl°)W9%.gƒi­Ã\ZÿÑ\nšžÀGÌŒ\r‰æ-ÙˆAcµr¾ëð)[U¢øO–~ le±Ú´ -úñ.QKR…L,ÕÁžÓ¸“ÌaQÏkÙtyèz´°YJXx—¬u/Ð\r5E	#—¾Œè¼*Êß:9mÄ…°uOòñgEe¶†ì <­Çå„Vñ<Ë·YWË”yE[xÊ>¦E“^›ÿg6Zm#ßSR„ÇNÇP¨Z‰Q%”`LVÓ6Ì\r¯«’ò%ŠÂEwR°$\r|ŒQ7Iëfð·–ŠÏ0äÕzùD	”‰g\nÍ1%áÈ¨‘qyÁ,…]ÞtP‚¤Üa*\\Ñíu~Æ#«¿À×Â-à(èžô\n]^Ÿ!±ÌQðTå`e•Õ2vê™šl,=LÏ¶	ù?(á¼sÏo~Cˆ?GYåžÌöxW\0Gà¨±ŠË(ÁO(A±c•-®#´-„\rÄÇ\0Ã\\„%bèHiç\"€Ðßó°yAd„Qµ†×I#5\ZQ¬é˜™ÈuÚúAœÔš¹u”àùÇ‚L\nÜÌznš1´pó+?‰Ó\\nV?êÕ:(¡‚Ý(Á—ú‹Žc<9?\',‘Îl©ÅŠ›0ÓWx	ËL¨}L	ßH4¨m\Z¿a°£$p”àS2ÙœÒ³9o§Ù­ùöƒ<SØc”,Ékä™GE¾1íOk”ÌH5Ç—u4u:qßRBÖ„`½l!½É„UŽ«ukN`r +ã0Ô¡&œA±ãM%\\qÅT%4\0×ñ%”í\\¸Êô¬!\"¥V|Óé‚¼`ÁîÂJösº]ð¼ó.Ì	ªþü+%,³ì¬Q†ªE±%êCÞ¬KBÆ¸¢\r-ªé™ŸhxL¢°ÐK€D®Ã­tnúŒ´ÊÑXEK–¹©³ìiÏ\ndí”:b8;«ÐYÀ§¡»¾øU‡uÍ\rjñÿò¹ç–ü´ï)a\nçÍ†Ô @ßŒu#&¶;+xkþËÌ‘#\n§™PùF”m×;ü‰GQ‚†Æ\"e¢Ø”0üìÝ¿,Ó¥uOÙÇ”\0ßÏþÃyÒ	ð}–9øÔh\nPÛ{/¡	ŸØŒDœP‚+Ì¡nr–ý.eÜƒr#‰‡c”ÍÃúºúºo)!¥Éýg_PM©N)L|ÃD™äc¹i¬pÅåfRÚ˜Vv™—`RQ¸Ã U$GÝq¤`\n‚eŽ0â«¯hÆ|Ç;—¨iiä*ÈpMóyLë€_5ÍdžœçŸdu5Ôºí²?Ž€‚D‚¦axÍ…ªàŽD=JÈ{À\nìY±~þbyã…”\0²Õ­Ðé:ÚHy‚r\ZàžÅšÛ7O	ðU,Š¹£êu¯Ž÷ÅÕ¨·\ZfuyøR‚÷ªp\0’6Öã¢„D´pÓX,¤æ¶ËÞoêbœ]¹]B®0Ý&îjP‘Žqjqÿ#.½ä\nÙcáþë¯»Iæ z¡\"<À²K[9¹Y;2{™üfvC	ücÙš›lÄ…\n.ACÅQ‚KÊL™gOK9»PX}ö-%Ô%¦Tû½%P\\>ûµ×LËæxSá2,È»\ZÌºŒÎ:ëB¥.ßé%í|ÐôrZ‹ …>àÖ°%„S¨èS÷H˜Å¯§øžÃWˆ¹k…­ú·?QÂÝwßÃâ–þv¨Xef<N›–R¹È-(ºM8ˆ\"x#«¹{.É|j”ˆÃtXšÄ~ž	†uïâC¢Bò³FgÕj_ñ=tû\\n\Z½RqÎ_®ó0ø\"Þ³LÛK¨-J^ÆŽdã7£„¸Ð§³ÔkñPBBsz‹@¸\Z\"7¿:Šµ³x¬m%l*õµ`j†Õ×ÙÇ^‚r¦›n¼ÅBc¦ñkmÈ(¡|À.”Ðbœ`Î¯ÂM(Alnj’Œ¼¤ÞÙ¯¸„b‰D;‹UÈÔò{áÔº´Ï)>éÒ,³!Ò9àKq§JÁK®ðl„ —+×tâ#V3ÿš#pô“Ÿü”ëZ‘œžÓQ‚¼Â\n/aè®út@&ê*¢–ö<Ôš–\"ÉžM3ÅÏ¦Ô-0XûŽZ¯u…¿âøý‰Pª<-$’Ú)Xv`xŠæê!yAì;¶\\H	ÈÞ¸â¦hv€àµÖ14¡uÂ÷Z6,ËLÆ4,ÜÈÆaÄˆà	æó0„‹\Z•ƒ$Q®{JGLW^¾(ÈÊAd½õžn[w—r!Ùò\"l­ul|x^j÷ôô£1mrÖˆlµA	ÑIë_ébòprþæžŠq“ü,0»öd[Èû˜,j}éPŒ·Ð«ÿ¾{ÿ\\Ø§AÖq3)±àøù‰á¯I5jéÓÙ ¶é¬\"Ô%Uü*/mä³r&§D~–¾ašJ÷áH	u]ŸŒ¦™ì\"õò)z`\0Çl´Ç»*týk!¹á\"Œ÷”püqÇ›u«‰Y(AŠ¢—uÅfn¯\0‚ÍÃã\\¹åÊÕ3]é·¬µ&ªKúÕ@U“šìJXKÝ‹›ŸW´û¶Lb›¨8\n’™ìCg}Ûˆ]CÒ^LßŸ­­&iÌ8\rLY8Ò:B‚(ç¨•K°Å7Yô…³&ŸÆC	7qš•Ö¹)\r1ô$/0NjíOõKâ9.ŽT«š¡0]mëÀz=¬—eë%t¬ÖÀ>™MV¿Á‰½¯I L¢…vb^êå:´Æ\\v’ýQ‚ãñ«%€t·Ú×ñŽ™Õî­] ¸7(a÷Ø\\ßÐFÛšãÛ2ËM|„e×ÛcM+…¤cÚ»~5:×œ¿ÿÝÙzàO[Œ1\Zùþ±,q­VµˆÓ.Èø0¥„ažÐ$Ñ[&FŽ?]üío¦ª­ÙLæÓZrV7£—ÍñšÏñ~ñ‹_b†Ô”²ú‚@ÌŠŠ£tp U3§{ž\n,(öÍü4pI*²‰`·ú×ÕÌ]‚f»{Kk]pÙt„+Ðj” Ž­Y‰TlP@¦is²%I^]#olg*rš`™½Ùœš&Øêàžª\"T+pE\0~Â+¢ÿ®Ó°äB=¦bÇp(•ÌªŸ†6KÊªYCF;[éº©yoëƒõ4ˆaÝmý»,£„˜½¸ä“•=•oÌÆY8\":­ ÐÇ3(¢¡^nV£—ãÝ(3ôgÆñÑ5ÊÈÞwâ²\"ÔµuÓ/õ6qÅMP‚SØŒòozã[%E£„BCýCvG’Ùtué?^t™™ï~ý«ß•~hZ‹~-ˆä²V\Z2\rÜ.aŠ]\"?ë¢Õ¾\r¥¯tH.Wàˆ¶r2ýä“N½Ýrª3N5`\r%°6O»SÂ—¿ü…\"Q•…òGhD‡vï˜~¢ÍÒ›\0BPÈwª¯8—Õ“Æ{$]ˆYÚ*äóË¬®+íÝ_W3Ž”0\r¸»ë.1zÀ­$¬¹êÂÅìŠXlÊ`”\0¤.Ø‹¦@Îâp =&4U‚!*ÆÄS9=I_ÀŽÇ=Y!þÔÄÜ*pOü+CöJØpnv¾XJ`ÜŒaÉYð9U@ubTaÑ¸„MK°Q‚/øC¢‚{=Ø¢_÷+JXÖá—yèo\\•å6aw¦\0½t1“*0dÁd‘¢(\'È3ôÌ€,“Ùyh fCºù(Á¼öì7^Â°þà…(\'¬SoBYƒ’€ ™À]M	_ýêqj¢’–Ò¬ˆhÙVxAÈ…”©5“§™0&pT Ú?QÈ’•j6GJ˜6ßuWæ¾iÜ(0y	ìVÆ{\rdk^6×f0¶À‹à\n(AU\Z?p¿ôrQ âB®6b¾C@œQièÏb !’s#\'2œ«Ü_5¼q}û}ßz	I/˜Ö¹dòM\n)¯ÓÚò+¼Ü,sÀ-Î½ \"Œ«v%4Ždø±©\"`V“iG£‡f.aÝŽì®K	Á·	ì¤—!8XšOüæ%k&2O	rÑG /tJqêœÿä–eL ¤ðfLÿÔãý/½„M\0Ù&bÖËé+34(\ZaE—”þúÒ—ŽSUŠ§šõG?ü™¢¬ñÊ»{	Q@i¸)–ÏÔíÝb%ÐZÑR$ªÞŽ*o)f\n•šKCbYÄ£\Zm±¬MmÙ)ëjææÔrÙ]öNàHyQøÞR8Ù\Z…HU8húàòüÌw¹\n\"ùSq‹À‘#½Ý0\rÄ×dƒ¤|‹6p¦¤¸™Síw_!HÕZóS|ã®»‘¸,ž9¼ßx	É!ÚÓ³ÈÁLDFz·2Ú\nJÀ»-ZW¹j2T²ÕPµ¨×éôì1Ø+WÑÍ	ÓŒ,ë*ù¿•£ÝÑv€ï2Ll¢l#€¾RT˜%ñÛL®að!¤—ÃèÛöÃ¾/éx£:l¤U%¸¦•ç¸:ÞŸ(¡\0.­Ò3¡°åšó@ç4{ rm­`ÿøU’4S.Ì%|å+_•K\0(ÅIùRŽþ,¦¹pskôãÖÊO3=\0¤àj¸HS¨‚žÿøÿ™¬ÿð¡ÿJ	kuÚÙLP“Í¨âá~áð”ù)(\'‘ð ŠÇÖÁ}Þ˜\r¾yuâxÍè‰ðÍámÈRø³ÕæÏu–„*¸w@úü	îy†n8†«Ê­D-+æa—^ÎêJŒDGòÌyI>VÞÒÂMcé¼v=Ña,é)h%4ÇQÐÙsÔ~ÅŽd°‡Í?D+ŽÖÒàÞ%4€ÀÖ¢@RÁª\'Çèªƒ\\VäGá/¡ëw/éåV”,m0pp‰ã­œc±U7ÍÅ½UéåRß»ÿ[z[ë%T4ÍjÓÏÍ;MítZ:wôgŽIóX˜U(é·¿ùCš…”ð¥/}YÅQ³¡¹&J^å%—mT¿Ê“Ò”[ˆÙN](Sv¦6U†¸t¼Âºšùpô`ì ÞLufxËcF¢4@ªI&\Zˆ\rak Õ0rC”j²Þ™båpYs(¡øå²\rÊ89Eƒ\n¢ù,×\Z½Š²ÓY\nO™º	+’OGJÐëh¾sËDA1q^ïÂÍ‘D$ûB¤(c¯ÄKÎMêÄÂGØôéOºb¤¬1[×Ù—G°—V*:2…Ñ›ßt( /%Ð‚FZ©5:òˆ©GŠ-â\0éSž! Ïþ¬>5ZòÅ1ÎRØ*å§­Í%ìsJ 7&¢@`BâK¬3û‘@L(/ÊÀ£Ž”ÁË(á_8ÖÔ7bÄi°0…x XA	zHë€·Z¬MòSèI‰Q‰n—ÎRJQÂsr“ÿZq´h1×ÜvÛŸ1Ê¦h^é8ÅTG-¬fgë´Ø	\\\na÷¸‹Q@õŽ¬§ÛPîoVÕe2†h1h…fµÇ-|ÊjŒT†±[ÃgÁÅÛÃ0—0V½N,„/ÌT\"óe”PtNcµ9‰‘Ê$g”P4)¹EíØB(O_+f;s6\"xJó×µ{¶ðøµG«m®*ˆš§hŒM…GË,èPÞ§qU¯;è<pÿŸš\"Méó¬WÜ}úégð¼ÀßLí¦©•EPu¤ÍLžÉ·ÌÌC·ã\r¯³	-ÆŒIãV”Íÿ´…±ŽM]jIÿªÇÏ¢\nj?D9õ[‚;:î¸ã‚®ºòªÉ3½Ýä”×°úUAÌ†S¶Èß4§®Ÿükõ\Zç\ZÔªóSV€®¨T<Ÿ-µ ’Ó`P;\05³.ŽHDæ¤ƒÁ1™ñÊ†E¹Wr	ºÕ2Pš–õ[üoí¾·wr	™ìa„2!¢ð¿ŽýmîÓûèççiëj²÷A„_\ZB’SÉ²ÌAÀ$Càš&´àêIðçšÎ…kÂAM¹}MƒŠjåŽïqFIÃ¸–\nÿÎ¿Ì€»º(qü:ë/@cÝë„Ÿ/BÍCJ\\DDI/¯Ë~rÀÁ;W%™2Ã¶Èµnâ“  R‹Y9ËãD5ñ7éåNq5—õ])×žðžt¹™…/[<ctÑá¥¶˜\nëïB	«±\"\"ö>Jøö‰ß»ïÞûŽ{†ˆ%H`¦å1*+ºÀÞQnzÜ2pyBFª˜¤XÇ@‡…±‰ŠÒMáûzÊ:»ÅRJ(Äi“ÅåÏBð(zæ8)„ûI‰¬†Âí$”¥fŽPBÃ‘˜9J ª%jZWIém¸Ù-ãîOff…(ÃüQ•(eÝDIšoj²½â%¬\0ëL°ÛúæØÞ¡“–od\ZïºK\\:L°¡™cˆía!f\ríÏáhÐ0Ž—`¬2w°¥Zl® i¤*Œ‡‹ÿ ~çR\\½Êú³ìÑ–ì\"-å$Õ‘]¼‚ÖE±½ÕÅþ²ÈÒ.”0f±öú\ZBáœ8›®Q*……4Ø¢ëFrÈÊ1ÅâHÌY†F	%l’¶&{ÊSžÒ¸\Z±ŸfžÄ´Ò×ÂQJXMõ((7BjOš¶ä§¸¡	ŠˆªCõ™˜ fˆ©¶¼&0\r\\ívbP®†“*7úÃYçí>èüS=ì(!€®£êá‚abð•`šô™ñHt%¶ŒÔ(\rBPƒ.\ZÌÏ•Èð‰ƒ› \"£5ôÁ]ÓÀÄ¡Ö:!‚‹¾¯*±J•¿RÂÌè[Û¹çžû\"uÂ.\ZˆH	6ëGc±=ÅîF¶ôð\Zwf¤²ú}:^îë6By\nI[UÍ8)îËsG2,°Hæ¿‹8%p\r“n8ÆMñŠ§Ê¶Ýo(!Ö´åfy_€.£#vDùsšmYf™üQ‹Ê½&Èë\n#yiîE¹„1$Ð)qþL«Ì¯ˆXá¶[÷/JiÌ‚BóyÝÝb ²R³X‹†7(aâ‰$‰T :tš:ÎS‚®âÏšDó5­ºý^3X˜íÎ$Hf-Á°ìßÃ”RV°kNÖ5j¼#e¬KGÅ÷ùÍcÅîâ% qj&E·‰#É%ø3<ª“·$	»…ž\0\ZÄ”[7äÅ‘Q‚[wƒQLƒIÕê];o·GÆÁž¥IþÛŸ¼eˆ„Yä­vdŒ«. A°ÌsžYmäHØ­!d,sˆÑ.ZMŒÈ˜Ä^v°ñ›öÎ2NÃ5­ìOÆDö8€?\nÂ<Fw¬Ó!!#c˜e\rû%xÍ‚¥ÍÒA Rè„àWx—à\0‘´Ž$(™‘4BËH%O\"r–ÀQ^xg9R÷ÑmµÿÃ¹vÖÖEA¦µêF^Â\np—4Ch»ššÓaËf­PŠZ2`:wç8XÆîmñÞÔÑ\'fÌîB	^öÉç0\'ö	ßøv£Vø+SJ¨ëáðXPJÊD;•FWñ‚âÚ“RcŒRO%ë†li­?urš]Ì:e-’	éŽŽ©-ì6å… ’ªÝ5…¢£ð¼·Xü¯£u#\'ÂÙøÉŸØå‡Ù›9þ$RõïêŠúÓBCC–\Z\"ˆÏ®‚k\0Hµqó ÙÆèe\ZR+GÞµ—s™nA£òä‡PþÇ \'#çi‹ƒSbŽ…›Èøº¯¿‡ÆÁÿú°Å#ÒÈ¢÷^^“\Zi“‹]½£^VJ¿˜¤¬¤jîŠA	²}UšQìâ,Á@iyéœŠ€‹ÔI§ž·?QBÑ®2QÂ\n;½(“Ã”Ï|€¿tšÖ¡ÆûÅ½Y(mó”à{îØLû7Öe³ø°Å;ÍQf{¢„$YAÏXŽÂÊY.¶æI.ÓK¡,ì”:íBJ`ûÈ:Š¹ |—„ÔÉERÓ>£Ÿ…‰ÄššNÇ¦{´zO¦†Í(‘oá‘¦\'Ù+£—÷\'/A¸n\':LÄ,o¬¥x„ÒE!¾Và®Õ(6V§VÐSZÓAL›–5lP»@ö¬Q\rÇ’À(\\ÛÍS‚_ÝŽçW‘‹7ôÁüaOˆIµ†@çgdÞî”P¬ø[é¶×$1‰z)•Va\\²Y¡ˆ›î3¡ìW¯!/Î—ÇÛÄ‹DxnT64©Æ\r¦¥yøQB#„wùç5\Z70æ\"}PJ(ÊäD£‘¹¢=öTªD‚•Çé”/ÍKpÚƒYÄþÕ\r¢„tºõ–yŸøøgLea&ŒždEÉÓÃÎKHù¼²Í+K<rZî•Ä€WM2b 0zÔŒˆVÃ4X¿ôrËòÅ¾DÚ|¨£¦}rËlU\"flòÜ1CÉŸÜ‘¨†,ä(üÕKØD.Á„s41ú&¾m]kûQôÉp€ækÂQB_jJF•J^‚Sò*4t|†€õ ºýaØÊP\0××\r¹žîìš\0•îñ£„žš\n7Ÿr¿¡„à~¸$ÓÔR¢@eÑmñ¢š Pâ%0ªH)ùÛ49ó-xvv¤SÈÖXN\"Mbƒ¿k­ÿð£„ÅÁÜ%T)UiwnuÑ8np™	Ê%l›êM\'Õ\0\r\'\rìh-û¢e¶l¨þ,/­_­‘¡¼Í‰­CÞ}×Ò5~÷° uÙaÿk×uE%Ò².¶Qœ&	ÈWÈš“-$–èSG%œ`QybE,Å£ÉÍñ-ÉËÞdf\nJ´Þo:%Þa	&Àree/€\'#7BnC¾‘YÔTÀötÓ­õ–Ë|ÓgnbŠÖåAóèzÁ¶öRºÌîÆðæ­Sî)–Iû¢±jˆ”÷ë)cò»By\Z±FG	âÚ+¤óEL/–=…0´U_Ÿçç:.N¯tºúšÚÝ«D˜µf^w-+º‰î³¬¶eE²0£3D*,±±‹y»wnõüFy\0A¿O/‹„†²ÀêÚ¨E4c„o¬$ÝGäÇã’±æÑé€ì*åFM&6˜;IÎÔgmµÜ½iØëëFíÖ.BÝó\\Â¶w”`š6ZÈ0l<ÒË™t›h68cï¨ŽOþ½B+ Î	æIÅ.4àæ›V9b› „ù™‚ç­®±‡rØí°Å”Ñâ7|ÔÀ‚Ö¼ñ‰…ç°ô•š\n:‡ã¬²ÇŽ\"ñhbDb¤% Ñó³ñ§Î\\`ŽsÅé®/zàâ­ls.KÓ.Õ.m[;ÇÑ–RÂ&òÞK)!¡-Ø–”º®Ò™;7æ4%ç\Z| [Ã©\ZˆF§ ¸_DB+W£í²aÇ $Øäk6z‚´æX,,CÕ)üv†8U„T$J¶	8¸ˆŠ/#Æ˜\'1Ü÷²é[\0%Ôpóå\'›è\ZK‹–ßG¢gõ+ÌQÂxìäi+îÿùô]‹ -\"‰Õ_dhlŽÌIÒ(GÐŸE¥ƒØìÔ&#i¸+T™j…{Üã\'þ1œ³„__[1.a…ÄæY!>h½€ˆ½0Øó íÄ¢—K0ù’¡Ú$P2òÉE`³t¶j\\Ê¤•ïÒ6ó”Ð¨kLÊ-½¬\0©)Qo7´y¹Æ¬Ë\nQBõT{âmBïg§,¦„º_Æ#\rƒø†ÅñgþðFYyErÆNÛaúµø@×\rµyÁ†PF	Ô—¯%DŽOÝ[Ö½h|S\Zt;È;P‚FqŒ‚à3J÷èÄÎ]!ÿÍ\ng¡wµ	/á!M	™Dûìt”ÐüwPk‹1pÌ1è™ñ¤GT<–éà\nŽáa<ñ‰Od®²UµÔ<€¼U…éVc•…rMñ°\\Ç­íÉ§dyà›æ¶s$×SÐ‰“á¦Ú:¥ú‹ð?\0·‹‹0ºÏ&Z]Jèó	Îî>‘Ól\"©Ý·ÑÅê)c(n“`”\r§ùbA6}DsÄµ*ñ8Üq‹T`lsIÙUk‡ñZ-u¤Bë•³Fß„ZnÄf’Ï<Ö?ü(¡¢#GïßáQ/a\Z=›àS{°GD6µA©…(¬Qì£¯1íD!wî0¥Ï!oy;oÃˆ„[n¾ý†ëon¤ÂRBnÍX\nt0Ê&ô{å)B	ä@™HÀÜ5Í@GÌý™õÑ\\Zö›ÎlOùÞj“F7à;½•³ŒZ\Z”@§Gåuæ’ùìdn@å§¤í“µh\\E“,!ia(¤t¸§/ôüW/aa·|P/ar°ff£¶à%\0#EÃy	¹ÎE\'Š¦úŽ›E$À´?ëÎÕvÈ^t‘¹ZI¥Ó¥‹”»D	]a”ÌûIê®‹„k.…!F.¡be3¦y–ò\'Ù7üÅ]ØÌ„kÏpµ¢+%ü]JgÂÿKGØÂ™üƒ‘³ix‹ÚÙ[J^Y_ ÿÊ´²¨xçvTÜ™‰3dÌ/Ò\"wƒrMf¦ðÚ£ñú½æ‹Š^Bn°Ø‹€œ°ä°fÞ¬›ºåŸÈ4JÈ¡¸\ZFrŒˆóïfÇßobŒ—¾ä•>ü¨‹ÿx¹ËºÚêsëºe¿Gþ_f…U”¾R þÈ«-Œ¦ŽðÝŸƒ 8¤hrSæ¡>Ê;²ß&ŸôRNØð%ÒôÃ¢q|aÚIà|ç®Á)q®è“çÉ†2ºEÙ\"éóçlÆêYŸb÷\'/¡	H6JP¶F!doÆýÌL=¬ÑY|fý«Âßf­àˆ\nF	.ËÕ“öœÏ…LŽd=hÓÎÍ)÷i\'þàkYrkÉjÜïjú]Ÿù ‹XaÃŸÞ—z^Â(€Sjn”AÎ¢s·Œž‚<i²/Þ]wSdÅ?+dW7déÄÎ{€òˆ\ZÑVÚ€ÖhAW¿j6®ô²Sê 5tÛ¬ÉÖžì`>b±ç¬ð¦s_[kóC<Úf×“ÐaÅ0]¾­’ Ñ } û(º6/áõ¯{Ó—¾xœAjDÓÐåÍ¡.c‹Æf/d…õQlíôrÚSGõâHQä§YH©W+pó%ŠÈ¬£µ&Ë±£tQß6Œ€ÖÚÏŸÃ\n¿Û(4õE$¾¸ì˜ø7sÆh\0.4kßYW1šºŒe€Rwú«—°®—0Uì4Q5MÁ”\0Ä>nWßó’Ï~âLˆû7mÎpæàW³$U*æ\\.—`0šª)v‘±ãr=sïb\Zû©™îÆ¥	Óœ»G-3á^è³/m»°«w—HÑHo¢¿lŽôåù)ºì]F	•uõ.É¶?Ü,r\' OÑy —tõ©z‡ k¥ñA–“ÎåWl¡	l0¼ü¤ßé¶ú©6Ê8‹V£™Mx	»Ä*öŠ”0\"ò¼„·¼ùmŠŽ4USæE˜TVÔ’(ãÂýt]ëZhPÂä½Þq—Âk^ýz3€šp;CÞÕšõhá¿Mx	ƒvg…M¨øòSÄKHd’+] 1MÝúëÒ~MÕ\\…Ö#†«ë\'!!f>ýÆâž,š–u, P¯ »Œ#^s“yù©†@	n×‘Õ°\Zô_Ø4ß9-ßÚÉ±	éå;vf>Âhy\Z#¸ì;âÇ\'öLûP¾–¢\nvk¦`]+k(˜Jrj*¡A›²ÂeP]ÒA+¿¦]Ãg™Qf?á~Ž1çR·Þ…vaUö»SÂ|Ây­^³9J˜•™Ü^PÚí6(ayàÈ‹#œÈ²—jÌ Ñ€ÞŸºƒ®ÁŠ\"]¬Ñà#dZjÍ~ÝÊÄ<ò.E’}ìcyc®q–]»Ô]}‘yé“%†‡\"%mP&Äº7ÇÑldÃ4Zí¶[\'4)ËOM)âð…5Fsáñš¹åÂks%™D/&hÜ7·	Øý”}ë%Ôuóh­0±ô²¯Oþl‹\\‚*nÀñ§…ÁD“ñ%ôÛP5N†ïBs&ca<6wHåF¾Pz0¿\\ÔF†`ö\0è„}„¹õ]B<78K‹D	`–.[÷ß&Â©ëŽî\\ÿ.³9ewßÈ*×j÷mš„uJ±îúOônmŠp°¯`)£U“MŠÊ‡}\Z	ÇABçý¬/h&ÐŸ…ž©ÔFøÈ£Ò£°ÆNÌ-íoZ$m\ZI¸RaðŠ]ø³jKŠ¡5]¶,hŸ,­ÙœÎÒ-0“HË#Äl+­Ý-lÃÄöv‹ôÖb‚qð2JX]ò§øðš«oFÁ«\')þYÂˆÕ®ifDô”;&b»ÝXå)¢!p@Ü¬»ñ\0|\'d_àU“&rCöóxÏœ<íõþû¿ÿ{Óá¾\\ŸÙ\0³éÖ©^Ùæ‰aS¶•GkÝxþ`ðý“Ÿ|à«²pæl~‹)»PA4î5á—H\\#¿õ‡ŽÿÅJR0CA1pŠ~õU×IHXoÇÊÌÃU|P\ZXW5¦—×½È¿ØKˆl¾ôG	œý—Íq/U‚—Xò¢€{ªs\n·–ºâ¹¢@tWˆd9I\0òÆ´X¬Y….ôÙ>~Enêj\n·GÁRX–©µö¶™œäÞ(]ŽüËaé°^2‰HH)ÚŸ„kD¿2ÉÁ:?,òÖjÀÅ÷A	Ãÿƒ2¼FH¤E³soü J¨9ÜÑY8ž£à\"a·Ã(I³•à’BR5zu¨<”€i\\Ÿ.¡{X!©bšV=2«ŸNíŽávÜ._ö ìêÜ¯2Q…ew…\r J‘äÌÄÍXŠwlc†’\'‰æë2d\"ÍC¤NIê)ÙþXVz™7ÆŠ•Bh(4SÌ¥†cƒz¾M¥Pÿ“vÉ¯%·=Ï6?$(aÖ6wƒrsY[ñÆfë½h?-G›™-l[µ\rÐ¿)gÛ²†XLô4ÃšÁ¯|ÿ{?nJŒr×¹+ˆa-ùNyïEGë^dŽ_J	AjÍÿøÇ3ó‰…yˆ;+HÍ‡\\|Ç<	ÇË=÷ @ŠQÌŽQødÊ’³Ã¸½(ð§?ý©HtxT¹HSÒ2-)½T„]\\vÜ7¼(~µÞö „Ä¢ˆÔw\\®„7lõ…À[6§å¬‘.HÒ(\Z¨\ZP_JClù\Z¥ýÊóØULÔÂßðEÏ2,‘æÀ5;…þ°žh\Zgw©£UKæÏÖÉ`.hh=Žž$v¢+HD!7òk½HŠj^Ï}H	ÁÜ(<Ý2Ú¹ö öâ.XÚÈË’Ccþ› VüGŸòú\"HÅ\n±/ù˜<R1m5ïŠ ´)¡.ŽBÓÏTé[CÏºÌ#›xgçžs¾‘†ªi€»¶ï\0âFœQMúÇÍ M@3:	5…Dä+<ÚØ´S«XiYôÉ“®ÃI:ÜeÁB\rØtþ¦Ê¾¥„Éêš›àžˆ%}Ëo‘F5Bú|†g>¬OdÑÀ=ú´(u\'ÆbÇŽ0½‚ŠŸV`²ú‹27Û<r–²ƒ¨¾é9ÇÙ˜ kñ¯”°ƒ{mÄú!O\\N†ŒJV\'þ¶ùÂÞ\'c\"ÄÇb×«kT\0ôÇLÌvíè“Ÿ×àADî?Q	ûÿñÿý3\Zt(dàDpÄ.@›Ì\'åŒB® 6IæYOÏ¢]VŒ\rèqn„œ”–é˜.¥émPO$ŠÍá»iWŽ‚WÜ‡”+\0OîÙñ\0>È?5HëRB ÊðÒJ2CÕ½ú‹\'Ÿf„Ôë}Ö ²¡Ž!díX¦hEGÎrnëwÚI\r¦þ2\'±ùøÏºH5Î]ËµÚÇ£iüÁ­Û,¦öê_ç“s êç~qæo‘¹ŠT}RM‘8\0”ñ˜xmÐ¾â[:mƒ_/Ù–I0ê­5|¬Bì3Ïq¿ñæGßQ/\r¢çëÉŒzÁ£“WÓ°ÒŽdË4•‚–(£¦™«NùÑá±uq¦ôÒù3ï!sM\\âH(#à@õáB£\\“˜à¯”°Ì=š§jLÎôÃqäíkT¨šnC^†¹©L¡6ú‡×6ÍGæÚÃk§€u>ø7Î…Ñ6mÚ€DWP¡ÎPxsv‚*aF¸†cBs\rM‹l¶ðÑ1=”×ùâÎbût<•c\Zë˜¹ws_Ø\nÃæt`_Žš0%À«“Û+°b–Q¸]cá]MDÅ‹HF‘’m3\Z•à´G@µ¹F ~Î“_ý©³q¿2àòÞfŽÂ®¹„GPàHÛ}úSŸ3¾Ìd¨Ër“~ø‡:K„àšÌ^lÜ:×«\Zm‰²ç>ç¢F®€f\\¹zÖ†Âí7”àÝÇ˜ÚIÿx	:0“­¥rÿA`â¢g€›‹J•É‚êÌôŽ…ÆË$XÁUìHã‘®_ÑCë*C\"—µ%ÇÊ\rf†.h4¥‡bU¯g\"…zõ–y	$CPœ¶r?1wq¤Pu¸er	ŒÐè ^(ì™M¯eµˆ\nc-ÎÏ`eÐš¸Ä&îorÃ\\F7BðŒ.ˆï\Zú»Ð×›Ü¢¹PžFé}­«ÊöoÝ*ÇÐ\r®F[\n¡¸G9-Gr+}×gùódé>É%Àd€ÜFìÂ/+,Hý©à›ž¢×p3èäM›u< s]F`sÂ€¾ö\"LaŒdUÝ	¬ùP5YéýFóLùù¹PÛ#ÎKÐH‚¼ÿÃ_ùòñ½¼6“â4œxâ4M:áfØ^UÚ°SH_30ˆÐ5Àzã9ÿ¼?\n=œðì7”LdÔ—ä%p˜H@÷®rXSMx5ôOæ$—‹	I5Y â	tÊ‹ ˆS¶¿kâ`Ôâ‚!3¥Ñp@urk–´‚EN‡´jhd€•ŽHT«36z3NÚÌöÈ%Ôˆ6‚¢ÆÅß£ÏòÌ`\"ú‹ƒu“_:W,Iˆà—ÈmT\n ç8‹ð„ž´ˆ°uëcw¢ƒy“8FÏÒè¼\nŒ®­Ñ	 ‡VZÒ¹¯(\\õìÑÄ\Z][‹œ´ ksmõ¨ÃÈÚM¯<Ú¾¥„fVÑ‚¾¯ø·‚¦Â¹Ù0OoŠ>Lzúóé\Z\ZEê˜x9mœ	²B¥öçBéMA™Žc`]ŽB]{&Ì¿d_ö[J(Ñ?ŸûîOðmJ\"SYüö7gÝ³ãÞf7;úä\'Žf~Òu#Ê——cÓœh3I6j\rIöóÇ|Ñ¬y¹LƒT¡èá~C	8XÓ*Iš)˜fz&\Z¨µÒTÊ¬cÒcû)+-„/6,‚)bÎ>µF-OzÒ“T1	%C\nÚÙ<Hä.pÇŸ,S¸ ì`å\"4Õ¶{\rh\0´6+<d)Á,½kn+ÞÝ•´‘&(v§M‹n>kíBÃQ»dÃe­	}ŒâÔ;ß® ­ùyÝwÕ,ÚHƒjµý×õ¥\"Ñò@(AŽZ›j/ ¦)±¾»hÄ®Ö¸À°_Odp0~i…\\syQ}Ñ…ç4L)åY¸rg’|oPÂ\nˆ/L4R{‘_ÚÛõ‚$ŒkÉÎs°¼,ÁrôÝÄØO$¦£	î±Éð¨Bpb‹X`­aEøsU¼\0JÞ‰¥çs;þdÔ›íîÈ#>&ÚÓc¡ž¢IU™\'‚y	yÙ¹~båÕâ–ÑTsî…\rE‰r÷|è=Ñ•}^qpäiú‚5õOxÂ`Y”4À±~¦¥l…EÇëØ<n¯˜? ñ÷Å7#?!Ãh9ãg c‚!óâÑ~b9‚zìš°	Aœì&°åRô;DpÍ0±ë·Åî«¶‡*%¨X_›ÞÖ?¡Z²-CÛüQ°CD¢©üJøÐ=TßR”Fb ~áÜ¦.—cp</yä%„àH]+3féƒ0ceEZ6‡ ßÅ¦MÁ™È’_«²×Ö Lã²4’ýU£-Ü¦¹Ç–]+í¹úà=ì¼{rØ\nóQŸ‹¢jA9eÁ~ô£ùßõ @Ï~Í„ÕìŽá»sÐ±²ú²eÅ…‹õõ,–™€‡~šô&¾y$Žè\nC‡Í¬ðýŸ˜¢îÛ\'~?>ÐêU\n­FèðŽ-#“ÆchÑ(*û˜-\0ˆ_LÊ¢FÔ]Ì4±Ï\r·VT úTÉ5ÇÄ&{¢\r{rÌC¤â¨Ä,£—vn˜’æQA²¢…¤„J!q˜”q‘!õeÈÐBE#Í™Z„/0¥±©]Õ#‘ÒxòÇ®‰}¡ƒËÚO;\0UPk¿Bf)»•#¢oäv8Ê±q’yoo)ZîkJXfu®X»m}ä_zÆÈ\"øbÓv€›5Êœ§ðÕ€iÓZðs5\\%0üYÍ©\0¾™¨Ôƒ— ï4\"¡¸?…1pœ±÷Ñ|ÌÀR×”t†µKOXÂƒL\n$ä×J$4´=ö¾Ü…ýž¦aw©¤º]ga{ÉÐBàOc_ŒG2†ÐXzNu\0‘êJÍ‰Wç0–ãL÷)ä§GDz9J(ÝFç\n|æÓÇ˜×HbùÞ{î·3\\àè¾{¸æêkè7J#\ZjŠˆ¾”&¡ë¬ÄØÉnØŒBí&œ°Ç­÷ë÷ð˜}K	ùæpmT\r%èºè“dôR\"\"F\n†p€H™.ØÁu•uà«JEBvs ÂôÁ¤.ž–£® hÎÕÖ:¿?žÀ°+5+ë\0´œíãR~•oÀÃ³#zæá+ôeðÄb\\|dSBÍ‘Ð|r‘5®Ô=±:°[\0yƒl©#ÈšQ¾&ÖY7ûÀ1¼lDnrl>_v«þhVan:àWo<‹¿(I³ÞrA\0–k:mhbŠä–/A»W°Pä*öÚ}£û—pëÙK¥‘(±3üÉœÉ–#¥sw$û8ÂÜº$Yi8h¦O\rÿ‹»4Šå(Ô:d9<•ý6— ”ÈK–*h…œ»¶ßc¬²,Â—¿t¼áÊ>+Â#ÄÄÆ7üø+_ù*A3IÔH$èAÖðˆÍ¥ÖÚ€²r`–FrnGøRÅQE¨û%Lo:5Jçh¤²ÂišÈ(TÕi Ôg`ö£ž÷ñ«ñDC“\nSk°â€RîRÜ…äIäØŽ¤(ù#„›ïR\Z‹~ã†h Î3(aáÿJ	6›«Ô\"F	3¾\Z[.ÓÜ€z‰\Z¯£\nnÁ~‡× \\¬[kY<-Á&¸Ä? ÒL=*™o\'FÑ•4Ÿ\"g±!„ƒÚZ\nF³º&›œÁ;Æ_“çáxý1¡¬Ã~O	å2t}\0ÕŠ#,T„-˜†ŒÉ!±8\0h)0å‹Á¯[‡y#—²S.Ú¥r¶n½ešx£û-%Ü{ÏŸšò\Z:#|pýu7}åË_3‹õ/ñÛyÓÞwf¦ß…Îq™›«½\\‘ýÐ=ô+‚6½3¼›ª5nßnu6§‹>ÍGØCóÛ·^B}/}Ò]™xŠ|X‘Ô±Îœ½F\Z,¸‚øåôg8¢‡#QALö S2…eþ¨Cu5ûã•B@A|õ‘Åìô)¬!\nc|ÑRÂš&tpîÀ¾Ýs	ÃuX€ûÚKX®{#—0Ï£¤¤QX‘ê´2…§üp‡Î3<áµÄÑ¦LQí«7\'O£èDðÜ«AÀÁ™FqMNƒºøJ’0[•J‘ZK9}ÈJÈþà‹à»¸\\?’Ñs–)Íí[´í?^Â4ˆlç\\Ù™G\"MG¨G fÆk1XÀUnœÑ6ÿ€×¥Qt·8¸1\n¤\n(¡ØVd#óGÄP5LPÞ¸ô2ö3¶ÉˆÌs\'0&)\rpá—˜ªè¿˜2ŸTœ )b	šE°\ZdKSÅò¸½ôÍüþwç¸2\'£<ó|åÙ\"þƒ¶o)!ô$jGzµÈ>\\Ð«[Qk> Sà8  ÕÉ9RÀÇw	y¶‰SãÎÊ(5kTEŽWZK¶E«ºH#²’ê\\d ±œŽNpv™ƒ2\nmN¯âÞ6•«¯N2ïkJãÞwù²‰¹96‘c˜\Z‘¼6BÞâ<x·d¯&sLFR•ò4ÊôÚŠj)^oÀèe¦@)bG:KéKÓ£RÄžS»TdœíÕÔ\Zz™ôû¨»¹2‡Ã êˆF3K¶¥«e<ìÒËÓŽ;§ŒÌåâœ6ÁR²å®5;¬­\\‚ƒíA¥šm\'üÖ€Áß…ëD_3€ï_ZŒ¼\\íÝŸÍ5T†YÚàæ›n;ñ[ßûàŽøéO~žßPÇðÈHÂÔ?øþ4o1Â]¥—Ê+3[ˆô€B]¥5²é+­½ÿ¾?Ó EÑüV.UÒ\"/d«þí5JXlqÍLþôR8¨9ï	‘Ka…ìy* ®«—$%Æ#[ÆZÈÃh&m¿ŠåCÈÍ°.El¹qF4à°?ÿùÏMtêj—p\0€€D,VD³}úô\0o¬°.\'Jà5¯õoíiï ê²ÁJ·ÝŠwå¢¦tÔÜÜ·Ó¬™{aa‡w0÷«ÑOQ{ê‘…NÔZª†ÞX$\0 èßþíß\n\"qãâ›6bÞZ	Ð·ÄEõ©ÙšR¹a+(R c-®÷á’ªZÓ](áˆh9»¯K	«¬ÛÁ9‹ÿM›)ä°û?ù¨wƒH†N„t™ÿJ98\r*åÏè‘€áQ5›HÞ6NdOøˆœ³œ £Êu8³@î8\Z¬û^»?_ú¿\'ßÊ	-š}; X¼Q_4SßÉ\'š¡9ÊÃ€¤æ†\r¶¨é°(	—Îñ…Â¾S_::ìJá8s¾ ³Eà;M!È–Aÿ¾½ü$&g“$t€öüÉ»or1_8ûâ˜r€‰ \\dÀa~*âLD¾H:ÑN²‚×@‚ƒUàYÿýŒc?wÌ§œêß™§žöƒï|÷‡¾í/yé\'?ú1ûþä§~þÓŸýä?üéôãïÿÀ÷“òSÿNúñO|:ÅO?ûÑõá|ðð÷Àþ¯}å«/;àÅ‡½ãw¾?¿ÿíïœvòÏO=åtÅ‰-­ªUs×Z¯óQ¦¡[þeÙÝéR–áëèçÕYn!xGì>º¦?ãÎazÏÆiYMýpót„\nZ\n:eôn½éæ;oeXÝqûÍ·øwÇ-2¡‰Ý©í4èg?õégÿ÷3ÎþÝïàß­7Þôå/ûâpM4ãí²m]00“Â—`2_ô_I)Z¤cæ>VÕZ¬iÞIÝÕ–RÂöÊek//ww6~)\"jÓX˜@<GAé‰Ü¸H:‡Ìj/ÃoÂÔ¤¤óbhlèÙèÄ÷ÄlõÙ{vL™×ùuYFRa¯¿/)aA/0?/ÙÍk0•bã³UÇ¤E¨}RA Èau²ÁŽ%MöKWp˜/ì\Z…1`»²j¡Ìäðî/|àELœÁ½#A1+á¸#kÑwûÅ°æßýÝß5r˜RRM™våX:y3ÞèÆx—›åxß[î¦9–Ñ	M}ücûÞw½ûë_=îø/€òÿzÒ“ŸûÌgó™£?rø‡ßôú7øå¿rì¿xÌç}úwÜ—¾l§?û‡>èu‡¾ù-o{Ë!èÄžW¾ôe/|îó¾óÍopü×àŽ=J<’Õg„¡qdÉïÞ”0rÚ{þ%<ev=ê°Ü·ŠÆeãƒ€8™äE…@¹ƒ(Ae@,n3fGpd§ß½ÍòŒÛîºãNˆïóº;î¼â²Ë¿ôù/hÊ_œvúsžñLü*ì¿å†?ô¾÷¿ýÐ·Ñ-ÒÍq€FNA\"dÃê°4ªÚyO¨ŠFéj£—h\Zž=½ËŒ$¶,pTx«þmŽJŸÄ\nÞc9p\nÀØaz%#,¦Ì°`IË#\0ÊO˜Êã,”àÈØ…¬x]U“Ï–j˜æ_˜ŸÑ¹òýM¼û¾¤.¶„AÃÄv‰ÕŒh515àrÉ•\r;±>@¹±esð+—3PÁ¿Æú×¢Ø9Œ+$Ú¬,›×Cö>Õ¡pÔ¨ð}1â¾€Ôõ¦‹œ\0j&\0‡¾J \r\'V¥Z” º#Âç.èÛÔ—²ò^ø¼çõá#.¹ð\"0qÛM7s^÷ê×¼ûíï¸ðÜó.¾àÂowü~û;@“Õ¹í¶ÛÁÍ½wï`~ÚiÏçœëvèÏ¿€Ópä‡?ë7¿e¾ëmo¿áškï½ëîJÓŽö-¸Qì¨ØÂm½„Õ¾È\nž6;¹øŠ*¶Šò²¬i/ä-]p9„Í„¤íô´/O+Øé\0~$Ý¢7^wýµW^Eì7]wýõW_sãµ×ù÷Û_þê£Gù¹OæÜ³þðôÿü¯OõQmí0í{ÈßôáÃgÃ6£*åÑ‰|êhø­r}ßù ¬–Âø“ò8ÞxF¦‡z6\Zh?Â x6:Æd.q:½ÅÖ¡n—õñÍQÂhšL=K‡ƒò~jª…f~ŒæÅ‹X`ÀŠI„Y)’.,š¤ú¨)§b…|/ƒHf32\\0†Ùèc)°MàÕ¾¤ÏÝ4=DNCï B:õ±¦F dL{Ý «¿Mg OÌaˆæ»°µ?ùS-ñXœÔuôI–&@ä%p5p††™:ê~8\nyj)¶e´–ï¥O’T(AW„‚˜H·¨&V`÷ÔÐËì\ZRò“ƒ[XmÊŒ½è\0þùgŸsÄÛ¶¸¿þ5¯…ì0åš+®ä:\r±7‘Áˆ˜ýÀ(áªË.ÿö	ßôïŠK/³ów¿úõÇŽüÈïý6©H…ÓQ‚ýŽÄá!i@?€uïŽVÜeEà(á;·žß´?‰q«(!vL2\ZòJE}Zí“1¤±`L¡ä<Bõï¬%æTóÙï\0‡ù}åK_þÂg?Ç‡ûæ×¾Î±ã¢ùwÄ?ô†×„øOyÂ_ü‚~ïÄoûŽ8\rïœ•\ná2ùùš‚P>ÓŸ¾øÓª’8”º›»;!Ù£üIà±»»†US­¦Såèëæ¹û%Œ\\Âð«´‚.f\rÑûøsR÷1ºMÚÃbã‘Ubi*Ï:u\ZEð:Ï^ˆé»ßù~(Ú°*UõWJðª?ç)á/œÆÒœQh8õ*.™klø±áB=.}ÕCààð¦Aë\nª\n‰¢+O×´	.}È2“ØÐ˜!¥ñ…)LÀ¬tN‚k’!q¦ ­nƒk)s²!é«+¼ùàƒ_õ²—Ë\"\0}Ø‹¤n¸ö:Áa?í€ä³pDV? ·ó”Ÿ€~ó‹_r\Z¶Ý~/\0‰Vs;¤ìÐÜÿ62¡;íëÞ«Û·^Â°EvÿÒƒð™îõ}«(¡›Ö)µCØD	\ZÑ²ÊEŒ6/Z\rŽA0JÀ\"6¥ïN…Àß”×ø²>R8þüÀ{Þ+-ÄEÐ.ïxë¡<ÿ’@šL”=|þ˜c@´ñÐ¿Ì“çLgøûô\'œr}2;tºžÁP5A\'º×_°òš+›þ%á¼ßy	9v|(ƒ\0/J\0hþä6áNâªÖN³j>EÃ\"\"¤ÚÜ!ÎEºH]Ógç•cp)ÍÍÀ5ó€2™(¡aUc’ÿMÀÔ¾ô<î|–|>—)”öÃq1qÔšÒJ},+¬yOè\n\"ñD§ç%ðË²M(³2ÖHú×&DöÐ<%¼H2Iiç;nÔ<BÉNºNÛ²‹Å7õO•…ÜüöŒ`%’¨¼$SÝ¯Gþáç=ëÙ,Ç›¯¿ÊËó¤\n¤9¿<ýaG²;@p©üäå_ÂÅ\nöã‰›o¸ð.ûãÅr0%Ä’œã-†mµÚâÞÚÀÑŠèÐ²”uaÌd>è!&ÛBJ˜OQLCÃÊ²ëkþ\"a 	¤;°:a1 AÕ˜y.ãÍ7Zo~\"`m”\'‡¤?õ±óÛJàï<ç¹¿>óÜ;”ðÚWxòÏ~æF œ¶\0únÝDÞT]YµªÀ«9²¸ž°ŒCÙÄ‘p#ùO•Ÿ&ëô^m&Àý*—PÇÌVH™Ycª<²ÈÿÆš¨ZÃUˆåuO\\K†F´7’á\"@°0©±SEAÜØaï=çì‚Ó¦ò®„r~ðÚž£Ö¾¤„UGpdæ†S,¯©Š—ºÜˆ¨úBŽ™±¤FÉœâ»ú-<œ†…qø–põ~k…vú\"á&-·ç’zˆ9«Wž²s¤D#®ˆQOæÎ‹!x÷–™Œ?˜~:9›%hóS“—/!Ëžðµ¯=ÿÙÏaƒ†#b¯yå«¾úÅ/…æÒ	ÌLèÿŽ{üÊWð	_D„À¤ôy8ÛNT¡¾¨µ8µôSŸüIˆ°Éñži„°<[ÙË€c¼n!%¬¨Sš¯‹²}QùAÀòA/žXêmÁM?åÛyô 2£Ò%´è—Òå…L§³f1=ÿ ?F¿îª«9y<6ßí¼wÇ=?üî÷ÄŽ”I\'ˆ&½ä…/úélŠ‹VŽäm*£®é“ÙŒIõ>f¸è– ¥O{•óH>½Â0G¦·Ù¢iï\n¹„\ZnÞÒâ?‹#Œf\'nn¬r](|OãoéwÚôù©0]M<2Ì.+yÃ¥xÇ;Þuæ¿Ž\0\nq,´¹éÚÖ¦„-DÆî½p\\B8UlSnò0~CjÆÇÖþÕP+>°hÐŽ…àç‚Â}²Ÿö\Z%ÌO”¸‡Ò[QO½ì§p1AÆ\0µDÇè0 +èziÓŸÕ-)×•q\'ã’‡ëSÏ§‘ÌÇ4@Æe¿{â‰¼EA“yëmÈ@ùÐw¿u\"·\0%\\}ù|…¢Füdà;\Z8ñ\'p)$|÷†àOHf >‡zGÑ	Øäxæ*[ ŒÈÊŽ4º¾!æ œÕ¬	>íñ´š•&xFçÚé_Ú_í¼ï.b¿/þt°¤G_J¿ÛœÛž‰5é¿Ã8L¾ûRNµâ]j†5i¦OËcª§-½Ü[Ø‚žXá‹,ûi0bb[1‡òÌ1:ŸÀ(}ÍGùõô:-–ÐYwÍ*Ž ¿&@Æw¥Ð¹&ÐLZç¿ÿë©|¸+/½Œ— `ÈÚ§	¤×lM!/Ò+1^²²?zÀ’ÉžA†‘¥®íò\nóo³‡Ýd/¶‰ôò<ÕeœiþºÌ?4˜#Ó:‘bÓÞ)FÒd,6©ÚÓlÿ™\ZÃcöEÊáC5†Tw4?®kH²/)¡B£…ÿpÁ¤µwÝEÿðUÖëc¡ñQ;=™@™·i|+u7/¡>i#\\Þ+îuNæ\"Ø6Œ¸½’KØæCdÕzÝV™Ép±u;,J0\'ì%À²Vú®pPg®ßŠÑFui” Ú–ñb#ÌÓŒêxþàþå—^†ÔŒ¾ý·\nýL~yæp²DežO=édá¦‚êŽéx¿²Fyâ°)Çì¬GÜ°\"=§\'ôc^ÉÝfÂ%“?›Ôð³cQI?—®–£MrÕŸŽ´¼Å|w$ÃÙXk:˜óns˜©ÂMè²]ÙŒovÚãÄN÷§Oñßú§ê`+Ó=êQòi­>æ1ù·û7×§xÄEždX<g>ð•¬¯pGþÁ/ÜâõîàŠÒX­””ZÕqz+”fÓ…*0½oÇ=;¶ßõ«3ÎDÒµ&¸ïî2\nÏzú«ã%¼ù\ró~üÃeèMÚ¢—uÓ\ZËVÛ‹æ|¢ŽÍw«2ÅôDïåQsk–n›ˆ¾7ÜýMPÂ0†Yà­Y\nåe4uIÑ:€âJ(áìÈŠbxðzk3Î»†uUA”7¼þŒQªÓ ®JuJ\'l\nIcò2¬þ›­eæe·¡FâcÔŒ2úZ›¥~E4¤IL¤I\'´:í4ÙÒ$î¦“Í™¥¾L–‹kº+%8w*²¼}iÔr_ó¡@	TG•xÇ\nx‘TÉAæªFÆ|c5&+1È2˜ØE.<ï<‘\"õˆ\\¨Aá}ï>LÌ§*#Yá 9ƒ?Ý{¬‡8¹<\0äqÎïÏ*¯péEt¿¡ÌÁ·¾þ\r#T£–…žOÝ6ÕõÏGE‚!¹qÄˆ\nî4²xÈ&ðä¶jjmL§¶J\\œ\"ä\n²½QS½:Å«¹¦/:§úî²G¸£ù\\‚»,T%+·Cü}?É»´\"1˜y¨Ô#!‡¥ºÙãº[H	EcºhÜÿ5_&k=N%\nÔLéœò:þ•é‘íç+.å{ÆSŸöñ¥tø¯{½ôò~0MŸàòÒ{ÄRü§!äÀòemÍéæS.ªÙÑYu$3¬G%_!.Ô×dû1BP\\Ì*\0“ÙÒ4qrßù¬ôœ«WQeè—Ü@Êæø×¼æ Ã?ô‘k¯¹±tBs97žyÿ¡„\" Š{nºhE^™$ƒ\néÞ­ÁÂžE§ú0Ws;;á\ZZOè,Tlm®ßî\"Ô}K	™Þ\rè±RŽdB¥è™ªÁbG¤Íß*ÀÙ\"dpÙAÉ8ƒ÷Ú«®V„úêW¼RÑ‘¬#¼xÿaï‘B€#Ð\\a»ò•Oü‚þ,T-:Áöbblr&ð„Œ%†ðÙ)â¬ÑÁ+‘ì‹o8s³n eùÌ^¯SŠ²¹…¼7;ÿüV@_ŸñEi²Sàò „þ6ô¥åˆ›â¿	àÊ\ndéûòÀ8¸\nâRÙ‰0ˆÂžb Ži(l:X{Üw–¼³)U\"†á\"Ô\"¹bkm+¼„aªÇ\nŽÄª	„\n}a!–“~Äfb{zE_ÕƒñD‡>øÞ÷i#$íÅ³_Â_Úß s\\þÖ7½ÙˆÂŸýd\ZýãúlX×Á¸½];q­2y\ZÕNì«±ÔµÈVð~ÊV+¸ÄåÙ†Ú&l¾Íy	µ~@” ¯1s9—ø£x8›0ó®öbà’•n¨KÂ+ú	»:=5ð=\rd ôÚ×™ÑÇ\ZŽ\0Ž:ÔXaÿ¡„^^·¤|@œõçÏ¢u­¾À}¢Y\"ÖE6u¼þOMuÎ–Õd15íƒ¥å6i¤‹oB-6qÊ¾¥„L	`‡	tcv.E„tŒF\\sE4H­A ’èüéçŒ	CÎuò)\"qÃÜ‚êP¯¸äRxØ˜Y÷\"?¼ÃÂ”XÁD´ã9|‘\"	j.H’¦Vå\"(0¸ç^;—-Æ¾”Õ©\\JßèI‚l0‡äô\"gÀn°X\'Ì†ð©çˆ2±ÅœU‘L]ËÓ‡E‚ÎÛ óI¯8Xúj«õeëÄëHÏ ÊDª˜¦‹d¶§Ê¾Û4¸Ñ\nJðÓx*wñŽšÛK	 e3ylÝ„©ŽÕ\Z×	’Ë÷üè{ßWM¤äTò`Êíï$r”ðäÿx‚Áê¤²~~ÒÉ¡¿,B+ig.éñHØ¥šíëpk~¼P¸Nõb‹ùtË#„Ò¢áø\nÜ1È-ùmT…\nƒ6­)=×^‚rQ„MÜâç‘}m%8WÜé¿Y.!&h\ry|ÐÂ”û%ˆŒ{sÝžmH‰›jJ ‘2i6tJdäåSlŽ(ôÿ4Õ‰ò¨˜À¹öð9D¿½k¯¡î[J)úÔÊuQÖkÉÆÆô¥jÀQä‰.QB¶I#‡cbá” 1 þc’ÿ¸U\n	DØiÊ\n¡†j\nñ	äe„p„…jwôÀi@xE\\4šaÊvî¬Ï2êN%ð©‡ëà©4´ˆ¹^ÑÔÁNä<{ræ‚4€Á‡Xà#S\ZY67\\JÕ}ëu”Ð•[ÊX¬©â¿nÔÁeHÕõÙtez=[<\Z+tÁqÓµœƒqð\nJÈëívOžÐÈ‚ÀbÄÉTgé\Z,\'¼5½¬#gè¯¦è _\rú±õW¨-xÿùÄ\'±\0ŒL4sÉ/ÅÉ\'OÓ\r[/ÛÛ5jêl›\'Áa­ãF@MÅ‡ãK)—ÏXy¿§„ŒƒÂkƒ¼;Ð4°Ë÷*wåD™c[€‹I#ÕXšLã,-M9N°o~ó!?þÑI\rRËÌø¾\'“ÖýïÙK¹„¬+Þ7 àV:d‘JM–Îöí˜V˜¸|t#Ü(ÄÁ%f¯ÎœGæ,bÕ±EHr\ZöÎPµ}K		­×[“Ió\'E$|)@T¯&Cz)ÎàWªŒG©¦_+$.Õ)¾üÅ/RHŠä$e‰ùôÝNãžÊ0…0úÉ(¸—‹V°Ä™À¬\Z˜ìCLþÁÌ«¸cV‚<pþÔ˜óáô$u0½È€,oÑ,‡áxH8dGu2ÌÄáxkBà€Š¹£„«­ÀRÙÂœXÅ…hcû]¶L©­ž‰ZäŸe‚¼€`—ðÈÿ=J¨z0OXžKÑvaž´ŒˆNá»A¼,™æ¹s’ÆÊGf¶ÈÛ+©#^ô´§ü\'‚7vñó$¤V\\Ù½Ø³,ÙæÉp_¸¯ßq×xQ¸¡^™³ÔÐ°l\rì©ˆk´ì#„zå46ý”¸8’²ÊsR#:*D÷è¹ïQ­}†³Ø†Öƒ\"gÎ™èÜ›ßtÈi§þbÌ$Ýè„ÿ=Öïá¶˜–ÅazgÆ,»Œ7JõóÑÕ‡p™?ü>,üò+ŒÀº®X¸™ÆdåUP\\„Ð+©Þ%Ìãû.ß4¦´VAØ¶Ç8Œi¸ðŸé¨¢»uÞSO9ý±yü‘G|ôÆn¹kû1ef¿Š(Ä7uri¬IÕÈ7ˆ<èü@RGã q”\0JŒB&Be)A	«_ºX:¡™ÔàËu×\\sÔ‘±ÔòõfÑ¹á†o|ýëÇ|îsW^~…`¿øÔ/Î<óyÏ}î\'>þqÿ[Í{±]Iõî¸¯»{NÆ&J\0@YÄ>å$uE“TžšcÃ¸h-=Íèy›0ê¶[n¼é¼³Ï¹â²Ëî»÷¾«.¿âõtà+_yÍUWmÇUÛ¶3òï5êå?<èÕ¯9úSŸöj\no®¾âJ«³žô“Ÿ\nµ;ÆP\"‡¹Ú{ß}˜s5›—¦ÇËˆ<•\n ‡å+ˆ”l»Ó?Oÿ¶ïÑ¿Å^Ä\n/až¢¢—™Srò°†‹\0\\t\r\0\rÊ™ùÉA˜ÄÈã}ðƒŸ=úhL¨2UCÜÉW»å–ßÿîwOyò“<âˆK.¾Ø¯>ðÀŸýä\'·ÉHß¹íâ‹.ú¼É?û9Ò&áÄ¯}õ¸¼÷}Ÿ1îÏNr³K~ð}ïÿÅgÜtÃMŸ?æØƒßð&¬t‹ìémG–?‘Æ“0Xüo²÷\nký[œ~XÑ[7Ó÷§[o¦jŽÈ@ëèPô„¡]¸SYAðÝa%G	vú|ñ·˜\Zøc°i_\\\nÄÇ÷¼ç}ç{Qƒ0\ZÀÜá£uadÇï%JÈcp±ñ!>¯žêŠSc6EÌUÓ­Ø	8à\ZíÔt\0\"Î«hZ¶Œ®R”`éåMPÂ^ç­^à¡Ò4ZòÇ‹.{Þs_ô¶Cß%eç—_!Óä0‚\nAÈ3³alÞ¥Êtô¦ë\'Jà%H#³\"Å°‚ÀÑ±ô¥$Ä‹\Z•fýeÆi†¼¨B‹;–üw_†¤Ò=$”o|¦¶«‰kå,_dPB˜ë`×ÑâÌæÃhJ%_ú®Òé3Ÿøäi??åþûîç ¨UÚ$\"‰\Z‡á€o}ã‰S)n\'â0…Oïyç»°š+øSö»ŠŸ§ýÛÙêoõaRª«ÒÕ½³û‚ó¿<ÿK+­—w¿Ùówa†”ÍØó—#K¡B½ƒ-IùQâ8Õ¬”3¡ô”ù‹hÝÄÑëƒâB\'ÿì¤êVù\rŠÍ8äƒõ¹\n\rÔ‹Ê°òoØÄ—]rÅþè»ßõ¾ßýöì¡‡™±ËL¨Mdq²ðj[K	wºËšÛ`‚œ\Z§öü6™­º^@\'… ¤ÓÁ:\rµ´£8½â%pèEGçU.„dO3ß~¤E_bµ§\röj\0ó& ~ÝSö%$y’â%ÐWb\ZLRd$ú¢ÒÃB\"é4ï@4pßOÐÇgÁG#Vñh©6{¦ž¼~.áaG	\ZX¢)JÀïxûa/}É+-/j¿øÃ¤a2ü0Æˆ­‘® ›vÒE?{IìŒDq¡)ÓøÓŸAOƒ\0¨ôr”P‹Ú!Qˆf¼ðï‚óÏWp\"n\0¾µ…/µŽ«ÙãO© qw»%T¡&Ï¹õ¢žU˜T³g(ôÑ´ÏÄ™c<ýé¾ûá; 7™’ÈUÃ#Š›+¶yÑóž\ZR§<Ÿ!<ßýkÐ–èÄ^#Ó&Š™<†ÈðÕÕÇ”¶W(!Îž\'Q½Ctˆ¤N%ˆE°–€ŽÞÁK©ˆBxÏ@÷ëh#ñ›.ñ&Þí`):\\ÂÛø9ç{6ƒ¡J\\+¦$ý@8þ”Š8ýç§ÈT›ä\nC|ùØ/~þèÏ*øå™¿b‚|ì£Ÿ2	O‘îJbZFw1Ž¯oŒ?Ä)aø\nÃK(Ì¨9¢\n¥£éŒ,$æ,³Ici ¾;ï“+Öô”áæp8«8ùQG}LŽZ©Þ÷¦ÀÛÿö%0Â¼pùCˆO@ƒ	&`š’YCxR… 8¸÷:¥ôpÔ¼ð®q€¥¼Øªà	7ly”P¯Ó[ŠîÓŸúÜcógœþ+ßÕaRµ&#£ˆâìÎDó\nE		?J0¶è?èdƒ›óÎŸF·–H€ªW@	ª‹@öôÓNSp1ÑŒÛi”\\\'YÌX•<¸¡ë—ZA	#~M\ZŠØŠ\\f,OrÉ]Ër³^M¦$oá	­ð„Ç=ž%ÛÌKŽ´S®[”œ…Ûl}ªn˜·R‘Ê	…amŸXªÏÌvÏPE“ÀQ¥úó¬ d´~·\\ÛKÄ“aíåÙ4\"[\n­ÑûÀ…CÐÚ\"þ™™N´_Ó³XGÈ;/\rË¾0¶ðJ 3¡Ù’‰\"³¥b¾IšÔö-¿(:0´M 1§AAÁ÷¿óƒ7¼þÍ_<ö«×_wsJHqÃ#‡p\r£‡’³q\rrÔÝJ¹çŸiá#8Í¸tùœc}¤É±›É‘®ŽÑ:vÒgþÜu×ÞD°å–Ó·½–NØk”0õ+ï¡hp¹ºb&Z%à\0î°àƒýV^«Q¸ˆñò+àµ$$ß\n¿¦²Åõ‡ª=ì¼„ì2ŠÒ°Æï~ç‡ú×Ç~áó_6­»Ì’6Øµ!3ŒD”ÀBB»ô«NáHÜ%4äµ/mžýˆ!>÷ÙÏÊ.òÈŸ¹ÊŽFÉu)_ò‡Õ£Ç÷y›·ÀÑ¤ý;§þ¯ô…† ù¡ï}ûÛàidG>V@Z¬ûæm-Ü¡Ðžg³šñÍ) Í’&w–Y1\"×åpøb\"&ƒw6(¡×Àý(ŠJtS×©¸yg…ëÍÜ÷µµMRÂx¤ÏÝÁº!ÙFB\Z•?ÈØ@NÕ¢#pä0ÝDt¨áýuÅ^‚nÅ–âFbôr#e©øÀxCík&@	ÒH¤Ç9HNœfAçL|êè×¿îM?øþOš	ßú(´±©:÷‚—ð é½5ØLàh@Ö l­£vƒ÷å…†ÒyZ¤3¶hJ@Þš\0Fi‡Q~Þ0ªˆeöÂP‰ÄKøÖ7¿M°AÓ`¯ö°,B]Ú$³`…×fï7aÖ(þ‹l‹<øËÌl\n×©c”Ž®é	~å¡QòJôZ–¸}\'ÓÍ^~ØQÂ¼fì¸û¾³ÿpþ/z™ð‘à#í\"“fEE\0I‘¢|OëÐÑE^Bre$cå–Áèõ×\\‹@¿Ùo¸áfËæƒf8[KâÑ¼ùMo2j\ZëhJ&g3º”b‰–3Ü«EB¢ÆÄEÿíÉˆŽ<‰cÚô.1/{,HŠàTk„¡“­ÊyTðêW|\0Ù£Ëu¼›)Ký˜¢C±¬¨Î@	c ßá»¨´©~zâ€Æ%\0ß¾v2ÇÞ „!½ä6ÚKûâ\0ceI*ÏÀ’4òe¤ˆ´‹|fcõ î;—\ZlÑ\r-Å\\0UŒÖÏsjö”@ªxT9Ë â4K’¡‹’Læ,a4¼ãmï|×;ßKëŠ^2DZ5+wáÿvàh(ÿî•k’AºJHj Á\n,-ñRmQC$p„­F$ùG@ßí¥û@6îšÃŠšÒ:ÈÆœ’^þå/7æ¼3¡;êõ×·HÖ7í%/!}õÚÂ>œ\0á \nÏëi™3A‘á\0:cé.z@›ÎB°\rÇ%GB,Ûì×&í©Ïl¦ê`FÅÿ-S²½Ð*«ÓËýÚH}ÒØ÷½ð¥¿ü…‘/wVRA9|ØA2D4>J¨43¬E>ö‘£DÞá¦‚ /> Â.¯P¿â3lFd€\' ƒJÎ™bVþAQ£:†/¼qíR‹h²*>WPÊ¹¹Q‹ö5ºr+µ,·/žryÂ\0ú›•fI\'1MÉ·ãžsÎú>76%ŸÔˆý’\nÍï]‚:÷B*»XççÓŸüŽÞjàËp¤,yrÈ[j! ˜1ÇÞ „(ªÞ‘Í”íI8 Ã:=‚ƒ%‡ÉK1Æš%CÇTõèÓ“ÉMýÊ>0æƒuå;ÓJzù;ñYfž¬p­´\Z bqG|èpœ*¼fL\"7‘)gCªozÃ›¹¤æZH	ñw‹°wÒËJÐ\"zP”ô—$`	¨r³{è<omCy\\z Á7-ÜtÆ–Ó©}¢Éh>ÏþŠË¯š¥7ò4pö+J²½¶ §@pP3Y&¥¯ÜÍË¿ÈˆMÊr$MH—d{6ŠMtµÙf6ÞâõV*¬K	+ôrEYêºÜ¾Úê¹—5ªå9Ï~Á±_øŠB‰›²J¤DuHR&\rp£…‚<ùXöÐã·¼ñM¼•ˆY…F0‰\Z—u `TR×8ƒ°ƒÇðºƒ| úb£ÄBÃÁ\Z%$ÍîÆÍ“£i*®ó˜³‡+Ýˆ§‘lõœ(òÚ_í^Õ8MþÊªdÌÄ`¢Vð$,Ì-ú˜}”2|!/¿,	Ào0Ÿw‘97¿›‡÷«Éà+ÜwÏ½Rèæ|}ï{Þƒ){¶±‘OSi™C©ÚÁ{Ã™¸coPÂ\rß¥D¶$ö¸Ç=N\\KCÆH\ZsZdZ,ß‹\Z0µé5ZÇ@Á=×Ô‚Gß8þk•¼&p„DÞš×ÄŸTBE²o˜aÈÊà×·¾åmô-·€¦‡ý[ÚË6“^^Z¿´tæàõ×dV;›Twßæõa—ïµÈ<%8@·jæ;£ôÜ5É%èƒòÉ)ó7 	»ªµoS?ÇkJ®›Ï[o¹]÷/>×Ž‰5·a–!Òß¬U+ŽÏµYø/§ÛÖêÕUÂmàøN° e\"|0Gc“)JÊdê;Y·~Hã$	Ž;­í¼l	­®][{šÆM8+|”ÒË^Š&Œ«öãà7¼E„÷œsÎ%*5ô|¨AÔùd³(›æ!&fr¾ñõoP*ÌRˆFÅ!JGjÒS|\0#†hJ\0Ð\0â\r¯})êÎJÕj®æ²ÃÙîUSX_&£~Õ­ó!p¿^$î×ã\rìó«ç?pV\'ñ`vÜu·é˜p•Ì‡rIÏé»˜†É3^vÀ‹_øÜçq $¤Áy9æö—&ùí¯~-Û,ý Hé¶Ùz¥ £¨÷ÚW¿¦¹Rê“ã	=³ý(!ã®!Åãû‚Cx0–YLd2	ÜgõE\ZéÞEëjóíD6Ý‡ÓÃQâð.ò¢s_ýÒ—-¦—·—38\"”€	?äZ‰¹	Ó™­DqÚP’ô®wÖTþQBË­·pü\nJØ…áüÏõWÝÙDàèvT–lCcòAP–7„YWª*è3XKà%Y±²kÐL«…Qjd´‚:¯§ä6>œŸ1›õy\"ƒø`~Q²Õï¸UH¾—(„„Å„ñòP~Ø_CîŽA	’\r´œ— **jÄÏ@¬¹1(Áu˜Kbå‚Ñ‚w/Ý´—°	eZ÷”-¤„LÝO\'¬\ZÁŸ\\„ç>ç…Çw<\\¨¾\r›â\0!Rjdx«Â6/–DµªZC— …(êª@m±e¶¹mT8àm‡Ê¥¸\\½&î‡•€FúG¯ÐjÅâ5nÞÉÀúyJ°»ctÉe©c8ÆœB\n(zõ«E´E«6¦dØ¶ÝH! †ªJ˜iYO¸m;#—CðÌ§=Ý3;¾u†åE^C$÷ßs¯+0r=ÿ/N?ˆ•¤\"|É‹_,rÅLÉ	®Éx<ÈK‡ÃñD×ï{k§——¦ jB½á£|\0bxlRM<97±ÊìHÂ¦]0œF#ÿIø…/|áç?wL!5”@n¨TsKÚƒ~ô ™Ìðï¬„dn>øþI$P¼Ô/»d¹Ãú!ûÙ¨·Å†×2á¯Û%gføÚ”R\rJHa9/A£€©X¦H	ÊC3åà\ZB8—ß É\"•ì³å§Gi8ã”P@±Pd ×ÑÊµG„š¢Û˜9Ñ÷]ºOp\\ô#¬)rÍÐ®ÙP©ÍŽ6¡Lëž²å”à‚ºbñÜûîý³eùPÂÁ¿Q¼Š¡ªIhÜae(\0)(¥J•1t¹h%ˆ·@|p%TÔû¨a}WûŽúÈQâïšš·f\'ÍÆpŠêSú¢F=ó@í {(Ã4/üÞÀŒò$+œåtCÜµûË^ü–©›6Q’òGˆÏ®—ŸËRk–ðÀgV†1M“\'WkÄÁ·ÜtóMèä¶Û‘‡s½&üùþ|¶°ª;àE/â‘Ãìòcè!¨eVWê¶ÆX{\'—°ÂŽFð²üã›\ZðÄ%8j[GòB	L%oTnß§ÃL™×z…^–ðe}>÷™£’iVcÐ”êær¶P¯‘ÈÀ¼˜@IÅB/yš£?ýY…óùƒÖUÿö_Jˆ!;o@¥œ$A1%*]ú“Ý#íÌ>k.ÃÅ\n¸¡V.Y¥7	ëí\\ñ‘á%S¦¦øPÉØj	[”³_Ø¶(A‚”oÔh</¡iñ¿¹ZÒkx¥IÓœX‚aCÕÖ÷Í¿…”PÒÉ?”pï=j˜»ë+:zÖ³ž­çË»@XFV¬ÂCò±§ŠÏa¶“ä™§n¬/,\0\r(\nA‡B°È\'ƒQ(¹’P?û…cEœš¢7ÄÇbzB|Bþ¬“ ­žŽ;öÅN¸6M|ÐAÚÔ#9R#FükFÃK^t\0pÓÊÄ…ÜºgMÁÝ0:³v£\rQ2\nÿü÷ÿ © IÐtLM¿á\nŽQV‹ÕdGõLÑŠêÄë_÷:áµt¯g‹<JŒÍ\rr@ï2™u{%½¼Œ<ÆEÀL~Þ6GÆˆó§Ý3ÿyá¹–BËêòðÌ)Ã,¼i»äÒ^(_òdòÙbGšÐ.‘ƒ¤—\rXC	<ËvÊ¾œxÂ·\r,‹P5ÇªQT±QBþÁî	³‡¤ý›•6cÈÆ\rS	še[4‰\0gÚ¸æI¥Ql q\'§KšbŽ©ÕnÝÈÐì×#Fçl%\" Ö\r=fÆåCvàÂ«Â\n^ #÷\0[T0ãx=¹ÉîaPFÜÔ‹Öª¶9ˆ_÷¬­¥„Üvêb9ÖÌûïûó·Oüþ³Ÿýœ¦‰Õÿ¹¥2ÀZÕRúk…R2*ˆDçƒ¢1à’5-7+”ÜÄ(àŠÉHÕúµ9$¾÷ÝïUÎÄÒd™º\ZMkr}Ñˆ>æ+dm„õf8—aå\'ü<a\ršóWXß[¼ü%/•Kð\0î‹Ÿ¸/(ALc\0}–>³¼8{ì£ýïy¬CK\r7Ž¡ú\"ÿìÄêkÄ¸ÿ„kZ8èío{›¤‹ÙzzþBjÞŽ—\0:c‹Ao{g¨Ú\n/l11{H‰‹!YIT¢°’Ã{ÐÐ-d[´9ˆÆ©5!3ÒÕ‚\nŽ9ú³($ÕnIº0\'Œë‡ØJÑ`f8\0ƒ*<;õç§ÍGŠt7ê‘K((%Œ4D ¬Ã\0f¦Ò˜Ä×þVÖlçÓ(Þ€`øàl” ±XÉÚHTp¦lÒªû{.ÁºZÝÈdHSµº	·¿AÂ€h*¢€â¼ŽŒ®7š¡iÇ	Ñ1íÜŒÓE¨[X¨°.l®tlÅ]ÆêªOžÞ_ò’—rQ9¤dp©~å\0îÆÈø@G	SiÍõ×CFX)>À¦ªõùöÉ{PØ£&pà	5<Fü²ù¹ðEƒ¦åÚ‚áÃPÝÈÁÎ\\’l»ó=(ŠÅPõ„…˜ò\\ÍYêðxÁh\rÏ†–<Cá¬3odžge²Ò	\\„ç<ã™ÒÐ4-þ(sàŸ<ªò$¦î½÷ÜkF<#T\"2+fËª\0ú)aUR(—Àjk2íGÓCî•ÑËË(!EÛñ2àCó[wvÔºÈô l@üjÓ.l,¯£ò½D7\n/Ö—OMÏ90Ì[)OÑi|UF¤Êb°Ä‚ÔÂ¹gŸ7Š#Ë*ëYŠ¡!éåôsPBúŸê\nc˜IE‡Æ{ú–^¦HJ‹hu’ƒš–X´\\|Iq`S$)…Úÿ)æKàùŒe‡|òÏßÿþüèG?¶SDx&X‘q”pÍW¿zÜ§?ý™O}êÓ„¢îœdËEh†q CèUw1„ã^L;AÏr/á¡I	+JÊVPBæ˜~(vdÄV @FÀü†ƒ,¹ŽYµ¬Lâ·¾õ©O~òÊ+®NF	¾›“éx\'hµ$‘´üm·½ûïxýk2ø@äý›_ÿÆK8€_¦IDä¹¾W^v¹±gœzª`×Yá<åç?ÇÇìPîH1\"å.­©‚{ŠÿD}N^ÉôÇôO[Ïî~‡9-¡yÐk_k\"Õ»ïšªÂ\\“\ZM3wÞõùcŽyþsžËnEBŸ¡\nžG.pß¢’B@xQ=úŸÿEéÑ)\'MóYBšUMªç¯`I=¥g:îË_ñ.7\\½9Vqá§>þ‰×¾fª8Ò±‡½’‚ÁM½Ôèe´%”0œ½ËFEàîm·,ó9Mžºd[á\r,ûÉãÑy=‚Õ/AìXAøH­‹‡÷¨òm€‰ý%\0&l!va…jîEž:N“‘€ˆ–UeÄÃý¾pÎJ!p\Z‘>qÔGEí0D©û+.»2J Ÿyë&´Xå%ÌZ{£•&K—/…²wßvñ¢m;Å`Ï<S‹ ê\\çl_Z¦>ò‘£Îú½²‹{nºIàè÷úé;ßù®K.QúÅº½ùÖ[nw¿¶\\ù-Œc\"ÝLzy«êS÷RÅQÃ²éMÖ„©_?räÇûê7*£´ß+_‘?æs_4æ›\'|G_å‹‰5óØ’<2ºNÖÕ·ˆ&)ï-F±a¾ð¶-Ú·¢ vX¯[‹²¹»,{€/}þX6¾ÒL6uKÜ¨ß )f_3\0…[û¬j¢)¢rË-,MõˆÌLÂ$ç¿Ü°Vî¸CB‚N“¼îYx!ÔÈ‰ãù‰äÙûb|aÑÂ^dçà4öýÐErdƒ‹ï·²BNøSº\0ñÀ½·ðFß8îøª`ã±¹,­à‹X\"T¹„üã§a>³	à²÷Û¼W6>””Ù`)ÌF_üâsw*ÌmóSãšW£Øf\n–=¸ÂìXZÃ’Þº­XœRßb€Ïå¤w@yìåáùzÚö=Vn¾ÐÊ„Ó;r%™¥Æ£I&k^™SIƒøøO4G\"A-/×þ¿¹§\Z°oÙÄ8žuëeÖ­í/›š¢õ/:>\\Û\"±A<Ì\n–¯I	Q‚‰Ä?ðþÿá¬óÊ»üþwçXzè[ß	#ÔK.¾âÀWôþ÷~á—”ÛÄÛ^7j½—(¡—L\n>/½äJóµ}æÓÇ˜Þ¹Iþ2{•Øê“ŸýÚñßüÅ™¿44[H×§~+p$ì[\'—mæ³J–þ%ýÈ¦„3N9ÍTnL<™Xó_žq&ËÎØÔe”À`\'FÅ\'œ0(CÈµÚ3M5ï ž/ô\'j #	Æ>C(¾À Æ)7N¹EÁë…Û´žÁÛÊúVÞÊ–/•\r÷+\'Œ+Æ\"<þ]|áEÆ—a2ØŒ:\r0ÿ§Õf”†Kd>÷èX¯|Å+ >fyÍ&æ™CÿL¶VhàYÚiiÒÆÖ18¼×†\'³srl1«ç)áAóU{²ú}zAýâŸÿùŸ™ÿÂ°è­Ø´Mh‚ß0¦½s¼–•90_žÀQ.‘/SÚó]ïVkDø¼1^‚€¡ä<ÿ€ðåü3J1_‘´„óíÎ^s´ÿº|°\"Ðú¥„x—\rAªåŠ‚ŠŽ¾ò•š;ö¬ßŸÛ`Žßüú,”À Î¹çåŸ~Ú/_ñòWÃ½+.¿&ÇëA	1dŽ‚×æ\rp>ôÁ#-UhlÒ¹ø—[‡ÚTn¤Ã8•…,‚P2\nŠ%Ê-Sqæž_ËLæ,o”¬<²)áš+¯65ÙLa¨ñe7\\{±ZR¯ì¾e” \\#scz˜% ižÌI:&\'Ð£þÄÄ<3a\"Ñ<”PsÀYpÃÕhò»…Û´|Í,²?&¨€AGý+/%´_\Z@	™PÍÑ¹æÊ«DxÎ?ç\\Q#ü	F)‘µt°übÛZOØ?I‚ã¾úU .–¢ê£ˆyLÐ#!9O.ŒËˆk»WSæÑàƒ\"]¨ûÎG‚cÃKxˆPB~LèÇ>ö±†ò\08Ó\rD°7ãìA	€É»ãlÙr%åÉ5(¦û[¥Ú…¦!¢õ¦´¨,øÊ%ØcÐ8SCòy;Bÿ+%Ì4jÞKð`%ü\r?. jÓRZáU¯zõûÞû¡Ö?P¬eì÷ÓŸö¬Ïó¥˜1|é‹Ç¡q¨8ŒæMðè²S¢^‚ÇB¥¡š!ë\'?>ù°w¿ÿ[ßünµ\nöäC™Šýûßû±8Ð€!ÉÜ\0›JWêÏK>1äÙ¹ÑOÙ”Àfzj$+LŒ`«<Á2J LÑ¹½èE,Mñ)2ßgû“jËò……ŒÀó³r:‰M €ð¹Lo“+,¿Î:ÇÆ¤cSðËgšeI\rƒ®0bV]¬ÄæTz÷ƒ–±\Z´’K°BNÕ±ÛïÜvÑù4¡…,‚WfÉ^üÇ?Âw+ŠbHêÑš£¥uZê\']·š\"{XÓ UŒ%2È¼ð¥bhE¨ƒìÌ¾Þç£pß“(*¼Æ‚šL;ãÔO‚BþlñŸx[£Ä/—P\rêVï8ôm¿<ýBÔ6jŠDâõª7/â:h&.šaªÎD·:î/”°lÚ»e–Í.ûw¡bYÍÁÂˆ0ˆZôûÕ¾–í{ÍÕ7€¸«¯ºþ„o|û)O~šúÀÖÃt`ÐÔ|…»zÂ¨MPÅC—\Zj[&8xRüÀŸûì±Í1	(}çÛ? ©·òV@Ã†Pºº¢£*ê ‘8Ë”S?¢¥u†Gx.ÁZc§œt²\0½‰}˜Õº7•¤5ôt%ÜbÍ™cR—`>G™ÉO¶¥ŽQ™‹!„x‹ƒôšAA&S¸€†˜‹·óÍüA]Á¨hñb-h$+ú¨lñÅž®»þŒSOÃp†7\'¶y]x£Hùg=aÉEVýô.rÂ\\±u+SøÈƒyxU7H®\0b;)PR*b°ùç¹‹$¿aŽÖX»¥¹„²ß2Ì \0	ôéf°ÀÅ+ôA×ÿé¥	åHÍZ:”GÈù~§içO;½8B¼QQ„†kQ/2 <ö‹\Zù21÷íKç2ÚZ­kØnmàh\Z#½dÛV˜§„ÔF+(“…ª¨šâ¡í×¾æu¼d\0ý,2Áæ%üì§§„Ôò¯>Ð\\[T1hÏ˜÷~/sÏqä\rÇ«¸Á²AÂg>ü¨sÏ¹°\Z*›ùìÑ_@¤ÒÎh@tÂ L–©NÞÒ]äÎÒ¸Á(pQ¤xcã­›9vsÊ·oÓË\"ìŠ\râ}ê“Ÿ\"U(sÈÓ—âõ+r	BFRÊÀî£„ÒËâ@Ð<Qs \0¨&€#ŠpØàL!Í!¾dOËyŽòÖÝ»Ó´rò,P2 B=\r6F	°FE“¸D‚òŠDí?û÷gy~ÿ18=ö8Œ­*èÄ’Åy³YO“2ÍV7dÓp	õø:ðH¢çàê5è½…ùT©ôë}àîÇ\nÎ\"üá:5Jð<|;c)¼ ~!P†Ò¸5¸÷¦Î¼„0KïÐ²ê‹¤—}iô²–•Š8ôCdJÉJ BÒË<0-e\'nÆ¾Æ\0r\Z&OîÖ¥síÛ\\¯\\ú`\"Ù‹¶\\«Ýv§„–]b$ižõ‹C9”í‹š·øÄo}ïYÏ|ÞÏO>½Š›S~~†ÔÐ*WÌ)»9	?D½„RF­³Ñ;}k3YÄ•Ç’Š)‰µ‘Žô²ïªì[ÏZGF¸Q’àï³d›GÐ=Amûôý‘M	wÜ2á˜ÚÞ¸-á»ôgîBý+Žè~ázÌK`5K‘)+jÚ@	YDí\nÙ\\Ì!–â0ig°\"lÍ‡Ëµh¦©…›\ZÐ±æÁ ód%^Ñ¥°1(‰&1KYýŽVR1uÞÙç˜øzJ,ßtsuGèÁ ;ß%N°ÛiÑ´©ˆùjÚ‚¤ý€¾\nqù§FÕ·ä\Z\"äÝeSŠŒ¥KN¤ZÌ$AJ á†òx~°ÎËiøŽ°ÞÆðÃ‹/ö‚à)hÓv^V}‘ô²¾SÅ‘†ûüú×ãZò$IJ\"p„‰ÉÖHoóc“°1\n¬\nq<Ž\Zuš–®»í¯”°¡ã»S7úÉO~²HlÖ*[„—yØaï5}½‚\Z¸gRq9ƒ<ÿÅ¦²7`‰ˆˆåÓ¿þµoù©JP¹9ô_xÖº¨¸vÅÑ¦×Û:·ÙüC	Ác:õ”3[ÄÕûK¨4°†ÊnÕ{K²Oñ¿žr·âUÝc\nÌ.²Ö¿uåÕ´BëþÛ;w¹í¦iFkö%ôj]Z™¹øŒ0WÝ‘\Zs}¾µ7\nC·o‡ïrb°ž¨q°X„²¢Æ=q\0‡ÌA!ô Ù€L×i<ä…5€I[,óÁ§˜Þ¬ìuä–}ý,}XÃKC\r88Œ—\0‰nºñ&f©/\"KRòÌ­vP\r«#[¼Áð´VžBR*gn¼‘n0ü=gé2Á\"2£\'MXÿìôø%T8˜‚œwÂ­Kú?¢Æ›!¿•E¨…°*Ws! L„ÑµE~ïâ{ëÔFrŒVÌáPB¬úÔ‹^ðBÕ¥ôÁ?Y}šC’Ä.½œg¦NAdI|O\ZŸ7\rúÛ§^Â\n/|Ý.9;~q»¬ö–é9“‚Wª2˜ýêKÞ˜Îò¶Cß¡õš«¯¿ïÞ?	ŒâãŸyþóPwÄê5!<3†€…Ó+´ñe¯³nˆbËG›£„Âdå£\"cQ+¬Ö€ø(AvÁ2~„‚lÅ1FÞkTY€X(INïÖCŽþJ	wo›ëJ	>÷™Ï’4¼ˆ\Zù§3óPBs5ÕÏ¾ß±z\n>H/£Ö¥	2ø2s¨>dx¢šN(é\0i[McÐ¯žƒ$ä–E-šÀjYoYF	°Æ0(Ï,–\r•”™Š%˜øZVùûßù.JøÃï~»å—o¼©Ä²Çn–ï`” ÊÔ,­Ü¡ØFÐÜëŒY‰ªTÎ^V{ÊQà÷µ{`fwG{ $\ZeêMý´%L/¸|’ÎåÝxË(!!†Ö(ì$íUOÑoÀ‹¨¢a´¡›=Ü«lŠ˜åj#?DøŠ—½\\ø®ù?Po¹f=á% ¹ªl–È¹ÛäOü¹¿RÂ-§?L(:#Žª²«t/%ˆ…\\rñå ñŽøðG_þ²O>é´ŠP<âc¼2‘ÉG\n%\0ý1>bð‰äâ¿÷ÝM\",úÉO-¾fö¶”²±²—l” ¯ReºÞà)Ý›rë½…5¦Š—G¶—pÏö»¹öÒ€Æñú÷ó“N¾öêkÄ^x	º=/Ad %”Cs³Í™Ãâ$Äw±xÑö&Ì‰D–×Ø—Ó&HÍm=Jyy	ÎŠ­×¥•BbÙ&)ò$ÓÃlÛîiU r$B”?Â©[B˜\ZÅaÍNQøˆkTÞFSò5g±áôHôW¬jöË4‰–ï>‘\0¢úflW,~¬,Ô‘-~¢¦°Õ†—ðP rným‡±$HØL*[<pµ.UiãéÆ3{f}„	%Ì­› ’pHàu¯y­xQ#B|ÉK¨L‰—O:Ãc°üj¡9b¿õæÛöaÅÑCÙK(iÌÜ%ˆW-7ôÝï~Ï{û€42ˆóéû>äøãNÈ,&Q*ŽT%Ü\\(âaæ%ä4±_”€$GC’’ÉA¹ŒKÀ¢¼*YáÕ\'¹ö2œ0HÝÖÛy	l½*P³þ*EÝÄ´w{\'¤³wîrç­·+Ó´Ø¤ý3žú4©Wß¹¢ö“Mmréi}±»wJ`<ƒ	f5XZ©GybÓjBßq\02&mL\0Cá©ƒ¶Ã&{ä-Ù¤Í4µ.%—PÅQ•Hp!3Hd\\¬WÉÃð¯i­·ŒZáG¬Ãˆ„¦º›xbVWÊ‰å²M^\'|§M]GUì”vbX\0Pl§ÐY…<-ËØñ6¯ïM¡íî” Nu}§~+½bÛzÕ¥(Ð+ ¶†éyÜ ¸\'ÏÜ[ûdÀÊ%àov+ -ûÆ7¼Áô£•Âp+	=P!ma ›YÛ¿1™à>ªö¥„šƒæ¶)v¤GÔPëð!½|Ñ…—B¹Î¿ØÊÕ\nÔU\Z…`aÌ‡¼åí•Ød7o:—ð0£„^¸y²¼vÎÑï~{öQùÄ—¿t|£ØÈBe*y:ýÐ‡‡ûú$¥×{Á[Õž&Å£âETGìhêöÕ(ÿÞ¡„Ûo¹Í’òæµg>²M‘î?Àµß˜/ñ÷€µÙ#Ì5d®YÃÓ|ÉKW)æÀÿ²k‚h|ÌoN>jè2ÛSéÍ„ßœÀ·e#õ¦ ¿9Mó\0Ô>rìDcbJ,Vå×Æ<G,ù„\\OxÜãQBË‚šZÑV¶œ<¼„àžªD>˜(ªC	Šúebsröýê\\‹)ß;½XÍ¬ãï{Jˆ·*ñ5Âj^÷ŒZ8DX¥Õ,Š‰q&8\nš8Îãy—`ÆS’TÄEIä|![Ž¦¹ø\Z\"ßÃuk™Uÿî¼}éŒ§›è}ËNÙ;ýes	ñ^@m„òt¼1xuÔG>&—`\n/«Ò¾‰›¸ù«Àò¥/yå;ßñˆW@~qŽ!Ã‡%xî’\'Ð¿wðæýà%ŽB£~U}dà†ê]±#Ð˜ ’h5ØÄ@	ü| 0\n¿šÕ+_¡ò»G¸—p×Ûy	æàéÓ#\0ÀÊÄó©3ËÜ\nÍ›µ¦1À­7)‹‹nÇ„T™B£AªêM}glúNøÏVñÄ •Òk#É´ò\nƒ{qÂ2J€þ‚Ôƒ$0¹¥ðÙëd•C¢òá8#†ð‰$X¸¼„VŠž–ÍP¯Oz¼•™–¦ûþ§?ý©¥fqÚ‰áðª«ÂªŽM£ãeA­ØK”Ð63÷1%x5²ù4xZ\nÁs\"\0í‚š†Ø#26žÜ‘ü\'õ0ÌXQAPe¤ÂGŽ8‚‰nP’ü-Â”vbUðÕð„BaÃÙšjZû¶}9.á¡ì%Ð3(A†HÂÙ}ô0\rÖ}Þ€ê£o|ýDË žqú¯¤žöÔg\"	_üú¿áƒuKþí¥Š#o;O	½¼ûÈtR^F°HvøHGm–€¥§Üü÷¦3#qn/&q?Q÷ùy7íÙ„L÷Ž=²wîb\\B_…\"æ»F\0’„‚EŒ>û®T¡	*Úiæ8³0*èR¢/\0\rJ ÐŠPpÙW©f%€ÎXA¨\ZØ¯-PÂ˜Žp]/4yQÅH&NxÞ³ž­(^¼hJ€K\\ÃD\'³*ÕfÆvpk½!6®8ocu|qã¬6„žéaª8Êb°ñw#äó2­·¼kÕ\nYC	Óš\r;)¡JÖ‡%Do^Äg–Jh2Í¤ðJ‰Wh)³QÅpu eŠ/kP7DãØÏÁ<áÔ†‰´E5]ä¯°…áæÄ6þ£àÒ”òÙ§£—Ê”Àiì!ká\r²œ>ñ‰OE	\0Ðê‡ãF«	˜‹ðÔÿz†)|”Õ˜ÎgØû›À±}L	•íþo…çhÆcƒ@éó4=20Ñ™qU—\\ª>DÕàÅsÏ•\0šEà;fÆVMø¹Ï#ÚËèc¢ê™<Â…Jò6Ž÷“¾QÀ—*ÊWSÂ²g^Ö+\n«V\\jÝ»l­Š›\\zÂw|íøã_ñò—ì£5Ë4Ô´óš«¯6µÕÈÓ‚Rš}È ¶7ôºŸÎ¦É;ák_7+õ;=ô²‹/Qç#î$ ,ž2Õ&Ýu÷O~øÃÏ|ò“ÿØÇ`\rT¢ôÑ\n:Ñ¶&¨-–ŽrJú7¬®¿Aª@)‹E»ì1Çµ[ˆTÈ.ð ÆšD³x‘¢¢C…M€›oq¼`w‹-;LRZõí»ÞþŽßvÍª’î½{‡Ÿ8O*Y•-M@Bº¯ã¦é¸ï£2ÌLìFç‘ò@	<Qc»Žz#?Uñ<ykÿ+ËµÖ¿Ué‡üã|´dt<pËmzNÍOòj¾7RÏ?q¡äŠ:;@ŠÜfBÙÙè6?}çÛßžúÑm·a”7|°R4Cá>\'‘pÍµÓ\Z³5—î¾ëÞêÈgÛ„•›Îˆna¬iS—ZÜ\"J–ÿ+x¸`+ê(ŒAÂx—HÉœ!ËF±À¹çžâÌŒ}ÄG÷ÕãøÙ¾0D^øÂ}Õ$ð³Á\"áæÊžÊ4–\'·a¶¬uŠ­€Q¾ºVaÐ¬{(ÏÞg“Â,{¨,¬e©2åFÏ-®‘^V`G/	=0f£„ª\\Fòp»®ëR­¦„u¯¶‰ã7¡â¢Ð¥…ÂYñ:³”€:yR8\"b×WEš¥À™ZŽ=Øt L›BøXè\0hšaÛ|mDfjä\'>ðþ÷ƒ¨Õ„§YÙe,Î,Üšö.$	D®5#ž=	jg§#s`?¶ß§èÐ-·ò\Z]%U.!vd¹PCÛœ%jå/E2ìÜÜ R&-Ø©´©©÷Æ¸9£ÛÀŸ¨:;£¹¾éÉÐÌ&ˆåø÷^ýTôiÙk.Ý¿ué‡ (å¯ö\ZñŠ\\hk&j#l­»SÓ¡<<‹Šs`t‚Œ3‹$,VÄUx–W!\r“iÎÄÓ¸¦ôPc6Ö„™)Û6êÈGà›nWäß&4öawÊ²ù´ƒ5B’ð\nC71pC#yÒšI¤lôJ7nJŸUBËùñ•ªžÞYÛî\\µ\\Ä&Àd­SÖmŽ¦¥rfªLp‰H±Îä\'\r>ÈÉj>56Žv#\0¹BÃLÑV¯6€–eÊ~É‘O¡§Z£1èK3Ìk	¥ú¨-Ì}­{÷ÍU¡pQfúÇNÑíy&p?‰5sË&ù¦›¢RÞ€	‘Ì d€’øŠ³(A‘ISæµ0„õÝ‘‡½ûÝ¬Q§³O5\ru×\r(g€¬G\\‹¯5ìÎV~jÐµ(=EŠÀwžb$¼2-}sÝõ\\{þtÿJ•p˜V.Ùhg¡$ôfŠ¤f_0ûÿ%ý‘‡Ê®7OƒÀ”Ó›a‰›‚î½çJÈÍÇ xŽ\"ÑC%IŒ;F‰L»Âªap5Ûç”P\r ‡òž1{~Öh˜‹é#¼‚fùŽ¥ÍQø¿þë¿jµ¦/G\rªÄi!TÔîõðøÇ?þÑ~´ttEÞU¾²Á¤û7VþÚ\\ ãaÇÞ±ÔK˜ørûv,«àßiDdA?U¦\"c¿B6é7€³i¢JƒEH¾‘žùÙSˆ•.©”ÙDz]ðÙk”°‘è#¸V4¤^¾J /*NLÊ\'À–:_òŽi9ídðb]ê+äíÏ&þÝ…¦Žº|déºry8Rà«ÌF&€Ùû¤\'=	+4	Ùø	õªê˜&ÿaV#\0³áCL£Ã$þëIOŠ‘ÝUŠ.¦d?è¯T¾QX%°qÀ£RšGK¹?W¤—Ëñ9\n*[¤:ŒJ‹˜ü(ƒ\"%Þ¸³fÌjwcÖNþéÏÊŽ7¯àÁL€ªÜVt%8‹¯#/*•\"ümÙ2ÞwØ{ŽúÈQŸÿÜ1RÖbåîûÀ}÷Wðê1Ì“A8|S}˜*Ö!§ Û5×°<#•Áš’Ê3Fúz¬°u^B÷íI´#”×kx	ÿ–+÷“®á\'ýˆƒØcVpVÔ\n\r¯„þŽ¨JcÎ8û§ÙÆ™ÀšÝt:øöi‚2±£ñ¯žµ¢¿<¡Ù3?¨—À®å\"¨UcaD	¬ÛA	ø%¨Öû¸ OÃê¸úéÇ^Âˆû{ß†¤aT“Ï[i³mŠSÛI¡ãLE/ø€ñ„2¶›HýõÈŽÅdÄ=²)Aà¨ª\\çôéO:ƒ¥å©‰W?\'@¶$‚!Üa¿ýÕ¯Ì)dÍl#ÿÌ-ð§å §¹äÊÜ6´­)1ÄdP‚ð˜\0¼V$Ë4ÐY\n”³lÄ.”à^ÒËGô›B‡½oâ„rÅ%sØþÙa#óAyA!Èî\'\'Böç?û9réUÎ¹­BÆ•§Çž†ðqÙßüòWH¢JÖVì™BX3_“ŽqªXm-\n\"}Á©‚Âó”°ÓÝ—£ yLÔá;›Iu€pCù=¤ÖÑÀº/ù#ø­½ŒyeÝýäDT¡*7W@S¢‡§<å)*”È¡Å3ºrhUD;ÜÓYîOÐ¿9J :A66+SÌÐâîô¦]‚\ZB £iŽDØŠ¾ÌœÊ¨²D¦jØï½„Ê²Â†©…@<‘	Aø•÷Ê[çpI”‹p°î*G|t–u<.^ü.£é¯” Ãfh¶t½džðd›&–Íbb–ÚÊœ´É*×\0MÃV!&è|û!o…°ìn–¸äA	á(Iˆõðdû°£ñ·!#Ê™ «r	SS£r	 [2™ƒÂiœ„‘tÆRj ¶Óúûâ\'Sñˆ!ƒ±\'Ç‘x*3¿ªŠMéúèGšØYVÜ~çŠ5UP?ÝZjF³æ¸ÜéIn¼‘ªËiñëé•A	Œ8AahIô‚Â|Ë(q«½„23‹ñd¨šN!$È´jfF(OÎo”?]ë{É$@ÏÆ\nèë•ô\r1 ±ësùUv”¦¶ÝE„sd=Ìü†ÍÌýõ°c‘Õ^B¾8VªugA–7À#=ÜÌ·v€/æ ¡ur	H—„µZ\r4sI”ÐèVo[b9ø 6‹<A” g¶ŒZC¥Ê0‚Š&QYVDó«Xv0×£„)æûÈöfä;¢Rx\r)\'„äåKù­MB€V¨7(IMúXåØ˜Kh	¿ãgahÓE\0nF·ÌsSI7i9v£d°è˜…Œ\"ƒQq$oaˆ¬¢XY_èŒ~ÌÖg^ò\0}šws–Lnið2¤™Ëâ\"öT‰À<3/Ç?þ?QW‰èj“&èŸR\Z£üD!,\n¤‚–5>ÇxŠ„ðDØXß<ýŒ˜9Yåýß§„aNýVZ1fP@ó°^¦MŸ*ùŒ¸q°¾?›$ÕÆJ•$ÐpÂË¨bÌº‚ì´ƒ\0r.ñ¾†ø%ÞÝØaÊë®½AÅ¤aüóÅˆ\"“óÌ¦$ØÌ@Ñý†vBùíÅ®mü#ˆ7ŠSIO,½ìWûjzè+8™-2B¯³”Õ>\rmm«,­‘š¥—Gb¹üqWÁSi±RÇ¾s¬ü”rCJIAy	J2fþ¤»úC‘PŸ¾WÅµ	½ÜDêút›ˆÀ–Ê…€¾ß{Ïý÷ì¸ªÜ¶]U¢þLn%Hy¬ˆ¶ž?È˜0uõÍ_>ö‹2Æ\"ÁPñw0ÍÐn]3³jO+_Î:ø”ËU½.¬tâ,_-Ô@³MŠ ÜWkj…lÉq£¨bràæ¦AÍWà‘pM¢„?ßÿ€=ƒÔÄ¦ü“9ÁGøh\Z¦0·ô`“pÛí¼žözÂà¼\n¿:¸	þ\Zé7TìÔjÏN¿èÂá&ô÷.dEÙÊ%ª NÖ717toŽp1z(¬¹bmMFëÙ€Œ\ZSIöÒ|æÚ:gÚæ§\rô3?AR“Û|á2â¹üÇìþÿ·wÞ²VežÇç›qéZ½cXã8†6\"ÁœsênµÛ€9 ¢¨HZ1·‚DÉA´p	*‚Š«£ó©úÞ»)Ï=UÜª©[ç\\8µjÕzë­·Þðìgÿ~OÚ{³ÃŠ\Zy@nŸP’ó»bÑ¤âQlX•¸®%…{˜e§}îó§|êToƒ­×[.×$•Êí›÷~w¾W(šŸ÷ |°È5­	¦vý¤¬‹*¯Æ-°Ÿ5¦æSðÖž(Lå~©ã‚f<Q¸at’V;ïœ6k™eÓ¨ÿm½­8ïòY(ªAF” ©…(½NH%ßmÓlÂõ2±¯t]_åÕ’#_XñV)Ó<Ù¢ç¦V¨|X#%¬0ZdeJ gšñ/ç{Áùç}ÿÂ.±ŠõYg-ã‚/yZ>™½æ´!·B9\nI^ˆcâ`Kì÷œcÄdŒj)’OV	ÊèM2ûPÅšÅp`¨T3âŒïžAÔZD\" \0(lm‚~ö{A¢BQÔ,%d§‹SITˆû³èÕ˜J	¸‹á¸\ra#¤D—ü ^`®‘Ÿ‰á¯\ZÕhƒ©Ã<”-I3pP‚\nTO¤²¨„A¬Ðhç	«MëY«FUq$~êg?Ûl»:gžSÑ!^»$/!ÿ`ÄŽæy‹ö/8’šéŠìGÚNíùÊ¢=l&&Q«Ú¡~3zP¸¢A= ×â~Õ‰ôP.o¤½€‘O\'ò\\JÍ}›¡ÆJðU«Õ^ö´d›3«PrNM¬\'jåýðÇ´˜rÆ4æi0Iƒ‰Ø%ëE’5žm.(­Àaì÷éÄ\'‘AÜ@†ÅëŒÇš$ŒBy>O.a‚D­wpIÓiJ1Ž‚¾ƒû‡«1)CÝÁ\\Â\ZE¿\0ó^)eX_$4J€\\N|£c	H‹ú‘4ãD¸¡²­˜Àë¯ýë´biïõ¨£„ìö¾ú×Y¤ÚÊt&1	ûÉŸøô‰ïfýŸ#@÷‹¶¡•…Yuõ¢á F^‹œŸü	 U=¨¸µ×\r;0L¡­”ÓÜÚewYå>Ùõ V}ç%_â„pÊ¢c@Ó…ÄÚÚ|°LÁ«$6–Å¤Ý\'“TÏŒK˜¥JËÁh	íÿùÈG©sÅ`ÁeQ«œÀô²e†¡üd‘µýÆ[Ï}Ö³Íiñ‡Û~‡!ð™šÚ+/»\\ˆid³gÉ µzä¢ÍÛƒ¥ÊEE\nøºs7œõ\'2F’dq¶ž¦„iÆdà±ûÑ`·[b‡6h<ÿ8‚æìPØ­S8L¿pXBÖ½¦ÉËq0ÜQqPº˜’ †*Çú	%ÄˆE´™h*Öó˜ÇP¡‘Ø›^ôö±|z…F­ÖÚ/»ó½FJ0,ƒˆjðUoòjŽ)]3%å÷UK!fq¡Ö)ÖÅy	¨\ZÐé5ÀÍàÍÔ¬b\ZëÁ8š\'ý–Ü\"¸æÔ,¸ÑL×<_|à…3Ù­L›Övop\rkÅz¸ZdÈ€Ž&qŒÏ‚¶\'aŠåšG%èu†Y|ÃøæŒDºïÞ¿xs½’p!Æú$´©’MÌ™*O˜ÀB¤XA¦bPðšånV@.•¥¶P;õYlG¨Ú$ëC\rÜL•5¨Ò”ÙMå™G„Pó5ï;jUÛâ%ò8ù_þüö¾Š#S3).²VšÊ\"…CˆáoZ#o·ª–ô¤}˜7#Ðtýu×#ì…6¸xBÔK’‰¥=‘\ZãÙ¸í_]ƒO‡IP#?éiåžwü	<w}˜ó®—éUìXsÙÑãµŠ‹0iŒå§Eš.×\\«¸ß°IÝ!µ÷bÑ{iSÙK·* ¤]ØF5´%Ô3u:‰jH”ÀƒÔ(xùµ&c±¢„¦}ÍìDâ%0xù¶Ó«iHÉMj(^Yzè½‚¿»1þX#%ü~ÂÂ ÀìÚ5> 4}JÈ¨™¦üÔr­TK7äíi\Zm*íÌrò	ÖÖÁúÿ¯YÅjhÁƒßK\ZˆiÔoØC­Ù³(çŽEuBªÉÉB\0BFvŒÄÙNåóm8Ò5b-›ã„:F‹¯ dG#%Ì–\0âÁF“î«âhÌ~ÃWØ‘\ržå²¢|TPè`²\0Î+^)bÃH7H10·A-J\0ÊÊ7KÃÖŒk¦:â¢/Ê¬~ä#é¯Œ\0ôÄ!˜–HfkOd\Z­žÉ%ÌR‚ú%-¸ß(ƒ=ü¿ÿa+#>øÞ÷ñZÜ0ÀR¼‡ê£äÜá‰‚@“š3Üsz¾®þù/nþÍMÍœH‚ë |ÖÊBOÊSZ0”ÁÙL~ÌsžcU2ÃtEZ†ÑG¯ØÈ¬i‘±Š8Ä\rKÃò”\0zÀzpÓåæ(4rn˜¡_\r¬Èë`zÂØ÷G¬Æ×ú\rKÄm\n¨tœf¬òtLTƒ‡e±bwQGv®¥9’+C/C™¨¥Q,SÛkâ\rà\08ÛBÁîCÄK˜Žú80\"ª0¬eC˜ˆñ›…:¸Áª\\`pT«\rýýê0¾Tµ…?rF‘zãä3­þðû¹‰™MU[Füe!ÇÙò%™!åaÐ÷à>E.N€¯Ž©Hœ—€3„ŒIã¹öÊˆ•6ó‘µ!æ\Z?D¼„1£ú¬]vÀ]˜ŽK ä2`ÌÞ,”0aå†Ô‘÷\0#ØË±9…Ô\\0jHð3žú4ºh;¤Æ\n‚ïûï¹·ñe—_vU$5IU–´ˆÂ¯¶ó¬sõ&}è`…Ï¨ù)—€Xî556†xß»Odøó	ä-ÔžŠ_oE±~â7¨[8†›Ýq§Å¿Ü§Äò­NÇ™Ä\r‘0ÿ¿ú•Ó¯½fz;þ˜çZ‰\Z18‘È`;@aæðùÆ×¿©‘6ºÿR|îÎBX=¹© \rdn€\\§«¸Å†ò­m¸Û¬{øvŸœ6y~¶›i¥ÑA¼OaC}Ä‘ÀÝ`l;qÎC*aZNN?â4èe¢Þ\ZT¼‘?AI\Z¡¦•°Uç´¯!(4Âi-ºüzad½g[£—Àá«+;ä£CÁ}.bnrò†ârô2 pÄü¢N\n|%sÉÜËêJ®¡ªËš[ƒ?ø½„ {‚Ó*·BfL~ÊÇÑñ8\né1a1s\n>ðdE½)%¡3i).C†Þ.ãÔýý@\'yäÆâtuÈf«grg³%éVsy>ãÏÐó©&\'=¦\nŒ(ÊO#ù¹OêÓ„h\Z049S.Ô2É1„º£Ÿüè¿n¸þ†óÏ9×bgÊU¯Ÿ¢½w­@×™–ñ½O8Rq&\0]@	X§‚Q÷`C]“ÅDù\r¢6+*OE	ò \"É$*$i`]¹q¤¥5ààœ3¿‡ÌÀýUW\\yÛ-¿Å(þË·0Â‘(¡çâŽóÌgö¹ÏáËBmn¥\n>êçcò™Ø%ð\\ŽMŒ\'Lh _AóIfjG=HG§ÈÞ²‡«Íùö2|§êFçÑ§Ø°v–àaéV4ª—ç. MÉ¶-PžKdP´*{æE¨hnÕn0kX\'¥Ö‹ãk<Û\Z)ýžEKÚ²PâØì}ý•6Ï]]Ã6“Ë‹œE¼ñtÓ‚‘!ÁBv°üM×V·õ™éæìFJ˜·œÞ‚î§Æ<3“\0ÂÔœOw}åä*ó /ú*J¸ô^œšmƒÆ³_‡ã©/wÀq²hü«iœ³òfï™LGñÙØŸ ’¶¸]{ºùaz¯[ƒ»üÜ±‹]±Ñ!{„2~“±¥S+C\'Ÿ®ð÷6ãoá…„$\rs˜^¶¨2Ë‘jÒË§üŸ\'í+§ß*<rç]¿6á\'N~Ñ^ð²—¼ô½\'žøóŸüô[_ÿÆ‰ï|×IùÈ)Ÿ:å=ï>ñô/ùg?ù©¹3]EÀ½Áú´\r’¸ÀšCš§™õ\ZNU[s&²“¡aYpàŽ_ÝpÃ§Nþä^÷úëö]kçÔÆ¿ÇaÞÅg~÷Œ}ëÛN8ö¸Ž;þiOyÊóŽ;þ3§|Ú½½ý_Þ*+àxsšÞtã¯}Õ«=æg¸g2^áNï?ïßËM7à}ï?þØã8\n¾ªM²€Äûßû¾œð¼‹/¸ð_ÞòOÎö¦×¿á¸çëqŠeq#†â]!&ãÎ£µJá¯ƒ”0Áêíß“B‘mßr	Ë½­ëN\\É½Áá‘B\r;ýâñ¼Î/wXÂ·íSß\"JBr£\\mÖX«ãyÅ…Á·#LÅ*þ`ULã´æ@½£÷d<éÖ;L‚+½<Ø<‰Nf\Zïžzé¢•øÀU¶²›$¹‰0¯Mor:‰\';\'Ëôëä}p{Zzÿ×Ù3ÿæÆ›/¾èÒSO=ímoU¾ñšû·wžyæYÓâT¬x×ÕWïûö·þóã?ùë_ûÆ¿6Qù]çŸÁG?r’jÀý÷í¿Ýn„ó-\'}ôcïxÇ»núÍÍwße*Ï+Ž;öø|àCûö©e`FNy´QÂ\nñ¬rS£â…ÎAp-\0åqƒ<Ìâ…IÈ½ _‹Dk¤>8Ã¾ÚßÂ	ìV­uòI ÛÜ½wÝ—;L˜Ùí@¿õ>£¾¬þëàñÓJz¹J>c©ùz@¥GªQ-Mj]³ì*\0Ð¢pRbDÙ#õöœ*_[°|#o8ë^ŽÁ¤+5IH{À‡ŸÄôD\'€öÍJÍv†\ZÂa!½)6íS!“Oÿªî%`\Z>DŒý©›lþÑ¼ÆR©}º/„¢â…£í%î²y=À„u«ÍózÐã>`j\0J;IŠÇ£7>‹\rýD2t:eçäÈNl´»ï.½œ—ÐSt{~j*ÐCßmˆÚÒë»IŸål4Ÿ‘B£–“Š·†?‘¨{º‚Ýæñë2]E3Xc\Z½ˆÐà›q•êœYßT$Ã#lÕîÄÉý\Zoyu¡äÙm«¯’]_¥xiþD¼Ó`uwX=Õ0çÚÙó&ŠÁ‹=û	§êVßüÆ·?ü¡Y\Zó¥/yåÛÞúëÆ·Ö‹…•(€bÜïüç÷,‰ãmÅ—_üüêÛnýÝ÷Ï¿È²šg|÷¬ë®½ÞÒÂö«|ç;Þ£Ppß57À«&¼ê•¯³¶šŠÞé©&ýzÙÁk<~î¸„^Â\n”@™3ÇRPêB·„‰ô[  ÆÓ*¬O?äÆRbÞ8Ã¶!¿Š\0\nÃ1åD<õssX¦o\\~Ù~yÃoJù35ÚTa\'mq·fô!z¨þÕÑ»óÅ^Þª>Œzõ«^ìsŸ÷â¿D@êZ®q5â´…”‹È8T%y«®Þ-‡ÁwåŒ„YsD!~ªÃL¦D`¾¬²(ÌÓÙªš\0<1B5J€VU	»zH1í@_kF	zZ7„\r¯Ü6Rq: æH6{ówrPL`œŸ9Âý%0ä¢øF2%üÇÓØ÷¤\'=‰©ëîŠ\n\rD‹=8¶ˆz\n?E	¸¼c”PÎ T/»Éá²>€i~^ÄÖ‘ƒÂµÊ64“\n\"=®°žQ$½–Üéøt#XìŠÑ­¯tÉÕ®RzþKzZ¡ I§$5ôx¼«Íô¦¹”0ËrÕè©n÷<Ø´†ökjï)è	åafÑpºÇèyùË^i=œ?íK—\\|Å/o¸1SR—¼õ·°Á8³º²qV‡4Žï”O}Ö€¡3¾{¶\ZñOŸrê—¾ø;Ñƒõ–Oÿ\nâ&rï=G\'¾ûý¯yõ.ºð²ì×‘¡Y#Ðþ©6D	õÛ,‘°Ö3Ö€HDmg%hìqÇ	`¯iÞ1ïfGøi°F…)ŒšÊ`|éK_iµkr¿ô’+¶†¿–k:ß ¿äéÛcÜ/þhO”—°Â‡ÍPH^ÂˆPY¢ï9Ï>Ž’Ñ§/¸P¥3àƒéŒzòÔÿ}dµÕR8@üÍØW	’Ó %T¤4zHÇ÷÷ £Î?@ÊNnA!äÍ¸F	 ÃUÅÔýê™Nkå»Š&”U&›žä”Þt“X‡mè=&†Ã¼¢*ÕxÔçs€xŽ7óâLøä6¹V#¢T(Ï £nÎòAñP³Æz9Ö4›w^Âh²Ï(ï64n3Ÿ·àA~Õ<J¨í<O®ÅÞ(@%$ó\Z\"ù¸úE¤‰¥ôœÄ¥u[¼;èjX2ì$gª<›I3Ì¥„ÇÝÌÔ¦\rÜKn6RÎ´ÔOdÅK6àFA‡åžRi@$žqÉÅ—]wí¯@ÿt½Õ‰í8‚!ûï&û¡¼Ÿåûú×¾Í\\Ã|‚÷¾çƒgŸu¾õ–o¹ù·×_÷kr×wÿþwt0u2Ü\"Ìx‚…×PÃGðµ¹Êèå¼„¼óWÃh!ž;)«W©OÆØš\'{?ÛO_Ùw0ÎÁ(¤åLkÝ ³1\n¶¸âò«ŒØâÓ½àù/ÑßüÆwŒçbÖZ9üúZ1TÙÁ# ´ˆ_ö*´Í›ê`2ŸüÐ\'þ¯\'S&AI«¿$“€Œi6£8Ãp0Á€½½E<@ƒƒIJB\n/8OU‹Ôa†IÕNH\rY„•j‘,}-”þö·¿…V&†%ëÌÐYÀ\nj7/[1™8\nßâì}TÇ4f«âg(HŠä/¦]±BaAH}˜±ç°âƒ4ÂŒnË<DfÕéJ\'”ÚíRQÂØ\0%Ô’ƒ»Ê’bÉê°fÕŽ³|ß\r×ÀhNÑù—Ôj(AçÂ1Açï¢ƒ «R«áªÈDrv\'LÕ_.W¿&¥]H	q^¨FÌÖI\rt–Êp}2;ˆEbŒ”ø”Fp 2p„¦wLÖIâ®ßÞòû[n¾uO`zÁ\02ÍÜaÜ{Ï~~Àçÿý?Þðú·¼åÍoýö·¾{ÏŸ|ßïn»}R/~ûÝ·Ýjn™ä‰%zÎÙßçåðeáÜ^;Ðþ	7D	h¸&IËw|ØlÒÛŸRÒï4Ì¶Ñ	:€ Dºd¼8>‹2æÐ‡%#jË^sÖïK^üŠO|üSW\\þƒ)-ßk„—öÓx¾j•ü;§Ä>©£¨=\"‰eÁz3Çßn·ßO†§¹sJÉ9xÖ3Ÿûº×¾é{gžE¼ú<JÎ+zj¤BâõªKh¢ö«|Œ¾­!	SšI_G*XRd9úWÑàL/ÿ…¶.­Yy~òC`ÂWýÍÔRå?Ca—Ö÷æZŒíÆœ³Þë$ödÓ5×‚¶æÍ8³+†Ý¼ùäT,Èn¬ˆY>Jç$_ïB~‚y¥v:¸B)´ßù…eh/ÄñÙÑÞ\'é\\BÈ„Õ\nq°X¼%Ì¤õ\\nïï­ò†ûó\n~Õé(‡	¦û©:zVå¹1ALl;{Ë6%I\'i\\‹”ž Š)rQNBbµ]j6¼„öì¬—˜æJ‘h.&	ñÅN1h^œ,43¢pS½€œt:4¼©AS	@Œ)OüÎž™‡þÒ×\"H¯}Í_ÿº7ÿäÇ?—^æ\"üàÊÿB\0°âÏûÿ:B²¯|Åk¥(„7v6‘àþQÂ‚ú™ÃçœGN+ŽBsŸÍ(+04)³(S1ý«Ð‚eDj¯Ô=°°AG‘A§Æ~Uq\\æó›ÞøÏˆAöF^HÖ\\@?úáO¹uÞ¦gñæÇiË‘]\0µ»¹¨nš ¹ã¾{\'ÄÖµ÷œøÇ?î‰Ï|Æ1§ž:qçyK`QÀ—ºëº¤¤?ø,º\"gð\Z—–x`ƒJ”\0qH;>ˆ¿I¸^4H¢s:ëÛ«t…Š(#4 £ùöÎã•cÞ=€$i¤\'<á	¬~Æ¸—V–\0ÀÎ ?$*b[èÆ99@qêåx`ÍÂ5–Ê\'[ÕØAp/¿zû9+¬iÈ…óŒärŒ_ì\0(À7Êá°ßÁþîåH?9XqtðßxØ§°Ãfr	YHÁtàNÈÌURâ!‘ž_`ùuä\0¦ÏRBh®­	xiˆæ®âhL‘úJ¼Xò	%ä«ù\Z+ÄšÚ]SBLíe6¬ ¥7´«ë$ƒip`ù‘z+YWÛŽRøò@™\ZzGƒ‡E„heÎ1\Z0‡¯ú‚/v@ÈÖß3UÑéä,F†ã(Hiî?ˆWŒÚ†OÄü‡9¾ê°ŒQ®\0)L%j§lóË^ú*srýw–6D	¥—)qCC‚Àß/â\\à(CÌK3L‚ ÷ÜãWf‘Àe)„É¡ëÙ#Î·X(ÓlO	ßŸî>I…sÏ¹àC<		óÎžþÏ|ç@<ÿy/>îØçkžôÎg>}Úw¿s‘ú•ôòˆ\'¥§£{~{Ë­x‹K”÷ê_ñò×´¬.Pú©Fr$É@¹Js#®9·”0W€­§Ÿ@\r™zËËßC¨ðÝÉù¢L2½ò=°ÕHýæYÓš L[Wj\rÔp¶‹þ÷»Idà¢4.í%Èã“zø\ZòQTWhËh,š Ñg»=¾ú#+µmn£c¸¡öØ¶¶&yžvš3´Ä·ýy9^â]ª³XÐÍ÷éWwÅìe·]‘Õ„ÕîÜÞ©_Tq4b©—f\"¢L¨‚oÜ$*ñÃ¶s1UQZã²¦è¤íI§8¸>9±£aJ&°Ä\0ºõŒ «e!¾ê>ñÆ&ÌðÉ¶ÀÍ¾Çàp6™$ŸOmSù\nX¶¹Ö»âhënf3”°`	L·@\Zd•ƒ«²Ñm?â°Œ¨ømD4„	Bß´¾H(RMSqãXâf2KéL5ŠmÞ¤2Ñjž ß<ØrÞ¹î¿o¢ÈD.^íW;óø…˜^ôÂ—ýÇ—¾*ç|íÔ{m.x†”»N¥zAá`™óý³Œ>S¡®¨üQ·oÚ÷Ò´<þØÒÍ¤kÊ”®‘ŠÝ|ÓmÜ‚«~ðc¥¨ø+_þº9ã¸uf=õ³ŸghËÐBUUe±ô‚pÞ\nT±Æà`Ð3«—ö¸gÌwÕU?j\rTzÔ¤=!ÂµÜPog*\ZÐˆ ‰Ÿúã ’bDáE”Oëºs›•\rÙ1³9mÖôh a·.…‰#ã×\r¡W–Ç¡¯ÂÛ¿æ¡.ìÀ÷èÐ×hˆC%S\0°6²!Ì\r¾¥yà8#	þ8,ÞjHŽñ‹ÉOÚ`]¯å…\n;I”—ÖXÑñqpåÝC+¹yÙ ù¼¾¦–u<®Å²\"&º`S#T]û\"N9XÀê*uÌ.:¶¬$<Oh+t±‹Û¸‚êèŸñôèG?š—‰	<®å˜zLòi*:ÜÉÅôIDH¢øáÔ;0ëß(È¬‚Q´§yŸ‚ ÛöÀúË.ý„;¶£?Ùè/Üý\nX%>_ü¢—c@=$(:Bºâ“n1›…1íŽB_ÂŒ#¸ÓÂËvÎBÛÀµAµGd0²Ä\Z€”eÐ\0ÄG*¾ø…¯h>Ý•W\\¥XX€ÏNU@Áî\Zõu”0ì‹¿ðzßwß~HMª´œÁéF	™oçÎ»^!f\\Zû\Z—€Ç”»?–Žë¡HÄÒ‘£.Ôômˆ¼Ûy(y? =•˜ÍkúaØ[6v–ˆ1Û¶6[•%þ°‡=ŒU‹XëM~Î9ã@c@Æ×æòU‚Ô~È^\06,ã\Z²úñPÔÖü\'D®ƒ-Ù£­¹Î†¨38­A|†³\ráßã!,.z0EYí>\Z}„y§Á¶÷ž×ØÅPBA­b	6ˆNÐìq{ÿ¸û‡û#Sé0Uu±Py¢Æ‹â-|4UÚÓ¾VàÞ»Ê”¶Ã\røÃØ\\úõ¯n>ë{ç}ú”Ïñê­ÞU¬Vç2-`›Œ.âIc¨JÚÙr£I.a3î	g€PJ’Õ\0´ªñßY ³Ýo€Hu#lŸÇ>8à¿\\_séF¿æIî\ZEí«ßâ”}é‹§âã§ð	4’Ú¤ã{gMòçÝïzŸÒ~û¯¼â‡ÓFš ì\Zõu”]Å&.\'º8#<É-3ó©»ÂÜ‘“ßÖK jÖçšh¿à€^!¢ÊhÜ+üª“;¦º—¨×¡!]EâAäôØŸ—I›…;šõÑˆ®}%™ô‚ò!FP.¬aéþ‹¹Ad¹dŒ|dÀ‹©[Â\0Ö{ŠFêÅ\nìª–†‡ï\Z% ­ßîl|ÛŒ0¡ÄÃFæm N\'†`ahq\0E9E)pN„‘„¼Ã¶›Xós	kìb(¡:”ª	Ü$_V.Á#p˜¬9Ê÷*Y%š„ä8YHÄ+8!‚äEP<!6“€ÛÏ~ú(ÏjÌú=Ñ†=jLÙ—‚ŠJGƒ\'êÁ‹Ô±D&sÓ[.É_ßâö?Ü~ÉÅ—ãÇ[ˆ\"úÜ,o{•\rQ)¸©a¨k´ÂÁrÈe¶TM„LQÐ†Š]„;Ãìí¿Ãøu…°²2€(Áà@D	Ôs/üêéßü×·¿H0üó?½+h«D]vé•7ß$F?™cDÈ~úºFJ±ÈQM;3„b’@C¥LE¶ÏW7Ý2ß†¸¢Õ*‘Ð@Á\"-b[W÷U\0*“*X´­ùŠ	x9RÄ@C™@UyåYXV¤b@v÷W»ôÎRB¾@ÏQCíp_ªœ=Ž	là¦º†fÔ³±s4\n@Í¼j´á«H jWÄ!5Â\'ÁÃwù‘Hw6M)qÝ’$\"\'Œ0çD-&Èr	t-¬Àn‰e\\+ÐDÀ7¸—@KMRYë×ô¹ûáï4%ä+ƒ&ê’`ñ\\d4I,*¼²V‘QžWÞÅ×Ç>ö±ÒÎ$ï×7¿yÜxÿG>òá³)™öŸ;õÒ\08€Y	ýËP²5ËV¾ð/=æ9Ç?õ)ÏÅ„3j@ì´¡,õ]ï|/ÃÔûc\'üº×¾ñ„ã_(PQjð!A	Ú£1GŽ@”/b ‡VY4´H7àR0x›ÜQ‰…¿œsAŒ7Ã˜’+^T¨}L­ˆøºko&ª=xÊùnªDÒšVXI,^}gQU‚v9%Œï*h[/Ø’	—&eõ0$	\0J0]ÆÐ”×ùÓ$É4°^¶dÕ¥%åüÑ°›X¬NJZ­¾Ì0œe÷bG®µ\Z(oà_ó¢F‹ÂVÉ%Œ Ì°„´E6»ÍzÔ£˜·\"à|Ã’Y\0Xm\08ŒÎœ×:^¿Â}\'t.ÿo§ “ÌÜo‚t§ñ€z5¸H‰Sù£¢L5E€˜O|â#VðÕ™ô‡1Q®ªdÜM\"!ùB…gå<vœF\r4ªt*[‡rb2jï)ZÁ·éÄÙ ôŸ†Ë‘°{ˆQ$äñ‡€yî1\'(þö†øÐô«i„òÀDDúÂ.½âòª2RÐ(g‰?p†aÌR6QËu@Î`ÃÜxåW¿¼Q|bç\'´ØŒ‡¬¹x×]Ô…âÒN\ZÓ(Í3ŒÊXäÁô ¸b”ÊÂÄŽx¸>µfcÃ:Iù´ªÑg2>K\0c\ZaKQ(Æ?à ù¼vß\r*9tF±=ê‘ÅÕˆ§Æ»òŠ)&›–î¨8.Àg¤Éî¤„üJ¼Õ¸Ç±’‰Ú*”Âq#ŒÐáÕSêùºœ_€.ì@˜öˆ6¨ÃÓ½‰Ôñ¢’ú¶^Í¢ä.­²‰š=¥çûU4™I…ÊÄÓ±Ò{\n\0\0IDAT.šÃQ#¢7Ðž=J˜jÑÒéeÎ±FAÕ^ÃÑ|F=c_¯±_XCÛ‰\rV>‹à¾vdÉ‚o/{Øõ>º$„“@º8C#6OªxºÓr\Z€S€o!–ÒVHÔôTE_£ð=è÷éÕ¤dÔ	’VÙå&›ü¹NêÓiqÃ41;wôò\Z»Ø‚ÀQYpPŒéLÚY‰v3—x5‰“ê;Pª\r¯êPýjƒ…*f­7–!\0ý¢=æ±<PªÙ€$ú)„A\r8˜NÂö§F0(nD2®È†`TV¬ñkE¼7Ë;8:0â¦™*X:ô•B—Ûa°ÐÈŠA•–ýŠ)ù§™\'à‰MÄr±“-cƒ®û¤÷È\\÷Àä“zÄSNýÔ\'?kš¸ôWoú¤ÿýÔ§<ùéÿø´gõÆÉøYA˜ö“ä§\nU§U_´j£;X)<™qúºÆ6.2†ÜªÅoñßó“\'FUƒÚKx¡—VvÉˆƒà(øøÇ?ž¨Šüá¸O¶ÁŠ¬(„ì_M:„xš€ðöÿðÊõD±ýQ¹¡ó`‘¨j ìâ­à¦ÙË…°…,r\nëc…>V0ù˜ðëúiÁ]ílàˆOÀœýÉ!O8Õ [_‚EÃÙ¿ö0uÉ±c\'ùÕË_ÀqÓWxÌV`Î‡nU±ŽmäáåAæÊ³*Ø’[™¡æ°bƒÉ-K®¿äJæ#Ú?Ò„;8Ê?.¢µÅô´Ÿôˆº§(§â[ehÄ¾z´ò¬Qm<ŠŽÆ3%ö\nûdº1OeÄ—ø\rÓBA%ª¿ÃêSAUªúrE«ÓÏ{ïùÓdJ…?%´òWõD`m\nß‹©™|Bè#&Çx7ØÏÄRŠÄvÔŒ\Z+ âO ²Ä&ôiŽ‘ÊI!»X)D/1à¿¦Žâ…™V#%á{gž{æçÈ%Hû sgHúA_Ëæzœ±œHÅN©Â,ŒJƒµ4s6HSq0XX<S¾*·T1•Çô&[¶I~+Úõ\"1æŒa“R)Žô¯Ÿÿì\ZŽÔ­·Þzà¸2¶¡Rá.¨Mô)¸Ì<ô9\n\"“—_åïçŸw©j\'ônÃ\'ÓÉ]%äVYd-¿à$£Òl6éwÜðíMþEP«Äš¶Oe\r<Ú®†gîX­@ÄfÁ_SS\\|ñe£:BÍT{Íd§¶V\nÍâà–íyeE“	Ã—}­utÛÛn¥ÇYnì˜MÒ+Çô9î§ë®ÑR\\­sSÙòÞTzù`Œ[õ¹„‚0ï\0zo]Y®»äyýyÿ_®þÅµ*…øV=se6õùâþ\r	OU\"!¤.Â>ûë¨Úœ2ö¤vxµV©gn¹“YèßÒuW»Ê¶ÍÜ\'Æt«±uÉaÌß×Q#$}:À(|#é§ïÿk˜ŒEË\núdW2üº\\•ÑHØŒàž&ŒJª5Gï-·ê®*úŠ½VÓ×eÿ5ºYÊ&4ëí²§ZxüÑG	\rguš•Ã!àsþùJ¨~¦†Û£„˜Ï|ÛSJ`žV„::ï€ŽµjæÒ£Ûv’‚é€2,È‚øñÎc\nÈìÐgíª¹óµŸ2jÆ0´Ù§ŠxÃ .ÔUú\ZzöÞbÈ/Õ0ã&ÃN5ËLaeª³vs`È°±}˜Ü¶\\}<øÎßñ¢á2ãMs\nT.7Tp°li\"ü®>Ûp£}gù²&[J¼+œÀgm7	ò8s+Ÿs»?•”P¨\rÄî­WxöÙçåéV½VÃÅ\róÞ+™Õi/?\rÁö(a+_™Šn@C¦ýp£f‘zh¦hþ\'ŽŸôÑ“\r%þBáñž…Ý”u¨ò@ÿ6Æ¨æñ—œåì÷YÓ¸‡šæ…&Ã\ZgïmËöºP©Ón!›C’ñ\\Cì³¤x¿s÷aºB¶_’Pz\0Tñ²íƒŸ·9”kÓ$<&~Y—æ§–ÕlÞœªõ½>JÈŒŠ§G	çœ}^µÚÃ×Œ–5„èøÝK	tçËÅˆ¶=›²¹E®\nV§{^‚œÉðÅl¿è|ÔÎêÛâã²Ââe†ÓžY&“I\rkqÏ¸îZ aø×Ñ›[—WÆ:nf‘×k&§U9CÅF\nþæ(„‰‹ŸqŠƒ-×\\ÝªOƒ%d‰CJg3rÊL?¨9$_ÛU_‘¿Ò-xÀ›YKst’Á”Ã‰9i,G%Œ<ðhP¹ÉjÅKçžûý\náëJÉp1“ƒ¹K)a‹!u„èATé‡LžÞn$äºáòê·FãfâÁúÞP.Áä$>²Y¬£\r†£0ÈsÀ*\"’&õFhh°B§\Záõ$	4;mPÕ;]\r¬kÑ ¯µ­þL^d<Ñ‘kìYJˆºl!ÀáŒ€þ¬oeNDzç¤¬ú\nƒwÄ—Õ6ž`6p”uYÙÉtñûíñq¡¬ËÁî5Á¸Û\r¨þJí¾î¶8Z)¡Åë\\@”À#<÷œóÇØ¨´h Õ|ÜÖ^ˆ§»—––¥ýfQ/qŽ=Jø;6Ë^ÉÏY(ßÂ–ƒºSPÅ-QÂ°_[lAŸô;k2svÀÿ§y(™ëà›µÄ×k,Ïß–;ékX?›gÞÒ]g»ˆ¹ÅŽ(A!i¨QB²üA¥‡FÞqpfèGX¯¶‰õ>þ:@[Ü…ü•µÒÑG	9\nc¨mmm@ÖÙg[î=^Ïê\Z\\>å—‡Ñ‡4%@0sJ	kÕÉ]‡ôfŸ[•Ú‚ÚÃ\08âv©¡Ì½\rkæaìq§a!.øË²m³í©¶œäÐc–½Ê¼ãgýA	³\\xègofKCTbÎ	ÀŽx	UpÏ¯ýßžféðÐæ^×ãæy¶Èÿ0ÿux‡m‚–5Ò\'Ï{ç¥âm¸J@Ç|$Þt\0ëžsÎùƒ/5§ÖHYžî8WyÀž~8,èJ‹±h)	Ì;•Š#F­ÀÑˆatd“ßžÊ­ˆø«|C£l•¤Ÿ^öÎÚE	fQ„j;\\-Ú³štvá¿wª¯+h.3Òå$¾,—ÐÀ¥ùcµv£o°¥v˜æ?éŒ‘Žª%Cÿ†FŸwÞ³”PŽj‚-‘3/M	æÓÞ`ƒnÜÕ$0¯÷YË$J¸òÆÐC‰ý|Ìy—ÞÕ”à¦Q‚HÜ%ÌÚà<úe5L.¡²OÃÓLfÀKÈoØ£„9Â<Ê(aL6(AûšªÁ÷/¸x¶õªW ¿yø®UÅ‘¡µ•ºdÔÊ+_]¶ÃnàøÝK	É«äŒñ·T¥\rˆfw^b½^‚EA EÍ¦—‹mÿš³¬Øî×¸«£b…fWí%½,odôò%iJ€`æ8’\r\rÁF¶oæÐŸõUgG‰Œ@%gö]sCùú=JX/%4³xs¼˜ÙÂtx¼„Qk´G	Û1ÊQF	#µPAEG¦r³XØ¥—^±G	Gš¤—M€j^6Æ|Zrl—3»×K(™ŒLkë(¡pçîßæïd½”`ÜR¸)Ì›&˜€ª8Úó£&}ës7Ù£„=w½¹fJ”ÀKhðcwó£v©ö(aE.=|¯÷ÈõRB¹„†¶–K^ÞË%,l²£ÏKµFcVZ#“W_zÉå{^Â‘öäÄ9*£\'íêŽÖ=¢~ ¶!JX®™\rÍnŸ<Œáà–B“K G2ÕîëÅÙ£èl‹«î–}¥æãåÆ|ÍÕû¾ðù/yÆÙÓEå¸b†8lón\nñµÔ;-{·»ãøíÅ2O\\ÓýKûµZy¾¶o²ézPVUš¤‡Lù~ûD¸çúë~yÚi_°¾ã˜§v\\±Â¿ª3çÿe2ÕüRo÷¼¶^ÍX6óÌK°ê;/!âÝµ•“;L	óø <	š¹äL¼Ú „\r¨ËCäÉY=œ•=®þÅ>ìKàù¶ó$° K<D„vÔ=¦^Ó40\rÒ4;½5[à”ÊÈFògl…Y…g×ØúËbèÆò®+ÜØ\n1.—`ÝF\"m™“2\n1è.Ô¥ÝK	äEv¦ø·(‚¡jcº¨uF]§Ïµxñ-\r0éìµû®\'jnYsc¬Ž:±<Èn¸’Ç1 Ýé¦ä²¤ 	†ÇTØ=rú° ˆcƒ`ÝV®pc+ü%È†¢Ë¹›‘>Xë<›yÌe•y÷RB³¯XzÂÚ,ÖQ˜×hÙ‡Ü;~žòº€eÝwÍu_=ý›üÜV˜Ø£„‡ÚŒ`…fe¥Ö¸Ö&±ò’E¥%ŒÂ¿Q(¹íãïQÂ²¬ dd†sb¢AÚþ¾›à;L	äKY™3ŒVî/o¸Ñö^Ôhí ÕüÕEä,OMÔ[.^mPXûmïðð%°müÚâ²fÝiý»1-Jvkí»Fƒ`YÝ˜ù¼Â­ð—¦bóGrsP–dÞï§„yY)æÌþûþÊíúÒOßwÍd¨Z”°@_w§ˆwó]åÃ¦ 1å“5ù‚{Þ£„ÝÜ ÛÞ›ö-^1š»‚ÈÙIÎGönqŒ{…Ö_F7QYáÆVøKò3Æ—¶ñÙ,Ù»P—v/%?7·ËÔÓTöOw‹fL„»åx”ÞRü\ZÑú4—÷âZˆ@á(Îƒæ¶Ç§5wó_Æ[‡> …¾Bë¯£þ\n7¶Â_J×ÁŽ¾¶\\í;L	ó\\„”uØ2}VFN6V{Ôü×–›yÀãè$³ÂOón5Dhý\"N¤ÛÉW+C<¢yð|q;ÎóŽÆÖš³á Ûc~øC)!ðš÷^€‰+üeÞÙ6c>Ì™÷^ý·ýËÈÍlYÍe×ÆŽ6D	‡£ß;uÌ0ŠG†mq8u3÷¹.\\íYFLy©ÛØŒd4WY ä˜8ëÃÍjì¼¿¬Ö”Kµûâƒ7ƒï+hÅf(a1‰®pÛGú/{”pÿÚj{”0´m59ÒÊú ;ÿ%ìlƒ®¦äz²Ü£„=JØ¦òaµÞ²³=ü¨»ú%ìl“­1Òµ\ZOìNÿiö(av¦pö(ag%°íÕ÷(aö(a–^eþðíâÝiCÃ=/a[JøaQµ(êºÈ\0\0\0\0IEND®B`','\0','2012-10-31 20:51:26',NULL,'system',NULL,1),(6,'Acupuncture','.jpg',288,288,'ÿØÿà\0JFIF\0\0H\0H\0\0ÿáÞExif\0\0MM\0*\0\0\0\0\0\0\0\0\0\0\0\Z\0\0\0\0\0\0\0b\0\0\0\0\0\0\0j(\0\0\0\0\0\0\01\0\0\0\0\0\0\0r2\0\0\0\0\0\0\0†‡i\0\0\0\0\0\0\0œ\0\0\0È\0\0\0\0\0\0\0\0\0\0\0\0Adobe Photoshop 7.0\02004:10:18 16:13:35\0\0\0\0 \0\0\0\0ÿÿ\0\0 \0\0\0\0\0\0\0Ã \0\0\0\0\0\0\0í\0\0\0\0\0\0\0\0\0\0\0\0\0\0\Z\0\0\0\0\0\0\0\0\0\0\0\0(\0\0\0\0\0\0\0\0\0\0\0\0\0&\0\0\0\0\0\0°\0\0\0\0\0\0\0H\0\0\0\0\0\0H\0\0\0ÿØÿà\0JFIF\0\0H\0H\0\0ÿí\0Adobe_CM\0ÿî\0Adobe\0d€\0\0\0ÿÛ\0„\0			\n\r\r\rÿÀ\0\0€\0i\"\0ÿÝ\0\0ÿÄ?\0\0\0\0\0\0\0\0\0\0	\n\0\0\0\0\0\0\0\0\0	\n\03\0!1AQa\"q2‘¡±B#$RÁb34r‚ÑC%’Sðáñcs5¢²ƒ&D“TdEÂ£t6ÒUâeò³„ÃÓuãóF\'”¤…´•ÄÔäô¥µÅÕåõVfv†–¦¶ÆÖæö7GWgw‡—§·Ç×ç÷\05\0!1AQaq\"2‘¡±B#ÁRÑð3$bár‚’CScs4ñ%¢²ƒ&5ÂÒD“T£dEU6teâò³„ÃÓuãóF”¤…´•ÄÔäô¥µÅÕåõVfv†–¦¶ÆÖæö\'7GWgw‡—§·ÇÿÚ\0\0\0?\0õ4’IVKS¨_eL¦ª¤Y•kikÚ,ë,·Ý¹¿£ª»6îoÓ@wJÂØ\rÙVA?£É=ïßü¯UïeíÓù‹Zú¿àÑ:­m£486‹‰-kŽÂ×Q‘ó µïÞßÜY\'§ô¼Ci²Œ.—úûržÑe.ÛWÚ_ú<ßá(õ¿3I ¥;¸9\'»œ6½À‡´pÒk°Ûr:«Ó1ìÇÂ­—G®íÖß´\0=[\\ë®­þvÇ+I‡r¥$’H)JÏÉËÊ³ÇcU@o¯ÐÒòç‚æÑGª×±»Y²ËmÙþŒ¯þ\nò©ŠÑNfUZnµÃ#pÝ$8\n=û½¾ÏCoèÿ\03ÿ\0pê¤tQ•‡{ZrlËÇ¸–ÅÁ¥õºö¹¶ÔÚ·Rí¾žË÷ïÿ\0\n¯ª¹@[}ð~—®^×íÇÐú~£ÝéúnüÏQZHô*RI$š§ÿÐõ4,œš1h~FMªš†çØã\0UH9×õgÔöÅxu2Êÿ\0”û¬/ø*éØÏøëUp-.6wÖ|,ê¬ÄÆµÔÕ}Nœ‹qï$´ûèUeURïk¿œºÿ\0ýµ+¯ú¢ÚŸ‘SÙE®×}ž›¥»mf›e5þ‘¬ØöÕúOðv-ì³—…’FÃ{`g†á»n«?§œì&—?Ò§í^Ç:H,³Ñ¡¶º+ÝéR÷±¬ôÿ\0ãÅWjS/­}\"ç²›òñëºÂQ\0Ûï¢Æ¶ßNÚ®wýÇµŸñ~²ÚUò]SŸV5ÕY~àC€-Fÿ\0sô”z{è¾—8ãXêCÝ©s[î­ÅßœïIìkÝþ‘4¸Si$“9Á­.:\0$üR¬Ü\\F´ß`iyŠëç¼vSS7Ysÿ\0‘[UL|·]—“akk¾ŠZŠ^\r»I{Øü†×½”ú›FßÒÂ~âé”õäd0?*ðÛšò\\ÂÐaíÆeµm¶ªv~‰û>Ÿ¾Ô,\Zñ[èÈ¼ÐiµÂ›M{joµ¯¤9­©û½Wn{­¶ßÒSüêx”Ù¿&Ü|Œl‡×ºËê-³¯À[úg»ôÛ“éîý6ÏÒìþoý\Z¹™‹–Òh°<·G³V½‡÷m©ûm¥ÿ\0Èµ‹#¨b¶üœµd[^#)ÇßNÏÓnu­ª·>ÆYc,vG¶Ÿ±Ýnÿ\0ðÈõôº:n32h¯fMok®v÷=ÎaŠŸE—Üç[{*¥ß¢õüíUÚ‘”ë¤’IŠÿÑõ5R±þV¼—\rqé†N¿O\'s¶+j¦-eùyŽ¶Š¿©Q~çë_m¿Øcê”8\rÆ;+%µ‡b1Ä\\ðßÑÀ5ÞæYþÚßz²Y}Èaf)u¾ƒÈØéÈ>®;n©ÅÎ­Õ·Ô¡þ¯§ú{hýn‘…eôúÙW>ÚM¯¶¬g5`sžnÜïI»­ô.sÛNû6…ý#ý+V•ƒ,_‰kÚØm¡ÍsZw\ríôìpklÛûô¿ôOÿ\0„Nµ5EtgáÕŽÀÊÿ\0X±Í\Z	qkì~ßß}×nEéâ+»¼ä\\f\"}îÿ\0©ú*¦=Ytušª¿$ä°cØ)cZðÒúÝcí¹¿Î¿ÛM^ÖSÿ\0Vðƒª·#Ú€óugMYqu†©¬Ïêl@ì¦ÚbAÐ§AÊ¼ÑC¬hÜý[|^ò+©¿ç¹4n¦µ6Xp0-®@>Žöñ-{vAþ«ž×¢7œfeYVí×nq’¡vÊš>ƒ=GYoüeŠž]´Œz°ØýîÃ»\\\\×4{of×»Ó¯Þê~›eu+vÛƒÔº~Cj½·c½¯ªÇÓ`ÓM¶3ÕoónÚœT•Ø´Üúnµ¤ÙV­‡86—XvËv}*ý]þ“ýõª–]mý,ÚÇÇ­k}7?›uÍc9ýúUœŽ¡ƒŠöSmÕ×kÄ×Sœ\ZOæ·é~ó½ŠŽ!ÈJÆÄ{½LÀÁc_é½µ“KÚöWdîô}M¾Ÿéê9bANºJÚË©eÌ–49³Ì:©¦õSÿÒõ¸1Žyá “Jƒì^‘ŽÍŸ¦²¶V+2=ïlØ^~“[_é.·ù£g<<3\r¤ú™&ŠšA½îýÖìýüe¬Uº‹ÃèÎÈ>ËM•VH:Xæn²ÎíuLÿ\0·T %¯Ñ_•Ók«9áø—‡\r¤:ÏwØrô+s71˜Ÿé¿šþsÓõ6Üæ±¥ï!­h%Î&\0’â üz, ãXÆÙInÇVðÒÞ6¹®YX˜[—u/ÑŠí¦¼‹ŸeaþÛ*­û¾ÐÏGÓ¿ôÖÛè½ÿ\0é½OIho¥)³-½J®©sãÝÕUA;vã»íNô¯éígæbúá+ZO;z¥#@,¢Ù×RXú6éùßÎ½,ö1ïÅkØ,c­,x kê¹‡Ÿë*î±ÔãQuÇ\\½;ÞâGèàÐoýjÊ²¬ÞŽêtÕ\\ “‰\\:Ò#³öî?õËjV•åV-ÈÌ¤¯†–måÖ7ÔÇ]¸ôÿ\0Çz•ÿ\0ƒMˆÕHs˜3zž&+½ØØîu™¹¡Ì{Ã?A[÷¡ßöøÏ³«yXï7W‘@ý(Š­×nê\\}ÚþýÏSÿ\0\\gøgªôÒÜ\\–Y{˜ßGÇßq07ÚöY{÷:Úÿ\0CþbÐ{ÚÆ9îÑ­¸ùN•Jkâb\nÛsîk]vS‹¯<‚>…u{‡ºº©ýþ¬Uú;™H·\0CE A¨<°mô.ýŸ¹ú?ô•­9¬isÈkZ%Î:\0r³\rb‹/É­»~Ï’_``ÕÛ]_hÝûÛ\\ÿ\0´è:C[µ6ºx\rÇ,ýË-lxEÛÇò>Š´ªPæÕ~1M±‘_ò„6«¶ÿ\0ÅØÖzŸñõ«iVªÿÓìïßgQÉ¯=ÏÆ¡íi,©Ís¯¯Ô²Ši±Û=Ziöïôh³Þü¬Wý\Z¹\rMÍÊl†ä—\\<a¬e-ÿ\0£Få>£ÑÙŸ“U¯¹õÖÖ–][	i{~“mcšê6»ü%¥ýÇÔ®[C_Œüv€\Zæ\ZÚØÐ6º¢½’•WÆ Ý—¸¿EGµÊX–úØµZD±¥Àv1îoö\\ƒÓÜ×?4´A.:jC*lû“ûÉ½ÔË1ù^â./€b@ªö{ÿ\0y›¬ÿ\0=gäYé[ÖÞêm®Ç<û=J¬m¡Ÿ¢¾¿ÞÚßß«Óÿ\0ŠZ.‹:‹1Qq>v»c?èÓj­ÔúAÌµ·QqÇ´·Ñ¸Æàê‰:nØËëÝº‹½ÿ\0¹mvÖˆ4¤8—fŽú›‹eVÕ±ÍÑk,ýZç9¬±˜¬·Ð®ïøSþ\r¬¼lŽŸUðË©†šäúd–—mÜÖûj`{÷ìüÅ¦Öµh†´\0T0:6.\rž«ûÐæU¼é[ÁéVÆíoÑª¿ÒY¾ïøD¬)1k]Ô^ Ó¨#s§þ©QËè8#ìk_mD5¬Ä¶×¿¾àßn-u;kúUÔïÑ3ü\Z¿iÍÇqô² {Ï¶æ·üÚlQêv¶¬MÎ$eM%³ +w·óZßsÿ\0•<”Á¦×c^ÚÜC¾ºeŽ©®\ZµÕã>Çc×³üÊ¿F‡c½L<û6º=GÈîáPeoÛ½èíZ{kc¬y†°8ø©AÀk†EÂ`6¼Î°›žßóž•õ*iuÁc1ßƒ¶û\Z÷=¥º¹»k{ä0ïßüÍ•7ôžÞ¥Í­_’ÎÄèxØ™,¾»msjmU=û€õ&^ïÒÙéý\n=K?EêYÿ\0éh¥aOÿÔõ4’IWKœÜÌ^žÜŸ´Y¶¦^v¥ÄznK¡µµÎØ×:û7~c=OôièÊÇÇºöX~Qk6°Ä¶–ïw¦±»žÊý[v{Ñ(¦œŸ´>ö6Úì¿Ú×€áúµ7Úéú7ÕeŒUzM5ÞÇ¿\"±m­¸än|;eÎÝêWWî}Ÿù¤ý5Swnõ¯&M¶»äÚÏ Æëÿ\0»ûjÊ­ÓÀn1hüÛ-åež*ÊiÝJI$S[<ŒnoÓÇ\"æÏò5{uÿ\0IVúÔzl³¬x–›¨‘ÿ\0^©O¨k‡k;Ú=!ñ°ú-ÿ\0¤õ_¬ˆÁQ ¶ÆØ\"$šZe>éþúGýq8tSc:Sh<^ñ[¿ª}Öûi¯Vl²f3ÌCnƒ?Êe•·oò·½Y@ì¤’I?ÿÕõ4’IWKW–»&—ƒ¹Å¿Õ°ú»^Ä[éå86r®q. È\'s^Í¿F·Wµ±_RxçêÎ§lýêTk´·¦äµëž+nžÑ¾úé¬Ó³ríS{¦û±EÑ·í}ÃYÑîsë?öÖÅiEŒk\ZÑ\rh\0!¢’iÝJI$S_=Û1]gj‹,tx1Í±ÿ\0ôZ«õŸ@×Œ.vÐÜª^ €}ŽÜwþŸNßø5o&¡~=´ž-c˜´V&oPªÜL{rŽÓUVdÜÆ‚t¡Ôµÿ\0ÖþuŸ¡ßïõãù)×Ënû±kß¥Þ|ƒ\Z÷nÿ\0·=5eR¢Ç_Ô²þ¢–Gïý-ÿ\0æí¡Šêiì¥$’H)ÿÖõ4’IWKQÓgU¬	ÛC‹ü&ç0Uÿ\0G\ZõŸ†ëI6½­te›,“ôjmáÌ·vÜV×wòÿ\0Œsß…›’Ëñ¯±¶?}wSKì•7iô›gÐs¿‘þôá,®ÜÑÓð_‹‘‹}Ö½®&ª©±À‚ÐÐÀüf[^÷~åný})ÝI\\1i\0Åm€díö»ó½ÈÉ…JI$’S,eUºÛ]m.{­œåÍ<TÆ´½–äf\ZöŠp¯Ó¹ô<ôÊh¾ÆÑöAõ3ô~ÿ\0ÑïºÊ=~ßWn[ºußcc-½»^Ú¬×†9¶YOýv¶¾¶,‹ºÖNc«¿§Þƒ7¾»Ò‘öGe\"ûØÿ\0Ó:úýLSôÍ\'Çe:˜õ†ãXlÇ¼×•¹Ïs…vVÊ¢oÝgó˜ö?oæoZk#¥du.¡–s²±Ž;*5WEšØ÷9Áïµû™[ê®¶ÔÖÖßðž«ÿ\0Ñ­t%º”’I&©ÿÙÿí–Photoshop 3.0\08BIM%\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\08BIMí\0\0\0\0\0\0H\0\0\0\0\0H\0\0\0\08BIM&\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0?€\0\08BIM\r\0\0\0\0\0\0\0\0x8BIM\0\0\0\0\0\0\0\08BIMó\0\0\0\0\0	\0\0\0\0\0\0\0\0\08BIM\n\0\0\0\0\0\0\08BIM\'\0\0\0\0\0\n\0\0\0\0\0\0\0\08BIMõ\0\0\0\0\0H\0/ff\0\0lff\0\0\0\0\0\0\0/ff\0\0¡™š\0\0\0\0\0\0\02\0\0\0\0Z\0\0\0\0\0\0\0\0\05\0\0\0\0-\0\0\0\0\0\0\0\08BIMø\0\0\0\0\0p\0\0ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿè\0\0\0\0ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿè\0\0\0\0ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿè\0\0\0\0ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿè\0\08BIM\0\0\0\0\0\0\0\08BIM\0\0\0\0\0\0\08BIM\0\0\0\0\0\0\0\0\0\0@\0\0@\0\0\0\08BIM\0\0\0\0\0\0\0\0\08BIM\Z\0\0\0\0I\0\0\0\0\0\0\0\0\0\0\0\0\0\0í\0\0\0Ã\0\0\0\n\0U\0n\0t\0i\0t\0l\0e\0d\0-\02\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0Ã\0\0\0í\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0null\0\0\0\0\0\0boundsObjc\0\0\0\0\0\0\0\0\0Rct1\0\0\0\0\0\0\0Top long\0\0\0\0\0\0\0\0Leftlong\0\0\0\0\0\0\0\0Btomlong\0\0\0í\0\0\0\0Rghtlong\0\0\0Ã\0\0\0slicesVlLs\0\0\0Objc\0\0\0\0\0\0\0\0slice\0\0\0\0\0\0sliceIDlong\0\0\0\0\0\0\0groupIDlong\0\0\0\0\0\0\0originenum\0\0\0ESliceOrigin\0\0\0\rautoGenerated\0\0\0\0Typeenum\0\0\0\nESliceType\0\0\0\0Img \0\0\0boundsObjc\0\0\0\0\0\0\0\0\0Rct1\0\0\0\0\0\0\0Top long\0\0\0\0\0\0\0\0Leftlong\0\0\0\0\0\0\0\0Btomlong\0\0\0í\0\0\0\0Rghtlong\0\0\0Ã\0\0\0urlTEXT\0\0\0\0\0\0\0\0\0nullTEXT\0\0\0\0\0\0\0\0\0MsgeTEXT\0\0\0\0\0\0\0\0altTagTEXT\0\0\0\0\0\0\0\0cellTextIsHTMLbool\0\0\0cellTextTEXT\0\0\0\0\0\0\0\0	horzAlignenum\0\0\0ESliceHorzAlign\0\0\0default\0\0\0	vertAlignenum\0\0\0ESliceVertAlign\0\0\0default\0\0\0bgColorTypeenum\0\0\0ESliceBGColorType\0\0\0\0None\0\0\0	topOutsetlong\0\0\0\0\0\0\0\nleftOutsetlong\0\0\0\0\0\0\0bottomOutsetlong\0\0\0\0\0\0\0rightOutsetlong\0\0\0\0\08BIM\0\0\0\0\0\08BIM\0\0\0\0\0\0\0\08BIM\0\0\0\0Ì\0\0\0\0\0\0i\0\0\0€\0\0<\0\0ž\0\0\0°\0\0ÿØÿà\0JFIF\0\0H\0H\0\0ÿí\0Adobe_CM\0ÿî\0Adobe\0d€\0\0\0ÿÛ\0„\0			\n\r\r\rÿÀ\0\0€\0i\"\0ÿÝ\0\0ÿÄ?\0\0\0\0\0\0\0\0\0\0	\n\0\0\0\0\0\0\0\0\0	\n\03\0!1AQa\"q2‘¡±B#$RÁb34r‚ÑC%’Sðáñcs5¢²ƒ&D“TdEÂ£t6ÒUâeò³„ÃÓuãóF\'”¤…´•ÄÔäô¥µÅÕåõVfv†–¦¶ÆÖæö7GWgw‡—§·Ç×ç÷\05\0!1AQaq\"2‘¡±B#ÁRÑð3$bár‚’CScs4ñ%¢²ƒ&5ÂÒD“T£dEU6teâò³„ÃÓuãóF”¤…´•ÄÔäô¥µÅÕåõVfv†–¦¶ÆÖæö\'7GWgw‡—§·ÇÿÚ\0\0\0?\0õ4’IVKS¨_eL¦ª¤Y•kikÚ,ë,·Ý¹¿£ª»6îoÓ@wJÂØ\rÙVA?£É=ïßü¯UïeíÓù‹Zú¿àÑ:­m£486‹‰-kŽÂ×Q‘ó µïÞßÜY\'§ô¼Ci²Œ.—úûržÑe.ÛWÚ_ú<ßá(õ¿3I ¥;¸9\'»œ6½À‡´pÒk°Ûr:«Ó1ìÇÂ­—G®íÖß´\0=[\\ë®­þvÇ+I‡r¥$’H)JÏÉËÊ³ÇcU@o¯ÐÒòç‚æÑGª×±»Y²ËmÙþŒ¯þ\nò©ŠÑNfUZnµÃ#pÝ$8\n=û½¾ÏCoèÿ\03ÿ\0pê¤tQ•‡{ZrlËÇ¸–ÅÁ¥õºö¹¶ÔÚ·Rí¾žË÷ïÿ\0\n¯ª¹@[}ð~—®^×íÇÐú~£ÝéúnüÏQZHô*RI$š§ÿÐõ4,œš1h~FMªš†çØã\0UH9×õgÔöÅxu2Êÿ\0”û¬/ø*éØÏøëUp-.6wÖ|,ê¬ÄÆµÔÕ}Nœ‹qï$´ûèUeURïk¿œºÿ\0ýµ+¯ú¢ÚŸ‘SÙE®×}ž›¥»mf›e5þ‘¬ØöÕúOðv-ì³—…’FÃ{`g†á»n«?§œì&—?Ò§í^Ç:H,³Ñ¡¶º+ÝéR÷±¬ôÿ\0ãÅWjS/­}\"ç²›òñëºÂQ\0Ûï¢Æ¶ßNÚ®wýÇµŸñ~²ÚUò]SŸV5ÕY~àC€-Fÿ\0sô”z{è¾—8ãXêCÝ©s[î­ÅßœïIìkÝþ‘4¸Si$“9Á­.:\0$üR¬Ü\\F´ß`iyŠëç¼vSS7Ysÿ\0‘[UL|·]—“akk¾ŠZŠ^\r»I{Øü†×½”ú›FßÒÂ~âé”õäd0?*ðÛšò\\ÂÐaíÆeµm¶ªv~‰û>Ÿ¾Ô,\Zñ[èÈ¼ÐiµÂ›M{joµ¯¤9­©û½Wn{­¶ßÒSüêx”Ù¿&Ü|Œl‡×ºËê-³¯À[úg»ôÛ“éîý6ÏÒìþoý\Z¹™‹–Òh°<·G³V½‡÷m©ûm¥ÿ\0Èµ‹#¨b¶üœµd[^#)ÇßNÏÓnu­ª·>ÆYc,vG¶Ÿ±Ýnÿ\0ðÈõôº:n32h¯fMok®v÷=ÎaŠŸE—Üç[{*¥ß¢õüíUÚ‘”ë¤’IŠÿÑõ5R±þV¼—\rqé†N¿O\'s¶+j¦-eùyŽ¶Š¿©Q~çë_m¿Øcê”8\rÆ;+%µ‡b1Ä\\ðßÑÀ5ÞæYþÚßz²Y}Èaf)u¾ƒÈØéÈ>®;n©ÅÎ­Õ·Ô¡þ¯§ú{hýn‘…eôúÙW>ÚM¯¶¬g5`sžnÜïI»­ô.sÛNû6…ý#ý+V•ƒ,_‰kÚØm¡ÍsZw\ríôìpklÛûô¿ôOÿ\0„Nµ5EtgáÕŽÀÊÿ\0X±Í\Z	qkì~ßß}×nEéâ+»¼ä\\f\"}îÿ\0©ú*¦=Ytušª¿$ä°cØ)cZðÒúÝcí¹¿Î¿ÛM^ÖSÿ\0Vðƒª·#Ú€óugMYqu†©¬Ïêl@ì¦ÚbAÐ§AÊ¼ÑC¬hÜý[|^ò+©¿ç¹4n¦µ6Xp0-®@>Žöñ-{vAþ«ž×¢7œfeYVí×nq’¡vÊš>ƒ=GYoüeŠž]´Œz°ØýîÃ»\\\\×4{of×»Ó¯Þê~›eu+vÛƒÔº~Cj½·c½¯ªÇÓ`ÓM¶3ÕoónÚœT•Ø´Üúnµ¤ÙV­‡86—XvËv}*ý]þ“ýõª–]mý,ÚÇÇ­k}7?›uÍc9ýúUœŽ¡ƒŠöSmÕ×kÄ×Sœ\ZOæ·é~ó½ŠŽ!ÈJÆÄ{½LÀÁc_é½µ“KÚöWdîô}M¾Ÿéê9bANºJÚË©eÌ–49³Ì:©¦õSÿÒõ¸1Žyá “Jƒì^‘ŽÍŸ¦²¶V+2=ïlØ^~“[_é.·ù£g<<3\r¤ú™&ŠšA½îýÖìýüe¬Uº‹ÃèÎÈ>ËM•VH:Xæn²ÎíuLÿ\0·T %¯Ñ_•Ók«9áø—‡\r¤:ÏwØrô+s71˜Ÿé¿šþsÓõ6Üæ±¥ï!­h%Î&\0’â üz, ãXÆÙInÇVðÒÞ6¹®YX˜[—u/ÑŠí¦¼‹ŸeaþÛ*­û¾ÐÏGÓ¿ôÖÛè½ÿ\0é½OIho¥)³-½J®©sãÝÕUA;vã»íNô¯éígæbúá+ZO;z¥#@,¢Ù×RXú6éùßÎ½,ö1ïÅkØ,c­,x kê¹‡Ÿë*î±ÔãQuÇ\\½;ÞâGèàÐoýjÊ²¬ÞŽêtÕ\\ “‰\\:Ò#³öî?õËjV•åV-ÈÌ¤¯†–måÖ7ÔÇ]¸ôÿ\0Çz•ÿ\0ƒMˆÕHs˜3zž&+½ØØîu™¹¡Ì{Ã?A[÷¡ßöøÏ³«yXï7W‘@ý(Š­×nê\\}ÚþýÏSÿ\0\\gøgªôÒÜ\\–Y{˜ßGÇßq07ÚöY{÷:Úÿ\0CþbÐ{ÚÆ9îÑ­¸ùN•Jkâb\nÛsîk]vS‹¯<‚>…u{‡ºº©ýþ¬Uú;™H·\0CE A¨<°mô.ýŸ¹ú?ô•­9¬isÈkZ%Î:\0r³\rb‹/É­»~Ï’_``ÕÛ]_hÝûÛ\\ÿ\0´è:C[µ6ºx\rÇ,ýË-lxEÛÇò>Š´ªPæÕ~1M±‘_ò„6«¶ÿ\0ÅØÖzŸñõ«iVªÿÓìïßgQÉ¯=ÏÆ¡íi,©Ís¯¯Ô²Ši±Û=Ziöïôh³Þü¬Wý\Z¹\rMÍÊl†ä—\\<a¬e-ÿ\0£Få>£ÑÙŸ“U¯¹õÖÖ–][	i{~“mcšê6»ü%¥ýÇÔ®[C_Œüv€\Zæ\ZÚØÐ6º¢½’•WÆ Ý—¸¿EGµÊX–úØµZD±¥Àv1îoö\\ƒÓÜ×?4´A.:jC*lû“ûÉ½ÔË1ù^â./€b@ªö{ÿ\0y›¬ÿ\0=gäYé[ÖÞêm®Ç<û=J¬m¡Ÿ¢¾¿ÞÚßß«Óÿ\0ŠZ.‹:‹1Qq>v»c?èÓj­ÔúAÌµ·QqÇ´·Ñ¸Æàê‰:nØËëÝº‹½ÿ\0¹mvÖˆ4¤8—fŽú›‹eVÕ±ÍÑk,ýZç9¬±˜¬·Ð®ïøSþ\r¬¼lŽŸUðË©†šäúd–—mÜÖûj`{÷ìüÅ¦Öµh†´\0T0:6.\rž«ûÐæU¼é[ÁéVÆíoÑª¿ÒY¾ïøD¬)1k]Ô^ Ó¨#s§þ©QËè8#ìk_mD5¬Ä¶×¿¾àßn-u;kúUÔïÑ3ü\Z¿iÍÇqô² {Ï¶æ·üÚlQêv¶¬MÎ$eM%³ +w·óZßsÿ\0•<”Á¦×c^ÚÜC¾ºeŽ©®\ZµÕã>Çc×³üÊ¿F‡c½L<û6º=GÈîáPeoÛ½èíZ{kc¬y†°8ø©AÀk†EÂ`6¼Î°›žßóž•õ*iuÁc1ßƒ¶û\Z÷=¥º¹»k{ä0ïßüÍ•7ôžÞ¥Í­_’ÎÄèxØ™,¾»msjmU=û€õ&^ïÒÙéý\n=K?EêYÿ\0éh¥aOÿÔõ4’IWKœÜÌ^žÜŸ´Y¶¦^v¥ÄznK¡µµÎØ×:û7~c=OôièÊÇÇºöX~Qk6°Ä¶–ïw¦±»žÊý[v{Ñ(¦œŸ´>ö6Úì¿Ú×€áúµ7Úéú7ÕeŒUzM5ÞÇ¿\"±m­¸än|;eÎÝêWWî}Ÿù¤ý5Swnõ¯&M¶»äÚÏ Æëÿ\0»ûjÊ­ÓÀn1hüÛ-åež*ÊiÝJI$S[<ŒnoÓÇ\"æÏò5{uÿ\0IVúÔzl³¬x–›¨‘ÿ\0^©O¨k‡k;Ú=!ñ°ú-ÿ\0¤õ_¬ˆÁQ ¶ÆØ\"$šZe>éþúGýq8tSc:Sh<^ñ[¿ª}Öûi¯Vl²f3ÌCnƒ?Êe•·oò·½Y@ì¤’I?ÿÕõ4’IWKW–»&—ƒ¹Å¿Õ°ú»^Ä[éå86r®q. È\'s^Í¿F·Wµ±_RxçêÎ§lýêTk´·¦äµëž+nžÑ¾úé¬Ó³ríS{¦û±EÑ·í}ÃYÑîsë?öÖÅiEŒk\ZÑ\rh\0!¢’iÝJI$S_=Û1]gj‹,tx1Í±ÿ\0ôZ«õŸ@×Œ.vÐÜª^ €}ŽÜwþŸNßø5o&¡~=´ž-c˜´V&oPªÜL{rŽÓUVdÜÆ‚t¡Ôµÿ\0ÖþuŸ¡ßïõãù)×Ënû±kß¥Þ|ƒ\Z÷nÿ\0·=5eR¢Ç_Ô²þ¢–Gïý-ÿ\0æí¡Šêiì¥$’H)ÿÖõ4’IWKQÓgU¬	ÛC‹ü&ç0Uÿ\0G\ZõŸ†ëI6½­te›,“ôjmáÌ·vÜV×wòÿ\0Œsß…›’Ëñ¯±¶?}wSKì•7iô›gÐs¿‘þôá,®ÜÑÓð_‹‘‹}Ö½®&ª©±À‚ÐÐÀüf[^÷~åný})ÝI\\1i\0Åm€díö»ó½ÈÉ…JI$’S,eUºÛ]m.{­œåÍ<TÆ´½–äf\ZöŠp¯Ó¹ô<ôÊh¾ÆÑöAõ3ô~ÿ\0ÑïºÊ=~ßWn[ºußcc-½»^Ú¬×†9¶YOýv¶¾¶,‹ºÖNc«¿§Þƒ7¾»Ò‘öGe\"ûØÿ\0Ó:úýLSôÍ\'Çe:˜õ†ãXlÇ¼×•¹Ïs…vVÊ¢oÝgó˜ö?oæoZk#¥du.¡–s²±Ž;*5WEšØ÷9Áïµû™[ê®¶ÔÖÖßðž«ÿ\0Ñ­t%º”’I&©ÿÙ8BIM!\0\0\0\0\0U\0\0\0\0\0\0\0A\0d\0o\0b\0e\0 \0P\0h\0o\0t\0o\0s\0h\0o\0p\0\0\0\0A\0d\0o\0b\0e\0 \0P\0h\0o\0t\0o\0s\0h\0o\0p\0 \07\0.\00\0\0\0\08BIM\0\0\0\0\0\0\0\0\0\0ÿáHhttp://ns.adobe.com/xap/1.0/\0<?xpacket begin=\'ï»¿\' id=\'W5M0MpCehiHzreSzNTczkc9d\'?>\n<?adobe-xap-filters esc=\"CR\"?>\n<x:xapmeta xmlns:x=\'adobe:ns:meta/\' x:xaptk=\'XMP toolkit 2.8.2-33, framework 1.5\'>\n<rdf:RDF xmlns:rdf=\'http://www.w3.org/1999/02/22-rdf-syntax-ns#\' xmlns:iX=\'http://ns.adobe.com/iX/1.0/\'>\n\n <rdf:Description about=\'uuid:0620b836-2118-11d9-bb9f-ddfb0074707f\'\n  xmlns:xapMM=\'http://ns.adobe.com/xap/1.0/mm/\'>\n  <xapMM:DocumentID>adobe:docid:photoshop:0620b834-2118-11d9-bb9f-ddfb0074707f</xapMM:DocumentID>\n </rdf:Description>\n\n</rdf:RDF>\n</x:xapmeta>\n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                                                                    \n                                                       \n<?xpacket end=\'w\'?>ÿî\0Adobe\0d@\0\0\0ÿÛ\0„\0ÿÀ\0\0í\0Ã\0ÿÝ\0\0ÿÄ¢\0\0\0\0\0\0\0\0\0\0\0\0\0	\n\0\0\0\0\0\0\0\0\0\0\0\0	\0\n\0	u!\"\01A2#	QBa$3Rqb‘%C¡±ð&4r\nÁÑ5\'áS6‚ñ’¢DTsEF7Gc(UVW\Z²ÂÒâòdƒt“„e£³ÃÓã)8fóu*9:HIJXYZghijvwxyz…†‡ˆ‰Š”•–—˜™š¤¥¦§¨©ª´µ¶·¸¹ºÄÅÆÇÈÉÊÔÕÖ×ØÙÚäåæçèéêôõö÷øùú\0m!1\0\"AQ2aqB#‘R¡b3	±$ÁÑCrðá‚4%’ScDñ¢²&5T6Ed\'\nsƒ“FtÂÒâòUeuV7„…£³ÃÓãó)\Z”¤´ÄÔäô•¥µÅÕåõ(GWf8v†–¦¶ÆÖæögw‡—§·Ç×ç÷HXhxˆ˜¨¸ÈØèø9IYiy‰™©¹ÉÙéù*:JZjzŠšªºÊÚêúÿÚ\0\0\0?\0ß{ÜÑ—^÷î½×½û¯uï~ëÝ{ßº÷DËså;Ëä.çÍm¾šìTè®”Ú9œŽØÝ=Í‚ÛÛsxvÇbnüKønéÚý?E½qyþ»Ù{ci×RTãr;“-ŠÜ5yhª)(¨)…ßÌn‘ÚYF¯r¾%Ã%p**S>”¡Î@n¬Ç&ŸàÍf7ŠCæoÎm·¾p¯÷T½…”ïŸô&O!á©F}ÏÖ½›¶7oKæ±µST–Ž=·KˆÕi5¯íÃ¼™–{ekvÁ\\à|ˆ¡û*z×‡JÙèPèžÉíwÜ»Ÿ£þBâ°Qv¾ËÆA¸ð‡²èê±}{Þ½kY_&2“~m¬B»%‘Ù»·o×ˆ¨7^Ý–ªµ15Õ4µÕSÐä©\n¦¼·„F·v‡ô[ˆóSóûOò=YX×CqèÑ{-êý{ßº÷^÷î½×½û¯uï~ëÝ{ßº÷^÷î½×½û¯uï~ëÝ½ŸÙ»W¨ö}~ôÝ²e†šz,v7·ñ5Û‹un­Ã•¨J,ÒÚkùMÇ¹÷BT‚’ŽÝØ³$‘ZÛIs DÀó>@u¦!EODò¿z2íõJû‡®ºgâŸIaç¡ið›7ädv>þìÊ¹ÌÕFšê1·àØ]Q=(€É71¼Ïyœ®f‚=žð§w‘ \ZpŒ+C^!}:¥dlAÒó§¾Avì;³\rÓ,º‹Ô½­›¢×´wçZîjýÿ\0ñ¿·²4ôu™¦¯÷¶oµ7–Úßx¼v>j¹¶æåÃcª§¤ŠI±•9Hiê¥§fâÊÝâ{»uEš©Ã_JŸ—™<+J¨>k¥†z9>Êzs¯{÷^ëÞý×ºì{qÈõî»¿»k_^µ×ÿÐß{ÜÑ—^÷î½×½û¯uï~ëÝ]ßÛ» ú‹±;“²÷~alnºÚ¹MË¸w~è¬€ÁÒÒBR\nœ\\¾„†Zùb‰E‰y$U\0–Ú»8e¸¸E…I—ˆ§òþtëL@ž‹Ý×Ò¿\rþü2‡äÇyuÞÉÌoN”Ú{Š«sn,¢á)7ÆôÜ{Z—³»#?‰¤¨iªžœå75E}\\|4Ë8.QYG³+Û9ïoî…´DªŸBh	í4 ~]6¬OF‹!ó?â¦7¬zã¹æïÎµ©êÞá’Xº«zâ7ì?b55&Bº°ìæÁŒ…NàL}&&¦J–¦ŽE¦X[ÊRÞÑ\r¶óÅ–œj|½+ûGù:¶µ¥kÐ#òµöðøò7\r¿v¼}}»{Åº®ƒter2â6öàÛß\"z³wãö²Qdf‚\'|–{~í¼.6’¤Å]eLJAiÇµ–pJ©lÑBšþÐ	Í)EÖsè|«Õ\\ŽÆ¯Vì§z÷¿uî½ïÝ{¯{÷^ëÞý×º÷¿uî½ïÝ{¯{÷^ëÞý×º\"Û;mKÛ7û‹²·luûoâÎauHb%ËÉ>Ù±ª»»û2qGF›¿-²;mízjéžzŠuF\ny\ZÁ9ÌÒ]¶+t?©/sb˜àäÊkþp¤4;¤$ðŸdÝ;Ð#ò+kdwgPîˆpµòb·Ü—¿öîRŸM’­¢ÍõÖâÅïz6ÇRUþË×ÖjXõŸ“kû[a ŽàÁ^4ãŠþÂ>ªÃP§CL2y¡ŠcÅå%ñH-$^Eãp³%ìÄ{K%5¾‘E© ôêÃ¬¾é×º÷¿uî½ïÝ{®^Ôu®¿ÿÑß{ÜÑ—^÷î½×½û¯uï~ëÝ\"{#bá»?¯·¿\\î\ZzZ¬úÚ{ƒiå!­£ƒ!Li3øºœl²IET’SÔO‘U\Z”{~ÚSñ8jxú|Í<ÏZaPGUç…èlÇÏ‚ý/×ýÛ=ÉÔ=­²êööºwgÅ®Ì®é½æý»ÒyŠ­“ÙÛr=Ñ†£ªÈcöŽïÜÛz©êé)Ä^z9âx%UðËìÖk…Ûïåh-Ñ¢a€ëPÈãŠÒ„œÐÔSˆêŠ «~]5|‹¤ë¿˜3T|6ë¿’×ñóyv—Aöcmï>ŒÏäv_kõÝ\'O÷žÐÚ[Û;SM6×§Ëí˜wã…ðsTÔäè—qQ\nèiR_ÕDü\Z­oeÃ§P$~*\Z(¯¯ÂOÔ÷vkÐØ¬]‡ðÏá6ÕÜ“o¯´±÷_{î^ÛËùÙ¹Î§øî6Ö#­?Šæª>Þ¶«~ö?~åðóe¥‰R²ƒmæ¢Óë)í˜o7	bÐÇµtŠêƒO–š¯ªÕO\0k¶T«öEÓ½{ßº÷^÷î½×½û¯uï~ëÝ{ßº÷^÷î½×½û¯uß¿z÷D£â–cõÆíùÖôûr¯föžs·wmoª©ßy6ÿ\0Çî*ÊL&Óí¬KvÕåñf§rlüN*›5G‹­0QeéemI°ÆN7òÃkp$\r@¾UÏ%ŽšÕ¼ÏM%0óèë{\'éÞ€Ï‘9þ®ÇõNóÛ­›ÂQ`»jî½ŸÛÉïZ\r‰’ß£#·ëãÈìÝ»˜ªÉc*“)›Æ<k¦Mr5 RámŠÏã¤)Áã¤°oíóõê­J\ZðéUÓ›«¬ú›­zî·3—Ü5{cmm«U›ÏdësYŒ”ø,-6Z¬†c\'QY”ÊT;Ój*§žªPM,²‘›»”Mq4©c€)O•>\\:ÚŠ\0:}¦ë}{ßº÷^÷î½×½û¯uÿÒß{ÜÑ—^÷î½×½û¯uï~ëÝ{ßº÷UÍ—ïM£ñwå7yá·Æ3µ_«»/nuzîÞØ†…7ORtŽèÊã+:rŸºqøš¼ŽöØøMëŽêÊIœlcmz:ˆ§ûúÊ$CP|¶Æ÷o·ÐËã) )­HÍLR§PÀÏÂ\09£:´¹¯‹Sü¥øW±þMv7tSÿ\06Ê/˜žíÃGX|\\Ã÷¾ÞîoáYóZ²°u×Ju´yÜõu}\re=>3	G‚ÂÓe7ž*÷ÊTÌ’ÄªæÞúk8í¿v´jœY•€¨K\0’*q\\\ZéJ+j×Zôs¾\Ze2»šïß”;§¯ëúß=Ù}“’êM³´÷-:Ã¿0]Uñ³pn¾¿Û”»æ(êki1»ƒ=¾ª·>tÐRÊðÐÒe©áv’¢9¤d;ªÇl\"°G-áñ4¥I€©ü:Aù¯ŸVŽ¦¬|ú=Ézw¯{÷^ëÞý×º÷¿uî½ïÝ{¯{÷^ëÞý×º÷¿uî½ïÝ{¢å¿òYª’Ÿé)kª¢Àf6~Ðf1±©jJ¬=[e0•³•ÿ\07%4ˆ…½?å}H¹¤\n­¶Üñÿ\0!áö“Ÿ°tÛ|iÑöWÓ-ÛI>çù%Ö;~ª»É¶v—WöùÊm¶ûj¼~_qVî­„ÙÙÖ:¥dPøµÈÔãg\n®•K##‡ÙœÃ°ž@£^ §‘_Ì—çÕYGF7ÙgWëÞý×º÷¿uî½ïÝ{¯{÷^ëÿÓß{ÜÑ—^÷î½×½û¯uï~ãÇ¯tVûgåNÙØ;¶£©ºÿ\0doÞÿ\0ï…ÆÓäGQõF2Ž¢m¿KæÄäû?°÷n¬z‹	Zº$GÎå©«ê©Ü=s”‰Í­6™çYˆuPÖµ \"´\'Î•¡\0ñ§Ti\0ÀÉé7Ð=]ò\nîîŸÝÿ\0ë<E_fl>›ëÞ¾êN´§Êf¿ÑvÑëjžÇÜÙ(7Ÿhæ©0õ»3›¯´+MI‹Åbèéi\"Ždvš¢W/\Z4‚l£“ôËc^&ƒìPxž\'‡¥­Ib3ÑžÄì}•‚ÉÔæ°{;jásÆV­Ëbvî#“¬iß\\æª¾ŠŽ\Zºƒ3›¾·:&þË\rÕÃ\0ºñò\0€W«Ó¢ŸI˜íO‹y]óQ¾1[Ç»ºCyö?bvV?yì\rµ6g|ô=&öÜ´™¨öáë|TÙ]ßØÛ_’ÉVÅ¸0«WÇG*ÒM‰J\Zu­ŒÕá‹pXÚ$ð®@†´j šPz™$›¥jj½\Zn½ì]‡Û?Ø=g»öþûÙ;’—ï0{£kå)rø|ŒÚ)DUt’È‰SKPð¾™©çFŠTI”Ïo-´ÉF8\"£¥§¶zß^÷î½×½û¯uï~ëÝ{ßº÷^÷î½×½û¯tÕšÍa¶Þ#%ŸÜY|f†£Ÿ#—Íæ«é1XŒV>–&–ª¿%‘®–\n:\Z*h”´’Êèˆ ’@÷xã–V	\ZÄûp:ñ qáÑ:Ù½•¶þRwW^ïŽ«ÅÖn>œéz-í•§ï	±’c¶®öß{³Û6Ÿkumf^ŽŸ%½væ;oVÖWdóØÔ8¥j()k*åZ¨éä‡èleŠi!øsƒ‘äA®xãfÖàÐÛ»{ÓkìîóéÞ†ÉÓd›r÷NÖímË¶òøìÍN*õL[>¯!ŒÊdhñX\\e^gºfžŒVÖÑ½Bãj	9I<H\"³ymg¸Šäp!½xðòÿ\0(êå€*Ÿ@—tî,/Çn÷£ù;½°Ùyº«põ?Pvfÿ\0Âíù·úhnÌöûÚ»»wSâRmÅAÖY†ÝJ\\ÆJ\nZÚ|UTÕ\nZ?ºªEÖq}e“Ù¤€L\Z JÔ€e8ñ¥MQŽ–Ltp6îãÛÛ¿‰ÝK=†Ýg=CO“Àî=»”¡ÍàsXÚ¤Òdq9|dõXü\rLL\Z9¡‘ãu7eRE$LVE ƒLúŽ#§*ž½·Öú÷¿uî½ïÝ{¯{÷^ëÿÔß{ÜÑ—^÷î½ÔžO…Æä39zÊln+CY“ÊdkeH)(1ÔòUVÖÕO!	5-4M$ŽÄU$ûq¥uEÄõâiSÕcwE|¦ùÉ„Ân½›ó7µ~ôŽsír›w­:«¯vÍ/cv&ÎQÕâóÝÙ™‰¨û\'bÿ\0{¡Fž,&Ú«ÀÖÐã*R:ê§¬ia¤>‚çoÛÇ%·‰p0I\0Qšw\0=CÐÓ‹$Hùƒ¢í/òZÜa°¬óù„Çb2Ù\nº¾°›uE¹¾:Vbë¤«s‹‡©÷^C3¹¤­’JŸ¹¨Éd÷^Z¾|ƒIR$ŒÉãW¿Z±(ö(SÈé\Z«@8\0p¥GAALûÂ>½&{Cùxçz‡3×y:.ñØ»z—xï\ZNº‹°°_{—kîþº¯ÜK<;?pb»3¨¾QàNÍÎÒî	€£Égq9|%VV¢(kÒ(&r]·¿Šrí\Z»RhÌ `WN8P­)_.ªcaÑ€ùÓ¿\'ö?_SVoOæ¹ó»–þ°ö¥\'Iî¡Ù=£¹³ôÉÛ8}ÓŸøŸ¿zÛ´»7ybcZÚºLfÒƒjãr¾l¥+ÓRÉ<LÚ\\YË$‹oc¦B2I\0Jš–H¼05âµ§[`Àe±Ðq´¿–wÌž½ÎSî­™üÀj!ÎÍ‘+š9LÊ¬µ.^©qõpC-~?y|Þß[o+ö\Z•¨ŠŸ%‰®¢dO°:C_¾,Ž¥k:\0)‚ƒòªBì?1Õ¼6þ/ðÿ\0Ð]$wgÂoçÖLoa|bù£ð÷vö­^Q2©”í¿[‡¥ð÷åÆUQLý·ƒéœöïÙYÎ6h(d Íâ¶îsGRRË”’‰Å?»þõÚ&ŒÇsfú\0íµéM@y‚H©­+ž«áËZêêÛº³÷¶õÃWí.æÛ{[cwÿ\0_Óái{ShìÅ_ºv<“æie¨Ão^¸Ü9œFÝÏæzëw¥%A “!Ž¢¯¥©¥ª¢©‹ÍJï!%å²B|kvÕjÇ´ð#Î„TÐÓæ|‰¥GN©\'âè~ö‡«uï~ëÝ{ßº÷^÷î½×½û¯tY;³»÷ÖÙ«ÊußÇÞ° ïõ§ÚyØ»K5¼ß®ºókPÃM3`Geö\\[gyt²Þ¾¥ÀcÓUY”–)¥\"š‚š®¾˜ÆÚÎ7Ušê]…IõÅGøF\rk•\rFcÁEOTá€ù…Ö‹°ª$ùá¶7W{|€Ù;š“¶z§¶mVt^nÜµ]M.ÐÚ8žŽï\rÓ«í×ÇÎ*#Ü‡ûËç%ë°õô\Zi©D¦É’÷\\ë±\Zµ\ZpMSIù«âÖ¬÷ŠžvÞù¾ŸtlþöÌü…éú®¦ÏÕQlíÝñCb×uvúÈõ~¿\'Yˆ‹±$í]¿Ÿ}Ë¹÷þÊÎTã×vÐÑ‰¶î?µéG\rLôqä*Ëä²áxƒµr()Ž49#·$ŒÚ6†­{z>ù³ÿ\0KM^³(Ùô}¿žt9·*A¸ºå*²¸	¥Mß’ÊK,±EKYL²ãh¢ZˆêLSURùHr u(+¬zú7Ÿçù!î€Ìì>ÜÝ›ÿ\0zíNƒÞ½´ŸãÆ;nî>Ôÿ\0I{x×m]õ›ÝxJÍÇˆêÊ­ÛCŸ‹-Ö˜êY\\¾u1ïF2ØÉ)ã¬H²dÎÒÒ(áI.\"rÒ|:i€H>•ÈªšT0m˜–¢žSþãù¥ðÿ\0cv[¡r}ŸðÃä6S23µ!ÙÍ/Ã¾ÄrT8œ®wyšYÐgpá·.B¢SÜaX½—X*\'¥û£\rM‡¢Êéãð®4KoP\'¸ùŒ‚1ÅB!À–‹BV‹ñ7åOtoýµÔ4Ÿ.:sjôcwžÛmÇÕt{;uîýÏ¶7d´8JÅÙ¹\Zmû°:ëyl.ÉÄmêGË.N…þï$–ž¢I¨òô„7Û|ãÉe!hã9˜0u5rx×5«©åšÇ£ÿ\0ì—§:÷¿uî½ïÝ{¯ÿÕß{ÜÑ—^÷î½Ñùs¼2[ƒ³>,üKÆâ~ïòS{o,ïid«(««0ÔýÐxL^øßûf´ÐO‰û+?—ÀmÒ•7¡Ÿ‘­Ža&¤†S¶ °Ý^±ÃZ]\\E1ƒæÈ yÓRªúô3üªîZŸýØ±›lEšÛ´˜:\r²›ÎL”[b»wîýÓƒÙ[?–|;˜‡3¹÷%\"J»K2‹‹Ü#²….®V)	¡É§\ZŸØµ?—Wc¥kÑ„µ‰’8?ì=¦‘<)^3ÅI³XdW¢?üÅèv¦OâW`c÷WCÛøºÍÉÔÐQí3uÛr¦“pIÛ{ m­ç‰Íbqù|Ý.àëìçƒ7Š‚Š¶¶º¾‚\ZH©çiüLe´©¢P‡KqÏá8âxdÐW8¯T’šr:jù‚ÈT|‚øE¸ó•[‡ìëSÛ“mÔýåhà:WzUaû.»{ãúçpQáäÛÛtf lSd6zåRR*k#†LTê`•|ÊEJLxµGñ\n%É#$\ZSÈšŠ‘RƒË£ëì‡§z\r7ePíÞÎën°›˜«®ì¬/af¨s”‹Hp¸Xºî-«-e.]¥©Ž´TæëŒRb‘OÛË¬¥—R´¶i-žãX¢µ)ùW­W t÷`£ëŽåèní¥_¶¨Ü›žãfüÕ\ZZžÎì¹êò;\Z|ŠËWä¶¿kchbÅË\"I2Gž¯¥€¯oj­+qkqlíÚ£RüŽIÿ\0kJ³z:£a•€èØû*éÎ½ïÝ{¯{÷^ëÞý×ºCöfûÅuw\\ïÎÉÍ„lFÁÙûxd\"–®ž€TRíÜE^VJE­«e¦¦–°Rø‘ä:C¸¿µ›‰â„~#Oõ}¼?>´Æ€žƒŸ½y’ØÝkA–Þš»¶»:JnÌîÃ-ÍöNäÄãW%K•#¨M¿³±””˜$\rt¸LU$-wFfzþmÿ\0b˜_³í5©¦*Izª\r+óè?îÝ£µq9mÓUÙ{[Ø	òŸhu_s`3”0ÖÒmŠêÉ\'Ú{Ks×Âézí•š«ÌÒc2m©%ÂT}¶IiÖ®Ju–w2´Z!”¥Äyø€?.#çÃ:GZePjFE?¼¿—ßÃþ®ÀîÍë¶v¶ãÛÞY]Ñù}ã™ìNÉìÕºò½©¿¶.Ð¢¬©Ü}•¸7ææ¤Ýpã£’Ž,¥-U,ÂŠ¦¢:©%€ÙÙîW·@âª• \0\0AjPÄŠPS4ëLŠ3åÕ•äZ„ö®ÓˆUQÅ“Mƒ¿¥ZI6UUnB³\ZÛƒ®£’jNÄ\n(våXŒTa˜´Ù—ž\Z•qŽI)jC(Ø\\S#Ñ©Ž\'íòüúsª¥“ù||}ùuòåžcåÖ¯Ýù-ÞÕØø¯¸³]bô»sruQîM¹ŸÜ[ãª³[OölÁ*Rc7B«…ð°¦¡Öb}.áqekf-M”œ€x–¥+ZTPš2H„Ï«£—³6VÎÆï<OItN)¶ÞÀé}Ë‰ÜË»âÍnÖàÉoú<V3²ú¿/¹·4ù\\öüÎd¶æ~®r¿!“­©Æc#ÇÒZ¾6¤J×3Çnn.^²¹\ZW†;†ª\npÈÀÁ\'…6¢–ÒBÏÉM‡˜ßÝA¹àÚTÐOØû@ÑvOSÍ1Š&¥í½ªMÑ²ÄuSTÑÅK[)Ú³$‚	(+gŠu’žIbt6ˆ®ŠäDØo³ìó4&ž„×«8ªÐô!u¦ûÄö\\ìÍÀÇU±¶NÔßxXk©ç¤®‹»°Tƒm%TPÔÒÕ%A‘Èˆèà†PAÓ\\F\"žhb?aêÀÔ:[ûg­õï~ëÝÿÖß{ÜÑ—^÷î½Ñ,ù)/ßß7íY¯5ðwŸ`õm%6-ê\rem\'itgÔTAUOxëp4Y]A_X“:E¢J‘®JxÑŽ6æ/ouÏ\0u*×þ5ÓoÅOÏ©ß?°8ýÉñºñù&	MO†Û¹èÝ®,†ÖÞûcsâe!Ckå1±RkXðO¶v¦\"íOâ¡¶ƒü­Éðž†îìâ:û3/ÇOôRý±F>L/uÝ]×Ó-t_Å¨³5ÛM¹qsMi>Ú¦\nz±áuÁ\"¶7³ýZ¿‡­¾\ZW‰õÿ\08ûzßv‘§@ïËL™Ÿ‹Û‡¼±}Í»%¡ÙØ˜êöúg0¹<N÷Üù|6Ê©Ý½;WÉ®ïÛ=Š“rUO´¥£ÉÒdaÉšh¾þÏP®Ø8KÚÄÄE“šh+ÝŠõÇÃ\\ƒ§S^=+;c#O¶7wÅèàlæzª»¶fÚ44‘îÈ±ñÕÑä:‡±š¿tg©*·FÞ¦Ý§G‹û€†“30š]pÒ$Œµ´ž…Cý|uU@¼@¯P(@?·xæ‡Æ§Ï¥onn.éÀ/^ÃÓiµ;|ÿ\0emlbVoÃ}ƒC×½_SS$ûË~ã §Úûš³{çñxús/²ûªÊ˜ÚJ˜¡ŽCí%²[?Šn%+D$\nV§Èqf¿—ÕqAÐi¿ò4•_0>7íÔš4ÈÐõ7Èýß$4´ØÈò/¶˜Ñ™%ZŠ‰¢®ÍGäH‘ÂFC9_F¥°ãl¸4Çˆ?ãJÔÿ\0Žª´üºò¦83•Ÿöøê|Ô{ËåZWÕbê¢§ž\'¢êª\rÏÝI–1Õ#{s3×T5Ñ²–9à”úOºíúU.Ý¼£ãò\'AþOÖŸðý½?e}9×½û¯uï~ëÝ{ßº÷E·æ\Z£pü_ï<E,uSUõÞtø()!¯«x©¡ZªOGT\Zž¢E§Î—\ZxçÚý´Òòñ#ü#ªIðžŒlSÃUU4òÇ55Di<Dë$RÃ2‡ŠXäRUã‘A ƒí=ÁÕ<Äâ¬Oí=XpÛ…p=ïÔ‡·p5qÉI¹ñ“Ö²Í–ÇTE7rb*óÛ/~>®#®rã¥\rèd”C©5£)/Ä\rÜFUÈ¡ý ˜=h÷)¡è­ôu¯ÉüŸTbé’º»\'ñûK‘ù%UQIS3l|“‹hÕlÉ:~zÚº:ZMÃ»¶nK\'šÊfié£1â\'‡3È’TS«/»…mRæMY”ög%+Pßí¨\rkB	E:ª6 §GÖÖžÓßï.tc—¯šµ¶xÙ¯ýÚ’¶]È°&ånÁ8£ñØ ‰©FkAûw5-\ZdgOÒÁ¨·\ZçÏÊµþ_:ç«ùôW{;+œøãÛƒÚ»\'¤ûºk>Cm­ƒ‚\\XÓÝ?¶zÚŸsapUÀåóÔØŽ½Û{ÓkgqÔµ;©áÆP&ß&¶V–jTsp/à$¹T1r+ÛALb Pú“\\CJ\Z!$=})²(þ6ô^ÐÛ{ûxÁžÜ4oM[ÙYE=·çovNæŽ§wn©¨è T¥mßØ›šAK\0R)¡šÑ>ÒÝÈ÷×<Qö\0h®ÏÎ‚‚¾t¯¬£J€z²¹Lv’Íf+¨ñxœ=nS+“ÈÕAEÇcqôòU×WWVÔË5,-$²ÈÊ‘¢–b\0\'ÚFvªKzõn¿Œ	GÆŸ1ã«¡Êã×£ºœPdéªàÈSd(¿¸xI[M_K5M5m=M>—Žhä‘$BY¨¾ë.ª3â7øOZ_…~Î‡_izß^÷î½×ÿ×ß{ÜÑ—^÷î½ÑPù¶*ó=Ñð›=MF^-¡ß›Þ¿%’v¦ûlf?+ñ›½°h“%D2ê—+—¯¤¦„Äc™fuÒúK£™ÙSÁ¼RÐÿ\0˜t>^´§M¿áûzïçN7/”øyò6\r4Õ™Z.©Ý9êZJf¥Jš‘¶i¼u0S}ó%TËIŠF²¬äÏ¿mt«üh“¯Iðž>¾­R—Y¥ÈÑÒ×Ó£šzÈ¦\"èyG1È.ÐûI5|iñ$ŸÛž®8tUþdæv^[¨³?³Ë6suü«Äîž†ØûŸ¥Û»§v\rß¶rT[ç)ŠÊTá÷X\\W_l9ëó™LœÔTôT´|rÔOMO:Í¾)|ar†‘ÄCEF Ž&ƒóÍE$8§™è¢`±½×Õ;Ã«»G»î‡ª>)o-ÙñÅrý•è=Ý›ì^¼ì©áÄíŸ—XÕ°vèëzl\r$Û[oåÒ:ŒO‹Žlž ÇMAY1«5µÌRC	Y6…`‚ÐðÅ¯`¤Ž©F1øGÙÕ¸{¡ ñéþ‰Žül–Oç§Çm2cµþ8|žÝ™YØdI>_züvÚøªHZ*gÅK¨–a4©9ž(ŒJÊ%*if×9#‹¯óYò Sí>£ªíìÿ\07J‘ô˜·ßßòY\nÜÆ*£ò^5ÄWâL¬’ä2)Ü˜³ƒÌAB‰dÂçhj¦ŠFxê–)V6ñ‚‰M¼·‡zª*<>þjG×›Š}¿ä=\ZÏe_¯{÷^ëÞý×º÷¿uî?’yÃütïì´‹TñâºOµr2-@µmÅÏTºÒµTSÒ­IXŒÈ{jR.\ní¸½·ÇWòóþUê’|¥·\\TCY×›\n²”ÊiªöVÖ©¦3øÌæ	°TÂf0ªBe1¸Õ¡BÞö\0{jä<•ãþÇVèøÛO>»5Œ¯O´þLv]UL”K÷Tý§Aµ»ê¤Ç¨iªŠ‡1Û5Tbe,íH¸`UTî\Zdú[´g#ý)Ð?nž©5/¡èÍ{.,Æ•$Ó§:\nêäû¯oÄ•3}µ/Vï	*èãìO¶§×îíŽ¸úš¾¥Zv9¹ŒxÚ”§Üo*Q5#ÿ\0s\Z­\'èÙü¼Aåž\rçÆŸ•<¸Ž½çùt)ûI×º+ß%ò±d%èî¦†]‘í®óØÉ-/Q]>Öêš¦î­ÏÀê”T8ÕëúZydœ&’¶*OóÕP‚g· æbHUBØp1PGÈÀuGü#çÐ‡òg~†îä¦aKõe­<…Ds¶ËÍˆœ´_¸HAºú‡ãŸmmô7°Tb§ü¯Ið¦ô~©ÒÝ?µéâZx6ßVõöjŠˆá‡´±èâIêÏÝÎ±¥0åýÆíê\'ÛwÍ®êá½Xõ´øWìèQö—«uï~ëÝÿÐß{ÜÑ—]û²®¯³¯tH~Xc›uöÿ\0Àý™Ž«Aœ¥ùC[ÛQÔêWeuwGöÊn\\Ì±%Tÿ\0Â)3{¿–KÃ¬ËRBòƒ:$‡mbK¹éúa)ò\'R\Zyf€Ÿ\\tÔ™Ò<ëÑŠï(¶lý-Û”½‰–8…_Ö›ç¼s*$iq{c#¶rtªÊxâWžj¸qõ2cZI%Òˆ¬Ä’ÈÈ.¢0­d®åOõ|ú»SI¯«×ã7ó1øÛ»ºãæÜÀv÷Él·Zí­¸>:õÆ#%šïLWkí,ÝÛ½™·{`ËKK‘êv^ö¨’7•Ý¿ÁpØ™ÍERäÊëj›ê®¨–ºªÊ†¥tžÊ¿ zm\\i‹tr:k¦÷6/uç»Ëºò8}ËÞ»ÇßŽ¨©Ù}7×ÉT™:Ÿ¬\'ÉSRd*1Õ(Ò³pg&‚–»tå!†iá¦££Æcñèï.£e[kPDôÏýGÏ9bÖU?qèÄd±´9œvC•£§Èbò´UxÜ•\\k5-v>º	)k(êa`VZzšiO¬G´Q±ÖUøÿ\0W®EEˆæÍíOöR²Ûc þEï*¶Ø¹ÌÛíŸÿ\0!wv¸°;‡$/S·zO¸·œºq;{¹öý42Ðâ+²-IM½qÔôï²æ¾ö—Ù¬¶ÃpSujƒÅ\0jAû*8c…x_ :l­Ã¬]WÙ}Û?~HÑmNÅÇn¬Ç½	×9M±†«¥«¡ÚÛ¶7möïžª¦\nF¹žiíD\":¹RÓ<rG®uzh§¶ÚâI#\0Jä×‰Ò•à|3C×\r!¡áÐ‰ßù‡Ãw_ÂÏº­–ƒ—îý÷ƒ®w2&6¯/]ñ×¸¤ÚøÚÙ/öâ¦³/J>Íd±z¤P‡YPY±BÖ÷ÔðÏíªÓöž¬ÿ\0‡íèØû(êý{ßº÷^÷î½×½û¯twÌØÈ:;¹$ÍUµ$u_`.F±ä–žŽ]§–Šy!Š9iåš£Chèò=•Hb=¬Ûõ}d:FkþCÕ$øO½]G.;¬ºç5˜ÉèvÐ£›*I¸ùivö:	(dŽP$ŽJGC\Z[}Òïºær\rAn>½m~û:/ÿ\03Û·7Þÿ\09)²™UNÎÛ=ã×ofcé*ðµyLmJ|gémÁ»N]q•ähEnOpÄôôùŽ \"´ˆÄ}©ºX–ÊÄ•>)SëÃ\\Ÿ—§ZZêoOöFóÙoWèŸííÁÙYï›Ûÿ\0ûcéí…ÐûsE»+öfÓ Þ²7&ç‡=–ÄâweVö“å:ß¶’‰âjm·£;=|\r’’³\ZÔ¨i\"Â›d4rdf­*h\0¨áJq­jkò¡© ®¶Æ:8Êú¿E¶ó3òÿ\0áå&ç—KœÜ[äî®¦ª4)š­ÝÑmÞ´Ïd0x—,¹V¥ªØ˜LÕm\\iz6lt-8ó%!ö¢S¶Þè®K_JPþU©×=6ÔÖ	?(YWã7È¦o.ôÛ¥ÌGÊŸèÿ\0pkh¤ˆã‘ä1àiöÏ÷>Úœu«öqër|¡;dÇ;3hÅ\r[_,°Ñ&&‘c`Ö]@ Ø_ÚiÉ2½z¸éQíž½×½û¯uÿÑßéîø>uèË^úûõuã‡^è²uvÚmÛÝ}·ß9ŸáUëNbèî¢ž’G©—×›BjjþÂªyøir›Ë¸>ú*±+>;nâ™œ²•C+™|+X-Ìo´ð¦8R„ –+Õ÷ë¿—ôÙZÞ„Ü´8Šb¢·tuU%^ÜÏ}è¦Ý¸JŽÙÙn-“$uî}Ùjœ~DãÊÔS³#¨*[ÛÈ\0ê Òßà4?`4\'ä^“á=Kø­ˆ«¡ê8sRÕÑÖbû{ö—jlÅ¥ÅÅŒz¼í~ÉÝ=²1õ–\"ª³ vþç†¢¦Z•&¢¡ÖUWB=Úú@dXÖºÑ·Ú >Ê‚1é×PŒ‡´}[ ©{{Ü»â·±»[dÒìŽÆÝndín¼ÏuÄÛº¯hUGC]»ö.?rEM“Ü]w”«.¸¼ÒÃ.M\"iiŒ•Ý¸·x J­©`ƒJù`œ>Ôu k\\t¿Ü{kno_kný¿„Ý[_pPTbóûorb¨3¸Þ.­uXì¾)OUŽÉPTÆJÉÑ¼n¼G¶¢–HdŒÃ­U[ñc¯v\'O|µ¦ê=ƒ´vç^É²¶7Í–æÛ;OkÑì³[IÙ&:ƒµ6nK·1ñ¾>Z8v¾û¡€ÔÀ`dŠHÖ(ÓLhxí-ƒÈI5\nÖº¨æû½4¢„êz°®ýë)ûoª·FÑÅU®\'wEèëÅ«D›W´6fF—tõÎçô¸ñá·†&’Zˆ™^\ZºA-4é$K”YÌ\"ký›`PqO•kJñ=]—P§ŸJ>ªÞØ½k±wÌÔÐQÖn­†ËäñôÏ#ÁÌÔÑEükMûÿ\0î+,³S‘%¤Sœ.âðn%E€ãýŽ8ôùS­©¨¡Ún­×½û¯uï~ëÝ^åh{W´öÇ\"c¨ÛG;§º¨¤‚¡ ÈìM±Ÿ¦ÇõþÉ«œRMJ ìÆ¥5Ó¼‘\nìFÛÉS>¸e’)MmWéí¥¼eî=¨h4§	 ŒŽ›næTòéNÿ\0\':®>ü¦øã$›Ù{©%‚<¤o¿!ëFÜ©´GaÇ°cíYðì\ZžÂ©ëÈçÏG‡‡!-_ðºIåeS“Hìe’4º‘Ô«0$j\Z´“BÄWU+ŠÓ‰ã\\u²à#¢‹ñƒtm±Ìö×cïœ†Ëê­µ”ë£Ù]á¼·IÚû?kï.Óï^áÞû–¶lM,ÙI6JââI*ˆ(–}(±¡üR]Cn«;†+æx@>ÑZ’uPV½6ŒOV×]Ö½¿µé7¿TvÉìí›sìëýÕƒÞr®UŠÞ3[z·!Ž’xá©™šÕ]IãÙÖó[6‰ã*þ„PþÃ‘ùôð äu&,«?`Wàÿ\0¼»bE§ÙØŒ·÷::{o:6¬Íç(Ææ¬«þ*ú¶¾LPý­4b–«¤¼ï{+ú\nÞq9òü±þ^={Ï ç¸¾P|qø÷S…¢ï.óê®¦Èn:jÊÜ7o¿¶r™ºs$uõØ¼nR¾ž¾º‚†YU%š8Ú(äuV`Ì´6WW\n“ÔÀ?ÕCèzÑe^\'¢\'Ü_è—¸þNtGyÑ¾3´ñ+ftÖþøÏØØWÆçúâŽ£uü‚‡¬û“%±wý.Ið‘îÜîÙÞX+ÒšF’£BCëñû:€Okdb#CwëxRÉ¨R¼U¿*W‡M\Z;ã‡G»§žîœÿ\0AÔõvüÈ`©¶êÃœí\\†/jKÓu››+ŠL¼HòVnoï^SvÔlú±‘™	.\'í¢z±=áöV-\Z+t»Y—]kLÖ™áJãRj)Lôîª’½1üwŠ¿«ßßâ9ÆtŒ»Zn¼Ÿ%WU’¨—¦·æ>¾³bcfÈUI4ŽÛ+3‚ÍíªxžG™q˜J9¤æu½ï¿]b½Uøo]C¥qBO©¦\0­.*¾Ïe_¯{÷^ëÿÒßƒÜ/ÑHNÏÞPõÇZv\'aT$/O°ö&îÞs%D“ÃNðím¿ÎJ•RÓVÔÅ%	ÑÃ4Š·*Žl¥ËhŒóÇµ	<ŸZc@OIŽ{\"£­ú§v=vA³}»×Jpç¶¿#.áÝ2a©j÷^ãŸ!”c’®©Ü;Ž¢ª¶I§´²I9f\0’¯%ÜHÃá® ãOË¯( ¢yüÒ»§`üx\\¾?1™£¬Úµ{·¹j0{nŽz½Å¹©:¬÷§díz,A‡	œñÔR÷N3gJ*jˆðy%”C\"ý¢ îÄ¨ÔÅTÀTÑ¼ÿ\0„ÿ\0„ùuI:]|	ùK°ûë©v¾ÐÇã±»3°ºÓcí\\>äØxÝËI»°G†£M¤w7^nÈ)1Sní‡çÀ×á^j¼~#3‹Ìc*±Ù|f7!O%0¦çha‘çV,ŒjM(AÇÀV Š(E	\'ÈÕ\0=?e½9×½û¯t—Þ{ÃoõöÒÜ»ãväW¶v–#¸s¹ŽIE&/K%]\\«(óO(Š\"4Vy…PIÞ ‰¦‘\"^$õâ@=R÷Å¯‘ùîÃù·»w¾ðÚ¸½CÙ»ã9±6kÃŸ“qPîm£_Ô¸:M¯ŒÆfÌT˜Ü®{iö7Å}åŒÝU8È¤ÀÐçå¢ÆÐWåËU0šòßM‚*Ô Eò¡:IZÓ¥#D¼:`ÿ\0•z¼Ÿa®Ÿè°|`’“AÞ;§Æâzóä§ocqEQ=Lò.ýÈc»»+UXÕ´O>ãíZïH\"Š\Z1hT»qNÛY*K§ä½£öÐþ}Q?ÛÑöSÓ{ßº÷^÷î½ÑTè§]ÅÝŸ/7³VTÕµgì~¢¢‚¬ü+×}I³7$ÔXçt\r2§pö]}V…bŸs4¯`ÌÄšÞÕ,ìP¨þ`hÓ_R:¢üoùt0wóÂõŸWïþÕÏS}ÅUlãØšâÅœÆB—û±µs5õ2áè#sU”¨Ç¬ôñÅYªí\nŸÜ £µIqHô$€=8Ô:uf OU¡ü¼þ8õ—nõ&Êù[Üküg±;+„|FÎßÙMÁ»öG[ã6v16T5[7®7ƒË·6®âÜ¹,NC/U|yÎGüY¨Ú¯í¢HTët½šÖG³·p\Z–\0wV¸ùR3^\0àäµ\Z\ZŽz7]§Ôy±«¢îÿ\0»k‚Ý›Z¢*¾ËêÍ¯CŽÛÛ{¿zâ#[&oWc(©!ÇÓÛðWO“Úyr!œäâþUP¸êú¢ˆ­îÖåZÚð$vµ\0Ògg¥xš®VËÇ¡\'p÷NÛÀõŽkä\r$˜]ÃÕ	ÕXÝû¶sZš™w>ð–¾\nÌžŒ¢|w‚dÜ‘Vãéñ1¤òUTd«Z\0mFRÐ™ÖÑÉ ã€ÆIÍ¯ðñ={PÓ«Ë¤ÆN‡¨ë|>K³;6Ÿžù5ÛëåîÝöþ,•u-ut’äq½G´3	*ñÝAÔÖÛØÈdþ*w®˜K­­ªžû…á‘…¼-KXðØ)Sè}içò\n‘)“ñŠWËžµÚ_wvÛù²¦Ý›\'ie¥ÜËÙ;`KŽÇlÌÖïÁWlñ¡Þðí“Žx¨·ööÄüz›fÌôRÑÇ™|õ<•ÉQ5$\"ë)ä½‰à”VAÁ¡)ùkÕö.)šÑ€FR:µtX½RD#´ÇÈÒF‰nª«!eÿ\09©\0yàd/©žçÓÝ}Â­†ùÕU”´ÕŒ»÷ »“ªýÇÇCþŽ·ÇPæv´$)Á]Qýÿ\0Ì[Q’8ÈÐn½K6× Å@>t “ûNŸÙÕ?Ñ?.Œï²Þ¯×½û¯uÿÓßƒÜ/ÑE·åÞ7=¸~8v¾ËÚã&ãì}º:Ã\rO—¨†–Ž®«²2;&Za,ÒÂ†ªJäÆÕv”(ÕíºEÔnÊI^áOU ÿ\0€©\'Âz14”±PÒSQAþbŽš\ZXuŸ<IzÏ:_ÚW\Z¦uNêšŸ§íêýUfë?Ü;çÝ“ñáûä¯@ô—Ç*I²TQá¥éŽ™í¼/ö¿aáÞ–¤6F¯½ò};\\°‰	Z¬ÝÃÏNu	ãX¡cðÑ‹ã‰e* ãËQÇ‘,F\rI©xt6v7ÅfØ=á–ù—ñ¤ïl¶Þ_·ºâ¿1‘ÆlO»2™œ8÷ËRí=‡ß&§Jq[Å©­\\)ãÇfÌÔF\n¬j8¯ÍÌ_G|ä¡8o0|ªx‘æ~yãZÜ©RqèÈtxõÿ\0È»ã¯«ëŒ4y|žÕÝÛ[pcåÀï®·ßx	R›tußcíJÂr[K{íŠÇÕÑN,ÈñÔ@óROOQ*»Imd(â«äG<ˆ?êÿ\0WV*:=³Öú%ïC…ùW”ÊüR ›‹« ÈQEòã#K[ªÄGµ©“¸\"øÝ“‰$š¦]ÁÜ”ÔÃ=H~ÓfIV³Iù,qÎÝ\r„fîPD§à‡9ûAô q£\0Ù:Ø(áÐQò¿ª6ÞG±þ:uÏ_®¯w„½aÛ¸ÞŽ£Ädé6~ÙÅnÎ‘ÉôÇqõf\n“lã±ïMU‚Âîƒ–’Š\0i°‚µR2ŒJ,Ûå‘á’âBÇõr@©àF‘%øúÐñ§Uw\0=:=YØ¸>ÚëÝ§Ø›xTÅÝ¨ëM\rt?m’Ãd¡’JæÝÌQëÑföæj–¢‚¶Máª§‘55®Iná0LèÄzŠp¡ôùztêš€z/ÝO‰Àüù¿¶é_1S/ØÝAÚUt9h3)G&;xt^ËÙ´y­¹S…1µx:ìÇWäi[ìâ‹!AT’i—Pö¶ô–³±m@4òã©˜ËXý£ª¯Æý/dÄƒÓuï]{¬5ÁKÕU2ÇOKMµÊÁ\"‚Q¤–i¬8ãRÌO\0wD.Ê¾Dÿ\0¨õî‹ŸÅVÉf:²nÅËUPVUw.ûßý»ŸYK¢;+ynzùº®khè1ÐUIIÔ´˜(f,ê#r%•H‘—_šÊ±DE™­@îãý .©Ã_3ÐSòÇvÉ¹¢Ïõ½-N£au~ÈÊ÷çîLb\Z:¼F?¯ ;Ó¬:²\nH§¦ÈÖe;;pmé+ëR\"A‚ÂMAŒåi$ö¯mƒÃOš’Èt(§¯öŸØAí#ªÈuT£ñïod¶ŸBô®ÚÍAKMÂuG_cwt&¥;†›jb“?,RÂ«ÿ\0q˜ÈÒû¬åÍËíó$·sH¤é\'åþuêëP †ù´kÚQ”ä\Zõn‹ÖÐøÕ×›W¯p]#f·ÌÚ]­í©ˆÊä\Z*LLvV[´vŽÝŽŸ\Z´0äv¯^î,Œ?Áé+qÆÒ<†I`Gö²KéÝÞLj(àp¥\ZüY\'Ô“ÕB¨èÂ{AÕº)6¥ÛØþ¯Ü;¢º›ˆÚÝ“Ñ¹êÜÌï[ãq©Ý[¸~Ò«EÈRUeö¶N¿$†0á+XkŒ\"™mZ¼iB‚I©öÒ€þUêø~Þ¤ü_Ý;Ý?{ä¨ì¿M„ÀK›û*ŠZÂê\\áÌÅÒýŸŽ’XRžJÁ¶¶äøìÔ1<‹G¹°Ù8Už‚Ym¹B¡’â6\Z$òÆ¥F==<…+š¨Ï>]9wT“à;sâÞöl˜Çá“±·wZn–iã¦©¦ìÞºÜmÅâ§W5Uu=…µ0´£¯‰~í¤b¥ûÕ•f¶¼€GRV£×U@öWöu¶Ã!¯FgÙgWëÞý×ºÿÔßƒÜ/ÑE·~Ï_»~Ct¾Ã¢–Q„Ø8ÙÝÛÞ8“ÉNõ_cU×eŠÉÈŽë\Zdò›5“¤†hÂÏQ·ŒÑ8’Œk …Ä¤QØ…û>ÃM_ÑnªjYGNŸ!«·\rvÒÅõŽÏ¨žƒt÷Fz>º‡5MÍQµ¶¥unG±·4Ñ¡¦¤Éíý‰¯8É*!þ1-\Z+:Á-,Q5´ÒÔùóõ d‚iÃ¯=i¤q=}£OƒÄwßÁ^ŒÛQaöþÝÛÕÝ¯Ù˜Í¹L´\"jm»ÓE\'[a1˜z9ätø¼mOrÒ	g€;F\"Ž\'ôÌX)†Fšßp™ØêjWþ<?ã¬?:šÓªœ2.Ž×²¾œè¥wÅZ-ï½[¹º²wgÇ.ÿ\0þCˆÈvvÂ¤Ãæ0Ý„Å¤‘böçuuŽä§ªÙ«†ÅE+%\rELt¹üJ˜ìrJ’Zî-\Z.#[ùÄzSüÜ=A Q’¦ ÐôEÔÿ\033ƒlõ/wÿ\01~¶£MÍ€ÞyÁü}øÑèîõì\r·ˆ«ÁSfr¸=Ù½;Ë»¢Út{PçèéjòLU0¾FJšiÚ\nÒîÑ®âÛ\r’TÓ­+CçL|$TuR¬{Kÿ\0«öô|ºÛ¬vPm*Öûfƒjíš	«kV†ªjjkò¹J©+óüö_#=fgrn|öFi*²9L…EVG!W#ÏS4²»9&¸¸šáƒÌåšŸêÿ\0WåÀ\0 »ºÍ}¡ñYqÖdO}nªj:æôTb)j>2|„5ÕÌ\"r`«’ xK\"ÈÒ#rÑ¨öý¹§½»t·N´ÜSíÿ\0!éŸ­éáêß=ŸÕ4ô©Cµû^‡!ò7aSÄ…iaÜRåñ;o¿qÔ`ÌÂ0û»7„ÜU\n#@ù\rÕS.§22ÄõÁYì¡¸-YTé?Î•Ç\ZyP8žª*ÃÖ=Õ8Ù/º¯7&J¾<uõ6ôêºŒ`ƒ\"}ÑÖ%ìíS=q¢\\VYvæsu­5)©xÄq,q.™Ý­l•HF¨>b´¯ííQó?gZ?Ú³£Wì»§:÷¿uî€ï‘ûª]¥Ò{ú®Œ§ñÌö*-…´ÒHk*a“zöNB`láWO’œ˜Øw&ä¦–°Ó¸ŽŽ9dOR{Qe’áN4¯qû>t¯Zc¤W¥]^ÊèŸzŠ‰*qû§:ò(ÔË;Wä!Úûo¥5-:ÍU*I’É¾?\Z‘G­ÃÔNÀ_S{Õ%¼»¡#[7ûêùõ¬\"ôB{§¯ Æ|)Þt}±-f~ü¬ìþž›·h–,†âÈÔnžíí~­ÙMÔ´íAK%en/mlF£Ùtó¬)XÌp¨‘#ŒHÖ)ƒ^Ò\Z\"€4„¯Ê´-O#QçN¨E‡Ìõh\n‰\Z¬q¢¤h\"\"…DEPªˆªªª‹\08Ù¿ÚIOSÓ£¬ž÷×º÷¿uî½ïÝ{¢Ñó#ea;â§È]§¸väÛ·_Ô»Ö´íÚZÚlm~F¿…©Ü8eÆdë(2´ØÌ½6kO5%SSOöÕ1Ç F*·¶ó$w–ìJ°ì®zÓÐ©¯INÖÍÖc7‡Æß’	‰í<ö[Ö]ÒRÔÓ\ZŽžîôÇÏ´÷kÃWWŽ’†¯gv}>ß”½E4ÒRb²y0Vò¸[ø‚öÒsIOr×…~/Ëž94…\r¡‡Ò«å–5Ÿ¦2{Î\nzŠªîÜÛ#¼i ¥Š¦ªªh:—ub·–~š’‚–9\'ÉWÖm<nB\njp?z¦XÔ‘{„»s„¢cM`¨ù1E~Êõg¯˜èË­ôü¾þžËÚš8W«õïzëÝÿÕÞ_¶;SitÎÇÊoÍçSS6†£‰Æã1´Ï_ŸÝ;§qäé0[GfmlTDO™Ý{¿qä)±øêHýSUT %WS,9oo-ËhA€	\'È\0+“ù~Êž\0ô`H\\ž‚^±§Îl\nlÿ\0n|€¯Ã`;/»·nÃÛ­·0ÕYÜFÀÇÕ×&×êŽ˜Äf ¦3îI±]ÇWU‘É$4ôSærù\n¨Ò\n-,]i’ÖÎ=I9	¦Oìkš4«QqÜÇ\'¬[+7Q¸~Z÷~#qå1d:»®z®»ÛxöªjÌnÁíOã™ÃºóŸt<+™Ý{÷®ªqê”Ö‰1û~•˜™%pº”\"m°hSÜæ¿hÿ\00aŸéSÒžÚ7ÙÓvã“;Uó›©iéiqU›wñ‹»«ròÖBÑä°¹,ïeô.¯Sâ*Û+®\n˜Á§0CbÒùBÇèV›lì|Ýo³Ìç‚qÖÏöŸ—FçÙWé)¾6fÞì]›º¶ì£9\r³½6þWlç¨Öi)¥Ÿ›¡›X)êáežŽ­aœ´3FVHe\nèC(\"ÐJÐÍ‹Ä×ˆ¨#¢yðko<Æt÷Gte±;Ë¸«·6ùé~ë¢¦Ò”=KÐÝ‹ºzÛkABµÑÕbò‰‘Û5»rÄ²J²grïË-=a1Ü§\0Go+•b>lªßÊ´ùÒ§$ž›@rÄdô{½–tçESåf‡/è&©ºVâ~Vt&c\r2[î!®¤ÝM`€•u«S[O5Ç4ÓJ‚¶†\"Y‡FÕöôÜŸûzwù\Z´Ø3Ò½‘÷ñc+v\'zõÖ5&ð\ZŠÜ¦/¶²ËÓ-¹Cšœä¦ß´ÓÉ¯XŽ:3*©’4!«ªÜÆTPŸÈOí¥>ÊüúÛþ·©Ÿ%¶èÞ{œØ˜ó½“Ô}‰²»‹`á›/Þ›påv^EÆâÙ”{†­ãÇ`ê»¯²y­·÷Uäq¦]¾ãL%Ýu·Ì‘LRSú.(j*¥~ÃCùuçZŒqésÔ·´{¿`â;e6b^Jl¦3!…ÜØzÍ¹»¶–æÛÙ:¼êÙ{ÏmdR<†ÝÝÛKpcê(rsÅQigŒ¤ŒžêÕíe(ùGÔyòõe`Â£¡?Û\0\Z€Xõ¾‰Ö?tfþFw7û·…£ãŸGn¹ò•=‡xª›¹û£_ÞëÌx´ì¡ËÔ=NKp´Š2;––*\ZðPä%ÛBíöä³¬q€Â1“óãò¡ùá¿íú#§ÎúÅÔïîÏøíÔµÈÄËî­ÇÚ{ö’GÃ¸¢éšL6oemJ‚åâ¯¢Ÿ±sx¬µM‚j Ã0f¬É%,Yb‚êôP(>Uù`ƒöƒÇ­¾YWË¥È·‰±EM&]qV|‰éD¦‘…9ûù¨·}>Yñ\0Ôdé±ÒF\n~ç>›}ëm©–c¦§Iÿ\0åëRpoO½‰ß[ª7ïYl}ÿ\0smÐöÝfComÆ­ §^³Mÿ\0ØäÁõžáÝ´¾ØÞÛî\ZÙ_oE[O\r^j)é ©5íMKPÄ6r\\E4±tqtÎ~ÁLçxT‹–\0€|ú,oksôµ¹¿´Ú_‡ ë­)L×ìëukÑ,Ù?9>5ï?“]«ñ·ò;¦ò‡°—jb¬éêjq[®›vOj¯uâ)·NW$»S°ò¸ø–’:¬NeÈméáš<H©ƒí—+i×Ó¸SšÔR˜Í)QŸSšŠzõPãQZç£‡”Éc°˜êìÆj¾‹ˆÆRÍ]’ÊäêàÇã±ôTÑ´Õ•µõrÅKIKO–y$eDPI {Gô—D…ð\'ÐûN:Þ¥õºÎÍÚ=íñÿ\0·7FÃ¯—#°r{;°0»SÒŒu^ÞßX´Ù’¥VóÙ3-EXËíXsUTõUÃ|Øég§Yè%¥ª©SZ]À²¯\ZzÐŸ?,\ZV!‘ˆáÓ®ÈÙØnÎøÃ±¶6öI3]ëÑÛ?¸Ìñtù‹¦­¬¡’‹Ã6½^S5<’’`­VE#w4;ƒIóòñ}yER‡®ºB¶«´þ;ìø»¶\rãQ¸v]vÒÝÙµ¦ŸI¾’„ävv_tEHÐR=%÷‚…ò±ÇX<.ñè‘›º¥­÷‰À:©\\Œð®r=x×=m{Ó=!¾1nMÃ´iªþ0ö\\QïÎ–ÄQc6>~«#t}ÑÑØ¯¶ÂlžÔÄ0ñÔ®^‚™`ÄnªCÔbó±	ä¤ÈcªjœÜ#Y_ÂÕGøO§ Cä=u¤4ì<GFßÙWNuÿÖÚ#äÇÊÎ¾ÛÝõÃœ¥.îÞ7¼6WWu¯RáæªÏWMòGäG]í/mvÖmÁ•ÎlŽ‘é\r©Ü˜4¬ÜÙ#ÅÁ.ñcYòqbã1­•«-‹L\rƒq]+\\\nùÔ`q%{MXÎCSË¡Ï­><öóîÝµÜ4;Ïdw.êêš¾ë}¿×]iWÒÝ#Öû„mœ–ßÝ».ÜÜƒÙ[ƒ;ÙÌZ¾•+òY¦Çàquucé zŠª™ÓOy	…íl-ô‡4bMI¨@ýžU$ãM‚š†cÒÇ¨ðÕü¢ùcØôÍU£«¤èž°ƒ-Y›¥ÉGI“Ù{75¼3»ooã)i!l.ß¢§ìÜuc™ežZœ­ui,©q£WÂÒ\"{ÍZ”ò8$ŸZ¨ü€ëÀw¹éO‘¦x¾[í\nµ’ G]ñË²)¦ˆÏ\'ÚHø®Îê™i¤ZA ˆÔB¹™A©`²\0\Z¬f»mÂã¿ÌŸóu³ñ¯çÑö‡«uáõÿ\0cî‹ñ¯úc×¼º-ÿ\0ˆž™¦“)Fh+\'ìÞþ©Å£fZ:ŸŸQ‘Ú)ª]F6H¤aªà½ˆSu·H@5ý$ÿ\0UOÅöžŒ‡´[¢]òÛbÉ¿·ÇÁê¸2˜\Zm»ó+oï¹“D²gæØý\ròrRmì–’<kÐj{¨h#Æ;í³ý:_8@Lqÿ\0L£ü½QÅtŸK’ð7¡2²ÂÍÁ|—êŠÜ­YTû|l6ÎílmUd²K\ZAûpÐÒ£z‰ž¢5\0–öÜÀ-Êê¡*ä*OìjN)öôÃÙ[äþ×ìì¿j||ÝÛ;c7–#\r…Þ	ßÛÇxíM…„ªÛ8ÌÜMãÓÛßfmÀ¯ØŒ¥^F%Üxê­¿–£ÎÅO‘M¨…ä©ÜRÚK\nÚÜÂVPpÊ3ZŽ\"¢¸â8’ji§Ä0%”ã ã?È-…»û“1¹ö–G»{¿²y­ØxM½’Èäðeó—ã½%VÞî>µ¯š··¤“/ºö6*9¨2ïIOá¡Ú®0©SHj•ÞÚ¹´P{¤@\0%Aâqšž$±ˆaÕQ†¯—ùz2Ÿ(7­63iRu¼[’§íó49ýÝ6]vüuÓÛ~€e{›³+wbÛë·vk=c<~,æNƒ×\Zy&‰ÝHí>A8\nT’xcÏç^\"´ÏW‘¨)æz_tûëžÎé¦ì. ¦û>¨ÞlgúÖœREDØµøªY¶£GAÓý„á;	¡V	*¤Šè©oc–;™RcY\Zñã_ŸVRáÒs}<ù½‡¢ÌMG>\'n÷>úËa’vŠÎ‹Ú;(É4b&J—Çg÷í¢–Rº‹sk{~Ø¢Ù]3%|¿Þ§ì§Uo:jù+²ÓôTñTGOö_&:ryÔ\nv¨Š|µv9©¡ÔÏ#šÐL|Eknme|I†ž)þÏù:ÔœÛÐÑ¿6Èí-›¸úó²6–ßß[wâçÂî¡ºñ4YÍ½žÅTÛÍC”Åd!ž’ªJ¬º””uWRA!ž[yX\\«Nœ CÃ¢-ÿ\0\r—Ó©Røú^ðù¿ë&Æ¾	:?óKäUÓíù\"Zy6õ,Ñw•Òji\"Í$S¹†5H‚¢šêsƒzÿ\0‹Jú×…)ÇåóãÓ~õÇNû#ãgÇ\rû”ùÑÙ^‡ØnƒÙ•ÝCÕÔ½]¸6î+Ö´yí¹×ƒ±j«vžÇ¨ÅÛUôÔÃE$¹8[ïj²,ÚÒDÖö–þâ8­îDô¸~ì\rÓBÕ¯õ©ó<zÐ@Y½gÄÿ\0+ß‚¬ñ	>?ã7$\"¾šíþÀÞÝÙ{%*éj)êé4uÿ\0aomÏ±Ò–Ž¦’9!§ÿ\0·…ÖèŠI»x½¡£ó\0ûE	üÉ¯z·†¾/Yˆ¶oÇÎæÍÒIGŠ‹nuý«£wÅ•\rðý¥”û@0pÔãÆJž\Z…£Iaó\0#V]@„¶º¥¼‚¦¬ÎüÍ?ÕéÖÛ\nz_mÝ½ýÙÙx-§Œ•c;{jâöî:yÌ±œN\"e$²Æ¯‘SíÔ°¥­ktšC%Ã»ŒŸ/Êl\0\0 ÃâÄ4”¿>;ÑP³56?£ú§}^X¿†ì\\\rÓÔ,“TËU4”æ9Qäy#‘YX–Ýïú»C:Ûü=y~û:.?,ûQö§luþÞÛ_™ï\r£Õ=Ÿò7£±ÐæèeÝ·Œêœ–ÕÁü‚èLÏ€Eº«r›×®÷Ö>LDÔb¶—øÜTÕpiÇGRý¶\rvóVR bðhµ­\Z¼((I®h\nŒ·MÈ{–ƒ=öez»þWòVÿ\0@_ìÉßøtÖÿ\0F7·Þýâé«¶ýwö_ô\rZPÿ\0k£Ë+N?—¥|úsXôò¯_ÿ×Û×æ÷Iì}áÓÛ®¢›¬¶î[sö/hüTÛ½•ÅÃE´·NñÙT%ú~Ÿ%ŽÜûÿ\0ŽmÆvþm-DÌd•Öž–¡nDS·\\É­ÊB¢;(ã\'Ëó­<È.p=8ž«ÿ\0w|‘©Ü¸Í½`öNØÂm,÷Qm6ÌDÛ›%[vo/™Ý½û‡-Õ;­®Ùù}ÓKñC§²¸Ý«CWWZµYmË¤¤†&Èh‘ešÉ–¦§…4vêÆhY†¼R‰áÓdúü?çêàúfg¶WZbaÞQãbß»¦»1¿·ôŠjz|eñÞù÷WŽzx)n3hŠÈðôuR¢ÏSIA’v*¯¤F›L@øhŠü±ÃËæ=kÓÊ9é#½ä8”Ý”ñTJ»›­;ça>žXi¥5}U¾á¨«É\Zw«¤I#Ù’Æ‘,±Å3°,‘4»n¥¬n…@îýä1?ÌŠuVøÓ£-íWëÃê?ÆÞõ¬‘Šñ#¯¢“ðgr×oO‹}e¼òj=£6ð¨ì\rÛ))ç†Ÿ¹»?zfñ°UÒE\r-}>g_lu1¢¥JT	Gí~ê]«J\0~ÑŠuHþ·´=_¢áÙ5Ã\'òãfÏ	U\"b×·»ni(ªž1IQ¶6dgzxÑÒlE|Ç[ HP}å<,¤”*VBŠ,®ä¯q¢þUÔ)ö”§U,uªô§ùµ÷6ðé>ÉÃll^33ØPíªÅÖ˜üÍCRc_´6lÔûÃ¬êjjÇ¦”ã÷æP’8hã’%gV@Ê[±%n¢RHF:[æ§~co§[o„ýTîÒízŒ—QdJv&÷Åíc¶76kdÐewç¢¯Å¿ò7©ûib1pä2•jOî§Yü‚}«”…Ãÿ\0Çâ`Š¨\ZHa‰D>z¤iá_Uhq’j¤š\n…˜zŒô›Q¡ÇF\'ªþôGwmÎ»íjÊ^ÈÙÙò~ïÝ¿E²{Szí:Á\'Yüœì]á°qÝŒÂÖãq›ç·÷/ÜOFµ´íQM®¨Å¬íŽ¨©‚tw;„¶å¡Ò\n4Bœ**xxg\"„œ×…T5óèêößAuWrÉËoÍ··neÔ¦WhM¸NTâáÊÐTÇ–ÆRn,~+!AèÚ‘î\n\ZJùñ5â§5e5KBjii¥ˆº×p¹·V‰$¤,r(8úÖ•à¥qÓŒ€š‘ž©Û¡¾Tö¶Èë>²ÊRîÊŠÎ¼ ë•ìéE½¶vC¹ñ}SÓè>Hw.ù Lö_õýÛÿ\0 6~.\n=]=!#µ9cQ4\'76°Ë,‘\ZêhÕ®K•-*OÚ8Ó4(ðõf²´•¿$²yÚ-Ìw†ËÙ}s³sØ\nÊ‰vnáÚ»\Z:êÝË¿ö•‘ÁOKŽì>ËÎå¤§©Š;d°XÜLÞI\"ˆÊïÊEv±©R	b^\0Ÿ:÷“Qž®™%ÈéQò²±pÝ³wTòÁÑïŸ¹¼“ÔT›¸6~¥bi…–¡ÿ\0€–!‰à}}Óm$K(:þÅ<:¶ÿ\0‡íèÌû.nÆe<AéÎ»÷~µÐ=ÖPà£Ý½û.|DµÕ=½“s&.yæª¦ÏEÒ½;M9øæ¤¦JlÓmøh$o:\Z¦o fhãvm^¥A¦ƒO÷¶áù“éÖ‡ûÉÐÃí®·ÑHù­„¢Þ} iYO%Jö¯jô†ÀEU=$‹O¶övK/V³RWã+?Ü~WQ¦ã‘„Vñí~ØYe–U\nÛBGóüú¤œ|ú6÷æÿ\0ï¾¾ËÌ‡Ä/ó¯óêþTêŸß½·‡ÅëØÝA¹6Žã¦ëÈûk¶0;?ÁÇ¾«z†¿xm>û}ÝO›*ûÊ«›?Ù;N†¾T8ÅÌlXL®²UÅˆZ¯ã†jU­8\ZBiBucçF4\0ôÀb„¯K­&Õù7òo°ðû˜ÓRî=ÓRmìf÷ÙôU[g°öøê”?$zª«vuþì5Yj¨ù8±FW¤2TE]K9§¬JšGžõ)6•¯käWPWôò®hr3ÖÅÍz6?ì­lK[ûÏÙ–ÿ\0eÃý–}*ãÈÕ¯ûÕÿ\0\0ÿ\0ægy9þ5úÿ\0æß²Ÿ­o÷Úÿ\0¹\'çéÃ‡òùtæŸá§_ÿÐÞS¹:›cw¿Uïþ›ì¼3tlNÈÚÙm§¹p™Š²5tJf„I%,¥Uª(j4T@êÈñO:2º«nÖá­¦IG¦ú‡JŒô`ÃP#¢añƒà3ªk£ß}÷¹¶7}v­6Onæ0scz[dl°Ø9Ý™´öÇ^ímí×ûJ¢-á½(û*=‹²ñ´sg2›—+QF¢¢Bâèªg¥ÂëtÔ¦;U(…hI%˜Ô–\"¤Tù*ŸZñêŠ”Ë|]Xß²¾œè·|€ÌUm×ÇÝógøv7¾6¦ÉÝ	³Ë¸q™¾®Ä,TðÅ#H*{7sí¸årQiéÌ“1ÓuˆGu=å*?.æÿ\0ŒŽ¨ø*Þ]hz¿MÙl¶7ŠÉçs´øÜ>[–ËdjÜEKAŒÇSKY_[S!â:zJh^GoÂ©>í\n³Í ÔÕëÝ…YJ<ÇÄ_•´9WÍ¢t·^cêòRá+öÕLùŒ>Û Äg£®Û™JZ,–ßÈÒg(j!©¢¨†)é*#xC)Ôn\në{riW$yà“LõHþÑ ö—«ô[öÓRn¯”››\\lª½MÖ[¬èóW¥Vw°rÝ‘½pÏ©yãÅmünÐ¨†d+ºùã:Ù?mtšá°IÖŸÑ*n¯ÙÕ“éÑö‡«õR½ûü¾÷ÐÞ›“süNÈu6ÅÛÃ]ÚU}É×[–Úÿ\0Ã»½1»{ooï“½m¼ð8­Þ)»KA´ñ™·êðËˆÍæ¨\"®’²Ž Ô5QÌ¤M\Z ÆDÓ¦œ\n¯ ‘çZ‘œð#¢S¤àõf=m×ø.¬ØÛo¯öÔùÊÌ>Ù 4päw>s%¹·.^¦z‰ë²Y½Å¸²óÔä³yìÞRªjºÊ™œ´Õ;X\0)¸›Ç•¤ü>B€Pytàésí°<éž·Ñùrô4ý™”Þ™¹w6ga¾ÞÜgltVQ`:kka÷Þø¤ì®ÍÄ6ÙÛTX‰7VÝßÛë‹ÈUbòòÖcáþ<q}£4ÌÛs˜Æ¢øµµ*j¢ŠjI¥#9ã\\ôß†+ÇH)éèéà¥¥‚\ZZZx£§¦¥¦‰ §§‚	0C¬qG\Z(\nª\0P,²¶bÄ³¶ONtüˆ¦ÏÔtgkM´éòU»³²3»Ÿjã°ñÒË“Êî¥HÛ£oa¨â¬Šji$Ìæ0ðRéuåf6 Ø…V¢æ!%4±¡¯\'?Ë­7Âz0Yª\rÉƒÃn,TÆ£žÄã³xÊƒÐñùJ8k¨æ0TGD&ZiÕ´:«­ìÀi¤CŽ@Àõ¾ýï¯tY¾9MK\'òK<Š:œ×Éñ\rdt2G*,›3hu÷YÒ=D¨ˆæº|VÈ§’e{´R9Œ¨§ÚýÁki­~2Oùz¢gQùôf} êýnÊ«ÇîÏ]	ÖbI§ŸiC½>@n\Z#Ž·¸Ý·Šn·ÙÐe*¥+ü3)[»ûä±L¡šFÛÕ\\\r\Z½¯·\r•Äÿ\0ÅE¡¯_`ÒGçO<ÔšºŽŒÏ´[¢¯òá·Cü—Èc7>ÿ\0Û•¸¾ÉÀl½ù×{c·v>JM«Ù»{cön¯oo}£M¸ébš<ÆÒÏÐÖ\'Ãåiò–«Ž*“LgŠ9]¦ãqf4GC hxT«íô\"¦µd\rÇRz/âÎÍéûÝ}·kpï.ÕïÍÍk}nÌíuLXú\\6l÷OfìÝcí}“€Ã¦R¦ª±q´ôòfsuµ™*ÂóÔÚ?^_=ÒC\"AZšV§Ï#‚€PuäM5ÎOFÚN­×ÿÑßƒÜ/Ñ^÷î½×½û¯t÷~ÆÈöWQöÈÂÕ\n\rÁŸÚù(6ÍsUÍAé¥‹ø†×«ž²ž)ª©)©óô”Í,‘)•#RÍcíû9V+„wNý_>a=UÅTÔÞ ìJ>Ùêý‰ØôTïCýîÛxì¦C1O¼Ûùã§Ü›_\'\Z<‚›3µ·XêØK‚®–HÛÔ§Þ¯\"h®%Võ$#ž#ä|ºÚš¨=:ö.Û©Þ¾¶}D•›¯gnm·IUT©iês˜ZÜdÕ,i$†ž)jƒHYŠ`Oëján!và‡ýUùuæt\0ü“=]ñ#¢·&é¡Ç¿¶\\}¥šÁ-m~F-·‘í|–C²\'ÚôÕÙ:j\ZÚº]°Û§ø|RIMJdJ`ÞoâU“+]Ê¨p½¿nžÚþtê±ü#£K__C‹¡®Éäë)±ØÌu%M~G![<TÔt44pµE]e]LÌ°ÓÒÒÁ<Žä*\"’HÚ8Ñ¤p‹ÄŸõ~Î®M3Ñvø³ŽÈÔõÞW³sQUÓå{ß{îNç\\}}ÍnkîÇ¥¦ëŒ%V¹j$†·Ö˜œ@«ƒË$Tõí<pŸ\nÆíÆE×¸ôÅ)Ÿ:Fµ+ò?ŸTŒb¾g£-íWëÞý×º÷¿uî½ï@P×º÷½õî½ïÝ{¬rÅñIÈ$†hÞ)Ü‰#‘JH†Ö6e${ºWVuªƒŸŸ^è¹üS‡3ˆéÜ~ÄÜY*Ü®wª7Fûêšºü¥p­ËÖâö6ðÌâ6fO&Z8ç¦›=°‘†9CIö•¿’Uu•Õî+ªãÄEe£$TÓËÓQ>ÑQþÂÿ\0ë{B€	WÃQ_³«žªÝþ_8™ñ™–MG­Åc$ùœ¤Þ¿ÄkêkçÜ!1ëSso*Ykp˜‰$Åç±²m¨ –œ¶>V£“ÁL²´Ç{¹a°”ÖZcä¹ÁÏ\ZùùúštÔ|Zœ:²/d;ÑeêšJÍÓÞ? ûN­™ñ4ÛK£6(’Ž¦˜œG[ãê·øÊA%LTïQweoœ†-ˆWŒ¶\0<nÉ\'µ÷L\"³¶¶§êeŽkJž¿3ëÕ,Æ½½¯h:¿^÷î½×½û¯uï~ëÝÿÒßƒÜ/Ñ^÷î½×½û¯u×¶×PøºßE‡ã~3´ó?$:ïf8]›ò#rdñÉ=_ÜÉM/ol­…ß9ú4NŠŽ—uv•zÓ@\0ÑM ó{“+æ2F*hÆÈ•ýåGç^¨¸Õöô;á÷ŽÒÜËŸM±ºöÞá—läªð[pyÜVXíìí~J¼>tPÕTÿ\0ÊÒ+†–ž G,`‚Ê´ž±”Õ…GÌuºƒÀô\\þ\Zo<oÂï‹û²¿9I2£¡z•*3YŠZý¥K-d{7Ž¨‘¨÷M6\'!CNF‰ãC*•dÔ¬¤¨½‰ÍýÒ…ÏˆÞcÔüúÒ‘¡såÒ³äí4û—©+zï”¥ÆÖ÷.{ju<u²TÐÇ\'ð\rï›££ßsbÒ»\'ŠJì¥Z&f®–ä’Id€¢èÛ±VŽs1BD`Ÿ>4ªŽ	 ùƒé×˜)ëÐýIGI¤¥  ¥§¡ ¢¦‚ŽŠŠŽé©hé)bX)©i©¡TŠžššTDP\0\0ÚG¬ŽÎ~\"Ô?.­Ã©V÷Mé×º÷½Ðzuî½ï}{¯{÷^ëÞý×º÷¿uî½ïÝ{¢Ó†¦}‰ò“saè•Î¾úÞ£³§‚8œAßý=]±zçrä§‘^*dŸwlÍë¶¢ŒxäžOà\"ÄžÌÖkþ8ØÉµOØÄüÏTàÿ\0#Ñ–ö_Õú+Ÿ¢¦Nì¬¥ªû¨²?)~CÊÀÁ=;SÔãû%­¥ežIž*Ü<šdM1º@›šî†²Æ´à€ ËÓqð?oC—an‰vFÀßÒžrÓíŸ¹·<Ç¬‡¹)°ZÜ¬X÷ÈT«ÓÐŠÙ)FiHµj`@>ÐA\ZÉ4Q–¥Xf•þ]\\šzcéÝ›ýÃë=Ÿ·&ji²°âS)¹«¨Ñã§ËïÃ,»ƒyfãŽG‘ã\\ÖèÉÕÕ,t	BŽ{ÝÛøÓ3-tŒ\núòû:ò „Ï§´ŸÎ½[]ûs­uï~ëÝ{ßº÷_ÿÓßƒÜ/Ñ^÷î½×½û¯uï~ëÝ>»ÅE›ïo˜G>J[/Pll¬ôóŠG‡xÑuBg3ø¤HQ–©6fûÛË÷„ÈÏ$\"+LLå”Gk·ƒBâ­óÓ‘ÇÖµû: i=:+ýñ‘~mçîžÅÝ]1E‰ÙK²#›¨:‘zºöWTÒîËMÙýç÷;‡wÔvuæêr3G-Z4ttþZ“M>FeU—ÿ\0½ÚÞ8¨Á_!ÄÓ<XÒ¼U¢¬OXp½%¹~Aü\røH´ý}Ó»ûwl·éìþw§>Tm¬´½o—Ê¯LÔuÎöÚÛç>ÍÏg6žöÙ5{‚±á3`f¨£Éãä¢žš4’ÓÖ;”³ÜïL¤ˆÝÚ…}5TCiŒùÖ¸§Z¦¤R½	mÔùN˜ëßå÷Óe¶²”ýyÜûW”©Ùô5_fmê]¿ÒýÉ&>ŸhíÝá»·Nâ¡Û8ÚÁOÆPÿ\0ÊWAH±†vHäu¢Ü‰Úò@\Z…|òpâ„Ð\"|¿È:±Z^¬Ù/Nuï~ëÝ{ßº÷^÷î½×½û¯uï~ëÝ{ßº÷^÷î½ÑiìÜf/äwÅÿ\0²Ê=:î(;Ëjå±¬h²˜fÙXß÷e`¦–•\'ÅçvmI\'’%	Rê¥Â“;TW´¼.µ ­}\nƒOÛSû:m:2ÞËB’@ONtMþo]¥¾v/tWmö?O…ùòïlf&ÀÖRÖCC¸0½ù½á¯ÇWµ%MJÅ“†\'ŠIŠ³$©&®¾Í7eu”€È¤|ûW#¦ãà~Þ„?””9lçKn¡ƒ˜Ód»9°úî:¦¥ª«¦¦¤ß[ómmŒ¬Ùi ¨œãF%P*\"\"u[°go*·\ZˆÈ´w?e?>¶ÿ\0	èÁª$j©\ZªFŠ@UDQdUÀ*`=¢r…Ø¯Ã\\}ž]_®^ë×º÷ºTqëÝ{Ýú÷^÷î½×ÿÔßƒÜ/Ñ^÷î½×½û¯uïtÓR7^è±|P§‹)ÖùžÔAU«¿;ywE,Õ“PUÏSµ·MlXî¶¨Ž¿,ÐÖãfêüèKi–*&Š)2 Çq`fH…*‹Bpk•üš§óê‘Ö„Ÿ>‘ß:12fú—`b²1á*:þ»åÄúnØ¡Íø¼y…7ÈN¼ˆPBÕU4Øÿ\0·¨Þo‡þ&•%âŸ+¢ï\"nm\ZK)bÞ/†úiþ‘«_ò|úôœxW¡ã~^®ï¤ÈPÉAÃüí¬~ØV®ÆÕÁS†“#ÊÖVRAŒ§1ÔnŒ¦H¼jLþIe}Ré]îJŠÐéþŸ–*Rj>íëÉD¦¢‡¢·ux‹ø~Ëù\'Ôõr»0’	wíN_¦0ÒS(š’I7gÑFá‹…Ý‚³ªƒKEIæc\'ò^óÿ\0ëoø~ÞŒ¯²þ¯×½û¯uï~ëÝ{ßº÷^÷î½×v÷m\ré×ºõ½ûCzuî½o~ÐÞ{ šEÜ¿&rR*j¦ê^ŸƒDU®øçqîŠ|ÆbŠ¿\ZÃÆóã°M‰–’¡lR<LdvÚ/Èq«µOûQŠ}¡ìê€êo³¥gyn,ÆÏéNáÝÛ{_ñý­Õ…¸ð~:sY\'ñŒ&ÑËä±ž:EhÚ©þö•-e.}7öÕœk%Ìüg­±¢“ÑhøË·±¹û[®úÛ… ê<wJüfÜ©-!©jîÅËb7ÞÚÈfrsŠÙ1ùÚ¼ï^ìÍ½%NOíÒ²®¢ò‰§ÓvàÒËn’N”“U0\0Åò­XŸžxñ¤`!xt1ü®ÄTe~<vœ´U‰Ž®Û;y;†½’¡Þ’¯­²t´ÿ\0iþTµe¶Ö˜š?ZÈÀhöâÒ©Ôý¼•z³ü\'¡ß“£Íâñ¹œt«>?-AG“¡2MG_OU4¨ÊJ²ÉªÀ‚AÚYª%’«F©Ç§Ë«§ûo­õï~ëÝ{ÝYu}½{¯{·^ëÿÕßƒÜ/Ñ^÷î½×½û¯u\Z¦UO=+I4\"¦	iÚZy^\nˆ„Ñ˜ÌOY!™]]H*ÀÏ½GRëEîÕ×º.ÿ\0kà­ø·Ð”ÑCOKQ¶:Ãjõþf†’	é¨ñ»£­1ÑõæîÅÑCTïP”x­Ñ¶+)¢ÖÌZ8¹½Êëô)w;‡bÃæ	4=U\rTuÏåþÚ¦Ý¿ûûU*Ãêå—<QÌ«>ÚÃÔîJK¤¤*Þ«ƒ_Ö;êïÛ}~®0ògü”ëOðž±|C‡)7Çn´Ý™üíÏÚØÊþòÝ{jy\"©«Û;›¼óY.ÛÌí:Ü„IÊÔìú½äq?tULéB¯¥A\n-¹=nš1•Œø´à7æ)×£økë×,é3Ù‘ÉâöµµÛ—-¿úC’*x*¤Âerã×T[ÀCQ\rBØ³TÓ2¯’4 /#ªºûo(·¥ø5~}§Ÿ¼õ¦=z2gê[{Däb¢ŠI§Wë¯uëÝ{ßº÷^÷î½×~î	cCÃ­uß·z÷^÷î½×½û¯t]ºÓì¿ÓçÉ\ZD2_qÓ†®D¬Y¥z°çá5 ™ÞŒ$â«MÑ€ê\Z¹!|ú¾†Þ¼*?ËOåÓcãn‡úÚHëèêèe,\"­¦ž’R¶Ô#©‰â® Ë¨+›\\ì¾0^DPrH·§:¯åu&ñÜ°Øs½VðÞÙÜÎæha†îHÊŸ6UQ×dš’víž«‹sÏNge‹+¸+J¬aôMá¿ÆUŸ•ûk“éÀ’£ä:n1‚|ú3¿)ª*©~3ü„|}}>/)7Jö}\Zºª« §Îd¶fgƒg£M/Z_/S\n¬*uLÌrÃÚM½iyÓQ«ýòõgøOB–ÊÃ¹³vŽÞÒ«ül`0Ú#§‚ø^*’‡JÒRÅ-*¯ÛØG\Z$h=* \0=¤™üI]Ïz°\0t§ö×[ëÞý×º÷¿uî½ïÝ{¯ÿÖßƒÜ/Ñ^÷î½×½û¯uï~ëÝ?Ž4çhïo“ýWãhhv¿xUoí®“U™ªj6·ym|fÖ×%6³?håwN6’4TC4µ‹–$Òú²Ek>‘¥O\"1OÉ@éµÃ0éUò¢¯7ÇîËÃm‰h©wFýÄQu&×Èdâ¨¨Æb77sfñ]Q·s™:JFJÊì~3¼à¬žžYªb¢FWpCEÒ3\0ÄýI?+O+Ž¶ÿ\0	ê?Ä¬öpüoê·YÇIºp[R‰¹«°ÔŸÃð¹ÅÖµÕ½wÜXLQydÃá7.WkË£¢‘ÞZ*j”‚Fg˜ïqˆ-Ô„Ö£>GÔúŸ^¼†ª:Jwt-¿{çãVÐÖÆiî½Åò;|ÑÄÌ›[¯6ÎWeíjØ«ÒÀ™ŽÌìL|ÔÑÊµ+ˆª’ŸšIš7ìÿ\0FÎêb(\r\0ÇåO—Ä­öŠõ¦Ë(=_e]9×½û¯uï~ëÝ{ßº÷^÷î½×/j:×^÷î½×½û¯t]¢£nü¬ldqQÿ\0\rí~ˆ­Ü\Zzxb­ƒ9Ò{ë„¬©ÉT´þzèó~í Š•R=4Ã6¶¼Ñj‰I,²N¤zqÁÔ\r)öþ}Wƒý½\r[³uíÍ‰µ÷õÞœ~ÞÚ›O“Ü{“;•©Š‡Âa¨æÈdòUÕSºEOKGG»³\0ÓÀ$ÑFªX–O[&€žŠÇÁœÞV££åÙ›¢‡‚Þ]Ø]‡³·.ÓÀµkµ(2[ž·°z×“Åã©é`Áeëºs|mÜ…E‚`zËk”~ã+ÜTx©\"±1²ŠZ\n7ükWZNóéaò¢’£sl-¯ÕÔÐÁ‘îÕë­ñ\n1]`±ÙØû°\Z(¬}OÖ{5-+JB\n¤ŒrÅT×o!$’F:bT5üð?ãZzÓäSÏ£.yçýhd>#´”âIêã®½µÖú÷¿uî½ïÝ{¯[Ýt/§^ëÿ×ßƒÜ/Ñ^÷î½×½û¯uï~ëÝLm!Û_/·Tæ\n‘ntÐ¨Ž²z˜~Ñò]\'¾·UU2”i›îÜ°MVçRéðnnaPÛjyY+O™À±IêŸèŸ—O!2X•¡ê\r«•X™·×ÈN ÆâÖx¡–#–Ù„÷=1d]D©WHÐ²i8FR=a&i¼‘iþö\nðõç<¯X~+ÓÒQô¾6‚…™©qÛóºñ±3\"ÆOðÞîìJô\'¥TIN@ño~ÜX¼Ñ±âc_ðu¨þ™ú¶™·Èÿ\0’ý€íOQKƒ©ú+4RQ¼Ðÿ\0s6æK³·\r4‚–i¤1¯r q¢Hä@UYïpU,m„Iÿ\0!þjûOŸ[>²Î¯×½û¯uï~ëÝ{ßº÷^÷î½×~ì­§ìëÝwíîµ×½û¯tP~Aæp½yÜ\Z{wrfWlm}½?ul½á¹*ä¦ƒI¶7?YTïG£ÌO,‹R´óçú³S…dc% ,ºAe2±A-½ÔAjÄ>Þÿ\0=6øe=wó¶\\eoÃ¾ö¡¬ÄmÏŒÝûl±Aºi1™}³P;+‹Ù”y*ª£ŒVOøTÛ…+i¢•Ò9ª u­ÃöÕ\"íX³.Š61Z0Ç[“áêÅM£.Þì›9|»aò;Ÿs|¯¬¨­ÜX6+3Yµèú3¤?¸öOCK]¸[hmšÈèég®’yãƒöÕôQmÃJÇi\Z !õâ…iÀf§ó=i2Xùôò7¬‡òß´6ÆrivÏEu–æÍöE%!ž8$ßý“›ÇmÍƒ‡ÉE<IßÃööÒÜUI$wÒîª†‘}ûÃHlY¤ÄfÇ®0eT˜?—ƒø8èÙ{/éÎ¸û`‚0zß^÷®½×½û¯uï~ëÝÿÐßƒÜ/Ñ^÷î½×½û¯uï~ëÝ¿\rEµ»ã\'iÏ÷‘Ííçë„Ô,ÀÔmÎôÛ¹-š¸ÚàÕ0S\nÿ\0H´»c!3H²ØÔÐºÈöaaªX®­ª4¨¯“ä ×ª>¥Ÿq¥#gzêé£¨1w–1éP§ìë]vdqU!b4É¢FŒr|–_z±Ôc»Éà¿ñî¶ßþ}%>*gk¯÷ÍlqTA·hû÷ä>;kÕÕºøkð¾àÝô³ä¨™ÏÜ®9³±W$fY’¿´c>Ý½‰¶ª[Š%~Í+Ÿðþ`õ¥üoL¿	)qµŽß˜ÖIGtïîÝïjº”ŽZ‰û´wnõ¤Xª\"Põøü~+KIC<¬ò½<\0›*]Ì¸¹Òìu*ö4ŸÚEOÛ×£øz6ÞËz¿^÷î½×½û¯uï~ëÝ{ßº÷^÷î½×½ìR¹á×ºåíþµÑQù•€>›‚ig8Ž¼Þ›G|oe5µ•ƒ­ikßovÎÜ¤†	©çþ#˜êÝÉ™Š£f+ýj·n,²!q*]?ñ¢:¤‚¢¾ÎÜìmÏOñçãwNf6ínâ·å—Y|<í­Ñ)VÚcvU;°dÆÒKGEØ1uö>x\0‘ã¢þðSFÆG¿³8-	º¸“Ä£êÌÓ¶¾z|þÎ›\'°ŸB/\\÷>gk|°þc;o!‹™ºÏ§¶çH÷Ume6Q]˜Ýûë§\r.cCaäf Úý=Aha†hÒJÔýó!xafHkmº_ÆÌÕôËT|µTŒúÃ=YN–qÒ§à†ÙÍ&Ýí¾ÈÜÓÔ×g7×gVíåÉ×š…ÈÕÑõM;#qŠúZ¹ê§ÄÖ7rA»ê\'£GûhjjehE ºmÁÒF¼\0­<…rûTþ×‰ëqŽ\'£çì»§:ëñí£ð·¯yõ×ºu¾½ïÝ{¯{÷^ëÿÑßƒÜ/Ñ^÷î½×½û¯uï~ëÝš97Ú}·hê)©q{Ëå§ÆÍ¿ºe–	\'¬8;#¹…>\'Æ’šzúÌÞÝ¢…¦(V*i&bRÚÔËhDi&-_€÷ Ê‘4ùÓ¦åà>Þ¸üñßî·øýùÉæ(vüo½¾*ä¿äL1Ñc¢ä÷Qzª™Þ(©)*p­UÎ]/®—:ôµvÑ#ÝQ$«à|Ñ¿ËÖä )écÒ1Plˆ=^ÛáÃ¡Û?ö¾O}¾qÀ}¬Ô»“)¼«sóKWŽŸî§®™¬òJ²;LO½\\–þmÌdm?1SJ|©Ã­ŠiáNŸ>+UÑ×üeø÷[JÄTô·YK„|d³ÍªÀ6ÎÃÿ\0ÈQš¨ ªŽŸ%‡ðT,r¨–!.‡%”“­À¼œ5k¨ÿ\0±ûEçÖ“ážÐõ~½ïÝ{¯{÷^ëÞý×º÷¿uî½ïÝ{¯{÷^ëÞý×º/.38=¿ñoäFcrcëóŠ—ì‰gÂâpÕ›‹5œ©m©”‹ƒÀmìtsd³û‹5“’\Z\\}\r2=Mel±C™A_·w^B gWüU}iRp8œuGøOD°>#nýçÓ{+¿ûkäÊ<Wsa÷§H÷Ô[Úµ[{«v6îÛÛ·bäð}{?SÁM6ÍÍãpú$Æe¾:©²y«ÈkŠ¢tðœÃyI%´.§¼P“_Å]5>DPã P¶ÊiRsÐ¸>2uËìßhvžôÍ÷\'Zv~µ÷ŸOVg¾?|ïNyö¿Fv^è ë¸÷FÙÙ›û³³¹:Ü`©«9]@©¥É4V4Î‹í¹¦6S­¸‰Zq©PÒ _.G ç¯Ô	®zSÿ\0,J¬Ðø¶ð¿så7bmNËïŒ_eçó{wum<¦Wzf»¯~oº¬ÅNÜÞ{chçð±n<Fð¢ÊSÒKFV––º8Vz‘žD{²¨VE¤e€áPMiÂ¿.¯öõ`žËú¿]l¿Äzß]{¯^ëÞý×º÷¿uî¿ÿÒßƒÜ/Ñ^÷î½×½û¯uï~ëÝ¼ö*£±þ`m\n,‹\nÍŸñ×«¦ìZLj¼jÅÛµûƒbíœöB—É6Clu¦ÝÜtøò©ã‰7K92x\n!m¯\"\ZI#i­3ACJúµzlæ@<‡I¯›Onö§Uìž†ÞqM>ÅïîâÙm¾© ¦Îë¶•->s±3™­½”Ädv|ðì0Ðå<â:FK²Kq›ÛÅã\\¢þ¤kŽ©¯\Z×ù½&t¯‘èwèÅ’ì>‚éÝÕ»\nì®öêMƒÜÂÍO[Ü[C]–Ö¥¥Ia©¨«’à–ê}§ºý+ÙÂŒ,„~ÃLÿ\0—«/Â>Î¿iÆÊÚ»›¡$z_7D›m­vZ¶F¯ªªñô›“¨ëæ‘DU)\rËÊÃ…&h¢-Y…©Ð\Z%GgïÁÇv>	qLæµòÉ©*uTÅWÓ£9ì·§:÷¿uî½ïÝ{¯{÷^ëÞý×º÷¿uî½ïÝ{¯{÷^è¡ü…›-Ø}•Ðý¶éh+é¦ßû_½ûŠª®iÌ[c«úgqRnÍ $‚Žª–FÌo®çÃai1±LÆ\ZŠLnVCÉG4~Íl•\"·¸¹–¢ªU~u\Z~UÏ‘Ó^5\r½IP§ /å&Ûù\r»÷¦\'wnšÃ|rêîøÅ”Ù{®7®GûõÛ_ôß´#ìËîØ©±ØìRu?_íº øŸ^Gø¬«S’Êä¥ÇSDý›ZÄšc!§dz’0“@>lh§Ä“§kü=3t~Àî|olÔ|³êºJLÆÕùAÜ[ê›äwZg7æ{ŒŸ®öîXõ·Ç¿Ýk‚ËSä6Þ³vŸXìLF?sâhF2›uaªESÍQ”ÆÒ\n·n^ÔÂö’ÔZ¡Á<*TœÜ[O\0EP5u¶ÅËVí/ýµÖY(h×¿ðØŽöØ”²Êõ5+>®ûG\r•§xÛÏÎÐak ŸË¢ªÿ\0‰#V£•ãÇ´ŠáI%pÕùš×çRHõsÕ×Ã£\'ì³§:÷¿uî½ïÝ{¯{÷^ëÞý×ºÿÓßƒÜ/Ñ^÷î½×½û¯uÐöÚV™øzßUññ÷¼¶¦uü§íƒhojÞíîÝn~|•&nuì.­Îlž¢Û»™?»ø÷¦Ùxz4©ÅíœV!3f2Y,umBÇ#Ë\'³ÛÛ9¤X„=ÊŠ½§Îµò©4<(@\'¦Q‚Ö¼OB\'É½å8n Ý›{3M‘Ú»K½sõ}6ì33wºë©»Êmí‚§A#1Íã3ÛtRMM‘T­LNä*¬¢tŽñ–‰JÔq?ìƒþ¶Ç(AÇJ®ŽÝ[O¬¾6ô.?}n¯µê1=/Ó5±×ç±ÑC¹LNÒÙ¸³²VÌf£Èn¬­.>žpòE5UDq¬ŒÎ·nâ	g»ºdBT»Ê¤Ôúcx\Zu°BªÔôõ÷uì]Ñó›tl…†Ý_Ä7/ÆªñØÙŒ®9ƒÂ²lžÊ“dõ½}\r=}ÿ\0Èd2ûÃ\r‘ZÆ§ÉÅ6ÙŠ4¦jtiý¨š†Ú¦g¯@<øf¿`Ò1ŸZõ W_o§GÛÙ?Nuï~ëÝ{ßº÷^÷î½×½û¯uï~ëÝ{ßº÷^÷î½ÕPæ>lìþ’þb}µÕ?!\'ÙÝ_±ww[ôŽ¥»;uîM³‰¬Êdé(·ÆãÜôÙºIiirØ^®Èfwðü^á®¬|BnŠ*ÌdÆŽª³\Z¹Ð5ÎÝnÐd$fžBž€ÔöNgV—5áÐÇòƒäOÇ}ãÕ›“«6ßxtÆíìç›ë­§·öµ6~Sxe2û—°¶­8PmÌ&O+¸*d¦YañÒH\nw‘ôÄŽêÕ•Ü4²BB>‡çÀ\Zù“Ï­»)Zž‹¯Kÿ\04?…!Ó½ÔýóÝuWbuÈ}½1›³­û›‰ýE’¥êÜ¾K¸3=yG‡ÜXœžjŠ˜ÐÏCQQh®€@dò¥Ü¸Ún®gi PÉ!Ô2	î%€ A§­)šÒidP(xŽ•3òtü¨þ`\r–ë}³¹v_I|xèmý²û2“¸º¸:«µ³û÷¶÷/Uî®±ÈíÚþÆÚX\'ú<Ìm¥Y_4sÉ—wRÙ*Z4û¸ôö«e·”ÖWe\"Œ Vô\'Í‡•<zðmoŽ«]öAÓÝ{ßº÷^÷î½×½û¯uëû®µõëÝÿÔßƒÜ/Ñ^÷î½×½û¯uï~ëÝPçÍ?öW¿ÒNóþãÿ\0³¿ýïÿ\0fwâ—úzÿ\0d{ý}‡úqþ%šÿ\0Aÿ\0ßôßþGö¿ÞñßîO«øçØÿ\0rz½‰ì¿xx+ã}6/Oº©C«†iJ×V=qN™mÅkòéuñ3ý–Ïö`ðÞ¯ö}ÒñÎÖÿ\0eçý¯ã?èçûÍáÝ_éçýÿ\0ÜÏô‹¯øïñïWûúþ×ï¾ÃýÆy=êüßøêX)Q¯I:«QMU\0úG…{éÖ“N¡Jüº,ý¡K‘—=µäéÜçó( Øëñ[n¸¤ëÍ«ü´rÒKñÅ;l¶Û]™‘ïÝã…íè7\rF_ø\Zá[1K&ä„ý³ZZÏ*{[“ÁŸÆ[}Y×S\'‹H§ÇñiÅ+øz¡ÓQJÓòèñÿ\0,¯ôAüWä—÷;ýô‘ýþïïû;À½ßÃÿ\0¾]—oôqýÌÿ\0~w÷ý*ÿ\0|¾ãGû–þ3÷_{û?aì³{úªÇõ\r*~\nÖ´Õ«»U4Ö¿/:ôäzs¦¿ŸV½ì?ÓÝ{ßº÷^÷î½×½û¯uï~ëÝ{ßº÷^÷î½×½û¯uS_Ín>—¬±‘v%WvPï	6gfG-oBPuW1GñùÆÒ^õªîì_|d°ÝQ‘ø³iÛo¼)ó”¾iRMQGû/ÔTøz<*ŠêÕÆ½´ÓÝªµÑåª½5%?Ûu^01ý÷ñv§Û·|cæ—h¦ÄÂ|Sø÷ñ®wDæýCÜ¢j­éº:«äçid¡èê]¬¹#,;É>EñÃ\'4*•EÏ.ŽáàÝ\r«Œ³yê_„i\n¶Ž:xŠ0º*µéßäGû-?é{t}çü=/ú=ûíÑýÐÿ\0eüä?Ùzûô‰ƒÿ\0KèÏìÇúTþâÿ\0¤ÿ\0²óÿ\0q>÷gü‹Çí<&ûÁRÚ×N5ªÔâ4Óâ¥<ë«º½\\è©ø«ÕÐ|#ÿ\0eGýÃþÉÎŸôWýèÌû¿ô‡ýêþý}¦/ø÷÷ïý-ÆMþø}‡Ùyÿ\0ÿ\0–}¿‚ßµãöÜþ³Ä_«ãAJpáååöÓ¶µÓÓÉ¦½ße_¯{÷^ëÞý×º÷¿uî½ïÝ{¯ÿ','\0','2014-05-01 15:55:59',NULL,'system',NULL,1);
/*!40000 ALTER TABLE `diagram` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `followuptreatment`
--

DROP TABLE IF EXISTS `followuptreatment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `followuptreatment` (
  `FTR_FTRId` int(11) NOT NULL,
  `FTR_ITRId` int(11) NOT NULL,
  `FTR_USRId` int(11) NOT NULL,
  `FTR_TreatmentTime` datetime NOT NULL,
  `FTR_TreatmentDurationMinutes` int(11) NOT NULL,
  `FTR_Note` text,
  `FTR_OpenToTherapist` bit(1) NOT NULL,
  `FTR_IsComplete` bit(1) NOT NULL DEFAULT b'0',
  `FTR_IsTestData` bit(1) NOT NULL,
  `FTR_CreatedTime` datetime NOT NULL,
  `FTR_UpdatedTime` datetime DEFAULT NULL,
  `FTR_CreatedBy` varchar(50) NOT NULL,
  `FTR_UpdatedBy` varchar(50) DEFAULT NULL,
  `FTR_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`FTR_FTRId`),
  KEY `fk_followuptreatment_initialtreatment` (`FTR_ITRId`),
  CONSTRAINT `fk_followuptreatment_initialtreatment` FOREIGN KEY (`FTR_ITRId`) REFERENCES `initialtreatment` (`ITR_ITRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `followuptreatment`
--

LOCK TABLES `followuptreatment` WRITE;
/*!40000 ALTER TABLE `followuptreatment` DISABLE KEYS */;
/*!40000 ALTER TABLE `followuptreatment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `hibernate_unique_key`
--

DROP TABLE IF EXISTS `hibernate_unique_key`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `hibernate_unique_key` (
  `next_hi` int(11) NOT NULL,
  PRIMARY KEY (`next_hi`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hibernate_unique_key`
--

LOCK TABLES `hibernate_unique_key` WRITE;
/*!40000 ALTER TABLE `hibernate_unique_key` DISABLE KEYS */;
INSERT INTO `hibernate_unique_key` VALUES (100);
/*!40000 ALTER TABLE `hibernate_unique_key` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `historytracking`
--

DROP TABLE IF EXISTS `historytracking`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `historytracking` (
  `HST_HSTID` int(11) NOT NULL,
  `HST_ObjectName` varchar(50) NOT NULL,
  `HST_ObjectId` varchar(50) NOT NULL,
  `HST_OldValue` text,
  `HST_NewValue` text,
  `HST_ActionType` int(11) NOT NULL,
  `HST_ActionTime` datetime NOT NULL,
  `HST_ActionBy` varchar(50) NOT NULL,
  PRIMARY KEY (`HST_HSTID`),
  KEY `Index_ObjectName` (`HST_ObjectName`),
  KEY `Index_ObjectId` (`HST_ObjectId`),
  KEY `Index_ActionTime` (`HST_ActionTime`),
  KEY `Index_ActionBy` (`HST_ActionBy`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `historytracking`
--

LOCK TABLES `historytracking` WRITE;
/*!40000 ALTER TABLE `historytracking` DISABLE KEYS */;
/*!40000 ALTER TABLE `historytracking` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `initialtreatment`
--

DROP TABLE IF EXISTS `initialtreatment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `initialtreatment` (
  `ITR_ITRId` int(11) NOT NULL,
  `ITR_PTNId` int(11) NOT NULL,
  `ITR_USRId` int(11) NOT NULL,
  `ITR_THPId` int(11) NOT NULL,
  `ITR_TreatmentTime` datetime NOT NULL,
  `ITR_TreatmentDurationMinutes` int(11) NOT NULL,
  `ITR_OpenToTherapist` bit(1) NOT NULL,
  `ITR_IsComplete` bit(1) NOT NULL DEFAULT b'0',
  `ITR_IsTestData` bit(1) NOT NULL,
  `ITR_CreatedTime` datetime NOT NULL,
  `ITR_UpdatedTime` datetime DEFAULT NULL,
  `ITR_CreatedBy` varchar(50) NOT NULL,
  `ITR_UpdatedBy` varchar(50) DEFAULT NULL,
  `ITR_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ITR_ITRId`),
  KEY `fk_initialtreatment_therapyType` (`ITR_THPId`),
  KEY `fk_initialtreatment_patient` (`ITR_PTNId`),
  KEY `fk_initialtreatment_user` (`ITR_USRId`),
  CONSTRAINT `fk_initialtreatment_patient` FOREIGN KEY (`ITR_PTNId`) REFERENCES `patient` (`PTN_PTNId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_initialtreatment_therapyType` FOREIGN KEY (`ITR_THPId`) REFERENCES `therapytype` (`THP_THPId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_initialtreatment_user` FOREIGN KEY (`ITR_USRId`) REFERENCES `user` (`USR_USRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `initialtreatment`
--

LOCK TABLES `initialtreatment` WRITE;
/*!40000 ALTER TABLE `initialtreatment` DISABLE KEYS */;
/*!40000 ALTER TABLE `initialtreatment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `insurer`
--

DROP TABLE IF EXISTS `insurer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `insurer` (
  `INS_INSId` int(11) NOT NULL,
  `INS_InsurerName` varchar(100) NOT NULL,
  `INS_WarnTreatmentPerDay` int(11) NOT NULL DEFAULT '0',
  `INS_MaxTreatmentPerDay` int(11) NOT NULL DEFAULT '0',
  `INS_Comment` varchar(255) DEFAULT NULL,
  `INS_IsTestData` bit(1) NOT NULL,
  `INS_CreatedTime` datetime NOT NULL,
  `INS_UpdatedTime` datetime DEFAULT NULL,
  `INS_CreatedBy` varchar(50) NOT NULL,
  `INS_UpdatedBy` varchar(50) DEFAULT NULL,
  `INS_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`INS_INSId`),
  UNIQUE KEY `INS_InsurerName_UNIQUE` (`INS_InsurerName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `insurer`
--

LOCK TABLES `insurer` WRITE;
/*!40000 ALTER TABLE `insurer` DISABLE KEYS */;
/*!40000 ALTER TABLE `insurer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `invoice`
--

DROP TABLE IF EXISTS `invoice`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `invoice` (
  `INV_INVId` int(11) NOT NULL,
  `INV_PTNId` int(11) NOT NULL,
  `INV_USRId` int(11) NOT NULL,
  `INV_TherapistRegistration` varchar(500) NOT NULL DEFAULT '',
  `INV_THPId` int(11) NOT NULL,
  `INV_InvoiceNumber` varchar(15) NOT NULL,
  `INV_StatementDate` datetime NOT NULL,
  `INV_HSTNumber` varchar(15) DEFAULT NULL,
  `INV_Title` varchar(255) NOT NULL,
  `INV_Note` varchar(255) DEFAULT NULL,
  `INV_IsTestData` bit(1) NOT NULL,
  `INV_CreatedTime` datetime NOT NULL,
  `INV_UpdatedTime` datetime DEFAULT NULL,
  `INV_CreatedBy` varchar(50) NOT NULL,
  `INV_UpdatedBy` varchar(50) DEFAULT NULL,
  `INV_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`INV_INVId`),
  UNIQUE KEY `INV_InvoiceNumber_UNIQUE` (`INV_InvoiceNumber`),
  KEY `fk_invoice_patient` (`INV_PTNId`),
  KEY `fk_invoice_therapist` (`INV_USRId`),
  KEY `fk_invoice_therapytype_idx` (`INV_THPId`),
  CONSTRAINT `fk_invoice_patient` FOREIGN KEY (`INV_PTNId`) REFERENCES `patient` (`PTN_PTNId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_invoice_therapist` FOREIGN KEY (`INV_USRId`) REFERENCES `user` (`USR_USRId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_invoice_therapytype` FOREIGN KEY (`INV_THPId`) REFERENCES `therapytype` (`THP_THPId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `invoice`
--

LOCK TABLES `invoice` WRITE;
/*!40000 ALTER TABLE `invoice` DISABLE KEYS */;
/*!40000 ALTER TABLE `invoice` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `invoiceitem`
--

DROP TABLE IF EXISTS `invoiceitem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `invoiceitem` (
  `INI_INIId` int(11) NOT NULL,
  `INI_INVId` int(11) NOT NULL,
  `INI_ServiceDate` datetime NOT NULL,
  `INI_ServiceDuration` int(11) NOT NULL DEFAULT '60',
  `INI_ServiceDescription` varchar(100) NOT NULL,
  `INI_Amount` decimal(10,2) NOT NULL,
  `INI_IsTestData` bit(1) NOT NULL,
  `INI_CreatedTime` datetime NOT NULL,
  `INI_UpdatedTime` datetime DEFAULT NULL,
  `INI_CreatedBy` varchar(50) NOT NULL,
  `INI_UpdatedBy` varchar(50) DEFAULT NULL,
  `INI_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`INI_INIId`),
  KEY `fk_invoiceitem_invoice` (`INI_INVId`),
  KEY `fk_invoiceitem_therapytype` (`INI_ServiceDescription`),
  CONSTRAINT `fk_invoiceitem_invoice` FOREIGN KEY (`INI_INVId`) REFERENCES `invoice` (`INV_INVId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `invoiceitem`
--

LOCK TABLES `invoiceitem` WRITE;
/*!40000 ALTER TABLE `invoiceitem` DISABLE KEYS */;
/*!40000 ALTER TABLE `invoiceitem` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `invoicenotereference`
--

DROP TABLE IF EXISTS `invoicenotereference`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `invoicenotereference` (
  `INO_INOId` int(11) NOT NULL,
  `INO_Text` varchar(255) NOT NULL,
  `INO_IsTestData` bit(1) NOT NULL,
  `INO_CreatedTime` datetime NOT NULL,
  `INO_UpdatedTime` datetime DEFAULT NULL,
  `INO_CreatedBy` varchar(50) NOT NULL,
  `INO_UpdatedBy` varchar(50) DEFAULT NULL,
  `INO_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`INO_INOId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `invoicenotereference`
--

LOCK TABLES `invoicenotereference` WRITE;
/*!40000 ALTER TABLE `invoicenotereference` DISABLE KEYS */;
/*!40000 ALTER TABLE `invoicenotereference` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `massagedetail`
--

DROP TABLE IF EXISTS `massagedetail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `massagedetail` (
  `MSG_MSGId` int(11) NOT NULL,
  `MSG_ITRId` int(11) NOT NULL,
  `MSG_MassageDGRId` int(11) DEFAULT NULL,
  `MSG_ActivityLimitation` text,
  `MSG_TreatmentGoal` text,
  `MSG_TreatmentFocus` text,
  `MSG_TreatmentFrequency` text,
  `MSG_TreatmentDuration` text,
  `MSG_TreatmentPlanDiscussedWithClient` bit(1) DEFAULT NULL,
  `MSG_ConsentReceived` bit(1) DEFAULT NULL,
  `MSG_TreatmentAreaBack` bit(1) DEFAULT NULL,
  `MSG_TreatmentAreaNeck` bit(1) DEFAULT NULL,
  `MSG_TreatmentAreaShoulders` bit(1) DEFAULT NULL,
  `MSG_TreatmentAreaFace` bit(1) DEFAULT NULL,
  `MSG_TreatmentAreaLeftArm` bit(1) DEFAULT NULL,
  `MSG_TreatmentAreaRightArm` bit(1) DEFAULT NULL,
  `MSG_TreatmentAreaLeftLeg` bit(1) DEFAULT NULL,
  `MSG_TreatmentAreaRightLeg` bit(1) DEFAULT NULL,
  `MSG_TreatmentAreaGluteus` bit(1) DEFAULT NULL,
  `MSG_TreatmentAreaAbdominals` bit(1) DEFAULT NULL,
  `MSG_TreatmentAreaChest` bit(1) DEFAULT NULL,
  `MSG_TreatmentAreaBreast` bit(1) DEFAULT NULL,
  `MSG_TreatmentAreaOther` varchar(255) DEFAULT NULL,
  `MSG_AssessmentsPerformed` text,
  `MSG_ResultsOfAssessments` text,
  `MSG_ReassessmentSchedule` text,
  `MSG_Referrals` text,
  `MSG_AnticipatedProgressionOfResponses` text,
  `MSG_RemedialExercisesRecommended` text,
  `MSG_Risks` text,
  `MSG_IsTestData` bit(1) NOT NULL,
  `MSG_CreatedTime` datetime NOT NULL,
  `MSG_UpdatedTime` datetime DEFAULT NULL,
  `MSG_CreatedBy` varchar(50) NOT NULL,
  `MSG_UpdatedBy` varchar(50) DEFAULT NULL,
  `MSG_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`MSG_MSGId`),
  UNIQUE KEY `MSG_ITRId_UNIQUE` (`MSG_ITRId`),
  KEY `fk_massage_initialtreatment` (`MSG_ITRId`),
  KEY `fk_massage_diagram` (`MSG_MassageDGRId`),
  CONSTRAINT `fk_massage_diagram` FOREIGN KEY (`MSG_MassageDGRId`) REFERENCES `diagram` (`DGR_DGRId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_massage_initialtreatment` FOREIGN KEY (`MSG_ITRId`) REFERENCES `initialtreatment` (`ITR_ITRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `massagedetail`
--

LOCK TABLES `massagedetail` WRITE;
/*!40000 ALTER TABLE `massagedetail` DISABLE KEYS */;
/*!40000 ALTER TABLE `massagedetail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `massagefollowupdetail`
--

DROP TABLE IF EXISTS `massagefollowupdetail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `massagefollowupdetail` (
  `MSD_MSDId` int(11) NOT NULL,
  `MSD_FTRId` int(11) DEFAULT NULL,
  `MSD_TreatmentUsedStroking` bit(1) DEFAULT NULL,
  `MSD_TreatmentUsedRocking` bit(1) DEFAULT NULL,
  `MSD_TreatmentUsedEffleurage` bit(1) DEFAULT NULL,
  `MSD_TreatmentUsedPetrissage` bit(1) DEFAULT NULL,
  `MSD_TreatmentUsedFriction` bit(1) DEFAULT NULL,
  `MSD_TreatmentUsedVibration` bit(1) DEFAULT NULL,
  `MSD_TreatmentUsedTapotement` bit(1) DEFAULT NULL,
  `MSD_TreatmentUsedFacial` bit(1) DEFAULT NULL,
  `MSD_TreatmentUsedMyoFacialTriggerPoint` bit(1) DEFAULT NULL,
  `MSD_TreatmentUsedHighGradeJointMobilization` bit(1) DEFAULT NULL,
  `MSD_TreatmentUsedLowGradeJointMobilization` bit(1) DEFAULT NULL,
  `MSD_TreatmentUsedStretch` bit(1) DEFAULT NULL,
  `MSD_TreatmentUsedIntraOral` bit(1) DEFAULT NULL,
  `MSD_TreatmentUsedBreastMassage` bit(1) DEFAULT NULL,
  `MSD_TreatmentUsedOther` varchar(255) DEFAULT NULL,
  `MSD_TreatmentNote` text,
  `MSD_TreatmentAreaBack` bit(1) DEFAULT NULL,
  `MSD_TreatmentAreaNeck` bit(1) DEFAULT NULL,
  `MSD_TreatmentAreaShoulders` bit(1) DEFAULT NULL,
  `MSD_TreatmentAreaFace` bit(1) DEFAULT NULL,
  `MSD_TreatmentAreaLeftArm` bit(1) DEFAULT NULL,
  `MSD_TreatmentAreaRightArm` bit(1) DEFAULT NULL,
  `MSD_TreatmentAreaLeftLeg` bit(1) DEFAULT NULL,
  `MSD_TreatmentAreaRightLeg` bit(1) DEFAULT NULL,
  `MSD_TreatmentAreaGluteus` bit(1) DEFAULT NULL,
  `MSD_TreatmentAreaAbdominals` bit(1) DEFAULT NULL,
  `MSD_TreatmentAreaChest` bit(1) DEFAULT NULL,
  `MSD_TreatmentAreaBreast` bit(1) DEFAULT NULL,
  `MSD_TreatmentAreaOther` varchar(255) DEFAULT NULL,
  `MSD_IsTestData` bit(1) NOT NULL,
  `MSD_CreatedTime` datetime NOT NULL,
  `MSD_UpdatedTime` datetime DEFAULT NULL,
  `MSD_CreatedBy` varchar(50) NOT NULL,
  `MSD_UpdatedBy` varchar(50) DEFAULT NULL,
  `MSD_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`MSD_MSDId`),
  KEY `fk_massagefollowupdetail_followuptreatment` (`MSD_FTRId`),
  CONSTRAINT `fk_massagefollowupdetail_followuptreatment` FOREIGN KEY (`MSD_FTRId`) REFERENCES `followuptreatment` (`FTR_FTRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `massagefollowupdetail`
--

LOCK TABLES `massagefollowupdetail` WRITE;
/*!40000 ALTER TABLE `massagefollowupdetail` DISABLE KEYS */;
/*!40000 ALTER TABLE `massagefollowupdetail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `naturopathicdetail`
--

DROP TABLE IF EXISTS `naturopathicdetail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `naturopathicdetail` (
  `NPC_NPCId` int(11) NOT NULL,
  `NPC_ITRId` int(11) NOT NULL,
  `NPC_Height` varchar(50) DEFAULT NULL,
  `NPC_Weight` varchar(50) DEFAULT NULL,
  `NPC_Sex` varchar(50) DEFAULT NULL,
  `NPC_Acne` bit(1) DEFAULT NULL,
  `NPC_Eczema` bit(1) DEFAULT NULL,
  `NPC_DrySkin` bit(1) DEFAULT NULL,
  `NPC_Psoriasis` bit(1) DEFAULT NULL,
  `NPC_NightSweats` bit(1) DEFAULT NULL,
  `NPC_PainUrinating` bit(1) DEFAULT NULL,
  `NPC_BloodInUrine` bit(1) DEFAULT NULL,
  `NPC_PoorVision` bit(1) DEFAULT NULL,
  `NPC_EyeDryness` bit(1) DEFAULT NULL,
  `NPC_EyeDischarge` bit(1) DEFAULT NULL,
  `NPC_Glaucoma` bit(1) DEFAULT NULL,
  `NPC_Cataracts` bit(1) DEFAULT NULL,
  `NPC_Heartburn` bit(1) DEFAULT NULL,
  `NPC_Anxiety` bit(1) DEFAULT NULL,
  `NPC_MouthDryness` bit(1) DEFAULT NULL,
  `NPC_Constipation` bit(1) DEFAULT NULL,
  `NPC_MuscleWeakness` bit(1) DEFAULT NULL,
  `NPC_RectalBleeding` bit(1) DEFAULT NULL,
  `NPC_SwollenNeckGlands` bit(1) DEFAULT NULL,
  `NPC_WeightGainLoss` bit(1) DEFAULT NULL,
  `NPC_NeckStiffness` bit(1) DEFAULT NULL,
  `NPC_SinusProblems` bit(1) DEFAULT NULL,
  `NPC_HeartPalpitations` bit(1) DEFAULT NULL,
  `NPC_AnkleSwelling` bit(1) DEFAULT NULL,
  `NPC_AppetiteChagnes` bit(1) DEFAULT NULL,
  `NPC_ThirstChanges` bit(1) DEFAULT NULL,
  `NPC_ColdHandsFeet` bit(1) DEFAULT NULL,
  `NPC_ThyroidProblems` bit(1) DEFAULT NULL,
  `NPC_Headaches` bit(1) DEFAULT NULL,
  `NPC_NeckPain` bit(1) DEFAULT NULL,
  `NPC_NoseBleeds` bit(1) DEFAULT NULL,
  `NPC_Cough` bit(1) DEFAULT NULL,
  `NPC_Wheeze` bit(1) DEFAULT NULL,
  `NPC_ChestPain` bit(1) DEFAULT NULL,
  `NPC_ShortBreath` bit(1) DEFAULT NULL,
  `NPC_Asthma` bit(1) DEFAULT NULL,
  `NPC_Bronchitis` bit(1) DEFAULT NULL,
  `NPC_Emphysema` bit(1) DEFAULT NULL,
  `NPC_BreastLump` bit(1) DEFAULT NULL,
  `NPC_BreastPain` bit(1) DEFAULT NULL,
  `NPC_ProstateSymptoms` bit(1) DEFAULT NULL,
  `NPC_Arthritis` bit(1) DEFAULT NULL,
  `NPC_HotFlushes` bit(1) DEFAULT NULL,
  `NPC_HeavyPeriods` bit(1) DEFAULT NULL,
  `NPC_EasyBruising` bit(1) DEFAULT NULL,
  `NPC_Allergies` bit(1) DEFAULT NULL,
  `NPC_Depression` bit(1) DEFAULT NULL,
  `NPC_HeartDisease` bit(1) DEFAULT NULL,
  `NPC_PoorSleep` bit(1) DEFAULT NULL,
  `NPC_Anemia` bit(1) DEFAULT NULL,
  `NPC_JointPain` bit(1) DEFAULT NULL,
  `NPC_Fatigue` bit(1) DEFAULT NULL,
  `NPC_Dizziness` bit(1) DEFAULT NULL,
  `NPC_Seizures` bit(1) DEFAULT NULL,
  `NPC_PMS` bit(1) DEFAULT NULL,
  `NPC_Diarrhea` bit(1) DEFAULT NULL,
  `NPC_SeenNaturopathicDoctorBefore` bit(1) DEFAULT NULL,
  `NPC_SeenNaturopathicDoctorDate` datetime DEFAULT NULL,
  `NPC_VisitPurposeGeneral` bit(1) DEFAULT NULL,
  `NPC_VisitPurposeSpecificConcern` bit(1) DEFAULT NULL,
  `NPC_VisitPurposeSpecificConcernDescription` text,
  `NPC_IsTestData` bit(1) NOT NULL DEFAULT b'0',
  `NPC_CreatedTime` datetime NOT NULL,
  `NPC_UpdatedTime` datetime DEFAULT NULL,
  `NPC_CreatedBy` varchar(50) NOT NULL,
  `NPC_UpdatedBy` varchar(50) DEFAULT NULL,
  `NPC_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`NPC_NPCId`),
  KEY `fk_naturopathic_initialtreatment` (`NPC_ITRId`),
  CONSTRAINT `fk_naturopathic_initialtreatment` FOREIGN KEY (`NPC_ITRId`) REFERENCES `initialtreatment` (`ITR_ITRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `naturopathicdetail`
--

LOCK TABLES `naturopathicdetail` WRITE;
/*!40000 ALTER TABLE `naturopathicdetail` DISABLE KEYS */;
/*!40000 ALTER TABLE `naturopathicdetail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `organization`
--

DROP TABLE IF EXISTS `organization`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `organization` (
  `ORG_ORGId` int(11) NOT NULL,
  `ORG_OrganizationName` varchar(255) NOT NULL,
  `ORG_IsTestData` bit(1) NOT NULL,
  `ORG_CreatedTime` datetime NOT NULL,
  `ORG_UpdatedTime` datetime DEFAULT NULL,
  `ORG_CreatedBy` varchar(50) NOT NULL,
  `ORG_UpdatedBy` varchar(50) DEFAULT NULL,
  `ORG_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ORG_ORGId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organization`
--

LOCK TABLES `organization` WRITE;
/*!40000 ALTER TABLE `organization` DISABLE KEYS */;
INSERT INTO `organization` VALUES (1,'CCO','\0','2012-11-17 09:41:48',NULL,'system',NULL,1),(2,'DOMP','\0','2012-11-17 09:41:57',NULL,'system',NULL,1),(3,'RPK','\0','2012-11-17 09:42:04',NULL,'system',NULL,1),(4,'COCOO','\0','2013-09-14 16:31:39',NULL,'system',NULL,1),(5,'CTCMPAO','\0','2013-09-25 16:34:31',NULL,'system',NULL,1),(6,'DOMP','\0','2013-09-25 16:34:51',NULL,'system',NULL,1),(7,'CTCMPAO','\0','2013-09-30 18:05:19',NULL,'system',NULL,1),(8,'OAND','\0','2013-11-07 13:07:44',NULL,'system',NULL,1),(9,'OOAMA','\0','2014-04-13 17:33:35',NULL,'system',NULL,1);
/*!40000 ALTER TABLE `organization` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `osteopathydetail`
--

DROP TABLE IF EXISTS `osteopathydetail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `osteopathydetail` (
  `OSP_OSPId` int(11) NOT NULL,
  `OSP_ITRId` int(11) NOT NULL,
  `OSP_OsteopathyDGRId` int(11) DEFAULT NULL,
  `OSP_ActivityLimitation` text,
  `OSP_TreatmentGoal` text,
  `OSP_TreatmentFocus` text,
  `OSP_TreatmentFrequency` text,
  `OSP_TreatmentDuration` text,
  `OSP_TreatmentPlanDiscussedWithClient` bit(1) DEFAULT NULL,
  `OSP_ConsentReceived` bit(1) DEFAULT NULL,
  `OSP_TreatmentAreaBack` bit(1) DEFAULT NULL,
  `OSP_TreatmentAreaNeck` bit(1) DEFAULT NULL,
  `OSP_TreatmentAreaShoulders` bit(1) DEFAULT NULL,
  `OSP_TreatmentAreaFace` bit(1) DEFAULT NULL,
  `OSP_TreatmentAreaLeftArm` bit(1) DEFAULT NULL,
  `OSP_TreatmentAreaRightArm` bit(1) DEFAULT NULL,
  `OSP_TreatmentAreaLeftLeg` bit(1) DEFAULT NULL,
  `OSP_TreatmentAreaRightLeg` bit(1) DEFAULT NULL,
  `OSP_TreatmentAreaGluteus` bit(1) DEFAULT NULL,
  `OSP_TreatmentAreaAbdominals` bit(1) DEFAULT NULL,
  `OSP_TreatmentAreaChest` bit(1) DEFAULT NULL,
  `OSP_TreatmentAreaBreast` bit(1) DEFAULT NULL,
  `OSP_TreatmentAreaOther` varchar(255) DEFAULT NULL,
  `OSP_AssessmentsPerformed` text,
  `OSP_ResultsOfAssessments` text,
  `OSP_ReassessmentSchedule` text,
  `OSP_Referrals` text,
  `OSP_AnticipatedProgressionOfResponses` text,
  `OSP_RemedialExercisesRecommended` text,
  `OSP_Risks` text,
  `OSP_IsTestData` bit(1) NOT NULL,
  `OSP_CreatedTime` datetime NOT NULL,
  `OSP_UpdatedTime` datetime DEFAULT NULL,
  `OSP_CreatedBy` varchar(50) NOT NULL,
  `OSP_UpdatedBy` varchar(50) DEFAULT NULL,
  `OSP_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`OSP_OSPId`),
  UNIQUE KEY `OSP_ITRId_UNIQUE` (`OSP_ITRId`),
  KEY `fk_osteopathy_diagram_idx` (`OSP_OsteopathyDGRId`),
  KEY `fk_osteopathy_initialTreatment_idx` (`OSP_ITRId`),
  CONSTRAINT `fk_osteopathy_diagram` FOREIGN KEY (`OSP_OsteopathyDGRId`) REFERENCES `diagram` (`DGR_DGRId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_osteopathy_initialTreatment` FOREIGN KEY (`OSP_ITRId`) REFERENCES `initialtreatment` (`ITR_ITRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `osteopathydetail`
--

LOCK TABLES `osteopathydetail` WRITE;
/*!40000 ALTER TABLE `osteopathydetail` DISABLE KEYS */;
/*!40000 ALTER TABLE `osteopathydetail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `osteopathyfollowupdetail`
--

DROP TABLE IF EXISTS `osteopathyfollowupdetail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `osteopathyfollowupdetail` (
  `OSF_OSFId` int(11) NOT NULL,
  `OSF_FTRId` int(11) DEFAULT NULL,
  `OSF_TreatmentUsedRocking` bit(1) DEFAULT NULL,
  `OSF_TreatmentUsedPetrissage` bit(1) DEFAULT NULL,
  `OSF_TreatmentUsedFriction` bit(1) DEFAULT NULL,
  `OSF_TreatmentUsedVibration` bit(1) DEFAULT NULL,
  `OSF_TreatmentUsedTapotement` bit(1) DEFAULT NULL,
  `OSF_TreatmentUsedMyofacialRelease` bit(1) DEFAULT NULL,
  `OSF_TreatmentUsedTenderPointRelease` bit(1) DEFAULT NULL,
  `OSF_TreatmentUsedMuscleEnergy` bit(1) DEFAULT NULL,
  `OSF_TreatmentUsedTotalBodyAdjustment` bit(1) DEFAULT NULL,
  `OSF_TreatmentUsedStillTechnique` bit(1) DEFAULT NULL,
  `OSF_TreatmentUsedCraniosacral` bit(1) DEFAULT NULL,
  `OSF_TreatmentUsedVisceral` bit(1) DEFAULT NULL,
  `OSF_TreatmentUsedSoftTissueJointMobilization` bit(1) DEFAULT NULL,
  `OSF_TreatmentUsedStretch` bit(1) DEFAULT NULL,
  `OSF_TreatmentUsedIntraOral` bit(1) DEFAULT NULL,
  `OSF_TreatmentUsedOther` varchar(255) DEFAULT NULL,
  `OSF_TreatmentNote` text,
  `OSF_TreatmentAreaBack` bit(1) DEFAULT NULL,
  `OSF_TreatmentAreaNeck` bit(1) DEFAULT NULL,
  `OSF_TreatmentAreaShoulders` bit(1) DEFAULT NULL,
  `OSF_TreatmentAreaFace` bit(1) DEFAULT NULL,
  `OSF_TreatmentAreaLeftArm` bit(1) DEFAULT NULL,
  `OSF_TreatmentAreaRightArm` bit(1) DEFAULT NULL,
  `OSF_TreatmentAreaLeftLeg` bit(1) DEFAULT NULL,
  `OSF_TreatmentAreaRightLeg` bit(1) DEFAULT NULL,
  `OSF_TreatmentAreaGluteus` bit(1) DEFAULT NULL,
  `OSF_TreatmentAreaAbdominals` bit(1) DEFAULT NULL,
  `OSF_TreatmentAreaChest` bit(1) DEFAULT NULL,
  `OSF_TreatmentAreaBreast` bit(1) DEFAULT NULL,
  `OSF_TreatmentAreaOther` varchar(255) DEFAULT NULL,
  `OSF_IsTestData` bit(1) NOT NULL,
  `OSF_CreatedTime` datetime NOT NULL,
  `OSF_UpdatedTime` datetime DEFAULT NULL,
  `OSF_CreatedBy` varchar(50) NOT NULL,
  `OSF_UpdatedBy` varchar(50) DEFAULT NULL,
  `OSF_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`OSF_OSFId`),
  KEY `fk_osteopathy_followuptreatment_idx` (`OSF_FTRId`),
  CONSTRAINT `fk_osteopathy_followuptreatment` FOREIGN KEY (`OSF_FTRId`) REFERENCES `followuptreatment` (`FTR_FTRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `osteopathyfollowupdetail`
--

LOCK TABLES `osteopathyfollowupdetail` WRITE;
/*!40000 ALTER TABLE `osteopathyfollowupdetail` DISABLE KEYS */;
/*!40000 ALTER TABLE `osteopathyfollowupdetail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patient`
--

DROP TABLE IF EXISTS `patient`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `patient` (
  `PTN_PTNId` int(11) NOT NULL,
  `PTN_FileNumber` varchar(4) NOT NULL,
  `PTN_FirstName` varchar(50) NOT NULL,
  `PTN_MiddleName` varchar(50) DEFAULT NULL,
  `PTN_LastName` varchar(50) NOT NULL,
  `PTN_DateOfBirth` datetime NOT NULL,
  `PTN_Sex` int(11) NOT NULL,
  `AddressLine1` varchar(50) DEFAULT NULL,
  `AddressLine2` varchar(50) DEFAULT NULL,
  `PostalCode` varchar(7) DEFAULT NULL,
  `HomePhone` varchar(20) DEFAULT NULL,
  `CellPhone` varchar(20) DEFAULT NULL,
  `HomeFax` varchar(20) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `PTN_INSId` int(11) DEFAULT NULL,
  `PTN_IsTestData` bit(1) NOT NULL,
  `PTN_CreatedTime` datetime NOT NULL,
  `PTN_UpdatedTime` datetime DEFAULT NULL,
  `PTN_CreatedBy` varchar(50) NOT NULL,
  `PTN_UpdatedBy` varchar(50) DEFAULT NULL,
  `PTN_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`PTN_PTNId`),
  UNIQUE KEY `PTN_FileNumber_UNIQUE` (`PTN_FileNumber`),
  KEY `fk_patient_insurer_idx` (`PTN_INSId`),
  CONSTRAINT `fk_patient_insurer` FOREIGN KEY (`PTN_INSId`) REFERENCES `insurer` (`INS_INSId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patient`
--

LOCK TABLES `patient` WRITE;
/*!40000 ALTER TABLE `patient` DISABLE KEYS */;
/*!40000 ALTER TABLE `patient` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `physiotherapydetail`
--

DROP TABLE IF EXISTS `physiotherapydetail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `physiotherapydetail` (
  `TRD_TRDId` int(11) NOT NULL,
  `TRD_ITRId` int(11) NOT NULL,
  `TRD_PainScaleDGRId` int(11) NOT NULL,
  `TRD_PainScaleNow` int(11) DEFAULT NULL,
  `TRD_PainScaleBest` int(11) DEFAULT NULL,
  `TRD_PainScaleWorst` int(11) DEFAULT NULL,
  `TRD_PainKey` int(11) DEFAULT NULL,
  `TRD_DescriptionOfSymptoms` text,
  `TRD_SymptomTrend` int(11) DEFAULT NULL,
  `TRD_ConstantVsIntermittent` text,
  `TRD_DayChanges` text,
  `TRD_IrritabilityLevel` int(11) DEFAULT NULL,
  `TRD_AggravatedBy` text,
  `TRD_EasedBy` text,
  `TRD_Occupation` text,
  `TRD_WorkStatus` text,
  `TRD_HistoryOfPresentingComplaint` text,
  `TRD_JointSoundsAndAbnormalities` text,
  `TRD_PastRelevantHistoryAndTreatment` text,
  `TRD_Investigations` text,
  `TRD_Medications` text,
  `TRD_GeneralMedicalHistoryAndMedications` text,
  `TRD_SocialHistoryAndRecreationalActivities` text,
  `TRD_Goals` text,
  `TRD_RedFlagsAndPrecautions` text,
  `TRD_ObservationPostureGait` text,
  `TRD_ActiveRangeOfMotion` text,
  `TRD_MuscleStrengthAndMyotomes` text,
  `TRD_Palpation` text,
  `TRD_SpineDGRId` int(11) NOT NULL,
  `TRD_Neurological` text,
  `TRD_Reflexes` text,
  `TRD_Sensation` text,
  `TRD_PassiveRangeOfMotion` text,
  `TRD_StabilityTestsAndSpecialTests` text,
  `TRD_Analysis` text,
  `TRD_TreatmentPlan` text,
  `TRD_ConsentForTreatment` int(11) DEFAULT NULL,
  `TRD_IsTestData` bit(1) NOT NULL,
  `TRD_CreatedTime` datetime NOT NULL,
  `TRD_UpdatedTime` datetime DEFAULT NULL,
  `TRD_CreatedBy` varchar(50) NOT NULL,
  `TRD_UpdatedBy` varchar(50) DEFAULT NULL,
  `TRD_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`TRD_TRDId`),
  UNIQUE KEY `TRD_ITRId_UNIQUE` (`TRD_ITRId`),
  KEY `fk_physiotherapydetail_initialtreatment` (`TRD_ITRId`),
  KEY `fk_physiotherapydetail_pain_diagram` (`TRD_PainScaleDGRId`),
  KEY `fk_physiotherapydetail_spine_diagram` (`TRD_SpineDGRId`),
  CONSTRAINT `fk_physiotherapydetail_initialtreatment` FOREIGN KEY (`TRD_ITRId`) REFERENCES `initialtreatment` (`ITR_ITRId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_physiotherapydetail_pain_diagram` FOREIGN KEY (`TRD_PainScaleDGRId`) REFERENCES `diagram` (`DGR_DGRId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_physiotherapydetail_spine_diagram` FOREIGN KEY (`TRD_SpineDGRId`) REFERENCES `diagram` (`DGR_DGRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `physiotherapydetail`
--

LOCK TABLES `physiotherapydetail` WRITE;
/*!40000 ALTER TABLE `physiotherapydetail` DISABLE KEYS */;
/*!40000 ALTER TABLE `physiotherapydetail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pointonacupuncture`
--

DROP TABLE IF EXISTS `pointonacupuncture`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pointonacupuncture` (
  `PAC_PACId` int(11) NOT NULL,
  `PAC_DGRId` int(11) DEFAULT NULL,
  `PAC_TRDId` int(11) DEFAULT NULL,
  `PAC_X` int(11) DEFAULT NULL,
  `PAC_Y` int(11) DEFAULT NULL,
  `PAC_PainKey` int(11) DEFAULT NULL,
  `PAC_IsTestData` bit(1) NOT NULL,
  `PAC_CreatedTime` datetime NOT NULL,
  `PAC_UpdatedTime` datetime DEFAULT NULL,
  `PAC_CreatedBy` varchar(50) NOT NULL,
  `PAC_UpdatedBy` varchar(50) DEFAULT NULL,
  `PAC_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`PAC_PACId`),
  KEY `fk_pointonacupuncture_diagram` (`PAC_DGRId`),
  KEY `fk_pointonacupuncture_acupuncturedetail` (`PAC_TRDId`),
  CONSTRAINT `fk_pointonacupuncture_acupuncturedetail` FOREIGN KEY (`PAC_TRDId`) REFERENCES `acupuncturedetail` (`ACP_ACPId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_pointonacupuncture_diagram` FOREIGN KEY (`PAC_DGRId`) REFERENCES `diagram` (`DGR_DGRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pointonacupuncture`
--

LOCK TABLES `pointonacupuncture` WRITE;
/*!40000 ALTER TABLE `pointonacupuncture` DISABLE KEYS */;
/*!40000 ALTER TABLE `pointonacupuncture` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pointonacupuncturefollowup`
--

DROP TABLE IF EXISTS `pointonacupuncturefollowup`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pointonacupuncturefollowup` (
  `PAF_PAFId` int(11) NOT NULL,
  `PAF_DGRId` int(11) DEFAULT NULL,
  `PAF_AFDId` int(11) DEFAULT NULL,
  `PAF_X` int(11) DEFAULT NULL,
  `PAF_Y` int(11) DEFAULT NULL,
  `PAF_PainKey` int(11) DEFAULT NULL,
  `PAF_IsTestData` bit(1) NOT NULL,
  `PAF_CreatedTime` datetime NOT NULL,
  `PAF_UpdatedTime` datetime DEFAULT NULL,
  `PAF_CreatedBy` varchar(50) NOT NULL,
  `PAF_UpdatedBy` varchar(50) DEFAULT NULL,
  `PAF_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`PAF_PAFId`),
  KEY `fk_pointonacupuncturefollowup_diagram` (`PAF_DGRId`),
  KEY `fk_pointonacupuncturefollowup_acupuncturefollowupdetail` (`PAF_AFDId`),
  CONSTRAINT `fk_pointonacupuncturefollowup_acupuncturefollowdetail` FOREIGN KEY (`PAF_AFDId`) REFERENCES `acupuncturefollowupdetail` (`AFD_AFDId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_pointonacupuncturefollowup_diagram` FOREIGN KEY (`PAF_DGRId`) REFERENCES `diagram` (`DGR_DGRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pointonacupuncturefollowup`
--

LOCK TABLES `pointonacupuncturefollowup` WRITE;
/*!40000 ALTER TABLE `pointonacupuncturefollowup` DISABLE KEYS */;
/*!40000 ALTER TABLE `pointonacupuncturefollowup` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pointonchiropractic`
--

DROP TABLE IF EXISTS `pointonchiropractic`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pointonchiropractic` (
  `PCH_PCHId` int(11) NOT NULL,
  `PCH_DGRId` int(11) NOT NULL,
  `PCH_TRDId` int(11) NOT NULL,
  `PCH_X` int(11) NOT NULL,
  `PCH_Y` int(11) NOT NULL,
  `PCH_PainKey` int(11) NOT NULL,
  `PCH_IsTestData` bit(1) NOT NULL,
  `PCH_CreatedTime` datetime NOT NULL,
  `PCH_UpdatedTime` datetime DEFAULT NULL,
  `PCH_CreatedBy` varchar(50) NOT NULL,
  `PCH_UpdatedBy` varchar(50) DEFAULT NULL,
  `PCH_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`PCH_PCHId`),
  KEY `fk_pointonchiropractic_chiropracticdetail` (`PCH_TRDId`),
  KEY `fk_pointonchiropractic_diagram` (`PCH_DGRId`),
  CONSTRAINT `fk_pointonchiropractic_chiropracticdetail` FOREIGN KEY (`PCH_TRDId`) REFERENCES `chiropracticdetail` (`CHP_CHPId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_pointonchiropractic_diagram` FOREIGN KEY (`PCH_DGRId`) REFERENCES `diagram` (`DGR_DGRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pointonchiropractic`
--

LOCK TABLES `pointonchiropractic` WRITE;
/*!40000 ALTER TABLE `pointonchiropractic` DISABLE KEYS */;
/*!40000 ALTER TABLE `pointonchiropractic` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pointonchiropracticfollowup`
--

DROP TABLE IF EXISTS `pointonchiropracticfollowup`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pointonchiropracticfollowup` (
  `PCF_PCFId` int(11) NOT NULL,
  `PCF_DGRId` int(11) NOT NULL,
  `PCF_FUDId` int(11) NOT NULL,
  `PCF_X` int(11) NOT NULL,
  `PCF_Y` int(11) NOT NULL,
  `PCF_PainKey` int(11) NOT NULL,
  `PCF_IsTestData` bit(1) NOT NULL,
  `PCF_CreatedTime` datetime NOT NULL,
  `PCF_UpdatedTime` datetime DEFAULT NULL,
  `PCF_CreatedBy` varchar(50) NOT NULL,
  `PCF_UpdatedBy` varchar(50) DEFAULT NULL,
  `PCF_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`PCF_PCFId`),
  KEY `fk_pointonchiropracticfollowup_chiropracticfollowupdetail` (`PCF_FUDId`),
  KEY `fk_pointonchiropracticfollowup_diagram` (`PCF_DGRId`),
  CONSTRAINT `fk_pointonchiropracticfollowup_chiropracticfollowupdetail` FOREIGN KEY (`PCF_FUDId`) REFERENCES `chiropracticfollowupdetail` (`CFD_CFDId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_pointonchiropracticfollowup_diagram` FOREIGN KEY (`PCF_DGRId`) REFERENCES `diagram` (`DGR_DGRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pointonchiropracticfollowup`
--

LOCK TABLES `pointonchiropracticfollowup` WRITE;
/*!40000 ALTER TABLE `pointonchiropracticfollowup` DISABLE KEYS */;
/*!40000 ALTER TABLE `pointonchiropracticfollowup` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pointonmassage`
--

DROP TABLE IF EXISTS `pointonmassage`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pointonmassage` (
  `PMG_PMGId` int(11) NOT NULL,
  `PMG_DGRId` int(11) DEFAULT NULL,
  `PMG_TRDId` int(11) DEFAULT NULL,
  `PMG_X` int(11) DEFAULT NULL,
  `PMG_Y` int(11) DEFAULT NULL,
  `PMG_PainKey` int(11) DEFAULT NULL,
  `PMG_IsTestData` bit(1) NOT NULL,
  `PMG_CreatedTime` datetime NOT NULL,
  `PMG_UpdatedTime` datetime DEFAULT NULL,
  `PMG_CreatedBy` varchar(50) NOT NULL,
  `PMG_UpdatedBy` varchar(50) DEFAULT NULL,
  `PMG_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`PMG_PMGId`),
  KEY `fk_pointonmassage_diagram` (`PMG_DGRId`),
  KEY `fk_pointonmassage_massagedetail` (`PMG_TRDId`),
  CONSTRAINT `fk_pointonmassage_diagram` FOREIGN KEY (`PMG_DGRId`) REFERENCES `diagram` (`DGR_DGRId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_pointonmassage_massagedetail` FOREIGN KEY (`PMG_TRDId`) REFERENCES `massagedetail` (`MSG_MSGId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pointonmassage`
--

LOCK TABLES `pointonmassage` WRITE;
/*!40000 ALTER TABLE `pointonmassage` DISABLE KEYS */;
/*!40000 ALTER TABLE `pointonmassage` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pointonosteopathy`
--

DROP TABLE IF EXISTS `pointonosteopathy`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pointonosteopathy` (
  `POS_POSId` int(11) NOT NULL,
  `POS_DGRId` int(11) DEFAULT NULL,
  `POS_TRDId` int(11) DEFAULT NULL,
  `POS_X` int(11) DEFAULT NULL,
  `POS_Y` int(11) DEFAULT NULL,
  `POS_PainKey` int(11) DEFAULT NULL,
  `POS_IsTestData` bit(1) NOT NULL,
  `POS_CreatedTime` datetime NOT NULL,
  `POS_UpdatedTime` datetime DEFAULT NULL,
  `POS_CreatedBy` varchar(50) NOT NULL,
  `POS_UpdatedBy` varchar(50) DEFAULT NULL,
  `POS_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`POS_POSId`),
  KEY `fk_pointonosteopathy_diagram_idx` (`POS_DGRId`),
  KEY `fk_pointonosteopathy_detail_idx` (`POS_TRDId`),
  CONSTRAINT `fk_pointonosteopathy_detail` FOREIGN KEY (`POS_TRDId`) REFERENCES `osteopathydetail` (`OSP_OSPId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_pointonosteopathy_diagram` FOREIGN KEY (`POS_DGRId`) REFERENCES `diagram` (`DGR_DGRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pointonosteopathy`
--

LOCK TABLES `pointonosteopathy` WRITE;
/*!40000 ALTER TABLE `pointonosteopathy` DISABLE KEYS */;
/*!40000 ALTER TABLE `pointonosteopathy` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pointonphysiotherapy`
--

DROP TABLE IF EXISTS `pointonphysiotherapy`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pointonphysiotherapy` (
  `PPT_PPTId` int(11) NOT NULL,
  `PPT_DGRId` int(11) NOT NULL,
  `PPT_TRDId` int(11) NOT NULL,
  `PPT_X` int(11) NOT NULL,
  `PPT_Y` int(11) NOT NULL,
  `PPT_PainKey` int(11) NOT NULL,
  `PPT_IsTestData` bit(1) NOT NULL,
  `PPT_CreatedTime` datetime NOT NULL,
  `PPT_UpdatedTime` datetime DEFAULT NULL,
  `PPT_CreatedBy` varchar(50) NOT NULL,
  `PPT_UpdatedBy` varchar(50) DEFAULT NULL,
  `PPT_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`PPT_PPTId`),
  KEY `fk_pointonphysiotherapy_physiotherapydetail` (`PPT_TRDId`),
  KEY `fk_pointonphysiotherapy_diagram` (`PPT_DGRId`),
  CONSTRAINT `fk_pointonphysiotherapy_diagram` FOREIGN KEY (`PPT_DGRId`) REFERENCES `diagram` (`DGR_DGRId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_pointonphysiotherapy_physiotherapydetail` FOREIGN KEY (`PPT_TRDId`) REFERENCES `physiotherapydetail` (`TRD_TRDId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pointonphysiotherapy`
--

LOCK TABLES `pointonphysiotherapy` WRITE;
/*!40000 ALTER TABLE `pointonphysiotherapy` DISABLE KEYS */;
/*!40000 ALTER TABLE `pointonphysiotherapy` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `servicetype`
--

DROP TABLE IF EXISTS `servicetype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `servicetype` (
  `SER_SERId` int(11) NOT NULL,
  `SER_Description` varchar(255) NOT NULL,
  `SER_IsTestData` bit(1) NOT NULL,
  `SER_CreatedTime` datetime NOT NULL,
  `SER_UpdatedTime` datetime DEFAULT NULL,
  `SER_CreatedBy` varchar(50) NOT NULL,
  `SER_UpdatedBy` varchar(50) DEFAULT NULL,
  `SER_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`SER_SERId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `servicetype`
--

LOCK TABLES `servicetype` WRITE;
/*!40000 ALTER TABLE `servicetype` DISABLE KEYS */;
INSERT INTO `servicetype` VALUES (1,'Acupuncture Treatment','\0','2012-11-17 09:46:54',NULL,'system',NULL,1),(2,'Massage Treatment','\0','2012-11-17 09:47:03',NULL,'system',NULL,1),(3,'Naturopathic Treatment','\0','2012-11-17 09:47:24',NULL,'system',NULL,1),(4,'Chiropractic Treatment','\0','2012-11-17 09:47:34',NULL,'system',NULL,1);
/*!40000 ALTER TABLE `servicetype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `systemsetting`
--

DROP TABLE IF EXISTS `systemsetting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `systemsetting` (
  `SET_SETId` int(11) NOT NULL,
  `SET_Name` varchar(50) NOT NULL,
  `SET_Value` varchar(255) NOT NULL,
  `SET_IsTestData` bit(1) NOT NULL,
  `SET_CreatedTime` datetime NOT NULL,
  `SET_UpdatedTime` datetime DEFAULT NULL,
  `SET_CreatedBy` varchar(50) NOT NULL,
  `SET_UpdatedBy` varchar(50) DEFAULT NULL,
  `SET_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`SET_SETId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `systemsetting`
--

LOCK TABLES `systemsetting` WRITE;
/*!40000 ALTER TABLE `systemsetting` DISABLE KEYS */;
INSERT INTO `systemsetting` VALUES (1,'Invoice Title','<such as your Company Name, Address and Phone info>','\0','2012-11-17 22:26:20',NULL,'system',NULL,1),(2,'Invoice Number Template','INV000000','\0','2012-11-23 23:38:13',NULL,'system',NULL,1),(3,'HST Number','<your HST number>','\0','2013-09-24 17:38:19',NULL,'system',NULL,1),(4,'Treatment Note Title','<your treatment note title>','\0','2014-06-17 15:07:54',NULL,'system',NULL,1);
/*!40000 ALTER TABLE `systemsetting` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `templatetreatmentdetail`
--

DROP TABLE IF EXISTS `templatetreatmentdetail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `templatetreatmentdetail` (
  `TTD_TTDId` int(11) NOT NULL,
  `TTD_THPId` int(11) NOT NULL,
  `TTD_IsInitial` bit(1) NOT NULL,
  `TTD_DetailId` int(11) NOT NULL,
  `TTD_TreatmentNote` text,
  `TTD_Note` varchar(255) NOT NULL,
  `TTD_IsTestData` bit(1) NOT NULL,
  `TTD_CreatedTime` datetime NOT NULL,
  `TTD_UpdatedTime` datetime DEFAULT NULL,
  `TTD_CreatedBy` varchar(50) NOT NULL,
  `TTD_UpdatedBy` varchar(50) DEFAULT NULL,
  `TTD_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`TTD_TTDId`),
  KEY `fk_template_treatmenttype_idx` (`TTD_THPId`),
  CONSTRAINT `fk_template_treatmenttype` FOREIGN KEY (`TTD_THPId`) REFERENCES `therapytype` (`THP_THPId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `templatetreatmentdetail`
--

LOCK TABLES `templatetreatmentdetail` WRITE;
/*!40000 ALTER TABLE `templatetreatmentdetail` DISABLE KEYS */;
/*!40000 ALTER TABLE `templatetreatmentdetail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `therapistorganizationregistration`
--

DROP TABLE IF EXISTS `therapistorganizationregistration`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `therapistorganizationregistration` (
  `TOR_TORId` int(11) NOT NULL,
  `TOR_USRId` int(11) NOT NULL,
  `TOR_ORGId` int(11) NOT NULL,
  `TOR_RegistrationNumber` varchar(50) NOT NULL,
  `TOR_TRGId` int(11) NOT NULL,
  `TOR_IsTestData` bit(1) NOT NULL,
  `TOR_CreatedTime` datetime NOT NULL,
  `TOR_UpdatedTime` datetime DEFAULT NULL,
  `TOR_CreatedBy` varchar(50) NOT NULL,
  `TOR_UpdatedBy` varchar(50) DEFAULT NULL,
  `TOR_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`TOR_TORId`),
  KEY `fk_registration_therapist` (`TOR_USRId`),
  KEY `fk_registtration_organization` (`TOR_ORGId`),
  KEY `fk_registration_group` (`TOR_TRGId`),
  CONSTRAINT `fk_registration_group` FOREIGN KEY (`TOR_TRGId`) REFERENCES `therapistorganizationregistrationgroup` (`TRG_TRGId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_registration_therapist` FOREIGN KEY (`TOR_USRId`) REFERENCES `user` (`USR_USRId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_registtration_organization` FOREIGN KEY (`TOR_ORGId`) REFERENCES `organization` (`ORG_ORGId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `therapistorganizationregistration`
--

LOCK TABLES `therapistorganizationregistration` WRITE;
/*!40000 ALTER TABLE `therapistorganizationregistration` DISABLE KEYS */;
/*!40000 ALTER TABLE `therapistorganizationregistration` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `therapistorganizationregistrationgroup`
--

DROP TABLE IF EXISTS `therapistorganizationregistrationgroup`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `therapistorganizationregistrationgroup` (
  `TRG_TRGId` int(11) NOT NULL,
  `TRG_USRId` int(11) NOT NULL,
  `TRG_IsTestData` bit(1) NOT NULL,
  `TRG_CreatedTime` datetime NOT NULL,
  `TRG_UpdatedTime` datetime DEFAULT NULL,
  `TRG_CreatedBy` varchar(50) NOT NULL,
  `TRG_UpdatedBy` varchar(50) DEFAULT NULL,
  `TRG_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`TRG_TRGId`),
  KEY `fk_registrationgroup_therapist` (`TRG_USRId`),
  CONSTRAINT `fk_registrationgroup_therapist` FOREIGN KEY (`TRG_USRId`) REFERENCES `user` (`USR_USRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `therapistorganizationregistrationgroup`
--

LOCK TABLES `therapistorganizationregistrationgroup` WRITE;
/*!40000 ALTER TABLE `therapistorganizationregistrationgroup` DISABLE KEYS */;
/*!40000 ALTER TABLE `therapistorganizationregistrationgroup` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `therapytype`
--

DROP TABLE IF EXISTS `therapytype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `therapytype` (
  `THP_THPId` int(11) NOT NULL,
  `THP_TherapyType` varchar(100) NOT NULL,
  `THP_EnableMinutesPerTreatment` bit(1) NOT NULL DEFAULT b'0',
  `THP_MinutesPerTreatment` int(11) NOT NULL DEFAULT '60',
  `THP_EnableMaxTreatmentsPerDayPerInsurer` bit(1) NOT NULL DEFAULT b'0',
  `THP_MaxTreatmentsPerDayPerInsurer` int(11) NOT NULL,
  `THP_IsTestData` bit(1) NOT NULL,
  `THP_CreatedTime` datetime NOT NULL,
  `THP_UpdatedTime` datetime DEFAULT NULL,
  `THP_CreatedBy` varchar(50) NOT NULL,
  `THP_UpdatedBy` varchar(50) DEFAULT NULL,
  `THP_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`THP_THPId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `therapytype`
--

LOCK TABLES `therapytype` WRITE;
/*!40000 ALTER TABLE `therapytype` DISABLE KEYS */;
INSERT INTO `therapytype` VALUES (1,'Physiotherapy','\0',60,'\0',0,'\0','2012-06-13 17:46:24',NULL,'system',NULL,1),(2,'Chiropractic','\0',60,'\0',0,'\0','2012-08-28 10:34:51',NULL,'system',NULL,1),(3,'Acupuncture','\0',60,'\0',0,'\0','2012-10-19 10:41:48',NULL,'system',NULL,1),(4,'Massage','',60,'\0',0,'\0','2012-10-23 12:00:45',NULL,'system',NULL,1),(5,'Naturopathic','\0',60,'\0',0,'\0','2012-11-17 09:29:13',NULL,'system',NULL,1),(6,'Osteopath','',60,'\0',0,'\0','2013-08-15 09:50:00',NULL,'system',NULL,1);
/*!40000 ALTER TABLE `therapytype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `USR_USRId` int(11) NOT NULL,
  `USR_TTLId` int(11) DEFAULT NULL,
  `USR_Username` varchar(20) NOT NULL,
  `USR_Password` varchar(20) NOT NULL,
  `USR_FirstName` varchar(50) NOT NULL,
  `USR_MiddleName` varchar(50) DEFAULT NULL,
  `USR_LastName` varchar(50) NOT NULL,
  `USR_GoogleCalendarId` varchar(50) DEFAULT NULL,
  `USR_UserType` int(11) NOT NULL,
  `USR_LastLoginTime` datetime DEFAULT NULL,
  `USR_Signature` blob,
  `USR_IsTestData` bit(1) NOT NULL,
  `USR_CreatedTime` datetime NOT NULL,
  `USR_UpdatedTime` datetime DEFAULT NULL,
  `USR_CreatedBy` varchar(50) NOT NULL,
  `USR_UpdatedBy` varchar(50) DEFAULT NULL,
  `USR_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`USR_USRId`),
  UNIQUE KEY `USR_Username_UNIQUE` (`USR_Username`),
  KEY `fk_user_title_idx` (`USR_TTLId`),
  CONSTRAINT `fk_user_title` FOREIGN KEY (`USR_TTLId`) REFERENCES `usertitle` (`TTL_TTLId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,NULL,'admin','admin','Admin',NULL,'Admin','',0,'0001-01-01 00:00:00','‰PNG\r\n\Z\n\0\0\0\rIHDR\0\0\0\0\0\0\0\0\0àw=ø\0\0\0sRGB\0®Îé\0\0\0gAMA\0\0±üa\0\0IDATHKí”mHSQÆ¯MÝv÷®Ù”°2CK¢‚>d5mºÍLM]Ó´wL“¤ìM‚ **¢úd¹2CÒJ$¢üEõ!’cÈt»wwÛ½wÆÓYea¤èÜÇ\\Î}9ÿßóüŸÃ=õü•€Ç`¢¹¢’nñÜp\"¬1i[+ù³§ñíÝ3Œ=¹oMõPØ˜¬{àa+ÆzÚ \\®‡·j7Üé–ä°¸‹Ë½ ÚÏC8]oÝ^Œ\Z,E?à®‹Þe0­w¥›u¡¨¹m¥bO;„+\'À7„ïÈ~Œde_úÍòÔ×B°ß\0×x.sþŸÓPµøcÉ©*ð\r{á«Ý§q›}B©«Ðö^hª…xÿ:ÄŽfŒZK§Á¦œÖâÁ@—Âñrðu¥àëËàÌÉíûgíH^Á˜PY±é0·á,,˜JdØZÜxÔñä!ðU6ˆ5»à0e™´ÆYZ®ÍËÀ\n¾zm·0\\TúÏN>Ç\'Øý×.’µ;ÁYÒ ìÊÃHAµSïá¹°³æÃ_Rñ(éäÞ]|NY;!Ï7©Í1[·Š«SÁ¡ãE¦+5´ÙèÊŠáÍ4À—’ÖhÄ[)ÝÕKE&~Pj^yÌA¸ŒZF§…\'F‹—Ñ²Ó‚õ›òÖÇëÁÊä`ärpKâÁÛløš”òc>3\n•\n¥ôÌ‚—TD£PƒQ*‰‰!n>¸Äd0Ú04“÷¬R·Rn‰ôçÏ4“Ñ#‘*>gšÄ¼äDHFÀÁ{¥†t —Ì/¢¥ÜL¸ÖvK¢[|r”»P¥Õ`:â<…™\Zís¢¬!bEÿ¯.X•¬&\\ì²©óá×ÎÃóHYèîÇ]uJ¢º|:âxá\"pËá]±\n>ýR8bâÐLE†î~\\à&%1:bcá]½þ-›àÏÌ€¸.O¥ôìÝ‹tËi¯°1þüLfŒ¬IÆU*böîÇ.RTº35ÂŽ¶o@›Tö:ä¬ð*Eµö-ž‹NµòkØáá\0~B¨0Î8Íi\0\0\0\0IEND®B`‚','\0','2012-06-14 10:57:37',NULL,'system',NULL,1);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usertherapytype`
--

DROP TABLE IF EXISTS `usertherapytype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usertherapytype` (
  `User_id` int(11) NOT NULL,
  `TherapyType_id` int(11) NOT NULL,
  PRIMARY KEY (`User_id`,`TherapyType_id`),
  KEY `fk_user` (`User_id`),
  KEY `fk_therapyType` (`TherapyType_id`),
  CONSTRAINT `fk_therapyType` FOREIGN KEY (`TherapyType_id`) REFERENCES `therapytype` (`THP_THPId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_user` FOREIGN KEY (`User_id`) REFERENCES `user` (`USR_USRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usertherapytype`
--

LOCK TABLES `usertherapytype` WRITE;
/*!40000 ALTER TABLE `usertherapytype` DISABLE KEYS */;
/*!40000 ALTER TABLE `usertherapytype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usertimeoff`
--

DROP TABLE IF EXISTS `usertimeoff`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usertimeoff` (
  `UTO_UTOId` int(11) NOT NULL,
  `UTO_USRId` int(11) NOT NULL,
  `UTO_StartTime` datetime NOT NULL,
  `UTO_EndTime` datetime NOT NULL,
  `UTO_Reason` varchar(255) DEFAULT NULL,
  `UTO_IsTestData` bit(1) NOT NULL,
  `UTO_CreatedTime` datetime NOT NULL,
  `UTO_UpdatedTime` datetime DEFAULT NULL,
  `UTO_CreatedBy` varchar(50) NOT NULL,
  `UTO_UpdatedBy` varchar(50) DEFAULT NULL,
  `UTO_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`UTO_UTOId`),
  KEY `fk_user_timeoff_idx` (`UTO_USRId`),
  CONSTRAINT `fk_user_timeoff` FOREIGN KEY (`UTO_USRId`) REFERENCES `user` (`USR_USRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usertimeoff`
--

LOCK TABLES `usertimeoff` WRITE;
/*!40000 ALTER TABLE `usertimeoff` DISABLE KEYS */;
/*!40000 ALTER TABLE `usertimeoff` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usertitle`
--

DROP TABLE IF EXISTS `usertitle`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usertitle` (
  `TTL_TTLId` int(11) NOT NULL,
  `TTL_Name` varchar(10) NOT NULL,
  `TTL_IsTestData` bit(1) NOT NULL,
  `TTL_CreatedTime` datetime NOT NULL,
  `TTL_UpdatedTime` datetime DEFAULT NULL,
  `TTL_CreatedBy` varchar(50) NOT NULL,
  `TTL_UpdatedBy` varchar(50) DEFAULT NULL,
  `TTL_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`TTL_TTLId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usertitle`
--

LOCK TABLES `usertitle` WRITE;
/*!40000 ALTER TABLE `usertitle` DISABLE KEYS */;
INSERT INTO `usertitle` VALUES (1,'Dr','\0','2012-11-17 23:33:16',NULL,'system',NULL,1),(2,'PhD','\0','2012-11-17 23:33:42',NULL,'system',NULL,1);
/*!40000 ALTER TABLE `usertitle` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userweeklyavailability`
--

DROP TABLE IF EXISTS `userweeklyavailability`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `userweeklyavailability` (
  `UWA_UWAId` int(11) NOT NULL,
  `UWA_USRId` int(11) NOT NULL,
  `UWA_WeekDay` int(11) NOT NULL,
  `UWA_FullDayAvailable` bit(1) NOT NULL,
  `UWA_StartHour` decimal(4,2) NOT NULL,
  `UWA_EndHour` decimal(4,2) NOT NULL,
  `UWA_IsTestData` bit(1) NOT NULL,
  `UWA_CreatedTime` datetime NOT NULL,
  `UWA_UpdatedTime` datetime DEFAULT NULL,
  `UWA_CreatedBy` varchar(50) NOT NULL,
  `UWA_UpdatedBy` varchar(50) DEFAULT NULL,
  `UWA_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`UWA_UWAId`),
  KEY `fk_user_weeklyavailability_idx` (`UWA_USRId`),
  CONSTRAINT `fk_user_weeklyavailability` FOREIGN KEY (`UWA_USRId`) REFERENCES `user` (`USR_USRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userweeklyavailability`
--

LOCK TABLES `userweeklyavailability` WRITE;
/*!40000 ALTER TABLE `userweeklyavailability` DISABLE KEYS */;
/*!40000 ALTER TABLE `userweeklyavailability` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-09-15 16:45:32
