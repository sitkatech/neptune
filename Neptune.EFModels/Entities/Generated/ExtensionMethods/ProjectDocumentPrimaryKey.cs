//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectDocument


namespace Neptune.EFModels.Entities
{
    public class ProjectDocumentPrimaryKey : EntityPrimaryKey<ProjectDocument>
    {
        public ProjectDocumentPrimaryKey() : base(){}
        public ProjectDocumentPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectDocumentPrimaryKey(ProjectDocument projectDocument) : base(projectDocument){}

        public static implicit operator ProjectDocumentPrimaryKey(int primaryKeyValue)
        {
            return new ProjectDocumentPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectDocumentPrimaryKey(ProjectDocument projectDocument)
        {
            return new ProjectDocumentPrimaryKey(projectDocument);
        }
    }
}