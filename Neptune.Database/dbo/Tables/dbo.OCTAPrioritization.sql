CREATE TABLE [dbo].[OCTAPrioritization](
	[OCTAPrioritizationID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_OCTAPrioritization_OCTAPrioritizationID] PRIMARY KEY,
	[OCTAPrioritizationKey] [int] NOT NULL CONSTRAINT [AK_OCTAPrioritization_OCTAPrioritizationKey] UNIQUE,
	[OCTAPrioritizationGeometry] [geometry] NOT NULL,
	[OCTAPrioritizationGeometry4326] [geometry] NULL,
	[LastUpdate] [datetime] NOT NULL,
	[Watershed] [varchar](80),
	[CatchIDN] [varchar](80),
	[TPI] [float] NOT NULL,
	[WQNLU] [float] NOT NULL,
	[WQNMON] [float] NOT NULL,
	[IMPAIR] [float] NOT NULL,
	[MON] [float] NOT NULL,
	[SEA] [float] NOT NULL,
	[SEA_PCTL] [varchar](80),
	[PC_VOL_PCT] [float] NOT NULL,
	[PC_NUT_PCT] [float] NOT NULL,
	[PC_BAC_PCT] [float] NOT NULL,
	[PC_MET_PCT] [float] NOT NULL,
	[PC_TSS_PCT] [float] NOT NULL
)
GO

create spatial index [SPATIAL_OCTAPrioritization_OCTAPrioritizationGeometry] on [dbo].[OCTAPrioritization]
(
	[OCTAPrioritizationGeometry]
)
with (BOUNDING_BOX=(1.82653e+006, 636160, 1.89215e+006, 699354))
GO

create spatial index [SPATIAL_OCTAPrioritization_OCTAPrioritizationGeometry4326] on [dbo].[OCTAPrioritization]
(
	[OCTAPrioritizationGeometry4326]
)
with (BOUNDING_BOX=(-119, 33, -117, 34))