CREATE TABLE [dbo].[County](
	[CountyID] [int] NOT NULL CONSTRAINT [PK_County_CountyID] PRIMARY KEY,
	[CountyName] [varchar](100),
	[StateProvinceID] [int] NOT NULL CONSTRAINT [FK_County_StateProvince_StateProvinceID] FOREIGN KEY REFERENCES [dbo].[StateProvince] ([StateProvinceID]),
	[CountyFeature] [geometry] NULL,
	CONSTRAINT [AK_County_CountyName_StateProvinceID] UNIQUE ([CountyName] ASC, [StateProvinceID] ASC)
)
