using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EHD.Login;
using EHD.Model.Entity;
using EHD.Repository;
using EHD.Service;
using NHibernate;
using FluentNHibernate.Cfg;
using System.Drawing;
using System.Reflection;
using System.IO;
using FilePathExtender;
using EHD.Constant;
using System.Drawing.Drawing2D;
using log4net;
using System.Threading;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;
//using GoogleCalendarHelper;
using NHibernate.Criterion;
using NHibernate.Transform;
using FluentNHibernate.MappingModel;

namespace EHD.Admin
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(false, @"{A8999D8A-D228-480F-958D-CAD9C7F90CC6}")) {

                if(!mutex.WaitOne(0, false)) {
                    MessageBox.Show("The application has already been running.");
                    return;
                }

                /***** NOTE *******
                 * !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                 * Running the following lines in Debug->Watch window to set to the proper DBName !
                 * Properties.Settings.Default.DBName = "proper database name here"
                 * Properties.Settings.Default.Save()
                 ******************/
                //Activate the NHibernate Profiler
                bool isDebugMode = Properties.Settings.Default.DebugMode;
                if (isDebugMode) {
                    HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
                }

                //Enable Log4Net
                log4net.Config.XmlConfigurator.Configure();

                log.Info("log4net config completed.");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //Load configuration
                try {

                    log.Info("Before show wait screen");

                    //Show please wait form before loading config and creating session factory
                    Thread thread = new Thread(new ThreadStart(showWaitScreen));
                    thread.Start();

                    log.Info("show wait screen thread started");

                    //Load config and construct session factory
                    config = JCConfig.instance();
                    SessionFactory.InitiateSessionFactory(config.ConnectionString);

                    //Initiate GoogleCalendarHelper

                    //Close wait form.
                    _hideWaitForm = true;

                    //debug
                    DBUpdater dbUpdater = new DBUpdater(config.ConnectionString);
                    dbUpdater.Update();

                } catch (Exception ex) {

                    //Close wait form.
                    _hideWaitForm = true;

                    MessageBox.Show(
                        "Failed to load configuration file.\n\nPlease make sure the settings are correct.\n\n" + ex.Message,
                        "Configuration Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly
                        );
                    
                    showConfigWindow();
                    
                    return;
                }

                //Verify config file
                if(config == null) {
                    MessageBox.Show(
                        "Configuration file is not correct!\nPlease contact support.",
                        "Configuration Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }

                //Other Initialization
                Assembly myAssembly = Assembly.GetExecutingAssembly();
                Stream s = myAssembly.GetManifestResourceStream("EHD.Admin.images.DotRed.png");
                PinPointImages.Add(PainKeys.None, Image.FromStream(s));
                s = myAssembly.GetManifestResourceStream("EHD.Admin.images.PainLegend.png");
                PinPointImages.Add(PainKeys.Pain, Image.FromStream(s));
                s = myAssembly.GetManifestResourceStream("EHD.Admin.images.NumbnessLegend.png");
                PinPointImages.Add(PainKeys.Numbness, Image.FromStream(s));
                s = myAssembly.GetManifestResourceStream("EHD.Admin.images.NeedleLegend.png");
                PinPointImages.Add(PainKeys.PinsAndNeedles, Image.FromStream(s));

                #region Initiate GoogleCalendar
                /*
                if (config.EnableGoogleCalendar) {
                    try {
                        _calendar = new CalendarAPI();
                    } catch (Exception ex) {
                        MessageBox.Show(
                            "Failed to initiate Google Calendar.\n" + ex.Message + "\n" + ex.StackTrace,
                            "Google Calendar Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }*/
                #endregion

                //Start
                StartBylogin();
            }
        }

        public static string CreateDiagramImageBy(PictureBox picBox) {
            //Get app root path
            string AppRootPath = System.IO.Path.GetTempPath();
            //AppRootPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //AppRootPath = AppRootPath.Substring(0, AppRootPath.LastIndexOf('\\'));

            //Create file name
            string strImagePath = FileExtender.ConcatPhysicalPath(AppRootPath,
                //Constants.CFG_PRINTOUT_FOLDER,
                Guid.NewGuid().ToString() + ".bmp");

            //Get the diagram attached to the picturebox
            Diagram diagram = picBox.Tag as Diagram;

            //Create a new bitmap
            Bitmap imgBase = new Bitmap(diagram.ImageWidth, diagram.ImageHeight);

            //Print diagram and points to the new bitmap
            using(Graphics g = Graphics.FromImage(imgBase)) {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                //Draw diagram image
                Image imgBackground = picBox.Image;
                g.DrawImage(imgBackground, 0, 0, diagram.ImageWidth, diagram.ImageHeight);

                //Draw points
                foreach(Control ctr in picBox.Controls) {
                    if(ctr is PictureBox) {
                        PictureBox picPoint = ctr as PictureBox;
                        Image imgPoint = picPoint.Image;
                        g.DrawImage(imgPoint, picPoint.Location.X, picPoint.Location.Y);
                    }
                }
            }


            //Save image
            imgBase.Save(strImagePath);

            return strImagePath;
        }

        public static string CreateImageFile(Image img) {
            //Get app root path
            string AppRootPath = System.IO.Path.GetTempPath();
            //AppRootPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //AppRootPath = AppRootPath.Substring(0, AppRootPath.LastIndexOf('\\'));

            //Create file name
            string strImagePath = FileExtender.ConcatPhysicalPath(AppRootPath,
                //Constants.CFG_PRINTOUT_FOLDER,
                Guid.NewGuid().ToString() + ".bmp");

            img.Save(strImagePath);

            return strImagePath;
        }

        public static string CreateImageFile(byte[] buff) {
            if(buff == null) return string.Empty;

            MemoryStream stream = new MemoryStream(buff);
            Image img = Image.FromStream(stream);
            stream.Close();

            return CreateImageFile(img);
        }

        private static User tryLogon(string username, string password, string newPassword) {
            User user = null;

            try {
                using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
                using(ITransaction tr = session.BeginTransaction()){

                    try {
                        JCService service = new JCService(session, Program.LogonUser);
                        user = service.GetUserByUsernamePassword(username, password);

                        if (!password.Equals(newPassword)) {
                            user.Password = newPassword;
                            user.UpdatedBy = "PassChange";
                            session.Save(user);
                        }

                        tr.Commit();
                    } catch {
                        tr.Rollback();
                    }
                }
            } catch(FluentConfigurationException ex) {
                MessageBox.Show("Configuration Error. " + ex);
            } catch(Exception ex) {
                MessageBox.Show("Failed to logon. " + ex.Message);
            }
            return user;
        }

        private static void StartBylogin() {
            #region THIS SECTION CAN BE REMOVED AFTER THE FIRST TIME RUN ON THE CLIENT'S COMPUTER
            /*
             * Verify if the database has been updated.
             * A new column INI_ServiceDuration will be added if it doesn't exist.
             * 
             * Jan 03, 2014 --- updated AcupunctureDetail table -------------
             */
            
            try {
                using (MySqlConnection conn = new MySqlConnection(config.ConnectionString)) {

                    bool exists = true;
                    string qry = string.Empty;
                    string dbName = config.Database;

                    //1) Open database connection
                    conn.Open();


                    //2) Insert a diagram record into Diagram table if it does NOT exist
                    int dgId = default(int);
                    try {
                        using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
                        using (ITransaction tr = session.BeginTransaction()) {

                            Diagram dg = session.CreateCriteria<Diagram>()
                                .Add(Restrictions.Eq("DiagramName", "Acupuncture"))
                                .UniqueResult<Diagram>();
                                
                            if (dg == null || dg.Id == default(int)) {

                                Diagram dgTemp = session.CreateCriteria<Diagram>()
                                .Add(Restrictions.Eq("DiagramName", "Physiotherapy"))
                                .UniqueResult<Diagram>();

                                Diagram dgAcupuncture = new Diagram();
                                dgAcupuncture.CreatedBy = "system";
                                dgAcupuncture.CreatedTime = DateTime.Now;
                                dgAcupuncture.DiagramName = "Acupuncture";
                                dgAcupuncture.ImageHeight = 288;
                                dgAcupuncture.ImageWidth = 288;
                                dgAcupuncture.ImageType = ".jpg";
                                dgAcupuncture.IsTestData = false;
                                dgAcupuncture.Image = dgTemp.Image;

                                session.Save(dgAcupuncture);

                                dgId = dgAcupuncture.Id;
                            } else {
                                dgId = dg.Id;
                            }

                            tr.Commit();
                        }
                    } catch (Exception ex) {
                        MessageBox.Show(
                            "Failed to insert the Acupuncture Diagram record.\n\n" + ex.Message,
                            "MySQL Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        Application.Exit();
                        return;
                    }

                    /* Removed @ Feb 07, 2023 -------- These columns have been renamed to THP_EnableMaxTreatmentsPerDay & THP_MaxTreatmentsPerDay
                    //(Mar 17, 2014 --- Add THP_EnableMaxTreatmentsPerDayPerInsurer (bit, not null, true) and THP_MaxTreatmentsPerDayPerInsurer (int, not null)
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'therapytype' AND COLUMN_NAME = 'THP_MaxTreatmentsPerDayPerInsurer';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        //Add new columns
                        qry = "ALTER TABLE therapytype ADD THP_EnableMaxTreatmentsPerDayPerInsurer bit(1) NOT NULL DEFAULT 0 AFTER THP_MinutesPerTreatment;" +
                                "ALTER TABLE therapytype ADD THP_MaxTreatmentsPerDayPerInsurer INT(11) NOT NULL AFTER THP_EnableMaxTreatmentsPerDayPerInsurer;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }*/

                    // April 25, 2014 --- PostalCode varchar(7) NULL after AddressLine2
                    exists = true;
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'patient' AND COLUMN_NAME = 'PostalCode';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        //Add new columns
                        qry = "ALTER TABLE patient ADD PostalCode varchar(7) NULL AFTER AddressLine2;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }


                    //3) Create PointOnAcupuncture table if it does NOT exist
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'pointonacupuncture';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        qry = @"CREATE TABLE `pointonacupuncture` (
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
CONSTRAINT `fk_pointonacupuncture_diagram` FOREIGN KEY (`PAC_DGRId`) REFERENCES `diagram` (`DGR_DGRId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT `fk_pointonacupuncture_acupuncturedetail` FOREIGN KEY (`PAC_TRDId`) REFERENCES `acupuncturedetail` (`ACP_ACPId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    //4) Create AcupunctureFollowUpDetail table if it does NOT exist
                    exists = true;
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'acupuncturefollowupdetail';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        qry = @"CREATE TABLE `acupuncturefollowupdetail` (
`AFD_AFDId` int(11) NOT NULL,
`AFD_FTRId` int(11) NOT NULL,
`AFD_TongueDGRId` int(11) NOT NULL,

`AFD_Pulse` VARCHAR(250) DEFAULT NULL,
`AFD_Tongue` VARCHAR(250) DEFAULT NULL,
`AFD_BloodPressure` VARCHAR(250) DEFAULT NULL,
`AFD_BloodSugar` VARCHAR(250) DEFAULT NULL,
`AFD_Subjective` VARCHAR(1000) DEFAULT NULL,
`AFD_TCMDiagnosis` VARCHAR(250) DEFAULT NULL,
`AFD_TreatmentPlan` TEXT DEFAULT NULL,

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }

                        //For each existing acupuncture followup treatment, insert an acupunctureFollowUpDetail record
                        try {
                            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
                            using (ITransaction tr = session.BeginTransaction()) {

                                var followups = session.CreateCriteria<FollowUpTreatment>("f")
                                    .CreateCriteria("f.InitialTreatment", "it")
                                    .CreateCriteria("it.TreatmentType", "tt")
                                    .Add(Restrictions.Eq("tt.TherapyTypeName", "Acupuncture"))
                                    .SetResultTransformer(Transformers.DistinctRootEntity)
                                    .Future<FollowUpTreatment>();

                                Diagram dg = session.Load<Diagram>(dgId);
                                
                                foreach (FollowUpTreatment followup in followups) {
                                    AcupunctureFollowUpDetail fDetail = new AcupunctureFollowUpDetail();
                                    fDetail.CreatedBy = "system";
                                    fDetail.CreatedTime = DateTime.Now;
                                    fDetail.IsTestData = false;
                                    fDetail.TreatmentPlan = followup.Note;
                                    fDetail.TongueDiagram = dg;
                                    fDetail.FollowUpTreatment = followup;

                                    session.Save(fDetail);
                                }

                                tr.Commit();
                            }
                        } catch (Exception ex) {
                            MessageBox.Show(
                                "Failed to generate acupuncture follow up detail records.\n\n" + ex.Message,
                                "MySQL Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            Application.Exit();
                            return;
                        }

                    }

                    //5) Create PointOnAcupunctureFollowUp table if it does NOT exist
                    exists = true;
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'pointonacupuncturefollowup';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        qry = @"CREATE TABLE `pointonacupuncturefollowup` (
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
CONSTRAINT `fk_pointonacupuncturefollowup_diagram` FOREIGN KEY (`PAF_DGRId`) REFERENCES `diagram` (`DGR_DGRId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT `fk_pointonacupuncturefollowup_acupuncturefollowdetail` FOREIGN KEY (`PAF_AFDId`) REFERENCES `acupuncturefollowupdetail` (`AFD_AFDId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    //6) Add/update columns if the ACP_TreatmentPlan column does NOT exist in table AcupunctureDetail
                    exists = true;
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'acupuncturedetail' AND COLUMN_NAME = 'ACP_TreatmentPlan';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {

                        //Add new columns
                        qry = "ALTER TABLE acupuncturedetail ADD ACP_TongueDGRId INT NOT NULL DEFAULT " + dgId.ToString() + " AFTER ACP_ITRId;" +
                                "ALTER TABLE acupuncturedetail ADD ACP_Pulse VARCHAR(250) NULL AFTER ACP_TongueDGRId;" +
                                "ALTER TABLE acupuncturedetail ADD ACP_Tongue VARCHAR(250) NULL AFTER ACP_Pulse;" +
                                "ALTER TABLE acupuncturedetail ADD ACP_BloodPressure VARCHAR(250) NULL AFTER ACP_Tongue;" +
                                "ALTER TABLE acupuncturedetail ADD ACP_BloodSugar VARCHAR(250) NULL AFTER ACP_BloodPressure;" +
                                "ALTER TABLE acupuncturedetail ADD ACP_Subjective VARCHAR(1000) NULL AFTER ACP_BloodSugar;" +
                                "ALTER TABLE acupuncturedetail ADD ACP_TCMDiagnosis VARCHAR(250) NULL AFTER ACP_Subjective;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }

                        //Add foreign key
                        qry = @"ALTER TABLE acupuncturedetail 
ADD CONSTRAINT `fk_acupuncturedetail_diagram` 
FOREIGN KEY (`ACP_TongueDGRId` ) 
REFERENCES `diagram` (`DGR_DGRId` )  
ON DELETE NO ACTION  ON UPDATE NO ACTION;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }


                        //Rename column from ACP_Note to ACP_TreatmentPlan
                        qry = "ALTER TABLE acupuncturedetail CHANGE COLUMN ACP_Note ACP_TreatmentPlan TEXT NULL AFTER ACP_TCMDiagnosis;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }

                    }

                    // April 24, 2014 -- Replace INV_TRGId column with INV_TherapistRegistration varchar(500) not null
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'invoice' AND COLUMN_NAME = 'INV_TherapistRegistration';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        //Backup Invoice table before making any changes
                        string dt = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        qry = "CREATE TABLE invoices_bk_" + dt + " LIKE invoice; INSERT invoices_bk_" + dt + " SELECT * FROM invoice;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }

                        //Add new columns
                        qry = "ALTER TABLE invoice ADD INV_TherapistRegistration varchar(500) NOT NULL DEFAULT '' AFTER INV_TRGId;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }

                        //Update new column with registration string
                        qry = @"UPDATE invoice inv
	INNER JOIN (SELECT 
					inv_invid, 
					group_concat(concat(TOR_RegistrationNumber, ' - ', org_organizationName) SEPARATOR '\n') registration
				FROM invoice
					INNER JOIN therapistorganizationregistrationgroup on inv_trgid = TRG_TRGId
					INNER JOIN therapistorganizationregistration on TOR_TRGId = trg_trgid
					INNER JOIN organization on TOR_ORGId = ORG_ORGId
				GROUP BY inv_invid) t
	ON inv.inv_invid = t.inv_invid
    SET inv.INV_TherapistRegistration = t.registration";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }

                        //Update new column with registration string for Invoices pointing to the missing registration group
                        qry = @"UPDATE invoice inv
	INNER JOIN(
		SELECT 
			inv_invid, 
			group_concat(concat(TOR_RegistrationNumber, ' - ', org_organizationName) SEPARATOR '\n') registration 
		FROM invoice 
			INNER JOIN user on inv_usrid = usr_usrid
			INNER JOIN therapistorganizationregistrationgroup on trg_usrid = usr_usrid
			INNER JOIN therapistorganizationregistration on TOR_TRGId = trg_trgid
			INNER JOIN organization on TOR_ORGId = ORG_ORGId
		WHERE inv_therapistregistration = ''
		GROUP BY inv_invid
	) t ON inv.inv_invid = t.inv_invid
    SET inv.INV_TherapistRegistration = t.registration;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }

                        //Remove the INV_TRGId column
                        qry = @"ALTER TABLE invoice DROP FOREIGN KEY fk_invoice_registration;
ALTER TABLE invoice DROP COLUMN INV_TRGId, DROP INDEX fk_invoice_registration;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }

                    }

                    // June 17, 2014 ------ Add one row into SystemSetting ("Treatment Note Title") if not exists
                    #region June 17, 2014 ------ Add one row into SystemSetting ("Treatment Note Title") if not exists
                    try {
                        using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
                        using (ITransaction tr = session.BeginTransaction()) {

                            var noteTitle = session.QueryOver<SystemSetting>()
                                .Where(x => x.Name == "Treatment Note Title")
                                .SingleOrDefault<SystemSetting>();

                            if (noteTitle == null) {
                                SystemSetting newSetting = new SystemSetting {
                                    Name = "Treatment Note Title",
                                    Value = "",
                                    CreatedBy = "system",
                                    CreatedTime = DateTime.Now
                                };

                                session.Save(newSetting);
                            }

                            tr.Commit();
                        }
                    } catch (Exception ex) {
                        MessageBox.Show(
                            "Failed to insert Treatment Note Title into SystemSetting table.\n\n" + ex.Message,
                            "MySQL Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        Application.Exit();
                        return;
                    }
                    #endregion

                    // Dec 11, 2014 --- Add UserWeeklyAvailability table
                    #region Dec 11, 2014 --- Add UserWeeklyAvailability table
                    exists = true;
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'userweeklyavailability';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        qry = @"CREATE TABLE `userweeklyavailability` (
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;";

                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion

                    // Dec 11, 2014 --- Add UserTimeOff table
                    #region Dec 11, 2014 --- Add UserTimeOff table
                    exists = true;
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'usertimeoff';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        qry = @"CREATE TABLE `usertimeoff` (
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;";

                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion

                    // Dec 13, 2014 --- Add TemplateTreatmentDetail
                    #region Dec 13, 2014 --- Add TemplateTreatmentDetail
                    exists = true;
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'templatetreatmentdetail';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        qry = @"CREATE TABLE `templatetreatmentdetail` (
  `TTD_TTDId` int(11) NOT NULL,
  `TTD_THPId` int(11) NOT NULL,
  `TTD_IsInitial` bit(1) NOT NULL,
  `TTD_DetailId` int(11) NOT NULL,
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;";

                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion

                    #region Feb 6, 2015 ---- Add OsteopathyDetail and OsteopathyFollowupDetail tables

                    //1 Create OsteopathyDetail table
                    exists = true;
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'osteopathydetail';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        qry = @"CREATE TABLE `osteopathydetail` (
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
  CONSTRAINT `fk_osteopathy_initialTreatment` FOREIGN KEY (`OSP_ITRId`) REFERENCES `initialtreatment` (`ITR_ITRId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_osteopathy_diagram` FOREIGN KEY (`OSP_OsteopathyDGRId`) REFERENCES `diagram` (`DGR_DGRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;";

                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    //2 Create OsteopathyFollowupDetail table
                    exists = true;
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'osteopathyfollowupdetail';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        qry = @"CREATE TABLE `osteopathyfollowupdetail` (
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;";

                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    //3 Create PointOnOsteopathy table
                    exists = true;
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'pointonosteopathy';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        qry = @"CREATE TABLE `pointonosteopathy` (
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
  CONSTRAINT `fk_pointonosteopathy_diagram` FOREIGN KEY (`POS_DGRId`) REFERENCES `diagram` (`DGR_DGRId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_pointonosteopathy_detail` FOREIGN KEY (`POS_TRDId`) REFERENCES `osteopathydetail` (`OSP_OSPId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;";

                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion

                    #region Feb 11, 2015 --- Make Invoice.INV_HSTNumber optional
                    exists = false;
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'invoice' AND COLUMN_NAME = 'INV_HSTNumber' AND IS_NULLABLE = 'NO'";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (exists) {
                        qry = @"ALTER TABLE `invoice` CHANGE COLUMN `INV_HSTNumber` `INV_HSTNumber` VARCHAR(15) NULL  ;";

                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    #endregion Feb 11, 2015 --- Make Invoice.INV_HSTNumber optional

                    #region Mar 5, 2016 ---- Add TreatmentNote to TemplateTreatmentDetail table
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'templatetreatmentdetail' AND COLUMN_NAME = 'TTD_TreatmentNote';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        //Add new columns
                        qry = "ALTER TABLE templatetreatmentdetail ADD TTD_TreatmentNote text NULL AFTER TTD_DetailId;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion Mar 5, 2016 ---- Add TreatmentNote to TemplateTreatmentDetail table

                    #region Mar 5, 2016 ---- Add indexes to table HistoryTracking
                    qry = "SELECT * FROM information_schema.STATISTICS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'historytracking' AND COLUMN_NAME = 'HST_ObjectName' AND INDEX_NAME = 'Index_ObjectName';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        //Add new index
                        qry = "ALTER TABLE historytracking ADD INDEX Index_ObjectName (`HST_ObjectName` ASC)  COMMENT '';";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    qry = "SELECT * FROM information_schema.STATISTICS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'historytracking' AND COLUMN_NAME = 'HST_ObjectId' AND INDEX_NAME = 'Index_ObjectId';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        //Add new index
                        qry = "ALTER TABLE historytracking ADD INDEX Index_ObjectId (`HST_ObjectId` ASC)  COMMENT '';";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    qry = "SELECT * FROM information_schema.STATISTICS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'historytracking' AND COLUMN_NAME = 'HST_ActionTime' AND INDEX_NAME = 'Index_ActionTime';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        //Add new index
                        qry = "ALTER TABLE historytracking ADD INDEX Index_ActionTime (`HST_ActionTime` ASC)  COMMENT '';";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    qry = "SELECT * FROM information_schema.STATISTICS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'historytracking' AND COLUMN_NAME = 'HST_ActionBy' AND INDEX_NAME = 'Index_ActionBy';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        //Add new index
                        qry = "ALTER TABLE historytracking ADD INDEX Index_ActionBy (`HST_ActionBy` ASC)  COMMENT '';";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion Mar 5, 2016 ---- Add indexes to table HistoryTracking

                    // Sep 29, 2016 ------ For Lisa only -- Naturopath table redesigned --------------
                    #region Sep 29, 2016 ------ For Lisa only -- Naturopath table redesigned --------------

                    //Check if the NaturopathicDetail table is updated
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'naturopathicdetail' AND COLUMN_NAME = 'NPC_HealthState';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    //Update NaturopathicDetail table if needed
                    if (!exists) {

                        //Check if the table is empty
                        qry = "SELECT * FROM naturopathicdetail LIMIT 1;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                        using (MySqlDataReader reader = cmd.ExecuteReader()) {
                            exists = reader.HasRows;
                        }
                        if (exists) {
                            MessageBox.Show(
                                "The system was trying to update the NaturopathicDetail table, but failed as the table is not empty. Please contact the developer.",
                                "Failed to update NaturopathicDetail table",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            Application.Exit();
                            return;
                        } else {
                            //Update NaturopathicDetail table
                            //1) remove all columns except NPC_NPCId, NPC_ITRId, NPC_IsTestData, NPC_CreatedTime, NPC_UpdatedTime, NPC_CreatedBy, NPC_UpdatedBy, NPC_Version
                            qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'naturopathicdetail' AND COLUMN_NAME = 'NPC_VisitPurposeSpecificConcernDescription';";
                            using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                            using (MySqlDataReader reader = cmd.ExecuteReader()) {
                                exists = reader.HasRows;
                            }
                            if (exists) {
                                qry = "ALTER TABLE naturopathicdetail DROP COLUMN `NPC_VisitPurposeSpecificConcernDescription` , DROP COLUMN `NPC_VisitPurposeSpecificConcern` , DROP COLUMN `NPC_VisitPurposeGeneral` , DROP COLUMN `NPC_SeenNaturopathicDoctorDate` , DROP COLUMN `NPC_SeenNaturopathicDoctorBefore` , DROP COLUMN `NPC_Diarrhea` , DROP COLUMN `NPC_PMS` , DROP COLUMN `NPC_Seizures` , DROP COLUMN `NPC_Dizziness` , DROP COLUMN `NPC_Fatigue` , DROP COLUMN `NPC_JointPain` , DROP COLUMN `NPC_Anemia` , DROP COLUMN `NPC_PoorSleep` , DROP COLUMN `NPC_HeartDisease` , DROP COLUMN `NPC_Depression` , DROP COLUMN `NPC_Allergies` , DROP COLUMN `NPC_EasyBruising` , DROP COLUMN `NPC_HeavyPeriods` , DROP COLUMN `NPC_HotFlushes` , DROP COLUMN `NPC_Arthritis` , DROP COLUMN `NPC_ProstateSymptoms` , DROP COLUMN `NPC_BreastPain` , DROP COLUMN `NPC_BreastLump` , DROP COLUMN `NPC_Emphysema` , DROP COLUMN `NPC_Bronchitis` , DROP COLUMN `NPC_Asthma` , DROP COLUMN `NPC_ShortBreath` , DROP COLUMN `NPC_ChestPain` , DROP COLUMN `NPC_Wheeze` , DROP COLUMN `NPC_Cough` , DROP COLUMN `NPC_NoseBleeds` , DROP COLUMN `NPC_NeckPain` , DROP COLUMN `NPC_Headaches` , DROP COLUMN `NPC_ThyroidProblems` , DROP COLUMN `NPC_ColdHandsFeet` , DROP COLUMN `NPC_ThirstChanges` , DROP COLUMN `NPC_AppetiteChagnes` , DROP COLUMN `NPC_AnkleSwelling` , DROP COLUMN `NPC_HeartPalpitations` , DROP COLUMN `NPC_SinusProblems` , DROP COLUMN `NPC_NeckStiffness` , DROP COLUMN `NPC_WeightGainLoss` , DROP COLUMN `NPC_SwollenNeckGlands` , DROP COLUMN `NPC_RectalBleeding` , DROP COLUMN `NPC_MuscleWeakness` , DROP COLUMN `NPC_Constipation` , DROP COLUMN `NPC_MouthDryness` , DROP COLUMN `NPC_Anxiety` , DROP COLUMN `NPC_Heartburn` , DROP COLUMN `NPC_Cataracts` , DROP COLUMN `NPC_Glaucoma` , DROP COLUMN `NPC_EyeDischarge` , DROP COLUMN `NPC_EyeDryness` , DROP COLUMN `NPC_PoorVision` , DROP COLUMN `NPC_BloodInUrine` , DROP COLUMN `NPC_PainUrinating` , DROP COLUMN `NPC_NightSweats` , DROP COLUMN `NPC_Psoriasis` , DROP COLUMN `NPC_DrySkin` , DROP COLUMN `NPC_Eczema` , DROP COLUMN `NPC_Acne` , DROP COLUMN `NPC_Sex` , DROP COLUMN `NPC_Weight` , DROP COLUMN `NPC_Height` ;";
                                using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            //2) add new columns
                            qry = @"ALTER TABLE naturopathicdetail 
ADD COLUMN NPC_HealthState INT NULL AFTER NPC_ITRId, 
ADD COLUMN NPC_CurrentEnergyLevel INT UNSIGNED NULL AFTER NPC_HealthState, 
ADD COLUMN NPC_MorningEnergyLevel INT UNSIGNED NULL AFTER NPC_CurrentEnergyLevel, 
ADD COLUMN NPC_CurrentWeight VARCHAR(10) NULL AFTER NPC_MorningEnergyLevel, 
ADD COLUMN NPC_YearAgoWeight VARCHAR(10) NULL AFTER NPC_CurrentWeight, 
ADD COLUMN NPC_IdealWeight VARCHAR(10) NULL AFTER NPC_YearAgoWeight, 
ADD COLUMN NPC_CurrentHeight VARCHAR(10) NULL AFTER NPC_IdealWeight, 
ADD COLUMN NPC_CurrentStressLevel INT UNSIGNED NULL AFTER NPC_CurrentHeight, 
ADD COLUMN NPC_StressIdentifiedBy INT NULL AFTER NPC_CurrentStressLevel, 
ADD COLUMN NPC_StressedBehavior VARCHAR(255) NULL AFTER NPC_StressIdentifiedBy, 
ADD COLUMN NPC_StressLocalizedOfBody VARCHAR(255) NULL AFTER NPC_StressedBehavior, 
ADD COLUMN NPC_StressHandlingTools VARCHAR(255) NULL AFTER NPC_StressLocalizedOfBody, 
ADD COLUMN NPC_HasDrugAllergy BIT NULL AFTER NPC_StressHandlingTools, 
ADD COLUMN NPC_DrugAllergies VARCHAR(255) NULL AFTER NPC_HasDrugAllergy, 
ADD COLUMN NPC_HasFoodAllergy BIT NULL AFTER NPC_DrugAllergies, 
ADD COLUMN NPC_FoodAllergies VARCHAR(255) NULL AFTER NPC_HasFoodAllergy, 
ADD COLUMN NPC_HasAnimalAllergy BIT NULL AFTER NPC_FoodAllergies, 
ADD COLUMN NPC_AnimalAllergies VARCHAR(255) NULL AFTER NPC_HasAnimalAllergy, 
ADD COLUMN NPC_HasPlantAllergy BIT NULL AFTER NPC_AnimalAllergies, 
ADD COLUMN NPC_PlantAllergies VARCHAR(255) NULL AFTER NPC_HasPlantAllergy, 
ADD COLUMN NPC_HasEnvironmentalAllergy BIT NULL AFTER NPC_PlantAllergies, 
ADD COLUMN NPC_EnvironmentalAllergies VARCHAR(255) NULL AFTER NPC_HasEnvironmentalAllergy, 
ADD COLUMN NPC_MajorInjuries VARCHAR(255) NULL AFTER NPC_EnvironmentalAllergies, 
ADD COLUMN NPC_Surgeries VARCHAR(255) NULL AFTER NPC_MajorInjuries, 
ADD COLUMN NPC_HadFluVaccineLastFiveYears BIT NULL AFTER NPC_Surgeries, 
ADD COLUMN NPC_HadReactionToVaccine BIT NULL AFTER NPC_HadFluVaccineLastFiveYears, 
ADD COLUMN NPC_C_Allergies BIT NULL AFTER NPC_HadReactionToVaccine, 
ADD COLUMN NPC_P_Allergies BIT NULL AFTER NPC_C_Allergies, 
ADD COLUMN NPC_C_Anemia BIT NULL AFTER NPC_P_Allergies, 
ADD COLUMN NPC_P_Anemia BIT NULL AFTER NPC_C_Anemia, 
ADD COLUMN NPC_C_Stroke BIT NULL AFTER NPC_P_Anemia, 
ADD COLUMN NPC_P_Stroke BIT NULL AFTER NPC_C_Stroke, 
ADD COLUMN NPC_C_Depression BIT NULL AFTER NPC_P_Stroke, 
ADD COLUMN NPC_P_Depression BIT NULL AFTER NPC_C_Depression, 
ADD COLUMN NPC_C_Asthma BIT NULL AFTER NPC_P_Depression, 
ADD COLUMN NPC_P_Asthma BIT NULL AFTER NPC_C_Asthma, 
ADD COLUMN NPC_C_Measles BIT NULL AFTER NPC_P_Asthma, 
ADD COLUMN NPC_P_Measles BIT NULL AFTER NPC_C_Measles, 
ADD COLUMN NPC_C_HeartDisease BIT NULL AFTER NPC_P_Measles, 
ADD COLUMN NPC_P_HeartDisease BIT NULL AFTER NPC_C_HeartDisease, 
ADD COLUMN NPC_C_EmotionalAbuse BIT NULL AFTER NPC_P_HeartDisease, 
ADD COLUMN NPC_P_EmotionalAbuse BIT NULL AFTER NPC_C_EmotionalAbuse, 
ADD COLUMN NPC_C_Eczema BIT NULL AFTER NPC_P_EmotionalAbuse, 
ADD COLUMN NPC_P_Eczema BIT NULL AFTER NPC_C_Eczema, 
ADD COLUMN NPC_C_Mumps BIT NULL AFTER NPC_P_Eczema, 
ADD COLUMN NPC_P_Mumps BIT NULL AFTER NPC_C_Mumps, 
ADD COLUMN NPC_C_RheumaticFever BIT NULL AFTER NPC_P_Mumps, 
ADD COLUMN NPC_P_RheumaticFever BIT NULL AFTER NPC_C_RheumaticFever, 
ADD COLUMN NPC_C_PhysicalMentalAbuse BIT NULL AFTER NPC_P_RheumaticFever, 
ADD COLUMN NPC_P_PhysicalMentalAbuse BIT NULL AFTER NPC_C_PhysicalMentalAbuse, 
ADD COLUMN NPC_C_Psoriasis BIT NULL AFTER NPC_P_PhysicalMentalAbuse, 
ADD COLUMN NPC_P_Psoriasis BIT NULL AFTER NPC_C_Psoriasis, 
ADD COLUMN NPC_C_ChickenPox BIT NULL AFTER NPC_P_Psoriasis, 
ADD COLUMN NPC_P_ChickenPox BIT NULL AFTER NPC_C_ChickenPox, 
ADD COLUMN NPC_C_HighBloodPressure BIT NULL AFTER NPC_P_ChickenPox, 
ADD COLUMN NPC_P_HighBloodPressure BIT NULL AFTER NPC_C_HighBloodPressure, 
ADD COLUMN NPC_C_NumbnessTingling BIT NULL AFTER NPC_P_HighBloodPressure, 
ADD COLUMN NPC_P_NumbnessTingling BIT NULL AFTER NPC_C_NumbnessTingling, 
ADD COLUMN NPC_C_HayFever BIT NULL AFTER NPC_P_NumbnessTingling, 
ADD COLUMN NPC_P_HayFever BIT NULL AFTER NPC_C_HayFever, 
ADD COLUMN NPC_C_WhoopingCough BIT NULL AFTER NPC_P_HayFever, 
ADD COLUMN NPC_P_WhoopingCough BIT NULL AFTER NPC_C_WhoopingCough, 
ADD COLUMN NPC_C_HighCholesterol BIT NULL AFTER NPC_P_WhoopingCough, 
ADD COLUMN NPC_P_HighCholesterol BIT NULL AFTER NPC_C_HighCholesterol, 
ADD COLUMN NPC_C_ColdHandsFeet BIT NULL AFTER NPC_P_HighCholesterol, 
ADD COLUMN NPC_P_ColdHandsFeet BIT NULL AFTER NPC_C_ColdHandsFeet, 
ADD COLUMN NPC_C_Pneumonia BIT NULL AFTER NPC_P_ColdHandsFeet, 
ADD COLUMN NPC_P_Pneumonia BIT NULL AFTER NPC_C_Pneumonia, 
ADD COLUMN NPC_C_Shingles BIT NULL AFTER NPC_P_Pneumonia, 
ADD COLUMN NPC_P_Shingles BIT NULL AFTER NPC_C_Shingles, 
ADD COLUMN NPC_C_Cancer BIT NULL AFTER NPC_P_Shingles, 
ADD COLUMN NPC_P_Cancer BIT NULL AFTER NPC_C_Cancer, 
ADD COLUMN NPC_C_ThyroidProblems BIT NULL AFTER NPC_P_Cancer, 
ADD COLUMN NPC_P_ThyroidProblems BIT NULL AFTER NPC_C_ThyroidProblems, 
ADD COLUMN NPC_C_EarInfections BIT NULL AFTER NPC_P_ThyroidProblems, 
ADD COLUMN NPC_P_EarInfections BIT NULL AFTER NPC_C_EarInfections, 
ADD COLUMN NPC_C_Diphtheria BIT NULL AFTER NPC_P_EarInfections, 
ADD COLUMN NPC_P_Diphtheria BIT NULL AFTER NPC_C_Diphtheria, 
ADD COLUMN NPC_C_Type1Diabetes BIT NULL AFTER NPC_P_Diphtheria, 
ADD COLUMN NPC_P_Type1Diabetes BIT NULL AFTER NPC_C_Type1Diabetes, 
ADD COLUMN NPC_C_Warts BIT NULL AFTER NPC_P_Type1Diabetes, 
ADD COLUMN NPC_P_Warts BIT NULL AFTER NPC_C_Warts, 
ADD COLUMN NPC_C_StrepThroat BIT NULL AFTER NPC_P_Warts, 
ADD COLUMN NPC_P_StrepThroat BIT NULL AFTER NPC_C_StrepThroat, 
ADD COLUMN NPC_C_ScarletFever BIT NULL AFTER NPC_P_StrepThroat, 
ADD COLUMN NPC_P_ScarletFever BIT NULL AFTER NPC_C_ScarletFever, 
ADD COLUMN NPC_C_Type2Diabetes BIT NULL AFTER NPC_P_ScarletFever, 
ADD COLUMN NPC_P_Type2Diabetes BIT NULL AFTER NPC_C_Type2Diabetes, 
ADD COLUMN NPC_C_Mono BIT NULL AFTER NPC_P_Type2Diabetes, 
ADD COLUMN NPC_P_Mono BIT NULL AFTER NPC_C_Mono, 
ADD COLUMN NPC_C_Tonsillitis BIT NULL AFTER NPC_P_Mono, 
ADD COLUMN NPC_P_Tonsillitis BIT NULL AFTER NPC_C_Tonsillitis, 
ADD COLUMN NPC_C_Polio BIT NULL AFTER NPC_P_Tonsillitis, 
ADD COLUMN NPC_P_Polio BIT NULL AFTER NPC_C_Polio, 
ADD COLUMN NPC_C_KidneyDisease BIT NULL AFTER NPC_P_Polio, 
ADD COLUMN NPC_P_KidneyDisease BIT NULL AFTER NPC_C_KidneyDisease, 
ADD COLUMN NPC_C_RheumatoidArthritis BIT NULL AFTER NPC_P_KidneyDisease, 
ADD COLUMN NPC_P_RheumatoidArthritis BIT NULL AFTER NPC_C_RheumatoidArthritis, 
ADD COLUMN NPC_C_CankerSores BIT NULL AFTER NPC_P_RheumatoidArthritis, 
ADD COLUMN NPC_P_CankerSores BIT NULL AFTER NPC_C_CankerSores, 
ADD COLUMN NPC_C_Smallpox BIT NULL AFTER NPC_P_CankerSores, 
ADD COLUMN NPC_P_Smallpox BIT NULL AFTER NPC_C_Smallpox, 
ADD COLUMN NPC_C_VisualProblems BIT NULL AFTER NPC_P_Smallpox, 
ADD COLUMN NPC_P_VisualProblems BIT NULL AFTER NPC_C_VisualProblems, 
ADD COLUMN NPC_C_Osteoarthritis BIT NULL AFTER NPC_P_VisualProblems, 
ADD COLUMN NPC_P_Osteoarthritis BIT NULL AFTER NPC_C_Osteoarthritis, 
ADD COLUMN NPC_C_Jaundice BIT NULL AFTER NPC_P_Osteoarthritis, 
ADD COLUMN NPC_P_Jaundice BIT NULL AFTER NPC_C_Jaundice, 
ADD COLUMN NPC_C_Tuberculosis BIT NULL AFTER NPC_P_Jaundice, 
ADD COLUMN NPC_P_Tuberculosis BIT NULL AFTER NPC_C_Tuberculosis, 
ADD COLUMN NPC_C_AutoimmuneDisease BIT NULL AFTER NPC_P_Tuberculosis, 
ADD COLUMN NPC_P_AutoimmuneDisease BIT NULL AFTER NPC_C_AutoimmuneDisease, 
ADD COLUMN NPC_C_WeightProblems BIT NULL AFTER NPC_P_AutoimmuneDisease, 
ADD COLUMN NPC_P_WeightProblems BIT NULL AFTER NPC_C_WeightProblems, 
ADD COLUMN NPC_C_Alcoholism BIT NULL AFTER NPC_P_WeightProblems, 
ADD COLUMN NPC_P_Alcoholism BIT NULL AFTER NPC_C_Alcoholism, 
ADD COLUMN NPC_C_Malaria BIT NULL AFTER NPC_P_Alcoholism, 
ADD COLUMN NPC_P_Malaria BIT NULL AFTER NPC_C_Malaria, 
ADD COLUMN NPC_C_Epilepsy BIT NULL AFTER NPC_P_Malaria, 
ADD COLUMN NPC_P_Epilepsy BIT NULL AFTER NPC_C_Epilepsy, 
ADD COLUMN NPC_C_Gout BIT NULL AFTER NPC_P_Epilepsy, 
ADD COLUMN NPC_P_Gout BIT NULL AFTER NPC_C_Gout, 
ADD COLUMN NPC_C_Hepatitis BIT NULL AFTER NPC_P_Gout, 
ADD COLUMN NPC_P_Hepatitis BIT NULL AFTER NPC_C_Hepatitis, 
ADD COLUMN NPC_C_Gallstones BIT NULL AFTER NPC_P_Hepatitis, 
ADD COLUMN NPC_P_Gallstones BIT NULL AFTER NPC_C_Gallstones, 
ADD COLUMN NPC_C_VaricoseVeins BIT NULL AFTER NPC_P_Gallstones, 
ADD COLUMN NPC_P_VaricoseVeins BIT NULL AFTER NPC_C_VaricoseVeins, 
ADD COLUMN NPC_CP_Other VARCHAR(255) NULL AFTER NPC_P_VaricoseVeins, 
ADD COLUMN NPC_CP_NeverWellSince VARCHAR(255) NULL AFTER NPC_CP_Other, 
ADD COLUMN NPC_CU_Alcohol VARCHAR(50) NULL AFTER NPC_CP_NeverWellSince, 
ADD COLUMN NPC_CU_Hormones VARCHAR(50) NULL AFTER NPC_CU_Alcohol, 
ADD COLUMN NPC_CU_Cortisone VARCHAR(50) NULL AFTER NPC_CU_Hormones, 
ADD COLUMN NPC_CU_Sedatives VARCHAR(50) NULL AFTER NPC_CU_Cortisone, 
ADD COLUMN NPC_CU_Antacids VARCHAR(50) NULL AFTER NPC_CU_Sedatives, 
ADD COLUMN NPC_CU_Tobacco VARCHAR(50) NULL AFTER NPC_CU_Antacids, 
ADD COLUMN NPC_CU_Coffee VARCHAR(50) NULL AFTER NPC_CU_Tobacco, 
ADD COLUMN NPC_CU_BlackTea VARCHAR(50) NULL AFTER NPC_CU_Coffee, 
ADD COLUMN NPC_CU_Laxatives VARCHAR(50) NULL AFTER NPC_CU_BlackTea, 
ADD COLUMN NPC_CPM_Name1 VARCHAR(50) NULL AFTER NPC_CU_Laxatives, 
ADD COLUMN NPC_CPM_Reason1 VARCHAR(50) NULL AFTER NPC_CPM_Name1, 
ADD COLUMN NPC_CPM_Amount1 VARCHAR(50) NULL AFTER NPC_CPM_Reason1, 
ADD COLUMN NPC_CPM_PrescriptionDate1 VARCHAR(50) NULL AFTER NPC_CPM_Amount1, 
ADD COLUMN NPC_CPM_DrugCategory1 VARCHAR(50) NULL AFTER NPC_CPM_PrescriptionDate1, 
ADD COLUMN NPC_CPM_Name2 VARCHAR(50) NULL AFTER NPC_CPM_DrugCategory1, 
ADD COLUMN NPC_CPM_Reason2 VARCHAR(50) NULL AFTER NPC_CPM_Name2, 
ADD COLUMN NPC_CPM_Amount2 VARCHAR(50) NULL AFTER NPC_CPM_Reason2, 
ADD COLUMN NPC_CPM_PrescriptionDate2 VARCHAR(50) NULL AFTER NPC_CPM_Amount2, 
ADD COLUMN NPC_CPM_DrugCategory2 VARCHAR(50) NULL AFTER NPC_CPM_PrescriptionDate2, 
ADD COLUMN NPC_CPM_Name3 VARCHAR(50) NULL AFTER NPC_CPM_DrugCategory2, 
ADD COLUMN NPC_CPM_Reason3 VARCHAR(50) NULL AFTER NPC_CPM_Name3, 
ADD COLUMN NPC_CPM_Amount3 VARCHAR(50) NULL AFTER NPC_CPM_Reason3, 
ADD COLUMN NPC_CPM_PrescriptionDate3 VARCHAR(50) NULL AFTER NPC_CPM_Amount3, 
ADD COLUMN NPC_CPM_DrugCategory3 VARCHAR(50) NULL AFTER NPC_CPM_PrescriptionDate3, 
ADD COLUMN NPC_CPM_Name4 VARCHAR(50) NULL AFTER NPC_CPM_DrugCategory3, 
ADD COLUMN NPC_CPM_Reason4 VARCHAR(50) NULL AFTER NPC_CPM_Name4, 
ADD COLUMN NPC_CPM_Amount4 VARCHAR(50) NULL AFTER NPC_CPM_Reason4, 
ADD COLUMN NPC_CPM_PrescriptionDate4 VARCHAR(50) NULL AFTER NPC_CPM_Amount4, 
ADD COLUMN NPC_CPM_DrugCategory4 VARCHAR(50) NULL AFTER NPC_CPM_PrescriptionDate4, 
ADD COLUMN NPC_CPM_Name5 VARCHAR(50) NULL AFTER NPC_CPM_DrugCategory4, 
ADD COLUMN NPC_CPM_Reason5 VARCHAR(50) NULL AFTER NPC_CPM_Name5, 
ADD COLUMN NPC_CPM_Amount5 VARCHAR(50) NULL AFTER NPC_CPM_Reason5, 
ADD COLUMN NPC_CPM_PrescriptionDate5 VARCHAR(50) NULL AFTER NPC_CPM_Amount5, 
ADD COLUMN NPC_CPM_DrugCategory5 VARCHAR(50) NULL AFTER NPC_CPM_PrescriptionDate5, 
ADD COLUMN NPC_VH_Name1 VARCHAR(50) NULL AFTER NPC_CPM_DrugCategory5, 
ADD COLUMN NPC_VH_Brand1 VARCHAR(50) NULL AFTER NPC_VH_Name1, 
ADD COLUMN NPC_VH_Amount1 VARCHAR(50) NULL AFTER NPC_VH_Brand1, 
ADD COLUMN NPC_VH_Reason1 VARCHAR(50) NULL AFTER NPC_VH_Amount1, 
ADD COLUMN NPC_VH_SuggestedBy1 INT NULL AFTER NPC_VH_Reason1, 
ADD COLUMN NPC_VH_Helped1 INT NULL AFTER NPC_VH_SuggestedBy1, 
ADD COLUMN NPC_VH_Name2 VARCHAR(50) NULL AFTER NPC_VH_Helped1, 
ADD COLUMN NPC_VH_Brand2 VARCHAR(50) NULL AFTER NPC_VH_Name2, 
ADD COLUMN NPC_VH_Amount2 VARCHAR(50) NULL AFTER NPC_VH_Brand2, 
ADD COLUMN NPC_VH_Reason2 VARCHAR(50) NULL AFTER NPC_VH_Amount2, 
ADD COLUMN NPC_VH_SuggestedBy2 INT NULL AFTER NPC_VH_Reason2, 
ADD COLUMN NPC_VH_Helped2 INT NULL AFTER NPC_VH_SuggestedBy2, 
ADD COLUMN NPC_VH_Name3 VARCHAR(50) NULL AFTER NPC_VH_Helped2, 
ADD COLUMN NPC_VH_Brand3 VARCHAR(50) NULL AFTER NPC_VH_Name3, 
ADD COLUMN NPC_VH_Amount3 VARCHAR(50) NULL AFTER NPC_VH_Brand3, 
ADD COLUMN NPC_VH_Reason3 VARCHAR(50) NULL AFTER NPC_VH_Amount3, 
ADD COLUMN NPC_VH_SuggestedBy3 INT NULL AFTER NPC_VH_Reason3, 
ADD COLUMN NPC_VH_Helped3 INT NULL AFTER NPC_VH_SuggestedBy3, 
ADD COLUMN NPC_VH_Name4 VARCHAR(50) NULL AFTER NPC_VH_Helped3, 
ADD COLUMN NPC_VH_Brand4 VARCHAR(50) NULL AFTER NPC_VH_Name4, 
ADD COLUMN NPC_VH_Amount4 VARCHAR(50) NULL AFTER NPC_VH_Brand4, 
ADD COLUMN NPC_VH_Reason4 VARCHAR(50) NULL AFTER NPC_VH_Amount4, 
ADD COLUMN NPC_VH_SuggestedBy4 INT NULL AFTER NPC_VH_Reason4, 
ADD COLUMN NPC_VH_Helped4 INT NULL AFTER NPC_VH_SuggestedBy4, 
ADD COLUMN NPC_VH_Name5 VARCHAR(50) NULL AFTER NPC_VH_Helped4, 
ADD COLUMN NPC_VH_Brand5 VARCHAR(50) NULL AFTER NPC_VH_Name5, 
ADD COLUMN NPC_VH_Amount5 VARCHAR(50) NULL AFTER NPC_VH_Brand5, 
ADD COLUMN NPC_VH_Reason5 VARCHAR(50) NULL AFTER NPC_VH_Amount5, 
ADD COLUMN NPC_VH_SuggestedBy5 INT NULL AFTER NPC_VH_Reason5, 
ADD COLUMN NPC_VH_Helped5 INT NULL AFTER NPC_VH_SuggestedBy5, 
ADD COLUMN NPC_VH_HasOtherSupplementation BIT NULL AFTER NPC_VH_Helped5, 
ADD COLUMN NPC_VH_OtherSupplementation VARCHAR(255) NULL AFTER NPC_VH_HasOtherSupplementation, 
ADD COLUMN NPC_FH_CancerMother VARCHAR(50) NULL AFTER NPC_VH_OtherSupplementation, 
ADD COLUMN NPC_FH_CancerFather VARCHAR(50) NULL AFTER NPC_FH_CancerMother, 
ADD COLUMN NPC_FH_CancerSibling VARCHAR(50) NULL AFTER NPC_FH_CancerFather, 
ADD COLUMN NPC_FH_CancerGrandparent VARCHAR(50) NULL AFTER NPC_FH_CancerSibling, 
ADD COLUMN NPC_FH_TuberculosisMother VARCHAR(50) NULL AFTER NPC_FH_CancerGrandparent, 
ADD COLUMN NPC_FH_TuberculosisFather VARCHAR(50) NULL AFTER NPC_FH_TuberculosisMother, 
ADD COLUMN NPC_FH_TuberculosisSibling VARCHAR(50) NULL AFTER NPC_FH_TuberculosisFather, 
ADD COLUMN NPC_FH_TuberculosisGrandparent VARCHAR(50) NULL AFTER NPC_FH_TuberculosisSibling, 
ADD COLUMN NPC_FH_HeartDiseaseMother VARCHAR(50) NULL AFTER NPC_FH_TuberculosisGrandparent, 
ADD COLUMN NPC_FH_HeartDiseaseFather VARCHAR(50) NULL AFTER NPC_FH_HeartDiseaseMother, 
ADD COLUMN NPC_FH_HeartDiseaseSibling VARCHAR(50) NULL AFTER NPC_FH_HeartDiseaseFather, 
ADD COLUMN NPC_FH_HeartDiseaseGrandparent VARCHAR(50) NULL AFTER NPC_FH_HeartDiseaseSibling, 
ADD COLUMN NPC_FH_StrokeMother VARCHAR(50) NULL AFTER NPC_FH_HeartDiseaseGrandparent, 
ADD COLUMN NPC_FH_StrokeFather VARCHAR(50) NULL AFTER NPC_FH_StrokeMother, 
ADD COLUMN NPC_FH_StrokeSibling VARCHAR(50) NULL AFTER NPC_FH_StrokeFather, 
ADD COLUMN NPC_FH_StrokeGrandparent VARCHAR(50) NULL AFTER NPC_FH_StrokeSibling, 
ADD COLUMN NPC_FH_HighBloodPressureMother VARCHAR(50) NULL AFTER NPC_FH_StrokeGrandparent, 
ADD COLUMN NPC_FH_HighBloodPressureFather VARCHAR(50) NULL AFTER NPC_FH_HighBloodPressureMother, 
ADD COLUMN NPC_FH_HighBloodPressureSibling VARCHAR(50) NULL AFTER NPC_FH_HighBloodPressureFather, 
ADD COLUMN NPC_FH_HighBloodPressureGrandparent VARCHAR(50) NULL AFTER NPC_FH_HighBloodPressureSibling, 
ADD COLUMN NPC_FH_HighCholesterolMother VARCHAR(50) NULL AFTER NPC_FH_HighBloodPressureGrandparent, 
ADD COLUMN NPC_FH_HighCholesterolFather VARCHAR(50) NULL AFTER NPC_FH_HighCholesterolMother, 
ADD COLUMN NPC_FH_HighCholesterolSibling VARCHAR(50) NULL AFTER NPC_FH_HighCholesterolFather, 
ADD COLUMN NPC_FH_HighCholesterolGrandparent VARCHAR(50) NULL AFTER NPC_FH_HighCholesterolSibling, 
ADD COLUMN NPC_FH_KidneyDiseaseMother VARCHAR(50) NULL AFTER NPC_FH_HighCholesterolGrandparent, 
ADD COLUMN NPC_FH_KidneyDiseaseFather VARCHAR(50) NULL AFTER NPC_FH_KidneyDiseaseMother, 
ADD COLUMN NPC_FH_KidneyDiseaseSibling VARCHAR(50) NULL AFTER NPC_FH_KidneyDiseaseFather, 
ADD COLUMN NPC_FH_KidneyDiseaseGrandparent VARCHAR(50) NULL AFTER NPC_FH_KidneyDiseaseSibling, 
ADD COLUMN NPC_FH_RheumatoidMother VARCHAR(50) NULL AFTER NPC_FH_KidneyDiseaseGrandparent, 
ADD COLUMN NPC_FH_RheumatoidFather VARCHAR(50) NULL AFTER NPC_FH_RheumatoidMother, 
ADD COLUMN NPC_FH_RheumatoidSibling VARCHAR(50) NULL AFTER NPC_FH_RheumatoidFather, 
ADD COLUMN NPC_FH_RheumatoidGrandparent VARCHAR(50) NULL AFTER NPC_FH_RheumatoidSibling, 
ADD COLUMN NPC_FH_OsteoarthritisMother VARCHAR(50) NULL AFTER NPC_FH_RheumatoidGrandparent, 
ADD COLUMN NPC_FH_OsteoarthritisFather VARCHAR(50) NULL AFTER NPC_FH_OsteoarthritisMother, 
ADD COLUMN NPC_FH_OsteoarthritisSibling VARCHAR(50) NULL AFTER NPC_FH_OsteoarthritisFather, 
ADD COLUMN NPC_FH_OsteoarthritisGrandparent VARCHAR(50) NULL AFTER NPC_FH_OsteoarthritisSibling, 
ADD COLUMN NPC_FH_AllergiesMother VARCHAR(50) NULL AFTER NPC_FH_OsteoarthritisGrandparent, 
ADD COLUMN NPC_FH_AllergiesFather VARCHAR(50) NULL AFTER NPC_FH_AllergiesMother, 
ADD COLUMN NPC_FH_AllergiesSibling VARCHAR(50) NULL AFTER NPC_FH_AllergiesFather, 
ADD COLUMN NPC_FH_AllergiesGrandparent VARCHAR(50) NULL AFTER NPC_FH_AllergiesSibling, 
ADD COLUMN NPC_FH_AsthmaMother VARCHAR(50) NULL AFTER NPC_FH_AllergiesGrandparent, 
ADD COLUMN NPC_FH_AsthmaFather VARCHAR(50) NULL AFTER NPC_FH_AsthmaMother, 
ADD COLUMN NPC_FH_AsthmaSibling VARCHAR(50) NULL AFTER NPC_FH_AsthmaFather, 
ADD COLUMN NPC_FH_AsthmaGrandparent VARCHAR(50) NULL AFTER NPC_FH_AsthmaSibling, 
ADD COLUMN NPC_FH_DiabetesTypeIMother VARCHAR(50) NULL AFTER NPC_FH_AsthmaGrandparent, 
ADD COLUMN NPC_FH_DiabetesTypeIFather VARCHAR(50) NULL AFTER NPC_FH_DiabetesTypeIMother, 
ADD COLUMN NPC_FH_DiabetesTypeISibling VARCHAR(50) NULL AFTER NPC_FH_DiabetesTypeIFather, 
ADD COLUMN NPC_FH_DiabetesTypeIGrandparent VARCHAR(50) NULL AFTER NPC_FH_DiabetesTypeISibling, 
ADD COLUMN NPC_FH_DiabetesTypeIIMother VARCHAR(50) NULL AFTER NPC_FH_DiabetesTypeIGrandparent, 
ADD COLUMN NPC_FH_DiabetesTypeIIFather VARCHAR(50) NULL AFTER NPC_FH_DiabetesTypeIIMother, 
ADD COLUMN NPC_FH_DiabetesTypeIISibling VARCHAR(50) NULL AFTER NPC_FH_DiabetesTypeIIFather, 
ADD COLUMN NPC_FH_DiabetesTypeIIGrandparent VARCHAR(50) NULL AFTER NPC_FH_DiabetesTypeIISibling, 
ADD COLUMN NPC_FH_DepressionMother VARCHAR(50) NULL AFTER NPC_FH_DiabetesTypeIIGrandparent, 
ADD COLUMN NPC_FH_DepressionFather VARCHAR(50) NULL AFTER NPC_FH_DepressionMother, 
ADD COLUMN NPC_FH_DepressionSibling VARCHAR(50) NULL AFTER NPC_FH_DepressionFather, 
ADD COLUMN NPC_FH_DepressionGrandparent VARCHAR(50) NULL AFTER NPC_FH_DepressionSibling, 
ADD COLUMN NPC_FH_OtherName VARCHAR(50) NULL AFTER NPC_FH_DepressionGrandparent, 
ADD COLUMN NPC_FH_OtherMother VARCHAR(50) NULL AFTER NPC_FH_OtherName, 
ADD COLUMN NPC_FH_OtherFather VARCHAR(50) NULL AFTER NPC_FH_OtherMother, 
ADD COLUMN NPC_FH_OtherSibling VARCHAR(50) NULL AFTER NPC_FH_OtherFather, 
ADD COLUMN NPC_FH_OtherGrandparent VARCHAR(50) NULL AFTER NPC_FH_OtherSibling, 
ADD COLUMN NPC_PH_EnjoyMost VARCHAR(255) NULL AFTER NPC_FH_OtherGrandparent, 
ADD COLUMN NPC_PH_MainInterests VARCHAR(255) NULL AFTER NPC_PH_EnjoyMost, 
ADD COLUMN NPC_PH_WorryMost VARCHAR(255) NULL AFTER NPC_PH_MainInterests, 
ADD COLUMN NPC_PH_WhatNurtures VARCHAR(255) NULL AFTER NPC_PH_WorryMost, 
ADD COLUMN NPC_PH_DoExercise BIT NULL AFTER NPC_PH_WhatNurtures, 
ADD COLUMN NPC_PH_ExerciseDetails VARCHAR(255) NULL AFTER NPC_PH_DoExercise, 
ADD COLUMN NPC_PH_ReligiousPractice BIT NULL AFTER NPC_PH_ExerciseDetails, 
ADD COLUMN NPC_PH_BodyTemperature INT NULL AFTER NPC_PH_ReligiousPractice, 
ADD COLUMN NPC_PH_EnjoyWork BIT NULL AFTER NPC_PH_BodyTemperature, 
ADD COLUMN NPC_PH_TakeVacations BIT NULL AFTER NPC_PH_EnjoyWork, 
ADD COLUMN NPC_PH_ColdFluDetails VARCHAR(255) NULL AFTER NPC_PH_TakeVacations, 
ADD COLUMN NPC_SH_SleepQuality INT UNSIGNED NULL AFTER NPC_PH_ColdFluDetails, 
ADD COLUMN NPC_SH_FallingAsleepProblem BIT NULL AFTER NPC_SH_SleepQuality, 
ADD COLUMN NPC_SH_StayingAsleepProblem BIT NULL AFTER NPC_SH_FallingAsleepProblem, 
ADD COLUMN NPC_SH_HoursInSleep VARCHAR(10) NULL AFTER NPC_SH_StayingAsleepProblem, 
ADD COLUMN NPC_SH_HoursNeededInSleep VARCHAR(10) NULL AFTER NPC_SH_HoursInSleep, 
ADD COLUMN NPC_SH_WakeRefreshed BIT NULL AFTER NPC_SH_HoursNeededInSleep, 
ADD COLUMN NPC_SH_TakeNap BIT NULL AFTER NPC_SH_WakeRefreshed, 
ADD COLUMN NPC_SH_NapDuration VARCHAR(10) NULL AFTER NPC_SH_TakeNap, 
ADD COLUMN NPC_F_FirstMensesAge VARCHAR(10) NULL AFTER NPC_SH_NapDuration, 
ADD COLUMN NPC_F_PeriodsStoppedAge VARCHAR(10) NULL AFTER NPC_F_FirstMensesAge, 
ADD COLUMN NPC_F_CyclesRegular BIT NULL AFTER NPC_F_PeriodsStoppedAge, 
ADD COLUMN NPC_F_PeriodBeginsEveryDays VARCHAR(10) NULL AFTER NPC_F_CyclesRegular, 
ADD COLUMN NPC_F_PeriodLastsDays VARCHAR(10) NULL AFTER NPC_F_PeriodBeginsEveryDays, 
ADD COLUMN NPC_F_PeriodFlow VARCHAR(255) NULL AFTER NPC_F_PeriodLastsDays, 
ADD COLUMN NPC_F_PeriodBloodColor VARCHAR(255) NULL AFTER NPC_F_PeriodFlow, 
ADD COLUMN NPC_F_AnyClots BIT NULL AFTER NPC_F_PeriodBloodColor, 
ADD COLUMN NPC_F_AnyCramps BIT NULL AFTER NPC_F_AnyClots, 
ADD COLUMN NPC_F_AnySpottingBleeding BIT NULL AFTER NPC_F_AnyCramps, 
ADD COLUMN NPC_F_SpottingBleedingEveryMonth BIT NULL AFTER NPC_F_AnySpottingBleeding, 
ADD COLUMN NPC_F_YeastInfectionInPast BIT NULL AFTER NPC_F_SpottingBleedingEveryMonth, 
ADD COLUMN NPC_F_YeastInfectionFrequency VARCHAR(255) NULL AFTER NPC_F_YeastInfectionInPast, 
ADD COLUMN NPC_F_YeastInfectionTreatment VARCHAR(255) NULL AFTER NPC_F_YeastInfectionFrequency, 
ADD COLUMN NPC_F_AnyPremenstrualSymptoms BIT NULL AFTER NPC_F_YeastInfectionTreatment, 
ADD COLUMN NPC_F_PremenstrualSymptomsDetails VARCHAR(255) NULL AFTER NPC_F_AnyPremenstrualSymptoms, 
ADD COLUMN NPC_F_NumberOfPregnancies VARCHAR(10) NULL AFTER NPC_F_PremenstrualSymptomsDetails, 
ADD COLUMN NPC_F_NumberOfMiscarriages VARCHAR(10) NULL AFTER NPC_F_NumberOfPregnancies, 
ADD COLUMN NPC_F_NumberOfLiveBirths VARCHAR(10) NULL AFTER NPC_F_NumberOfMiscarriages, 
ADD COLUMN NPC_F_AnyPregancyProblem VARCHAR(255) NULL AFTER NPC_F_NumberOfLiveBirths, 
ADD COLUMN NPC_F_RegularPAP BIT NULL AFTER NPC_F_AnyPregancyProblem, 
ADD COLUMN NPC_F_AnyAbnormalPAP BIT NULL AFTER NPC_F_RegularPAP, 
ADD COLUMN NPC_F_RegularBreastSelfExam BIT NULL AFTER NPC_F_AnyAbnormalPAP, 
ADD COLUMN NPC_F_NoticedBreastLumps BIT NULL AFTER NPC_F_RegularBreastSelfExam, 
ADD COLUMN NPC_DE_ProblemWithGasBloating BIT NULL AFTER NPC_F_NoticedBreastLumps, 
ADD COLUMN NPC_DE_Hemorrhoids BIT NULL AFTER NPC_DE_ProblemWithGasBloating, 
ADD COLUMN NPC_DE_RectalBleeding BIT NULL AFTER NPC_DE_Hemorrhoids, 
ADD COLUMN NPC_DE_Parasites BIT NULL AFTER NPC_DE_RectalBleeding, 
ADD COLUMN NPC_DE_ProblemFrequency INT NULL AFTER NPC_DE_Parasites, 
ADD COLUMN NPC_DE_ProblemSeverity VARCHAR(255) NULL AFTER NPC_DE_ProblemFrequency, 
ADD COLUMN NPC_DE_ProblemDuration VARCHAR(255) NULL AFTER NPC_DE_ProblemSeverity, 
ADD COLUMN NPC_DE_BowelMovements VARCHAR(255) NULL AFTER NPC_DE_ProblemDuration, 
ADD COLUMN NPC_DE_AnyBloodInStool VARCHAR(255) NULL AFTER NPC_DE_BowelMovements, 
ADD COLUMN NPC_DE_HadBlackTarryGrayStool BIT NULL AFTER NPC_DE_AnyBloodInStool, 
ADD COLUMN NPC_DE_HadYellowLightColoredStool BIT NULL AFTER NPC_DE_HadBlackTarryGrayStool, 
ADD COLUMN NPC_DE_HadRectalItching BIT NULL AFTER NPC_DE_HadYellowLightColoredStool, 
ADD COLUMN NPC_DE_StoolsFormedOrLoose VARCHAR(255) NULL AFTER NPC_DE_HadRectalItching, 
ADD COLUMN NPC_DE_HadConstipationDiarrhea BIT NULL AFTER NPC_DE_StoolsFormedOrLoose, 
ADD COLUMN NPC_DE_ConstipationDiarrheaDetails VARCHAR(255) NULL AFTER NPC_DE_HadConstipationDiarrhea, 
ADD COLUMN NPC_DE_HaveToStrainToPassStool BIT NULL AFTER NPC_DE_ConstipationDiarrheaDetails, 
ADD COLUMN NPC_DE_HaveToStrainDetails VARCHAR(255) NULL AFTER NPC_DE_HaveToStrainToPassStool, 
ADD COLUMN NPC_DE_PassGasFrequently VARCHAR(255) NULL AFTER NPC_DE_HaveToStrainDetails, 
ADD COLUMN NPC_DE_BurpFrequently VARCHAR(255) NULL AFTER NPC_DE_PassGasFrequently, 
ADD COLUMN NPC_DE_StrongDisagreeableOdor BIT NULL AFTER NPC_DE_BurpFrequently, 
ADD COLUMN NPC_DE_TraveledOutsideOfCanadaPast5Year BIT NULL AFTER NPC_DE_StrongDisagreeableOdor, 
ADD COLUMN NPC_DE_TraveledCountries VARCHAR(255) NULL AFTER NPC_DE_TraveledOutsideOfCanadaPast5Year, 
ADD COLUMN NPC_DE_BeenCammpingPast5Year BIT NULL AFTER NPC_DE_TraveledCountries, 
ADD COLUMN NPC_DE_HadFasted BIT NULL AFTER NPC_DE_BeenCammpingPast5Year, 
ADD COLUMN NPC_DE_FastType VARCHAR(255) NULL AFTER NPC_DE_HadFasted;
";
                            using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                                cmd.ExecuteNonQuery();
                            }
                        }

                    }
                    #endregion Sep 29, 2016 ------ For Lisa only -- Naturopath table redesigned --------------

                    // Oct 25, 2016 ------ Add Booking table ---------------------------
                    #region Oct 25, 2016 ------ Add Booking table ---------------------------
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'booking';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        qry = @"CREATE TABLE `booking` (
  `BOK_BOKId` int(11) NOT NULL,
  `BOK_PTNId` int(11) NOT NULL,
  `BOK_USRId` int(11) NOT NULL,
  `BOK_THPId` int(11) NOT NULL,
  `BOK_StartTime` datetime NOT NULL,
  `BOK_EndTime` datetime NOT NULL,
  `BOK_Note` varchar(255) DEFAULT NULL,
  `BOK_IsTestData` bit(1) NOT NULL,
  `BOK_CreatedTime` datetime NOT NULL,
  `BOK_UpdatedTime` datetime DEFAULT NULL,
  `BOK_CreatedBy` varchar(50) NOT NULL,
  `BOK_UpdatedBy` varchar(50) DEFAULT NULL,
  `BOK_Version` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`BOK_BOKId`),
  KEY `fk_booking_patient_idx` (`BOK_PTNId`),
  KEY `fk_booking_user_idx` (`BOK_USRId`),
  KEY `fk_booking_therapytype_idx` (`BOK_THPId`),
  CONSTRAINT `fk_booking_patient` FOREIGN KEY (`BOK_PTNId`) REFERENCES `patient` (`PTN_PTNId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_booking_therapytype` FOREIGN KEY (`BOK_THPId`) REFERENCES `therapytype` (`THP_THPId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_booking_user` FOREIGN KEY (`BOK_USRId`) REFERENCES `user` (`USR_USRId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion Oct 25, 2016 ------ Add Booking table ---------------------------

                    #region Dec 12, 2016 ---- Add Note & FamilyPatientId columns to Patient table
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'patient' AND COLUMN_NAME = 'PTN_Note';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        //Add new columns
                        qry = @"ALTER TABLE patient ADD COLUMN PTN_FamilyPatientId INT NULL  AFTER PTN_Sex , ADD COLUMN PTN_Note VARCHAR(1000) NULL  AFTER PTN_FamilyPatientId , 
ADD INDEX FamilyPatientId_idx (PTN_FamilyPatientId ASC) ;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion Dec 12, 2016 ---- Add Note & FamilyPatientId columns to Patient table

                    #region Dec 14, 2016 ---- Add ShowDurationOnInvoice column to TherapyType table
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'therapytype' AND COLUMN_NAME = 'THP_ShowDurationOnInvoice';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        //Add new columns
                        qry = @"ALTER TABLE therapytype ADD COLUMN THP_ShowDurationOnInvoice BIT NOT NULL DEFAULT 0 AFTER THP_MaxTreatmentsPerDayPerInsurer;
";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }

                        //Set Massage & Osteopath to 1
                        qry = @"UPDATE therapytype
SET THP_ShowDurationOnInvoice = 1
WHERE THP_TherapyType = 'Massage' Or THP_TherapyType = 'Osteopath';
";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion Dec 14, 2016 ---- Add ShowDurationOnInvoice column to TherapyType table

                    #region June 13, 2017 ------ Enlarge PTN_FileNumber field in Patient table from 4 to 7
                    int maxLength = 0;
                    qry = @"SELECT CHARACTER_MAXIMUM_LENGTH FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'patient' AND COLUMN_NAME = 'PTN_FileNumber';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {
                            maxLength = Convert.ToInt32(reader[0]);
                        }
                    }
                    if (maxLength < 7) {
                        //Add new columns
                        qry = @"ALTER TABLE patient
CHANGE COLUMN PTN_FileNumber PTN_FileNumber VARCHAR(7) NOT NULL;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    exists = true;
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'next_file_number';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        qry = @"CREATE TABLE `next_file_number` (
  `next_key` char(1) NOT NULL,
  `next_number` int(11) NOT NULL,
  PRIMARY KEY (`next_key`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;";

                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    #endregion June 13, 2017 ------ Enlarge PTN_FileNumber field in Patient table from 4 to 7

                    #region July 06, 2017 ----- Add one column TemplateTreatmentDetail.IsDefault
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'templatetreatmentdetail' AND COLUMN_NAME = 'TTD_IsDefault';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        //Add new columns
                        qry = "ALTER TABLE templatetreatmentdetail ADD TTD_IsDefault bit NOT NULL DEFAULT 0 AFTER TTD_DetailId;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion July 06, 2017 ----- Add one column TemplateTreatmentDetail.IsDefault

                    #region Dec 20, 2022 ------- Add two tables DocumentType and Document
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'documenttype';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        qry = @"
CREATE TABLE `documenttype` (
  `DTY_DTYId` INT NOT NULL,
  `DTY_DocumentTypeName` VARCHAR(50) NOT NULL,
  `DTY_Note` VARCHAR(255) NULL,
  `DTY_IsTestData` BIT(1) NOT NULL,
  `DTY_CreatedTime` DATETIME NOT NULL,
  `DTY_UpdatedTime` DATETIME NULL DEFAULT NULL,
  `DTY_CreatedBy` VARCHAR(50) NOT NULL,
  `DTY_UpdatedBy` VARCHAR(50) NULL DEFAULT NULL,
  `DTY_Version` INT NOT NULL DEFAULT '0',
  PRIMARY KEY (`DTY_DTYId`));
";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'document';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        qry = @"
CREATE TABLE `document` (
  `DOC_DOCId` INT NOT NULL,
  `DOC_PTNId` INT NOT NULL,
  `DOC_DTYId` INT NULL,
  `DOC_DocumentCreationTime` DATETIME NOT NULL,
  `DOC_DocumentName` VARCHAR(100) NOT NULL,
  `DOC_Note` VARCHAR(255) NULL,
  `DOC_Blob` LONGBLOB NOT NULL,
  `DOC_IsTestData` BIT(1) NOT NULL,
  `DOC_CreatedTime` DATETIME NOT NULL,
  `DOC_UpdatedTime` DATETIME NULL DEFAULT NULL,
  `DOC_CreatedBy` VARCHAR(50) NOT NULL,
  `DOC_UpdatedBy` VARCHAR(50) NULL DEFAULT NULL,
  `DOC_Version` INT NOT NULL DEFAULT '0',
  PRIMARY KEY (`DOC_DOCId`),
  KEY `fk_document_patient_idx` (`DOC_PTNId`),
  KEY `fk_document_documenttype_idx` (`DOC_DTYId`),
  CONSTRAINT `fk_document_patient` FOREIGN KEY (`DOC_PTNId`) REFERENCES `patient` (`PTN_PTNId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_document_documenttype` FOREIGN KEY (`DOC_DTYId`) REFERENCES `documenttype` (`DTY_DTYId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion Dec 20, 2022 ------- Add two tables DocumentType and Document

                    #region Feb 06, 2023 -------- Add tax table --------------------------
                    //Create tax table if it does NOT exist
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'tax';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        qry = @"CREATE TABLE `tax` (
`TAX_TAXId` int(11) NOT NULL,
`TAX_Name` varchar(5) NOT NULL,
`TAX_Rate` decimal(3,2) NOT NULL,
`TAX_IsTestData` bit(1) NOT NULL,
`TAX_CreatedTime` datetime NOT NULL,
`TAX_UpdatedTime` datetime DEFAULT NULL,
`TAX_CreatedBy` varchar(50) NOT NULL,
`TAX_UpdatedBy` varchar(50) DEFAULT NULL,
`TAX_Version` int(11) NOT NULL DEFAULT '0',
PRIMARY KEY (`TAX_TAXId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    //Add tax columns to invoice table
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'invoice' AND COLUMN_NAME = 'INV_TaxRate';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        //Add new columns
                        qry = @"ALTER TABLE invoice ADD COLUMN INV_TaxRate decimal(3,2) NULL  AFTER INV_Note , ADD COLUMN INV_TaxName VARCHAR(5) NULL  AFTER INV_TaxRate ;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion Feb 06, 2023 --------- Add tax table ----------------------

                    #region Feb 07, 2023 --------- Remove INS_WarnTreatmentPerDay & INS_MaxTreatmentPerDay columns --------------
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'insurer' AND COLUMN_NAME = 'INS_WarnTreatmentPerDay';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (exists) {
                        //Remove both INS_WarnTreatmentPerDay & INS_MaxTreatmentPerDay columns
                        qry = @"ALTER TABLE insurer DROP COLUMN INS_WarnTreatmentPerDay , DROP COLUMN INS_MaxTreatmentPerDay ;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion Feb 07, 2023 --------- Remove INS_WarnTreatmentPerDay & INS_MaxTreatmentPerDay columns --------------

                    #region Feb 07, 2023 --------- Rename columns THP_EnableMaxTreatmentsPerDayPerInsurer & THP_MaxTreatmentsPerDayPerInsurer columns --------------
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'therapytype' AND COLUMN_NAME = 'THP_EnableMaxTreatmentsPerDayPerInsurer';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (exists) {
                        //Rename both THP_EnableMaxTreatmentsPerDayPerInsurer & THP_MaxTreatmentsPerDayPerInsurer columns to remove 'PerInsurer'
                        qry = @"ALTER TABLE therapytype RENAME COLUMN THP_EnableMaxTreatmentsPerDayPerInsurer TO THP_EnableMaxTreatmentsPerDay;ALTER TABLE therapytype RENAME COLUMN THP_MaxTreatmentsPerDayPerInsurer TO THP_MaxTreatmentsPerDay;
";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    } else {
                        qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'therapytype' AND COLUMN_NAME = 'THP_EnableMaxTreatmentsPerDay';";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                        using (MySqlDataReader reader = cmd.ExecuteReader()) {
                            exists = reader.HasRows;
                        }
                        if (!exists) {
                            qry = "ALTER TABLE therapytype ADD THP_EnableMaxTreatmentsPerDay bit(1) NOT NULL DEFAULT 0 AFTER THP_MinutesPerTreatment;" +
                                    "ALTER TABLE therapytype ADD THP_MaxTreatmentsPerDay INT(11) NOT NULL AFTER THP_EnableMaxTreatmentsPerDay;";
                            using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    #endregion Feb 07, 2023 --------- Rename columns THP_EnableMaxTreatmentsPerDayPerInsurer & THP_MaxTreatmentsPerDayPerInsurer columns --------------

                    #region Feb 08, 2023 ---------- Add default price for each treatment type --------------
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'therapytype' AND COLUMN_NAME = 'THP_DefaultPrice';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        qry = @"ALTER TABLE therapytype ADD COLUMN THP_DefaultPrice decimal(10,2) NOT NULL DEFAULT 0 AFTER THP_MaxTreatmentsPerDay;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion Feb 08, 2023 ---------- Add default price for each treatment type --------------

                    #region Mar 10, 2023 ------- Add one more column THP_AllowTimeOverlap --------
                    qry = "SELECT * FROM information_schema.COLUMNS WHERE TABLE_SCHEMA LIKE '" + dbName + "' AND TABLE_NAME = 'therapytype' AND COLUMN_NAME = 'THP_AllowTimeOverlap';";
                    using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
                        exists = reader.HasRows;
                    }
                    if (!exists) {
                        qry = @"ALTER TABLE therapytype ADD COLUMN THP_AllowTimeOverlap BIT(1) NOT NULL DEFAULT b'0' AFTER THP_DefaultPrice;";
                        using (MySqlCommand cmd = new MySqlCommand(qry, conn)) {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion Mar 10, 2023 ------- Add one more column THP_AllowTimeOverlap --------

                }
            } catch (Exception ex) {
                MessageBox.Show(
                    "Failed to update database structure.\n\n" + ex.Message,
                    "MySQL Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Application.Exit();
                return;
            }
            #endregion

            //Login and load main form
            var loginForm = new Login.LoginForm();
            loginForm.LoginAction += new LoginActionHandler((username, password, newPassword) => tryLogon(username, password, newPassword));
            loginForm.HotKeyAction += new HotKeyHandler(showConfigWindow);
            loginForm.ShowDialog();
            if(loginForm.Success) {
                LogonUser = loginForm.LogonUser;
                SessionFactory.SetCurrentUserName(LogonUser.DisplayName);

                if(LogonUser.UserType == UserType.Therapist) {
                    Application.Run(new MyAssignmentForm(LogonUser));
                } else {
                    Application.Run(new MainForm());
                }
            }
        }

        private static void showConfigWindow(){
            ConfigForm frmConfig = new ConfigForm();
            frmConfig.TopMost = true;
            frmConfig.ShowInTaskbar = true;
            frmConfig.ShowDialog();
        }

        public static void AddUserCache(string key, object value){
            if (key == null || key.Length <= 0) throw new ArgumentException("Key cannot be empty");

            if (UserCache.ContainsKey(key)) {
                UserCache[key] = value;
            } else {
                UserCache.Add(key, value);
            }
        }

        public static object GetUserCache(string key) {
            if (key == null || key.Length <= 0) throw new ArgumentException("Key cannot be empty");

            object val = null;

            if (UserCache.ContainsKey(key)) {
                val = UserCache[key];
            }

            return val;
        }

        public static void showWaitScreen() {
            _waitForm = new PleaseWaitForm();
            _waitForm.ProcessCheckHandler += new EventHandler(_waitForm_ProcessCheckHandler);
            _waitForm.ShowDialog();
        }

        static void _waitForm_ProcessCheckHandler(object sender, EventArgs e) {
            if(_hideWaitForm) {
                if(_waitForm != null) {
                    _waitForm.Close();
                    _waitForm.Dispose();
                    _waitForm = null;

                    //Bring LoginForm to font
                    IntPtr hLoginForm = FindWindow(null, "Easy Healthcare Desktop Login");
                    if(hLoginForm != null) {
                        SetForegroundWindow(hLoginForm);
                    } else {
                        IntPtr hConfigForm = FindWindow(null, "J.C Configuration");
                        if(hConfigForm != null) {
                            SetForegroundWindow(hConfigForm);
                        }
                    }
                }
            }
        }

        public static JCConfig config = null;
        public static User LogonUser = null;
        public static IDictionary<PainKeys, Image> PinPointImages = new Dictionary<PainKeys, Image>();
        public static readonly ILog log = LogManager.GetLogger(typeof(Program));
        public static PleaseWaitForm _waitForm = null;
        public static bool _hideWaitForm = false;
        //public static GoogleCalendarHelper.CalendarAPI _calendar = null;

        /// <summary>
        /// Used to record the user entered criteria, such as searching invoice dating back to date
        /// </summary>
        private static IDictionary<string, object> UserCache = new Dictionary<string, object>();

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(String sClassName, String sAppName);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hwnd);
    }
}
