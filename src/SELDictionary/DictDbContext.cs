using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SELDictionary.Model;

namespace SELDictionary
{
    /// <summary>
    /// A read-only DbContext
    /// </summary>
    public abstract class DictDbContext : DbContext, IDictDbContextReadOnly
    {
        protected DictDbContext(DbContextOptions options) :  base(options) { }
        protected DbSet<Entities> Entities { get; set; }
        protected DbSet<Attributes> Attributes { get; set; }
        protected DbSet<UniqueAtts> UniqueAtts { get; set; }
        protected DbSet<Item> Items { get; set; }
        protected DbSet<ItemAttributions> ItemAtts { get; set; }
        protected DbSet<CorrelAtts> CorrelAtts { get; set; }
        protected DbSet<Relationships> Relationships { get; set; }
        protected DbSet<SourceDestObjectRels> SourceDestObjectRels { get; set; }
        protected DbSet<Enumerations> Enumerations { get; set; }
        protected DbSet<CodeLists> CodeLists { get; set; }
        protected DbSet<SPTPViews> SPTPViews { get; set; }
        protected DbSet<SPTPLayouts> SPTPLayouts { get; set; }
        protected DbSet<SPTPAttributes> SPTPAttributes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DictDbContext).Assembly);
        }

        /// <summary>
        /// Prevent calling the SaveChanges to commit to the DB
        /// </summary>
        public override int SaveChanges()
        {
            throw new Exception("This is a read-only context");
        }

        #region IDictDbContextReadOnly

        // Provide a read-only interface by returning entities with NoTracking

        IQueryable<Entities> IDictDbContextReadOnly.Entities => Set<Entities>().AsNoTracking();
        IQueryable<Attributes> IDictDbContextReadOnly.Attributes => Set<Attributes>().AsNoTracking();
        IQueryable<UniqueAtts> IDictDbContextReadOnly.UniqueAtts => Set<UniqueAtts>().AsNoTracking();
        IQueryable<Item> IDictDbContextReadOnly.Items => Set<Item>().AsNoTracking();
        IQueryable<ItemAttributions> IDictDbContextReadOnly.ItemAtts => Set<ItemAttributions>().AsNoTracking();
        IQueryable<CorrelAtts> IDictDbContextReadOnly.CorrelAtts => Set<CorrelAtts>().AsNoTracking();
        IQueryable<Relationships> IDictDbContextReadOnly.Relationships => Set<Relationships>().AsNoTracking();
        IQueryable<SourceDestObjectRels> IDictDbContextReadOnly.SourceDestObjectRels => Set<SourceDestObjectRels>().AsNoTracking();
        IQueryable<Enumerations> IDictDbContextReadOnly.Enumerations => Set<Enumerations>().AsNoTracking();
        IQueryable<CodeLists> IDictDbContextReadOnly.CodeLists => Set<CodeLists>().AsNoTracking();
        IQueryable<SPTPViews> IDictDbContextReadOnly.SPTPViews => Set<SPTPViews>().AsNoTracking();
        IQueryable<SPTPLayouts> IDictDbContextReadOnly.SPTPLayouts => Set<SPTPLayouts>().AsNoTracking();
        IQueryable<SPTPAttributes> IDictDbContextReadOnly.SPTPAttributes => Set<SPTPAttributes>().AsNoTracking();

        #endregion
    }

    public interface IDictDbContextReadOnly
    {
        IQueryable<Entities> Entities { get; }
        IQueryable<Attributes> Attributes { get; }
        IQueryable<UniqueAtts> UniqueAtts { get; }
        IQueryable<Item> Items { get; }
        IQueryable<ItemAttributions> ItemAtts { get; }
        IQueryable<CorrelAtts> CorrelAtts { get; }
        IQueryable<Relationships> Relationships { get; }
        IQueryable<SourceDestObjectRels> SourceDestObjectRels { get; }
        IQueryable<Enumerations> Enumerations { get; }
        IQueryable<CodeLists> CodeLists { get; }
        IQueryable<SPTPViews> SPTPViews { get; }
        IQueryable<SPTPLayouts> SPTPLayouts { get; }
        IQueryable<SPTPAttributes> SPTPAttributes { get; }
    }


    public interface ISELDictContextReadOnly : IDictDbContextReadOnly { }
    public interface ISITEDictContextReadOnly : IDictDbContextReadOnly { }
    public interface ISPLANTDictContextReadOnly : IDictDbContextReadOnly { }
}
