using Neptune.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    public class ProjectNetworkSolveHistories
    {
        public static List<ProjectNetworkSolveHistorySimpleDto> GetByProjectIDAsDto(NeptuneDbContext dbContext, int projectID)
        {
            return dbContext.ProjectNetworkSolveHistories
                .Include(x => x.RequestedByPerson)
                .Where(x => x.ProjectID == projectID)
                .OrderByDescending(x => x.LastUpdated)
                .Select(x => x.AsSimpleDto())
                .ToList();
        }
    }
}