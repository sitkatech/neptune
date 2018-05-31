//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterBreadCrumbEntity]
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
    public abstract partial class StormwaterBreadCrumbEntity : IHavePrimaryKey
    {
        public static readonly StormwaterBreadCrumbEntityTreatmentBMP TreatmentBMP = StormwaterBreadCrumbEntityTreatmentBMP.Instance;
        public static readonly StormwaterBreadCrumbEntityModeledCatchment ModeledCatchment = StormwaterBreadCrumbEntityModeledCatchment.Instance;
        public static readonly StormwaterBreadCrumbEntityJurisdiction Jurisdiction = StormwaterBreadCrumbEntityJurisdiction.Instance;
        public static readonly StormwaterBreadCrumbEntityUsers Users = StormwaterBreadCrumbEntityUsers.Instance;
        public static readonly StormwaterBreadCrumbEntityAssessments Assessments = StormwaterBreadCrumbEntityAssessments.Instance;
        public static readonly StormwaterBreadCrumbEntityFieldVisits FieldVisits = StormwaterBreadCrumbEntityFieldVisits.Instance;
        public static readonly StormwaterBreadCrumbEntityFieldRecords FieldRecords = StormwaterBreadCrumbEntityFieldRecords.Instance;

        public static readonly List<StormwaterBreadCrumbEntity> All;
        public static readonly ReadOnlyDictionary<int, StormwaterBreadCrumbEntity> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static StormwaterBreadCrumbEntity()
        {
            All = new List<StormwaterBreadCrumbEntity> { TreatmentBMP, ModeledCatchment, Jurisdiction, Users, Assessments, FieldVisits, FieldRecords };
            AllLookupDictionary = new ReadOnlyDictionary<int, StormwaterBreadCrumbEntity>(All.ToDictionary(x => x.StormwaterBreadCrumbEntityID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected StormwaterBreadCrumbEntity(int stormwaterBreadCrumbEntityID, string stormwaterBreadCrumbEntityName, string stormwaterBreadCrumbEntityDisplayName, string glyphIconClass, string colorClass)
        {
            StormwaterBreadCrumbEntityID = stormwaterBreadCrumbEntityID;
            StormwaterBreadCrumbEntityName = stormwaterBreadCrumbEntityName;
            StormwaterBreadCrumbEntityDisplayName = stormwaterBreadCrumbEntityDisplayName;
            GlyphIconClass = glyphIconClass;
            ColorClass = colorClass;
        }

        [Key]
        public int StormwaterBreadCrumbEntityID { get; private set; }
        public string StormwaterBreadCrumbEntityName { get; private set; }
        public string StormwaterBreadCrumbEntityDisplayName { get; private set; }
        public string GlyphIconClass { get; private set; }
        public string ColorClass { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return StormwaterBreadCrumbEntityID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(StormwaterBreadCrumbEntity other)
        {
            if (other == null)
            {
                return false;
            }
            return other.StormwaterBreadCrumbEntityID == StormwaterBreadCrumbEntityID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as StormwaterBreadCrumbEntity);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return StormwaterBreadCrumbEntityID;
        }

        public static bool operator ==(StormwaterBreadCrumbEntity left, StormwaterBreadCrumbEntity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(StormwaterBreadCrumbEntity left, StormwaterBreadCrumbEntity right)
        {
            return !Equals(left, right);
        }

        public StormwaterBreadCrumbEntityEnum ToEnum { get { return (StormwaterBreadCrumbEntityEnum)GetHashCode(); } }

        public static StormwaterBreadCrumbEntity ToType(int enumValue)
        {
            return ToType((StormwaterBreadCrumbEntityEnum)enumValue);
        }

        public static StormwaterBreadCrumbEntity ToType(StormwaterBreadCrumbEntityEnum enumValue)
        {
            switch (enumValue)
            {
                case StormwaterBreadCrumbEntityEnum.Assessments:
                    return Assessments;
                case StormwaterBreadCrumbEntityEnum.FieldRecords:
                    return FieldRecords;
                case StormwaterBreadCrumbEntityEnum.FieldVisits:
                    return FieldVisits;
                case StormwaterBreadCrumbEntityEnum.Jurisdiction:
                    return Jurisdiction;
                case StormwaterBreadCrumbEntityEnum.ModeledCatchment:
                    return ModeledCatchment;
                case StormwaterBreadCrumbEntityEnum.TreatmentBMP:
                    return TreatmentBMP;
                case StormwaterBreadCrumbEntityEnum.Users:
                    return Users;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum StormwaterBreadCrumbEntityEnum
    {
        TreatmentBMP = 1,
        ModeledCatchment = 2,
        Jurisdiction = 3,
        Users = 4,
        Assessments = 5,
        FieldVisits = 6,
        FieldRecords = 7
    }

    public partial class StormwaterBreadCrumbEntityTreatmentBMP : StormwaterBreadCrumbEntity
    {
        private StormwaterBreadCrumbEntityTreatmentBMP(int stormwaterBreadCrumbEntityID, string stormwaterBreadCrumbEntityName, string stormwaterBreadCrumbEntityDisplayName, string glyphIconClass, string colorClass) : base(stormwaterBreadCrumbEntityID, stormwaterBreadCrumbEntityName, stormwaterBreadCrumbEntityDisplayName, glyphIconClass, colorClass) {}
        public static readonly StormwaterBreadCrumbEntityTreatmentBMP Instance = new StormwaterBreadCrumbEntityTreatmentBMP(1, @"TreatmentBMP", @"Treatment BMP", @"glyphicon-leaf", @"treatmentBMPColor");
    }

    public partial class StormwaterBreadCrumbEntityModeledCatchment : StormwaterBreadCrumbEntity
    {
        private StormwaterBreadCrumbEntityModeledCatchment(int stormwaterBreadCrumbEntityID, string stormwaterBreadCrumbEntityName, string stormwaterBreadCrumbEntityDisplayName, string glyphIconClass, string colorClass) : base(stormwaterBreadCrumbEntityID, stormwaterBreadCrumbEntityName, stormwaterBreadCrumbEntityDisplayName, glyphIconClass, colorClass) {}
        public static readonly StormwaterBreadCrumbEntityModeledCatchment Instance = new StormwaterBreadCrumbEntityModeledCatchment(2, @"ModeledCatchment", @"Modeled Catchment", @"glyphicon-tint", @"modeledCatchmentColor");
    }

    public partial class StormwaterBreadCrumbEntityJurisdiction : StormwaterBreadCrumbEntity
    {
        private StormwaterBreadCrumbEntityJurisdiction(int stormwaterBreadCrumbEntityID, string stormwaterBreadCrumbEntityName, string stormwaterBreadCrumbEntityDisplayName, string glyphIconClass, string colorClass) : base(stormwaterBreadCrumbEntityID, stormwaterBreadCrumbEntityName, stormwaterBreadCrumbEntityDisplayName, glyphIconClass, colorClass) {}
        public static readonly StormwaterBreadCrumbEntityJurisdiction Instance = new StormwaterBreadCrumbEntityJurisdiction(3, @"Jurisdiction", @"Jurisdiction", @"glyphicon-home", @"jurisdictionColor");
    }

    public partial class StormwaterBreadCrumbEntityUsers : StormwaterBreadCrumbEntity
    {
        private StormwaterBreadCrumbEntityUsers(int stormwaterBreadCrumbEntityID, string stormwaterBreadCrumbEntityName, string stormwaterBreadCrumbEntityDisplayName, string glyphIconClass, string colorClass) : base(stormwaterBreadCrumbEntityID, stormwaterBreadCrumbEntityName, stormwaterBreadCrumbEntityDisplayName, glyphIconClass, colorClass) {}
        public static readonly StormwaterBreadCrumbEntityUsers Instance = new StormwaterBreadCrumbEntityUsers(4, @"Users", @"Users", @"glyphicon-user", @"userColor");
    }

    public partial class StormwaterBreadCrumbEntityAssessments : StormwaterBreadCrumbEntity
    {
        private StormwaterBreadCrumbEntityAssessments(int stormwaterBreadCrumbEntityID, string stormwaterBreadCrumbEntityName, string stormwaterBreadCrumbEntityDisplayName, string glyphIconClass, string colorClass) : base(stormwaterBreadCrumbEntityID, stormwaterBreadCrumbEntityName, stormwaterBreadCrumbEntityDisplayName, glyphIconClass, colorClass) {}
        public static readonly StormwaterBreadCrumbEntityAssessments Instance = new StormwaterBreadCrumbEntityAssessments(5, @"Assessments", @"Assessments", @"glyphicon-pencil", @"registrationColor");
    }

    public partial class StormwaterBreadCrumbEntityFieldVisits : StormwaterBreadCrumbEntity
    {
        private StormwaterBreadCrumbEntityFieldVisits(int stormwaterBreadCrumbEntityID, string stormwaterBreadCrumbEntityName, string stormwaterBreadCrumbEntityDisplayName, string glyphIconClass, string colorClass) : base(stormwaterBreadCrumbEntityID, stormwaterBreadCrumbEntityName, stormwaterBreadCrumbEntityDisplayName, glyphIconClass, colorClass) {}
        public static readonly StormwaterBreadCrumbEntityFieldVisits Instance = new StormwaterBreadCrumbEntityFieldVisits(6, @"FieldVisits", @"Field Visits", @"glyphicon-globe", @"registrationColor");
    }

    public partial class StormwaterBreadCrumbEntityFieldRecords : StormwaterBreadCrumbEntity
    {
        private StormwaterBreadCrumbEntityFieldRecords(int stormwaterBreadCrumbEntityID, string stormwaterBreadCrumbEntityName, string stormwaterBreadCrumbEntityDisplayName, string glyphIconClass, string colorClass) : base(stormwaterBreadCrumbEntityID, stormwaterBreadCrumbEntityName, stormwaterBreadCrumbEntityDisplayName, glyphIconClass, colorClass) {}
        public static readonly StormwaterBreadCrumbEntityFieldRecords Instance = new StormwaterBreadCrumbEntityFieldRecords(7, @"FieldRecords", @"Field Records", @"glyphicon-globe", @"registrationColor");
    }
}