//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandUseBlockGeometryStaging]
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
    // Table [dbo].[LandUseBlockGeometryStaging] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[LandUseBlockGeometryStaging]")]
    public partial class LandUseBlockGeometryStaging : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected LandUseBlockGeometryStaging()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public LandUseBlockGeometryStaging(int landUseBlockGeometryStagingID, int personID, int stormwaterJurisdictionID, string featureClassName, string landUseBlockGeometryStagingGeoJson, string selectedProperty, bool shouldImport) : this()
        {
            this.LandUseBlockGeometryStagingID = landUseBlockGeometryStagingID;
            this.PersonID = personID;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.FeatureClassName = featureClassName;
            this.LandUseBlockGeometryStagingGeoJson = landUseBlockGeometryStagingGeoJson;
            this.SelectedProperty = selectedProperty;
            this.ShouldImport = shouldImport;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public LandUseBlockGeometryStaging(int personID, int stormwaterJurisdictionID, string featureClassName, string landUseBlockGeometryStagingGeoJson, bool shouldImport) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.LandUseBlockGeometryStagingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.PersonID = personID;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.FeatureClassName = featureClassName;
            this.LandUseBlockGeometryStagingGeoJson = landUseBlockGeometryStagingGeoJson;
            this.ShouldImport = shouldImport;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public LandUseBlockGeometryStaging(Person person, StormwaterJurisdiction stormwaterJurisdiction, string featureClassName, string landUseBlockGeometryStagingGeoJson, bool shouldImport) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.LandUseBlockGeometryStagingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.PersonID = person.PersonID;
            this.Person = person;
            person.LandUseBlockGeometryStagings.Add(this);
            this.StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID;
            this.StormwaterJurisdiction = stormwaterJurisdiction;
            stormwaterJurisdiction.LandUseBlockGeometryStagings.Add(this);
            this.FeatureClassName = featureClassName;
            this.LandUseBlockGeometryStagingGeoJson = landUseBlockGeometryStagingGeoJson;
            this.ShouldImport = shouldImport;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static LandUseBlockGeometryStaging CreateNewBlank(Person person, StormwaterJurisdiction stormwaterJurisdiction)
        {
            return new LandUseBlockGeometryStaging(person, stormwaterJurisdiction, default(string), default(string), default(bool));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return false;
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(LandUseBlockGeometryStaging).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.LandUseBlockGeometryStagings.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int LandUseBlockGeometryStagingID { get; set; }
        public int PersonID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public string FeatureClassName { get; set; }
        public string LandUseBlockGeometryStagingGeoJson { get; set; }
        public string SelectedProperty { get; set; }
        public bool ShouldImport { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return LandUseBlockGeometryStagingID; } set { LandUseBlockGeometryStagingID = value; } }

        public virtual Person Person { get; set; }
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }

        public static class FieldLengths
        {
            public const int FeatureClassName = 255;
            public const int SelectedProperty = 255;
        }
    }
}