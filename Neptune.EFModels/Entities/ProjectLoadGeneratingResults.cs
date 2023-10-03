using System.Collections.Generic;
using System.Linq;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static class ProjectLoadGeneratingResults
{
    public static List<ProjectLoadGeneratingResultDto> ListByProjectIDAsDto(NeptuneDbContext dbContext, int projectID)
    {
        return dbContext.vProjectLoadGeneratingResults
            .Where(x => x.ProjectID == projectID)
            .Select(x => x.AsDto())
            .ToList();
    }

}