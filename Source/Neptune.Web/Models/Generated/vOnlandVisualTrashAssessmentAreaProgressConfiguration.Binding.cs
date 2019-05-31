//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vOnlandVisualTrashAssessmentAreaProgress]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vOnlandVisualTrashAssessmentAreaProgressConfiguration : EntityTypeConfiguration<vOnlandVisualTrashAssessmentAreaProgress>
    {
        public vOnlandVisualTrashAssessmentAreaProgressConfiguration() : this("dbo"){}

        public vOnlandVisualTrashAssessmentAreaProgressConfiguration(string schema)
        {
            ToTable("vOnlandVisualTrashAssessmentAreaProgress", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
        }
    }
}