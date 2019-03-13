using System.Linq;

namespace restlessmedia.Module.Data.EF
{
  public class LicensedEntityRepository<T> : EntityRepository<T>
    where T : LicensedEntity
  {
    public LicensedEntityRepository(DatabaseContext context)
      : base(context) { }

    protected IQueryable<T> Licensed()
    {
      return Set().Where(x => x.LicenseId == Context.LicenseId);
    }
  }
}