//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPInfiltrationReading
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPInfiltrationReadingPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPInfiltrationReading>
    {
        public TreatmentBMPInfiltrationReadingPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPInfiltrationReadingPrimaryKey(TreatmentBMPInfiltrationReading treatmentBMPInfiltrationReading) : base(treatmentBMPInfiltrationReading){}

        public static implicit operator TreatmentBMPInfiltrationReadingPrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPInfiltrationReadingPrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPInfiltrationReadingPrimaryKey(TreatmentBMPInfiltrationReading treatmentBMPInfiltrationReading)
        {
            return new TreatmentBMPInfiltrationReadingPrimaryKey(treatmentBMPInfiltrationReading);
        }
    }
}