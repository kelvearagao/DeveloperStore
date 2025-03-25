using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale: BaseEntity
    {
        public int SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Branch { get; set; }
        public List<SaleItem> Items { get; set; } = new List<SaleItem>();
        public bool IsCancelled { get; set; } = false;
    }

    public class SaleItem
    {
        public int Id { get; set; }
        public Guid SaleId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; } = false;
    }
}