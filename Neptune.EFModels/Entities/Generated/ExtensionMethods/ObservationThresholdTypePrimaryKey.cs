//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ObservationThresholdType


namespace Neptune.EFModels.Entities
{
    public class ObservationThresholdTypePrimaryKey : EntityPrimaryKey<ObservationThresholdType>
    {
        public ObservationThresholdTypePrimaryKey() : base(){}
        public ObservationThresholdTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ObservationThresholdTypePrimaryKey(ObservationThresholdType observationThresholdType) : base(observationThresholdType){}

        public static implicit operator ObservationThresholdTypePrimaryKey(int primaryKeyValue)
        {
            return new ObservationThresholdTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ObservationThresholdTypePrimaryKey(ObservationThresholdType observationThresholdType)
        {
            return new ObservationThresholdTypePrimaryKey(observationThresholdType);
        }
    }
}