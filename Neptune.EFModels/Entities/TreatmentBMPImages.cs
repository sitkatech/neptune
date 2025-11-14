using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static class TreatmentBMPImages
{
    public static async Task<TreatmentBMPImageDto> CreateAsync(NeptuneDbContext dbContext, int treatmentBMPID, int fileResourceID, TreatmentBMPImageCreateDto createDto, int callingPerson)
    {
        var treatmentBMPImage = new TreatmentBMPImage()
        {
            TreatmentBMPID = treatmentBMPID,
            FileResourceID = fileResourceID,
            Caption = createDto.Caption,
            UploadDate = DateOnly.FromDateTime(DateTime.UtcNow)
        };

        await dbContext.TreatmentBMPImages.AddAsync(treatmentBMPImage);
        await dbContext.SaveChangesAsync();
        await dbContext.Entry(treatmentBMPImage).ReloadAsync();

        var treatmentBMPImageDto = treatmentBMPImage.AsDto();
        return treatmentBMPImageDto;
    }

    public static async Task<List<TreatmentBMPImageDto>> ListAsync(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        var treatmentBMPImages = await dbContext.TreatmentBMPImages.AsNoTracking()
            .Include(x => x.FileResource)
            .Where(x => x.TreatmentBMPID == treatmentBMPID)
            .ToListAsync();

        var treatmentBMPImageDtos = treatmentBMPImages.Select(x => x.AsDto()).ToList();
        return treatmentBMPImageDtos;
    }

    public static async Task<TreatmentBMPImageDto?> GetAsync(NeptuneDbContext dbContext, int treatmentBMPID, int treatmentBMPImageID)
    {
        var treatmentBMPImage = await dbContext.TreatmentBMPImages.AsNoTracking()
            .Include(x => x.FileResource)
            .SingleOrDefaultAsync(x => x.TreatmentBMPID == treatmentBMPID && x.TreatmentBMPImageID == treatmentBMPImageID);

        var treatmentBMPImageDto = treatmentBMPImage?.AsDto();
        return treatmentBMPImageDto;
    }

    public static async Task<TreatmentBMPImageDto> UpdateAsync(NeptuneDbContext dbContext, int treatmentBMPID, int treatmentBMPImageID, TreatmentBMPImageUpdateDto updateDto)
    {
        var treatmentBMPImage = await dbContext.TreatmentBMPImages
            .SingleAsync(x => x.TreatmentBMPID == treatmentBMPID && x.TreatmentBMPImageID == treatmentBMPImageID);

        treatmentBMPImage.Caption = updateDto.Caption;

        await dbContext.SaveChangesAsync();
        await dbContext.Entry(treatmentBMPImage).ReloadAsync();

        var treatmentBMPImageDto = treatmentBMPImage.AsDto();
        return treatmentBMPImageDto;
    }

    public static async Task DeleteAsync(NeptuneDbContext dbContext, int treatmentBMPID, int treatmentBMPImageID)
    {
        await dbContext.TreatmentBMPImages
            .Where(x => x.TreatmentBMPID == treatmentBMPID && x.TreatmentBMPImageID == treatmentBMPImageID)
            .ExecuteDeleteAsync();
    }

    #region Used by WebMVC
    public static List<TreatmentBMPImage> ListByTreatmentBMPID(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        return GetImpl(dbContext).AsNoTracking().Where(x => x.TreatmentBMPID == treatmentBMPID).OrderBy(ht => ht.Caption).ToList();
    }

    public static List<TreatmentBMPImage> ListByTreatmentBMPIDWithChangeTracking(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        return GetImpl(dbContext).Where(x => x.TreatmentBMPID == treatmentBMPID).OrderBy(ht => ht.Caption).ToList();
    }

    public static TreatmentBMPImage GetByIDWithChangeTracking(NeptuneDbContext dbContext, int treatmentBMPImageID)
    {
        var treatmentBMPImage = GetImpl(dbContext)
            .SingleOrDefault(x => x.TreatmentBMPImageID == treatmentBMPImageID);
        Check.RequireNotNull(treatmentBMPImage, $"TreatmentBMPImage with ID {treatmentBMPImageID} not found!");
        return treatmentBMPImage;
    }

    public static TreatmentBMPImage GetByIDWithChangeTracking(NeptuneDbContext dbContext, TreatmentBMPImagePrimaryKey treatmentBMPImagePrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, treatmentBMPImagePrimaryKey.PrimaryKeyValue);
    }

    public static TreatmentBMPImage GetByID(NeptuneDbContext dbContext, int treatmentBMPImageID)
    {
        var treatmentBMPImage = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.TreatmentBMPImageID == treatmentBMPImageID);
        Check.RequireNotNull(treatmentBMPImage, $"TreatmentBMPImage with ID {treatmentBMPImageID} not found!");
        return treatmentBMPImage;
    }

    public static TreatmentBMPImage GetByID(NeptuneDbContext dbContext, TreatmentBMPImagePrimaryKey treatmentBMPImagePrimaryKey)
    {
        return GetByID(dbContext, treatmentBMPImagePrimaryKey.PrimaryKeyValue);
    }


    private static IQueryable<TreatmentBMPImage> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.TreatmentBMPImages
            .Include(x => x.FileResource);
    }

    #endregion
}