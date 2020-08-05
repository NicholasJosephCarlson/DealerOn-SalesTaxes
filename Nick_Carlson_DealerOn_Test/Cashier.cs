using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nick_Carlson_DealerOn_Test.Models
{
    /// <summary>
    /// Responsible for handling the user's shopping basket, and generating Receipt
    /// </summary>
    public class Cashier
    {
        public ShoppingBasket basket { get; }

        public string GenerateReceipt()
        {
            string Receipt = "";





            return Receipt;
        }


        public ShoppingBasket AddtoBasket(Item newItem)
        {
            basket.Items.Add(newItem);
            return basket;
        }

        public ShoppingBasket ClearBasket()
        {
            basket.Items.Clear();
            basket.Total = 0.0f;
            basket.SalesTax = 0.0f;
            return basket;
        }


    }
}
