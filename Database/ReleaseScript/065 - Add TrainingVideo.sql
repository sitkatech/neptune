insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID) values
(29, 'Training', 'Training', 2)

INSERT INTO dbo.NeptunePage (TenantID, NeptunePageTypeID, NeptunePageContent)
VALUES (2, 29, null)

--Create TrainingVideo table
CREATE TABLE dbo.TrainingVideo
(
	TrainingVideoID INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_TrainingVideo_TrainingVideoID PRIMARY KEY,
	TenantID INT NOT NULL CONSTRAINT FK_TrainingVideo_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
	VideoName varchar(100) NOT NULL,
	VideoDescription varchar(500) NULL,
	VideoURL varchar(100) NOT NULL,
	Constraint AK_TrainingVideo_TrainingVideoID_TenantID unique(TrainingVideoID, TenantID)
)
GO

--add values
INSERT INTO dbo.TrainingVideo (TenantID, VideoName, VideoDescription, VideoURL)
VALUES 
(2, 'Tool Overview', null, 'https://youtu.be/ElXmMBq0sLc'),
(2, 'Getting Started', null, 'https://youtu.be/SIAr2Ts8KH4'),
(2, 'Finding a BMP', null, 'https://youtu.be/3EMlKbGyfv8'),
(2, 'Adding a BMP', null, 'https://youtu.be/zmNfk7iwv5A'),
(2, 'Performing a Field Visit', null, 'https://youtu.be/2F25tiWU0GU'),
(2, 'Adding a WQMP', null, 'https://yo0utu.be/iMcQz4xHqlA');