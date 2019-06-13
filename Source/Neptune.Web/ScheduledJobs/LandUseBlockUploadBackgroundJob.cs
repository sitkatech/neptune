using System;
using System.Collections.Generic;
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
            // you can use breakpoints inside hangfire jobs like normal. delete this line of code and comment once you have something real to attach to.
            var i = 1;
            //todo: process data from stagging table reporitng errors: on success send successful email, on failue send email of errors in upload file
            throw new NotImplementedException();
        }
    }
}