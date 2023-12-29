CREATE TABLE [dbo].[HydromodificationAppliesType](
	[HydromodificationAppliesTypeID] [int] NOT NULL CONSTRAINT [PK_HydromodificationAppliesType_HydromodificationAppliesTypeID] PRIMARY KEY,
	[HydromodificationAppliesTypeName] [varchar](100) CONSTRAINT [AK_HydromodificationAppliesType_HydromodificationAppliesTypeName] UNIQUE,
	[HydromodificationAppliesTypeDisplayName] [varchar](100) CONSTRAINT [AK_HydromodificationAppliesType_HydromodificationAppliesTypeDisplayName] UNIQUE
)
