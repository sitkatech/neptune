namespace Neptune.Jobs.EsriAsynchronousJob;

public class EsriAsynchronousJobUnrecognizedStatusException(EsriJobStatusResponse jobStatusResponse) : EsriAsynchronousJobException($"Unexpected job status from HRU job {jobStatusResponse.jobId}. Last message: {jobStatusResponse.messages.Last().description}")
{
}