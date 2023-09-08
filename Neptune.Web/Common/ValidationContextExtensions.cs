using System.ComponentModel.DataAnnotations;

namespace Neptune.Web.Common;

public static class ValidationContextExtensions
{
    public static T GetService<T>(this ValidationContext validationContext)
    {
        return (T)validationContext.GetService(typeof(T));
    }
}