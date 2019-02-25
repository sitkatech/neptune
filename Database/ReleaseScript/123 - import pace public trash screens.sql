create table dbo.PaceTemp(
PaceTempID int not null identity (1,1) constraint PK_PaceTemp primary key,
TreatmentBMPName varchar(50) not null,
LocationPoint geometry not null,
StormwaterJurisdiction varchar(50) not null,
Notes varchar(150) null,
SystemOfRecordID varchar(100) not null,
OwnerOrganization varchar(50) not null,
FullTrashCapture bit not null
)

SET IDENTITY_INSERT [dbo].[PaceTemp] ON 
Go
INSERT [dbo].[PaceTemp] ([PaceTempID], [TreatmentBMPName], [LocationPoint], [StormwaterJurisdiction], [Notes], [SystemOfRecordID], [OwnerOrganization], [FullTrashCapture]) VALUES
(1, N'OC-TS-0780', 0xE6100000010C936A21C614765DC020DBBD9421EB4040, N'County of Orange', N'ORANGE-OLIVE, Cross streets: Main at Olive W/S, Map Page=769, Map Grid=H4, Filter Type =None', N'780', N'County of Orange', 0)
, (2, N'OC-TS-0028', 0xE6100000010CEF3E452247735DC0E83026EADFE44040, N'County of Orange', N'EL MODENA, Cross streets: RANCHO SANTIAGO BLVD at CHAPMAN AVE E/S, Map Page=800, Map Grid=D4, Filter Type =CPS', N'28', N'County of Orange', 1)
, (3, N'OC-TS-1590', 0xE6100000010C5B2C206F30695DC0D059DE655AC84040, N'County of Orange', N'LADERA RANCH, Cross streets: Crown Valley at O''Neil MEDIA, Map Page=922, Map Grid=E6, Filter Type =None', N'1590', N'County of Orange', 0)
, (4, N'OC-TS-1825', 0xE6100000010C64D11DBC4B695DC01FEDB8080FC74040, N'County of Orange', N'LADERA RANCH, Cross streets: O''Neill at Dorance , Map Page=952, Map Grid=E2, Filter Type =None', N'1825', N'County of Orange', 0)
, (5, N'OC-TS-1906', 0xE6100000010C58720B326A685DC088FEDF65B7C54040, N'County of Orange', N'LADERA RANCH, Cross streets: O''Neill at Hydrangea , Map Page=952, Map Grid=E2, Filter Type =None', N'1906', N'County of Orange', 0)
, (6, N'OC-TS-1917', 0xE6100000010CA8F2357E51695DC0106029F12BC64040, N'County of Orange', N'LADERA RANCH, Cross streets: O''Neill at Narrow Canyon , Map Page=952, Map Grid=E2, Filter Type =None', N'1917', N'County of Orange', 0)
, (7, N'OC-TS-1919', 0xE6100000010C6CBAEC4A46695DC0404D7C9F0DC64040, N'County of Orange', N'LADERA RANCH, Cross streets: O''Neill at Eton , Map Page=952, Map Grid=E2, Filter Type =None', N'1919', N'County of Orange', 0)
, (8, N'OC-TS-0080', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS A/C 10991 at JOHNS WAY N/S, Map Page=800, Map Grid=F2, Filter Type =GRATE', N'80', N'County of Orange', 0)
, (9, N'OC-TS-0081', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS A/C 10991 at JOHNS WAY N/S, Map Page=800, Map Grid=F2, Filter Type =B/C', N'81', N'County of Orange', 0)
, (10, N'OC-TS-0083', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS AT 11041 at VICKIE W/S, Map Page=800, Map Grid=F2, Filter Type =B/C', N'83', N'County of Orange', 0)
, (11, N'OC-TS-0084', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS AT 11051 at ORANGE PARK N/S, Map Page=800, Map Grid=F2, Filter Type =B/C', N'84', N'County of Orange', 0)
, (12, N'OC-TS-2232', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS at VICKIE N/S, Map Page=800, Map Grid=F2, Filter Type =B/C', N'2232', N'County of Orange', 0)
, (13, N'OC-TS-2230', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MORADA AT 10821 at ADAMS N/S, Map Page=800, Map Grid=G1, Filter Type =GRATE INLET', N'2230', N'County of Orange', 0)
, (14, N'OC-TS-2229', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MORADA AT 10821 at ADAMS N/S, Map Page=800, Map Grid=G1, Filter Type =GRATE INLET', N'2229', N'County of Orange', 0)
, (15, N'OC-TS-2231', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MORADA AT 10821 at ADAMS N/S, Map Page=800, Map Grid=G1, Filter Type =GRATE INLET', N'2231', N'County of Orange', 0)
, (16, N'OC-TS-2246', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS at ORANGE PARK W/S, Map Page=800, Map Grid=F2, Filter Type =GRATE INLET', N'2246', N'County of Orange', 0)
, (17, N'OC-TS-2233', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS AT 11071 at SHETLAND N/S, Map Page=800, Map Grid=F2, Filter Type =GRATE INLET', N'2233', N'County of Orange', 0)
, (18, N'OC-TS-2234', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS AT 11099 at SHETLAND W/S, Map Page=800, Map Grid=F2, Filter Type =GRATE INLET', N'2234', N'County of Orange', 0)
, (19, N'OC-TS-2235', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS at SHETLAND W/S, Map Page=800, Map Grid=F2, Filter Type =GRATE INLET', N'2235', N'County of Orange', 0)
, (20, N'OC-TS-2236', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS at A/C 11108 W/S, Map Page=800, Map Grid=F2, Filter Type =GRATE INLET', N'2236', N'County of Orange', 0)
, (21, N'OC-TS-2238', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS AT 11107 at MADRID W/S, Map Page=800, Map Grid=F2, Filter Type =GRATE INLET', N'2238', N'County of Orange', 0)
, (22, N'OC-TS-2237', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS AT 11111 at MADRID W/S, Map Page=800, Map Grid=F2, Filter Type =GRATE INLET', N'2237', N'County of Orange', 0)
, (23, N'OC-TS-2240', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS AT 11113 at MADRID W/S, Map Page=800, Map Grid=F2, Filter Type =GRATE INLET', N'2240', N'County of Orange', 0)
, (24, N'OC-TS-2242', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS AT 11115 at MADRID W/S, Map Page=800, Map Grid=F2, Filter Type =GRATE INLET', N'2242', N'County of Orange', 0)
, (25, N'OC-TS-2241', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS AT 11115 at MADRID W/S, Map Page=800, Map Grid=F2, Filter Type =GRATE INLET', N'2241', N'County of Orange', 0)
, (26, N'OC-TS-2243', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: OARNGE PARK at MEADS , Map Page=800, Map Grid=F2, Filter Type =GRATE INLET', N'2243', N'County of Orange', 0)
, (27, N'OC-TS-0296', 0xE6100000010CD0A16EA211725DC040B0815514E34040, N'County of Orange', N'COWAN HEIGHTS/LEMON HEIGHTS, Cross streets: COWAN HEIGHTS at NEWPORT S/S, Map Page=800, Map Grid=F6, Filter Type =None', N'296', N'County of Orange', 0)
, (28, N'OC-TS-0446', 0xE6100000010CA87C65A79E725DC098EDC1C6C3E14040, N'County of Orange', N'COWAN HEIGHTS/LEMON HEIGHTS, Cross streets: FOOTHILL BLVD at ARROYO , Map Page=800, Map Grid=G7, Filter Type =None', N'446', N'County of Orange', 0)
, (29, N'OC-TS-0447', 0xE6100000010CA87C65A79E725DC098EDC1C6C3E14040, N'County of Orange', N'COWAN HEIGHTS/LEMON HEIGHTS, Cross streets: FOOTHILL BLVD at ARROYO , Map Page=800, Map Grid=G7, Filter Type =None', N'447', N'County of Orange', 0)
, (30, N'OC-TS-0448', 0xE6100000010CA87C65A79E725DC098EDC1C6C3E14040, N'County of Orange', N'COWAN HEIGHTS/LEMON HEIGHTS, Cross streets: FOOTHILL BLVD at ARROYO , Map Page=800, Map Grid=G7, Filter Type =None', N'448', N'County of Orange', 0)
, (31, N'OC-TS-0449', 0xE6100000010CA87C65A79E725DC098EDC1C6C3E14040, N'County of Orange', N'COWAN HEIGHTS/LEMON HEIGHTS, Cross streets: FOOTHILL BLVD at ARROYO , Map Page=800, Map Grid=G7, Filter Type =None', N'449', N'County of Orange', 0)
, (32, N'OC-TS-0450', 0xE6100000010CA87C65A79E725DC098EDC1C6C3E14040, N'County of Orange', N'COWAN HEIGHTS/LEMON HEIGHTS, Cross streets: FOOTHILL BLVD at ARROYO , Map Page=800, Map Grid=G7, Filter Type =None', N'450', N'County of Orange', 0)
, (33, N'OC-TS-0451', 0xE6100000010CA87C65A79E725DC098EDC1C6C3E14040, N'County of Orange', N'COWAN HEIGHTS/LEMON HEIGHTS, Cross streets: FOOTHILL BLVD at ARROYO , Map Page=800, Map Grid=G7, Filter Type =None', N'451', N'County of Orange', 0)
, (34, N'OC-TS-0456', 0xE6100000010CA87C65A79E725DC098EDC1C6C3E14040, N'County of Orange', N'COWAN HEIGHTS/LEMON HEIGHTS, Cross streets: FOOTHILL BLVD at ARROYO , Map Page=800, Map Grid=G7, Filter Type =None', N'456', N'County of Orange', 0)
, (35, N'OC-TS-0457', 0xE6100000010CA87C65A79E725DC098EDC1C6C3E14040, N'County of Orange', N'COWAN HEIGHTS/LEMON HEIGHTS, Cross streets: FOOTHILL BLVD at ARROYO , Map Page=800, Map Grid=G7, Filter Type =None', N'457', N'County of Orange', 0)
, (36, N'OC-TS-0006', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS AVE at ACRE RD , Map Page=800, Map Grid=G1, Filter Type =GRATE INLET', N'6', N'County of Orange', 0)
, (37, N'OC-TS-0009', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS AVE at HILLSIDE DR E/S, Map Page=800, Map Grid=G1, Filter Type =CPS', N'9', N'County of Orange', 1)
, (38, N'OC-TS-2239', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS AT 11113 at MADRID W/S, Map Page=800, Map Grid=F2, Filter Type =GRATE INLET', N'2239', N'County of Orange', 0)
, (39, N'OC-TS-2244', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS A/C 11141 at MADRID W/S, Map Page=800, Map Grid=F2, Filter Type =GRATE INLET', N'2244', N'County of Orange', 0)
, (40, N'OC-TS-2245', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS A/C 11192 at ORANGE PARK W/S, Map Page=800, Map Grid=F2, Filter Type =B/C', N'2245', N'County of Orange', 0)
, (41, N'OC-TS-0017', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: MEADS A/C 11192 at ORANGE PARK N/S, Map Page=800, Map Grid=F2, Filter Type =B/C', N'17', N'County of Orange', 0)
, (42, N'OC-TS-0089', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: ORANGE PARK BLVD at AMAPOLA E/S, Map Page=800, Map Grid=f2, Filter Type =B/C', N'89', N'County of Orange', 0)
, (43, N'OC-TS-0090', 0xE6100000010C6027CDC812725DC0B0C4F86CA4E64040, N'County of Orange', N'ORANGE PARK ACRES, Cross streets: ORANGE PARK BLVD at AMAPOLA E/S, Map Page=800, Map Grid=F2, Filter Type =B/C', N'90', N'County of Orange', 0)
, (44, N'OC-TS-1673', 0xE6100000010C387057B78C685DC01890664432C74040, N'County of Orange', N'LADERA RANCH, Cross streets: Snapdragon at BELLFLOWER , Map Page=952, Map Grid=F1, Filter Type =None', N'1673', N'County of Orange', 0)
, (45, N'OC-TS-1420', 0xE6100000010C08526BAF0F695DC010590725D3C84040, N'County of Orange', N'LADERA RANCH, Cross streets: O''Neil at Winfield , Map Page=922, Map Grid=F6, Filter Type =CPS', N'1420', N'County of Orange', 1)
, (46, N'OC-TS-7000', 0xE6100000010C978C3A3042685DC073EE283119C74040, N'County of Orange', N'LADERA RANCH, Cross streets: 1101 Corporate Dr. at Terrace RD. , Map Page=922, Map Grid=G7, Filter Type =None', N'7000', N'County of Orange', 0)
, (47, N'OC-TS-3000', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Sendero at  , Map Page=952, Map Grid=G5, Filter Type =None', N'3000', N'County of Orange', 0)
, (48, N'OC-TS-3001', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: 39 Sendero at Abarrota , Map Page=952, Map Grid=G5, Filter Type =None', N'3001', N'County of Orange', 0)
, (49, N'OC-TS-3002', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Sendero at A/C from 39 , Map Page=952, Map Grid=G5, Filter Type =None', N'3002', N'County of Orange', 0)
, (50, N'OC-TS-3003', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: 11 Sendero at Aldelfa , Map Page=952, Map Grid=G5, Filter Type =None', N'3003', N'County of Orange', 0)
, (51, N'OC-TS-3004', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Sendero at A/C from 11 Aldelfa , Map Page=952, Map Grid=G5, Filter Type =None', N'3004', N'County of Orange', 0)
, (52, N'OC-TS-3005', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Sendero at Corner of Futura , Map Page=952, Map Grid=G5, Filter Type =None', N'3005', N'County of Orange', 0)
, (53, N'OC-TS-3007', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Sendero at Corner of Naciente , Map Page=952, Map Grid=G5, Filter Type =None', N'3007', N'County of Orange', 0)
, (54, N'OC-TS-3006', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Sendero at At Cirlce Ascenso , Map Page=952, Map Grid=G5, Filter Type =None', N'3006', N'County of Orange', 0)
, (55, N'OC-TS-3008', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Acenso at At 1 Lucida , Map Page=952, Map Grid=G5, Filter Type =None', N'3008', N'County of Orange', 0)
, (56, N'OC-TS-3009', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Acenso at A/C CB 3008 Lucido , Map Page=952, Map Grid=G5, Filter Type =None', N'3009', N'County of Orange', 0)
, (57, N'OC-TS-3010', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Acenso at Cielo , Map Page=952, Map Grid=G5, Filter Type =None', N'3010', N'County of Orange', 0)
, (58, N'OC-TS-3011', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Acenso at Alza , Map Page=952, Map Grid=G5, Filter Type =None', N'3011', N'County of Orange', 0)
, (59, N'OC-TS-3012', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Acenso at Alza , Map Page=952, Map Grid=G5, Filter Type =None', N'3012', N'County of Orange', 0)
, (60, N'OC-TS-3013', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Acenso at Frenda , Map Page=952, Map Grid=G5, Filter Type =None', N'3013', N'County of Orange', 0)
, (61, N'OC-TS-3014', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Acenso at Nido , Map Page=952, Map Grid=G5, Filter Type =None', N'3014', N'County of Orange', 0)
, (62, N'OC-TS-3015', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Acenso at Faisan , Map Page=952, Map Grid=G5, Filter Type =None', N'3015', N'County of Orange', 0)
, (63, N'OC-TS-3016', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Acenso at At Cirlce , Map Page=952, Map Grid=G5, Filter Type =None', N'3016', N'County of Orange', 0)
, (64, N'OC-TS-3017', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Acenso at At End of Cirlce , Map Page=952, Map Grid=G5, Filter Type =None', N'3017', N'County of Orange', 0)
, (65, N'OC-TS-3018', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Acenso at At Corner of Cirlce , Map Page=952, Map Grid=G5, Filter Type =None', N'3018', N'County of Orange', 0)
, (66, N'OC-TS-3019', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Ribera at Before Briosa , Map Page=952, Map Grid=G5, Filter Type =None', N'3019', N'County of Orange', 0)
, (67, N'OC-TS-3020', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Ribera at After Briosa , Map Page=952, Map Grid=G5, Filter Type =None', N'3020', N'County of Orange', 0)
, (68, N'OC-TS-3021', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Ribera at A/C From CB 3020 , Map Page=952, Map Grid=G5, Filter Type =None', N'3021', N'County of Orange', 0)
, (69, N'OC-TS-3022', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Ribera at At Corner of Reata , Map Page=952, Map Grid=G5, Filter Type =None', N'3022', N'County of Orange', 0)
, (70, N'OC-TS-3023', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Reata at At Circle , Map Page=952, Map Grid=G5, Filter Type =None', N'3023', N'County of Orange', 0)
, (71, N'OC-TS-3025', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Reata at 100 from Otrega Hwy , Map Page=952, Map Grid=G5, Filter Type =None', N'3025', N'County of Orange', 0)
, (72, N'OC-TS-3024', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Reata at 100 ft After Circle , Map Page=952, Map Grid=G5, Filter Type =None', N'3024', N'County of Orange', 0)
, (73, N'OC-TS-3026', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Corner of Reata at Otrega Hwy , Map Page=952, Map Grid=G5, Filter Type =None', N'3026', N'County of Orange', 0)
, (74, N'OC-TS-3027', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Reata at Otrega Hwy , Map Page=952, Map Grid=G5, Filter Type =None', N'3027', N'County of Orange', 0)
, (75, N'OC-TS-3028', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Reata at Otrega Hwy , Map Page=952, Map Grid=G5, Filter Type =None', N'3028', N'County of Orange', 0)
, (76, N'OC-TS-3029', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Reata at 200 ft From CB 3028 , Map Page=952, Map Grid=G5, Filter Type =None', N'3029', N'County of Orange', 0)
, (77, N'OC-TS-3030', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Reata at 100 ft From CB 3029 , Map Page=952, Map Grid=G5, Filter Type =None', N'3030', N'County of Orange', 0)
, (78, N'OC-TS-3032', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: 15 Briosa at Faisan , Map Page=952, Map Grid=G5, Filter Type =None', N'3032', N'County of Orange', 0)
, (79, N'OC-TS-3031', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: 21 Briosa at Ribera , Map Page=952, Map Grid=G5, Filter Type =None', N'3031', N'County of Orange', 0)
, (80, N'OC-TS-3033', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Briosa at Nido , Map Page=952, Map Grid=G5, Filter Type =None', N'3033', N'County of Orange', 0)
, (81, N'OC-TS-3034', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: 23  Briosa at Ascenso , Map Page=952, Map Grid=G5, Filter Type =None', N'3034', N'County of Orange', 0)
, (82, N'OC-TS-3035', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Amado at Ascenso , Map Page=952, Map Grid=G5, Filter Type =None', N'3035', N'County of Orange', 0)
, (83, N'OC-TS-3036', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Amado at Madaro , Map Page=952, Map Grid=G5, Filter Type =None', N'3036', N'County of Orange', 0)
, (84, N'OC-TS-3037', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Amado at Rollizo , Map Page=952, Map Grid=G5, Filter Type =None', N'3037', N'County of Orange', 0)
, (85, N'OC-TS-3038', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Amado at Glicina , Map Page=952, Map Grid=G5, Filter Type =None', N'3038', N'County of Orange', 0)
, (86, N'OC-TS-3039', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: A/C from 27 Amado at Glicina , Map Page=952, Map Grid=G5, Filter Type =None', N'3039', N'County of Orange', 0)
, (87, N'OC-TS-3040', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: 37 Florear at Zabila , Map Page=952, Map Grid=G5, Filter Type =None', N'3040', N'County of Orange', 0)
, (88, N'OC-TS-3041', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: 20 Florear at Granar , Map Page=952, Map Grid=G5, Filter Type =None', N'3041', N'County of Orange', 0)
, (89, N'OC-TS-3042', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Contigo at Ribera , Map Page=952, Map Grid=G5, Filter Type =None', N'3042', N'County of Orange', 0)
, (90, N'OC-TS-3043', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: A/C 31 Contigo at Maduro , Map Page=952, Map Grid=G5, Filter Type =None', N'3043', N'County of Orange', 0)
, (91, N'OC-TS-3044', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: A/C 21 Contigo at Lavanda , Map Page=952, Map Grid=G5, Filter Type =None', N'3044', N'County of Orange', 0)
, (92, N'OC-TS-3045', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Contigo at Amado , Map Page=952, Map Grid=G5, Filter Type =None', N'3045', N'County of Orange', 0)
, (93, N'OC-TS-3046', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: 33 Ribera at Contigo , Map Page=952, Map Grid=G5, Filter Type =None', N'3046', N'County of Orange', 0)
, (94, N'OC-TS-3047', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: ANTONIO PKWY at After bridge , Map Page=952, Map Grid=G5, Filter Type =None', N'3047', N'County of Orange', 0)
, (95, N'OC-TS-3048', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: ANTONIO PKWY at 40ft from Cow Camp , Map Page=952, Map Grid=G5, Filter Type =None', N'3048', N'County of Orange', 0)
, (96, N'OC-TS-3049', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: ANTONIO PKWY Island at A/C CB 3048 , Map Page=952, Map Grid=G5, Filter Type =None', N'3049', N'County of Orange', 0)
, (97, N'OC-TS-3050', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: ANTONIO PKWY Island at Cow Camp , Map Page=952, Map Grid=G5, Filter Type =None', N'3050', N'County of Orange', 0)
, (98, N'OC-TS-3051', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: ANTONIO PKWY at 100ft from Cow Camp , Map Page=952, Map Grid=G5, Filter Type =None', N'3051', N'County of Orange', 0)
, (99, N'OC-TS-3052', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: ANTONIO PKWY at 100ft from Sendero , Map Page=952, Map Grid=G5, Filter Type =None', N'3052', N'County of Orange', 0)
, (100, N'OC-TS-3054', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: ANTONIO PKWY at 100ft from CB 3053 E/S, Map Page=952, Map Grid=G5, Filter Type =None', N'3054', N'County of Orange', 0)
, (101, N'OC-TS-3055', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: ANTONIO PKWY at 200 ft from CB 3054 E/S, Map Page=952, Map Grid=G5, Filter Type =None', N'3055', N'County of Orange', 0)
, (102, N'OC-TS-3056', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: ANTONIO PKWY at 300 ft from CB 3055 E/S, Map Page=952, Map Grid=G5, Filter Type =None', N'3056', N'County of Orange', 0)
, (103, N'OC-TS-3057', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: ANTONIO PKWY at 400 ft from CB 3056 E/S, Map Page=952, Map Grid=G5, Filter Type =None', N'3057', N'County of Orange', 0)
, (104, N'OC-TS-3058', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: ANTONIO PKWY at R.M.V. Sign W/S, Map Page=952, Map Grid=G5, Filter Type =None', N'3058', N'County of Orange', 0)
, (105, N'OC-TS-3059', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: ANTONIO PKWY at 100 ft from C/B 3058 W/S, Map Page=952, Map Grid=G5, Filter Type =None', N'3059', N'County of Orange', 0)
, (106, N'OC-TS-3060', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: ANTONIO PKWY at 100 ft from C/B 3059 W/S, Map Page=952, Map Grid=G5, Filter Type =None', N'3060', N'County of Orange', 0)
, (107, N'OC-TS-3061', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: ANTONIO PKWY at 100 ft from C/B 3060 W/S, Map Page=952, Map Grid=G5, Filter Type =None', N'3061', N'County of Orange', 0)
, (108, N'OC-TS-3062', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: ANTONIO PKWY at Sendero 100 ft from C/B 3061 W/S, Map Page=952, Map Grid=G5, Filter Type =None', N'3062', N'County of Orange', 0)
, (109, N'OC-TS-3063', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: ANTONIO PKWY at 50 ft from Cow Camp W/S, Map Page=952, Map Grid=G5, Filter Type =None', N'3063', N'County of Orange', 0)
, (110, N'OC-TS-3064', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: ANTONIO PKWY at 100 ft C/B 3063 Cow Camp W/S, Map Page=952, Map Grid=G5, Filter Type =None', N'3064', N'County of Orange', 0)
, (111, N'OC-TS-3065', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: ANTONIO PKWY at Before Bride W/S, Map Page=952, Map Grid=G5, Filter Type =None', N'3065', N'County of Orange', 0)
, (112, N'OC-TS-3066', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Cow Camp at Antonio Pkwy Median S/S, Map Page=952, Map Grid=G5, Filter Type =None', N'3066', N'County of Orange', 0)
, (113, N'OC-TS-3067', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Cow Camp at 100 ft from C/B 3066 S/S, Map Page=952, Map Grid=G5, Filter Type =None', N'3067', N'County of Orange', 0)
, (114, N'OC-TS-3068', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Cow Camp at Before Bride S/W, Map Page=952, Map Grid=G5, Filter Type =None', N'3068', N'County of Orange', 0)
, (115, N'OC-TS-3070', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Cow Camp at 200 ft After C/B 3069 S/W, Map Page=952, Map Grid=G5, Filter Type =None', N'3070', N'County of Orange', 0)
, (116, N'OC-TS-3072', 0xE6100000010C530E4657BC675DC0D86FBD1B8FC34040, N'County of Orange', N'RANCHO MISSION VIEJO, Cross streets: Cow Camp Island at ANTONIO PKWY S/W, Map Page=952, Map Grid=G5, Filter Type =None', N'3072', N'County of Orange', 0)
SET IDENTITY_INSERT [dbo].[PaceTemp] OFF
GO

DECLARE @TreatmentBMPTypeID int
SET @TreatmentBMPTypeID = 35

DECLARE @InventoryIsVerified bit
SET @InventoryIsVerified = 0

Declare @FullTrashCapture int
Set @FullTrashCapture = 1

Declare @PartialTrashCapture int
Set @PartialTrashCapture = 2

Insert into dbo.TreatmentBMP(TreatmentBMPName, TreatmentBMPTypeID, LocationPoint, StormwaterJurisdictionID, Notes, SystemOfRecordID, OwnerOrganizationID, InventoryIsVerified, TrashCaptureStatusTypeID)
select p.TreatmentBMPName, @TreatmentBMPTypeID, p.LocationPoint, s.StormwaterJurisdictionID, p.Notes, p.SystemOfRecordID, oagain.OrganizationID, @InventoryIsVerified, 
(case when p.FullTrashCapture = 1 then @FullTrashCapture else @PartialTrashCapture end) as TrashCaptureStatusTypeID
 from PaceTemp p join Organization o on p.StormwaterJurisdiction = o.OrganizationName join StormwaterJurisdiction s on s.OrganizationID = o.OrganizationID join Organization oagain on p.OwnerOrganization = oagain.OrganizationName
 where TreatmentBMPName not in (select TreatmentBMPName from neptune.dbo.TreatmentBMP)

Drop Table dbo.PaceTemp

