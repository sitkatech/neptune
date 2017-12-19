//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ModeledCatchment
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class ModeledCatchmentPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ModeledCatchment>
    {
        public ModeledCatchmentPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ModeledCatchmentPrimaryKey(ModeledCatchment modeledCatchment) : base(modeledCatchment){}

        public static implicit operator ModeledCatchmentPrimaryKey(int primaryKeyValue)
        {
            return new ModeledCatchmentPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ModeledCatchmentPrimaryKey(ModeledCatchment modeledCatchment)
        {
            return new ModeledCatchmentPrimaryKey(modeledCatchment);
        }
    }
}