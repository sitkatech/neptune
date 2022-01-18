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
        public static PersonDto CreateNewPerson(HippocampDbContext dbContext, PersonUpsertDto personToCreate, string loginName, Guid personGuid)
        {
            if (!personToCreate.RoleID.HasValue)
            {
                return null;
            }

            var person = new Person
            {
                PersonGuid = personGuid,
                LoginName = loginName,
                Email = personToCreate.Email,
                FirstName = personToCreate.FirstName,
                LastName = personToCreate.LastName,
                IsActive = true,
                RoleID = personToCreate.RoleID.Value,
                CreateDate = DateTime.UtcNow,
            };

            dbContext.People.Add(person);
            dbContext.SaveChanges();
            dbContext.Entry(person).Reload();

            return GetByIDAsDto(dbContext, person.PersonID);
        }

        public static IEnumerable<PersonDto> ListAsDto(HippocampDbContext dbContext)
        {
            return GetPersonImpl(dbContext)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Select(x => x.AsDto()).AsEnumerable();
        }

        public static IEnumerable<string> GetEmailAddressesForAdminsThatReceiveSupportEmails(HippocampDbContext dbContext)
        {
            var persons = GetPersonImpl(dbContext)
                .Where(x => x.IsActive && (x.RoleID == (int) RoleEnum.Admin ||  x.RoleID == (int) RoleEnum.SitkaAdmin) && x.ReceiveSupportEmails)
                .Select(x => x.Email)
                .AsEnumerable();

            return persons;
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

        private static IQueryable<Person> GetPersonImpl(HippocampDbContext dbContext)
        {
            return dbContext.People
                .Include(x => x.Role)
                .Include(x => x.Organization).ThenInclude(x => x.OrganizationType)
                .AsNoTracking();
        }

        public static PersonDto GetByEmailAsDto(HippocampDbContext dbContext, string email)
        {
            var person = GetPersonImpl(dbContext).SingleOrDefault(x => x.Email == email);
            return person?.AsDto();
        }

        public static PersonDto UpdatePersonEntity(HippocampDbContext dbContext, int personID, PersonUpsertDto personEditDto)
        {
            if (!personEditDto.RoleID.HasValue)
            {
                return null;
            }

            var person = dbContext.People
                .Include(x => x.Role)
                .Single(x => x.PersonID == personID);

            person.RoleID = personEditDto.RoleID.Value;
            person.ReceiveSupportEmails = personEditDto.RoleID.Value == 1 && personEditDto.ReceiveSupportEmails;
            person.UpdateDate = DateTime.UtcNow;

            dbContext.SaveChanges();
            dbContext.Entry(person).Reload();
            return GetByIDAsDto(dbContext, personID);
        }

        public static PersonDto UpdatePersonGuid(HippocampDbContext dbContext, int personID, Guid personGuid)
        {
            var person = dbContext.People
                .Single(x => x.PersonID == personID);

            person.PersonGuid = personGuid;
            person.UpdateDate = DateTime.UtcNow;

            dbContext.SaveChanges();
            dbContext.Entry(person).Reload();
            return GetByIDAsDto(dbContext, personID);
        }

        public static List<ErrorMessage> ValidateUpdate(HippocampDbContext dbContext, PersonUpsertDto personEditDto, int personID)
        {
            var result = new List<ErrorMessage>();
            if (!personEditDto.RoleID.HasValue)
            {
                result.Add(new ErrorMessage() { Type = "Role ID", Message = "Role ID is required." });
            }

            return result;
        }
    }
}