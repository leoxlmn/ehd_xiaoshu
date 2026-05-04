using NHibernate.Event;
using JCManagement.Model.Entity;
using System;
using Utility;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using System.Reflection;
using NHibernate.Event.Default;

namespace JCManagement.Audit {
    [Serializable]
    public class FlushFixEventListener : DefaultFlushEventListener {

        protected override void PerformExecutions(IEventSource session) {
            try {
                session.ConnectionManager.FlushBeginning();
                session.PersistenceContext.Flushing = true;
                session.ActionQueue.PrepareActions();
                session.ActionQueue.ExecuteActions();
            } catch (HibernateException) {
                throw;
            } finally {
                session.PersistenceContext.Flushing = false;
                session.ConnectionManager.FlushEnding();
            }

        }
    } 
}