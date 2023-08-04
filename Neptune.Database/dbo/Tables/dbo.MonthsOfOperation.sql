CREATE TABLE [dbo].[MonthsOfOperation](
	[MonthsOfOperationID] [int] NOT NULL CONSTRAINT [PK_MonthsOfOperation_MonthsOfOperationID] PRIMARY KEY,
	[MonthsOfOperationName] [varchar](6) CONSTRAINT [AK_MonthsOfOperation_MonthsOfOperationName] UNIQUE,
	[MonthsOfOperationDisplayName] [varchar](6) CONSTRAINT [AK_MonthsOfOperation_MonthsOfOperationDisplayName] UNIQUE,
	[MonthsOfOperationNereidAlias] [varchar](6) null
)
