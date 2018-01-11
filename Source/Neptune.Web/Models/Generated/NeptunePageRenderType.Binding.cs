//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePageRenderType]
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
    public abstract partial class NeptunePageRenderType : IHavePrimaryKey
    {
        public static readonly NeptunePageRenderTypeIntroductoryText IntroductoryText = NeptunePageRenderTypeIntroductoryText.Instance;
        public static readonly NeptunePageRenderTypePageContent PageContent = NeptunePageRenderTypePageContent.Instance;

        public static readonly List<NeptunePageRenderType> All;
        public static readonly ReadOnlyDictionary<int, NeptunePageRenderType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static NeptunePageRenderType()
        {
            All = new List<NeptunePageRenderType> { IntroductoryText, PageContent };
            AllLookupDictionary = new ReadOnlyDictionary<int, NeptunePageRenderType>(All.ToDictionary(x => x.NeptunePageRenderTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected NeptunePageRenderType(int neptunePageRenderTypeID, string neptunePageRenderTypeName, string neptunePageRenderTypeDisplayName)
        {
            NeptunePageRenderTypeID = neptunePageRenderTypeID;
            NeptunePageRenderTypeName = neptunePageRenderTypeName;
            NeptunePageRenderTypeDisplayName = neptunePageRenderTypeDisplayName;
        }
        public List<NeptunePageType> NeptunePageTypes { get { return NeptunePageType.All.Where(x => x.NeptunePageRenderTypeID == NeptunePageRenderTypeID).ToList(); } }
        [Key]
        public int NeptunePageRenderTypeID { get; private set; }
        public string NeptunePageRenderTypeName { get; private set; }
        public string NeptunePageRenderTypeDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return NeptunePageRenderTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(NeptunePageRenderType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.NeptunePageRenderTypeID == NeptunePageRenderTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as NeptunePageRenderType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return NeptunePageRenderTypeID;
        }

        public static bool operator ==(NeptunePageRenderType left, NeptunePageRenderType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(NeptunePageRenderType left, NeptunePageRenderType right)
        {
            return !Equals(left, right);
        }

        public NeptunePageRenderTypeEnum ToEnum { get { return (NeptunePageRenderTypeEnum)GetHashCode(); } }

        public static NeptunePageRenderType ToType(int enumValue)
        {
            return ToType((NeptunePageRenderTypeEnum)enumValue);
        }

        public static NeptunePageRenderType ToType(NeptunePageRenderTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case NeptunePageRenderTypeEnum.IntroductoryText:
                    return IntroductoryText;
                case NeptunePageRenderTypeEnum.PageContent:
                    return PageContent;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum NeptunePageRenderTypeEnum
    {
        IntroductoryText = 1,
        PageContent = 2
    }

    public partial class NeptunePageRenderTypeIntroductoryText : NeptunePageRenderType
    {
        private NeptunePageRenderTypeIntroductoryText(int neptunePageRenderTypeID, string neptunePageRenderTypeName, string neptunePageRenderTypeDisplayName) : base(neptunePageRenderTypeID, neptunePageRenderTypeName, neptunePageRenderTypeDisplayName) {}
        public static readonly NeptunePageRenderTypeIntroductoryText Instance = new NeptunePageRenderTypeIntroductoryText(1, @"IntroductoryText", @"Introductory Text");
    }

    public partial class NeptunePageRenderTypePageContent : NeptunePageRenderType
    {
        private NeptunePageRenderTypePageContent(int neptunePageRenderTypeID, string neptunePageRenderTypeName, string neptunePageRenderTypeDisplayName) : base(neptunePageRenderTypeID, neptunePageRenderTypeName, neptunePageRenderTypeDisplayName) {}
        public static readonly NeptunePageRenderTypePageContent Instance = new NeptunePageRenderTypePageContent(2, @"PageContent", @"Page Content");
    }
}