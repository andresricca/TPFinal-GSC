using Microsoft.EntityFrameworkCore;
using TPFinal_GSC.DataAccess.Interfaces;
using TPFinal_GSC.Entities;

namespace TPFinal_GSC.DataAccess
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : EntityBase
    {
        protected LoansContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(LoansContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            var savedEntity = dbSet.Add(entity);
            return savedEntity.Entity;
        }

        public bool Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null)
                return false;
            dbSet.Remove(entity);
            return true;
        }

        public List<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public TEntity GetById(int id)
        {
            return dbSet.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public TEntity Update(TEntity entity)
        {
            var changedEntity = dbSet.Update(entity);
            return changedEntity.Entity;
        }
    }
}
