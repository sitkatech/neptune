namespace Neptune.Web.Models
{
    public abstract partial class TrashCaptureStatusType
    {
        public abstract string FeatureColorOnTrashModuleMap();
    }

    public partial class TrashCaptureStatusTypeFull
    {
        public override string FeatureColorOnTrashModuleMap()
        {
            return "#935F59";
        }
    }

    public partial class TrashCaptureStatusTypePartial
    {
        public override string FeatureColorOnTrashModuleMap()
        {
            return "#0051ff";
        }
    }

    public partial class TrashCaptureStatusTypeNone
    {
        public override string FeatureColorOnTrashModuleMap()
        {
            return "#3d3d3e";
        }
    }

    public partial class TrashCaptureStatusTypeNotProvided
    {
        public override string FeatureColorOnTrashModuleMap()
        {
            return "#878688";
        }
    }
}