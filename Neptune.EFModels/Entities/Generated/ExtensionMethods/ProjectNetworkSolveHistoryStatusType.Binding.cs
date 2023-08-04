//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectNetworkSolveHistoryStatusType]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Neptune.Models.DataTransferObjects;


namespace Neptune.EFModels.Entities
{
    public abstract partial class ProjectNetworkSolveHistoryStatusType
    {
        public static readonly ProjectNetworkSolveHistoryStatusTypeQueued Queued = Neptune.EFModels.Entities.ProjectNetworkSolveHistoryStatusTypeQueued.Instance;
        public static readonly ProjectNetworkSolveHistoryStatusTypeSucceeded Succeeded = Neptune.EFModels.Entities.ProjectNetworkSolveHistoryStatusTypeSucceeded.Instance;
        public static readonly ProjectNetworkSolveHistoryStatusTypeFailed Failed = Neptune.EFModels.Entities.ProjectNetworkSolveHistoryStatusTypeFailed.Instance;

        public static readonly List<ProjectNetworkSolveHistoryStatusType> All;
        public static readonly List<ProjectNetworkSolveHistoryStatusTypeDto> AllAsDto;
        public static readonly ReadOnlyDictionary<int, ProjectNetworkSolveHistoryStatusType> AllLookupDictionary;
        public static readonly ReadOnlyDictionary<int, ProjectNetworkSolveHistoryStatusTypeDto> AllAsDtoLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static ProjectNetworkSolveHistoryStatusType()
        {
            All = new List<ProjectNetworkSolveHistoryStatusType> { Queued, Succeeded, Failed };
            AllAsDto = new List<ProjectNetworkSolveHistoryStatusTypeDto> { Queued.AsDto(), Succeeded.AsDto(), Failed.AsDto() };
            AllLookupDictionary = new ReadOnlyDictionary<int, ProjectNetworkSolveHistoryStatusType>(All.ToDictionary(x => x.ProjectNetworkSolveHistoryStatusTypeID));
            AllAsDtoLookupDictionary = new ReadOnlyDictionary<int, ProjectNetworkSolveHistoryStatusTypeDto>(AllAsDto.ToDictionary(x => x.ProjectNetworkSolveHistoryStatusTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected ProjectNetworkSolveHistoryStatusType(int projectNetworkSolveHistoryStatusTypeID, string projectNetworkSolveHistoryStatusTypeName, string projectNetworkSolveHistoryStatusTypeDisplayName)
        {
            ProjectNetworkSolveHistoryStatusTypeID = projectNetworkSolveHistoryStatusTypeID;
            ProjectNetworkSolveHistoryStatusTypeName = projectNetworkSolveHistoryStatusTypeName;
            ProjectNetworkSolveHistoryStatusTypeDisplayName = projectNetworkSolveHistoryStatusTypeDisplayName;
        }

        [Key]
        public int ProjectNetworkSolveHistoryStatusTypeID { get; private set; }
        public string ProjectNetworkSolveHistoryStatusTypeName { get; private set; }
        public string ProjectNetworkSolveHistoryStatusTypeDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectNetworkSolveHistoryStatusTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(ProjectNetworkSolveHistoryStatusType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.ProjectNetworkSolveHistoryStatusTypeID == ProjectNetworkSolveHistoryStatusTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as ProjectNetworkSolveHistoryStatusType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return ProjectNetworkSolveHistoryStatusTypeID;
        }

        public static bool operator ==(ProjectNetworkSolveHistoryStatusType left, ProjectNetworkSolveHistoryStatusType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProjectNetworkSolveHistoryStatusType left, ProjectNetworkSolveHistoryStatusType right)
        {
            return !Equals(left, right);
        }

        public ProjectNetworkSolveHistoryStatusTypeEnum ToEnum => (ProjectNetworkSolveHistoryStatusTypeEnum)GetHashCode();

        public static ProjectNetworkSolveHistoryStatusType ToType(int enumValue)
        {
            return ToType((ProjectNetworkSolveHistoryStatusTypeEnum)enumValue);
        }

        public static ProjectNetworkSolveHistoryStatusType ToType(ProjectNetworkSolveHistoryStatusTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case ProjectNetworkSolveHistoryStatusTypeEnum.Failed:
                    return Failed;
                case ProjectNetworkSolveHistoryStatusTypeEnum.Queued:
                    return Queued;
                case ProjectNetworkSolveHistoryStatusTypeEnum.Succeeded:
                    return Succeeded;
                default:
                    throw new ArgumentException("Unable to map Enum: {enumValue}");
            }
        }
    }

    public enum ProjectNetworkSolveHistoryStatusTypeEnum
    {
        Queued = 1,
        Succeeded = 2,
        Failed = 3
    }

    public partial class ProjectNetworkSolveHistoryStatusTypeQueued : ProjectNetworkSolveHistoryStatusType
    {
        private ProjectNetworkSolveHistoryStatusTypeQueued(int projectNetworkSolveHistoryStatusTypeID, string projectNetworkSolveHistoryStatusTypeName, string projectNetworkSolveHistoryStatusTypeDisplayName) : base(projectNetworkSolveHistoryStatusTypeID, projectNetworkSolveHistoryStatusTypeName, projectNetworkSolveHistoryStatusTypeDisplayName) {}
        public static readonly ProjectNetworkSolveHistoryStatusTypeQueued Instance = new ProjectNetworkSolveHistoryStatusTypeQueued(1, @"Queued", @"Queued");
    }

    public partial class ProjectNetworkSolveHistoryStatusTypeSucceeded : ProjectNetworkSolveHistoryStatusType
    {
        private ProjectNetworkSolveHistoryStatusTypeSucceeded(int projectNetworkSolveHistoryStatusTypeID, string projectNetworkSolveHistoryStatusTypeName, string projectNetworkSolveHistoryStatusTypeDisplayName) : base(projectNetworkSolveHistoryStatusTypeID, projectNetworkSolveHistoryStatusTypeName, projectNetworkSolveHistoryStatusTypeDisplayName) {}
        public static readonly ProjectNetworkSolveHistoryStatusTypeSucceeded Instance = new ProjectNetworkSolveHistoryStatusTypeSucceeded(2, @"Succeeded", @"Succeeded");
    }

    public partial class ProjectNetworkSolveHistoryStatusTypeFailed : ProjectNetworkSolveHistoryStatusType
    {
        private ProjectNetworkSolveHistoryStatusTypeFailed(int projectNetworkSolveHistoryStatusTypeID, string projectNetworkSolveHistoryStatusTypeName, string projectNetworkSolveHistoryStatusTypeDisplayName) : base(projectNetworkSolveHistoryStatusTypeID, projectNetworkSolveHistoryStatusTypeName, projectNetworkSolveHistoryStatusTypeDisplayName) {}
        public static readonly ProjectNetworkSolveHistoryStatusTypeFailed Instance = new ProjectNetworkSolveHistoryStatusTypeFailed(3, @"Failed", @"Failed");
    }
}