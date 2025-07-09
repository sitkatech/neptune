namespace Neptune.Models.DataTransferObjects
{
    public class RoleSimpleDto
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleDisplayName { get; set; }
        public int PeopleWithRoleCount { get; set; }
    }
}
