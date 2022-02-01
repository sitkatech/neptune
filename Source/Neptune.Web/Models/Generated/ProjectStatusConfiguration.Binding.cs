//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectStatus]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class ProjectStatusConfiguration : EntityTypeConfiguration<ProjectStatus>
    {
        public ProjectStatusConfiguration() : this("dbo"){}

        public ProjectStatusConfiguration(string schema)
        {
            ToTable("ProjectStatus", schema);
            HasKey(x => x.ProjectStatusID);
            Property(x => x.ProjectStatusID).HasColumnName(@"ProjectStatusID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ProjectStatusName).HasColumnName(@"ProjectStatusName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(50);
            Property(x => x.ProjectStatusDisplayName).HasColumnName(@"ProjectStatusDisplayName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(50);
            Property(x => x.ProjectStatusSortOrder).HasColumnName(@"ProjectStatusSortOrder").HasColumnType("int").IsRequired();

            // Foreign keys

        }
    }
}