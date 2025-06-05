namespace Neptune.Jobs.EsriAsynchronousJob;

public class EsriAsynchronousJobFailedException(
    EsriJobStatusResponse jobStatusResponse,
    string requestObjectString)
    : EsriAsynchronousJobException(
        $"{jobStatusResponse.jobId} failed. Last messages: {string.Join(", ", jobStatusResponse.messages.Select(x => x.description))}. Last request Object: {requestObjectString}");