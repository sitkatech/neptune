//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlan]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanConfiguration : EntityTypeConfiguration<WaterQualityManagementPlan>
    {
        public WaterQualityManagementPlanConfiguration() : this("dbo"){}

        public WaterQualityManagementPlanConfiguration(string schema)
        {
            ToTable("WaterQualityManagementPlan", schema);
            HasKey(x => x.WaterQualityManagementPlanID);
            Property(x => x.WaterQualityManagementPlanID).HasColumnName(@"WaterQualityManagementPlanID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TenantID).HasColumnName(@"TenantID").HasColumnType("int").IsRequired();
            Property(x => x.StormwaterJurisdictionID).HasColumnName(@"StormwaterJurisdictionID").HasColumnType("int").IsRequired();
            Property(x => x.WaterQualityManagementPlanLandUseID).HasColumnName(@"WaterQualityManagementPlanLandUseID").HasColumnType("int").IsRequired();
            Property(x => x.WaterQualityManagementPlanPriorityID).HasColumnName(@"WaterQualityManagementPlanPriorityID").HasColumnType("int").IsRequired();
            Property(x => x.WaterQualityManagementPlanStatusID).HasColumnName(@"WaterQualityManagementPlanStatusID").HasColumnType("int").IsRequired();
            Property(x => x.WaterQualityManagementPlanDevelopmentTypeID).HasColumnName(@"WaterQualityManagementPlanDevelopmentTypeID").HasColumnType("int").IsRequired();
            Property(x => x.WaterQualityManagementPlanName).HasColumnName(@"WaterQualityManagementPlanName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);
            Property(x => x.ApprovalDate).HasColumnName(@"ApprovalDate").HasColumnType("datetime").IsOptional();
            Property(x => x.MaintenanceContactName).HasColumnName(@"MaintenanceContactName").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.MaintenanceContactOrganization).HasColumnName(@"MaintenanceContactOrganization").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.MaintenanceContactPhone).HasColumnName(@"MaintenanceContactPhone").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.MaintenanceContactAddress1).HasColumnName(@"MaintenanceContactAddress1").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.MaintenanceContactAddress2).HasColumnName(@"MaintenanceContactAddress2").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.MaintenanceContactCity).HasColumnName(@"MaintenanceContactCity").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.MaintenanceContactState).HasColumnName(@"MaintenanceContactState").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.MaintenanceContactZip).HasColumnName(@"MaintenanceContactZip").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.WaterQualityManagementPlanPermitTermID).HasColumnName(@"WaterQualityManagementPlanPermitTermID").HasColumnType("int").IsOptional();
            Property(x => x.HydromodificationAppliesID).HasColumnName(@"HydromodificationAppliesID").HasColumnType("int").IsOptional();
            Property(x => x.DateOfContruction).HasColumnName(@"DateOfContruction").HasColumnType("datetime").IsOptional();
            Property(x => x.HydrologicSubareaID).HasColumnName(@"HydrologicSubareaID").HasColumnType("int").IsOptional();
            Property(x => x.RecordNumber).HasColumnName(@"RecordNumber").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.RecordedWQMPAreaInAcres).HasColumnName(@"RecordedWQMPAreaInAcres").HasColumnType("decimal").IsOptional().HasPrecision(5,1);

            // Foreign keys
            HasRequired(a => a.StormwaterJurisdiction).WithMany(b => b.WaterQualityManagementPlans).HasForeignKey(c => c.StormwaterJurisdictionID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlan_StormwaterJurisdiction_StormwaterJurisdictionID
            HasOptional(a => a.HydrologicSubarea).WithMany(b => b.WaterQualityManagementPlans).HasForeignKey(c => c.HydrologicSubareaID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlan_HydrologicSubarea_HydrologicSubareaID
        }
    }
}