using FluentSql;
using FluentSql.Contracts;
using JanusData.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JanusData.Repositories
{
    public class Repository : IRepository
    {
        public string ConnectionName { get; set; }
        protected IDbConnection DbConnection;
        protected IEntityStore EntityStore;

        public Repository(IDbConnection dbconnection)
        {
            DbConnection = dbconnection;
            EntityStore = new EntityStore(DbConnection);
        }

        public IRepository WithDatabase(string databaseName)
        {
            if (DbConnection == null) return this;

            DbConnection.ChangeDatabase(databaseName);
            return this;
        }

        public T Add<T>(T entity) where T : new()
        {
            throw new NotImplementedException();
        }

        public int Remove<T>(T item)
        {
            throw new NotImplementedException();
        }

        public int Update<T>(T item)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByKeyAsync<T>(dynamic key)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindSingleAsync<T>(Expression<Func<T, bool>> predicateExpression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> predicateExpression)
        {
            throw new NotImplementedException();
        }

        public T FindByKey<T>(dynamic id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll<T>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tuple<T, R>> FindAll<T, R>() where R : new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tuple<T, R>> Find<T, R>(Expression<Func<T, R, bool>> filterExpression) where R : new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAndOrderby<T>(Expression<Func<T, bool>> predicateExpression, Expression<Func<T, object>> orderByExpression)
        {
            throw new NotImplementedException();
        }

        //public T Add<T>(T entity) where T : new()
        //{
        //    return EntityStore.Insert<T>(entity);
        //}

        //public async Task<T> FindByKeyAsync<T>(dynamic key)
        //{
        //    return await EntityStore.GetByKeyAsync<T>(key);
        //}

        //public async Task<T> FindSingleAsync<T>(Expression<Func<T, bool>> predicateExpression)
        //{
        //    return await EntityStore.GetSingleAsync<T>(predicateExpression);
        //}

        //public async Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> predicateExpression)
        //{
        //    return await EntityStore.GetAsync<T>(predicateExpression);
        //}

        public IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicateExpression)
        {
            var entities = EntityStore.Get<T>(predicateExpression);
            return entities;
        }

        //public IEnumerable<T> FindAndOrderby<T>(Expression<Func<T, bool>> predicateExpression, Expression<Func<T, object>> orderByExpression)
        //{
        //    var entities = EntityStore.GetAndOrderBy<T>(predicateExpression, orderByExpression);
        //    return entities;
        //}

        //public IEnumerable<T> FindAll<T>()
        //{
        //    var entities = EntityStore.GetAll<T>();
        //    return entities;
        //}

        //public IEnumerable<Tuple<T, R>> FindAll<T, R>() where R : new()
        //{
        //    var entities = EntityStore.GetAll<T, R>();
        //    return entities;
        //}

        //public IEnumerable<Tuple<T, R>> Find<T, R>(Expression<Func<T, R, bool>> filterExpression) where R : new()
        //{
        //    var entities = EntityStore.Get<T, R>(filterExpression);
        //    return entities;
        //}

        //public async Task<IEnumerable<TResult>> FindAsync<T, R, TResult>(Expression<Func<T, R, bool>> filterExpression) where R : new()
        //    where TResult : new()
        //{
        //    var entities = await EntityStore.GetAsync<T, R, TResult>(filterExpression);
        //    return entities;
        //}

        //public T FindByKey<T>(dynamic id)
        //{
        //    var entity = EntityStore.GetByKey<T>(id);
        //    return entity;
        //}

        //public int Remove<T>(T item)
        //{
        //    throw new NotImplementedException();
        //}

        //public int Update<T>(T entity)
        //{
        //    return EntityStore.Update<T>(entity);
        //}
    }
}
