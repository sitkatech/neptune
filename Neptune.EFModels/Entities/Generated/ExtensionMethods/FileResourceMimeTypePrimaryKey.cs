//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.FileResourceMimeType


namespace Neptune.EFModels.Entities
{
    public class FileResourceMimeTypePrimaryKey : EntityPrimaryKey<FileResourceMimeType>
    {
        public FileResourceMimeTypePrimaryKey() : base(){}
        public FileResourceMimeTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public FileResourceMimeTypePrimaryKey(FileResourceMimeType fileResourceMimeType) : base(fileResourceMimeType){}

        public static implicit operator FileResourceMimeTypePrimaryKey(int primaryKeyValue)
        {
            return new FileResourceMimeTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator FileResourceMimeTypePrimaryKey(FileResourceMimeType fileResourceMimeType)
        {
            return new FileResourceMimeTypePrimaryKey(fileResourceMimeType);
        }
    }
}