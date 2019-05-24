﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Hangfire;
using Hangfire.Client;
using Hangfire.Common;
using Hangfire.Server;
using Hangfire.SqlServer;
using Hangfire.Storage;
using Owin;
using Neptune.Web.Common;

namespace Neptune.Web.ScheduledJobs
{
    /// <summary>
    /// All the logic to get the Corral Scheduled Jobs wired up in <see cref="Hangfire"/>
    /// </summary>
    public class ScheduledBackgroundJobBootstrapper
    {
        /// <summary>
        /// Configuration entry point for <see cref="Startup"/> via the <see cref="Microsoft.Owin.OwinStartupAttribute"/>
        /// </summary>
        public static void ConfigureHangfireAndScheduledBackgroundJobs(IAppBuilder app)
        {
            ConfigureHangfire(app);
            ConfigureScheduledBackgroundJobs();
        }

        /// <summary>
        /// Setup the basics for <see cref="Hangfire"/>, database connectivity and security on urls
        /// </summary>
        private static void ConfigureHangfire(IAppBuilder app)
        {
            var sqlServerStorageOptions = new SqlServerStorageOptions
            {
                // We have scripted out the Hangfire tables, so we tell Hangfire not to insert them. 
                // This might be an issue when Hangfire does a change to its schema, but this should work for now.
                PrepareSchemaIfNecessary = true
            };
            GlobalConfiguration.Configuration.UseSqlServerStorage(NeptuneWebConfiguration.DatabaseConnectionString,
                sqlServerStorageOptions);

            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                WorkerCount = 1,
            }); // 11/03/2015 MF - limit the number of worker threads, we really don't need that many - in fact we try to have each job run serially for the time being because we're not sure how concurrent the different jobs could really be.

