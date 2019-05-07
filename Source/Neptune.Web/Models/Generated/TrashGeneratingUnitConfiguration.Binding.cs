//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashGeneratingUnit]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class TrashGeneratingUnitConfiguration : EntityTypeConfiguration<TrashGeneratingUnit>
    {
        public TrashGeneratingUnitConfiguration() : this("dbo"){}

        public TrashGeneratingUnitConfiguration(string schema)
        {
            ToTable("TrashGeneratingUnit", schema);
            HasKey(x => x.TrashGeneratingUnitID);
            Property(x => x.TrashGeneratingUnitID).HasColumnName(@"TrashGeneratingUnitID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.StormwaterJurisdictionID).HasColumnName(@"StormwaterJurisdictionID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPID).HasColumnName(@"TreatmentBMPID").HasColumnType("int").IsOptional();
            Property(x => x.OnlandVisualTrashAssessmentAreaID).HasColumnName(@"OnlandVisualTrashAssessmentAreaID").HasColumnType("int").IsOptional();
            Property(x => x.LandUseBlockID).HasColumnName(@"LandUseBlockID").HasColumnType("int").IsOptional();
            Property(x => x.TrashGeneratingUnitGeometry).HasColumnName(@"TrashGeneratingUnitGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.LastUpdateDate).HasColumnName(@"LastUpdateDate").HasColumnType("datetime").IsOptional();

            // Foreign keys
            HasRequired(a => a.StormwaterJurisdiction).WithMany(b => b.TrashGeneratingUnits).HasForeignKey(c => c.StormwaterJurisdictionID).WillCascadeOnDelete(false); // FK_TrashGeneratingUnit_StormwaterJurisdiction_StormwaterJurisdictionID
            HasOptional(a => a.TreatmentBMP).WithMany(b => b.TrashGeneratingUnits).HasForeignKey(c => c.TreatmentBMPID).WillCascadeOnDelete(false); // FK_TrashGeneratingUnit_TreatmentBMP_TreatmentBMPID
            HasOptional(a => a.OnlandVisualTrashAssessmentArea).WithMany(b => b.TrashGeneratingUnits).HasForeignKey(c => c.OnlandVisualTrashAssessmentAreaID).WillCascadeOnDelete(false); // FK_TrashGeneratingUnit_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID
            HasOptional(a => a.LandUseBlock).WithMany(b => b.TrashGeneratingUnits).HasForeignKey(c => c.LandUseBlockID).WillCascadeOnDelete(false); // FK_TrashGeneratingUnit_LandUseBlock_LandUseBlockID
        }
    }
}