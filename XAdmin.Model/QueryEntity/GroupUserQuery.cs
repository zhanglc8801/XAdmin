namespace XAdmin.Model.QueryEntity
{
    public class GroupUserQuery:QueryBase
    {
        public int GroupID { get; set; }

        public string GroupName { get; set; }

        public string UserName { get; set; }

        public string RealName { get; set; }

        public string Mobile { get; set; }

        public int RoleId { get; set; }
    }
}
