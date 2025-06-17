using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        private DbContextTransaction _tran;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> ExecuteStoredProcedure<T>(string procedureName, object parameters) where T : class
        {
            // Lấy các tham số từ object thông qua reflection
            var sqlParams = new List<SqlParameter>();
            foreach (var prop in parameters.GetType().GetProperties())
            {
                var value = prop.GetValue(parameters) ?? DBNull.Value;
                sqlParams.Add(new SqlParameter("@" + prop.Name, value));
            }

            // Tạo câu lệnh stored procedure
            var sql = $"EXEC {procedureName} " + string.Join(", ", sqlParams.Select(p => p.ParameterName));

            // Gọi stored procedure và trả về kết quả
            return _context.Database.SqlQuery<T>(sql, sqlParams.ToArray()).ToList();
        }

        public IQueryable<T> Query()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<T> QueryAsNoTracking()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public DbContextTransaction BeginTransaction() {
            _tran = _context.Database.BeginTransaction();
            return _tran;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Commit()
        {
            _tran.Commit();
        }
        public void Rollback()
        {
            _tran.Rollback();
        }

    }

}
