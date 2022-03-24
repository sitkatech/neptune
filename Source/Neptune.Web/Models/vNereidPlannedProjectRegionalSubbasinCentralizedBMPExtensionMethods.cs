namespace Neptune.Web.Models
{
    public static class vNereidProjectRegionalSubbasinCentralizedBMPExtensionMethods
    {
        public static vNereidRegionalSubbasinCentralizedBMP tovNereidRegionalSubbasinCentralizedBMP(this vNereidProjectRegionalSubbasinCentralizedBMP vNereidProjectRegionalSubbasinCentralizedBMP)
        {
            return new vNereidRegionalSubbasinCentralizedBMP()
            {
                OCSurveyCatchmentID = vNereidProjectRegionalSubbasinCentralizedBMP.OCSurveyCatchmentID,
                RegionalSubbasinID = vNereidProjectRegionalSubbasinCentralizedBMP.RegionalSubbasinID,
                RowNumber = vNereidProjectRegionalSubbasinCentralizedBMP.RowNumber,
                TreatmentBMPID = vNereidProjectRegionalSubbasinCentralizedBMP.TreatmentBMPID,
                //3/16/22 at this point these will always be null, but in the future that may change
                UpstreamBMPID = vNereidProjectRegionalSubbasinCentralizedBMP.UpstreamBMPID
            };
        }
    }
}
