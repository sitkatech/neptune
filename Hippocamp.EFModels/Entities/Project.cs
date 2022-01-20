using System.Collections.Generic;
using System.Linq;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    public partial class Project
    {
        public static IEnumerable<ProjectDto> ListAsDtos(HippocampDbContext dbContext)
        {
            var projectDtos = dbContext.Projects
                .AsNoTracking()
                .Select(x => x.AsDto());

            return projectDtos;
        }
    }
}