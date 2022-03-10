using System.Collections.Generic;
using XAdmin.Model.Entity;
namespace XAdmin.Model.CustomEntity
{
    public class UserInfoListModel: PageBase
    {
        public List<UserInfoEx> UserInfoList { get; set; }
    }

    public class UserInfoEx:UserInfo
    {
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
            set { }
        }

        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }

        public string StatusStr
        {
            get
            {
                if (Status == 1)
                    return "<span style='color:green;'>正常</span>";
                else
                    return "<span style='color:red;'>已禁用</span>";
            }
            set { }
        }

        public string AddDateStr
        {
            get {
                return AddDate == null ? "1970-01-01 12:00:00" : AddDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        public string LoginDateStr
        {
            get
            {
                return LoginDate == null ? "1970-01-01 12:00:00" : LoginDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
    }

}
