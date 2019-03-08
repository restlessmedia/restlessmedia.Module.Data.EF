using System;

namespace restlessmedia.Module.Data.EF
{
  public class RepositoryException : Exception
  {
    public RepositoryException(string message)
      : base(message)
    { }
  }
}