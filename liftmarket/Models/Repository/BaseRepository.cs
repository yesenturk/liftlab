using liftmarket.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace liftmarket.Models.Repository
{
    public abstract class AbstractBaseRepository<T> : IDisposable
      where T : class, IBaseModel
    {
        internal DatabaseContext context = null;

        public DbSet<T> Entity { get { return context.Set<T>(); } }

        public AbstractBaseRepository()
        {
            context = new DatabaseContext();
        }

        public virtual bool Add(T entity)
        {
            entity.CreateDate = DateTime.Now;
            Entity.Add(entity);

            return context.SaveChanges() > 0;
        }

        public virtual T Find(int Id)
        {
            return Entity.FirstOrDefault(x => x.Id == Id);
        }

        public virtual bool Delete(T entity)
        {
            if (entity != null && entity.Id != default(int))
            {
                var record = Find(entity.Id);
                if (record == null)
                {
                    throw new NullReferenceException(nameof(entity.Id));
                }
                record.IsDeleted = true;
                record.DeleteDate = DateTime.Now;

                return context.SaveChanges() > 0;

            }

            throw new ArgumentOutOfRangeException(nameof(entity.Id));
        }

        public virtual bool Update(T entity)
        {
            if (entity != null && entity.Id != default(int))
            {
                var record = Find(entity.Id);
                if (record == null)
                {
                    throw new NullReferenceException(nameof(entity.Id));
                }

                record = entity;
                record.UpdateDate = DateTime.Now;

                return context.SaveChanges() > 0;
            }

            throw new ArgumentOutOfRangeException(nameof(entity.Id));
        }

        public virtual IQueryable<T> ListAll()
        {
            return Entity.Where(x => !x.IsDeleted);
        }

        public virtual int Count()
        {
            return ListAll().Count();
        }

        public virtual IQueryable<T> ListPaged(Expression<Func<T, bool>> expression, int pageNumber, int pageSize = 10)
        {
            var totalRecordCount = Count();
            var currentPage = pageNumber == 1 ? 0 : pageNumber - 1;

            return ListAll()
                .OrderByDescending(x => x.CreateDate)
                .Where(expression)
                .Skip(pageSize * currentPage)
                .Take(pageSize);
        }

        public IQueryable<K> Query<K>() where K : class
        {
            return context.Set<K>();
        }


        readonly string _connString = ConfigurationManager.ConnectionStrings["liftmarketConnectionString"].ConnectionString;

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }
    }



    public class BaseRepository<T> : AbstractBaseRepository<T>
        where T : class, IBaseModel
    {

    }
}