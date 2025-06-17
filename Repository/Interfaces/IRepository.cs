using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query();

        IQueryable<T> QueryAsNoTracking();
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
        int SaveChanges();
        DbContextTransaction BeginTransaction();
        void Rollback();
        void Commit();
        IEnumerable<T> ExecuteStoredProcedure<T>(string procedureName, object parameters) where T : class;
    }
}
