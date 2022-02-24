//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ModelBasin
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class ModelBasinPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ModelBasin>
    {
        public ModelBasinPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ModelBasinPrimaryKey(ModelBasin modelBasin) : base(modelBasin){}

        public static implicit operator ModelBasinPrimaryKey(int primaryKeyValue)
        {
            return new ModelBasinPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ModelBasinPrimaryKey(ModelBasin modelBasin)
        {
            return new ModelBasinPrimaryKey(modelBasin);
        }
    }
}