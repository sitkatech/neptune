

ALTER TABLE dbo.[DirtyModelNode]  WITH CHECK ADD  CONSTRAINT [FK_DirtyModelNode_Dileneation_DelineationID] FOREIGN KEY(DelineationID)
REFERENCES [dbo].[Delineation] (DelineationID)
GO

ALTER TABLE [dbo].[DirtyModelNode] CHECK CONSTRAINT [FK_DirtyModelNode_Dileneation_DelineationID]
GO

ALTER TABLE dbo.[DirtyModelNode]  WITH CHECK ADD  CONSTRAINT [FK_DirtyModelNode_RegionalSubbasin_RegionalSubbasinID] FOREIGN KEY(RegionalSubbasinID)
REFERENCES [dbo].RegionalSubbasin (RegionalSubbasinID)
GO

ALTER TABLE [dbo].[DirtyModelNode] CHECK CONSTRAINT [FK_DirtyModelNode_RegionalSubbasin_RegionalSubbasinID]
GO



ALTER TABLE dbo.[DirtyModelNode]  WITH CHECK ADD  CONSTRAINT [FK_DirtyModelNode_TreatmentBMP_TreatmentBMPID] FOREIGN KEY(TreatmentBMPID)
REFERENCES [dbo].TreatmentBMP (TreatmentBMPID)
GO

ALTER TABLE [dbo].[DirtyModelNode] CHECK CONSTRAINT [FK_DirtyModelNode_TreatmentBMP_TreatmentBMPID]
GO


ALTER TABLE dbo.[DirtyModelNode]  WITH CHECK ADD  CONSTRAINT [FK_DirtyModelNode_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY(WaterQualityManagementPlanID)
REFERENCES [dbo].WaterQualityManagementPlan (WaterQualityManagementPlanID)
GO

ALTER TABLE [dbo].[DirtyModelNode] CHECK CONSTRAINT [FK_DirtyModelNode_WaterQualityManagementPlan_WaterQualityManagementPlanID]
GO



ALTER TABLE dbo.NereidResult  WITH CHECK ADD  CONSTRAINT [FK_NereidResult_Delineation_DelineationID] FOREIGN KEY(DelineationID)
REFERENCES [dbo].Delineation (DelineationID)
GO

ALTER TABLE [dbo].NereidResult CHECK CONSTRAINT [FK_NereidResult_Delineation_DelineationID]
GO


ALTER TABLE dbo.NereidResult  WITH CHECK ADD  CONSTRAINT [FK_NereidResult_RegionalSubbasin_RegionalSubbasinID] FOREIGN KEY(RegionalSubbasinID)
REFERENCES [dbo].RegionalSubbasin (RegionalSubbasinID)
GO

ALTER TABLE [dbo].NereidResult CHECK CONSTRAINT [FK_NereidResult_RegionalSubbasin_RegionalSubbasinID]
GO


ALTER TABLE dbo.NereidResult  WITH CHECK ADD  CONSTRAINT [FK_NereidResult_TreatmentBMP_TreatmentBMPID] FOREIGN KEY(TreatmentBMPID)
REFERENCES [dbo].TreatmentBMP (TreatmentBMPID)
GO

ALTER TABLE [dbo].NereidResult CHECK CONSTRAINT [FK_NereidResult_TreatmentBMP_TreatmentBMPID]
GO

ALTER TABLE dbo.NereidResult  WITH CHECK ADD  CONSTRAINT [FK_NereidResult_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY(WaterQualityManagementPlanID)
REFERENCES [dbo].WaterQualityManagementPlan (WaterQualityManagementPlanID)
GO

ALTER TABLE [dbo].NereidResult CHECK CONSTRAINT [FK_NereidResult_WaterQualityManagementPlan_WaterQualityManagementPlanID]
GO



ALTER TABLE dbo.ProjectNereidResult  WITH CHECK ADD  CONSTRAINT [FK_ProjectNereidResult_Delineation_DelineationID] FOREIGN KEY(DelineationID)
REFERENCES [dbo].Delineation (DelineationID)
GO

ALTER TABLE [dbo].ProjectNereidResult CHECK CONSTRAINT [FK_ProjectNereidResult_Delineation_DelineationID]
GO

ALTER TABLE dbo.ProjectNereidResult  WITH CHECK ADD  CONSTRAINT [FK_ProjectNereidResult_RegionalSubbasin_RegionalSubbasinID] FOREIGN KEY(RegionalSubbasinID)
REFERENCES [dbo].RegionalSubbasin (RegionalSubbasinID)
GO

ALTER TABLE [dbo].ProjectNereidResult CHECK CONSTRAINT [FK_ProjectNereidResult_RegionalSubbasin_RegionalSubbasinID]
GO


ALTER TABLE dbo.ProjectNereidResult  WITH CHECK ADD  CONSTRAINT [FK_ProjectNereidResult_TreatmentBMP_TreatmentBMPID] FOREIGN KEY(TreatmentBMPID)
REFERENCES [dbo].TreatmentBMP (TreatmentBMPID)
GO

ALTER TABLE [dbo].ProjectNereidResult CHECK CONSTRAINT [FK_ProjectNereidResult_TreatmentBMP_TreatmentBMPID]
GO

ALTER TABLE dbo.ProjectNereidResult  WITH CHECK ADD  CONSTRAINT [FK_ProjectNereidResult_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY(WaterQualityManagementPlanID)
REFERENCES [dbo].WaterQualityManagementPlan (WaterQualityManagementPlanID)
GO

ALTER TABLE [dbo].ProjectNereidResult CHECK CONSTRAINT [FK_ProjectNereidResult_WaterQualityManagementPlan_WaterQualityManagementPlanID]
GO



ALTER TABLE dbo.TrashGeneratingUnit4326  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnit4326_Delineation_DelineationID] FOREIGN KEY(DelineationID)
REFERENCES [dbo].Delineation (DelineationID)
GO

ALTER TABLE [dbo].TrashGeneratingUnit4326 CHECK CONSTRAINT [FK_TrashGeneratingUnit4326_Delineation_DelineationID]
GO

ALTER TABLE dbo.TrashGeneratingUnit4326  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnit4326_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID] FOREIGN KEY(OnlandVisualTrashAssessmentAreaID)
REFERENCES [dbo].OnlandVisualTrashAssessmentArea (OnlandVisualTrashAssessmentAreaID)
GO

ALTER TABLE [dbo].TrashGeneratingUnit4326 CHECK CONSTRAINT [FK_TrashGeneratingUnit4326_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID]
GO

ALTER TABLE dbo.TrashGeneratingUnit4326  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnit4326_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY(WaterQualityManagementPlanID)
REFERENCES [dbo].WaterQualityManagementPlan (WaterQualityManagementPlanID)
GO

ALTER TABLE [dbo].TrashGeneratingUnit4326 CHECK CONSTRAINT [FK_TrashGeneratingUnit4326_WaterQualityManagementPlan_WaterQualityManagementPlanID]
GO

