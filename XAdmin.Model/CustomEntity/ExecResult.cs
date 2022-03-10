using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAdmin.Model.Enum;

namespace XAdmin.Model.CustomEntity
{
    public class ExecResult
    {
        /// <summary>
        /// 执行结果 1=成功 0=失败 -1=错误
        /// </summary>
        public int code
        {
            get;
            set;
        }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string msg
        {
            get;
            set;
        }
        /// <summary>
        /// 返回ID
        /// </summary>
        public int ReturnInt { get; set; }

        /// <summary>
        /// 返回ID
        /// </summary>
        public Int64 ReturnInt64 { get; set; }
    }
}
