//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DirtyModelNode]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class DirtyModelNodeConfiguration : EntityTypeConfiguration<DirtyModelNode>
    {
        public DirtyModelNodeConfiguration() : this("dbo"){}

        public DirtyModelNodeConfiguration(string schema)
        {
            ToTable("DirtyModelNode", schema);
            HasKey(x => x.DirtyModelNodeID);
            Property(x => x.DirtyModelNodeID).HasColumnName(@"DirtyModelNodeID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TreatmentBMPID).HasColumnName(@"TreatmentBMPID").HasColumnType("int").IsOptional();
            Property(x => x.WaterQualityManagementPlanID).HasColumnName(@"WaterQualityManagementPlanID").HasColumnType("int").IsOptional();
            Property(x => x.RegionalSubbasinID).HasColumnName(@"RegionalSubbasinID").HasColumnType("int").IsOptional();
            Property(x => x.DelineationID).HasColumnName(@"DelineationID").HasColumnType("int").IsOptional();
            Property(x => x.CreateDate).HasColumnName(@"CreateDate").HasColumnType("datetime").IsRequired();

            // Foreign keys
            HasOptional(a => a.TreatmentBMP).WithMany(b => b.DirtyModelNodes).HasForeignKey(c => c.TreatmentBMPID).WillCascadeOnDelete(false); // FK_DirtyModelNode_TreatmentBMP_TreatmentBMPID
            HasOptional(a => a.WaterQualityManagementPlan).WithMany(b => b.DirtyModelNodes).HasForeignKey(c => c.WaterQualityManagementPlanID).WillCascadeOnDelete(false); // FK_DirtyModelNode_WaterQualityManagementPlan_WaterQualityManagementPlanID
            HasOptional(a => a.RegionalSubbasin).WithMany(b => b.DirtyModelNodes).HasForeignKey(c => c.RegionalSubbasinID).WillCascadeOnDelete(false); // FK_DirtyModelNode_RegionalSubbasin_RegionalSubbasinID
            HasOptional(a => a.Delineation).WithMany(b => b.DirtyModelNodes).HasForeignKey(c => c.DelineationID).WillCascadeOnDelete(false); // FK_DirtyModelNode_Delineation_DelineationID
        }
    }
}