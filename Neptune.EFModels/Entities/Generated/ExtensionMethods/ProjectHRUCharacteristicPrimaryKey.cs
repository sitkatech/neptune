//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectHRUCharacteristic


namespace Neptune.EFModels.Entities
{
    public class ProjectHRUCharacteristicPrimaryKey : EntityPrimaryKey<ProjectHRUCharacteristic>
    {
        public ProjectHRUCharacteristicPrimaryKey() : base(){}
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