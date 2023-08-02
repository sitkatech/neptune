CREATE TABLE [dbo].[OCTAPrioritizationStaging](
	[OCTAPrioritizationStagingID] [int] IDENTITY(1,1) NOT NULL,
	[OCTAPrioritizationKey] [int] NOT NULL,
	[OCTAPrioritizationGeometry] [geometry] NOT NULL,
	[Watershed] [varchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CatchIDN] [varchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TPI] [float] NOT NULL,
	[WQNLU] [float] NOT NULL,
	[WQNMON] [float] NOT NULL,
	[IMPAIR] [float] NOT NULL,
	[MON] [float] NOT NULL,
	[SEA] [float] NOT NULL,
	[SEA_PCTL] [varchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PC_VOL_PCT] [float] NOT NULL,
	[PC_NUT_PCT] [float] NOT NULL,
	[PC_BAC_PCT] [float] NOT NULL,
	[PC_MET_PCT] [float] NOT NULL,
	[PC_TSS_PCT] [float] NOT NULL,
 CONSTRAINT [PK_OCTAPrioritizationStaging_OCTAPrioritizationStagingID] PRIMARY KEY CLUSTERED 
(
	[OCTAPrioritizationStagingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_OCTAPrioritizationStaging_OCTAPrioritizationKey] UNIQUE NONCLUSTERED 
(
	[OCTAPrioritizationKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
