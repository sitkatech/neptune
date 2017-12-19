//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ModeledCatchmentGeometryStaging]
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
    [Table("[dbo].[ModeledCatchmentGeometryStaging]")]
    public partial class ModeledCatchmentGeometryStaging : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ModeledCatchmentGeometryStaging()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ModeledCatchmentGeometryStaging(int modeledCatchmentGeometryStagingID, int personID, string featureClassName, string geoJson, string selectedProperty, bool shouldImport) : this()
        {
            this.ModeledCatchmentGeometryStagingID = modeledCatchmentGeometryStagingID;
            this.PersonID = personID;
            this.FeatureClassName = featureClassName;
            this.GeoJson = geoJson;
            this.SelectedProperty = selectedProperty;
            this.ShouldImport = shouldImport;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ModeledCatchmentGeometryStaging(int personID, string featureClassName, string geoJson, bool shouldImport) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ModeledCatchmentGeometryStagingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.PersonID = personID;
            this.FeatureClassName = featureClassName;
            this.GeoJson = geoJson;
            this.ShouldImport = shouldImport;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ModeledCatchmentGeometryStaging(Person person, string featureClassName, string geoJson, bool shouldImport) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ModeledCatchmentGeometryStagingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.PersonID = person.PersonID;
            this.Person = person;
            person.ModeledCatchmentGeometryStagings.Add(this);
            this.FeatureClassName = featureClassName;
            this.GeoJson = geoJson;
            this.ShouldImport = shouldImport;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ModeledCatchmentGeometryStaging CreateNewBlank(Person person)
        {
            return new ModeledCatchmentGeometryStaging(person, default(string), default(string), default(bool));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ModeledCatchmentGeometryStaging).Name};

        [Key]
        public int ModeledCatchmentGeometryStagingID { get; set; }
        public int TenantID { get; private set; }
        public int PersonID { get; set; }
        public string FeatureClassName { get; set; }
        public string GeoJson { get; set; }
        public string SelectedProperty { get; set; }
        public bool ShouldImport { get; set; }
        public int PrimaryKey { get { return ModeledCatchmentGeometryStagingID; } set { ModeledCatchmentGeometryStagingID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual Person Person { get; set; }

        public static class FieldLengths
        {
            public const int FeatureClassName = 255;
            public const int SelectedProperty = 255;
        }
    }
}