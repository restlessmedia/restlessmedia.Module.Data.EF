using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restlessmedia.Module.Data.EF
{
  [Table("TLicense")]
  public class TLicense
  {
    [Key]
    public int LicenseId { get; set; }

    public Guid LicenseKey { get; set; }

    [Varchar]
    public string Description { get; set; }

    public Guid ApplicationId { get; set; }
  }
}
