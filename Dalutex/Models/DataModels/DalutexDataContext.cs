namespace Dalutex.Models.DataModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DalutexDataContext : DbContext
    {
        public DalutexDataContext()
            : base("name=DalutexConnection")
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .Property(e => e.NM_USU)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.USU_PWD)
                .IsUnicode(false);
        }
    }
}
