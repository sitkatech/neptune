using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public static class vTreatmentBMPAssessmentDetaileds
{
    public static List<vTreatmentBMPAssessmentDetailed> ListViewableByPerson(NeptuneDbContext dbContext, Person person)
    {
        var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(dbContext, person);

        //These users can technically see all Jurisdictions, just potentially not the WQMPs inside them
        var treatmentBMPAssessmentDetaileds = dbContext.vTreatmentBMPAssessmentDetaileds.AsNoTracking()
            .Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID));
        return treatmentBMPAssessmentDetaileds.ToList();
    }

}