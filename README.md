# DealerOn-SalesTaxes
DealerOn Programming Test Problem #2 - Sales Taxes 
By Nicholas Joseph Carlson 8/5/2020

This is my submission for the DealerOn Development Candidate coding test, problem #2 - Sales Taxes, written in C# using .Net Framework version 4.7.2. 

Assumptions
This submission assumes that the user must choose and input the name of an item from a predetermined (hard-coded) list of products, but may enter any positive quantity and non-zero price they desire. This assumption was made to simplify determining if the item is a book, food, or medical item, and if the item was imported or domestic. 

Design
Each file is organized by region, with variables and functions ordered alphabetically. The Main function in Program.cs is responsible for initializing the Cashier class.

The Cashier.cs file handles all of the Store functionality, including displaying available items, validating user input, adding items to the basket, and generating a receipt. 

The files in the Models folder and namespace are used to organize data refferenced in the Cashier.cs file. The Item.cs file includes details for each item the user can buy. InputResponse.cs is used to return results when attempting to parse user input

Unit Test
I also created a unit test, which can be run by inputting "unit test". This will run all of the test input found in the UnitTest.txt file : 

DealerOn-SalesTaxes\Nick_Carlson_DealerOn_Test\UnitTest.txt  

This unit test parses the UnitTest.txt file line by line, as if it were input from a user.
