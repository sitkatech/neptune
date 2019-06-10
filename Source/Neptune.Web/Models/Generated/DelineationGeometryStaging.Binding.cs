//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DelineationGeometryStaging]
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
    // Table [dbo].[DelineationGeometryStaging] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[DelineationGeometryStaging]")]
    public partial class DelineationGeometryStaging : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected DelineationGeometryStaging()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public DelineationGeometryStaging(int delineationGeometryStagingID, int personID, string featureClassName, string delineationGeometryStagingGeoJson, string selectedProperty, bool shouldImport) : this()
        {
            this.DelineationGeometryStagingID = delineationGeometryStagingID;
            this.PersonID = personID;
            this.FeatureClassName = featureClassName;
            this.DelineationGeometryStagingGeoJson = delineationGeometryStagingGeoJson;
            this.SelectedProperty = selectedProperty;
            this.ShouldImport = shouldImport;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public DelineationGeometryStaging(int personID, string featureClassName, string delineationGeometryStagingGeoJson, bool shouldImport) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.DelineationGeometryStagingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.PersonID = personID;
            this.FeatureClassName = featureClassName;
            this.DelineationGeometryStagingGeoJson = delineationGeometryStagingGeoJson;
            this.ShouldImport = shouldImport;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public DelineationGeometryStaging(Person person, string featureClassName, string delineationGeometryStagingGeoJson, bool shouldImport) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.DelineationGeometryStagingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.PersonID = person.PersonID;
            this.Person = person;
            person.DelineationGeometryStagings.Add(this);
            this.FeatureClassName = featureClassName;
            this.DelineationGeometryStagingGeoJson = delineationGeometryStagingGeoJson;
            this.ShouldImport = shouldImport;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static DelineationGeometryStaging CreateNewBlank(Person person)
        {
            return new DelineationGeometryStaging(person, default(string), default(string), default(bool));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(DelineationGeometryStaging).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.DelineationGeometryStagings.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int DelineationGeometryStagingID { get; set; }
        public int PersonID { get; set; }
        public string FeatureClassName { get; set; }
        public string DelineationGeometryStagingGeoJson { get; set; }
        public string SelectedProperty { get; set; }
        public bool ShouldImport { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return DelineationGeometryStagingID; } set { DelineationGeometryStagingID = value; } }

        public virtual Person Person { get; set; }

        public static class FieldLengths
        {
            public const int FeatureClassName = 255;
            public const int SelectedProperty = 255;
        }
    }
}