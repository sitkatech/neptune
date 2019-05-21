//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vTrashGeneratingUnitLoadBasedTrashAssessment]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vTrashGeneratingUnitLoadBasedTrashAssessmentConfiguration : EntityTypeConfiguration<vTrashGeneratingUnitLoadBasedTrashAssessment>
    {
        public vTrashGeneratingUnitLoadBasedTrashAssessmentConfiguration() : this("dbo"){}

        public vTrashGeneratingUnitLoadBasedTrashAssessmentConfiguration(string schema)
        {
            ToTable("vTrashGeneratingUnitLoadBasedTrashAssessment", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
            
        }
    }
}