CREATE TABLE [dbo].[CustomAttributeTypePurpose](
	[CustomAttributeTypePurposeID] [int] NOT NULL CONSTRAINT [PK_CustomAttributeTypePurpose_CustomAttributeTypePurposeID] PRIMARY KEY,
	[CustomAttributeTypePurposeName] [varchar](100) CONSTRAINT [AK_CustomAttributeTypePurpose_CustomAttributeTypePurposeName] UNIQUE,
	[CustomAttributeTypePurposeDisplayName] [varchar](100) CONSTRAINT [AK_CustomAttributeTypePurpose_CustomAttributeTypePurposeDisplayName] UNIQUE
)
