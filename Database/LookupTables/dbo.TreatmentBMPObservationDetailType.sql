delete from dbo.TreatmentBMPObservationDetailType
go

INSERT 
dbo.TreatmentBMPObservationDetailType (TreatmentBMPObservationDetailTypeID, TreatmentBMPObservationDetailTypeName, TreatmentBMPObservationDetailTypeDisplayName, ObservationTypeID, SortOrder) VALUES
(1, 'Inlet', 'Inlet', 10, 10),
(2, 'Outlet', 'Outlet', 10, 20),
(3, 'StaffPlate', 'Staff Plate', 3, 30),
(4, 'VaultCapacityStadiaRod', 'Stadia Rod', 4, 40),
(5, 'SedimentTrapCapacityStadiaRod', 'Stadia Rod', 8, 50),
(6, 'DurationOfInfiltration', 'Duration of Infiltration', 6, 60),
(7, 'ConstantHeadPermeameter', 'Constant Head Permeameter (CHP)', 1, 70),
(8, 'Infiltrometer', 'Infiltrometer', 1, 80),
(9, 'UserDefinedInfiltrationMeasurement', 'User Defined Infiltration Measurement', 1, 90),
(10, 'StandingWater', 'Standing Water', 5, 100),
(11, 'VegetativeCoverWetlandAndRiparianSpecies', 'Wetland & Riparian Species', 2, 110),
(12, 'VegetativeCoverTreeSpecies', 'Tree Species', 2, 120),
(13, 'VegetativeCoverGrassSpecies', 'Grass Species', 2, 130),
(14, 'WetBasinVegetativeCoverWetlandAndRiparianSpecies', 'Wetland & Riparian Species', 9, 140),
(15, 'WetBasinVegetativeCoverTreeSpecies', 'Tree Species', 9, 150),
(16, 'WetBasinVegetativeCoverGrassSpecies', 'Grass Species', 9, 160),
(17, 'Installation', 'Installation', 11, 170)
