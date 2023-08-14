using Neptune.EFModels.Entities;

namespace Neptune.Web.Models;

public static class FieldDefinitionTypeModelExtensions
{
    //private static readonly EnglishPluralizationService PluralizationService = new EnglishPluralizationService();

    public static FieldDefinition GetFieldDefinitionData(this FieldDefinitionType fieldDefinitionType, NeptuneDbContext dbContext)
    {
        return FieldDefinitions.GetFieldDefinitionByFieldDefinitionType(dbContext, fieldDefinitionType);
    }


    public static string GetContentUrl(this FieldDefinitionType fieldDefinitionType)
    {
        return ""; // todo: SitkaRoute<FieldDefinitionController>.BuildUrlFromExpression(x => x.FieldDefinitionDetails(FieldDefinitionTypeID));
    }


    public static bool HasCustomFieldDefinition(this FieldDefinitionType fieldDefinitionType, NeptuneDbContext dbContext)
    {
        var fieldDefinitionData = fieldDefinitionType.GetFieldDefinitionData(dbContext);
        return fieldDefinitionData is { FieldDefinitionValue: not null };
    }

    public static string GetFieldDefinitionLabelPluralized(this FieldDefinitionType fieldDefinitionType)
    {
        return fieldDefinitionType.FieldDefinitionTypeDisplayName + "s"; //TODO: move to db as FieldDefinitionTypeDisplayNamePluralized
    }
}