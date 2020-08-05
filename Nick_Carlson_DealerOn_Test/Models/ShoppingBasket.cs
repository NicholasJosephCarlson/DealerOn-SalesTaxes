using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nick_Carlson_DealerOn_Test.Models
{
    public class ShoppingBasket
    {
       public List<Item> Items { get; set; }
       public float SalesTax { get; set; }
       public float Total { get; set; } 

    }
}
