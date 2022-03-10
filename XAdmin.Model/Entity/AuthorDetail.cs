using System;
namespace XAdmin.Model.Entity
{
    ///<summary>
    ///作者详细信息
    ///</summary>
    public partial class AuthorDetail
    {
           public AuthorDetail(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long PKID {get;set;}

           /// <summary>
           /// Desc:作者信息
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long AuthorID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long JournalID {get;set;}

           /// <summary>
           /// Desc:作者姓名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string AuthorName {get;set;}

           /// <summary>
           /// Desc:作者英文名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string EnglishName {get;set;}

           /// <summary>
           /// Desc:性别，1=男 2=女
           /// Default:1
           /// Nullable:False
           /// </summary>           
           public byte Gender {get;set;}

           /// <summary>
           /// Desc:民族
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Nation {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? Birthday {get;set;}

           /// <summary>
           /// Desc:籍贯
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string NativePlace {get;set;}

           /// <summary>
           /// Desc:省份
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Province {get;set;}

           /// <summary>
           /// Desc:城市
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string City {get;set;}

           /// <summary>
           /// Desc:区县
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Area {get;set;}

           /// <summary>
           /// Desc:身份证号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string IDCard {get;set;}

           /// <summary>
           /// Desc:联系地址
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Address {get;set;}

           /// <summary>
           /// Desc:邮编
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ZipCode {get;set;}

           /// <summary>
           /// Desc:手机号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Mobile {get;set;}

           /// <summary>
           /// Desc:办公电话
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Tel {get;set;}

           /// <summary>
           /// Desc:传真
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Fax {get;set;}

           /// <summary>
           /// Desc:学历，数据字典
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int Education {get;set;}

           /// <summary>
           /// Desc:专业
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Professional {get;set;}

           /// <summary>
           /// Desc:职称，数据字典
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int JobTitle {get;set;}

           /// <summary>
           /// Desc:职务
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Job {get;set;}

           /// <summary>
           /// Desc:研究方向
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ResearchTopics {get;set;}

           /// <summary>
           /// Desc:工作单位
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string WorkUnit {get;set;}

           /// <summary>
           /// Desc:工作单位级别，可以用来设置数据字典，例如：三甲、二甲等
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int WorkUnitLevel {get;set;}

           /// <summary>
           /// Desc:科室
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string SectionOffice {get;set;}

           /// <summary>
           /// Desc:发票抬头
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string InvoiceUnit {get;set;}

           /// <summary>
           /// Desc:导师
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Mentor {get;set;}

           /// <summary>
           /// Desc:QQ
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string QQ {get;set;}

           /// <summary>
           /// Desc:MSN
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string MSN {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Remark {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AuthorPhoto {get;set;}

           /// <summary>
           /// Desc:作者著作
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AuthorOpus {get;set;}

           /// <summary>
           /// Desc:备用字段
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ReserveField {get;set;}

           /// <summary>
           /// Desc:备用字段
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ReserveField1 {get;set;}

           /// <summary>
           /// Desc:备用字段
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ReserveField2 {get;set;}

           /// <summary>
           /// Desc:备用字段
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ReserveField3 {get;set;}

           /// <summary>
           /// Desc:备用字段
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ReserveField4 {get;set;}

           /// <summary>
           /// Desc:备用字段
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ReserveField5 {get;set;}

           /// <summary>
           /// Desc:添加日期
           /// Default:DateTime.Now
           /// Nullable:False
           /// </summary>           
           public DateTime AddDate {get;set;}

    }
}
