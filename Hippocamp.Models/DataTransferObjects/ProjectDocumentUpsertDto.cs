using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Hippocamp.Models.DataTransferObjects
{
    public class ProjectDocumentUpsertDto : IValidatableObject
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var acceptableExtensions = new List<string>{ ".pdf", ".zip", ".doc", ".docx", ".xls", ".xlsx", ".jpg", ".png" };
            var file = ((ProjectDocumentUpsertDto)validationContext.ObjectInstance).FileResource;
            var extension = Path.GetExtension(file.FileName);
            var size = file.Length;

            if (extension == null || !acceptableExtensions.Contains(extension.ToLower()))
                yield return new ValidationResult("File extension is not valid.", new[] { "FileResource" });

            if (size > 26214400)
                yield return new ValidationResult("File size is greater than 25MB.", new[] { "FileResource" });
        }
    }
}
