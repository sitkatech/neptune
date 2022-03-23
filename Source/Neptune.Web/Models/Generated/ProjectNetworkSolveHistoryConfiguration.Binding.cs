//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectNetworkSolveHistory]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class ProjectNetworkSolveHistoryConfiguration : EntityTypeConfiguration<ProjectNetworkSolveHistory>
    {
        public ProjectNetworkSolveHistoryConfiguration() : this("dbo"){}

        public ProjectNetworkSolveHistoryConfiguration(string schema)
        {
            ToTable("ProjectNetworkSolveHistory", schema);
            HasKey(x => x.ProjectNetworkSolveHistoryID);
            Property(x => x.ProjectNetworkSolveHistoryID).HasColumnName(@"ProjectNetworkSolveHistoryID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ProjectID).HasColumnName(@"ProjectID").HasColumnType("int").IsRequired();
            Property(x => x.RequestedByPersonID).HasColumnName(@"RequestedByPersonID").HasColumnType("int").IsRequired();
            Property(x => x.ProjectNetworkSolveHistoryStatusTypeID).HasColumnName(@"ProjectNetworkSolveHistoryStatusTypeID").HasColumnType("int").IsRequired();
            Property(x => x.LastUpdated).HasColumnName(@"LastUpdated").HasColumnType("datetime").IsRequired();
            Property(x => x.ErrorMessage).HasColumnName(@"ErrorMessage").HasColumnType("varchar").IsOptional();

            // Foreign keys
            HasRequired(a => a.Project).WithMany(b => b.ProjectNetworkSolveHistories).HasForeignKey(c => c.ProjectID).WillCascadeOnDelete(false); // FK_ProjectNetworkSolveHistory_Project_ProjectID
            HasRequired(a => a.RequestedByPerson).WithMany(b => b.ProjectNetworkSolveHistoriesWhereYouAreTheRequestedByPerson).HasForeignKey(c => c.RequestedByPersonID).WillCascadeOnDelete(false); // FK_ProjectNetworkSolveHistory_Person_RequestedByPersonID_PersonID
        }
    }
}