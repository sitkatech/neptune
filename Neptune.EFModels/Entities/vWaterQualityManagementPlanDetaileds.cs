using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public static class vWaterQualityManagementPlanDetaileds
{
    public static List<vWaterQualityManagementPlanDetailed> ListViewableByPerson(NeptuneDbContext dbContext, Person person)
    {
        var stormwaterJurisdictionsPersonCanViewWithContext = StormwaterJurisdictions.ListViewableByPerson(dbContext, person);

        var stormwaterJurisdictionIDsPersonCanView = person.IsAnonymousOrUnassigned()
            ? stormwaterJurisdictionsPersonCanViewWithContext
                .Where(x => x.StormwaterJurisdictionPublicWQMPVisibilityTypeID !=
                            (int)StormwaterJurisdictionPublicWQMPVisibilityTypeEnum.None)
                .Select(x => x.StormwaterJurisdictionID)
            : stormwaterJurisdictionsPersonCanViewWithContext.Select(x => x.StormwaterJurisdictionID);

        //These users can technically see all Jurisdictions, just potentially not the WQMPs inside them
        var waterQualityManagementPlans = dbContext.vWaterQualityManagementPlanDetaileds.AsNoTracking()
            .Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID));
        if (person.IsAnonymousOrUnassigned())
        {
            var publicWaterQualityManagementPlans = waterQualityManagementPlans.Where(x =>
                x.WaterQualityManagementPlanStatusID ==
                (int)WaterQualityManagementPlanStatusEnum.Active ||
                x.WaterQualityManagementPlanStatusID ==
                (int)WaterQualityManagementPlanStatusEnum.Inactive &&
                x.StormwaterJurisdictionPublicWQMPVisibilityTypeID ==
                (int)StormwaterJurisdictionPublicWQMPVisibilityTypeEnum.ActiveAndInactive).ToList();
            return publicWaterQualityManagementPlans;
        }

        return waterQualityManagementPlans.ToList();
    }

}