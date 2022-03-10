using System;
namespace XAdmin.Model.QueryEntity
{
    public class AddGroupUser: QueryBase
    {
        public int GroupID { get; set; }

        public long UserID { get; set; }

        public DateTime AddDate { get; set; }
    }
}
