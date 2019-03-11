SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomAttributeTypePurpose](
	[CustomAttributeTypePurposeID] [int] NOT NULL,
	[CustomAttributeTypePurposeName] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CustomAttributeTypePurposeDisplayName] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_CustomAttributeTypePurpose_CustomAttributeTypePurposeID] PRIMARY KEY CLUSTERED 
(
	[CustomAttributeTypePurposeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_CustomAttributeTypePurpose_CustomAttributeTypePurposeDisplayName] UNIQUE NONCLUSTERED 
(
	[CustomAttributeTypePurposeDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_CustomAttributeTypePurpose_CustomAttributeTypePurposeName] UNIQUE NONCLUSTERED 
(
	[CustomAttributeTypePurposeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
