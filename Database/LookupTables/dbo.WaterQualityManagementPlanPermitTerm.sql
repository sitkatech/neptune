DELETE FROM dbo.WaterQualityManagementPlanPermitTerm;
GO

INSERT INTO dbo.WaterQualityManagementPlanPermitTerm(WaterQualityManagementPlanPermitTermID, WaterQualityManagementPlanPermitTermName, WaterQualityManagementPlanPermitTermDisplayName, SortOrder)
VALUES
(1, 'NorthOC1stTerm', 'North OC 1st Term - 1990', 10),
(2, 'NorthOC2ndTerm', 'North OC 2nd Term - 1996', 20),
(3, 'NorthOC3rdTerm', 'North OC 3rd Term – 2002 (2003 Model WQMP)', 30),
(4, 'NorthOC4thTerm', 'North OC 4th Term - 2009 (2011 Model WQMP and TGD)', 40),
(5, 'SouthOC1stTerm', 'South OC 1st Term - 1990', 50),
(6, 'SouthOC2ndTerm', 'South OC 2nd Term - 1996', 60),
(7, 'SouthOC3rdTerm', 'South OC 3rd Term – 2002 (2003 Model WQMP)', 70),
(8, 'SouthOC4thTerm', 'South OC 4th Term – 2009 (2013 Model WQMP, TGD, and 2012 HMP)', 80),
(9, 'SouthOC5thTerm', 'South OC 5th Term – 2015 (2017 Model WQMP, TGD, and HMP)', 90);