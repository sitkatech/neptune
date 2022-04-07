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
                .Include(x => x.DelineationType)
                .AsNoTracking();
        }

        public static List<DelineationUpsertDto> ListByProjectIDAsUpsertDto(HippocampDbContext dbContext, int projectID)
        {
            return GetDelineationsImpl(dbContext)
                .Where(x => x.TreatmentBMP.ProjectID == projectID)
                .Select(x => x.AsUpsertDto())
                .ToList();
        }

        public static List<DelineationSimpleDto> ListAsSimpleDto(HippocampDbContext dbContext)
        {
            var treatmentBMPDisplayDtos = GetDelineationsImpl(dbContext)
                .Select(x => x.AsSimpleDto())
                .ToList();

            return treatmentBMPDisplayDtos;
        }

        public static List<DelineationSimpleDto> ListByPersonIDAsSimpleDto(HippocampDbContext dbContext, int personID)
        {
            var person = People.GetByID(dbContext, personID);
            if (person.RoleID == (int)RoleEnum.Admin || person.RoleID == (int)RoleEnum.SitkaAdmin)
            {
                return ListAsSimpleDto(dbContext);
            }

            var jurisdictionIDs = People.ListStormwaterJurisdictionIDsByPersonID(dbContext, personID);

            var treatmentBMPDisplayDtos = GetDelineationsImpl(dbContext)
                .Where(x => jurisdictionIDs.Contains(x.TreatmentBMP.StormwaterJurisdictionID))
                .Select(x => x.AsSimpleDto())
                .ToList();

            return treatmentBMPDisplayDtos;
        }

        public static void MergeDelineations(HippocampDbContext dbContext, List<DelineationUpsertDto> delineationUpsertDtos, Project project)
        {
            var existingProjectDelineations = dbContext.Delineations.Include(x => x.TreatmentBMP).Where(x => x.TreatmentBMP.ProjectID == project.ProjectID).ToList();
            
            var allDelineationsInDatabase = dbContext.Delineations;
            
            // merge new Delineations
            var newDelineations = delineationUpsertDtos
                .Select(x => DelineationFromUpsertDto(x)).ToList();
            
            existingProjectDelineations.MergeNew(newDelineations, allDelineationsInDatabase,
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID);

            dbContext.SaveChanges();

            // update upsert dtos with new DelineationIDs
            foreach (var delineationUpsertDto in delineationUpsertDtos)
            {
                delineationUpsertDto.DelineationID = existingProjectDelineations
                    .Single(x => x.TreatmentBMPID == delineationUpsertDto.TreatmentBMPID).DelineationID;
            }

            existingProjectDelineations.MergeUpdate(newDelineations,
                (x, y) => x.DelineationID == y.DelineationID,
                (x, y) =>
                {
                    x.DelineationTypeID = y.DelineationTypeID;
                    x.DelineationGeometry = y.DelineationGeometry;
                    x.DelineationGeometry4326 = y.DelineationGeometry4326;
                    x.DateLastModified = y.DateLastModified;
                });

            var delineationsToBeDeletedIDs = existingProjectDelineations.Where(x => newDelineations.Any(y => y.DelineationID == x.DelineationID)).Select(x => x.DelineationID).ToList();
            // delete all the Delineation related entities
            dbContext.ProjectHRUCharacteristics.RemoveRange(dbContext.ProjectHRUCharacteristics.Include(x => x.ProjectLoadGeneratingUnit).Where(x => x.ProjectLoadGeneratingUnit.DelineationID.HasValue && delineationsToBeDeletedIDs.Contains(x.ProjectLoadGeneratingUnit.DelineationID.Value)).ToList());
            dbContext.ProjectLoadGeneratingUnits.RemoveRange(dbContext.ProjectLoadGeneratingUnits.Where(x => x.DelineationID.HasValue && delineationsToBeDeletedIDs.Contains(x.DelineationID.Value)).ToList());
            dbContext.DelineationOverlaps.RemoveRange(dbContext.DelineationOverlaps.Where(x => delineationsToBeDeletedIDs.Contains(x.DelineationID) || delineationsToBeDeletedIDs.Contains(x.DelineationOverlapID)).ToList());

            existingProjectDelineations.MergeDelete(newDelineations,
                (x, y) => x.DelineationID == y.DelineationID,
                allDelineationsInDatabase);


            dbContext.SaveChanges();
        }

        public static Delineation DelineationFromUpsertDto(DelineationUpsertDto delineationUpsertDto)
        {
            var delineationGeometry = !string.IsNullOrWhiteSpace(delineationUpsertDto.Geometry) ? GeoJsonHelpers.GetFeatureFromGeoJson(delineationUpsertDto.Geometry) : null;
            var delineation = new Delineation()
            {
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
        
        public static DelineationUpsertDto GetByDelineationID(HippocampDbContext dbContext, int delineationID)
        {
            return GetDelineationsImpl(dbContext).SingleOrDefault(x => x.DelineationID == delineationID).AsUpsertDto();
        }
    }
}