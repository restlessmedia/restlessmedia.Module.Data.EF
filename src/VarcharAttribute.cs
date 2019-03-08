using System.ComponentModel.DataAnnotations.Schema;

namespace restlessmedia.Module.Data.EF
{
  public class VarcharAttribute : ColumnAttribute
  {
    public VarcharAttribute()
    {
      TypeName = _typeName;
    }

    private const string _typeName = "varchar";
  }
}
