using System.ComponentModel.DataAnnotations;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.Web.Models
{
    public static class CustomAttributeTypeModelExtensions
    {
        //public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(t => t.DeleteCustomAttributeType(UrlTemplate.Parameter1Int)));
        //public static string GetDeleteUrl(this CustomAttributeType customAttributeType)
        //{
        //    return DeleteUrlTemplate.ParameterReplace(customAttributeType.CustomAttributeTypeID);
        //}

        //public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(t => t.Edit(UrlTemplate.Parameter1Int)));
        //public static string GetEditUrl(this CustomAttributeType customAttributeType)
        //{
        //    return EditUrlTemplate.ParameterReplace(customAttributeType.CustomAttributeTypeID);
        //}

        //public static HtmlString GetDisplayNameAsUrl(this CustomAttributeType customAttributeType)
        //{
        //    return customAttributeType != null ? UrlTemplate.MakeHrefString(customAttributeType.GetDetailUrl(), customAttributeType.CustomAttributeTypeName) : new HtmlString(null);
        //}

        //public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        //public static string GetDetailUrl(this CustomAttributeType customAttributeType)
        //{
        //    return customAttributeType == null ? "" : DetailUrlTemplate.ParameterReplace(customAttributeType.CustomAttributeTypeID);
        //}

        public static List<ValidationResult> CheckCustomAttributeTypeExpectations(List<CustomAttributeUpsertDto> customAttributeSimples, NeptuneDbContext dbContext)
        {
            var customAttributeTypes = CustomAttributeTypes.GetCustomAttributeTypes(dbContext, customAttributeSimples);
            var errors = new List<ValidationResult>();
            foreach (var customAttributeSimple in customAttributeSimples.Where(x =>
                x.CustomAttributeValues != null && x.CustomAttributeValues.Count > 0))
            {
                var customAttributeType = customAttributeTypes.Single(x =>
                    x.CustomAttributeTypeID == customAttributeSimple.CustomAttributeTypeID);

                var customAttributeDataType = customAttributeType.CustomAttributeDataType;

                foreach (var value in customAttributeSimple.CustomAttributeValues)
                {
                    if (!string.IsNullOrWhiteSpace(value) && !customAttributeDataType.ValueIsCorrectDataType(value))
                    {
                        errors.Add(new ValidationResult(
                            $"Entered value for {customAttributeType.CustomAttributeTypeName} does not match expected type ({customAttributeDataType.CustomAttributeDataTypeDisplayName})."));
                    }
                }
            }

            return errors;
        }
    }
}