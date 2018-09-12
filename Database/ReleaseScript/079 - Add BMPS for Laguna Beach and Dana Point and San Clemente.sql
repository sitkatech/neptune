alter table dbo.TreatmentBMP alter column TreatmentBMPName varchar(200) not null
alter table dbo.TreatmentBMP alter column notes varchar(1000) null
GO

if object_id('tempdb.dbo.#TreatmentBMP') is not null 
    drop table #TreatmentBMP


CREATE TABLE #TreatmentBMP
(
	TreatmentBMPName varchar(200) NOT NULL,
	LocationPoint geometry NULL,
	YearBuilt int null,
	Notes varchar(1000) null,
	TreatmentBMPLifespanType varchar(500),
	RequiredFieldVisitsPerYear int,
	StormdrainOutletID varchar(100), 
	ReceivingSanitationDistrict varchar(500), 
	ImpactedBeachArea varchar(500), 
	PermittedFlow int, 
	StormdrainOutletDiameter int, 
	StormdrainOutletHeight int, 
	DiversionMethod varchar(500), 
	DiversionValve varchar(500), 
	NumberOfPumps int, 
	PretreatmentMethod varchar(500), 
	PretreatmentBMPDefinedSeparately varchar(500), 
	DesignGoals varchar(500), 
	DiversionCapacity int, 
	EstimatedDivertedFlow int, 
	MonthsOfOperation varchar(500), 
	UrbanTributaryArea decimal(6, 2)
)
GO

--=CONCATENATE("INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter", ", StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea)", " VALUES(N'", B5, "', geometry::Point(", D5, ", ", C5, ", 4326), ", I5, ", ", IF(ISBLANK(N5), "NULL", CONCATENATE("'", N5, "'")), ", ", IF(ISBLANK(K5), "NULL", CONCATENATE("'", K5, "'")), ", ", IF(ISBLANK(M5), "NULL", CONCATENATE("'", M5, "'")), ", ", IF(ISBLANK(O5), "NULL", CONCATENATE("'", O5, "'")), ", ", IF(ISBLANK(P5), "NULL", CONCATENATE("'", P5, "'")), ", ", IF(ISBLANK(Q5), "NULL", CONCATENATE("'", Q5, "'")), ", ", IF(ISBLANK(R5), "NULL", R5), ", ", IF(ISBLANK(S5), "NULL", S5), ", ", IF(ISBLANK(T5), "NULL", T5), ", ", IF(ISBLANK(U5), "NULL", CONCATENATE("'", U5, "'")), ", ", IF(ISBLANK(V5), "NULL", CONCATENATE("'", V5, "'")), ", ", IF(ISBLANK(W5), "NULL", W5), ", ", IF(ISBLANK(X5), "NULL", CONCATENATE("'", X5, "'")), ", ", IF(ISBLANK(Y5), "NULL", CONCATENATE("'", Y5, "'")), ", ", IF(ISBLANK(Z5), "NULL", CONCATENATE("'", Z5, "'")), ", ", IF(ISBLANK(AA5), "NULL", CONCATENATE("'", AA5, "'")), ", ", IF(ISBLANK(AB5), "NULL", AB5), ", ", IF(ISBLANK(AC5), "NULL", CONCATENATE("'", AC5, "'")), ", ", IF(ISBLANK(AD5), "NULL", AD5), ")")

INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-2-Irvine Cove', geometry::Point(-117.815665, 33.5545, 4326), 2012, NULL, NULL, NULL, '2', 'City of Laguna Beach', 'Irvine Cove', NULL, 36, NULL, 'Pumped', 'Auto', NULL, 'CDS', 'Yes', 'Dry weather flow reduction', NULL, 7560, NULL, 108)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-5-Circle Way ', geometry::Point(-117.802039, 33.547249, 4326), 2011, NULL, NULL, NULL, '5', 'City of Laguna Beach', 'Crescent Bay Beach', NULL, 18, NULL, 'Pumped', 'Auto', NULL, 'CDS', 'Yes', 'Dry weather flow reduction', NULL, 2660, NULL, 45)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-6-Cresecent Bay', geometry::Point(-117.800975, 33.547342, 4326), 2001, 'Permanent Installation', NULL, NULL, '6', 'City of Laguna Beach', 'Crescent Bay Beach', NULL, 24, NULL, 'Pumped', 'Manual', NULL, 'CDS', 'Yes', 'Dry weather flow reduction', NULL, 1400, 'Year Round', 20)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-9b-Boat Canyon', geometry::Point(-117.795328, 33.545575, 4326), 2008, 'The original project called for a CDS unit in street, could not construct due to right away and project scope issues.  The 72 inch flood control outfall only operates under extreme weather events.  No typical drainage through this unit.', NULL, NULL, '9b', 'City of Laguna Beach', 'Heisler Park Reserve', NULL, 72, NULL, NULL, NULL, NULL, 'None', 'NA', 'Dry weather flow reduction', NULL, 2380, 'Year Round', 45)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-9a-Heisler Park North End/Divers Cove', geometry::Point(-117.794669, 33.545036, 4326), 2008, 'Drain not CDS unit, serves all 45 acres except on extreme flood flows. Seasonal Installation', NULL, NULL, '9a', 'City of Laguna Beach', 'Just outside Heisler Park', NULL, 36, NULL, 'Pumped', NULL, NULL, 'None', 'NA', 'Dry weather flow reduction', NULL, 3150, 'Year Round', 34)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-10-Myrtle at Cliff', geometry::Point(-117.79279, 33.544403, 4326), 2007, 'Permanent Installation', NULL, NULL, '10', 'City of Laguna Beach', 'Heisler Park Reserve (CBI Grant)', NULL, 18, NULL, 'Gravity', 'Auto', NULL, 'CDS', 'Yes', 'Dry weather flow reduction', NULL, 1470, 'Year Round', 21)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-11-Jasmine at Cliff', geometry::Point(-117.790585, 33.543906, 4326), 2003, 'Permanent Installation', NULL, NULL, '11', 'City of Laguna Beach', 'HP Reserve', NULL, 18, NULL, 'Gravity', 'Auto', NULL, 'CDS', 'Yes', 'Dry weather flow reduction', NULL, 2240, 'Year Round', 32)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-12-Aster/Las Brisas at Cliff', geometry::Point(-117.789152, 33.543129, 4326), 2007, 'Permanent Installation', NULL, NULL, '12', 'City of Laguna Beach', 'Heisler Park Reserve (CBI Grant)', NULL, 30, NULL, 'Gravity', 'Auto', NULL, 'CDS', 'Yes', 'Dry weather flow reduction', NULL, 4410, 'Year Round', 63)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-13-North Main Beach', geometry::Point(-117.78649, 33.543071, 4326), 2007, 'check with Wade, changed with construction', NULL, NULL, '13', 'City of Laguna Beach', 'N. Main Beach & HP Reserve (CBI Grant)', NULL, 24, NULL, 'Pumped', NULL, NULL, 'None', 'NA', 'Dry weather flow reduction', NULL, 840, 'April, May, June, July, August, September, October', 12)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-15a-Laguna Canyon Channel - Corp Yard', geometry::Point(-117.781637, 33.546406, 4326), 2007, 'Permanent Installation', NULL, NULL, '15a', 'City of Laguna Beach', 'Main Beach & HP Reserve', NULL, 132, 72, 'Pumped', 'Auto', NULL, 'CDS', 'Yes', 'Dry weather flow reduction', NULL, 140000, 'Year Round', 133)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-15b-Laguna Canyon Channel - Center Main Beach', geometry::Point(-117.785249, 33.542205, 4326), 2007, 'Seasonal, sump pump and trash can', NULL, NULL, '15b', 'City of Laguna Beach', 'Main Beach & HP Reserve (CBI Grant)', NULL, NULL, NULL, 'Pumped', 'Auto', NULL, 'None', 'NA', 'Dry weather flow reduction', NULL, 7140, 'April, May, June, July, August, September, October', 102)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-16-South Main Beach', geometry::Point(-117.783198, 33.541224, 4326), 1998, 'Permanent Installation, hotel Laguna', NULL, NULL, '16', 'City of Laguna Beach', 'S. Main Beach', NULL, 48, NULL, 'Gravity', NULL, NULL, 'None', 'NA', 'Dry weather flow reduction', NULL, 8400, 'Year Round', 120)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-17-Cleo Street', geometry::Point(-117.780339, 33.53721, 4326), 2001, 'Permanent Installation', NULL, NULL, '17', 'City of Laguna Beach', 'Cleo Beach', NULL, 66, NULL, 'Pumped', 'Manual', NULL, 'CDS', 'Yes', 'Dry weather flow reduction', NULL, 14630, 'Year Round', 209)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-20-Anita Street', geometry::Point(-117.778329, 33.534657, 4326), 2003, 'Permanent Installation', NULL, NULL, '20', 'City of Laguna Beach', 'Anita St. Beach', NULL, 24, NULL, 'Pumped', 'Manual', NULL, 'CDS', 'Yes', 'Dry weather flow reduction', NULL, 2310, 'Year Round', 33)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-21-Oak Street', geometry::Point(-117.77769, 33.533828, 4326), 2003, 'Permanent Installation', NULL, NULL, '21', 'City of Laguna Beach', 'Oak St. Beach', NULL, 24, NULL, 'Pumped', 'Manual', NULL, 'CDS', 'Yes', 'Dry weather flow reduction', NULL, 2310, 'Year Round', 33)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-22-Gaviota Drive', geometry::Point(-117.777062, 33.533426, 4326), 2014, 'Permanent Installation', NULL, NULL, '22', 'City of Laguna Beach', 'Brooks St. Beach', NULL, 18, NULL, 'Pumped', 'Manual', NULL, 'CDS', 'Yes', 'Dry weather flow reduction', NULL, 1050, 'Year Round', 15)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-24-Cress Street', geometry::Point(-117.776168, 33.532019, 4326), 2007, 'Permanent Installation', NULL, NULL, '24', 'City of Laguna Beach', 'Cress St. beach (CBI Grant)', NULL, 24, NULL, 'Pumped', 'Manual', NULL, 'CDS', 'Yes', 'Dry weather flow reduction', NULL, 1470, 'Year Round', 21)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-25-Mountain Road', geometry::Point(-117.775412, 33.531404, 4326), 2015, 'Permanent Installation', NULL, NULL, '25', 'City of Laguna Beach', 'Mountain Road Beach', NULL, NULL, NULL, NULL, NULL, NULL, 'CDS', 'Yes', 'Dry weather flow reduction', NULL, 840, 'Year Round', 12)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-27-Bluebird Canyon Drive', geometry::Point(-117.773382, 33.529685, 4326), 1997, 'Concrete Headwall with Board Slots, Seasonal', NULL, NULL, '27', 'City of Laguna Beach', 'Bluebird Beach', NULL, 84, NULL, 'Pumped', NULL, NULL, 'None', 'NA', 'Dry weather flow reduction', NULL, 28140, 'April, May, June, July, August, September, October', 402)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-28-Pearl Street', geometry::Point(-117.771827, 33.528345, 4326), 2003, 'Permanent Installation', NULL, NULL, '28', 'City of Laguna Beach', 'Pearl St. beach', NULL, 48, NULL, 'Pumped', 'Manual', NULL, 'CDS', 'Yes', 'Dry weather flow reduction', NULL, 6970, 'Year Round', 97)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-33-Dumond Drive', geometry::Point(-117.762496, 33.519978, 4326), 2003, 'Permanent Installation', NULL, NULL, '33', 'City of Laguna Beach', 'Victoria Beach', NULL, 54, NULL, 'Gravity', 'Manual', NULL, 'CDS', 'Yes', 'Dry weather flow reduction', NULL, 12250, 'Year Round', 175)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-38-Treasure Island - South', geometry::Point(-117.755048, 33.513046, 4326), 2003, 'Drains to SCWD service area, Permanent Installation', NULL, NULL, '38', 'South Coast Water District', 'Treasure Island', NULL, 24, NULL, 'Gravity', 'Auto', NULL, 'CDS', 'Yes', 'Dry weather flow reduction', NULL, 4410, 'Year Round', 63)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-47-5th Avenue', geometry::Point(-117.743304, 33.500726, 4326), 1999, 'Drains to SCWD service area, 5th Avenue', NULL, NULL, '47', 'South Coast Water District', 'Totuava Bay Beach', NULL, 48, NULL, 'Gravity', NULL, NULL, 'None', 'NA', 'Dry weather flow reduction', NULL, 3150, 'April, May, June, July, August, September, October', 45)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'LB-LFD-48-31861 South Coast Highway', geometry::Point(-117.743206, 33.499544, 4326), NULL, 'Drains to SCWD service area', NULL, NULL, '48', 'South Coast Water District', NULL, NULL, 36, NULL, 'Gravity', NULL, NULL, 'None', 'NA', 'Dry weather flow reduction', NULL, NULL, NULL, 10)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'DP-LFD-Baby Beach', geometry::Point(-117.70669, 33.462488, 4326), 2009, 'Parking Lot south of Cove Road; 4" pvc piping to sewer', 'Perpetuity/Life of Project', '1', '33', 'SCWD-JBL', 'Baby Beach', 10000, NULL, NULL, 'Gravity', 'Manual', NULL, 'Upstream LID or TC BMP', 'Yes', 'Dry weather flow reduction', NULL, NULL, 'April, May, June, July, August, September, October', 35.6)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'DP-LFD-Priority One (Capo Beach)', geometry::Point(-117.669061, 33.455807, 4326), 2007, 'West side of Beach Road, North of Capo Bay District, in gated access road, ; 4" pvc piping to sewer', 'Perpetuity/Life of Project', '1', '20', 'SCWD-JBL', 'Capistrano Beach', 50000, NULL, NULL, 'Gravity', 'Manual', NULL, 'CDS', 'Yes', 'Dry weather flow reduction', NULL, 25000, 'April, May, June, July, August, September, October', 261)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'DP-LFD-North Creek', geometry::Point(-117.688498, 33.46442, 4326), 2003, 'Dana Point Harbor Drive, west side, in gated structure between PCH & Park Lantern; 4" pvc piping to sewer', 'Perpetuity/Life of Project', '1', '27', 'SCWD-JBL', 'Doheny State Beach', 72000, NULL, NULL, 'Pumped', 'Manual', 1, 'CDS', 'Yes', 'Dry weather flow reduction', NULL, 25000, 'April, May, June, July, August, September, October', 150)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'DP-LFD-Del Obispo', geometry::Point(-117.682919, 33.466939, 4326), 2003, 'located behind the Dana Point Community Center at 34052 Del Obispo Street, at the east end of the Del Obispo Channel on the north side of the channel, in a utility vault in gated area across from baseball fields; 4" pvc piping to sewer', 'Perpetuity/Life of Project', '1', '52', 'SCWD-JBL', 'Doheny State Beach', 50000, NULL, NULL, 'Pumped', 'Manual', 1, 'CDS', 'Yes', 'Dry weather flow reduction', NULL, 8000, 'April, May, June, July, August, September, October', 27)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'DP-LFD-Urban Runoff - 1 North of 34760 PCH', geometry::Point(-117.6769, 33.46123, 4326), 2003, 'Approximately 20'' north of 34512-1/2 Coast Highway, near SDG&E Meter; flow limiting plate - 2" opening on 8" PVC to sewer', 'Perpetuity/Life of Project', '1', '26', 'SCWD-JBL', 'Capistrano Beach', 10000, NULL, NULL, 'Gravity', 'Manual', NULL, 'None', 'NA', 'Dry weather flow reduction', NULL, 0, 'April, May, June, July, August, September, October', 2.8)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'DP-LFD-Urban Runoff - 2 Adjacent to PCH, btwn 34766 & 34776', geometry::Point(-117.673469, 33.459167, 4326), 2003, 'Approximately 20'' north of Olamendis Mexican Cuisine at 34660 Pacific Coast Highway; flow limiting plate - 2" opening on 8" PVC to sewer', 'Perpetuity/Life of Project', '1', '24', 'SCWD-JBL', 'Capistrano Beach', 10000, NULL, NULL, 'Gravity', 'Manual', NULL, 'None', 'NA', 'Dry weather flow reduction', NULL, 0, 'April, May, June, July, August, September, October', 1.7)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'DP-LFD-Urban Runoff - 3 South side PCH, west of Palisades', geometry::Point(-117.67117, 33.45706, 4326), 2003, 'North of Palisades Drive along beach access road.  Approximately 300'' north of Lifeguard Tower Number 6 and directly across from the South Coast Water District Maintenance Facility.  Located on the fence line across from Best Western.; flow limiting plate - 2" opening on 8" PVC to sewer', 'Perpetuity/Life of Project', '1', '22', 'SCWD-JBL', 'Capistrano Beach', 10000, NULL, NULL, 'Gravity', 'Manual', NULL, 'None', 'NA', 'Dry weather flow reduction', NULL, 0, 'April, May, June, July, August, September, October', 2.2)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'DP-LFD-Urban Runoff - 4 btwn Beach Rd. & Metrolink', geometry::Point(-117.66619, 33.45415, 4326), 2003, 'On Beach Road in line with the County''s storm drain outlet which outlets onto the Capistrano Beach, approximately 200'' north of the basketball courts. below Pines Park, ATS sign/bridge, manhole #19; flow limiting plate - 2" opening on 8" PVC to sewer', 'Perpetuity/Life of Project', '1', '19', 'SCWD-JBL', 'Capistrano Beach', 10000, NULL, NULL, 'Gravity', 'Manual', NULL, 'None', 'NA', 'Dry weather flow reduction', NULL, 1000, 'April, May, June, July, August, September, October', 4.4)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'DP-LFD-Urban Runoff - 5 5800'' E. of Palisades', geometry::Point(-117.65759, 33.44992, 4326), 2003, 'across from 35355 Beach Road, manhole #12 ; flow limiting plate - 2" opening on 8" PVC to sewer', 'Perpetuity/Life of Project', '1', '12', 'SCWD-JBL', 'Capistrano Beach', 10000, NULL, NULL, 'Gravity', 'Manual', NULL, 'None', 'NA', 'Dry weather flow reduction', NULL, 0, 'April, May, June, July, August, September, October', 6.4)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'DP-LFD-Urban Runoff - 6 6300'' E. of Palisades', geometry::Point(-117.65246, 33.44652, 4326), 2003, 'across from 35595 Beach Road, manhole #6; flow limiting plate - 2" opening on 8" PVC to sewer', 'Perpetuity/Life of Project', '1', '6', 'SCWD-JBL', 'Capistrano Beach', 10000, NULL, NULL, 'Gravity', 'Manual', NULL, 'None', 'NA', 'Dry weather flow reduction', NULL, 1000, 'April, May, June, July, August, September, October', 6.4)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'DP-LFD-Urban Runoff - 7 8000'' E. of Palisades', geometry::Point(-117.65136, 33.44559, 4326), 2003, 'across from 35655 Beach Road, manhole #5; flow limiting plate - 2" opening on 8" PVC to sewer', 'Perpetuity/Life of Project', '1', '5', 'SCWD-JBL', 'Capistrano Beach', 10000, NULL, NULL, 'Gravity', 'Manual', NULL, 'None', 'NA', 'Dry weather flow reduction', NULL, 1000, 'April, May, June, July, August, September, October', 4.4)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'DP-LFD-Urban Runoff - 8 3800'' E. of Palisades', geometry::Point(-117.635026, 33.433151, 4326), 2003, 'to the right of 35787 Beach Road, (a tiki type house), manhole #2; flow limiting plate - 2" opening on 8" PVC to sewer', 'Perpetuity/Life of Project', '1', '2', 'SCWD-JBL', 'Capistrano Beach', 10000, NULL, NULL, 'Gravity', 'Manual', NULL, 'None', 'NA', 'Dry weather flow reduction', NULL, 1000, 'April, May, June, July, August, September, October', 9.4)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'DP-LFD-Urban Runoff - 9 Camino de Estrella', geometry::Point(-117.661551, 33.453098, 4326), 2000, 'in street in front of 35101 Camino de Estrella; 4" pvc piping to sewer', 'Perpetuity/Life of Project', '1', '18', 'SCWD-JBL', 'Capistrano Beach', 10000, NULL, NULL, 'Gravity', 'Manual', NULL, 'None', 'NA', 'Dry weather flow reduction', NULL, 1000, 'April, May, June, July, August, September, October', NULL)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'DP-LFD-Strands Beach', geometry::Point(-117.717114, 33.469289, 4326), 2009, 'in parking lot at far west end of Niguel Shores Homeowner''s Association at end of Niguel Shores Drive.; includes beach shower, 8" pipe with limiting plate (1/4" slots, spaced at 1/2") to sewer', 'Perpetuity/Life of Project', '1', '38', 'SCWD-CTP', 'Strands Beach', 50000, NULL, NULL, 'Gravity', 'Manual', NULL, 'Settling Chamber', 'No', 'Dry weather flow reduction', NULL, 10500, 'April, May, June, July, August, September, October', 58)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'DP-LFD-Headlands - North', geometry::Point(-117.716991, 33.468884, 4326), 2009, 'North end Strand Beach Drive; 3" PVC pipe to sewer (pumped)', 'Perpetuity/Life of Project', '1', '37', 'SCWD-CTP', 'Strands Beach', 50000, NULL, NULL, 'Pumped', 'Manual', 1, 'Upstream LID or TC BMP', 'Yes', 'Dry weather flow reduction', NULL, 5000, 'April, May, June, July, August, September, October', 10.2)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'DP-LFD-Headlands - Central ', geometry::Point(-117.715395, 33.466733, 4326), 2009, 'in intersetion of Strand Beach Drive and Ocean Front Lane; 3" PVC pipe to sewer (pumped), includes beach shower', 'Perpetuity/Life of Project', '1', '36', 'SCWD-CTP', 'Strands Beach', 50000, NULL, NULL, 'Pumped', 'Manual', 1, 'Upstream LID or TC BMP', 'Yes', 'Dry weather flow reduction', NULL, 20000, 'April, May, June, July, August, September, October', 52)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'DP-LFD-Headlands - South', geometry::Point(-117.714313, 33.464555, 4326), 2009, 'South end Strand Beach Drive; 3" PVC pipe to sewer (pumped)', 'Perpetuity/Life of Project', '1', '35', 'SCWD-CTP', 'Strands Beach', 50000, NULL, NULL, 'Pumped', 'Manual', 1, 'Upstream LID or TC BMP', 'Yes', 'Dry weather flow reduction', NULL, 1000, 'April, May, June, July, August, September, October', 8.1)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'DP-LFD-Alipaz', geometry::Point(-117.681127, 33.471344, 4326), 2003, 'located just north of Quail Run on Sycamore Creek Trail, approximately 100 feet west of San Juan Creek in a utility vault; ', 'Perpetuity/Life of Project', '1', '54', 'SCWD-JBL', 'Doheny State Beach', 72000, NULL, NULL, 'Gravity', 'Manual', NULL, 'CDS', 'Yes', 'Dry weather flow reduction', NULL, 30000, 'April, May, June, July, August, September, October', 372)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'SC-LFD-Linda Lane Channel Urban Runoff Diversion', geometry::Point(-117.621955555556, 33.4237138888889, 4326), 2001, NULL, NULL, NULL, 'M00', NULL, 'Linda Lane Beach', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'SC-LFD-Riviera Channel Urban Runoff Diversion', geometry::Point(-117.609386111111, 33.4086638888889, 4326), 2001, NULL, NULL, NULL, 'M00', NULL, 'Riviera Beach', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'SC-LFD-Segunda Deshecha (MO2)', geometry::Point(-117.631583333333, 33.4333527777778, 4326), 2008, NULL, NULL, NULL, 'M02', NULL, 'North Beach', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea) VALUES(N'SC-LFD-North Beach Diversion', geometry::Point(-117.631416666667, 33.4322138888889, 4326), 2009, NULL, NULL, NULL, 'M00', NULL, 'North Beach', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)



