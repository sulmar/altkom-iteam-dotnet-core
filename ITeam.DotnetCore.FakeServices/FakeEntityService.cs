using ITeam.DotnetCore.IServices;
using ITeam.DotnetCore.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ITeam.DotnetCore.FakeServices
{
    public class FakeEntityService<TEntity> : IEntityService<TEntity>
        where TEntity : BaseEntity
    {
        protected ICollection<TEntity> entities;

        public FakeEntityService()
        {
            entities = new Collection<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            entities.Add(entity);
        }

        public virtual ICollection<TEntity> Get()
        {
            return entities;
        }

        public virtual TEntity Get(int id)
        {
            return entities.SingleOrDefault(e => e.Id == id);
        }

        public virtual void Remove(int id)
        {
            entities.Remove(Get(id));
        }

        public virtual void Update(TEntity entity)
        {
            Remove(entity.Id);
            Add(entity);
        }
    }
}
