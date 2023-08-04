//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DryWeatherFlowOverride]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Neptune.Models.DataTransferObjects;


namespace Neptune.EFModels.Entities
{
    public abstract partial class DryWeatherFlowOverride
    {
        public static readonly DryWeatherFlowOverrideNo No = Neptune.EFModels.Entities.DryWeatherFlowOverrideNo.Instance;
        public static readonly DryWeatherFlowOverrideYes Yes = Neptune.EFModels.Entities.DryWeatherFlowOverrideYes.Instance;

        public static readonly List<DryWeatherFlowOverride> All;
        public static readonly List<DryWeatherFlowOverrideDto> AllAsDto;
        public static readonly ReadOnlyDictionary<int, DryWeatherFlowOverride> AllLookupDictionary;
        public static readonly ReadOnlyDictionary<int, DryWeatherFlowOverrideDto> AllAsDtoLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static DryWeatherFlowOverride()
        {
            All = new List<DryWeatherFlowOverride> { No, Yes };
            AllAsDto = new List<DryWeatherFlowOverrideDto> { No.AsDto(), Yes.AsDto() };
            AllLookupDictionary = new ReadOnlyDictionary<int, DryWeatherFlowOverride>(All.ToDictionary(x => x.DryWeatherFlowOverrideID));
            AllAsDtoLookupDictionary = new ReadOnlyDictionary<int, DryWeatherFlowOverrideDto>(AllAsDto.ToDictionary(x => x.DryWeatherFlowOverrideID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected DryWeatherFlowOverride(int dryWeatherFlowOverrideID, string dryWeatherFlowOverrideName, string dryWeatherFlowOverrideDisplayName)
        {
            DryWeatherFlowOverrideID = dryWeatherFlowOverrideID;
            DryWeatherFlowOverrideName = dryWeatherFlowOverrideName;
            DryWeatherFlowOverrideDisplayName = dryWeatherFlowOverrideDisplayName;
        }

        [Key]
        public int DryWeatherFlowOverrideID { get; private set; }
        public string DryWeatherFlowOverrideName { get; private set; }
        public string DryWeatherFlowOverrideDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return DryWeatherFlowOverrideID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(DryWeatherFlowOverride other)
        {
            if (other == null)
            {
                return false;
            }
            return other.DryWeatherFlowOverrideID == DryWeatherFlowOverrideID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as DryWeatherFlowOverride);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return DryWeatherFlowOverrideID;
        }

        public static bool operator ==(DryWeatherFlowOverride left, DryWeatherFlowOverride right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DryWeatherFlowOverride left, DryWeatherFlowOverride right)
        {
            return !Equals(left, right);
        }

        public DryWeatherFlowOverrideEnum ToEnum => (DryWeatherFlowOverrideEnum)GetHashCode();

        public static DryWeatherFlowOverride ToType(int enumValue)
        {
            return ToType((DryWeatherFlowOverrideEnum)enumValue);
        }

        public static DryWeatherFlowOverride ToType(DryWeatherFlowOverrideEnum enumValue)
        {
            switch (enumValue)
            {
                case DryWeatherFlowOverrideEnum.No:
                    return No;
                case DryWeatherFlowOverrideEnum.Yes:
                    return Yes;
                default:
                    throw new ArgumentException("Unable to map Enum: {enumValue}");
            }
        }
    }

    public enum DryWeatherFlowOverrideEnum
    {
        No = 1,
        Yes = 2
    }

    public partial class DryWeatherFlowOverrideNo : DryWeatherFlowOverride
    {
        private DryWeatherFlowOverrideNo(int dryWeatherFlowOverrideID, string dryWeatherFlowOverrideName, string dryWeatherFlowOverrideDisplayName) : base(dryWeatherFlowOverrideID, dryWeatherFlowOverrideName, dryWeatherFlowOverrideDisplayName) {}
        public static readonly DryWeatherFlowOverrideNo Instance = new DryWeatherFlowOverrideNo(1, @"No", @"No - As Modeled");
    }

    public partial class DryWeatherFlowOverrideYes : DryWeatherFlowOverride
    {
        private DryWeatherFlowOverrideYes(int dryWeatherFlowOverrideID, string dryWeatherFlowOverrideName, string dryWeatherFlowOverrideDisplayName) : base(dryWeatherFlowOverrideID, dryWeatherFlowOverrideName, dryWeatherFlowOverrideDisplayName) {}
        public static readonly DryWeatherFlowOverrideYes Instance = new DryWeatherFlowOverrideYes(2, @"Yes", @"Yes - DWF Effectively Eliminated");
    }
}