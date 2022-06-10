using System.Collections.Generic;
using System.Linq;
using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities;

public static class ProjectLoadGeneratingResults
{
    public static List<ProjectLoadGeneratingResultDto> ListByProjectIDAsDto(HippocampDbContext dbContext, int projectID)
    {
        return dbContext.vProjectLoadGeneratingResults
            .Where(x => x.ProjectID == projectID)
            .Select(x => x.AsDto())
            .ToList();
    }

}