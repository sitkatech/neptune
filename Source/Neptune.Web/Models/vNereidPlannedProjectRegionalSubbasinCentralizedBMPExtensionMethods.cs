namespace Neptune.Web.Models
{
    public static class vNereidPlannedProjectRegionalSubbasinCentralizedBMPExtensionMethods
    {
        public static vNereidRegionalSubbasinCentralizedBMP tovNereidRegionalSubbasinCentralizedBMP(this vNereidPlannedProjectRegionalSubbasinCentralizedBMP vNereidPlannedProjectRegionalSubbasinCentralizedBMP)
        {
            return new vNereidRegionalSubbasinCentralizedBMP()
            {
                OCSurveyCatchmentID = vNereidPlannedProjectRegionalSubbasinCentralizedBMP.OCSurveyCatchmentID,
                RegionalSubbasinID = vNereidPlannedProjectRegionalSubbasinCentralizedBMP.RegionalSubbasinID,
                RowNumber = vNereidPlannedProjectRegionalSubbasinCentralizedBMP.RowNumber,
                TreatmentBMPID = vNereidPlannedProjectRegionalSubbasinCentralizedBMP.TreatmentBMPID,
                //3/16/22 at this point these will always be null, but in the future that may change
                UpstreamBMPID = vNereidPlannedProjectRegionalSubbasinCentralizedBMP.UpstreamBMPID
            };
        }
    }
}
