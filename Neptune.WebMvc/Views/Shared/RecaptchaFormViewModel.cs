using Neptune.WebMvc.Common.Models;

namespace Neptune.WebMvc.Views.Shared;

public class RecaptchaFormViewModel : FormViewModel
{
    public string RecaptchaToken { get; set; }
}