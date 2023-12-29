create view dbo.vStormwaterJurisdictionOrganizationMapping
as
select StormwaterJurisdictionID, o.OrganizationID, OrganizationName 
from dbo.StormwaterJurisdiction sj 
join dbo.Organization o on sj.OrganizationID = o.OrganizationID
