namespace AllYouMedia.DataAccess.DataLayer
{
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using AllYouMedia.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using System.Reflection;

    public class DataContext : IdentityDbContext<AspNetUser, AspNetRole, long, AspNetUserLogin, AspNetUserRole, AspNetUserClaim>, IDbContext
    {
        public DataContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<DataContext>(null);
#if DEBUG
            Database.Log = s => System.Diagnostics.Trace.WriteLine(s);
#endif
        }
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public DbSet<AspNetUserAddress> AspNetUserAddresss { get; set; }
        public DbSet<AspNetUserHierarchy> AspNetUserHierarchys { get; set; }
        public DbSet<AspNetUserConnection> AspNetUserConnections { get; set; }
        public DbSet<AllYouMedia.DataAccess.EntityLayer.DBEntity.Attribute> Attributes { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<CategoryType> CategoryTypes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemAttribute> ItemAttributes { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PayoutDistribution> PayoutDistributions { get; set; }
        public DbSet<SubCategory> SubCategorys { get; set; }
        public DbSet<Fan> Fans { get; set; }
        public DbSet<FanUserMap> FanUserMaps { get; set; }
        public DbSet<UserSpotlight> UserSpotlights { get; set; }
        public DbSet<FanSharingUserRequest> FanSharingRequests { get; set; }
        public DbSet<FanSharingFanRequest> FanSharingFanRequests { get; set; }
        public DbSet<GenderSpecific> GenderSpecifics { get; set; }

        public DbSet<GenreCategory> GenreCategorys { get; set; }
        public DbSet<InstrumentCategory> InstrumentCategorys { get; set; }
        public DbSet<InstrumentSpecificationCategory> InstrumentSpecificationCategorys { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("");
            //modelBuilder.HasDefaultSchema("public");
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !string.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            modelBuilder.Entity<AspNetUser>().Property(x => x.UserName).HasMaxLength(200);
            modelBuilder.Entity<AspNetUser>().Property(x => x.Email).HasMaxLength(200);

            modelBuilder.Entity<AspNetUserHierarchy>().HasRequired(f => f.AspNetUser).WithMany(a => a.AspNetUser_AspNetUserHierarchy).HasForeignKey(f => f.AspNetUserID);
            modelBuilder.Entity<AspNetUserHierarchy>().HasRequired(f => f.ParentAspNetUser).WithMany(a => a.ParentAspNetUser_AspNetUserHierarchy).HasForeignKey(f => f.ParentAspNetUserID);

            modelBuilder.Entity<AspNetUserConnection>().HasRequired(f => f.AspNetUser).WithMany(a => a.AspNetUser_AspNetUserConnection).HasForeignKey(f => f.AspNetUserID);
            modelBuilder.Entity<AspNetUserConnection>().HasRequired(f => f.ConnectedAspNetUser).WithMany(a => a.ConnectedAspNetUser_AspNetUserConnection).HasForeignKey(f => f.ConnectedAspNetUserID);

            modelBuilder.Entity<FanSharingUserRequest>().HasRequired(f => f.AspNetUser).WithMany(a => a.AspNetUser_FanSharingRequests).HasForeignKey(f => f.AspNetUserID);
            modelBuilder.Entity<FanSharingUserRequest>().HasRequired(f => f.RequestingAspNetUser).WithMany(a => a.RequestingAspNetUser_FanSharingRequests).HasForeignKey(f => f.RequestingAspNetUserID);

            modelBuilder.Entity<UserSpotlight>().HasRequired(f => f.AspNetUser).WithMany(a => a.AspNetUser_UserSpotlights).HasForeignKey(f => f.AspNetUserID);
            modelBuilder.Entity<UserSpotlight>().HasRequired(f => f.ReviewingAspNetUser).WithMany(a => a.ReviewingAspNetUser_UserSpotlights).HasForeignKey(f => f.ReviewingAspNetUserID);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AspNetUserRole>().HasKey(hk => hk.ID);
            modelBuilder.Entity<Category>()
            .HasMany<GenderSpecific>(s => s.GenderSpecific)
            .WithMany(c => c.Categories)
            .Map(cs =>
            {
                cs.MapLeftKey("CategoryId");
                cs.MapRightKey("GenderSpecificId");
                cs.ToTable("GenderSpecificCategory");
            });

            modelBuilder.Entity<GenderSpecific>()
         .HasMany<GenreCategory>(s => s.GenreCategory)
         .WithMany(c => c.genderSpecific)
         .Map(cs =>
         {
             cs.MapLeftKey("GenderSpecificId");
             cs.MapRightKey("GenerCategoryId");
             cs.ToTable("GenderSpecificGenerCategory");
         });


            modelBuilder.Entity<Category>()
         .HasMany<GenreCategory>(s => s.GenreCategory)
         .WithMany(c => c.Categories)
         .Map(cs =>
         {
             cs.MapLeftKey("CategoryId");
             cs.MapRightKey("GenerCategoryId");
             cs.ToTable("CategoryGener");
         });

            modelBuilder.Entity<GenreCategory>()
       .HasMany<InstrumentCategory>(s => s.InstrumentCategory)
       .WithMany(c => c.GenreCategory)
       .Map(cs =>
       {
           cs.MapLeftKey("GenreCategoryId");
           cs.MapRightKey("InstrumentCategoryId");
           cs.ToTable("InstrumentCategoryGener");
       });
         
        }
    }
}