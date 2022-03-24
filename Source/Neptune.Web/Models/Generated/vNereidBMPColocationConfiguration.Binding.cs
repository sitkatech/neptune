//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vNereidBMPColocation]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vNereidBMPColocationConfiguration : EntityTypeConfiguration<vNereidBMPColocation>
    {
        public vNereidBMPColocationConfiguration() : this("dbo"){}

        public vNereidBMPColocationConfiguration(string schema)
        {
            ToTable("vNereidBMPColocation", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
            
        }
    }
}