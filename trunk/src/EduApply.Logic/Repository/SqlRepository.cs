using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EduApply.Logic.Interfaces;

namespace EduApply.Logic.Repository
{
    public class SqlRepository : IRepository
    {
        private readonly IDbContext context;

        public SqlRepository(IDbContext context)
        {
            this.context = context;
        }
        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            GetEntities<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return GetEntities<TEntity>().AsQueryable();
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            GetEntities<TEntity>().Add(entity);
        }
        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            var context = this.context as EFContext;
            var entry = context.Entry<TEntity>(entity);
            if (entry.State == EntityState.Detached)
            {
                this.context.Set<TEntity>().Attach(entity);
            }

            entry.State = EntityState.Modified;

        }


        public TEntity Find<TEntity>(object Id) where TEntity : class
        {
            return GetEntities<TEntity>().Find(Id);
        }

        private IDbSet<TEntity> GetEntities<TEntity>() where TEntity : class
        {
            return this.context.Set<TEntity>();
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
            if (this.context != null)
            {
                this.context.Dispose();
            }
        }


        public async void SaveChangesAsync()
        {

            await this.context.SaveChangesAsync();
        }



    }
}
