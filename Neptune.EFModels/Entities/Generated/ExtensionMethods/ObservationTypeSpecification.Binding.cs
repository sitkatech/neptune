//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationTypeSpecification]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Neptune.Models.DataTransferObjects;


namespace Neptune.EFModels.Entities
{
    public abstract partial class ObservationTypeSpecification : IHavePrimaryKey
    {
        public static readonly ObservationTypeSpecificationPassFail_PassFail_None PassFail_PassFail_None = Neptune.EFModels.Entities.ObservationTypeSpecificationPassFail_PassFail_None.Instance;
        public static readonly ObservationTypeSpecificationDiscreteValues_HighTargetValue_DiscreteThresholdValue DiscreteValues_HighTargetValue_DiscreteThresholdValue = Neptune.EFModels.Entities.ObservationTypeSpecificationDiscreteValues_HighTargetValue_DiscreteThresholdValue.Instance;
        public static readonly ObservationTypeSpecificationDiscreteValues_HighTargetValue_PercentFromBenchmark DiscreteValues_HighTargetValue_PercentFromBenchmark = Neptune.EFModels.Entities.ObservationTypeSpecificationDiscreteValues_HighTargetValue_PercentFromBenchmark.Instance;
        public static readonly ObservationTypeSpecificationDiscreteValues_LowTargetValue_DiscreteThresholdValue DiscreteValues_LowTargetValue_DiscreteThresholdValue = Neptune.EFModels.Entities.ObservationTypeSpecificationDiscreteValues_LowTargetValue_DiscreteThresholdValue.Instance;
        public static readonly ObservationTypeSpecificationDiscreteValues_LowTargetValue_PercentFromBenchmark DiscreteValues_LowTargetValue_PercentFromBenchmark = Neptune.EFModels.Entities.ObservationTypeSpecificationDiscreteValues_LowTargetValue_PercentFromBenchmark.Instance;
        public static readonly ObservationTypeSpecificationDiscreteValues_SpecificTargetValue_DiscreteThresholdValue DiscreteValues_SpecificTargetValue_DiscreteThresholdValue = Neptune.EFModels.Entities.ObservationTypeSpecificationDiscreteValues_SpecificTargetValue_DiscreteThresholdValue.Instance;
        public static readonly ObservationTypeSpecificationDiscreteValues_SpecificTargetValue_PercentFromBenchmark DiscreteValues_SpecificTargetValue_PercentFromBenchmark = Neptune.EFModels.Entities.ObservationTypeSpecificationDiscreteValues_SpecificTargetValue_PercentFromBenchmark.Instance;
        public static readonly ObservationTypeSpecificationPercentValue_HighTargetValue_DiscreteThresholdValue PercentValue_HighTargetValue_DiscreteThresholdValue = Neptune.EFModels.Entities.ObservationTypeSpecificationPercentValue_HighTargetValue_DiscreteThresholdValue.Instance;
        public static readonly ObservationTypeSpecificationPercentValue_HighTargetValue_PercentFromBenchmark PercentValue_HighTargetValue_PercentFromBenchmark = Neptune.EFModels.Entities.ObservationTypeSpecificationPercentValue_HighTargetValue_PercentFromBenchmark.Instance;
        public static readonly ObservationTypeSpecificationPercentValue_LowTargetValue_DiscreteThresholdValue PercentValue_LowTargetValue_DiscreteThresholdValue = Neptune.EFModels.Entities.ObservationTypeSpecificationPercentValue_LowTargetValue_DiscreteThresholdValue.Instance;
        public static readonly ObservationTypeSpecificationPercentValue_LowTargetValue_PercentFromBenchmark PercentValue_LowTargetValue_PercentFromBenchmark = Neptune.EFModels.Entities.ObservationTypeSpecificationPercentValue_LowTargetValue_PercentFromBenchmark.Instance;
        public static readonly ObservationTypeSpecificationPercentValue_SpecificTargetValue_DiscreteThresholdValue PercentValue_SpecificTargetValue_DiscreteThresholdValue = Neptune.EFModels.Entities.ObservationTypeSpecificationPercentValue_SpecificTargetValue_DiscreteThresholdValue.Instance;
        public static readonly ObservationTypeSpecificationPercentValue_SpecificTargetValue_PercentFromBenchmark PercentValue_SpecificTargetValue_PercentFromBenchmark = Neptune.EFModels.Entities.ObservationTypeSpecificationPercentValue_SpecificTargetValue_PercentFromBenchmark.Instance;