            // Hangfire defaults to 10 retries for failed jobs; this disables that behavior by doing no automatic retries.
            // http://hangfire.readthedocs.org/en/latest/background-processing/dealing-with-exceptions.html
            // Note that specific jobs may override this; look for uses of the AutomaticRetry symbol on specific job start functions.
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireNeptuneWebAuthorizationFilter() }
            });
        }

        /// <summary>
        /// Set up the jobs particular to this application
        /// </summary>
        private static void ConfigureScheduledBackgroundJobs()
        {
            var recurringJobIds = new List<string>();

            AddRecurringJob(TrashGeneratingUnitRefreshScheduledBackgroundJob.JobName,
                () => ScheduledBackgroundJobLaunchHelper.RunTrashGeneratingUnitRefreshScheduledBackgroundJob(),
                MakeDailyUtcCronJobStringFromLocalTime(1, 23), recurringJobIds);

            AddMinutelyRecurringJob(TrashGeneratingUnitAdjustmentScheduledBackgroundJob.JobName,
                () => RunTrashGeneratingUnitAdjustmentScheduledBackgroundJob(),
                 recurringJobIds);
            
            // Remove any jobs we haven't explicity scheduled
            RemoveExtraneousJobs(recurringJobIds);
        }

        [DisableMultipleQueuedItemsFilter]
        public static void RunTrashGeneratingUnitAdjustmentScheduledBackgroundJob()
        {
            ScheduledBackgroundJobLaunchHelper.RunTrashGeneratingUnitAdjustmentScheduledBackgroundJob();
        }

        private static void AddRecurringJob(string jobName, Expression<Action> methodCallExpression,
            string cronExpression, List<string> recurringJobIds)
        {
            RecurringJob.AddOrUpdate(jobName, methodCallExpression, cronExpression);
            recurringJobIds.Add(jobName);
        }

        /// <summary>
        /// Adds a recurring job that will run every 5 minutes
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="methodCallExpression"></param>
        /// <param name="recurringJobIds"></param>
        private static void AddMinutelyRecurringJob(string jobName, Expression<Action> methodCallExpression,
            List<string> recurringJobIds)
        {
            RecurringJob.AddOrUpdate(jobName, methodCallExpression, "*/5 * * * *");
            recurringJobIds.Add(jobName);
        }

        private static void RemoveExtraneousJobs(List<string> recurringJobIds)
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                var recurringJobs = connection.GetRecurringJobs();
                var jobsToRemove = recurringJobs.Where(x => !recurringJobIds.Contains(x.Id)).ToList();
                foreach (var job in jobsToRemove)
                {
                    RecurringJob.RemoveIfExists(job.Id);
                }
            }
        }

        /// <summary>
        /// Hangfire defaults to a UTC time, so here convert from local time to UTC, then use the equivalent
        /// UTC time to create a cron string.
        /// 
        /// Since SetUpBackgroundHangfireJobs should be re-run when the webserver restarts, this should get
        /// updated enough to handle the problems associated with DST/UTC/TimeZone conversions. At the least,
        /// problems won't hang around for too long since AddOrUpdate will adjust the time to be the correct one
        /// after a DST change. -- SLG 03/16/2015
        /// </summary>
        private static string MakeDailyUtcCronJobStringFromLocalTime(int hour, int minute)
        {
            var utcCronTime = MakeUtcCronTime(hour, minute);
            return Cron.Daily(utcCronTime.Hour, utcCronTime.Minute);
        }

        private static string MakeYearlyUtcCronJobStringFromLocalTime(int month, int day, int hour, int minute)
        {
            var utcCronTime = MakeUtcCronTime(month, day, hour, minute);
            return Cron.Yearly(utcCronTime.Month, utcCronTime.Day, utcCronTime.Hour, utcCronTime.Minute);
        }

        private static DateTime MakeUtcCronTime(int hour, int minute)
        {
            var now = DateTime.Now;
            return MakeUtcCronTime(now.Year, now.Month, now.Day, hour, minute);
        }

        private static DateTime MakeUtcCronTime(int month, int day, int hour, int minute)
        {
            var now = DateTime.Now;
            return MakeUtcCronTime(now.Year, month, day, hour, minute);
        }

        private static DateTime MakeUtcCronTime(int year, int month, int day, int hour, int minute)
        {
            var localCrontTime = new DateTime(year, month, day, hour, minute, 0, DateTimeKind.Local);
            var utcCronTime = TimeZoneInfo.ConvertTimeToUtc(localCrontTime);
            return utcCronTime;
        }
    }
    public class DisableMultipleQueuedItemsFilter : JobFilterAttribute, IClientFilter, IServerFilter
    {
        private static readonly TimeSpan LockTimeout = TimeSpan.FromSeconds(5);
        private static readonly TimeSpan FingerprintTimeout = TimeSpan.FromHours(1);

        public void OnCreating(CreatingContext filterContext)
        {
            if (!AddFingerprintIfNotExists(filterContext.Connection, filterContext.Job))
            {
                filterContext.Canceled = true;
            }
        }

        public void OnPerformed(PerformedContext filterContext)
        {
            RemoveFingerprint(filterContext.Connection, filterContext.BackgroundJob);
        }

        private static bool AddFingerprintIfNotExists(IStorageConnection connection, Job job)
        {
            using (connection.AcquireDistributedLock(GetFingerprintLockKey(job), LockTimeout))
            {
                var fingerprint = connection.GetAllEntriesFromHash(GetFingerprintKey(job));

                DateTimeOffset timestamp;

                if (fingerprint != null &&
                    fingerprint.ContainsKey("Timestamp") &&
                    DateTimeOffset.TryParse(fingerprint["Timestamp"], null, DateTimeStyles.RoundtripKind, out timestamp) &&
                    DateTimeOffset.UtcNow <= timestamp.Add(FingerprintTimeout))
                {
                    // Actual fingerprint found, returning.
                    return false;
                }

                // Fingerprint does not exist, it is invalid (no `Timestamp` key),
                // or it is not actual (timeout expired).
                connection.SetRangeInHash(GetFingerprintKey(job), new Dictionary<string, string>
            {
                { "Timestamp", DateTimeOffset.UtcNow.ToString("o") }
            });

                return true;
            }
        }

        private static void RemoveFingerprint(IStorageConnection connection, BackgroundJob job)
        {
            using (connection.AcquireDistributedLock(GetFingerprintLockKey(job.Job), LockTimeout))
            using (var transaction = connection.CreateWriteTransaction())
            {
                transaction.RemoveHash(GetFingerprintKey(job.Job));
                transaction.Commit();
            }
        }

        private static string GetFingerprintLockKey(Job job)
        {
            return $"{GetFingerprintKey(job)}:lock";
        }

        private static string GetFingerprintKey(Job job)
        {
            return $"fingerprint:{GetFingerprint(job)}";
        }

        private static string GetFingerprint(Job job)
        {
            string parameters = string.Empty;
            if (job.Args != null)
            {
                parameters = string.Join(".", job.Args);
            }
            if (job.Type == null || job.Method == null)
            {
                return string.Empty;
            }
            var fingerprint = $"{job.Type.FullName}.{job.Method.Name}.{parameters}";

            return fingerprint;
        }

        void IClientFilter.OnCreated(CreatedContext filterContext)
        {
        }

        void IServerFilter.OnPerforming(PerformingContext filterContext)
        {
        }
    }
}

