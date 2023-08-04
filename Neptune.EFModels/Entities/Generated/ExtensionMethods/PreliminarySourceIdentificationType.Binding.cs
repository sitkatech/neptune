//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PreliminarySourceIdentificationType]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Neptune.Models.DataTransferObjects;


namespace Neptune.EFModels.Entities
{
    public abstract partial class PreliminarySourceIdentificationType
    {
        public static readonly PreliminarySourceIdentificationTypeMovingVehicles MovingVehicles = Neptune.EFModels.Entities.PreliminarySourceIdentificationTypeMovingVehicles.Instance;
        public static readonly PreliminarySourceIdentificationTypeParkedCars ParkedCars = Neptune.EFModels.Entities.PreliminarySourceIdentificationTypeParkedCars.Instance;
        public static readonly PreliminarySourceIdentificationTypeUncoveredLoads UncoveredLoads = Neptune.EFModels.Entities.PreliminarySourceIdentificationTypeUncoveredLoads.Instance;
        public static readonly PreliminarySourceIdentificationTypeVehiclesOther VehiclesOther = Neptune.EFModels.Entities.PreliminarySourceIdentificationTypeVehiclesOther.Instance;
        public static readonly PreliminarySourceIdentificationTypeOverflowingReceptacles OverflowingReceptacles = Neptune.EFModels.Entities.PreliminarySourceIdentificationTypeOverflowingReceptacles.Instance;
        public static readonly PreliminarySourceIdentificationTypeTrashDispersal TrashDispersal = Neptune.EFModels.Entities.PreliminarySourceIdentificationTypeTrashDispersal.Instance;
        public static readonly PreliminarySourceIdentificationTypeInadequateWasteContainerManagementOther InadequateWasteContainerManagementOther = Neptune.EFModels.Entities.PreliminarySourceIdentificationTypeInadequateWasteContainerManagementOther.Instance;
        public static readonly PreliminarySourceIdentificationTypeRestaurants Restaurants = Neptune.EFModels.Entities.PreliminarySourceIdentificationTypeRestaurants.Instance;
        public static readonly PreliminarySourceIdentificationTypeConvenienceStores ConvenienceStores = Neptune.EFModels.Entities.PreliminarySourceIdentificationTypeConvenienceStores.Instance;
        public static readonly PreliminarySourceIdentificationTypeLiquorStores LiquorStores = Neptune.EFModels.Entities.PreliminarySourceIdentificationTypeLiquorStores.Instance;
        public static readonly PreliminarySourceIdentificationTypeBusStops BusStops = Neptune.EFModels.Entities.PreliminarySourceIdentificationTypeBusStops.Instance;
        public static readonly PreliminarySourceIdentificationTypeSpecialEvents SpecialEvents = Neptune.EFModels.Entities.PreliminarySourceIdentificationTypeSpecialEvents.Instance;
        public static readonly PreliminarySourceIdentificationTypePedestrianLitterOther PedestrianLitterOther = Neptune.EFModels.Entities.PreliminarySourceIdentificationTypePedestrianLitterOther.Instance;
        public static readonly PreliminarySourceIdentificationTypeIllegalDumpingOnLand IllegalDumpingOnLand = Neptune.EFModels.Entities.PreliminarySourceIdentificationTypeIllegalDumpingOnLand.Instance;
        public static readonly PreliminarySourceIdentificationTypeHomelessencampments Homelessencampments = Neptune.EFModels.Entities.PreliminarySourceIdentificationTypeHomelessencampments.Instance;
        public static readonly PreliminarySourceIdentificationTypeIllegalDumpingOther IllegalDumpingOther = Neptune.EFModels.Entities.PreliminarySourceIdentificationTypeIllegalDumpingOther.Instance;

