//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DelineationStaging]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class DelineationStagingConfiguration : EntityTypeConfiguration<DelineationStaging>
    {
        public DelineationStagingConfiguration() : this("dbo"){}

        public DelineationStagingConfiguration(string schema)
        {
            ToTable("DelineationStaging", schema);
            HasKey(x => x.DelineationStagingID);
            Property(x => x.DelineationStagingID).HasColumnName(@"DelineationStagingID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.DelineationStagingGeometry).HasColumnName(@"DelineationStagingGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.IsVerified).HasColumnName(@"IsVerified").HasColumnType("bit").IsRequired();
            Property(x => x.DateLastVerified).HasColumnName(@"DateLastVerified").HasColumnType("datetime").IsOptional();
            Property(x => x.UploadedByPersonID).HasColumnName(@"UploadedByPersonID").HasColumnType("int").IsOptional();
            Property(x => x.TreatmentBMPName).HasColumnName(@"TreatmentBMPName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(200);
            Property(x => x.DateLastModified).HasColumnName(@"DateLastModified").HasColumnType("datetime").IsRequired();
            Property(x => x.StormwaterJurisdictionID).HasColumnName(@"StormwaterJurisdictionID").HasColumnType("int").IsRequired();

            // Foreign keys
            HasOptional(a => a.UploadedByPerson).WithMany(b => b.DelineationStagingsWhereYouAreTheUploadedByPerson).HasForeignKey(c => c.UploadedByPersonID).WillCascadeOnDelete(false); // FK_DelineationStaging_Person_UploadedByPersonID_PersonID
            HasRequired(a => a.StormwaterJurisdiction).WithMany(b => b.DelineationStagings).HasForeignKey(c => c.StormwaterJurisdictionID).WillCascadeOnDelete(false); // FK_DelineationStaging_StormwaterJurisdiction_StormwaterJurisdictionID
        }
    }
}