//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.LoadGeneratingUnitRefreshArea
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class LoadGeneratingUnitRefreshAreaPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<LoadGeneratingUnitRefreshArea>
    {
        public LoadGeneratingUnitRefreshAreaPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public LoadGeneratingUnitRefreshAreaPrimaryKey(LoadGeneratingUnitRefreshArea loadGeneratingUnitRefreshArea) : base(loadGeneratingUnitRefreshArea){}

        public static implicit operator LoadGeneratingUnitRefreshAreaPrimaryKey(int primaryKeyValue)
        {
            return new LoadGeneratingUnitRefreshAreaPrimaryKey(primaryKeyValue);
        }

        public static implicit operator LoadGeneratingUnitRefreshAreaPrimaryKey(LoadGeneratingUnitRefreshArea loadGeneratingUnitRefreshArea)
        {
            return new LoadGeneratingUnitRefreshAreaPrimaryKey(loadGeneratingUnitRefreshArea);
        }
    }
}