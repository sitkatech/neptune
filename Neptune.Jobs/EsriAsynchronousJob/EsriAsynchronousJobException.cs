namespace Neptune.Jobs.EsriAsynchronousJob;

public class EsriAsynchronousJobException(string message,
    int? hruLogID = null) : Exception(message)
{
    public int? HRULogID { get; set; } = hruLogID;
}