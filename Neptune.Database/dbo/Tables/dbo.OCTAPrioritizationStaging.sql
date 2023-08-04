CREATE TABLE [dbo].[OCTAPrioritizationStaging](
	[OCTAPrioritizationStagingID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_OCTAPrioritizationStaging_OCTAPrioritizationStagingID] PRIMARY KEY,
	[OCTAPrioritizationKey] [int] NOT NULL CONSTRAINT [AK_OCTAPrioritizationStaging_OCTAPrioritizationKey] UNIQUE,
	[OCTAPrioritizationGeometry] [geometry] NOT NULL,
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
