ALTER TABLE TreatmentBMP
ADD InventoryVerifiedByPersonID INT NULL CONSTRAINT FK_FieldVisit_Person_InventoryVerifiedByPersonID_PersonID FOREIGN KEY REFERENCES dbo.Person (PersonID);



ALTER TABLE TreatmentBMP
ADD InventoryLastChangedDate DATETIME NULL;