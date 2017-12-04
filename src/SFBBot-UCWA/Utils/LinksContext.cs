namespace SFBBot_UCWA.Utils
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LinksContext : DbContext
    {
        public LinksContext()
            : base("name=LinksContext")
        {
        }

        public virtual DbSet<DataStore> DataStores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
