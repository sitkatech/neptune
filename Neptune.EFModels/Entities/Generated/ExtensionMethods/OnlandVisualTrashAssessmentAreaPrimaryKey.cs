//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OnlandVisualTrashAssessmentArea


namespace Neptune.EFModels.Entities
{
    public class OnlandVisualTrashAssessmentAreaPrimaryKey : EntityPrimaryKey<OnlandVisualTrashAssessmentArea>
    {
        public OnlandVisualTrashAssessmentAreaPrimaryKey() : base(){}
        public OnlandVisualTrashAssessmentAreaPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OnlandVisualTrashAssessmentAreaPrimaryKey(OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea) : base(onlandVisualTrashAssessmentArea){}

        public static implicit operator OnlandVisualTrashAssessmentAreaPrimaryKey(int primaryKeyValue)
        {
            return new OnlandVisualTrashAssessmentAreaPrimaryKey(primaryKeyValue);
        }

        public static implicit operator OnlandVisualTrashAssessmentAreaPrimaryKey(OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            return new OnlandVisualTrashAssessmentAreaPrimaryKey(onlandVisualTrashAssessmentArea);
        }
    }
}