        public static readonly List<PreliminarySourceIdentificationType> All;
        public static readonly List<PreliminarySourceIdentificationTypeDto> AllAsDto;
        public static readonly ReadOnlyDictionary<int, PreliminarySourceIdentificationType> AllLookupDictionary;
        public static readonly ReadOnlyDictionary<int, PreliminarySourceIdentificationTypeDto> AllAsDtoLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static PreliminarySourceIdentificationType()
        {
            All = new List<PreliminarySourceIdentificationType> { MovingVehicles, ParkedCars, UncoveredLoads, VehiclesOther, OverflowingReceptacles, TrashDispersal, InadequateWasteContainerManagementOther, Restaurants, ConvenienceStores, LiquorStores, BusStops, SpecialEvents, PedestrianLitterOther, IllegalDumpingOnLand, Homelessencampments, IllegalDumpingOther };
            AllAsDto = new List<PreliminarySourceIdentificationTypeDto> { MovingVehicles.AsDto(), ParkedCars.AsDto(), UncoveredLoads.AsDto(), VehiclesOther.AsDto(), OverflowingReceptacles.AsDto(), TrashDispersal.AsDto(), InadequateWasteContainerManagementOther.AsDto(), Restaurants.AsDto(), ConvenienceStores.AsDto(), LiquorStores.AsDto(), BusStops.AsDto(), SpecialEvents.AsDto(), PedestrianLitterOther.AsDto(), IllegalDumpingOnLand.AsDto(), Homelessencampments.AsDto(), IllegalDumpingOther.AsDto() };
            AllLookupDictionary = new ReadOnlyDictionary<int, PreliminarySourceIdentificationType>(All.ToDictionary(x => x.PreliminarySourceIdentificationTypeID));
            AllAsDtoLookupDictionary = new ReadOnlyDictionary<int, PreliminarySourceIdentificationTypeDto>(AllAsDto.ToDictionary(x => x.PreliminarySourceIdentificationTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected PreliminarySourceIdentificationType(int preliminarySourceIdentificationTypeID, string preliminarySourceIdentificationTypeName, string preliminarySourceIdentificationTypeDisplayName, int preliminarySourceIdentificationCategoryID)
        {
            PreliminarySourceIdentificationTypeID = preliminarySourceIdentificationTypeID;
            PreliminarySourceIdentificationTypeName = preliminarySourceIdentificationTypeName;
            PreliminarySourceIdentificationTypeDisplayName = preliminarySourceIdentificationTypeDisplayName;
            PreliminarySourceIdentificationCategoryID = preliminarySourceIdentificationCategoryID;
        }
        public PreliminarySourceIdentificationCategory PreliminarySourceIdentificationCategory => PreliminarySourceIdentificationCategory.AllLookupDictionary[PreliminarySourceIdentificationCategoryID];
        [Key]
        public int PreliminarySourceIdentificationTypeID { get; private set; }
        public string PreliminarySourceIdentificationTypeName { get; private set; }
        public string PreliminarySourceIdentificationTypeDisplayName { get; private set; }
        public int PreliminarySourceIdentificationCategoryID { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return PreliminarySourceIdentificationTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(PreliminarySourceIdentificationType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.PreliminarySourceIdentificationTypeID == PreliminarySourceIdentificationTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as PreliminarySourceIdentificationType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return PreliminarySourceIdentificationTypeID;
        }

        public static bool operator ==(PreliminarySourceIdentificationType left, PreliminarySourceIdentificationType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PreliminarySourceIdentificationType left, PreliminarySourceIdentificationType right)
        {
            return !Equals(left, right);
        }

        public PreliminarySourceIdentificationTypeEnum ToEnum => (PreliminarySourceIdentificationTypeEnum)GetHashCode();

        public static PreliminarySourceIdentificationType ToType(int enumValue)
        {
            return ToType((PreliminarySourceIdentificationTypeEnum)enumValue);
        }

        public static PreliminarySourceIdentificationType ToType(PreliminarySourceIdentificationTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case PreliminarySourceIdentificationTypeEnum.BusStops:
                    return BusStops;
                case PreliminarySourceIdentificationTypeEnum.ConvenienceStores:
                    return ConvenienceStores;
                case PreliminarySourceIdentificationTypeEnum.Homelessencampments:
                    return Homelessencampments;
                case PreliminarySourceIdentificationTypeEnum.IllegalDumpingOnLand:
                    return IllegalDumpingOnLand;
                case PreliminarySourceIdentificationTypeEnum.IllegalDumpingOther:
                    return IllegalDumpingOther;
                case PreliminarySourceIdentificationTypeEnum.InadequateWasteContainerManagementOther:
                    return InadequateWasteContainerManagementOther;
                case PreliminarySourceIdentificationTypeEnum.LiquorStores:
                    return LiquorStores;
                case PreliminarySourceIdentificationTypeEnum.MovingVehicles:
                    return MovingVehicles;
                case PreliminarySourceIdentificationTypeEnum.OverflowingReceptacles:
                    return OverflowingReceptacles;
                case PreliminarySourceIdentificationTypeEnum.ParkedCars:
                    return ParkedCars;
                case PreliminarySourceIdentificationTypeEnum.PedestrianLitterOther:
                    return PedestrianLitterOther;
                case PreliminarySourceIdentificationTypeEnum.Restaurants:
                    return Restaurants;
                case PreliminarySourceIdentificationTypeEnum.SpecialEvents:
                    return SpecialEvents;
                case PreliminarySourceIdentificationTypeEnum.TrashDispersal:
                    return TrashDispersal;
                case PreliminarySourceIdentificationTypeEnum.UncoveredLoads:
                    return UncoveredLoads;
                case PreliminarySourceIdentificationTypeEnum.VehiclesOther:
                    return VehiclesOther;
                default:
                    throw new ArgumentException("Unable to map Enum: {enumValue}");
            }
        }
    }

    public enum PreliminarySourceIdentificationTypeEnum
    {
        MovingVehicles = 1,
        ParkedCars = 2,
        UncoveredLoads = 3,
        VehiclesOther = 4,
        OverflowingReceptacles = 5,
        TrashDispersal = 6,
        InadequateWasteContainerManagementOther = 7,
        Restaurants = 8,
        ConvenienceStores = 9,
        LiquorStores = 10,
        BusStops = 11,
        SpecialEvents = 12,
        PedestrianLitterOther = 13,
        IllegalDumpingOnLand = 14,
        Homelessencampments = 15,
        IllegalDumpingOther = 16
    }

    public partial class PreliminarySourceIdentificationTypeMovingVehicles : PreliminarySourceIdentificationType
    {
        private PreliminarySourceIdentificationTypeMovingVehicles(int preliminarySourceIdentificationTypeID, string preliminarySourceIdentificationTypeName, string preliminarySourceIdentificationTypeDisplayName, int preliminarySourceIdentificationCategoryID) : base(preliminarySourceIdentificationTypeID, preliminarySourceIdentificationTypeName, preliminarySourceIdentificationTypeDisplayName, preliminarySourceIdentificationCategoryID) {}
        public static readonly PreliminarySourceIdentificationTypeMovingVehicles Instance = new PreliminarySourceIdentificationTypeMovingVehicles(1, @"MovingVehicles", @"Moving Vehicles", 1);
    }

    public partial class PreliminarySourceIdentificationTypeParkedCars : PreliminarySourceIdentificationType
    {
        private PreliminarySourceIdentificationTypeParkedCars(int preliminarySourceIdentificationTypeID, string preliminarySourceIdentificationTypeName, string preliminarySourceIdentificationTypeDisplayName, int preliminarySourceIdentificationCategoryID) : base(preliminarySourceIdentificationTypeID, preliminarySourceIdentificationTypeName, preliminarySourceIdentificationTypeDisplayName, preliminarySourceIdentificationCategoryID) {}
        public static readonly PreliminarySourceIdentificationTypeParkedCars Instance = new PreliminarySourceIdentificationTypeParkedCars(2, @"ParkedCars", @"Parked Cars", 1);
    }

    public partial class PreliminarySourceIdentificationTypeUncoveredLoads : PreliminarySourceIdentificationType
    {
        private PreliminarySourceIdentificationTypeUncoveredLoads(int preliminarySourceIdentificationTypeID, string preliminarySourceIdentificationTypeName, string preliminarySourceIdentificationTypeDisplayName, int preliminarySourceIdentificationCategoryID) : base(preliminarySourceIdentificationTypeID, preliminarySourceIdentificationTypeName, preliminarySourceIdentificationTypeDisplayName, preliminarySourceIdentificationCategoryID) {}
        public static readonly PreliminarySourceIdentificationTypeUncoveredLoads Instance = new PreliminarySourceIdentificationTypeUncoveredLoads(3, @"UncoveredLoads", @"Uncovered Loads", 1);
    }

    public partial class PreliminarySourceIdentificationTypeVehiclesOther : PreliminarySourceIdentificationType
    {
        private PreliminarySourceIdentificationTypeVehiclesOther(int preliminarySourceIdentificationTypeID, string preliminarySourceIdentificationTypeName, string preliminarySourceIdentificationTypeDisplayName, int preliminarySourceIdentificationCategoryID) : base(preliminarySourceIdentificationTypeID, preliminarySourceIdentificationTypeName, preliminarySourceIdentificationTypeDisplayName, preliminarySourceIdentificationCategoryID) {}
        public static readonly PreliminarySourceIdentificationTypeVehiclesOther Instance = new PreliminarySourceIdentificationTypeVehiclesOther(4, @"VehiclesOther", @"Vehicles (Other)", 1);
    }

    public partial class PreliminarySourceIdentificationTypeOverflowingReceptacles : PreliminarySourceIdentificationType
    {
        private PreliminarySourceIdentificationTypeOverflowingReceptacles(int preliminarySourceIdentificationTypeID, string preliminarySourceIdentificationTypeName, string preliminarySourceIdentificationTypeDisplayName, int preliminarySourceIdentificationCategoryID) : base(preliminarySourceIdentificationTypeID, preliminarySourceIdentificationTypeName, preliminarySourceIdentificationTypeDisplayName, preliminarySourceIdentificationCategoryID) {}
        public static readonly PreliminarySourceIdentificationTypeOverflowingReceptacles Instance = new PreliminarySourceIdentificationTypeOverflowingReceptacles(5, @"OverflowingReceptacles", @"Overflowing or uncovered receptacles/dumpsters", 2);
    }

    public partial class PreliminarySourceIdentificationTypeTrashDispersal : PreliminarySourceIdentificationType
    {
        private PreliminarySourceIdentificationTypeTrashDispersal(int preliminarySourceIdentificationTypeID, string preliminarySourceIdentificationTypeName, string preliminarySourceIdentificationTypeDisplayName, int preliminarySourceIdentificationCategoryID) : base(preliminarySourceIdentificationTypeID, preliminarySourceIdentificationTypeName, preliminarySourceIdentificationTypeDisplayName, preliminarySourceIdentificationCategoryID) {}
        public static readonly PreliminarySourceIdentificationTypeTrashDispersal Instance = new PreliminarySourceIdentificationTypeTrashDispersal(6, @"TrashDispersal", @"Dispersal of household trash and recyclables before, during, and after collection ", 2);
    }

    public partial class PreliminarySourceIdentificationTypeInadequateWasteContainerManagementOther : PreliminarySourceIdentificationType
    {
        private PreliminarySourceIdentificationTypeInadequateWasteContainerManagementOther(int preliminarySourceIdentificationTypeID, string preliminarySourceIdentificationTypeName, string preliminarySourceIdentificationTypeDisplayName, int preliminarySourceIdentificationCategoryID) : base(preliminarySourceIdentificationTypeID, preliminarySourceIdentificationTypeName, preliminarySourceIdentificationTypeDisplayName, preliminarySourceIdentificationCategoryID) {}
        public static readonly PreliminarySourceIdentificationTypeInadequateWasteContainerManagementOther Instance = new PreliminarySourceIdentificationTypeInadequateWasteContainerManagementOther(7, @"InadequateWasteContainerManagementOther", @"Inadequate Waste Container Management (Other)", 2);
    }

    public partial class PreliminarySourceIdentificationTypeRestaurants : PreliminarySourceIdentificationType
    {
        private PreliminarySourceIdentificationTypeRestaurants(int preliminarySourceIdentificationTypeID, string preliminarySourceIdentificationTypeName, string preliminarySourceIdentificationTypeDisplayName, int preliminarySourceIdentificationCategoryID) : base(preliminarySourceIdentificationTypeID, preliminarySourceIdentificationTypeName, preliminarySourceIdentificationTypeDisplayName, preliminarySourceIdentificationCategoryID) {}
        public static readonly PreliminarySourceIdentificationTypeRestaurants Instance = new PreliminarySourceIdentificationTypeRestaurants(8, @"Restaurants", @"Restaurants", 3);
    }

    public partial class PreliminarySourceIdentificationTypeConvenienceStores : PreliminarySourceIdentificationType
    {
        private PreliminarySourceIdentificationTypeConvenienceStores(int preliminarySourceIdentificationTypeID, string preliminarySourceIdentificationTypeName, string preliminarySourceIdentificationTypeDisplayName, int preliminarySourceIdentificationCategoryID) : base(preliminarySourceIdentificationTypeID, preliminarySourceIdentificationTypeName, preliminarySourceIdentificationTypeDisplayName, preliminarySourceIdentificationCategoryID) {}
        public static readonly PreliminarySourceIdentificationTypeConvenienceStores Instance = new PreliminarySourceIdentificationTypeConvenienceStores(9, @"ConvenienceStores", @"Convenience Stores", 3);
    }

    public partial class PreliminarySourceIdentificationTypeLiquorStores : PreliminarySourceIdentificationType
    {
        private PreliminarySourceIdentificationTypeLiquorStores(int preliminarySourceIdentificationTypeID, string preliminarySourceIdentificationTypeName, string preliminarySourceIdentificationTypeDisplayName, int preliminarySourceIdentificationCategoryID) : base(preliminarySourceIdentificationTypeID, preliminarySourceIdentificationTypeName, preliminarySourceIdentificationTypeDisplayName, preliminarySourceIdentificationCategoryID) {}
        public static readonly PreliminarySourceIdentificationTypeLiquorStores Instance = new PreliminarySourceIdentificationTypeLiquorStores(10, @"LiquorStores", @"Liquor Stores", 3);
    }

    public partial class PreliminarySourceIdentificationTypeBusStops : PreliminarySourceIdentificationType
    {
        private PreliminarySourceIdentificationTypeBusStops(int preliminarySourceIdentificationTypeID, string preliminarySourceIdentificationTypeName, string preliminarySourceIdentificationTypeDisplayName, int preliminarySourceIdentificationCategoryID) : base(preliminarySourceIdentificationTypeID, preliminarySourceIdentificationTypeName, preliminarySourceIdentificationTypeDisplayName, preliminarySourceIdentificationCategoryID) {}
        public static readonly PreliminarySourceIdentificationTypeBusStops Instance = new PreliminarySourceIdentificationTypeBusStops(11, @"BusStops", @"Bus Stops", 3);
    }

    public partial class PreliminarySourceIdentificationTypeSpecialEvents : PreliminarySourceIdentificationType
    {
        private PreliminarySourceIdentificationTypeSpecialEvents(int preliminarySourceIdentificationTypeID, string preliminarySourceIdentificationTypeName, string preliminarySourceIdentificationTypeDisplayName, int preliminarySourceIdentificationCategoryID) : base(preliminarySourceIdentificationTypeID, preliminarySourceIdentificationTypeName, preliminarySourceIdentificationTypeDisplayName, preliminarySourceIdentificationCategoryID) {}
        public static readonly PreliminarySourceIdentificationTypeSpecialEvents Instance = new PreliminarySourceIdentificationTypeSpecialEvents(12, @"SpecialEvents", @"Special Events", 3);
    }

    public partial class PreliminarySourceIdentificationTypePedestrianLitterOther : PreliminarySourceIdentificationType
    {
        private PreliminarySourceIdentificationTypePedestrianLitterOther(int preliminarySourceIdentificationTypeID, string preliminarySourceIdentificationTypeName, string preliminarySourceIdentificationTypeDisplayName, int preliminarySourceIdentificationCategoryID) : base(preliminarySourceIdentificationTypeID, preliminarySourceIdentificationTypeName, preliminarySourceIdentificationTypeDisplayName, preliminarySourceIdentificationCategoryID) {}
        public static readonly PreliminarySourceIdentificationTypePedestrianLitterOther Instance = new PreliminarySourceIdentificationTypePedestrianLitterOther(13, @"PedestrianLitterOther", @"Pedestrian Litter (Other)", 3);
    }

    public partial class PreliminarySourceIdentificationTypeIllegalDumpingOnLand : PreliminarySourceIdentificationType
    {
        private PreliminarySourceIdentificationTypeIllegalDumpingOnLand(int preliminarySourceIdentificationTypeID, string preliminarySourceIdentificationTypeName, string preliminarySourceIdentificationTypeDisplayName, int preliminarySourceIdentificationCategoryID) : base(preliminarySourceIdentificationTypeID, preliminarySourceIdentificationTypeName, preliminarySourceIdentificationTypeDisplayName, preliminarySourceIdentificationCategoryID) {}
        public static readonly PreliminarySourceIdentificationTypeIllegalDumpingOnLand Instance = new PreliminarySourceIdentificationTypeIllegalDumpingOnLand(14, @"IllegalDumpingOnLand", @"Illegal dumping on-land", 4);
    }

    public partial class PreliminarySourceIdentificationTypeHomelessencampments : PreliminarySourceIdentificationType
    {
        private PreliminarySourceIdentificationTypeHomelessencampments(int preliminarySourceIdentificationTypeID, string preliminarySourceIdentificationTypeName, string preliminarySourceIdentificationTypeDisplayName, int preliminarySourceIdentificationCategoryID) : base(preliminarySourceIdentificationTypeID, preliminarySourceIdentificationTypeName, preliminarySourceIdentificationTypeDisplayName, preliminarySourceIdentificationCategoryID) {}
        public static readonly PreliminarySourceIdentificationTypeHomelessencampments Instance = new PreliminarySourceIdentificationTypeHomelessencampments(15, @"Homelessencampments", @"Homeless encampments", 4);
    }

    public partial class PreliminarySourceIdentificationTypeIllegalDumpingOther : PreliminarySourceIdentificationType
    {
        private PreliminarySourceIdentificationTypeIllegalDumpingOther(int preliminarySourceIdentificationTypeID, string preliminarySourceIdentificationTypeName, string preliminarySourceIdentificationTypeDisplayName, int preliminarySourceIdentificationCategoryID) : base(preliminarySourceIdentificationTypeID, preliminarySourceIdentificationTypeName, preliminarySourceIdentificationTypeDisplayName, preliminarySourceIdentificationCategoryID) {}
        public static readonly PreliminarySourceIdentificationTypeIllegalDumpingOther Instance = new PreliminarySourceIdentificationTypeIllegalDumpingOther(16, @"IllegalDumpingOther", @"Illegal Dumping (Other)", 4);
    }
}