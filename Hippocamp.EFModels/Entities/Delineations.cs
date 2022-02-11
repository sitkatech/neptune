using Hippocamp.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Hippocamp.EFModels.Entities
{
    public partial class Delineations
    {
        private static IQueryable<Delineation> GetDelineationsImpl(HippocampDbContext dbContext)
        {
            return dbContext.Delineations
                .Include(x => x.TreatmentBMP)
                .AsNoTracking();
        }

        public static List<DelineationUpsertDto> ListByProjectIDAsUpsertDto(HippocampDbContext dbContext, int projectID)
        {
            return GetDelineationsImpl(dbContext)
                .Where(x => x.TreatmentBMP.ProjectID == projectID)
                .Select(x => x.AsUpsertDto())
                .ToList();
        }
    }
}