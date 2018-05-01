ALTER TABLE dbo.TreatmentBMPAttributeType  drop  CONSTRAINT CK_TreatmentBMPAttributeType_PickListTypeOptionSchemaNotNull

go

ALTER TABLE dbo.TreatmentBMPAttributeType  WITH CHECK ADD  CONSTRAINT CK_TreatmentBMPAttributeType_PickListTypeOptionSchemaNotNull CHECK  ((TreatmentBMPAttributeDataTypeID not in (5, 6) AND TreatmentBMPAttributeTypeOptionsSchema IS NULL) OR (TreatmentBMPAttributeDataTypeID in (5, 6) AND TreatmentBMPAttributeTypeOptionsSchema IS NOT NULL))
GO
ALTER TABLE dbo.TreatmentBMPAttributeType CHECK CONSTRAINT CK_TreatmentBMPAttributeType_PickListTypeOptionSchemaNotNull
GO

ALTER TABLE dbo.TreatmentBMPAttribute ADD  CONSTRAINT AK_TreatmentBMPAttribute_TreatmentBMPAttributeID_TenantID UNIQUE NONCLUSTERED 
(
	TreatmentBMPAttributeID ASC,
	TenantID ASC
)
go

CREATE TABLE dbo.TreatmentBMPAttributeValue(
	TreatmentBMPAttributeValueID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_TreatmentBMPAttributeValue_TreatmentBMPAttributeValueID PRIMARY KEY,
	TenantID int NOT NULL CONSTRAINT FK_TreatmentBMPAttributeValue_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
	TreatmentBMPAttributeID int NOT NULL CONSTRAINT FK_TreatmentBMPAttributeValue_TreatmentBMPAttribute_TreatmentBMPAttributeID FOREIGN KEY REFERENCES dbo.TreatmentBMPAttribute (TreatmentBMPAttributeID),
	AttributeValue varchar(1000) NOT NULL,
	CONSTRAINT FK_TreatmentBMPAttributeValue_TreatmentBMPAttribute_TreatmentBMPAttributeID_TenantID foreign key (TreatmentBMPAttributeID, TenantID) references dbo.TreatmentBMPAttribute (TreatmentBMPAttributeID, TenantID)
)

insert into dbo.TreatmentBMPAttributeValue 
select tbmpa.TenantID, tbmpa.TreatmentBMPAttributeID, tbmpa.TreatmentBMPAttributeValue 
from dbo.TreatmentBMPAttribute tbmpa 

go

alter table dbo.TreatmentBMPAttribute drop column TreatmentBMPAttributeValue

