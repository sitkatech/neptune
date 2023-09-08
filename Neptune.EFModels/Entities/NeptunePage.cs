namespace Neptune.EFModels.Entities;

public partial class NeptunePage
{
    public bool HasNeptunePageContent()
    {
        return !string.IsNullOrWhiteSpace(NeptunePageContent);
    }
}