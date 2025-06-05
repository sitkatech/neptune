namespace Neptune.Jobs.EsriAsynchronousJob;

public class EsriAsynchronousJobCancelledException(string jobId)
    : EsriAsynchronousJobException($"{jobId} cancelled unexpectedly");