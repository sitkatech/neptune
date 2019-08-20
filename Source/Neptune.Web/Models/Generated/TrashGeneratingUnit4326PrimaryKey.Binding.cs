//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TrashGeneratingUnit4326
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TrashGeneratingUnit4326PrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TrashGeneratingUnit4326>
    {
        public TrashGeneratingUnit4326PrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TrashGeneratingUnit4326PrimaryKey(TrashGeneratingUnit4326 trashGeneratingUnit4326) : base(trashGeneratingUnit4326){}

        public static implicit operator TrashGeneratingUnit4326PrimaryKey(int primaryKeyValue)
        {
            return new TrashGeneratingUnit4326PrimaryKey(primaryKeyValue);
        }

        public static implicit operator TrashGeneratingUnit4326PrimaryKey(TrashGeneratingUnit4326 trashGeneratingUnit4326)
        {
            return new TrashGeneratingUnit4326PrimaryKey(trashGeneratingUnit4326);
        }
    }
}