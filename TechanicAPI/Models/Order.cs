using System;
using System.Collections.Generic;

namespace TechanicAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int productId { get; set; }
        public int quantity {get; set; }

        public double total { get; set; }
    }
}
