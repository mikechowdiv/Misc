/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2008 (10.0.1600)
    Source Database Engine Edition : Microsoft SQL Server Standard Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [master]
GO
/****** Object:  Database [LBTest1]    Script Date: 26/09/2017 4:20:50 PM ******/
CREATE DATABASE [LBTest1] ON  PRIMARY 
( NAME = N'LBTest1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\LBTest1.mdf' , SIZE = 240640KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'LBTest1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\LBTest1_log.ldf' , SIZE = 630976KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [LBTest1] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LBTest1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LBTest1] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LBTest1] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LBTest1] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LBTest1] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LBTest1] SET ARITHABORT OFF 
GO
ALTER DATABASE [LBTest1] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LBTest1] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LBTest1] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LBTest1] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LBTest1] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LBTest1] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LBTest1] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LBTest1] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LBTest1] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LBTest1] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LBTest1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LBTest1] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LBTest1] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LBTest1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LBTest1] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LBTest1] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LBTest1] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LBTest1] SET RECOVERY FULL 
GO
ALTER DATABASE [LBTest1] SET  MULTI_USER 
GO
ALTER DATABASE [LBTest1] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LBTest1] SET DB_CHAINING OFF 
GO
USE [LBTest1]
GO
/****** Object:  User [LMGMARKETING\Administrator]    Script Date: 26/09/2017 4:20:51 PM ******/
CREATE USER [LMGMARKETING\Administrator] FOR LOGIN [LMGMARKETING\Administrator] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Batch]    Script Date: 26/09/2017 4:20:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Batch](
	[BatchNumber] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[VenueCount] [int] NULL,
 CONSTRAINT [PK_Batch_1] PRIMARY KEY CLUSTERED 
(
	[BatchNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BatchSummary]    Script Date: 26/09/2017 4:20:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchSummary](
	[BatchNumber] [int] NOT NULL,
	[ProcessedDate] [datetime] NOT NULL,
	[FileName] [nvarchar](max) NULL,
	[SalesDate] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Execptions]    Script Date: 26/09/2017 4:20:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Execptions](
	[BatchNumber] [int] NOT NULL,
	[ProcessedDate] [datetime] NOT NULL,
	[ErrorCode] [nvarchar](50) NULL,
	[ErrorDescription] [nvarchar](max) NULL,
	[Details] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDemographicsTemp]    Script Date: 26/09/2017 4:20:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDemographicsTemp](
	[BatchNumber] [int] NOT NULL,
	[MemberID] [bigint] NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Gender] [nvarchar](10) NULL,
	[Address1] [nvarchar](50) NULL,
	[Address2] [nvarchar](50) NULL,
	[SuburbTown] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Postcode] [nvarchar](10) NULL,
	[Country] [nvarchar](10) NULL,
	[Email] [nvarchar](50) NULL,
	[MobilePhone] [nvarchar](50) NULL,
	[DateOfBirth] [datetime] NULL,
	[ReceiveEmail] [nvarchar](5) NULL,
	[ReceiveSMS] [nvarchar](5) NULL,
	[CompanyName] [nvarchar](50) NULL,
	[RegistrationDate] [datetime] NULL,
	[LastUpdatedDate] [datetime] NULL,
	[Location] [nvarchar](50) NULL,
	[MemberStatus] [nvarchar](50) NULL,
	[Balance] [nvarchar](20) NULL,
	[StaffId] [nvarchar](20) NULL,
	[Pin] [nvarchar](20) NULL,
	[Password] [nvarchar](20) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblItemTemp]    Script Date: 26/09/2017 4:20:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblItemTemp](
	[BatchNumber] [int] NOT NULL,
	[Date] [datetime] NULL,
	[Site] [int] NULL,
	[Site Name] [nvarchar](100) NULL,
	[Location] [int] NULL,
	[Location Name] [nvarchar](100) NULL,
	[Location Type] [nvarchar](100) NULL,
	[Location Type Name] [nvarchar](100) NULL,
	[Item] [nvarchar](100) NULL,
	[Description] [nvarchar](100) NULL,
	[Department] [int] NULL,
	[Department Name] [nvarchar](100) NULL,
	[Division] [nvarchar](100) NULL,
	[Division Name] [nvarchar](100) NULL,
	[Supplier] [int] NULL,
	[Supplier Name] [nvarchar](100) NULL,
	[Brand] [int] NULL,
	[Brand Name] [nvarchar](100) NULL,
	[Item Size] [nvarchar](50) NULL,
	[Sales] [money] NULL,
	[GST] [money] NULL,
	[Quantity] [float] NULL,
	[Cost] [money] NULL,
	[Item Discount] [money] NULL,
	[Sales Discount Quantity] [int] NULL,
	[ItemNoOrLF] [int] NULL,
	[LFDesc] [nvarchar](60) NULL,
	[ItemFileName] [nvarchar](50) NULL,
	[ImportedDate] [datetime] NULL,
	[tblId] [decimal](18, 0) IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblJournalTemp]    Script Date: 26/09/2017 4:20:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblJournalTemp](
	[BatchNumber] [int] NOT NULL,
	[Date] [datetime] NULL,
	[Site] [int] NULL,
	[Site Name] [nvarchar](100) NULL,
	[Location] [int] NULL,
	[Location Name] [nvarchar](100) NULL,
	[Location Type] [nvarchar](100) NULL,
	[Location Type Name] [nvarchar](100) NULL,
	[Terminal] [int] NULL,
	[Journal] [int] NULL,
	[Sequence] [int] NULL,
	[Time] [datetime] NULL,
	[Item] [nvarchar](100) NULL,
	[Barcode] [nvarchar](30) NULL,
	[Description] [nvarchar](100) NULL,
	[Department] [int] NULL,
	[Department Name] [nvarchar](100) NULL,
	[Division] [nvarchar](100) NULL,
	[Division Name] [nvarchar](100) NULL,
	[Sales] [money] NULL,
	[GST] [money] NULL,
	[Quantity] [float] NULL,
	[Cost] [money] NULL,
	[Item Discount] [money] NULL,
	[Loyalty Discount] [money] NULL,
	[Offer Discount] [money] NULL,
	[Price] [money] NULL,
	[Original Price] [money] NULL,
	[Scanned] [nvarchar](5) NULL,
	[Is Loyalty] [nvarchar](5) NULL,
	[Sales Discount Quantity] [float] NULL,
	[Customer Name] [nvarchar](100) NULL,
	[Postcode] [nvarchar](5) NULL,
	[Email] [nvarchar](100) NULL,
	[Mobile] [nvarchar](20) NULL,
	[Covers] [int] NULL,
	[Card] [nvarchar](30) NULL,
	[Card Type] [int] NULL,
	[Total Points] [float] NULL,
	[Bonus Points] [float] NULL,
	[Redeemed Points] [float] NULL,
	[Redeemed Amount] [money] NULL,
	[Expired Points] [float] NULL,
	[Home Site] [int] NULL,
	[JournalFileName] [nvarchar](50) NULL,
	[ImportedDate] [datetime] NULL,
	[tblId] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[ItemNoOrLF] [int] NULL,
	[LFDesc] [nvarchar](60) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblSalesTemp]    Script Date: 26/09/2017 4:20:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSalesTemp](
	[BatchNumber] [int] NOT NULL,
	[Date] [datetime] NULL,
	[Site] [int] NULL,
	[Site Name] [nvarchar](100) NULL,
	[Location] [int] NULL,
	[Location Name] [nvarchar](100) NULL,
	[Location Type] [nvarchar](100) NULL,
	[Location Type Name] [nvarchar](100) NULL,
	[Sales] [money] NULL,
	[GST] [money] NULL,
	[Quantity] [float] NULL,
	[Cost] [money] NULL,
	[Item Discount] [money] NULL,
	[Sales Discount] [money] NULL,
	[Sales Discount GST] [money] NULL,
	[Sale Discount Quantity] [money] NULL,
	[DataFrom] [nvarchar](255) NULL,
	[SalesFileName] [nvarchar](50) NULL,
	[ImportedDate] [datetime] NULL,
	[tblId] [decimal](18, 0) IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [LBTest1] SET  READ_WRITE 
GO
