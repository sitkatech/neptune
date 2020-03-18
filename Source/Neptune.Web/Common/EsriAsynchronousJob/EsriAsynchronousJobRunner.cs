using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace Neptune.Web.Common.EsriAsynchronousJob
{
    public class EsriAsynchronousJobRunner: IDisposable
    {
        public static HttpClient HttpClient { get; }
        public string BaseUrl { get; set; }
        public string ResultEndpoint { get; }

        public string PostUrl => $"{BaseUrl}/submitJob/?f=pjson";

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

        public T RunJob<T>(Object requestObject, out string responseRaw)
        {
            responseRaw = RunJobRaw(requestObject);
            return JsonConvert.DeserializeObject<T>(responseRaw);
        }

        public string RunJobRaw(Object requestObject)
        {
            var requestFormData = requestObject.ToKeyValue();
            //var requestContent = new FormUrlEncodedContent(requestFormData);

            var encodedItems = requestFormData.Select(i => WebUtility.UrlEncode(i.Key) + "=" + WebUtility.UrlEncode(i.Value));
            var encodedContent = new StringContent(String.Join("&", encodedItems), null, "application/x-www-form-urlencoded");

            var httpResponseMessage = HttpClient.PostAsync(PostUrl, encodedContent).Result;
            var responseContent = httpResponseMessage.Content.ReadAsStringAsync().Result;
            var jobStatusResponse = JsonConvert.DeserializeObject<EsriJobStatusResponse>(responseContent);

            JobID = jobStatusResponse.jobId;
            var jobStatusUrl = JobStatusUrl;
            var isExecuting = jobStatusResponse.IsExecuting();

            while (isExecuting)
            {
                Thread.Sleep(5000);
                var jobStatusHttpResponseMessage = HttpClient.GetAsync(jobStatusUrl).Result;
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
                    throw new EsriAsynchronousJobFailedException(jobStatusResponse.jobId);
                default:
                    // ReSharper disable once NotResolvedInText
                    throw new ArgumentOutOfRangeException("jobStatusResponse.jobStatus",
                        $"Unexpected job status from HRU job {jobStatusResponse.jobId}");
            }
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
        public EsriAsynchronousJobFailedException(string jobId): base($"{jobId} failed.")
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
        [JsonProperty("paramName")]
        public string ParameterName { get; set; }
        [JsonProperty("dataType")]
        public string DataType { get; set; }
        [JsonProperty("value")]
        public T Value { get; set; }
    }
}