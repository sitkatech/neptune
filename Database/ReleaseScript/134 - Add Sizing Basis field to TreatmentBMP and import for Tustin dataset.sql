Create Table dbo.SizingBasisType(
SizingBasisTypeID int not null constraint PK_SizingBasisType_SizingBasisTypeID primary key,
SizingBasisTypeName varchar(50) not null constraint AK_SizingBasisType_SizingBasisTypeName unique,
SizingBasisTypeDisplayName varchar(50) not null constraint AK_SizingBasisType_SizingBasisTypeDisplayName unique
)
go

Insert into dbo.SizingBasisType (SizingBasisTypeID,SizingBasisTypeName,SizingBasisTypeDisplayName)
values
(1, 'FullTrashCapture', 'Full Trash Capture'),
(2, 'WaterQuality', 'Water Quality'),
(3, 'Other', 'Other (less than Water Quality)'),
(4, 'NotProvided', 'Not Provided')

Alter table dbo.TreatmentBMP
Add SizingBasisTypeID int null constraint FK_TreatmentBMP_SizingBasisType_SizingBasisTypeID foreign key references dbo.SizingBasisType(SizingBasisTypeID)
go

-- temp data for tustin import

/****** Object:  Table dbo.SizingBasisTemp    Script Date: 3/4/2019 9:08:24 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].SizingBasisTemp(
	[BMP_Name] [nvarchar](50) NOT NULL,
	[Sizing_Basis] [nvarchar](50) NULL
) ON [PRIMARY]
GO

INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-01-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-01-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-01-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-01-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-10-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-10-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-10-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-11-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-11-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-11-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-11-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-11-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-11-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-11-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-11-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-11-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-11-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-11-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-11-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-11-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-11-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-11-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-11-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-08', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-13', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-18', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-21', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-22', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-23', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-24', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-25', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-12-26', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-13-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-13-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-13-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-13-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-13-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-13-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-13-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-13-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-13-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-13-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-13-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-18', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-21', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-22', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-23', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-24', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-25', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-26', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-27', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-28', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-29', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-14-30', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-15-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-15-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-15-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-15-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-15-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-15-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-15-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-15-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-15-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-15-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-15-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-15-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-15-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-15-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-15-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-15-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-15-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-PRP1', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-PRP2', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-16-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-16-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-16-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-16-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-16-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-16-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-16-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-16-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-16-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-16-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-16-11', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-16-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-16-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-16-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-17-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-17-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-17-03', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-17-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-17-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-17-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-17-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-17-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-17-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-17-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-23-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-23-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-23-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-23-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-23-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-23-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-24-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-27-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-24-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-24-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-24-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-24-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-24-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-25-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-25-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-25-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-25-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-25-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-25-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-25-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-25-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-25-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-25-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-25-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-25-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-25-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-25-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-26-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-26-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-26-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-26-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-26-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-26-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-26-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-26-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-26-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-26-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-26-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-26-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-26-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-26-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-26-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-26-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-26-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-26-18', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-26-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-28-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-28-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-28-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-29-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-29-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-29-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-29-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-31-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-31-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-31-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-31-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-31-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-32-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-32-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-32-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-32-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-32-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-32-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-32-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-32-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-32-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-32-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-32-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-32-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-32-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-32-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-32-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-32-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-18', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-21', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-22', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-23', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-24', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-25', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-26', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-27', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-28', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-29', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-30', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-31', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-32', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-33', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-33-34', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-18', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-21', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-22', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-23', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-24', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-25', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-26', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-27', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-28', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-29', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-30', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-31', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-32', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-33', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-34', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-35', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-36', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-37', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-34-38', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-35-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-35-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-35-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-35-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-35-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-35-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-35-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-35-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-35-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-35-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-35-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-35-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-35-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-35-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-35-15', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-35-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-35-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-35-18', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-CT-1', N' ')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-CT-2', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-CT-3', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-CT-4', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-CT-5', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-CT-6', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-36-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-36-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-36-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-36-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-36-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-36-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-36-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-36-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-36-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-36-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-37-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-37-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-37-31', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-37-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-37-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-37-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-37-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-37-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-37-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-37-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-37-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-37-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-37-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-37-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-37-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-37-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-37-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-38-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-38-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-38-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-38-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-38-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-38-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-38-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-38-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-38-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-38-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-38-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-38-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-38-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-38-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-38-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-38-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-38-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-38-18', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-02', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-18', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-21', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-22', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-23', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-24', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-25', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-26', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-27', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-28', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-29', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-39-30', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-21', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-22', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-23', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-24', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-25', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-26', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-27', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-28', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-29', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-40-30', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-HP1', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-41-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-41-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-41-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-41-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-41-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-41-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-41-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-41-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-41-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-41-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-41-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-41-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-41-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-41-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-41-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-41-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-41-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-18', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-120A', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-20', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-21', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-22', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-23', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-24', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-25', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-26', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-27', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-28', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-29', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-30', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-31', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-32', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-33', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-34', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-35', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-36', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-37', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-38', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-39', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-42-40', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-09', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-15', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-17', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-18', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-21', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-22', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-23', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-24', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-25', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-26', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-27', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-28', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-29', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-30', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-31', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-43-32', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-CC1', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-CC2', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-CC3', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-W1', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-W2', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-M1', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-M2', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-M3', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-P1', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-P2', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-21', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-22', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-23', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-24', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-25', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-26', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-27', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-28', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-29', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-30', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-31', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-32', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-33', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-34', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-35', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-36', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-44-37', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-45-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-45-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-45-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-45-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-45-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-45-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-45-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-45-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-45-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-45-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-45-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-45-14', N' ')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-45-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-45-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-18', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-21', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-22', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-23', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-24', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-46-25', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-18', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-21', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-22', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-23', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-24', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-25', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-26', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-27', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-28', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-29', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-30', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-31', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-32', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-33', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-34', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-35', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-36', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-37', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-38', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-47-39', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-48-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-48-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-48-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-48-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-48-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-48-06', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-48-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-48-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-49-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-49-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-49-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-49-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-49-05', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-49-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-49-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-49-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-49-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-49-10', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-49-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-49-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-18', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-21', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-22', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-23', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-24', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-25', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-26', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-27', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-28', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-29', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-30', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-31', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-50-32', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-18', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-21', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-22', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-23', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-24', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-51-25', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-04', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-17', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-18', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-21', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-52-22', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-18', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-21', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-22', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-23', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-24', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-25', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-26', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-53-29', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-54-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-54-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-54-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-54-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-54-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-54-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-54-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-54-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-54-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-54-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-54-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-54-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-54-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-55-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-55-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-55-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-55-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-55-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-55-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-55-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-55-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-55-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-55-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-55-11', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-55-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-55-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-55-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-55-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-55-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-55-18', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-55-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-55-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-55-21', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-01', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-02', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-18', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-56-21', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-CY-1', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-CY-2', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-CY-3', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-CY-4', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-CY-5', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-57-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-57-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-57-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-57-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-57-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-57-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-57-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-57-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-57-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-57-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-57-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-57-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-57-23', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-57-28', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-57-29', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-57-30', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-18', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-21', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-25', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-26', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-29', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-59-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-59-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-59-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-59-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-59-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-59-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-59-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-59-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-59-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-59-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-59-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-59-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-59-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-59-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-59-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-59-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-59-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-22', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-25', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-29', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-31', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-32', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-33', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-35', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-37', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-38', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-39', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-42', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-46', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-47', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-48', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-49', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-50', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-51', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-52', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-53', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-54', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-55', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-60-56', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-61-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-61-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-61-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-61-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-61-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-61-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-61-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-61-35', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-61-37', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-61-39', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-61-40', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-61-41', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-62-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-62-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-62-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-62-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-62-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-62-23', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-62-24', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-62-25', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-62-28', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-62-29', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-62-30', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-62-31', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-61-38', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-63-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-63-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-63-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-63-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-63-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-63-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-63-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-63-10', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-63-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-63-13', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-63-14', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-63-15', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-63-16', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-03', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-04', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-08', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-09', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-14', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-15', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-16', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-17', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-18', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-19', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-20', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-21', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-22', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-23', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-24', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-25', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-26', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-27', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-28', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-29', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-30', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-31', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-32', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-33', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-34', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-35', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-36', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-37', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-38', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-39', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-40', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-41', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-42', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-43', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-44', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-45', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-46', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-64-47', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-15', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-18', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-21', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-22', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-23', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-66-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-66-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-66-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-65-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-66-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-66-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-66-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-66-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-69-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-69-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-69-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-69-06', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-69-11', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-69-12', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-69-13', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-69-14', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-69-15', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-05', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-71-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-71-02', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-71-03', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-71-04', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-71-05', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-71-06', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-71-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-71-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-71-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-71-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-72-01', NULL)
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-72-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-72-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-72-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-72-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-72-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-72-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-72-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-72-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-72-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-72-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-L1', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-L2', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-L3', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-L4', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-L5', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-L6', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-L7', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-L8', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-L9', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-L10', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-L11', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-L12', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-48-09', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-48-10', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-48-13', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-48-14', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-48-15', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-54-12', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-22', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-24', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-58-27', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-61-14', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-62-34', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-ML-1', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-ML-2', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-ML-4', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-ML-5', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-ML-6', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-ML-7', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-ML-8', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-ML-9', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-66-10', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-66-11', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-66-25', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-67-02', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-67-03', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-67-05', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-67-06', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-03', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-04', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-05', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-12', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-13', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-17', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-38-18', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-19', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-20', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-21', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-22', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-23', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-24', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-26', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-27', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-28', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-29', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-30', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-31', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-32', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-33', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-34', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-35', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-68-37', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-69-07', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-69-08', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-69-09', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-69-10', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-01', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-08', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-09', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-10', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-11', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-12', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-13', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-14', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-15', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-16', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-17', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-18', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-19', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-20', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-21', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-22', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-23', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-INL-70-24', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-61-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-61-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-61-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-61-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-61-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-61-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-61-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-62-16', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-62-18', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-62-21', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-62-22', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-66-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-66-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-66-13', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-66-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-68-01', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-68-02', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-69-03', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-69-04', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-71-10', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-71-11', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-71-12', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-71-14', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-71-17', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-71-18', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-71-19', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-71-20', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-71-21', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-71-22', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-71-23', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-71-24', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-71-25', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-72-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-72-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-58-23', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-58-28', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-E1', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-61-14', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-62-33', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-62-35', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-62-36', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-62-37', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-62-38', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-62-39', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-62-40', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-62-41', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-62-42', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-62-43', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-63-12', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-66-15', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-66-16', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-66-17', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-66-18', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-66-19', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-66-20', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-66-21', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-66-22', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-66-23', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-66-24', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-67-01', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-67-04', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-67-07', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-67-08', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-67-09', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-67-10', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-67-11', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-67-12', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-68-06', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-68-07', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-68-08', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-68-09', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-68-10', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-68-11', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-68-14', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-68-15', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-68-16', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-68-25', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-68-36', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-68-38', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-68-39', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-68-40', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-68-41', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-V-1', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-70-01', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-70-02', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-70-03', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-70-04', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-71-26', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-71-27', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-71-28', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-72-05', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-65-05', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-66-06', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-66-07', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-66-08', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-66-09', N'TRASH CAPTURE')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-66-10', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-66-11', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-66-25', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-67-02', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-67-03', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-67-05', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-67-06', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-68-12', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-68-13', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-68-17', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-38-18', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-68-19', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-68-20', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-68-21', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-68-22', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-68-23', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-68-24', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-68-26', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-68-27', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-68-28', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-68-29', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-68-30', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-68-31', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-68-32', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-68-33', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-68-34', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-68-35', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-68-37', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-69-07', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-69-08', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-69-09', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-69-10', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-70-08', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-70-09', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-70-10', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-70-11', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-70-12', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-70-14', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-70-16', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-70-18', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-70-19', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-70-20', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-70-21', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-70-22', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-SW-70-24', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-RR-1', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-32-02', N'WQ/DCV')
GO
INSERT dbo.SizingBasisTemp ([BMP_Name], [Sizing_Basis]) VALUES (N'TUS-PB-ML-3', N'WQ/DCV')
GO

Declare @TrashCapture int = 1;
Declare @WaterQuality int = 2;
Declare @Other int = 3;
Declare @NotProvided int = 4;

update dbo.TreatmentBMP set SizingBasisTypeID = 4


update t
set t.SizingBasisTypeID =
	case
		when sb.Sizing_Basis = 'TRASH CAPTURE' then @TrashCapture
		when sb.Sizing_Basis = 'WQ/DCV' then @WaterQuality
		else @NotProvided
	end
from dbo.TreatmentBMP t inner join dbo.SizingBasisTemp sb on t.TreatmentBMPName = sb.BMP_Name



drop table dbo.SizingBasisTemp

Alter table dbo.TreatmentBMP
Alter column SizingBasisTypeID int not null
go