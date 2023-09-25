using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Neptune.EFModels.Entities
{
    public partial class TreatmentBMP
    {
        public double Longitude => LocationPoint4326.Coordinate.X;
        public double Latitude => LocationPoint4326.Coordinate.Y;
        public RegionalSubbasin GetRegionalSubbasin(NeptuneDbContext dbContext)
        {
            return dbContext.RegionalSubbasins.SingleOrDefault(x =>
                    x.CatchmentGeometry.Contains(LocationPoint));
        }
        public IEnumerable<ProjectHRUCharacteristic> GetHRUCharacteristics(NeptuneDbContext dbContext)
        {
            if (Delineation == null)
            {
                return new List<ProjectHRUCharacteristic>();
            }

            if (Delineation.DelineationTypeID == (int)DelineationType.DelineationTypeEnum.Centralized && TreatmentBMPType.TreatmentBMPModelingTypeID != null)
            {
                var catchmentRegionalSubbasins = GetRegionalSubbasin(dbContext).TraceUpstreamCatchmentsReturnIDList(dbContext);

                catchmentRegionalSubbasins.Add(RegionalSubbasinID.GetValueOrDefault());

                return dbContext.ProjectHRUCharacteristics
                    .Include(x => x.ProjectLoadGeneratingUnit)
                    .Include(x => x.HRUCharacteristicLandUseCode)
                    .Where(x =>
                    x.ProjectID == ProjectID &&
                    x.ProjectLoadGeneratingUnit.RegionalSubbasinID != null &&
                    catchmentRegionalSubbasins.Contains(x.ProjectLoadGeneratingUnit.RegionalSubbasinID.Value));
            }

            else
            {
                return dbContext.ProjectHRUCharacteristics
                    .Include(x => x.ProjectLoadGeneratingUnit)
                    .ThenInclude(x => x.Delineation)
                    .Include(x => x.HRUCharacteristicLandUseCode)
                    .Where(x =>
                    x.ProjectID == ProjectID &&
                    x.ProjectLoadGeneratingUnit.Delineation != null && 
                    x.ProjectLoadGeneratingUnit.Delineation.TreatmentBMPID == TreatmentBMPID);
            }
        }
    }
}