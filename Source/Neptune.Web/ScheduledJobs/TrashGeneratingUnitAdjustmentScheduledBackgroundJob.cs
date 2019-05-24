using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using MoreLinq;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.ScheduledJobs
{
    public class TrashGeneratingUnitAdjustmentScheduledBackgroundJob : ScheduledBackgroundJobBase
    {
        public new static string JobName => "TGU Adjustment";

        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Local,
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Qa
        };

        protected override void RunJobImplementation()
        {
            TrashGeneratingUnitAdjustmentImpl();
        }

        public const string DelineationObjectType = "Delineation";
        public const string OnlandVisualTrashAssessmentAreaObjectType = "OnlandVisualTrashAssessmentArea";

        protected virtual void TrashGeneratingUnitAdjustmentImpl()
        {
            Logger.Info($"Processing '{JobName}'");

            DbContext.Database.CommandTimeout = 36000;

            // TODO: Pull the least recent adjustment off the stack
            var trashGeneratingUnitAdjustments =
                DbContext.TrashGeneratingUnitAdjustments.Where(x => !x.IsProcessed);

            if (!trashGeneratingUnitAdjustments.Any())
            {
                return;
            }

            var trashGeneratingUnitAdjustment = trashGeneratingUnitAdjustments.MinBy(x => x.AdjustmentDate);

            if (trashGeneratingUnitAdjustment.AdjustedDelineationID != null)
            {
                var adjustedDelineation = trashGeneratingUnitAdjustment.GetAdjustedDelineation(DbContext);

                if (adjustedDelineation != null)
                {

                    var objectIDs =
                        new SqlParameter("@ObjectIDs", FormatIDString(new List<int> { adjustedDelineation.DelineationID }));
                    var objectType = new SqlParameter("@ObjectType", DelineationObjectType);

                    DbContext.Database.ExecuteSqlCommand(
                        "dbo.pRebuildTrashGeneratingUnitTableRelative @ObjectIDs, @ObjectType", objectIDs, objectType);

                }
                else
                {
                    Logger.Info($"Delineation {trashGeneratingUnitAdjustment.AdjustedDelineationID} already deleted");
                }
            }
            else if (trashGeneratingUnitAdjustment.AdjustedOnlandVisualTrashAssessmentAreaID != null)
            {
                var adjustedOnlandVisualTrashAssessmentArea = trashGeneratingUnitAdjustment.GetAdjustedOnlandVisualTrashAssessmentArea(DbContext);

                if (adjustedOnlandVisualTrashAssessmentArea == null)
                {

                    var objectIDs = new SqlParameter("@ObjectIDs",
                        FormatIDString(new List<int>
                        {
                            adjustedOnlandVisualTrashAssessmentArea
                                .OnlandVisualTrashAssessmentAreaID
                        }));
                    var objectType = new SqlParameter("@ObjectType", OnlandVisualTrashAssessmentAreaObjectType);

                    DbContext.Database.ExecuteSqlCommand(
                        "dbo.pRebuildTrashGeneratingUnitTableRelative @ObjectIDs, @ObjectType", objectIDs, objectType);

                }
                else
                {

                    Logger.Info(
                        $"Assessment Area {trashGeneratingUnitAdjustment.AdjustedOnlandVisualTrashAssessmentAreaID} already deleted");
                }

            }
            else if (trashGeneratingUnitAdjustment.DeletedGeometry != null)
            {
                var wellKnownText = trashGeneratingUnitAdjustment.DeletedGeometry.ToString();
                wellKnownText =
                    wellKnownText.Substring(wellKnownText.IndexOf("POLYGON", StringComparison.InvariantCulture));

                var geometryWKT = new SqlParameter("@GeometryWKT", wellKnownText);

                Logger.Info($"Command Timeout: {DbContext.Database.CommandTimeout}");
                DbContext.Database.ExecuteSqlCommand(
                    "dbo.pRebuildTrashGeneratingUnitTableRelativeExplicit @GeometryWKT", geometryWKT);

            }

            trashGeneratingUnitAdjustment.IsProcessed = true;
            trashGeneratingUnitAdjustment.ProcessedDate =
                DateTime.Now;
            DbContext.SaveChangesWithNoAuditing();
        }

        public static string FormatIDString(IEnumerable<int> idList)
        {
            return String.Join(",", idList);
        }
    }
}