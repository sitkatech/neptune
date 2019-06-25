using System;
using System.Data.Entity.Spatial;
using System.Diagnostics;
using LtInfo.Common.Models;
using Neptune.Web.Models;
using System.Linq;
using Neptune.Web.Common;

namespace Neptune.Web.Views.DelineationUpload
{
    public class ApproveDelineationGisUploadViewModel : FormViewModel
    {
        public int? UpdateModel(Person currentPerson, out string stormwaterJurisdictionName)

        {
            var delineationStagings = currentPerson.DelineationStagingsWhereYouAreTheUploadedByPerson.ToList();

            // Will  break if there are multiple batches of staged uploads, which is precisely what we want to happen. 
            var stormwaterJurisdiction = delineationStagings.Select(x => x.StormwaterJurisdiction).Distinct().Single();

            stormwaterJurisdictionName = stormwaterJurisdiction.GetOrganizationDisplayName();

            // Starting from the treatment BMP is kind of backwards, conceptually, but it's easier to read and write
            var treatmentBMPNames = delineationStagings.Select(x => x.TreatmentBMPName).ToList();
            var treatmentBMPsToUpdate = stormwaterJurisdiction.TreatmentBMPs.Where(x => treatmentBMPNames.Contains(x.TreatmentBMPName)).ToList();

            foreach (var treatmentBMP in treatmentBMPsToUpdate)
            {
                var wktAndAnnotation = delineationStagings.Single(z => treatmentBMP.TreatmentBMPName == z.TreatmentBMPName);

                treatmentBMP.Delineation?.Delete(HttpRequestStorage.DatabaseEntities);

                treatmentBMP.Delineation = new Models.Delineation(
                    wktAndAnnotation.DelineationStagingGeometry,
                    DelineationType.Distributed.DelineationTypeID, false, treatmentBMP.TreatmentBMPID, DateTime.Now) {VerifiedByPersonID = currentPerson.PersonID, DateLastVerified = DateTime.Now};
            }

            return treatmentBMPsToUpdate.Count;
        }
    }
}
