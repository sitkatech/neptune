SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NeptunePage](
	[NeptunePageID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[NeptunePageTypeID] [int] NOT NULL,
	[NeptunePageContent] [dbo].[html] NULL,
 CONSTRAINT [PK_NeptunePage_NeptunePageID] PRIMARY KEY CLUSTERED 
(
	[NeptunePageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_NeptunePage_NeptunePageID_TenantID] UNIQUE NONCLUSTERED 
(
	[NeptunePageID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_NeptunePage_NeptunePageTypeID_TenantID] UNIQUE NONCLUSTERED 
(
	[NeptunePageTypeID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[NeptunePage]  WITH CHECK ADD  CONSTRAINT [FK_NeptunePage_NeptunePageType_NeptunePageTypeID] FOREIGN KEY([NeptunePageTypeID])
REFERENCES [dbo].[NeptunePageType] ([NeptunePageTypeID])
GO
ALTER TABLE [dbo].[NeptunePage] CHECK CONSTRAINT [FK_NeptunePage_NeptunePageType_NeptunePageTypeID]
GO
ALTER TABLE [dbo].[NeptunePage]  WITH CHECK ADD  CONSTRAINT [FK_NeptunePage_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[NeptunePage] CHECK CONSTRAINT [FK_NeptunePage_Tenant_TenantID]