﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XAdmin.Common.Utils
{
    public class TextHelper
    {
        /// <summary>
        /// 指示正则表达式在一次匹配时，最多匹配5000个项
        /// </summary>
        public const int MAX_MATCH_COUNT = 4000;

        /// <summary>
        /// 清除HTML内容中指定的标签,如果不指定标记参数则清除字符中的所有HTML标签
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TagClean(string value, params string[] tagNames)
        {
            string strReg = "<.+?>";
            if (tagNames != null)
            {
                strReg = "<\\s*[/]?(";
                bool isFirst = true;
                for (int i = 0; i < tagNames.Length; i++)
                {
                    if (isFirst)
                    {
                        strReg += tagNames[i];
                    }
                    else
                    {
                        strReg += "|" + tagNames[i];
                    }
                    isFirst = false;
                }
                strReg += ").*?>";
            }
            return new Regex(strReg, RegexOptions.Singleline | RegexOptions.IgnoreCase).Replace(value, string.Empty);
        }



        /// <summary>
        /// 将HTML中获取到的&nbsp;空格去掉,并对字符串执行trim
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string DecodeHtmlSpace(string html)
        {
            if (html == null)
            {
                return null;
            }
            return html.Replace("&nbsp;", " ").Trim();
        }


        /// <summary>
        /// 清除html文档中的js代码和样式 iframe
        /// 过滤链接,去掉注释 去掉<div >和</div>
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ClearScriptStyle(string html)
        {
            if (html == null)
                return null;
            Regex regex = new Regex("<(script|style|iframe).*?>.*?</(script|style|iframe).*?>|(<a.*?>|</a>)|(<!--.*?-->)|(<div.*?>|</div>)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            return regex.Replace(html, string.Empty);
        }

        public static string CleanHtmlEscapChar(string str)
        {
            if (str == null)
            {
                return null;
            }
            Regex regex = new Regex("&\\w+?;", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            return regex.Replace(str, string.Empty);
        }

        public static string ClearHtml(string html)
        {
            if (html == null)
                return null;
            string str = CleanHtmlEscapChar(ClearScriptStyle(html));
            return str;
        }

        /// <summary>
        /// 过滤掉 \r \t \n &nbsp;
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FormatStr(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            return str.Replace("\n", string.Empty).Replace("\t", string.Empty).Replace("\r", string.Empty).Replace("&nbsp;", string.Empty);
        }
        /// <summary>
        /// 规范化模板和即将要比较的内容，将windows换行符\r\n转换为统一的unix格式的\n
        /// 在模板存入库之前需要全部处理为这种格式的
        /// </summary>
        /// <param name="strtoformat"></param>
        /// <returns></returns>
        public static string FormatCRLF(string strtoformat)
        {
            if (strtoformat == null)
                return strtoformat;
            return strtoformat.Replace("\r\n", "\n");
        }


        /// <summary>
        /// 将unix格式的字符串，转换为windows格式的，主要征对模板从数据库读出需要编辑的时候
        /// 还源字符串在编辑器中的格式
        /// </summary>
        /// <param name="strtoformat"></param>
        /// <returns></returns>
        public static string UnFormatCRLF(string strtoformat)
        {
            if (strtoformat == null)
                return strtoformat;

            //必须先全部转换为unix格式的，再转换为windows格式的
            return strtoformat.Replace("\r\n", "\n").Replace("\n", "\r\n");
        }

        /// <summary>
        /// 返回符合条件的字符串
        /// </summary>
        /// <param name="strToMatch"></param>
        /// <param name="regex"></param>
        /// <returns>匹配的字符串，如果不匹配，则返回null</returns>
        public static string MatchedGroup(string strToMatch, string regex)
        {
            if (strToMatch == null || regex == null)
            {
                return null;
            }
            Match match = Regex.Match(strToMatch, regex, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return match.Value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strToMatch"></param>
        /// <param name="regex"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static string MatchedGroup(string strToMatch, string regex, string groupName)
        {
            if (strToMatch == null || regex == null || groupName == null)
            {
                return null;
            }
            Match match = Regex.Match(strToMatch, regex, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (match.Success)
            {
                if (match.Groups[groupName] != null)
                {
                    return match.Groups[groupName].Value;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 找出一个字符串中由start和end包围的一个值
        /// 如果开始和结速字符用 $$包含起来，表示需要把开始和结束标记也放进匹配的内容中
        /// 如果开始的左侧由%开始,表示开始标签和第一个匹配的标签匹配(默许)
        /// 如果开始的右侧由%开始,表示要开始标签和结束标签的左边匹配
        /// 如果结束的左侧由%开始,表示结束标签和最靠近开始标签的匹配
        /// 如果结束右侧由%开始，表示结束标签和离开始标签最远的标签匹配
        /// %只有出现在整个字符串的最初或最必才有效
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string StrBetween(string text, string start, string end)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
                return null;

            bool rightAligh = false;
            bool startLeft = true;
            if (start[0] == '%')
            {
                startLeft = true;
                start = start.Substring(1, start.Length - 1);
            }

            if (start[start.Length - 1] == '%')
            {
                startLeft = false;
                start = start.Substring(0, start.Length - 1);
            }

            if (end[end.Length - 1] == '%')
            {
                rightAligh = true;
                end = end.Substring(0, end.Length - 1);
            }

            if (end[0] == '%')
            {
                rightAligh = false;
                end = end.Substring(1, end.Length - 1);
            }

            bool startInclude = start[0] == '$' && start[start.Length - 1] == '$';
            bool endInclude = end[0] == '$' && end[end.Length - 1] == '$';
            if (startInclude)
            {
                if (start.Length <= 2)
                {
                    return null;
                }
                start = start.Substring(1, start.Length - 2);
            }

            if (endInclude)
            {
                if (end.Length <= 2)
                {
                    return null;
                }
                end = end.Substring(1, end.Length - 2);
            }

            if (start.Length + end.Length >= text.Length)
            {
                return null;
            }

            int startIndex = -1;
            int endIndex = -1;
            int len = 0;

            if (startLeft)
            {
                startIndex = text.IndexOf(start);
                if (startIndex < 0)
                {
                    return null;
                }
                startIndex = startIndex + start.Length;
                if (rightAligh) //右对齐
                {
                    endIndex = text.LastIndexOf(end);
                }
                else
                {
                    endIndex = text.IndexOf(end, startIndex);
                }
                if (endIndex < 0 || endIndex <= startIndex)
                {
                    return null;
                }
                len = endIndex - startIndex;
            }
            else
            {
                if (rightAligh) //右对齐
                {
                    endIndex = text.LastIndexOf(end);
                }
                else
                {
                    endIndex = text.IndexOf(end);
                }

                if (endIndex < 0)
                {
                    return null;
                }
                string temptext = text.Substring(0, endIndex);
                startIndex = temptext.LastIndexOf(start);
                if (startIndex == -1)
                {
                    return null;
                }
                startIndex = startIndex + start.Length;
                len = temptext.Length - startIndex;
            }

            if (len <= 0)
            {
                return null;
            }
            string result = text.Substring(startIndex, len);
            if (startInclude)
                result = start + result;

            if (endInclude)
                result = result + end;

            return result;
        }

        /// <summary>
        /// 找出text中，由开始和结束标记指定的所有数据项，并放到List中返回
        ///  如果开始和结速字符用 $$包含起来，表示需要把开始和结束标记也放进匹配的内容中
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IList<string> StrBetweenList(string text, string start, string end)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
                return null;

            bool startInclude = start[0] == '$' && start[start.Length - 1] == '$';
            bool endInclude = end[0] == '$' && end[end.Length - 1] == '$';
            if (startInclude)
            {
                if (start.Length <= 2)
                {
                    return null;
                }
                start = start.Substring(1, start.Length - 2);
            }

            if (endInclude)
            {
                if (end.Length <= 2)
                {
                    return null;
                }
                end = end.Substring(1, end.Length - 2);
            }

            if (start.Length + end.Length >= text.Length)
            {
                return null;
            }

            IList<string> strList = new List<string>();
            int currentIndex = 0;
            while (currentIndex < text.Length - 1)
            {
                int startIndex = text.IndexOf(start, currentIndex);
                if (startIndex < 0 || (startIndex + end.Length >= text.Length))
                {
                    break;
                }
                int endIndex = text.IndexOf(end, startIndex + start.Length);
                if (endIndex < 0 || (endIndex - startIndex) == 0)
                {
                    break;
                }
                startIndex = startIndex + start.Length;
                string item = text.Substring(startIndex, endIndex - startIndex);
                if (!String.IsNullOrEmpty(item) && item.Trim().Length > 0)
                {
                    strList.Add(item.Trim());
                }
                currentIndex = endIndex + end.Length;
            }

            if (strList.Count > 0)
            {
                return strList;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///  按字节码截取
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length">汉字长度</param>
        /// <returns></returns>
        public static String SubStr(string str, int length)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return byteSubstring(str.ToString(), length, "");
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        ///  按字节码截取
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length">汉字长度</param>
        /// <returns></returns>
        public static String SubStr(string str, int length, string suffix)
        {
            if (!string.IsNullOrEmpty(str.ToString()))
            {
                return byteSubstring(str.ToString(), length, suffix);
            }
            else
            {

                return String.Empty;
            }
        }

        /// <summary>
        ///  根据字节数截取字符串 当最后一个字符是字母或数字，则保留该字符，如果是汉字，说明这个汉字被截了一半，则去掉这个汉字。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static String byteSubstring(string s, int length, string suffix)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(s);
            int n = 0;  //  表示当前的字节数
            int i = 0;
            int totalLength = bytes.GetLength(0);
            for (; i < totalLength && n < (2 * length); i++)
            {
                //  偶数位置，如0、2、4等，为UCS2编码中两个字节的第一个字节
                if (i % 2 == 0)
                {
                    n++;//在UCS2第一个字节时n加1
                }
                else
                {
                    //当UCS2编码的第二个字节大于0时，该UCS2字符为汉字，一个汉字算两个字节
                    if (bytes[i] > 0)
                    {
                        n++;
                    }
                }
            }

            //如果i为奇数时，处理成偶数
            if (i % 2 == 1)
            {
                //该UCS2字符是汉字时，去掉这个截一半的汉字
                if (bytes[i] > 0)
                    i = i - 1;
                //该UCS2字符是字母或数字，则保留该字符
                else
                    i = i + 1;
            }
            string result = System.Text.Encoding.Unicode.GetString(bytes, 0, i);
            if (length * 2 < totalLength)
            {
                result = result + suffix;
            }
            return result;
        }

        /// <summary>
        /// 获取截取内容
        /// </summary>
        /// <param name="board"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetSubHtmlText(string htmlStr, Int32 length)
        {
            string text = string.Empty;
            if (!string.IsNullOrWhiteSpace(htmlStr))
            {
                text = Regex.Replace(htmlStr, @"(<[^>]*>)|([\s　])", string.Empty, RegexOptions.IgnoreCase);
                text = text.Replace("\n", "").Replace("\t", "");
                if (text.Length > length)
                    text = text.Substring(0, length);
            }
            return text;
        }

        /// <summary>
        /// 格式化文件名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string FormatFileName(string fileName)
        {
            return fileName.Replace("\\", "").Replace("/", "").Replace("*", "").Replace(":", "：").Replace("?", "").Replace("<", "").Replace(">", "").Replace("|", "");
        }

        #region 取拼音

        public static String IndexCode(String IndexTxt)
        {
            String _Temp = null;
            for (int i = 0; i < IndexTxt.Length; i++)
            {
                _Temp = _Temp + GetOneIndex(IndexTxt.Substring(i, 1));
            }
            return _Temp;
        }


        /// <summary>
        /// 得到单个字符的首字母
        /// </summary>
        /// <param name="OneIndexTxt"></param>
        /// <returns></returns>
        private static String GetOneIndex(String OneIndexTxt)
        {
            if (Convert.ToChar(OneIndexTxt) >= 0 && Convert.ToChar(OneIndexTxt) < 256)
                return OneIndexTxt;
            else
            {
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                byte[] unicodeBytes = Encoding.Unicode.GetBytes(OneIndexTxt);
                byte[] gb2312Bytes = Encoding.Convert(Encoding.Unicode, gb2312, unicodeBytes);
                return GetX(Convert.ToInt32(
                                String.Format("{0:D2}", Convert.ToInt16(gb2312Bytes[0]) - 160)
                                + String.Format("{0:D2}", Convert.ToInt16(gb2312Bytes[1]) - 160)
                                ));
            }

        }


        /// <summary>
        /// 根据区位得到首字母
        /// </summary>
        /// <param name="GBCode"></param>
        /// <returns></returns>
        private static String GetX(int GBCode)
        {
            if (GBCode >= 1601 && GBCode < 1637) return "A";
            if (GBCode >= 1637 && GBCode < 1833) return "B";
            if (GBCode >= 1833 && GBCode < 2078) return "C";
            if (GBCode >= 2078 && GBCode < 2274) return "D";
            if (GBCode >= 2274 && GBCode < 2302) return "E";
            if (GBCode >= 2302 && GBCode < 2433) return "F";
            if (GBCode >= 2433 && GBCode < 2594) return "G";
            if (GBCode >= 2594 && GBCode < 2787) return "H";
            if (GBCode >= 2787 && GBCode < 3106) return "J";
            if (GBCode >= 3106 && GBCode < 3212) return "K";
            if (GBCode >= 3212 && GBCode < 3472) return "L";
            if (GBCode >= 3472 && GBCode < 3635) return "M";
            if (GBCode >= 3635 && GBCode < 3722) return "N";
            if (GBCode >= 3722 && GBCode < 3730) return "O";
            if (GBCode >= 3730 && GBCode < 3858) return "P";
            if (GBCode >= 3858 && GBCode < 4027) return "Q";
            if (GBCode >= 4027 && GBCode < 4086) return "R";
            if (GBCode >= 4086 && GBCode < 4390) return "S";
            if (GBCode >= 4390 && GBCode < 4558) return "T";
            if (GBCode >= 4558 && GBCode < 4684) return "W";
            if (GBCode >= 4684 && GBCode < 4925) return "X";
            if (GBCode >= 4925 && GBCode < 5249) return "Y";
            if (GBCode >= 5249 && GBCode <= 5589) return "Z";
            if (GBCode >= 5601 && GBCode <= 8794)
            {
                String CodeData = @"cjwgnspgcenegypbtwxzdxykygtpjnmjqmbsgzscyjsyyfpggbzgydywjkgaljswkbjqhyjwpdzlsgmr
ybywwccgznkydgttngjeyekzydcjnmcylqlypyqbqrpzslwbdgkjfyxjwcltbncxjjjjcxdtqsqzycdxxhgckbphffss
pybgmxjbbyglbhlssmzmpjhsojnghdzcdklgjhsgqzhxqgkezzwymcscjnyetxadzpmdssmzjjqjyzcjjfwqjbdzbjgd
nzcbwhgxhqkmwfbpbqdtjjzkqhylcgxfptyjyyzpsjlfchmqshgmmxsxjpkdcmbbqbefsjwhwwgckpylqbgldlcctnma
eddksjngkcsgxlhzaybdbtsdkdylhgymylcxpycjndqjwxqxfyyfjlejbzrwccqhqcsbzkymgplbmcrqcflnymyqmsqt
rbcjthztqfrxchxmcjcjlxqgjmshzkbswxemdlckfsydsglycjjssjnqbjctyhbftdcyjdgwyghqfrxwckqkxebpdjpx
jqsrmebwgjlbjslyysmdxlclqkxlhtjrjjmbjhxhwywcbhtrxxglhjhfbmgykldyxzpplggpmtcbbajjzyljtyanjgbj
flqgdzyqcaxbkclecjsznslyzhlxlzcghbxzhznytdsbcjkdlzayffydlabbgqszkggldndnyskjshdlxxbcghxyggdj
mmzngmmccgwzszxsjbznmlzdthcqydbdllscddnlkjyhjsycjlkohqasdhnhcsgaehdaashtcplcpqybsdmpjlpcjaql
cdhjjasprchngjnlhlyyqyhwzpnccgwwmzffjqqqqxxaclbhkdjxdgmmydjxzllsygxgkjrywzwyclzmcsjzldbndcfc
xyhlschycjqppqagmnyxpfrkssbjlyxyjjglnscmhcwwmnzjjlhmhchsyppttxrycsxbyhcsmxjsxnbwgpxxtaybgajc
xlypdccwqocwkccsbnhcpdyznbcyytyckskybsqkkytqqxfcwchcwkelcqbsqyjqcclmthsywhmktlkjlychwheqjhtj
hppqpqscfymmcmgbmhglgsllysdllljpchmjhwljcyhzjxhdxjlhxrswlwzjcbxmhzqxsdzpmgfcsglsdymjshxpjxom
yqknmyblrthbcftpmgyxlchlhlzylxgsssscclsldclepbhshxyyfhbmgdfycnjqwlqhjjcywjztejjdhfblqxtqkwhd
chqxagtlxljxmsljhdzkzjecxjcjnmbbjcsfywkbjzghysdcpqyrsljpclpwxsdwejbjcbcnaytmgmbapclyqbclzxcb
nmsggfnzjjbzsfqyndxhpcqkzczwalsbccjxpozgwkybsgxfcfcdkhjbstlqfsgdslqwzkxtmhsbgzhjcrglyjbpmljs
xlcjqqhzmjczydjwbmjklddpmjegxyhylxhlqyqhkycwcjmyhxnatjhyccxzpcqlbzwwwtwbqcmlbmynjcccxbbsnzzl
jpljxyztzlgcldcklyrzzgqtgjhhgjljaxfgfjzslcfdqzlclgjdjcsnclljpjqdcclcjxmyzftsxgcgsbrzxjqqcczh
gyjdjqqlzxjyldlbcyamcstylbdjbyregklzdzhldszchznwczcllwjqjjjkdgjcolbbzppglghtgzcygezmycnqcycy
hbhgxkamtxyxnbskyzzgjzlqjdfcjxdygjqjjpmgwgjjjpkjsbgbmmcjssclpqpdxcdyykypcjddyygywchjrtgcnyql
dkljczzgzccjgdyksgpzmdlcphnjafyzdjcnmwescsglbtzcgmsdllyxqsxsbljsbbsgghfjlwpmzjnlyywdqshzxtyy
whmcyhywdbxbtlmswyyfsbjcbdxxlhjhfpsxzqhfzmqcztqcxzxrdkdjhnnyzqqfnqdmmgnydxmjgdhcdycbffallztd
ltfkmxqzdngeqdbdczjdxbzgsqqddjcmbkxffxmkdmcsychzcmljdjynhprsjmkmpcklgdbqtfzswtfgglyplljzhgjj
gypzltcsmcnbtjbhfkdhbyzgkpbbymtdlsxsbnpdkleycjnycdykzddhqgsdzsctarlltkzlgecllkjljjaqnbdggghf
jtzqjsecshalqfmmgjnlyjbbtmlycxdcjpldlpcqdhsycbzsckbzmsljflhrbjsnbrgjhxpdgdjybzgdlgcsezgxlblg
yxtwmabchecmwyjyzlljjshlgndjlslygkdzpzxjyyzlpcxszfgwyydlyhcljscmbjhblyjlycblydpdqysxktbytdkd
xjypcnrjmfdjgklccjbctbjddbblblcdqrppxjcglzcshltoljnmdddlngkaqakgjgyhheznmshrphqqjchgmfprxcjg
dychghlyrzqlcngjnzsqdkqjymszswlcfqjqxgbggxmdjwlmcrnfkkfsyyljbmqammmycctbshcptxxzzsmphfshmclm
ldjfyqxsdyjdjjzzhqpdszglssjbckbxyqzjsgpsxjzqznqtbdkwxjkhhgflbcsmdldgdzdblzkycqnncsybzbfglzzx
swmsccmqnjqsbdqsjtxxmbldxcclzshzcxrqjgjylxzfjphymzqqydfqjjlcznzjcdgzygcdxmzysctlkphtxhtlbjxj
lxscdqccbbqjfqzfsltjbtkqbsxjjljchczdbzjdczjccprnlqcgpfczlclcxzdmxmphgsgzgszzqjxlwtjpfsyaslcj
btckwcwmytcsjjljcqlwzmalbxyfbpnlschtgjwejjxxglljstgshjqlzfkcgnndszfdeqfhbsaqdgylbxmmygszldyd
jmjjrgbjgkgdhgkblgkbdmbylxwcxyttybkmrjjzxqjbhlmhmjjzmqasldcyxyqdlqcafywyxqhz";
                String _gbcode = GBCode.ToString();
                int pos = (Convert.ToInt16(_gbcode.Substring(0, 2)) - 56) * 94 + Convert.ToInt16(_gbcode.Substring(_gbcode.Length - 2, 2));
                return CodeData.Substring(pos - 1, 1);
            }
            return "";
        }

        #endregion

        #region 替换字符串的第一个匹配项
        /// <summary>
        /// 替换字符串的第一个匹配项
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="match">需要替换的字符串</param>
        /// <param name="replacement">替换后的字符串</param>
        /// <returns></returns>
        public static string Replace(string source, string match, string replacement)
        {
            char[] sArr = source.ToCharArray();
            char[] mArr = match.ToCharArray();
            char[] rArr = replacement.ToCharArray();
            int idx = IndexOf(sArr, mArr);
            if (idx == -1)
            {
                return source;
            }
            else
            {
                return new string(sArr.Take(idx).Concat(rArr).Concat(sArr.Skip(idx + mArr.Length)).ToArray());
            }
        }
        /// <summary>
        /// 查找字符数组在另一个字符数组中匹配的位置
        /// </summary>
        /// <param name="source">源字符数组</param>
        /// <param name="match">匹配字符数组</param>
        /// <returns>匹配的位置，未找到匹配则返回-1</returns>
        private static int IndexOf(char[] source, char[] match)
        {
            int idx = -1;
            for (int i = 0; i < source.Length - match.Length; i++)
            {
                if (source[i] == match[0])
                {
                    bool isMatch = true;
                    for (int j = 0; j < match.Length; j++)
                    {
                        if (source[i + j] != match[j])
                        {
                            isMatch = false;
                            break;
                        }
                    }
                    if (isMatch)
                    {
                        idx = i;
                        break;
                    }
                }
            }
            return idx;
        }
        #endregion

        #region 截取字符串
        public static string GetSubString(string pSrcString, int pLength, string pTailString)
        {
            return GetSubString(pSrcString, 0, pLength, pTailString);
        }
        public static string GetSubString(string pSrcString, int pStartIndex, int pLength, string pTailString)
        {
            string str = pSrcString;
            byte[] bytes = Encoding.UTF8.GetBytes(pSrcString);
            foreach (char ch in Encoding.UTF8.GetChars(bytes))
            {
                if (((ch > 'ࠀ') && (ch < '一')) || ((ch > 0xac00) && (ch < 0xd7a3)))
                {
                    if (pStartIndex >= pSrcString.Length)
                    {
                        return "";
                    }
                    return pSrcString.Substring(pStartIndex, ((pLength + pStartIndex) > pSrcString.Length) ? (pSrcString.Length - pStartIndex) : pLength);
                }
            }
            if (pLength < 0)
            {
                return str;
            }
            byte[] sourceArray = Encoding.Default.GetBytes(pSrcString);
            if (sourceArray.Length <= pStartIndex)
            {
                return str;
            }
            int length = sourceArray.Length;
            if (sourceArray.Length > (pStartIndex + pLength))
            {
                length = pLength + pStartIndex;
            }
            else
            {
                pLength = sourceArray.Length - pStartIndex;
                pTailString = "";
            }
            int num2 = pLength;
            int[] numArray = new int[pLength];
            byte[] destinationArray = null;
            int num3 = 0;
            for (int i = pStartIndex; i < length; i++)
            {
                if (sourceArray[i] > 0x7f)
                {
                    num3++;
                    if (num3 == 3)
                    {
                        num3 = 1;
                    }
                }
                else
                {
                    num3 = 0;
                }
                numArray[i] = num3;
            }
            if ((sourceArray[length - 1] > 0x7f) && (numArray[pLength - 1] == 1))
            {
                num2 = pLength + 1;
            }
            destinationArray = new byte[num2];
            Array.Copy(sourceArray, pStartIndex, destinationArray, 0, num2);
            return (Encoding.Default.GetString(destinationArray) + pTailString);
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
    }
}
