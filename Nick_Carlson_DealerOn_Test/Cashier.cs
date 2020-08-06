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
        #region Variables   
        public List<Item> AvailableItems { get; }
        public List<Item> Basket { get; }
        #endregion

        #region Constructor
        public Cashier()
        {
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
            Basket = new List<Item>();
        }
        #endregion

        #region Public Functions
        public string AddtoBasket(int quantity, string name, decimal price)
        {
            Item newItem = ValidItem(name);

            if (newItem != null)
            {
                newItem.Price = price;
                newItem.Quantity = quantity;

                Item duplicateItem = null;

                if (Basket.Count > 0)
                {
                    foreach (var item in Basket)
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
                    Basket.Add(new Item(newItem));
                }

                return "Added " + quantity + " " + name + " @ " + price + " to basket. Input another selection, or hit Enter to complete purchase.";
            }
            else
            {
                return "Sorry, we don't carry that product.";
            }
        }
        public List<Item> ClearBasket()
        {
            Basket.Clear();
            return Basket;
        }
        public void DisplayProducts()
        {
            string display = "\n Welcome to the store where you name the price! \n  Available Products : \n";
            display += "=======================================================================\n";

            foreach (var item in AvailableItems)
            {
                string newline = item.Name + " \n";
                display += newline;
            }
            display += "=======================================================================";
            Console.WriteLine(display);
            Console.WriteLine();
            Console.WriteLine("Please enter your selection in the following format : ");
            Console.WriteLine("[Quantity - integer] [Item Name - string] at [Price - decimal] ");
            Console.WriteLine();
        }
        public string GenerateReceipt()
        {
            if (Basket.Count < 1)
            {
                return "There is nothing in your basket!";
            }

            string receipt = "===========================RECEIPT============================\n";
            decimal tax = 0.00M, total = 0.00M;

            foreach (var item in Basket)
            {
                string newLine = item.Name + " : ";
                decimal salesTax = 0.00M;
                decimal importTax = 0.00M;
                decimal priceAfterTax = 0.00M;
                decimal totalAfterTax = 0.00M;

                //Sales Tax
                if (!item.TaxExempt)
                {
                    salesTax = item.Price * .10M; // %10 sales tax
                    //Round up to the nearest 5 cents
                    salesTax = Math.Ceiling(salesTax / .05M) * .05M;
                }

                //Import Tax
                if (item.Imported)
                {
                    importTax = item.Price * .05M; // %5 Import Tax
                    //Round up to the nearest 5 cents
                    importTax = Math.Ceiling(importTax / .05M) * .05M;
                }

                priceAfterTax = item.Price + salesTax + importTax;
                totalAfterTax = priceAfterTax * item.Quantity;

                if (item.Quantity > 1)
                {
                    newLine += totalAfterTax + " (" + item.Quantity + " @ " + priceAfterTax + ")" + "\n";
                }
                else
                {
                    newLine += totalAfterTax + "\n";
                }

                tax += (salesTax + importTax) * item.Quantity;
                total += totalAfterTax;
                receipt += newLine;

            }

            string salestaxes = "Sales Taxes : " + tax + "\n";
            receipt += salestaxes;
            string totalAmount = "Total :" + total + "\n";
            receipt += totalAmount;

            receipt += "===========================RECEIPT============================\n";
            return receipt;
        }
        public bool ResetOrExit()
        {
            Console.WriteLine("Hit Enter to start a new basket, or type EXIT to close the program.");
            string finalInput = Console.ReadLine();

            if (finalInput.ToUpper().Trim() == "EXIT")
            {
                return false;
            }
            else
            {
                ClearBasket();
                DisplayProducts();
                return true;
            }
        }
        public bool Working()
        {
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))//Generate Receipt
            {
                if (Basket.Count < 1)
                {
                    Console.WriteLine("Please enter a quantity, item name, and price.");
                    return true;
                }

                Console.WriteLine(GenerateReceipt());
                return ResetOrExit();
            }
            else if (input.Trim().ToLower() == "unit test")
            {
                UnitTest();
                return ResetOrExit();
            }
            else // attempt to parse input and add to basket.
            {
                InputResponse response = ParseInput(input.Trim());
                Console.WriteLine(response.Message);
                return true;
            }
        }
        #endregion

        #region Private Functions
        private InputResponse ParseInput(string input)
        {
            InputResponse response = new InputResponse
            {
                Success = false,
                Message = "Not Started",
                Quantity = 1,
                Price = 0.00M,
                Name = ""
            };

            string[] splitInput = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Parse Quantity
            try
            {
                response.Quantity = int.Parse(splitInput[0]);

                if (response.Quantity <= 0)
                {
                    throw new Exception("Invalid quantity.");
                }
            }
            catch (Exception)
            {
                response.Message = "Please enter a valid quantity.";
                return response;
            }

            for (int i = 1; i < splitInput.Length; i++)
            {
                if (splitInput[i] == "at")
                {
                    try
                    {
                        //Parse Price
                        response.Price = decimal.Parse(splitInput[i + 1]);

                        if (response.Price <= 0)
                        {
                            throw new Exception("Invalid Price.");
                        }
                        i = splitInput.Length; // stop parsing
                    }
                    catch (Exception)
                    {
                        response.Message = "Please enter a valid Price.";
                        return response;
                    }
                }
                else
                {
                    //Parse Name
                    response.Name += " " + splitInput[i];
                }
            }
            response.Success = true;
            response.Message = AddtoBasket(response.Quantity, response.Name, response.Price);
            return response;
        }
        private Item ValidItem(string Name)
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
        #endregion

        #region Unit Test
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
                        InputResponse response = ParseInput(line.Trim());
                        Console.WriteLine(response.Message);
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
        #endregion
    }
}
