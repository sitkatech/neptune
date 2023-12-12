//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RoutingConfiguration]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class RoutingConfigurationExtensionMethods
    {

        public static RoutingConfigurationSimpleDto AsSimpleDto(this RoutingConfiguration routingConfiguration)
        {
            var routingConfigurationSimpleDto = new RoutingConfigurationSimpleDto()
            {
                RoutingConfigurationID = routingConfiguration.RoutingConfigurationID,
                RoutingConfigurationName = routingConfiguration.RoutingConfigurationName,
                RoutingConfigurationDisplayName = routingConfiguration.RoutingConfigurationDisplayName
            };
            DoCustomSimpleDtoMappings(routingConfiguration, routingConfigurationSimpleDto);
            return routingConfigurationSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(RoutingConfiguration routingConfiguration, RoutingConfigurationSimpleDto routingConfigurationSimpleDto);
    }
}