//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePage]
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    [Table("[dbo].[NeptunePage]")]
    public partial class NeptunePage : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected NeptunePage()
        {
            this.NeptunePageImages = new HashSet<NeptunePageImage>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public NeptunePage(int neptunePageID, int neptunePageTypeID, string neptunePageContent) : this()
        {
            this.NeptunePageID = neptunePageID;
            this.NeptunePageTypeID = neptunePageTypeID;
            this.NeptunePageContent = neptunePageContent;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public NeptunePage(int neptunePageTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.NeptunePageID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.NeptunePageTypeID = neptunePageTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public NeptunePage(NeptunePageType neptunePageType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.NeptunePageID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.NeptunePageTypeID = neptunePageType.NeptunePageTypeID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static NeptunePage CreateNewBlank(NeptunePageType neptunePageType)
        {
            return new NeptunePage(neptunePageType);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return NeptunePageImages.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(NeptunePage).Name, typeof(NeptunePageImage).Name};

        [Key]
        public int NeptunePageID { get; set; }
        public int TenantID { get; private set; }
        public int NeptunePageTypeID { get; set; }
        public string NeptunePageContent { get; set; }
        [NotMapped]
        public HtmlString NeptunePageContentHtmlString
        { 
            get { return NeptunePageContent == null ? null : new HtmlString(NeptunePageContent); }
            set { NeptunePageContent = value?.ToString(); }
        }
        [NotMapped]
        public int PrimaryKey { get { return NeptunePageID; } set { NeptunePageID = value; } }

        public virtual ICollection<NeptunePageImage> NeptunePageImages { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public NeptunePageType NeptunePageType { get { return NeptunePageType.AllLookupDictionary[NeptunePageTypeID]; } }

        public static class FieldLengths
        {

        }
    }
}