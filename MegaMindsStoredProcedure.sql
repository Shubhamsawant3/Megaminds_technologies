/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2022 (16.0.1000)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2022
    Target Database Engine Edition : Microsoft SQL Server Express Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [MegaMindsDB]
GO
/****** Object:  Table [dbo].[tblCity]    Script Date: 20-10-2024 00:23:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CityName] [varchar](60) NULL,
	[StateId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblState]    Script Date: 20-10-2024 00:23:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblState](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [varchar](60) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUserRegistration]    Script Date: 20-10-2024 00:23:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUserRegistration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](70) NOT NULL,
	[Email] [varchar](350) NOT NULL,
	[Phone] [varchar](14) NOT NULL,
	[Address] [varchar](max) NULL,
	[StateId] [int] NULL,
	[CityId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblCity] ON 

INSERT [dbo].[tblCity] ([Id], [CityName], [StateId]) VALUES (1, N'Mumbai', 1)
INSERT [dbo].[tblCity] ([Id], [CityName], [StateId]) VALUES (2, N'Pune', 1)
INSERT [dbo].[tblCity] ([Id], [CityName], [StateId]) VALUES (3, N'Surat', 2)
INSERT [dbo].[tblCity] ([Id], [CityName], [StateId]) VALUES (4, N'Bardoli', 2)
INSERT [dbo].[tblCity] ([Id], [CityName], [StateId]) VALUES (5, N'Baroda', 2)
SET IDENTITY_INSERT [dbo].[tblCity] OFF
GO
SET IDENTITY_INSERT [dbo].[tblState] ON 

INSERT [dbo].[tblState] ([Id], [StateName]) VALUES (1, N'Maharashtra')
INSERT [dbo].[tblState] ([Id], [StateName]) VALUES (2, N'Gujrat')
SET IDENTITY_INSERT [dbo].[tblState] OFF
GO
SET IDENTITY_INSERT [dbo].[tblUserRegistration] ON 

INSERT [dbo].[tblUserRegistration] ([Id], [Name], [Email], [Phone], [Address], [StateId], [CityId]) VALUES (4, N'Sagar Gore', N'sagargore27@gmail.com', N'9075245789', N'Satara', 1, 2)
INSERT [dbo].[tblUserRegistration] ([Id], [Name], [Email], [Phone], [Address], [StateId], [CityId]) VALUES (6, N'Sarvesh Borate', N'hr@sarvesh.com', N'9075245464', N'Satara', 1, 1)
INSERT [dbo].[tblUserRegistration] ([Id], [Name], [Email], [Phone], [Address], [StateId], [CityId]) VALUES (8, N'Bhushan Bhosale', N'bhushan@gmail.com', N'7845965847', N'pune', 1, 2)
INSERT [dbo].[tblUserRegistration] ([Id], [Name], [Email], [Phone], [Address], [StateId], [CityId]) VALUES (9, N'Aniket Dhok', N'Aniket123@gmail.com', N'9075241865', N'Dahiwadi, Satara', 2, 3)
SET IDENTITY_INSERT [dbo].[tblUserRegistration] OFF
GO
ALTER TABLE [dbo].[tblCity]  WITH CHECK ADD FOREIGN KEY([StateId])
REFERENCES [dbo].[tblState] ([Id])
GO
ALTER TABLE [dbo].[tblUserRegistration]  WITH CHECK ADD FOREIGN KEY([StateId])
REFERENCES [dbo].[tblState] ([Id])
GO
ALTER TABLE [dbo].[tblUserRegistration]  WITH CHECK ADD FOREIGN KEY([CityId])
REFERENCES [dbo].[tblCity] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[UserMindsProcedure]    Script Date: 20-10-2024 00:23:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[UserMindsProcedure](@Name varchar(70)=null, @Email varchar(350)=null,@Phone varchar(14)=null,
@Address varchar(max)=null, @StateName varchar(50)=null, @CityName varchar(50)=null,
@StateId int=null,@CityId int=null,@UserId int=null,@Flag varchar(50)=null)
as begin
if(@Flag = 'ListData')
begin
select Id,[Name],Email,Phone from tblUserRegistration
end
if(@Flag = 'GetStates')
begin
select * from tblState
end
if(@Flag = 'GetCities')
begin
select c.Id,CityName from tblCity c inner join tblState s on s.Id=c.StateId where c.StateId = @StateId
end
if(@Flag = 'InsertData')
begin
insert into tblUserRegistration values(@Name,@Email,@Phone,@Address,@StateId,@CityId)
end
if(@Flag = 'Fetchuserdata')
begin
select u.Id as UserId, Name,Email,Phone,Address,StateName,s.Id as StateId,CityName,c.Id as CityId from tblUserRegistration u
inner join tblState s on s.Id = u.StateId
inner join tblCity c on c.Id = u.CityId
where u.Id = @UserId;
end
if(@Flag = 'Updatedata')
begin
update tblUserRegistration set [Name]=@Name,Email =@Email,Phone=@Phone,[Address] = @Address,
StateId = @StateId, CityId=@CityId where Id = @UserId
end
if(@Flag = 'Deletedata')
begin
delete from tblUserRegistration where Id = @UserId
end
end
GO
