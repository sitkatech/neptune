namespace Neptune.Jobs.EsriAsynchronousJob;

public class EsriAsynchronousJobFailedException : Exception
{
    public EsriAsynchronousJobFailedException(EsriJobStatusResponse jobStatusResponse, string requestObjectString) : base($"{jobStatusResponse.jobId} failed. Last messages: {string.Join(", ", jobStatusResponse.messages.Select(x => x.description))}. Last request Oboject: {requestObjectString}")
    {
    }
}