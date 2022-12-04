using TPFinal_GSC.Entities;

namespace TPFinal_GSC.DataAccess.Interfaces
{
    public interface IGenericRepository<TEntity>
        where TEntity : EntityBase
    {
        TEntity Add(TEntity entity);
        bool Delete(int id);
        TEntity Update(TEntity entity);
        TEntity GetById(int id);
        List<TEntity> GetAll();
    }
}
