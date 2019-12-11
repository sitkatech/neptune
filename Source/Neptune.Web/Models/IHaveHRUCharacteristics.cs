using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using LtInfo.Common.Models;

namespace Neptune.Web.Models
{
    public interface IHaveHRUCharacteristics : IHavePrimaryKey
    {
        DbGeometry GetCatchmentGeometry();

        ICollection<HRUCharacteristic> HRUCharacteristics { get; set; }
    }

    public static class IHaveHRUCharacteristicsModelExtensions
    {
        public static double GetLandUse(this IHaveHRUCharacteristics entityWithHRUCharacteristics)
        {
            //entityWithHRUCharacteristics.HRUCharacteristics

            throw new NotImplementedException();
        }
        public static double GetArea(this IHaveHRUCharacteristics entityWithHRUCharacteristics)
        {
            throw new NotImplementedException();
        }

        public static double GetImperviousCover(this IHaveHRUCharacteristics entityWithHRUCharacteristics)
        {
            throw new NotImplementedException();
        }

        public static double GetGrossArea(this IHaveHRUCharacteristics entityWithHRUCharacteristics)
        {
            throw new NotImplementedException();
        }
    }

    public class HRUCharacteristicsSummarySimple
    {
        public string LandUse { get; set; }
        public double Area { get; set; }
        public double ImperviousCover { get; set; }
    }
}
