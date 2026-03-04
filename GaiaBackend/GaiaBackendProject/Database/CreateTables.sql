USE [Calculator]
GO

/****** Object:  Table [dbo].[CalculationHistory]    Script Date: 04/03/2026 21:35:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CalculationHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FieldA] [nvarchar](100) NULL,
	[FieldB] [nvarchar](100) NULL,
	[OperationCode] [nvarchar](50) NULL,
	[Result] [nvarchar](100) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Symbol] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CalculationHistory] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO


