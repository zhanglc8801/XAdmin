namespace XAdmin.Model.Entity
{
    ///<summary>
    ///
    ///</summary>
    public partial class DesContentAtt
    {
           public DesContentAtt(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long OID {get;set;}

           /// <summary>
           /// Desc:描述类内容ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? DesID {get;set;}

           /// <summary>
           /// Desc:内容
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Content {get;set;}

           /// <summary>
           /// Desc:附件列表
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Files {get;set;}

    }
}
