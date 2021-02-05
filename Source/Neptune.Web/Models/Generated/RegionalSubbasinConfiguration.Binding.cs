//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasin]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class RegionalSubbasinConfiguration : EntityTypeConfiguration<RegionalSubbasin>
    {
        public RegionalSubbasinConfiguration() : this("dbo"){}

        public RegionalSubbasinConfiguration(string schema)
        {
            ToTable("RegionalSubbasin", schema);
            HasKey(x => x.RegionalSubbasinID);
            Property(x => x.RegionalSubbasinID).HasColumnName(@"RegionalSubbasinID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.DrainID).HasColumnName(@"DrainID").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(10);
            Property(x => x.Watershed).HasColumnName(@"Watershed").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.CatchmentGeometry).HasColumnName(@"CatchmentGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.OCSurveyCatchmentID).HasColumnName(@"OCSurveyCatchmentID").HasColumnType("int").IsRequired();
            Property(x => x.OCSurveyDownstreamCatchmentID).HasColumnName(@"OCSurveyDownstreamCatchmentID").HasColumnType("int").IsOptional();
            Property(x => x.CatchmentGeometry4326).HasColumnName(@"CatchmentGeometry4326").HasColumnType("geometry").IsOptional();
            Property(x => x.LastUpdate).HasColumnName(@"LastUpdate").HasColumnType("datetime").IsOptional();
            Property(x => x.IsWaitingForLGURefresh).HasColumnName(@"IsWaitingForLGURefresh").HasColumnType("bit").IsOptional();
            Property(x => x.IsInLSPCBasin).HasColumnName(@"IsInLSPCBasin").HasColumnType("bit").IsOptional();
            Property(x => x.LSPCBasinID).HasColumnName(@"LSPCBasinID").HasColumnType("int").IsOptional();

            // Foreign keys
            HasOptional(a => a.OCSurveyDownstreamCatchment).WithMany(b => b.RegionalSubbasinsWhereYouAreTheOCSurveyDownstreamCatchment).HasForeignKey(c => c.OCSurveyDownstreamCatchmentID).WillCascadeOnDelete(false); // FK_RegionalSubbasin_RegionalSubbasin_OCSurveyDownstreamCatchmentID_OCSurveyCatchmentID
            HasOptional(a => a.LSPCBasin).WithMany(b => b.RegionalSubbasins).HasForeignKey(c => c.LSPCBasinID).WillCascadeOnDelete(false); // FK_RegionalSubbasin_LSPCBasin_LSPCBasinID
        }
    }
}