/*
TreatmentBMPTypeID	TreatmentBMPTypeCustomAttributeTypeID	CustomAttributeTypeID	CustomAttributeTypeName						CustomAttributeDataTypeID		CustomAttributeDataTypeDisplayName
38								322									26				Full Trash Capture?										5					Pick One from List
38								323									32				Number of Pumps (if applicable)							2					Integer
38								321									35				Diversion Method										5					Pick One from List
38								324									36				Other Design Features 									6					Select Many from List
38								1599								37				Pretreatment Method										6					Select Many from List
38								1598								38				Pretreatment BMP Defined Separately?					5					Pick One from List
38								319									44				Design Goals 											6					Select Many from List
38								320									70				Diversion Capacity										3					Decimal
38								495									78				Structural Repair Conducted								5					Pick One from List
38								493									79				Mechanical Repair Conducted								5					Pick One from List
38								496									84				Percent Trash											3					Decimal
38								492									85				Percent Green Waste										3					Decimal
38								494									86				Percent Sediment										3					Decimal
38								565									107				Total Material Volume Removed (cu-ft)					3					Decimal
38								566									108				Total Material Volume Removed (gal)						3					Decimal
38								1591								1119			Allowable End Date of Installation (if applicable)		4					Date/Time
38								1595								1120			Maintenance Frequency, times per year					2					Integer
38								1603								1121			Stormdrain Outlet ID									1					String
38								1600								1122			Receiving Sanitation District							1					String
38								1594								1123			Impacted Beach Area										1					String
38								1597								1124			Permitted Flow											3					Decimal
38								1601								1125			Stormdrain Outlet Diameter (or width)					3					Decimal
38								1602								1126			Stormdrain Outlet height (if needed)					3					Decimal
38								1592								1127			Diversion Valve											5					Pick One from List
38								1593								1128			Estimated Diverted Flow									3					Decimal
38								1596								1129			Months of Operation										6					Select Many from List
38								1604								1130			Urban Tributary Area									3					Decimal
*/

