using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nick_Carlson_DealerOn_Test.Models
{
    public class Item
    {
        public Item()
        {
            Name = "";
            Price = 0.00M;
            TaxExempt = false;
            Imported = false;
            Quantity = 1;
        }

        public Item(Item item)
            {
            Name = item.Name;
            Price = item.Price;
            TaxExempt = item.TaxExempt;
            Imported = item.Imported;
            Quantity = item.Quantity;
            }

       public string Name { get; set; }
       public decimal Price { get; set; }
       public bool TaxExempt { get; set; }
       public bool Imported { get; set; }
       public int Quantity { get; set; }
    }
}
