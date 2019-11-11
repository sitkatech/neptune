//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Neighborhood]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class NeighborhoodConfiguration : EntityTypeConfiguration<Neighborhood>
    {
        public NeighborhoodConfiguration() : this("dbo"){}

        public NeighborhoodConfiguration(string schema)
        {
            ToTable("Neighborhood", schema);
            HasKey(x => x.NeighborhoodID);
            Property(x => x.NeighborhoodID).HasColumnName(@"NeighborhoodID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.DrainID).HasColumnName(@"DrainID").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10);
            Property(x => x.Watershed).HasColumnName(@"Watershed").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);
            Property(x => x.NeighborhoodGeometry).HasColumnName(@"NeighborhoodGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.OCSurveyNeighborhoodID).HasColumnName(@"OCSurveyNeighborhoodID").HasColumnType("int").IsRequired();
            Property(x => x.OCSurveyDownstreamNeighborhoodID).HasColumnName(@"OCSurveyDownstreamNeighborhoodID").HasColumnType("int").IsOptional();
            Property(x => x.NeighborhoodGeometry4326).HasColumnName(@"NeighborhoodGeometry4326").HasColumnType("geometry").IsOptional();

            // Foreign keys
            HasOptional(a => a.OCSurveyDownstreamNeighborhood).WithMany(b => b.NeighborhoodsWhereYouAreTheOCSurveyDownstreamNeighborhood).HasForeignKey(c => c.OCSurveyDownstreamNeighborhoodID).WillCascadeOnDelete(false); // FK_Neighborhood_Neighborhood_OCSurveyDownstreamNeighborhoodID_OCSurveyNeighborhoodID
        }
    }
}