﻿using System.Text.Json.Serialization;
using Neptune.Web.Common;
using NetTopologySuite.Geometries;

public class LoadGeneratingUnitResult : IHasGeometry
{
    [JsonPropertyName("ModelID")]
    public int ModelBasinID { get; set; }
    [JsonPropertyName("DelinID")]
    public int? DelineationID { get; set; }
    [JsonPropertyName("WQMPID")]
    public int? WaterQualityManagementPlanID { get; set; }
    [JsonPropertyName("RSBID")]
    public int? RegionalSubbasinID { get; set; }
    public Geometry Geometry { get; set; }
}