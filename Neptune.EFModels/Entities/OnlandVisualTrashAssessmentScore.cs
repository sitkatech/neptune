namespace Neptune.EFModels.Entities
{
    public abstract partial class OnlandVisualTrashAssessmentScore
    {
        public abstract string LegendImgUrl();
    }

    public partial class OnlandVisualTrashAssessmentScoreA
    {
        public override string LegendImgUrl()
        {
            return "/Areas/Trash/Content/img/green.png";
        }
    }

    public partial class OnlandVisualTrashAssessmentScoreB
    {
        public override string LegendImgUrl()
        {
            return "/Areas/Trash/Content/img/yellow.png";
        }

    }

    public partial class OnlandVisualTrashAssessmentScoreC
    {
        public override string LegendImgUrl()
        {
            return "/Areas/Trash/Content/img/orange.png";
        }

    }

    public partial class OnlandVisualTrashAssessmentScoreD
    {
        public override string LegendImgUrl()
        {
            return "/Areas/Trash/Content/img/red.png";
        }

    }
}