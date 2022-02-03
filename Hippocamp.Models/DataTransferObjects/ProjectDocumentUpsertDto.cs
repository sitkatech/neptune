using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Hippocamp.Models.DataTransferObjects
{
    public class ProjectDocumentUpsertDto
    {
        [Required]
        public int ProjectID { get; set; }
        [Required]
        public IFormFile FileResource { get; set; }
        [Required]
        [StringLength(200)]
        public string DisplayName { get; set; }
        [StringLength(500)]
        public string DocumentDescription { get; set; }
    }
}
