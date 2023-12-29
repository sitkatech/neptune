namespace Neptune.EFModels.Entities
{
    public abstract partial class PreliminarySourceIdentificationType
    {
        public virtual string GetDisplayName()
        {
            return PreliminarySourceIdentificationTypeDisplayName;
        }

        public virtual bool IsOther()
        {
            return false;
        }
    }

    public partial class PreliminarySourceIdentificationTypeIllegalDumpingOther
    {
        public override string GetDisplayName()
        {
            return "Other";
        }

        public override bool IsOther()
        {
            return true;
        }
    }

    public partial class PreliminarySourceIdentificationTypeVehiclesOther
    {
        public override string GetDisplayName()
        {
            return "Other";
        }

        public override bool IsOther()
        {
            return true;
        }
    }

    public partial class PreliminarySourceIdentificationTypeInadequateWasteContainerManagementOther
    {
        public override string GetDisplayName()
        {
            return "Other";
        }

        public override bool IsOther()
        {
            return true;
        }
    }

    public partial class PreliminarySourceIdentificationTypePedestrianLitterOther
    {
        public override string GetDisplayName()
        {
            return "Other";
        }

        public override bool IsOther()
        {
            return true;
        }
    }
}