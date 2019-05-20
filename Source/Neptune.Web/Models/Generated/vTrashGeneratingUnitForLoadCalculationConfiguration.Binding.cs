//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vTrashGeneratingUnitForLoadCalculation]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vTrashGeneratingUnitForLoadCalculationConfiguration : EntityTypeConfiguration<vTrashGeneratingUnitForLoadCalculation>
    {
        public vTrashGeneratingUnitForLoadCalculationConfiguration() : this("dbo"){}

        public vTrashGeneratingUnitForLoadCalculationConfiguration(string schema)
        {
            ToTable("vTrashGeneratingUnitForLoadCalculation", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
            
            
            
        }
    }
}