# MyKitchen Demo Project 
Asp.Net app for the course C# ASP.NET Core February 2022 @SoftUni
# Project Specification
This is an application useful for users who are looking for ideas for a kitchen and who can make it for them. In it you can look at kitchens by categories and manufacturers, as there is information about the price of the kitchen, colors, the term of manufacture, the material from which it is made and others. Registered users can rate people's kitchens from 1 to 5 and write comments and reply to comments and have a collection of favorite kitchens.
The application has an admin area where you can add roles and categories, as well as edit, delete and view users, categories, manufacturers and kitchens.

# Roles
- Visitor
- User
- Administrator
- Manufacturer

# Used Frameworks
- ASP.Net 6
- NUnit
- Moq
- Newtonsoft.Json
- TynyMCE
- Entity Framework Core
- AutoMapper

# Used techniques
- MVC
- Services
- Web Api controllers + AJAX
- In-Memmory Cache

# Functionality
1. User
  - Login 
```
Login in current application using email and password of already registered user. 
```
  - Register
```
Register a new user by providing email, password and username. 
```
  - Logout 
```
Logouts from the application. 
```
2. Home
  - Index 
```
Lists three random kitchen in carousel.
Lists all category kitchen with information about them.
```
3. Category
  - By name 
```
Lists all kitchens in this category with information about them and the ability to view details.
There is a button to add to a collection.
```
4. Kitchen
  - Add
```
Only for registered users.
Create a new kitchen entry and save it to the database.
```
- All
```
Lists all kitchens with information about them and the ability to view details.
There is a button to add to a collection.
```
- My
```
Only for registered users.
Lists all user kitchens with information about them and the ability to view details.
```
- Details
```
Show kitchen details.
Registered users can post comments.
Registered users can rate the kitchen from 1 to 5.
The user added the kitchen can edit and delete the kitchen.
```
- Еdit
```
The user added the kitchen and admin can edit the kitchen.
The manufacturer can edit the published kitchen only if there is incorrect data.
```
- AddToCollection
```
Only for registered users.
Every registered user can add their favorite kitchens to a collection.
```
- Collection
```
Only for registered users.
Lists all kitchens in user collection with information about them and the ability to view details.
There is a button to remove from collection.
```
4. Manufacturer
  - Add
```
Only for registered users.
Create a new manufacturer entry and save it to the database.
```
- All
```
Lists all manufacturers with information about them and the ability to view details.
Тhere is a button to see all the kitchens from this manufacturer
```
- Еdit
```
The manufacturer, the user added the manufacturer and admin can edit the manufacturer.
```
- Delete
```
The manufacturer, the user added the manufacturer and admin can delete the manufacturer.
```
- All Kitchens
```
Llsts all kitchens from the manufacturer with information about them and the ability to view details.
Тhere is a button to see all the kitchens from this manufacturer
```
# Admin Area
- The admin can edit view and delete users.
- The admin can edit view and delete manufacturers.
- The admin can edit view and delete kitchens.
- The admin can add edit views and delete categories
-  The admin can add edit and delete roles

# Manufacturer Area
- The manufacturer can edit view and delete kitchens noted that they are manufactured by him.

# Template Layouts
- Default ASP.Net Core site templete 
- AdminLTE- https://adminlte.io/ (Admin and Manufacturer area)
