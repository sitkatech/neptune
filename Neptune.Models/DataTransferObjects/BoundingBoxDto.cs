using NetTopologySuite.Geometries;

namespace Neptune.Models.DataTransferObjects
{
    public class BoundingBoxDto
    {
        public double Left { get; set; }
        public double Bottom { get; set; }
        public double Right { get; set; }
        public double Top { get; set; }

        public BoundingBoxDto()
        {
            Left = -117.5193786621095;
            Top = 33.844679670212059;
            Right = -118.09341430664051;
            Bottom = 33.46459577300336;
        }

        public BoundingBoxDto(IReadOnlyCollection<Point> pointList)
        {
            if (pointList.Any())
            {
                Left = pointList.Max(x => x.X);
                Top = pointList.Max(x => x.Y);
                Right = pointList.Min(x => x.X);
                Bottom = pointList.Min(x => x.Y);
            }
            else
            {
                Left = -117.5193786621095;
                Top = 33.844679670212059;
                Right = -118.09341430664051;
                Bottom = 33.46459577300336;
            }
        }


        public BoundingBoxDto(IEnumerable<Geometry> geometries) : this(geometries.SelectMany(GetPointsFromDbGeometry).ToList())
        {
        }

        public BoundingBoxDto(Geometry? geometry) : this(GetPointsFromDbGeometry(geometry))
        {
        }

        public static List<Point> GetPointsFromDbGeometry(Geometry? geometry)
        {
            var pointList = new List<Point>();
            if (geometry != null)
            {
                var envelope = geometry.EnvelopeInternal;
                pointList.Add(new Point(envelope.MinX, envelope.MinY));
                pointList.Add(new Point(envelope.MaxX, envelope.MaxY));
            }

            return pointList;
        }
    }
}
