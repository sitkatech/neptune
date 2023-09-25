namespace Neptune.Models.DataTransferObjects
{
    public class TreatmentBMPModelingAttributeDropdownItemDto
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string FieldName { get; set; }

        public TreatmentBMPModelingAttributeDropdownItemDto()
        {
        }

        public TreatmentBMPModelingAttributeDropdownItemDto(int itemID, string itemName, string fieldName)
        {
            ItemID = itemID;
            ItemName = itemName;
            FieldName = fieldName;
        }
    }
}