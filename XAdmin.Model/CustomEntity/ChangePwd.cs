namespace XAdmin.Model.CustomEntity
{
    /// <summary>
    /// 更改密码实体类
    /// </summary>
    public class ChangePwd
    {
        /// <summary>
        /// 站点ID
        /// </summary>
        public string SiteID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPwd { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPwd { get; set; }
    }
}
