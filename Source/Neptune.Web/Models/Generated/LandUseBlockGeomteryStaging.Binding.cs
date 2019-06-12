//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandUseBlockGeomteryStaging]
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
    // Table [dbo].[LandUseBlockGeomteryStaging] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[LandUseBlockGeomteryStaging]")]
    public partial class LandUseBlockGeomteryStaging : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected LandUseBlockGeomteryStaging()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public LandUseBlockGeomteryStaging(int landUseBlockStagingID, int personID, string featureClassName, string landUseBlockStagingGeoJson, string selectedProperty, bool shouldImport) : this()
        {
            this.LandUseBlockStagingID = landUseBlockStagingID;
            this.PersonID = personID;
            this.FeatureClassName = featureClassName;
            this.LandUseBlockStagingGeoJson = landUseBlockStagingGeoJson;
            this.SelectedProperty = selectedProperty;
            this.ShouldImport = shouldImport;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public LandUseBlockGeomteryStaging(int personID, string featureClassName, string landUseBlockStagingGeoJson, bool shouldImport) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.LandUseBlockStagingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.PersonID = personID;
            this.FeatureClassName = featureClassName;
            this.LandUseBlockStagingGeoJson = landUseBlockStagingGeoJson;
            this.ShouldImport = shouldImport;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public LandUseBlockGeomteryStaging(Person person, string featureClassName, string landUseBlockStagingGeoJson, bool shouldImport) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.LandUseBlockStagingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.PersonID = person.PersonID;
            this.Person = person;
            person.LandUseBlockGeomteryStagings.Add(this);
            this.FeatureClassName = featureClassName;
            this.LandUseBlockStagingGeoJson = landUseBlockStagingGeoJson;
            this.ShouldImport = shouldImport;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static LandUseBlockGeomteryStaging CreateNewBlank(Person person)
        {
            return new LandUseBlockGeomteryStaging(person, default(string), default(string), default(bool));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(LandUseBlockGeomteryStaging).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.LandUseBlockGeomteryStagings.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int LandUseBlockStagingID { get; set; }
        public int PersonID { get; set; }
        public string FeatureClassName { get; set; }
        public string LandUseBlockStagingGeoJson { get; set; }
        public string SelectedProperty { get; set; }
        public bool ShouldImport { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return LandUseBlockStagingID; } set { LandUseBlockStagingID = value; } }

        public virtual Person Person { get; set; }

        public static class FieldLengths
        {
            public const int FeatureClassName = 255;
            public const int SelectedProperty = 255;
        }
    }
}