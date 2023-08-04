MERGE INTO dbo.WaterQualityManagementPlanPermitTerm AS Target
USING (VALUES
(1, 'NorthOCFirstTerm1990', 'North OC 1st Term - 1990'),
(2, 'NorthOCSecondTerm1996', 'North OC 2nd Term - 1996'),
(3, 'NorthOCThirdTerm2002', 'North OC 3rd Term - 2002 (2003 Model WQMP)'),
(4, 'NorthOCFourthTerm2009', 'North OC 4th Term - 2009 (2011 Model WQMP and TGD)'),
(5, 'SouthOCFirstTerm1990', 'South OC 1st Term - 1990'),
(6, 'SouthOCSecondTerm1996', 'South OC 2nd Term - 1996'),
(7, 'SouthOCThirdTerm2002', 'South OC 3rd Term - 2002 (2003 Model WQMP)'),
(8, 'SouthOCFourthTerm2009', 'South OC 4th Term - 2009 (2013 Model WQMP, TGD, and 2012 HMP)'),
(9, 'SouthOCFithTerm2015', 'South OC 5th Term - 2015 (2017 Model WQMP, TGD, and HMP)')
)
AS Source (WaterQualityManagementPlanPermitTermID, WaterQualityManagementPlanPermitTermName, WaterQualityManagementPlanPermitTermDisplayName)
ON Target.WaterQualityManagementPlanPermitTermID = Source.WaterQualityManagementPlanPermitTermID
WHEN MATCHED THEN
UPDATE SET
	WaterQualityManagementPlanPermitTermName = Source.WaterQualityManagementPlanPermitTermName,
	WaterQualityManagementPlanPermitTermDisplayName = Source.WaterQualityManagementPlanPermitTermDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (WaterQualityManagementPlanPermitTermID, WaterQualityManagementPlanPermitTermName, WaterQualityManagementPlanPermitTermDisplayName)
	VALUES (WaterQualityManagementPlanPermitTermID, WaterQualityManagementPlanPermitTermName, WaterQualityManagementPlanPermitTermDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;