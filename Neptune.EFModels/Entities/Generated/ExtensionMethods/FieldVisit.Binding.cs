//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldVisit]
namespace Neptune.EFModels.Entities
{
    public partial class FieldVisit
    {
        public FieldVisitStatus FieldVisitStatus => FieldVisitStatus.AllLookupDictionary[FieldVisitStatusID];
        public FieldVisitType FieldVisitType => FieldVisitType.AllLookupDictionary[FieldVisitTypeID];
    }
}