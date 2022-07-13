USE [BankDojoDB]
GO
/****** Object:  Table [dbo].[tblCliente]    Script Date: 7/13/2022 6:55:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCliente](
	[idCliente] [int] IDENTITY(1,1) NOT NULL,
	[idPersonaFK] [int] NOT NULL,
	[contrasena] [nvarchar](300) NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_tblCliente] PRIMARY KEY CLUSTERED 
(
	[idCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCuenta]    Script Date: 7/13/2022 6:55:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCuenta](
	[idCuenta] [int] IDENTITY(1,1) NOT NULL,
	[idClienteFK] [int] NOT NULL,
	[numeroCuenta] [varchar](500) NULL,
	[tipoCuenta] [varchar](50) NULL,
	[saldoInicial] [decimal](16, 2) NULL,
	[estado] [bit] NULL,
 CONSTRAINT [PK_tblCuenta] PRIMARY KEY CLUSTERED 
(
	[idCuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblMovimiento]    Script Date: 7/13/2022 6:55:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMovimiento](
	[idMovimiento] [int] IDENTITY(1,1) NOT NULL,
	[idCuentaFK] [int] NOT NULL,
	[fechaMovimiento] [datetime] NULL,
	[tipoMovimiento] [varchar](50) NULL,
	[valor] [decimal](16, 2) NULL,
	[saldo] [decimal](16, 2) NULL,
 CONSTRAINT [PK_tblMovimiento] PRIMARY KEY CLUSTERED 
(
	[idMovimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPersona]    Script Date: 7/13/2022 6:55:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPersona](
	[idPersona] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](1000) NULL,
	[genero] [varchar](50) NULL,
	[edad] [int] NULL,
	[identificacion] [nvarchar](500) NULL,
	[direccion] [varchar](500) NULL,
	[telefono] [varchar](500) NULL,
 CONSTRAINT [PK_tblPersona] PRIMARY KEY CLUSTERED 
(
	[idPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblCliente]  WITH CHECK ADD  CONSTRAINT [FK_tblCliente_tblPersona1] FOREIGN KEY([idPersonaFK])
REFERENCES [dbo].[tblPersona] ([idPersona])
GO
ALTER TABLE [dbo].[tblCliente] CHECK CONSTRAINT [FK_tblCliente_tblPersona1]
GO
ALTER TABLE [dbo].[tblCuenta]  WITH CHECK ADD  CONSTRAINT [FK_tblCuenta_tblCliente1] FOREIGN KEY([idClienteFK])
REFERENCES [dbo].[tblCliente] ([idCliente])
GO
ALTER TABLE [dbo].[tblCuenta] CHECK CONSTRAINT [FK_tblCuenta_tblCliente1]
GO
ALTER TABLE [dbo].[tblMovimiento]  WITH CHECK ADD  CONSTRAINT [FK_tblMovimiento_tblCuenta1] FOREIGN KEY([idCuentaFK])
REFERENCES [dbo].[tblCuenta] ([idCuenta])
GO
ALTER TABLE [dbo].[tblMovimiento] CHECK CONSTRAINT [FK_tblMovimiento_tblCuenta1]
GO
