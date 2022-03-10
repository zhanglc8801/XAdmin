using XAdmin.Common.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace XAdmin.Common.Security
{
    public class TicketTool
    {
        private const string COOKIEKEY = "XAdmin_COM";
        //private const string COOKIENAME = "FASTPLATUSERCOOKIE";
        private const string COOKIEPATH = "/";

        public class Team
        {
            public string UserID { get; set; }
        }


        /// <summary>
        /// 创建一个票据，放在cookie中
        /// 票据中的数据经过加密，解决了cookie的安全问题。
        /// </summary>
        /// <param name="userID">员工ID</param>
        /// <param name="userData">员工实体类json字符串</param>
        public static void SetCookie(string userID, string userData)
        {
            HttpContextAccessor _context = new HttpContextAccessor();
            var cookie = _context.HttpContext.Request.Cookies[COOKIEKEY];            
            Team team = JsonConvert.DeserializeObject<Team>(userData);
            string UserID = team.UserID;

            //防止中文乱码
            userData = System.Web.HttpUtility.UrlEncode(userData);
            //加密
            userData = DES.Encrypt(userData);
            string domain = SiteConfigManager.SiteDomain;
            string CookieExpires= ConfigurationManager.GetJson("Strings", "CookieExpires");//Cookie过期日期

            if (cookie == null)
            {
                CookieOptions co = new CookieOptions();
                co.Domain = domain;
                co.Expires = DateTimeOffset.Now.AddDays(double.Parse(CookieExpires));
                co.Path = COOKIEPATH;
                _context.HttpContext.Response.Cookies.Append(COOKIEKEY, userData);

            }
        }

        /// <summary>
        /// 创建一个票据，放在cookie中
        /// 票据中的数据经过加密，解决了cookie的安全问题。
        /// </summary>
        /// <param name="userID">员工ID</param>
        /// <param name="userData">员工实体类json字符串</param>
        public static void SetCookie(string userID, string userData, bool autologin)
        {
            HttpContextAccessor _context = new HttpContextAccessor();
            var cookie = _context.HttpContext.Request.Cookies[COOKIEKEY];
            Team team = JsonConvert.DeserializeObject<Team>(userData);
            string UserID = team.UserID;

            //防止中文乱码
            userData = System.Web.HttpUtility.UrlEncode(userData);
            //加密
            userData = DES.Encrypt(userData);
            string domain = SiteConfigManager.SiteDomain;
            string CookieExpires = ConfigurationManager.GetJson("Strings", "CookieExpires");//Cookie过期日期

            if (cookie == null)
            {
                if(autologin)
                    CookieHelper.SetCookie(COOKIEKEY, userData, int.Parse(CookieExpires));
                else
                    CookieHelper.SetCookie(COOKIEKEY, userData,1);

            }
        }

        

        /// <summary>
        /// 通过此法判断登录
        /// </summary>
        /// <returns>已登录返回true</returns>
        public static bool IsLogin()
        {
            HttpContextAccessor _context = new HttpContextAccessor();
            string cookie = _context.HttpContext.Request.Cookies[COOKIEKEY];

            if (cookie != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        public static void Logout()
        {
            CookieHelper.SetCookie(COOKIEKEY, "", -31);
        }

        /// <summary>
        /// 取得票据中数据
        /// </summary>
        /// <returns></returns>
        public static string GetUserData()
        {
            HttpContextAccessor _context = new HttpContextAccessor();
            string cookie = _context.HttpContext.Request.Cookies[COOKIEKEY];
            string val = "";
            if (cookie != null)
            {
                //解密
                val = DES.Decrypt(cookie);
                //防止中文乱码
                val = System.Web.HttpUtility.UrlDecode(val);
                return val;
            }
            else
            {
                return "";
            }
        }
    }
}
