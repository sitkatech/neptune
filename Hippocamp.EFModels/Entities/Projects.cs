using System;
using System.Collections.Generic;
using System.Linq;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    public partial class Projects
    {
        private static IQueryable<Project> GetProjectsImpl(HippocampDbContext dbContext)
        {
            return dbContext.Projects
                .Include(x => x.Organization)
                .Include(x => x.StormwaterJurisdiction).ThenInclude(x => x.Organization)
                .Include(x => x.ProjectStatus)
                .Include(x => x.CreatePerson)
                .Include(x => x.PrimaryContactPerson)
                .AsNoTracking();
        }

        public static List<ProjectSimpleDto> ListAsSimpleDto(HippocampDbContext dbContext)
        {
            return GetProjectsImpl(dbContext).Select(x => x.AsSimpleDto()).ToList();
        }

        public static Project GetByID(HippocampDbContext dbContext, int projectID)
        {
            return dbContext.Projects.SingleOrDefault(x => x.ProjectID == projectID);
        }

        public static ProjectSimpleDto GetByIDAsSimpleDto(HippocampDbContext dbContext, int projectID)
        {
            return GetProjectsImpl(dbContext).SingleOrDefault(x => x.ProjectID == projectID).AsSimpleDto();
        }

        public static List<ProjectSimpleDto> ListByPersonIDAsSimpleDto(HippocampDbContext dbContext, int personID)
        {
            var person = People.GetByID(dbContext, personID);
            if (person.RoleID == (int)RoleEnum.Admin || person.RoleID == (int)RoleEnum.SitkaAdmin)
            {
                return ListAsSimpleDto(dbContext);
            }

            var jurisdictionIDs = People.ListStormwaterJurisdictionIDsByPersonID(dbContext, personID);

            return GetProjectsImpl(dbContext)
                .Where(x => jurisdictionIDs.Contains(x.StormwaterJurisdictionID))
                .Select(x => x.AsSimpleDto())
                .ToList();
        }

        public static void CreateNew(HippocampDbContext dbContext, ProjectCreateDto projectCreateDto, PersonDto personDto)
        {
            var project = new Project()
            {
                ProjectName = projectCreateDto.ProjectName,
                OrganizationID = projectCreateDto.OrganizationID,
                StormwaterJurisdictionID = projectCreateDto.StormwaterJurisdictionID,
                ProjectStatusID = (int) ProjectStatusEnum.Draft,
                PrimaryContactPersonID = projectCreateDto.PrimaryContactPersonID,
                CreatePersonID = personDto.PersonID,
                DateCreated = DateTime.UtcNow,
                ProjectDescription = projectCreateDto.ProjectDescription,
                AdditionalContactInformation = projectCreateDto.AdditionalContactInformation
            };
            dbContext.Add(project);
            dbContext.SaveChanges();
        }

        public static void Update(HippocampDbContext dbContext, Project project, ProjectCreateDto projectEditDto)
        {
            project.ProjectName = projectEditDto.ProjectName;
            project.OrganizationID = projectEditDto.OrganizationID;
            project.StormwaterJurisdictionID = projectEditDto.StormwaterJurisdictionID;
            project.PrimaryContactPersonID = projectEditDto.PrimaryContactPersonID;
            project.ProjectDescription = projectEditDto.ProjectDescription;
            project.AdditionalContactInformation = projectEditDto.AdditionalContactInformation;

            dbContext.SaveChanges();
        }

        public static void Delete(HippocampDbContext dbContext, Project project)
        {
            dbContext.Projects.Remove(project);
            dbContext.SaveChanges();
        }
    }
}