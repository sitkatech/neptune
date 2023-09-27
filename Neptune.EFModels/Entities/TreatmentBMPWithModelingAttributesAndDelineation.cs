namespace Neptune.EFModels.Entities;

public class TreatmentBMPWithModelingAttributesAndDelineation
{
    public TreatmentBMP TreatmentBMP { get; set; }
    public Delineation Delineation { get; set; }

    public TreatmentBMPWithModelingAttributesAndDelineation(TreatmentBMP treatmentBMP, Delineation delineation)
    {
        TreatmentBMP = treatmentBMP;
        Delineation = delineation;
    }
}