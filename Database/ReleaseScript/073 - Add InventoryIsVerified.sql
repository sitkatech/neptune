ALTER TABLE dbo.TreatmentBMP
ADD InventoryIsVerified BIT NULL;
GO


UPDATE dbo.TreatmentBMP
SET InventoryIsVerified = 0;


ALTER TABLE dbo.TreatmentBMP
ALTER COLUMN InventoryIsVerified BIT NOT NULL;