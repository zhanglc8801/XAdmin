using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace XAdmin.Common.Utils
{
    public class CookieHelper
    {
        /// <summary>
        /// 读Cookie值
        /// </summary>
        /// <param name="key">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string key)
        {
            HttpContextAccessor _context = new HttpContextAccessor();
            if (_context.HttpContext.Request.Cookies != null && _context.HttpContext.Request.Cookies[key] != null)
            {
                return _context.HttpContext.Request.Cookies[key];
            }
            return "";
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetCookie(string key,string value)
        {
            HttpContextAccessor _context = new HttpContextAccessor();
            _context.HttpContext.Response.Cookies.Append(key, value);
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="days">过期天数</param>
        public static void SetCookie(string key, string value,int days)
        {
            HttpContextAccessor _context = new HttpContextAccessor();
            _context.HttpContext.Response.Cookies.Append(key, value, new CookieOptions { Expires= DateTimeOffset.Now.AddDays(days) });
        }

        /// <summary>
        /// 删除Cookie值
        /// </summary>
        /// <param name="strName">Cookie名称</param>
        /// <returns>cookie值</returns>
        public static void RemoveCookie(string strName)
        {
            HttpContextAccessor _context = new HttpContextAccessor();
            _context.HttpContext.Response.Cookies.Delete(strName);
        }
    }
}
