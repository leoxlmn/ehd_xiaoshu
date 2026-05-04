
namespace EHD.Model.Entity {

    /// <summary>
    /// Define user rights/rolls
    /// </summary>
    public enum UserType {
        /// <summary>
        /// Can do all for real data.
        /// Should be used in rare cases.
        /// </summary>
        Administrator,

        /// <summary>
        /// Can do all for TESTING DATA ONLY.
        /// All data created by Developer will be marked as test data.
        /// </summary>
        Developer,

        /// <summary>
        /// Can view all real data, patients, therapists, treatments and so on.
        /// Can add/update/delete therapists, patients.
        /// Can assign patients to therapists.
        /// Can add/update/delete ALL treatment records.
        /// </summary>
        Manager,

        /// <summary>
        /// Can view assigned patients' info.
        /// Can add treatment records for the assigned patients.
        /// Can update/delete treatment records created by him/herself.
        /// </summary>
        Therapist,

        /// <summary>
        /// Can't do anything
        /// </summary>
        None,

        /// <summary>
        /// Same as Manager, but do not have access to Settings
        /// </summary>
        Receptionist

    }
}
