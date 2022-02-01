//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Project]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class ProjectConfiguration : EntityTypeConfiguration<Project>
    {
        public ProjectConfiguration() : this("dbo"){}

        public ProjectConfiguration(string schema)
        {
            ToTable("Project", schema);
            HasKey(x => x.ProjectID);
            Property(x => x.ProjectID).HasColumnName(@"ProjectID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ProjectName).HasColumnName(@"ProjectName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(200);
            Property(x => x.OrganizationID).HasColumnName(@"OrganizationID").HasColumnType("int").IsRequired();
            Property(x => x.StormwaterJurisdictionID).HasColumnName(@"StormwaterJurisdictionID").HasColumnType("int").IsRequired();
            Property(x => x.ProjectStatusID).HasColumnName(@"ProjectStatusID").HasColumnType("int").IsRequired();
            Property(x => x.PrimaryContactPersonID).HasColumnName(@"PrimaryContactPersonID").HasColumnType("int").IsRequired();
            Property(x => x.CreatePersonID).HasColumnName(@"CreatePersonID").HasColumnType("int").IsRequired();
            Property(x => x.DateCreated).HasColumnName(@"DateCreated").HasColumnType("datetime").IsRequired();
            Property(x => x.ProjectDescription).HasColumnName(@"ProjectDescription").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.AdditionalContactInformation).HasColumnName(@"AdditionalContactInformation").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);

            // Foreign keys
            HasRequired(a => a.Organization).WithMany(b => b.Projects).HasForeignKey(c => c.OrganizationID).WillCascadeOnDelete(false); // FK_Project_Organization_OrganizationID
            HasRequired(a => a.StormwaterJurisdiction).WithMany(b => b.Projects).HasForeignKey(c => c.StormwaterJurisdictionID).WillCascadeOnDelete(false); // FK_Project_StormwaterJurisdiction_StormwaterJurisdictionID
            HasRequired(a => a.ProjectStatus).WithMany(b => b.Projects).HasForeignKey(c => c.ProjectStatusID).WillCascadeOnDelete(false); // FK_Project_ProjectStatus_ProjectStatusID
            HasRequired(a => a.PrimaryContactPerson).WithMany(b => b.ProjectsWhereYouAreThePrimaryContactPerson).HasForeignKey(c => c.PrimaryContactPersonID).WillCascadeOnDelete(false); // FK_Project_Person_PrimaryContactPersonID_PersonID
            HasRequired(a => a.CreatePerson).WithMany(b => b.ProjectsWhereYouAreTheCreatePerson).HasForeignKey(c => c.CreatePersonID).WillCascadeOnDelete(false); // FK_Project_Person_CreatePersonID_PersonID
        }
    }
}