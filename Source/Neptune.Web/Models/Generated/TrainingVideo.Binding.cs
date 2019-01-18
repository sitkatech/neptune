//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrainingVideo]
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
    // Table [dbo].[TrainingVideo] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[TrainingVideo]")]
    public partial class TrainingVideo : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TrainingVideo()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TrainingVideo(int trainingVideoID, string videoName, string videoDescription, string videoURL) : this()
        {
            this.TrainingVideoID = trainingVideoID;
            this.VideoName = videoName;
            this.VideoDescription = videoDescription;
            this.VideoURL = videoURL;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TrainingVideo(string videoName, string videoURL) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TrainingVideoID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.VideoName = videoName;
            this.VideoURL = videoURL;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TrainingVideo CreateNewBlank()
        {
            return new TrainingVideo(default(string), default(string));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TrainingVideo).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            dbContext.TrainingVideos.Remove(this);
        }

        [Key]
        public int TrainingVideoID { get; set; }
        public string VideoName { get; set; }
        public string VideoDescription { get; set; }
        public string VideoURL { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TrainingVideoID; } set { TrainingVideoID = value; } }



        public static class FieldLengths
        {
            public const int VideoName = 100;
            public const int VideoDescription = 500;
            public const int VideoURL = 100;
        }
    }
}