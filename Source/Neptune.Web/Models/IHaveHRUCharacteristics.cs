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
            throw new NotImplementedException();

        }
    }
}
