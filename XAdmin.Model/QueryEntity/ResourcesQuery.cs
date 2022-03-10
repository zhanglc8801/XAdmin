using System;
namespace XAdmin.Model.QueryEntity
{
    public class ResourcesQuery:QueryBase
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long ID { get; set; }

        public string IDs { get; set; }

        /// <summary>
        /// Desc:文件名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FileName { get; set; }

        /// <summary>
        /// Desc:文件类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FileType { get; set; }

        /// <summary>
        /// Desc:文件MD5值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FileMD5 { get; set; }

        /// <summary>
        /// Desc:文件分类
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FileClass { get; set; }

        /// <summary>
        /// Desc:上传日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? UploadDate { get; set; }

        /// <summary>
        /// Desc:是否删除
        /// Default:
        /// Nullable:True
        /// </summary>           
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// Desc:上传用户ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long? UserId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
