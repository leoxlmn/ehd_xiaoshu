using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EHD.Model.Entity;
using NHibernate;
using EHD.Repository;
using Utility;
using NHibernate.Transform;
using NHibernate.Criterion;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using NHibernate.SqlCommand;

namespace EHD.Admin {
    public partial class EditAccountBalanceForm : Form {
        private Patient _patient;

        int pageCount = 1;
        int rowIndexToPrint = 0;
        double fTotalSpend = 0;
        double fTotalDeposit = 0;
        PrintAction printAction = PrintAction.PrintToPrinter;

        public EditAccountBalanceForm(Patient patient) {
            InitializeComponent();

            _patient = patient;

            if (_patient == null || _patient.Id == default(int)) {
                throw new ApplicationException("Failed to open the account balance screen as the patient does NOT exist.");
            }
        }

        public EditAccountBalanceForm(int patientId) {
            InitializeComponent();

            if (patientId == default(int))
                throw new ApplicationException("patientId cannot be 0.");

            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {

                try {
                    _patient = session.Get<Patient>(patientId);
                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, ex.Message);
                    Program.log.Error("Populate patient account balance error.", ex);
                }
            }

            if (_patient == null || _patient.Id == default(int)) {
                throw new ApplicationException("Failed to open the account balance screen as the patient does NOT exist.");
            }
        }

        private void loadTransaction() {

            //Get the depost and cost

            IList<Transaction> lstTransaction = null;

            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {

                try {
                    //Get the deposts from AccountBalance
                    Transaction aliasTran = null;
                    lstTransaction = session.QueryOver<AccountBalance>()
                        .Where(x => x.Patient.Id == _patient.Id)
                        .SelectList(list => list
                            .Select(x => x.Id).WithAlias(() => aliasTran.AccountBalanceId)
                            .Select(x => x.TransactionTime).WithAlias(() => aliasTran.TransactionTime)
                            .Select(x => x.TransactionDescription).WithAlias(() => aliasTran.TransactionDescription)
                            .Select(x => x.TransactionAmount).WithAlias(() => aliasTran.DepositSpendAmount)
                            .Select(x => x.CreatedBy).WithAlias(() => aliasTran.EnteredBy)
                            .Select(x => x.CreatedTime).WithAlias(() => aliasTran.EnteredTime)
                        )
                        .TransformUsing(Transformers.AliasToBean<Transaction>())
                        .OrderBy(x => x.TransactionTime).Asc
                        .ThenBy(x => x.CreatedTime).Asc
                        .List<Transaction>();

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, ex.Message);
                    Program.log.Error("Populate patient account balance error.", ex);
                }
            }

            //Calculate the balance
            double total = 0;
            for (int i = 0; i < lstTransaction.Count; i++) {
                Transaction tran = lstTransaction[i];
                if (tran.DepositSpendAmount.HasValue) {
                    total += tran.DepositSpendAmount.Value;
                }
                tran.Balance = total;
            }

            lblBalance.Text = total.ToString("0.##");
            lblBalance.ForeColor = (total >= 0 ? Color.Green : Color.Red);

