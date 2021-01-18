using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBus.Contract
{
    public class ProductCreated
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal price { get; set; }
    }
}
