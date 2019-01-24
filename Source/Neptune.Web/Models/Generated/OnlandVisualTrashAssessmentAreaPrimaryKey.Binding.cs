//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OnlandVisualTrashAssessmentArea
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class OnlandVisualTrashAssessmentAreaPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<OnlandVisualTrashAssessmentArea>
    {
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