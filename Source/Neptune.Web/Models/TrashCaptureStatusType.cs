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
            return "https://api.tiles.mapbox.com/v3/marker/pin-m-water+" + TrashCaptureStatusTypeColorCode + ".png";
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