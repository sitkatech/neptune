ALTER TABLE dbo.FieldVisit
ADD IsFieldVisitVerified BIT NULL;
GO


UPDATE dbo.FieldVisit
SET IsFieldVisitVerified = 0;


ALTER TABLE dbo.FieldVisit
ALTER COLUMN IsFieldVisitVerified BIT NOT NULL;