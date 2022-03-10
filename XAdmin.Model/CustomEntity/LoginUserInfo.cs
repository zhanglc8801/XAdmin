using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XAdmin.Model.CustomEntity
{
    public class LoginUserInfo
    {
        public UserInfoModel UserModel { get; set; }
        public List<LoginRoleModel> RoleList { get; set; }
        public LoginRoleModel CurRole { get; set; }
    }
    public class UserInfoModel
    {
        /// <summary>
        /// Desc:用户ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long ID { get; set; }

        /// <summary>
        /// Desc:站点ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SiteID { get; set; }

        /// <summary>
        /// Desc:用户名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserName { get; set; }

        /// <summary>
        /// Desc:用户类型 1=管理员 2=普通用户
        /// Default:2
        /// Nullable:True
        /// </summary>           
        public byte UserType { get; set; }

        /// <summary>
        /// Desc:真实姓名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RealName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Desc:性别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte Gender { get; set; }

        /// <summary>
        /// Desc:出生日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Desc:手机号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Mobile { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginCount { get; set; }

        /// <summary>
        /// 最后登录角色
        /// </summary>
        public int LoginRole { get; set; }

        /// <summary>
        /// Desc:状态 1=启用 0=停用
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte Status { get; set; }

        /// <summary>
        /// 是否自动登录
        /// </summary>
        public int IsAutoLogin { get; set; }

        public UserInfoModel()
        {
            this.ID = 0;
            this.UserName = string.Empty;
            this.Status = 0;
            this.UserType = 0;
            this.Photo = "/Content/Images/nullphoto.jpg";
        }
    }

    public class LoginRoleModel
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public LoginRoleModel()
        {
            this.RoleID = 0;
            this.RoleName = string.Empty;
        }
    }
}