-- TreatmentBMPName, LocationPoint, YearBuilt, Notes, TreatmentBMPLifespanType, RequiredFieldVisitsPerYear, StormdrainOutletID, ReceivingSanitationDistrict, ImpactedBeachArea, PermittedFlow, StormdrainOutletDiameter, StormdrainOutletHeight, DiversionMethod, DiversionValve, NumberOfPumps, PretreatmentMethod, PretreatmentBMPDefinedSeparately, DesignGoals, DiversionCapacity, EstimatedDivertedFlow, MonthsOfOperation, UrbanTributaryArea

insert into dbo.TreatmentBMP(TenantID, StormwaterJurisdictionID,TreatmentBMPTypeID, OwnerOrganizationID, TreatmentBMPName, LocationPoint, YearBuilt, Notes, RequiredFieldVisitsPerYear, TreatmentBMPLifespanTypeID, InventoryIsVerified)
select 2 as TenantID, 3 as StormwaterJurisdictionID, 38 as TreatmentBMPTypeID, 17 as OwnerOrganizationID, b.TreatmentBMPName, b.LocationPoint, b.YearBuilt, b.Notes, b.RequiredFieldVisitsPerYear, tblt.TreatmentBMPLifespanTypeID, 0 as InventoryIsVerified
from #TreatmentBMP b
left join dbo.TreatmentBMP tb on b.TreatmentBMPName = tb.TreatmentBMPName
left join dbo.TreatmentBMPLifespanType tblt on b.TreatmentBMPLifespanType = tblt.TreatmentBMPLifespanTypeDisplayName
where b.TreatmentBMPName like 'LB%' and tb.TreatmentBMPID is null

