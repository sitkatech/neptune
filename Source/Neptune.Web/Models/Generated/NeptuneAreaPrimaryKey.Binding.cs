//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.NeptuneArea
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class NeptuneAreaPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<NeptuneArea>
    {
        public NeptuneAreaPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public NeptuneAreaPrimaryKey(NeptuneArea neptuneArea) : base(neptuneArea){}

        public static implicit operator NeptuneAreaPrimaryKey(int primaryKeyValue)
        {
            return new NeptuneAreaPrimaryKey(primaryKeyValue);
        }

        public static implicit operator NeptuneAreaPrimaryKey(NeptuneArea neptuneArea)
        {
            return new NeptuneAreaPrimaryKey(neptuneArea);
        }
    }
}