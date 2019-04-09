-- Add field to OVTA to replace the TransectBackingAssessmentID field on OVTAA
Alter table dbo.OnlandVisualTrashAssessment
Add IsTransectBackingAssessment bit null
go

Update dbo.OnlandVisualTrashAssessment
set IsTransectBackingAssessment = 0
go

Alter table dbo.OnlandVisualTrashAssessment
alter column IsTransectBackingAssessment bit not null
go

-- update the OVTA table setting the bit corectly.
Update ass
set ass.IsTransectBackingAssessment = 1
from
OnlandVisualTrashAssessmentArea area inner join OnlandVisualTrashAssessment ass on area.TransectBackingAssessmentID = ass.OnlandVisualTrashAssessmentID

-- ensure that at most one assessment per area is transect-backing
CREATE UNIQUE NONCLUSTERED INDEX CK_OnlandVisualTrashAssessment_AtMostOneTransectBackingAssessmentPerArea
ON dbo.OnlandVisualTrashAssessment(OnlandVisualTrashAssessmentAreaID)
WHERE IsTransectBackingAssessment = 1

--ensure that only completed assessments are transect-backing
Alter Table dbo.OnlandVisualTrashAssessment
Add Constraint CK_OnlandVisualTrashAssessment_TransectBackingAssessmentMustBeComplete
Check ((IsTransectBackingAssessment = 0 OR OnlandVisualTrashAssessmentStatusID = 2) AND NOT (IsTransectBackingAssessment = 1 AND OnlandVisualTrashAssessmentStatusID <> 2) )

-- remove the TransectBackingAssessmentID field from OVTAA
Alter table dbo.OnlandVisualTrashAssessmentArea
Drop Constraint FK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessment_TransectBackingAssessmentID_OnlandVisualTrashAssessmentID

Alter Table dbo.OnlandVisualTrashAssessmentArea
Drop Column TransectBackingAssessmentID