insert into dbo.Tenant(TenantID, TenantName, TenantDomain, TenantSubdomain)
values 
(2, 'OCStormwater', 'ocstormwatertools.org', null)

insert into dbo.NeptunePageRenderType(NeptunePageRenderTypeID, NeptunePageRenderTypeName, NeptunePageRenderTypeDisplayName)
values
(1, 'IntroductoryText', 'Introductory Text'),
(2, 'PageContent', 'Page Content')

insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(1, 'HomePage', 'Home Page', 2),
(2, 'About', 'About', 2),
(3, 'OrganizationsList', 'Organizations List', 1),
(4, 'HomeMapInfo', 'Home Page Map Info', 2),
(5, 'HomeAdditionalInfo', 'Home Page Additional Info', 2)

insert into dbo.NeptunePage(TenantID, NeptunePageTypeID)
select 
	t.tenantid,
	npt.NeptunePageTypeID
from dbo.neptunepagetype npt
cross join dbo.tenant t

insert into dbo.TenantAttribute (TenantID, DefaultBoundingBox, MinimumYear, PrimaryContactPersonID, TenantSquareLogoFileResourceID, TenantBannerLogoFileResourceID, TenantStyleSheetFileResourceID, TenantDisplayName, ToolDisplayName, RecaptchaPublicKey, RecaptchaPrivateKey)
values
(2, 0xE61000000104050000000100000040285FC06911DAA3DB1C47400100000040285FC08D97B8A52102454001000000B0415DC08D97B8A52102454001000000B0415DC06911DAA3DB1C47400100000040285FC06911DAA3DB1C474001000000020000000001000000FFFFFFFF0000000003, 2017, NULL, NULL, NULL, NULL, N'O.C. Stormwater Tools', N'O.C. Stormwater Tools', N'6LfZQQoUAAAAAIJ_2lD6ct0lBHQB9j5kv8p994SP', N'6LfZQQoUAAAAAOeNQDcXlTV9JM7PBQE3jCqlDBSB')

declare @TenantIDTo int
set @TenantIDTo = 2

insert into dbo.OrganizationType(TenantID, OrganizationTypeName, OrganizationTypeAbbreviation, LegendColor, IsDefaultOrganizationType)
values
(@TenantIDTo, 'Federal', 'FED', '#1f77b4', 0, 0),
(@TenantIDTo, 'Local', 'LOC', '#aec7e8', 0, 0),
(@TenantIDTo, 'Private', 'PRI', '#ff7f0e', 0, 1),
(@TenantIDTo, 'State', 'ST', '#ffbb78', 0, 0)

declare @privateOrgTypeIDForTenant int
select @privateOrgTypeIDForTenant = OrganizationTypeID from dbo.OrganizationType o where o.TenantID = @TenantIDTo and o.OrganizationTypeName = 'Private'


insert into dbo.Organization(TenantID, OrganizationGuid, OrganizationName, OrganizationShortName, OrganizationTypeID, IsActive, OrganizationUrl)
values
(2, '6E020A68-7277-41A2-B627-52046A3D5558', 'Sitka Technology Group',	'Sitka', @privateOrgTypeIDForTenant, 1, 'http://sitkatech.com'),
(2, NULL, '(Unknown or Unspecified Organization)', 'N/A', @privateOrgTypeIDForTenant, 0, NULL)


declare @sitkaOrgIDForTenant int
select @sitkaOrgIDForTenant = OrganizationID from dbo.Organization o where o.TenantID = @TenantIDTo and o.OrganizationName = 'Sitka Technology Group'


insert into dbo.Person(TenantID, PersonGuid, FirstName, LastName, Email, Phone, RoleID,	CreateDate, UpdateDate, LastActivityDate, IsActive, OrganizationID, ReceiveSupportEmails, LoginName)
values
(2, 'CD3DAB18-4242-4FE9-AB10-874CA43AAEE2', 'Ray', 'Lee', 'ray@sitkatech.com', NULL, 8, '12/21/2017', null, '12/21/2017', '1', '1', '0', 'ray@sitkatech.com'),
(2, '2F783A30-36E1-4B0C-A1B6-AA4AFE68DDB3', 'John', 'Burns', 'john.burns@sitkatech.com', '(503) 808-1245', 8, '12/21/2017', null, '12/21/2017', '1', '1', '1', 'john.burns@sitkatech.com'),
(2, '06836387-9236-4A6C-B663-3A2B5485F6CD', 'Laryea', 'Quaye', 'laryea.quaye@sitkatech.com', NULL, 8, '12/21/2017', null, '12/21/2017', '1', '1', '0', 'laryea.quaye'),
(2, '61E2A1D0-6A3F-499C-B72C-2160196006F0', 'Liz', 'Christeleit', 'liz.christeleit@sitkatech.com', NULL, 8, '12/21/2017', null, '12/21/2017', '1', '1', '0', 'liz.christeleit@sitkatech.com'),
(2, '98B8288A-59FB-456E-839A-24EC7B528BFE', 'Matt', 'Deniston', 'matt@sitkatech.com', '(503) 808-1204', 8, '12/21/2017', null, '12/21/2017', '1', '1', '0', 'mdeniston'),
(2, '8A9C82C3-9900-4877-A83C-73986DD63A18', 'Nick', 'Padinha', 'nick.padinha@sitkatech.com', null, 8, '12/21/2017', null, '12/21/2017', '1', '1', '0', 'npadinha')