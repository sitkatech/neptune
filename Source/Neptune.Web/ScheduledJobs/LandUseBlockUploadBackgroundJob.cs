using System;
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.GdalOgr;
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

            var person = landUseBlockGeometryStaging.Person;


            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneWebConfiguration.Ogr2OgrExecutable, Ogr2OgrCommandLineRunner.DefaultCoordinateSystemId, NeptuneWebConfiguration.HttpRuntimeExecutionTimeout.TotalMilliseconds*10);
            ogr2OgrCommandLineRunner.ImportGeoJsonToMsSql(
                landUseBlockGeometryStaging.LandUseBlockGeometryStagingGeoJson,
                NeptuneWebConfiguration.DatabaseConnectionString, "LandUseBlockStaging", "Select LU_for_TGR as PriorityLandUseTypeID, LU_Descr as LandUseDescription, Geometry as LandUseBlockStagingGeoJson, TGR as TrashGenerationRate, LU_for_TGR as LandUseForTGR, MHI as MedianHouseHoldIncome, Jurisdic as StormwaterJurisdiction, Permit as PermitType", false);
     

            //todo: process data from stagging table reporitng errors: on success send successful email, on failue send email of errors in upload file

            // TODO: use Ogr2OgrCommandLineRunner.ImportGeoJsonToMsSql to convert the geojson into LandUseBlockStaging objects
            // also TODO: create a LandUseBlockStaging table with the same shape as the LandUseBlock table
            // look at Corral for examples of this pattern
            


            throw new NotImplementedException();
        }
    }
}