//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.RoutingConfiguration


namespace Neptune.EFModels.Entities
{
    public class RoutingConfigurationPrimaryKey : EntityPrimaryKey<RoutingConfiguration>
    {
        public RoutingConfigurationPrimaryKey() : base(){}
        public RoutingConfigurationPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public RoutingConfigurationPrimaryKey(RoutingConfiguration routingConfiguration) : base(routingConfiguration){}

        public static implicit operator RoutingConfigurationPrimaryKey(int primaryKeyValue)
        {
            return new RoutingConfigurationPrimaryKey(primaryKeyValue);
        }

        public static implicit operator RoutingConfigurationPrimaryKey(RoutingConfiguration routingConfiguration)
        {
            return new RoutingConfigurationPrimaryKey(routingConfiguration);
        }
    }
}