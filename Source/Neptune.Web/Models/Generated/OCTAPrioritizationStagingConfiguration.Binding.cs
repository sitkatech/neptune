//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OCTAPrioritizationStaging]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class OCTAPrioritizationStagingConfiguration : EntityTypeConfiguration<OCTAPrioritizationStaging>
    {
        public OCTAPrioritizationStagingConfiguration() : this("dbo"){}

        public OCTAPrioritizationStagingConfiguration(string schema)
        {
            ToTable("OCTAPrioritizationStaging", schema);
            HasKey(x => x.OCTAPrioritizationStagingID);
            Property(x => x.OCTAPrioritizationStagingID).HasColumnName(@"OCTAPrioritizationStagingID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.OCTAPrioritizationKey).HasColumnName(@"OCTAPrioritizationKey").HasColumnType("int").IsRequired();
            Property(x => x.OCTAPrioritizationGeometry).HasColumnName(@"OCTAPrioritizationGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.Watershed).HasColumnName(@"Watershed").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(80);
            Property(x => x.CatchIDN).HasColumnName(@"CatchIDN").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(80);
            Property(x => x.TPI).HasColumnName(@"TPI").HasColumnType("float").IsRequired();
            Property(x => x.WQNLU).HasColumnName(@"WQNLU").HasColumnType("float").IsRequired();
            Property(x => x.WQNMON).HasColumnName(@"WQNMON").HasColumnType("float").IsRequired();
            Property(x => x.IMPAIR).HasColumnName(@"IMPAIR").HasColumnType("float").IsRequired();
            Property(x => x.MON).HasColumnName(@"MON").HasColumnType("float").IsRequired();
            Property(x => x.SEA).HasColumnName(@"SEA").HasColumnType("float").IsRequired();
            Property(x => x.SEA_PCTL).HasColumnName(@"SEA_PCTL").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(80);
            Property(x => x.PC_VOL_PCT).HasColumnName(@"PC_VOL_PCT").HasColumnType("float").IsRequired();
            Property(x => x.PC_NUT_PCT).HasColumnName(@"PC_NUT_PCT").HasColumnType("float").IsRequired();
            Property(x => x.PC_BAC_PCT).HasColumnName(@"PC_BAC_PCT").HasColumnType("float").IsRequired();
            Property(x => x.PC_MET_PCT).HasColumnName(@"PC_MET_PCT").HasColumnType("float").IsRequired();
            Property(x => x.PC_TSS_PCT).HasColumnName(@"PC_TSS_PCT").HasColumnType("float").IsRequired();

        }
    }
}