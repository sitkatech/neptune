namespace Neptune.EFModels.Entities;

public partial class vWaterQualityManagementPlanDetailed
{
    public string MaintenanceContactAddressToString()
    {
        return string.Join(" ",
            new List<string>
            {
                MaintenanceContactAddress1,
                MaintenanceContactAddress2,
                MaintenanceContactCity,
                MaintenanceContactState,
                MaintenanceContactZip
            }.Where(x => !string.IsNullOrWhiteSpace(x)));
    }
}