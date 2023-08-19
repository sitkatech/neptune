﻿using Neptune.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    public static class People
    {
        //public static PersonDto CreateUnassignedPerson(NeptuneDbContext dbContext, PersonCreateDto userCreateDto)
        //{
        //    var userUpsertDto = new PersonUpsertDto()
        //    {
        //        FirstName = userCreateDto.FirstName,
        //        LastName = userCreateDto.LastName,
        //        OrganizationName = userCreateDto.OrganizationName,
        //        Email = userCreateDto.Email,
        //        RoleID = (int)RoleEnum.Unassigned,  // don't allow non-admin user to set their role to something other than Unassigned
        //    };
        //    return CreateNewPerson(dbContext, userUpsertDto, userCreateDto.LoginName, userCreateDto.UserGuid);
        //}

        //public static List<ErrorMessage> ValidateCreateUnassignedPerson(NeptuneDbContext dbContext, PersonCreateDto userCreateDto)
        //{
        //    var result = new List<ErrorMessage>();

        //    var userByGuidDto = GetByPersonGuid(dbContext, userCreateDto.UserGuid);  // A duplicate Guid not only leads to 500s, it allows someone to hijack another user's account
        //    if (userByGuidDto != null)
        //    {
        //        result.Add(new ErrorMessage() { Type = "Person Creation", Message = "Invalid user information." });  // purposely vague; we don't want a naughty person realizing they figured out someone else's Guid
        //    }

        //    var userByEmailDto = GetByEmailAsDto(dbContext, userCreateDto.Email);  // A duplicate email leads to 500s, so need to prevent duplicates
        //    if (userByEmailDto != null)
        //    {
        //        result.Add(new ErrorMessage() { Type = "Person Creation", Message = "There is already a user account with this email address." });
        //    }

        //    return result;
        //}

        //public static PersonDto CreateNewPerson(NeptuneDbContext dbContext, PersonUpsertDto personToCreate, string loginName, Guid userGuid)
        //{
        //    if (!personToCreate.RoleID.HasValue)
        //    {
        //        return null;
        //    }

        //    var organizationID = Organizations.OrganizationIDUnassigned;
        //    var organization = Organizations.GetByName(dbContext, personToCreate.OrganizationName);
        //    if (organization != null)
        //    {
        //        organizationID = organization.OrganizationID;
        //    }

        //    var person = new Person
        //    {
        //        PersonGuid = userGuid,
        //        LoginName = loginName,
        //        Email = personToCreate.Email,
        //        FirstName = personToCreate.FirstName,
        //        LastName = personToCreate.LastName,
        //        IsActive = true,
        //        RoleID = personToCreate.RoleID.Value,
        //        CreateDate = DateTime.UtcNow,
        //        OrganizationID = organizationID
        //    };

        //    dbContext.People.Add(person);
        //    dbContext.SaveChanges();
        //    dbContext.Entry(person).Reload();

        //    return GetByIDAsDto(dbContext, person.PersonID);
        //}

        public static IEnumerable<string> GetEmailAddressesForAdminsThatReceiveSupportEmails(NeptuneDbContext dbContext)
        {
            var persons = GetImpl(dbContext)
                .Where(x => x.IsActive && (x.RoleID == (int)RoleEnum.Admin || x.RoleID == (int)RoleEnum.SitkaAdmin) && x.ReceiveSupportEmails)
                .Select(x => x.Email)
                .AsEnumerable();

            return persons;
        }

        public static List<PersonSimpleDto> ListActiveAsSimpleDto(NeptuneDbContext dbContext)
        {
            return ListActive(dbContext).Select(x => x.AsSimpleDto()).ToList();
        }

        public static IQueryable<Person> ListActive(NeptuneDbContext dbContext)
        {
            return GetImpl(dbContext).Where(x => x.IsActive)
                .OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
        }

        public static Person GetByID(NeptuneDbContext dbContext, int personID)
        {
            return GetImpl(dbContext).SingleOrDefault(x => x.PersonID == personID);
        }

        public static PersonDto GetByIDAsDto(NeptuneDbContext dbContext, int personID)
        {
            var person = GetImpl(dbContext).SingleOrDefault(x => x.PersonID == personID);
            return person?.AsDto();
        }

        public static PersonDto GetByEmailAsDto(NeptuneDbContext dbContext, string email)
        {
            var person = GetImpl(dbContext).SingleOrDefault(x => x.Email == email);
            return person?.AsDto();
        }

        public static Person? GetByGuid(NeptuneDbContext dbContext, Guid personGuid)
        {
            return GetImpl(dbContext)
                .SingleOrDefault(x => x.PersonGuid == personGuid);
        }

        public static PersonDto GetByGuidAsDto(NeptuneDbContext dbContext, Guid personGuid)
        {
            var person = GetImpl(dbContext).Include(x => x.StormwaterJurisdictionPeople)
                .SingleOrDefault(x => x.PersonGuid == personGuid);

            return person?.AsDto();
        }

        public static List<int> ListStormwaterJurisdictionIDsByPersonID(NeptuneDbContext dbContext, int personID)
        {
            var personDto = GetByIDAsDto(dbContext, personID);
            return ListStormwaterJurisdictionIDsByPersonDto(dbContext, personDto);
        }

        public static List<int> ListStormwaterJurisdictionIDsByPersonDto(NeptuneDbContext dbContext, PersonDto person)
        {
            if (person.Role.RoleID == (int)RoleEnum.Admin || person.Role.RoleID == (int)RoleEnum.SitkaAdmin)
            {
                return dbContext.StormwaterJurisdictions.Select(x => x.StormwaterJurisdictionID).ToList();
            }

            return dbContext.StormwaterJurisdictionPeople
                .Where(x => x.PersonID == person.PersonID)
                .Select(x => x.StormwaterJurisdictionID)
                .ToList();
        }

        private static IQueryable<Person> GetImpl(NeptuneDbContext dbContext)
        {
            return dbContext.People
                .Include(x => x.Organization).ThenInclude(x => x.OrganizationType)
                .AsNoTracking();
        }

        public static List<Person> ListWithRole(NeptuneDbContext dbContext, int roleID)
        {
            return ListActive(dbContext).Where(x => x.RoleID == roleID).ToList();
        }
    }
}
