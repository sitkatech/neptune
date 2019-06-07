//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DelineationGeometryStaging]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class DelineationGeometryStagingConfiguration : EntityTypeConfiguration<DelineationGeometryStaging>
    {
        public DelineationGeometryStagingConfiguration() : this("dbo"){}

        public DelineationGeometryStagingConfiguration(string schema)
        {
            ToTable("DelineationGeometryStaging", schema);
            HasKey(x => x.DelineationGeometryStagingID);
            Property(x => x.DelineationGeometryStagingID).HasColumnName(@"DelineationGeometryStagingID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.DelineationGeometryStagingGeometry).HasColumnName(@"DelineationGeometryStagingGeometry").HasColumnType("varchar").IsRequired();
            Property(x => x.PersonID).HasColumnName(@"PersonID").HasColumnType("int").IsRequired();
            Property(x => x.FeatureClassName).HasColumnName(@"FeatureClassName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(255);
            Property(x => x.SelectedProperty).HasColumnName(@"SelectedProperty").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.ShouldImport).HasColumnName(@"ShouldImport").HasColumnType("bit").IsRequired();

            // Foreign keys
            HasRequired(a => a.Person).WithMany(b => b.DelineationGeometryStagings).HasForeignKey(c => c.PersonID).WillCascadeOnDelete(false); // FK_DelineationGeometryStaging_Person_PersonID
        }
    }
}