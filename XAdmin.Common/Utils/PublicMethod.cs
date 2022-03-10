using Microsoft.AspNetCore.Http;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace XAdmin.Common.Utils
{
    public class PublicMethod
    {
        #region 判断是不是IP地址
        private static bool IsIP(string ip)
        {
            string[] sections = ip.Split('.');
            if (sections.Length != 4)
                return false;
            foreach (string s in sections)
            {
                int tmp = int.Parse(s.Trim());
                if (tmp > 255)
                    return false;
            }
            return true;
        }
        #endregion

        #region 获得当前页面客户端的IP
        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetRealIP()
        {
            HttpContextAccessor _context = new HttpContextAccessor();
            var ip = _context.HttpContext.Request.Headers["X-Forwarded-For"].ToString();
            if (string.IsNullOrEmpty(ip))
            {
                ip = _context.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }
        #endregion

        #region 获得当前访问的URL地址
        /// <summary>
        /// 获得当前访问的URL地址
        /// </summary>
        /// <param name="All">true=返回全路径</param>
        /// <returns></returns>
        public static string GetUrl(bool All = false)
        {
            HttpContextAccessor _context = new HttpContextAccessor();
            if (All)
                return _context.HttpContext.Request.Scheme + "://" + _context.HttpContext.Request.Host + _context.HttpContext.Request.Path.ToString();
            else
                return _context.HttpContext.Request.Path.ToString();
        }
        #endregion

        /// <summary>
        /// 获取当前访问者机器名称
        /// </summary>
        /// <returns></returns>
        public static string GetPCName()
        {
            return Dns.GetHostName();
        }

        /// <summary>
        /// 获取当前访问者的浏览器信息及用户代理信息
        /// </summary>
        /// <returns></returns>
        public static string GetUserAgent()
        {
            HttpContextAccessor _context = new HttpContextAccessor();
            return _context.HttpContext.Request.Headers["User-Agent"].ToString();
        }

        

        /// <summary>
        /// 取得当月第一天的日期
        /// </summary>
        /// <returns></returns>
        public static DateTime GetMonthFirstDay()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            DateTime firstDayOfThisMonth = new DateTime(year, month, 1);
            return firstDayOfThisMonth;
        }

        /// <summary>
        /// 取得当月第最后一的日期
        /// </summary>
        /// <returns></returns>
        public static DateTime GetMonthLastDay()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            DateTime lastDayOfThisMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            return lastDayOfThisMonth;
        }

        /// <summary>
        /// 取得当月第最后一的日期
        /// </summary>
        /// <returns></returns>
        public static DateTime GetMonthLastDay(DateTime dt)
        {
            int year = dt.Year;
            int month = dt.Month;
            DateTime lastDayOfThisMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            return lastDayOfThisMonth;
        }

        /// <summary>
        /// 获取指定日期和当前日期相差天数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int SpanDays(DateTime? dt)
        {
            if (dt == null)
            {
                return 0;
            }
            TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan ts2 = new TimeSpan(dt.Value.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts.Days;
        }

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            return "";// HttpContext.Current.Server.MapPath(strPath);
        }

        /// <summary>
        /// 返回文件是否存在
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否存在</returns>
        public static bool FileExists(string filename)
        {
            return System.IO.File.Exists(filename);
        }

        /// <summary>
        /// 获取文件编码
        /// </summary>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static Encoding GetEncodingByEncode(string encode)
        {
            Encoding encoding;
            switch (encode.ToLower())
            {
                case "gb2312":
                    encoding = Encoding.Default;
                    break;
                case "utf-8":
                    encoding = new UTF8Encoding(false);
                    break;
                default:
                    encoding = Encoding.Default;
                    break;
            }
            return encoding;
        }

        /// <summary>
        /// 获取首页
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string SiteHome()
        {
            //string homeurl = "";
            //string domain = ConfigurationManager.AppSettings["SiteDomain"];
            //if (string.IsNullOrEmpty(domain))
            //{
            //    homeurl = "/";
            //}
            //else
            //{
            //    if (domain.StartsWith("www."))
            //    {
            //        homeurl = "http://" + domain;
            //    }
            //    else
            //    {
            //        homeurl = "http://www." + domain;
            //    }
            //}
            //return homeurl;
            return "";
        }

        #region 补零
        /// <summary>
        /// 补零
        /// </summary>
        /// <param name="num"></param>
        /// <param name="numlen"></param>
        /// <returns></returns>
        public static string MendZero(long num, int numlen)
        {
            StringBuilder retValue = new StringBuilder("");
            if (num.ToString().Length < numlen)
            {
                int length = numlen - num.ToString().Length;

                for (int i = 0; i < length; i++)
                {
                    retValue.Append("0");
                }
            }
            retValue.Append(num);
            return retValue.ToString();
        } 
        #endregion

        #region 获取页码显示链接
        /// <summary>
        /// 获得页码显示链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <returns>页码html</returns>
        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage)
        {
            int startPage = 1;
            int endPage = 1;

            //bool b = url.IndexOf("javascript") == -1 ? false : true;

            //string t1 = "<a class=\"link\" href=\"" + RegularUrl("page", "1", url) + "\">&laquo;首页</a>";
            //string t2 = "<a class=\"link\" href=\"" + RegularUrl("page", countPage.ToString(), url) + "\">尾页&raquo;</a>";

            if (countPage < 1) countPage = 1;
            if (extendPage < 3) extendPage = 2;

            if (countPage > extendPage)
            {
                if (curPage - (extendPage / 2) > 0)
                {
                    if (curPage + (extendPage / 2) < countPage)
                    {
                        startPage = curPage - (extendPage / 2);
                        endPage = startPage + extendPage - 1;
                    }
                    else
                    {
                        endPage = countPage;
                        startPage = endPage - extendPage + 1;
                        //t2 = "";
                    }
                }
                else
                {
                    endPage = extendPage;
                    //t1 = "";
                }
            }
            else
            {
                startPage = 1;
                endPage = countPage;
                //t1 = "";
                //t2 = "";
            }
            StringBuilder s = new StringBuilder("");
            //s.Append(t1);
            if (curPage == 1)
            {
                s.Append("<a href='javascript:void()' class='link'>");
                s.Append("首页");
                s.Append("</a>");

                s.Append("<a href='javascript:void()' class='link'>");
                s.Append("上一页");
                s.Append("</a>");
            }
            if (curPage > 1 && curPage <= endPage)
            {
                s.Append("<a href='" + RegularUrl("page", "1", url) + "' class='link'>");
                s.Append("首页");
                s.Append("</a>");

                s.Append("<a href=\"" + RegularUrl("page", (curPage - 1).ToString(), url) + "\" class='link'>");
                s.Append("上一页");
                s.Append("</a>");
            }
            for (int i = startPage; i <= endPage; i++)
            {
                if (i == curPage)
                {
                    s.Append("<a class='curPage'>");
                    s.Append(i);
                    s.Append("</a>");
                }
                else
                {
                    s.Append("<a href=\"");
                    s.Append(RegularUrl("page", i.ToString(), url));
                    s.Append("\" class=\"link\">");
                    s.Append(i);
                    s.Append("</a>");
                }
            }
            if (curPage == endPage)
            {
                s.Append("<a href='javascript:void()' class='link'>");
                s.Append("下一页");
                s.Append("</a>");
                s.Append("<a href='javascript:void()' class='link' style='border-right:0px;'>");
                s.Append("尾页");
                s.Append("</a>");
            }
            if (curPage < endPage)
            {
                s.Append("<a href=\"" + RegularUrl("page", (curPage + 1).ToString(), url) + "\" class='link'>");
                s.Append("下一页");
                s.Append("</a>");

                s.Append("<a href='" + RegularUrl("page", countPage.ToString(), url) + "' class='link' style='border-right:0px;'>");
                s.Append("尾页");
                s.Append("</a>");
            }
            //s.Append(t2);

            return s.ToString();
        }

        /// <summary>
        /// 获取页码显示链接
        /// </summary>
        /// <param name="curPage"></param>
        /// <param name="countPage"></param>
        /// <param name="url"></param>
        /// <param name="extendPage"></param>
        /// <param name="func">JS方法名称</param>
        /// <param name="roleID">角色ID</param>
        /// <param name="ShowFirstEnd">是否显示首页尾页</param>
        /// <returns></returns>
        public static string GetPageNumbers2(int curPage, int countPage, string url, int extendPage, string func, int roleID, bool ShowFirstEnd)
        {
            if (countPage < 2)
            {
                return "";
            }
            else
            {
                int startPage = 1;
                int endPage = 1;
                if (countPage < 1) countPage = 1;
                if (extendPage < 3) extendPage = 2;

                if (countPage > extendPage)
                {
                    if (curPage - (extendPage / 2) > 0)
                    {
                        if (curPage + (extendPage / 2) < countPage)
                        {
                            startPage = curPage - (extendPage / 2);
                            endPage = startPage + extendPage - 1;
                        }
                        else
                        {
                            endPage = countPage;
                            startPage = endPage - extendPage + 1;
                            //t2 = "";
                        }
                    }
                    else
                    {
                        endPage = extendPage;
                        //t1 = "";
                    }
                }
                else
                {
                    startPage = 1;
                    endPage = countPage;
                    //t1 = "";
                    //t2 = "";
                }
                StringBuilder s = new StringBuilder("");
                //s.Append(t1);
                if (ShowFirstEnd)
                {
                    if (curPage == 1)
                    {
                        s.Append("<a href='javascript:void()' class='link'>");
                        s.Append("首页");
                        s.Append("</a>");

                        s.Append("<a href='javascript:void()' class='link'>");
                        s.Append("上一页");
                        s.Append("</a>");
                    }
                    if (curPage > 1 && curPage <= endPage)
                    {
                        //s.Append("<a href='" + RegularUrl("page", "1", url) + "' class='link'>");
                        //s.Append("首页");
                        //s.Append("</a>");
                        //s.Append("<a href=\"" + RegularUrl("page", (curPage - 1).ToString(), url) + "\" class='link'>");
                        //s.Append("上一页");
                        //s.Append("</a>");

                        s.Append("<a href=\"");
                        s.Append("javascript:" + func + "('" + roleID + "','" + 1 + "')");
                        s.Append("\" class=\"link\">");
                        s.Append("首页");
                        s.Append("</a>");

                        s.Append("<a href=\"");
                        s.Append("javascript:" + func + "('" + roleID + "','" + (curPage - 1).ToString() + "')");
                        s.Append("\" class=\"link\">");
                        s.Append("上一页");
                        s.Append("</a>");
                    }
                }
                for (int i = startPage; i <= endPage; i++)
                {
                    if (i == curPage)
                    {
                        s.Append("<a class='curPage'>");
                        s.Append(i);
                        s.Append("</a>");
                    }
                    else
                    {
                        s.Append("<a href=\"");
                        s.Append("javascript:" + func + "('" + roleID + "','" + i.ToString() + "')");
                        s.Append("\" class=\"link\">");
                        s.Append(i);
                        s.Append("</a>");
                    }
                }
                if (ShowFirstEnd)
                {
                    if (curPage == endPage)
                    {
                        s.Append("<a href='javascript:void()' class='link'>");
                        s.Append("下一页");
                        s.Append("</a>");
                        s.Append("<a href='javascript:void()' class='link' style='border-right:0px;'>");
                        s.Append("尾页");
                        s.Append("</a>");
                    }
                    if (curPage < endPage)
                    {
                        //s.Append("<a href=\"" + RegularUrl("page", (curPage + 1).ToString(), url) + "\" class='link'>");
                        //s.Append("下一页");
                        //s.Append("</a>");

                        //s.Append("<a href='" + RegularUrl("page", countPage.ToString(), url) + "' class='link' style='border-right:0px;'>");
                        //s.Append("尾页");
                        //s.Append("</a>");

                        s.Append("<a href=\"");
                        s.Append("javascript:" + func + "('" + roleID + "','" + (curPage + 1).ToString() + "')");
                        s.Append("\" class=\"link\">");
                        s.Append("下一页");
                        s.Append("</a>");

                        s.Append("<a href=\"");
                        s.Append("javascript:" + func + "('" + roleID + "','" + countPage.ToString() + "')");
                        s.Append("\" class=\"link\">");
                        s.Append("尾页");
                        s.Append("</a>");
                    }
                }

                return s.ToString();
            }

        } 
        #endregion

        /// 将Url中key的值替换为value，如果不存在key则追加
        public static string RegularUrl(string key, string value, string url)
        {
            int fragPos = url.LastIndexOf("#");
            string fragment = "";
            if (fragPos > -1)
            {
                fragment = url.Substring(fragPos);
                url = url.Substring(0, fragPos);
            }
            int querystart = url.IndexOf("?");
            if (querystart < 0)
            {
                url += "?" + key + "=" + value;
            }
            else if (querystart == url.Length - 1)
            {
                url += key + "=" + value;
            }
            else
            {
                Regex Re = new Regex(key + "=[^\\s&#]*", RegexOptions.IgnoreCase);
                url = (Re.IsMatch(url)) ? Re.Replace(url, key + "=" + value) : url + "&" + key + "=" + value;
            }
            return url + fragment;
        }
        /// <summary>
        /// 去掉HTML标签
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string ClearHtml(string strHtml)
        {
            if (string.IsNullOrEmpty(strHtml))
            {
                return "";
            }
            else
            {
                Regex r = null;
                Match m = null;

                r = new Regex(@"<\/?[^>]*>", RegexOptions.IgnoreCase);
                for (m = r.Match(strHtml); m.Success; m = m.NextMatch())
                {
                    strHtml = strHtml.Replace(m.Groups[0].ToString(), "");
                }
            }
            return strHtml;
        }
        /// <summary>
        /// 去掉HTML标签 并去掉&nbsp空格
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string ClearHtmlNbsp(string strHtml)
        {
            if (string.IsNullOrEmpty(strHtml))
                return "";
            else
                strHtml = ClearHtml(strHtml).Replace("&nbsp;", "");
            return strHtml;
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static string ReadFileContent(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            string str = "";
            if (!fi.Exists)
            {
                str = "文件不存在";
            }
            else if (fi.Length > 1024 * 1024)//如果读取的文件较大
            {
                FileStream fs = new FileStream(filePath, FileMode.Open);
                StreamReader m_streamReader = new StreamReader(fs);
                m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);// 从数据流中读取每一行，直到文件的最后一行
                while (m_streamReader.ReadLine() != null)
                {
                    str += m_streamReader.ReadLine();
                }
                m_streamReader.Close();
            }
            else
                str = File.ReadAllText(filePath, Encoding.UTF8);
            return str;
        }
        /// <summary>
        /// 将字符串写入到文本文件中
        /// </summary>
        /// <param name="str">将要写入的字符串</param>
        /// <param name="filePath">保存的路径</param>
        /// <returns></returns>
        public static bool WritStrToFile(string str, string filePath)
        {
            StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8);
            //StreamWriter sw = File.CreateText(filePath);
            sw.Write(str);
            sw.Close();
            return true;
        }

        /// <summary>
        /// 文件扩展名安全验证
        /// </summary>
        /// <param name="FileExt">上传文件的扩展名</param>
        /// <returns></returns>
        public static bool FileExtVerify(string FileExt)
        {
            bool result = false;
            string Extension = ".doc|.docx|.xls|.xlsx|.rar|.zip|.7z|.htm|.html|.mht|.pdf|.txt|.jpg|.jpeg|.bmp|.png|.gif";
            if (Extension.Contains(FileExt))
            {
                result = true;
            }
            return result;
        }

        #region 将String数组转换为Int数组
        private static int StrToInt(string str)
        {
            return int.Parse(str);
        }
        /// <summary>
        /// 将String数组转换为Int数组
        /// </summary>
        /// <param name="arrs"></param>
        /// <returns></returns>
        public static int[] ConvertStringArrayToInt(string[] arrs)
        {
            int[] arri = Array.ConvertAll(arrs, new Converter<string, int>(StrToInt));
            return arri;
        }
        #endregion

        #region 截取字符长度
        /// <summary>
        /// 截取字符长度
        /// </summary>
        /// <param name="inputString">字符</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string CutString(string inputString, int len)
        {
            if (string.IsNullOrEmpty(inputString))
                return "";
            inputString = DropHtml(inputString);
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > len)
                    break;
            }
            //如果截过则加上半个省略号 
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (mybyte.Length > len)
                tempString += "…";
            return tempString;
        }

        public static string DropHtml(string htmlstring)
        {
            if (string.IsNullOrEmpty(htmlstring)) return "";
            //删除脚本  
            htmlstring = Regex.Replace(htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML  
            htmlstring = Regex.Replace(htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);

            htmlstring = Regex.Replace(htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            htmlstring = htmlstring.Replace("<", "");
            htmlstring = htmlstring.Replace(">", "");
            htmlstring = htmlstring.Replace("\r\n", "");
            //htmlstring = HttpContext.Current.Server.HtmlEncode(htmlstring).Trim(); 
            return htmlstring;
        }
        #endregion

        #region 得到一个汉字的拼音第一个字母，如果是一个英文字母则直接返回大写字母
        /// <summary>
        /// 得到一个汉字的拼音第一个字母，如果是一个英文字母则直接返回大写字母
        /// </summary>
        /// <param name="CnChar">单个汉字</param>
        /// <returns>单个大写字母</returns>
        public static string GetCharSpellCode(string CnChar)
        {
            long iCnChar;
            byte[] ZW = Encoding.Default.GetBytes(CnChar);

            //如果是字母，则直接返回首字母
            if (ZW.Length == 1)
            {
                return CutString(CnChar.ToUpper(), 1);
            }
            else
            {
                int i1 = (short)(ZW[0]);
                int i2 = (short)(ZW[1]);
                iCnChar = i1 * 256 + i2;
            }
            // iCnChar match the constant
            if ((iCnChar >= 45217) && (iCnChar <= 45252))
            {
                return "A";
            }
            else if ((iCnChar >= 45253) && (iCnChar <= 45760))
            {
                return "B";
            }
            else if ((iCnChar >= 45761) && (iCnChar <= 46317))
            {
                return "C";
            }
            else if ((iCnChar >= 46318) && (iCnChar <= 46825))
            {
                return "D";
            }
            else if ((iCnChar >= 46826) && (iCnChar <= 47009))
            {
                return "E";
            }
            else if ((iCnChar >= 47010) && (iCnChar <= 47296))
            {
                return "F";
            }
            else if ((iCnChar >= 47297) && (iCnChar <= 47613))
            {
                return "G";
            }
            else if ((iCnChar >= 47614) && (iCnChar <= 48118))
            {
                return "H";
            }
            else if ((iCnChar >= 48119) && (iCnChar <= 49061))
            {
                return "J";
            }
            else if ((iCnChar >= 49062) && (iCnChar <= 49323))
            {
                return "K";
            }
            else if ((iCnChar >= 49324) && (iCnChar <= 49895))
            {
                return "L";
            }
            else if ((iCnChar >= 49896) && (iCnChar <= 50370))
            {
                return "M";
            }
            else if ((iCnChar >= 50371) && (iCnChar <= 50613))
            {
                return "N";
            }
            else if ((iCnChar >= 50614) && (iCnChar <= 50621))
            {
                return "O";
            }
            else if ((iCnChar >= 50622) && (iCnChar <= 50905))
            {
                return "P";
            }
            else if ((iCnChar >= 50906) && (iCnChar <= 51386))
            {
                return "Q";
            }
            else if ((iCnChar >= 51387) && (iCnChar <= 51445))
            {
                return "R";
            }
            else if ((iCnChar >= 51446) && (iCnChar <= 52217))
            {
                return "S";
            }
            else if ((iCnChar >= 52218) && (iCnChar <= 52697))
            {
                return "T";
            }
            else if ((iCnChar >= 52698) && (iCnChar <= 52979))
            {
                return "W";
            }
            else if ((iCnChar >= 52980) && (iCnChar <= 53640))
            {
                return "X";
            }
            else if ((iCnChar >= 53689) && (iCnChar <= 54480))
            {
                return "Y";
            }
            else if ((iCnChar >= 54481) && (iCnChar <= 55289))
            {
                return "Z";
            }
            else
                return ("?");

        }
        #endregion

        #region 得到一周的周一和周日的日期
        /// <summary> 
        /// 计算本周的周一日期 
        /// </summary> 
        /// <returns></returns> 
        public static DateTime GetMondayDate()
        {
            return GetMondayDate(DateTime.Now);
        }
        /// <summary> 
        /// 计算本周周日的日期 
        /// </summary> 
        /// <returns></returns> 
        public static DateTime GetSundayDate()
        {
            return GetSundayDate(DateTime.Now);
        }
        /// <summary> 
        /// 计算某日起始日期（礼拜一的日期） 
        /// </summary> 
        /// <param name="someDate">该周中任意一天</param> 
        /// <returns>返回礼拜一日期，后面的具体时、分、秒和传入值相等</returns> 
        public static DateTime GetMondayDate(DateTime someDate)
        {
            int i = someDate.DayOfWeek - DayOfWeek.Monday;
            if (i == -1) i = 6;// i值 > = 0 ，因为枚举原因，Sunday排在最前，此时Sunday-Monday=-1，必须+7=6。 
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return someDate.Subtract(ts);
        }
        /// <summary> 
        /// 计算某日结束日期（礼拜日的日期） 
        /// </summary> 
        /// <param name="someDate">该周中任意一天</param> 
        /// <returns>返回礼拜日日期，后面的具体时、分、秒和传入值相等</returns> 
        public static DateTime GetSundayDate(DateTime someDate)
        {
            int i = someDate.DayOfWeek - DayOfWeek.Sunday;
            if (i != 0) i = 7 - i;// 因为枚举原因，Sunday排在最前，相减间隔要被7减。 
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return someDate.Add(ts);
        }

        /// <summary>
        /// 根据星期几获得数字的星期几
        /// </summary>
        /// <param name="weekName">例如周一：Monday</param>
        /// <returns></returns>
        public static int GetWeekByWeekName(string weekName)
        {
            var week = 1;
            switch (weekName)
            {
                case "Monday":
                    week = 1;
                    break;
                case "Tuesday":
                    week = 2;
                    break;
                case "Wednesday":
                    week = 3;
                    break;
                case "Thursday":
                    week = 4;
                    break;
                case "Friday":
                    week = 5;
                    break;
                case "Saturday":
                    week = 6;
                    break;
                case "Sunday":
                    week = 7;
                    break;
            }
            return week;
        }
        #endregion

        #region 日期时间戳转换
        /// <summary>
        /// 时间戳转换为日期（时间戳单位秒）
        /// </summary>
        /// <param name="TimeStamp"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(long timeStamp)
        {
            var dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = new TimeSpan(timeStamp);
            return dtStart.Add(toNow);
            //var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            //return start.AddMilliseconds(timeStamp).AddHours(8);
        }
        /// <summary>
        /// 日期转换为时间戳（时间戳单位秒）
        /// </summary>
        /// <param name="TimeStamp"></param>
        /// <returns></returns>
        public static long ConvertToTimeStamp(DateTime time)
        {
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(time.AddHours(-8) - Jan1st1970).TotalMilliseconds;
        }
        #endregion

        /// <summary>
        /// 生成唯一数
        /// </summary>
        public class UniqueData
        {
            private static object obj = new object();
            private static int _sn = 0;
            public static string Gener()
            {
                lock (obj)
                {
                    if (_sn == int.MaxValue)
                    {
                        _sn = 0;
                    }
                    else
                    {
                        _sn++;
                    }
                    //Thread.Sleep(100);
                    return DateTime.Now.ToString("yyyyMMdd") + _sn.ToString().PadLeft(5, '0');
                }
            }
        }

        #region 返回时间差
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                //TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                //TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                //TimeSpan ts = ts1.Subtract(ts2).Duration();
                TimeSpan ts = DateTime2 - DateTime1;
                if (ts.Days >= 1)
                {
                    dateDiff = DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "分钟前";
                    }
                }
            }
            catch
            { }
            return dateDiff;
        }
        #endregion


    }
}
