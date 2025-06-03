namespace Neptune.Jobs.EsriAsynchronousJob;

public class EsriAsynchronousJobCancelledException(string jobId, int? hruLogID = null)
    : EsriAsynchronousJobException($"{jobId} cancelled unexpectedly", hruLogID);