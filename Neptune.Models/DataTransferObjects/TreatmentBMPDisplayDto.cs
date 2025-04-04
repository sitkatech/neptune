﻿namespace Neptune.Models.DataTransferObjects;

public class TreatmentBMPDisplayDto
{
    public int TreatmentBMPID { get; set; }
    public string TreatmentBMPName { get; set; }
    public string TreatmentBMPTypeName { get; set; }
    public string DisplayName { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public int? ProjectID { get; set; }
    public bool InventoryIsVerified { get; set; }
    public bool IsFullyParameterized { get; set; }
    public TreatmentBMPModelingAttributeSimpleDto TreatmentBMPModelingAttribute { get; set; }
    public string WatershedName { get; set; }
    public string Notes { get; set; }
}