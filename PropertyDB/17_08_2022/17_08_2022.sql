USE [master]
GO
/****** Object:  Database [EnManai]    Script Date: 8/17/2022 11:43:21 PM ******/
CREATE DATABASE [EnManai]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EnManai', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\EnManai.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EnManai_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\EnManai_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [EnManai] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EnManai].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EnManai] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EnManai] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EnManai] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EnManai] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EnManai] SET ARITHABORT OFF 
GO
ALTER DATABASE [EnManai] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EnManai] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EnManai] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EnManai] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EnManai] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EnManai] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EnManai] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EnManai] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EnManai] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EnManai] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EnManai] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EnManai] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EnManai] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EnManai] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EnManai] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EnManai] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EnManai] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EnManai] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EnManai] SET  MULTI_USER 
GO
ALTER DATABASE [EnManai] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EnManai] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EnManai] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EnManai] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EnManai] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EnManai] SET QUERY_STORE = OFF
GO
USE [EnManai]
GO
/****** Object:  Table [dbo].[HouseOwner]    Script Date: 8/17/2022 11:43:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HouseOwner](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[FatherName] [nvarchar](max) NOT NULL,
	[MotherName] [nvarchar](max) NOT NULL,
	[AadharNo] [nvarchar](50) NULL,
	[PhonePrimary] [nvarchar](30) NOT NULL,
	[Phone2] [nvarchar](30) NULL,
	[LandLine1] [nvarchar](30) NULL,
	[LandLine2] [nvarchar](30) NULL,
	[EmailAddress] [nvarchar](50) NOT NULL,
	[Address1] [nvarchar](max) NOT NULL,
	[Address2] [nvarchar](max) NOT NULL,
	[CIty] [nvarchar](max) NOT NULL,
	[District] [nvarchar](max) NOT NULL,
	[State] [nvarchar](max) NOT NULL,
	[Pincode] [nchar](10) NULL,
	[ResidingAddress] [bit] NOT NULL,
 CONSTRAINT [PK_HouseOwner] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HouseOwnerResidingAddress]    Script Date: 8/17/2022 11:43:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HouseOwnerResidingAddress](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HouseOwnerID] [int] NOT NULL,
	[Address1] [nvarchar](max) NOT NULL,
	[Address2] [nvarchar](max) NOT NULL,
	[CIty] [nvarchar](max) NOT NULL,
	[District] [nvarchar](max) NOT NULL,
	[State] [nvarchar](max) NOT NULL,
	[Pincode] [nchar](10) NULL,
 CONSTRAINT [PK_ouseOwnerResidingAddress] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RentalHouseDetails]    Script Date: 8/17/2022 11:43:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentalHouseDetails](
	[Id] [int] NOT NULL,
	[HouseOwnerId] [int] NOT NULL,
	[FlatNoOrDoorNo] [nvarchar](50) NOT NULL,
	[Address1] [nvarchar](max) NOT NULL,
	[Address2] [nvarchar](max) NOT NULL,
	[AreaOrNagar] [nvarchar](max) NOT NULL,
	[City] [nvarchar](max) NOT NULL,
	[District] [nvarchar](max) NOT NULL,
	[State] [nvarchar](max) NOT NULL,
	[Pincode] [nvarchar](20) NOT NULL,
	[Floor] [nvarchar](10) NOT NULL,
	[Vasuthu] [bit] NOT NULL,
	[Co-OperationWater] [bit] NOT NULL,
	[BoreWater] [bit] NOT NULL,
	[SeparateEB] [bit] NOT NULL,
	[TwoWheelerParking] [bit] NOT NULL,
	[FourWheelerParking] [bit] NOT NULL,
	[SeparateHouse] [bit] NOT NULL,
	[HouseOwnerResidingInSameBuilding] [bit] NOT NULL,
	[RentalOccupied] [nchar](10) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[HouseOwner] ADD  CONSTRAINT [DF_HouseOwner_ResidingAddress]  DEFAULT ((0)) FOR [ResidingAddress]
GO
USE [master]
GO
ALTER DATABASE [EnManai] SET  READ_WRITE 
GO
