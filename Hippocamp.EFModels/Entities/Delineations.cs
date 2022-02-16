using Hippocamp.API.Util;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hippocamp.EFModels.Entities
{
    public partial class Delineations
    {
        private static IQueryable<Delineation> GetDelineationsImpl(HippocampDbContext dbContext)
        {
            return dbContext.Delineations
                .Include(x => x.TreatmentBMP)
                .AsNoTracking();
        }

        public static List<DelineationUpsertDto> ListByProjectIDAsUpsertDto(HippocampDbContext dbContext, int projectID)
        {
            return GetDelineationsImpl(dbContext)
                .Where(x => x.TreatmentBMP.ProjectID == projectID)
                .Select(x => x.AsUpsertDto())
                .ToList();
        }

        public static void MergeDelineations(HippocampDbContext dbContext, List<DelineationUpsertDto> delineationUpsertDtos, Project project)
        {
            var existingProjectDelineations = dbContext.Delineations.Include(x => x.TreatmentBMP).Where(x => x.TreatmentBMP.ProjectID == project.ProjectID).ToList();
            
            var allDelineationsInDatabase = dbContext.Delineations;
            
            // merge new Delineations
            var newDelineations = delineationUpsertDtos
                .Select(x => DelineationFromUpsertDto(x)).ToList();

            existingProjectDelineations.Merge(newDelineations, allDelineationsInDatabase,
                (x, y) => x.DelineationID == y.DelineationID,
                (x, y) =>
                {
                    x.DelineationTypeID = y.DelineationTypeID;
                    x.DelineationGeometry = y.DelineationGeometry;
                    x.DelineationGeometry4326 = y.DelineationGeometry4326;
                    x.DateLastModified = y.DateLastModified;
                });

            dbContext.SaveChanges();
        }

        public static Delineation DelineationFromUpsertDto(DelineationUpsertDto delineationUpsertDto)
        {
            var delineationGeometry = !string.IsNullOrWhiteSpace(delineationUpsertDto.Geometry) ? GeoJsonHelpers.GetFeatureFromGeoJson(delineationUpsertDto.Geometry) : null;
            var delineation = new Delineation()
            {
                //DelineationOverlapOverlappingDelineations
                //DelineationOverlapDelineations
                DelineationTypeID = delineationUpsertDto.DelineationTypeID,
                DelineationGeometry4326 = delineationGeometry != null ? delineationGeometry.Geometry : null,
                DelineationGeometry = delineationGeometry != null ? delineationGeometry.Geometry.ProjectTo2771() : null,
                DateLastModified = DateTime.Now,
                TreatmentBMPID = delineationUpsertDto.TreatmentBMPID
            };

            if (delineationUpsertDto.DelineationID > 0)
            {
                delineation.DelineationID = delineationUpsertDto.DelineationID;
            }

            return delineation;
        }
    }
}