namespace restlessmedia.Module.Data.EF
{
  public class RecordNotFoundException : RepositoryException
  {
    public RecordNotFoundException(string message)
      : base(message)
    { }
  }
}