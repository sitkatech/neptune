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
            Property(x => x.DrainID).HasColumnName(@"DrainID").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10);
            Property(x => x.Watershed).HasColumnName(@"Watershed").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);
            Property(x => x.CatchmentGeometry).HasColumnName(@"CatchmentGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.OCSurveyCatchmentID).HasColumnName(@"OCSurveyCatchmentID").HasColumnType("int").IsRequired();
            Property(x => x.OCSurveyDownstreamCatchmentID).HasColumnName(@"OCSurveyDownstreamCatchmentID").HasColumnType("int").IsOptional();
            Property(x => x.CatchmentGeometry4326).HasColumnName(@"CatchmentGeometry4326").HasColumnType("geometry").IsOptional();

            // Foreign keys
            HasOptional(a => a.OCSurveyDownstreamCatchment).WithMany(b => b.NetworkCatchmentsWhereYouAreTheOCSurveyDownstreamCatchment).HasForeignKey(c => c.OCSurveyDownstreamCatchmentID).WillCascadeOnDelete(false); // FK_NetworkCatchment_NetworkCatchment_OCSurveyDownstreamCatchmentID_OCSurveyCatchmentID
        }
    }
}