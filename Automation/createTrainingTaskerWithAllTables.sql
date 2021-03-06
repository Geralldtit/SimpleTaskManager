USE [master]
GO
/****** Object:  Database [TrainingTasker]    Script Date: 05/19/2011 12:54:24 ******/
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
/****** Object:  Table [dbo].[Status]    Script Date: 05/19/2011 12:54:26 ******/
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
/****** Object:  Table [dbo].[Projects]    Script Date: 05/19/2011 12:54:26 ******/
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
/****** Object:  Table [dbo].[Persons]    Script Date: 05/19/2011 12:54:26 ******/
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
/****** Object:  Table [dbo].[Tasks]    Script Date: 05/19/2011 12:54:26 ******/
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
/****** Object:  Table [dbo].[TasksPersons]    Script Date: 05/19/2011 12:54:26 ******/
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
/****** Object:  ForeignKey [FK_Tasks_Projects]    Script Date: 05/19/2011 12:54:26 ******/
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Projects] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Projects] ([ProjectID])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Projects]
GO
/****** Object:  ForeignKey [FK_Tasks_Status]    Script Date: 05/19/2011 12:54:26 ******/
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([StatusID])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Status]
GO
/****** Object:  ForeignKey [FK_TasksPersons_Persons]    Script Date: 05/19/2011 12:54:26 ******/
ALTER TABLE [dbo].[TasksPersons]  WITH CHECK ADD  CONSTRAINT [FK_TasksPersons_Persons] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Persons] ([PersonID])
GO
ALTER TABLE [dbo].[TasksPersons] CHECK CONSTRAINT [FK_TasksPersons_Persons]
GO
/****** Object:  ForeignKey [FK_TasksPersons_Tasks]    Script Date: 05/19/2011 12:54:26 ******/
ALTER TABLE [dbo].[TasksPersons]  WITH CHECK ADD  CONSTRAINT [FK_TasksPersons_Tasks] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Tasks] ([TaskID])
GO
ALTER TABLE [dbo].[TasksPersons] CHECK CONSTRAINT [FK_TasksPersons_Tasks]
GO
