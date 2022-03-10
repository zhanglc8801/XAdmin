using System;
namespace XAdmin.Model.Entity
{
    ///<summary>
    ///
    ///</summary>
    public partial class UserInfo
    {
        public UserInfo()
        {

            this.Photo = "";
            this.RealName = "";
            this.LastChanged = "";

        }
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
        /// Desc:密码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Pwd { get; set; }

        /// <summary>
        /// Desc:用户类型 1=管理员 2=普通用户
        /// Default:2
        /// Nullable:True
        /// </summary>           
        public byte? UserType { get; set; }

        /// <summary>
        /// Desc:真实姓名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RealName { get; set; }

        /// <summary>
        /// 所在地区ID
        /// </summary>
        public int AreaID { get; set; }

        /// <summary>
        /// Desc:头像
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Photo { get; set; }

        /// <summary>
        /// Desc:性别 1=男 2=女
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte? Gender { get; set; }

        /// <summary>
        /// Desc:邮箱
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string EMail { get; set; }

        /// <summary>
        /// Desc:手机号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Mobile { get; set; }

        /// <summary>
        /// Desc:最后登录IP
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string LoginIP { get; set; }

        /// <summary>
        /// Desc:登录次数
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? LoginCount { get; set; }

        /// <summary>
        /// Desc:最后登录日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? LoginDate { get; set; }

        /// <summary>
        /// Desc:最后登录角色
        /// Default:0
        /// Nullable:True
        /// </summary>           
        public int? LoginRole { get; set; }

        /// <summary>
        /// Desc:状态 1=启用 0=停用
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte? Status { get; set; }

        /// <summary>
        /// Desc:创建日期
        /// Default:DateTime.Now
        /// Nullable:True
        /// </summary>           
        public DateTime? AddDate { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Remark { get; set; }

        public string LastChanged { get; set; }

    }
}
