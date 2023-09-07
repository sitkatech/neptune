using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Models.DataTransferObjects
{
    public class ManagePhotoWithPreviewPhotoDto
    {
        [Required]
        public int PrimaryKey { get; set; }

        [DisplayName("Caption")]
        public string Caption { get; set; }

        [DisplayName("Delete")]
        public bool FlagForDeletion { get; set; }
    }
}
