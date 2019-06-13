using System;
using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Common;

namespace Neptune.Web.ScheduledJobs
{
    public class LandUseBlockUploadBackgroundJob : ScheduledBackgroundJobBase
    {
        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Local,
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Qa
        };

        protected override void RunJobImplementation()
        {
            var landUseBlockGeometryStaging = HttpRequestStorage.DatabaseEntities.LandUseBlockGeometryStagings.FirstOrDefault();

            if (landUseBlockGeometryStaging == null)
            {
                return;
            }

            var stormwaterJurisdiction = landUseBlockGeometryStaging.StormwaterJurisdiction;
            var person = landUseBlockGeometryStaging.Person;

            // you can use breakpoints inside hangfire jobs like normal. delete this line of code and comment once you have something real to attach to.
            var i = 1;
            //todo: process data from stagging table reporitng errors: on success send successful email, on failue send email of errors in upload file

            // TODO: use Ogr2OgrCommandLineRunner.ImportGeoJsonToMsSql to convert the geojson into LandUseBlockStaging objects
            // also TODO: create a LandUseBlockStaging table with the same shape as the LandUseBlock table
            // look at Corral for examples of this pattern
            


            throw new NotImplementedException();
        }
    }
}