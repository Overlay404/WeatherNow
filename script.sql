USE [ProductTable]
GO
/****** Object:  Table [dbo].[Managers]    Script Date: 17.05.2023 12:12:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Managers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Fio] [nvarchar](50) NULL,
	[Salary] [decimal](18, 2) NULL,
	[Age] [int] NULL,
	[Phone] [nvarchar](20) NULL,
 CONSTRAINT [PK_Managers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 17.05.2023 12:12:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Cost] [decimal](18, 2) NULL,
	[Volume] [float] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sells]    Script Date: 17.05.2023 12:12:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sells](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Manag] [int] NULL,
	[ID_Prod] [int] NULL,
	[Count] [int] NULL,
	[Sum] [decimal](18, 2) NULL,
	[Data] [date] NULL,
 CONSTRAINT [PK_Sells] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Managers] ON 

INSERT [dbo].[Managers] ([ID], [Fio], [Salary], [Age], [Phone]) VALUES (1, N'Савин Лукьян Геннадьевич', CAST(30000.00 AS Decimal(18, 2)), 12, N'8(912)837-70-91')
INSERT [dbo].[Managers] ([ID], [Fio], [Salary], [Age], [Phone]) VALUES (2, N'Петухов Эрик Игоревич
', CAST(12000.00 AS Decimal(18, 2)), 11, NULL)
INSERT [dbo].[Managers] ([ID], [Fio], [Salary], [Age], [Phone]) VALUES (3, N'Данилов Рубен Петрович', CAST(23000.00 AS Decimal(18, 2)), 10, N'8(912)171-35-03')
INSERT [dbo].[Managers] ([ID], [Fio], [Salary], [Age], [Phone]) VALUES (4, N'Рыбаков Николай Александрович', CAST(40000.00 AS Decimal(18, 2)), 41, NULL)
SET IDENTITY_INSERT [dbo].[Managers] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ID], [Name], [Cost], [Volume]) VALUES (1, N'Фанера', CAST(1200.00 AS Decimal(18, 2)), 100)
INSERT [dbo].[Products] ([ID], [Name], [Cost], [Volume]) VALUES (2, N'ОСБ', CAST(2000.00 AS Decimal(18, 2)), 200)
INSERT [dbo].[Products] ([ID], [Name], [Cost], [Volume]) VALUES (3, N'Фанера', CAST(3000.00 AS Decimal(18, 2)), 100)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Sells] ON 

INSERT [dbo].[Sells] ([ID], [ID_Manag], [ID_Prod], [Count], [Sum], [Data]) VALUES (1, 1, 1, 12, CAST(200.00 AS Decimal(18, 2)), CAST(N'2021-08-22' AS Date))
INSERT [dbo].[Sells] ([ID], [ID_Manag], [ID_Prod], [Count], [Sum], [Data]) VALUES (2, 1, 2, 23, CAST(122.00 AS Decimal(18, 2)), CAST(N'2021-08-22' AS Date))
INSERT [dbo].[Sells] ([ID], [ID_Manag], [ID_Prod], [Count], [Sum], [Data]) VALUES (3, 2, 1, 32, CAST(3200.00 AS Decimal(18, 2)), CAST(N'2021-08-22' AS Date))
INSERT [dbo].[Sells] ([ID], [ID_Manag], [ID_Prod], [Count], [Sum], [Data]) VALUES (4, 2, 1, 31, CAST(400.00 AS Decimal(18, 2)), CAST(N'2021-06-20' AS Date))
INSERT [dbo].[Sells] ([ID], [ID_Manag], [ID_Prod], [Count], [Sum], [Data]) VALUES (5, 2, 2, 12, CAST(400.00 AS Decimal(18, 2)), CAST(N'2021-08-22' AS Date))
INSERT [dbo].[Sells] ([ID], [ID_Manag], [ID_Prod], [Count], [Sum], [Data]) VALUES (6, 3, 2, 15, CAST(400.00 AS Decimal(18, 2)), CAST(N'2021-07-20' AS Date))
INSERT [dbo].[Sells] ([ID], [ID_Manag], [ID_Prod], [Count], [Sum], [Data]) VALUES (7, 3, 1, 63, CAST(300.00 AS Decimal(18, 2)), CAST(N'2021-07-20' AS Date))
SET IDENTITY_INSERT [dbo].[Sells] OFF
GO
ALTER TABLE [dbo].[Sells]  WITH CHECK ADD  CONSTRAINT [FK_Sells_Managers] FOREIGN KEY([ID_Manag])
REFERENCES [dbo].[Managers] ([ID])
GO
ALTER TABLE [dbo].[Sells] CHECK CONSTRAINT [FK_Sells_Managers]
GO
ALTER TABLE [dbo].[Sells]  WITH CHECK ADD  CONSTRAINT [FK_Sells_Products] FOREIGN KEY([ID_Prod])
REFERENCES [dbo].[Products] ([ID])
GO
ALTER TABLE [dbo].[Sells] CHECK CONSTRAINT [FK_Sells_Products]
GO
