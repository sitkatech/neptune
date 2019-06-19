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
            Property(x => x.UploadedByPersonID).HasColumnName(@"UploadedByPersonID").HasColumnType("int").IsOptional();

            // Foreign keys
            HasOptional(a => a.UploadedByPerson).WithMany(b => b.DelineationStagingsWhereYouAreTheUploadedByPerson).HasForeignKey(c => c.UploadedByPersonID).WillCascadeOnDelete(false); // FK_DelineationStaging_Person_UploadedByPersonID_PersonID
        }
    }
}