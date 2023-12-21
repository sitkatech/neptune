namespace Neptune.Web.Models
{
    public abstract partial class TrashCaptureStatusType
    {

        public string FeatureColorOnTrashModuleMap()
        {
            return "#" + TrashCaptureStatusTypeColorCode;
        }

        public string MapboxMarkerUrlForLegend()
        {
            return "/Content/leaflet/images/marker-icon-2x-" + TrashCaptureStatusTypeColorCode + ".png";
        }

        public string SquareImageUrlForLegend()
        {
            return "/Content/img/legendImages/" + TrashCaptureStatusTypeName + ".png";
        }
    }

    public partial class TrashCaptureStatusTypeFull
    {
        
        
    }

    public partial class TrashCaptureStatusTypePartial
    {
        
    }

    public partial class TrashCaptureStatusTypeNone
    {
       
    }

    public partial class TrashCaptureStatusTypeNotProvided
    {
        
    }
}