USE [master]
GO
/****** Object:  Database [VoicePlatform]    Script Date: 10/28/2021 10:25:36 PM ******/
CREATE DATABASE [VoicePlatform]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VoicePlatform1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.JANGLEE\MSSQL\DATA\VoicePlatform1.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VoicePlatform1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.JANGLEE\MSSQL\DATA\VoicePlatform1_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [VoicePlatform] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VoicePlatform].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VoicePlatform] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VoicePlatform] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VoicePlatform] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VoicePlatform] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VoicePlatform] SET ARITHABORT OFF 
GO
ALTER DATABASE [VoicePlatform] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [VoicePlatform] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VoicePlatform] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VoicePlatform] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VoicePlatform] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VoicePlatform] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VoicePlatform] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VoicePlatform] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VoicePlatform] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VoicePlatform] SET  ENABLE_BROKER 
GO
ALTER DATABASE [VoicePlatform] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VoicePlatform] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VoicePlatform] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VoicePlatform] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VoicePlatform] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VoicePlatform] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VoicePlatform] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VoicePlatform] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VoicePlatform] SET  MULTI_USER 
GO
ALTER DATABASE [VoicePlatform] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VoicePlatform] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VoicePlatform] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VoicePlatform] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VoicePlatform] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VoicePlatform] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [VoicePlatform] SET QUERY_STORE = OFF
GO
USE [VoicePlatform]
GO
/****** Object:  User [newrelic]    Script Date: 10/28/2021 10:25:40 PM ******/
CREATE USER [newrelic] FOR LOGIN [newrelic] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[AdminToken]    Script Date: 10/28/2021 10:25:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminToken](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Token] [varchar](256) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Artist]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Artist](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [varchar](256) NOT NULL,
	[Email] [varchar](256) NOT NULL,
	[Phone] [varchar](10) NULL,
	[Password] [varchar](256) NOT NULL,
	[FirstName] [nvarchar](256) NOT NULL,
	[LastName] [nvarchar](256) NOT NULL,
	[Role] [int] NOT NULL,
	[Gender] [uniqueidentifier] NOT NULL,
	[Avatar] [nvarchar](max) NOT NULL,
	[Bio] [nvarchar](512) NOT NULL,
	[Price] [float] NOT NULL,
	[Studio] [bit] NOT NULL,
	[Rate] [float] NULL,
	[Status] [int] NOT NULL,
	[LastLoginTime] [datetime] NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdateBy] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArtistCountry]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArtistCountry](
	[ArtistId] [uniqueidentifier] NOT NULL,
	[CountryId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ArtistId] ASC,
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArtistProject]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArtistProject](
	[ArtistId] [uniqueidentifier] NOT NULL,
	[ProjectId] [uniqueidentifier] NOT NULL,
	[Rate] [float] NULL,
	[Comment] [nvarchar](256) NULL,
	[InvitedDate] [datetime] NULL,
	[RequestedDate] [datetime] NULL,
	[JoinedDate] [datetime] NULL,
	[CanceledDate] [datetime] NULL,
	[FinishedDate] [datetime] NULL,
	[ReviewDate] [datetime] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK__ArtistPr__3211C0BF127A0A09] PRIMARY KEY CLUSTERED 
