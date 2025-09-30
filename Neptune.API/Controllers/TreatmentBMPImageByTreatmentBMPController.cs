using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // <-- Ensure Task is imported
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.API.Controllers
{
    [ApiController]
    [Route("treatment-bmps/{treatmentBMPID}/treatment-bmp-images")]
    public class TreatmentBMPImageByTreatmentBMPController : ControllerBase
    {
        private readonly NeptuneDbContext _dbContext;

        public TreatmentBMPImageByTreatmentBMPController(NeptuneDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TreatmentBMPImageDto>> List([FromRoute] int treatmentBMPID)
        {
            var images = _dbContext.TreatmentBMPImages.Include(x => x.FileResource)
                .Where(x => x.TreatmentBMPID == treatmentBMPID)
                .Select(x => x.AsDto())
                .ToList();
            return Ok(images);
        }

        [HttpGet("{treatmentBMPImageID}")]
        public ActionResult<TreatmentBMPImageDto> Get([FromRoute] int treatmentBMPID, [FromRoute] int treatmentBMPImageID)
        {
            var image = _dbContext.TreatmentBMPImages.Include(x => x.FileResource)
                .Where(x => x.TreatmentBMPID == treatmentBMPID && x.TreatmentBMPImageID == treatmentBMPImageID)
                .Select(x => x.AsDto())
                .FirstOrDefault();
            if (image == null)
                return NotFound();
            return Ok(image);
        }

        [HttpPost]
        public async Task<ActionResult<TreatmentBMPImageDto>> Create([FromRoute] int treatmentBMPID, [FromBody] TreatmentBMPImageDto dto)
        {
            // Validate FileResourceID
            var fileResource = await _dbContext.FileResources.FindAsync(dto.FileResourceID);
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
            _dbContext.TreatmentBMPImages.Add(entity);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { treatmentBMPID, treatmentBMPImageID = entity.TreatmentBMPImageID }, entity.AsDto());
        }

        [HttpPut("{treatmentBMPImageID}")]
        public async Task<IActionResult> Update([FromRoute] int treatmentBMPID, [FromRoute] int treatmentBMPImageID, [FromBody] TreatmentBMPImageDto dto)
        {
            var entity = await _dbContext.TreatmentBMPImages
                .FirstOrDefaultAsync(x => x.TreatmentBMPID == treatmentBMPID && x.TreatmentBMPImageID == treatmentBMPImageID);
            if (entity == null)
                return NotFound();
            entity.Caption = dto.Caption;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{treatmentBMPImageID}")]
        public async Task<IActionResult> Delete([FromRoute] int treatmentBMPID, [FromRoute] int treatmentBMPImageID)
        {
            var entity = await _dbContext.TreatmentBMPImages
                .FirstOrDefaultAsync(x => x.TreatmentBMPID == treatmentBMPID && x.TreatmentBMPImageID == treatmentBMPImageID);
            if (entity == null)
                return NotFound();
            _dbContext.TreatmentBMPImages.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
