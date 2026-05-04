
using Utility;
using System;
using NHibernate.Util;

namespace EHD.PureNHibernateModel.Entity {
    public static class HistoryTrackingHelper {
        /// <summary>
        /// Build a HistoryTracking object.
        /// </summary>
        /// <param name="Entity">Always required.</param>
        /// <param name="ActionType">Insert/Update/Delete</param>
        /// <param name="Operator"></param>
        /// <param name="OldEntity">Required when ActionType is Update.</param>
        /// <returns></returns>
        public static HistoryTracking CreateHistory(
            EntityBase Entity, ActionType ActionType, string Operator, string OldEntityValue) {

            //Verify parameters
            if(Entity == null)
                throw new ArgumentException("Entity is missing.");

            //Build a HistoryTracking object
            HistoryTracking history = new HistoryTracking();
            history.ActionBy = Operator;
            history.ActionTime =
                ActionType == ActionType.Insert ? Entity.CreatedTime
                : (ActionType == ActionType.Update && Entity.UpdatedTime.HasValue) ? Entity.UpdatedTime.Value
                : DateTime.Now;
            history.ActionType = ActionType;
            history.ObjectName = Entity.GetType().Name;
            history.ObjectId = Entity.Id.ToString();

            //Set OldValue
            if(ActionType == ActionType.Update)
                history.OldValue = OldEntityValue;
            else if(ActionType == ActionType.Delete)
                history.OldValue = JSONHelper.GetJSON(Entity);

            //Set NewValue
            if(ActionType == ActionType.Insert || ActionType == ActionType.Update)
                history.NewValue = JSONHelper.GetJSON(Entity);

            return history;
        }

        /// <summary>
        /// Build a HistoryTracking object.
        /// </summary>
        /// <param name="Entity">Always required.</param>
        /// <param name="ActionType">Insert/Update/Delete</param>
        /// <param name="Operator"></param>
        /// <param name="OldEntity">Required when ActionType is Update.</param>
        /// <returns></returns>
        public static HistoryTracking CreateHistory(
            EntityBase Entity, ActionType ActionType, string Operator) {
            return CreateHistory(Entity, ActionType, Operator, string.Empty);
        }
    }
}
