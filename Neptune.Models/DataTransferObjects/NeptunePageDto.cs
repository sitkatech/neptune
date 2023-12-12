namespace Neptune.Models.DataTransferObjects
{
    public class NeptunePageDto
    {
        public int NeptunePageID { get; set; }
        public NeptunePageTypeSimpleDto NeptunePageType { get; set; }
        public string NeptunePageContent { get; set; }
        public bool IsEmptyContent { get; set; }
    }
}