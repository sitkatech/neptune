//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.FileResource


namespace Neptune.EFModels.Entities
{
    public class FileResourcePrimaryKey : EntityPrimaryKey<FileResource>
    {
        public FileResourcePrimaryKey() : base(){}
        public FileResourcePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public FileResourcePrimaryKey(FileResource fileResource) : base(fileResource){}

        public static implicit operator FileResourcePrimaryKey(int primaryKeyValue)
        {
            return new FileResourcePrimaryKey(primaryKeyValue);
        }

        public static implicit operator FileResourcePrimaryKey(FileResource fileResource)
        {
            return new FileResourcePrimaryKey(fileResource);
        }
    }
}