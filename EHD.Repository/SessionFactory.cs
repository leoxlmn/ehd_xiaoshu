using NHibernate;
using FluentNHibernate.Cfg;
using EHD.Model.Entity;
using FluentNHibernate.Cfg.Db;
using EHD.Model.Mapping;
using EHD.Audit;
using NHibernate.Event;
using System;
using System.Collections.Generic;

namespace EHD.Repository {
    public static class SessionFactory {

        private static ISessionFactory _sessionFactory;

        private static AuditListener _auditListener;

        public static ISessionFactory InitiateSessionFactory(string ConnectionString) {
            if (_sessionFactory == null) {

                _auditListener = new AuditListener();

                _sessionFactory = Fluently.Configure().Database(MySQLConfiguration.Standard
                    //.ShowSql() //Enable this line to output SQL in Output window
                    .ConnectionString(ConnectionString))
                    .CurrentSessionContext("thread_static")
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>()
                        .Conventions.Add(
                            new EnumMappingConvention(), //This EnumMappingConvention is important!
                            new LowercaseTableNameConvention() //This is necessary if the database is hosted on a Unix/Linus server
                            )
                        )
                    .ExposeConfiguration(c => c.AppendListeners(NHibernate.Event.ListenerType.PostInsert, new IPostInsertEventListener[] { _auditListener }))
                    .ExposeConfiguration(c => c.AppendListeners(NHibernate.Event.ListenerType.PostUpdate, new IPostUpdateEventListener[] { _auditListener }))
                    .ExposeConfiguration(c => c.AppendListeners(NHibernate.Event.ListenerType.PostDelete, new IPostDeleteEventListener[] { _auditListener }))

                    .BuildSessionFactory();

            }

            return _sessionFactory;
        }

        public static ISessionFactory GetSessionFactory() {
            if(_sessionFactory == null)
                throw new ApplicationException("SessionFactory has not been initiated.");

            return _sessionFactory;
        }

        public static void SetCurrentUserName(string username) {
            _auditListener.LogonUserName = username;
        }

        /*
         * Session Management
         * Any method that need to access NHibernate session would:
         *  1) Create a unique guid key.
         *  2) Use this key to get a session by calling GetOpenSession(key).
         *  3) By the end of the method, call TryCloseSession(key).
         */
        private static ISession _currentSession = null;

        //Tracking how many methods are using the current session
        private static IList<Guid> _registeredKeys = new List<Guid>();

        //
        public static ISession GetOpenSession(Guid key) {

            if (key == null) throw new ArgumentException("key cannot be null.");
            if (_registeredKeys.Contains(key)) throw new ApplicationException("The key has already been registered!");
            
            try {
                if (NHibernate.Context.CurrentSessionContext.HasBind(_sessionFactory)) {
                    _currentSession = _sessionFactory.GetCurrentSession();
                } else {
                    _currentSession = _sessionFactory.OpenSession();
                    NHibernate.Context.CurrentSessionContext.Bind(_currentSession);
                }

                _registeredKeys.Add(key);
                return _currentSession;
            } catch (Exception ex) {
                throw new ApplicationException("Failed to get the current session or open a new one.", ex);
            }
        }

        public static void TryCloseSession(Guid key) {
            if (_registeredKeys.Contains(key)) {
                _registeredKeys.Remove(key);
            }

            //When there is no registered keys, close this session
            if (_registeredKeys.Count <= 0 && _currentSession != null && _currentSession.IsOpen) {
                NHibernate.Context.CurrentSessionContext.Unbind(_sessionFactory);
                _currentSession.Dispose();
            }
        }

        public static void EnsureSessionClose() {
            _registeredKeys.Clear();
            if (_currentSession != null && _currentSession.IsOpen) {
                NHibernate.Context.CurrentSessionContext.Unbind(_sessionFactory);
                _currentSession.Dispose();
            }
        }
    }
}
