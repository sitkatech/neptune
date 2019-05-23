//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashGeneratingUnitAdjustment]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class TrashGeneratingUnitAdjustmentConfiguration : EntityTypeConfiguration<TrashGeneratingUnitAdjustment>
    {
        public TrashGeneratingUnitAdjustmentConfiguration() : this("dbo"){}

        public TrashGeneratingUnitAdjustmentConfiguration(string schema)
        {
            ToTable("TrashGeneratingUnitAdjustment", schema);
            HasKey(x => x.TrashGeneratingUnitAdjustmentID);
            Property(x => x.TrashGeneratingUnitAdjustmentID).HasColumnName(@"TrashGeneratingUnitAdjustmentID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.AdjustedDelineationID).HasColumnName(@"AdjustedDelineationID").HasColumnType("int").IsOptional();
            Property(x => x.AdjustedOnlandVisualTrashAssessmentAreaID).HasColumnName(@"AdjustedOnlandVisualTrashAssessmentAreaID").HasColumnType("int").IsOptional();
            Property(x => x.DeletedGeometry).HasColumnName(@"DeletedGeometry").HasColumnType("geometry").IsOptional();
            Property(x => x.AdjustmentDate).HasColumnName(@"AdjustmentDate").HasColumnType("datetime").IsRequired();
            Property(x => x.AdjustedByPersonID).HasColumnName(@"AdjustedByPersonID").HasColumnType("int").IsRequired();
            Property(x => x.IsProcessed).HasColumnName(@"IsProcessed").HasColumnType("bit").IsRequired();
            Property(x => x.ProcessedDate).HasColumnName(@"ProcessedDate").HasColumnType("datetime").IsOptional();

            // Foreign keys
            HasRequired(a => a.AdjustedByPerson).WithMany(b => b.TrashGeneratingUnitAdjustmentsWhereYouAreTheAdjustedByPerson).HasForeignKey(c => c.AdjustedByPersonID).WillCascadeOnDelete(false); // FK_TrashGeneratingUnitAdjustment_Person_AdjustedByPersonID_PersonID
        }
    }
}