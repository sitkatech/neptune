using Hippocamp.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Hippocamp.EFModels.Entities
{
    public partial class ProjectNetworkSolveHistories
    {
        public static List<ProjectNetworkSolveHistorySimpleDto> GetByProjectIDAsDto(HippocampDbContext dbContext, int projectID)
        {
            return dbContext.ProjectNetworkSolveHistories
                .Include(x => x.RequestedByPerson)
                .Include(x => x.ProjectNetworkSolveHistoryStatusType)
                .Where(x => x.ProjectID == projectID)
                .OrderByDescending(x => x.LastUpdated)
                .Select(x => x.AsSimpleDto())
                .ToList();
        }
    }
}