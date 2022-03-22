using Hippocamp.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace Hippocamp.EFModels.Entities
{
    public partial class ProjectNereidResults
    {
        public static List<TreatmentBMPModeledResultSimpleDto> GetTreatmentBMPModeledResultSimpleDtosByProjectID(HippocampDbContext dbContext, int projectID)
        {
            var treatmentBMPIDs = dbContext.TreatmentBMPs.Where(x => x.ProjectID == projectID).Select(x => x.TreatmentBMPID).ToList();

            return dbContext.ProjectNereidResults
                .Where(x => x.ProjectID == projectID && x.TreatmentBMPID != null)
                .ToList()
                .Where(x => treatmentBMPIDs.Contains(x.TreatmentBMPID.Value))
                .Select(x => x.AsTreatmentBMPModeledResultSimpleDto())
                .ToList();
        }
    }
}