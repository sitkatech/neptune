//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectStatus]
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
    public abstract partial class ProjectStatus : IHavePrimaryKey
    {
        public static readonly ProjectStatusDraft Draft = ProjectStatusDraft.Instance;

        public static readonly List<ProjectStatus> All;
        public static readonly ReadOnlyDictionary<int, ProjectStatus> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static ProjectStatus()
        {
            All = new List<ProjectStatus> { Draft };
            AllLookupDictionary = new ReadOnlyDictionary<int, ProjectStatus>(All.ToDictionary(x => x.ProjectStatusID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected ProjectStatus(int projectStatusID, string projectStatusName, string projectStatusDisplayName, int projectStatusSortOrder)
        {
            ProjectStatusID = projectStatusID;
            ProjectStatusName = projectStatusName;
            ProjectStatusDisplayName = projectStatusDisplayName;
            ProjectStatusSortOrder = projectStatusSortOrder;
        }

        [Key]
        public int ProjectStatusID { get; private set; }
        public string ProjectStatusName { get; private set; }
        public string ProjectStatusDisplayName { get; private set; }
        public int ProjectStatusSortOrder { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectStatusID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(ProjectStatus other)
        {
            if (other == null)
            {
                return false;
            }
            return other.ProjectStatusID == ProjectStatusID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as ProjectStatus);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return ProjectStatusID;
        }

        public static bool operator ==(ProjectStatus left, ProjectStatus right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProjectStatus left, ProjectStatus right)
        {
            return !Equals(left, right);
        }

        public ProjectStatusEnum ToEnum { get { return (ProjectStatusEnum)GetHashCode(); } }

        public static ProjectStatus ToType(int enumValue)
        {
            return ToType((ProjectStatusEnum)enumValue);
        }

        public static ProjectStatus ToType(ProjectStatusEnum enumValue)
        {
            switch (enumValue)
            {
                case ProjectStatusEnum.Draft:
                    return Draft;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum ProjectStatusEnum
    {
        Draft = 1
    }

    public partial class ProjectStatusDraft : ProjectStatus
    {
        private ProjectStatusDraft(int projectStatusID, string projectStatusName, string projectStatusDisplayName, int projectStatusSortOrder) : base(projectStatusID, projectStatusName, projectStatusDisplayName, projectStatusSortOrder) {}
        public static readonly ProjectStatusDraft Instance = new ProjectStatusDraft(1, @"Draft", @"Draft", 10);
    }
}