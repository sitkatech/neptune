insert into dbo.Tenant(TenantID, TenantName, TenantDomain, TenantSubdomain)
values 
(1, 'SitkaTechnologyGroup', 'projectneptune.com', 'sitka'),
(2, 'OCStormwater', 'ocstormwatertools.org', null)

insert into dbo.neptunepage(TenantID, NeptunePageTypeID)
select 
	t.tenantid,
	npt.NeptunePageTypeID
from dbo.neptunepagetype npt
cross join dbo.tenant t

insert into dbo.TenantAttribute (TenantID, DefaultBoundingBox, MinimumYear, PrimaryContactPersonID, TenantSquareLogoFileResourceID, TenantBannerLogoFileResourceID, TenantStyleSheetFileResourceID, TenantDisplayName, ToolDisplayName, RecaptchaPublicKey, RecaptchaPrivateKey)
values
(1, 0xE6100000010405000000183F8D7BF3A65EC0E17A14AE47C94640183F8D7BF3A65EC0A7255646236946403A799109F8725EC0A7255646236946403A799109F8725EC0E17A14AE47C94640183F8D7BF3A65EC0E17A14AE47C9464001000000020000000001000000FFFFFFFF0000000003, 2017, NULL, NULL, NULL, NULL, N'Sitka Technology Group', N'Sitka Technology Group', N'6LfSQQoUAAAAAFHpdE_ueMs4ptzC7zRzvWpdaeZp', N'6LfSQQoUAAAAAJWSKhiXBLvd3JPbHgcrGOes6t5K'),
(2, 0xE61000000104050000000100000040285FC06911DAA3DB1C47400100000040285FC08D97B8A52102454001000000B0415DC08D97B8A52102454001000000B0415DC06911DAA3DB1C47400100000040285FC06911DAA3DB1C474001000000020000000001000000FFFFFFFF0000000003, 2017, NULL, NULL, NULL, NULL, N'O.C. Stormwater Tools', N'O.C. Stormwater Tools', N'6LfZQQoUAAAAAIJ_2lD6ct0lBHQB9j5kv8p994SP', N'6LfZQQoUAAAAAOeNQDcXlTV9JM7PBQE3jCqlDBSB')
