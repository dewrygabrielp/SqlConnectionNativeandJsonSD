USE [Person]
GO
/****** Object:  Table [dbo].[DataPerson]    Script Date: 1/4/2022 1:13:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataPerson](
	[FirstName] [varchar](20) NOT NULL,
	[LastName] [varchar](30) NOT NULL,
	[Age] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DataPerson] ON 

INSERT [dbo].[DataPerson] ([FirstName], [LastName], [Age], [Id]) VALUES (N'Dewry', N'Peña', 24, 1)
INSERT [dbo].[DataPerson] ([FirstName], [LastName], [Age], [Id]) VALUES (N'Removed', N'Removed', 0, 2)
INSERT [dbo].[DataPerson] ([FirstName], [LastName], [Age], [Id]) VALUES (N'Rosa', N'Guadalupe', 22, 3)
INSERT [dbo].[DataPerson] ([FirstName], [LastName], [Age], [Id]) VALUES (N'Jose', N'Capablanca', 31, 4)
INSERT [dbo].[DataPerson] ([FirstName], [LastName], [Age], [Id]) VALUES (N'Pedro', N'Pica Piedras', 48, 5)
INSERT [dbo].[DataPerson] ([FirstName], [LastName], [Age], [Id]) VALUES (N'Juan Pablo', N'Duarte', 47, 6)
INSERT [dbo].[DataPerson] ([FirstName], [LastName], [Age], [Id]) VALUES (N'Ramon', N'Mella', 39, 7)
INSERT [dbo].[DataPerson] ([FirstName], [LastName], [Age], [Id]) VALUES (N'Junior', N'Almanzar', 12, 8)
INSERT [dbo].[DataPerson] ([FirstName], [LastName], [Age], [Id]) VALUES (N'Pablo', N'Neruda', 55, 9)
INSERT [dbo].[DataPerson] ([FirstName], [LastName], [Age], [Id]) VALUES (N'Leslie', N'Sue', 23, 10)
SET IDENTITY_INSERT [dbo].[DataPerson] OFF
GO
