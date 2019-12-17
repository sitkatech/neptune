using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Models;

namespace Neptune.Web.Views.Delineation
{
    public class MisalignedDistributedDelineationGridSpec : GridSpec<vTreatmentBMPWithMisalignedDistributedDelineation>
    {
        public MisalignedDistributedDelineationGridSpec()
        {
            Add(Models.FieldDefinition.TreatmentBMP.ToGridHeaderString("Name"), x => UrlTemplate.MakeHrefString(TreatmentBMPModelExtensions.DetailUrlTemplate.ParameterReplace(x.TreatmentBMPID), x.TreatmentBMPName), 170, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.TreatmentBMPType.ToGridHeaderString("Type"), x => x.TreatmentBMPTypeName, 150, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.DelineationType.ToGridHeaderString(), x => x.DelineationTypeDisplayName, 130, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.Area.ToGridHeaderString(), x => x.DelineationArea, 100);
            Add("Date of Last Delineation Modification", x => x.DateLastModified, 240);
            Add("Date of Last Delineation Verification", x => x.DateLastVerified, 240);
        }
    }
}