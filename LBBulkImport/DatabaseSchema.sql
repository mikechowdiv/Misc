
/****** Object:  Table [dbo].[Batch]    Script Date: 8/30/2017 10:36:01 AM ******/
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
/****** Object:  Table [dbo].[BatchSummary]    Script Date: 8/30/2017 10:36:01 AM ******/
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
/****** Object:  Table [dbo].[Execptions]    Script Date: 8/30/2017 10:36:01 AM ******/
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
/****** Object:  Table [dbo].[tblDemographicsTemp]    Script Date: 8/30/2017 10:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDemographicsTemp](
	[BatchNumber] [int] NOT NULL,
	[MemberID] [int] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Gender] [nvarchar](10) NULL,
	[Address1] [nvarchar](50) NULL,
	[Address2] [nvarchar](50) NULL,
	[SuburbTown] [nvarchar](50) NULL,
	[State] [nvarchar](10) NULL,
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
/****** Object:  Table [dbo].[tblItemTemp]    Script Date: 8/30/2017 10:36:01 AM ******/
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
/****** Object:  Table [dbo].[tblJournalTemp]    Script Date: 8/30/2017 10:36:01 AM ******/
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
/****** Object:  Table [dbo].[tblSalesTemp]    Script Date: 8/30/2017 10:36:01 AM ******/
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
