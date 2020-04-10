//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vNereidLoadingInput]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vNereidLoadingInputConfiguration : EntityTypeConfiguration<vNereidLoadingInput>
    {
        public vNereidLoadingInputConfiguration() : this("dbo"){}

        public vNereidLoadingInputConfiguration(string schema)
        {
            ToTable("vNereidLoadingInput", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
            
            
            
            
            
            
            
        }
    }
}