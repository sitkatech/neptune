//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMP]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class TreatmentBMPConfiguration : EntityTypeConfiguration<TreatmentBMP>
    {
        public TreatmentBMPConfiguration() : this("dbo"){}

        public TreatmentBMPConfiguration(string schema)
        {
            ToTable("TreatmentBMP", schema);
            HasKey(x => x.TreatmentBMPID);
            Property(x => x.TreatmentBMPID).HasColumnName(@"TreatmentBMPID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TreatmentBMPName).HasColumnName(@"TreatmentBMPName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(200);
            Property(x => x.TreatmentBMPTypeID).HasColumnName(@"TreatmentBMPTypeID").HasColumnType("int").IsRequired();
            Property(x => x.LocationPoint).HasColumnName(@"LocationPoint").HasColumnType("geometry").IsOptional();
            Property(x => x.StormwaterJurisdictionID).HasColumnName(@"StormwaterJurisdictionID").HasColumnType("int").IsRequired();
            Property(x => x.ModeledCatchmentID).HasColumnName(@"ModeledCatchmentID").HasColumnType("int").IsOptional();
            Property(x => x.Notes).HasColumnName(@"Notes").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(1000);
            Property(x => x.SystemOfRecordID).HasColumnName(@"SystemOfRecordID").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.YearBuilt).HasColumnName(@"YearBuilt").HasColumnType("int").IsOptional();
            Property(x => x.OwnerOrganizationID).HasColumnName(@"OwnerOrganizationID").HasColumnType("int").IsRequired();
            Property(x => x.WaterQualityManagementPlanID).HasColumnName(@"WaterQualityManagementPlanID").HasColumnType("int").IsOptional();
            Property(x => x.TreatmentBMPLifespanTypeID).HasColumnName(@"TreatmentBMPLifespanTypeID").HasColumnType("int").IsOptional();
            Property(x => x.TreatmentBMPLifespanEndDate).HasColumnName(@"TreatmentBMPLifespanEndDate").HasColumnType("datetime").IsOptional();
            Property(x => x.RequiredFieldVisitsPerYear).HasColumnName(@"RequiredFieldVisitsPerYear").HasColumnType("int").IsOptional();
            Property(x => x.RequiredPostStormFieldVisitsPerYear).HasColumnName(@"RequiredPostStormFieldVisitsPerYear").HasColumnType("int").IsOptional();
            Property(x => x.InventoryIsVerified).HasColumnName(@"InventoryIsVerified").HasColumnType("bit").IsRequired();
            Property(x => x.DateOfLastInventoryVerification).HasColumnName(@"DateOfLastInventoryVerification").HasColumnType("datetime").IsOptional();
            Property(x => x.InventoryVerifiedByPersonID).HasColumnName(@"InventoryVerifiedByPersonID").HasColumnType("int").IsOptional();
            Property(x => x.InventoryLastChangedDate).HasColumnName(@"InventoryLastChangedDate").HasColumnType("datetime").IsOptional();
            Property(x => x.TrashCaptureStatusTypeID).HasColumnName(@"TrashCaptureStatusTypeID").HasColumnType("int").IsRequired();
            Property(x => x.DelineationID).HasColumnName(@"DelineationID").HasColumnType("int").IsOptional();
            Property(x => x.SizingBasisTypeID).HasColumnName(@"SizingBasisTypeID").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.TreatmentBMPType).WithMany(b => b.TreatmentBMPs).HasForeignKey(c => c.TreatmentBMPTypeID).WillCascadeOnDelete(false); // FK_TreatmentBMP_TreatmentBMPType_TreatmentBMPTypeID
            HasRequired(a => a.StormwaterJurisdiction).WithMany(b => b.TreatmentBMPs).HasForeignKey(c => c.StormwaterJurisdictionID).WillCascadeOnDelete(false); // FK_TreatmentBMP_StormwaterJurisdiction_StormwaterJurisdictionID
            HasOptional(a => a.ModeledCatchment).WithMany(b => b.TreatmentBMPs).HasForeignKey(c => c.ModeledCatchmentID).WillCascadeOnDelete(false); // FK_TreatmentBMP_ModeledCatchment_ModeledCatchmentID
            HasRequired(a => a.OwnerOrganization).WithMany(b => b.TreatmentBMPsWhereYouAreTheOwnerOrganization).HasForeignKey(c => c.OwnerOrganizationID).WillCascadeOnDelete(false); // FK_TreatmentBMP_Organization_OwnerOrganizationID_OrganizationID
            HasOptional(a => a.WaterQualityManagementPlan).WithMany(b => b.TreatmentBMPs).HasForeignKey(c => c.WaterQualityManagementPlanID).WillCascadeOnDelete(false); // FK_TreatmentBMP_WaterQualityManagementPlan_WaterQualityManagementPlanID
            HasOptional(a => a.InventoryVerifiedByPerson).WithMany(b => b.TreatmentBMPsWhereYouAreTheInventoryVerifiedByPerson).HasForeignKey(c => c.InventoryVerifiedByPersonID).WillCascadeOnDelete(false); // FK_TreatmentBMP_Person_InventoryVerifiedByPersonID_PersonID
            HasOptional(a => a.Delineation).WithMany(b => b.TreatmentBMPs).HasForeignKey(c => c.DelineationID).WillCascadeOnDelete(false); // FK_TreatmentBMP_Delineation_DelineationID
        }
    }
}