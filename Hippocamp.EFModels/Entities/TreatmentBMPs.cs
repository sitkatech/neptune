using System;
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
                .Include(x => x.Watershed)
                .AsNoTracking();
        }

        public static List<TreatmentBMPUpsertDto> ListByProjectIDAsUpsertDto(HippocampDbContext dbContext, int projectID)
        {
            var treatmentBMPs = GetTreatmentBMPsImpl(dbContext)
                .Where(x => x.ProjectID == projectID).ToList();

            var treatmentBMPIDs = treatmentBMPs.Select(x => x.TreatmentBMPID).ToList();

            var treatmentBMPModelingAttributes = dbContext.TreatmentBMPModelingAttributes
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

        public static List<TreatmentBMPModelingAttributeDropdownItemDto> GetModelingAttributeDropdownItemsAsDto(HippocampDbContext dbContext)
        {
            var treatmentBMPModelingAttributeDropdownItemDtos = new List<TreatmentBMPModelingAttributeDropdownItemDto>();

            var timeOfConcentrationDropdownItemDtos = dbContext.TimeOfConcentrations.Select(x =>
                new TreatmentBMPModelingAttributeDropdownItemDto(x.TimeOfConcentrationID, x.TimeOfConcentrationDisplayName, "TimeOfConcentrationID"));
            treatmentBMPModelingAttributeDropdownItemDtos.AddRange(timeOfConcentrationDropdownItemDtos);

            var routingConfigurationDropdownItemDtos = dbContext.RoutingConfigurations.Select(x =>
                new TreatmentBMPModelingAttributeDropdownItemDto(x.RoutingConfigurationID, x.RoutingConfigurationDisplayName, "RoutingConfigurationID"));
            treatmentBMPModelingAttributeDropdownItemDtos.AddRange(routingConfigurationDropdownItemDtos);

            var monthsOfOperationDropdownItemDtos = dbContext.MonthsOfOperations.Select(x =>
                new TreatmentBMPModelingAttributeDropdownItemDto(x.MonthsOfOperationID, x.MonthsOfOperationDisplayName, "MonthsOfOperationID"));
            treatmentBMPModelingAttributeDropdownItemDtos.AddRange(monthsOfOperationDropdownItemDtos);

            var underlyingHydrologicSoilGroupsDropdownItemDtos = dbContext.UnderlyingHydrologicSoilGroups.Select(x =>
                new TreatmentBMPModelingAttributeDropdownItemDto(x.UnderlyingHydrologicSoilGroupID, x.UnderlyingHydrologicSoilGroupDisplayName, "UnderlyingHydrologicSoilGroupID"));
            treatmentBMPModelingAttributeDropdownItemDtos.AddRange(underlyingHydrologicSoilGroupsDropdownItemDtos);

            var dryWeatherFlowOverrideDropdownItemDtos = dbContext.DryWeatherFlowOverrides.Select(x =>
                new TreatmentBMPModelingAttributeDropdownItemDto(x.DryWeatherFlowOverrideID, x.DryWeatherFlowOverrideDisplayName, "DryWeatherFlowOverrideID"));
            treatmentBMPModelingAttributeDropdownItemDtos.AddRange(dryWeatherFlowOverrideDropdownItemDtos);

            return treatmentBMPModelingAttributeDropdownItemDtos;
        }
    }
}