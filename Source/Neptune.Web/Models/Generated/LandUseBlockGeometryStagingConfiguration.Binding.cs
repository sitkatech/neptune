//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandUseBlockGeometryStaging]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class LandUseBlockGeometryStagingConfiguration : EntityTypeConfiguration<LandUseBlockGeometryStaging>
    {
        public LandUseBlockGeometryStagingConfiguration() : this("dbo"){}

        public LandUseBlockGeometryStagingConfiguration(string schema)
        {
            ToTable("LandUseBlockGeometryStaging", schema);
            HasKey(x => x.LandUseBlockGeometryStagingID);
            Property(x => x.LandUseBlockGeometryStagingID).HasColumnName(@"LandUseBlockGeometryStagingID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.PersonID).HasColumnName(@"PersonID").HasColumnType("int").IsRequired();
            Property(x => x.FeatureClassName).HasColumnName(@"FeatureClassName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(255);
            Property(x => x.LandUseBlockGeometryStagingGeoJson).HasColumnName(@"LandUseBlockGeometryStagingGeoJson").HasColumnType("varchar").IsRequired();
            Property(x => x.SelectedProperty).HasColumnName(@"SelectedProperty").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.ShouldImport).HasColumnName(@"ShouldImport").HasColumnType("bit").IsRequired();

            // Foreign keys
            HasRequired(a => a.Person).WithMany(b => b.LandUseBlockGeometryStagings).HasForeignKey(c => c.PersonID).WillCascadeOnDelete(false); // FK_LandUseBlockGeometryStaging_Person_PersonID
        }
    }
}