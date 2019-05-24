using System;
using System.Collections.Generic;

namespace LinqSamples.Models
{
    public class Order
    {
        public virtual int Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string CustomerName { get; set; }

        public virtual List<OrderItem> Items { get; set; }
    }
}
