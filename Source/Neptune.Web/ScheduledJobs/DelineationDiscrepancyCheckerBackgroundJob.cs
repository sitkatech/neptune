using Neptune.Web.Common;
using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.ScheduledJobs
{
    public class DelineationDiscrepancyCheckerBackgroundJob : ScheduledBackgroundJobBase
    {
        public new static string JobName => "Delineation Discrepancy Checker";

        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Local,
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Qa
        };

        protected override void RunJobImplementation()
        {
            var dbContext = DbContext;
            dbContext.Database.CommandTimeout = 30000;
            dbContext.Database.ExecuteSqlCommand("EXEC dbo.pDelineationMarkThoseThatHaveDiscrepancies");
        }
    }

    // todo: remove this after it's run once in prod
    public class RefreshAssessmentScoreJob : ScheduledBackgroundJobBase
    {
        public new static string JobName => "(Temporary) Refresh Assessment Score Cache";

        public int PersonID { get; set; }
        public RefreshAssessmentScoreJob(int personID) : base()
        {
            PersonID = personID;
        }

        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Local,
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Qa
        };

        protected override void RunJobImplementation()
        {
            var person = DbContext.People.Find(PersonID);
            
            var dbContext = DbContext;
            foreach (var treatmentBMPAssessment in dbContext.TreatmentBMPAssessments)
            {
                treatmentBMPAssessment.CalculateIsAssessmentComplete();
                treatmentBMPAssessment.CalculateAssessmentScore();
            }

            dbContext.SaveChanges(person);
        }
    }
}