//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LSPCBasin]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class LSPCBasinConfiguration : EntityTypeConfiguration<LSPCBasin>
    {
        public LSPCBasinConfiguration() : this("dbo"){}

        public LSPCBasinConfiguration(string schema)
        {
            ToTable("LSPCBasin", schema);
            HasKey(x => x.LSPCBasinID);
            Property(x => x.LSPCBasinID).HasColumnName(@"LSPCBasinID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.LSPCBasinKey).HasColumnName(@"LSPCBasinKey").HasColumnType("int").IsRequired();
            Property(x => x.LSPCBasinName).HasColumnName(@"LSPCBasinName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);
            Property(x => x.LSPCBasinGeometry).HasColumnName(@"LSPCBasinGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.LastUpdate).HasColumnName(@"LastUpdate").HasColumnType("datetime").IsRequired();

            // Foreign keys

        }
    }
}