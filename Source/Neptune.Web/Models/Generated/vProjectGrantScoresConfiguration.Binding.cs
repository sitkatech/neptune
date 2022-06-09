//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vProjectGrantScores]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vProjectGrantScoresConfiguration : EntityTypeConfiguration<vProjectGrantScores>
    {
        public vProjectGrantScoresConfiguration() : this("dbo"){}

        public vProjectGrantScoresConfiguration(string schema)
        {
            ToTable("vProjectGrantScores", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
            
            
            
            
            
            
            
            
            
        }
    }
}