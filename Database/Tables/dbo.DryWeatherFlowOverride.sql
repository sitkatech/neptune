SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DryWeatherFlowOverride](
	[DryWeatherFlowOverrideID] [int] NOT NULL,
	[DryWeatherFlowOverrideName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DryWeatherFlowOverrideDisplayName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_DryWeatherFlowOverride_DryWeatherFlowOverrideID] PRIMARY KEY CLUSTERED 
(
	[DryWeatherFlowOverrideID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_DryWeatherFlowOverride_DryWeatherFlowOverrideDisplayName] UNIQUE NONCLUSTERED 
(
	[DryWeatherFlowOverrideDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_DryWeatherFlowOverride_DryWeatherFlowOverrideName] UNIQUE NONCLUSTERED 
(
	[DryWeatherFlowOverrideName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
