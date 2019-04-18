ALTER TABLE [dbo].[OnlandVisualTrashAssessment] DROP CONSTRAINT [CK_OnlandVisualTrashAssessment_NewAssessmentCannotHaveOfficialAreaUnlessComplete]

GO

ALTER TABLE [dbo].[OnlandVisualTrashAssessment] DROP CONSTRAINT [CK_OnlandVisualTrashAssessment_TransectBackingAssessmentMustBeComplete]



