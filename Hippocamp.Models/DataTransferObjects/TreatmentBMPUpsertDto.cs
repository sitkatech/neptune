using System;
using NetTopologySuite.Geometries;

namespace Hippocamp.Models.DataTransferObjects
{
    public class TreatmentBMPUpsertDto
    {
        public int TreatmentBMPID { get; set; }
        public string TreatmentBMPName { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public string? TreatmentBMPTypeName { get; set; }
        public string? StormwaterJurisdictionName { get; set; }
        public string? WatershedName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string? Notes { get; set; }
    }
}