using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;
using Neptune.Common.GeoSpatial;
using Neptune.Models.DataTransferObjects;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

public static class OnlandVisualTrashAssessmentAreas
{
    private static IQueryable<OnlandVisualTrashAssessmentArea> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.OnlandVisualTrashAssessmentAreas
                .Include(x => x.StormwaterJurisdiction)
                .ThenInclude(x => x.Organization)
            ;
    }

    public static OnlandVisualTrashAssessmentArea GetByIDWithChangeTracking(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentAreaID)
    {
        var onlandVisualTrashAssessmentArea = GetImpl(dbContext)
            .SingleOrDefault(x => x.OnlandVisualTrashAssessmentAreaID == onlandVisualTrashAssessmentAreaID);
        Check.RequireNotNull(onlandVisualTrashAssessmentArea, $"OnlandVisualTrashAssessmentArea with ID {onlandVisualTrashAssessmentAreaID} not found!");
        return onlandVisualTrashAssessmentArea;
    }

    public static OnlandVisualTrashAssessmentArea GetByIDWithChangeTracking(NeptuneDbContext dbContext, OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, onlandVisualTrashAssessmentAreaPrimaryKey.PrimaryKeyValue);
    }

    public static OnlandVisualTrashAssessmentArea GetByID(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentAreaID)
    {
        var onlandVisualTrashAssessmentArea = GetImpl(dbContext)
            .AsNoTracking()
            .SingleOrDefault(x => x.OnlandVisualTrashAssessmentAreaID == onlandVisualTrashAssessmentAreaID);
        Check.RequireNotNull(onlandVisualTrashAssessmentArea, $"OnlandVisualTrashAssessmentArea with ID {onlandVisualTrashAssessmentAreaID} not found!");
        return onlandVisualTrashAssessmentArea;
    }

    public static OnlandVisualTrashAssessmentArea GetByID(NeptuneDbContext dbContext, OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
    {
        return GetByID(dbContext, onlandVisualTrashAssessmentAreaPrimaryKey.PrimaryKeyValue);
    }

    public static List<OnlandVisualTrashAssessmentArea> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).AsNoTracking().ToList().OrderBy(x => x.OnlandVisualTrashAssessmentAreaName).ToList();

    }

    public static async Task Update(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentAreaID, OnlandVisualTrashAssessmentAreaSimpleDto ovtaAreaDto)
    {
        var onlandVisualTrashAssessmentArea = await dbContext.OnlandVisualTrashAssessmentAreas.SingleOrDefaultAsync(x =>
            x.OnlandVisualTrashAssessmentAreaID == onlandVisualTrashAssessmentAreaID);
        if (onlandVisualTrashAssessmentArea != null)
        {
            onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName = ovtaAreaDto.OnlandVisualTrashAssessmentAreaName;
            onlandVisualTrashAssessmentArea.AssessmentAreaDescription = ovtaAreaDto.AssessmentAreaDescription;
        }
        
        await dbContext.SaveChangesAsync();
    }

    public static OnlandVisualTrashAssessmentArea GetByIDForFeatureContextCheck(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentAreaID)
    {
        var onlandVisualTrashAssessmentArea = dbContext.OnlandVisualTrashAssessmentAreas
            .Include(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.Organization).AsNoTracking()
            .SingleOrDefault(x => x.OnlandVisualTrashAssessmentAreaID == onlandVisualTrashAssessmentAreaID);
        Check.RequireNotNull(onlandVisualTrashAssessmentArea, $"OnlandVisualTrashAssessmentArea with ID {onlandVisualTrashAssessmentAreaID} not found!");
        return onlandVisualTrashAssessmentArea;
    }

    public static List<OnlandVisualTrashAssessmentArea> ListByStormwaterJurisdictionIDList(NeptuneDbContext dbContext, IEnumerable<int> stormwaterJurisdictionIDList)
    {
        return GetImpl(dbContext).Include(x => x.OnlandVisualTrashAssessments).AsNoTracking().Where(x => stormwaterJurisdictionIDList.Contains(x.StormwaterJurisdictionID)).OrderBy(x => x.OnlandVisualTrashAssessmentAreaName).ToList();
    }

    public static void UpdateGeometry(NeptuneDbContext dbContext, OnlandVisualTrashAssessmentAreaGeometryDto onlandVisualTrashAssessmentAreaGeometryDto)
    {
        var onlandVisualTrashAssessmentArea = dbContext.OnlandVisualTrashAssessmentAreas.Single(x =>
            x.OnlandVisualTrashAssessmentAreaID == onlandVisualTrashAssessmentAreaGeometryDto.OnlandVisualTrashAssessmentAreaID);
        if (onlandVisualTrashAssessmentAreaGeometryDto.UsingParcels)
        {
            // since this is parcel picks, we don't need to reproject; the parcels are already in the correct system (State Plane)
            onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaGeometry = ParcelGeometries.UnionAggregateByParcelIDs(dbContext, onlandVisualTrashAssessmentAreaGeometryDto.ParcelIDs);
            onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaGeometry4326 = ParcelGeometries.UnionAggregate4326ByParcelIDs(dbContext, onlandVisualTrashAssessmentAreaGeometryDto.ParcelIDs);
        }
        else
        {
            var newGeometry4326 = GeoJsonSerializer.Deserialize<IFeature>(onlandVisualTrashAssessmentAreaGeometryDto.GeometryAsGeoJson);
            newGeometry4326.Geometry.SRID = Proj4NetHelper.WEB_MERCATOR;

            // since this is coming from the browser, we have to transform to State Plane
            onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaGeometry4326 = newGeometry4326.Geometry;
            onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaGeometry = newGeometry4326.Geometry.ProjectTo2771();
        }
    }

    public static OnlandVisualTrashAssessmentScore? CalculateBaselineScoreFromBackingData(
        List<Entities.OnlandVisualTrashAssessment> onlandVisualTrashAssessments)
    {
        var completedAndIsBaselineAssessment = onlandVisualTrashAssessments.Where(x => x.OnlandVisualTrashAssessmentStatusID == (int)
            OnlandVisualTrashAssessmentStatusEnum.Complete && !x.IsProgressAssessment).ToList();

        if (completedAndIsBaselineAssessment.Count < 2)
        {
            return null;
        }

        var average = completedAndIsBaselineAssessment.Average(x => x.OnlandVisualTrashAssessmentScore.NumericValue);
        var round = (int)Math.Round(average);
        return OnlandVisualTrashAssessmentScore.All.SingleOrDefault(x => x.NumericValue == round);
    }

    public static FeatureCollection GetAssessmentAreaByIDAsFeatureCollection(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentAreaID)
    {
        var onlandVisualTrashAssessmentArea = dbContext.OnlandVisualTrashAssessmentAreas.AsNoTracking()
            .Single(x => x.OnlandVisualTrashAssessmentAreaID == onlandVisualTrashAssessmentAreaID);
        var attributesTable = new AttributesTable
        {
            {
                "OnlandVisualTrashAssessmentAreaID", onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID
            }
        };
        var featureCollection = new FeatureCollection();
        if (onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaGeometry4326 != null)
        {
            var feature = new Feature(onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaGeometry4326, attributesTable);
            featureCollection.Add(feature);
        }
 
        return featureCollection;
    }

    public static FeatureCollection GetTransectLineByIDAsFeatureCollection(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentAreaID)
    {
        var onlandVisualTrashAssessmentArea = dbContext.OnlandVisualTrashAssessmentAreas.AsNoTracking()
            .Single(x => x.OnlandVisualTrashAssessmentAreaID == onlandVisualTrashAssessmentAreaID);
        var attributesTable = new AttributesTable
        {
            {
                "OnlandVisualTrashAssessmentAreaID", onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID
            }
        };
        var featureCollection = new FeatureCollection();
        if (onlandVisualTrashAssessmentArea.TransectLine4326 != null)
        {
            var feature = new Feature(onlandVisualTrashAssessmentArea.TransectLine4326, attributesTable);
            featureCollection.Add(feature);
        }
 
        return featureCollection;
    }

    public static Geometry RecomputeTransectLine(List<OnlandVisualTrashAssessment> onlandVisualTrashAssessments)
    {
        var completedOVTAs = onlandVisualTrashAssessments
            .Where(x => x.OnlandVisualTrashAssessmentStatusID == (int)OnlandVisualTrashAssessmentStatusEnum.Complete).ToList();

        // new transect should come from the earliest completed assessment
        if (completedOVTAs.Any())
        {
            var onlandVisualTrashAssessment = completedOVTAs.MinBy(x => x.CompletedDate);
            onlandVisualTrashAssessment.IsTransectBackingAssessment = true;

            return OnlandVisualTrashAssessments.GetTransectLine(onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations);
        }
        return null;
    }

}