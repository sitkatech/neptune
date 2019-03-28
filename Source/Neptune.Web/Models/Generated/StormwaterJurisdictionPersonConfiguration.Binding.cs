//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdictionPerson]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class StormwaterJurisdictionPersonConfiguration : EntityTypeConfiguration<StormwaterJurisdictionPerson>
    {
        public StormwaterJurisdictionPersonConfiguration() : this("dbo"){}

        public StormwaterJurisdictionPersonConfiguration(string schema)
        {
            ToTable("StormwaterJurisdictionPerson", schema);
            HasKey(x => x.StormwaterJurisdictionPersonID);
            Property(x => x.StormwaterJurisdictionPersonID).HasColumnName(@"StormwaterJurisdictionPersonID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.StormwaterJurisdictionID).HasColumnName(@"StormwaterJurisdictionID").HasColumnType("int").IsRequired();
            Property(x => x.PersonID).HasColumnName(@"PersonID").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.StormwaterJurisdiction).WithMany(b => b.StormwaterJurisdictionPeople).HasForeignKey(c => c.StormwaterJurisdictionID).WillCascadeOnDelete(false); // FK_StormwaterJurisdictionPerson_StormwaterJurisdiction_StormwaterJurisdictionID
            HasRequired(a => a.Person).WithMany(b => b.StormwaterJurisdictionPeople).HasForeignKey(c => c.PersonID).WillCascadeOnDelete(false); // FK_StormwaterJurisdictionPerson_Person_PersonID
        }
    }
}