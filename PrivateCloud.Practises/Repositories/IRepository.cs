using System;
using System.Linq;

namespace PrivateCloud.Practises.Repositories
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();

        TEntity GetById(
            Guid id);

        TEntity Add(
            TEntity entity);

        void Update(
            TEntity entity);

        void Delete(
            TEntity entity);
    }
}
