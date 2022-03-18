# SQL .Net Case Project

Hi there! :wave: Recently I joined a technical interview of a company and below tasks asked from me.

``` 
We ask you to develop an .NET project which has following functionalities:

Get sales history
 
Add new sales history record
 
Delete sales history record
 
Update sales history record
 
Get profit for given store
 
Get the most profitable store
 
Get the best seller product by sales quantity

You can use table schemas specified in csv files. Please specify any changes you make - if any - in the database design.
 
You should not use any ORM tool. All database operations should be done by queries.
 
 ```
Three .csv files provided for me (Available inside project folder). You can create a local database and tables then populate these tables with given .csv files.
You can use this website to populate your tables from .csv [Website](https://blog.sqlauthority.com/2008/02/06/sql-server-import-csv-file-into-sql-server-using-bulk-insert-load-comma-delimited-file-into-sql-server/).

My final hierarchy for database and tables is like this:

 **Database.mdf**
   - **inventory-sales table**
   - **products table**
   - **stores table**

## Disclaimer!
My solution does not claim to be the best, correct, most optimized or time performant ever. Its only to give an idea to those who want to create a solution for similar tasks. Feel free to use, modify this code as you wish.


