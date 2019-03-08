using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace restlessmedia.Module.Data.EF
{
  public abstract class Repository<T> : Repository
    where T : class
  {
    public Repository(DatabaseContext context)
      : base(context)
    { }

    public virtual int Count()
    {
      return Set().Count();
    }

    public virtual T Find(object id)
    {
      return Set().Find(id);
    }

    public bool Exists(Expression<Func<T, bool>> predicate)
    {
      return Set().Any(predicate);
    }

    protected T Add(T obj)
    {
      return Add<T>(obj);
    }

    protected T Update(T obj, params Expression<Func<T, object>>[] propertiesToUpdate)
    {
      return Update<T>(obj, propertiesToUpdate);
    }

    protected DbSet<T> Set()
    {
        return Context.Set<T>();
    }
  }

  public abstract class Repository
  {
    public Repository(DatabaseContext context)
    {
      Context = context ?? throw new ArgumentNullException("context");
    }

    protected void ThrowEntityNotFoundException(EntityType entityType, int entityId)
    {
      throw new RecordNotFoundException($"Entity not found for {entityType} with id {entityId}");
    }

    protected TEntity GetEntity(EntityType entityType, int entityId)
    {
      return Context.Entity.FirstOrDefault(x => x.EntityId == entityId && x.EntityType == entityType);
    }

    protected TEntity Add<TEntity>(TEntity obj)
      where TEntity : class
    {
      return Context.Set<TEntity>().Add(obj);
    }

    protected TEntity Update<TEntity>(TEntity obj, params Expression<Func<TEntity, object>>[] propertiesToUpdate)
      where TEntity : class
    {
      Context.Set<TEntity>().Attach(obj);

      foreach (var p in propertiesToUpdate)
      {
        Context.Entry(obj).Property(p).IsModified = true;
      }

      return obj;
    }

    protected readonly DatabaseContext Context;
  }
}