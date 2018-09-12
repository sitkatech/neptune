SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HydrologicSubarea](
	[HydrologicSubareaID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[HydrologicSubareaName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_HydrologicSubarea_HydrologicSubareaID] PRIMARY KEY CLUSTERED 
(
	[HydrologicSubareaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_HydrologicSubarea_HydrologicSubareaName] UNIQUE NONCLUSTERED 
(
	[HydrologicSubareaName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[HydrologicSubarea]  WITH CHECK ADD  CONSTRAINT [FK_HydrologicSubarea_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[HydrologicSubarea] CHECK CONSTRAINT [FK_HydrologicSubarea_Tenant_TenantID]