//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.Neighborhood
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class NeighborhoodPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<Neighborhood>
    {
        public NeighborhoodPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public NeighborhoodPrimaryKey(Neighborhood neighborhood) : base(neighborhood){}

        public static implicit operator NeighborhoodPrimaryKey(int primaryKeyValue)
        {
            return new NeighborhoodPrimaryKey(primaryKeyValue);
        }

        public static implicit operator NeighborhoodPrimaryKey(Neighborhood neighborhood)
        {
            return new NeighborhoodPrimaryKey(neighborhood);
        }
    }
}