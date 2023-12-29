//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ModelBasin


namespace Neptune.EFModels.Entities
{
    public class ModelBasinPrimaryKey : EntityPrimaryKey<ModelBasin>
    {
        public ModelBasinPrimaryKey() : base(){}
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