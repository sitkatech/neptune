namespace Neptune.EFModels.Entities;

public class TreatmentBMPWithModelingAttributesAndDelineation
{
    public TreatmentBMP TreatmentBMP { get; set; }
    public Delineation Delineation { get; set; }
    public vTreatmentBMPModelingAttribute? TreatmentBMPModelingAttribute { get; set; }

    public TreatmentBMPWithModelingAttributesAndDelineation(TreatmentBMP treatmentBMP, Delineation delineation, vTreatmentBMPModelingAttribute? treatmentBMPModelingAttribute)
    {
        TreatmentBMP = treatmentBMP;
        Delineation = delineation;
        TreatmentBMPModelingAttribute = treatmentBMPModelingAttribute;
    }
}