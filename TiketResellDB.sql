USE [master]
GO
/****** Object:  Database [TicketResell]    Script Date: 11/5/2024 9:59:34 AM ******/
CREATE DATABASE [TicketResell]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TicketResell', FILENAME = N'D:\file download\download IT\MSSQL16.MSSQLSERVER\MSSQL\DATA\TicketResell.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TicketResell_log', FILENAME = N'D:\file download\download IT\MSSQL16.MSSQLSERVER\MSSQL\DATA\TicketResell_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [TicketResell] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TicketResell].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TicketResell] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TicketResell] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TicketResell] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TicketResell] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TicketResell] SET ARITHABORT OFF 
GO
ALTER DATABASE [TicketResell] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TicketResell] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TicketResell] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TicketResell] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TicketResell] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TicketResell] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TicketResell] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TicketResell] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TicketResell] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TicketResell] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TicketResell] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TicketResell] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TicketResell] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TicketResell] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TicketResell] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TicketResell] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TicketResell] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TicketResell] SET RECOVERY FULL 
GO
ALTER DATABASE [TicketResell] SET  MULTI_USER 
GO
ALTER DATABASE [TicketResell] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TicketResell] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TicketResell] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TicketResell] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TicketResell] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TicketResell] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TicketResell', N'ON'
GO
ALTER DATABASE [TicketResell] SET QUERY_STORE = ON
GO
ALTER DATABASE [TicketResell] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [TicketResell]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/5/2024 9:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chat]    Script Date: 11/5/2024 9:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chat](
	[id] [uniqueidentifier] NOT NULL,
	[conversation_id] [uniqueidentifier] NULL,
	[buyer_id] [uniqueidentifier] NULL,
	[seller_id] [uniqueidentifier] NULL,
	[message] [text] NULL,
	[sent_at] [datetime] NULL,
	[end_at] [datetime] NULL,
	[is_sender] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[conversation]    Script Date: 11/5/2024 9:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[conversation](
	[id] [uniqueidentifier] NOT NULL,
	[buyer_id] [uniqueidentifier] NULL,
	[seller_id] [uniqueidentifier] NULL,
	[started_at] [datetime] NULL,
	[ended_at] [datetime] NULL,
	[ticket_id] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventType]    Script Date: 11/5/2024 9:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventType](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[feedback]    Script Date: 11/5/2024 9:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[feedback](
	[id] [uniqueidentifier] NOT NULL,
	[rating] [int] NULL,
	[comment] [nvarchar](255) NULL,
	[user_id] [uniqueidentifier] NULL,
	[submitted_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[image]    Script Date: 11/5/2024 9:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[image](
	[id] [uniqueidentifier] NOT NULL,
	[ticket_id] [uniqueidentifier] NULL,
	[url] [nvarchar](255) NULL,
	[create_at] [datetime] NULL,
	[update_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[membership]    Script Date: 11/5/2024 9:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[membership](
	[id] [uniqueidentifier] NOT NULL,
	[user_id] [uniqueidentifier] NULL,
	[package_type] [nvarchar](50) NULL,
	[subscription_fee] [decimal](18, 2) NULL,
	[valid_from] [datetime] NULL,
	[valid_to] [datetime] NULL,
	[status] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[notification]    Script Date: 11/5/2024 9:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[notification](
	[id] [uniqueidentifier] NOT NULL,
	[user_id] [uniqueidentifier] NULL,
	[message] [text] NULL,
	[sent_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order]    Script Date: 11/5/2024 9:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order](
	[id] [uniqueidentifier] NOT NULL,
	[user_id] [uniqueidentifier] NULL,
	[ticket_id] [uniqueidentifier] NULL,
	[order_status] [nvarchar](50) NULL,
	[delivery_address] [nvarchar](255) NULL,
	[delivery_phone] [nvarchar](20) NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 11/5/2024 9:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[social_media]    Script Date: 11/5/2024 9:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[social_media](
	[id] [uniqueidentifier] NOT NULL,
	[user_id] [uniqueidentifier] NULL,
	[type] [nvarchar](50) NULL,
	[link] [nvarchar](255) NULL,
	[status] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[support]    Script Date: 11/5/2024 9:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[support](
	[id] [uniqueidentifier] NOT NULL,
	[user_id] [uniqueidentifier] NULL,
	[description] [nvarchar](255) NULL,
	[title] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ticket]    Script Date: 11/5/2024 9:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ticket](
	[id] [uniqueidentifier] NOT NULL,
	[owner_id] [uniqueidentifier] NULL,
	[event_name] [nvarchar](255) NULL,
	[event_type_id] [uniqueidentifier] NULL,
	[event_date] [datetime] NULL,
	[price] [float] NULL,
	[ticket_status] [nvarchar](50) NULL,
	[submitted_at] [datetime] NULL,
	[serial] [nvarchar](50) NULL,
	[expiration_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transactions]    Script Date: 11/5/2024 9:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transactions](
	[id] [uniqueidentifier] NOT NULL,
	[ticket_id] [uniqueidentifier] NOT NULL,
	[buyer_id] [uniqueidentifier] NOT NULL,
	[seller_id] [uniqueidentifier] NOT NULL,
	[type_transaction] [nvarchar](30) NULL,
	[amount] [decimal](18, 2) NOT NULL,
	[platform_fee] [decimal](18, 2) NOT NULL,
	[transaction_date] [datetime] NOT NULL,
	[transaction_status] [nvarchar](30) NULL,
	[penalty_amount] [decimal](18, 2) NOT NULL,
	[paypalPayment_id] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/5/2024 9:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [uniqueidentifier] NOT NULL,
	[phone] [nvarchar](20) NULL,
	[email] [nvarchar](100) NULL,
	[username] [nvarchar](50) NULL,
	[created_at] [datetime] NULL,
	[reputation_points] [int] NULL,
	[password] [nchar](30) NULL,
	[wallet] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_role]    Script Date: 11/5/2024 9:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_role](
	[id] [uniqueidentifier] NOT NULL,
	[user_id] [uniqueidentifier] NULL,
	[role_id] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[chat]  WITH CHECK ADD FOREIGN KEY([buyer_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[chat]  WITH CHECK ADD FOREIGN KEY([conversation_id])
REFERENCES [dbo].[conversation] ([id])
GO
ALTER TABLE [dbo].[chat]  WITH CHECK ADD FOREIGN KEY([seller_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[conversation]  WITH CHECK ADD FOREIGN KEY([ticket_id])
REFERENCES [dbo].[ticket] ([id])
GO
ALTER TABLE [dbo].[conversation]  WITH CHECK ADD  CONSTRAINT [FK__conversation__ticket__10566F31] FOREIGN KEY([id])
REFERENCES [dbo].[conversation] ([id])
GO
ALTER TABLE [dbo].[conversation] CHECK CONSTRAINT [FK__conversation__ticket__10566F31]
GO
ALTER TABLE [dbo].[feedback]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[feedback]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[image]  WITH CHECK ADD FOREIGN KEY([ticket_id])
REFERENCES [dbo].[ticket] ([id])
GO
ALTER TABLE [dbo].[membership]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[membership]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[notification]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[notification]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[order]  WITH CHECK ADD FOREIGN KEY([ticket_id])
REFERENCES [dbo].[ticket] ([id])
GO
ALTER TABLE [dbo].[order]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[social_media]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[social_media]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[support]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[support]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[ticket]  WITH CHECK ADD FOREIGN KEY([event_type_id])
REFERENCES [dbo].[EventType] ([id])
GO
ALTER TABLE [dbo].[ticket]  WITH CHECK ADD FOREIGN KEY([owner_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[transactions]  WITH CHECK ADD FOREIGN KEY([buyer_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[transactions]  WITH CHECK ADD FOREIGN KEY([seller_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[transactions]  WITH CHECK ADD FOREIGN KEY([ticket_id])
REFERENCES [dbo].[ticket] ([id])
GO
ALTER TABLE [dbo].[user_role]  WITH CHECK ADD FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([id])
GO
ALTER TABLE [dbo].[user_role]  WITH CHECK ADD FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([id])
GO
ALTER TABLE [dbo].[user_role]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[user_role]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
USE [master]
GO
ALTER DATABASE [TicketResell] SET  READ_WRITE 
GO
