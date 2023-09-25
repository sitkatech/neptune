/*-----------------------------------------------------------------------
<copyright file="FieldVisit.DatabaseContextExtensions.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/
using Neptune.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Neptune.Common;
using Neptune.Common.DesignByContract;
using Neptune.Common.GeoSpatial;
using Neptune.Web.Common;
using NetTopologySuite.Features;

namespace Neptune.EFModels.Entities
{
    public static class Delineations
    {
        public static IQueryable<Delineation> GetImpl(NeptuneDbContext dbContext)
        {
            return dbContext.Delineations
                    .Include(x => x.TreatmentBMP)
                    .Include(x => x.DelineationType);
        }

        public static Delineation GetByIDWithChangeTracking(NeptuneDbContext dbContext,
            int delineationID)
        {
            var delineation = GetImpl(dbContext)
                .SingleOrDefault(x => x.DelineationID == delineationID);
            Check.RequireNotNull(delineation,
                $"Delineation with ID {delineationID} not found!");
            return delineation;
        }

        public static Delineation GetByIDWithChangeTracking(NeptuneDbContext dbContext,
            DelineationPrimaryKey delineationPrimaryKey)
        {
            return GetByIDWithChangeTracking(dbContext, delineationPrimaryKey.PrimaryKeyValue);
        }

        public static List<DelineationUpsertDto> ListByProjectIDAsUpsertDto(NeptuneDbContext dbContext, int projectID)
        {
            return GetImpl(dbContext)
                .Where(x => x.TreatmentBMP.ProjectID == projectID)
                .Select(x => x.AsUpsertDto())
                .ToList();
        }

        public static List<DelineationSimpleDto> ListProjectDelineationsAsSimpleDto(NeptuneDbContext dbContext)
        {
            var treatmentBMPDisplayDtos = GetImpl(dbContext).Where(x => x.TreatmentBMP.ProjectID != null)
                .Select(x => x.AsSimpleDto())
                .ToList();

            return treatmentBMPDisplayDtos;
        }

        public static Delineation GetByID(NeptuneDbContext dbContext, int delineationID)
        {
            var delineation = GetImpl(dbContext).AsNoTracking()
                .SingleOrDefault(x => x.DelineationID == delineationID);
            Check.RequireNotNull(delineation,
                $"Delineation with ID {delineationID} not found!");
            return delineation;
        }

        public static Delineation GetByID(NeptuneDbContext dbContext,
            DelineationPrimaryKey delineationPrimaryKey)
        {
            return GetByID(dbContext, delineationPrimaryKey.PrimaryKeyValue);
        }

        public static Delineation? GetByTreatmentBMPID(NeptuneDbContext dbContext, int treatmentBMPID)
        {
            var delineation = GetImpl(dbContext).AsNoTracking()
                .SingleOrDefault(x => x.TreatmentBMPID == treatmentBMPID);
            return delineation;
        }

        public static Delineation? GetByTreatmentBMPIDWithChangeTracking(NeptuneDbContext dbContext, int treatmentBMPID)
        {
            var delineation = GetImpl(dbContext)
                .SingleOrDefault(x => x.TreatmentBMPID == treatmentBMPID);
            return delineation;
        }

        public static List<Delineation> ListByTreatmentBMPIDList(NeptuneDbContext dbContext, IEnumerable<int> treatmentBMPIDList)
        {
            return GetImpl(dbContext).AsNoTracking()
                .Where(x => treatmentBMPIDList.Contains(x.TreatmentBMPID)).OrderBy(x => x.DelineationID).ToList();
        }


        public static List<Delineation> GetProvisionalBMPDelineations(NeptuneDbContext dbContext, Person currentPerson)
        {
            return GetImpl(dbContext).AsNoTracking()
                .Include(x => x.TreatmentBMP)
                .ThenInclude(x => x.TreatmentBMPType)
                .Include(x => x.TreatmentBMP)
                .ThenInclude(x => x.StormwaterJurisdiction)
                .ThenInclude(x => x.Organization)
                .Where(x => x.IsVerified == false).ToList()
                .Where(x => x.TreatmentBMP.CanView(currentPerson))
                .OrderBy(x => x.TreatmentBMP.TreatmentBMPName).ToList();
        }

        public static List<DelineationSimpleDto> ListByPersonIDAsSimpleDto(NeptuneDbContext dbContext, int personID)
        {
            var person = People.GetByID(dbContext, personID);
            if (person.RoleID == (int)RoleEnum.Admin || person.RoleID == (int)RoleEnum.SitkaAdmin)
            {
                return ListProjectDelineationsAsSimpleDto(dbContext);
            }

            var jurisdictionIDs = People.ListStormwaterJurisdictionIDsByPersonID(dbContext, personID);

            var treatmentBMPDisplayDtos = GetImpl(dbContext)
                .Where(x => jurisdictionIDs.Contains(x.TreatmentBMP.StormwaterJurisdictionID))
                .Select(x => x.AsSimpleDto())
                .ToList();

            return treatmentBMPDisplayDtos;
        }

        public static void MergeDelineations(NeptuneDbContext dbContext, List<DelineationUpsertDto> delineationUpsertDtos, Project project)
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
            var delineationGeometry = !string.IsNullOrWhiteSpace(delineationUpsertDto.Geometry) ? GeoJsonSerializer.Deserialize<Feature>(delineationUpsertDto.Geometry) : null;
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


        public static void MarkAsVerified(Delineation delineation, Person currentPerson)
        {
            delineation.IsVerified = true;
            delineation.DateLastVerified = DateTime.Now;
            delineation.VerifiedByPersonID = currentPerson.PersonID;
        }
    }
}