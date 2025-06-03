namespace Neptune.Jobs.EsriAsynchronousJob;

public class EsriAsynchronousJobFailedException(
    EsriJobStatusResponse jobStatusResponse,
    string requestObjectString,
    int? hruLogID = null)
    : EsriAsynchronousJobException(
        $"{jobStatusResponse.jobId} failed. Last messages: {string.Join(", ", jobStatusResponse.messages.Select(x => x.description))}. Last request Oboject: {requestObjectString}",
        hruLogID);