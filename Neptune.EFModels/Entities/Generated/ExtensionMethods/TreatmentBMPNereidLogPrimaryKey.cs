//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPNereidLog


namespace Neptune.EFModels.Entities
{
    public class TreatmentBMPNereidLogPrimaryKey : EntityPrimaryKey<TreatmentBMPNereidLog>
    {
        public TreatmentBMPNereidLogPrimaryKey() : base(){}
        public TreatmentBMPNereidLogPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPNereidLogPrimaryKey(TreatmentBMPNereidLog treatmentBMPNereidLog) : base(treatmentBMPNereidLog){}

        public static implicit operator TreatmentBMPNereidLogPrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPNereidLogPrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPNereidLogPrimaryKey(TreatmentBMPNereidLog treatmentBMPNereidLog)
        {
            return new TreatmentBMPNereidLogPrimaryKey(treatmentBMPNereidLog);
        }
    }
}