USE [master]
GO
/****** Object:  Database [TicketResellDB]    Script Date: 11/12/2024 9:59:18 AM ******/
CREATE DATABASE [TicketResellDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TicketResellDB_Data', FILENAME = N'D:\file download\download IT\MSSQL16.MSSQLSERVER\MSSQL\DATA\TicketResellDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TicketResellDB_Log', FILENAME = N'D:\file download\download IT\MSSQL16.MSSQLSERVER\MSSQL\DATA\TicketResellDB.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [TicketResellDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TicketResellDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TicketResellDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TicketResellDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TicketResellDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TicketResellDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TicketResellDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TicketResellDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TicketResellDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TicketResellDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TicketResellDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TicketResellDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TicketResellDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TicketResellDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TicketResellDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TicketResellDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TicketResellDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TicketResellDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TicketResellDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TicketResellDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TicketResellDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TicketResellDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TicketResellDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TicketResellDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TicketResellDB] SET RECOVERY FULL 
GO
ALTER DATABASE [TicketResellDB] SET  MULTI_USER 
GO
ALTER DATABASE [TicketResellDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TicketResellDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TicketResellDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TicketResellDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TicketResellDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TicketResellDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TicketResellDB', N'ON'
GO
ALTER DATABASE [TicketResellDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [TicketResellDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [TicketResellDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/12/2024 9:59:18 AM ******/
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
/****** Object:  Table [dbo].[chat]    Script Date: 11/12/2024 9:59:18 AM ******/
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
 CONSTRAINT [PK__chat__3213E83F2FA51E38] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[conversation]    Script Date: 11/12/2024 9:59:18 AM ******/
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
 CONSTRAINT [PK__conversa__3213E83F65F77360] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventType]    Script Date: 11/12/2024 9:59:18 AM ******/
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
/****** Object:  Table [dbo].[feedback]    Script Date: 11/12/2024 9:59:18 AM ******/
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
	[ticketId] [uniqueidentifier] NULL,
 CONSTRAINT [PK__feedback__3213E83F84D22FB2] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[image]    Script Date: 11/12/2024 9:59:18 AM ******/
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
 CONSTRAINT [PK__image__3213E83FBC9FA28E] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[membership]    Script Date: 11/12/2024 9:59:18 AM ******/
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
 CONSTRAINT [PK__membersh__3213E83F19560ECA] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[notification]    Script Date: 11/12/2024 9:59:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[notification](
	[id] [uniqueidentifier] NOT NULL,
	[user_id] [uniqueidentifier] NULL,
	[message] [text] NULL,
	[sent_at] [datetime] NULL,
 CONSTRAINT [PK__notifica__3213E83FEC850A9B] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order]    Script Date: 11/12/2024 9:59:18 AM ******/
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
 CONSTRAINT [PK__order__3213E83F78DE46FD] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 11/12/2024 9:59:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NULL,
 CONSTRAINT [PK__role__3213E83FCB30EE41] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[social_media]    Script Date: 11/12/2024 9:59:18 AM ******/
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
 CONSTRAINT [PK__social_m__3213E83F637FDEB7] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[support]    Script Date: 11/12/2024 9:59:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[support](
	[id] [uniqueidentifier] NOT NULL,
	[user_id] [uniqueidentifier] NULL,
	[description] [nvarchar](255) NULL,
	[title] [nvarchar](100) NULL,
 CONSTRAINT [PK__support__3213E83F58F4AB2F] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ticket]    Script Date: 11/12/2024 9:59:18 AM ******/
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
 CONSTRAINT [PK__ticket__3213E83F4C9F3B98] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transactions]    Script Date: 11/12/2024 9:59:18 AM ******/
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
	[paypalPayment_id] [nvarchar](255) NULL,
 CONSTRAINT [PK__transact__3213E83F562E345D] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/12/2024 9:59:18 AM ******/
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
	[password] [varchar](64) NULL,
	[wallet] [decimal](18, 2) NULL,
	[is_verified] [bit] NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK__User__3213E83FE1FCAFE4] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_role]    Script Date: 11/12/2024 9:59:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_role](
	[id] [uniqueidentifier] NOT NULL,
	[user_id] [uniqueidentifier] NULL,
	[role_id] [uniqueidentifier] NULL,
 CONSTRAINT [PK__user_rol__3213E83FC17315BB] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[chat]  WITH CHECK ADD  CONSTRAINT [FK__chat__buyer_id__114A936A] FOREIGN KEY([buyer_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[chat] CHECK CONSTRAINT [FK__chat__buyer_id__114A936A]
GO
ALTER TABLE [dbo].[chat]  WITH CHECK ADD  CONSTRAINT [FK__chat__conversati__10566F31] FOREIGN KEY([conversation_id])
REFERENCES [dbo].[conversation] ([id])
GO
ALTER TABLE [dbo].[chat] CHECK CONSTRAINT [FK__chat__conversati__10566F31]
GO
ALTER TABLE [dbo].[chat]  WITH CHECK ADD  CONSTRAINT [FK__chat__seller_id__123EB7A3] FOREIGN KEY([seller_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[chat] CHECK CONSTRAINT [FK__chat__seller_id__123EB7A3]
GO
ALTER TABLE [dbo].[conversation]  WITH CHECK ADD  CONSTRAINT [FK__conversat__buyer__208CD6FA] FOREIGN KEY([buyer_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[conversation] CHECK CONSTRAINT [FK__conversat__buyer__208CD6FA]
GO
ALTER TABLE [dbo].[conversation]  WITH CHECK ADD  CONSTRAINT [FK__conversat__selle__2180FB33] FOREIGN KEY([seller_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[conversation] CHECK CONSTRAINT [FK__conversat__selle__2180FB33]
GO
ALTER TABLE [dbo].[conversation]  WITH CHECK ADD  CONSTRAINT [FK__conversat__ticke__1F98B2C1] FOREIGN KEY([ticket_id])
REFERENCES [dbo].[ticket] ([id])
GO
ALTER TABLE [dbo].[conversation] CHECK CONSTRAINT [FK__conversat__ticke__1F98B2C1]
GO
ALTER TABLE [dbo].[feedback]  WITH CHECK ADD  CONSTRAINT [FK__feedback__user_i__4CA06362] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[feedback] CHECK CONSTRAINT [FK__feedback__user_i__4CA06362]
GO
ALTER TABLE [dbo].[feedback]  WITH CHECK ADD  CONSTRAINT [FK_feedback_ticket] FOREIGN KEY([ticketId])
REFERENCES [dbo].[ticket] ([id])
GO
ALTER TABLE [dbo].[feedback] CHECK CONSTRAINT [FK_feedback_ticket]
GO
ALTER TABLE [dbo].[image]  WITH CHECK ADD  CONSTRAINT [FK__image__ticket_id__7D439ABD] FOREIGN KEY([ticket_id])
REFERENCES [dbo].[ticket] ([id])
GO
ALTER TABLE [dbo].[image] CHECK CONSTRAINT [FK__image__ticket_id__7D439ABD]
GO
ALTER TABLE [dbo].[membership]  WITH CHECK ADD  CONSTRAINT [FK__membershi__user___5DCAEF64] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[membership] CHECK CONSTRAINT [FK__membershi__user___5DCAEF64]
GO
ALTER TABLE [dbo].[notification]  WITH CHECK ADD  CONSTRAINT [FK__notificat__user___49C3F6B7] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[notification] CHECK CONSTRAINT [FK__notificat__user___49C3F6B7]
GO
ALTER TABLE [dbo].[order]  WITH CHECK ADD  CONSTRAINT [FK__order__ticket_id__71D1E811] FOREIGN KEY([ticket_id])
REFERENCES [dbo].[ticket] ([id])
GO
ALTER TABLE [dbo].[order] CHECK CONSTRAINT [FK__order__ticket_id__71D1E811]
GO
ALTER TABLE [dbo].[order]  WITH CHECK ADD  CONSTRAINT [FK__order__user_id__70DDC3D8] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[order] CHECK CONSTRAINT [FK__order__user_id__70DDC3D8]
GO
ALTER TABLE [dbo].[social_media]  WITH CHECK ADD  CONSTRAINT [FK__social_me__user___398D8EEE] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[social_media] CHECK CONSTRAINT [FK__social_me__user___398D8EEE]
GO
ALTER TABLE [dbo].[support]  WITH CHECK ADD  CONSTRAINT [FK__support__user_id__4F7CD00D] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[support] CHECK CONSTRAINT [FK__support__user_id__4F7CD00D]
GO
ALTER TABLE [dbo].[ticket]  WITH CHECK ADD  CONSTRAINT [FK__ticket__event_ty__6E01572D] FOREIGN KEY([event_type_id])
REFERENCES [dbo].[EventType] ([id])
GO
ALTER TABLE [dbo].[ticket] CHECK CONSTRAINT [FK__ticket__event_ty__6E01572D]
GO
ALTER TABLE [dbo].[ticket]  WITH CHECK ADD  CONSTRAINT [FK__ticket__owner_id__6D0D32F4] FOREIGN KEY([owner_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[ticket] CHECK CONSTRAINT [FK__ticket__owner_id__6D0D32F4]
GO
ALTER TABLE [dbo].[transactions]  WITH CHECK ADD  CONSTRAINT [FK__transacti__buyer__245D67DE] FOREIGN KEY([buyer_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[transactions] CHECK CONSTRAINT [FK__transacti__buyer__245D67DE]
GO
ALTER TABLE [dbo].[transactions]  WITH CHECK ADD  CONSTRAINT [FK__transacti__selle__25518C17] FOREIGN KEY([seller_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[transactions] CHECK CONSTRAINT [FK__transacti__selle__25518C17]
GO
ALTER TABLE [dbo].[transactions]  WITH CHECK ADD  CONSTRAINT [FK__transacti__ticke__2645B050] FOREIGN KEY([ticket_id])
REFERENCES [dbo].[ticket] ([id])
GO
ALTER TABLE [dbo].[transactions] CHECK CONSTRAINT [FK__transacti__ticke__2645B050]
GO
ALTER TABLE [dbo].[user_role]  WITH CHECK ADD  CONSTRAINT [FK__user_role__role___5AEE82B9] FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([id])
GO
ALTER TABLE [dbo].[user_role] CHECK CONSTRAINT [FK__user_role__role___5AEE82B9]
GO
ALTER TABLE [dbo].[user_role]  WITH CHECK ADD  CONSTRAINT [FK__user_role__user___59FA5E80] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[user_role] CHECK CONSTRAINT [FK__user_role__user___59FA5E80]
GO
USE [master]
GO
ALTER DATABASE [TicketResellDB] SET  READ_WRITE 
GO
