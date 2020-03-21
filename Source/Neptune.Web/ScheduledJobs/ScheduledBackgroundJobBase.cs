﻿using System;
using System.Collections.Generic;
using log4net;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.ScheduledJobs
{
    public abstract class ScheduledBackgroundJobBase
    {
        /// <summary>
        /// A safety guard to ensure only one job is running at a time, some jobs seem like they would collide if allowed to run concurrently or possibly drag the server down.
        /// </summary>
        public static readonly object ScheduledBackgroundJobLock = new object();

        public static string JobName
        {
            get { return "Background Job"; }
        }


        protected ILog Logger { get; }
        protected DatabaseEntities DbContext;

        /// <summary> 
        /// Jobs must have a proscribed environment to run in (for example, to prevent a job that makes a lot of calls to an external API from accidentally DOSing that API by running on all local boxes, QA, and Prod at the same time.
        /// </summary>
        public abstract List<NeptuneEnvironmentType> RunEnvironments { get; }

        protected ScheduledBackgroundJobBase()
        {
            Logger = LogManager.GetLogger(GetType());
            var databaseEntities = new DatabaseEntities(); // default to Sitka
            databaseEntities.Configuration.AutoDetectChangesEnabled = false;
            DbContext = databaseEntities;
        }

        protected ScheduledBackgroundJobBase(DatabaseEntities dbContext)
        {
            Logger = LogManager.GetLogger(GetType());

            DbContext = dbContext;
        }

        /// <summary>
        /// This wraps the call to <see cref="RunJobImplementation"/> with all of the housekeeping for being a scheduled job.
        /// </summary>
        public void RunJob()
        {
            lock (ScheduledBackgroundJobLock)
            {
                // No-Op if we're not running in an allowed environment
                if (!RunEnvironments.Contains(NeptuneWebConfiguration.NeptuneEnvironment.NeptuneEnvironmentType))
                {
                    return;
                }

                try
                {
                    Logger.Info($"Begin Neptune Job {JobName}");
                    RunJobImplementation();
                    Logger.Info($"End Neptune Job {JobName}");
                }
                catch (Exception ex)
                {
                    // Wrap and rethrow with the information about which job encountered the problem
                    Logger.Error(ex.Message);
                    throw new ScheduledBackgroundJobException(JobName, ex);
                }
            }
        }

        /// <summary>
        /// Jobs can fill this in with whatever they need to run. This is called by <see cref="RunJob"/> which handles other miscellaneous stuff
        /// </summary>
        protected abstract void RunJobImplementation(); 
    }
}
