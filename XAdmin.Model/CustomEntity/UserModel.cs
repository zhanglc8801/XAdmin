﻿using XAdmin.Model.Entity;
using System;
using System.Collections.Generic;

namespace XAdmin.Model.CustomEntity
{
    public class UserModel
    {
        public UserModel()
        {
            this.County = "";

        }

        /// <summary>
        /// Desc:用户ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long ID { get; set; }

        /// <summary>
        /// Desc:站点ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SiteID { get; set; }

        /// <summary>
        /// Desc:用户名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserName { get; set; }

        /// <summary>
        /// Desc:密码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Pwd { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Desc:用户类型 1=管理员 2=普通用户
        /// Default:2
        /// Nullable:True
        /// </summary>           
        public byte? UserType { get; set; }

        /// <summary>
        /// Desc:真实姓名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RealName { get; set; }

        public int AreaID { get; set; }

        /// <summary>
        /// Desc:性别 1=男 2=女
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte? Gender { get; set; }

        /// <summary>
        /// Desc:邮箱
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string EMail { get; set; }

        /// <summary>
        /// Desc:手机号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Mobile { get; set; }

        /// <summary>
        /// Desc:最后登录IP
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string LoginIP { get; set; }

        /// <summary>
        /// Desc:登录次数
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? LoginCount { get; set; }

        /// <summary>
        /// Desc:最后登录日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? LoginDate { get; set; }

        /// <summary>
        /// Desc:最后登录角色
        /// Default:0
        /// Nullable:True
        /// </summary>           
        public int? LoginRole { get; set; }

        /// <summary>
        /// Desc:状态 1=启用 0=停用
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte? Status { get; set; }

        /// <summary>
        /// Desc:创建日期
        /// Default:DateTime.Now
        /// Nullable:True
        /// </summary>           
        public DateTime? AddDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 用户角色列表
        /// </summary>
        public List<RoleUser> RoleUserList { get; set; }

        //========================================

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long PKID { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long? UserID { get; set; }

        /// <summary>
        /// Desc:所在省份
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Province { get; set; }

        /// <summary>
        /// Desc:所在城市
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string City { get; set; }

        /// <summary>
        /// Desc:所在区县
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string County { get; set; }

        /// <summary>
        /// Desc:民族
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Nation { get; set; }

        /// <summary>
        /// Desc:出生日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Desc:证件类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte? IDType { get; set; }

        /// <summary>
        /// Desc:证件号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string IDNo { get; set; }

        /// <summary>
        /// Desc:详细地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Address { get; set; }

        /// <summary>
        /// Desc:邮编
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZipCode { get; set; }

        /// <summary>
        /// Desc:工作单位
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string WorkUnit { get; set; }

        /// <summary>
        /// Desc:单位电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Tel { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Fox { get; set; }

        /// <summary>
        /// Desc:学历
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte? Education { get; set; }

        /// <summary>
        /// Desc:毕业院校
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string University { get; set; }

        /// <summary>
        /// Desc:专业
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Professional { get; set; }

        /// <summary>
        /// Desc:职称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string JobTitle { get; set; }

    }
}
