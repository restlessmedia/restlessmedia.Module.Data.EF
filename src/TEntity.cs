using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restlessmedia.Module.Data.EF
{
  [Table("TEntity")]
  public class TEntity
  {
    public TEntity()
    {
      CreatedDate = DateTime.Now;
    }

    public TEntity(TLicense license)
      : this()
    {
      License = license;
    }

    [Key]
    public int EntityGuid { get; set; }

    public int? LicenseId { get; set; }

    public int EntityId { get; set; }

    public EntityType EntityType { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    [ForeignKey("LicenseId")]
    public TLicense License { get; set; }
  }
}
