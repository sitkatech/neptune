//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandUseBlock]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class LandUseBlockConfiguration : EntityTypeConfiguration<LandUseBlock>
    {
        public LandUseBlockConfiguration() : this("dbo"){}

        public LandUseBlockConfiguration(string schema)
        {
            ToTable("LandUseBlock", schema);
            HasKey(x => x.LandUseBlockID);
            Property(x => x.LandUseBlockID).HasColumnName(@"LandUseBlockID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.PriorityLandUseTypeID).HasColumnName(@"PriorityLandUseTypeID").HasColumnType("int").IsOptional();
            Property(x => x.LandUseDescription).HasColumnName(@"LandUseDescription").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.LandUseBlockGeometry).HasColumnName(@"LandUseBlockGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.TrashGenerationRate).HasColumnName(@"TrashGenerationRate").HasColumnType("decimal").IsOptional().HasPrecision(4,1);
            Property(x => x.LandUseForTGR).HasColumnName(@"LandUseForTGR").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(80);
            Property(x => x.MedianHouseholdIncomeResidential).HasColumnName(@"MedianHouseholdIncomeResidential").HasColumnType("decimal").IsOptional();
            Property(x => x.MedianHouseholdIncomeRetail).HasColumnName(@"MedianHouseholdIncomeRetail").HasColumnType("decimal").IsOptional();
            Property(x => x.StormwaterJurisdictionID).HasColumnName(@"StormwaterJurisdictionID").HasColumnType("int").IsRequired();
            Property(x => x.PermitTypeID).HasColumnName(@"PermitTypeID").HasColumnType("int").IsRequired();
            Property(x => x.LandUseBlockGeometry4326).HasColumnName(@"LandUseBlockGeometry4326").HasColumnType("geometry").IsOptional();

            // Foreign keys
            HasRequired(a => a.StormwaterJurisdiction).WithMany(b => b.LandUseBlocks).HasForeignKey(c => c.StormwaterJurisdictionID).WillCascadeOnDelete(false); // FK_LandUseBlock_StormwaterJurisdiction_StormwaterJurisdictionID
        }
    }
}