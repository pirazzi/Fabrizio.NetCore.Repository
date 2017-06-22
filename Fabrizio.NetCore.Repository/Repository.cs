using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fabrizio.NetCore.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public virtual IQueryable<T> List()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public virtual async Task<IEnumerable<T>> ListAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public IEnumerable<T2> Exec<T2>(string sql, params object[] parameters) where T2 : class
        {
            return _dbContext.Set<T2>().FromSql(sql, parameters);
        }

        /// <summary>
        /// Esegue un stored procedure e restituice un recordset
        /// </summary>
        /// <typeparam name="T2">Il tipo dati restituito tramite interfaccia IEnumerable</typeparam>
        /// <param name="sql">Nome della stored procedure, comprensiva di parametria, Es. 'NomeSP p1, p2, p3' </param>
        /// <param name="parameters">Elenco dei parametri necessati al funzionamento della procedura</param>
        /// <returns></returns>
        public async Task<IEnumerable<T2>> ExecAsync<T2>(string sql, params object[] parameters) where T2 : class
        {

            var result = await Task.Run<IEnumerable<T2>>(() =>
            {
                return _dbContext.Set<T2>().FromSql(sql, parameters);
            });

            return result;

        }

        public IQueryable<T> List(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate).AsQueryable();
        }

        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void Edit(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }


    }
}
