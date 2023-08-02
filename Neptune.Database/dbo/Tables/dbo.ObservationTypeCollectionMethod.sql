CREATE TABLE [dbo].[ObservationTypeCollectionMethod](
	[ObservationTypeCollectionMethodID] [int] NOT NULL,
	[ObservationTypeCollectionMethodName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ObservationTypeCollectionMethodDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SortOrder] [int] NOT NULL,
	[ObservationTypeCollectionMethodDescription] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_ObservationTypeCollectionMethod_ObservationTypeCollectionMethodID] PRIMARY KEY CLUSTERED 
(
	[ObservationTypeCollectionMethodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_ObservationTypeCollectionMethod_ObservationTypeCollectionMethodDisplayName] UNIQUE NONCLUSTERED 
(
	[ObservationTypeCollectionMethodDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_ObservationTypeCollectionMethod_ObservationTypeCollectionMethodName] UNIQUE NONCLUSTERED 
(
	[ObservationTypeCollectionMethodName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
