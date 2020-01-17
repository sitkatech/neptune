SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DroolToolWatershed](
	[DroolToolWatershedID] [int] IDENTITY(1,1) NOT NULL,
	[DroolToolWatershedGeometry] [geometry] NULL,
	[DroolToolWatershedName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DroolToolWatershedGeometry4326] [geometry] NULL,
 CONSTRAINT [PK_DroolToolWatershed_DroolToolWatershedID] PRIMARY KEY CLUSTERED 
(
	[DroolToolWatershedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
