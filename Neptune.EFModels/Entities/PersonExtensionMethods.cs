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
            Role = person.Role.AsSimpleDto(),
            CreateDate = person.CreateDate,
            UpdateDate = person.UpdateDate,
            LastActivityDate = person.LastActivityDate,
            IsActive = person.IsActive,
            Organization = person.Organization.AsDto(),
            ReceiveSupportEmails = person.ReceiveSupportEmails,
            LoginName = person.LoginName,
            ReceiveRSBRevisionRequestEmails = person.ReceiveRSBRevisionRequestEmails,
            WebServiceAccessToken = person.WebServiceAccessToken,
            IsOCTAGrantReviewer = person.IsOCTAGrantReviewer
        };
        return personDto;
    }
}