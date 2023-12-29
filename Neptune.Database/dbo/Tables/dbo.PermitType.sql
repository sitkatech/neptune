CREATE TABLE [dbo].[PermitType](
	[PermitTypeID] [int] NOT NULL CONSTRAINT [PK_PermitType_PermitTypeID] PRIMARY KEY,
	[PermitTypeName] [varchar](100) CONSTRAINT [AK_PermitType_PermitTypeName] UNIQUE,
	[PermitTypeDisplayName] [varchar](100) CONSTRAINT [AK_PermitType_PermitTypeDisplayName] UNIQUE
)
