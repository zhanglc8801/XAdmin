namespace XAdmin.Model.QueryEntity
{
    public class SetRoleMenu:QueryBase
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// 菜单ID列表
        /// </summary>
        public string MenuIDStr { get; set; }
    }
}
