using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Hippocamp.Models.DataTransferObjects;
using Hippocamp.Models.DataTransferObjects.Person;

namespace Hippocamp.EFModels.Entities
{
    public static class People
    {
        public static PersonDto CreateNewPerson(HippocampDbContext dbContext, PersonCreateDto personToCreate)
        {
            if (!personToCreate.RoleID.HasValue)
            {
                return null;
            }

            var organizationID = Organizations.OrganizationIDUnassigned;
            var organization = Organizations.GetByName(dbContext, personToCreate.OrganizationName);
            if (organization != null)
            {
                organizationID = organization.OrganizationID;
            }

            var person = new Person
            {
                PersonGuid = personToCreate.UserGuid,
                LoginName = personToCreate.LoginName,
                Email = personToCreate.Email,
                FirstName = personToCreate.FirstName,
                LastName = personToCreate.LastName,
                IsActive = true,
                RoleID = personToCreate.RoleID.Value,
                CreateDate = DateTime.UtcNow,
                OrganizationID = organizationID
            };

            dbContext.People.Add(person);
            dbContext.SaveChanges();
            dbContext.Entry(person).Reload();

            return GetByIDAsDto(dbContext, person.PersonID);
        }

        public static IEnumerable<string> GetEmailAddressesForAdminsThatReceiveSupportEmails(HippocampDbContext dbContext)
        {
            var persons = GetPersonImpl(dbContext)
                .Where(x => x.IsActive && (x.RoleID == (int) RoleEnum.Admin ||  x.RoleID == (int) RoleEnum.SitkaAdmin) && x.ReceiveSupportEmails)
                .Select(x => x.Email)
                .AsEnumerable();

            return persons;
        }

        public static List<PersonSimpleDto> ListAsSimpleDto(HippocampDbContext dbContext)
        {
            return GetPersonImpl(dbContext).Select(x => x.AsSimpleDto()).ToList();
        }

        public static Person GetByID(HippocampDbContext dbContext, int personID)
        {
            return GetPersonImpl(dbContext).SingleOrDefault(x => x.PersonID == personID);
        }

        public static PersonDto GetByIDAsDto(HippocampDbContext dbContext, int personID)
        {
            var person = GetPersonImpl(dbContext).SingleOrDefault(x => x.PersonID == personID);
            return person?.AsDto();
        }

        public static PersonDto GetByPersonGuidAsDto(HippocampDbContext dbContext, Guid personGuid)
        {
            var person = GetPersonImpl(dbContext)
                .SingleOrDefault(x => x.PersonGuid == personGuid);

            return person?.AsDto();
        }

        public static List<int> ListStormwaterJurisdictionIDsByPersonID(HippocampDbContext dbContext, int personID)
        {
            var personDto = GetByIDAsDto(dbContext, personID);
            return ListStormwaterJurisdictionIDsByPersonDto(dbContext, personDto);
        }

        public static List<int> ListStormwaterJurisdictionIDsByPersonDto(HippocampDbContext dbContext, PersonDto person)
        {
            if (person.Role.RoleID == (int) RoleEnum.Admin || person.Role.RoleID == (int) RoleEnum.SitkaAdmin)
            {
                return dbContext.StormwaterJurisdictions.Select(x => x.StormwaterJurisdictionID).ToList();
            }

            return dbContext.StormwaterJurisdictionPeople
                .Where(x => x.PersonID == person.PersonID)
                .Select(x => x.StormwaterJurisdictionID)
                .ToList();
        }

        private static IQueryable<Person> GetPersonImpl(HippocampDbContext dbContext)
        {
            return dbContext.People
                .Include(x => x.Role)
                .Include(x => x.Organization).ThenInclude(x => x.OrganizationType)
                .AsNoTracking();
        }
    }
}