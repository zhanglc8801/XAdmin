using System;
using System.Collections.Generic;
using System.Text;

namespace XAdmin.Model.CustomEntity
{
    /// <summary>
    /// 树形菜单下拉框实体类
    /// </summary>
    public class TreeSelectModel
    {
        public List<TreeSelectItem> TreeSelectList { get; set; }
    }

    #region MyRegion
    public class TreeSelectItem
    {
        public int id { get; set; }

        public string name { get; set; }

        public bool open { get; set; }

        public bool @checked { get; set; }

        public string icon { get; set; }

        public List<TreeSelectItem> children { get; set; }
    }
    #endregion

}
