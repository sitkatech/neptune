CREATE TABLE #TreatmentBMP
(
	TreatmentBMPName varchar(50) NOT NULL,
	LocationPoint geometry NULL,
	SystemOfRecordID varchar(50) null,
	Notes varchar(1000) NULL
)
GO

--=CONCATENATE("INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, SystemOfRecordID, Notes) VALUES(N'", B7, "', geometry::Point(", E7, ", ", D7, ", 4326), ", K7, ", N'", O7, "')")

INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, SystemOfRecordID, Notes) VALUES(N'OC-F-2193', geometry::Point(-117.9885, 33.7466833333333, 4326), 2193, N'MIDWAY CITY, Cross streets: ADAMS AT 14852 at WASHINGTON E/S, Map Page=828, Map Grid=A3, Filter Type = FILTERRA')
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, SystemOfRecordID, Notes) VALUES(N'OC-F-2197', geometry::Point(-117.984316666667, 33.75035, 4326), 2197, N'MIDWAY CITY, Cross streets: JEFFERSON A/C 14612 at ROOSEVELT W/S, Map Page=828, Map Grid=A3, Filter Type = FILTERRA')
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, SystemOfRecordID, Notes) VALUES(N'OC-F-2195', geometry::Point(-117.9842, 33.7503833333333, 4326), 2195, N'MIDWAY CITY, Cross streets: JEFFERSON AT 14612 at ROOSEVELT E/S, Map Page=828, Map Grid=A3, Filter Type = FILTERRA')
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, SystemOfRecordID, Notes) VALUES(N'OC-F-2199', geometry::Point(-117.9853, 33.7503166666667, 4326), 2199, N'MIDWAY CITY, Cross streets: MONROE AT 14612 at ROOSEVELT W/S, Map Page=828, Map Grid=A3, Filter Type = FILTERRA')
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, SystemOfRecordID, Notes) VALUES(N'OC-F-2200', geometry::Point(-117.9864, 33.7376666666667, 4326), 2200, N'MIDWAY CITY, Cross streets: VAN BUREN at MCFADDEN W/S, Map Page=828, Map Grid=A3, Filter Type = FILTERRA')
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, SystemOfRecordID, Notes) VALUES(N'OC-F-2201', geometry::Point(-117.986366666667, 33.7414, 4326), 2201, N'MIDWAY CITY, Cross streets: VAN BUREN at BISHOP W/S, Map Page=828, Map Grid=A3, Filter Type = FILTERRA')
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, SystemOfRecordID, Notes) VALUES(N'OC-F-2202', geometry::Point(-117.986366666667, 33.7394166666667, 4326), 2202, N'MIDWAY CITY, Cross streets: VAN BUREN at BISHOP W/S, Map Page=828, Map Grid=A3, Filter Type = FILTERRA')
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, SystemOfRecordID, Notes) VALUES(N'OC-F-2194', geometry::Point(-117.98635, 33.7466833333333, 4326), 2194, N'MIDWAY CITY, Cross streets: VAN BUREN A/C at WASHINGTON W/S, Map Page=828, Map Grid=A3, Filter Type = FILTERRA')
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, SystemOfRecordID, Notes) VALUES(N'OC-F-2203', geometry::Point(-117.986366666667, 33.7394166666667, 4326), 2203, N'MIDWAY CITY, Cross streets: VAN BUREN A/C at SCHOOL 15400 W/S, Map Page=828, Map Grid=A3, Filter Type = FILTERRA')
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, SystemOfRecordID, Notes) VALUES(N'OC-F-2196', geometry::Point(-117.9831, 33.75115, 4326), 2196, N'MIDWAY CITY, Cross streets: WILSON AT 14552 at ROOSEVELT E/S, Map Page=828, Map Grid=A3, Filter Type = FILTERRA')
INSERT #TreatmentBMP(TreatmentBMPName, LocationPoint, SystemOfRecordID, Notes) VALUES(N'OC-F-2198', geometry::Point(-117.983083333333, 33.7485, 4326), 2198, N'MIDWAY CITY, Cross streets: WILSON AT 14732 at MADISON W/S, Map Page=828, Map Grid=A3, Filter Type = FILTERRA')



/*
TreatmentBMPTypeID	TreatmentBMPTypeCustomAttributeTypeID	CustomAttributeTypeID	CustomAttributeTypeName							CustomAttributeDataTypeID
35							296										26				Full Trash Capture?										5
35							294										28				Design Capture Volume of Tributary Area					3
35							295										44				Design Goals 											6
35							300										69				Treatment Flowrate										3
35							297										75				Connector Pipe Screen Name and Manufacturer				5
35							466										78				Structural Repair Conducted								5
35							464										79				Mechanical Repair Conducted								5
35							467										84				Percent Trash											3
35							463										85				Percent Green Waste										3
35							465										86				Percent Sediment										3
35							502										90				Number of Inlet Screens									2
35							503										91				Number of Pipe Connector Screens						2
35							509										94				Trash and Debris Storage Volume							3
35							520										96				Model of Inlet Screens									1
35							521										97				Number of Trash Baskets 								2
35							519										98				Model of Trash Baskets									1
35							512										99				Catch Basin Sump Present?								5
35							515										100				Catch Basin Length Parallel to Curbline					3
35							516										101				Inlet Opening Height									3
35							511										102				Catch Basin Depth from Curbline							3
35							513										103				Catch Basin Width Perpendicular to Curb					3
35							514										104				Connector Pipe Diameter									3
35							517										105				Location of Connector Pipe								5
35							518										106				Manhole Access?											5
35							522										107				Total Material Volume Removed (cu-ft)					3
35							523										108				Total Material Volume Removed (gal)						3
35							583										110				Trash Screen Design Variation							6
35							584										111				Inlet Opening Length Along Curbline						3
35							1586									1114			CPS Design Notes										1
35							1587									1115			Maintenance Area										1
35							1588									1116			Outlet													1
35							1589									1117			Priority Land Use										5
*/

insert into dbo.TreatmentBMP(TenantID, StormwaterJurisdictionID,TreatmentBMPTypeID, OwnerOrganizationID, TreatmentBMPName, LocationPoint, Notes, SystemOfRecordID)
select 2 as TenantID, 12 as StormwaterJurisdictionID, 26 as TreatmentBMPTypeID, 26 as OwnerOrganizationID, b.TreatmentBMPName, b.LocationPoint, b.Notes, b.SystemOfRecordID
from #TreatmentBMP b
left join dbo.TreatmentBMP tb on b.TreatmentBMPName = tb.TreatmentBMPName
where tb.TreatmentBMPID is null
