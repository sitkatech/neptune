using System.Net.Mail;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Common.Email;
using Neptune.EFModels.Entities;

namespace Neptune.Jobs.Hangfire
{
    public abstract class ScheduledBackgroundJobBase<T>
    {
        /// <summary>
        /// A safety guard to ensure only one job is running at a time, some jobs seem like they would collide if allowed to run concurrently or possibly drag the server down.
        /// </summary>
        public static readonly object ScheduledBackgroundJobLock = new();

        private readonly string _jobName;
        protected readonly ILogger<T> Logger;
        protected readonly IWebHostEnvironment _webHostEnvironment;
        protected readonly NeptuneDbContext DbContext;
        protected readonly NeptuneJobConfiguration NeptuneJobConfiguration;
        protected readonly SitkaSmtpClientService _sitkaSmtpClient;

        /// <summary> 
        /// Jobs must have a proscribed environment to run in (for example, to prevent a job that makes a lot of calls to an external API from accidentally DOSing that API by running on all local boxes, QA, and Prod at the same time.
        /// </summary>
        public abstract List<RunEnvironment> RunEnvironments { get; }


        protected ScheduledBackgroundJobBase(string jobName, ILogger<T> logger, IWebHostEnvironment webHostEnvironment, NeptuneDbContext dbContext, IOptions<NeptuneJobConfiguration> neptuneJobConfiguration, SitkaSmtpClientService sitkaSmtpClient)
        {
            _jobName = jobName;
            Logger = logger;
            _webHostEnvironment = webHostEnvironment;
            DbContext = dbContext;
            NeptuneJobConfiguration = neptuneJobConfiguration.Value;
            _sitkaSmtpClient = sitkaSmtpClient;
        }

        /// <summary>
        /// This wraps the call to <see cref="RunJobImplementation"/> with all of the housekeeping for being a scheduled job.
        /// </summary>
        public void RunJob(IJobCancellationToken token)
        {
            lock (ScheduledBackgroundJobLock)
            {
                // No-Op if we're not running in an allowed environment
                if (_webHostEnvironment.IsDevelopment() && !RunEnvironments.Contains(RunEnvironment.Development))
                {
                    return;
                }
                if (_webHostEnvironment.IsStaging() && !RunEnvironments.Contains(RunEnvironment.Staging))
                {
                    return;
                }
                if (_webHostEnvironment.IsProduction() && !RunEnvironments.Contains(RunEnvironment.Production))
                {
                    return;
                }

                token.ThrowIfCancellationRequested();

                try
                {
                    Logger.LogInformation($"Begin Job {_jobName}");
                    RunJobImplementation();
                    Logger.LogInformation($"End Job {_jobName}");
                }
                catch (Exception ex)
                {
                    // Wrap and rethrow with the information about which job encountered the problem
                    Logger.LogError(ex.Message);
                    var mailMessage = new MailMessage
                    {
                        Subject = $"Neptune Hangfire Job Failed: Job {_jobName}",
                        Body = $"Details: <br /><br />{ex.Message}",
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(new MailAddress(NeptuneJobConfiguration.SitkaSupportEmail));
                    
                    _sitkaSmtpClient.Send(mailMessage);
                    throw new ScheduledBackgroundJobException(_jobName, ex);
                }
            }
        }

        /// <summary>
        /// Jobs can fill this in with whatever they need to run. This is called by <see cref="RunJob"/> which handles other miscellaneous stuff
        /// </summary>
        protected abstract void RunJobImplementation(); 
    }
}
