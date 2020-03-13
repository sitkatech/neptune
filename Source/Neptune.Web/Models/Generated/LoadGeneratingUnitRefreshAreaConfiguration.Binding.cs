//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LoadGeneratingUnitRefreshArea]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class LoadGeneratingUnitRefreshAreaConfiguration : EntityTypeConfiguration<LoadGeneratingUnitRefreshArea>
    {
        public LoadGeneratingUnitRefreshAreaConfiguration() : this("dbo"){}

        public LoadGeneratingUnitRefreshAreaConfiguration(string schema)
        {
            ToTable("LoadGeneratingUnitRefreshArea", schema);
            HasKey(x => x.LoadGeneratingUnitRefreshAreaID);
            Property(x => x.LoadGeneratingUnitRefreshAreaID).HasColumnName(@"LoadGeneratingUnitRefreshAreaID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.LoadGeneratingUnitRefreshAreaGeometry).HasColumnName(@"LoadGeneratingUnitRefreshAreaGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.ProcessDate).HasColumnName(@"ProcessDate").HasColumnType("datetime").IsOptional();

        }
    }
}