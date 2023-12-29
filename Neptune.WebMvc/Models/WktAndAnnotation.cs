namespace Neptune.WebMvc.Models
{
    public class WktAndAnnotation
    {
        public WktAndAnnotation(string wkt, string annotation)
        {
            Wkt = wkt;
            Annotation = annotation;
        }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public WktAndAnnotation()
        {
        }

        public string Wkt { get; set; }
        public string Annotation { get; set; }
    }
}