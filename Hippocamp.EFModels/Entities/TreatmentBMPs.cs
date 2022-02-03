using System.Collections.Generic;
using System.Linq;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    public partial class TreatmentBMPs
    {
        private static IQueryable<TreatmentBMP> GetTreatmentBMPsImpl(HippocampDbContext dbContext)
        {
            return dbContext.TreatmentBMPs
                .Include(x => x.StormwaterJurisdiction).ThenInclude(x => x.Organization)
                .Include(x => x.TreatmentBMPType)
                .Include(x => x.Watershed);
        }

        private static IQueryable<TreatmentBMPModelingAttribute> GetTreatmentBMPModelingAttributesImpl(HippocampDbContext dbContext)
        {
            return dbContext.TreatmentBMPModelingAttributes
                .Include(x => x.RoutingConfiguration)
                .Include(x => x.TimeOfConcentration)
                .Include(x => x.UnderlyingHydrologicSoilGroup)
                .Include(x => x.MonthsOfOperation)
                .Include(x => x.DryWeatherFlowOverride);
        }

        public static List<TreatmentBMPUpsertDto> ListByProjectIDAsUpsertDto(HippocampDbContext dbContext, int projectID)
        {
            var treatmentBMPUpsertDtos = GetTreatmentBMPsImpl(dbContext)
                .Where(x => x.ProjectID == projectID)
                .Select(x => x.AsUpsertDto())
                .ToList();

            return treatmentBMPUpsertDtos;
        }

        public static List<TreatmentBMPDisplayDto> ListAsDisplayDto(HippocampDbContext dbContext)
        {
            var treatmentBMPDisplayDtos = GetTreatmentBMPsImpl(dbContext)
                .Select(x => x.AsDisplayDto())
                .ToList();

            return treatmentBMPDisplayDtos;
        }

        public static TreatmentBMPModelingAttribute GetModelingAttributeByTreatmentBMPID(HippocampDbContext dbContext, int treatmentBMPID)
        {
            var treatmentBMPModelingAttribute = GetTreatmentBMPModelingAttributesImpl(dbContext)
                .SingleOrDefault(x => x.TreatmentBMPID == treatmentBMPID);

            return treatmentBMPModelingAttribute;
        }
    }
}