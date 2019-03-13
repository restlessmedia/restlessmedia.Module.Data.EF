using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace restlessmedia.Module.Data.EF
{
  public abstract class EntityConfiguration<T> : EntityTypeConfiguration<T>
    where T : Entity
  {
    public EntityConfiguration()
    {
      Property(x => x.EntityGuid).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
      Ignore(x => x.EntityId);
      Ignore(x => x.EntityType);
    }
  }
}