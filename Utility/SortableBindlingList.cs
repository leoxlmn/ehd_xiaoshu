/*
 * USAGE: Assume you have a collection, MyClassCollection, which contains objects of MyClass.
 *        You want to bind the collection to a DataGridView and enable sorting. 
 *        Follow the code below:
 * 
 *  
 *      Example 1:
 *      
 *      SortableBindlingList<MyClass> myBindlingList = new SortableBindlingList<MyClass>();
 *      foreach(MyClass o in MyClassCollection){
 *          myBindlingList.Add(o);
 *      }
 *      
 *      myDataGridView.DataSource = myBindlingList;
 *      myDataGridView.Sort(columnYouWantToSort, directionYouWantSort);
 *      
 *      Example 2:
 *      
 *      SortableBindlingList<MyClass> myBindlingList = new SortableBindlingList<MyClass>();
 *      foreach(MyClass o in MyClassCollection){
 *          myBindlingList.Add(o);
 *      }
 *      
 *      myBindlingList.ApplySort(listOfSortCollection);//Refers to the source code below on how to use it.
 *      myDataGridView.DataSource = myBindlingList;
 *      
 * 
 *      ===================================================================================================
 *      For C# 3.0 and above, if you have a List of object T. Using the Extension methods is a lot easier.
 *      
 *      Exmaple 3: 
 *      
 *      List<Employee> employees = GetAllEmployees();
 *      
 *      //Sort by one property
 *      employees.Sort("EmployeeName", ListSortDirection.Ascending);
 *      
 *      //Sort by multiple properties
 *      employees.Sort(
 *          new string[]{"Department", "EmployeeName"},
 *          new ListSortDirection[]{ListSortDirection.Ascending, ListSortDirection.Descending});
 *          
 *      //Sort by multiple properties with sub properties
 *      employees.Sort(
 *          new string[]{"Department.ListOrder", "Manager.LastName"},
 *          new ListSortDirection[]{ListSortDirection.Ascending, ListSortDirection.Descending});
 *          
 * 
 * NOTES: The actually property to be compared, must implement the IComparable interface. 
 * For example: employees.Sort("Department", ListSortDirection.Ascending) Doesn't work if the Department object doesn't implement the IComparable interface.
 * However, employee.Sort("Department.DepartmentName", ...) will work, as the DepartmentName is string type.
 *      
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;

namespace Utility {
    public class SortableBindingList<T> : BindingList<T>, IBindingListView {

        #region Data Members
        PropertyComparerCollection<T> sorts;
        #endregion

        #region Properties
        protected override bool IsSortedCore {
            get { return sorts != null; }
        }
        protected override void RemoveSortCore() {
            sorts = null;
        }
        protected override bool SupportsSortingCore {
            get { return true; }
        }
        protected override ListSortDirection SortDirectionCore {
            get {
                return sorts == null ? ListSortDirection.Ascending :
                sorts.PrimaryDirection;
            }
        }
        protected override PropertyDescriptor SortPropertyCore {
            get { return sorts == null ? null : sorts.PrimaryProperty; }
        }
        #endregion

        protected override void ApplySortCore(PropertyDescriptor prop,
            ListSortDirection direction) {

            ListSortDescription[] arr = { new ListSortDescription(prop, direction) };
            ApplySort(new ListSortDescriptionCollection(arr));
        }

        public void ApplySort(ListSortDescriptionCollection sortCollection) {
            bool oldRaise = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            try {
                PropertyComparerCollection<T> tmp = new PropertyComparerCollection<T>(sortCollection);
                List<T> items = new List<T>(this);
                items.Sort(tmp);
                int index = 0;
                foreach (T item in items) {
                    SetItem(index++, item);
                }
                sorts = tmp;
            } finally {
                RaiseListChangedEvents = oldRaise;
                ResetBindings();
            }
        }

        public void ApplySort(List<ListSortDescriptionEx> sortCollection) {
            bool oldRaise = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            try {
                PropertyComparerCollection<T> tmp = new PropertyComparerCollection<T>(sortCollection);
                List<T> items = new List<T>(this);
                items.Sort(tmp);
                int index = 0;
                foreach (T item in items) {
                    SetItem(index++, item);
                }
                sorts = tmp;
            } finally {
                RaiseListChangedEvents = oldRaise;
                ResetBindings();
            }
        }

        //Sort list by field name
        public void Sort(string fieldName, ListSortDirection direction) {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            string[] nameList = fieldName.Split(new char[] { '.' });
            if (nameList.Length > 0) {
                List<PropertyDescriptor> propList = new List<PropertyDescriptor>();
                for (int i = 0; i < nameList.Length; i++) {
                    PropertyDescriptor p = props.Find(nameList[i], true);
                    if (p != null) {
                        propList.Add(p);
                        props = TypeDescriptor.GetProperties(p.PropertyType);
                    }
                }
                if (propList.Count > 0) {
                    List<ListSortDescriptionEx> listSortDescEx = new List<ListSortDescriptionEx>();
                    listSortDescEx.Add(new ListSortDescriptionEx(propList, direction));
                    ApplySort(listSortDescEx);
                }
            } else {
                PropertyDescriptor prop = props.Find(fieldName, true);
                if (prop != null) {
                    ListSortDescription[] listSortDesc = new ListSortDescription[]{
                        new ListSortDescription(prop, direction)
                        };
                    ApplySort(new ListSortDescriptionCollection(listSortDesc));
                }
            }
        }

        //Sort list in ascending order by default
        public void Sort(string fieldName) {
            Sort(fieldName, ListSortDirection.Ascending);
        }

        string IBindingListView.Filter {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        void IBindingListView.RemoveFilter() {
            throw new NotImplementedException();
        }
        ListSortDescriptionCollection IBindingListView.SortDescriptions {
            get { return sorts.Sorts; }
        }
        bool IBindingListView.SupportsAdvancedSorting {
            get { return true; }
        }
        bool IBindingListView.SupportsFiltering {
            get { return false; }
        }
    }

    [Serializable]
    public class PropertyComparerCollection<T> : IComparer<T> {
        private readonly ListSortDescriptionCollection sorts;
        private readonly List<ListSortDescriptionEx> listSorts;
        private readonly PropertyComparer<T>[] comparers;
        public ListSortDescriptionCollection Sorts {
            get { return sorts; }
        }
        public PropertyComparerCollection(ListSortDescriptionCollection sorts) {
            if (sorts == null) throw new ArgumentNullException("sorts");
            this.sorts = sorts;
            List<PropertyComparer<T>> list = new List<PropertyComparer<T>>();
            foreach (ListSortDescription item in sorts) {
                list.Add(new PropertyComparer<T>(item.PropertyDescriptor,
                    item.SortDirection == ListSortDirection.Descending));
            }
            comparers = list.ToArray();
        }
        public PropertyComparerCollection(List<ListSortDescriptionEx> listSorts) {
            if (listSorts == null) throw new ArgumentNullException("listSorts");
            this.listSorts = listSorts;
            List<PropertyComparer<T>> list = new List<PropertyComparer<T>>();
            foreach (ListSortDescriptionEx item in listSorts) {
                list.Add(new PropertyComparer<T>(item.PropertyDescriptors,
                    item.SortDirection == ListSortDirection.Descending));
            }
            comparers = list.ToArray();
        }
        public PropertyDescriptor PrimaryProperty {
            get {
                return comparers.Length == 0 ? null : comparers[0].Property;
            }
        }
        public ListSortDirection PrimaryDirection {
            get {
                return comparers.Length == 0 ? ListSortDirection.Ascending :
                    comparers[0].Descending ? ListSortDirection.Descending :
                    ListSortDirection.Ascending;
            }
        }

        int IComparer<T>.Compare(T x, T y) {
            int result = 0;
            for (int i = 0; i < comparers.Length; i++) {
                result = comparers[i].Compare(x, y);
                if (result != 0) break;
            }
            return result;
        }
    }

    public class PropertyComparer<T> : IComparer<T> {
        private readonly bool descending;
        public bool Descending {
            get { return descending; }
        }
        private readonly PropertyDescriptor property;
        public PropertyDescriptor Property {
            get { return property; }
        }
        private readonly PropertyDescriptor[] properties;
        public PropertyDescriptor[] Properties {
            get { return properties; }
        }
        public PropertyComparer(PropertyDescriptor property, bool descending) {
            if (property == null) throw new ArgumentNullException("property");
            this.descending = descending;
            this.property = property;
        }
        public PropertyComparer(PropertyDescriptor[] properties, bool descending) {
            if (properties == null || properties.Length <= 0) throw new ArgumentException("properties");
            this.descending = descending;
            this.properties = properties;
        }
        public int Compare(T x, T y) {
            int value = 0;
            if (properties != null && properties.Length > 0) {
                object vX = x;
                object vY = y;

                for (int i = 0; i < properties.Length; i++) {
                    vX = properties[i].GetValue(vX);
                    vY = properties[i].GetValue(vY);
                }

                if (vX is IComparable || vY is IComparable) {
                    value = Comparer.Default.Compare(vX, vY);
                } else {
                    value = 0;
                }
            } else {
                if (property.GetValue(x) is IComparable || property.GetValue(y) is IComparable) {
                    value = Comparer.Default.Compare(property.GetValue(x), property.GetValue(y));
                } else {
                    value = 0;
                }
            }
            return descending ? -value : value;
        }
    }

    public class ListSortDescriptionEx {
        private List<PropertyDescriptor> listPropertyDescriptor;
        private ListSortDirection sortDirection;

        public ListSortDescriptionEx(List<PropertyDescriptor> listPropertyDescriptor, ListSortDirection sortDirection) {
            this.listPropertyDescriptor = listPropertyDescriptor;
            this.sortDirection = sortDirection;
        }

        public PropertyDescriptor[] PropertyDescriptors {
            get { return listPropertyDescriptor.ToArray(); }
        }
        public ListSortDirection SortDirection {
            get { return sortDirection; }
        }
    }

    public static class SortableBindlingListExtension {
        public static SortableBindingList<T> GetSortableBindlingList<T>(this List<T> list) {
            SortableBindingList<T> sortList = new SortableBindingList<T>();
            foreach (T item in list) {
                sortList.Add(item);
            }
            return sortList;
        }

        /// <summary>
        /// Sort the List by one or multiple properties of the list item.
        /// </summary>
        /// <param name="sortFieldNames">
        /// An array of the List item's properties. 
        /// Each SortFieldName can have sub property names delimited by '.'.
        /// For example:
        ///     An Employee object contains a Department property, an EmployeeName property.
        ///     Department has a ListOrder property.
        ///     
        /// The SortFieldNames can be: "Department.ListOrder", "EmployeeName".
        /// So, the list of Employee objects will sort by Department's ListOrder first, then sort by EmployeeName.
        /// </param>
        /// <param name="sortDirections">
        /// An array of SortDirections. It should have the same number of elements as the SortFieldNames.
        /// </param>
        public static void Sort<T>(this List<T> list,
            string[] sortFieldNames,
            ListSortDirection[] sortDirections) {

            if (null == sortFieldNames || sortFieldNames.Length <= 0 ||
                null == sortDirections || sortDirections.Length <= 0 ||
                sortFieldNames.Length != sortDirections.Length)
                throw new ArgumentException("SortFiledNames and SortDirections cannot be null, and must have same length.");

            SortableBindingList<T> sortList = list.GetSortableBindlingList<T>();
            List<ListSortDescriptionEx> listSortDescEx = new List<ListSortDescriptionEx>();

            for (int f = 0; f < sortFieldNames.Length; f++) {
                string fieldName = sortFieldNames[f];
                ListSortDirection sortDirection = sortDirections[f];

                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                List<PropertyDescriptor> lstPropDescriptor = new List<PropertyDescriptor>();

                string[] nameList = fieldName.Split(new char[] { '.' });
                for (int i = 0; i < nameList.Length; i++) {
                    PropertyDescriptor p = props.Find(nameList[i], true);
                    if (p != null) {
                        lstPropDescriptor.Add(p);
                        props = TypeDescriptor.GetProperties(p.PropertyType);
                    } else {
                        throw new ArgumentException("Cannot find the property " + nameList[i]);
                    }
                }
                listSortDescEx.Add(new ListSortDescriptionEx(lstPropDescriptor, sortDirection));
            }

            sortList.ApplySort(listSortDescEx);

            //Write back the sorted items into the original list
            list.Clear();
            foreach (T item in sortList) {
                list.Add(item);
            }
        }

        /// <summary>
        /// Sort the List by the list item's property.
        /// </summary>
        /// <param name="sortFieldName">
        /// The SortFieldName can have sub property names delimited by '.'.
        /// For example:
        ///     An Employee object contains a Department property, an EmployeeName property.
        ///     Department has a ListOrder property.
        ///     
        /// The SortFieldName can be: "Department.ListOrder".
        /// So, the list of Employee objects will sort by Department's ListOrder first.
        /// </param>
        /// <param name="sortDirection">
        /// </param>
        public static void Sort<T>(this List<T> list,
            string sortFieldName,
            ListSortDirection sortDirection) {

            list.Sort<T>(new string[] { sortFieldName }, new ListSortDirection[] { sortDirection });
        }
    }
}