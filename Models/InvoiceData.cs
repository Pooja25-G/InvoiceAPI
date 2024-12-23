namespace InvoiceMangementApi.Models
{
    public class InvoiceData
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public List<InvoiceItems> Items { get; set; } = new List<InvoiceItems>();
        public decimal TotalAmt { get; set; }
        public string Status { get; set; } // Draft, Sent, Paid, Overdue
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

    }

    
}
