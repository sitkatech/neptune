//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vMostRecentTreatmentBMPAssessment]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vMostRecentTreatmentBMPAssessmentConfiguration : EntityTypeConfiguration<vMostRecentTreatmentBMPAssessment>
    {
        public vMostRecentTreatmentBMPAssessmentConfiguration() : this("dbo"){}

        public vMostRecentTreatmentBMPAssessmentConfiguration(string schema)
        {
            ToTable("vMostRecentTreatmentBMPAssessment", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
            
            
            
            
            
            
            
            
        }
    }
}