using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JanusData.Interfaces
{
    public interface IRepository
    {
        T Add<T>(T entity) where T : new();

        int Remove<T>(T item);

        int Update<T>(T item);

        Task<T> FindByKeyAsync<T>(dynamic key);

        Task<T> FindSingleAsync<T>(Expression<Func<T, bool>> predicateExpression);

        Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> predicateExpression);

        T FindByKey<T>(dynamic id);

        IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate);

        IEnumerable<T> FindAll<T>();

        IEnumerable<Tuple<T, R>> FindAll<T, R>() where R : new();

        IEnumerable<Tuple<T, R>> Find<T, R>(Expression<Func<T, R, bool>> filterExpression) where R : new();

        IEnumerable<T> FindAndOrderby<T>(Expression<Func<T, bool>> predicateExpression, Expression<Func<T, object>> orderByExpression);
    }
}
