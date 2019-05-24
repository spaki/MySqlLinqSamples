namespace LinqSamples.Models
{
    public class OrderItem
    {
        public virtual int Id { get; set; }
        public virtual decimal Value { get; set; }
        public virtual int Quantity { get; set; }
        public virtual string ProductName { get; set; }
        public virtual Order Order { get; set; }
    }
}
