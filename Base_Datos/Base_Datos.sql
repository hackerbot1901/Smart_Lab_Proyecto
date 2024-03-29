USE [master]
GO
/****** Object:  Database [LaboratorioDB]    Script Date: 2/26/2024 1:52:51 PM ******/
CREATE DATABASE [LaboratorioDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LaboratorioDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MYSQLSERVER\MSSQL\DATA\LaboratorioDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LaboratorioDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MYSQLSERVER\MSSQL\DATA\LaboratorioDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [LaboratorioDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LaboratorioDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LaboratorioDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LaboratorioDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LaboratorioDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LaboratorioDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LaboratorioDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [LaboratorioDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LaboratorioDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LaboratorioDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LaboratorioDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LaboratorioDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LaboratorioDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LaboratorioDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LaboratorioDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LaboratorioDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LaboratorioDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LaboratorioDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LaboratorioDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LaboratorioDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LaboratorioDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LaboratorioDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LaboratorioDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LaboratorioDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LaboratorioDB] SET RECOVERY FULL 
GO
ALTER DATABASE [LaboratorioDB] SET  MULTI_USER 
GO
ALTER DATABASE [LaboratorioDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LaboratorioDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LaboratorioDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LaboratorioDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LaboratorioDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LaboratorioDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'LaboratorioDB', N'ON'
GO
ALTER DATABASE [LaboratorioDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [LaboratorioDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [LaboratorioDB]
GO
/****** Object:  Table [dbo].[CodigoQr]    Script Date: 2/26/2024 1:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CodigoQr](
	[IdCodigoQr] [int] NOT NULL,
	[NombreLaboratorista] [nvarchar](50) NULL,
	[Subdominio] [nvarchar](50) NULL,
	[Token] [nvarchar](50) NULL,
	[LaboratoristaId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCodigoQr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExamenesMuestra]    Script Date: 2/26/2024 1:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExamenesMuestra](
	[IdExamenMuestra] [int] NOT NULL,
	[IdExterno] [int] NULL,
	[Codigo] [nvarchar](50) NULL,
	[Nombre] [nvarchar](100) NULL,
	[FechaCreacion] [date] NULL,
	[FechaTomaMuestra] [date] NULL,
	[FechaReporte] [date] NULL,
	[FechaValidacion] [date] NULL,
	[UsuarioValidacion] [nvarchar](50) NULL,
	[Estado] [nvarchar](20) NULL,
	[MuestraId] [int] NULL,
	[Valor] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdExamenMuestra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Laboratorista]    Script Date: 2/26/2024 1:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Laboratorista](
	[IdLaboratorista] [int] NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Apellido] [nvarchar](50) NULL,
	[Especialidad] [nvarchar](50) NULL,
	[FechaContratacion] [date] NULL,
	[SucursalId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdLaboratorista] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Muestra]    Script Date: 2/26/2024 1:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Muestra](
	[IdMuestra] [int] NOT NULL,
	[FechaRecepcion] [date] NULL,
	[PacienteId] [int] NULL,
	[SucursalId] [int] NULL,
	[CodigoBarras] [nvarchar](20) NULL,
	[NumeroOrden] [int] NULL,
	[Estado] [nvarchar](20) NULL,
	[Urgencia] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdMuestra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Paciente]    Script Date: 2/26/2024 1:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paciente](
	[IdPaciente] [int] NOT NULL,
	[NombrePaciente] [nvarchar](50) NULL,
	[ApellidoPaciente] [nvarchar](50) NULL,
	[FechaNacimiento] [date] NULL,
	[Genero] [nvarchar](10) NULL,
	[Direccion] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPaciente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sucursal]    Script Date: 2/26/2024 1:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sucursal](
	[IdSucursal] [int] NOT NULL,
	[NombreSucursal] [nvarchar](50) NULL,
	[subdominio] [nvarchar](100) NULL,
	[Telefono] [nvarchar](15) NULL,
	[Encargado] [nvarchar](50) NULL,
	[FechaApertura] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdSucursal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CodigoQr]  WITH CHECK ADD FOREIGN KEY([LaboratoristaId])
REFERENCES [dbo].[Laboratorista] ([IdLaboratorista])
GO
ALTER TABLE [dbo].[ExamenesMuestra]  WITH CHECK ADD FOREIGN KEY([MuestraId])
REFERENCES [dbo].[Muestra] ([IdMuestra])
GO
ALTER TABLE [dbo].[Laboratorista]  WITH CHECK ADD FOREIGN KEY([SucursalId])
REFERENCES [dbo].[Sucursal] ([IdSucursal])
GO
ALTER TABLE [dbo].[Muestra]  WITH CHECK ADD FOREIGN KEY([PacienteId])
REFERENCES [dbo].[Paciente] ([IdPaciente])
GO
ALTER TABLE [dbo].[Muestra]  WITH CHECK ADD FOREIGN KEY([SucursalId])
REFERENCES [dbo].[Sucursal] ([IdSucursal])
GO
USE [master]
GO
ALTER DATABASE [LaboratorioDB] SET  READ_WRITE 
GO
