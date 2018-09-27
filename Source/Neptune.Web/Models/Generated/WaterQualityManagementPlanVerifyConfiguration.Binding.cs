//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerify]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanVerifyConfiguration : EntityTypeConfiguration<WaterQualityManagementPlanVerify>
    {
        public WaterQualityManagementPlanVerifyConfiguration() : this("dbo"){}

        public WaterQualityManagementPlanVerifyConfiguration(string schema)
        {
            ToTable("WaterQualityManagementPlanVerify", schema);
            HasKey(x => x.WaterQualityManagementPlanVerifyID);
            Property(x => x.WaterQualityManagementPlanVerifyID).HasColumnName(@"WaterQualityManagementPlanVerifyID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TenantID).HasColumnName(@"TenantID").HasColumnType("int").IsRequired();
            Property(x => x.WaterQualityManagementPlanID).HasColumnName(@"WaterQualityManagementPlanID").HasColumnType("int").IsRequired();
            Property(x => x.WaterQualityManagementPlanVerifyTypeID).HasColumnName(@"WaterQualityManagementPlanVerifyTypeID").HasColumnType("int").IsRequired();
            Property(x => x.WaterQualityManagementPlanVisitStatusID).HasColumnName(@"WaterQualityManagementPlanVisitStatusID").HasColumnType("int").IsRequired();
            Property(x => x.WaterQualityManagementPlanDocumentID).HasColumnName(@"WaterQualityManagementPlanDocumentID").HasColumnType("int").IsOptional();
            Property(x => x.WaterQualityManagementPlanVerifyStatusID).HasColumnName(@"WaterQualityManagementPlanVerifyStatusID").HasColumnType("int").IsRequired();
            Property(x => x.LastEditedByPersonID).HasColumnName(@"LastEditedByPersonID").HasColumnType("int").IsRequired();
            Property(x => x.SourceControlCondition).HasColumnName(@"SourceControlCondition").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(1000);
            Property(x => x.EnforcementOrFollowupActions).HasColumnName(@"EnforcementOrFollowupActions").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(1000);
            Property(x => x.LastEditedDate).HasColumnName(@"LastEditedDate").HasColumnType("datetime").IsRequired();

            // Foreign keys
            HasRequired(a => a.WaterQualityManagementPlan).WithMany(b => b.WaterQualityManagementPlanVerifies).HasForeignKey(c => c.WaterQualityManagementPlanID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlanVerify_WaterQualityManagementPlan_WaterQualityManagementPlanID
            HasRequired(a => a.WaterQualityManagementPlanVerifyType).WithMany(b => b.WaterQualityManagementPlanVerifies).HasForeignKey(c => c.WaterQualityManagementPlanVerifyTypeID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyType_WaterQualityManagementPlanVerifyTypeID
            HasRequired(a => a.WaterQualityManagementPlanVisitStatus).WithMany(b => b.WaterQualityManagementPlanVerifies).HasForeignKey(c => c.WaterQualityManagementPlanVisitStatusID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVisitStatus_WaterQualityManagementPlanVisitStatusID
            HasOptional(a => a.WaterQualityManagementPlanDocument).WithMany(b => b.WaterQualityManagementPlanVerifies).HasForeignKey(c => c.WaterQualityManagementPlanDocumentID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlanVerify_WaterQualityManagementPlanDocument_WaterQualityManagementPlanDocumentID
            HasRequired(a => a.WaterQualityManagementPlanVerifyStatus).WithMany(b => b.WaterQualityManagementPlanVerifies).HasForeignKey(c => c.WaterQualityManagementPlanVerifyStatusID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyStatus_WaterQualityManagementPlanVerifyStatusID
            HasRequired(a => a.LastEditedByPerson).WithMany(b => b.WaterQualityManagementPlanVerifiesWhereYouAreTheLastEditedByPerson).HasForeignKey(c => c.LastEditedByPersonID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlanVerify_Person_LastEditedByPersonID_PersonID
        }
    }
}