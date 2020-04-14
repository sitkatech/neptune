//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vNereidRegionalSubbasinCentralizedBMP]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vNereidRegionalSubbasinCentralizedBMPConfiguration : EntityTypeConfiguration<vNereidRegionalSubbasinCentralizedBMP>
    {
        public vNereidRegionalSubbasinCentralizedBMPConfiguration() : this("dbo"){}

        public vNereidRegionalSubbasinCentralizedBMPConfiguration(string schema)
        {
            ToTable("vNereidRegionalSubbasinCentralizedBMP", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
            
        }
    }
}