using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using Newtonsoft.Json;

namespace Neptune.API.Models.EsriAsynchronousJob
{
    public class EsriAsynchronousJobRunner: IDisposable
    {
        private static readonly int MAX_RETRIES = 3;
        private static readonly int DEFAULT_MILLISECONDS_TIMEOUT = 5000;
        public static HttpClient HttpClient { get; }
        public string BaseUrl { get; set; }
        public string ResultEndpoint { get; }

        public string PostUrl => $"{BaseUrl}/submitJob/";

        public string JobStatusUrl => $"{BaseUrl}/jobs/{JobID}/?f=pjson";
        public string JobResultUrl => $"{BaseUrl}/jobs/{JobID}/results/{ResultEndpoint}/?f=pjson";

        static EsriAsynchronousJobRunner()
        {
            HttpClient = new HttpClient();
        }

        public EsriAsynchronousJobRunner(string baseUrl, string resultEndpoint)
        {
            BaseUrl = !baseUrl.EndsWith("/") ? baseUrl : baseUrl.Replace("/", "");
            ResultEndpoint = resultEndpoint;
        }

        public T RunJob<T>(object requestObject, out string responseRaw)
        {
            responseRaw = RunJobRaw(requestObject);
            return JsonConvert.DeserializeObject<T>(responseRaw);
        }

        public string RunJobRaw(Object requestObject)
        {
            var requestFormData = new Dictionary<string, string>();//todo:requestObject.ToKeyValue();
            //var requestContent = new FormUrlEncodedContent(requestFormData);

            requestFormData.Add("env:outSR", "");
            requestFormData.Add("env:processSR", "");
            requestFormData.Add("context", "");

            var retry = true;
            var attempts = 0;
            EsriJobStatusResponse jobStatusResponse = null;
            while (retry && attempts < MAX_RETRIES)
            {
                jobStatusResponse = SubmitJob(requestFormData);
                JobID = jobStatusResponse.jobId;
                int timeout;
                // wait 5 seconds before checking for process on first attempt, 30 on second, and 90 on third
                switch (attempts)
                {
                    case 0:
                        timeout = 5000;
                        break;
                    case 1:
                        timeout = 30000;
                        break;
                    case 2:
                        timeout = 90000;
                        break;
                    default:
                        timeout = 5000;
                        break;
                }
                retry = CheckShouldRetry(timeout);
                attempts++;
            }

            if (retry && attempts >= MAX_RETRIES)
            {
                throw new TimeoutException("Remote service failed to respond within the timeout.");
            }

            var isExecuting = jobStatusResponse.IsExecuting();

            while (isExecuting)
            {
                Thread.Sleep(DEFAULT_MILLISECONDS_TIMEOUT);
                var jobStatusHttpResponseMessage = HttpClient.GetAsync(JobStatusUrl).Result;
                jobStatusResponse =
                    JsonConvert.DeserializeObject<EsriJobStatusResponse>(jobStatusHttpResponseMessage.Content
                        .ReadAsStringAsync().Result);
                isExecuting = jobStatusResponse.IsExecuting();
            }

            switch (jobStatusResponse.jobStatus)
            {
                case EsriJobStatus.esriJobSucceeded:
                    var resultContent = HttpClient.GetAsync(JobResultUrl).Result.Content.ReadAsStringAsync().Result;
                    return resultContent;
                case EsriJobStatus.esriJobCancelling:
                case EsriJobStatus.esriJobCancelled:
                    throw new EsriAsynchronousJobCancelledException(jobStatusResponse.jobId);
                case EsriJobStatus.esriJobFailed:
                    throw new EsriAsynchronousJobFailedException(jobStatusResponse, requestObject.ToString());
                default:
                    // ReSharper disable once NotResolvedInText
                    throw new ArgumentOutOfRangeException("jobStatusResponse.jobStatus",
                        $"Unexpected job status from HRU job {jobStatusResponse.jobId}. Last message: {jobStatusResponse.messages.Last().description}");
            }
        }

        private bool CheckShouldRetry(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
            var jobStatusHttpResponseMessage = HttpClient.GetAsync(JobStatusUrl).Result;
            var jobStatusResponse =
                JsonConvert.DeserializeObject<EsriJobStatusResponse>(jobStatusHttpResponseMessage.Content
                    .ReadAsStringAsync().Result);

            // if we don't have any messages on the status response,
            // this request ended up in a bad state and we should just abandon it and try again.
            // A little rude of us to send two requests for the same data, but the server should
            // respond correctly the first time if it doesn't want us to keep asking.
            return jobStatusResponse.messages.Count == 0;
        }

        private EsriJobStatusResponse SubmitJob(IDictionary<string, string> requestFormData)
        {
            var encodedItems = requestFormData.Select(i => WebUtility.UrlEncode(i.Key) + "=" + WebUtility.UrlEncode(i.Value));
            var encodedContent = new StringContent(String.Join("&", encodedItems), null, "application/x-www-form-urlencoded");

            var httpResponseMessage = HttpClient.PostAsync(PostUrl, encodedContent).Result;
            var responseContent = httpResponseMessage.Content.ReadAsStringAsync().Result;
            var jobStatusResponse = JsonConvert.DeserializeObject<EsriJobStatusResponse>(responseContent);
            return jobStatusResponse;
        }

        public string JobID { get; set;  }

        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }

    public class EsriAsynchronousJobUnknownErrorException : Exception
    {
        public EsriAsynchronousJobUnknownErrorException(string message, Exception innerException) : base(message,
            innerException)
        {

        }
    }

    public class EsriAsynchronousJobFailedException : Exception
    {
        public EsriAsynchronousJobFailedException(EsriJobStatusResponse jobStatusResponse, string requestObjectString) : base($"{jobStatusResponse.jobId} failed. Last messages: {string.Join(", ", jobStatusResponse.messages.Select(x => x.description))}. Last request Oboject: {requestObjectString}")
        {
        }
    }

    public class EsriAsynchronousJobCancelledException : Exception
    {
        public EsriAsynchronousJobCancelledException(string jobId) : base($"{jobId} cancelled unexpectedly")
        {
        }
    }

    public static class EsriAsyncronousJobRunnerExtensions
    {
        public static bool IsExecuting(this EsriJobStatusResponse jobStatusResponse)
        {
            return jobStatusResponse.jobStatus == EsriJobStatus.esriJobExecuting
                   || jobStatusResponse.jobStatus == EsriJobStatus.esriJobWaiting
                   || jobStatusResponse.jobStatus == EsriJobStatus.esriJobSubmitted;
        }
    }

    public class EsriAsynchronousJobOutputParameter<T>
    {
        [JsonPropertyName("paramName")]
        public string ParameterName { get; set; }
        [JsonPropertyName("dataType")]
        public string DataType { get; set; }
        [JsonPropertyName("value")]
        public T Value { get; set; }
    }
}