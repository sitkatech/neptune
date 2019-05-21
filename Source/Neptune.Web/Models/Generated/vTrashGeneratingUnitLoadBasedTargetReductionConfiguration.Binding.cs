//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vTrashGeneratingUnitLoadBasedTargetReduction]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vTrashGeneratingUnitLoadBasedTargetReductionConfiguration : EntityTypeConfiguration<vTrashGeneratingUnitLoadBasedTargetReduction>
    {
        public vTrashGeneratingUnitLoadBasedTargetReductionConfiguration() : this("dbo"){}

        public vTrashGeneratingUnitLoadBasedTargetReductionConfiguration(string schema)
        {
            ToTable("vTrashGeneratingUnitLoadBasedTargetReduction", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
        }
    }
}