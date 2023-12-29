CREATE TABLE [dbo].[SizingBasisType](
	[SizingBasisTypeID] [int] NOT NULL CONSTRAINT [PK_SizingBasisType_SizingBasisTypeID] PRIMARY KEY,
	[SizingBasisTypeName] [varchar](100) CONSTRAINT [AK_SizingBasisType_SizingBasisTypeName] UNIQUE,
	[SizingBasisTypeDisplayName] [varchar](100) CONSTRAINT [AK_SizingBasisType_SizingBasisTypeDisplayName] UNIQUE
)
