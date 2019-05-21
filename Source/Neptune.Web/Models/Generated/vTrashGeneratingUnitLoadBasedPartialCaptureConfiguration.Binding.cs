//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vTrashGeneratingUnitLoadBasedPartialCapture]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vTrashGeneratingUnitLoadBasedPartialCaptureConfiguration : EntityTypeConfiguration<vTrashGeneratingUnitLoadBasedPartialCapture>
    {
        public vTrashGeneratingUnitLoadBasedPartialCaptureConfiguration() : this("dbo"){}

        public vTrashGeneratingUnitLoadBasedPartialCaptureConfiguration(string schema)
        {
            ToTable("vTrashGeneratingUnitLoadBasedPartialCapture", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
            
        }
    }
}