using InvoiceMangementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceMangementApi.Data
{
    public class InvoiceDataDbContext : DbContext
    {
        public InvoiceDataDbContext(DbContextOptions<InvoiceDataDbContext> options) : base(options) { }

        public DbSet<InvoiceData> Invoices { get; set; }
        public DbSet<InvoiceItems> InvoiceItems { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}

