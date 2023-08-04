//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HRUCharacteristicLandUseCode]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Neptune.Models.DataTransferObjects;


namespace Neptune.EFModels.Entities
{
    public abstract partial class HRUCharacteristicLandUseCode
    {
        public static readonly HRUCharacteristicLandUseCodeCOMM COMM = Neptune.EFModels.Entities.HRUCharacteristicLandUseCodeCOMM.Instance;
        public static readonly HRUCharacteristicLandUseCodeEDU EDU = Neptune.EFModels.Entities.HRUCharacteristicLandUseCodeEDU.Instance;
        public static readonly HRUCharacteristicLandUseCodeIND IND = Neptune.EFModels.Entities.HRUCharacteristicLandUseCodeIND.Instance;
        public static readonly HRUCharacteristicLandUseCodeUTIL UTIL = Neptune.EFModels.Entities.HRUCharacteristicLandUseCodeUTIL.Instance;
        public static readonly HRUCharacteristicLandUseCodeRESSFH RESSFH = Neptune.EFModels.Entities.HRUCharacteristicLandUseCodeRESSFH.Instance;
        public static readonly HRUCharacteristicLandUseCodeRESSFL RESSFL = Neptune.EFModels.Entities.HRUCharacteristicLandUseCodeRESSFL.Instance;
        public static readonly HRUCharacteristicLandUseCodeRESMF RESMF = Neptune.EFModels.Entities.HRUCharacteristicLandUseCodeRESMF.Instance;
        public static readonly HRUCharacteristicLandUseCodeTRFWY TRFWY = Neptune.EFModels.Entities.HRUCharacteristicLandUseCodeTRFWY.Instance;
        public static readonly HRUCharacteristicLandUseCodeTRANS TRANS = Neptune.EFModels.Entities.HRUCharacteristicLandUseCodeTRANS.Instance;
        public static readonly HRUCharacteristicLandUseCodeTROTH TROTH = Neptune.EFModels.Entities.HRUCharacteristicLandUseCodeTROTH.Instance;
        public static readonly HRUCharacteristicLandUseCodeOSAGIR OSAGIR = Neptune.EFModels.Entities.HRUCharacteristicLandUseCodeOSAGIR.Instance;
        public static readonly HRUCharacteristicLandUseCodeOSAGNI OSAGNI = Neptune.EFModels.Entities.HRUCharacteristicLandUseCodeOSAGNI.Instance;
        public static readonly HRUCharacteristicLandUseCodeOSDEV OSDEV = Neptune.EFModels.Entities.HRUCharacteristicLandUseCodeOSDEV.Instance;
        public static readonly HRUCharacteristicLandUseCodeOSIRR OSIRR = Neptune.EFModels.Entities.HRUCharacteristicLandUseCodeOSIRR.Instance;
        public static readonly HRUCharacteristicLandUseCodeOSLOW OSLOW = Neptune.EFModels.Entities.HRUCharacteristicLandUseCodeOSLOW.Instance;
        public static readonly HRUCharacteristicLandUseCodeOSFOR OSFOR = Neptune.EFModels.Entities.HRUCharacteristicLandUseCodeOSFOR.Instance;
        public static readonly HRUCharacteristicLandUseCodeOSWET OSWET = Neptune.EFModels.Entities.HRUCharacteristicLandUseCodeOSWET.Instance;
        public static readonly HRUCharacteristicLandUseCodeOSVAC OSVAC = Neptune.EFModels.Entities.HRUCharacteristicLandUseCodeOSVAC.Instance;
        public static readonly HRUCharacteristicLandUseCodeWATER WATER = Neptune.EFModels.Entities.HRUCharacteristicLandUseCodeWATER.Instance;

