SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrashCaptureStatusType](
	[TrashCaptureStatusTypeID] [int] NOT NULL,
	[TrashCaptureStatusTypeName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TrashCaptureStatusTypeDisplayName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TrashCaptureStatusTypeSortOrder] [int] NOT NULL,
	[TrashCaptureStatusTypePriority] [int] NOT NULL,
 CONSTRAINT [PK_TrashCaptureStatusType_TrashCaptureStatusTypeID] PRIMARY KEY CLUSTERED 
(
	[TrashCaptureStatusTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TrashCaptureStatusType_TrashCaptureStatusTypeDisplayName] UNIQUE NONCLUSTERED 
(
	[TrashCaptureStatusTypeDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TrashCaptureStatusType_TrashCaptureStatusTypeName] UNIQUE NONCLUSTERED 
(
	[TrashCaptureStatusTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
