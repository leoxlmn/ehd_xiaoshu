
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System;

namespace EHD.Admin {
    public class DBUpdater {
        private string _conn = string.Empty;

        public DBUpdater(string connectiongString) {
            _conn = connectiongString;
        }

        public void Update() {
            /*
           
            //Update 1
            try {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySqlConnection(_conn)) {

                    conn.Open();

                    using (MySqlTransaction tr = conn.BeginTransaction()) {

                        try {
                            //Check if there is the column THP_Description in table TherapyType
                            MySqlCommand cmd = conn.CreateCommand();
                            cmd.CommandText = "SELECT column_name FROM information_schema.columns WHERE table_name = 'InvoiceItem' AND column_name = 'INI_SERId'";
                            cmd.CommandType = System.Data.CommandType.Text;
                            MySqlDataReader reader = cmd.ExecuteReader();
                            bool columnExists = reader.HasRows;
                            if (!reader.IsClosed) reader.Close();

                            if (columnExists) {

                                if (DialogResult.OK == MessageBox.Show(@"
The database has been changed. Do you want to update the database?

Press[OK] to update.\nPress[Cancel] to cancel.",
                                    "Database Update",
                                    MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Question)) {

                                    MySqlCommand cmdUpdate = conn.CreateCommand();
                                    cmdUpdate.CommandText = @"
ALTER TABLE `EHD`.`therapytype` ADD COLUMN `THP_Description` VARCHAR(100) NOT NULL DEFAULT '' AFTER `THP_TherapyType`;
ALTER TABLE `EHD`.`therapytype` ADD COLUMN `THP_TempSERId` INT(11) NULL AFTER `THP_TherapyType`;
ALTER TABLE `EHD`.`invoiceitem` ADD COLUMN `INI_THPId` INT(11) NULL AFTER `INI_SERId`;
update TherapyType t, ServiceType s
    set t.THP_Description = s.SER_Description,
        t.THP_TempSERId = s.SER_SERId
where s.SER_Description like concat(t.THP_TherapyType, '%');
update invoiceItem i, TherapyType t
	set i.INI_THPId = t.THP_THPId
where 
	i.INI_SERID = t.THP_TempSERId;
ALTER TABLE `EHD`.`therapytype` DROP COLUMN `THP_TempSERId`;
ALTER TABLE `EHD`.`invoiceitem` DROP FOREIGN KEY `fk_invoiceitem_service` ;
ALTER TABLE `EHD`.`invoiceitem` DROP COLUMN `INI_SERId` ;
ALTER TABLE `EHD`.`invoiceitem` 
    ADD CONSTRAINT `fk_invoiceitem_therapytype`
    FOREIGN KEY (`INI_THPId` )
    REFERENCES `EHD`.`therapytype` (`THP_THPId` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION;
ALTER TABLE `EHD`.`invoiceitem` CHANGE COLUMN `INI_THPId` `INI_THPId` INT(11) NOT NULL;
";
                                    cmdUpdate.CommandType = System.Data.CommandType.Text;
                                    cmdUpdate.ExecuteNonQuery();
                                }

                                

                                tr.Commit();
                            }
                        } catch(Exception ex) {
                            tr.Rollback();
                            MessageBox.Show(
                                "Failed to update the database.\n\nThe application may not run properly!\n\n" + ex.Message,
                                "ERROR",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }

                    conn.Close();
                }
            } catch {
                //Do nothing
            }
            */
        }
        
    }
}