        public static readonly List<ObservationTypeSpecification> All;
        public static readonly List<ObservationTypeSpecificationDto> AllAsDto;
        public static readonly ReadOnlyDictionary<int, ObservationTypeSpecification> AllLookupDictionary;
        public static readonly ReadOnlyDictionary<int, ObservationTypeSpecificationDto> AllAsDtoLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static ObservationTypeSpecification()
        {
            All = new List<ObservationTypeSpecification> { PassFail_PassFail_None, DiscreteValues_HighTargetValue_DiscreteThresholdValue, DiscreteValues_HighTargetValue_PercentFromBenchmark, DiscreteValues_LowTargetValue_DiscreteThresholdValue, DiscreteValues_LowTargetValue_PercentFromBenchmark, DiscreteValues_SpecificTargetValue_DiscreteThresholdValue, DiscreteValues_SpecificTargetValue_PercentFromBenchmark, PercentValue_HighTargetValue_DiscreteThresholdValue, PercentValue_HighTargetValue_PercentFromBenchmark, PercentValue_LowTargetValue_DiscreteThresholdValue, PercentValue_LowTargetValue_PercentFromBenchmark, PercentValue_SpecificTargetValue_DiscreteThresholdValue, PercentValue_SpecificTargetValue_PercentFromBenchmark };
            AllAsDto = new List<ObservationTypeSpecificationDto> { PassFail_PassFail_None.AsDto(), DiscreteValues_HighTargetValue_DiscreteThresholdValue.AsDto(), DiscreteValues_HighTargetValue_PercentFromBenchmark.AsDto(), DiscreteValues_LowTargetValue_DiscreteThresholdValue.AsDto(), DiscreteValues_LowTargetValue_PercentFromBenchmark.AsDto(), DiscreteValues_SpecificTargetValue_DiscreteThresholdValue.AsDto(), DiscreteValues_SpecificTargetValue_PercentFromBenchmark.AsDto(), PercentValue_HighTargetValue_DiscreteThresholdValue.AsDto(), PercentValue_HighTargetValue_PercentFromBenchmark.AsDto(), PercentValue_LowTargetValue_DiscreteThresholdValue.AsDto(), PercentValue_LowTargetValue_PercentFromBenchmark.AsDto(), PercentValue_SpecificTargetValue_DiscreteThresholdValue.AsDto(), PercentValue_SpecificTargetValue_PercentFromBenchmark.AsDto() };
            AllLookupDictionary = new ReadOnlyDictionary<int, ObservationTypeSpecification>(All.ToDictionary(x => x.ObservationTypeSpecificationID));
            AllAsDtoLookupDictionary = new ReadOnlyDictionary<int, ObservationTypeSpecificationDto>(AllAsDto.ToDictionary(x => x.ObservationTypeSpecificationID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected ObservationTypeSpecification(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID)
        {
            ObservationTypeSpecificationID = observationTypeSpecificationID;
            ObservationTypeSpecificationName = observationTypeSpecificationName;
            ObservationTypeSpecificationDisplayName = observationTypeSpecificationDisplayName;
            ObservationTypeCollectionMethodID = observationTypeCollectionMethodID;
            ObservationTargetTypeID = observationTargetTypeID;
            ObservationThresholdTypeID = observationThresholdTypeID;
        }
        public ObservationTypeCollectionMethod ObservationTypeCollectionMethod => ObservationTypeCollectionMethod.AllLookupDictionary[ObservationTypeCollectionMethodID];
        public ObservationTargetType ObservationTargetType => ObservationTargetType.AllLookupDictionary[ObservationTargetTypeID];
        public ObservationThresholdType ObservationThresholdType => ObservationThresholdType.AllLookupDictionary[ObservationThresholdTypeID];
        [Key]
        public int ObservationTypeSpecificationID { get; private set; }
        public string ObservationTypeSpecificationName { get; private set; }
        public string ObservationTypeSpecificationDisplayName { get; private set; }
        public int ObservationTypeCollectionMethodID { get; private set; }
        public int ObservationTargetTypeID { get; private set; }
        public int ObservationThresholdTypeID { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return ObservationTypeSpecificationID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(ObservationTypeSpecification other)
        {
            if (other == null)
            {
                return false;
            }
            return other.ObservationTypeSpecificationID == ObservationTypeSpecificationID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as ObservationTypeSpecification);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return ObservationTypeSpecificationID;
        }

        public static bool operator ==(ObservationTypeSpecification left, ObservationTypeSpecification right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ObservationTypeSpecification left, ObservationTypeSpecification right)
        {
            return !Equals(left, right);
        }

        public ObservationTypeSpecificationEnum ToEnum => (ObservationTypeSpecificationEnum)GetHashCode();

        public static ObservationTypeSpecification ToType(int enumValue)
        {
            return ToType((ObservationTypeSpecificationEnum)enumValue);
        }

        public static ObservationTypeSpecification ToType(ObservationTypeSpecificationEnum enumValue)
        {
            switch (enumValue)
            {
                case ObservationTypeSpecificationEnum.DiscreteValues_HighTargetValue_DiscreteThresholdValue:
                    return DiscreteValues_HighTargetValue_DiscreteThresholdValue;
                case ObservationTypeSpecificationEnum.DiscreteValues_HighTargetValue_PercentFromBenchmark:
                    return DiscreteValues_HighTargetValue_PercentFromBenchmark;
                case ObservationTypeSpecificationEnum.DiscreteValues_LowTargetValue_DiscreteThresholdValue:
                    return DiscreteValues_LowTargetValue_DiscreteThresholdValue;
                case ObservationTypeSpecificationEnum.DiscreteValues_LowTargetValue_PercentFromBenchmark:
                    return DiscreteValues_LowTargetValue_PercentFromBenchmark;
                case ObservationTypeSpecificationEnum.DiscreteValues_SpecificTargetValue_DiscreteThresholdValue:
                    return DiscreteValues_SpecificTargetValue_DiscreteThresholdValue;
                case ObservationTypeSpecificationEnum.DiscreteValues_SpecificTargetValue_PercentFromBenchmark:
                    return DiscreteValues_SpecificTargetValue_PercentFromBenchmark;
                case ObservationTypeSpecificationEnum.PassFail_PassFail_None:
                    return PassFail_PassFail_None;
                case ObservationTypeSpecificationEnum.PercentValue_HighTargetValue_DiscreteThresholdValue:
                    return PercentValue_HighTargetValue_DiscreteThresholdValue;
                case ObservationTypeSpecificationEnum.PercentValue_HighTargetValue_PercentFromBenchmark:
                    return PercentValue_HighTargetValue_PercentFromBenchmark;
                case ObservationTypeSpecificationEnum.PercentValue_LowTargetValue_DiscreteThresholdValue:
                    return PercentValue_LowTargetValue_DiscreteThresholdValue;
                case ObservationTypeSpecificationEnum.PercentValue_LowTargetValue_PercentFromBenchmark:
                    return PercentValue_LowTargetValue_PercentFromBenchmark;
                case ObservationTypeSpecificationEnum.PercentValue_SpecificTargetValue_DiscreteThresholdValue:
                    return PercentValue_SpecificTargetValue_DiscreteThresholdValue;
                case ObservationTypeSpecificationEnum.PercentValue_SpecificTargetValue_PercentFromBenchmark:
                    return PercentValue_SpecificTargetValue_PercentFromBenchmark;
                default:
                    throw new ArgumentException("Unable to map Enum: {enumValue}");
            }
        }
    }

    public enum ObservationTypeSpecificationEnum
    {
        PassFail_PassFail_None = 1,
        DiscreteValues_HighTargetValue_DiscreteThresholdValue = 2,
        DiscreteValues_HighTargetValue_PercentFromBenchmark = 3,
        DiscreteValues_LowTargetValue_DiscreteThresholdValue = 4,
        DiscreteValues_LowTargetValue_PercentFromBenchmark = 5,
        DiscreteValues_SpecificTargetValue_DiscreteThresholdValue = 6,
        DiscreteValues_SpecificTargetValue_PercentFromBenchmark = 7,
        PercentValue_HighTargetValue_DiscreteThresholdValue = 14,
        PercentValue_HighTargetValue_PercentFromBenchmark = 15,
        PercentValue_LowTargetValue_DiscreteThresholdValue = 16,
        PercentValue_LowTargetValue_PercentFromBenchmark = 17,
        PercentValue_SpecificTargetValue_DiscreteThresholdValue = 18,
        PercentValue_SpecificTargetValue_PercentFromBenchmark = 19
    }

    public partial class ObservationTypeSpecificationPassFail_PassFail_None : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationPassFail_PassFail_None(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationPassFail_PassFail_None Instance = new ObservationTypeSpecificationPassFail_PassFail_None(1, @"PassFail_PassFail_None", @" PassFail_PassFail_None", 3, 1, 3);
    }

    public partial class ObservationTypeSpecificationDiscreteValues_HighTargetValue_DiscreteThresholdValue : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationDiscreteValues_HighTargetValue_DiscreteThresholdValue(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationDiscreteValues_HighTargetValue_DiscreteThresholdValue Instance = new ObservationTypeSpecificationDiscreteValues_HighTargetValue_DiscreteThresholdValue(2, @"DiscreteValues_HighTargetValue_DiscreteThresholdValue", @" DiscreteValues_HighTargetValue_DiscreteThresholdValue", 1, 2, 1);
    }

    public partial class ObservationTypeSpecificationDiscreteValues_HighTargetValue_PercentFromBenchmark : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationDiscreteValues_HighTargetValue_PercentFromBenchmark(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationDiscreteValues_HighTargetValue_PercentFromBenchmark Instance = new ObservationTypeSpecificationDiscreteValues_HighTargetValue_PercentFromBenchmark(3, @"DiscreteValues_HighTargetValue_PercentFromBenchmark", @" DiscreteValues_HighTargetValue_PercentFromBenchmark", 1, 2, 2);
    }

    public partial class ObservationTypeSpecificationDiscreteValues_LowTargetValue_DiscreteThresholdValue : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationDiscreteValues_LowTargetValue_DiscreteThresholdValue(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationDiscreteValues_LowTargetValue_DiscreteThresholdValue Instance = new ObservationTypeSpecificationDiscreteValues_LowTargetValue_DiscreteThresholdValue(4, @"DiscreteValues_LowTargetValue_DiscreteThresholdValue", @" DiscreteValues_LowTargetValue_DiscreteThresholdValue", 1, 3, 1);
    }

    public partial class ObservationTypeSpecificationDiscreteValues_LowTargetValue_PercentFromBenchmark : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationDiscreteValues_LowTargetValue_PercentFromBenchmark(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationDiscreteValues_LowTargetValue_PercentFromBenchmark Instance = new ObservationTypeSpecificationDiscreteValues_LowTargetValue_PercentFromBenchmark(5, @"DiscreteValues_LowTargetValue_PercentFromBenchmark", @" DiscreteValues_LowTargetValue_PercentFromBenchmark", 1, 3, 2);
    }

    public partial class ObservationTypeSpecificationDiscreteValues_SpecificTargetValue_DiscreteThresholdValue : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationDiscreteValues_SpecificTargetValue_DiscreteThresholdValue(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationDiscreteValues_SpecificTargetValue_DiscreteThresholdValue Instance = new ObservationTypeSpecificationDiscreteValues_SpecificTargetValue_DiscreteThresholdValue(6, @"DiscreteValues_SpecificTargetValue_DiscreteThresholdValue", @" DiscreteValues_SpecificTargetValue_DiscreteThresholdValue", 1, 4, 1);
    }

    public partial class ObservationTypeSpecificationDiscreteValues_SpecificTargetValue_PercentFromBenchmark : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationDiscreteValues_SpecificTargetValue_PercentFromBenchmark(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationDiscreteValues_SpecificTargetValue_PercentFromBenchmark Instance = new ObservationTypeSpecificationDiscreteValues_SpecificTargetValue_PercentFromBenchmark(7, @"DiscreteValues_SpecificTargetValue_PercentFromBenchmark", @" DiscreteValues_SpecificTargetValue_PercentFromBenchmark", 1, 4, 2);
    }

    public partial class ObservationTypeSpecificationPercentValue_HighTargetValue_DiscreteThresholdValue : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationPercentValue_HighTargetValue_DiscreteThresholdValue(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationPercentValue_HighTargetValue_DiscreteThresholdValue Instance = new ObservationTypeSpecificationPercentValue_HighTargetValue_DiscreteThresholdValue(14, @"PercentValue_HighTargetValue_DiscreteThresholdValue", @" PercentValue_HighTargetValue_DiscreteThresholdValue", 4, 2, 1);
    }

    public partial class ObservationTypeSpecificationPercentValue_HighTargetValue_PercentFromBenchmark : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationPercentValue_HighTargetValue_PercentFromBenchmark(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationPercentValue_HighTargetValue_PercentFromBenchmark Instance = new ObservationTypeSpecificationPercentValue_HighTargetValue_PercentFromBenchmark(15, @"PercentValue_HighTargetValue_PercentFromBenchmark", @" PercentValue_HighTargetValue_PercentFromBenchmark", 4, 2, 2);
    }

    public partial class ObservationTypeSpecificationPercentValue_LowTargetValue_DiscreteThresholdValue : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationPercentValue_LowTargetValue_DiscreteThresholdValue(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationPercentValue_LowTargetValue_DiscreteThresholdValue Instance = new ObservationTypeSpecificationPercentValue_LowTargetValue_DiscreteThresholdValue(16, @"PercentValue_LowTargetValue_DiscreteThresholdValue", @" PercentValue_LowTargetValue_DiscreteThresholdValue", 4, 3, 1);
    }

    public partial class ObservationTypeSpecificationPercentValue_LowTargetValue_PercentFromBenchmark : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationPercentValue_LowTargetValue_PercentFromBenchmark(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationPercentValue_LowTargetValue_PercentFromBenchmark Instance = new ObservationTypeSpecificationPercentValue_LowTargetValue_PercentFromBenchmark(17, @"PercentValue_LowTargetValue_PercentFromBenchmark", @" PercentValue_LowTargetValue_PercentFromBenchmark", 4, 3, 2);
    }

    public partial class ObservationTypeSpecificationPercentValue_SpecificTargetValue_DiscreteThresholdValue : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationPercentValue_SpecificTargetValue_DiscreteThresholdValue(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationPercentValue_SpecificTargetValue_DiscreteThresholdValue Instance = new ObservationTypeSpecificationPercentValue_SpecificTargetValue_DiscreteThresholdValue(18, @"PercentValue_SpecificTargetValue_DiscreteThresholdValue", @" PercentValue_SpecificTargetValue_DiscreteThresholdValue", 4, 4, 1);
    }

    public partial class ObservationTypeSpecificationPercentValue_SpecificTargetValue_PercentFromBenchmark : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationPercentValue_SpecificTargetValue_PercentFromBenchmark(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationPercentValue_SpecificTargetValue_PercentFromBenchmark Instance = new ObservationTypeSpecificationPercentValue_SpecificTargetValue_PercentFromBenchmark(19, @"PercentValue_SpecificTargetValue_PercentFromBenchmark", @" PercentValue_SpecificTargetValue_PercentFromBenchmark", 4, 4, 2);
    }
}