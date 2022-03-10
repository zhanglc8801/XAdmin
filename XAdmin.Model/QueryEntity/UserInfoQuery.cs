
namespace XAdmin.Model.QueryEntity
{
    public class UserInfoQuery : QueryBase
    {
        /// <summary>
        /// Desc:用户ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long ID { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Desc:用户名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserName { get; set; }

        /// <summary>
        /// Desc:真实姓名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RealName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

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
        /// Desc:状态 1=启用 0=停用
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte? Status { get; set; }

        /// <summary>
        /// 验证标识
        /// </summary>
        public string lastChanged { get; set; }

        public UserInfoQuery()
        {
            CurrentPage = 1;
            PageSize = 10;
            UserName = string.Empty;
            RealName = string.Empty;
            EMail = string.Empty;
            Mobile = string.Empty;
            Gender = 0;
            Status =2;
            RoleId = 0;
        }
            

    }
}
