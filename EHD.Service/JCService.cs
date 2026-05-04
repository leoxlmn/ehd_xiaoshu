using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHD.Repository;
using EHD.Model.Entity;
using EHD.Model;
using EHD.Constant;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using HolidayCalculator;
using System.Text.RegularExpressions;


namespace EHD.Service {
    class dtoMPT {
        public int Minutes { get; set; }
        public int MinutesPerTreatment { get; set; }
    }

    public class JCService {
        private ISession _session;
        private User _actionBy;

        public JCService(ISession session, User actionBy) {
            if(session == null)
                throw new Exception("Session cannot be null.");

            if(!session.IsOpen)
                throw new Exception("Session is closed.");

            _session = session;
            _actionBy = actionBy;
        }

        public bool UsernameExists(string username) {
            bool exists = false;

            try {
                int count = _session.QueryOver<User>()
                    .List<User>()
                    .Where(u => u.Username.Trim().ToLower().Equals(username.Trim().ToLower()))
                    .Count();

                exists = count > 0;
            } catch(Exception ex) {
                throw new ApplicationException("UsernameExists Failed. " + ex.Message, ex);
            }

            return exists;
        }

        public User GetUserByUsernamePassword(string username, string password) {
            User user = null;

            try {
                user = _session.CreateCriteria<User>("u")
                    .CreateCriteria("u.Title", "t", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                    .Add(Restrictions.Eq("u.Username", username))
                    .SetMaxResults(1)
                    .List<User>()
                    .Where(u => u.Password.Equals(password))
                    .SingleOrDefault();
            } catch(Exception ex) {
                throw new ApplicationException("GetUserByUsernamePassword Failed. " + ex.Message, ex);
            }

            return user;
        }

        public string GetNextFileNumber(string lastname) {
            
            if(lastname.Length <= 0)
                throw new ArgumentException("Lastname cannot be empty!");

            StringBuilder sb = new StringBuilder();
            int maxSeq = 0;

            char prefLetter = lastname.ToUpper().ToCharArray()[0];
            sb.Append(prefLetter);

            try {
                //1) Try to find the next number from next_file_number table
                var nextNumber = _session.QueryOver<NextFileNumber>()
                    .Where(x => x.Key == prefLetter)
                    .SingleOrDefault<NextFileNumber>();

                if(nextNumber != null) {
                    maxSeq = nextNumber.NextNumber;
                }else {
                    var fileNumbers = _session.QueryOver<Patient>()
                        .Where(x => x.FileNumber.IsLike(prefLetter + "%"))
                        .Select(x => x.FileNumber)
                        .Take(1000000) //Shouldn't be more than 1000000 patient having the same first last name character
                        .List<string>();

                    //Find next sequence
                    foreach (string fn in fileNumbers) {

                        if (fn.Length < 4) continue;

                        string sSeq = fn.Substring(1);
                        int iSeq;
                        if (!int.TryParse(sSeq, out iSeq)) continue;

                        if (maxSeq < iSeq) {
                            maxSeq = iSeq;
                        }
                    }
                    maxSeq++;
                }

                sb.Append((maxSeq).ToString().PadLeft(3, '0'));
            } catch (Exception ex) {
                throw new ApplicationException("Failed to get a new File Number.", ex);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Advance the next_number in Next_File_Number table based on the given existing file number
        /// </summary>
        /// <param name="ExistingFileNumber"></param>
        public void UpdateNextFileNumber(string ExistingFileNumber) {
            if (ExistingFileNumber == null || ExistingFileNumber.Trim().Length <= 0) return;

            char key = ExistingFileNumber.Trim().ToUpper().ToCharArray()[0];
            int curNumber = 0;
            Match matchNumber = Regex.Match(ExistingFileNumber, "^[A-Z][0-9]+");
            if (matchNumber.Success) {
                curNumber = Convert.ToInt32(matchNumber.Value.Substring(1));
            }

            int nextNumber = curNumber + 1;

            var nextFileNumber = _session.QueryOver<NextFileNumber>()
                .Where(x => x.Key == key)
                .SingleOrDefault<NextFileNumber>();
            if (nextFileNumber != null) {
                if (nextNumber > nextFileNumber.NextNumber) {
                    nextFileNumber.NextNumber = nextNumber;
                    _session.Save(nextFileNumber);
                }
            } else {
                nextFileNumber = new NextFileNumber {
                    Key = key,
                    NextNumber = nextNumber
                };
                _session.Save(nextFileNumber);
            }
        }

        public bool FileNumberExists(string fileNumber) {
            bool exists = false;

            try {
                int numFileNumber = _session.QueryOver<Patient>()
                    .Where(x => x.FileNumber.IsLike(fileNumber))
                    .RowCount();

                exists = (numFileNumber > 0);
            } catch(Exception ex) {
                throw new ApplicationException("Failed to check File Number.", ex);
            }

            return exists;
        }

        public bool isFileNumberUnique(int patientId, string fileNumber) {
            bool isUnique = false;

            try {
                int numFileNumber = _session.QueryOver<Patient>()
                    .Where(x => x.FileNumber.IsLike(fileNumber) &&
                        x.Id != patientId)
                    .RowCount();

                isUnique = (numFileNumber <= 0);
            } catch(Exception ex) {
                throw new ApplicationException("Failed to check File Number.", ex);
            }

            return isUnique;
        }

        //Only check on first name and last name, middle name is ignored for now
        public bool isPatientNameUnique(int patientId, string firstName, string lastName) {
            bool isUnique = false;

            try {
                int numName = _session.QueryOver<Patient>()
                    .Where(x => x.Id != patientId)
                    .And(x => x.FirstName.IsLike(firstName.Trim()))
                    .And(x => x.LastName.IsLike(lastName.Trim()))
                    .RowCount();

                isUnique = (numName <= 0);
            } catch (Exception ex) {
                throw new ApplicationException("Failed to check patient's name.", ex);
            }

            return isUnique;
        }

        /// <summary>
        /// Decommissioned
        /// Retrieve the next available invoice number
        /// <returns>Return the next available invoice number. Return empty if not found.</returns>
        /// </summary>
        /*
        public string GetNextInvoiceNumber() {
            string invNum = string.Empty;

            SystemSetting invoiceNumSetting = GetSettingBy(Constants.SETTING_NAME_INVOICE_NUMBER_TEMPLATE);

            if(invoiceNumSetting != null) {
                //Get invoice number template info
                int sequenceLength = 1;
                int sequence = 1;
                char[] chrs = invoiceNumSetting.Value.ToArray();
                int ind = -1;
                string invNumTempateString = string.Empty;
                for (int i = chrs.Length - 1; i >= 0; i--) {
                    char chr = chrs[i];

                    if (chr < '0' || chr > '9') {
                        ind = i;
                        if (ind < chrs.Length - 1) {
                            sequenceLength = chrs.Length - 1 - ind;
                        }
                        if (ind > 0) {
                            invNumTempateString = invoiceNumSetting.Value.Substring(0, ind+1);
                        }
                        break;
                    }
                }

                //Get the max invoice number matching the invoice number template
                string curMaxInvNumber = _session.CreateCriteria<Invoice>()
                    .Add(Restrictions.Like("InvoiceNumber", invNumTempateString + "%"))
                    .SetProjection(Projections.Max("InvoiceNumber"))
                    .UniqueResult<string>();

                if (curMaxInvNumber != null && curMaxInvNumber.Length > 0) {
                    int curMaxSeq = -1;
                    if (int.TryParse(curMaxInvNumber.Substring(invNumTempateString.Length), out curMaxSeq)) {
                        sequence = curMaxSeq + 1;
                        invNum = invNumTempateString + sequence.ToString().PadLeft(sequenceLength, '0');
                    }
                } else {
                    invNum = invNumTempateString + sequence.ToString().PadLeft(sequenceLength, '0');
                }
            }

            if (invNum.Length > 0 && IsInvoiceNumberInUse(invNum)) {
                invNum = string.Empty;
            }

            return invNum;
        }*/

        /// <summary>
        /// Retrieve the next available invoice number
        /// Invoice number is in format:
        ///     [File Number] + [First letter of treatment type] + [last 2 digits of year] + [2 digits of month] + [1 digit sequence letter from 'a' to 'z']
        /// <returns>Return the next available invoice number. Return empty if not found.</returns>
        /// </summary>
        /// <returns></returns>
        public string GetNextInvoiceNumber(string fileNo, string treatmentType, DateTime statementDate, int invoiceId) {
            StringBuilder sb = new StringBuilder();

            //Add invoice prefix if defined
            string prefix = _session.QueryOver<SystemSetting>()
                .Where(x => x.Name == Constants.SETTING_NAME_INVOICE_NUMBER_PREFIX)
                .Select(x => x.Value)
                .SingleOrDefault<string>();

            if(prefix != null) {
                prefix = prefix.Trim();
                if(prefix.Length > 2) {
                    throw new ApplicationException("The setting " + Constants.SETTING_NAME_INVOICE_NUMBER_PREFIX + " is too long. Maximum length is 2.");
                }
                sb.Append(prefix);
            }

            sb.Append(fileNo);

            if(treatmentType != null && treatmentType.Length > 0) {
                sb.Append(treatmentType.Substring(0, 1).ToUpper());
            }

            sb.Append(statementDate.ToString("yyMM"));

            string invNumTemp = sb.ToString();

            //Check if the default invoice number has been used already
            string curDefaultInvNumber = _session.CreateCriteria<Invoice>()
                .Add(Restrictions.Like("InvoiceNumber", invNumTemp))
                .Add(Restrictions.Not(Restrictions.Eq("Id", invoiceId)))
                .SetProjection(Projections.Max("InvoiceNumber"))
                .UniqueResult<string>();

            if (curDefaultInvNumber != null) {
                //Get the max invoice number matching the invoice number template
                string curMaxInvNumber = _session.CreateCriteria<Invoice>()
                    .Add(Restrictions.Like("InvoiceNumber", invNumTemp + "_"))
                    .Add(Restrictions.Not(Restrictions.Eq("Id", invoiceId)))
                    .SetProjection(Projections.Max("InvoiceNumber"))
                    .UniqueResult<string>();

                //Get the next available sequence character
                char chrSeq = 'b';
                if (curMaxInvNumber != null) {
                    char curSeq = curMaxInvNumber.Substring(curMaxInvNumber.Length - 1, 1).ToCharArray()[0];
                    if (curSeq >= 'z') {
                        throw new ApplicationException("There are too many invoices created. The invoice number "
                            + curSeq
                            + " has reached the maximum allowed number.");
                    } else if (curSeq >= 'b') {
                        chrSeq = Convert.ToChar(Convert.ToInt32(curSeq) + 1);
                    }
                }

                sb.Append(chrSeq);
            }

            return sb.ToString();
        }

        public string GetNextInvoiceNumber(string fileNo, string treatmentType, DateTime statementDate) {
            return GetNextInvoiceNumber(fileNo, treatmentType, statementDate, 0);
        }

        public bool IsInvoiceNumberInUse(string invNum) {
            var invNumCheck = _session.QueryOver<Invoice>()
                .Where(inv => inv.InvoiceNumber == invNum)
                .RowCount();

            return invNumCheck > 0;
        }

        /*
        public string MakeNextInvoiceNumber(string invNum) {
            StringBuilder sb = new StringBuilder();

            char[] chrs = invNum.ToCharArray();
            int ind = -1;
            int sequence = 1;
            int sequenceLength = 1;
            for(int i = chrs.Length - 1; i >= 0; i--) {
                char chr = chrs[i];

                if(chr < '0' || chr > '9') {
                    ind = i;
                    if(ind < chrs.Length - 1) {
                        sequenceLength = chrs.Length - 1 - ind;
                        sequence = Convert.ToInt32(invNum.Substring(ind + 1, sequenceLength));
                    }
                }
            }

            if(ind >= 0) {
                sb.Append(invNum.Substring(0, ind));
            }

            sb.Append((sequence++).ToString().PadLeft(sequenceLength));

            return sb.ToString();
        }*/

        public double GetNumberOfTreatmentFor(int therapistId, int insurerId, DateTime date, 
            int selfInitialTreatmentId, int selfFollowUpId) {

            /*
             * Rewrite to calculate minutes if EnableMinutesPerTreatment has been set for a treatment type
             */
            #region Old
            //Calculate the treatments that do NOT enable the MinutesPerTreatment
            /*
                var qryInit = _session.CreateCriteria<InitialTreatment>("it")
                    .Add(Restrictions.Not(Restrictions.Eq("Id", selfInitialTreatmentId)))
                    .Add(Restrictions.Eq("Therapist.Id", therapist.Id))
                    .CreateCriteria("TreatmentType", "type")
                    .Add(Restrictions.Eq("type.EnableMinutesPerTreatment", false))
                    .CreateCriteria("it.Patient", "p")
                    .CreateCriteria("p.Insurer", "i")
                    .Add(Restrictions.Eq("i.Id", insurer.Id))
                    .Future<InitialTreatment>();
                var qryFup = _session.CreateCriteria<FollowUpTreatment>("ft")
                    .Add(Restrictions.Not(Restrictions.Eq("Id", selfFollowUpId)))
                    .Add(Restrictions.Eq("Therapist.Id", therapist.Id))
                    .CreateCriteria("ft.InitialTreatment", "init")
                    .CreateCriteria("init.TreatmentType", "type")
                    .Add(Restrictions.Eq("type.EnableMinutesPerTreatment", false))
                    .CreateCriteria("init.Patient", "p")
                    .CreateCriteria("p.Insurer", "i")
                    .Add(Restrictions.Eq("i.Id", insurer.Id))
                    .Future<FollowUpTreatment>();

            //Calculate the treatments that DO enabled the MinutesPerTreatment
                var qryInitMinutes = _session.CreateCriteria<InitialTreatment>("it")
                    .Add(Restrictions.Not(Restrictions.Eq("Id", selfInitialTreatmentId)))
                    .Add(Restrictions.Eq("Therapist.Id", therapist.Id))
                    .CreateCriteria("TreatmentType", "type")
                    .Add(Restrictions.Eq("type.EnableMinutesPerTreatment", true))
                    .CreateCriteria("it.Patient", "p")
                    .CreateCriteria("p.Insurer", "i")
                    .Add(Restrictions.Eq("i.Id", insurer.Id))
                    .Future<InitialTreatment>();

                var qryFupMinutes = _session.CreateCriteria<FollowUpTreatment>("ft")
                    .Add(Restrictions.Not(Restrictions.Eq("Id", selfFollowUpId)))
                    .Add(Restrictions.Eq("Therapist.Id", therapist.Id))
                    .CreateCriteria("ft.InitialTreatment", "init")
                    .CreateCriteria("init.TreatmentType", "type")
                    .Add(Restrictions.Eq("type.EnableMinutesPerTreatment", true))
                    .CreateCriteria("init.Patient", "p")
                    .CreateCriteria("p.Insurer", "i")
                    .Add(Restrictions.Eq("i.Id", insurer.Id))
                    .Future<FollowUpTreatment>();

                double ret = qryInit.ToList()
                    .Where(x => x.TreatmentTime.Date == date.Date)
                    .Count() +
                    qryFup.ToList()
                    .Where(x => x.TreatmentTime.Date == date.Date)
                    .Count() +

                    qryInitMinutes.ToList()
                    .Where(x => x.TreatmentTime.Date == date.Date)
                    .Sum(x => ((double)x.TreatmentDurationMinutes / x.TreatmentType.MinutesPerTreatment)) +

                    qryFupMinutes.ToList()
                    .Where(x => x.TreatmentTime.Date == date.Date)
                    .Sum(x => ((double)x.TreatmentDurationMinutes / x.InitialTreatment.TreatmentType.MinutesPerTreatment));

            return ret;
            */
            #endregion Old

            /* Rewrote */
            //Calculate the treatments that do NOT enable the MinutesPerTreatment
            InitialTreatment it = null;
            FollowUpTreatment ft = null;
            TherapyType type = null;
            Patient p = null;
            var qryItCnt = _session.QueryOver<InitialTreatment>(() => it)
                .JoinQueryOver(() => it.TreatmentType, () => type)
                .JoinQueryOver(() => it.Patient, () => p)
                .Where(() => it.Id != selfInitialTreatmentId)
                .Where(() => it.Therapist.Id == therapistId)
                .Where(() => type.EnableMinutesPerTreatment == false)
                .Where(() => p.Insurer.Id == insurerId)
                .Where(() => it.TreatmentTime >= date.Date)
                .Where(() => it.TreatmentTime < date.Date.AddDays(1))
                .Select(Projections.CountDistinct(() => it.Id))
                .FutureValue<int>();

            ft = null;
            it = null;
            type = null;
            p = null;
            var qryFtCnt = _session.QueryOver<FollowUpTreatment>(() => ft)
                .JoinQueryOver(() => ft.InitialTreatment, () => it)
                .JoinQueryOver(() => it.TreatmentType, () => type)
                .JoinQueryOver(() => it.Patient, () => p)
                .Where(() => ft.Id != selfFollowUpId)
                .Where(() => ft.Therapist.Id == therapistId)
                .Where(() => type.EnableMinutesPerTreatment == false)
                .Where(() => p.Insurer.Id == insurerId)
                .Where(() => ft.TreatmentTime >= date.Date)
                .Where(() => ft.TreatmentTime < date.Date.AddDays(1))
                .Select(Projections.CountDistinct(() => ft.Id))
                .FutureValue<int>();

            //Calculate the treatments that DO enabled the MinutesPerTreatment
            ft = null;
            it = null;
            type = null;
            p = null;
            dtoMPT dto = null;
            var qryItMinutes = _session.QueryOver<InitialTreatment>(() => it)
                .JoinQueryOver(() => it.TreatmentType, () => type)
                .JoinQueryOver(() => it.Patient, () => p)
                .Where(() => it.Id != selfInitialTreatmentId)
                .Where(() => it.Therapist.Id == therapistId)
                .Where(() => type.EnableMinutesPerTreatment == true)
                .Where(() => p.Insurer.Id == insurerId)
                .Where(() => it.TreatmentTime >= date.Date)
                .Where(() => it.TreatmentTime < date.Date.AddDays(1))
                .SelectList(list => list
                    .Select(() => it.TreatmentDurationMinutes).WithAlias(() => dto.Minutes)
                    .Select(() => type.MinutesPerTreatment).WithAlias(() => dto.MinutesPerTreatment)
                )
                .TransformUsing(Transformers.AliasToBean<dtoMPT>())
                .Future<dtoMPT>();

            ft = null;
            it = null;
            type = null;
            p = null;
            dto = null;
            var qryFtMinutes = _session.QueryOver<FollowUpTreatment>(() => ft)
                .JoinQueryOver(() => ft.InitialTreatment, () => it)
                .JoinQueryOver(() => it.TreatmentType, () => type)
                .JoinQueryOver(() => it.Patient, () => p)
                .Where(() => ft.Id != selfFollowUpId)
                .Where(() => ft.Therapist.Id == therapistId)
                .Where(() => type.EnableMinutesPerTreatment == true)
                .Where(() => p.Insurer.Id == insurerId)
                .Where(() => ft.TreatmentTime >= date.Date)
                .Where(() => ft.TreatmentTime < date.Date.AddDays(1))
                .SelectList(list => list
                    .Select(() => ft.TreatmentDurationMinutes).WithAlias(() => dto.Minutes)
                    .Select(() => type.MinutesPerTreatment).WithAlias(() => dto.MinutesPerTreatment)
                )
                .TransformUsing(Transformers.AliasToBean<dtoMPT>())
                .Future<dtoMPT>();

            return qryItCnt.Value
                + qryFtCnt.Value
                + qryItMinutes.ToList().Sum(x => ((double)x.Minutes / x.MinutesPerTreatment))
                + qryFtMinutes.ToList().Sum(x => ((double)x.Minutes / x.MinutesPerTreatment));
        }

        /// <summary>
        /// Get number of treatment for the given Therapist, Insurer, TherapyType and treatment time.
        /// * If the therapy type has MinutesPerTreatment enabled, that will be used to calcuate the number of treatment.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public double GetNumberOfTreatmentFor(TherapyType therapyType, User therapist, DateTime date,
            int selfInitialId, int selfFollowUpId) {

            double ret = 0;
            InitialTreatment it = null;
            FollowUpTreatment ft = null;
            Patient p = null;

            if (therapyType.EnableMinutesPerTreatment) {
                //Initial Treatments
                var qryItMinutes = _session.QueryOver<InitialTreatment>(() => it)
                    .Where(() => it.Id != selfInitialId)
                    .Where(() => it.Therapist.Id == therapist.Id)
                    .Where(() => it.TreatmentType.Id == therapyType.Id)
                    .Where(() => it.TreatmentTime >= date.Date)
                    .Where(() => it.TreatmentTime < date.Date.AddDays(1))
                    .Select(Projections.Property(() => it.TreatmentDurationMinutes))
                    .Future<int>();
                /*
                var qryInit = _session.CreateCriteria<InitialTreatment>("it")
                    .Add(Restrictions.Not(Restrictions.Eq("Id", selfInitialId)))
                    .Add(Restrictions.Eq("Therapist.Id", therapist.Id))
                    .Add(Restrictions.Eq("TreatmentType.Id", therapyType.Id))
                    .CreateCriteria("it.Patient", "p")
                    .CreateCriteria("p.Insurer", "i")
                    .Add(Restrictions.Eq("i.Id", insurer.Id))
                    .Add(Expression.Between("it.TreatmentTime", date.Date, date.Date.AddDays(1).AddSeconds(-1)))
                    .Future<InitialTreatment>();*/

                //FollowUp treatments
                var qryFtMinutes = _session.QueryOver<FollowUpTreatment>(() => ft)
                    .JoinQueryOver(() => ft.InitialTreatment, () => it)
                    .Where(() => ft.Id != selfFollowUpId)
                    .Where(() => ft.Therapist.Id == therapist.Id)
                    .Where(() => it.TreatmentType.Id == therapyType.Id)
                    .Where(() => ft.TreatmentTime >= date.Date)
                    .Where(() => ft.TreatmentTime < date.Date.AddDays(1))
                    .Select(Projections.Property(() => ft.TreatmentDurationMinutes))
                    .Future<int>();
                /*
                var qryFup = _session.CreateCriteria<FollowUpTreatment>("ft")
                    .Add(Restrictions.Not(Restrictions.Eq("Id", selfFollowUpId)))
                    .Add(Restrictions.Eq("Therapist.Id", therapist.Id))
                    .CreateCriteria("ft.InitialTreatment", "init")
                    .Add(Restrictions.Eq("init.TreatmentType.Id", therapyType.Id))
                    .CreateCriteria("init.Patient", "p")
                    .CreateCriteria("p.Insurer", "i")
                    .Add(Restrictions.Eq("i.Id", insurer.Id))
                    .Add(Expression.Between("ft.TreatmentTime", date.Date, date.Date.AddDays(1).AddSeconds(-1)))
                    .Future<FollowUpTreatment>();*/
                //Get count
                ret = qryItMinutes.ToList().Sum(x => ((double)x / therapyType.MinutesPerTreatment))
                    + qryFtMinutes.ToList().Sum(x => ((double)x / therapyType.MinutesPerTreatment));
                /*
                ret = qryInit.ToList<InitialTreatment>()
                    .Sum(x => (double)x.TreatmentDurationMinutes / therapyType.MinutesPerTreatment) +
                    qryFup.ToList<FollowUpTreatment>()
                    .Sum(x => (double)x.TreatmentDurationMinutes / therapyType.MinutesPerTreatment);*/
            } else {
                //Initial Treatments
                var qryInit = _session.CreateCriteria<InitialTreatment>("it")
                    .Add(Restrictions.Not(Restrictions.Eq("Id", selfInitialId)))
                    .Add(Restrictions.Eq("Therapist.Id", therapist.Id))
                    .Add(Restrictions.Eq("TreatmentType.Id", therapyType.Id))
                    .CreateCriteria("it.Patient", "p")
                    .Add(Expression.Between("it.TreatmentTime", date.Date, date.Date.AddDays(1).AddSeconds(-1)))
                    .SetProjection(Projections.RowCount())
                    .FutureValue<int>();
                //FollowUp treatments
                var qryFup = _session.CreateCriteria<FollowUpTreatment>("ft")
                    .Add(Restrictions.Not(Restrictions.Eq("Id", selfFollowUpId)))
                    .Add(Restrictions.Eq("Therapist.Id", therapist.Id))
                    .CreateCriteria("ft.InitialTreatment", "init")
                    .Add(Restrictions.Eq("init.TreatmentType.Id", therapyType.Id))
                    .CreateCriteria("init.Patient", "p")
                    .Add(Expression.Between("ft.TreatmentTime", date.Date, date.Date.AddDays(1).AddSeconds(-1)))
                    .SetProjection(Projections.RowCount())
                    .FutureValue<int>();
                //Get count
                ret = qryInit.Value + qryFup.Value;
            }

            return ret;
        }

        /// <summary>
        /// Get number of invoices for the given Therapist, TherapyType and treatment time.
        /// * If the therapy type has MinutesPerTreatment enabled, that will be used to calcuate the number of treatment.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public double GetNumberOfInvoiceItemFor(TherapyType therapyType, User therapist, DateTime date,
            int selfInvoiceItemId) {

            double ret = 0;

            if (therapyType.EnableMinutesPerTreatment) {
                var qry = _session.CreateCriteria<InvoiceItem>("ii")
                    .Add(Restrictions.Not(Restrictions.Eq("Id", selfInvoiceItemId)))
                    .CreateCriteria("Invoice", "inv")
                    .Add(Restrictions.Eq("inv.TreatmentType.Id", therapyType.Id))
                    .Add(Restrictions.Eq("inv.Therapist.Id", therapist.Id))
                    //.CreateCriteria("inv.Patient", "p")
                    //.Add(Restrictions.Eq("p.Insurer.Id", insurer.Id))
                    //.Add(Expression.Between("ii.ServiceDate", date.Date, date.Date.AddDays(1)))
                    .Add(Expression.Conjunction()
                        .Add(Restrictions.Ge("ii.ServiceDate", date.Date))
                        .Add(Restrictions.Lt("ii.ServiceDate", date.Date.AddDays(1)))
                    )
                    .Future<InvoiceItem>();
                //Get count
                ret = qry.ToList<InvoiceItem>()
                    .Sum(x => (double)x.ServiceDuration / therapyType.MinutesPerTreatment);
            } else {
                var qry = _session.CreateCriteria<InvoiceItem>("ii")
                    .Add(Restrictions.Not(Restrictions.Eq("Id", selfInvoiceItemId)))
                    .CreateCriteria("Invoice", "inv")
                    .Add(Restrictions.Eq("inv.TreatmentType.Id", therapyType.Id))
                    .Add(Restrictions.Eq("inv.Therapist.Id", therapist.Id))
                    //.CreateCriteria("inv.Patient", "p")
                    //.Add(Restrictions.Eq("p.Insurer.Id", insurer.Id))
                    //.Add(Expression.Between("ii.ServiceDate", date.Date, date.Date.AddDays(1)))
                    .Add(Expression.Conjunction()
                        .Add(Restrictions.Ge("ii.ServiceDate", date.Date))
                        .Add(Restrictions.Lt("ii.ServiceDate", date.Date.AddDays(1)))
                    )
                    .SetProjection(Projections.RowCount())
                    .FutureValue<int>();
                //Get count
                ret = qry.Value;
            }

            return ret;
        }


        public SystemSetting GetSettingBy(string Name) {
            return _session.QueryOver<SystemSetting>()
                .Where(x => x.Name == Name)
                .SingleOrDefault();
        }

        public void UpdateInvoiceStatementDate(Invoice invToUpdate) {
            DateTime latestDate = _session.CreateCriteria<InvoiceItem>()
                .Add(Restrictions.Eq("Invoice.Id", invToUpdate.Id))
                .SetProjection(Projections.Max("ServiceDate"))
                .UniqueResult<DateTime>();

            if(latestDate != default(DateTime)) {
                invToUpdate.StatementDate = latestDate;
                _session.Save(invToUpdate);
            }
            /*
            long tickMaxServiceDate = _session.QueryOver<InvoiceItem>().List<InvoiceItem>()
                .Where<InvoiceItem>(x => x.Invoice.Id == invToUpdate.Id)
                .Max<InvoiceItem>(x => x.ServiceDate.Ticks);

            if (tickMaxServiceDate > 0) {
                invToUpdate.StatementDate = new DateTime(tickMaxServiceDate);
                _session.Save(invToUpdate);
            }*/
                
        }


        #region InitialTreatment
        public void DeleteInitialTreatment(int itId) {
            try {
                InitialTreatment itToDelete = _session.Get<InitialTreatment>(itId);

                //Delete all related treatment details
                DeleteTreatmentDetail(itToDelete);

                //Delete InitialTreatment
                //Update UpdateBy field so the audit listener would know who is deleting
                if(_actionBy != null) itToDelete.UpdatedBy = _actionBy.DisplayName;
                _session.Delete(itToDelete);

            } catch(Exception ex) {
                throw new ApplicationException("Delete InitialTreatment Failed. " + ex.Message, ex);
            }
        }

        public ITreatmentDetail GetInitialTreatmentDetail(int Id, string TreatmentTypeName) {
            ITreatmentDetail detail = null;

            if (Id == 0 || TreatmentTypeName == null) return null;

            // 1) Physiotherapy
            if (TreatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_PHYSIOTHERAPY.ToLower())) {
                var physiotherapyDetail = _session.QueryOver<PhysiotherapyDetail>()
                    .Where(x => x.InitialTreatment.Id == Id)
                    .SingleOrDefault();

                detail = physiotherapyDetail;
            }
            // 2) Chiropractic / Osteopath
            else if(TreatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_CHIROPRACTIC.ToLower()))
            {
                var chiropracticDetail = _session.QueryOver<ChiropracticDetail>()
                    .Where(x => x.InitialTreatment.Id == Id)
                    .SingleOrDefault();

                detail = chiropracticDetail;
            }
            // Osteopathy
            else if (TreatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_OSTEOPATH.ToLower())) {
                var ostDetail = _session.QueryOver<OsteopathyDetail>()
                    .Where(x => x.InitialTreatment.Id == Id)
                    .SingleOrDefault();

                detail = ostDetail;
            }
            // 3) Acupuncture
            else if(TreatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_ACUPUNCTURE.ToLower())) {
                var acupunctureDetail = _session.QueryOver<AcupunctureDetail>()
                    .Where(x => x.InitialTreatment.Id == Id)
                    .SingleOrDefault();

                detail = acupunctureDetail;
            }
            // 4) Massage
            else if(TreatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_MASSAGE.ToLower())) {
                var massageDetail = _session.QueryOver<MassageDetail>()
                    .Where(x => x.InitialTreatment.Id == Id)
                    .SingleOrDefault();

                detail = massageDetail;
            }
            // 5) Naturopathic
            else if(TreatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_NATUROPATHIC.ToLower())) {
                var naturopathicDetail = _session.QueryOver<NaturopathicDetail>()
                    .Where(x => x.InitialTreatment.Id == Id)
                    .SingleOrDefault();

                detail = naturopathicDetail;
            }

            //TO DO: Add other TreatmentTypes
            else {
                throw new ApplicationException("The treatment type " + TreatmentTypeName + " has not been implemented yet in GetTreatmentDetail() function.");
            }

            return detail;
        }

        public void DeleteTreatmentDetail(InitialTreatment it) {
            try {
                ITreatmentDetail detail = GetInitialTreatmentDetail(it.Id, it.TreatmentType.TherapyTypeName);
                if(detail != null) {
                    //Delete all points first
                    //1) Physiotherapy
                    if(detail is PhysiotherapyDetail) {
                        int detailId = (detail as PhysiotherapyDetail).Id;
                        var points = _session.QueryOver<PointOnPhysiotherapy>()
                            .Where(p => p.PhysiotherapyDetail.Id == detailId)
                            .List<PointOnPhysiotherapy>();
                        foreach(PointOnPhysiotherapy p in points) {
                            if(_actionBy != null) p.UpdatedBy = _actionBy.DisplayName;
                            _session.Delete(p);
                        }
                    }
                    //2) Chiropractic
                    if(detail is ChiropracticDetail) {
                        int detailId = (detail as ChiropracticDetail).Id;
                        var points = _session.QueryOver<PointOnChiropractic>()
                            .Where(p => p.ChiropracticDetail.Id == detailId)
                            .List<PointOnChiropractic>();
                        foreach(PointOnChiropractic p in points) {
                            //Update UpdateBy field so the audit listener would know who is deleting
                            if(_actionBy != null) p.UpdatedBy = _actionBy.DisplayName;
                            _session.Delete(p);
                        }
                    }
                    //3) Acupuncture
                    if (detail is AcupunctureDetail) {
                        int detailId = (detail as AcupunctureDetail).Id;
                        var points = _session.QueryOver<PointOnAcupuncture>()
                            .Where(p => p.AcupunctureDetail.Id == detailId)
                            .List<PointOnAcupuncture>();
                        foreach (PointOnAcupuncture p in points) {
                            //Update UpdateBy field so the audit listener would know who is deleting
                            if (_actionBy != null) p.UpdatedBy = _actionBy.DisplayName;
                            _session.Delete(p);
                        }
                    }
                    
                    //4) Massage
                    if(detail is MassageDetail) {
                        int detailId = (detail as MassageDetail).Id;
                        var points = _session.QueryOver<PointOnMassage>()
                            .Where(p => p.MassageDetail.Id == detailId)
                            .List<PointOnMassage>();
                        foreach(PointOnMassage p in points) {
                            //Update UpdateBy field so the audit listener would know who is deleting
                            if(_actionBy != null) p.UpdatedBy = _actionBy.DisplayName;
                            _session.Delete(p);
                        }
                    }

                    //5) Naturopathic
                    //No points for Naturopathic

                    //6) Osteopath
                    if (detail is OsteopathyDetail) {
                        int detailId = (detail as OsteopathyDetail).Id;
                        var points = _session.QueryOver<PointOnOsteopathy>()
                            .Where(p => p.OsteopathyDetail.Id == detailId)
                            .List<PointOnOsteopathy>();
                        foreach (PointOnOsteopathy p in points) {
                            //Update UpdateBy field so the audit listener would know who is deleting
                            if (_actionBy != null) p.UpdatedBy = _actionBy.DisplayName;
                            _session.Delete(p);
                        }
                    }


                    //TO DO: Other TherapyType

                    //Delete TreatmentDetail
                    //Update UpdateBy field so the audit listener would know who is deleting
                    if (_actionBy != null) (detail as EntityBase).UpdatedBy = _actionBy.DisplayName;
                    _session.Delete(detail);
                }
            } catch(Exception ex) {
                throw new ApplicationException("Delete TreatmentDetail Failed. " + ex.Message, ex);
            }
        }

        /*
         * Foreach InitialTreatment, there is ONLY ONE TreatmentDetail record.
         * It could be PhysiotherapyDetail, ChiropracticDetail and so on.
         * When updating an InitialTreatment with a different TreatmentType, the current TreatmentDetail will be left as an orphan record,
         * which should be removed.
         */
        public void DeleteOrphanTreatmentDetails(InitialTreatment it) {
            if (it == null) return;
            if (it.Id == default(int)) return;

            string treatmentType = it.TreatmentType != null ? it.TreatmentType.TherapyTypeName.Trim().ToLower()
                : string.Empty;

            try {
                //1) Physiotherapy
                if (!treatmentType.Equals(Constants.DEFINED_TREATMENT_TYPE_PHYSIOTHERAPY.ToLower())) {
                    /*
                    var details = _session.QueryOver<PhysiotherapyDetail>()
                        .Where(x => x.InitialTreatment.Id == it.Id)
                        .List<PhysiotherapyDetail>();
                    foreach(PhysiotherapyDetail d in details) {
                        //Update UpdateBy field so the audit listener would know who is deleting
                        if(_actionBy != null) d.UpdatedBy = _actionBy.DisplayName;
                        _session.Delete(d);
                    }*/

                    var detailIds = _session.QueryOver<PhysiotherapyDetail>()
                        .Where(x => x.InitialTreatment.Id == it.Id)
                        .Select(Projections.Id())
                        .List<int>();
                    foreach (int id in detailIds) {
                        var pointIds = _session.QueryOver<PointOnPhysiotherapy>()
                            .Where(x => x.PhysiotherapyDetail.Id == id)
                            .Select(Projections.Id())
                            .List<int>();
                        foreach (int pId in pointIds) {
                            _session.Delete(_session.Load<PointOnPhysiotherapy>(pId));
                        }
                        _session.Delete(_session.Load<PhysiotherapyDetail>(id));
                    }
                }

                //2) Chiropractic
                if (!(treatmentType.Equals(Constants.DEFINED_TREATMENT_TYPE_CHIROPRACTIC.ToLower()))
                    ) {
                    /*
                    var details = _session.QueryOver<ChiropracticDetail>()
                        .Where(x => x.InitialTreatment.Id == it.Id)
                        .List<ChiropracticDetail>();
                    foreach(ChiropracticDetail d in details) {
                        if(_actionBy != null) d.UpdatedBy = _actionBy.DisplayName;
                        _session.Delete(d);
                    }*/
                    var detailIds = _session.QueryOver<ChiropracticDetail>()
                        .Where(x => x.InitialTreatment.Id == it.Id)
                        .Select(Projections.Id())
                        .List<int>();
                    foreach (int id in detailIds) {
                        var pointIds = _session.QueryOver<PointOnChiropractic>()
                            .Where(x => x.ChiropracticDetail.Id == id)
                            .Select(Projections.Id())
                            .List<int>();
                        foreach (int pId in pointIds) {
                            _session.Delete(_session.Load<PointOnChiropractic>(pId));
                        }
                        _session.Delete(_session.Load<ChiropracticDetail>(id));
                    }
                }

                // Osteopathy
                if (!treatmentType.Equals(Constants.DEFINED_TREATMENT_TYPE_OSTEOPATH.ToLower())) {
                    /*
                    var details = _session.QueryOver<OsteopathyDetail>()
                        .Where(x => x.InitialTreatment.Id == it.Id)
                        .List<OsteopathyDetail>();
                    foreach (OsteopathyDetail d in details) {
                        if (_actionBy != null) d.UpdatedBy = _actionBy.DisplayName;
                        _session.Delete(d);
                    }*/
                    var detailIds = _session.QueryOver<OsteopathyDetail>()
                        .Where(x => x.InitialTreatment.Id == it.Id)
                        .Select(Projections.Id())
                        .List<int>();
                    foreach (int id in detailIds) {
                        var pointIds = _session.QueryOver<PointOnOsteopathy>()
                            .Where(x => x.OsteopathyDetail.Id == id)
                            .Select(Projections.Id())
                            .List<int>();
                        foreach (int pId in pointIds) {
                            _session.Delete(_session.Load<PointOnOsteopathy>(pId));
                        }
                        _session.Delete(_session.Load<OsteopathyDetail>(id));
                    }
                }

                //3) Acupuncture
                if (!treatmentType.Equals(Constants.DEFINED_TREATMENT_TYPE_ACUPUNCTURE.ToLower())) {
                    /*
                    var details = _session.QueryOver<AcupunctureDetail>()
                        .Where(x => x.InitialTreatment.Id == it.Id)
                        .List<AcupunctureDetail>();
                    foreach(AcupunctureDetail d in details) {
                        if(_actionBy != null) d.UpdatedBy = _actionBy.DisplayName;
                        _session.Delete(d);
                    }*/
                    var detailIds = _session.QueryOver<AcupunctureDetail>()
                        .Where(x => x.InitialTreatment.Id == it.Id)
                        .Select(Projections.Id())
                        .List<int>();
                    foreach (int id in detailIds) {
                        var pointIds = _session.QueryOver<PointOnAcupuncture>()
                            .Where(x => x.AcupunctureDetail.Id == id)
                            .Select(Projections.Id())
                            .List<int>();
                        foreach (int pId in pointIds) {
                            _session.Delete(_session.Load<PointOnAcupuncture>(pId));
                        }
                        _session.Delete(_session.Load<AcupunctureDetail>(id));
                    }
                }

                //4) Massage
                if (!treatmentType.Equals(Constants.DEFINED_TREATMENT_TYPE_MASSAGE.ToLower())) {
                    /*
                    var details = _session.QueryOver<MassageDetail>()
                        .Where(x => x.InitialTreatment.Id == it.Id)
                        .List<MassageDetail>();
                    foreach(MassageDetail d in details) {
                        if(_actionBy != null) d.UpdatedBy = _actionBy.DisplayName;
                        _session.Delete(d);
                    }*/
                    var detailIds = _session.QueryOver<MassageDetail>()
                        .Where(x => x.InitialTreatment.Id == it.Id)
                        .Select(Projections.Id())
                        .List<int>();
                    foreach (int id in detailIds) {
                        var pointIds = _session.QueryOver<PointOnMassage>()
                            .Where(x => x.MassageDetail.Id == id)
                            .Select(Projections.Id())
                            .List<int>();
                        foreach (int pId in pointIds) {
                            _session.Delete(_session.Load<PointOnMassage>(pId));
                        }
                        _session.Delete(_session.Load<MassageDetail>(id));
                    }
                }

                //5) Naturopathic
                if (!treatmentType.Equals(Constants.DEFINED_TREATMENT_TYPE_NATUROPATHIC.ToLower())) {
                    /*
                    var details = _session.QueryOver<NaturopathicDetail>()
                        .Where(x => x.InitialTreatment.Id == it.Id)
                        .List<NaturopathicDetail>();
                    foreach(NaturopathicDetail d in details) {
                        if(_actionBy != null) d.UpdatedBy = _actionBy.DisplayName;
                        _session.Delete(d);
                    }*/
                    var detailIds = _session.QueryOver<NaturopathicDetail>()
                        .Where(x => x.InitialTreatment.Id == it.Id)
                        .Select(Projections.Id())
                        .List<int>();
                    foreach (int id in detailIds) {
                        _session.Delete(_session.Load<NaturopathicDetail>(id));
                    }
                }

                //TO DO:
                //Other TherapyType

            } catch (Exception ex) {
                throw new ApplicationException("Delete Orphan TreatmentDetails Failed. " + ex.Message, ex);
            }
        }
        #endregion

        #region FollowUpTreatment
        public IFollowUpDetail GetFollowUpTreatmentDetail(FollowUpTreatment ft) {
            IFollowUpDetail detail = null;

            if(ft == null) return null;
            if(ft.Id == default(int)) return null;

            // 1) Chiropractic
            if( ft.InitialTreatment.TreatmentType.TherapyTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_CHIROPRACTIC.ToLower())) {
                /*
                var chiropracticFollowUpDetail = _session.QueryOver<ChiropracticFollowUpDetail>().List<ChiropracticFollowUpDetail>()
                    .Where(d => d.FollowUpTreatment.Id == ft.Id)
                    .SingleOrDefault();*/
                var chiropracticFollowUpDetail = _session.CreateCriteria<ChiropracticFollowUpDetail>("d")
                    .CreateCriteria("d.FollowUpTreatment", "f")
                    .CreateCriteria("d.ChiropracticSOAPDiagram", "dg")
                    .CreateCriteria("f.InitialTreatment", "it")
                    .CreateCriteria("it.TreatmentType", "tt")
                    .CreateCriteria("it.Patient", "p")
                    .CreateCriteria("p.Insurer", "i", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                    .CreateCriteria("f.Therapist", "t")
                    .CreateCriteria("t.Title", "title", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                    .Add(Restrictions.Eq("f.Id", ft.Id))
                    .SetMaxResults(1)
                    .List<ChiropracticFollowUpDetail>()
                    .SingleOrDefault();

                detail = chiropracticFollowUpDetail;
            }

            // Osteopathy
            if (ft.InitialTreatment.TreatmentType.TherapyTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_OSTEOPATH.ToLower())) {
                var ostFollowUpDetail = _session.CreateCriteria<OsteopathyFollowUpDetail>("d")
                    .CreateCriteria("d.FollowUpTreatment", "f")
                    .CreateCriteria("f.InitialTreatment", "it")
                    .CreateCriteria("it.TreatmentType", "tt")
                    .CreateCriteria("it.Patient", "p")
                    .CreateCriteria("p.Insurer", "i", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                    .CreateCriteria("f.Therapist", "t")
                    .CreateCriteria("t.Title", "title", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                    .Add(Restrictions.Eq("f.Id", ft.Id))
                    .SetMaxResults(1)
                    .List<OsteopathyFollowUpDetail>()
                    .SingleOrDefault();

                detail = ostFollowUpDetail;
            }
            
            // 2) Physiotherapy
            if(ft.InitialTreatment.TreatmentType.TherapyTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_PHYSIOTHERAPY.ToLower())) {
                //There is no Physiotherapy FollowUp Detail
                detail = null;
            }

            // 3) Acupuncture
            if(ft.InitialTreatment.TreatmentType.TherapyTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_ACUPUNCTURE.ToLower())) {
                var acupunctureFollowUpDetail = _session.CreateCriteria<AcupunctureFollowUpDetail>("d")
                    .CreateCriteria("d.FollowUpTreatment", "f")
                    .CreateCriteria("d.TongueDiagram", "dg")
                    .CreateCriteria("f.InitialTreatment", "it")
                    .CreateCriteria("it.TreatmentType", "tt")
                    .CreateCriteria("it.Patient", "p")
                    .CreateCriteria("p.Insurer", "i", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                    .CreateCriteria("f.Therapist", "t")
                    .CreateCriteria("t.Title", "title", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                    .Add(Restrictions.Eq("f.Id", ft.Id))
                    .SetMaxResults(1)
                    .List<AcupunctureFollowUpDetail>()
                    .SingleOrDefault();

                detail = acupunctureFollowUpDetail;
            }

            // 4) Massage
            if(ft.InitialTreatment.TreatmentType.TherapyTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_MASSAGE.ToLower())) {
                /*
                var massageFollowUpDetail = _session.QueryOver<MassageFollowUpDetail>()
                    .List<MassageFollowUpDetail>()
                    .Where(d => d.FollowUpTreatment.Id == ft.Id)
                    .SingleOrDefault();*/
                var massageFollowUpDetail = _session.CreateCriteria<MassageFollowUpDetail>("d")
                    .CreateCriteria("d.FollowUpTreatment", "f")
                    .CreateCriteria("f.InitialTreatment", "it")
                    .CreateCriteria("it.TreatmentType", "tt")
                    .CreateCriteria("it.Patient", "p")
                    .CreateCriteria("p.Insurer", "i", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                    .CreateCriteria("f.Therapist", "t")
                    .CreateCriteria("t.Title", "title", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                    .Add(Restrictions.Eq("f.Id", ft.Id))
                    .SetMaxResults(1)
                    .List<MassageFollowUpDetail>()
                    .SingleOrDefault();


                detail = massageFollowUpDetail;
            }

            // 5) Naturopathic
            if(ft.InitialTreatment.TreatmentType.TherapyTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_NATUROPATHIC.ToLower())) {
                //There is no Naturopathic FollowUp detail
                detail = null;
            }

            //TO DO: Add other TreatmentTypes if there is any

            return detail;
        }

        public void DeleteFollowUpTreatment(FollowUpTreatment ft) {
            try {
                FollowUpTreatment ftToDelete = _session.Get<FollowUpTreatment>(ft.Id);

                //Delete all related followup treatment details
                DeleteFollowUpDetail(ft);

                //Delete InitialTreatment
                if(_actionBy != null) ftToDelete.UpdatedBy = _actionBy.DisplayName;
                _session.Delete(ftToDelete);

            } catch(Exception ex) {
                throw new ApplicationException("Delete FollowUpTreatment Failed. " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Before updating an InitialTreatment to a new treatment type, the existing follow-up details should be deleted.
        /// NOTE: The FollowUpTreatments will not be changed. Only the Details will be deleted.
        /// </summary>
        /// <param name="InitialTreatmentId"></param>
        /// <param name="OldTreatmentType"></param>
        public void DeleteAllFollowUpDetails(int InitialTreatmentId, string OldTreatmentType) {
            try {
                //Get all followup treatment ids
                var ftIds = _session.QueryOver<FollowUpTreatment>()
                    .Where(x => x.InitialTreatment.Id == InitialTreatmentId)
                    .Select(Projections.Id())
                    .List<int>();

                if (ftIds.Count > 0) {

                    //Get all followup details
                    if (OldTreatmentType.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_ACUPUNCTURE.ToLower())) {
                        var details = _session.QueryOver<AcupunctureFollowUpDetail>()
                            .WhereRestrictionOn(x => x.FollowUpTreatment.Id).IsIn(ftIds.ToArray())
                            .List();
                        foreach (var detail in details) {
                            var points = _session.QueryOver<PointOnAcupunctureFollowUp>()
                                .Where(x => x.AcupunctureFollowUpDetail.Id == detail.Id)
                                .List();
                            foreach (var point in points) {
                                _session.Delete(point);
                            }
                            _session.Delete(detail);
                        }
                    } else if (OldTreatmentType.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_CHIROPRACTIC.ToLower())) {
                        var details = _session.QueryOver<ChiropracticFollowUpDetail>()
                            .WhereRestrictionOn(x => x.FollowUpTreatment.Id).IsIn(ftIds.ToArray())
                            .List();
                        foreach (var detail in details) {
                            var points = _session.QueryOver<PointOnChiropracticFollowUp>()
                                .Where(x => x.ChiropracticFollowUpDetail.Id == detail.Id)
                                .List();
                            foreach (var point in points) {
                                _session.Delete(point);
                            }
                            _session.Delete(detail);
                        }
                    } else if (OldTreatmentType.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_MASSAGE.ToLower())) {
                        var details = _session.QueryOver<MassageFollowUpDetail>()
                            .WhereRestrictionOn(x => x.FollowUpTreatment.Id).IsIn(ftIds.ToArray())
                            .List();
                        foreach (var detail in details) {
                            _session.Delete(detail);
                        }
                    } else if (OldTreatmentType.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_OSTEOPATH.ToLower())) {
                        var details = _session.QueryOver<OsteopathyFollowUpDetail>()
                            .WhereRestrictionOn(x => x.FollowUpTreatment.Id).IsIn(ftIds.ToArray())
                            .List();
                        foreach (var detail in details) {
                            _session.Delete(detail);
                        }
                    } else {
                        //Do nothing. Other types do NOT have followup details
                    }

                }

            } catch (Exception ex) {
                throw new ApplicationException("Delete old treatment details failed. " + ex.Message, ex);
            }
        }

        public void DeleteFollowUpDetail(FollowUpTreatment ft) {
            try {
                IFollowUpDetail detail = GetFollowUpTreatmentDetail(ft);
                if(detail != null) {

                    //Delete all points first
                    
                    //1) Chiropractic
                    if(detail is ChiropracticFollowUpDetail) {
                        int detailId = (detail as ChiropracticFollowUpDetail).Id;
                        var points = _session.QueryOver<PointOnChiropracticFollowUp>()
                            .Where(p => p.ChiropracticFollowUpDetail.Id == detailId)
                            .List<PointOnChiropracticFollowUp>();
                        foreach(PointOnChiropracticFollowUp p in points) {
                            if(_actionBy != null) p.UpdatedBy = _actionBy.DisplayName;
                            _session.Delete(p);
                        }
                    }

                    //2) Acupuncture
                    if (detail is AcupunctureFollowUpDetail) {
                        int detailId = (detail as AcupunctureFollowUpDetail).Id;
                        var points = _session.QueryOver<PointOnAcupunctureFollowUp>()
                            .Where(p => p.AcupunctureFollowUpDetail.Id == detailId)
                            .List<PointOnAcupunctureFollowUp>();
                        foreach (PointOnAcupunctureFollowUp p in points) {
                            if (_actionBy != null) p.UpdatedBy = _actionBy.DisplayName;
                            _session.Delete(p);
                        }
                    }

                    //TO DO: Other TherapyType (Currently, only Chiropractic SOAP and Acupuncture SOAP have diagrams on follow up treatments

                    //Delete TreatmentDetail
                    if(_actionBy != null) (detail as EntityBase).UpdatedBy = _actionBy.DisplayName;
                    _session.Delete(detail);
                }
            } catch(Exception ex) {
                throw new ApplicationException("Delete TreatmentDetail Failed. " + ex.Message, ex);
            }
        }
        #endregion

        #region Get Diagram Points

        public IList<PointOnDiagram> GetAcupunctureTonguePoints(AcupunctureDetail detail, int maxResults) {
            IList<PointOnDiagram> points = null;
            Diagram dg = _session.Merge<Diagram>(detail.TongueDiagram);
            try {
                var dgPoints = _session.QueryOver<PointOnAcupuncture>()
                    .Where(p => p.Diagram.Id == dg.Id)
                    .And(p => p.AcupunctureDetail.Id == detail.Id)
                    .Take(maxResults);

                points = dgPoints.List<PointOnDiagram>();
            } catch (Exception ex) {
                throw new ApplicationException("Failed to retrieve points. " + ex.Message);
            }

            return points;
        }

        public IList<PointOnDiagram> GetAcupunctureFollowUpTonguePoints(AcupunctureFollowUpDetail detail, int maxResults) {
            IList<PointOnDiagram> points = null;
            Diagram dg = _session.Merge<Diagram>(detail.TongueDiagram);
            try {
                var dgPoints = _session.QueryOver<PointOnAcupunctureFollowUp>()
                    .Where(p => p.Diagram.Id == dg.Id)
                    .And(p => p.AcupunctureFollowUpDetail.Id == detail.Id)
                    .Take(maxResults);

                points = dgPoints.List<PointOnDiagram>();
            } catch (Exception ex) {
                throw new ApplicationException("Failed to retrieve points. " + ex.Message);
            }

            return points;
        }

        public IList<PointOnDiagram> GetChiropracticSOAPPoints(ChiropracticFollowUpDetail detail, int maxResults) {
            IList<PointOnDiagram> points = null;
            Diagram dg = _session.Merge<Diagram>(detail.ChiropracticSOAPDiagram);
            try {
                var dgPoints = _session.QueryOver<PointOnChiropracticFollowUp>()
                    .Where(p => p.Diagram.Id == dg.Id)
                    .And(p => p.ChiropracticFollowUpDetail.Id == detail.Id)
                    .Take(maxResults);


                points = dgPoints.List<PointOnDiagram>();
            } catch(Exception ex) {
                throw new ApplicationException("Failed to retrieve pin points. " + ex.Message);
            }

            return points;
        }

        public IList<PointOnDiagram> GetChiropracticPoints(ChiropracticDetail detail, int maxResults) {
            IList<PointOnDiagram> points = null;
            Diagram dg = _session.Merge<Diagram>(detail.ChiropracticDiagram);
            try {
                var dgPoints = _session.QueryOver<PointOnChiropractic>()
                    .Where(p => p.Diagram.Id == dg.Id)
                    .And(p => p.ChiropracticDetail.Id == detail.Id)
                    .Take(maxResults);

                points = dgPoints.List<PointOnDiagram>();
            } catch(Exception ex) {
                throw new ApplicationException("Failed to retrieve pin points. " + ex.Message);
            }

            return points;
        }

        public IList<PointOnDiagram> GetPhysiotherapyPoints(PhysiotherapyDetail detail, Diagram dg, int maxResults) {
            IList<PointOnDiagram> points = null;

            try {
                var dgPoints = _session.QueryOver<PointOnPhysiotherapy>()
                    .Where(p => p.Diagram.Id == dg.Id)
                    .And(p => p.PhysiotherapyDetail.Id == detail.Id)
                    .Take(maxResults);

                points = dgPoints.List<PointOnDiagram>();
            } catch(Exception ex) {
                throw new ApplicationException("Failed to retrieve pin points. " + ex.Message);
            }

            return points;
        }

        public IList<PointOnDiagram> GetMassagePoints(MassageDetail detail, Diagram dg, int maxResults) {
            IList<PointOnDiagram> points = null;

            try {
                var dgPoints = _session.QueryOver<PointOnMassage>()
                    .Where(p => p.Diagram.Id == dg.Id)
                    .And(p => p.MassageDetail.Id == detail.Id)
                    .Take(maxResults);

                points = dgPoints.List<PointOnDiagram>();
            } catch(Exception ex) {
                throw new ApplicationException("Failed to retrieve pin points. " + ex.Message);
            }

            return points;
        }

        public IList<PointOnDiagram> GetOsteopathyPoints(OsteopathyDetail detail, Diagram dg, int maxResults) {
            IList<PointOnDiagram> points = null;

            try {
                var dgPoints = _session.QueryOver<PointOnOsteopathy>()
                    .Where(p => p.Diagram.Id == dg.Id)
                    .And(p => p.OsteopathyDetail.Id == detail.Id)
                    .Take(maxResults);

                points = dgPoints.List<PointOnDiagram>();
            } catch (Exception ex) {
                throw new ApplicationException("Failed to retrieve pin points. " + ex.Message);
            }

            return points;
        }

        #endregion Get Diagram Points

        /*
        /// <summary>
        /// Check if the given practitioner is available at the given time range
        /// </summary>
        /// <param name="Practitioner"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public AvailableStatus CheckPractitionerAvailability(User Practitioner, DateTime StartTime, DateTime EndTime, 
            AvailabilityCheckType CheckType,
            int CurrentItemId = default(int)) {

            AvailableStatus ret = AvailableStatus.NoneWorkingHour;

            //Check if it's holiday
            if (CanadaHolidays.IsHoliday(StartTime.Date)) {
                ret = AvailableStatus.Holiday;
            } else {//Check if the practitioner is available
                IList<UserWeeklyAvailability> avails = _session.QueryOver<UserWeeklyAvailability>()
                    .Where(x => x.Practitioner.Id == Practitioner.Id)
                    .Where(x => x.WeekDay == StartTime.DayOfWeek)
                    .List<UserWeeklyAvailability>();

                foreach (UserWeeklyAvailability avail in avails) {
                    if (avail.FullDayAvailable) {
                        ret = AvailableStatus.Available;
                        break;
                    } else {
                        decimal treatmentStartHour = (decimal)StartTime.Hour + (decimal)StartTime.Minute / 60;
                        decimal treatmentEndHour = (decimal)EndTime.Hour + (decimal)EndTime.Minute / 60;

                        if (avail.StartHour <= treatmentStartHour && avail.EndHour >= treatmentEndHour) {
                            ret = AvailableStatus.Available;
                            break;
                        }
                    }
                }

                //Continue to check time offs
                if (ret == AvailableStatus.Available) {

                    int timeOffCount = _session.QueryOver<UserTimeOff>()
                        .Where(x => x.Practitioner.Id == Practitioner.Id)
                        .Where(x => (x.StartTime >= StartTime && x.EndTime <= EndTime) ||
                                    (x.StartTime < StartTime && x.EndTime > StartTime) ||
                                    (x.StartTime < EndTime && x.EndTime > EndTime))
                        .RowCount();

                    if (timeOffCount > 0) {
                        ret = AvailableStatus.TimeOff;
                    }

                }

                //Continue to check if the time slot has been taken by other treatments/bookings
                if (ret == AvailableStatus.Available) {
                    switch (CheckType) {
                        case AvailabilityCheckType.Treatment:
                            //Check Initial Treatments
                            var initTreatments = _session.QueryOver<InitialTreatment>()
                                .Where(x => x.Therapist.Id == Practitioner.Id)
                                .And(x => x.TreatmentTime >= StartTime.Date)
                                .And(x => x.TreatmentTime < StartTime.Date.AddDays(1))
                                .And(x => x.Id != CurrentItemId)
                                .List<InitialTreatment>();
                            foreach (InitialTreatment it in initTreatments) {
                                if (it.TreatmentTime >= EndTime || it.TreatmentTime.AddMinutes(it.TreatmentDurationMinutes) <= StartTime) {
                                    continue;
                                } else {
                                    ret = AvailableStatus.NotAvailable;
                                    break;
                                }
                            }
                            //Continue to check followup treatments
                            if (ret == AvailableStatus.Available) {
                                var fuTreatments = _session.QueryOver<FollowUpTreatment>()
                                    .Where(x => x.Therapist.Id == Practitioner.Id)
                                    .And(x => x.TreatmentTime >= StartTime.Date)
                                    .And(x => x.TreatmentTime < StartTime.Date.AddDays(1))
                                    .And(x => x.Id != CurrentItemId)
                                    .List<FollowUpTreatment>();
                                foreach (FollowUpTreatment ft in fuTreatments) {
                                    if (ft.TreatmentTime >= EndTime || ft.TreatmentTime.AddMinutes(ft.TreatmentDurationMinutes) <= StartTime) {
                                        continue;
                                    } else {
                                        ret = AvailableStatus.NotAvailable;
                                        break;
                                    }
                                }
                            }
                            break;
                        case AvailabilityCheckType.Booking:
                            var bookings = _session.QueryOver<Booking>()
                                .Where(x => x.Therapist.Id == Practitioner.Id)
                                .And(x => x.StartTime >= StartTime.Date)
                                .And(x => x.StartTime < StartTime.Date.AddDays(1))
                                .And(x => x.Id != CurrentItemId)
                                .List<Booking>();
                            foreach (Booking bk in bookings) {
                                if (bk.StartTime >= EndTime || bk.EndTime <= StartTime) {
                                    continue;
                                } else {
                                    ret = AvailableStatus.NotAvailable;
                                    break;
                                }
                            }
                            break;
                        case AvailabilityCheckType.Invoice:
                            //Ignore. Invoices have date only, not time range. So, on the same day, there could be multiple invoices for the same practitioner. 
                            break;
                    }
                }
            }

            return ret;
        }
        */

        /// <summary>
        /// Check if the given practitioner is available at the given time range
        /// </summary>
        /// <param name="Practitioner"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns>Return a list of unavailable status codes. Return empty list if no problems found.</returns>
        public List<AvailableStatus> CheckPractitionerAvailability(User Practitioner, DateTime StartTime, DateTime EndTime,
            AvailabilityCheckType CheckType,
            int CurrentItemId = default(int),
            bool checkOverlap = true) {

            List<AvailableStatus> ret = new List<AvailableStatus>();

            //Check if there is time overlapping error first
            if (checkOverlap) {
                switch (CheckType) {
                    case AvailabilityCheckType.Treatment:
                        //Check Initial Treatments
                        var initTreatments = _session.QueryOver<InitialTreatment>()
                            .Where(x => x.Therapist.Id == Practitioner.Id)
                            .And(x => x.TreatmentTime >= StartTime.Date)
                            .And(x => x.TreatmentTime < StartTime.Date.AddDays(1))
                            .And(x => x.Id != CurrentItemId)
                            .Inner.JoinQueryOver<TherapyType>(x => x.TreatmentType)
                            .Where(x => x.AllowTimeOverlap == false)
                            .List<InitialTreatment>();
                        foreach (InitialTreatment it in initTreatments) {
                            if (it.TreatmentTime >= EndTime || it.TreatmentTime.AddMinutes(it.TreatmentDurationMinutes) <= StartTime) {
                                continue;
                            } else {
                                ret.Add(AvailableStatus.NotAvailable);
                                break;
                            }
                        }

                        //Continue to check followup treatments
                        if (!ret.Contains(AvailableStatus.NotAvailable)) {
                            var fuTreatments = _session.QueryOver<FollowUpTreatment>()
                                .Where(x => x.Therapist.Id == Practitioner.Id)
                                .And(x => x.TreatmentTime >= StartTime.Date)
                                .And(x => x.TreatmentTime < StartTime.Date.AddDays(1))
                                .And(x => x.Id != CurrentItemId)
                                .Inner.JoinQueryOver<InitialTreatment>(x => x.InitialTreatment)
                                .Inner.JoinQueryOver<TherapyType>(x => x.TreatmentType)
                                .Where(x => x.AllowTimeOverlap == false)
                                .List<FollowUpTreatment>();
                            foreach (FollowUpTreatment ft in fuTreatments) {
                                if (ft.TreatmentTime >= EndTime || ft.TreatmentTime.AddMinutes(ft.TreatmentDurationMinutes) <= StartTime) {
                                    continue;
                                } else {
                                    ret.Add(AvailableStatus.NotAvailable);
                                    break;
                                }
                            }
                        }
                        break;
                    case AvailabilityCheckType.Booking:
                        var bookings = _session.QueryOver<Booking>()
                            .Where(x => x.Therapist.Id == Practitioner.Id)
                            .And(x => x.StartTime >= StartTime.Date)
                            .And(x => x.StartTime < StartTime.Date.AddDays(1))
                            .And(x => x.Id != CurrentItemId)
                            .List<Booking>();
                        foreach (Booking bk in bookings) {
                            if (bk.StartTime >= EndTime || bk.EndTime <= StartTime) {
                                continue;
                            } else {
                                ret.Add(AvailableStatus.NotAvailable);
                                break;
                            }
                        }
                        break;
                    case AvailabilityCheckType.Invoice:
                        //Use special logic. Refer to EditInvoiceItemForm.cs
                        break;
                }
            }

            //Check if it's holiday
            if (CanadaHolidays.IsHoliday(StartTime.Date)) {
                ret.Add(AvailableStatus.Holiday);
            } 
            
            //Check if it's not the practitioner's regular working hours
            IList<UserWeeklyAvailability> avails = _session.QueryOver<UserWeeklyAvailability>()
                .Where(x => x.Practitioner.Id == Practitioner.Id)
                .Where(x => x.WeekDay == StartTime.DayOfWeek)
                .List<UserWeeklyAvailability>();

            bool checkOk = false;
            foreach (UserWeeklyAvailability avail in avails) {
                if (avail.FullDayAvailable) {
                    checkOk = true;
                    break;
                } else {
                    decimal treatmentStartHour = (decimal)StartTime.Hour + (decimal)StartTime.Minute / 60;
                    decimal treatmentEndHour = (decimal)EndTime.Hour + (decimal)EndTime.Minute / 60;

                    if (avail.StartHour <= treatmentStartHour && avail.EndHour >= treatmentEndHour) {
                        checkOk = true;
                        break;
                    }
                }
            }
            if (!checkOk) ret.Add(AvailableStatus.NoneWorkingHour);

            //Continue to check time offs
            int timeOffCount = _session.QueryOver<UserTimeOff>()
                .Where(x => x.Practitioner.Id == Practitioner.Id)
                .Where(x => (x.StartTime >= StartTime && x.EndTime <= EndTime) ||
                            (x.StartTime < StartTime && x.EndTime > StartTime) ||
                            (x.StartTime < EndTime && x.EndTime > EndTime))
                .RowCount();

            if (timeOffCount > 0) {
                ret.Add(AvailableStatus.TimeOff);
            }

            return ret;
        }

        /// <summary>
        /// Retrieve all avialable time ranges during the day for the given practitioner
        /// </summary>
        /// <param name="Practitioner"></param>
        /// <returns></returns>
        public TimeRangeCollection GetPractitionerAvailableHours(User Practitioner, DateTime CheckDate, AvailabilityCheckType CheckType, bool CheckPractitionerCalendar) {

            TimeRangeCollection timeRanges = new TimeRangeCollection();

            if (CheckType != AvailabilityCheckType.Booking)
                throw new ArgumentException("Only AvailabilityCheckType.Booking is supported for now.");

            //If it's holiday, nobody is working
            if (CanadaHolidays.IsHoliday(CheckDate.Date)) return timeRanges;

            //Check the practitioner's availability calendar
            if (CheckPractitionerCalendar) {

                //regular available hours
                IList<UserWeeklyAvailability> avails = _session.QueryOver<UserWeeklyAvailability>()
                    .Where(x => x.Practitioner.Id == Practitioner.Id)
                    .Where(x => x.WeekDay == CheckDate.DayOfWeek)
                    .List<UserWeeklyAvailability>();

                foreach (UserWeeklyAvailability avail in avails) {
                    if (avail.FullDayAvailable) {
                        timeRanges.Clear();
                        timeRanges.Add(new TimeRange(CheckDate.Date, CheckDate.Date.AddDays(1)));
                        break;
                    } else {
                        timeRanges.Add(new TimeRange(
                            new DateTime(CheckDate.Year, CheckDate.Month, CheckDate.Day, Convert.ToInt32(Math.Floor(avail.StartHour)), Convert.ToInt32((avail.StartHour - Math.Floor(avail.StartHour)) * 60), 0),
                            new DateTime(CheckDate.Year, CheckDate.Month, CheckDate.Day, Convert.ToInt32(Math.Floor(avail.EndHour)), Convert.ToInt32((avail.EndHour - Math.Floor(avail.EndHour)) * 60), 0)
                            ));
                    }
                }

                //remove time offs from available hours
                IList<UserTimeOff> timeOffs = _session.QueryOver<UserTimeOff>()
                    .Where(x => x.Practitioner.Id == Practitioner.Id)
                    .Where(x => x.StartTime >= CheckDate.Date && x.EndTime <= CheckDate.Date.AddDays(1))
                    .List<UserTimeOff>();

                foreach (UserTimeOff timeOff in timeOffs) {
                    timeRanges.Crop(new TimeRange(timeOff.StartTime, timeOff.EndTime));
                }

            } else {//If Practitioner Calendar is not enabled, assume practitioners always available
                timeRanges.Add(new TimeRange(CheckDate.Date, CheckDate.Date.AddDays(1)));
            }

            //remove all booked hours
            var bookings = _session.QueryOver<Booking>()
                .Where(x => x.Therapist.Id == Practitioner.Id)
                .And(x => x.StartTime >= CheckDate.Date)
                .And(x => x.StartTime < CheckDate.Date.AddDays(1))
                .List<Booking>();
            foreach (Booking bk in bookings) {
                timeRanges.Crop(new TimeRange(bk.StartTime, bk.EndTime));
            }

            return timeRanges;
        }

        public enum AvailableStatus {
            Available, //Available
            NotAvailable, //Not available (the time slot has been booked off)
            NoneWorkingHour, //The pratitioner will not work during this time range on the given week day
            TimeOff, //The practitioner has booked time off (e.g. vacation)
            Holiday //It's a holiday, nobody should be working
        }

        public enum AvailabilityCheckType {
            Treatment,
            Invoice,
            Booking
        }

    }
}
