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
    }
}
