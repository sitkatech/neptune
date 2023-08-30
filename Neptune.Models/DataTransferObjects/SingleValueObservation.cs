namespace Neptune.Models.DataTransferObjects;

public class SingleValueObservation
{
    public string PropertyObserved { get; set; }
    public object ObservationValue { get; set; }
    public string Notes { get; set; }
}