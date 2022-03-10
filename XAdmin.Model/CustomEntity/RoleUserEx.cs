using System.Collections.Generic;
using XAdmin.Model.Entity;
namespace XAdmin.Model.CustomEntity
{
    public class RoleUserEx:PageBase
    {
        public List<RoleUserList> list { get; set; }
        public List<UserInfo> UserList { get; set; }
    }

    public class RoleUserList: RoleUser
    {
        public string UserName { get; set; }

        public string RealName { get; set; }

        public byte? Gender { get; set; }

        public string Mobile { get; set; }

        public string EMail { get; set; }

        public byte? Status { get; set; }

        public string GenderStr
        {
            get
            {
                if (Gender == 1)
                    return "男";
                else if (Gender == 2)
                    return "女";
                else
                    return "";
            }
        }

        public string StatusStr
        {
            get
            {
                if (Status == 1)
                    return "<span style='color:green;'>正常</span>";
                else
                    return "<span style='color:red;'>已禁用</span>";
            }
        }
    }

}
