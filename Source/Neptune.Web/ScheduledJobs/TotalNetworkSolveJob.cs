using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.ScheduledJobs
{
    public class TotalNetworkSolveJob : ScheduledBackgroundJobBase
    {
        public new static string JobName => "Nereid Total Network Solve";

        public HttpClient HttpClient { get; set; }

        public TotalNetworkSolveJob() : base()
        {
            HttpClient = new HttpClient();
        }

        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Local,
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Qa
        };

        protected override void RunJobImplementation()
        {
            // clear out all dirty nodes since the whole network is being run.
            DbContext.DirtyModelNodes.DeleteDirtyModelNode(DbContext.DirtyModelNodes.ToList());
            NereidUtilities.TotalNetworkSolve(out _, out _, out _, DbContext, HttpClient);
        }
    }
    
    public class DeltaSolveJob : ScheduledBackgroundJobBase
    {
        public new static string JobName => "Nereid Delta Solve";

        public HttpClient HttpClient { get; set; }

        public DeltaSolveJob() : base()
        {
            HttpClient = new HttpClient();
        }

        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Local,
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Qa
        };

        protected override void RunJobImplementation()
        {
            var dirtyModelNodes = DbContext.DirtyModelNodes.ToList();
            NereidUtilities.DeltaSolve(out _, out _, out _, DbContext, HttpClient, dirtyModelNodes);
        }
    }
}