insert into dbo.TreatmentBMP(TenantID, StormwaterJurisdictionID,TreatmentBMPTypeID, OwnerOrganizationID, TreatmentBMPName, LocationPoint, YearBuilt, Notes, RequiredFieldVisitsPerYear, TreatmentBMPLifespanTypeID, InventoryIsVerified)
select 2 as TenantID, 2 as StormwaterJurisdictionID, 38 as TreatmentBMPTypeID, 9 as OwnerOrganizationID, b.TreatmentBMPName, b.LocationPoint, b.YearBuilt, b.Notes, b.RequiredFieldVisitsPerYear, tblt.TreatmentBMPLifespanTypeID, 0 as InventoryIsVerified
from #TreatmentBMP b
left join dbo.TreatmentBMP tb on b.TreatmentBMPName = tb.TreatmentBMPName
left join dbo.TreatmentBMPLifespanType tblt on b.TreatmentBMPLifespanType = tblt.TreatmentBMPLifespanTypeDisplayName
where b.TreatmentBMPName like 'DP%' and tb.TreatmentBMPID is null

insert into dbo.TreatmentBMP(TenantID, StormwaterJurisdictionID,TreatmentBMPTypeID, OwnerOrganizationID, TreatmentBMPName, LocationPoint, YearBuilt, Notes, RequiredFieldVisitsPerYear, TreatmentBMPLifespanTypeID, InventoryIsVerified)
select 2 as TenantID, 10 as StormwaterJurisdictionID, 38 as TreatmentBMPTypeID, 29 as OwnerOrganizationID, b.TreatmentBMPName, b.LocationPoint, b.YearBuilt, b.Notes, b.RequiredFieldVisitsPerYear, tblt.TreatmentBMPLifespanTypeID, 0 as InventoryIsVerified
from #TreatmentBMP b
left join dbo.TreatmentBMP tb on b.TreatmentBMPName = tb.TreatmentBMPName
left join dbo.TreatmentBMPLifespanType tblt on b.TreatmentBMPLifespanType = tblt.TreatmentBMPLifespanTypeDisplayName
where b.TreatmentBMPName like 'SC%' and tb.TreatmentBMPID is null


