update tb
set tb.OwnerOrganizationID = 26

--select *
from dbo.TreatmentBMP tb
join dbo.StormwaterJurisdiction sj on tb.OwnerOrganizationID = sj.StormwaterJurisdictionID
join dbo.Organization o on sj.OrganizationID = o.OrganizationID
join dbo.Organization oo on tb.OwnerOrganizationID = oo.OrganizationID
where tb.StormwaterJurisdictionID = 12 and tb.OwnerOrganizationID = 12