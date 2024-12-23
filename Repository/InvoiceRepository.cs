using InvoiceMangementApi.Data;
using InvoiceMangementApi.Interfaces;
using InvoiceMangementApi.Models;
using Microsoft.EntityFrameworkCore;
namespace InvoiceMangementApi.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly InvoiceDataDbContext _context;
        public InvoiceRepository(InvoiceDataDbContext context) { _context = context; }

        public async Task<IEnumerable<InvoiceData>> GetAllAsync() => await _context.Invoices.Include(i => i.Items).ToListAsync();
        public async Task<InvoiceData> GetByIdAsync(int id) => await _context.Invoices.Include(i => i.Items).FirstOrDefaultAsync(i => i.Id == id);
        public async Task AddAsync(InvoiceData invoice) { _context.Invoices.Add(invoice); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(InvoiceData invoice) { _context.Invoices.Update(invoice); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id) { var invoice = await GetByIdAsync(id); _context.Invoices.Remove(invoice); await _context.SaveChangesAsync(); }

    }
}
