using System.ComponentModel.DataAnnotations.Schema;

namespace restlessmedia.Module.Data.EF
{
  public class LicensedEntityConfiguration<T> : EntityConfiguration<T>
    where T : LicensedEntity
  {
    public LicensedEntityConfiguration()
      : base()
    {
      Property(x => x.LicenseId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
    }
  }
}