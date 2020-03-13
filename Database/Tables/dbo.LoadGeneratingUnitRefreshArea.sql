SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoadGeneratingUnitRefreshArea](
	[LoadGeneratingUnitRefreshAreaID] [int] IDENTITY(1,1) NOT NULL,
	[LoadGeneratingUnitRefreshAreaGeometry] [geometry] NOT NULL,
	[ProcessDate] [datetime] NULL,
 CONSTRAINT [PK_LoadGeneratingUnitRefreshArea_LoadGeneratingUnitRefreshAreaID] PRIMARY KEY CLUSTERED 
(
	[LoadGeneratingUnitRefreshAreaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
