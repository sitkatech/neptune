//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.StateProvince


namespace Neptune.EFModels.Entities
{
    public class StateProvincePrimaryKey : EntityPrimaryKey<StateProvince>
    {
        public StateProvincePrimaryKey() : base(){}
        public StateProvincePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public StateProvincePrimaryKey(StateProvince stateProvince) : base(stateProvince){}

        public static implicit operator StateProvincePrimaryKey(int primaryKeyValue)
        {
            return new StateProvincePrimaryKey(primaryKeyValue);
        }

        public static implicit operator StateProvincePrimaryKey(StateProvince stateProvince)
        {
            return new StateProvincePrimaryKey(stateProvince);
        }
    }
}