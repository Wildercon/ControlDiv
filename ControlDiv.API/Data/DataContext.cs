using ControlDiv.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ControlDiv.API.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext>options ) : base(options) 
        {
            
        }

        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<PriceDollar> Prices { get; set; }
        public DbSet<TemporalSale> Temporals { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Voucher>().HasIndex(x => x.Code).IsUnique();
            modelBuilder.Entity<TemporalSale>().HasIndex(x => x.VoucherCode).IsUnique();
        }
    }
}
