using XAdmin.Model.Entity;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace XAdmin.Model.CustomEntity
{
    public class MenuEx
    {
        /// <summary>
        /// 
        /// </summary>
        public List<TreeItem> data { get; set; }

        public List<Menu> list { get; set; }

    }

    public class TreeItem
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string treeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string createDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<childTreeItem> treeList { get; set; }
    }

    public class childTreeItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int pid { get; set; }

        /// <summary>
        /// 增加菜单
        /// </summary>
        public string treeName { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string createDate { get; set; }
    }

    

   

}
