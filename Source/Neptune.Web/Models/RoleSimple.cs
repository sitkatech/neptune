namespace Neptune.Web.Models
{
    public class RoleSimple
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public RoleSimple()
        {
        }

        public int RoleID { get; set; }
        public string RoleDisplayName { get; set; }

        public RoleSimple(Role role)
        {
            RoleID = role.RoleID;
            RoleDisplayName = role.RoleDisplayName;
        }
    }
}