namespace InvoiceMangementApi.Models
{
    public class InvoiceItems
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public decimal ProductAmt { get; set; }
        public decimal TotalCost => Qty * ProductAmt;

    }
}
