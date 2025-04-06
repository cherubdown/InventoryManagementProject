# Project Requirements and Objects
 
The purpose of this project is to fulfil the Coursera course "Foundations of Coding Front-End".

With that fundamental requirement, we shall simply post the design challenge here, since it is the source of requirements. We can think of this as our customer:

"Inventory management system

To complete this challenge, you will need to create a console application where users can manage product stock. Users should be able to add new products, update stock, and remove products.

Some key features include:

-Add new products with name, price, and stock quantity.
-Update stock when products are sold or restocked.
-View all products and their stock levels.
-Remove products from inventory."

Requirements:

One of the more fundamental requirements that are not mentioned in this request is the need for a data storage mechanism. After all, what is the point of an inventory management system if the data is not saved? We will need a storage mechanism that is saved after the program closes. A database will be necessary. Implementation details will be described later.

After we have cached the Items in memory, we can use it to look up items so that we may ensure a few input validation requirements are met:

-The program shall be able to look up an item by name (where case sensitivity is unnecessary) whenever we need to use a function on it. If the item is not found, inform the user.
-It shall check if Stock is greater than or equal to zero.
-It shall check to ensure the Price is greater than zero.
-It shall create a way to display all the products on a table.
-It shall create a way to display a single product's information by name.
-Adding and removing products from the inventory system are different than changing stock levels. Ensure that the user can remove items from the system, just as they can add them.

-The program shall start up with a menu screen with easy-to-follow instructions where the input is a number indicating the function to perform.
-It shall be able to display and read well over 100,000 items in less than 1 second.
-It shall be able to read and display all 100,000+ items in less than 1 second.
-Every other function should executed and return to the menu in less than 1 second.
-Every input validation error shall inform the user the details of why the action failed, then return back to the main menu.

Design outline:

-For this simple project, we shall implement a json serialized data store. The data "inventory.json" file shall be located at the root of the executable.
-We shall create a DataStore class to read and write the inventory data.
-At the end of the loop, the data store will write back to the file after every change to the inventory solution. This will ensure that after every action performed in the main loop, the data was saved.
-We shall create a main loop for overall program flow. Every action from here will branch out the corelating function by an integer input:
1. View all products
2. View a single product
3. Sell a product
4. Add stock to product
5. Add new product to the inventory system
6. Remove product from the inventory system
7. Exit
-We shall create a Display helper that will provide output for simple cases of displaying inventory data, such as the entire database, or a single entry.
-We shall create a user input validation class who's main purpose it to ensure input is exactly as expected. Other classes may use this static class when needed.
-We shall test all options, and every input and at every step.
-We shall test all expected erroneous inputs to ensure they are validated correctly.