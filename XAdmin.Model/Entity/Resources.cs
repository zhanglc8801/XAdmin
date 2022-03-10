using System;
namespace XAdmin.Model.Entity
{
    ///<summary>
    ///
    ///</summary>
    public partial class Resources
    {
        public Resources()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long ID { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SiteID { get; set; }

        /// <summary>
        /// Desc:文件名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FileName { get; set; }

        /// <summary>
        /// 客户端文件名
        /// </summary>
        public string ClientFileName { get; set; }

        /// <summary>
        /// Desc:文件类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FileType { get; set; }

        /// <summary>
        /// Desc:文件大小
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long? FileSize { get; set; }

        /// <summary>
        /// Desc:文件MD5值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FileMD5 { get; set; }

        public long LinkFileID { get; set; }

        /// <summary>
        /// Desc:文件分类
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FileClass { get; set; }

        /// <summary>
        /// Desc:存储路径
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SavePath { get; set; }

        /// <summary>
        /// Desc:上传日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? UploadDate { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Remark { get; set; }

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

    }
}
