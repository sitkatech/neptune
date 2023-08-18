//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ObservationTypeCollectionMethod


namespace Neptune.EFModels.Entities
{
    public class ObservationTypeCollectionMethodPrimaryKey : EntityPrimaryKey<ObservationTypeCollectionMethod>
    {
        public ObservationTypeCollectionMethodPrimaryKey() : base(){}
        public ObservationTypeCollectionMethodPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ObservationTypeCollectionMethodPrimaryKey(ObservationTypeCollectionMethod observationTypeCollectionMethod) : base(observationTypeCollectionMethod){}

        public static implicit operator ObservationTypeCollectionMethodPrimaryKey(int primaryKeyValue)
        {
            return new ObservationTypeCollectionMethodPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ObservationTypeCollectionMethodPrimaryKey(ObservationTypeCollectionMethod observationTypeCollectionMethod)
        {
            return new ObservationTypeCollectionMethodPrimaryKey(observationTypeCollectionMethod);
        }
    }
}