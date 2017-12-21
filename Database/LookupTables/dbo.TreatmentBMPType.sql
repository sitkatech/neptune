delete from dbo.TreatmentBMPType
go

INSERT 
dbo.TreatmentBMPType (TreatmentBMPTypeID, TreatmentBMPTypeName, TreatmentBMPTypeDisplayName, SortOrder, DisplayColor, GlyphIconClass, IsDistributedBMPType) VALUES
 (1, N'DryBasin', N'Dry Basin', 10, N'#935F59', N'wetland', 0),
 (2, N'WetBasin', N'Wet Basin', 20, N'#935F59', N'wetland', 0),
 (3, N'InfiltrationBasin', N'Infiltration Basin', 30, N'#935F59', N'wetland', 0),
 (4, N'TreatmentVault', N'Treatment Vault', 40, N'#935F59', N'water', 0),
 (5, N'CartridgeFilter', N'Cartridge Filter', 50, N'#935F59', N'water', 0),
 (6, N'BedFilter', N'Bed Filter', 60, N'#935F59', N'water', 0),
 (7, N'SettlingBasin', N'Settling Basin', 70, N'#935F59', N'wetland', 0),
 (8, N'BioFilter', N'Biofilter', 80, N'#935F59', N'wetland', 1),
 (9, N'InfiltrationFeature', N'Infiltration Feature', 90, N'#935F59', N'water', 1),
 (10, N'PorousPavement', N'Porous Pavement', 100, N'#935F59', N'bicycle', 1),
 (11, N'SedimentTrap', N'Sediment Trap', 110, N'#935F59', N'water', 1),
 (12, N'DropInlet', N'Drop Inlet', 120, N'#935F59', N'water', 0)

