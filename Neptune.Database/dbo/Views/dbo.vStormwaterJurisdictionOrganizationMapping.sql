create view dbo.vStormwaterJurisdictionOrganizationMapping
as
select StormwaterJurisdictionID, o.OrganizationID, OrganizationName from StormwaterJurisdiction sj join organization o on sj.OrganizationID = o.OrganizationID
