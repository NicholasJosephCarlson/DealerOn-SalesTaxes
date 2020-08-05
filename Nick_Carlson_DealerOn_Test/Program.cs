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
            bool running = true;
            String input;
            Cashier cashier = new Cashier();
            Console.WriteLine(" DealerOn Development Candidate Coding Test - Problem #2 : Sales Taxes");
            Console.WriteLine(" Nicholas Joseph Carlson - 8 / 5 / 2020");
            Console.WriteLine(" Type 'Unit Test' to run the test input found in UnitTest.txt");
            Console.WriteLine(" -----------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Welcome to the store where you name the price!");
            Console.WriteLine();
            Console.WriteLine("Please enter your selection in the following format : ");
            Console.WriteLine("[Quantity - integer] [Item Name - string] at [Price - decimal] ");
            Console.WriteLine();
            Console.WriteLine(cashier.DisplayProducts());
            Console.WriteLine();

            while (running)
            {
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))//Generate Receipt
                {
                    if (cashier.Basket.Items.Count < 1)
                    {
                        Console.WriteLine("Please enter a quantity, item name, and price.");
                        continue;
                    }

                    Console.WriteLine(cashier.GenerateReceipt());
                    Console.WriteLine("Hit Enter to start a new basket, or type EXIT to close the program.");

                    string finalInput = Console.ReadLine();

                    if (finalInput.ToUpper().Trim() == "EXIT")
                    {
                        running = false;
                    }
                    else
                    {
                        cashier.ClearBasket();
                        Console.WriteLine();
                        Console.WriteLine("Please enter your selection in the following format : ");
                        Console.WriteLine("[Quantity - integer] [Item Name - string] at [Price - decimal] ");
                        Console.WriteLine();
                        Console.WriteLine(cashier.DisplayProducts());
                        Console.WriteLine();
                    }
                }
                else if (input.Trim().ToLower() == "unit test")
                {
                    cashier.UnitTest();
                    Console.WriteLine("Hit Enter to start a new basket, or type EXIT to close the program.");

                    string finalInput = Console.ReadLine();

                    if (finalInput.ToUpper().Trim() == "EXIT")
                    {
                        running = false;
                    }
                    else
                    {
                        cashier.ClearBasket();
                        Console.WriteLine();
                        Console.WriteLine("Please enter your selection in the following format : ");
                        Console.WriteLine("[Quantity - integer] [Item Name - string] at [Price - decimal] ");
                        Console.WriteLine();
                        Console.WriteLine(cashier.DisplayProducts());
                        Console.WriteLine();
                    }
                }
                else // attempt to parse input and add to basket.
                {
                    int quantity;
                    string name = "";
                    decimal price = 0.0M;

                    string[] splitInput = input.Split(' ');

                    try
                    {
                        quantity = int.Parse(splitInput[0]);

                        if (quantity <= 0)
                        {
                            throw new Exception("Invalid quantity.");
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Please enter a valid quantity.");
                        continue;
                    }

                    for (int i = 1; i < splitInput.Length; i++)
                    {
                        if (splitInput[i] == "at")
                        {
                            try
                            {
                                price = decimal.Parse(splitInput[i + 1]);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Please enter a valid Price.");
                            }

                            break;
                        }
                        else
                        {
                            name += " " + splitInput[i];
                        }
                    }

                    Console.WriteLine(cashier.AddtoBasket(quantity, name, price));
                    Console.WriteLine("Continue Shopping, or hit the Enter Key to complete your purchase and generate a receipt.");
                    Console.WriteLine();
                }



            }
        }
    }
}
