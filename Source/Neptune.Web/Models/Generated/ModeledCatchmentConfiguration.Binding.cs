//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ModeledCatchment]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class ModeledCatchmentConfiguration : EntityTypeConfiguration<ModeledCatchment>
    {
        public ModeledCatchmentConfiguration() : this("dbo"){}

        public ModeledCatchmentConfiguration(string schema)
        {
            ToTable("ModeledCatchment", schema);
            HasKey(x => x.ModeledCatchmentID);
            Property(x => x.ModeledCatchmentID).HasColumnName(@"ModeledCatchmentID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ModeledCatchmentName).HasColumnName(@"ModeledCatchmentName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);
            Property(x => x.StormwaterJurisdictionID).HasColumnName(@"StormwaterJurisdictionID").HasColumnType("int").IsRequired();
            Property(x => x.Notes).HasColumnName(@"Notes").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(1000);
            Property(x => x.ModeledCatchmentGeometry).HasColumnName(@"ModeledCatchmentGeometry").HasColumnType("geometry").IsOptional();

            // Foreign keys
            HasRequired(a => a.StormwaterJurisdiction).WithMany(b => b.ModeledCatchments).HasForeignKey(c => c.StormwaterJurisdictionID).WillCascadeOnDelete(false); // FK_ModeledCatchment_StormwaterJurisdiction_StormwaterJurisdictionID
        }
    }
}