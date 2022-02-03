CREATE TABLE dbo.ProjectDocument(
	ProjectDocumentID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ProjectDocument_ProjectDocumentID PRIMARY KEY,
	FileResourceID int NOT NULL CONSTRAINT FK_ProjectDocument_FileResource_FileResourceID FOREIGN KEY REFERENCES dbo.FileResource(FileResourceID),
	ProjectID int NOT NULL CONSTRAINT FK_ProjectDocument_Project_ProjectID FOREIGN KEY REFERENCES dbo.Project(ProjectID),
	DisplayName varchar(200) NOT NULL,
	UploadDate date NOT NULL,
	DocumentDescription varchar(500) NULL
)

insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName)
values
(59, 'HippocampProjectAttachments', 'Hippocamp Project Attachments')

insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent)
values
(59, '<p>Welcome! This is a friendly place to upload attachments for your project.</p>')