namespace Neptune.API.Models.EsriAsynchronousJob
{
    public enum EsriJobStatus
    {
        esriJobWaiting,
        esriJobSubmitted,
        esriJobExecuting,
        esriJobSucceeded,
        esriJobFailed,
        esriJobCancelling,
        esriJobCancelled
    }
}