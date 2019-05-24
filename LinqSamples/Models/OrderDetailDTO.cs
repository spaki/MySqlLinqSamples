namespace LinqSamples.Models
{
    public class OrderDetailDTO
    {
        public virtual int OrderId { get; set; }
        public virtual string Date { get; set; }
        public virtual string CustomerName { get; set; }
        public virtual int OrderItemId { get; set; }
        public virtual string Value { get; set; }
        public virtual int Quantity { get; set; }
        public virtual string ProductName { get; set; }
    }
}
