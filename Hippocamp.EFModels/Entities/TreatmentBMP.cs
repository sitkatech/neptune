using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Hippocamp.EFModels.Entities
{
    public partial class TreatmentBMP
    {
        public double Longitude => LocationPoint4326.Coordinate.X;
        public double Latitude => LocationPoint4326.Coordinate.Y;
        public RegionalSubbasin GetRegionalSubbasin(HippocampDbContext dbContext)
        {
            return dbContext.RegionalSubbasins.SingleOrDefault(x =>
                    x.CatchmentGeometry.Contains(LocationPoint));
        }
        public IEnumerable<ProjectHRUCharacteristic> GetHRUCharacteristics(HippocampDbContext dbContext)
        {
            if (Delineation == null)
            {
                return new List<ProjectHRUCharacteristic>();
            }

            if (Delineation.DelineationTypeID == (int)DelineationType.DelineationTypeEnum.Centralized && TreatmentBMPType.TreatmentBMPModelingType != null)
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
                    .Include(x => x.HRUCharacteristicLandUseCode)
                    .Include(x => x.ProjectLoadGeneratingUnit)
                    .ThenInclude(x => x.Delineation)
                    .Where(x =>
                    x.ProjectID == ProjectID &&
                    x.ProjectLoadGeneratingUnit.Delineation != null && 
                    x.ProjectLoadGeneratingUnit.Delineation.TreatmentBMPID == TreatmentBMPID);
            }
        }
    }
}