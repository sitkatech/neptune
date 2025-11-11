using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.API.Controllers
{
    [ApiController]
    [Route("treatment-bmps/{treatmentBMPID}/treatment-bmp-images")]
    public class TreatmentBMPImageByTreatmentBMPController : SitkaController<TreatmentBMPImageByTreatmentBMPController>
    {
        public TreatmentBMPImageByTreatmentBMPController(
            NeptuneDbContext dbContext,
            ILogger<TreatmentBMPImageByTreatmentBMPController> logger,
            KeystoneService keystoneService,
            IOptions<NeptuneConfiguration> neptuneConfiguration)
            : base(dbContext, logger, keystoneService, neptuneConfiguration)
        {
        }

        [HttpGet]
        public ActionResult<IEnumerable<TreatmentBMPImageDto>> List([FromRoute] int treatmentBMPID)
        {
            var images = DbContext.TreatmentBMPImages.Include(x => x.FileResource)
                .Where(x => x.TreatmentBMPID == treatmentBMPID)
                .Select(x => x.AsDto())
                .ToList();
            return Ok(images);
        }

        [HttpGet("{treatmentBMPImageID}")]
        public ActionResult<TreatmentBMPImageDto> Get([FromRoute] int treatmentBMPID, [FromRoute] int treatmentBMPImageID)
        {
            var image = DbContext.TreatmentBMPImages.Include(x => x.FileResource)
                .Where(x => x.TreatmentBMPID == treatmentBMPID && x.TreatmentBMPImageID == treatmentBMPImageID)
                .Select(x => x.AsDto())
                .FirstOrDefault();
            if (image == null)
            {
                return NotFound();
            }
            return Ok(image);
        }

        [HttpPost]
        public async Task<ActionResult<TreatmentBMPImageDto>> Create([FromRoute] int treatmentBMPID, [FromBody] TreatmentBMPImageDto dto)
        {
            // Validate FileResourceID
            var fileResource = await DbContext.FileResources.FindAsync(dto.FileResourceID);
            if (fileResource == null)
            {
                return BadRequest($"FileResourceID {dto.FileResourceID} does not exist.");
            }

            var entity = new TreatmentBMPImage
            {
                TreatmentBMPID = treatmentBMPID,
                FileResourceID = dto.FileResourceID,
                FileResource = fileResource,
                Caption = dto.Caption,
                UploadDate = DateOnly.FromDateTime(DateTime.UtcNow)
            };
            DbContext.TreatmentBMPImages.Add(entity);
            await DbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { treatmentBMPID, treatmentBMPImageID = entity.TreatmentBMPImageID }, entity.AsDto());
        }

        [HttpPut("{treatmentBMPImageID}")]
        public async Task<IActionResult> Update([FromRoute] int treatmentBMPID, [FromRoute] int treatmentBMPImageID, [FromBody] TreatmentBMPImageDto dto)
        {
            var entity = await DbContext.TreatmentBMPImages
                .FirstOrDefaultAsync(x => x.TreatmentBMPID == treatmentBMPID && x.TreatmentBMPImageID == treatmentBMPImageID);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Caption = dto.Caption;
            await DbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{treatmentBMPImageID}")]
        public async Task<IActionResult> Delete([FromRoute] int treatmentBMPID, [FromRoute] int treatmentBMPImageID)
        {
            var entity = await DbContext.TreatmentBMPImages
                .FirstOrDefaultAsync(x => x.TreatmentBMPID == treatmentBMPID && x.TreatmentBMPImageID == treatmentBMPImageID);
            if (entity == null)
            {
                return NotFound();
            }
            DbContext.TreatmentBMPImages.Remove(entity);
            await DbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
