USE [xyz]
GO
/****** Object:  Table [dbo].[tbl_order]    Script Date: 6/03/2020 11:50:09 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_order](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[day] [nvarchar](10) NOT NULL,
	[request] [nvarchar](20) NOT NULL,
	[typeVehicle] [nvarchar](50) NOT NULL,
	[quantity] [int] NOT NULL,
 CONSTRAINT [PK_tbl_order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tbl_order] ON 
GO
INSERT [dbo].[tbl_order] ([id], [day], [request], [typeVehicle], [quantity]) VALUES (23, N'lunes', N'0001', N'toyota', 9)
GO
SET IDENTITY_INSERT [dbo].[tbl_order] OFF
GO