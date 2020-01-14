//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DelineationOverlap]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class DelineationOverlapConfiguration : EntityTypeConfiguration<DelineationOverlap>
    {
        public DelineationOverlapConfiguration() : this("dbo"){}

        public DelineationOverlapConfiguration(string schema)
        {
            ToTable("DelineationOverlap", schema);
            HasKey(x => x.DelineationOverlapID);
            Property(x => x.DelineationOverlapID).HasColumnName(@"DelineationOverlapID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.DelineationID).HasColumnName(@"DelineationID").HasColumnType("int").IsRequired();
            Property(x => x.OverlappingDelineationID).HasColumnName(@"OverlappingDelineationID").HasColumnType("int").IsRequired();
            Property(x => x.OverlappingGeometry).HasColumnName(@"OverlappingGeometry").HasColumnType("geometry").IsRequired();

            // Foreign keys
            HasRequired(a => a.Delineation).WithMany(b => b.DelineationOverlaps).HasForeignKey(c => c.DelineationID).WillCascadeOnDelete(false); // FK_DelineationOverlap_Delineation_DelineationID
            HasRequired(a => a.OverlappingDelineation).WithMany(b => b.DelineationOverlapsWhereYouAreTheOverlappingDelineation).HasForeignKey(c => c.OverlappingDelineationID).WillCascadeOnDelete(false); // FK_DelineationOverlap_Delineation_OverlappingDelineationID_DelineationID
        }
    }
}