//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Person]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
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
                Role = person.Role.AsDto(),
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
            DoCustomMappings(person, personDto);
            return personDto;
        }

        static partial void DoCustomMappings(Person person, PersonDto personDto);

        public static PersonSimpleDto AsSimpleDto(this Person person)
        {
            var personSimpleDto = new PersonSimpleDto()
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
            DoCustomSimpleDtoMappings(person, personSimpleDto);
            return personSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(Person person, PersonSimpleDto personSimpleDto);
    }
}