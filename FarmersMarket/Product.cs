using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmersMarket
{
    internal class Product
    {
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public double Amount { get; set; }
        public double PricePerKg { get; set; }
    }
}