            grdTransaction.DataMember = "";
            grdTransaction.DataSource = lstTransaction;
        }

        private void EditAccountBalanceForm_Load(object sender, EventArgs e) {
            this.lblPatientName.Text = _patient.DisplayName;

            loadTransaction();
        }

        private void grdTransaction_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            //Hide id columns
            grdTransaction.Columns[0].Visible = false;

            //Highlight numbers
            Font fontBold = new Font(grdTransaction.Font, FontStyle.Bold);
            for (int r = 0; r < grdTransaction.Rows.Count; r++) {
                setMoneyColumn(r, 3, fontBold);
                setMoneyColumn(r, 4, fontBold);
            }

        }

        private void setMoneyColumn(int rowIndex, int colIndex, Font defaultFont) {
            DataGridViewCell c = grdTransaction.Rows[rowIndex].Cells[colIndex];
            //c.Style.Font = defaultFont; //Setting new Font cost too much time
            c.Style.ForeColor = (double)c.Value > 0 ? Color.Green : Color.Red;
        }

        private void btnAddDeposit_Click(object sender, EventArgs e) {
            (new AddDepositForm(_patient)).ShowDialog(this);
            loadTransaction();
        }

        private void btnDeleteDeposit_Click(object sender, EventArgs e) {
            if (grdTransaction.SelectedRows.Count <= 0) return;

            Transaction tran = grdTransaction.SelectedRows[0].DataBoundItem as Transaction;

            if (DialogResult.Yes != MessageBox.Show(this,
                string.Format("Do you want to delete this Deposit/Spend [{0:MMM dd, yyyy} - {1:0.##}]?", tran.TransactionTime, tran.DepositSpendAmount),
                "Delete Deposit/Spend",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)) return;

            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    //Get the AccountBalance record
                    AccountBalance depositToDelete = session.Get<AccountBalance>(tran.AccountBalanceId);

                    //Force to load the Invoice collection.
                    //!!! These 2 lines of code will fix the "collection ...Patient.Invoices was not processed by flush()" error !!!!
                    //!!! This error caused by the AuditListener -> JSONHelper.GetJSON(entity); !!!
                    session.CreateCriteria<Invoice>()
                        .Add(Restrictions.Eq("Patient.Id", depositToDelete.Patient.Id));
                    
                    depositToDelete.UpdatedBy = Program.LogonUser.DisplayName;
                    depositToDelete.UpdatedTime = DateTime.Now;
                    session.Delete(depositToDelete);
                    
                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to delete deposit. " + ex.Message);
                    Program.log.Error("Failed to delete deposit.", ex);
                }
            }

            loadTransaction();
        }

        //Inner class used to populate the datagrid
        class Transaction {
            public int? AccountBalanceId { get; set; }
            //public int? InvoiceItemId { get; set; }
            public DateTime? TransactionTime { get; set; }
            public string TransactionDescription { get; set; }
            public double? DepositSpendAmount { get; set; }
            //public string TreatmentName { get; set; }
            //public double? TreatmentCost { get; set; }
            public double? Balance { get; set; }
            public string EnteredBy { get; set; }
            public DateTime? EnteredTime { get; set; }
        }

        private void btnPrint_Click(object sender, EventArgs e) {
            printBalance(false);
        }

        private void printDoc_PrintPage(object sender, PrintPageEventArgs e) {
            if (grdTransaction.Rows.Count<= 0) return;

            #region Get actual printable area
            RectangleF marginBounds = e.MarginBounds;
            RectangleF printableArea = e.PageSettings.PrintableArea;

            //Adjust starting print position for print preview screen
            if (printAction == PrintAction.PrintToPreview) {
                e.Graphics.TranslateTransform(printableArea.X, printableArea.Y);
            }

            //Get the maximum printable size
            int availableWidth = (int)Math.Floor(printDoc.OriginAtMargins
                ? marginBounds.Width
                : (e.PageSettings.Landscape
                ? printableArea.Height
                : printableArea.Width));
            int availableHeight = (int)Math.Floor(printDoc.OriginAtMargins
                ? marginBounds.Height
                : (e.PageSettings.Landscape
                ? printableArea.Width
                : printableArea.Height));
            #endregion Get actual printable area

            //
            int clientHeight = e.MarginBounds.Height;
            int clientWidth = e.MarginBounds.Width;

            float top = e.MarginBounds.Top - (int)printableArea.Y;
            int left = e.MarginBounds.Left - (int)printableArea.X;

            Font fontLg = new Font("Arial", 15);
            Font fontLgBold = new Font("Arial", 15, FontStyle.Bold);
            Font fontMd = new Font("Arial", 12);
            Font fontMdBold = new Font("Arial", 12, FontStyle.Bold);
            Font fontSm = new Font("Arial", 10);
            Font fontSmBold = new Font("Arial", 10, FontStyle.Bold);
            Brush brushToPrint = Brushes.Black;
            Brush brushBG = Brushes.LightGray;

            Graphics gc = e.Graphics;

            if (pageCount == 1) {
                top += printPageHeader(e);
            }

            //5) Print details table
            //Print header row
            top += (int)printRow(e, left, top, brushToPrint, fontMdBold, new string[] { "Date", "Description", "Charge", "Deposit", "Balance"}, true);

            //Print data rows
            while (rowIndexToPrint < grdTransaction.Rows.Count && top <= clientHeight) {

                Transaction tr = grdTransaction.Rows[rowIndexToPrint].DataBoundItem as Transaction;

                if (tr.DepositSpendAmount > 0) {
                    fTotalDeposit += tr.DepositSpendAmount.Value;
                } else {
                    fTotalSpend += Math.Abs(tr.DepositSpendAmount.Value);
                }
                
                top += (int)printRow(e, left, top, brushToPrint, fontMd, new string[] {
                    tr.EnteredTime.Value.ToString("yyyy-MM-dd"),
                    tr.TransactionDescription,
                    tr.DepositSpendAmount.Value < 0 ? Math.Abs(tr.DepositSpendAmount.Value).ToString("0.00") : "",
                    tr.DepositSpendAmount.Value > 0 ? tr.DepositSpendAmount.Value.ToString("0.00") : "",
                    tr.Balance.Value.ToString("0.00") });

                rowIndexToPrint++;
            }

            //Print totals row
            if (top > clientHeight) {
                e.HasMorePages = true;
                pageCount++;
            } else {
                top += 15; //Add padding
                if (top >= clientHeight) {
                    e.HasMorePages = true;
                    pageCount++;
                } else {
                    top += (int)printRow(e, left, top, brushToPrint, fontMd, new string[] {
                    "",
                    "            *** Totals ***",
                    fTotalSpend.ToString("0.00"),
                    fTotalDeposit.ToString("0.00"),
                    ""});

                    resetPrintVariables();
                }
            }
        }

        private float printPageHeader(PrintPageEventArgs e) {

            int clientHeight = e.MarginBounds.Height;
            int clientWidth = e.MarginBounds.Width;

            float top = e.MarginBounds.Top;
            int left = e.MarginBounds.Left;

            const int MARGIN_LEFT = 30;

            Font fontLg = new Font("Arial", 15);
            Font fontLgBold = new Font("Arial", 15, FontStyle.Bold);
            Font fontMd = new Font("Arial", 12);
            Font fontMdBold = new Font("Arial", 12, FontStyle.Bold);
            Font fontSm = new Font("Arial", 10);
            Font fontSmBold = new Font("Arial", 10, FontStyle.Bold);
            Brush brushToPrint = Brushes.Black;
            Brush brushBG = Brushes.LightGray;

            Graphics gc = e.Graphics;

            //1) Print Logo at the top-left corner
            //Maximum size is pageWidth/2 * 100
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            Stream s = myAssembly.GetManifestResourceStream("EHD.Admin.images.logo.png");
            RectangleF recLogo = new Rectangle {
                X = left,
                Y = (int)top,
                Width = 0,
                Height = 0
            };

            if (s != null) {
                Image imgLogo = Image.FromStream(s);

                recLogo.Width = imgLogo.Width > clientWidth / 2 ? clientWidth / 2 : imgLogo.Width;
                recLogo.Height = recLogo.Width / imgLogo.Width * imgLogo.Height;

                gc.DrawImage(imgLogo, recLogo);

                s.Close();
            }

            //2) Print Title to the top-right with Logo
            string strToPrint = "ACCOUNT STATEMENT";
            SizeF sizeToPrint = gc.MeasureString(strToPrint, fontLgBold);
            gc.DrawString(strToPrint, fontLgBold, brushToPrint, new PointF(clientWidth - sizeToPrint.Width + MARGIN_LEFT, top + (recLogo.Height > 0 ? recLogo.Height/2 - sizeToPrint.Height/2 : 0)));

            top += (int)Math.Max(recLogo.Height, sizeToPrint.Height);

            //3) Print Client info
            top += 30; //Add padding

            strToPrint = _patient.DisplayName.ToUpper() + "\n" + _patient.ContactInfo.Address;
            SizeF sizePatientInfo = gc.MeasureString(strToPrint, fontMd);
            gc.DrawString(strToPrint, fontMd, brushToPrint, new PointF(left, top));

            //4) Print Date range and Account No. (Patient File #)
            const int WIDTH_ACCOUNT_NO = 100;
            const int WIDTH_DATE_RANGE = 250;

            string sDateRangeHeader = "Statement Period";
            string sAccountNoHeader = "Account No.";
            string sDateStart = (grdTransaction.Rows[0].DataBoundItem as Transaction).TransactionTime.Value.ToString("yyyy-MM-dd");
            string sDateEnd = (grdTransaction.Rows[grdTransaction.Rows.Count - 1].DataBoundItem as Transaction).TransactionTime.Value.ToString("yyyy-MM-dd");
            string sDateRangeContent = sDateStart + " to " + sDateEnd;
            string sAccountNoContent = _patient.FileNumber;

            SizeF sizeDateRangeHeader = gc.MeasureString(sDateRangeHeader, fontMdBold);
            SizeF sizeAccountNoHeader = gc.MeasureString(sAccountNoHeader, fontMdBold);
            SizeF sizeDateRangeContent = gc.MeasureString(sDateRangeContent, fontMd);
            SizeF sizeAccountNoContent = gc.MeasureString(sAccountNoContent, fontMd);

            int maxDateRangeWidth = (int)Math.Max(sizeDateRangeHeader.Width, sizeDateRangeContent.Width) + 10;
            int maxAccountNoWidth = (int)Math.Max(sizeAccountNoHeader.Width, sizeAccountNoHeader.Width) + 10;
            int columnBorderSize = 2;

            RectangleF recDateRangeHeader = new RectangleF {
                X = clientWidth - maxAccountNoWidth - columnBorderSize - maxDateRangeWidth + MARGIN_LEFT,
                Y = top,
                Width = maxDateRangeWidth,
                Height = sizeDateRangeHeader.Height
            };

            RectangleF recAccountNoHeader = new RectangleF {
                X = recDateRangeHeader.X + recDateRangeHeader.Width + columnBorderSize,
                Y = top,
                Width = maxAccountNoWidth,
                Height = sizeAccountNoHeader.Height
            };

            //Print date range and account no. headers with background
            gc.FillRectangle(brushBG, recDateRangeHeader);
            gc.FillRectangle(brushBG, recAccountNoHeader);
            gc.DrawString(sDateRangeHeader, fontMdBold, brushToPrint,
                recDateRangeHeader.X + (maxDateRangeWidth - sizeDateRangeHeader.Width) / 2,
                recDateRangeHeader.Y);
            gc.DrawString(sAccountNoHeader, fontMdBold, brushToPrint,
                recAccountNoHeader.X + (maxAccountNoWidth - sizeAccountNoHeader.Width) / 2,
                recAccountNoHeader.Y);

            //Print actual date range and account no. content
            gc.DrawString(sDateRangeContent, fontMd, brushToPrint,
                recDateRangeHeader.X + (maxDateRangeWidth - sizeDateRangeContent.Width) / 2,
                recDateRangeHeader.Y + recDateRangeHeader.Height);
            gc.DrawString(sAccountNoContent, fontMd, brushToPrint,
                recAccountNoHeader.X + (maxAccountNoWidth - sizeAccountNoContent.Width) / 2,
                recAccountNoHeader.Y + recAccountNoHeader.Height);

            top += (int)Math.Max(sizePatientInfo.Height, sizeDateRangeHeader.Height + sizeDateRangeContent.Height);

            top += 30; //Add padding

            return top;
        }

        private float printRow(PrintPageEventArgs e, float left, float top, Brush brushDefualt, Font fontDefault, 
            string[] colText, bool isHeader = false) {

            if (colText.Length != 5) throw new ArgumentException("colText's length has to be 5.");

            float printedHeight = 0;
            float deltaX = 0;

            //Define column widths
            int[] colWd = new int[5] {
                100,
                e.MarginBounds.Width - 400,
                100,
                100,
                100
            };

            //Print header background
            printedHeight = e.Graphics.MeasureString("A", fontDefault).Height;

            //Print background for header row
            if (isHeader) {
                deltaX = 0;
                for (int i = 0; i < colWd.Length; i++) {
                    e.Graphics.FillRectangle(Brushes.LightGray, left + deltaX, top, colWd[i] - 1, printedHeight);
                    deltaX += colWd[i];
                }
            } else {
                printedHeight = fontDefault.Height;
            }

            //Print text
            deltaX = 0;
            float paddingX = 0;
            for (int i=0; i<colWd.Length; i++) {

                if (i >= 2 && !isHeader) {//Column [2] to [4] align text to right
                    float fTextWidth = e.Graphics.MeasureString(colText[i], fontDefault).Width;
                    if (colWd[i] > fTextWidth) {
                        paddingX = colWd[i] - fTextWidth;
                    }
                } else if (isHeader) { //Align header text in the center
                    float fTextWidth = e.Graphics.MeasureString(colText[i], fontDefault).Width;
                    if (colWd[i] > fTextWidth) {
                        paddingX = (colWd[i] - fTextWidth) / 2f;
                    }
                } else {
                    paddingX = 0;
                }

                e.Graphics.DrawString(colText[i], fontDefault, brushDefualt, left + deltaX + paddingX, top);

                //prepare for the next column's start x
                deltaX += colWd[i];
            }

            return printedHeight;
        }

        private void btnPrintPreview_Click(object sender, EventArgs e) {
            printBalance(true);
        }

        private void resetPrintVariables() {
            pageCount = 1;
            rowIndexToPrint = 0;
            fTotalSpend = 0;
            fTotalDeposit = 0;
        }

        private void printBalance(bool preview) {

            resetPrintVariables();

            if (grdTransaction.Rows.Count <= 0) {
                MessageBox.Show(this, "There is nothing to print.");
                return;
            }

            try {
                //Set page margins
                printDoc.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);
                //printDoc.OriginAtMargins = true;

                if (preview) {
                    dlgPrintPreview.ShowDialog(this);
                } else {
                    if (dlgPrint.ShowDialog(this) == DialogResult.OK) {
                        printDoc.Print();
                    }
                }

            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to print. " + ex.Message);
                Program.log.Error("Failed to print.", ex);
            }
        }

        private void printDoc_BeginPrint(object sender, PrintEventArgs e) {
            printAction = e.PrintAction;
        }
    }
}
