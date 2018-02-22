SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPAttributeTypePurpose](
	[TreatmentBMPAttributeTypePurposeID] [int] NOT NULL,
	[TreatmentBMPAttributeTypePurposeName] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TreatmentBMPAttributeTypePurposeDisplayName] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_TreatmentBMPAttributeTypePurpose_TreatmentBMPAttributeTypePurposeID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPAttributeTypePurposeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPAttributeTypePurpose_TreatmentBMPAttributeTypePurposeDisplayName] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPAttributeTypePurposeDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPAttributeTypePurpose_TreatmentBMPAttributeTypePurposeName] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPAttributeTypePurposeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
