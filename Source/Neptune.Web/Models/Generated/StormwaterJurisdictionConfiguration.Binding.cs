//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdiction]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class StormwaterJurisdictionConfiguration : EntityTypeConfiguration<StormwaterJurisdiction>
    {
        public StormwaterJurisdictionConfiguration() : this("dbo"){}

        public StormwaterJurisdictionConfiguration(string schema)
        {
            ToTable("StormwaterJurisdiction", schema);
            HasKey(x => x.StormwaterJurisdictionID);
            Property(x => x.StormwaterJurisdictionID).HasColumnName(@"StormwaterJurisdictionID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.OrganizationID).HasColumnName(@"OrganizationID").HasColumnType("int").IsRequired();
            Property(x => x.StormwaterJurisdictionGeometry).HasColumnName(@"StormwaterJurisdictionGeometry").HasColumnType("geometry").IsOptional();
            Property(x => x.StateProvinceID).HasColumnName(@"StateProvinceID").HasColumnType("int").IsRequired();
            Property(x => x.IsTransportationJurisdiction).HasColumnName(@"IsTransportationJurisdiction").HasColumnType("bit").IsRequired();

            // Foreign keys
            HasRequired(a => a.Organization).WithMany(b => b.StormwaterJurisdictions).HasForeignKey(c => c.OrganizationID).WillCascadeOnDelete(false); // FK_StormwaterJurisdiction_Organization_OrganizationID
            HasRequired(a => a.StateProvince).WithMany(b => b.StormwaterJurisdictions).HasForeignKey(c => c.StateProvinceID).WillCascadeOnDelete(false); // FK_StormwaterJurisdiction_StateProvince_StateProvinceID
        }
    }
}