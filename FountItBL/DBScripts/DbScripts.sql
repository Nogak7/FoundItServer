
--scaffold-dbcontext "Server=localhost\sqlexpress;Database=FoundItDB;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models –force
--scaffold-dbcontext "Server=(localdb)\MSSQLLocalDB;Database=FoundItDB;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models –force

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

ProfilePicture  NVARCHAR (200) NULL,


CONSTRAINT UC_Email UNIQUE(Email)

)

Go

CREATE TABLE [dbo].[Communities]
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
CREATE TABLE [dbo].[PostStatus] (
    [Id]         INT           NOT NULL,
    [Poststatus] NVARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[Post] (
    [Id]           INT   IDENTITY(1,1)         NOT NULL,
    [Theme]        NVARCHAR (50)  NULL,
    [Context]      NVARCHAR (500) NULL,
    [FoundItem]    BIT            NULL,
    [Picture]      NVARCHAR (200) NULL,
    [Creator]      INT            NULL,
    [CreatingDate] DATETIME       NULL,
    [Location]     NVARCHAR (250) NULL,
    [Status]       INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PostToPostStatus] FOREIGN KEY ([Status]) REFERENCES [dbo].[PostStatus] ([Id]),
    CONSTRAINT [FK_Post_User] FOREIGN KEY ([Creator]) REFERENCES [dbo].[Users] ([ID])
);
GO
CREATE TABLE [dbo].[PostComment]
(
    [Id]     INT   IDENTITY(1,1)         NOT NULL,    [Post] INT NULL, 
    [Comment] NVARCHAR(MAX) NULL, 
    [Date] DATETIME NULL, 
    [Postcomment] INT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PostComment_Post] FOREIGN KEY ([Post]) REFERENCES [Post]([Id]), 
    CONSTRAINT [FK_PostComment_PostComment] FOREIGN KEY ([Postcomment]) REFERENCES [PostComment]([Id])
)
GO
INSERT INTO [dbo].[PostStatus] ([Id], [Poststatus]) VALUES (1, N'Waiting For approval')
INSERT INTO [dbo].[PostStatus] ([Id], [Poststatus]) VALUES (2, N'Not Found Yet')
INSERT INTO [dbo].[PostStatus] ([Id], [Poststatus]) VALUES (3, N'Verification')
INSERT INTO [dbo].[PostStatus] ([Id], [Poststatus]) VALUES (4, N'Found')

----Data---
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT INTO [dbo].[Users] ([ID], [Email], [FirstName], [LastName], [Pasword], [UserName], [ProfilePicture]) VALUES (1, N'tsss@hhh.com', N'Tal', N'ggg', N'234', N'fggg', NULL)
INSERT INTO [dbo].[Users] ([ID], [Email], [FirstName], [LastName], [Pasword], [UserName], [ProfilePicture]) VALUES (2, N'nk@hhh.com', N'noga', N'kimhi', N'3737', N'nono', N'user2_profilepic.jpg')
SET IDENTITY_INSERT [dbo].[Users] OFF


SET IDENTITY_INSERT [dbo].[Post] ON
INSERT INTO [dbo].[Post] ([Id], [Theme], [Context], [FoundItem], [Picture], [Creator], [CreatingDate], [Location], [Status]) VALUES (2, N'sun glasses', N'I found those glases today in magdiel park', 1, N'11_postImage.jpg', 2, N'2024-02-22 13:21:39', N'Hod hasharon', 4)
INSERT INTO [dbo].[Post] ([Id], [Theme], [Context], [FoundItem], [Picture], [Creator], [CreatingDate], [Location], [Status]) VALUES (3, N'keys', N'During my run i found those keys at Park Hayarkon, contact me if those are yours', 0, N'14_postImage.jpg', 2, N'2023-09-13 12:24:09', N'tel aviv', 2)
INSERT INTO [dbo].[Post] ([Id], [Theme], [Context], [FoundItem], [Picture], [Creator], [CreatingDate], [Location], [Status]) VALUES (4, N'bottle', N'i found this at school in class yud bet 2', 1, N'15_postImage.jpg', 2, N'2024-11-24 09:12:24', N'Herzalia', 2)
SET IDENTITY_INSERT [dbo].[Post] OFF


SET IDENTITY_INSERT [dbo].[PostComment] ON
INSERT INTO [dbo].[PostComment] ([Id], [Post], [Comment], [Date], [Postcomment]) VALUES (1, 2, N'hi ! my name is shira and those are mine, it would be grate if you could return those to me to night, us ut possibale?', N'2024-06-06 08:56:09', NULL)
INSERT INTO [dbo].[PostComment] ([Id], [Post], [Comment], [Date], [Postcomment]) VALUES (2, 2, N'hi shira im noa ,yes im avalible tonight', N'2024-06-06 09:01:11', NULL)
INSERT INTO [dbo].[PostComment] ([Id], [Post], [Comment], [Date], [Postcomment]) VALUES (3, 2, N'greate! please contact text me so we can arrange the meating, my number is - 0523795743', N'2024-06-06 09:02:36', NULL)
INSERT INTO [dbo].[PostComment] ([Id], [Post], [Comment], [Date], [Postcomment]) VALUES (5, 3, N'hi my name is yoav, i think my sister found your keys, ill tell her to check this', N'2024-06-06 09:52:07', NULL)
INSERT INTO [dbo].[PostComment] ([Id], [Post], [Comment], [Date], [Postcomment]) VALUES (6, 3, N'thank you!', N'2024-06-06 09:52:20', NULL)
INSERT INTO [dbo].[PostComment] ([Id], [Post], [Comment], [Date], [Postcomment]) VALUES (7, 3, N'hi im sapir, yoav''s sister, i have your keys text me so i can return them to you - 0546783748', N'2024-06-06 09:53:29', NULL)
INSERT INTO [dbo].[PostComment] ([Id], [Post], [Comment], [Date], [Postcomment]) VALUES (8, 3, N'great! thank you sapir', N'2024-06-06 09:53:44', NULL)
INSERT INTO [dbo].[PostComment] ([Id], [Post], [Comment], [Date], [Postcomment]) VALUES (9, 4, N'hi, this belongs to my friend daniela, please contact her - 0526758956', N'2024-06-06 09:54:45', NULL)
INSERT INTO [dbo].[PostComment] ([Id], [Post], [Comment], [Date], [Postcomment]) VALUES (10, 4, N'great ill text her ', N'2024-06-06 09:55:17', NULL)
SET IDENTITY_INSERT [dbo].[PostComment] OFF


