SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnderlyingHydrologicSoilGroup](
	[UnderlyingHydrologicSoilGroupID] [int] NOT NULL,
	[UnderlyingHydrologicSoilGroupName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UnderlyingHydrologicSoilGroupDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_UnderlyingHydrologicSoilGroup_UnderlyingHydrologicSoilGroupID] PRIMARY KEY CLUSTERED 
(
	[UnderlyingHydrologicSoilGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_UnderlyingHydrologicSoilGroup_UnderlyingHydrologicSoilGroupDisplayName] UNIQUE NONCLUSTERED 
(
	[UnderlyingHydrologicSoilGroupDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_UnderlyingHydrologicSoilGroup_UnderlyingHydrologicSoilGroupName] UNIQUE NONCLUSTERED 
(
	[UnderlyingHydrologicSoilGroupName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
