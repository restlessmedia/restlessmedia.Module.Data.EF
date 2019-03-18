using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace restlessmedia.Module.Data.EF
{
  public class DatabaseContext<T> : DatabaseContext
    where T : class
  {
    public DatabaseContext(IDataContext dataContext, bool autoDetectChanges = false)
      : base(dataContext, autoDetectChanges) { }

    public DbSet<T> Data
    {
      get
      {
        return Set<T>();
      }
    }
  }

  public class DatabaseContext : DbContext
  {
    static DatabaseContext()
    {
      DbConfiguration.SetConfiguration(new DatabaseConfiguration());
      Database.SetInitializer<DatabaseContext>(null);
    }

    private DatabaseContext(DbConnection connection)
      : base(connection, contextOwnsConnection: false)
    {
      _connection = connection;
    }

    public DatabaseContext(IDataContext dataContext, bool autoDetectChanges = false)
      : this(CreateConnection(dataContext))
    {
      _dataContext = dataContext;
      Configuration.LazyLoadingEnabled = false;
      Configuration.AutoDetectChangesEnabled = autoDetectChanges;
      Configuration.ProxyCreationEnabled = false;
    }

    public DbSet<TEntity> Entity
    {
      get
      {
        return Set<TEntity>();
      }
    }

    public DbSet<TLicense> License
    {
      get
      {
        return Set<TLicense>();
      }
    }

    public int LicenseId
    {
      get
      {
        return LicenseHelper.GetLicenseId(_connection, _dataContext.LicenseSettings.LicenseKey);
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (_isDisposed)
      {
        return;
      }

      _isDisposed = true;
      _connection.Dispose();
      base.Dispose(disposing);
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      Configure(modelBuilder);
      base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
      LicenseHelper.SetContext(_connection, _dataContext.LicenseSettings);
      return base.SaveChanges();
    }

    protected virtual void Configure(DbModelBuilder modelBuilder)
    {
      modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

      modelBuilder.Entity<TLicense>();
      modelBuilder.Entity<TEntity>();
    }

    private readonly IDataContext _dataContext;

    private static DbConnection CreateConnection(IDataContext dataContext)
    {
      if (dataContext == null)
      {
        throw new ArgumentException("dataContext");
      }

      DbConnection connection = dataContext.ConnectionFactory.CreateConnection(true) as DbConnection;

      if (connection == null)
      {
        throw new InvalidOperationException($"The connection returned from CreateConnection is null or not a {nameof(DbConnection)}.");
      }

      return connection;
    }

    private bool _isDisposed = false;

    private readonly DbConnection _connection;
  }
}