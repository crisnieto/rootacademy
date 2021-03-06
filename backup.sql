USE [master]
GO
/****** Object:  Database [rootacademy]    Script Date: 7/12/2020 3:19:06 PM ******/
CREATE DATABASE [rootacademy]
ALTER DATABASE [rootacademy] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [rootacademy].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [rootacademy] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [rootacademy] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [rootacademy] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [rootacademy] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [rootacademy] SET ARITHABORT OFF 
GO
ALTER DATABASE [rootacademy] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [rootacademy] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [rootacademy] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [rootacademy] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [rootacademy] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [rootacademy] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [rootacademy] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [rootacademy] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [rootacademy] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [rootacademy] SET  DISABLE_BROKER 
GO
ALTER DATABASE [rootacademy] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [rootacademy] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [rootacademy] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [rootacademy] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [rootacademy] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [rootacademy] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [rootacademy] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [rootacademy] SET RECOVERY FULL 
GO
ALTER DATABASE [rootacademy] SET  MULTI_USER 
GO
ALTER DATABASE [rootacademy] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [rootacademy] SET DB_CHAINING OFF 
GO
ALTER DATABASE [rootacademy] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [rootacademy] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [rootacademy] SET DELAYED_DURABILITY = DISABLED 
GO
USE [rootacademy]
GO
/****** Object:  User [rootacademysuper]    Script Date: 7/12/2020 3:19:08 PM ******/
CREATE USER [rootacademysuper] FOR LOGIN [rootacademysuper] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [rootacademysuper]
GO
/****** Object:  Table [dbo].[Bitacora]    Script Date: 7/12/2020 3:19:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Bitacora](
	[actividad] [varchar](200) NULL,
	[fecha] [datetime] NULL,
	[mensaje] [varchar](200) NULL,
	[tipoCriticidad] [varchar](50) NULL,
	[bitacoraID] [int] IDENTITY(1,1) NOT NULL,
	[usuarioID] [int] NULL,
 CONSTRAINT [PK_Bitacora] PRIMARY KEY CLUSTERED 
(
	[bitacoraID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DVV]    Script Date: 7/12/2020 3:19:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DVV](
	[nombreTabla] [varchar](50) NULL,
	[valor] [int] NULL,
	[idDVV] [int] NOT NULL,
 CONSTRAINT [PK_DVV] PRIMARY KEY CLUSTERED 
(
	[idDVV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[JoinUsuarioPermiso]    Script Date: 7/12/2020 3:19:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JoinUsuarioPermiso](
	[usuarioID] [int] NOT NULL,
	[permisoID] [int] NOT NULL,
 CONSTRAINT [PK_JounUsuarioPermiso] PRIMARY KEY CLUSTERED 
(
	[usuarioID] ASC,
	[permisoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Permiso]    Script Date: 7/12/2020 3:19:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Permiso](
	[permisoID] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](100) NULL,
	[codigo] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Permiso] PRIMARY KEY CLUSTERED 
(
	[permisoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Permiso_Jerarquia]    Script Date: 7/12/2020 3:19:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permiso_Jerarquia](
	[IdPadrePermiso] [int] NOT NULL,
	[IdHijoPermiso] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Persona]    Script Date: 7/12/2020 3:19:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Persona](
	[personaID] [int] IDENTITY(1,1) NOT NULL,
	[dni] [int] NOT NULL,
	[apellido] [varchar](50) NOT NULL,
	[usuarioID] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[sexo] [char](1) NOT NULL,
	[eliminado] [bit] NOT NULL CONSTRAINT [DF_Persona_eliminado]  DEFAULT ((0)),
	[DVH] [int] NOT NULL,
 CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED 
(
	[personaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 7/12/2020 3:19:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[usuarioID] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[eliminado] [bit] NOT NULL CONSTRAINT [DF_Usuario_eliminado]  DEFAULT ((0)),
	[DVH] [int] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[usuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Bitacora] ON 

INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-09 19:22:09.907' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2072, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-09 19:23:41.157' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2073, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-09 23:16:49.900' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2074, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-09 23:16:55.543' AS DateTime), N'Se detecto un login incorrecto', N'Media', 2075, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-09 23:16:55.557' AS DateTime), N'Ocurrio un error en el login de usuario Login_messagebox_error_login', N'Media', 2076, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-09 23:16:59.807' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2077, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-09 21:14:45.823' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2078, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-09 23:35:27.130' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2079, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-09 23:39:15.483' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2080, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-09 23:45:01.963' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2081, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-09 23:49:21.790' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2082, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-09 23:53:39.243' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2083, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 00:05:11.577' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2084, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 00:07:50.090' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2085, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 00:18:24.267' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2086, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 00:26:53.587' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2087, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 00:48:44.523' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2088, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 00:57:02.217' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2089, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 01:49:01.447' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2090, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 01:58:14.490' AS DateTime), N'Se detecto un login incorrecto', N'Media', 2091, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 01:58:14.800' AS DateTime), N'Ocurrio un error en el login de usuario Login_messagebox_error_login', N'Media', 2092, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 01:58:15.603' AS DateTime), N'Se detecto un login incorrecto', N'Media', 2093, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 01:58:15.797' AS DateTime), N'Ocurrio un error en el login de usuario Login_messagebox_error_login', N'Media', 2094, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 01:58:39.547' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2095, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 02:04:09.383' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2096, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 02:05:20.800' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2097, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 05:09:33.453' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2098, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 05:10:01.830' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2099, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 05:11:41.177' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2100, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 05:12:37.367' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2101, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 02:13:36.623' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2102, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 02:32:44.903' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2103, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 02:46:01.830' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2104, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 02:52:47.117' AS DateTime), N'Se detecto un login incorrecto', N'Media', 2105, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 02:52:47.323' AS DateTime), N'Ocurrio un error en el login de usuario Login_messagebox_error_login', N'Media', 2106, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 02:53:11.200' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2107, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 03:06:51.417' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2108, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 03:09:00.910' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2109, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 03:10:35.980' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2110, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 03:11:44.490' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2111, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 06:20:45.537' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2112, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 06:24:58.277' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2113, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 06:25:11.150' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2114, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 03:28:33.570' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2115, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 06:43:06.583' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2116, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 14:39:18.680' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2117, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 15:49:49.170' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2118, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 15:52:23.203' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2119, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 15:58:01.793' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2120, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Desasociacion de Rol', CAST(N'2020-07-10 15:59:30.560' AS DateTime), N'Error al desasociar el rol GE102 del usuario test', N'Media', 2121, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Desasociacion de Rol', CAST(N'2020-07-10 15:59:37.713' AS DateTime), N'Error al desasociar el rol GE102 del usuario test', N'Media', 2122, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Desasociacion de Rol', CAST(N'2020-07-10 15:59:41.687' AS DateTime), N'Error al desasociar el rol GE101 del usuario test', N'Media', 2123, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Creacion de Nutricionista', CAST(N'2020-07-10 16:00:25.390' AS DateTime), N'Error en la creacion de Nutricionista: Ya existe el Usuario / Nutricionista. Verifique los datos.', N'Alta', 2124, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Creacion de Nutricionista', CAST(N'2020-07-10 16:00:37.953' AS DateTime), N'Se creo el nutricionista con usuario: admin', N'Media', 2125, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Logout de Usuario', CAST(N'2020-07-10 16:00:43.993' AS DateTime), N'Se detectó un logout', N'Baja', 2126, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 16:00:50.363' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2127, 39)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Logout de Usuario', CAST(N'2020-07-10 16:00:53.970' AS DateTime), N'Se detectó un logout', N'Baja', 2128, 39)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 16:01:16.447' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2129, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Asociacion de Rol', CAST(N'2020-07-10 16:02:38.940' AS DateTime), N'Se asocio el rol AA099 del usuario admin', N'Media', 2130, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Creacion de Rol', CAST(N'2020-07-10 16:19:16.480' AS DateTime), N'Se creo el rol GE112', N'Alta', 2131, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Creacion de Rol', CAST(N'2020-07-10 16:20:10.530' AS DateTime), N'Se creo el rol OP300', N'Alta', 2132, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Creacion de Rol', CAST(N'2020-07-10 16:20:48.807' AS DateTime), N'Se creo el rol OP301', N'Alta', 2133, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Creacion de Rol', CAST(N'2020-07-10 16:21:28.580' AS DateTime), N'Se creo el rol OP302', N'Alta', 2134, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Creacion de Rol', CAST(N'2020-07-10 16:23:47.870' AS DateTime), N'Se creo el rol GE400', N'Alta', 2135, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Creacion de Rol', CAST(N'2020-07-10 16:24:20.517' AS DateTime), N'Se creo el rol OP210', N'Alta', 2136, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Creacion de Rol', CAST(N'2020-07-10 16:24:54.510' AS DateTime), N'Se creo el rol OP211', N'Alta', 2137, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Creacion de Rol', CAST(N'2020-07-10 16:25:20.510' AS DateTime), N'Se creo el rol OP212', N'Alta', 2138, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Asociacion de Rol', CAST(N'2020-07-10 16:25:49.740' AS DateTime), N'Se asocio el rol GE400 del usuario test', N'Media', 2139, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Asociacion de Rol', CAST(N'2020-07-10 16:26:20.980' AS DateTime), N'Se asocio el rol GE112 del usuario test', N'Media', 2140, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Asociacion de Rol', CAST(N'2020-07-10 16:27:06.193' AS DateTime), N'Se asocio el rol GE112 del usuario admin', N'Media', 2141, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Asociacion de Rol', CAST(N'2020-07-10 16:27:24.267' AS DateTime), N'Se asocio el rol GE400 del usuario admin', N'Media', 2142, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Asociacion de Rol', CAST(N'2020-07-10 16:27:45.030' AS DateTime), N'Se asocio el rol GE100 del usuario admin', N'Media', 2143, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Eliminacion de Usuario', CAST(N'2020-07-10 16:36:06.053' AS DateTime), N'Se elimino correctamente el usuario nutricionista', N'Media', 2144, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Modificacion de Nutricionista', CAST(N'2020-07-10 16:36:36.393' AS DateTime), N'Se modifico el Nutricionista con ID: 24', N'Alta', 2145, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Modificacion de Nutricionista', CAST(N'2020-07-10 16:36:43.350' AS DateTime), N'Se modifico el Nutricionista con ID: 23', N'Alta', 2146, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Modificacion de Nutricionista', CAST(N'2020-07-10 16:36:49.110' AS DateTime), N'Se modifico el Nutricionista con ID: 21', N'Alta', 2147, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Modificacion de Nutricionista', CAST(N'2020-07-10 16:36:55.583' AS DateTime), N'Se modifico el Nutricionista con ID: 12', N'Alta', 2148, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 19:48:24.253' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2149, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 19:48:49.503' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2150, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 19:49:32.770' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2151, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Creacion de Nutricionista', CAST(N'2020-07-10 16:49:57.227' AS DateTime), N'Se creo el nutricionista con usuario: restringido', N'Media', 2152, 1)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 19:50:32.023' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2153, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 19:51:04.303' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2154, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 19:52:07.603' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2155, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 19:52:19.353' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2156, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 20:13:35.213' AS DateTime), N'Se detecto un login incorrecto', N'Media', 2157, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 20:13:35.213' AS DateTime), N'Ocurrio un error en el login de usuario Login_messagebox_error_login', N'Media', 2158, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 20:13:39.577' AS DateTime), N'Se detecto un login incorrecto', N'Media', 2159, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 20:13:39.577' AS DateTime), N'Ocurrio un error en el login de usuario Login_messagebox_error_login', N'Media', 2160, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 20:13:48.577' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2161, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 20:14:02.937' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2162, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Desbloqueo de Usuario', CAST(N'2020-07-10 20:14:02.950' AS DateTime), N'Se desbloqueo el usuario 39', N'Media', 2163, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Calculo de DVVH', CAST(N'2020-07-10 17:20:11.430' AS DateTime), N'Se detecto un error de calculo de DVH para la entidad Nutricionista con ID: 1', N'Alta', 2164, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Calculo de DVVH', CAST(N'2020-07-10 17:21:47.557' AS DateTime), N'Se detecto un error de calculo de DVH para la entidad Nutricionista con ID: 1', N'Alta', 2165, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 17:22:59.497' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2166, 39)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Calculo de DVVH', CAST(N'2020-07-10 17:29:23.650' AS DateTime), N'Se detecto un error de calculo de DVH para la entidad Nutricionista con ID: 1', N'Alta', 2167, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Calculo de DVVH', CAST(N'2020-07-10 17:29:51.620' AS DateTime), N'Se detecto un error de calculo de DVH para la entidad Nutricionista con ID: 1', N'Alta', 2168, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Calculo de DVVH', CAST(N'2020-07-10 17:30:41.017' AS DateTime), N'Se detecto un error de calculo de DVH para la entidad Nutricionista con ID: 2', N'Alta', 2169, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Calculo de DVVH', CAST(N'2020-07-10 17:32:11.650' AS DateTime), N'Se detecto un error de calculo de DVH para la entidad Nutricionista con ID: 3', N'Alta', 2170, NULL)
GO
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 20:33:24.997' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2171, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 20:33:36.123' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2172, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 17:48:59.890' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2173, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 17:49:56.370' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2174, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 17:53:19.737' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2175, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 17:56:56.050' AS DateTime), N'Se detecto un login incorrecto', N'Media', 2176, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 17:56:56.357' AS DateTime), N'Ocurrio un error en el login de usuario Login_messagebox_error_login', N'Media', 2177, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 17:57:14.837' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2178, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Desbloqueo de Usuario', CAST(N'2020-07-10 17:57:15.793' AS DateTime), N'Se desbloqueo el usuario 39', N'Media', 2179, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Calculo de DVVH', CAST(N'2020-07-10 18:17:57.687' AS DateTime), N'Se detecto un error de calculo de DVH para la entidad Usuario con ID: 1', N'Alta', 2180, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Calculo de DVVH', CAST(N'2020-07-10 18:18:17.643' AS DateTime), N'Se detecto un error de calculo de DVH para la entidad Usuario con ID: 1', N'Alta', 2181, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Calculo de DVVH', CAST(N'2020-07-10 18:19:13.357' AS DateTime), N'Se detecto un error de calculo de DVH para la entidad Usuario con ID: 39', N'Alta', 2182, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Calculo de DVVH', CAST(N'2020-07-10 18:19:43.920' AS DateTime), N'Se detecto un error de calculo de DVH para la entidad Usuario con ID: 40', N'Alta', 2183, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Calculo de DVVH', CAST(N'2020-07-10 18:25:25.477' AS DateTime), N'Se detecto un error de calculo de DVH para la entidad Usuario con ID: 40', N'Alta', 2184, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 18:27:50.363' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2185, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 18:37:08.830' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2186, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 18:47:34.750' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2187, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 18:48:17.550' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2188, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 18:58:23.297' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2189, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 18:59:12.947' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2190, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 19:13:47.930' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2191, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 19:40:43.550' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2192, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 19:43:19.250' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2193, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Calculo de DVVH', CAST(N'2020-07-10 19:44:26.300' AS DateTime), N'Se detecto un error de calculo de DVH para la entidad Usuario con ID: 1', N'Alta', 2194, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 19:44:42.410' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2195, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Calculo de DVVH', CAST(N'2020-07-10 19:45:32.070' AS DateTime), N'Se detecto un error de calculo de DVH para la entidad Persona con ID: 1', N'Alta', 2196, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 19:45:50.257' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2197, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 22:53:06.047' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2198, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 23:14:02.707' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2199, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-10 23:14:48.973' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2200, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-11 00:06:30.393' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2201, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-11 00:06:53.223' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2202, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-11 15:15:26.173' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2203, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-12 17:39:31.337' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2204, NULL)
INSERT [dbo].[Bitacora] ([actividad], [fecha], [mensaje], [tipoCriticidad], [bitacoraID], [usuarioID]) VALUES (N'Login de Usuario', CAST(N'2020-07-12 15:05:00.370' AS DateTime), N'Se detecto un evento de ingreso', N'Media', 2205, NULL)
SET IDENTITY_INSERT [dbo].[Bitacora] OFF
INSERT [dbo].[DVV] ([nombreTabla], [valor], [idDVV]) VALUES (N'Usuario', 109297, 1)
INSERT [dbo].[DVV] ([nombreTabla], [valor], [idDVV]) VALUES (N'Persona', 110669, 2)
INSERT [dbo].[JoinUsuarioPermiso] ([usuarioID], [permisoID]) VALUES (1, 36)
INSERT [dbo].[JoinUsuarioPermiso] ([usuarioID], [permisoID]) VALUES (1, 48)
INSERT [dbo].[JoinUsuarioPermiso] ([usuarioID], [permisoID]) VALUES (1, 137)
INSERT [dbo].[JoinUsuarioPermiso] ([usuarioID], [permisoID]) VALUES (1, 143)
INSERT [dbo].[JoinUsuarioPermiso] ([usuarioID], [permisoID]) VALUES (39, 36)
INSERT [dbo].[JoinUsuarioPermiso] ([usuarioID], [permisoID]) VALUES (39, 48)
INSERT [dbo].[JoinUsuarioPermiso] ([usuarioID], [permisoID]) VALUES (39, 137)
INSERT [dbo].[JoinUsuarioPermiso] ([usuarioID], [permisoID]) VALUES (39, 143)
SET IDENTITY_INSERT [dbo].[Permiso] ON 

INSERT [dbo].[Permiso] ([permisoID], [descripcion], [codigo]) VALUES (3, N'Alta de datos de alumnos', N'OP004')
INSERT [dbo].[Permiso] ([permisoID], [descripcion], [codigo]) VALUES (4, N'Modificacion de datos de alumnos', N'OP005')
INSERT [dbo].[Permiso] ([permisoID], [descripcion], [codigo]) VALUES (36, N'Gestión de alumnos', N'GE100')
INSERT [dbo].[Permiso] ([permisoID], [descripcion], [codigo]) VALUES (46, N'Manejo Bitacora', N'OP45')
INSERT [dbo].[Permiso] ([permisoID], [descripcion], [codigo]) VALUES (48, N'Administrador', N'AA099')
INSERT [dbo].[Permiso] ([permisoID], [descripcion], [codigo]) VALUES (130, N'Consultar Alumnos', N'OP043')
INSERT [dbo].[Permiso] ([permisoID], [descripcion], [codigo]) VALUES (137, N'Gestión de Profesores', N'GE112')
INSERT [dbo].[Permiso] ([permisoID], [descripcion], [codigo]) VALUES (138, N'Alta de Profesor', N'OP300')
INSERT [dbo].[Permiso] ([permisoID], [descripcion], [codigo]) VALUES (140, N'Consultar Profesores', N'OP301')
INSERT [dbo].[Permiso] ([permisoID], [descripcion], [codigo]) VALUES (141, N'Borrar Profesor', N'OP302')
INSERT [dbo].[Permiso] ([permisoID], [descripcion], [codigo]) VALUES (143, N'Gestion de Cursos', N'GE400')
INSERT [dbo].[Permiso] ([permisoID], [descripcion], [codigo]) VALUES (144, N'Alta de Curso', N'OP210')
INSERT [dbo].[Permiso] ([permisoID], [descripcion], [codigo]) VALUES (146, N'Baja de Curso', N'OP211')
INSERT [dbo].[Permiso] ([permisoID], [descripcion], [codigo]) VALUES (147, N'Obtener Cursos', N'OP212')
SET IDENTITY_INSERT [dbo].[Permiso] OFF
INSERT [dbo].[Permiso_Jerarquia] ([IdPadrePermiso], [IdHijoPermiso]) VALUES (36, 3)
INSERT [dbo].[Permiso_Jerarquia] ([IdPadrePermiso], [IdHijoPermiso]) VALUES (36, 4)
INSERT [dbo].[Permiso_Jerarquia] ([IdPadrePermiso], [IdHijoPermiso]) VALUES (137, 138)
INSERT [dbo].[Permiso_Jerarquia] ([IdPadrePermiso], [IdHijoPermiso]) VALUES (137, 140)
INSERT [dbo].[Permiso_Jerarquia] ([IdPadrePermiso], [IdHijoPermiso]) VALUES (137, 141)
INSERT [dbo].[Permiso_Jerarquia] ([IdPadrePermiso], [IdHijoPermiso]) VALUES (143, 146)
INSERT [dbo].[Permiso_Jerarquia] ([IdPadrePermiso], [IdHijoPermiso]) VALUES (143, 147)
INSERT [dbo].[Permiso_Jerarquia] ([IdPadrePermiso], [IdHijoPermiso]) VALUES (36, 130)
INSERT [dbo].[Permiso_Jerarquia] ([IdPadrePermiso], [IdHijoPermiso]) VALUES (143, 144)
INSERT [dbo].[Permiso_Jerarquia] ([IdPadrePermiso], [IdHijoPermiso]) VALUES (48, 46)
SET IDENTITY_INSERT [dbo].[Persona] ON 

INSERT [dbo].[Persona] ([personaID], [dni], [apellido], [usuarioID], [nombre], [sexo], [eliminado], [DVH]) VALUES (1, 37171095, N'Nieto', 39, N'Cristian', N'M', 0, 37822)
INSERT [dbo].[Persona] ([personaID], [dni], [apellido], [usuarioID], [nombre], [sexo], [eliminado], [DVH]) VALUES (2, 12345962, N'test', 1, N'test', N'M', 0, 36580)
INSERT [dbo].[Persona] ([personaID], [dni], [apellido], [usuarioID], [nombre], [sexo], [eliminado], [DVH]) VALUES (3, 12345678, N'Lopez', 40, N'Pepe', N'M', 0, 36267)
SET IDENTITY_INSERT [dbo].[Persona] OFF
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([usuarioID], [username], [password], [eliminado], [DVH]) VALUES (1, N'test', N'b08c8c585b6d67164c163767076445d6', 0, 38564)
INSERT [dbo].[Usuario] ([usuarioID], [username], [password], [eliminado], [DVH]) VALUES (39, N'admin', N'21232f297a57a5a743894a0e4a801fc3', 0, 36994)
INSERT [dbo].[Usuario] ([usuarioID], [username], [password], [eliminado], [DVH]) VALUES (40, N'restringido', N'd0f068d3a00292ff0e9a1aa641c76730', 0, 33739)
SET IDENTITY_INSERT [dbo].[Usuario] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Permiso]    Script Date: 7/12/2020 3:19:13 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Permiso] ON [dbo].[Permiso]
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Usuario]    Script Date: 7/12/2020 3:19:13 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Usuario] ON [dbo].[Usuario]
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bitacora]  WITH CHECK ADD  CONSTRAINT [FK_Bitacora_Usuario] FOREIGN KEY([usuarioID])
REFERENCES [dbo].[Usuario] ([usuarioID])
GO
ALTER TABLE [dbo].[Bitacora] CHECK CONSTRAINT [FK_Bitacora_Usuario]
GO
ALTER TABLE [dbo].[JoinUsuarioPermiso]  WITH CHECK ADD  CONSTRAINT [FK_JoinUsuarioPermiso_Permiso] FOREIGN KEY([permisoID])
REFERENCES [dbo].[Permiso] ([permisoID])
GO
ALTER TABLE [dbo].[JoinUsuarioPermiso] CHECK CONSTRAINT [FK_JoinUsuarioPermiso_Permiso]
GO
ALTER TABLE [dbo].[JoinUsuarioPermiso]  WITH CHECK ADD  CONSTRAINT [FK_JoinUsuarioPermiso_Usuario] FOREIGN KEY([usuarioID])
REFERENCES [dbo].[Usuario] ([usuarioID])
GO
ALTER TABLE [dbo].[JoinUsuarioPermiso] CHECK CONSTRAINT [FK_JoinUsuarioPermiso_Usuario]
GO
ALTER TABLE [dbo].[Permiso_Jerarquia]  WITH CHECK ADD  CONSTRAINT [FK_Permiso_Jerarquia_Permiso] FOREIGN KEY([IdPadrePermiso])
REFERENCES [dbo].[Permiso] ([permisoID])
GO
ALTER TABLE [dbo].[Permiso_Jerarquia] CHECK CONSTRAINT [FK_Permiso_Jerarquia_Permiso]
GO
ALTER TABLE [dbo].[Permiso_Jerarquia]  WITH CHECK ADD  CONSTRAINT [FK_Permiso_Jerarquia_Permiso1] FOREIGN KEY([IdHijoPermiso])
REFERENCES [dbo].[Permiso] ([permisoID])
GO
ALTER TABLE [dbo].[Permiso_Jerarquia] CHECK CONSTRAINT [FK_Permiso_Jerarquia_Permiso1]
GO
ALTER TABLE [dbo].[Persona]  WITH CHECK ADD  CONSTRAINT [FK_Persona_Usuario] FOREIGN KEY([usuarioID])
REFERENCES [dbo].[Usuario] ([usuarioID])
GO
ALTER TABLE [dbo].[Persona] CHECK CONSTRAINT [FK_Persona_Usuario]
GO
USE [master]
GO
ALTER DATABASE [rootacademy] SET  READ_WRITE 
GO