        public static readonly List<HRUCharacteristicLandUseCode> All;
        public static readonly List<HRUCharacteristicLandUseCodeDto> AllAsDto;
        public static readonly ReadOnlyDictionary<int, HRUCharacteristicLandUseCode> AllLookupDictionary;
        public static readonly ReadOnlyDictionary<int, HRUCharacteristicLandUseCodeDto> AllAsDtoLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static HRUCharacteristicLandUseCode()
        {
            All = new List<HRUCharacteristicLandUseCode> { COMM, EDU, IND, UTIL, RESSFH, RESSFL, RESMF, TRFWY, TRANS, TROTH, OSAGIR, OSAGNI, OSDEV, OSIRR, OSLOW, OSFOR, OSWET, OSVAC, WATER };
            AllAsDto = new List<HRUCharacteristicLandUseCodeDto> { COMM.AsDto(), EDU.AsDto(), IND.AsDto(), UTIL.AsDto(), RESSFH.AsDto(), RESSFL.AsDto(), RESMF.AsDto(), TRFWY.AsDto(), TRANS.AsDto(), TROTH.AsDto(), OSAGIR.AsDto(), OSAGNI.AsDto(), OSDEV.AsDto(), OSIRR.AsDto(), OSLOW.AsDto(), OSFOR.AsDto(), OSWET.AsDto(), OSVAC.AsDto(), WATER.AsDto() };
            AllLookupDictionary = new ReadOnlyDictionary<int, HRUCharacteristicLandUseCode>(All.ToDictionary(x => x.HRUCharacteristicLandUseCodeID));
            AllAsDtoLookupDictionary = new ReadOnlyDictionary<int, HRUCharacteristicLandUseCodeDto>(AllAsDto.ToDictionary(x => x.HRUCharacteristicLandUseCodeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected HRUCharacteristicLandUseCode(int hRUCharacteristicLandUseCodeID, string hRUCharacteristicLandUseCodeName, string hRUCharacteristicLandUseCodeDisplayName)
        {
            HRUCharacteristicLandUseCodeID = hRUCharacteristicLandUseCodeID;
            HRUCharacteristicLandUseCodeName = hRUCharacteristicLandUseCodeName;
            HRUCharacteristicLandUseCodeDisplayName = hRUCharacteristicLandUseCodeDisplayName;
        }

        [Key]
        public int HRUCharacteristicLandUseCodeID { get; private set; }
        public string HRUCharacteristicLandUseCodeName { get; private set; }
        public string HRUCharacteristicLandUseCodeDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return HRUCharacteristicLandUseCodeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(HRUCharacteristicLandUseCode other)
        {
            if (other == null)
            {
                return false;
            }
            return other.HRUCharacteristicLandUseCodeID == HRUCharacteristicLandUseCodeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as HRUCharacteristicLandUseCode);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return HRUCharacteristicLandUseCodeID;
        }

        public static bool operator ==(HRUCharacteristicLandUseCode left, HRUCharacteristicLandUseCode right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(HRUCharacteristicLandUseCode left, HRUCharacteristicLandUseCode right)
        {
            return !Equals(left, right);
        }

        public HRUCharacteristicLandUseCodeEnum ToEnum => (HRUCharacteristicLandUseCodeEnum)GetHashCode();

        public static HRUCharacteristicLandUseCode ToType(int enumValue)
        {
            return ToType((HRUCharacteristicLandUseCodeEnum)enumValue);
        }

        public static HRUCharacteristicLandUseCode ToType(HRUCharacteristicLandUseCodeEnum enumValue)
        {
            switch (enumValue)
            {
                case HRUCharacteristicLandUseCodeEnum.COMM:
                    return COMM;
                case HRUCharacteristicLandUseCodeEnum.EDU:
                    return EDU;
                case HRUCharacteristicLandUseCodeEnum.IND:
                    return IND;
                case HRUCharacteristicLandUseCodeEnum.OSAGIR:
                    return OSAGIR;
                case HRUCharacteristicLandUseCodeEnum.OSAGNI:
                    return OSAGNI;
                case HRUCharacteristicLandUseCodeEnum.OSDEV:
                    return OSDEV;
                case HRUCharacteristicLandUseCodeEnum.OSFOR:
                    return OSFOR;
                case HRUCharacteristicLandUseCodeEnum.OSIRR:
                    return OSIRR;
                case HRUCharacteristicLandUseCodeEnum.OSLOW:
                    return OSLOW;
                case HRUCharacteristicLandUseCodeEnum.OSVAC:
                    return OSVAC;
                case HRUCharacteristicLandUseCodeEnum.OSWET:
                    return OSWET;
                case HRUCharacteristicLandUseCodeEnum.RESMF:
                    return RESMF;
                case HRUCharacteristicLandUseCodeEnum.RESSFH:
                    return RESSFH;
                case HRUCharacteristicLandUseCodeEnum.RESSFL:
                    return RESSFL;
                case HRUCharacteristicLandUseCodeEnum.TRANS:
                    return TRANS;
                case HRUCharacteristicLandUseCodeEnum.TRFWY:
                    return TRFWY;
                case HRUCharacteristicLandUseCodeEnum.TROTH:
                    return TROTH;
                case HRUCharacteristicLandUseCodeEnum.UTIL:
                    return UTIL;
                case HRUCharacteristicLandUseCodeEnum.WATER:
                    return WATER;
                default:
                    throw new ArgumentException("Unable to map Enum: {enumValue}");
            }
        }
    }

    public enum HRUCharacteristicLandUseCodeEnum
    {
        COMM = 1,
        EDU = 2,
        IND = 3,
        UTIL = 4,
        RESSFH = 5,
        RESSFL = 6,
        RESMF = 7,
        TRFWY = 8,
        TRANS = 9,
        TROTH = 10,
        OSAGIR = 11,
        OSAGNI = 12,
        OSDEV = 13,
        OSIRR = 14,
        OSLOW = 15,
        OSFOR = 16,
        OSWET = 17,
        OSVAC = 18,
        WATER = 19
    }

    public partial class HRUCharacteristicLandUseCodeCOMM : HRUCharacteristicLandUseCode
    {
        private HRUCharacteristicLandUseCodeCOMM(int hRUCharacteristicLandUseCodeID, string hRUCharacteristicLandUseCodeName, string hRUCharacteristicLandUseCodeDisplayName) : base(hRUCharacteristicLandUseCodeID, hRUCharacteristicLandUseCodeName, hRUCharacteristicLandUseCodeDisplayName) {}
        public static readonly HRUCharacteristicLandUseCodeCOMM Instance = new HRUCharacteristicLandUseCodeCOMM(1, @"COMM", @"Commercial");
    }

    public partial class HRUCharacteristicLandUseCodeEDU : HRUCharacteristicLandUseCode
    {
        private HRUCharacteristicLandUseCodeEDU(int hRUCharacteristicLandUseCodeID, string hRUCharacteristicLandUseCodeName, string hRUCharacteristicLandUseCodeDisplayName) : base(hRUCharacteristicLandUseCodeID, hRUCharacteristicLandUseCodeName, hRUCharacteristicLandUseCodeDisplayName) {}
        public static readonly HRUCharacteristicLandUseCodeEDU Instance = new HRUCharacteristicLandUseCodeEDU(2, @"EDU", @"Education");
    }

    public partial class HRUCharacteristicLandUseCodeIND : HRUCharacteristicLandUseCode
    {
        private HRUCharacteristicLandUseCodeIND(int hRUCharacteristicLandUseCodeID, string hRUCharacteristicLandUseCodeName, string hRUCharacteristicLandUseCodeDisplayName) : base(hRUCharacteristicLandUseCodeID, hRUCharacteristicLandUseCodeName, hRUCharacteristicLandUseCodeDisplayName) {}
        public static readonly HRUCharacteristicLandUseCodeIND Instance = new HRUCharacteristicLandUseCodeIND(3, @"IND", @"Industrial");
    }

    public partial class HRUCharacteristicLandUseCodeUTIL : HRUCharacteristicLandUseCode
    {
        private HRUCharacteristicLandUseCodeUTIL(int hRUCharacteristicLandUseCodeID, string hRUCharacteristicLandUseCodeName, string hRUCharacteristicLandUseCodeDisplayName) : base(hRUCharacteristicLandUseCodeID, hRUCharacteristicLandUseCodeName, hRUCharacteristicLandUseCodeDisplayName) {}
        public static readonly HRUCharacteristicLandUseCodeUTIL Instance = new HRUCharacteristicLandUseCodeUTIL(4, @"UTIL", @"Utility");
    }

    public partial class HRUCharacteristicLandUseCodeRESSFH : HRUCharacteristicLandUseCode
    {
        private HRUCharacteristicLandUseCodeRESSFH(int hRUCharacteristicLandUseCodeID, string hRUCharacteristicLandUseCodeName, string hRUCharacteristicLandUseCodeDisplayName) : base(hRUCharacteristicLandUseCodeID, hRUCharacteristicLandUseCodeName, hRUCharacteristicLandUseCodeDisplayName) {}
        public static readonly HRUCharacteristicLandUseCodeRESSFH Instance = new HRUCharacteristicLandUseCodeRESSFH(5, @"RESSFH", @"Residential - Single Family High Density");
    }

    public partial class HRUCharacteristicLandUseCodeRESSFL : HRUCharacteristicLandUseCode
    {
        private HRUCharacteristicLandUseCodeRESSFL(int hRUCharacteristicLandUseCodeID, string hRUCharacteristicLandUseCodeName, string hRUCharacteristicLandUseCodeDisplayName) : base(hRUCharacteristicLandUseCodeID, hRUCharacteristicLandUseCodeName, hRUCharacteristicLandUseCodeDisplayName) {}
        public static readonly HRUCharacteristicLandUseCodeRESSFL Instance = new HRUCharacteristicLandUseCodeRESSFL(6, @"RESSFL", @"Residential - Single Family Low Density");
    }

    public partial class HRUCharacteristicLandUseCodeRESMF : HRUCharacteristicLandUseCode
    {
        private HRUCharacteristicLandUseCodeRESMF(int hRUCharacteristicLandUseCodeID, string hRUCharacteristicLandUseCodeName, string hRUCharacteristicLandUseCodeDisplayName) : base(hRUCharacteristicLandUseCodeID, hRUCharacteristicLandUseCodeName, hRUCharacteristicLandUseCodeDisplayName) {}
        public static readonly HRUCharacteristicLandUseCodeRESMF Instance = new HRUCharacteristicLandUseCodeRESMF(7, @"RESMF", @"Residential - MultiFamily");
    }

    public partial class HRUCharacteristicLandUseCodeTRFWY : HRUCharacteristicLandUseCode
    {
        private HRUCharacteristicLandUseCodeTRFWY(int hRUCharacteristicLandUseCodeID, string hRUCharacteristicLandUseCodeName, string hRUCharacteristicLandUseCodeDisplayName) : base(hRUCharacteristicLandUseCodeID, hRUCharacteristicLandUseCodeName, hRUCharacteristicLandUseCodeDisplayName) {}
        public static readonly HRUCharacteristicLandUseCodeTRFWY Instance = new HRUCharacteristicLandUseCodeTRFWY(8, @"TRFWY", @"Transportation - Freeway");
    }

    public partial class HRUCharacteristicLandUseCodeTRANS : HRUCharacteristicLandUseCode
    {
        private HRUCharacteristicLandUseCodeTRANS(int hRUCharacteristicLandUseCodeID, string hRUCharacteristicLandUseCodeName, string hRUCharacteristicLandUseCodeDisplayName) : base(hRUCharacteristicLandUseCodeID, hRUCharacteristicLandUseCodeName, hRUCharacteristicLandUseCodeDisplayName) {}
        public static readonly HRUCharacteristicLandUseCodeTRANS Instance = new HRUCharacteristicLandUseCodeTRANS(9, @"TRANS", @"Transportation - Local Road");
    }

    public partial class HRUCharacteristicLandUseCodeTROTH : HRUCharacteristicLandUseCode
    {
        private HRUCharacteristicLandUseCodeTROTH(int hRUCharacteristicLandUseCodeID, string hRUCharacteristicLandUseCodeName, string hRUCharacteristicLandUseCodeDisplayName) : base(hRUCharacteristicLandUseCodeID, hRUCharacteristicLandUseCodeName, hRUCharacteristicLandUseCodeDisplayName) {}
        public static readonly HRUCharacteristicLandUseCodeTROTH Instance = new HRUCharacteristicLandUseCodeTROTH(10, @"TROTH", @"Transportation - Other");
    }

    public partial class HRUCharacteristicLandUseCodeOSAGIR : HRUCharacteristicLandUseCode
    {
        private HRUCharacteristicLandUseCodeOSAGIR(int hRUCharacteristicLandUseCodeID, string hRUCharacteristicLandUseCodeName, string hRUCharacteristicLandUseCodeDisplayName) : base(hRUCharacteristicLandUseCodeID, hRUCharacteristicLandUseCodeName, hRUCharacteristicLandUseCodeDisplayName) {}
        public static readonly HRUCharacteristicLandUseCodeOSAGIR Instance = new HRUCharacteristicLandUseCodeOSAGIR(11, @"OSAGIR", @"Open Space - Irrigated Agriculture");
    }

    public partial class HRUCharacteristicLandUseCodeOSAGNI : HRUCharacteristicLandUseCode
    {
        private HRUCharacteristicLandUseCodeOSAGNI(int hRUCharacteristicLandUseCodeID, string hRUCharacteristicLandUseCodeName, string hRUCharacteristicLandUseCodeDisplayName) : base(hRUCharacteristicLandUseCodeID, hRUCharacteristicLandUseCodeName, hRUCharacteristicLandUseCodeDisplayName) {}
        public static readonly HRUCharacteristicLandUseCodeOSAGNI Instance = new HRUCharacteristicLandUseCodeOSAGNI(12, @"OSAGNI", @"Open Space - Non-Irrigated Agriculture");
    }

    public partial class HRUCharacteristicLandUseCodeOSDEV : HRUCharacteristicLandUseCode
    {
        private HRUCharacteristicLandUseCodeOSDEV(int hRUCharacteristicLandUseCodeID, string hRUCharacteristicLandUseCodeName, string hRUCharacteristicLandUseCodeDisplayName) : base(hRUCharacteristicLandUseCodeID, hRUCharacteristicLandUseCodeName, hRUCharacteristicLandUseCodeDisplayName) {}
        public static readonly HRUCharacteristicLandUseCodeOSDEV Instance = new HRUCharacteristicLandUseCodeOSDEV(13, @"OSDEV", @"Open Space - Low Density Development");
    }

    public partial class HRUCharacteristicLandUseCodeOSIRR : HRUCharacteristicLandUseCode
    {
        private HRUCharacteristicLandUseCodeOSIRR(int hRUCharacteristicLandUseCodeID, string hRUCharacteristicLandUseCodeName, string hRUCharacteristicLandUseCodeDisplayName) : base(hRUCharacteristicLandUseCodeID, hRUCharacteristicLandUseCodeName, hRUCharacteristicLandUseCodeDisplayName) {}
        public static readonly HRUCharacteristicLandUseCodeOSIRR Instance = new HRUCharacteristicLandUseCodeOSIRR(14, @"OSIRR", @"Open Space - Irrigated Recreation");
    }

    public partial class HRUCharacteristicLandUseCodeOSLOW : HRUCharacteristicLandUseCode
    {
        private HRUCharacteristicLandUseCodeOSLOW(int hRUCharacteristicLandUseCodeID, string hRUCharacteristicLandUseCodeName, string hRUCharacteristicLandUseCodeDisplayName) : base(hRUCharacteristicLandUseCodeID, hRUCharacteristicLandUseCodeName, hRUCharacteristicLandUseCodeDisplayName) {}
        public static readonly HRUCharacteristicLandUseCodeOSLOW Instance = new HRUCharacteristicLandUseCodeOSLOW(15, @"OSLOW", @"Open Space - Low Canopy Vegetation");
    }

    public partial class HRUCharacteristicLandUseCodeOSFOR : HRUCharacteristicLandUseCode
    {
        private HRUCharacteristicLandUseCodeOSFOR(int hRUCharacteristicLandUseCodeID, string hRUCharacteristicLandUseCodeName, string hRUCharacteristicLandUseCodeDisplayName) : base(hRUCharacteristicLandUseCodeID, hRUCharacteristicLandUseCodeName, hRUCharacteristicLandUseCodeDisplayName) {}
        public static readonly HRUCharacteristicLandUseCodeOSFOR Instance = new HRUCharacteristicLandUseCodeOSFOR(16, @"OSFOR", @"Open Space - Forest");
    }

    public partial class HRUCharacteristicLandUseCodeOSWET : HRUCharacteristicLandUseCode
    {
        private HRUCharacteristicLandUseCodeOSWET(int hRUCharacteristicLandUseCodeID, string hRUCharacteristicLandUseCodeName, string hRUCharacteristicLandUseCodeDisplayName) : base(hRUCharacteristicLandUseCodeID, hRUCharacteristicLandUseCodeName, hRUCharacteristicLandUseCodeDisplayName) {}
        public static readonly HRUCharacteristicLandUseCodeOSWET Instance = new HRUCharacteristicLandUseCodeOSWET(17, @"OSWET", @"Open Space - Wetlands");
    }

    public partial class HRUCharacteristicLandUseCodeOSVAC : HRUCharacteristicLandUseCode
    {
        private HRUCharacteristicLandUseCodeOSVAC(int hRUCharacteristicLandUseCodeID, string hRUCharacteristicLandUseCodeName, string hRUCharacteristicLandUseCodeDisplayName) : base(hRUCharacteristicLandUseCodeID, hRUCharacteristicLandUseCodeName, hRUCharacteristicLandUseCodeDisplayName) {}
        public static readonly HRUCharacteristicLandUseCodeOSVAC Instance = new HRUCharacteristicLandUseCodeOSVAC(18, @"OSVAC", @"Open Space - Vacant Land");
    }

    public partial class HRUCharacteristicLandUseCodeWATER : HRUCharacteristicLandUseCode
    {
        private HRUCharacteristicLandUseCodeWATER(int hRUCharacteristicLandUseCodeID, string hRUCharacteristicLandUseCodeName, string hRUCharacteristicLandUseCodeDisplayName) : base(hRUCharacteristicLandUseCodeID, hRUCharacteristicLandUseCodeName, hRUCharacteristicLandUseCodeDisplayName) {}
        public static readonly HRUCharacteristicLandUseCodeWATER Instance = new HRUCharacteristicLandUseCodeWATER(19, @"WATER", @"Water");
    }
}