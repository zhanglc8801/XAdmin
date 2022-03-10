using XAdmin.Common.Security;
using XAdmin.Common.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Web.Admin.Controllers
{
    public class VerifyCodeController : Controller
    {
        /// <summary>
        /// 验证码Key
        /// </summary>
        private const string VERFIYCODEKEY = "XADMIN_AUTH_VERFIFYCODE";

        /// <summary>
        /// 验证码
        /// </summary>
        public string VerifyCodeText
        {
            get
            {
                if (string.IsNullOrWhiteSpace(CookieHelper.GetCookie(VERFIYCODEKEY)))
                {
                    return CookieHelper.GetCookie(VERFIYCODEKEY);
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        public FileContentResult VerifyCode()
        {
            VerifyCode vCode = new VerifyCode();
            string randomcode = vCode.CreateRandomCode(4);
            CookieHelper.SetCookie(VERFIYCODEKEY, DES.Encrypt(randomcode.ToLower()),1);
            byte[] bytes = vCode.CreateImageToByte(randomcode, false);
            return File(bytes, @"image/gif");
        }
    }
}