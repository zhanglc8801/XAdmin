using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace XAdmin.Model.CustomEntity
{
    public class RoleMenuEx
    {
        private List<RoleMenuModel> _menuList { get; set; }
        public List<RoleMenuModel> menuList
        {
            get
            {
                for (int i = 0; i < _menuList.Count; i++)
                {
                    for (int j = 0; j < menuAnyList.Count; j++)
                    {
                        if (_menuList[i].MenuID == menuAnyList[j].MenuID)
                            _menuList[i].CHECKED = "true";

                        if (_menuList[i].PMenuID == 0)
                            _menuList[i].open = true;
                    }
                }
                return _menuList;
            }
            set { _menuList = value; }
        }

        public List<RoleMenuModel> menuAnyList { get; set; }


    }
    [DataContract, Serializable]
    public class RoleMenuModel
    {

        public int MenuID { get; set; }
        [DataMember]
        public int id
        {
            get
            {
                return MenuID;
            }
            set { MenuID = value; }
        }

        [DataMember]
        public string SiteID { get; set; }

        public int PMenuID { get; set; }

        [DataMember]
        public int pId
        {
            get
            {
                return PMenuID;
            }
            set { PMenuID = value; }
        }

        public string MenuName { get; set; }

        [DataMember]
        public string name
        {
            get
            {
                return MenuName;
            }
            set { MenuName = value; }
        }

        [DataMember(Name = "checked")]
        public string CHECKED { get; set; }

        [DataMember]
        public bool open { get; set; }

        [DataMember]
        public string MenuUrl { get; set; }

        [DataMember]
        public string Icon { get; set; }

        [DataMember]
        public string IconColor { get; set; }

        /// <summary>
        /// 菜单权限字符串
        /// </summary>
        [DataMember]
        public string AuthStr { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [DataMember]
        public int RoleID { get; set; }
    }

}
