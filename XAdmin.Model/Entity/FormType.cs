using System;
using System.Linq;
using System.Text;

namespace XAdmin.Model.Entity
{
    ///<summary>
    ///
    ///</summary>
    public partial class FormType
    {
        public FormType()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int ID { get; set; }


        public string SiteID { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FormTypeName { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Note { get; set; }

    }
}
