using System;
using System.Collections.Generic;
using System.Text;

namespace XAdmin.Model.QueryEntity
{
    /// <summary>
    /// 网站栏目内容通用查询实体类
    /// </summary>
    public class SiteChannelQuery:QueryBase
    {
        /// <summary>
        /// 内容类型 1=描述类 2=列表类 3=图片类 4=音频类 5=视频类 6=文件类
        /// </summary>
        public int ContentType { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Abstruct { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 状态 1=启用 0=停用
        /// </summary>
        public bool? Status { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastTime { get; set; }
    }
}
