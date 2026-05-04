using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace EHD.Admin {
    public class JCConfig {
        #region private Memembers
        /// <summary>
        /// Single instance handle
        /// </summary>
        private static JCConfig _instance;

        //private string _connectionString;
        //private string _protectionProvider;

        //Stored in userSettings
        /*
        private string _server;
        private int _port;
        private string _db;
        private string _user;
        private string _password;
        private int _maxSigWidth;
        private int _maxSigHeight;*/

        #endregion

        #region Properties
        /*
        public string ConnectionString {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public int SignatureMaxWidth { get; set; }
        public int SignatureMaxHeight { get; set; }
        */
        /*
        public string Server {
            get { return _server; }
            set { _server = value; }
        }
        public int Port {
            get { return _port; }
            set { _port = value; }
        }
        public string Database {
            get { return _db; }
            set { _db = value; }
        }
        public string DBUser {
            get { return _user; }
            set { _user = value; }
        }
        public string DBPassword {
            get { return _password; }
            set { _password = value; }
        }
        public int MaxSigWidth {
            get { return _maxSigWidth; }
            set { _maxSigWidth = value; }
        }
        public int MaxSigHeight {
            get { return _maxSigHeight; }
            set { _maxSigHeight = value; }
        }*/
        public string Server {
            get { return Properties.Settings.Default.DBServer; }
            set { Properties.Settings.Default.DBServer = value; }
        }
        public int Port {
            get { return Properties.Settings.Default.DBPort; }
            set { Properties.Settings.Default.DBPort = value; }
        }
        public string Database {
            get { return Properties.Settings.Default.DBName; }
            set { Properties.Settings.Default.DBName = value; }
        }
        public string DBUser {
            get { return Properties.Settings.Default.DBUser; }
            set { Properties.Settings.Default.DBUser = value; }
        }
        public string DBPassword {
            get { return Properties.Settings.Default.DBPassword; }
            set { Properties.Settings.Default.DBPassword = value; }
        }
        public int SignatureMaxWidth {
            get { return Properties.Settings.Default.SignatureMaxWidth; }
            set { Properties.Settings.Default.SignatureMaxWidth = value; }
        }
        public int SignatureMaxHeight {
            get { return Properties.Settings.Default.SignatureMaxHeight; }
            set { Properties.Settings.Default.SignatureMaxHeight = value; }
        }
        public bool EnableGoogleCalendar {
            get { return Properties.Settings.Default.EnableGoogleCalendar; }
            set { Properties.Settings.Default.EnableGoogleCalendar = value; }
        }
        public bool EnablePractitionerCalendar {
            get { return Properties.Settings.Default.EnablePractitionerCalendar; }
            set { Properties.Settings.Default.EnablePractitionerCalendar = value; }
        }
        public int WorkingHourStart {
            get { return Properties.Settings.Default.WorkingHourStart; }
            set { Properties.Settings.Default.WorkingHourStart = value; }
        }
        public int WorkingHourEnd {
            get { return Properties.Settings.Default.WorkingHourEnd; }
            set { Properties.Settings.Default.WorkingHourEnd = value; }
        }
        public bool DebugMode {
            get { return Properties.Settings.Default.DebugMode; }
            set { Properties.Settings.Default.DebugMode = value; }
        }
        public int MaxInsurers {
            get { return Properties.Settings.Default.MaxInsurers; }
            set { Properties.Settings.Default.MaxInsurers = value; }
        }
        public int MaxTherapists {
            get { return Properties.Settings.Default.MaxTherapists; }
            set { Properties.Settings.Default.MaxTherapists = value; }
        }
        public int MaxTreatmentTypes {
            get { return Properties.Settings.Default.MaxTreatmentTypes; }
            set { Properties.Settings.Default.MaxTreatmentTypes = value; }
        }
        public int MaxInitialTreatmentsPerPatient {
            get { return Properties.Settings.Default.MaxInitialTreatmentsPerPatient; }
            set { Properties.Settings.Default.MaxInitialTreatmentsPerPatient = value; }
        }
        public int MaxFollowUpsPerTreatment {
            get { return Properties.Settings.Default.MaxFollowUpsPerTreatment; }
            set { Properties.Settings.Default.MaxFollowUpsPerTreatment = value; }
        }
        public int MaxPointsPerDiagram {
            get { return Properties.Settings.Default.MaxPointsPerDiagram; }
            set { Properties.Settings.Default.MaxPointsPerDiagram = value; }
        }
        public int MaxInvoicesPerPatient {
            get { return Properties.Settings.Default.MaxInvoicesPerPatient; }
            set { Properties.Settings.Default.MaxInvoicesPerPatient = value; }
        }
        public int MaxInvoiceItemsPerInvoice {
            get { return Properties.Settings.Default.MaxInvoiceItemsPerInvoice; }
            set { Properties.Settings.Default.MaxInvoiceItemsPerInvoice = value; }
        }
        public int MaxDocumentType {
            get { return Properties.Settings.Default.MaxDocumentType; }
            set { Properties.Settings.Default.MaxDocumentType = value; }
        }

        //Application settings
        public string LogFileName {
            get { return Properties.Settings.Default.LogFileName; }
        }
        public string LogSMTPServer {
            get { return Properties.Settings.Default.LogSMTPServer; }
        }
        public int LogSMTPSSLPort {
            get { return Properties.Settings.Default.LogSMTPSSLPort; }
        }
        public string LogSenderAccount {
            get { return Properties.Settings.Default.LogSenderAccount; }
        }
        public string LogSenderPassword {
            get { return Properties.Settings.Default.LogSenderPassword; }
        }
        public string LogReceiverAccount {
            get { return Properties.Settings.Default.LogReceiverAccount; }
        }
        public string HelpFormName
        {
            get { return Properties.Settings.Default.HelpFormName; }
            set { Properties.Settings.Default.HelpFormName = value; }
        }
        public string HelpFormEmail
        {
            get { return Properties.Settings.Default.HelpFormEmail; }
            set { Properties.Settings.Default.HelpFormEmail = value; }
        }
        public string HelpFormPhone
        {
            get { return Properties.Settings.Default.HelpFormPhone; }
            set { Properties.Settings.Default.HelpFormPhone = value; }
        }
        public string HelpFormSubject
        {
            get { return Properties.Settings.Default.HelpFormSubject; }
            set { Properties.Settings.Default.HelpFormSubject = value; }
        }


        public string ConnectionString {
            get {
                return string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};",
                    Server, Port, Database, DBUser, DBPassword);
            }
        }

        #endregion

        #region public methods
        /// <summary>
        /// Create single instance of JCConfig class
        /// </summary>
        /// <returns>Returns JCConfig object or null if error.</returns>
        public static JCConfig instance() {
            //Read config file and populate JCConfig data members
            if(_instance == null) {
                //Create JCConfig instance
                _instance = new JCConfig();

                //Decrypt config file for reading
                //_instance.decryptConfig();

                /*
                //Read config file into data members
                _instance.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                int intValue = 0;
                if(int.TryParse(ConfigurationManager.AppSettings["SignatureMaxWidth"], out intValue) &&
                    intValue > 0) {
                    _instance.SignatureMaxWidth = intValue;
                }
                if(int.TryParse(ConfigurationManager.AppSettings["SignatureMaxHeight"], out intValue) &&
                    intValue > 0) {
                    _instance.SignatureMaxHeight = intValue;
                }*/

                /*
                _instance.Server = Properties.Settings.Default.DBServer;
                _instance.Port = Properties.Settings.Default.DBPort;
                _instance.Database = Properties.Settings.Default.DBName;
                _instance.DBUser = Properties.Settings.Default.DBUser;
                _instance.DBPassword = Properties.Settings.Default.DBPassword; ;
                _instance.MaxSigWidth = Properties.Settings.Default.SignatureMaxWidth;
                _instance.MaxSigHeight = Properties.Settings.Default.SignatureMaxHeight;
                */

                //Encrypt config file
                //_instance.encryptConfig();

                //Verify the config file
                /*
                if(_instance.ConnectionString == null ||
                    _instance.SignatureMaxWidth <= 0 ||
                    _instance.SignatureMaxHeight <= 0) {
                    return null;
                }*/
            }

            return _instance;
        }

        /// <summary>
        /// Save changes to config files
        /// </summary>
        public void Save() {
            //Decrypt config file before saving
            //decryptConfig();

            //Save changes
            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //config.ConnectionStrings.ConnectionStrings["ConnectionString"].ConnectionString = _connectionString;
            //config.AppSettings.Settings["SignatureMaxWidth"].Value = SignatureMaxWidth.ToString();
            //config.AppSettings.Settings["SignatureMaxHeight"].Value = SignatureMaxHeight.ToString();
            //config.Save(ConfigurationSaveMode.Modified);

            /*
            Properties.Settings.Default.DBServer= _instance.Server;
            Properties.Settings.Default.DBPort = _instance.Port;
            Properties.Settings.Default.DBName = _instance.Database;
            Properties.Settings.Default.DBUser = _instance.DBUser;
            Properties.Settings.Default.DBPassword = _instance.DBPassword;
            Properties.Settings.Default.SignatureMaxWidth = _instance.MaxSigWidth;
            Properties.Settings.Default.SignatureMaxHeight = _instance.MaxSigHeight;
            */

            //Encrypt config file after saving
            //encryptConfig();
            Properties.Settings.Default.Save();
        }
        #endregion

        #region private methods
        /// <summary>
        /// Encrypt config file
        /// </summary>
        private void encryptConfig() {
            _cryptConfig(true);
        }

        /// <summary>
        /// Decrypt config file
        /// </summary>
        private void decryptConfig() {
            _cryptConfig(false);
        }

        /// <summary>
        /// Encrypt/Decrypt config file
        /// </summary>
        private void _cryptConfig(bool isToEncrypt) {
            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //ConfigurationSection connSection = config.ConnectionStrings;
            //ConfigurationSection appSection = config.AppSettings;

            /*
            //ConnectionString section
            if(connSection != null
                && !connSection.ElementInformation.IsLocked) {
                if(isToEncrypt && !connSection.SectionInformation.IsProtected)
                    connSection.SectionInformation.ProtectSection(_protectionProvider);
                else if(!isToEncrypt && connSection.SectionInformation.IsProtected)
                    connSection.SectionInformation.UnprotectSection();

                connSection.SectionInformation.ForceSave = true;
            }

            //AppSettings section
            if(appSection != null
                && !appSection.ElementInformation.IsLocked) {
                if(isToEncrypt && !appSection.SectionInformation.IsProtected)
                    appSection.SectionInformation.ProtectSection(_protectionProvider);
                else if(!isToEncrypt && appSection.SectionInformation.IsProtected)
                    appSection.SectionInformation.UnprotectSection();

                appSection.SectionInformation.ForceSave = true;
            }
            //userSettings will be encrypted once, and keep encrypted all the time.
            foreach (ConfigurationSection section in config.SectionGroups["userSettings"].Sections) {
                if (section != null
                    && !section.ElementInformation.IsLocked
                    && !section.SectionInformation.IsProtected) {

                    section.SectionInformation.ProtectSection(_protectionProvider);
                    section.SectionInformation.ForceSave = true;
                }
            }

            //Save
            config.Save();
            */
        }
        #endregion

        #region Constructor
        protected JCConfig() {
            //_protectionProvider = "DataProtectionConfigurationProvider";
        }
        #endregion

    }
}
