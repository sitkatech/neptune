//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdictionPerson]
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
    // Table [dbo].[StormwaterJurisdictionPerson] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[StormwaterJurisdictionPerson]")]
    public partial class StormwaterJurisdictionPerson : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected StormwaterJurisdictionPerson()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public StormwaterJurisdictionPerson(int stormwaterJurisdictionPersonID, int stormwaterJurisdictionID, int personID) : this()
        {
            this.StormwaterJurisdictionPersonID = stormwaterJurisdictionPersonID;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.PersonID = personID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public StormwaterJurisdictionPerson(int stormwaterJurisdictionID, int personID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.StormwaterJurisdictionPersonID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.PersonID = personID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public StormwaterJurisdictionPerson(StormwaterJurisdiction stormwaterJurisdiction, Person person) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.StormwaterJurisdictionPersonID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID;
            this.StormwaterJurisdiction = stormwaterJurisdiction;
            stormwaterJurisdiction.StormwaterJurisdictionPeople.Add(this);
            this.PersonID = person.PersonID;
            this.Person = person;
            person.StormwaterJurisdictionPeople.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static StormwaterJurisdictionPerson CreateNewBlank(StormwaterJurisdiction stormwaterJurisdiction, Person person)
        {
            return new StormwaterJurisdictionPerson(stormwaterJurisdiction, person);
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
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(StormwaterJurisdictionPerson).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.StormwaterJurisdictionPeople.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int StormwaterJurisdictionPersonID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int PersonID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return StormwaterJurisdictionPersonID; } set { StormwaterJurisdictionPersonID = value; } }

        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        public virtual Person Person { get; set; }

        public static class FieldLengths
        {

        }
    }
}