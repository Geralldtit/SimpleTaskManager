
USE [master]
GO
USE [master]
GO
/****** Object:  Database [TrainingTasker]    Script Date: 05/19/2011 13:46:52 ******/
CREATE DATABASE [TrainingTasker] ON  PRIMARY 
( NAME = N'TrainingTasker', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\TrainingTasker.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TrainingTasker_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\TrainingTasker_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TrainingTasker] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TrainingTasker].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TrainingTasker] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [TrainingTasker] SET ANSI_NULLS OFF
GO
ALTER DATABASE [TrainingTasker] SET ANSI_PADDING OFF
GO
ALTER DATABASE [TrainingTasker] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [TrainingTasker] SET ARITHABORT OFF
GO
ALTER DATABASE [TrainingTasker] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [TrainingTasker] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [TrainingTasker] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [TrainingTasker] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [TrainingTasker] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [TrainingTasker] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [TrainingTasker] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [TrainingTasker] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [TrainingTasker] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [TrainingTasker] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [TrainingTasker] SET  DISABLE_BROKER
GO
ALTER DATABASE [TrainingTasker] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [TrainingTasker] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [TrainingTasker] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [TrainingTasker] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [TrainingTasker] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [TrainingTasker] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [TrainingTasker] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [TrainingTasker] SET  READ_WRITE
GO
ALTER DATABASE [TrainingTasker] SET RECOVERY FULL
GO
ALTER DATABASE [TrainingTasker] SET  MULTI_USER
GO
ALTER DATABASE [TrainingTasker] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [TrainingTasker] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'TrainingTasker', N'ON'
GO
USE [TrainingTasker]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 05/19/2011 13:46:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[StatusID] [int] IDENTITY(1,1) NOT NULL,
	[StatusTitle] [nchar](10) NOT NULL
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Status] ON [dbo].[Status] 
(
	[StatusID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Status] ON
INSERT [dbo].[Status] ([StatusID], [StatusTitle]) VALUES (1, N'Не начата ')
INSERT [dbo].[Status] ([StatusID], [StatusTitle]) VALUES (2, N'В процессе')
INSERT [dbo].[Status] ([StatusID], [StatusTitle]) VALUES (3, N'Завершена ')
INSERT [dbo].[Status] ([StatusID], [StatusTitle]) VALUES (4, N'Отложена  ')
SET IDENTITY_INSERT [dbo].[Status] OFF
/****** Object:  Table [dbo].[Projects]    Script Date: 05/19/2011 13:46:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[ProjectID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [nchar](100) NOT NULL,
	[PrShortName] [nchar](15) NOT NULL,
	[PrDescription] [nvarchar](max) NULL,
 CONSTRAINT [PK_Projects_1] PRIMARY KEY CLUSTERED 
(
	[ProjectID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Projects] ON
INSERT [dbo].[Projects] ([ProjectID], [ProjectName], [PrShortName], [PrDescription]) VALUES (3, N'ICQ For Asian Users                                                                                 ', N'ICQ   2        ', N'icq skdfjl aslkf')
INSERT [dbo].[Projects] ([ProjectID], [ProjectName], [PrShortName], [PrDescription]) VALUES (8, N'Soc Club For Children                                                                               ', N'SocClub        ', N'asdkfjlksdf')
INSERT [dbo].[Projects] ([ProjectID], [ProjectName], [PrShortName], [PrDescription]) VALUES (9, N'Big Bad Project                                                                                     ', N'BBP            ', N'laksjdflkds')
INSERT [dbo].[Projects] ([ProjectID], [ProjectName], [PrShortName], [PrDescription]) VALUES (20, N'Oreol project                                                                                       ', N'Oreol          ', N'ashflka laekjrlkje')
INSERT [dbo].[Projects] ([ProjectID], [ProjectName], [PrShortName], [PrDescription]) VALUES (21, N'newProject                                                                                          ', N'newP           ', N'wow!')
INSERT [dbo].[Projects] ([ProjectID], [ProjectName], [PrShortName], [PrDescription]) VALUES (22, N'Initila Test Task                                                                                   ', N'ITT            ', N'create and edit tasks for projects')
SET IDENTITY_INSERT [dbo].[Projects] OFF
/****** Object:  Table [dbo].[Persons]    Script Date: 05/19/2011 13:46:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[Soname] [nchar](100) NOT NULL,
	[Name] [nchar](25) NULL,
	[SecondName] [nchar](35) NULL,
	[Position] [nchar](50) NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Persons] ON
INSERT [dbo].[Persons] ([PersonID], [Soname], [Name], [SecondName], [Position]) VALUES (10, N'Двирков                                                                                             ', N'Анатолий                 ', N'Викторович                         ', N'Junior                                            ')
INSERT [dbo].[Persons] ([PersonID], [Soname], [Name], [SecondName], [Position]) VALUES (18, N'Вторников                                                                                           ', N'Вторник                  ', N'Вторникович                        ', N'Вторящий                                          ')
INSERT [dbo].[Persons] ([PersonID], [Soname], [Name], [SecondName], [Position]) VALUES (19, N'Григорий                                                                                            ', N'Иващенко                 ', N'Иванович                           ', N'Пилот                                             ')
INSERT [dbo].[Persons] ([PersonID], [Soname], [Name], [SecondName], [Position]) VALUES (24, N'Глагольев                                                                                           ', N'Петр                     ', N'Николаевич                         ', N'Охранник                                          ')
SET IDENTITY_INSERT [dbo].[Persons] OFF
/****** Object:  Table [dbo].[Tasks]    Script Date: 05/19/2011 13:46:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[TaskID] [int] IDENTITY(1,1) NOT NULL,
	[TaskName] [nchar](100) NOT NULL,
	[BeginTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[StatusID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[Hours] [int] NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[TaskID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Tasks] ON
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [BeginTime], [EndTime], [StatusID], [ProjectID], [Hours]) VALUES (5, N'Task1                                                                                               ', CAST(0x00009E5E00000000 AS DateTime), CAST(0x00009F3200000000 AS DateTime), 2, 3, NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [BeginTime], [EndTime], [StatusID], [ProjectID], [Hours]) VALUES (9, N'Task3                                                                                               ', CAST(0x00009E6200000000 AS DateTime), CAST(0x00009E6200000000 AS DateTime), 2, 3, 0)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [BeginTime], [EndTime], [StatusID], [ProjectID], [Hours]) VALUES (10, N'Task1Pr8                                                                                            ', CAST(0x00009E5E00000000 AS DateTime), CAST(0x00009F3200000000 AS DateTime), 3, 8, 0)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [BeginTime], [EndTime], [StatusID], [ProjectID], [Hours]) VALUES (11, N'TasksPr9                                                                                            ', CAST(0x00009CF800000000 AS DateTime), CAST(0x0000A04C00000000 AS DateTime), 2, 9, 0)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [BeginTime], [EndTime], [StatusID], [ProjectID], [Hours]) VALUES (12, N'Task2Pr8                                                                                            ', CAST(0x00009AB200000000 AS DateTime), CAST(0x00009C2000000000 AS DateTime), 4, 8, NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [BeginTime], [EndTime], [StatusID], [ProjectID], [Hours]) VALUES (27, N'CCCCCc                                                                                              ', CAST(0x000095D000000000 AS DateTime), CAST(0x0000973D00000000 AS DateTime), 1, 8, 1)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [BeginTime], [EndTime], [StatusID], [ProjectID], [Hours]) VALUES (28, N'DDDD                                                                                                ', CAST(0x000095EE00000000 AS DateTime), CAST(0x00009A8F00000000 AS DateTime), 2, 9, 1)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [BeginTime], [EndTime], [StatusID], [ProjectID], [Hours]) VALUES (29, N'EEEEE                                                                                               ', CAST(0x0000901A00000000 AS DateTime), CAST(0x000095CF00000000 AS DateTime), 2, 3, 2)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [BeginTime], [EndTime], [StatusID], [ProjectID], [Hours]) VALUES (30, N'FFFFFFF                                                                                             ', CAST(0x00009E5E00000000 AS DateTime), CAST(0x0000A12500000000 AS DateTime), 2, 3, 2)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [BeginTime], [EndTime], [StatusID], [ProjectID], [Hours]) VALUES (33, N'цфуацу                                                                                              ', CAST(0x000095D000000000 AS DateTime), CAST(0x000097D300000000 AS DateTime), 2, 3, 2)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [BeginTime], [EndTime], [StatusID], [ProjectID], [Hours]) VALUES (34, N'www                                                                                                 ', CAST(0x0000911400000000 AS DateTime), CAST(0x00009B1200000000 AS DateTime), 1, 3, 1)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [BeginTime], [EndTime], [StatusID], [ProjectID], [Hours]) VALUES (37, N'15                                                                                                  ', CAST(0x0000901A00000000 AS DateTime), CAST(0x00009BA400000000 AS DateTime), 2, 20, 2)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [BeginTime], [EndTime], [StatusID], [ProjectID], [Hours]) VALUES (38, N'wer                                                                                                 ', CAST(0x00009A9400000000 AS DateTime), CAST(0x00009C0100000000 AS DateTime), 2, 20, 2)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [BeginTime], [EndTime], [StatusID], [ProjectID], [Hours]) VALUES (43, N'qqeeaass                                                                                            ', CAST(0x00009EE200000000 AS DateTime), CAST(0x00009EE700000000 AS DateTime), 1, 3, 8)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [BeginTime], [EndTime], [StatusID], [ProjectID], [Hours]) VALUES (44, N'test                                                                                                ', CAST(0x00008E9900000000 AS DateTime), CAST(0x00008D9700000000 AS DateTime), 3, 3, 999)
SET IDENTITY_INSERT [dbo].[Tasks] OFF
/****** Object:  Table [dbo].[TasksPersons]    Script Date: 05/19/2011 13:46:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TasksPersons](
	[PersonID] [int] NOT NULL,
	[TaskID] [int] NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_TasksPersons] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[TasksPersons] ON
INSERT [dbo].[TasksPersons] ([PersonID], [TaskID], [id]) VALUES (10, 30, 24)
INSERT [dbo].[TasksPersons] ([PersonID], [TaskID], [id]) VALUES (24, 10, 48)
INSERT [dbo].[TasksPersons] ([PersonID], [TaskID], [id]) VALUES (19, 11, 50)
INSERT [dbo].[TasksPersons] ([PersonID], [TaskID], [id]) VALUES (10, 9, 52)
INSERT [dbo].[TasksPersons] ([PersonID], [TaskID], [id]) VALUES (18, 28, 53)
INSERT [dbo].[TasksPersons] ([PersonID], [TaskID], [id]) VALUES (10, 33, 58)
INSERT [dbo].[TasksPersons] ([PersonID], [TaskID], [id]) VALUES (10, 34, 61)
INSERT [dbo].[TasksPersons] ([PersonID], [TaskID], [id]) VALUES (18, 37, 68)
INSERT [dbo].[TasksPersons] ([PersonID], [TaskID], [id]) VALUES (10, 43, 72)
SET IDENTITY_INSERT [dbo].[TasksPersons] OFF
/****** Object:  ForeignKey [FK_Tasks_Projects]    Script Date: 05/19/2011 13:46:53 ******/
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Projects] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Projects] ([ProjectID])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Projects]
GO
/****** Object:  ForeignKey [FK_Tasks_Status]    Script Date: 05/19/2011 13:46:53 ******/
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([StatusID])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Status]
GO
/****** Object:  ForeignKey [FK_TasksPersons_Persons]    Script Date: 05/19/2011 13:46:53 ******/
ALTER TABLE [dbo].[TasksPersons]  WITH CHECK ADD  CONSTRAINT [FK_TasksPersons_Persons] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Persons] ([PersonID])
GO
ALTER TABLE [dbo].[TasksPersons] CHECK CONSTRAINT [FK_TasksPersons_Persons]
GO
/****** Object:  ForeignKey [FK_TasksPersons_Tasks]    Script Date: 05/19/2011 13:46:53 ******/
ALTER TABLE [dbo].[TasksPersons]  WITH CHECK ADD  CONSTRAINT [FK_TasksPersons_Tasks] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Tasks] ([TaskID])
GO
ALTER TABLE [dbo].[TasksPersons] CHECK CONSTRAINT [FK_TasksPersons_Tasks]
GO
