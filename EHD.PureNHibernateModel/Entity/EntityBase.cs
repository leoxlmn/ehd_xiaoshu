using System;
namespace EHD.PureNHibernateModel.Entity {
    [Serializable]
    public abstract class EntityBase {

        private int? oldHashCode;

        public override int GetHashCode() {
            if(oldHashCode.HasValue) return oldHashCode.Value;

            if(Id == 0) {
                oldHashCode = base.GetHashCode();
                return oldHashCode.Value;
            }

            return Id.GetHashCode();
        }

        /// <summary>
        /// Primary Key Field
        /// </summary>
        public virtual int Id { get; private set; }

        public override bool Equals(object obj) {
            var that = obj as EntityBase;
            if(that == null) return false;

            if(Id == 0 && that.Id == 0) {
                return ReferenceEquals(this, that);
            }

            return Id == that.Id;
        }

        public static bool operator ==(EntityBase lhs, EntityBase rhs) {
            return Equals(lhs, rhs);
        }

        public static bool operator !=(EntityBase lhs, EntityBase rhs) {
            return !Equals(lhs, rhs);
        }

        /// <summary>
        /// Concurrency control by version
        /// </summary>
        public virtual int Version { get; set; }

        public virtual DateTime CreatedTime { get; set; }
        public virtual DateTime? UpdatedTime { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string UpdatedBy { get; set; }
        public virtual bool IsTestData { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public EntityBase() {
            IsTestData = false;
        }

        /// <summary>
        /// This property is created for displaying in dropdownlist
        /// </summary>
        public virtual string DisplayName {
            get {
                return this.ToString();
            }
        }

    }
}
