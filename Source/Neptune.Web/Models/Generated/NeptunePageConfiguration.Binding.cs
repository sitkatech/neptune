//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePage]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class NeptunePageConfiguration : EntityTypeConfiguration<NeptunePage>
    {
        public NeptunePageConfiguration() : this("dbo"){}

        public NeptunePageConfiguration(string schema)
        {
            ToTable("NeptunePage", schema);
            HasKey(x => x.NeptunePageID);
            Property(x => x.NeptunePageID).HasColumnName(@"NeptunePageID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.NeptunePageTypeID).HasColumnName(@"NeptunePageTypeID").HasColumnType("int").IsRequired();
            Property(x => x.NeptunePageContent).HasColumnName(@"NeptunePageContent").HasColumnType("varchar").IsOptional();

            // Foreign keys

        }
    }
}