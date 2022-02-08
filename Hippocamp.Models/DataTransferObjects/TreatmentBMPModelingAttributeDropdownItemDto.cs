namespace Hippocamp.Models.DataTransferObjects
{
    public class TreatmentBMPModelingAttributeDropdownItemDto
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }

        public TreatmentBMPModelingAttributeDropdownItemDto()
        {
        }

        public TreatmentBMPModelingAttributeDropdownItemDto(int itemID, string itemName, string itemType)
        {
            ItemID = itemID;
            ItemName = itemName;
            ItemType = itemType;
        }
    }
}