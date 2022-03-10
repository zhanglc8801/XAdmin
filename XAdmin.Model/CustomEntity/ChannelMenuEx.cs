using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace XAdmin.Model.CustomEntity
{
    public class ChannelMenuEx
    {
        public List<ChannelMenuModel> menuList { get; set; }
    }

    [DataContract, Serializable]
    public class ChannelMenuModel
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string SiteID { get; set; }

        [DataMember]
        public int ParentID { get; set; }

        [DataMember]
        public int pId
        {
            get
            {
                return ParentID;
            }
            set { ParentID = value; }
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember(Name = "checked")]
        public string CHECKED { get; set; }

        [DataMember]
        public bool open { get { return true; } set { } }

        [DataMember]
        public string RoleIDs { get; set; }

        [DataMember]
        public bool IsNav { get; set; }

        [DataMember]
        public int ContentType { get; set; }

        //内容类型 0=仅栏目 1=描述类 2=列表类 3=图片类 4=音频类 5=视频类 6=文件类
        [DataMember]
        public string ContentTypeName {
            get {
                if (ContentType == 0)
                    return "栏目目录";
                else if (ContentType == 1)
                    return "描述类栏目";
                else if (ContentType == 2)
                    return "列表类栏目";
                else if (ContentType == 3)
                    return "图片类栏目";
                else if (ContentType == 4)
                    return "音频类栏目";
                else if (ContentType == 5)
                    return "视频类栏目";
                else if (ContentType == 6)
                    return "文件类栏目";
                else
                    return "";

            }
        }

        [DataMember]
        public string Keywords { get; set; }

        [DataMember]
        public string Description { get; set; }

    }

}
