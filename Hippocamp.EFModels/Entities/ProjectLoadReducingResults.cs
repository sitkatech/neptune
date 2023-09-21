using Hippocamp.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace Hippocamp.EFModels.Entities
{
    public static class ProjectLoadReducingResults
    {
        public static List<ProjectLoadReducingResultDto> ListByProjectIDAsDto(
            HippocampDbContext dbContext, int projectID)
        {
            var treatmentBMPIDs = dbContext.TreatmentBMPs.Where(x => x.ProjectID == projectID)
                .Select(x => x.TreatmentBMPID).ToList();

            return dbContext.vProjectLoadReducingResults
                .Where(x => x.ProjectID == projectID && treatmentBMPIDs.Contains(x.TreatmentBMPID)
                )
                .Select(x => x.AsDto())
                .ToList();
        }

        public static List<vProjectLoadReducingResult> ListByProjectIDs(HippocampDbContext dbContext, List<int> projectIDs)
        {
            return dbContext.vProjectLoadReducingResults
                .Where(x => projectIDs.Contains(x.ProjectID)).ToList();
        }
    }
}