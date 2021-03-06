SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HRUCharacteristicLandUseCode](
	[HRUCharacteristicLandUseCodeID] [int] NOT NULL,
	[HRUCharacteristicLandUseCodeName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[HRUCharacteristicLandUseCodeDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeID] PRIMARY KEY CLUSTERED 
(
	[HRUCharacteristicLandUseCodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeDisplayName] UNIQUE NONCLUSTERED 
(
	[HRUCharacteristicLandUseCodeDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeName] UNIQUE NONCLUSTERED 
(
	[HRUCharacteristicLandUseCodeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
