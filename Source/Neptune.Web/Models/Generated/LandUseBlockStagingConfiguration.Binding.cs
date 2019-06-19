//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandUseBlockStaging]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class LandUseBlockStagingConfiguration : EntityTypeConfiguration<LandUseBlockStaging>
    {
        public LandUseBlockStagingConfiguration() : this("dbo"){}

        public LandUseBlockStagingConfiguration(string schema)
        {
            ToTable("LandUseBlockStaging", schema);
            HasKey(x => x.LandUseBlockStagingID);
            Property(x => x.LandUseBlockStagingID).HasColumnName(@"LandUseBlockStagingID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.PriorityLandUseType).HasColumnName(@"PriorityLandUseType").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.LandUseDescription).HasColumnName(@"LandUseDescription").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.LandUseBlockStagingGeometry).HasColumnName(@"LandUseBlockStagingGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.TrashGenerationRate).HasColumnName(@"TrashGenerationRate").HasColumnType("decimal").IsRequired().HasPrecision(4,1);
            Property(x => x.LandUseForTGR).HasColumnName(@"LandUseForTGR").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(80);
            Property(x => x.MedianHouseholdIncome).HasColumnName(@"MedianHouseholdIncome").HasColumnType("decimal").IsRequired();
            Property(x => x.StormwaterJurisdiction).HasColumnName(@"StormwaterJurisdiction").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(255);
            Property(x => x.PermitType).HasColumnName(@"PermitType").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.UploadedByPersonID).HasColumnName(@"UploadedByPersonID").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.UploadedByPerson).WithMany(b => b.LandUseBlockStagingsWhereYouAreTheUploadedByPerson).HasForeignKey(c => c.UploadedByPersonID).WillCascadeOnDelete(false); // FK_LandUseBlockStaging_Person_UploadedByPersonID_PersonID
        }
    }
}