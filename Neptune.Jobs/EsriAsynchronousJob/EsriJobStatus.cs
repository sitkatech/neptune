namespace Neptune.Jobs.EsriAsynchronousJob
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