SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HydromodificationApplies](
	[HydromodificationAppliesID] [int] NOT NULL,
	[HydromodificationAppliesName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[HydromodificationAppliesDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SortOrder] [int] NOT NULL,
 CONSTRAINT [PK_HydromodificationApplies_HydromodificationAppliesID] PRIMARY KEY CLUSTERED 
(
	[HydromodificationAppliesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_HydromodificationApplies_HydromodificationAppliesDisplayName] UNIQUE NONCLUSTERED 
(
	[HydromodificationAppliesDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_HydromodificationApplies_HydromodificationAppliesName] UNIQUE NONCLUSTERED 
(
	[HydromodificationAppliesName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
