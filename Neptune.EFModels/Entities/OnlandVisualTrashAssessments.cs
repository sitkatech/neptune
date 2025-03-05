using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static class OnlandVisualTrashAssessments
{
    private static IQueryable<OnlandVisualTrashAssessment> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.OnlandVisualTrashAssessments
                .Include(x => x.StormwaterJurisdiction)
                .ThenInclude(x => x.Organization)
                .Include(x => x.OnlandVisualTrashAssessmentArea)
                .Include(x => x.OnlandVisualTrashAssessmentObservations)
                .Include(x => x.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes)
                .Include(x => x.CreatedByPerson)
            ;
    }

    public static OnlandVisualTrashAssessment GetByIDWithChangeTracking(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentID)
    {
        var onlandVisualTrashAssessment = GetImpl(dbContext)
            .SingleOrDefault(x => x.OnlandVisualTrashAssessmentID == onlandVisualTrashAssessmentID);
        Check.RequireNotNull(onlandVisualTrashAssessment, $"OnlandVisualTrashAssessment with ID {onlandVisualTrashAssessmentID} not found!");
        return onlandVisualTrashAssessment;
    }

    public static OnlandVisualTrashAssessment GetByIDWithChangeTracking(NeptuneDbContext dbContext, OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, onlandVisualTrashAssessmentPrimaryKey.PrimaryKeyValue);
    }

    public static OnlandVisualTrashAssessment GetByID(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentID)
    {
        var onlandVisualTrashAssessment = GetImpl(dbContext).AsNoTracking()
            .Include(x => x.OnlandVisualTrashAssessmentObservations)
            .ThenInclude(x => x.OnlandVisualTrashAssessmentObservationPhotos)
            .ThenInclude(x => x.FileResource)
            .SingleOrDefault(x => x.OnlandVisualTrashAssessmentID == onlandVisualTrashAssessmentID);
        Check.RequireNotNull(onlandVisualTrashAssessment, $"OnlandVisualTrashAssessment with ID {onlandVisualTrashAssessmentID} not found!");
        return onlandVisualTrashAssessment;
    }

    public static OnlandVisualTrashAssessment GetByID(NeptuneDbContext dbContext, OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
    {
        return GetByID(dbContext, onlandVisualTrashAssessmentPrimaryKey.PrimaryKeyValue);
    }

    public static List<OnlandVisualTrashAssessment> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).AsNoTracking().ToList().OrderByDescending(x => x.CompletedDate).ThenBy(x => x.OnlandVisualTrashAssessmentArea == null)
            .ThenBy(x => x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentAreaName).ToList();

    }

    public static OnlandVisualTrashAssessment GetByIDForFeatureContextCheck(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentID)
    {
        var onlandVisualTrashAssessment = dbContext.OnlandVisualTrashAssessments
            .Include(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.Organization).AsNoTracking()
            .SingleOrDefault(x => x.OnlandVisualTrashAssessmentID == onlandVisualTrashAssessmentID);
        Check.RequireNotNull(onlandVisualTrashAssessment, $"OnlandVisualTrashAssessment with ID {onlandVisualTrashAssessmentID} not found!");
        return onlandVisualTrashAssessment;
    }

    public static List<OnlandVisualTrashAssessment> ListByOnlandVisualTrashAssessmentAreaID(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentAreaID)
    {
        return GetImpl(dbContext).AsNoTracking().Where(x => x.OnlandVisualTrashAssessmentAreaID == onlandVisualTrashAssessmentAreaID).OrderByDescending(x => x.CompletedDate).ToList();
    }

    public static List<OnlandVisualTrashAssessment> ListByStormwaterJurisdictionIDList(NeptuneDbContext dbContext, IEnumerable<int> stormwaterJurisdictionIDList)
    {
        return GetImpl(dbContext).AsNoTracking().Where(x => stormwaterJurisdictionIDList.Contains(x.StormwaterJurisdictionID)).ToList().OrderByDescending(x => x.CompletedDate).ThenBy(x => x.OnlandVisualTrashAssessmentArea == null)
            .ThenBy(x => x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentAreaName).ToList();
    }

    public static List<OnlandVisualTrashAssessmentGridDto> ListByStormwaterJurisdictionIDAsGridDto(NeptuneDbContext dbContext, IEnumerable<int> stormwaterJurisdictionIDList)
    {
        return dbContext.OnlandVisualTrashAssessments
            .Include(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.Organization)
            .Include(x => x.OnlandVisualTrashAssessmentArea)
            .Include(x => x.CreatedByPerson).AsNoTracking().Where(x => stormwaterJurisdictionIDList.Contains(x.StormwaterJurisdictionID)).ToList()
            .OrderByDescending(x => x.CompletedDate)
            .ThenBy(x => x.OnlandVisualTrashAssessmentArea == null)
            .ThenBy(x => x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentAreaName)
            .Select(x => x.AsGridDto()).ToList();
    }

    public static OnlandVisualTrashAssessment? GetTransectBackingAssessment(NeptuneDbContext dbContext, int? onlandVisualTrashAssessmentAreaID)
    {
        return ListByOnlandVisualTrashAssessmentAreaID(dbContext, onlandVisualTrashAssessmentAreaID.Value).SingleOrDefault(x =>
            x.IsTransectBackingAssessment);
    }

    public static async Task<OnlandVisualTrashAssessmentSimpleDto> CreateNew(NeptuneDbContext dbContext, OnlandVisualTrashAssessmentSimpleDto dto, PersonDto currentUser)
    {
        var onlandVisualTrashAssessment = new OnlandVisualTrashAssessment()
        {
            CreatedByPersonID = currentUser.PersonID,
            AssessingNewArea = dto.AssessingNewArea,
            CreatedDate = DateTime.UtcNow,
            OnlandVisualTrashAssessmentAreaID = dto.OnlandVisualTrashAssessmentAreaID,
            StormwaterJurisdictionID = dto.StormwaterJurisdictionID,
            OnlandVisualTrashAssessmentStatusID = (int)OnlandVisualTrashAssessmentStatusEnum.InProgress
        };

        await dbContext.AddAsync(onlandVisualTrashAssessment);
        await dbContext.SaveChangesAsync();
        await dbContext.Entry(onlandVisualTrashAssessment).ReloadAsync();
        
        return onlandVisualTrashAssessment.AsSimpleDto();
    }

    //public static List<PreliminarySourceIdentificationCategoryTypeDto> GetPreliminarySourceIdentificationCategoryTypeDtos(NeptuneDbContext dbContext)
    //{
    //    var temp = PreliminarySourceIdentificationCategory.All;
    //    var temp2 = PreliminarySourceIdentificationType.All.GroupBy(x => x.PreliminarySourceIdentificationCategoryID);
    //    var temp3 = new List<PreliminarySourceIdentificationCategoryTypeDto>();
    //    //foreach (var t in temp2)
    //    //{
    //    //    temp3.Add(new PreliminarySourceIdentificationCategoryTypeDto()
    //    //    {
    //    //        PreliminarySourceIdentificationCategoryID = t.Key,
    //    //        PreliminarySourceIdentificationCategoryName = t.Single(x => x.PreliminarySourceIdentificationCategory.PreliminarySourceIdentificationCategoryName),
    //    //        PreliminarySourceIdentificationCategoryDisplayName = t.Single(x => x.PreliminarySourceIdentificationCategory.PreliminarySourceIdentificationCategoryDisplayName),
    //    //    });
    //    //}

    //    return temp3;
    //}

}