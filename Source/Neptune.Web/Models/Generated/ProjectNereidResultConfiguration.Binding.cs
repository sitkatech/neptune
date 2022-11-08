//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectNereidResult]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class ProjectNereidResultConfiguration : EntityTypeConfiguration<ProjectNereidResult>
    {
        public ProjectNereidResultConfiguration() : this("dbo"){}

        public ProjectNereidResultConfiguration(string schema)
        {
            ToTable("ProjectNereidResult", schema);
            HasKey(x => x.ProjectNereidResultID);
            Property(x => x.ProjectNereidResultID).HasColumnName(@"ProjectNereidResultID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ProjectID).HasColumnName(@"ProjectID").HasColumnType("int").IsRequired();
            Property(x => x.IsBaselineCondition).HasColumnName(@"IsBaselineCondition").HasColumnType("bit").IsRequired();
            Property(x => x.TreatmentBMPID).HasColumnName(@"TreatmentBMPID").HasColumnType("int").IsOptional();
            Property(x => x.WaterQualityManagementPlanID).HasColumnName(@"WaterQualityManagementPlanID").HasColumnType("int").IsOptional();
            Property(x => x.RegionalSubbasinID).HasColumnName(@"RegionalSubbasinID").HasColumnType("int").IsOptional();
            Property(x => x.DelineationID).HasColumnName(@"DelineationID").HasColumnType("int").IsOptional();
            Property(x => x.NodeID).HasColumnName(@"NodeID").HasColumnType("varchar").IsOptional();
            Property(x => x.FullResponse).HasColumnName(@"FullResponse").HasColumnType("varchar").IsOptional();
            Property(x => x.LastUpdate).HasColumnName(@"LastUpdate").HasColumnType("datetime").IsOptional();

            // Foreign keys
            HasRequired(a => a.Project).WithMany(b => b.ProjectNereidResults).HasForeignKey(c => c.ProjectID).WillCascadeOnDelete(false); // FK_ProjectNereidResult_Project_ProjectID
            HasOptional(a => a.TreatmentBMP).WithMany(b => b.ProjectNereidResults).HasForeignKey(c => c.TreatmentBMPID).WillCascadeOnDelete(false); // FK_ProjectNereidResult_TreatmentBMP_TreatmentBMPID
            HasOptional(a => a.WaterQualityManagementPlan).WithMany(b => b.ProjectNereidResults).HasForeignKey(c => c.WaterQualityManagementPlanID).WillCascadeOnDelete(false); // FK_ProjectNereidResult_WaterQualityManagementPlan_WaterQualityManagementPlanID
            HasOptional(a => a.RegionalSubbasin).WithMany(b => b.ProjectNereidResults).HasForeignKey(c => c.RegionalSubbasinID).WillCascadeOnDelete(false); // FK_ProjectNereidResult_RegionalSubbasin_RegionalSubbasinID
            HasOptional(a => a.Delineation).WithMany(b => b.ProjectNereidResults).HasForeignKey(c => c.DelineationID).WillCascadeOnDelete(false); // FK_ProjectNereidResult_Delineation_DelineationID
        }
    }
}