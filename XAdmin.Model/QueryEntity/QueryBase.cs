using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XAdmin.Model.QueryEntity
{
    public class QueryBase
    {
        /// <summary>
        /// 省级行政区划ID
        /// </summary>
        public int ProvinceID { get; set; }

        /// <summary>
        /// SiteID
        /// </summary>
        public string SiteID { get; set; }

        /// <summary>
        /// 主键Key
        /// </summary>
        public int Int32Key { get; set; }

        /// <summary>
        /// 主键Key
        /// </summary>
        public Int64 Int64Key { get; set; }

        /// <summary>
        /// 多个以逗号进行分割的主键Key
        /// </summary>
        public string KeyStr { get; set; }

        /// <summary>
        /// 排序字段  如：Id
        /// </summary>
        public String OrderStr { get; set; }

        /// <summary>
        /// 排序方式 如：desc
        /// </summary>
        public string OrderSort
        {
            get;
            set;
        }

        /// <summary>
        /// Int值
        /// </summary>
        public int IntValue { get; set; }

        /// <summary>
        /// bool值
        /// </summary>
        public bool boolValue { get; set; }

        /// <summary>
        /// 修改字段类型
        /// </summary>
        public string UpdateField { get; set; }

        /// <summary>
        /// 字段值
        /// </summary>
        public string UpdateFieldValue { get; set; }

        #region  分页

        private int pageSize = 10;
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                if (value <= 0)
                    pageSize = 10;
                else
                    pageSize = value;
            }
        }

        private int currentPage = 0;
        /// <summary>
        /// 当前页面（页号）
        /// </summary>
        public int CurrentPage
        {
            get
            {
                return currentPage;
            }
            set
            {
                if (value <= 0)
                    currentPage = 1;
                else
                    currentPage = value;
            }
        }

        /// <summary>
        /// 开始页码
        /// </summary>
        public Int32 StartIndex
        {
            get
            {
                return (CurrentPage - 1) * PageSize + 1;
            }
        }

        /// <summary>
        /// 结束页码
        /// </summary>
        public Int32 EndIndex
        {
            get
            {
                return CurrentPage * PageSize;
            }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public Int32 TotalCount { get; set; }

        #endregion
    }
}
