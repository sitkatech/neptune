//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vModelingResultUnitConversion]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vModelingResultUnitConversionConfiguration : EntityTypeConfiguration<vModelingResultUnitConversion>
    {
        public vModelingResultUnitConversionConfiguration() : this("dbo"){}

        public vModelingResultUnitConversionConfiguration(string schema)
        {
            ToTable("vModelingResultUnitConversion", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
        }
    }
}