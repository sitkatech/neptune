//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ModelBasin]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class ModelBasinConfiguration : EntityTypeConfiguration<ModelBasin>
    {
        public ModelBasinConfiguration() : this("dbo"){}

        public ModelBasinConfiguration(string schema)
        {
            ToTable("ModelBasin", schema);
            HasKey(x => x.ModelBasinID);
            Property(x => x.ModelBasinID).HasColumnName(@"ModelBasinID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ModelBasinKey).HasColumnName(@"ModelBasinKey").HasColumnType("int").IsRequired();
            Property(x => x.ModelBasinName).HasColumnName(@"ModelBasinName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);
            Property(x => x.ModelBasinGeometry).HasColumnName(@"ModelBasinGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.LastUpdate).HasColumnName(@"LastUpdate").HasColumnType("datetime").IsRequired();

            // Foreign keys

        }
    }
}