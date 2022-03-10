using System.Collections.Generic;
using XAdmin.Model.Entity;
namespace XAdmin.Model.CustomEntity
{
    public class GroupUserEx
    {
        public List<GroupUserList> list { get; set; }

        public List<UserInfoEx> UserList { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecords { get; set; }

        public int PageSize { get; set; }

        private int totalPage = 0;
        public int TotalPage
        {
            get
            {
                if (totalPage == 0)
                {
                    totalPage = (int)TotalRecords / PageSize;
                    if ((TotalRecords % PageSize) > 0)
                        totalPage++;
                }
                return totalPage;
            }
            set
            {
                totalPage = 0;
            }
        }


        public string PageInfo { get; set; }
    }

    public class GroupUserList : GroupUser
    {
        public string GroupName { get; set; }

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
