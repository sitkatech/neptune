//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandUseBlockGeomteryStaging]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class LandUseBlockGeomteryStagingConfiguration : EntityTypeConfiguration<LandUseBlockGeomteryStaging>
    {
        public LandUseBlockGeomteryStagingConfiguration() : this("dbo"){}

        public LandUseBlockGeomteryStagingConfiguration(string schema)
        {
            ToTable("LandUseBlockGeomteryStaging", schema);
            HasKey(x => x.LandUseBlockStagingID);
            Property(x => x.LandUseBlockStagingID).HasColumnName(@"LandUseBlockStagingID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.PersonID).HasColumnName(@"PersonID").HasColumnType("int").IsRequired();
            Property(x => x.FeatureClassName).HasColumnName(@"FeatureClassName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(255);
            Property(x => x.LandUseBlockStagingGeoJson).HasColumnName(@"LandUseBlockStagingGeoJson").HasColumnType("varchar").IsRequired();
            Property(x => x.SelectedProperty).HasColumnName(@"SelectedProperty").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.ShouldImport).HasColumnName(@"ShouldImport").HasColumnType("bit").IsRequired();

            // Foreign keys
            HasRequired(a => a.Person).WithMany(b => b.LandUseBlockGeomteryStagings).HasForeignKey(c => c.PersonID).WillCascadeOnDelete(false); // FK_LandUseBlockGeomteryStaging_Person_PersonID
        }
    }
}