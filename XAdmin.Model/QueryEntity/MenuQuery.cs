namespace XAdmin.Model.QueryEntity
{
    public class MenuQuery:QueryBase
    {
        public int MenuID { get; set; }

        public int SortID { get; set; }

        public string Icon { get; set; }

        public int Status { get; set;}

        public string MenuName { get; set; }

        public string MenuUrl { get; set; }

        /// <summary>
        /// 修改字段类型
        /// </summary>
        public string UpdateField { get; set; }

        /// <summary>
        /// 字段值
        /// </summary>
        public string UpdateFieldValue { get; set; }
    }
}
