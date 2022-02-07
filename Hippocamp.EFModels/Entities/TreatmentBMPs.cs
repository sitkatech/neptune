using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Hippocamp.EFModels.Entities
{
    public partial class TreatmentBMPs
    {
        private static IQueryable<TreatmentBMP> GetTreatmentBMPsImpl(HippocampDbContext dbContext)
        {
            return dbContext.TreatmentBMPs
                .Include(x => x.StormwaterJurisdiction).ThenInclude(x => x.Organization)
                .Include(x => x.TreatmentBMPType)
                .Include(x => x.Watershed)
                .AsNoTracking();
        }

        private static IQueryable<TreatmentBMPModelingAttribute> GetTreatmentBMPModelingAttributesImpl(HippocampDbContext dbContext)
        {
            return dbContext.TreatmentBMPModelingAttributes
                .Include(x => x.RoutingConfiguration)
                .Include(x => x.TimeOfConcentration)
                .Include(x => x.UnderlyingHydrologicSoilGroup)
                .Include(x => x.MonthsOfOperation)
                .Include(x => x.DryWeatherFlowOverride)
                .AsNoTracking();
        }

        public static List<TreatmentBMPUpsertDto> ListByProjectIDAsUpsertDto(HippocampDbContext dbContext, int projectID)
        {
            var treatmentBMPs = GetTreatmentBMPsImpl(dbContext)
                .Where(x => x.ProjectID == projectID).ToList();

            var treatmentBMPIDs = treatmentBMPs.Select(x => x.TreatmentBMPID).ToList();

            var treatmentBMPModelingAttributes = GetTreatmentBMPModelingAttributesImpl(dbContext)
                .Where(x => treatmentBMPIDs.Contains(x.TreatmentBMPID)).ToList();

            var treatmentBMPUpsertDtos = treatmentBMPs
                .GroupJoin(treatmentBMPModelingAttributes,
                    x => x.TreatmentBMPID,
                    y => y.TreatmentBMPID,
                    (x, y) => new {TreatmentBMP = x, TreatmentBmpModelingAttribute = y.SingleOrDefault()})
                .Select(x => x.TreatmentBMP.AsUpsertDtoWithModelingAttributes(x.TreatmentBmpModelingAttribute))
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

        public static List<TreatmentBMPTypeSimpleDto> ListTypesAsSimpleDto(HippocampDbContext dbContext)
        {
            var treatmentBMPTypeSimpleDtos = dbContext.TreatmentBMPTypes.Select(x => x.AsSimpleDto()).ToList();
            return treatmentBMPTypeSimpleDtos;
        }
    }
}