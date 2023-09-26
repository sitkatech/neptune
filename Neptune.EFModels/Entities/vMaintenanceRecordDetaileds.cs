using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public static class vMaintenanceRecordDetaileds
{
    public static List<vMaintenanceRecordDetailed> ListViewableByPerson(NeptuneDbContext dbContext, Person person)
    {
        var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(dbContext, person);

        //These users can technically see all Jurisdictions, just potentially not the WQMPs inside them
        var waterQualityManagementPlans = dbContext.vMaintenanceRecordDetaileds.AsNoTracking()
            .Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID));
        return waterQualityManagementPlans.ToList();
    }

}