using System.ComponentModel.DataAnnotations;

namespace Neptune.Models.DataTransferObjects
{
    public class ProjectDocumentUpdateDto
    {
        [Required]
        public int ProjectID { get; set; }
        [Required] [StringLength(200)] 
        public string DisplayName { get; set; }
        [StringLength(500)] 
        public string DocumentDescription { get; set; }
    }
}
