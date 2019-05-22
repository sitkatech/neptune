using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using Neptune.Web.Common;

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

            if (trashGeneratingUnitAdjustment.AdjustedDelineation != null)
            {
                // TODO: execute the stored proc for the adjustment

            }
            else if (trashGeneratingUnitAdjustment.AdjustedOnlandVisualTrashAssessmentArea != null)
            {
                // TODO: execute the stored proc for the adjustment

            }
            else if (trashGeneratingUnitAdjustment.DeletedGeometry != null)
            {
                // TODO: execute the stored proc for the adjustment
            }


            trashGeneratingUnitAdjustment.IsProcessed = true;
            trashGeneratingUnitAdjustment.ProcessedDate =
                DateTime.Now;
            DbContext.SaveChanges();
        }
    }
}