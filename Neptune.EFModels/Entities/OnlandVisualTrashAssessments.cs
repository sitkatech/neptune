using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;
using Neptune.Common.GeoSpatial;
using Neptune.Models.DataTransferObjects;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

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
            .Include(x => x.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes)
            .Include(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.StormwaterJurisdictionGeometry)
            .SingleOrDefault(x => x.OnlandVisualTrashAssessmentID == onlandVisualTrashAssessmentID);
        Check.RequireNotNull(onlandVisualTrashAssessment, $"OnlandVisualTrashAssessment with ID {onlandVisualTrashAssessmentID} not found!");
        return onlandVisualTrashAssessment;
    }

    public static OnlandVisualTrashAssessmentArea GetOnlandVisualTrashAssessmentAreaByID(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentID)
    {
        var onlandVisualTrashAssessment = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.OnlandVisualTrashAssessmentID == onlandVisualTrashAssessmentID);
        Check.RequireNotNull(onlandVisualTrashAssessment, $"OnlandVisualTrashAssessment with ID {onlandVisualTrashAssessmentID} not found!");
        return OnlandVisualTrashAssessmentAreas.GetByID(dbContext,
            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID);
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

        if (onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID.HasValue)
        {
            var transectBackingAssessment = GetTransectBackingAssessment(dbContext,
                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID);

            // ensure the area to which this assessment is assigned ends up with only one transect-backing assessment
            if (transectBackingAssessment != null)
            {
                if (transectBackingAssessment.OnlandVisualTrashAssessmentID != onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID)
                {
                    onlandVisualTrashAssessment.IsTransectBackingAssessment = false;
                }
            }
            else
            {
                onlandVisualTrashAssessment.IsTransectBackingAssessment = true;
            }
        }
        else
        {
            onlandVisualTrashAssessment.IsTransectBackingAssessment = false;
        }

        await dbContext.AddAsync(onlandVisualTrashAssessment);
        await dbContext.SaveChangesAsync();
        await dbContext.Entry(onlandVisualTrashAssessment).ReloadAsync();
        
        return onlandVisualTrashAssessment.AsSimpleDto();
    }

    public static async Task Update(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentID,
        OnlandVisualTrashAssessmentReviewAndFinalizeDto dto)
    {

        var onlandVisualTrashAssessment = dbContext.OnlandVisualTrashAssessments
            .Include(x => x.OnlandVisualTrashAssessmentArea)
            .Include(onlandVisualTrashAssessment => onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations)
            .Single(x =>
            x.OnlandVisualTrashAssessmentID == onlandVisualTrashAssessmentID);

        if (dto.Finalize)
        {
            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentScoreID = dto.OnlandVisualTrashAssessmentScoreID;
            onlandVisualTrashAssessment.Notes = dto.Notes;
            onlandVisualTrashAssessment.CompletedDate = dto.AssessmentDate;
            onlandVisualTrashAssessment.IsProgressAssessment = dto.IsProgressAssessment ?? false;

            // create the assessment area
            if (onlandVisualTrashAssessment.AssessingNewArea.GetValueOrDefault())
            {
                var onlandVisualTrashAssessmentAreaGeometry2771 = onlandVisualTrashAssessment.DraftGeometry;

                var onlandVisualTrashAssessmentArea = new OnlandVisualTrashAssessmentArea
                {
                    OnlandVisualTrashAssessmentAreaName = dto.OnlandVisualTrashAssessmentAreaName,
                    StormwaterJurisdictionID = onlandVisualTrashAssessment.StormwaterJurisdictionID,
                    OnlandVisualTrashAssessmentAreaGeometry = onlandVisualTrashAssessmentAreaGeometry2771,
                    OnlandVisualTrashAssessmentAreaGeometry4326 = onlandVisualTrashAssessmentAreaGeometry2771.ProjectTo4326()
                };
                await dbContext.OnlandVisualTrashAssessmentAreas.AddAsync(onlandVisualTrashAssessmentArea);
                await dbContext.SaveChangesAsync();
                await dbContext.Entry(onlandVisualTrashAssessmentArea).ReloadAsync();

                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID;
                onlandVisualTrashAssessment.DraftGeometry = null;
                onlandVisualTrashAssessment.DraftAreaDescription = null;
                onlandVisualTrashAssessment.DraftAreaName = null;
            }

            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentStatusID = (int) OnlandVisualTrashAssessmentStatusEnum.Complete;

            await dbContext.SaveChangesAsync();

            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.AssessmentAreaDescription = dto.AssessmentAreaDescription;

            var onlandVisualTrashAssessments = OnlandVisualTrashAssessments.ListByOnlandVisualTrashAssessmentAreaID(dbContext, onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID.Value);
            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID =
                OnlandVisualTrashAssessmentAreas.CalculateScoreFromBackingData(onlandVisualTrashAssessments, false)?
                    .OnlandVisualTrashAssessmentScoreID;

            if (dto.IsProgressAssessment ?? false)
            {
                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea
                        .OnlandVisualTrashAssessmentProgressScoreID =
                    onlandVisualTrashAssessment.OnlandVisualTrashAssessmentScoreID;
            }

            if (onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.TransectLine == null && onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations.Count >= 2)
            {
                var transect = GetTransect(onlandVisualTrashAssessment);
                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.TransectLine = transect;
                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.TransectLine4326 = transect.ProjectTo4326();
                onlandVisualTrashAssessment.IsTransectBackingAssessment = true;

                var transectBackingAssessment =
                    GetTransectBackingAssessment(dbContext, onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID);
                if (transectBackingAssessment != null)
                {
                    transectBackingAssessment.IsTransectBackingAssessment = false;
                }
            }
        }
        else
        {
            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentScoreID = dto.OnlandVisualTrashAssessmentScoreID;
            onlandVisualTrashAssessment.Notes = dto.Notes;
            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentStatusID = (int)OnlandVisualTrashAssessmentStatusEnum.InProgress;
            if (onlandVisualTrashAssessment.AssessingNewArea ?? false)
            {
                onlandVisualTrashAssessment.DraftAreaName = dto.OnlandVisualTrashAssessmentAreaName;
                onlandVisualTrashAssessment.DraftAreaDescription = dto.AssessmentAreaDescription;
            }
            await dbContext.SaveChangesAsync();
        }

        //await dbContext.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.Where(x =>
        //        x.OnlandVisualTrashAssessmentID == onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID)
        //    .ExecuteDeleteAsync();

        //var onlandVisualTrashAssessmentPreliminarySourceIdentificationTypesToUpdate =
        //    dto.PreliminarySourceIdentifications
        //        //.Where(x => x.Has)
        //        .Select(x =>
        //            new OnlandVisualTrashAssessmentPreliminarySourceIdentificationType
        //            {
        //                OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentID,
        //                PreliminarySourceIdentificationTypeID =
        //                    x.PreliminarySourceIdentificationTypeID,
        //                ExplanationIfTypeIsOther = x.ExplanationIfTypeIsOther
        //            }).ToList();

        //await dbContext.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.AddRangeAsync(onlandVisualTrashAssessmentPreliminarySourceIdentificationTypesToUpdate);
        //await dbContext.SaveChangesAsync();
    }

    public static List<PreliminarySourceIdentificationTypeSimpleDto> GetPreliminarySourceIdentificationTypeSimpleDtos(NeptuneDbContext dbContext)
    {
        var preliminarySourceIdentificationTypeSimpleDtos = PreliminarySourceIdentificationType.AllAsSimpleDto;

        return preliminarySourceIdentificationTypeSimpleDtos;
    }

    public static async Task RefreshParcels(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentID)
    {
        var onlandVisualTrashAssessment = dbContext.OnlandVisualTrashAssessments.Single(x =>
            x.OnlandVisualTrashAssessmentID == onlandVisualTrashAssessmentID);
        onlandVisualTrashAssessment.DraftGeometry = null;
        onlandVisualTrashAssessment.IsDraftGeometryManuallyRefined = false;

        await dbContext.SaveChangesAsync();
    }

    public static async Task UpdateGeometry(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentID, List<int> parcelIDs)
    {
        var onlandVisualTrashAssessment = dbContext.OnlandVisualTrashAssessments.Single(x =>
            x.OnlandVisualTrashAssessmentID == onlandVisualTrashAssessmentID);
        onlandVisualTrashAssessment.DraftGeometry = ParcelGeometries.UnionAggregate4326ByParcelIDs(dbContext, parcelIDs);

        await dbContext.SaveChangesAsync();
    }

    public static async Task UpdateGeometry(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentID, OnlandVisualTrashAssessmentRefineAreaDto onlandVisualTrashAssessmentRefineAreaDto)
    {
        var onlandVisualTrashAssessment = dbContext.OnlandVisualTrashAssessments.Single(x =>
            x.OnlandVisualTrashAssessmentID == onlandVisualTrashAssessmentRefineAreaDto.OnlandVisualTrashAssessmentID);

        var newGeometry4326 = GeoJsonSerializer.Deserialize<IFeature>(onlandVisualTrashAssessmentRefineAreaDto.GeometryAsGeoJson);
        newGeometry4326.Geometry.SRID = Proj4NetHelper.WEB_MERCATOR;

        // since this is coming from the browser, we have to transform to State Plane
        onlandVisualTrashAssessment.IsDraftGeometryManuallyRefined = !newGeometry4326.Equals(onlandVisualTrashAssessment.DraftGeometry);
        onlandVisualTrashAssessment.DraftGeometry = newGeometry4326.Geometry;
        await dbContext.SaveChangesAsync();
    }

    public static string GetTransectLine4326GeoJson(OnlandVisualTrashAssessment ovta)
    {
        var attributesTable = new AttributesTable
        {
            { "OnlandVisualTrashAssessmentID", ovta.OnlandVisualTrashAssessmentID },
        };

        var feature = new Feature(GetTransect(ovta).ProjectTo4326(), attributesTable);
        return GeoJsonSerializer.Serialize(feature);
    }

    private static Geometry GetTransect(OnlandVisualTrashAssessment ovta)
    {
        if (ovta.OnlandVisualTrashAssessmentObservations.Count > 1)
        {
            var points = string.Join(",",
                ovta.OnlandVisualTrashAssessmentObservations.OrderBy(x => x.ObservationDatetime)
                    .Select(x => x.LocationPoint).ToList().Select(x => $"{x.Coordinate.X} {x.Coordinate.Y}")
                    .ToList());

            var linestring = $"LINESTRING ({points})";

            // the transect is going to be in 2771 because it was generated from points in 2771
            return GeometryHelper.FromWKT(linestring, Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID);
        }

        return null;
    }

}