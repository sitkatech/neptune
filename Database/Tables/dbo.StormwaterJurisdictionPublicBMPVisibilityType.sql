SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StormwaterJurisdictionPublicBMPVisibilityType](
	[StormwaterJurisdictionPublicBMPVisibilityTypeID] [int] NOT NULL,
	[StormwaterJurisdictionPublicBMPVisibilityTypeName] [varchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName] [varchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_StormwaterJurisdictionPublicBMPVisibilityType_StormwaterJurisdictionPublicBMPVisibilityTypeID] PRIMARY KEY CLUSTERED 
(
	[StormwaterJurisdictionPublicBMPVisibilityTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_StormwaterJurisdictionPublicBMPVisibilityType_StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName] UNIQUE NONCLUSTERED 
(
	[StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_StormwaterJurisdictionPublicBMPVisibilityType_StormwaterJurisdictionPublicBMPVisibilityTypeName] UNIQUE NONCLUSTERED 
(
	[StormwaterJurisdictionPublicBMPVisibilityTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