declare @customAttributeTypeID int, @treatmentBMPTypeCustomAttributeTypeID int

-- StormdrainOutletID
select @customAttributeTypeID = 1121, @treatmentBMPTypeCustomAttributeTypeID = 1603

insert into dbo.CustomAttribute(TenantID, TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, TreatmentBMPTypeID, CustomAttributeTypeID)
select b.TenantID, b.TreatmentBMPID, @treatmentBMPTypeCustomAttributeTypeID as TreatmentBMPTypeCustomAttributeTypeID, b.TreatmentBMPTypeID, @customAttributeTypeID as CustomAttributeTypeID
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
left join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.StormdrainOutletID is not null and ca.CustomAttributeID is null

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, tb.StormdrainOutletID
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.StormdrainOutletID is not null

-- ReceivingSanitationDistrict
select @customAttributeTypeID = 1122, @treatmentBMPTypeCustomAttributeTypeID = 1600

insert into dbo.CustomAttribute(TenantID, TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, TreatmentBMPTypeID, CustomAttributeTypeID)
select b.TenantID, b.TreatmentBMPID, @treatmentBMPTypeCustomAttributeTypeID as TreatmentBMPTypeCustomAttributeTypeID, b.TreatmentBMPTypeID, @customAttributeTypeID as CustomAttributeTypeID
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
left join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.ReceivingSanitationDistrict is not null and ca.CustomAttributeID is null

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, tb.ReceivingSanitationDistrict
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.ReceivingSanitationDistrict is not null

