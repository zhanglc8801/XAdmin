namespace XAdmin.Model.CustomEntity
{
    public class PageBase
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// 每页显示大小
        /// </summary>
        public int PageSize { get; set; }

        private int totalPage = 0;
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            get
            {
                if (totalPage == 0 && PageSize > 0)
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

        /// <summary>
        /// 分页字符串
        /// </summary>
        public string PageInfo { get; set; }
    }
}
