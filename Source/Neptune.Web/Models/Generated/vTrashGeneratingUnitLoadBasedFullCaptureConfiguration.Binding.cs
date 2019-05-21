//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vTrashGeneratingUnitLoadBasedFullCapture]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vTrashGeneratingUnitLoadBasedFullCaptureConfiguration : EntityTypeConfiguration<vTrashGeneratingUnitLoadBasedFullCapture>
    {
        public vTrashGeneratingUnitLoadBasedFullCaptureConfiguration() : this("dbo"){}

        public vTrashGeneratingUnitLoadBasedFullCaptureConfiguration(string schema)
        {
            ToTable("vTrashGeneratingUnitLoadBasedFullCapture", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
        }
    }
}