using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Attributes;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neptune.API.Controllers
{
    [ApiController]
    [Route("treatment-bmps/{treatmentBMPID}/treatment-bmp-images")]
    public class TreatmentBMPImageByTreatmentBMPController(NeptuneDbContext dbContext, ILogger<TreatmentBMPImageByTreatmentBMPController> logger, KeystoneService keystoneService, IOptions<NeptuneConfiguration> neptuneConfiguration, AzureBlobStorageService blobStorageService, PersonDto callingPerson)
        : SitkaController<TreatmentBMPImageByTreatmentBMPController>(dbContext, logger, keystoneService, neptuneConfiguration)
    {
        [HttpPost]
        [EntityNotFound(typeof(TreatmentBMP), "treatmentBMPID")]
        public async Task<ActionResult<TreatmentBMPImageDto>> Create([FromRoute] int treatmentBMPID, [FromForm] TreatmentBMPImageCreateDto imageCreateDto)
        {
            var errors = FileResources.ValidateFileUpload(imageCreateDto.File, true);
            if (!ModelState.IsValid || errors.Any())
            {
                errors.ForEach(x => ModelState.AddModelError(x.Type, x.Message));
                return BadRequest(ModelState);
            }

            var fileResource = await HttpUtilities.MakeFileResourceFromFormFileAsync(DbContext, HttpContext, blobStorageService, imageCreateDto.File);

            var treatmentBMPImageDto = await TreatmentBMPImages.CreateAsync(DbContext, treatmentBMPID, fileResource.FileResourceID, imageCreateDto, callingPerson.PersonID);
            return CreatedAtAction(nameof(Get), new { treatmentBMPID, treatmentBMPImageID = treatmentBMPImageDto.TreatmentBMPImageID }, treatmentBMPImageDto);
        }

        [HttpGet]
        [EntityNotFound(typeof(TreatmentBMP), "treatmentBMPID")]
        public async Task<ActionResult<IEnumerable<TreatmentBMPImageDto>>> List([FromRoute] int treatmentBMPID)
        {
            var treatmentBMPImageDtos = await TreatmentBMPImages.ListAsync(DbContext, treatmentBMPID);
            return Ok(treatmentBMPImageDtos);
        }

        [HttpGet("{treatmentBMPImageID}")]
        [EntityNotFound(typeof(TreatmentBMP), "treatmentBMPID")]
        [EntityNotFound(typeof(TreatmentBMPImage), "treatmentBMPImageID")]
        public async Task<ActionResult<TreatmentBMPImageDto>> Get([FromRoute] int treatmentBMPID, [FromRoute] int treatmentBMPImageID)
        {
            var treatmentBMPImageDto = await TreatmentBMPImages.GetAsync(DbContext, treatmentBMPID, treatmentBMPImageID);
            return Ok(treatmentBMPImageDto);
        }

        [HttpPut("{treatmentBMPImageID}")]
        [EntityNotFound(typeof(TreatmentBMP), "treatmentBMPID")]
        [EntityNotFound(typeof(TreatmentBMPImage), "treatmentBMPImageID")]
        public async Task<IActionResult> Update([FromRoute] int treatmentBMPID, [FromRoute] int treatmentBMPImageID, [FromBody] TreatmentBMPImageUpdateDto updateDto)
        {
            var updatedTreatmentBMPImageDto = await TreatmentBMPImages.UpdateAsync(DbContext, treatmentBMPID, treatmentBMPImageID, updateDto);
            return Ok(updatedTreatmentBMPImageDto);
        }

        [HttpDelete("{treatmentBMPImageID}")]
        [EntityNotFound(typeof(TreatmentBMP), "treatmentBMPID")]
        [EntityNotFound(typeof(TreatmentBMPImage), "treatmentBMPImageID")]
        public async Task<IActionResult> Delete([FromRoute] int treatmentBMPID, [FromRoute] int treatmentBMPImageID)
        {
            await TreatmentBMPImages.DeleteAsync(DbContext, treatmentBMPID, treatmentBMPImageID);
            return NoContent();
        }
    }
}
