using Microsoft.EntityFrameworkCore;
using Models.DbModels;

namespace BusinessLayer.Context
{
    /// <summary>
    /// 
    /// </summary>
    public class LokellaDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public LokellaDbContext(DbContextOptions<LokellaDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("sa_lokella");

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<MembershipLevel>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Business>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<BusinessInfo>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<BusinessCategory>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<StoredFile>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Business>()
                .HasOne<MembershipLevel>()
                .WithMany()
                .HasForeignKey(p => p.Level);

            modelBuilder.Entity<Business>()
                .HasOne<BusinessCategory>()
                .WithMany()
                .HasForeignKey(p => p.Category);

            modelBuilder.Entity<MenuCategory>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<MenuItem>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<CatalogAdditive>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<CatalogAllergy>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<CatalogMenuItemTag>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<MenuItemAdditive>()
                .HasKey(c => new { c.MenuItemId, c.AdditiveId });

            modelBuilder.Entity<MenuItemAllergy>()
                .HasKey(c => new { c.MenuItemId, c.AllergyId });

            modelBuilder.Entity<MenuItemTag>()
                .HasKey(c => new { c.MenuItemId, c.TagId });

            modelBuilder.Entity<MenuItemPrice>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<MenuItemPrice>()
                .Property(c => c.Quantity)
                .HasPrecision(5, 2);

            modelBuilder.Entity<MenuItemPrice>()
                .Property(c => c.Price)
                .HasPrecision(5, 2);

            modelBuilder.Entity<SpecialOffer>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<User>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<UserToken>()
                .HasKey(c => new { c.UserId, c.Token });

            modelBuilder.Entity<CustomerVisit>()
                .HasKey(c => new { c.CustomerId, c.BusinessId, c.VisitDate });

        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Business> Businesses { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<BusinessInfo> BusinessInfos { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<MembershipLevel> MembershipLevels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<BusinessCategory> BusinessCategories { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<StoredFile> StoredFiles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<MenuCategory> MenuCategories { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<MenuItem> MenuItems { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<CatalogAdditive> CatalogAdditives { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<CatalogAllergy> CatalogAllergies { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<CatalogMenuItemTag> CatalogMenuItemTags { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<MenuItemAdditive> MenuItemAdditives { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<MenuItemAllergy> MenuItemAllergies { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<MenuItemTag> MenuItemTags { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<MenuItemPrice> MenuItemPrices { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<SpecialOffer> SpecialOffers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<UserToken> UserTokens { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<CustomerVisit> CustomerVisits { get; set; }
    }
}
