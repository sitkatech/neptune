using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.DelineationUpload
{
    public class UpdateDelineationGeometryViewData : NeptuneViewData
    {
        public string NewGisUploadUrl { get; }
        public string ApprovedGisUploadUrl { get; }

        public IEnumerable<SelectListItem> StormwaterJurisdictions { get; }

        public UpdateDelineationGeometryViewData(Person currentPerson, string newGisUploadUrl, string approvedGisUploadUrl, IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            NewGisUploadUrl = newGisUploadUrl;
            ApprovedGisUploadUrl = approvedGisUploadUrl;
            EntityName = Models.FieldDefinition.Delineation.FieldDefinitionDisplayName;
            PageTitle = "Update Delineation Geometry";

            StormwaterJurisdictions =
                stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
                    .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture), y => y.Organization.OrganizationName);

        }
    }
}
