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
        static void Main(string[] args)
        {
            bool running = true;
            String input = "";
            Cashier cashier = new Cashier();

            while (running)
            {
                Console.WriteLine("Welcome to the store! ");
                input  = Console.ReadLine();

                if (input == "X")
                {
                    running = false;
                }

                Console.WriteLine("THIS WAS YOUR INPUT: " + input);
              

            


            }
        }
    }
}
