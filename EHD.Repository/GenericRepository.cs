using System.Collections.Generic;
using JCManagement.Model.Entity;
using NHibernate;
using System;

namespace JCManagement.Repository {
    public class GenericRepository<T> : IRepository<T> where T : EntityBase {
        protected readonly ISession _session;

        public GenericRepository(ISession session) {
            _session = session;
        }

        protected virtual TResult Transact<TResult>(Func<TResult> func) {
            if(!_session.Transaction.IsActive) {
                // Wrap in transaction
                TResult result;
                using(var tx = _session.BeginTransaction()) {
                    result = func.Invoke();
                    tx.Commit();
                }
                return result;
            }
            // Don't wrap;
            return func.Invoke();
        }

        protected virtual void Transact(Action action) {
            Transact<bool>(() => {
                action.Invoke();
                return false;
            });
        }

        public T GetById(int id) {
            return Transact(() => _session.Get<T>(id));
        }

        public IList<T> GetAll() {
            return Transact(() => _session.QueryOver<T>().List<T>());
        }

        public void Add(T item) {
            Transact(() => _session.Save(item));
        }

        public void Update(T item) {
            Transact(() => _session.Update(item));
        }

        public void Delete(T item) {
            Transact(() => _session.Delete(item));
        }

        public bool Contains(T item) {
            if(item.Id == default(int))
                return false;
            return Transact(() => _session.Get<T>(item.Id)) != null;
        }

        public int Count {
            get {
                return Transact(() => _session.QueryOver<T>().RowCount());
            }
        }

    }
}
