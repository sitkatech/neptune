//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ModeledCatchmentGeometryStaging]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class ModeledCatchmentGeometryStagingConfiguration : EntityTypeConfiguration<ModeledCatchmentGeometryStaging>
    {
        public ModeledCatchmentGeometryStagingConfiguration() : this("dbo"){}

        public ModeledCatchmentGeometryStagingConfiguration(string schema)
        {
            ToTable("ModeledCatchmentGeometryStaging", schema);
            HasKey(x => x.ModeledCatchmentGeometryStagingID);
            Property(x => x.ModeledCatchmentGeometryStagingID).HasColumnName(@"ModeledCatchmentGeometryStagingID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TenantID).HasColumnName(@"TenantID").HasColumnType("int").IsRequired();
            Property(x => x.PersonID).HasColumnName(@"PersonID").HasColumnType("int").IsRequired();
            Property(x => x.FeatureClassName).HasColumnName(@"FeatureClassName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(255);
            Property(x => x.GeoJson).HasColumnName(@"GeoJson").HasColumnType("varchar").IsRequired();
            Property(x => x.SelectedProperty).HasColumnName(@"SelectedProperty").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.ShouldImport).HasColumnName(@"ShouldImport").HasColumnType("bit").IsRequired();

            // Foreign keys
            HasRequired(a => a.Person).WithMany(b => b.ModeledCatchmentGeometryStagings).HasForeignKey(c => c.PersonID).WillCascadeOnDelete(false); // FK_ModeledCatchmentGeometryStaging_Person_PersonID
        }
    }
}