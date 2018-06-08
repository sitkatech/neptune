using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Web.Models
{
    public class ManagePhotoWithPreviewPhotoSimple
    {
        [Required]
        public int PrimaryKey { get; set; }

        [DisplayName("Caption")]
        public string Caption { get; set; }

        [DisplayName("Delete")]
        public bool FlagForDeletion { get; set; }
    }
}