-- ImpactedBeachArea
select @customAttributeTypeID = 1123, @treatmentBMPTypeCustomAttributeTypeID = 1594

insert into dbo.CustomAttribute(TenantID, TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, TreatmentBMPTypeID, CustomAttributeTypeID)
select b.TenantID, b.TreatmentBMPID, @treatmentBMPTypeCustomAttributeTypeID as TreatmentBMPTypeCustomAttributeTypeID, b.TreatmentBMPTypeID, @customAttributeTypeID as CustomAttributeTypeID
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
left join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.ImpactedBeachArea is not null and ca.CustomAttributeID is null

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, tb.ImpactedBeachArea
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.ImpactedBeachArea is not null

-- PermittedFlow
select @customAttributeTypeID = 1124, @treatmentBMPTypeCustomAttributeTypeID = 1597

insert into dbo.CustomAttribute(TenantID, TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, TreatmentBMPTypeID, CustomAttributeTypeID)
select b.TenantID, b.TreatmentBMPID, @treatmentBMPTypeCustomAttributeTypeID as TreatmentBMPTypeCustomAttributeTypeID, b.TreatmentBMPTypeID, @customAttributeTypeID as CustomAttributeTypeID
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
left join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.PermittedFlow is not null and ca.CustomAttributeID is null

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, tb.PermittedFlow
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.PermittedFlow is not null

-- StormdrainOutletDiameter
select @customAttributeTypeID = 1125, @treatmentBMPTypeCustomAttributeTypeID = 1601

insert into dbo.CustomAttribute(TenantID, TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, TreatmentBMPTypeID, CustomAttributeTypeID)
select b.TenantID, b.TreatmentBMPID, @treatmentBMPTypeCustomAttributeTypeID as TreatmentBMPTypeCustomAttributeTypeID, b.TreatmentBMPTypeID, @customAttributeTypeID as CustomAttributeTypeID
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
left join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.StormdrainOutletDiameter is not null and ca.CustomAttributeID is null

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, tb.StormdrainOutletDiameter
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.StormdrainOutletDiameter is not null


-- StormdrainOutletHeight
select @customAttributeTypeID = 1126, @treatmentBMPTypeCustomAttributeTypeID = 1602

insert into dbo.CustomAttribute(TenantID, TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, TreatmentBMPTypeID, CustomAttributeTypeID)
select b.TenantID, b.TreatmentBMPID, @treatmentBMPTypeCustomAttributeTypeID as TreatmentBMPTypeCustomAttributeTypeID, b.TreatmentBMPTypeID, @customAttributeTypeID as CustomAttributeTypeID
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
left join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.StormdrainOutletHeight is not null and ca.CustomAttributeID is null

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, tb.StormdrainOutletHeight
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.StormdrainOutletHeight is not null


-- DiversionMethod
select @customAttributeTypeID = 35, @treatmentBMPTypeCustomAttributeTypeID = 321

insert into dbo.CustomAttribute(TenantID, TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, TreatmentBMPTypeID, CustomAttributeTypeID)
select b.TenantID, b.TreatmentBMPID, @treatmentBMPTypeCustomAttributeTypeID as TreatmentBMPTypeCustomAttributeTypeID, b.TreatmentBMPTypeID, @customAttributeTypeID as CustomAttributeTypeID
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
left join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.DiversionMethod is not null and ca.CustomAttributeID is null

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, tb.DiversionMethod
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.DiversionMethod is not null


-- DiversionValve
select @customAttributeTypeID = 1127, @treatmentBMPTypeCustomAttributeTypeID = 1592

insert into dbo.CustomAttribute(TenantID, TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, TreatmentBMPTypeID, CustomAttributeTypeID)
select b.TenantID, b.TreatmentBMPID, @treatmentBMPTypeCustomAttributeTypeID as TreatmentBMPTypeCustomAttributeTypeID, b.TreatmentBMPTypeID, @customAttributeTypeID as CustomAttributeTypeID
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
left join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.DiversionValve is not null and ca.CustomAttributeID is null

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, tb.DiversionValve
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.DiversionValve is not null


-- NumberOfPumps
select @customAttributeTypeID = 32, @treatmentBMPTypeCustomAttributeTypeID = 323

insert into dbo.CustomAttribute(TenantID, TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, TreatmentBMPTypeID, CustomAttributeTypeID)
select b.TenantID, b.TreatmentBMPID, @treatmentBMPTypeCustomAttributeTypeID as TreatmentBMPTypeCustomAttributeTypeID, b.TreatmentBMPTypeID, @customAttributeTypeID as CustomAttributeTypeID
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
left join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.NumberOfPumps is not null and ca.CustomAttributeID is null

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, tb.NumberOfPumps
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.NumberOfPumps is not null



-- PretreatmentMethod
select @customAttributeTypeID = 37, @treatmentBMPTypeCustomAttributeTypeID = 1599

insert into dbo.CustomAttribute(TenantID, TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, TreatmentBMPTypeID, CustomAttributeTypeID)
select b.TenantID, b.TreatmentBMPID, @treatmentBMPTypeCustomAttributeTypeID as TreatmentBMPTypeCustomAttributeTypeID, b.TreatmentBMPTypeID, @customAttributeTypeID as CustomAttributeTypeID
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
left join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.PretreatmentMethod is not null and ca.CustomAttributeID is null

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, tb.PretreatmentMethod
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.PretreatmentMethod is not null



