using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBus.Producer.Models.Request
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public decimal price { get; set; }
    }
}
