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
        public virtual DbSet<Address> Addresss { get; set; }
        public virtual DbSet<Bool> Bools { get; set; }
        public virtual DbSet<BuyCategory> BuyCategorys { get; set; }
        public virtual DbSet<BuyerUnit> BuyerUnits { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<CertificateContent> CertificateContents { get; set; }
        public virtual DbSet<Dep> Deps { get; set; }
        public virtual DbSet<Dep1> Dep1s { get; set; }
        public virtual DbSet<Dep2> Dep2s{ get; set; }
        public virtual DbSet<Dep3> Dep3s { get; set; }
        public virtual DbSet<Dep4> Dep4s { get; set; }
        public virtual DbSet<Graduation> Graduations { get; set; }
        public virtual DbSet<Highestedu> Highestedus { get; set; }
        public virtual DbSet<Hire> Hires { get; set; }
        public virtual DbSet<Hr> Hrs { get; set; }
        public virtual DbSet<Marriage> Marriages { get; set; }
        public virtual DbSet<Military> Militarys { get; set; }
        public virtual DbSet<Nationality> Nationalitys { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Relationship> Relationships { get; set; }
        public virtual DbSet<Send> Sends { get; set; }
        public virtual DbSet<Sex> Sexs { get; set; }
        public virtual DbSet<Status> Statuss { get; set; }
        public virtual DbSet<Title> Titless { get; set; }
        public virtual DbSet<UsersTable> UsersTables { get; set; }
        public virtual DbSet<Welfare> Welfares { get; set; }
        public virtual DbSet<WorkingHR> WorkingHRs { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Bool>()
                .Property(e => e.BoolName)
                .IsFixedLength();

            modelBuilder.Entity<Bool>()
                .HasMany(e => e.Hr)
                .WithOptional(e => e.Bool)
                .HasForeignKey(e => e.Blood);
            modelBuilder.Entity<BuyCategory>()
                .HasMany(e => e.Order)
                .WithOptional(e => e.BuyCategory1)
                .HasForeignKey(e => e.Category);

            modelBuilder.Entity<BuyerUnit>()
                .HasMany(e => e.Order)
                .WithOptional(e => e.BuyerUnit1)
                .HasForeignKey(e => e.BuyerUnit);

            modelBuilder.Entity<Certificate>()
                .Property(e => e.CertificateName)
                .IsFixedLength();

            modelBuilder.Entity<Certificate>()
                .HasMany(e => e.CertificateContent)
                .WithOptional(e => e.Certificate)
                .HasForeignKey(e => e.Category);



            modelBuilder.Entity<Dep>()
                .Property(e => e.DepID)
                .IsFixedLength();

            modelBuilder.Entity<Dep>()
                .Property(e => e.DepName)
                .IsFixedLength();

            modelBuilder.Entity<Dep>()
                .HasMany(e => e.Hr)
                .WithOptional(e => e.Dep1)
                .HasForeignKey(e => e.Dep);

            modelBuilder.Entity<Dep1>()
                .Property(e => e.Dep1ID)
                .IsFixedLength();

            modelBuilder.Entity<Dep1>()
                .Property(e => e.Dep1Name)
                .IsFixedLength();

            modelBuilder.Entity<Dep2>()
                .Property(e => e.Dep2ID)
                .IsFixedLength();

            modelBuilder.Entity<Dep2>()
                .Property(e => e.Dep2Name)
                .IsFixedLength();

            modelBuilder.Entity<Dep2>()
                .Property(e => e.Dep1ID)
                .IsFixedLength();

            modelBuilder.Entity<Dep3>()
                .Property(e => e.Dep3ID)
                .IsFixedLength();

            modelBuilder.Entity<Dep3>()
                .Property(e => e.Dep3Name)
                .IsFixedLength();

            modelBuilder.Entity<Dep3>()
                .Property(e => e.Dep2ID)
                .IsFixedLength();

            modelBuilder.Entity<Dep4>()
                .Property(e => e.Dep4ID)
                .IsFixedLength();

            modelBuilder.Entity<Dep4>()
                .Property(e => e.Dep4Name)
                .IsFixedLength();

            modelBuilder.Entity<Dep4>()
                .Property(e => e.Dep3ID)
                .IsFixedLength();
            modelBuilder.Entity<Graduation>()
                .Property(e => e.GraduationName)
                .IsFixedLength();

            modelBuilder.Entity<Graduation>()
                .HasMany(e => e.Hr)
                .WithOptional(e => e.Graduation1)
                .HasForeignKey(e => e.Graduation);

            modelBuilder.Entity<Highestedu>()
                .Property(e => e.HighestName)
                .IsFixedLength();

            modelBuilder.Entity<Highestedu>()
                .HasMany(e => e.Hr)
                .WithOptional(e => e.Highestedu1)
                .HasForeignKey(e => e.Highestedu);

            modelBuilder.Entity<Hire>()
                .Property(e => e.HireName)
                .IsFixedLength();

            modelBuilder.Entity<Hire>()
                .HasMany(e => e.Hr)
                .WithOptional(e => e.Hire1)
                .HasForeignKey(e => e.Hire);

         

            modelBuilder.Entity<Hr>()
                .Property(e => e.IDcardNO)
                .IsFixedLength();

            modelBuilder.Entity<Hr>()
                .Property(e => e.Dep)
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
           


            modelBuilder.Entity<Marriage>()
                .Property(e => e.MarriageName)
                .IsFixedLength();

            modelBuilder.Entity<Marriage>()
                .HasMany(e => e.Hr)
                .WithOptional(e => e.Marriage1)
                .HasForeignKey(e => e.Marriage);

            modelBuilder.Entity<Military>()
                .Property(e => e.MilitaryName)
                .IsFixedLength();

            modelBuilder.Entity<Military>()
                .HasMany(e => e.Hr)
                .WithOptional(e => e.Military1)
                .HasForeignKey(e => e.Military);

            modelBuilder.Entity<Nationality>()
                .Property(e => e.NationalityName)
                .IsFixedLength();

            modelBuilder.Entity<Nationality>()
                .HasMany(e => e.Hr)
                .WithOptional(e => e.Nationality1)
                .HasForeignKey(e => e.Nationality);

            modelBuilder.Entity<Order>()
                 .Property(e => e.BuyerName)
                 .IsFixedLength();

            modelBuilder.Entity<Order>()
                .Property(e => e.Money)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Order>()
                .Property(e => e.UserID)
                .IsFixedLength();

            modelBuilder.Entity<Relationship>()
                .Property(e => e.RelationshipName)
                .IsFixedLength();

            modelBuilder.Entity<Relationship>()
                .HasMany(e => e.Hr)
                .WithOptional(e => e.Relationship1)
                .HasForeignKey(e => e.Relationship);

            modelBuilder.Entity<Send>()
                .Property(e => e.SendName)
                .IsFixedLength();

            modelBuilder.Entity<Send>()
                .HasMany(e => e.Hr)
                .WithOptional(e => e.Send1)
                .HasForeignKey(e => e.Send);

            modelBuilder.Entity<Sex>()
                .Property(e => e.SexName)
                .IsFixedLength();

            modelBuilder.Entity<Sex>()
                .HasMany(e => e.Hr)
                .WithOptional(e => e.Sex1)
                .HasForeignKey(e => e.Sex);

            modelBuilder.Entity<Status>()
                .Property(e => e.StatusName)
                .IsFixedLength();

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Hr)
                .WithOptional(e => e.Status1)
                .HasForeignKey(e => e.Status);

            modelBuilder.Entity<Title>()
                .HasMany(e => e.Hr)
                .WithOptional(e => e.Title1)
                .HasForeignKey(e => e.TitleID);

            modelBuilder.Entity<UsersTable>()
                .Property(e => e.UserID)
                .IsFixedLength();

            modelBuilder.Entity<UsersTable>()
                .Property(e => e.UserName)
                .IsFixedLength();

            modelBuilder.Entity<Welfare>()
                .Property(e => e.WelfareName)
                .IsFixedLength();

            modelBuilder.Entity<Welfare>()
                .HasMany(e => e.Hr)
                .WithOptional(e => e.Welfare1)
                .HasForeignKey(e => e.Welfare);

            modelBuilder.Entity<WorkingHR>()
                .Property(e => e.WorkingHRName)
                .IsFixedLength();

            modelBuilder.Entity<WorkingHR>()
                .HasMany(e => e.Hr)
                .WithOptional(e => e.WorkingHR1)
                .HasForeignKey(e => e.WorkingHR);

            
        }
    }
}
