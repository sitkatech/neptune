CREATE TABLE [dbo].[RoutingConfiguration](
	[RoutingConfigurationID] [int] NOT NULL CONSTRAINT [PK_RoutingConfiguration_RoutingConfigurationID] PRIMARY KEY,
	[RoutingConfigurationName] [varchar](100) CONSTRAINT [AK_RoutingConfiguration_RoutingConfigurationName] UNIQUE,
	[RoutingConfigurationDisplayName] [varchar](100) CONSTRAINT [AK_RoutingConfiguration_RoutingConfigurationDisplayName] UNIQUE
)