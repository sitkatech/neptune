SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ObservationType](
	[ObservationTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[ObservationTypeName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ObservationTypeSpecificationID] [int] NOT NULL,
	[ObservationTypeSchema] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_ObservationType_ObservationTypeID] PRIMARY KEY CLUSTERED 
(
	[ObservationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ObservationType_ObservationTypeID_TenantID] UNIQUE NONCLUSTERED 
(
	[ObservationTypeID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ObservationType_ObservationTypeName] UNIQUE NONCLUSTERED 
(
	[ObservationTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[ObservationType]  WITH CHECK ADD  CONSTRAINT [FK_ObservationType_ObservationTypeSpecification_ObservationTypeSpecificationID] FOREIGN KEY([ObservationTypeSpecificationID])
REFERENCES [dbo].[ObservationTypeSpecification] ([ObservationTypeSpecificationID])
GO
ALTER TABLE [dbo].[ObservationType] CHECK CONSTRAINT [FK_ObservationType_ObservationTypeSpecification_ObservationTypeSpecificationID]
GO
ALTER TABLE [dbo].[ObservationType]  WITH CHECK ADD  CONSTRAINT [FK_ObservationType_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[ObservationType] CHECK CONSTRAINT [FK_ObservationType_Tenant_TenantID]