using Microsoft.EntityFrameworkCore;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static class ProjectLoadReducingResults
    {
        public static List<ProjectLoadReducingResultDto> ListByProjectIDAsDto(
            NeptuneDbContext dbContext, int projectID)
        {
            var treatmentBMPIDs = dbContext.TreatmentBMPs.AsNoTracking().Where(x => x.ProjectID == projectID)
                .Select(x => x.TreatmentBMPID).ToList();

            return dbContext.vProjectLoadReducingResults.AsNoTracking()
                .Where(x => x.ProjectID == projectID && treatmentBMPIDs.Contains(x.TreatmentBMPID)
                )
                .Select(x => x.AsDto())
                .ToList();
        }

        public static List<vProjectLoadReducingResult> ListByProjectIDs(NeptuneDbContext dbContext, List<int> projectIDs)
        {
            return dbContext.vProjectLoadReducingResults.AsNoTracking()
                .Where(x => projectIDs.Contains(x.ProjectID)).ToList();
        }
    }
}