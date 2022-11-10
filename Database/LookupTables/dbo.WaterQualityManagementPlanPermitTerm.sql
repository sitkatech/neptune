DELETE FROM dbo.WaterQualityManagementPlanPermitTerm;
GO

INSERT INTO dbo.WaterQualityManagementPlanPermitTerm(WaterQualityManagementPlanPermitTermID, WaterQualityManagementPlanPermitTermName, WaterQualityManagementPlanPermitTermDisplayName, SortOrder)
VALUES
(1, 'NorthOCFirstTerm1990', 'North OC 1st Term - 1990', 10),
(2, 'NorthOCSecondTerm1996', 'North OC 2nd Term - 1996', 20),
(3, 'NorthOCThirdTerm2002', 'North OC 3rd Term - 2002 (2003 Model WQMP)', 30),
(4, 'NorthOCFourthTerm2009', 'North OC 4th Term - 2009 (2011 Model WQMP and TGD)', 40),
(5, 'SouthOCFirstTerm1990', 'South OC 1st Term - 1990', 50),
(6, 'SouthOCSecondTerm1996', 'South OC 2nd Term - 1996', 60),
(7, 'SouthOCThirdTerm2002', 'South OC 3rd Term - 2002 (2003 Model WQMP)', 70),
(8, 'SouthOCFourthTerm2009', 'South OC 4th Term - 2009 (2013 Model WQMP, TGD, and 2012 HMP)', 80),
(9, 'SouthOCFithTerm2015', 'South OC 5th Term - 2015 (2017 Model WQMP, TGD, and HMP)', 90);