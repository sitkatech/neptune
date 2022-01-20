using System;
using System.Collections.Generic;
using System.Linq;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    public partial class Project
    {
        private static IQueryable<Project> GetProjectsImpl(HippocampDbContext dbContext)
        {
            return dbContext.Projects
                .Include(x => x.Organization)
                .Include(x => x.StormwaterJurisdiction)
                .Include(x => x.ProjectStatus)
                .Include(x => x.CreatePerson)
                .Include(x => x.PrimaryContactPerson)
                .AsNoTracking();
        }

        public static List<ProjectSimpleDto> ListAsSimpleDtos(HippocampDbContext dbContext)
        {
            return GetProjectsImpl(dbContext).Select(x => x.AsSimpleDto()).ToList();
        }
    }
}