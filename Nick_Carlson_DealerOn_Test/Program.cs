using Nick_Carlson_DealerOn_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nick_Carlson_DealerOn_Test
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(" DealerOn Development Candidate Coding Test - Problem #2 : Sales Taxes");
            Console.WriteLine(" Nicholas Joseph Carlson - 8 / 5 / 2020");
            Console.WriteLine(" Type 'Unit Test' to run the test input found in UnitTest.txt");
            Console.WriteLine(" -----------------------------------------------------------------------------------");
            bool On = true;
            Cashier cashier = new Cashier();
            cashier.DisplayProducts();
            while (On)
            {
                On = cashier.Working();
            }
        }
    }
}
