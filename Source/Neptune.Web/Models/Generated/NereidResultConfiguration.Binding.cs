//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NereidResult]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class NereidResultConfiguration : EntityTypeConfiguration<NereidResult>
    {
        public NereidResultConfiguration() : this("dbo"){}

        public NereidResultConfiguration(string schema)
        {
            ToTable("NereidResult", schema);
            HasKey(x => x.NereidResultID);
            Property(x => x.NereidResultID).HasColumnName(@"NereidResultID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TreatmentBMPID).HasColumnName(@"TreatmentBMPID").HasColumnType("int").IsOptional();
            Property(x => x.WaterQualityManagementPlanID).HasColumnName(@"WaterQualityManagementPlanID").HasColumnType("int").IsOptional();
            Property(x => x.RegionalSubbasinID).HasColumnName(@"RegionalSubbasinID").HasColumnType("int").IsOptional();
            Property(x => x.DelineationID).HasColumnName(@"DelineationID").HasColumnType("int").IsOptional();
            Property(x => x.NodeID).HasColumnName(@"NodeID").HasColumnType("varchar").IsOptional();
            Property(x => x.FullResponse).HasColumnName(@"FullResponse").HasColumnType("varchar").IsRequired();
            Property(x => x.LastUpdate).HasColumnName(@"LastUpdate").HasColumnType("datetime").IsOptional();

        }
    }
}