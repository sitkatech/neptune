namespace Neptune.Web.Common.EsriAsynchronousJob
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