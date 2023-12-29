namespace Neptune.Jobs.EsriAsynchronousJob;

public class EsriAsynchronousJobCancelledException : Exception
{
    public EsriAsynchronousJobCancelledException(string jobId) : base($"{jobId} cancelled unexpectedly")
    {
    }
}