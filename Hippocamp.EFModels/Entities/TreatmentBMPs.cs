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

        public static List<TreatmentBMPModelingAttributeDropdownItemDto> GetModelingAttributeDropdownItemsAsDto(HippocampDbContext dbContext)
        {
            var treatmentBMPModelingAttributeDropdownItemDtos = new List<TreatmentBMPModelingAttributeDropdownItemDto>();

            var timeOfConcentrationDropdownItemDtos = dbContext.TimeOfConcentrations.Select(x =>
                new TreatmentBMPModelingAttributeDropdownItemDto(x.TimeOfConcentrationID, x.TimeOfConcentrationDisplayName, "TimeOfConcentration"));
            treatmentBMPModelingAttributeDropdownItemDtos.AddRange(timeOfConcentrationDropdownItemDtos);

            var routingConfigurationDropdownItemDtos = dbContext.RoutingConfigurations.Select(x =>
                new TreatmentBMPModelingAttributeDropdownItemDto(x.RoutingConfigurationID, x.RoutingConfigurationDisplayName, "RoutingConfiguration"));
            treatmentBMPModelingAttributeDropdownItemDtos.AddRange(routingConfigurationDropdownItemDtos);

            var monthsOfOperationDropdownItemDtos = dbContext.MonthsOfOperations.Select(x =>
                new TreatmentBMPModelingAttributeDropdownItemDto(x.MonthsOfOperationID, x.MonthsOfOperationDisplayName, "MonthsOfOperation"));
            treatmentBMPModelingAttributeDropdownItemDtos.AddRange(monthsOfOperationDropdownItemDtos);

            var underlyingHydrologicSoilGroupsDropdownItemDtos = dbContext.UnderlyingHydrologicSoilGroups.Select(x =>
                new TreatmentBMPModelingAttributeDropdownItemDto(x.UnderlyingHydrologicSoilGroupID, x.UnderlyingHydrologicSoilGroupDisplayName, "UnderlyingHydrologicSoilGroup"));
            treatmentBMPModelingAttributeDropdownItemDtos.AddRange(underlyingHydrologicSoilGroupsDropdownItemDtos);

            var dryWeatherFlowOverrideDropdownItemDtos = dbContext.DryWeatherFlowOverrides.Select(x =>
                new TreatmentBMPModelingAttributeDropdownItemDto(x.DryWeatherFlowOverrideID, x.DryWeatherFlowOverrideDisplayName, "DryWeatherFlowOverride"));
            treatmentBMPModelingAttributeDropdownItemDtos.AddRange(dryWeatherFlowOverrideDropdownItemDtos);

            return treatmentBMPModelingAttributeDropdownItemDtos;
        }

        public static List<TimeOfConcentrationDto> ListTimesOfConcentrationAsDto(HippocampDbContext dbContext)
        {
            var timeOfConcentrationDtos = dbContext.TimeOfConcentrations.Select(x => x.AsDto()).ToList();
            return timeOfConcentrationDtos;
        }

        public static List<RoutingConfigurationDto> ListRoutingConfigurationsAsDto(HippocampDbContext dbContext)
        {
            var routingConfigurationDtos = dbContext.RoutingConfigurations.Select(x => x.AsDto()).ToList();
            return routingConfigurationDtos;
        }

        public static List<MonthsOfOperationDto> ListMonthsOfOperationAsDto(HippocampDbContext dbContext)
        {
            var monthsOfOperationDtos = dbContext.MonthsOfOperations.Select(x => x.AsDto()).ToList();
            return monthsOfOperationDtos;
        }

        public static List<UnderlyingHydrologicSoilGroupDto> ListUnderlyingHydrologicSoilGroupsAsDto(HippocampDbContext dbContext)
        {
            var underlyingHydrologicSoilGroupDtos = dbContext.UnderlyingHydrologicSoilGroups.Select(x => x.AsDto()).ToList();
            return underlyingHydrologicSoilGroupDtos;
        }

        public static List<DryWeatherFlowOverrideDto> ListDryWeatherFlowOverridesAsDto(HippocampDbContext dbContext)
        {
            var dryWeatherFlowOverrideDtos = dbContext.DryWeatherFlowOverrides.Select(x => x.AsDto()).ToList();
            return dryWeatherFlowOverrideDtos;
        }
    }
}