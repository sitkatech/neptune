using System;
using System.ComponentModel.DataAnnotations;
using Neptune.Web.Views.ObservationType;
using Newtonsoft.Json;

namespace Neptune.Web.Models
{
    public partial class ObservationTypeCollectionMethod
    {
        public abstract bool ValidateJson(string json);
    }

    public partial class ObservationTypeCollectionMethodDiscreteValue
    {
        public override bool ValidateJson(string json)
        {
            try
            {
                DiscreteValueSchema schema = JsonConvert.DeserializeObject<DiscreteValueSchema>(json);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }

    public partial class ObservationTypeCollectionMethodRate
    {
        public override bool ValidateJson(string json)
        {
            try
            {
                RateSchema schema = JsonConvert.DeserializeObject<RateSchema>(json);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }

    public partial class ObservationTypeCollectionMethodPassFail
    {
        public override bool ValidateJson(string json)
        {
            try
            {
                PassFailSchema schema = JsonConvert.DeserializeObject<PassFailSchema>(json);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }

    public partial class ObservationTypeCollectionMethodPercentage
    {
        public override bool ValidateJson(string json)
        {
            try
            {
                PercentageSchema schema = JsonConvert.DeserializeObject<PercentageSchema>(json);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}