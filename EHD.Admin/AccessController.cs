using System.Collections.Generic;
using EHD.Model.Entity;
using System;

namespace EHD.Admin {
    public static class AccessController {
        public static void RemoveItemFromList<T>(IList<T> list, T item, User logonUser) 
            where T: EntityBase {
            
            if(list == null || item == null || list.Count <= 0) return;

            Type itemType = typeof(T);
            if( !itemType.Name.Equals("InitialTreatment") &&
                !itemType.Name.Equals("FollowUpTreatment") &&
                !itemType.Name.Equals("Patient")
                ) {
                
                throw new ApplicationException(string.Format("The item type[{0}] has not been implemented.",
                    itemType)
                    );
            }

            switch(logonUser.UserType) {
                case UserType.Administrator: //Can see all
                    break;
                case UserType.Manager: //Can see all
                    break;
                case UserType.Receptionist: //Can see all
                    break;
                case UserType.Therapist: //Can see his/her assigned and open InitialTreatments and FollowUpTreatments only
                    if(itemType.Name.Equals("InitialTreatment")) {
                        InitialTreatment it = item as InitialTreatment;
                        if(it.Therapist != Program.LogonUser || !it.OpenToTherapist) {
                            list.Remove(item);
                        }
                    } else if(itemType.Name.Equals("FollowUpTreatment")) {
                        FollowUpTreatment ft = item as FollowUpTreatment;
                        if(ft.Therapist != logonUser || !ft.OpenToTherapist){
                            list.Remove(item);
                        }
                    } else if(itemType.Name.Equals("Patient")) {
                        //Therapist do not have access to patient list
                        list.Remove(item);
                    }
                    break;
                case UserType.Developer: //Can only see Test Data
                    if(!item.IsTestData) {
                        list.Remove(item);
                    }
                    break;
                case UserType.None: //Can see NONE
                    list.Remove(item);
                    break;
                default:
                    throw new ApplicationException(
                        string.Format("The access logic for UserType[{0}] has not been implemented.",
                            logonUser.UserType.ToString())
                        );
            }
        }
    }
}
