//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LSPCBasinStaging]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class LSPCBasinStagingConfiguration : EntityTypeConfiguration<LSPCBasinStaging>
    {
        public LSPCBasinStagingConfiguration() : this("dbo"){}

        public LSPCBasinStagingConfiguration(string schema)
        {
            ToTable("LSPCBasinStaging", schema);
            HasKey(x => x.LSPCBasinStagingID);
            Property(x => x.LSPCBasinStagingID).HasColumnName(@"LSPCBasinStagingID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.LSPCBasinKey).HasColumnName(@"LSPCBasinKey").HasColumnType("int").IsRequired();
            Property(x => x.LSPCBasinName).HasColumnName(@"LSPCBasinName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);
            Property(x => x.LSPCBasinGeometry).HasColumnName(@"LSPCBasinGeometry").HasColumnType("geometry").IsRequired();

        }
    }
}