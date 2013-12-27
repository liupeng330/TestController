USE [master]
GO
/****** Object:  Database [TestJobDB]    Script Date: 12/27/2013 21:18:26 ******/
CREATE DATABASE [TestJobDB] ON  PRIMARY 
( NAME = N'TestJobDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\TestJobDB.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TestJobDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\TestJobDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TestJobDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TestJobDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TestJobDB] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [TestJobDB] SET ANSI_NULLS OFF
GO
ALTER DATABASE [TestJobDB] SET ANSI_PADDING OFF
GO
ALTER DATABASE [TestJobDB] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [TestJobDB] SET ARITHABORT OFF
GO
ALTER DATABASE [TestJobDB] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [TestJobDB] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [TestJobDB] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [TestJobDB] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [TestJobDB] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [TestJobDB] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [TestJobDB] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [TestJobDB] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [TestJobDB] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [TestJobDB] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [TestJobDB] SET  DISABLE_BROKER
GO
ALTER DATABASE [TestJobDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [TestJobDB] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [TestJobDB] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [TestJobDB] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [TestJobDB] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [TestJobDB] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [TestJobDB] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [TestJobDB] SET  READ_WRITE
GO
ALTER DATABASE [TestJobDB] SET RECOVERY FULL
GO
ALTER DATABASE [TestJobDB] SET  MULTI_USER
GO
ALTER DATABASE [TestJobDB] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [TestJobDB] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'TestJobDB', N'ON'
GO
USE [TestJobDB]
GO
/****** Object:  Table [dbo].[JobAssigment]    Script Date: 12/27/2013 21:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobAssigment](
	[AssignmentID] [int] IDENTITY(1,1) NOT NULL,
	[Status] [int] NOT NULL,
	[Result] [int] NULL,
	[StartTime] [datetime2](7) NULL,
	[EndTime] [datetime2](7) NULL,
	[Owner] [nvarchar](50) NULL,
 CONSTRAINT [PK_JobAssigment] PRIMARY KEY CLUSTERED 
(
	[AssignmentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 12/27/2013 21:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[TaskID] [int] IDENTITY(1,1) NOT NULL,
	[TaskName] [nvarchar](50) NOT NULL,
	[TaskExecuteFilePath] [nvarchar](max) NOT NULL,
	[TaskArgs] [nvarchar](50) NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[TaskID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskGroup]    Script Date: 12/27/2013 21:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskGroup](
	[TaskGroupID] [int] IDENTITY(1,1) NOT NULL,
	[TaskGroupName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TaskGroup] PRIMARY KEY CLUSTERED 
(
	[TaskGroupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task_TaskGroup]    Script Date: 12/27/2013 21:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task_TaskGroup](
	[TaskID] [int] NOT NULL,
	[TaskGroupID] [int] NOT NULL,
	[TaskOrder] [int] NOT NULL,
 CONSTRAINT [PK_Task_TaskGroup] PRIMARY KEY CLUSTERED 
(
	[TaskID] ASC,
	[TaskGroupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobGroup]    Script Date: 12/27/2013 21:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobGroup](
	[JobGroupID] [int] IDENTITY(1,1) NOT NULL,
	[StartTime] [datetime2](7) NULL,
	[EndTime] [datetime2](7) NULL,
	[Status] [int] NOT NULL,
	[TaskGroupID] [int] NOT NULL,
 CONSTRAINT [PK_JobGroup] PRIMARY KEY CLUSTERED 
(
	[JobGroupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Job]    Script Date: 12/27/2013 21:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Job](
	[JobID] [int] IDENTITY(1,1) NOT NULL,
	[StartTime] [datetime2](7) NULL,
	[EndTime] [datetime2](7) NULL,
	[Status] [int] NOT NULL,
	[ResultInfo] [nvarchar](max) NULL,
	[TaskID] [int] NOT NULL,
	[JobGroupID] [int] NOT NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[JobID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client_Job_Assignment]    Script Date: 12/27/2013 21:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client_Job_Assignment](
	[AssignmentID] [int] NOT NULL,
	[ClientID] [nvarchar](50) NOT NULL,
	[JobGroupID] [int] NOT NULL,
 CONSTRAINT [PK_Client_Job_Assignment] PRIMARY KEY CLUSTERED 
(
	[AssignmentID] ASC,
	[ClientID] ASC,
	[JobGroupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Task_TaskGroup_Task]    Script Date: 12/27/2013 21:18:26 ******/
ALTER TABLE [dbo].[Task_TaskGroup]  WITH CHECK ADD  CONSTRAINT [FK_Task_TaskGroup_Task] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Task] ([TaskID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Task_TaskGroup] CHECK CONSTRAINT [FK_Task_TaskGroup_Task]
GO
/****** Object:  ForeignKey [FK_Task_TaskGroup_TaskGroup]    Script Date: 12/27/2013 21:18:26 ******/
ALTER TABLE [dbo].[Task_TaskGroup]  WITH CHECK ADD  CONSTRAINT [FK_Task_TaskGroup_TaskGroup] FOREIGN KEY([TaskGroupID])
REFERENCES [dbo].[TaskGroup] ([TaskGroupID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Task_TaskGroup] CHECK CONSTRAINT [FK_Task_TaskGroup_TaskGroup]
GO
/****** Object:  ForeignKey [FK_JobGroup_TaskGroup]    Script Date: 12/27/2013 21:18:26 ******/
ALTER TABLE [dbo].[JobGroup]  WITH CHECK ADD  CONSTRAINT [FK_JobGroup_TaskGroup] FOREIGN KEY([TaskGroupID])
REFERENCES [dbo].[TaskGroup] ([TaskGroupID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[JobGroup] CHECK CONSTRAINT [FK_JobGroup_TaskGroup]
GO
/****** Object:  ForeignKey [FK_Job_JobGroup]    Script Date: 12/27/2013 21:18:26 ******/
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_Job_JobGroup] FOREIGN KEY([JobGroupID])
REFERENCES [dbo].[JobGroup] ([JobGroupID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_Job_JobGroup]
GO
/****** Object:  ForeignKey [FK_Job_Task]    Script Date: 12/27/2013 21:18:26 ******/
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_Job_Task] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Task] ([TaskID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_Job_Task]
GO
/****** Object:  ForeignKey [FK_Client_Job_Assignment_JobAssigment]    Script Date: 12/27/2013 21:18:26 ******/
ALTER TABLE [dbo].[Client_Job_Assignment]  WITH CHECK ADD  CONSTRAINT [FK_Client_Job_Assignment_JobAssigment] FOREIGN KEY([AssignmentID])
REFERENCES [dbo].[JobAssigment] ([AssignmentID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Client_Job_Assignment] CHECK CONSTRAINT [FK_Client_Job_Assignment_JobAssigment]
GO
/****** Object:  ForeignKey [FK_Client_Job_Assignment_JobGroup]    Script Date: 12/27/2013 21:18:26 ******/
ALTER TABLE [dbo].[Client_Job_Assignment]  WITH CHECK ADD  CONSTRAINT [FK_Client_Job_Assignment_JobGroup] FOREIGN KEY([JobGroupID])
REFERENCES [dbo].[JobGroup] ([JobGroupID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Client_Job_Assignment] CHECK CONSTRAINT [FK_Client_Job_Assignment_JobGroup]
GO
