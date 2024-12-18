USE [master]
GO
/****** Object:  Database [ClinicaDb2]    Script Date: 9/11/2024 16:06:10 ******/
CREATE DATABASE [ClinicaDb2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ClinicaDb2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS01\MSSQL\DATA\ClinicaDb2.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ClinicaDb2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS01\MSSQL\DATA\ClinicaDb2_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ClinicaDb2] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ClinicaDb2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ClinicaDb2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ClinicaDb2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ClinicaDb2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ClinicaDb2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ClinicaDb2] SET ARITHABORT OFF 
GO
ALTER DATABASE [ClinicaDb2] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ClinicaDb2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ClinicaDb2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ClinicaDb2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ClinicaDb2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ClinicaDb2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ClinicaDb2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ClinicaDb2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ClinicaDb2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ClinicaDb2] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ClinicaDb2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ClinicaDb2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ClinicaDb2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ClinicaDb2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ClinicaDb2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ClinicaDb2] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ClinicaDb2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ClinicaDb2] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ClinicaDb2] SET  MULTI_USER 
GO
ALTER DATABASE [ClinicaDb2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ClinicaDb2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ClinicaDb2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ClinicaDb2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ClinicaDb2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ClinicaDb2] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ClinicaDb2] SET QUERY_STORE = ON
GO
ALTER DATABASE [ClinicaDb2] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ClinicaDb2]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/11/2024 16:06:10 ******/
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
/****** Object:  Table [dbo].[Administrator]    Script Date: 9/11/2024 16:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Administrator](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [nvarchar](max) NOT NULL,
	[last_name] [nvarchar](max) NOT NULL,
	[DNI] [int] NOT NULL,
	[email] [nvarchar](max) NOT NULL,
	[phone] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Administrator] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 9/11/2024 16:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[patient_id] [int] NOT NULL,
	[doctor_id] [int] NOT NULL,
	[specialty_id] [int] NOT NULL,
	[appointment_date] [datetime2](7) NOT NULL,
	[status] [int] NOT NULL,
	[administrator_id] [int] NULL,
	[AdministratorId] [int] NULL,
 CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 9/11/2024 16:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 9/11/2024 16:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 9/11/2024 16:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 9/11/2024 16:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 9/11/2024 16:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 9/11/2024 16:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 9/11/2024 16:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctor]    Script Date: 9/11/2024 16:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[license_number] [int] NOT NULL,
	[first_name] [nvarchar](max) NOT NULL,
	[last_name] [nvarchar](max) NOT NULL,
	[DNI] [int] NOT NULL,
	[email] [nvarchar](max) NOT NULL,
	[phone] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Doctor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoctorSpecialty]    Script Date: 9/11/2024 16:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorSpecialty](
	[DoctorsId] [int] NOT NULL,
	[SpecialtiesId] [int] NOT NULL,
 CONSTRAINT [PK_DoctorSpecialty] PRIMARY KEY CLUSTERED 
(
	[DoctorsId] ASC,
	[SpecialtiesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 9/11/2024 16:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[birth_date] [nvarchar](max) NOT NULL,
	[address] [nvarchar](max) NULL,
	[medical_history] [nvarchar](max) NULL,
	[first_name] [nvarchar](max) NOT NULL,
	[last_name] [nvarchar](max) NOT NULL,
	[DNI] [int] NOT NULL,
	[email] [nvarchar](max) NOT NULL,
	[phone] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specialty]    Script Date: 9/11/2024 16:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialty](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Specialty] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241028131006_initial-migration', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241028131044_initial-migration', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241028182633_specialty-migration', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241028182653_specialty-migration', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241028212625_ClinicaDb2', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241030210918_IdentityMigration', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241030210956_IdentityMigration', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241031221000_ClinicaDb3', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241108182213_ClinicaDb4', N'8.0.10')
GO
SET IDENTITY_INSERT [dbo].[Administrator] ON 

INSERT [dbo].[Administrator] ([Id], [first_name], [last_name], [DNI], [email], [phone]) VALUES (1, N'Admin1', N'User', 99988877, N'admin1@example.com', N'555-9876')
INSERT [dbo].[Administrator] ([Id], [first_name], [last_name], [DNI], [email], [phone]) VALUES (2, N'Admin2', N'Manager', 99988866, N'admin2@example.com', N'555-8765')
INSERT [dbo].[Administrator] ([Id], [first_name], [last_name], [DNI], [email], [phone]) VALUES (3, N'Admin3', N'Supervisor', 99988855, N'admin3@example.com', N'555-7654')
INSERT [dbo].[Administrator] ([Id], [first_name], [last_name], [DNI], [email], [phone]) VALUES (4, N'Admin4', N'Director', 99988844, N'admin4@example.com', N'555-6543')
INSERT [dbo].[Administrator] ([Id], [first_name], [last_name], [DNI], [email], [phone]) VALUES (5, N'Admin5', N'Coordinator', 99988833, N'admin5@example.com', N'555-5432')
INSERT [dbo].[Administrator] ([Id], [first_name], [last_name], [DNI], [email], [phone]) VALUES (6, N'Admin6', N'Assistant', 99988822, N'admin6@example.com', N'555-4321')
INSERT [dbo].[Administrator] ([Id], [first_name], [last_name], [DNI], [email], [phone]) VALUES (7, N'Admin7', N'Lead', 99988811, N'admin7@example.com', N'555-3210')
INSERT [dbo].[Administrator] ([Id], [first_name], [last_name], [DNI], [email], [phone]) VALUES (8, N'Admin8', N'Executive', 99988800, N'admin8@example.com', N'555-2109')
INSERT [dbo].[Administrator] ([Id], [first_name], [last_name], [DNI], [email], [phone]) VALUES (9, N'Admin9', N'Officer', 99988799, N'admin9@example.com', N'555-1098')
INSERT [dbo].[Administrator] ([Id], [first_name], [last_name], [DNI], [email], [phone]) VALUES (10, N'Admin10', N'Clerk', 99988788, N'admin10@example.com', N'555-0987')
SET IDENTITY_INSERT [dbo].[Administrator] OFF
GO
SET IDENTITY_INSERT [dbo].[Appointment] ON 

INSERT [dbo].[Appointment] ([id], [patient_id], [doctor_id], [specialty_id], [appointment_date], [status], [administrator_id], [AdministratorId]) VALUES (1, 1, 1, 1, CAST(N'2024-10-20T09:00:00.0000000' AS DateTime2), 0, NULL, NULL)
INSERT [dbo].[Appointment] ([id], [patient_id], [doctor_id], [specialty_id], [appointment_date], [status], [administrator_id], [AdministratorId]) VALUES (2, 2, 2, 2, CAST(N'2024-10-21T10:30:00.0000000' AS DateTime2), 0, NULL, NULL)
INSERT [dbo].[Appointment] ([id], [patient_id], [doctor_id], [specialty_id], [appointment_date], [status], [administrator_id], [AdministratorId]) VALUES (3, 3, 3, 3, CAST(N'2024-10-22T11:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Appointment] ([id], [patient_id], [doctor_id], [specialty_id], [appointment_date], [status], [administrator_id], [AdministratorId]) VALUES (4, 4, 4, 4, CAST(N'2024-10-23T14:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Appointment] ([id], [patient_id], [doctor_id], [specialty_id], [appointment_date], [status], [administrator_id], [AdministratorId]) VALUES (5, 5, 5, 5, CAST(N'2024-10-24T15:30:00.0000000' AS DateTime2), 2, NULL, NULL)
INSERT [dbo].[Appointment] ([id], [patient_id], [doctor_id], [specialty_id], [appointment_date], [status], [administrator_id], [AdministratorId]) VALUES (6, 6, 6, 6, CAST(N'2024-10-25T09:00:00.0000000' AS DateTime2), 2, NULL, NULL)
INSERT [dbo].[Appointment] ([id], [patient_id], [doctor_id], [specialty_id], [appointment_date], [status], [administrator_id], [AdministratorId]) VALUES (7, 7, 7, 7, CAST(N'2024-10-26T13:00:00.0000000' AS DateTime2), 3, NULL, NULL)
INSERT [dbo].[Appointment] ([id], [patient_id], [doctor_id], [specialty_id], [appointment_date], [status], [administrator_id], [AdministratorId]) VALUES (8, 8, 8, 8, CAST(N'2024-10-27T10:00:00.0000000' AS DateTime2), 2, NULL, NULL)
INSERT [dbo].[Appointment] ([id], [patient_id], [doctor_id], [specialty_id], [appointment_date], [status], [administrator_id], [AdministratorId]) VALUES (9, 9, 9, 9, CAST(N'2024-10-28T11:30:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Appointment] ([id], [patient_id], [doctor_id], [specialty_id], [appointment_date], [status], [administrator_id], [AdministratorId]) VALUES (10, 10, 10, 10, CAST(N'2024-10-29T12:00:00.0000000' AS DateTime2), 0, NULL, NULL)
INSERT [dbo].[Appointment] ([id], [patient_id], [doctor_id], [specialty_id], [appointment_date], [status], [administrator_id], [AdministratorId]) VALUES (11, 1, 11, 1, CAST(N'2024-11-08T02:33:45.8280000' AS DateTime2), 0, NULL, NULL)
INSERT [dbo].[Appointment] ([id], [patient_id], [doctor_id], [specialty_id], [appointment_date], [status], [administrator_id], [AdministratorId]) VALUES (12, 2, 11, 1, CAST(N'2024-12-08T02:33:45.8280000' AS DateTime2), 0, NULL, NULL)
INSERT [dbo].[Appointment] ([id], [patient_id], [doctor_id], [specialty_id], [appointment_date], [status], [administrator_id], [AdministratorId]) VALUES (23, 11, 11, 1, CAST(N'2024-11-08T18:35:20.4420000' AS DateTime2), 0, NULL, NULL)
INSERT [dbo].[Appointment] ([id], [patient_id], [doctor_id], [specialty_id], [appointment_date], [status], [administrator_id], [AdministratorId]) VALUES (24, 11, 11, 2, CAST(N'2024-11-08T18:35:20.4420000' AS DateTime2), 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Appointment] OFF
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'0fd7f721-0021-41e3-b71b-7c33d93777d6', N'Doctor', N'DOCTOR', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'2f4528d5-eed9-45ce-8e15-e35a53b5d490', N'Patient', N'PATIENT', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'3e671d6f-d860-4d62-b72f-3f164d15b0d6', N'Administrator', N'ADMINISTRATOR', NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f77b1b48-15c1-4f1e-97e9-0e782795e318', N'0fd7f721-0021-41e3-b71b-7c33d93777d6')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c2d90d10-5a38-43dc-bd8e-1098a6e6d570', N'2f4528d5-eed9-45ce-8e15-e35a53b5d490')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3bc9d19c-d117-4faa-a535-e778ad607f07', N'3e671d6f-d860-4d62-b72f-3f164d15b0d6')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3bc9d19c-d117-4faa-a535-e778ad607f07', N'Agustin', N'AGUSTIN', N'agustin@gmail.com', N'AGUSTIN@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEOWQ7iDJ655X45m5Gb1lIGoCPodimRfnCQ4hQ40ky0ey4pWTpUmEruLcldCEJUIMvA==', N'RJMIE4B54JNR6QW4TNFIUHNOH2IIHPRE', N'cdcd9bac-b791-4065-874b-34ca1c8f639a', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'c2d90d10-5a38-43dc-bd8e-1098a6e6d570', N'Florencia', N'FLORENCIA', N'flor@gmail.com', N'FLOR@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEERd6JOQ4VH+/ocP6fq7MTiwNW6HIZ+O5oMSWpZ6//HNL+NAOadrhdArZUHG6frYQA==', N'KDRIDI22FBDK6K32EHGHGXGEQDRFMCV7', N'12a1593c-df6b-48c7-8ea5-9c66c044e4cd', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'f77b1b48-15c1-4f1e-97e9-0e782795e318', N'Yamila', N'YAMILA', N'yamila@gmail.com', N'YAMILA@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEI4EJUTq2UiAxf+GzuwjvKzh/O/fGckg2xFMGffvhFZ4n2qWM+w60jvZJoxzHyNaPg==', N'XOUNCQNFZYY43MAOTPGFWEWLGJIYY6PZ', N'63af5969-f2f4-4068-a63f-2fcf761eb143', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Doctor] ON 

INSERT [dbo].[Doctor] ([Id], [license_number], [first_name], [last_name], [DNI], [email], [phone]) VALUES (1, 1001, N'Dr. Adam', N'White', 33322211, N'adam.white@example.com', N'555-4322')
INSERT [dbo].[Doctor] ([Id], [license_number], [first_name], [last_name], [DNI], [email], [phone]) VALUES (2, 1002, N'Dr. Beth', N'Green', 33445566, N'beth.green@example.com', N'555-7653')
INSERT [dbo].[Doctor] ([Id], [license_number], [first_name], [last_name], [DNI], [email], [phone]) VALUES (3, 1003, N'Dr. Carl', N'Black', 77665544, N'carl.black@example.com', N'555-9988')
INSERT [dbo].[Doctor] ([Id], [license_number], [first_name], [last_name], [DNI], [email], [phone]) VALUES (4, 1004, N'Dr. Dana', N'Blue', 55443322, N'dana.blue@example.com', N'555-4433')
INSERT [dbo].[Doctor] ([Id], [license_number], [first_name], [last_name], [DNI], [email], [phone]) VALUES (5, 1005, N'Dr. Eric', N'Red', 22113344, N'eric.red@example.com', N'555-5566')
INSERT [dbo].[Doctor] ([Id], [license_number], [first_name], [last_name], [DNI], [email], [phone]) VALUES (6, 1006, N'Dr. Fiona', N'Purple', 44556633, N'fiona.purple@example.com', N'555-2244')
INSERT [dbo].[Doctor] ([Id], [license_number], [first_name], [last_name], [DNI], [email], [phone]) VALUES (7, 1007, N'Dr. George', N'Yellow', 22334455, N'george.yellow@example.com', N'555-6677')
INSERT [dbo].[Doctor] ([Id], [license_number], [first_name], [last_name], [DNI], [email], [phone]) VALUES (8, 1008, N'Dr. Holly', N'Brown', 66778899, N'holly.brown@example.com', N'555-8899')
INSERT [dbo].[Doctor] ([Id], [license_number], [first_name], [last_name], [DNI], [email], [phone]) VALUES (9, 1009, N'Dr. Ian', N'Gray', 99887711, N'ian.gray@example.com', N'555-1122')
INSERT [dbo].[Doctor] ([Id], [license_number], [first_name], [last_name], [DNI], [email], [phone]) VALUES (10, 1010, N'Dr. Julia', N'Pink', 55667722, N'julia.pink@example.com', N'555-3344')
INSERT [dbo].[Doctor] ([Id], [license_number], [first_name], [last_name], [DNI], [email], [phone]) VALUES (11, 1234, N'Yamila', N'Caviglione', 44104119, N'yamila.caviglione@gmail.com', N'3435076921')
SET IDENTITY_INSERT [dbo].[Doctor] OFF
GO
INSERT [dbo].[DoctorSpecialty] ([DoctorsId], [SpecialtiesId]) VALUES (1, 1)
INSERT [dbo].[DoctorSpecialty] ([DoctorsId], [SpecialtiesId]) VALUES (11, 1)
INSERT [dbo].[DoctorSpecialty] ([DoctorsId], [SpecialtiesId]) VALUES (2, 2)
INSERT [dbo].[DoctorSpecialty] ([DoctorsId], [SpecialtiesId]) VALUES (11, 2)
INSERT [dbo].[DoctorSpecialty] ([DoctorsId], [SpecialtiesId]) VALUES (3, 3)
INSERT [dbo].[DoctorSpecialty] ([DoctorsId], [SpecialtiesId]) VALUES (4, 4)
INSERT [dbo].[DoctorSpecialty] ([DoctorsId], [SpecialtiesId]) VALUES (5, 5)
INSERT [dbo].[DoctorSpecialty] ([DoctorsId], [SpecialtiesId]) VALUES (6, 6)
INSERT [dbo].[DoctorSpecialty] ([DoctorsId], [SpecialtiesId]) VALUES (7, 7)
INSERT [dbo].[DoctorSpecialty] ([DoctorsId], [SpecialtiesId]) VALUES (8, 8)
INSERT [dbo].[DoctorSpecialty] ([DoctorsId], [SpecialtiesId]) VALUES (9, 9)
INSERT [dbo].[DoctorSpecialty] ([DoctorsId], [SpecialtiesId]) VALUES (10, 10)
GO
SET IDENTITY_INSERT [dbo].[Patient] ON 

INSERT [dbo].[Patient] ([Id], [birth_date], [address], [medical_history], [first_name], [last_name], [DNI], [email], [phone]) VALUES (1, N'1980-01-15', N'123 Main St', N'No allergies', N'John', N'Doe', 12345678, N'john.doe@example.com', N'555-1234')
INSERT [dbo].[Patient] ([Id], [birth_date], [address], [medical_history], [first_name], [last_name], [DNI], [email], [phone]) VALUES (2, N'1990-03-22', N'456 Oak St', N'Asthma', N'Jane', N'Smith', 87654321, N'jane.smith@example.com', N'555-5678')
INSERT [dbo].[Patient] ([Id], [birth_date], [address], [medical_history], [first_name], [last_name], [DNI], [email], [phone]) VALUES (3, N'1975-05-10', N'789 Pine St', N'Diabetes', N'Alice', N'Johnson', 12348765, N'alice.johnson@example.com', N'555-8765')
INSERT [dbo].[Patient] ([Id], [birth_date], [address], [medical_history], [first_name], [last_name], [DNI], [email], [phone]) VALUES (4, N'1968-07-30', N'321 Maple St', N'Hypertension', N'Bob', N'Brown', 11223344, N'bob.brown@example.com', N'555-3344')
INSERT [dbo].[Patient] ([Id], [birth_date], [address], [medical_history], [first_name], [last_name], [DNI], [email], [phone]) VALUES (5, N'1985-11-05', N'654 Elm St', N'No known conditions', N'Cathy', N'Davis', 55667788, N'cathy.davis@example.com', N'555-7788')
INSERT [dbo].[Patient] ([Id], [birth_date], [address], [medical_history], [first_name], [last_name], [DNI], [email], [phone]) VALUES (6, N'1982-08-18', N'987 Cedar St', N'Anemia', N'David', N'Wilson', 99887766, N'david.wilson@example.com', N'555-7766')
INSERT [dbo].[Patient] ([Id], [birth_date], [address], [medical_history], [first_name], [last_name], [DNI], [email], [phone]) VALUES (7, N'1992-12-01', N'159 Walnut St', N'Allergic to penicillin', N'Eve', N'Miller', 11225577, N'eve.miller@example.com', N'555-5577')
INSERT [dbo].[Patient] ([Id], [birth_date], [address], [medical_history], [first_name], [last_name], [DNI], [email], [phone]) VALUES (8, N'1979-06-14', N'753 Birch St', N'Migraines', N'Frank', N'Taylor', 77889900, N'frank.taylor@example.com', N'555-9900')
INSERT [dbo].[Patient] ([Id], [birth_date], [address], [medical_history], [first_name], [last_name], [DNI], [email], [phone]) VALUES (9, N'1983-09-25', N'852 Redwood St', N'Gluten intolerance', N'Grace', N'Anderson', 44556677, N'grace.anderson@example.com', N'555-6677')
INSERT [dbo].[Patient] ([Id], [birth_date], [address], [medical_history], [first_name], [last_name], [DNI], [email], [phone]) VALUES (10, N'1987-04-09', N'951 Spruce St', N'Healthy', N'Hank', N'Thomas', 22446688, N'hank.thomas@example.com', N'555-6688')
INSERT [dbo].[Patient] ([Id], [birth_date], [address], [medical_history], [first_name], [last_name], [DNI], [email], [phone]) VALUES (11, N'21/09/23', N'cencajipjp', N'iknk m m', N'Florencia', N'Silvero', 49384932, N'florencia@gmail.com', N'435453232')
SET IDENTITY_INSERT [dbo].[Patient] OFF
GO
SET IDENTITY_INSERT [dbo].[Specialty] ON 

INSERT [dbo].[Specialty] ([Id], [name]) VALUES (1, N'Cardiology')
INSERT [dbo].[Specialty] ([Id], [name]) VALUES (2, N'Dermatology')
INSERT [dbo].[Specialty] ([Id], [name]) VALUES (3, N'Pediatrics')
INSERT [dbo].[Specialty] ([Id], [name]) VALUES (4, N'Neurology')
INSERT [dbo].[Specialty] ([Id], [name]) VALUES (5, N'Orthopedics')
INSERT [dbo].[Specialty] ([Id], [name]) VALUES (6, N'Psychiatry')
INSERT [dbo].[Specialty] ([Id], [name]) VALUES (7, N'Gynecology')
INSERT [dbo].[Specialty] ([Id], [name]) VALUES (8, N'Oncology')
INSERT [dbo].[Specialty] ([Id], [name]) VALUES (9, N'Ophthalmology')
INSERT [dbo].[Specialty] ([Id], [name]) VALUES (10, N'General Medicine')
SET IDENTITY_INSERT [dbo].[Specialty] OFF
GO
/****** Object:  Index [IX_Appointment_AdministratorId]    Script Date: 9/11/2024 16:06:10 ******/
CREATE NONCLUSTERED INDEX [IX_Appointment_AdministratorId] ON [dbo].[Appointment]
(
	[AdministratorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Appointment_doctor_id]    Script Date: 9/11/2024 16:06:10 ******/
CREATE NONCLUSTERED INDEX [IX_Appointment_doctor_id] ON [dbo].[Appointment]
(
	[doctor_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Appointment_patient_id]    Script Date: 9/11/2024 16:06:10 ******/
CREATE NONCLUSTERED INDEX [IX_Appointment_patient_id] ON [dbo].[Appointment]
(
	[patient_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Appointment_specialty_id]    Script Date: 9/11/2024 16:06:10 ******/
CREATE NONCLUSTERED INDEX [IX_Appointment_specialty_id] ON [dbo].[Appointment]
(
	[specialty_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 9/11/2024 16:06:10 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 9/11/2024 16:06:10 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 9/11/2024 16:06:10 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 9/11/2024 16:06:10 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 9/11/2024 16:06:10 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 9/11/2024 16:06:10 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 9/11/2024 16:06:10 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DoctorSpecialty_SpecialtiesId]    Script Date: 9/11/2024 16:06:10 ******/
CREATE NONCLUSTERED INDEX [IX_DoctorSpecialty_SpecialtiesId] ON [dbo].[DoctorSpecialty]
(
	[SpecialtiesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Administrator_AdministratorId] FOREIGN KEY([AdministratorId])
REFERENCES [dbo].[Administrator] ([Id])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Administrator_AdministratorId]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Doctor_doctor_id] FOREIGN KEY([doctor_id])
REFERENCES [dbo].[Doctor] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Doctor_doctor_id]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Patient_patient_id] FOREIGN KEY([patient_id])
REFERENCES [dbo].[Patient] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Patient_patient_id]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Specialty_specialty_id] FOREIGN KEY([specialty_id])
REFERENCES [dbo].[Specialty] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Specialty_specialty_id]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[DoctorSpecialty]  WITH CHECK ADD  CONSTRAINT [FK_DoctorSpecialty_Doctor_DoctorsId] FOREIGN KEY([DoctorsId])
REFERENCES [dbo].[Doctor] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DoctorSpecialty] CHECK CONSTRAINT [FK_DoctorSpecialty_Doctor_DoctorsId]
GO
ALTER TABLE [dbo].[DoctorSpecialty]  WITH CHECK ADD  CONSTRAINT [FK_DoctorSpecialty_Specialty_SpecialtiesId] FOREIGN KEY([SpecialtiesId])
REFERENCES [dbo].[Specialty] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DoctorSpecialty] CHECK CONSTRAINT [FK_DoctorSpecialty_Specialty_SpecialtiesId]
GO
USE [master]
GO
ALTER DATABASE [ClinicaDb2] SET  READ_WRITE 
GO
