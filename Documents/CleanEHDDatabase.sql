
-- Select database ----------------
use easyhealthcare_dev;

-- Clear all diagram points --------------------
truncate table pointonacupuncture;
truncate table pointonacupuncturefollowup;
truncate table pointonchiropractic;
truncate table pointonchiropracticfollowup;
truncate table pointonmassage;
truncate table pointonosteopathy;
truncate table pointonphysiotherapy;

-- Clear all treatments ----------------------
delete from acupuncturefollowupdetail;
delete from acupuncturedetail;

delete from chiropracticfollowupdetail;
delete from chiropracticdetail;

delete from massagefollowupdetail;
delete from massagedetail;

delete from naturopathicdetail;

delete from osteopathyfollowupdetail;
delete from osteopathydetail;

delete from physiotherapydetail;

delete from followuptreatment;
delete from initialtreatment;

-- Clear all invoices -----------------------------
delete from invoiceitem;
delete from invoice;

-- Clear history ------------------------------------
truncate table historytracking;

-- Clear accountbalance ----------------------------
truncate table accountbalance;

-- Clear user info ----------------------------------
truncate table userweeklyavailability;
truncate table usertimeoff;
truncate table therapistorganizationregistration;
delete from therapistorganizationregistrationgroup;
delete from user where USR_Username != 'admin';

-- Clear patients ----------------------------------
delete from patient;

-- Clear other --------------------------------
delete from insurer;
delete from invoicenotereference;
delete from templatetreatmentdetail;








