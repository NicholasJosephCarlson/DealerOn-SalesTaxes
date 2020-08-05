# DealerOn-SalesTaxes
DealerOn Programming Test Problem #2 - Sales Taxes 
By Nicholas Joseph Carlson 8/5/2020

This is my submission for the DealerOn Development Candidate coding test, problem #2 - Sales Taxes, written in C# using .Net Framework version 4.7.2. 

This submission assumes that the user must choose and input the name of an item from a predetermined (hard-coded) list of products, 
but may enter any positive quantity and price they desire. This assumption was made to simplify determining if the item is a book, food, or
medical item, and if the item was imported or domestic. 

The Main function in Program.cs is responsible for running the main program and validating user input. 
The Cashier.cs file handles all of the Store functionality, including displaying available items, adding items to the basket, and generating a receipt. 
The Models folder includes Item.cs and ShoppingBasket.cs files, which are used to organize and store data transactions in Cashier.cs. 

I also created a unit test, which can be ran by inputting "unit test". 
This will run all of the test input found in the UnitTest.txt file (DealerOn-SalesTaxes\Nick_Carlson_DealerOn_Test\UnitTest.txt).  
