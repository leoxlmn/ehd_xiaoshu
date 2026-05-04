using NHibernate.Event;
using EHD.Model.Entity;
using System;
using Utility;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using System.Reflection;

namespace EHD.Audit {
    public class AuditListener :
        IPostInsertEventListener,
        IPostUpdateEventListener,
        IPostDeleteEventListener {

        public string LogonUserName { get; set; }

        public void OnPostInsert(PostInsertEvent @event) {
            //Do not audit history tracking entities
            if(@event.Entity is HistoryTracking) {
                return;
            }
            //Do not audit entities not EntityBase
            if (!(@event.Entity is EntityBase)) return;

            //Get the logon user's windows account without domain name.
            //string UserName = string.Empty;
            /*
            string[] temp = System.Web.HttpContext.Current.User.Identity.Name.Trim().Split(new char[] { '\\' });
            if(null != temp && temp.Length > 0) {
                UserName = temp[temp.Length - 1];
            }*/
            //UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            //Get Entity Type Name
            var entityTypeName = @event.Entity.GetType().Name;

            //Get the Updating Entity
            EntityBase entity = @event.Entity as EntityBase;

            //Create a new HistoryTracking entity
            HistoryTracking history = new HistoryTracking();
            history.ActionBy = entity.CreatedBy.Length > 0 ? entity.CreatedBy : LogonUserName;//UserName.Length > 0 ? UserName : Environment.UserName;
            history.ActionTime = entity.CreatedTime != default(DateTime) ? entity.CreatedTime : DateTime.Now;
            history.ActionType = ActionType.Insert;
            history.ObjectName = entityTypeName;
            history.ObjectId = entity.Id.ToString();

            //Set NewValue
            //history.NewValue = JSONHelper.GetJSON(entity);
            history.NewValue = entity.ToShallowJSON();

            //Save HistoryTracking into database
            var session = @event.Session.GetSession(EntityMode.Poco);
            session.Save(history);
            session.Flush();
        }

        public void OnPostUpdate(PostUpdateEvent @event) {
            //Do not audit history tracking entities
            if(@event.Entity is HistoryTracking) {
                return;
            }
            //Do not audit entities not EntityBase
            if (!(@event.Entity is EntityBase)) return;

            //Get Entity Type Name
            var entityTypeName = @event.Entity.GetType().Name;

            //Get the Updating Entity
            EntityBase entity = @event.Entity as EntityBase;

            //Create a new HistoryTracking entity
            HistoryTracking history = new HistoryTracking();
            history.ActionBy = (LogonUserName != null ? LogonUserName : entity.UpdatedBy != null ? entity.UpdatedBy : "");//entity.UpdatedBy;
            history.ActionTime = entity.UpdatedTime.HasValue ? entity.UpdatedTime.Value : DateTime.Now;
            history.ActionType = ActionType.Update;
            history.ObjectName = entityTypeName;
            history.ObjectId = entity.Id.ToString();

            history.NewValue = entity.ToShallowJSON();

            //Save HistoryTracking into database
            var session = @event.Session.GetSession(EntityMode.Poco);
            session.Save(history);
            session.Flush();
        }

        public void OnPostDelete(PostDeleteEvent @event) {
            //Do not audit history tracking entities
            if(@event.Entity is HistoryTracking) {
                return;
            }
            //Do not audit entities not EntityBase
            if (!(@event.Entity is EntityBase)) return;

            //Get the logon user's windows account without domain name.
            //string UserName = string.Empty;
            /*
            string[] temp = System.Web.HttpContext.Current.User.Identity.Name.Trim().Split(new char[] { '\\' });
            if(null != temp && temp.Length > 0) {
                UserName = temp[temp.Length - 1];
            }*/
            //UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            //Get Entity Type Name
            var entityTypeName = @event.Entity.GetType().Name;

            //Get the Updating Entity
            EntityBase entity = @event.Entity as EntityBase;

            //Create a new HistoryTracking entity
            HistoryTracking history = new HistoryTracking();
            //NOTE: when deleting entities, update the UpdatedBy field first so the audit listener would know who is deleting
            history.ActionBy = LogonUserName;// (entity.UpdatedBy != null && entity.UpdatedBy.Length > 0) ? entity.UpdatedBy : UserName.Length > 0 ? UserName : Environment.UserName;
            history.ActionTime = DateTime.Now;
            history.ActionType = ActionType.Delete;
            history.ObjectName = entityTypeName;
            history.ObjectId = entity.Id.ToString();

            //Save HistoryTracking into database
            var session = @event.Session.GetSession(EntityMode.Poco);
            session.Save(history);
            session.Flush();
        }

         private void setEntityValues(EntityBase entity,
            string[] propertyNames, object[] propertyValues) {

            Type entityType = entity.GetType();
            for(int i = 0; i < propertyNames.Length; i++) {
                entityType.GetProperty(propertyNames[i]).SetValue(entity, propertyValues[i], null);
            }
        }
    }
}
