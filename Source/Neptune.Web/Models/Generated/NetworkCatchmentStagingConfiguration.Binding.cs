//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NetworkCatchmentStaging]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class NetworkCatchmentStagingConfiguration : EntityTypeConfiguration<NetworkCatchmentStaging>
    {
        public NetworkCatchmentStagingConfiguration() : this("dbo"){}

        public NetworkCatchmentStagingConfiguration(string schema)
        {
            ToTable("NetworkCatchmentStaging", schema);
            HasKey(x => x.NetworkCatchmentStagingID);
            Property(x => x.NetworkCatchmentStagingID).HasColumnName(@"NetworkCatchmentStagingID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.DrainID).HasColumnName(@"DrainID").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(10);
            Property(x => x.Watershed).HasColumnName(@"Watershed").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.CatchmentGeometry).HasColumnName(@"CatchmentGeometry").HasColumnType("geometry").IsOptional();
            Property(x => x.OCSurveyCatchmentID).HasColumnName(@"OCSurveyCatchmentID").HasColumnType("int").IsOptional();
            Property(x => x.OCSurveyDownstreamCatchmentID).HasColumnName(@"OCSurveyDownstreamCatchmentID").HasColumnType("int").IsOptional();

        }
    }
}