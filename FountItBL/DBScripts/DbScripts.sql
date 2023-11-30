Use master

Create Database FoundItDB

Go

Use FoundItDB

Go

Create Table Users (

ID int Identity primary key,

Email nvarchar(100) not null,

FirstName nvarchar(30) not null,

LastName nvarchar(30) not null,

Pasword nvarchar(30) not null,


UserName nvarchar(30) not null,



CONSTRAINT UC_Email UNIQUE(Email)

)

Go