-- PretreatmentBMPDefinedSeparately
select @customAttributeTypeID = 38, @treatmentBMPTypeCustomAttributeTypeID = 1598

insert into dbo.CustomAttribute(TenantID, TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, TreatmentBMPTypeID, CustomAttributeTypeID)
select b.TenantID, b.TreatmentBMPID, @treatmentBMPTypeCustomAttributeTypeID as TreatmentBMPTypeCustomAttributeTypeID, b.TreatmentBMPTypeID, @customAttributeTypeID as CustomAttributeTypeID
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
left join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.PretreatmentBMPDefinedSeparately is not null and ca.CustomAttributeID is null

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, 
	case 
		when tb.PretreatmentBMPDefinedSeparately = 'Yes' then 'Yes, separate BMP record for pretreatment'
		when tb.PretreatmentBMPDefinedSeparately = 'No' then 'No, pretreatment is part of this BMP'
		when tb.PretreatmentBMPDefinedSeparately = 'NA' then 'N/A  No pretreatment'
		else null
	end
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.PretreatmentBMPDefinedSeparately is not null



-- DesignGoals
select @customAttributeTypeID = 44, @treatmentBMPTypeCustomAttributeTypeID = 319

insert into dbo.CustomAttribute(TenantID, TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, TreatmentBMPTypeID, CustomAttributeTypeID)
select b.TenantID, b.TreatmentBMPID, @treatmentBMPTypeCustomAttributeTypeID as TreatmentBMPTypeCustomAttributeTypeID, b.TreatmentBMPTypeID, @customAttributeTypeID as CustomAttributeTypeID
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
left join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.DesignGoals is not null and ca.CustomAttributeID is null

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, tb.DesignGoals
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.DesignGoals is not null


-- EstimatedDivertedFlow
select @customAttributeTypeID = 1128, @treatmentBMPTypeCustomAttributeTypeID = 1593

insert into dbo.CustomAttribute(TenantID, TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, TreatmentBMPTypeID, CustomAttributeTypeID)
select b.TenantID, b.TreatmentBMPID, @treatmentBMPTypeCustomAttributeTypeID as TreatmentBMPTypeCustomAttributeTypeID, b.TreatmentBMPTypeID, @customAttributeTypeID as CustomAttributeTypeID
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
left join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.EstimatedDivertedFlow is not null and ca.CustomAttributeID is null

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, tb.EstimatedDivertedFlow
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.EstimatedDivertedFlow is not null


-- MonthsOfOperation
select @customAttributeTypeID = 1129, @treatmentBMPTypeCustomAttributeTypeID = 1596

insert into dbo.CustomAttribute(TenantID, TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, TreatmentBMPTypeID, CustomAttributeTypeID)
select b.TenantID, b.TreatmentBMPID, @treatmentBMPTypeCustomAttributeTypeID as TreatmentBMPTypeCustomAttributeTypeID, b.TreatmentBMPTypeID, @customAttributeTypeID as CustomAttributeTypeID
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
left join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.MonthsOfOperation is not null and ca.CustomAttributeID is null

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, 'January' as AttributeValue
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.MonthsOfOperation like '%January%'

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, 'February' as AttributeValue
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.MonthsOfOperation like '%February%'

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, 'March' as AttributeValue
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.MonthsOfOperation like '%March%'

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, 'April' as AttributeValue
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.MonthsOfOperation like '%April%'

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, 'May' as AttributeValue
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.MonthsOfOperation like '%May%'

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, 'June' as AttributeValue
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.MonthsOfOperation like '%June%'

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, 'July' as AttributeValue
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.MonthsOfOperation like '%July%'

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, 'August' as AttributeValue
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.MonthsOfOperation like '%August%'

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, 'September' as AttributeValue
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.MonthsOfOperation like '%September%'

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, 'October' as AttributeValue
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.MonthsOfOperation like '%October%'

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, 'November' as AttributeValue
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.MonthsOfOperation like '%November%'

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, 'December' as AttributeValue
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.MonthsOfOperation like '%December%'

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, 'Year Round' as AttributeValue
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.MonthsOfOperation = 'Year Round'



-- UrbanTributaryArea
select @customAttributeTypeID = 1130, @treatmentBMPTypeCustomAttributeTypeID = 1604

insert into dbo.CustomAttribute(TenantID, TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, TreatmentBMPTypeID, CustomAttributeTypeID)
select b.TenantID, b.TreatmentBMPID, @treatmentBMPTypeCustomAttributeTypeID as TreatmentBMPTypeCustomAttributeTypeID, b.TreatmentBMPTypeID, @customAttributeTypeID as CustomAttributeTypeID
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
left join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.UrbanTributaryArea is not null and ca.CustomAttributeID is null

insert into dbo.CustomAttributeValue(TenantID, CustomAttributeID, AttributeValue)
select b.TenantID, ca.CustomAttributeID, tb.UrbanTributaryArea
from #TreatmentBMP tb
join dbo.TreatmentBMP b on tb.TreatmentBMPName = b.TreatmentBMPName
join dbo.CustomAttribute ca on b.TreatmentBMPID = ca.TreatmentBMPID and ca.CustomAttributeTypeID = @customAttributeTypeID
where tb.UrbanTributaryArea is not null

