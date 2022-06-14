//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectNetworkSolveHistoryStatusType]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class ProjectNetworkSolveHistoryStatusTypeConfiguration : EntityTypeConfiguration<ProjectNetworkSolveHistoryStatusType>
    {
        public ProjectNetworkSolveHistoryStatusTypeConfiguration() : this("dbo"){}

        public ProjectNetworkSolveHistoryStatusTypeConfiguration(string schema)
        {
            ToTable("ProjectNetworkSolveHistoryStatusType", schema);
            HasKey(x => x.ProjectNetworkSolveHistoryStatusTypeID);
            Property(x => x.ProjectNetworkSolveHistoryStatusTypeID).HasColumnName(@"ProjectNetworkSolveHistoryStatusTypeID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ProjectNetworkSolveHistoryStatusTypeName).HasColumnName(@"ProjectNetworkSolveHistoryStatusTypeName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(50);
            Property(x => x.ProjectNetworkSolveHistoryStatusTypeDisplayName).HasColumnName(@"ProjectNetworkSolveHistoryStatusTypeDisplayName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(50);

            // Foreign keys

        }
    }
}