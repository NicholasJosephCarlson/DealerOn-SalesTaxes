using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nick_Carlson_DealerOn_Test.Models
{
    public class Item
    {
        #region Constructors
        public Item()
        {
            Imported = false;
            Name = "";
            Price = 0.00M;
            Quantity = 1;
            TaxExempt = false;
        }

        public Item(Item item)
        {
            Imported = item.Imported;
            Name = item.Name;
            Price = item.Price;
            Quantity = item.Quantity;
            TaxExempt = item.TaxExempt;
        }
        #endregion

        #region Variables 
        public bool Imported { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool TaxExempt { get; set; }
        #endregion
    }
}
