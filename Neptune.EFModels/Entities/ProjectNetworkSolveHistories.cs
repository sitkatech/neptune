using Neptune.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Neptune.EFModels.Entities
{
    public partial class ProjectNetworkSolveHistories
    {
        public static List<ProjectNetworkSolveHistorySimpleDto> GetByProjectIDAsDto(NeptuneDbContext dbContext, int projectID)
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