(
	[ArtistId] ASC,
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArtistProjectFile]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArtistProjectFile](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Url] [nvarchar](max) NOT NULL,
	[ProjectId] [uniqueidentifier] NOT NULL,
	[MediaType] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreateBy] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](256) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Comment] [nvarchar](256) NULL,
 CONSTRAINT [PK__ArtistPr__3214EC0732DE8308] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArtistToken]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArtistToken](
	[ArtistId] [uniqueidentifier] NOT NULL,
	[Token] [nvarchar](256) NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ArtistToken] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArtistVoiceDemo]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArtistVoiceDemo](
	[ArtistId] [uniqueidentifier] NOT NULL,
	[VoiceDemoId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ArtistId] ASC,
	[VoiceDemoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArtistVoiceStyle]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArtistVoiceStyle](
	[ArtistId] [uniqueidentifier] NOT NULL,
	[VoiceStyleId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ArtistId] ASC,
	[VoiceStyleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [varchar](256) NOT NULL,
	[Email] [varchar](256) NOT NULL,
	[Phone] [varchar](10) NULL,
	[Password] [varchar](256) NOT NULL,
	[FirstName] [nvarchar](256) NOT NULL,
	[LastName] [nvarchar](256) NOT NULL,
	[Role] [int] NOT NULL,
	[Gender] [uniqueidentifier] NOT NULL,
	[Avatar] [nvarchar](max) NOT NULL,
	[Status] [int] NOT NULL,
	[LastLoginTime] [datetime] NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdateBy] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerProjectFile]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerProjectFile](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Url] [nvarchar](max) NOT NULL,
	[ProjectId] [uniqueidentifier] NOT NULL,
	[MediaType] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreateBy] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Description] [nvarchar](256) NOT NULL,
	[Comment] [nvarchar](256) NULL,
 CONSTRAINT [PK__Customer__3214EC0743D69A19] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerToken]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerToken](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[Token] [varchar](256) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Poster] [uniqueidentifier] NOT NULL,
	[MinAge] [int] NOT NULL,
	[MaxAge] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[Description] [nvarchar](512) NOT NULL,
	[ResponseDeadline] [datetime] NOT NULL,
	[ProjectDeadline] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectCountry]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectCountry](
	[ProjectId] [uniqueidentifier] NOT NULL,
	[CountryId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC,
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectGender]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectGender](
	[ProjectId] [uniqueidentifier] NOT NULL,
	[GenderId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC,
	[GenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectUsagePurpose]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectUsagePurpose](
	[ProjectId] [uniqueidentifier] NOT NULL,
	[UsagePurposeId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC,
	[UsagePurposeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectVoiceStyle]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectVoiceStyle](
	[ProjectId] [uniqueidentifier] NOT NULL,
	[VoiceStyleId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC,
	[VoiceStyleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsagePurpose]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsagePurpose](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [varchar](256) NOT NULL,
	[Email] [varchar](256) NOT NULL,
	[Phone] [varchar](10) NULL,
	[Password] [varchar](256) NOT NULL,
	[FirstName] [nvarchar](256) NOT NULL,
	[LastName] [nvarchar](256) NOT NULL,
	[Role] [int] NOT NULL,
	[Gender] [uniqueidentifier] NOT NULL,
	[Avatar] [nvarchar](max) NOT NULL,
	[Status] [int] NOT NULL,
	[LastLoginTime] [datetime] NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdateBy] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VoiceDemo]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VoiceDemo](
	[Id] [uniqueidentifier] NOT NULL,
	[Url] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VoiceStyle]    Script Date: 10/28/2021 10:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VoiceStyle](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Artist] ([Id], [Username], [Email], [Phone], [Password], [FirstName], [LastName], [Role], [Gender], [Avatar], [Bio], [Price], [Studio], [Rate], [Status], [LastLoginTime], [CreateDate], [UpdateDate], [UpdateBy]) VALUES (N'80063860-ce9b-475b-b5df-19a6f651cbdf', N'phucle', N'ledachoangphuc@gmail.com', N'0354125478', N'12345678', N'Phúc', N'Lê', 1, N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf', N'https://images.unsplash.com/photo-1535713875002-d1d0cf377fde?ixid=MnwxMjA3fDB8MHxzZWFyY2h8MXx8YXZhdGFyfGVufDB8fDB8fA%3D%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=500&q=60', N'I can use my voice to make the video for many type ', 150, 0, NULL, 0, NULL, CAST(N'2021-10-05T22:04:03.823' AS DateTime), CAST(N'2021-10-27T18:23:04.160' AS DateTime), NULL)
INSERT [dbo].[Artist] ([Id], [Username], [Email], [Phone], [Password], [FirstName], [LastName], [Role], [Gender], [Avatar], [Bio], [Price], [Studio], [Rate], [Status], [LastLoginTime], [CreateDate], [UpdateDate], [UpdateBy]) VALUES (N'84ff7a5e-a5d4-4c04-bb0e-2fd3248745df', N'trongns', N'strfsdang@gmail.com', N'0352125478', N'12345678', N'Trọng', N'Sĩ', 1, N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf', N'https://images.unsplash.com/photo-1599566150163-29194dcaad36?ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8YXZhdGFyfGVufDB8fDB8fA%3D%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=500&q=60', N'Tôi có thể lồng cho các loại vlog trên Youtube.', 120, 0, NULL, 0, NULL, CAST(N'2021-10-05T22:04:19.680' AS DateTime), NULL, NULL)
INSERT [dbo].[Artist] ([Id], [Username], [Email], [Phone], [Password], [FirstName], [LastName], [Role], [Gender], [Avatar], [Bio], [Price], [Studio], [Rate], [Status], [LastLoginTime], [CreateDate], [UpdateDate], [UpdateBy]) VALUES (N'f4a65019-94ed-40f6-86fd-aec24b3dc31e', N'andee123', N'andee@gmail.com', N'0912365487', N'andee123@', N'An', N'Mai', 1, N'4461e740-e465-4b4f-927f-3a4f4a3e8cb7', N'https://images.unsplash.com/photo-1607746882042-944635dfe10e?ixid=MnwxMjA3fDB8MHxzZWFyY2h8NXx8YXZhdGFyfGVufDB8fDB8fA%3D%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=500&q=60', N'Giọng tôi tuy không hay nhưng lại hay không tưởng.', 110, 0, NULL, 1, NULL, CAST(N'2021-10-11T10:05:58.790' AS DateTime), CAST(N'2021-10-21T09:42:19.287' AS DateTime), N'596cf15f-40c7-4b24-8c48-364ff0713356')
INSERT [dbo].[Artist] ([Id], [Username], [Email], [Phone], [Password], [FirstName], [LastName], [Role], [Gender], [Avatar], [Bio], [Price], [Studio], [Rate], [Status], [LastLoginTime], [CreateDate], [UpdateDate], [UpdateBy]) VALUES (N'37035726-a496-4899-98fd-bd190bf0f979', N'huyle', N'striasdang@gmail.com', N'0254125478', N'12345678', N'Huy', N'Võ', 1, N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf', N'https://images.unsplash.com/photo-1527980965255-d3b416303d12?ixid=MnwxMjA3fDB8MHxzZWFyY2h8OHx8YXZhdGFyfGVufDB8fDB8fA%3D%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=500&q=60', N'Gia đình tôi thích nghe giọng tôi lắm, nên chắc bạn cũng sẽ thích đấy.', 90, 0, NULL, 0, NULL, CAST(N'2021-10-05T22:03:12.173' AS DateTime), NULL, NULL)
INSERT [dbo].[Artist] ([Id], [Username], [Email], [Phone], [Password], [FirstName], [LastName], [Role], [Gender], [Avatar], [Bio], [Price], [Studio], [Rate], [Status], [LastLoginTime], [CreateDate], [UpdateDate], [UpdateBy]) VALUES (N'21ac1f58-b824-475b-902f-f29cc74b0d39', N'bachnguyen', N'bachnguyen@gmail.com', N'0912345678', N'12345678', N'Bách', N'Nguyễn', 1, N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf', N'https://images.unsplash.com/photo-1601455763557-db1bea8a9a5a?ixid=MnwxMjA3fDB8MHxzZWFyY2h8MTV8fGF2YXRhcnxlbnwwfHwwfHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=500&q=60', N'Hân hạnh được đóng góp tí công sức cho công việc của bạn.', 95, 0, 5, 0, NULL, CAST(N'2021-10-03T10:42:57.027' AS DateTime), CAST(N'2021-10-13T15:25:02.920' AS DateTime), NULL)
GO
INSERT [dbo].[ArtistCountry] ([ArtistId], [CountryId]) VALUES (N'80063860-ce9b-475b-b5df-19a6f651cbdf', N'e50a231b-1596-48eb-a1ce-1095fb6aa63b')
INSERT [dbo].[ArtistCountry] ([ArtistId], [CountryId]) VALUES (N'80063860-ce9b-475b-b5df-19a6f651cbdf', N'7cbe503c-c8a0-4596-93aa-3c9fb7366c33')
INSERT [dbo].[ArtistCountry] ([ArtistId], [CountryId]) VALUES (N'84ff7a5e-a5d4-4c04-bb0e-2fd3248745df', N'e50a231b-1596-48eb-a1ce-1095fb6aa63b')
INSERT [dbo].[ArtistCountry] ([ArtistId], [CountryId]) VALUES (N'84ff7a5e-a5d4-4c04-bb0e-2fd3248745df', N'd6dfce11-bd2f-4654-b24e-2a5df2e07cf0')
INSERT [dbo].[ArtistCountry] ([ArtistId], [CountryId]) VALUES (N'f4a65019-94ed-40f6-86fd-aec24b3dc31e', N'2179f408-b174-465d-8126-1562b04f12ad')
INSERT [dbo].[ArtistCountry] ([ArtistId], [CountryId]) VALUES (N'f4a65019-94ed-40f6-86fd-aec24b3dc31e', N'd6dfce11-bd2f-4654-b24e-2a5df2e07cf0')
INSERT [dbo].[ArtistCountry] ([ArtistId], [CountryId]) VALUES (N'f4a65019-94ed-40f6-86fd-aec24b3dc31e', N'7cbe503c-c8a0-4596-93aa-3c9fb7366c33')
INSERT [dbo].[ArtistCountry] ([ArtistId], [CountryId]) VALUES (N'37035726-a496-4899-98fd-bd190bf0f979', N'e50a231b-1596-48eb-a1ce-1095fb6aa63b')
INSERT [dbo].[ArtistCountry] ([ArtistId], [CountryId]) VALUES (N'37035726-a496-4899-98fd-bd190bf0f979', N'7cbe503c-c8a0-4596-93aa-3c9fb7366c33')
INSERT [dbo].[ArtistCountry] ([ArtistId], [CountryId]) VALUES (N'21ac1f58-b824-475b-902f-f29cc74b0d39', N'e50a231b-1596-48eb-a1ce-1095fb6aa63b')
GO
INSERT [dbo].[ArtistProject] ([ArtistId], [ProjectId], [Rate], [Comment], [InvitedDate], [RequestedDate], [JoinedDate], [CanceledDate], [FinishedDate], [ReviewDate], [Status]) VALUES (N'80063860-ce9b-475b-b5df-19a6f651cbdf', N'ac1b5aa1-676a-4d53-bf57-213b2acb5ca1', NULL, NULL, NULL, CAST(N'2021-10-26T12:06:16.377' AS DateTime), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ArtistProject] ([ArtistId], [ProjectId], [Rate], [Comment], [InvitedDate], [RequestedDate], [JoinedDate], [CanceledDate], [FinishedDate], [ReviewDate], [Status]) VALUES (N'80063860-ce9b-475b-b5df-19a6f651cbdf', N'975b3044-7ea2-49dd-afbb-5323542c900c', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2021-10-26T10:27:43.870' AS DateTime), NULL, 4)
INSERT [dbo].[ArtistProject] ([ArtistId], [ProjectId], [Rate], [Comment], [InvitedDate], [RequestedDate], [JoinedDate], [CanceledDate], [FinishedDate], [ReviewDate], [Status]) VALUES (N'80063860-ce9b-475b-b5df-19a6f651cbdf', N'93846e4f-e99b-4fb4-839a-7e200f594c8a', NULL, NULL, NULL, NULL, CAST(N'2021-10-26T09:50:22.303' AS DateTime), NULL, NULL, NULL, 2)
INSERT [dbo].[ArtistProject] ([ArtistId], [ProjectId], [Rate], [Comment], [InvitedDate], [RequestedDate], [JoinedDate], [CanceledDate], [FinishedDate], [ReviewDate], [Status]) VALUES (N'80063860-ce9b-475b-b5df-19a6f651cbdf', N'770de6ef-487e-4435-9a01-f7d87a4e9322', NULL, NULL, NULL, NULL, CAST(N'2021-10-26T11:33:06.990' AS DateTime), NULL, NULL, NULL, 2)
INSERT [dbo].[ArtistProject] ([ArtistId], [ProjectId], [Rate], [Comment], [InvitedDate], [RequestedDate], [JoinedDate], [CanceledDate], [FinishedDate], [ReviewDate], [Status]) VALUES (N'84ff7a5e-a5d4-4c04-bb0e-2fd3248745df', N'807f39a2-5a12-4295-abdf-232f984f7c4b', NULL, NULL, CAST(N'2021-10-18T00:00:00.000' AS DateTime), NULL, NULL, CAST(N'2021-10-19T00:00:00.000' AS DateTime), NULL, NULL, 3)
INSERT [dbo].[ArtistProject] ([ArtistId], [ProjectId], [Rate], [Comment], [InvitedDate], [RequestedDate], [JoinedDate], [CanceledDate], [FinishedDate], [ReviewDate], [Status]) VALUES (N'84ff7a5e-a5d4-4c04-bb0e-2fd3248745df', N'975b3044-7ea2-49dd-afbb-5323542c900c', NULL, NULL, CAST(N'2021-10-24T07:10:24.207' AS DateTime), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[ArtistProject] ([ArtistId], [ProjectId], [Rate], [Comment], [InvitedDate], [RequestedDate], [JoinedDate], [CanceledDate], [FinishedDate], [ReviewDate], [Status]) VALUES (N'84ff7a5e-a5d4-4c04-bb0e-2fd3248745df', N'0c6e948c-7f22-4e5d-99b4-68de8b4a00f1', NULL, NULL, NULL, NULL, CAST(N'2021-10-26T07:23:25.833' AS DateTime), NULL, NULL, NULL, 2)
INSERT [dbo].[ArtistProject] ([ArtistId], [ProjectId], [Rate], [Comment], [InvitedDate], [RequestedDate], [JoinedDate], [CanceledDate], [FinishedDate], [ReviewDate], [Status]) VALUES (N'84ff7a5e-a5d4-4c04-bb0e-2fd3248745df', N'93846e4f-e99b-4fb4-839a-7e200f594c8a', NULL, NULL, NULL, NULL, CAST(N'2021-10-26T07:38:08.867' AS DateTime), NULL, NULL, NULL, 2)
INSERT [dbo].[ArtistProject] ([ArtistId], [ProjectId], [Rate], [Comment], [InvitedDate], [RequestedDate], [JoinedDate], [CanceledDate], [FinishedDate], [ReviewDate], [Status]) VALUES (N'84ff7a5e-a5d4-4c04-bb0e-2fd3248745df', N'770de6ef-487e-4435-9a01-f7d87a4e9322', NULL, NULL, NULL, NULL, CAST(N'2021-10-26T07:59:51.623' AS DateTime), NULL, NULL, NULL, 2)
INSERT [dbo].[ArtistProject] ([ArtistId], [ProjectId], [Rate], [Comment], [InvitedDate], [RequestedDate], [JoinedDate], [CanceledDate], [FinishedDate], [ReviewDate], [Status]) VALUES (N'21ac1f58-b824-475b-902f-f29cc74b0d39', N'807f39a2-5a12-4295-abdf-232f984f7c4b', NULL, NULL, CAST(N'2021-10-18T00:00:00.000' AS DateTime), NULL, CAST(N'2021-10-19T00:00:00.000' AS DateTime), NULL, CAST(N'2021-10-23T00:00:00.000' AS DateTime), NULL, 2)
INSERT [dbo].[ArtistProject] ([ArtistId], [ProjectId], [Rate], [Comment], [InvitedDate], [RequestedDate], [JoinedDate], [CanceledDate], [FinishedDate], [ReviewDate], [Status]) VALUES (N'21ac1f58-b824-475b-902f-f29cc74b0d39', N'0c6e948c-7f22-4e5d-99b4-68de8b4a00f1', 5, N'Good', NULL, NULL, NULL, NULL, CAST(N'2021-10-26T06:34:40.067' AS DateTime), CAST(N'2021-10-26T08:39:06.773' AS DateTime), 4)
INSERT [dbo].[ArtistProject] ([ArtistId], [ProjectId], [Rate], [Comment], [InvitedDate], [RequestedDate], [JoinedDate], [CanceledDate], [FinishedDate], [ReviewDate], [Status]) VALUES (N'21ac1f58-b824-475b-902f-f29cc74b0d39', N'f32884c4-f453-4b93-a89e-d50e47815c27', NULL, NULL, NULL, NULL, CAST(N'2021-10-28T07:24:55.687' AS DateTime), NULL, NULL, NULL, 2)
GO
INSERT [dbo].[ArtistProjectFile] ([Id], [Name], [Url], [ProjectId], [MediaType], [Status], [CreateBy], [Description], [CreateDate], [Comment]) VALUES (N'22692093-8a97-42f8-8460-0227c9f47f4c', N'mp3-output-ttsfree(dot)com', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/audios%2Fmp3-output-ttsfree(dot)com?alt=media&token=27325326-c65d-48bb-ab01-883300da8a84', N'0c6e948c-7f22-4e5d-99b4-68de8b4a00f1', 0, 0, N'21ac1f58-b824-475b-902f-f29cc74b0d39', N'asd', CAST(N'2021-10-26T06:48:44.687' AS DateTime), N'asd')
INSERT [dbo].[ArtistProjectFile] ([Id], [Name], [Url], [ProjectId], [MediaType], [Status], [CreateBy], [Description], [CreateDate], [Comment]) VALUES (N'597739bd-3432-42b5-acc7-65824764e1f9', N'mp3-output-ttsfree(dot)com', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/audios%2Fmp3-output-ttsfree(dot)com?alt=media&token=e628c517-6b99-4d22-a152-a51e17732e17', N'770de6ef-487e-4435-9a01-f7d87a4e9322', 0, 1, N'21ac1f58-b824-475b-902f-f29cc74b0d39', N'qwe', CAST(N'2021-10-23T04:20:48.833' AS DateTime), N'dasd')
INSERT [dbo].[ArtistProjectFile] ([Id], [Name], [Url], [ProjectId], [MediaType], [Status], [CreateBy], [Description], [CreateDate], [Comment]) VALUES (N'2138294e-57ee-442a-8c54-9275ec574547', N'script.mp3', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/audios%2Fscript.mp3?alt=media&token=31a03aaf-d211-4bce-8b75-8bd61318ce0e', N'770de6ef-487e-4435-9a01-f7d87a4e9322', 0, 2, N'80063860-ce9b-475b-b5df-19a6f651cbdf', N'asd', CAST(N'2021-10-26T11:33:24.137' AS DateTime), N'An noi mat day')
INSERT [dbo].[ArtistProjectFile] ([Id], [Name], [Url], [ProjectId], [MediaType], [Status], [CreateBy], [Description], [CreateDate], [Comment]) VALUES (N'5891ce94-2e04-4b65-bf83-c38dbbc6aa5e', N'script.mp3', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/audios%2Fscript.mp3?alt=media&token=8cbf65bf-b80a-45ca-b7cf-e916da33ec7e', N'f32884c4-f453-4b93-a89e-d50e47815c27', 0, 0, N'21ac1f58-b824-475b-902f-f29cc74b0d39', N'wqe', CAST(N'2021-10-28T07:25:52.817' AS DateTime), N'dsadsdc')
INSERT [dbo].[ArtistProjectFile] ([Id], [Name], [Url], [ProjectId], [MediaType], [Status], [CreateBy], [Description], [CreateDate], [Comment]) VALUES (N'b88d435c-9f29-44b1-b3e2-e59b5d903a20', N'mp3-output-ttsfree(dot)com', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/audios%2Fmp3-output-ttsfree(dot)com?alt=media&token=6415ce13-76f4-4a88-849d-432662061059', N'807f39a2-5a12-4295-abdf-232f984f7c4b', 0, 0, N'21ac1f58-b824-475b-902f-f29cc74b0d39', N'asd', CAST(N'2021-10-26T05:32:28.180' AS DateTime), N'xzc')
INSERT [dbo].[ArtistProjectFile] ([Id], [Name], [Url], [ProjectId], [MediaType], [Status], [CreateBy], [Description], [CreateDate], [Comment]) VALUES (N'08d8514c-2d75-4136-9947-f1ed18b98d98', N'script.mp3', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/audios%2Fscript.mp3?alt=media&token=e6ac69c5-e9a8-4f2e-82ad-225aba6799ff', N'770de6ef-487e-4435-9a01-f7d87a4e9322', 0, 0, N'80063860-ce9b-475b-b5df-19a6f651cbdf', N'qwe', CAST(N'2021-10-26T11:38:23.817' AS DateTime), NULL)
INSERT [dbo].[ArtistProjectFile] ([Id], [Name], [Url], [ProjectId], [MediaType], [Status], [CreateBy], [Description], [CreateDate], [Comment]) VALUES (N'6de43b90-6f83-461a-9f2f-f8b37c0eb7d9', N'script.mp3', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/audios%2Fscript.mp3?alt=media&token=42eeb2d0-c465-4882-a885-50a7794f6ae8', N'975b3044-7ea2-49dd-afbb-5323542c900c', 0, 0, N'80063860-ce9b-475b-b5df-19a6f651cbdf', N'asd', CAST(N'2021-10-26T10:27:19.063' AS DateTime), NULL)
GO
INSERT [dbo].[ArtistToken] ([ArtistId], [Token], [Id]) VALUES (N'21ac1f58-b824-475b-902f-f29cc74b0d39', N'c2VW6FQcSKOYRpQ4uWRqra:APA91bGgAdJn-pAOKge0QmLa7oDtdb0QxHcX-WqnbiC6-lbyaGey1ymBtnVYEu2PCTWzbQaqqpaiYF3bDLLc_q3yMIEUM8mh0wKEqUUNilCI41LCWZGXUo6tBrgRNV6Ty87d6O-OG0iG', N'3aa65009-94dd-4d6c-863d-1204b7e35a21')
INSERT [dbo].[ArtistToken] ([ArtistId], [Token], [Id]) VALUES (N'80063860-ce9b-475b-b5df-19a6f651cbdf', N'dgIySbKXRcWOMIv0kAcAAU:APA91bFbdw2s1Js5ZV9OyP5dCE9wMpxY6CNL-4Mb8Er01IvaN60W6mI1WL2-m3RHVbJdncT0gUztFrgvMJRrADROPSRF5J0iU4ZvUrkvAdEY98scoYd5PUkY3FtjbicWkeeQkoWv5VA_', N'7fbdb06d-3f51-43e0-92cd-1365e1b00d14')
INSERT [dbo].[ArtistToken] ([ArtistId], [Token], [Id]) VALUES (N'21ac1f58-b824-475b-902f-f29cc74b0d39', N'cNNRZqAfRKan5ts8D5t3T-:APA91bGftoYSOSde5karQfc47EI3haISMVXELF2y7JlIo7tgw2s6a5M4g5aG1xK3ks0ly4w454s07W66hO--vUw9L4fXS2KmYNUCZ-SrUz7IvYSTY2hmOjX93Qnq2r0Ar3xpObGLJzBl', N'307f2d40-8f9e-47e0-9252-3a81e2737401')
INSERT [dbo].[ArtistToken] ([ArtistId], [Token], [Id]) VALUES (N'21ac1f58-b824-475b-902f-f29cc74b0d39', N'er7z7-XTTo6RR6gtRVvE74:APA91bGpTf_F_oRWISYoDRR6IjQJRTo2HRMQcC5Z7mHyWmpdQEziIT6DD1Pc4VBg06tIxksews6-i1QJ1txloaEjsE5mNZzvElbczYmahip1es781cTcMvmGHCkgMoFsPaOsfWtFKAx3', N'e42a6519-e9ee-4d0d-8246-45a04c3fe02a')
INSERT [dbo].[ArtistToken] ([ArtistId], [Token], [Id]) VALUES (N'80063860-ce9b-475b-b5df-19a6f651cbdf', N'euIXUxv4Q1q1pnxj5BBHqh:APA91bFU8LZDoueayXP2akprDPTvAHyLfc0U5zCrcFEVT1gYvqlTpNX0zHfvgodK_QfVIKdgax2FVBcw9VYqv2VnBcnZQWFAZAllwvZbCwaPzbDif3DsCfN9-daClHxdEhG3PFB0ULSC', N'd56435ef-d741-4608-9c7d-49f84b181541')
INSERT [dbo].[ArtistToken] ([ArtistId], [Token], [Id]) VALUES (N'21ac1f58-b824-475b-902f-f29cc74b0d39', N'cZv3TE66QXi4DV90Fb3H8a:APA91bF4AM6jjjuF0o7Um0UXhpSdIH2dMbp2iExC1TExCk-LBl63fJePSBZY54q5ystgto40pFTBrR_mv_LsMHoG_tutMaAU0KPV-SRK-MdDuDJvfRJzJydWxYcejcv7oYFTu3A8FkqL', N'0f005423-2b6e-4b3b-b81b-4a82debe06f6')
INSERT [dbo].[ArtistToken] ([ArtistId], [Token], [Id]) VALUES (N'21ac1f58-b824-475b-902f-f29cc74b0d39', N'efCi5oJhTmulFWRfMGCHSB:APA91bE07ffbFUiWBQFU1H7Z49r0mCNyiUzPJc3FGk6S5IwhBokz8aHz7iqrWxfiKODhcpJp8jS5Abpi_6zKgWihm8xr8LAS9fqQ_nu6YV1oNDeQypHkLNyF3UdWHIZn2OI-0C2szp4d', N'86085228-ec39-4bec-aae9-8fa750de31b3')
INSERT [dbo].[ArtistToken] ([ArtistId], [Token], [Id]) VALUES (N'80063860-ce9b-475b-b5df-19a6f651cbdf', N'dDs2aDc8TomXWxo0I7m0dE:APA91bH1hgFnZMvVuQvgFUFyjwaBS7lBivyh0wwENcBbCcfNkZuHCIoWYjwxo8UauQdd0mCE8ZKeYX5Cd6i3SQksN2hCErgx_Hg3C3fn_McGBA3A0Net8KXIHAnMf_UjaBbPvB_-tasc', N'eb0ca450-c752-4d7e-86a0-e476e511016f')
INSERT [dbo].[ArtistToken] ([ArtistId], [Token], [Id]) VALUES (N'80063860-ce9b-475b-b5df-19a6f651cbdf', N'cZIE3zSIQTS_hxRzXdsGQn:APA91bFgEDsDrNyUKVDaqcmPNy05-4b2gY7rLlBVv3MTcGHs5YP33_Fs9xyH1P7L4gdcbaW9WGphY-4hn_-T0__KzkdfUcl7M9siSs7FmrbRQ2FlcY-JeG-WwCuDbNEb62jRKmDl9mM1', N'caddd7e1-e659-4435-a623-f1490023385d')
INSERT [dbo].[ArtistToken] ([ArtistId], [Token], [Id]) VALUES (N'80063860-ce9b-475b-b5df-19a6f651cbdf', N'ej6xrQskRY-sd5_8XGcGCt:APA91bF5EE7Ge2nRPJU22KvelzS8DMzlC-e_7zdpQyBEPBZ5KqypXEoiVJgZBWkCoOJ_FVgUFILZqfqh80K4g_c8nWPA6T1B6VgBU3tkoVQOs3zBZJoxhaS_QZXZ9fOYpoPYoqU7oNFm', N'ab8d2fda-443b-4628-af85-fdb3ccc7c3e0')
GO
INSERT [dbo].[ArtistVoiceDemo] ([ArtistId], [VoiceDemoId]) VALUES (N'80063860-ce9b-475b-b5df-19a6f651cbdf', N'2947801f-9f02-4ade-be25-4aed127bf721')
INSERT [dbo].[ArtistVoiceDemo] ([ArtistId], [VoiceDemoId]) VALUES (N'80063860-ce9b-475b-b5df-19a6f651cbdf', N'9541c728-c68d-4215-ac05-a20799aba561')
INSERT [dbo].[ArtistVoiceDemo] ([ArtistId], [VoiceDemoId]) VALUES (N'84ff7a5e-a5d4-4c04-bb0e-2fd3248745df', N'8628ad6e-33fe-4f80-8bda-a97f3cf2d544')
INSERT [dbo].[ArtistVoiceDemo] ([ArtistId], [VoiceDemoId]) VALUES (N'84ff7a5e-a5d4-4c04-bb0e-2fd3248745df', N'f9f3868c-118c-4c7a-8a34-c95da16e7f12')
INSERT [dbo].[ArtistVoiceDemo] ([ArtistId], [VoiceDemoId]) VALUES (N'f4a65019-94ed-40f6-86fd-aec24b3dc31e', N'42d4e904-83f8-4906-8d95-301d05aa85b8')
INSERT [dbo].[ArtistVoiceDemo] ([ArtistId], [VoiceDemoId]) VALUES (N'f4a65019-94ed-40f6-86fd-aec24b3dc31e', N'a6ee5f6d-1f03-41e9-92f4-3a830572940d')
INSERT [dbo].[ArtistVoiceDemo] ([ArtistId], [VoiceDemoId]) VALUES (N'37035726-a496-4899-98fd-bd190bf0f979', N'18dc0ae1-17e9-4d88-b064-15e61c0619a2')
INSERT [dbo].[ArtistVoiceDemo] ([ArtistId], [VoiceDemoId]) VALUES (N'37035726-a496-4899-98fd-bd190bf0f979', N'234a5da4-3572-4063-893f-2d670a0d74b1')
INSERT [dbo].[ArtistVoiceDemo] ([ArtistId], [VoiceDemoId]) VALUES (N'37035726-a496-4899-98fd-bd190bf0f979', N'08b9c89a-ac42-4f39-801c-dd1ab01e3e52')
INSERT [dbo].[ArtistVoiceDemo] ([ArtistId], [VoiceDemoId]) VALUES (N'21ac1f58-b824-475b-902f-f29cc74b0d39', N'1421c411-618c-4b3b-8b3e-420aa324c6f3')
INSERT [dbo].[ArtistVoiceDemo] ([ArtistId], [VoiceDemoId]) VALUES (N'21ac1f58-b824-475b-902f-f29cc74b0d39', N'248829c6-8f84-4092-b0fd-c56f56029df5')
GO
INSERT [dbo].[ArtistVoiceStyle] ([ArtistId], [VoiceStyleId]) VALUES (N'80063860-ce9b-475b-b5df-19a6f651cbdf', N'f725d621-6e73-4492-a0d9-1664b8733781')
INSERT [dbo].[ArtistVoiceStyle] ([ArtistId], [VoiceStyleId]) VALUES (N'80063860-ce9b-475b-b5df-19a6f651cbdf', N'79cd1e1c-b5b7-4cb3-af1b-6cfb3cf96567')
INSERT [dbo].[ArtistVoiceStyle] ([ArtistId], [VoiceStyleId]) VALUES (N'84ff7a5e-a5d4-4c04-bb0e-2fd3248745df', N'1609d173-3987-4ba0-9bec-30f0fdc69128')
INSERT [dbo].[ArtistVoiceStyle] ([ArtistId], [VoiceStyleId]) VALUES (N'84ff7a5e-a5d4-4c04-bb0e-2fd3248745df', N'1fa6e893-ced3-4c3a-9005-9df45a0b22e8')
INSERT [dbo].[ArtistVoiceStyle] ([ArtistId], [VoiceStyleId]) VALUES (N'f4a65019-94ed-40f6-86fd-aec24b3dc31e', N'f725d621-6e73-4492-a0d9-1664b8733781')
INSERT [dbo].[ArtistVoiceStyle] ([ArtistId], [VoiceStyleId]) VALUES (N'f4a65019-94ed-40f6-86fd-aec24b3dc31e', N'1609d173-3987-4ba0-9bec-30f0fdc69128')
INSERT [dbo].[ArtistVoiceStyle] ([ArtistId], [VoiceStyleId]) VALUES (N'f4a65019-94ed-40f6-86fd-aec24b3dc31e', N'5d5ce036-117b-4d62-b807-5061861d0b90')
INSERT [dbo].[ArtistVoiceStyle] ([ArtistId], [VoiceStyleId]) VALUES (N'37035726-a496-4899-98fd-bd190bf0f979', N'e9f4e04a-d382-4e5b-9aff-3ea7645341cc')
INSERT [dbo].[ArtistVoiceStyle] ([ArtistId], [VoiceStyleId]) VALUES (N'37035726-a496-4899-98fd-bd190bf0f979', N'79cd1e1c-b5b7-4cb3-af1b-6cfb3cf96567')
INSERT [dbo].[ArtistVoiceStyle] ([ArtistId], [VoiceStyleId]) VALUES (N'37035726-a496-4899-98fd-bd190bf0f979', N'175c69a2-94f1-4783-8636-b13b7d110312')
INSERT [dbo].[ArtistVoiceStyle] ([ArtistId], [VoiceStyleId]) VALUES (N'21ac1f58-b824-475b-902f-f29cc74b0d39', N'e9f4e04a-d382-4e5b-9aff-3ea7645341cc')
INSERT [dbo].[ArtistVoiceStyle] ([ArtistId], [VoiceStyleId]) VALUES (N'21ac1f58-b824-475b-902f-f29cc74b0d39', N'5d5ce036-117b-4d62-b807-5061861d0b90')
GO
INSERT [dbo].[Country] ([Id], [Name]) VALUES (N'e50a231b-1596-48eb-a1ce-1095fb6aa63b', N'Miền Nam')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (N'2179f408-b174-465d-8126-1562b04f12ad', N'Miền Tây')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (N'd6dfce11-bd2f-4654-b24e-2a5df2e07cf0', N'Miền Trung')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (N'7cbe503c-c8a0-4596-93aa-3c9fb7366c33', N'Miền Bắc')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (N'48a00103-ebce-48e3-a012-9b329c8e7cdc', N'Miền Đông')
GO
INSERT [dbo].[Customer] ([Id], [Username], [Email], [Phone], [Password], [FirstName], [LastName], [Role], [Gender], [Avatar], [Status], [LastLoginTime], [CreateDate], [UpdateDate], [UpdateBy]) VALUES (N'3cd1fd5b-6a17-42be-b583-1d954c985160', N'ushionguyen', N'benny20001456@gmail.com', N'0912345678', N'12345678', N'Bách', N'Nguyen', 2, N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf', N'https://scontent.fsgn4-1.fna.fbcdn.net/v/t1.6435-9/140035352_2948508642095777_7110570318890363555_n.jpg?_nc_cat=101&ccb=1-5&_nc_sid=09cbfe&_nc_ohc=WMuFVVCQkVMAX8ysNOK&_nc_ht=scontent.fsgn4-1.fna&oh=6df5db0347faa4e99d5a025d66f6dfe0&oe=618CF6FB', 0, NULL, CAST(N'2021-10-02T07:36:57.910' AS DateTime), CAST(N'2021-10-23T07:31:00.873' AS DateTime), N'596cf15f-40c7-4b24-8c48-364ff0713356')
INSERT [dbo].[Customer] ([Id], [Username], [Email], [Phone], [Password], [FirstName], [LastName], [Role], [Gender], [Avatar], [Status], [LastLoginTime], [CreateDate], [UpdateDate], [UpdateBy]) VALUES (N'e8cfdc9a-4a77-49b4-8e33-3c5320704ee5', N'tai1000kg', N'tai1000kg@gmail.com', N'0945678912', N'12345678', N'Tai', N'Vo', 2, N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf', N'', 0, NULL, CAST(N'2021-10-28T14:04:57.047' AS DateTime), NULL, NULL)
INSERT [dbo].[Customer] ([Id], [Username], [Email], [Phone], [Password], [FirstName], [LastName], [Role], [Gender], [Avatar], [Status], [LastLoginTime], [CreateDate], [UpdateDate], [UpdateBy]) VALUES (N'80f4ae2a-6b85-44b1-945d-3ff4d4c03b50', N'Nubakachi', N'nuhuhu@gm.vl', N'0312456789', N'12345678', N'Nam', N'Nguyễn', 2, N'4461e740-e465-4b4f-927f-3a4f4a3e8cb7', N'https://images.unsplash.com/photo-1608889825205-eebdb9fc5806?ixid=MnwxMjA3fDB8MHxzZWFyY2h8MTR8fGF2YXRhcnxlbnwwfHwwfHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=500&q=60', 0, NULL, CAST(N'2021-10-04T13:20:24.250' AS DateTime), CAST(N'2021-10-28T13:45:19.137' AS DateTime), N'596cf15f-40c7-4b24-8c48-364ff0713356')
INSERT [dbo].[Customer] ([Id], [Username], [Email], [Phone], [Password], [FirstName], [LastName], [Role], [Gender], [Avatar], [Status], [LastLoginTime], [CreateDate], [UpdateDate], [UpdateBy]) VALUES (N'2ab97db1-3417-40b3-b819-862cafd3aee7', N'baoduong', N'ledachoangphuc249@gmail.com', N'0978765352', N'123456789', N'Dương Đinh Quốc', N'Bảo', 2, N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf', N'https://images.unsplash.com/photo-1623582854588-d60de57fa33f?ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mjl8fGF2YXRhcnxlbnwwfHwwfHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=500&q=60', 0, NULL, CAST(N'2021-10-14T12:19:26.183' AS DateTime), CAST(N'2021-10-22T15:41:30.130' AS DateTime), NULL)
INSERT [dbo].[Customer] ([Id], [Username], [Email], [Phone], [Password], [FirstName], [LastName], [Role], [Gender], [Avatar], [Status], [LastLoginTime], [CreateDate], [UpdateDate], [UpdateBy]) VALUES (N'b96959b4-6a70-4f56-b30f-8a2993e7e450', N'huunghi266', N'mhnghi266@gmail.com', N'0945587887', N'266maihuunghi', N'Nghị', N'Mai', 2, N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf', N'https://images.unsplash.com/photo-1558898479-33c0057a5d12?ixid=MnwxMjA3fDB8MHxzZWFyY2h8MTB8fGF2YXRhcnxlbnwwfHwwfHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=500&q=60', 0, NULL, CAST(N'2021-10-15T10:38:26.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Customer] ([Id], [Username], [Email], [Phone], [Password], [FirstName], [LastName], [Role], [Gender], [Avatar], [Status], [LastLoginTime], [CreateDate], [UpdateDate], [UpdateBy]) VALUES (N'a8aacaef-19ea-40fb-bf6b-a7960e470aac', N'trongnsiwa79', N'trongnsiwa79@gmail.com', N'0977158941', N'12345678', N'Trọng', N'Nguyễn', 2, N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf', N'https://scontent.fsgn13-2.fna.fbcdn.net/v/t1.6435-9/148556149_1345319329155493_4508354124037893429_n.jpg?_nc_cat=106&ccb=1-5&_nc_sid=8bfeb9&_nc_ohc=4nGQmnhZkLkAX8V5mYE&_nc_ht=scontent.fsgn13-2.fna&oh=6aa138926fdbc946ca86c2e9fa186eeb&oe=618BE08C', 0, NULL, CAST(N'2021-10-07T07:03:42.707' AS DateTime), NULL, NULL)
INSERT [dbo].[Customer] ([Id], [Username], [Email], [Phone], [Password], [FirstName], [LastName], [Role], [Gender], [Avatar], [Status], [LastLoginTime], [CreateDate], [UpdateDate], [UpdateBy]) VALUES (N'118562e6-7710-415f-8339-ad880289af19', N'huyle', N'huyl833@gmail.com', N'0358174147', N'12345678', N'Huy', N'Lê', 2, N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf', N'https://scontent.fsgn13-2.fna.fbcdn.net/v/t1.6435-9/235033582_3005525223066372_842936809344864497_n.jpg?_nc_cat=106&ccb=1-5&_nc_sid=8bfeb9&_nc_ohc=eRSnA2i4-gUAX8p-UVF&tn=HhtuBGdS4ErJgG95&_nc_ht=scontent.fsgn13-2.fna&oh=77507258d5ecd3f485c6d5e40edbc7ae&oe=618B12EF', 0, NULL, CAST(N'2021-10-16T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Customer] ([Id], [Username], [Email], [Phone], [Password], [FirstName], [LastName], [Role], [Gender], [Avatar], [Status], [LastLoginTime], [CreateDate], [UpdateDate], [UpdateBy]) VALUES (N'297c0bb3-6f92-4ba0-8354-e6eb0478fe86', N'votantai4899', N'string@gmail.com', N'0321225512', N'tantai4899', N'Tài', N'Võ', 2, N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf', N'https://scontent.fsgn3-1.fna.fbcdn.net/v/t1.6435-9/205488925_1121488188373233_1304544003081571317_n.jpg?_nc_cat=104&ccb=1-5&_nc_sid=09cbfe&_nc_ohc=PGarY6p5zv4AX9GBuFV&_nc_oc=AQlXHquBAy2oobtLATBaclH85TLowcBUk7vVCyLr3VqMaJ1H7dJV8oP_vHl7p499wNk&_nc_ht=scontent.fsgn3-1.fna&oh=3eadae615ad972addac8b30e88b9048d&oe=618E6E52', 0, NULL, CAST(N'2021-10-02T07:29:13.520' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[CustomerProjectFile] ([Id], [Name], [Url], [ProjectId], [MediaType], [Status], [CreateBy], [CreateDate], [Description], [Comment]) VALUES (N'466d4a0c-a1cd-4dd1-adb4-0d07e464c5f6', N'script.txt', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/files%2Fscript.txt?alt=media&token=895610c7-bd00-46d0-a77e-13d2cee17e5a', N'93846e4f-e99b-4fb4-839a-7e200f594c8a', 1, 0, N'3cd1fd5b-6a17-42be-b583-1d954c985160', CAST(N'2021-10-26T11:39:08.403' AS DateTime), N'ad', NULL)
INSERT [dbo].[CustomerProjectFile] ([Id], [Name], [Url], [ProjectId], [MediaType], [Status], [CreateBy], [CreateDate], [Description], [Comment]) VALUES (N'41add88a-7aab-4c7d-8a0a-2fa25023f91b', N'script.txt', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/files%2Fscript.txt?alt=media&token=d0e73d3f-0a21-4c7f-8198-bd117788f7e8', N'0c6e948c-7f22-4e5d-99b4-68de8b4a00f1', 1, 0, N'3cd1fd5b-6a17-42be-b583-1d954c985160', CAST(N'2021-10-26T08:56:41.410' AS DateTime), N'asd', NULL)
INSERT [dbo].[CustomerProjectFile] ([Id], [Name], [Url], [ProjectId], [MediaType], [Status], [CreateBy], [CreateDate], [Description], [Comment]) VALUES (N'9f611747-c0b3-47f0-8c84-8aab5ff6a33e', N'script.txt', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/files%2Fscript.txt?alt=media&token=9071b3fe-45f4-4741-a232-15c7f919bd6c', N'0c6e948c-7f22-4e5d-99b4-68de8b4a00f1', 1, 0, N'3cd1fd5b-6a17-42be-b583-1d954c985160', CAST(N'2021-10-26T10:18:12.330' AS DateTime), N'asd', NULL)
INSERT [dbo].[CustomerProjectFile] ([Id], [Name], [Url], [ProjectId], [MediaType], [Status], [CreateBy], [CreateDate], [Description], [Comment]) VALUES (N'1b0f0483-30b1-4aa1-912f-93a4be8b555d', N'script.txt', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/files%2Fscript.txt?alt=media&token=61417015-29ab-413d-b7d6-afd5e767c38d', N'807f39a2-5a12-4295-abdf-232f984f7c4b', 1, 0, N'a8aacaef-19ea-40fb-bf6b-a7960e470aac', CAST(N'2021-10-26T04:00:43.303' AS DateTime), N'ad', NULL)
INSERT [dbo].[CustomerProjectFile] ([Id], [Name], [Url], [ProjectId], [MediaType], [Status], [CreateBy], [CreateDate], [Description], [Comment]) VALUES (N'5fb89996-bda4-4a7f-9fb1-a411eade193a', N'script.txt', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/files%2Fscript.txt?alt=media&token=7ab324f6-449a-42da-8716-d4cdaeeacbab', N'975b3044-7ea2-49dd-afbb-5323542c900c', 1, 0, N'3cd1fd5b-6a17-42be-b583-1d954c985160', CAST(N'2021-10-26T10:26:44.470' AS DateTime), N'asd', NULL)
GO
INSERT [dbo].[CustomerToken] ([Id], [CustomerId], [Token]) VALUES (N'a4683a81-1ddc-47c1-b213-1d776da96d00', N'3cd1fd5b-6a17-42be-b583-1d954c985160', N'dXnkt4EjSrmo-Lu4Uy_Pq7:APA91bF_Ci89RK6iaO_EQpqKJW-NbDo7bcumZyLZYqwkn4OguA1ZGwSzV43FUj8OyBdDsNXdB2YPjdiZHV7z0Wf0NhQP2ykmlPqyqMl5JZmxI3Ja8q826JxPhnZmc_odvUBrbS1YmXa-')
INSERT [dbo].[CustomerToken] ([Id], [CustomerId], [Token]) VALUES (N'1009923b-c2e1-49ba-9889-214d1e36d7ff', N'3cd1fd5b-6a17-42be-b583-1d954c985160', N'fpGrMWxCSYKj6-b0I3tG8M:APA91bHv9FhfNyGB6QpR9tinVS3_YDoqKR3bF849GOc74aXSGCXRlsQfkz_rpWcklKS-8QNQy2eD4G84F85E8t3CuNXhi64w33F5eqp5tbDO7S8agMGzsqoeMqszV2UatMwTxpUQIXoI')
INSERT [dbo].[CustomerToken] ([Id], [CustomerId], [Token]) VALUES (N'42e7d840-947d-4bc0-b16b-269a05154798', N'3cd1fd5b-6a17-42be-b583-1d954c985160', N'cZv3TE66QXi4DV90Fb3H8a:APA91bF4AM6jjjuF0o7Um0UXhpSdIH2dMbp2iExC1TExCk-LBl63fJePSBZY54q5ystgto40pFTBrR_mv_LsMHoG_tutMaAU0KPV-SRK-MdDuDJvfRJzJydWxYcejcv7oYFTu3A8FkqL')
INSERT [dbo].[CustomerToken] ([Id], [CustomerId], [Token]) VALUES (N'9a11abfa-7ba6-4d51-a0dc-2c09a68895a1', N'3cd1fd5b-6a17-42be-b583-1d954c985160', N'dbU05DU4Rl-vN-H_VsA6jE:APA91bGfnXJjl5pZ1MQzKFwfrpvm2zEUJXQAlCqIsTUKzBq_bv5_9iSfGTYB1JgGcHKLWbQOqp200C7ItkzO_Oy8Mvk7nj_94BmXgUp539Y5bTCf8gZAP3xJFHtktNjmpPAC4tPRn25d')
INSERT [dbo].[CustomerToken] ([Id], [CustomerId], [Token]) VALUES (N'54ea98d3-0f4f-47bd-b0a8-3217b61aee03', N'3cd1fd5b-6a17-42be-b583-1d954c985160', N'fhrpIxnaScmBCAall4O7kq:APA91bFYm375wWWbL84XL6yXJvik4-1iHdis7-p-fQaiFqom_UFTzVVYrchLso8lwhYxF2NHvtkxPg9kw1PAgoRypB8gFDt3Zbf2348oJkr2i5wRXxI-PXvEEw3RugaA78acQ_AUjauy')
INSERT [dbo].[CustomerToken] ([Id], [CustomerId], [Token]) VALUES (N'088cc5f3-fc84-4cd3-8454-4303f1fe9638', N'3cd1fd5b-6a17-42be-b583-1d954c985160', N'ctt4mE80TsqbGBsEFqJ1I6:APA91bGMko1WJxmQW_wEf3cJp0vJpt91yq5TwkKZo6u15rwS9LrzihJ2usx3x69O4orXFa-3ekrnM0eutNFq7D8hTCjvYNuLbz2gNfJp0AXUZngL666YeGwIkXoGpf4zinsbLVqfa21u')
INSERT [dbo].[CustomerToken] ([Id], [CustomerId], [Token]) VALUES (N'f31a36ce-fb43-4292-84e3-668b27fbcc93', N'3cd1fd5b-6a17-42be-b583-1d954c985160', N'cNNRZqAfRKan5ts8D5t3T-:APA91bGftoYSOSde5karQfc47EI3haISMVXELF2y7JlIo7tgw2s6a5M4g5aG1xK3ks0ly4w454s07W66hO--vUw9L4fXS2KmYNUCZ-SrUz7IvYSTY2hmOjX93Qnq2r0Ar3xpObGLJzBl')
INSERT [dbo].[CustomerToken] ([Id], [CustomerId], [Token]) VALUES (N'56e2bf98-97d5-46de-ac1e-68b54dd067af', N'3cd1fd5b-6a17-42be-b583-1d954c985160', N'eGdUhWSLQbGEmkdmjYgAmI:APA91bHUzpbfwgS9oma2RWU8s2wgSIOvlv1D2zuJv3Cl4omh08lhKNYmm-cTrcFyaR9o6EVvTHbuxYp3u2j8W0BZjEQJ_pooZLt0EifYRvuI6gnmHbJU65Te10pLrtlmIvDjMZ48uol2')
INSERT [dbo].[CustomerToken] ([Id], [CustomerId], [Token]) VALUES (N'db917b85-3e2f-42d2-8919-9aad5c260b65', N'3cd1fd5b-6a17-42be-b583-1d954c985160', N'e8ug_8DdTFu56MI1W_DZVm:APA91bG5tnWdwOXJawBWaGyTEiy5_6boZrklIPhBtUU-I2RtmzlppBDOY-HYzhIdUNMQkMJDkPeZNFgmlOsb89RnORJLZ6gRCQCLF8OVnPhyuucQsw3eousq4-O7wdyaer1L7VAWKGLb')
INSERT [dbo].[CustomerToken] ([Id], [CustomerId], [Token]) VALUES (N'904e0875-ce44-4b08-947d-ac2c0a47b5b3', N'3cd1fd5b-6a17-42be-b583-1d954c985160', N'fqaG0sTVROuQPoaQGE6gUo:APA91bH9gom_4uNVqMduC6cI9JR7kK_fDDUWPf78jZvGQaEHKfQKyuDmbAQBaJYgNQlutM4ylOD7k5hXybf4R2UVE2dg-eipoFql_rvYm22l9kNBaJLNvAEDD0aUzWeK-PS1wIpXSaAB')
INSERT [dbo].[CustomerToken] ([Id], [CustomerId], [Token]) VALUES (N'd748c6de-ffc2-4d03-b6bc-afe8e96a5052', N'b96959b4-6a70-4f56-b30f-8a2993e7e450', N'c1CEISb6RfuCo0aZLBUo2U:APA91bGI7aU4zdlEEWKQg3sgkY2TsXOrxDuUvfBY1LpmHX0DQz_82XZRuKWjoTh8OlGF5ouPRw08d97RsS95NTQK1IdDBvjUl0CcybHanDcyZIuAzagsk_gOGVMxA18tn7BExnAI-Ogk')
INSERT [dbo].[CustomerToken] ([Id], [CustomerId], [Token]) VALUES (N'efd9330a-fc71-46aa-a678-b813c3851e15', N'3cd1fd5b-6a17-42be-b583-1d954c985160', N'dNZ_8lYMS52AvSbTW0EI0n:APA91bHhJ00XjZD_hh3jgrBLW6ectbWWBcWUbExmckEydWiF-nF2bV0DQknGmkdpSWdWA0_Ah6AMcmqDzpXFLCpA-TsbwywnfqSD_hpt7F55ptmuA6thNiBa3elYo1Tmp7J84NB4VKXv')
INSERT [dbo].[CustomerToken] ([Id], [CustomerId], [Token]) VALUES (N'17828bb8-a150-47a6-93dc-d3b78b81b639', N'3cd1fd5b-6a17-42be-b583-1d954c985160', N'er7z7-XTTo6RR6gtRVvE74:APA91bGpTf_F_oRWISYoDRR6IjQJRTo2HRMQcC5Z7mHyWmpdQEziIT6DD1Pc4VBg06tIxksews6-i1QJ1txloaEjsE5mNZzvElbczYmahip1es781cTcMvmGHCkgMoFsPaOsfWtFKAx3')
INSERT [dbo].[CustomerToken] ([Id], [CustomerId], [Token]) VALUES (N'ca58864f-ba08-4cbf-8ec0-e37bff327dd2', N'3cd1fd5b-6a17-42be-b583-1d954c985160', N'cVMdyiZ6TAmtPRNoSXj_1s:APA91bH-kNJlDLbkDQzdBp2i_EuMXprMFmHmhaKD4e-cLQqvEsdzgz2i8exLFFHLSuLMjkaLgRFTIXb3nw2AWAtZam6zbvEfR1eWgPeag4jau46ENKnlwEqS_6D_ubwYnc0rZRWqwq2x')
INSERT [dbo].[CustomerToken] ([Id], [CustomerId], [Token]) VALUES (N'fde8943d-906f-4cdd-9742-f03e50c6dc6a', N'3cd1fd5b-6a17-42be-b583-1d954c985160', N'eIaTdGg6QZmlbG58tQpEkb:APA91bHMnrB2_TpQ-zaP9QBWv4fdkbAYQOFmgKzuxa_NWw7tjeVHNOx5dyfBAwo_wkXc0Ce2M7OKZQnAiFqrBFp09PsJmvRSJp9R_vtR_lF0Owlk0Qi1wO3j7bBTSN9qZxGCc695vRuO')
INSERT [dbo].[CustomerToken] ([Id], [CustomerId], [Token]) VALUES (N'2014454b-4557-4459-b3ac-f74db2619abd', N'3cd1fd5b-6a17-42be-b583-1d954c985160', N'efCi5oJhTmulFWRfMGCHSB:APA91bE07ffbFUiWBQFU1H7Z49r0mCNyiUzPJc3FGk6S5IwhBokz8aHz7iqrWxfiKODhcpJp8jS5Abpi_6zKgWihm8xr8LAS9fqQ_nu6YV1oNDeQypHkLNyF3UdWHIZn2OI-0C2szp4d')
INSERT [dbo].[CustomerToken] ([Id], [CustomerId], [Token]) VALUES (N'd07bbf8c-52d1-4991-b3bb-fbdd9179630b', N'3cd1fd5b-6a17-42be-b583-1d954c985160', N'flwXFnIlRueKGWcWBS8mF9:APA91bGPXMGaO4L0-HGiQZS1B3ZYp8b8U06VvjXGDg-jQ2jGuCR8wDWIjnb4c9NM6Czttjkep2bKaL_Kvy9_lyHYvXUmr4OYHMioPh5p1v43EyfDgablQ8KwBJZrPUcloFXjxc01T1ii')
GO
INSERT [dbo].[Gender] ([Id], [Name]) VALUES (N'4461e740-e465-4b4f-927f-3a4f4a3e8cb7', N'Nữ')
INSERT [dbo].[Gender] ([Id], [Name]) VALUES (N'223ad67a-898e-4fa7-b30d-b014a5c05381', N'Khác')
INSERT [dbo].[Gender] ([Id], [Name]) VALUES (N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf', N'Nam')
GO
INSERT [dbo].[Project] ([Id], [Name], [Poster], [MinAge], [MaxAge], [Price], [Description], [ResponseDeadline], [ProjectDeadline], [Status], [CreateDate], [UpdateDate]) VALUES (N'06d084e6-a8e1-4cda-b292-18f98ae1a558', N'Review phim ma', N'3cd1fd5b-6a17-42be-b583-1d954c985160', 50, 60, CAST(100 AS Decimal(18, 0)), N'Cần một giọng ma mị', CAST(N'2021-10-29T00:00:00.000' AS DateTime), CAST(N'2021-10-31T00:00:00.000' AS DateTime), 4, CAST(N'2021-10-28T06:40:38.617' AS DateTime), CAST(N'2021-10-28T06:45:19.883' AS DateTime))
INSERT [dbo].[Project] ([Id], [Name], [Poster], [MinAge], [MaxAge], [Price], [Description], [ResponseDeadline], [ProjectDeadline], [Status], [CreateDate], [UpdateDate]) VALUES (N'ac1b5aa1-676a-4d53-bf57-213b2acb5ca1', N'Video Hài Nói', N'b96959b4-6a70-4f56-b30f-8a2993e7e450', 20, 27, CAST(120 AS Decimal(18, 0)), N'Tui đang tìm kiếm bạn nào có giọng hài hài ạ.', CAST(N'2021-10-10T00:00:00.000' AS DateTime), CAST(N'2021-10-15T00:00:00.000' AS DateTime), 1, CAST(N'2021-10-02T00:00:00.000' AS DateTime), CAST(N'2021-10-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Project] ([Id], [Name], [Poster], [MinAge], [MaxAge], [Price], [Description], [ResponseDeadline], [ProjectDeadline], [Status], [CreateDate], [UpdateDate]) VALUES (N'a8c00c80-359d-4f9b-985b-22ea2fe36163', N'Valorant', N'3cd1fd5b-6a17-42be-b583-1d954c985160', 18, 65, CAST(200 AS Decimal(18, 0)), N'Video game fps for Riot', CAST(N'2021-10-28T00:00:00.000' AS DateTime), CAST(N'2021-10-31T00:00:00.000' AS DateTime), 4, CAST(N'2021-10-26T10:19:57.943' AS DateTime), CAST(N'2021-10-27T15:08:22.277' AS DateTime))
INSERT [dbo].[Project] ([Id], [Name], [Poster], [MinAge], [MaxAge], [Price], [Description], [ResponseDeadline], [ProjectDeadline], [Status], [CreateDate], [UpdateDate]) VALUES (N'807f39a2-5a12-4295-abdf-232f984f7c4b', N'Du Lịch Đà Lạt', N'a8aacaef-19ea-40fb-bf6b-a7960e470aac', 20, 27, CAST(150 AS Decimal(18, 0)), N'Bạn nào có giọng nghe kiểu hướng dẫn viên du lịch thì liên hệ mình.', CAST(N'2021-09-19T00:00:00.000' AS DateTime), CAST(N'2021-09-25T00:00:00.000' AS DateTime), 2, CAST(N'2021-09-17T00:00:00.000' AS DateTime), CAST(N'2021-09-17T00:00:00.000' AS DateTime))
INSERT [dbo].[Project] ([Id], [Name], [Poster], [MinAge], [MaxAge], [Price], [Description], [ResponseDeadline], [ProjectDeadline], [Status], [CreateDate], [UpdateDate]) VALUES (N'975b3044-7ea2-49dd-afbb-5323542c900c', N'Chương Trình Hướng Dẫn Nấu Ăn', N'3cd1fd5b-6a17-42be-b583-1d954c985160', 18, 25, CAST(140 AS Decimal(18, 0)), N'Cần tìm một bạn có giọng nói nhẹ nhàng và trong để lồng tiếng cho những video hướng dẫn nấu ăn', CAST(N'2021-10-19T14:41:04.557' AS DateTime), CAST(N'2021-10-19T14:41:04.557' AS DateTime), 3, CAST(N'2021-10-19T14:52:12.853' AS DateTime), CAST(N'2021-10-26T11:40:18.577' AS DateTime))
INSERT [dbo].[Project] ([Id], [Name], [Poster], [MinAge], [MaxAge], [Price], [Description], [ResponseDeadline], [ProjectDeadline], [Status], [CreateDate], [UpdateDate]) VALUES (N'838dbc79-aec8-4d8c-8684-5ed9955d4150', N'Review Đồ Công Nghệ', N'3cd1fd5b-6a17-42be-b583-1d954c985160', 18, 30, CAST(150 AS Decimal(18, 0)), N'Bên công ty mình cần tìm 1 đến 2 bạn để lồng tiếng cho video giới thiệu, review sản phẩm công nghệ mới', CAST(N'2021-10-19T14:41:04.557' AS DateTime), CAST(N'2021-10-19T14:41:04.557' AS DateTime), 4, CAST(N'2021-10-19T14:53:05.573' AS DateTime), CAST(N'2021-10-22T02:56:15.937' AS DateTime))
INSERT [dbo].[Project] ([Id], [Name], [Poster], [MinAge], [MaxAge], [Price], [Description], [ResponseDeadline], [ProjectDeadline], [Status], [CreateDate], [UpdateDate]) VALUES (N'0c6e948c-7f22-4e5d-99b4-68de8b4a00f1', N'Chương Trình Khuyến Mãi và Ưu Đãi Khi Mua Xe Hơi', N'3cd1fd5b-6a17-42be-b583-1d954c985160', 18, 65, CAST(120 AS Decimal(18, 0)), N'Hiện tại showroom bên mình cần tìm 1 bạn để lồng tiếng cho video giới thiệu khuyến mãi và ưu đãi khi mua xe hơi bên mình', CAST(N'2021-10-20T00:00:00.000' AS DateTime), CAST(N'2021-10-27T00:00:00.000' AS DateTime), 1, CAST(N'2021-10-20T04:36:37.280' AS DateTime), CAST(N'2021-10-27T15:03:55.030' AS DateTime))
INSERT [dbo].[Project] ([Id], [Name], [Poster], [MinAge], [MaxAge], [Price], [Description], [ResponseDeadline], [ProjectDeadline], [Status], [CreateDate], [UpdateDate]) VALUES (N'622a9b29-8c80-42f4-a3ad-7805ded8f81a', N'Lắng Nghe Mỗi Ngày', N'3cd1fd5b-6a17-42be-b583-1d954c985160', 20, 35, CAST(100 AS Decimal(18, 0)), N'Mình đang cần tìm 1 bạn giọng trầm ấm để lồng tiếng thật truyền cảm cho những video nói về cách sồng, cũng như chia sẻ với những người nghe', CAST(N'2021-10-19T14:41:04.557' AS DateTime), CAST(N'2021-10-19T14:41:04.557' AS DateTime), 3, CAST(N'2021-10-19T14:51:33.950' AS DateTime), CAST(N'2021-10-19T00:00:00.000' AS DateTime))
INSERT [dbo].[Project] ([Id], [Name], [Poster], [MinAge], [MaxAge], [Price], [Description], [ResponseDeadline], [ProjectDeadline], [Status], [CreateDate], [UpdateDate]) VALUES (N'93846e4f-e99b-4fb4-839a-7e200f594c8a', N'Khuyến Mãi Mua Sắm tại AEON Mall', N'3cd1fd5b-6a17-42be-b583-1d954c985160', 20, 50, CAST(120 AS Decimal(18, 0)), N'Tìm 2 bạn 1 nam và 1 nữ để lồng tiếng cho video phát tại siêu thị để trưng bày quảng cáo cho người tiêu dùng khi đi vào siêu thị', CAST(N'2021-10-19T14:41:04.557' AS DateTime), CAST(N'2021-10-19T14:41:04.557' AS DateTime), 2, CAST(N'2021-10-19T14:50:23.133' AS DateTime), CAST(N'2021-10-26T14:59:43.790' AS DateTime))
INSERT [dbo].[Project] ([Id], [Name], [Poster], [MinAge], [MaxAge], [Price], [Description], [ResponseDeadline], [ProjectDeadline], [Status], [CreateDate], [UpdateDate]) VALUES (N'427c47da-3940-48a6-a925-92024298b387', N'Review Phim', N'a8aacaef-19ea-40fb-bf6b-a7960e470aac', 18, 25, CAST(150 AS Decimal(18, 0)), N'Tôi đang cần tìm 2 bạn 1 bạn nam và 1 bạn nữ có giọng dễ nghe và phải biết biến hóa giọng cho phù hợp với từng video review phim của tôi', CAST(N'2021-10-18T08:41:25.130' AS DateTime), CAST(N'2021-10-22T08:41:25.130' AS DateTime), 2, CAST(N'2021-10-02T08:12:45.420' AS DateTime), CAST(N'2021-10-03T02:55:26.747' AS DateTime))
INSERT [dbo].[Project] ([Id], [Name], [Poster], [MinAge], [MaxAge], [Price], [Description], [ResponseDeadline], [ProjectDeadline], [Status], [CreateDate], [UpdateDate]) VALUES (N'f32884c4-f453-4b93-a89e-d50e47815c27', N'Review đồ ăn Hà Nội ', N'3cd1fd5b-6a17-42be-b583-1d954c985160', 18, 65, CAST(150 AS Decimal(18, 0)), N'tôi cần một người có chất giọng dễ thương nghe nha', CAST(N'2021-10-28T00:00:00.000' AS DateTime), CAST(N'2021-10-31T00:00:00.000' AS DateTime), 2, CAST(N'2021-10-27T14:00:09.220' AS DateTime), CAST(N'2021-10-28T07:33:08.627' AS DateTime))
INSERT [dbo].[Project] ([Id], [Name], [Poster], [MinAge], [MaxAge], [Price], [Description], [ResponseDeadline], [ProjectDeadline], [Status], [CreateDate], [UpdateDate]) VALUES (N'770de6ef-487e-4435-9a01-f7d87a4e9322', N'Review Đồ Ăn', N'3cd1fd5b-6a17-42be-b583-1d954c985160', 18, 20, CAST(100 AS Decimal(18, 0)), N'Tui đang làm một video review vè đồ ăn, nên cần bạn nào có giọng dễ thương xíu nha.', CAST(N'2021-10-18T08:41:25.130' AS DateTime), CAST(N'2021-10-22T08:41:25.130' AS DateTime), 2, CAST(N'2021-10-02T08:12:45.420' AS DateTime), CAST(N'2021-10-26T16:50:33.770' AS DateTime))
GO
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'ac1b5aa1-676a-4d53-bf57-213b2acb5ca1', N'e50a231b-1596-48eb-a1ce-1095fb6aa63b')
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'ac1b5aa1-676a-4d53-bf57-213b2acb5ca1', N'7cbe503c-c8a0-4596-93aa-3c9fb7366c33')
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'a8c00c80-359d-4f9b-985b-22ea2fe36163', N'e50a231b-1596-48eb-a1ce-1095fb6aa63b')
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'a8c00c80-359d-4f9b-985b-22ea2fe36163', N'7cbe503c-c8a0-4596-93aa-3c9fb7366c33')
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'807f39a2-5a12-4295-abdf-232f984f7c4b', N'e50a231b-1596-48eb-a1ce-1095fb6aa63b')
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'975b3044-7ea2-49dd-afbb-5323542c900c', N'e50a231b-1596-48eb-a1ce-1095fb6aa63b')
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'975b3044-7ea2-49dd-afbb-5323542c900c', N'2179f408-b174-465d-8126-1562b04f12ad')
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'975b3044-7ea2-49dd-afbb-5323542c900c', N'd6dfce11-bd2f-4654-b24e-2a5df2e07cf0')
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'975b3044-7ea2-49dd-afbb-5323542c900c', N'7cbe503c-c8a0-4596-93aa-3c9fb7366c33')
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'838dbc79-aec8-4d8c-8684-5ed9955d4150', N'7cbe503c-c8a0-4596-93aa-3c9fb7366c33')
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'622a9b29-8c80-42f4-a3ad-7805ded8f81a', N'e50a231b-1596-48eb-a1ce-1095fb6aa63b')
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'622a9b29-8c80-42f4-a3ad-7805ded8f81a', N'2179f408-b174-465d-8126-1562b04f12ad')
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'622a9b29-8c80-42f4-a3ad-7805ded8f81a', N'd6dfce11-bd2f-4654-b24e-2a5df2e07cf0')
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'93846e4f-e99b-4fb4-839a-7e200f594c8a', N'e50a231b-1596-48eb-a1ce-1095fb6aa63b')
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'93846e4f-e99b-4fb4-839a-7e200f594c8a', N'2179f408-b174-465d-8126-1562b04f12ad')
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'93846e4f-e99b-4fb4-839a-7e200f594c8a', N'd6dfce11-bd2f-4654-b24e-2a5df2e07cf0')
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'93846e4f-e99b-4fb4-839a-7e200f594c8a', N'7cbe503c-c8a0-4596-93aa-3c9fb7366c33')
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'427c47da-3940-48a6-a925-92024298b387', N'7cbe503c-c8a0-4596-93aa-3c9fb7366c33')
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'f32884c4-f453-4b93-a89e-d50e47815c27', N'e50a231b-1596-48eb-a1ce-1095fb6aa63b')
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'f32884c4-f453-4b93-a89e-d50e47815c27', N'7cbe503c-c8a0-4596-93aa-3c9fb7366c33')
INSERT [dbo].[ProjectCountry] ([ProjectId], [CountryId]) VALUES (N'770de6ef-487e-4435-9a01-f7d87a4e9322', N'e50a231b-1596-48eb-a1ce-1095fb6aa63b')
GO
INSERT [dbo].[ProjectGender] ([ProjectId], [GenderId]) VALUES (N'06d084e6-a8e1-4cda-b292-18f98ae1a558', N'4461e740-e465-4b4f-927f-3a4f4a3e8cb7')
INSERT [dbo].[ProjectGender] ([ProjectId], [GenderId]) VALUES (N'06d084e6-a8e1-4cda-b292-18f98ae1a558', N'223ad67a-898e-4fa7-b30d-b014a5c05381')
INSERT [dbo].[ProjectGender] ([ProjectId], [GenderId]) VALUES (N'06d084e6-a8e1-4cda-b292-18f98ae1a558', N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf')
INSERT [dbo].[ProjectGender] ([ProjectId], [GenderId]) VALUES (N'ac1b5aa1-676a-4d53-bf57-213b2acb5ca1', N'4461e740-e465-4b4f-927f-3a4f4a3e8cb7')
INSERT [dbo].[ProjectGender] ([ProjectId], [GenderId]) VALUES (N'ac1b5aa1-676a-4d53-bf57-213b2acb5ca1', N'223ad67a-898e-4fa7-b30d-b014a5c05381')
INSERT [dbo].[ProjectGender] ([ProjectId], [GenderId]) VALUES (N'a8c00c80-359d-4f9b-985b-22ea2fe36163', N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf')
INSERT [dbo].[ProjectGender] ([ProjectId], [GenderId]) VALUES (N'807f39a2-5a12-4295-abdf-232f984f7c4b', N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf')
INSERT [dbo].[ProjectGender] ([ProjectId], [GenderId]) VALUES (N'975b3044-7ea2-49dd-afbb-5323542c900c', N'4461e740-e465-4b4f-927f-3a4f4a3e8cb7')
INSERT [dbo].[ProjectGender] ([ProjectId], [GenderId]) VALUES (N'975b3044-7ea2-49dd-afbb-5323542c900c', N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf')
INSERT [dbo].[ProjectGender] ([ProjectId], [GenderId]) VALUES (N'838dbc79-aec8-4d8c-8684-5ed9955d4150', N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf')
INSERT [dbo].[ProjectGender] ([ProjectId], [GenderId]) VALUES (N'622a9b29-8c80-42f4-a3ad-7805ded8f81a', N'4461e740-e465-4b4f-927f-3a4f4a3e8cb7')
INSERT [dbo].[ProjectGender] ([ProjectId], [GenderId]) VALUES (N'93846e4f-e99b-4fb4-839a-7e200f594c8a', N'4461e740-e465-4b4f-927f-3a4f4a3e8cb7')
INSERT [dbo].[ProjectGender] ([ProjectId], [GenderId]) VALUES (N'93846e4f-e99b-4fb4-839a-7e200f594c8a', N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf')
INSERT [dbo].[ProjectGender] ([ProjectId], [GenderId]) VALUES (N'427c47da-3940-48a6-a925-92024298b387', N'4461e740-e465-4b4f-927f-3a4f4a3e8cb7')
INSERT [dbo].[ProjectGender] ([ProjectId], [GenderId]) VALUES (N'f32884c4-f453-4b93-a89e-d50e47815c27', N'4461e740-e465-4b4f-927f-3a4f4a3e8cb7')
INSERT [dbo].[ProjectGender] ([ProjectId], [GenderId]) VALUES (N'f32884c4-f453-4b93-a89e-d50e47815c27', N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf')
INSERT [dbo].[ProjectGender] ([ProjectId], [GenderId]) VALUES (N'770de6ef-487e-4435-9a01-f7d87a4e9322', N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf')
GO
INSERT [dbo].[ProjectUsagePurpose] ([ProjectId], [UsagePurposeId]) VALUES (N'06d084e6-a8e1-4cda-b292-18f98ae1a558', N'c23ad2d4-5ff3-40ac-97a2-3c50b47e3dfb')
INSERT [dbo].[ProjectUsagePurpose] ([ProjectId], [UsagePurposeId]) VALUES (N'06d084e6-a8e1-4cda-b292-18f98ae1a558', N'984c3e7a-bef1-49e3-b4ea-ba4cf065c83e')
INSERT [dbo].[ProjectUsagePurpose] ([ProjectId], [UsagePurposeId]) VALUES (N'ac1b5aa1-676a-4d53-bf57-213b2acb5ca1', N'984c3e7a-bef1-49e3-b4ea-ba4cf065c83e')
INSERT [dbo].[ProjectUsagePurpose] ([ProjectId], [UsagePurposeId]) VALUES (N'a8c00c80-359d-4f9b-985b-22ea2fe36163', N'984c3e7a-bef1-49e3-b4ea-ba4cf065c83e')
INSERT [dbo].[ProjectUsagePurpose] ([ProjectId], [UsagePurposeId]) VALUES (N'807f39a2-5a12-4295-abdf-232f984f7c4b', N'984c3e7a-bef1-49e3-b4ea-ba4cf065c83e')
INSERT [dbo].[ProjectUsagePurpose] ([ProjectId], [UsagePurposeId]) VALUES (N'975b3044-7ea2-49dd-afbb-5323542c900c', N'984c3e7a-bef1-49e3-b4ea-ba4cf065c83e')
INSERT [dbo].[ProjectUsagePurpose] ([ProjectId], [UsagePurposeId]) VALUES (N'838dbc79-aec8-4d8c-8684-5ed9955d4150', N'984c3e7a-bef1-49e3-b4ea-ba4cf065c83e')
INSERT [dbo].[ProjectUsagePurpose] ([ProjectId], [UsagePurposeId]) VALUES (N'0c6e948c-7f22-4e5d-99b4-68de8b4a00f1', N'984c3e7a-bef1-49e3-b4ea-ba4cf065c83e')
INSERT [dbo].[ProjectUsagePurpose] ([ProjectId], [UsagePurposeId]) VALUES (N'622a9b29-8c80-42f4-a3ad-7805ded8f81a', N'984c3e7a-bef1-49e3-b4ea-ba4cf065c83e')
INSERT [dbo].[ProjectUsagePurpose] ([ProjectId], [UsagePurposeId]) VALUES (N'93846e4f-e99b-4fb4-839a-7e200f594c8a', N'984c3e7a-bef1-49e3-b4ea-ba4cf065c83e')
INSERT [dbo].[ProjectUsagePurpose] ([ProjectId], [UsagePurposeId]) VALUES (N'427c47da-3940-48a6-a925-92024298b387', N'984c3e7a-bef1-49e3-b4ea-ba4cf065c83e')
INSERT [dbo].[ProjectUsagePurpose] ([ProjectId], [UsagePurposeId]) VALUES (N'f32884c4-f453-4b93-a89e-d50e47815c27', N'984c3e7a-bef1-49e3-b4ea-ba4cf065c83e')
INSERT [dbo].[ProjectUsagePurpose] ([ProjectId], [UsagePurposeId]) VALUES (N'770de6ef-487e-4435-9a01-f7d87a4e9322', N'984c3e7a-bef1-49e3-b4ea-ba4cf065c83e')
GO
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'06d084e6-a8e1-4cda-b292-18f98ae1a558', N'1609d173-3987-4ba0-9bec-30f0fdc69128')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'06d084e6-a8e1-4cda-b292-18f98ae1a558', N'5d5ce036-117b-4d62-b807-5061861d0b90')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'ac1b5aa1-676a-4d53-bf57-213b2acb5ca1', N'79cd1e1c-b5b7-4cb3-af1b-6cfb3cf96567')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'ac1b5aa1-676a-4d53-bf57-213b2acb5ca1', N'1fa6e893-ced3-4c3a-9005-9df45a0b22e8')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'a8c00c80-359d-4f9b-985b-22ea2fe36163', N'79cd1e1c-b5b7-4cb3-af1b-6cfb3cf96567')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'807f39a2-5a12-4295-abdf-232f984f7c4b', N'1609d173-3987-4ba0-9bec-30f0fdc69128')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'807f39a2-5a12-4295-abdf-232f984f7c4b', N'79cd1e1c-b5b7-4cb3-af1b-6cfb3cf96567')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'807f39a2-5a12-4295-abdf-232f984f7c4b', N'175c69a2-94f1-4783-8636-b13b7d110312')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'975b3044-7ea2-49dd-afbb-5323542c900c', N'79cd1e1c-b5b7-4cb3-af1b-6cfb3cf96567')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'975b3044-7ea2-49dd-afbb-5323542c900c', N'1fa6e893-ced3-4c3a-9005-9df45a0b22e8')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'975b3044-7ea2-49dd-afbb-5323542c900c', N'175c69a2-94f1-4783-8636-b13b7d110312')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'975b3044-7ea2-49dd-afbb-5323542c900c', N'4b963959-0af8-4d42-8b72-ce4021fe3f56')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'838dbc79-aec8-4d8c-8684-5ed9955d4150', N'79cd1e1c-b5b7-4cb3-af1b-6cfb3cf96567')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'838dbc79-aec8-4d8c-8684-5ed9955d4150', N'1fa6e893-ced3-4c3a-9005-9df45a0b22e8')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'622a9b29-8c80-42f4-a3ad-7805ded8f81a', N'1609d173-3987-4ba0-9bec-30f0fdc69128')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'622a9b29-8c80-42f4-a3ad-7805ded8f81a', N'4b963959-0af8-4d42-8b72-ce4021fe3f56')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'93846e4f-e99b-4fb4-839a-7e200f594c8a', N'f725d621-6e73-4492-a0d9-1664b8733781')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'93846e4f-e99b-4fb4-839a-7e200f594c8a', N'1609d173-3987-4ba0-9bec-30f0fdc69128')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'93846e4f-e99b-4fb4-839a-7e200f594c8a', N'e9f4e04a-d382-4e5b-9aff-3ea7645341cc')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'93846e4f-e99b-4fb4-839a-7e200f594c8a', N'5d5ce036-117b-4d62-b807-5061861d0b90')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'93846e4f-e99b-4fb4-839a-7e200f594c8a', N'1f56c106-7a38-4c35-89d1-5844907bafeb')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'93846e4f-e99b-4fb4-839a-7e200f594c8a', N'79cd1e1c-b5b7-4cb3-af1b-6cfb3cf96567')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'93846e4f-e99b-4fb4-839a-7e200f594c8a', N'1fa6e893-ced3-4c3a-9005-9df45a0b22e8')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'93846e4f-e99b-4fb4-839a-7e200f594c8a', N'175c69a2-94f1-4783-8636-b13b7d110312')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'93846e4f-e99b-4fb4-839a-7e200f594c8a', N'4b963959-0af8-4d42-8b72-ce4021fe3f56')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'427c47da-3940-48a6-a925-92024298b387', N'1f56c106-7a38-4c35-89d1-5844907bafeb')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'427c47da-3940-48a6-a925-92024298b387', N'79cd1e1c-b5b7-4cb3-af1b-6cfb3cf96567')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'427c47da-3940-48a6-a925-92024298b387', N'1fa6e893-ced3-4c3a-9005-9df45a0b22e8')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'f32884c4-f453-4b93-a89e-d50e47815c27', N'79cd1e1c-b5b7-4cb3-af1b-6cfb3cf96567')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'f32884c4-f453-4b93-a89e-d50e47815c27', N'175c69a2-94f1-4783-8636-b13b7d110312')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'770de6ef-487e-4435-9a01-f7d87a4e9322', N'1f56c106-7a38-4c35-89d1-5844907bafeb')
INSERT [dbo].[ProjectVoiceStyle] ([ProjectId], [VoiceStyleId]) VALUES (N'770de6ef-487e-4435-9a01-f7d87a4e9322', N'79cd1e1c-b5b7-4cb3-af1b-6cfb3cf96567')
GO
INSERT [dbo].[UsagePurpose] ([Id], [Name]) VALUES (N'77e2ecd9-6dee-4c86-8466-22b45cc9a72c', N'Hoạt Hình')
INSERT [dbo].[UsagePurpose] ([Id], [Name]) VALUES (N'6d79e493-2519-4066-8ce7-3aec8277ffd7', N'Đài')
INSERT [dbo].[UsagePurpose] ([Id], [Name]) VALUES (N'c23ad2d4-5ff3-40ac-97a2-3c50b47e3dfb', N'Quảng cáo')
INSERT [dbo].[UsagePurpose] ([Id], [Name]) VALUES (N'09dddc68-c1cd-48ed-b33e-4cd000fcd63a', N'Đọc sách')
INSERT [dbo].[UsagePurpose] ([Id], [Name]) VALUES (N'377a969e-372d-4b77-80d1-558340575a5e', N'Giáo dục')
INSERT [dbo].[UsagePurpose] ([Id], [Name]) VALUES (N'034ab39d-2b64-4412-a8b5-8f04ce3f2cd2', N'Phim tài liệu')
INSERT [dbo].[UsagePurpose] ([Id], [Name]) VALUES (N'ebbf69bf-1215-46cf-98a8-977051008bb8', N'Đánh giá')
INSERT [dbo].[UsagePurpose] ([Id], [Name]) VALUES (N'984c3e7a-bef1-49e3-b4ea-ba4cf065c83e', N'Giải trí')
INSERT [dbo].[UsagePurpose] ([Id], [Name]) VALUES (N'd47e76f8-a512-4459-b5a0-fcf970adc3bb', N'Truyền hình')
GO
INSERT [dbo].[User] ([Id], [Username], [Email], [Phone], [Password], [FirstName], [LastName], [Role], [Gender], [Avatar], [Status], [LastLoginTime], [CreateDate], [UpdateDate], [UpdateBy]) VALUES (N'596cf15f-40c7-4b24-8c48-364ff0713356', N'admin', N'admin@gmail.com', N'0339040899', N'admin', N'Administrator', N'System', 0, N'220e9cbc-2a7a-4212-a4e8-d4d85de1e9cf', N'hihihi', 0, NULL, CAST(N'2021-01-01T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[VoiceDemo] ([Id], [Url]) VALUES (N'18dc0ae1-17e9-4d88-b064-15e61c0619a2', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/AUD-MAL-7.mp3?alt=media&token=1c66f6ed-7771-459e-a639-ccb5a72be85d')
INSERT [dbo].[VoiceDemo] ([Id], [Url]) VALUES (N'234a5da4-3572-4063-893f-2d670a0d74b1', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/AUD-MAL-9.mp3?alt=media&token=74442be8-e28b-47e0-ad14-22a17c8ca961')
INSERT [dbo].[VoiceDemo] ([Id], [Url]) VALUES (N'42d4e904-83f8-4906-8d95-301d05aa85b8', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/AUD-FEM-1.mp3?alt=media&token=a11fd800-570f-406d-b546-bbf38365ae65')
INSERT [dbo].[VoiceDemo] ([Id], [Url]) VALUES (N'a6ee5f6d-1f03-41e9-92f4-3a830572940d', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/AUD-FEM-2.mp3?alt=media&token=6b068860-ca22-4279-a626-0df1e8b91fd0')
INSERT [dbo].[VoiceDemo] ([Id], [Url]) VALUES (N'1421c411-618c-4b3b-8b3e-420aa324c6f3', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/AUD-MAL-6.mp3?alt=media&token=657f0683-f049-4e6a-8feb-2e66abc31691')
INSERT [dbo].[VoiceDemo] ([Id], [Url]) VALUES (N'2947801f-9f02-4ade-be25-4aed127bf721', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/AUD-MAL-2.mp3?alt=media&token=877b479c-210e-4c80-9ae7-0d590ff00a53')
INSERT [dbo].[VoiceDemo] ([Id], [Url]) VALUES (N'9541c728-c68d-4215-ac05-a20799aba561', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/AUD-MAL-1.mp3?alt=media&token=b91ee049-1404-484d-a921-0244c068f225')
INSERT [dbo].[VoiceDemo] ([Id], [Url]) VALUES (N'8628ad6e-33fe-4f80-8bda-a97f3cf2d544', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/AUD-MAL-4.mp3?alt=media&token=b84a65de-0ebd-4317-95fa-e6b593880741')
INSERT [dbo].[VoiceDemo] ([Id], [Url]) VALUES (N'248829c6-8f84-4092-b0fd-c56f56029df5', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/AUD-MAL-5.mp3?alt=media&token=ed75b8bd-2601-44d0-917c-2f19e0b6e62d')
INSERT [dbo].[VoiceDemo] ([Id], [Url]) VALUES (N'f9f3868c-118c-4c7a-8a34-c95da16e7f12', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/AUD-MAL-3.mp3?alt=media&token=043be1fa-6536-4156-a8c0-fd6ad4f1404e')
INSERT [dbo].[VoiceDemo] ([Id], [Url]) VALUES (N'08b9c89a-ac42-4f39-801c-dd1ab01e3e52', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/AUD-MAL-8.mp3?alt=media&token=243f0be2-4816-4216-9f81-5fc88f15bd32')
INSERT [dbo].[VoiceDemo] ([Id], [Url]) VALUES (N'21ac1f58-b824-475b-902f-f29cc74b0d39', N'https://firebasestorage.googleapis.com/v0/b/voiceplatform-73d6e.appspot.com/o/AUD-MAL-8.mp3?alt=media&token=243f0be2-4816-4216-9f81-5fc88f15bd32')
GO
INSERT [dbo].[VoiceStyle] ([Id], [Name]) VALUES (N'f725d621-6e73-4492-a0d9-1664b8733781', N'Giọng Trung Niên')
INSERT [dbo].[VoiceStyle] ([Id], [Name]) VALUES (N'1609d173-3987-4ba0-9bec-30f0fdc69128', N'Giọng Trầm')
INSERT [dbo].[VoiceStyle] ([Id], [Name]) VALUES (N'e9f4e04a-d382-4e5b-9aff-3ea7645341cc', N'Giọng Người Nổi Tiếng')
INSERT [dbo].[VoiceStyle] ([Id], [Name]) VALUES (N'5d5ce036-117b-4d62-b807-5061861d0b90', N'Giọng Mạnh Mẽ')
INSERT [dbo].[VoiceStyle] ([Id], [Name]) VALUES (N'1f56c106-7a38-4c35-89d1-5844907bafeb', N'Giọng Ấm')
INSERT [dbo].[VoiceStyle] ([Id], [Name]) VALUES (N'79cd1e1c-b5b7-4cb3-af1b-6cfb3cf96567', N'Giọng Trẻ Trung')
INSERT [dbo].[VoiceStyle] ([Id], [Name]) VALUES (N'1fa6e893-ced3-4c3a-9005-9df45a0b22e8', N'Giọng Cao')
INSERT [dbo].[VoiceStyle] ([Id], [Name]) VALUES (N'175c69a2-94f1-4783-8636-b13b7d110312', N'Giọng Thanh')
INSERT [dbo].[VoiceStyle] ([Id], [Name]) VALUES (N'4b963959-0af8-4d42-8b72-ce4021fe3f56', N'Giọng Người Nước Ngoài Bản Xứ')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Artist__536C85E492204412]    Script Date: 10/28/2021 10:26:13 PM ******/
ALTER TABLE [dbo].[Artist] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Artist__5C7E359E3D0B7733]    Script Date: 10/28/2021 10:26:13 PM ******/
ALTER TABLE [dbo].[Artist] ADD UNIQUE NONCLUSTERED 
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Artist__A9D105349D3C2CC2]    Script Date: 10/28/2021 10:26:13 PM ******/
ALTER TABLE [dbo].[Artist] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Customer__536C85E415D6A19B]    Script Date: 10/28/2021 10:26:13 PM ******/
ALTER TABLE [dbo].[Customer] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Customer__5C7E359E5F50667D]    Script Date: 10/28/2021 10:26:13 PM ******/
ALTER TABLE [dbo].[Customer] ADD UNIQUE NONCLUSTERED 
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Customer__A9D1053424A06F7F]    Script Date: 10/28/2021 10:26:13 PM ******/
ALTER TABLE [dbo].[Customer] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__User__536C85E4E29FB533]    Script Date: 10/28/2021 10:26:13 PM ******/
ALTER TABLE [dbo].[User] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__User__5C7E359E45FC8A21]    Script Date: 10/28/2021 10:26:13 PM ******/
ALTER TABLE [dbo].[User] ADD UNIQUE NONCLUSTERED 
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__User__A9D1053445DF918E]    Script Date: 10/28/2021 10:26:13 PM ******/
ALTER TABLE [dbo].[User] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AdminToken]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Artist]  WITH CHECK ADD FOREIGN KEY([Gender])
REFERENCES [dbo].[Gender] ([Id])
GO
ALTER TABLE [dbo].[Artist]  WITH CHECK ADD FOREIGN KEY([UpdateBy])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[ArtistCountry]  WITH CHECK ADD FOREIGN KEY([ArtistId])
REFERENCES [dbo].[Artist] ([Id])
GO
ALTER TABLE [dbo].[ArtistCountry]  WITH CHECK ADD FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[ArtistProject]  WITH CHECK ADD  CONSTRAINT [FK__ArtistPro__Artis__656C112C] FOREIGN KEY([ArtistId])
REFERENCES [dbo].[Artist] ([Id])
GO
ALTER TABLE [dbo].[ArtistProject] CHECK CONSTRAINT [FK__ArtistPro__Artis__656C112C]
GO
ALTER TABLE [dbo].[ArtistProject]  WITH CHECK ADD  CONSTRAINT [FK__ArtistPro__Proje__66603565] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[ArtistProject] CHECK CONSTRAINT [FK__ArtistPro__Proje__66603565]
GO
ALTER TABLE [dbo].[ArtistProjectFile]  WITH CHECK ADD  CONSTRAINT [FK__ArtistPro__Creat__6754599E] FOREIGN KEY([CreateBy])
REFERENCES [dbo].[Artist] ([Id])
GO
ALTER TABLE [dbo].[ArtistProjectFile] CHECK CONSTRAINT [FK__ArtistPro__Creat__6754599E]
GO
ALTER TABLE [dbo].[ArtistProjectFile]  WITH CHECK ADD  CONSTRAINT [FK__ArtistPro__Proje__68487DD7] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[ArtistProjectFile] CHECK CONSTRAINT [FK__ArtistPro__Proje__68487DD7]
GO
ALTER TABLE [dbo].[ArtistToken]  WITH CHECK ADD  CONSTRAINT [FK_ArtistToken_Artist1] FOREIGN KEY([ArtistId])
REFERENCES [dbo].[Artist] ([Id])
GO
ALTER TABLE [dbo].[ArtistToken] CHECK CONSTRAINT [FK_ArtistToken_Artist1]
GO
ALTER TABLE [dbo].[ArtistVoiceDemo]  WITH CHECK ADD FOREIGN KEY([ArtistId])
REFERENCES [dbo].[Artist] ([Id])
GO
ALTER TABLE [dbo].[ArtistVoiceDemo]  WITH CHECK ADD FOREIGN KEY([VoiceDemoId])
REFERENCES [dbo].[VoiceDemo] ([Id])
GO
ALTER TABLE [dbo].[ArtistVoiceStyle]  WITH CHECK ADD FOREIGN KEY([ArtistId])
REFERENCES [dbo].[Artist] ([Id])
GO
ALTER TABLE [dbo].[ArtistVoiceStyle]  WITH CHECK ADD FOREIGN KEY([VoiceStyleId])
REFERENCES [dbo].[VoiceStyle] ([Id])
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD FOREIGN KEY([Gender])
REFERENCES [dbo].[Gender] ([Id])
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD FOREIGN KEY([UpdateBy])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[CustomerProjectFile]  WITH CHECK ADD  CONSTRAINT [FK__CustomerP__Creat__6EF57B66] FOREIGN KEY([CreateBy])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[CustomerProjectFile] CHECK CONSTRAINT [FK__CustomerP__Creat__6EF57B66]
GO
ALTER TABLE [dbo].[CustomerProjectFile]  WITH CHECK ADD  CONSTRAINT [FK__CustomerP__Proje__6FE99F9F] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[CustomerProjectFile] CHECK CONSTRAINT [FK__CustomerP__Proje__6FE99F9F]
GO
ALTER TABLE [dbo].[CustomerToken]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[ProjectCountry]  WITH CHECK ADD FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[ProjectCountry]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[ProjectGender]  WITH CHECK ADD FOREIGN KEY([GenderId])
REFERENCES [dbo].[Gender] ([Id])
GO
ALTER TABLE [dbo].[ProjectGender]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[ProjectUsagePurpose]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[ProjectUsagePurpose]  WITH CHECK ADD FOREIGN KEY([UsagePurposeId])
REFERENCES [dbo].[UsagePurpose] ([Id])
GO
ALTER TABLE [dbo].[ProjectVoiceStyle]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[ProjectVoiceStyle]  WITH CHECK ADD FOREIGN KEY([VoiceStyleId])
REFERENCES [dbo].[VoiceStyle] ([Id])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([Gender])
REFERENCES [dbo].[Gender] ([Id])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([UpdateBy])
REFERENCES [dbo].[User] ([Id])
GO
USE [master]
GO
ALTER DATABASE [VoicePlatform] SET  READ_WRITE 
GO
