CREATE TABLE [dbo].[CustomAttributeValue](
	[CustomAttributeValueID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_CustomAttributeValue_CustomAttributeValueID] PRIMARY KEY,
	[CustomAttributeID] [int] NOT NULL CONSTRAINT [FK_CustomAttributeValue_CustomAttribute_CustomAttributeID] FOREIGN KEY REFERENCES [dbo].[CustomAttribute] ([CustomAttributeID]),
	[AttributeValue] [varchar](1000) null
)