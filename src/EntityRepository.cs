using System;
using System.Linq;

namespace restlessmedia.Module.Data.EF
{
  public class EntityRepository<T> : Repository<T>
    where T : Entity
  {
    public EntityRepository(DatabaseContext context)
      : base(context)
    { }
  }

  public class EntityRepository : Repository
  {
    public EntityRepository(DatabaseContext context)
      : base(context)
    { }

    public void Update(IEntity entity, DateTime? updatedDate)
    {
      TEntity existingEntity = Context.Entity.Where(x => x.EntityType == entity.EntityType && x.EntityId == entity.EntityId).FirstOrDefault();

      if (existingEntity == null)
      {
        return; // throw error?
      }

      existingEntity.UpdatedDate = updatedDate;

      Update(existingEntity, x => x.UpdatedDate);
    }
  }
}