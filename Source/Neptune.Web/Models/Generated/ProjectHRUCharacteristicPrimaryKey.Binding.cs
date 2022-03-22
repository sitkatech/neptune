//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectHRUCharacteristic
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class ProjectHRUCharacteristicPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectHRUCharacteristic>
    {
        public ProjectHRUCharacteristicPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectHRUCharacteristicPrimaryKey(ProjectHRUCharacteristic projectHRUCharacteristic) : base(projectHRUCharacteristic){}

        public static implicit operator ProjectHRUCharacteristicPrimaryKey(int primaryKeyValue)
        {
            return new ProjectHRUCharacteristicPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectHRUCharacteristicPrimaryKey(ProjectHRUCharacteristic projectHRUCharacteristic)
        {
            return new ProjectHRUCharacteristicPrimaryKey(projectHRUCharacteristic);
        }
    }
}