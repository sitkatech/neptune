using Newtonsoft.Json;

namespace Neptune.Web.Areas.Modeling.Models.Nereid
{
    public class Node
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        public Node(string id)
        {
            ID = id;
        }

        public Node()
        {
        }
    }
}