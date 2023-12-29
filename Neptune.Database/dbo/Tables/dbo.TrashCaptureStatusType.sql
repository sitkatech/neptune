CREATE TABLE [dbo].[TrashCaptureStatusType](
	[TrashCaptureStatusTypeID] [int] NOT NULL CONSTRAINT [PK_TrashCaptureStatusType_TrashCaptureStatusTypeID] PRIMARY KEY,
	[TrashCaptureStatusTypeName] [varchar](100) CONSTRAINT [AK_TrashCaptureStatusType_TrashCaptureStatusTypeName] UNIQUE,
	[TrashCaptureStatusTypeDisplayName] [varchar](100) CONSTRAINT [AK_TrashCaptureStatusType_TrashCaptureStatusTypeDisplayName] UNIQUE,
	[TrashCaptureStatusTypeSortOrder] [int] NOT NULL,
	[TrashCaptureStatusTypePriority] [int] NOT NULL,
	[TrashCaptureStatusTypeColorCode] [varchar](6)
)
