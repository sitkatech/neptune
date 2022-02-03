namespace Hippocamp.EFModels.Entities
{
    public partial class TreatmentBMP
    {
        public double Longitude => LocationPoint4326.Coordinate.X;
        public double Latitude => LocationPoint4326.Coordinate.Y;
    }
}