using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fabrizio.NetCore.Repository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        IEnumerable<T2> Exec<T2>(string sql, params object[] parameters) where T2 : class;
        Task<IEnumerable<T2>> ExecAsync<T2>(string sql, params object[] parameters) where T2 : class;
        T GetById(int id);
        IQueryable<T> List();
        IQueryable<T> List(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> ListAsync();
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate);
    }
}