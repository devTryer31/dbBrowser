# dbBrowser
WPF app representation of a specific database and statistics about it.
In this project I using Entity Framework Core with Model-first way.

For starting the app using database:
1. Create a DataBase with name "dbBrowserDataBase-GV" on your MS SQLserver
   For example you can make this steps:  
       1) Go to View-> SQL Server Object Explorer  
       2) ![image](https://user-images.githubusercontent.com/70976803/203626137-2cb3be19-07ce-46c6-a533-d9fd3d21b46b.png)  
       3) Type ```create database "dbBrowserDataBase-GV"``` and run it.  
       
 2. Run SQL script in file [UniversityDataBase.edmx.sql](https://github.com/devTryer31/dbBrowser/blob/e54d62d935326bec104eba1de74813f78450bd0f/dbBrowser/Data/Model/UniversityDataBase.edmx.sql) with selection dbBrowserDataBase-GV database.
