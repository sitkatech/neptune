﻿using System;
using System.Collections.Generic;
using System.Net.Mail;
using Hangfire;
using Neptune.API.Services;
using Neptune.EFModels.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Neptune.API.Hangfire;

public abstract class ScheduledBackgroundJobBase<T>
{
    /// <summary>
    /// A safety guard to ensure only one job is running at a time, some jobs seem like they would collide if allowed to run concurrently or possibly drag the server down.
    /// </summary>
    private static readonly object ScheduledBackgroundJobLock = new object();

    private readonly string _jobName;
    protected readonly ILogger<T> Logger;
    private readonly IWebHostEnvironment _webHostEnvironment;
    protected readonly NeptuneDbContext DbContext;
    protected readonly NeptuneConfiguration NeptuneConfiguration;
    private readonly SitkaSmtpClientService _sitkaSmtpClient;

    /// <summary> 
    /// Jobs must have a proscribed environment to run in (for example, to prevent a job that makes a lot of calls to an external API from accidentally DOSing that API by running on all local boxes, QA, and Prod at the same time.
    /// </summary>
    public abstract List<RunEnvironment> RunEnvironments { get; }

    protected ScheduledBackgroundJobBase(string jobName, ILogger<T> logger, IWebHostEnvironment webHostEnvironment, NeptuneDbContext dbContext, IOptions<NeptuneConfiguration> neptuneConfiguration, SitkaSmtpClientService sitkaSmtpClient)
    {
        _jobName = jobName;
        Logger = logger;
        _webHostEnvironment = webHostEnvironment;
        DbContext = dbContext;
        NeptuneConfiguration = neptuneConfiguration.Value;
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

                mailMessage.To.Add(new MailAddress(NeptuneConfiguration.SITKA_EMAIL_REDIRECT));
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