//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePageType]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public abstract partial class NeptunePageType : IHavePrimaryKey
    {
        public static readonly NeptunePageTypeHomePage HomePage = NeptunePageTypeHomePage.Instance;
        public static readonly NeptunePageTypeAbout About = NeptunePageTypeAbout.Instance;
        public static readonly NeptunePageTypeOrganizationsList OrganizationsList = NeptunePageTypeOrganizationsList.Instance;
        public static readonly NeptunePageTypeHomeMapInfo HomeMapInfo = NeptunePageTypeHomeMapInfo.Instance;
        public static readonly NeptunePageTypeHomeAdditionalInfo HomeAdditionalInfo = NeptunePageTypeHomeAdditionalInfo.Instance;
        public static readonly NeptunePageTypeTreatmentBMP TreatmentBMP = NeptunePageTypeTreatmentBMP.Instance;
        public static readonly NeptunePageTypeTreatmentBMPType TreatmentBMPType = NeptunePageTypeTreatmentBMPType.Instance;
        public static readonly NeptunePageTypeModeledCatchment ModeledCatchment = NeptunePageTypeModeledCatchment.Instance;
        public static readonly NeptunePageTypeJurisdiction Jurisdiction = NeptunePageTypeJurisdiction.Instance;
        public static readonly NeptunePageTypeAssessment Assessment = NeptunePageTypeAssessment.Instance;
        public static readonly NeptunePageTypeManageObservationTypesList ManageObservationTypesList = NeptunePageTypeManageObservationTypesList.Instance;
        public static readonly NeptunePageTypeManageTreatmentBMPTypesList ManageTreatmentBMPTypesList = NeptunePageTypeManageTreatmentBMPTypesList.Instance;
        public static readonly NeptunePageTypeManageObservationTypeInstructions ManageObservationTypeInstructions = NeptunePageTypeManageObservationTypeInstructions.Instance;
        public static readonly NeptunePageTypeManageObservationTypeObservationInstructions ManageObservationTypeObservationInstructions = NeptunePageTypeManageObservationTypeObservationInstructions.Instance;
        public static readonly NeptunePageTypeManageObservationTypeLabelsAndUnitsInstructions ManageObservationTypeLabelsAndUnitsInstructions = NeptunePageTypeManageObservationTypeLabelsAndUnitsInstructions.Instance;
        public static readonly NeptunePageTypeManageTreatmentBMPTypeInstructions ManageTreatmentBMPTypeInstructions = NeptunePageTypeManageTreatmentBMPTypeInstructions.Instance;
        public static readonly NeptunePageTypeManageTreatmentBMPAttributeTypeInstructions ManageTreatmentBMPAttributeTypeInstructions = NeptunePageTypeManageTreatmentBMPAttributeTypeInstructions.Instance;
        public static readonly NeptunePageTypeManageTreatmentBMPAttributeInstructions ManageTreatmentBMPAttributeInstructions = NeptunePageTypeManageTreatmentBMPAttributeInstructions.Instance;
        public static readonly NeptunePageTypeManageTreatmentBMPAttributeTypesList ManageTreatmentBMPAttributeTypesList = NeptunePageTypeManageTreatmentBMPAttributeTypesList.Instance;
        public static readonly NeptunePageTypeLegal Legal = NeptunePageTypeLegal.Instance;

        public static readonly List<NeptunePageType> All;
        public static readonly ReadOnlyDictionary<int, NeptunePageType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static NeptunePageType()
        {
            All = new List<NeptunePageType> { HomePage, About, OrganizationsList, HomeMapInfo, HomeAdditionalInfo, TreatmentBMP, TreatmentBMPType, ModeledCatchment, Jurisdiction, Assessment, ManageObservationTypesList, ManageTreatmentBMPTypesList, ManageObservationTypeInstructions, ManageObservationTypeObservationInstructions, ManageObservationTypeLabelsAndUnitsInstructions, ManageTreatmentBMPTypeInstructions, ManageTreatmentBMPAttributeTypeInstructions, ManageTreatmentBMPAttributeInstructions, ManageTreatmentBMPAttributeTypesList, Legal };
            AllLookupDictionary = new ReadOnlyDictionary<int, NeptunePageType>(All.ToDictionary(x => x.NeptunePageTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected NeptunePageType(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID)
        {
            NeptunePageTypeID = neptunePageTypeID;
            NeptunePageTypeName = neptunePageTypeName;
            NeptunePageTypeDisplayName = neptunePageTypeDisplayName;
            NeptunePageRenderTypeID = neptunePageRenderTypeID;
        }
        public NeptunePageRenderType NeptunePageRenderType { get { return NeptunePageRenderType.AllLookupDictionary[NeptunePageRenderTypeID]; } }
        [Key]
        public int NeptunePageTypeID { get; private set; }
        public string NeptunePageTypeName { get; private set; }
        public string NeptunePageTypeDisplayName { get; private set; }
        public int NeptunePageRenderTypeID { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return NeptunePageTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(NeptunePageType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.NeptunePageTypeID == NeptunePageTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as NeptunePageType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return NeptunePageTypeID;
        }

        public static bool operator ==(NeptunePageType left, NeptunePageType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(NeptunePageType left, NeptunePageType right)
        {
            return !Equals(left, right);
        }

        public NeptunePageTypeEnum ToEnum { get { return (NeptunePageTypeEnum)GetHashCode(); } }

        public static NeptunePageType ToType(int enumValue)
        {
            return ToType((NeptunePageTypeEnum)enumValue);
        }

        public static NeptunePageType ToType(NeptunePageTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case NeptunePageTypeEnum.About:
                    return About;
                case NeptunePageTypeEnum.Assessment:
                    return Assessment;
                case NeptunePageTypeEnum.HomeAdditionalInfo:
                    return HomeAdditionalInfo;
                case NeptunePageTypeEnum.HomeMapInfo:
                    return HomeMapInfo;
                case NeptunePageTypeEnum.HomePage:
                    return HomePage;
                case NeptunePageTypeEnum.Jurisdiction:
                    return Jurisdiction;
                case NeptunePageTypeEnum.Legal:
                    return Legal;
                case NeptunePageTypeEnum.ManageObservationTypeInstructions:
                    return ManageObservationTypeInstructions;
                case NeptunePageTypeEnum.ManageObservationTypeLabelsAndUnitsInstructions:
                    return ManageObservationTypeLabelsAndUnitsInstructions;
                case NeptunePageTypeEnum.ManageObservationTypeObservationInstructions:
                    return ManageObservationTypeObservationInstructions;
                case NeptunePageTypeEnum.ManageObservationTypesList:
                    return ManageObservationTypesList;
                case NeptunePageTypeEnum.ManageTreatmentBMPAttributeInstructions:
                    return ManageTreatmentBMPAttributeInstructions;
                case NeptunePageTypeEnum.ManageTreatmentBMPAttributeTypeInstructions:
                    return ManageTreatmentBMPAttributeTypeInstructions;
                case NeptunePageTypeEnum.ManageTreatmentBMPAttributeTypesList:
                    return ManageTreatmentBMPAttributeTypesList;
                case NeptunePageTypeEnum.ManageTreatmentBMPTypeInstructions:
                    return ManageTreatmentBMPTypeInstructions;
                case NeptunePageTypeEnum.ManageTreatmentBMPTypesList:
                    return ManageTreatmentBMPTypesList;
                case NeptunePageTypeEnum.ModeledCatchment:
                    return ModeledCatchment;
                case NeptunePageTypeEnum.OrganizationsList:
                    return OrganizationsList;
                case NeptunePageTypeEnum.TreatmentBMP:
                    return TreatmentBMP;
                case NeptunePageTypeEnum.TreatmentBMPType:
                    return TreatmentBMPType;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum NeptunePageTypeEnum
    {
        HomePage = 1,
        About = 2,
        OrganizationsList = 3,
        HomeMapInfo = 4,
        HomeAdditionalInfo = 5,
        TreatmentBMP = 6,
        TreatmentBMPType = 7,
        ModeledCatchment = 8,
        Jurisdiction = 9,
        Assessment = 10,
        ManageObservationTypesList = 11,
        ManageTreatmentBMPTypesList = 12,
        ManageObservationTypeInstructions = 13,
        ManageObservationTypeObservationInstructions = 14,
        ManageObservationTypeLabelsAndUnitsInstructions = 15,
        ManageTreatmentBMPTypeInstructions = 16,
        ManageTreatmentBMPAttributeTypeInstructions = 17,
        ManageTreatmentBMPAttributeInstructions = 18,
        ManageTreatmentBMPAttributeTypesList = 19,
        Legal = 20
    }

    public partial class NeptunePageTypeHomePage : NeptunePageType
    {
        private NeptunePageTypeHomePage(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeHomePage Instance = new NeptunePageTypeHomePage(1, @"HomePage", @"Home Page", 2);
    }

    public partial class NeptunePageTypeAbout : NeptunePageType
    {
        private NeptunePageTypeAbout(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeAbout Instance = new NeptunePageTypeAbout(2, @"About", @"About", 2);
    }

    public partial class NeptunePageTypeOrganizationsList : NeptunePageType
    {
        private NeptunePageTypeOrganizationsList(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeOrganizationsList Instance = new NeptunePageTypeOrganizationsList(3, @"OrganizationsList", @"Organizations List", 1);
    }

    public partial class NeptunePageTypeHomeMapInfo : NeptunePageType
    {
        private NeptunePageTypeHomeMapInfo(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeHomeMapInfo Instance = new NeptunePageTypeHomeMapInfo(4, @"HomeMapInfo", @"Home Page Map Info", 2);
    }

    public partial class NeptunePageTypeHomeAdditionalInfo : NeptunePageType
    {
        private NeptunePageTypeHomeAdditionalInfo(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeHomeAdditionalInfo Instance = new NeptunePageTypeHomeAdditionalInfo(5, @"HomeAdditionalInfo", @"Home Page Additional Info", 2);
    }

    public partial class NeptunePageTypeTreatmentBMP : NeptunePageType
    {
        private NeptunePageTypeTreatmentBMP(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeTreatmentBMP Instance = new NeptunePageTypeTreatmentBMP(6, @"TreatmentBMP", @"Treatment BMP", 2);
    }

    public partial class NeptunePageTypeTreatmentBMPType : NeptunePageType
    {
        private NeptunePageTypeTreatmentBMPType(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeTreatmentBMPType Instance = new NeptunePageTypeTreatmentBMPType(7, @"TreatmentBMPType", @"Treatment BMP Type", 2);
    }

    public partial class NeptunePageTypeModeledCatchment : NeptunePageType
    {
        private NeptunePageTypeModeledCatchment(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeModeledCatchment Instance = new NeptunePageTypeModeledCatchment(8, @"ModeledCatchment", @"Modeled Catchment", 2);
    }

    public partial class NeptunePageTypeJurisdiction : NeptunePageType
    {
        private NeptunePageTypeJurisdiction(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeJurisdiction Instance = new NeptunePageTypeJurisdiction(9, @"Jurisdiction", @"Jurisdiction", 2);
    }

    public partial class NeptunePageTypeAssessment : NeptunePageType
    {
        private NeptunePageTypeAssessment(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeAssessment Instance = new NeptunePageTypeAssessment(10, @"Assessment", @"Assessment", 2);
    }

    public partial class NeptunePageTypeManageObservationTypesList : NeptunePageType
    {
        private NeptunePageTypeManageObservationTypesList(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeManageObservationTypesList Instance = new NeptunePageTypeManageObservationTypesList(11, @"ManageObservationTypesList", @"Manage Observation Types List", 2);
    }

    public partial class NeptunePageTypeManageTreatmentBMPTypesList : NeptunePageType
    {
        private NeptunePageTypeManageTreatmentBMPTypesList(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeManageTreatmentBMPTypesList Instance = new NeptunePageTypeManageTreatmentBMPTypesList(12, @"ManageTreatmentBMPTypesList", @"Manage Treatment BMP Types List", 2);
    }

    public partial class NeptunePageTypeManageObservationTypeInstructions : NeptunePageType
    {
        private NeptunePageTypeManageObservationTypeInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeManageObservationTypeInstructions Instance = new NeptunePageTypeManageObservationTypeInstructions(13, @"ManageObservationTypeInstructions", @"Manage Observation Type Instructions", 2);
    }

    public partial class NeptunePageTypeManageObservationTypeObservationInstructions : NeptunePageType
    {
        private NeptunePageTypeManageObservationTypeObservationInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeManageObservationTypeObservationInstructions Instance = new NeptunePageTypeManageObservationTypeObservationInstructions(14, @"ManageObservationTypeObservationInstructions", @"Manage Observation Type Instructions for Observation Instructions", 2);
    }

    public partial class NeptunePageTypeManageObservationTypeLabelsAndUnitsInstructions : NeptunePageType
    {
        private NeptunePageTypeManageObservationTypeLabelsAndUnitsInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeManageObservationTypeLabelsAndUnitsInstructions Instance = new NeptunePageTypeManageObservationTypeLabelsAndUnitsInstructions(15, @"ManageObservationTypeLabelsAndUnitsInstructions", @"Manage Observation Type Labels and Units Instructions", 2);
    }

    public partial class NeptunePageTypeManageTreatmentBMPTypeInstructions : NeptunePageType
    {
        private NeptunePageTypeManageTreatmentBMPTypeInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeManageTreatmentBMPTypeInstructions Instance = new NeptunePageTypeManageTreatmentBMPTypeInstructions(16, @"ManageTreatmentBMPTypeInstructions", @"Manage Treatment BMP Type Instructions", 2);
    }

    public partial class NeptunePageTypeManageTreatmentBMPAttributeTypeInstructions : NeptunePageType
    {
        private NeptunePageTypeManageTreatmentBMPAttributeTypeInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeManageTreatmentBMPAttributeTypeInstructions Instance = new NeptunePageTypeManageTreatmentBMPAttributeTypeInstructions(17, @"ManageTreatmentBMPAttributeTypeInstructions", @"Manage Treatment BMP Attribute Type Instructions", 2);
    }

    public partial class NeptunePageTypeManageTreatmentBMPAttributeInstructions : NeptunePageType
    {
        private NeptunePageTypeManageTreatmentBMPAttributeInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeManageTreatmentBMPAttributeInstructions Instance = new NeptunePageTypeManageTreatmentBMPAttributeInstructions(18, @"ManageTreatmentBMPAttributeInstructions", @"Manage Treatment BMP Attribute Instructions", 2);
    }

    public partial class NeptunePageTypeManageTreatmentBMPAttributeTypesList : NeptunePageType
    {
        private NeptunePageTypeManageTreatmentBMPAttributeTypesList(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeManageTreatmentBMPAttributeTypesList Instance = new NeptunePageTypeManageTreatmentBMPAttributeTypesList(19, @"ManageTreatmentBMPAttributeTypesList", @"Manage Treatment BMP Attribute Types List", 2);
    }

    public partial class NeptunePageTypeLegal : NeptunePageType
    {
        private NeptunePageTypeLegal(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeLegal Instance = new NeptunePageTypeLegal(20, @"Legal", @"Legal", 2);
    }
}