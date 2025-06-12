using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

public class RegionalSubbasinNetworkResult
{
    public int BaseRegionalSubbasinID { get; set; }
    public int CurrentNodeRegionalSubbasinID { get; set; }
    public int OCSurveyCatchmentID { get; set; }
    public int? OCSurveyDownstreamCatchmentID { get; set; }
    public int Depth { get; set; }
    [Column(TypeName = "geometry")]
    public Geometry CatchmentGeometry4326 { get; set; }
    [Column(TypeName = "geometry")]
    public Geometry DownstreamLineGeometry { get; set; }
}