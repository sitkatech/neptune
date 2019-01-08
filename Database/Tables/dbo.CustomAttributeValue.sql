SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomAttributeValue](
	[CustomAttributeValueID] [int] IDENTITY(1,1) NOT NULL,
	[CustomAttributeID] [int] NOT NULL,
	[AttributeValue] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_CustomAttributeValue_CustomAttributeValueID] PRIMARY KEY CLUSTERED 
(
	[CustomAttributeValueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[CustomAttributeValue]  WITH CHECK ADD  CONSTRAINT [FK_CustomAttributeValue_CustomAttribute_CustomAttributeID] FOREIGN KEY([CustomAttributeID])
REFERENCES [dbo].[CustomAttribute] ([CustomAttributeID])
GO
ALTER TABLE [dbo].[CustomAttributeValue] CHECK CONSTRAINT [FK_CustomAttributeValue_CustomAttribute_CustomAttributeID]