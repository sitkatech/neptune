//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasinStaging]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class RegionalSubbasinStagingConfiguration : EntityTypeConfiguration<RegionalSubbasinStaging>
    {
        public RegionalSubbasinStagingConfiguration() : this("dbo"){}

        public RegionalSubbasinStagingConfiguration(string schema)
        {
            ToTable("RegionalSubbasinStaging", schema);
            HasKey(x => x.RegionalSubbasinStagingID);
            Property(x => x.RegionalSubbasinStagingID).HasColumnName(@"RegionalSubbasinStagingID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.DrainID).HasColumnName(@"DrainID").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(10);
            Property(x => x.Watershed).HasColumnName(@"Watershed").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.CatchmentGeometry).HasColumnName(@"CatchmentGeometry").HasColumnType("geometry").IsOptional();
            Property(x => x.OCSurveyCatchmentID).HasColumnName(@"OCSurveyCatchmentID").HasColumnType("int").IsOptional();
            Property(x => x.OCSurveyDownstreamCatchmentID).HasColumnName(@"OCSurveyDownstreamCatchmentID").HasColumnType("int").IsOptional();

        }
    }
}