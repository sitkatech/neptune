//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[BackboneSegment]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class BackboneSegmentConfiguration : EntityTypeConfiguration<BackboneSegment>
    {
        public BackboneSegmentConfiguration() : this("dbo"){}

        public BackboneSegmentConfiguration(string schema)
        {
            ToTable("BackboneSegment", schema);
            HasKey(x => x.BackboneSegmentID);
            Property(x => x.BackboneSegmentID).HasColumnName(@"BackboneSegmentID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.BackboneSegmentGeometry).HasColumnName(@"BackboneSegmentGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.CatchIDN).HasColumnName(@"CatchIDN").HasColumnType("int").IsRequired();
            Property(x => x.NetworkCatchmentID).HasColumnName(@"NetworkCatchmentID").HasColumnType("int").IsOptional();
            Property(x => x.BackboneSegmentTypeID).HasColumnName(@"BackboneSegmentTypeID").HasColumnType("int").IsRequired();
            Property(x => x.DownstreamBackboneSegmentID).HasColumnName(@"DownstreamBackboneSegmentID").HasColumnType("int").IsOptional();

            // Foreign keys
            HasOptional(a => a.NetworkCatchment).WithMany(b => b.BackboneSegments).HasForeignKey(c => c.NetworkCatchmentID).WillCascadeOnDelete(false); // FK_BackboneSegment_NetworkCatchment_NetworkCatchmentID
            HasOptional(a => a.DownstreamBackboneSegment).WithMany(b => b.BackboneSegmentsWhereYouAreTheDownstreamBackboneSegment).HasForeignKey(c => c.DownstreamBackboneSegmentID).WillCascadeOnDelete(false); // FK_BackboneSegment_BackboneSegment_DownstreamBackboneSegmentID_BackboneSegmentID
        }
    }
}