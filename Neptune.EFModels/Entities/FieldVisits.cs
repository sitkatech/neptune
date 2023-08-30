namespace Neptune.EFModels.Entities;

public static class FieldVisits
{
    public static List<vFieldVisitDetailed> GetProvisionalFieldVisits(NeptuneDbContext dbContext, Person currentPerson)
    {
        var stormwaterJurisdictionIDsPersonCanView = People.ListStormwaterJurisdictionIDsByPersonID(dbContext, currentPerson.PersonID);
        return dbContext.vFieldVisitDetaileds
            .Where(x => x.IsFieldVisitVerified == false && stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID)).ToList();
    }
}