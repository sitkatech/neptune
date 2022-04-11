//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ModelBasinStaging]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class ModelBasinStagingConfiguration : EntityTypeConfiguration<ModelBasinStaging>
    {
        public ModelBasinStagingConfiguration() : this("dbo"){}

        public ModelBasinStagingConfiguration(string schema)
        {
            ToTable("ModelBasinStaging", schema);
            HasKey(x => x.ModelBasinStagingID);
            Property(x => x.ModelBasinStagingID).HasColumnName(@"ModelBasinStagingID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ModelBasinKey).HasColumnName(@"ModelBasinKey").HasColumnType("int").IsRequired();
            Property(x => x.ModelBasinGeometry).HasColumnName(@"ModelBasinGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.ModelBasinState).HasColumnName(@"ModelBasinState").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(5);
            Property(x => x.ModelBasinRegion).HasColumnName(@"ModelBasinRegion").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10);

        }
    }
}