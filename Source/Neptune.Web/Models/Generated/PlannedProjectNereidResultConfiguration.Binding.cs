//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PlannedProjectNereidResult]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class PlannedProjectNereidResultConfiguration : EntityTypeConfiguration<PlannedProjectNereidResult>
    {
        public PlannedProjectNereidResultConfiguration() : this("dbo"){}

        public PlannedProjectNereidResultConfiguration(string schema)
        {
            ToTable("PlannedProjectNereidResult", schema);
            HasKey(x => x.PlannedProjectNereidResultID);
            Property(x => x.PlannedProjectNereidResultID).HasColumnName(@"PlannedProjectNereidResultID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ProjectID).HasColumnName(@"ProjectID").HasColumnType("int").IsRequired();
            Property(x => x.IsBaselineCondition).HasColumnName(@"IsBaselineCondition").HasColumnType("bit").IsRequired();
            Property(x => x.TreatmentBMPID).HasColumnName(@"TreatmentBMPID").HasColumnType("int").IsOptional();
            Property(x => x.WaterQualityManagementPlanID).HasColumnName(@"WaterQualityManagementPlanID").HasColumnType("int").IsOptional();
            Property(x => x.RegionalSubbasinID).HasColumnName(@"RegionalSubbasinID").HasColumnType("int").IsOptional();
            Property(x => x.NodeID).HasColumnName(@"NodeID").HasColumnType("varchar").IsOptional();
            Property(x => x.FullResponse).HasColumnName(@"FullResponse").HasColumnType("varchar").IsOptional();
            Property(x => x.LastUpdate).HasColumnName(@"LastUpdate").HasColumnType("datetime").IsOptional();

            // Foreign keys
            HasRequired(a => a.Project).WithMany(b => b.PlannedProjectNereidResults).HasForeignKey(c => c.ProjectID).WillCascadeOnDelete(false); // FK_PlannedProjectNereidResult_Project_ProjectID
        }
    }
}