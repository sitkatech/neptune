//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ObservationTypeSpecification
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class ObservationTypeSpecificationPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ObservationTypeSpecification>
    {
        public ObservationTypeSpecificationPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ObservationTypeSpecificationPrimaryKey(ObservationTypeSpecification observationTypeSpecification) : base(observationTypeSpecification){}

        public static implicit operator ObservationTypeSpecificationPrimaryKey(int primaryKeyValue)
        {
            return new ObservationTypeSpecificationPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ObservationTypeSpecificationPrimaryKey(ObservationTypeSpecification observationTypeSpecification)
        {
            return new ObservationTypeSpecificationPrimaryKey(observationTypeSpecification);
        }
    }
}