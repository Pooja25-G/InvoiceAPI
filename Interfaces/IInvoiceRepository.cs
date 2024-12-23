using InvoiceMangementApi.Models;

namespace InvoiceMangementApi.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<InvoiceData>> GetAllAsync();
        Task<InvoiceData> GetByIdAsync(int id);
        Task AddAsync(InvoiceData invoice);
        Task UpdateAsync(InvoiceData invoice);
        Task DeleteAsync(int id);

    }
}
