namespace Hippocamp.Models.DataTransferObjects
{
    public partial class CustomPageDto
    {
        public bool IsEmptyContent => string.IsNullOrEmpty(CustomPageContent);
    }
}