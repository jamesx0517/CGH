namespace CGH.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CGHContext : DbContext
    {
        public CGHContext()
            : base("name=CGH")
        {
        }

        public virtual DbSet<Hr> Hrs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hr>()
                .Property(e => e.Name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Hr>()
                .Property(e => e.IDcardNO)
                .IsFixedLength();

            modelBuilder.Entity<Hr>()
                .Property(e => e.Height)
                .IsFixedLength();

            modelBuilder.Entity<Hr>()
                .Property(e => e.Weight)
                .IsFixedLength();

            modelBuilder.Entity<Hr>()
                .Property(e => e.Health)
                .IsFixedLength();

            modelBuilder.Entity<Hr>()
                .Property(e => e.Birthplace)
                .IsFixedLength();

            modelBuilder.Entity<Hr>()
                .Property(e => e.MobilePhone)
                .IsFixedLength();

            modelBuilder.Entity<Hr>()
                .Property(e => e.HomePhone)
                .IsFixedLength();

            modelBuilder.Entity<Hr>()
                .Property(e => e.Emergency)
                .IsFixedLength();

            modelBuilder.Entity<Hr>()
                .Property(e => e.Ephone)
                .IsFixedLength();
        }
    }
}
