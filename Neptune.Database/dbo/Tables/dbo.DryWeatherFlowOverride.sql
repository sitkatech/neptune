CREATE TABLE [dbo].[DryWeatherFlowOverride](
	[DryWeatherFlowOverrideID] [int] NOT NULL CONSTRAINT [PK_DryWeatherFlowOverride_DryWeatherFlowOverrideID] PRIMARY KEY,
	[DryWeatherFlowOverrideName] [varchar](100) CONSTRAINT [AK_DryWeatherFlowOverride_DryWeatherFlowOverrideName] UNIQUE,
	[DryWeatherFlowOverrideDisplayName] [varchar](100) CONSTRAINT [AK_DryWeatherFlowOverride_DryWeatherFlowOverrideDisplayName] UNIQUE
)
