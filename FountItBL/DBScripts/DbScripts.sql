
--scaffold-dbcontext "Server=localhost\sqlexpress;Database=FoundItDB;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models –force

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

CREATE TABLE [dbo].[Communitys]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Manager] INT NULL, 
    [[Name]]] NVARCHAR(50) NULL, 
    [Location] NVARCHAR(250) NULL, 
    CONSTRAINT [FK_CommunitysToUsers] FOREIGN KEY ([Manager]) REFERENCES [Users]([ID])
)
Go

CREATE TABLE [dbo].[CommunityMember] (
    [Id]        INT           NOT NULL,
    [User]      INT NULL,
    [Community] INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CommunityMember_Communitiys] FOREIGN KEY ([Community]) REFERENCES [dbo].[Communities] ([Id]), 
    CONSTRAINT [FK_CommunityMember_ToUsers] FOREIGN KEY ([User]) REFERENCES [Users]([Id])
);
GO
CREATE TABLE [dbo].[PostStatus]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Poststatus] NVARCHAR(15) NULL
)
GO
CREATE TABLE [dbo].[Post]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Theme] NVARCHAR(50) NULL, 
    [Context] NVARCHAR(500) NULL, 
    [FoundItem] BIT NULL, 
    [Picture] NVARCHAR(200) NULL, 
    [Creator] INT NULL, 
    [CreatingDate] DATETIME NULL, 
    [Location] NVARCHAR(250) NULL, 
    [Status] INT NULL, 
    CONSTRAINT [FK_PostToPostStatus] FOREIGN KEY ([Status]) REFERENCES [PostStatus]([Id]), 
    CONSTRAINT [FK_Post_User] FOREIGN KEY ([Creator]) REFERENCES [Users]([Id])
)
GO
CREATE TABLE [dbo].[PostComment]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Post] INT NULL, 
    [Comment] NVARCHAR(MAX) NULL, 
    [Date] DATETIME NULL, 
    [Postcomment] INT NULL, 
    CONSTRAINT [FK_PostComment_Post] FOREIGN KEY ([Post]) REFERENCES [Post]([Id]), 
    CONSTRAINT [FK_PostComment_PostComment] FOREIGN KEY ([Postcomment]) REFERENCES [PostComment]([Id])
)
GO

----Data---
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT INTO [dbo].[Users] ([ID], [Email], [FirstName], [LastName], [Pasword], [UserName]) VALUES (1, N'tsss@hhh.com', N'Tal', N'ggg', N'234', N'fggg')
SET IDENTITY_INSERT [dbo].[Users] OFF