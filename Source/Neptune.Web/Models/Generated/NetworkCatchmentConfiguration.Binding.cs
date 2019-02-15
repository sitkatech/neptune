//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NetworkCatchment]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class NetworkCatchmentConfiguration : EntityTypeConfiguration<NetworkCatchment>
    {
        public NetworkCatchmentConfiguration() : this("dbo"){}

        public NetworkCatchmentConfiguration(string schema)
        {
            ToTable("NetworkCatchment", schema);
            HasKey(x => x.NetworkCatchmentID);
            Property(x => x.NetworkCatchmentID).HasColumnName(@"NetworkCatchmentID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.OCSurveyCatchmentID).HasColumnName(@"OCSurveyCatchmentID").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10);
            Property(x => x.DownstreamCatchmentID).HasColumnName(@"DownstreamCatchmentID").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10);
            Property(x => x.DrainID).HasColumnName(@"DrainID").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10);
            Property(x => x.Watershed).HasColumnName(@"Watershed").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);
            Property(x => x.CatchmentGeometry).HasColumnName(@"CatchmentGeometry").HasColumnType("geometry").IsRequired();

        }
    }
}