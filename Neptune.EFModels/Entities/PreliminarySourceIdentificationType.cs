namespace Neptune.EFModels.Entities
{
    public abstract partial class PreliminarySourceIdentificationType
    {
        public virtual bool IsOther()
        {
            return false;
        }
    }

    public partial class PreliminarySourceIdentificationTypeIllegalDumpingOther
    {
        public override bool IsOther()
        {
            return true;
        }
    }

    public partial class PreliminarySourceIdentificationTypeVehiclesOther
    {
        public override bool IsOther()
        {
            return true;
        }
    }

    public partial class PreliminarySourceIdentificationTypeInadequateWasteContainerManagementOther
    {
        public override bool IsOther()
        {
            return true;
        }
    }

    public partial class PreliminarySourceIdentificationTypePedestrianLitterOther
    {
        public override bool IsOther()
        {
            return true;
        }
    }
}