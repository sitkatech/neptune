CREATE TABLE [dbo].[HydrologicSubarea](
	[HydrologicSubareaID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_HydrologicSubarea_HydrologicSubareaID] PRIMARY KEY,
	[HydrologicSubareaName] [varchar](100) CONSTRAINT [AK_HydrologicSubarea_HydrologicSubareaName] UNIQUE
)
