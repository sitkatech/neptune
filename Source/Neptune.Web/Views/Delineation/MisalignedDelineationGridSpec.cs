using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Models;

namespace Neptune.Web.Views.Delineation
{
    public class MisalignedDelineationGridSpec : GridSpec<Models.Delineation>
    {
        public MisalignedDelineationGridSpec()
        {
            Add("", x => x.GetDetailUrlForGrid(), 60, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.TreatmentBMP.ToGridHeaderString("Name"), x => UrlTemplate.MakeHrefString(TreatmentBMPModelExtensions.DetailUrlTemplate.ParameterReplace(x.TreatmentBMPID), x.TreatmentBMP.TreatmentBMPName), 170, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.TreatmentBMPType.ToGridHeaderString("Type"), x => x.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeName, 150, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.DelineationType.ToGridHeaderString(), x => x.DelineationType.DelineationTypeDisplayName, 130, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.Area.ToGridHeaderString("Area (acres)"), x => x.DelineationGeometry.Area * DbSpatialHelper.SquareMetersToAcres, 100);
            Add("Date of Last Delineation Modification", x => x.DateLastModified, 240);
            Add("Date of Last Delineation Verification", x => x.DateLastVerified, 240);
        }
    }
}