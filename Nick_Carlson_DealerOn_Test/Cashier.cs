using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nick_Carlson_DealerOn_Test.Models
{
    /// <summary>
    /// Responsible for handling the user's shopping basket, and generating receipt.
    /// </summary>
    public class Cashier
    {
        public ShoppingBasket Basket { get; }
        public List<Item> AvailableItems { get; }

        public Cashier()
        {
            Basket = new ShoppingBasket { Items = new List<Item>(), SalesTax = 0.00M, Total = 0.00M };

            AvailableItems = new List<Item>
            {
                //Imported 
                new Item() { Name = "Imported bottle of perfume", Imported = true, Price = 0.00M, TaxExempt = false },
                new Item { Name = "Imported box of chocolates", Imported = true, Price = 0.00M, TaxExempt = true },

                //Domestic - Tax Exempt
                new Item { Name = "Book", Imported = false, Price = 0.00M, TaxExempt = true },
                new Item { Name = "Chocolate bar", Imported = false, Price = 0.00M, TaxExempt = true },
                new Item { Name = "Packet of headache pills", Imported = false, Price = 0.00M, TaxExempt = true },
                
                //Domestic - Taxable
                new Item { Name = "Bottle of perfume", Imported = false, Price = 0.00M, TaxExempt = false },
                new Item { Name = "Music CD", Imported = false, Price = 0.00M, TaxExempt = false },
            };

        }

        public string DisplayProducts()
        {
            string display = "Available Products : \n";
            display += "=======================================================================\n";

            foreach (var item in AvailableItems)
            {
                string newline = item.Name + " \n";
                display += newline;
            }
            display += "=======================================================================";

            return display;
        }

        public string GenerateReceipt()
        {

            if (Basket.Items.Count < 1)
            {
                return "There is nothing in your basket!";
            }


            string receipt = "===========================RECEIPT============================\n";
            decimal tax = 0.00M, total = 0.00M;

            foreach (var item in Basket.Items)
            {
                string newLine = item.Name + " : ";
                decimal subTotal = item.Quantity * item.Price;
                decimal priceAfterTax = 0.00M;
                decimal justTax = 0.00M;

                //Sales Tax
                if (item.TaxExempt)
                {
                    priceAfterTax = subTotal;
                }
                else
                {
                    decimal salesTax = subTotal * .10M; // %10 sales tax
                    //Round up to the nearest 5 cents
                    decimal roundedTax = Math.Round(salesTax * 20) / 20;
                    priceAfterTax = subTotal + roundedTax;
                    justTax = roundedTax;
                }

                //Import Tax
                if (item.Imported)
                {
                    decimal importTax = subTotal * .05M; // %5 Import Tax
                    //Round up to the nearest 5 cents
                    decimal roundedTax = Math.Round(importTax * 20) / 20;
                    priceAfterTax += roundedTax;
                    justTax += roundedTax;
                }

                if (item.Quantity > 1)
                {
                    newLine += priceAfterTax + " (" + item.Quantity + " @ " + item.Price + ")" + "\n";
                }
                else
                {
                    newLine += priceAfterTax + "\n";
                }

                tax += justTax;
                total += priceAfterTax;
                receipt += newLine;

            }

            string totalTax = "Sales Taxes : " + tax + "\n";
            receipt += totalTax;
            string totalAmount = "Total :" + total + "\n";
            receipt += totalAmount;

            Basket.SalesTax = tax;
            Basket.Total = total;

            receipt += "===========================RECEIPT============================\n";
            return receipt;
        }

        public string AddtoBasket(int quantity, string name, decimal price)
        {
            Item newItem = validItem(name);

            if (newItem != null)
            {
                newItem.Price = price;
                newItem.Quantity = quantity;

                Item duplicateItem = null;

                if (Basket.Items.Count > 0)
                {
                    foreach (var item in Basket.Items)
                    {
                        if (item.Name.ToLower().Trim() == name.ToLower().Trim() && item.Price == price)
                        {
                            duplicateItem = item;
                        }
                    }
                }

                if (duplicateItem != null)
                {
                    //Update the quantity instead of adding a new item
                    duplicateItem.Quantity += quantity;
                }
                else
                {
                    Basket.Items.Add(new Item(newItem));
                }

                return "Added " + quantity + " " + name + " @ " + price + " to basket.";
            }
            else
            {
                return "Sorry, we don't carry that product.";
            }
        }

        public ShoppingBasket ClearBasket()
        {
            Basket.Items.Clear();
            Basket.Total = 0.0M;
            Basket.SalesTax = 0.0M;
            return Basket;
        }

        private Item validItem(string Name)
        {
            foreach (var item in AvailableItems)
            {
                if (Name.ToLower().Trim() == item.Name.ToLower().Trim())
                {
                    return item;
                }
            }
            return null;
        }

        // Used to run unit test from txt file
        public void UnitTest()
        {
            Console.WriteLine("");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! Beginning Unit Test !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("");
            try
            {

                string workingDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

                string[] lines = File.ReadAllLines(projectDirectory + @"\UnitTest.txt");

                foreach (var line in lines)
                {

                    if (string.IsNullOrEmpty(line))
                    {
                        Console.WriteLine("TEST LINE : (Enter - Generate Receipt)");
                        Console.WriteLine(GenerateReceipt());
                        ClearBasket();
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("TEST LINE : " + line);
                        int quantity = 0;
                        string name = "";
                        decimal price = 0.0M;
                        string[] splitInput = line.Split(' ');

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
                            Console.WriteLine("Invalid quantity.");
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
                                    Console.WriteLine("Invalid Price.");
                                }

                                break;
                            }
                            else
                            {
                                name += " " + splitInput[i];
                            }
                        }

                        Console.WriteLine(AddtoBasket(quantity, name, price));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unit Test encountered an error : " + e);
                Console.WriteLine("");
                Console.WriteLine("Make sure the file UnitTest.txt exists in the project directory.");
            }
            Console.WriteLine("");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! End Unit Test !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("");
        }

    }
}
