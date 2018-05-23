using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class PhotosViewData : FieldVisitSectionViewData
    {
        public PhotosViewData(Person currentPerson, Models.FieldVisit fieldVisit) : base(currentPerson, fieldVisit,
            "Photos") // todo: handle subsection name elegantly
        {
        }
    }
}