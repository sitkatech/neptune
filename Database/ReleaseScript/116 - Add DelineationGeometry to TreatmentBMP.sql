Create Table dbo.DelineationType(
DelineationTypeID int not null constraint PK_DelineationType_DelineationTypeID primary key,
DelineationTypeName varchar(50) not null constraint AK_DelineationType_DelineationTypeName unique,
DelineationTypeDisplayName varchar(50) not null constraint AK_DelineationType_DelineationTypeDisplayName unique
)
go

Insert into dbo.DelineationType (DelineationTypeID, DelineationTypeName, DelineationTypeDisplayName)
values
(1, 'Centralized', 'Centralized'),
(2, 'Distributed', 'Distributed')
go

Alter table dbo.TreatmentBMP
Add DelineationGeometry geometry null,
DelineationTypeID int null constraint FK_TreatmentBMP_DelineationType_DelineationTypeID foreign key references dbo.DelineationType(DelineationTypeID),
constraint CK_TreatmentBMP_BMPWithDelineationMustHaveDelineationType check (DelineationTypeID is not null or DelineationGeometry is null)



INSERT [dbo].[FieldDefinition] ([FieldDefinitionID], [FieldDefinitionName], [FieldDefinitionDisplayName], [DefaultDefinition], CanCustomizeLabel)
values
(60, 'DelineationType', 'Delineation Type', 'Indicates whether the delineation is distributed or centralized.', 1)

Insert dbo.FieldDefinitionData (FieldDefinitionID)
values (60)