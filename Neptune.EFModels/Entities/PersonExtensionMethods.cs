using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class PersonExtensionMethods
{
    public static PersonDto AsDto(this Person person)
    {
        var personDto = new PersonDto()
        {
            PersonID = person.PersonID,
            PersonGuid = person.PersonGuid,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Email = person.Email,
            Phone = person.Phone,
            RoleID = person.RoleID,
            CreateDate = person.CreateDate,
            UpdateDate = person.UpdateDate,
            LastActivityDate = person.LastActivityDate,
            IsActive = person.IsActive,
            OrganizationID = person.OrganizationID,
            ReceiveSupportEmails = person.ReceiveSupportEmails,
            LoginName = person.LoginName,
            ReceiveRSBRevisionRequestEmails = person.ReceiveRSBRevisionRequestEmails,
            WebServiceAccessToken = person.WebServiceAccessToken,
            IsOCTAGrantReviewer = person.IsOCTAGrantReviewer
        };
        return personDto;
    }

    public static bool CanEditJurisdiction(this Person person, int stormwaterJurisdictionID, NeptuneDbContext dbContext)
    {
        if (person.RoleID == (int) RoleEnum.Admin || person.RoleID == (int) RoleEnum.SitkaAdmin )
        {
            return true;
        }

        if (person.RoleID == (int) RoleEnum.JurisdictionEditor || person.RoleID == (int) RoleEnum.JurisdictionManager)
        {
            var stormwaterJurisdictionIDs = People.ListStormwaterJurisdictionIDsByPersonID(dbContext, person.PersonID);
            return stormwaterJurisdictionIDs.Contains(stormwaterJurisdictionID);
        }

        return false;
    }

    public static bool CanEditJurisdiction(this PersonDto person, int stormwaterJurisdictionID, NeptuneDbContext dbContext)
    {
        if (person.RoleID == (int) RoleEnum.Admin || person.RoleID == (int) RoleEnum.SitkaAdmin )
        {
            return true;
        }

        if (person.RoleID == (int)RoleEnum.JurisdictionEditor || person.RoleID == (int)RoleEnum.JurisdictionManager)
        {
            var stormwaterJurisdictionIDs = People.ListStormwaterJurisdictionIDsByPersonID(dbContext, person.PersonID);
            return stormwaterJurisdictionIDs.Contains(stormwaterJurisdictionID);
        }

        return false;
    }
}