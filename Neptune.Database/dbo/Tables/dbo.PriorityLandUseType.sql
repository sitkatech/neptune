CREATE TABLE [dbo].[PriorityLandUseType](
	[PriorityLandUseTypeID] [int] NOT NULL CONSTRAINT [PK_PriorityLandUseType_PriorityLandUseTypeID] PRIMARY KEY,
	[PriorityLandUseTypeName] [varchar](100) CONSTRAINT [AK_PriorityLandUseType_PriorityLandUseTypeName] UNIQUE,
	[PriorityLandUseTypeDisplayName] [varchar](100) CONSTRAINT [AK_PriorityLandUseType_PriorityLandUseTypeDisplayName] UNIQUE,
	[MapColorHexCode] [varchar](7)
)
