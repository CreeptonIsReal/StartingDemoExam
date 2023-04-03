USE [master]
GO
/****** Object:  Database [BaseKitchen]    Script Date: 01.04.2023 11:24:02 ******/
CREATE DATABASE [BaseKitchen]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BaseKitchen', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\BaseKitchen.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BaseKitchen_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\BaseKitchen_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BaseKitchen] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BaseKitchen].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BaseKitchen] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BaseKitchen] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BaseKitchen] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BaseKitchen] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BaseKitchen] SET ARITHABORT OFF 
GO
ALTER DATABASE [BaseKitchen] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BaseKitchen] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BaseKitchen] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BaseKitchen] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BaseKitchen] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BaseKitchen] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BaseKitchen] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BaseKitchen] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BaseKitchen] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BaseKitchen] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BaseKitchen] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BaseKitchen] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BaseKitchen] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BaseKitchen] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BaseKitchen] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BaseKitchen] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BaseKitchen] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BaseKitchen] SET RECOVERY FULL 
GO
ALTER DATABASE [BaseKitchen] SET  MULTI_USER 
GO
ALTER DATABASE [BaseKitchen] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BaseKitchen] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BaseKitchen] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BaseKitchen] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BaseKitchen] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BaseKitchen] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BaseKitchen', N'ON'
GO
ALTER DATABASE [BaseKitchen] SET QUERY_STORE = OFF
GO
USE [BaseKitchen]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 01.04.2023 11:24:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[OrderStatus] [nvarchar](max) NOT NULL,
	[OrderDeliveryDate] [datetime] NOT NULL,
	[OrderPickupPoint] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderProduct]    Script Date: 01.04.2023 11:24:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderProduct](
	[OrderID] [int] NOT NULL,
	[ProductArticleNumber] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[ProductArticleNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 01.04.2023 11:24:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductArticleNumber] [nvarchar](100) NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[ProductDescription] [nvarchar](max) NOT NULL,
	[ProductCategory] [nvarchar](max) NOT NULL,
	[ProductPhoto] [image] NULL,
	[ProductManufacturer] [nvarchar](max) NOT NULL,
	[ProductCost] [decimal](19, 4) NOT NULL,
	[ProductDiscountAmount] [tinyint] NULL,
	[ProductQuantityInStock] [int] NOT NULL,
	[ProductStatus] [nvarchar](max) NULL,
 CONSTRAINT [PK__Product__2EA7DCD5F0938ACF] PRIMARY KEY CLUSTERED 
(
	[ProductArticleNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 01.04.2023 11:24:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 01.04.2023 11:24:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserSurname] [nvarchar](100) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[UserPatronymic] [nvarchar](100) NOT NULL,
	[UserLogin] [nvarchar](max) NOT NULL,
	[UserPassword] [nvarchar](max) NOT NULL,
	[UserRole] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderProduct]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([OrderID])
GO
ALTER TABLE [dbo].[OrderProduct]  WITH CHECK ADD  CONSTRAINT [FK__OrderProd__Produ__2E1BDC42] FOREIGN KEY([ProductArticleNumber])
REFERENCES [dbo].[Product] ([ProductArticleNumber])
GO
ALTER TABLE [dbo].[OrderProduct] CHECK CONSTRAINT [FK__OrderProd__Produ__2E1BDC42]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([UserRole])
REFERENCES [dbo].[Role] ([RoleID])
GO
USE [master]
GO
ALTER DATABASE [BaseKitchen] SET  READ_WRITE 
GO
