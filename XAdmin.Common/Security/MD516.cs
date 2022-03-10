using System;
using System.Text;
using System.Security.Cryptography;

namespace XAdmin.Common.Security
{
    public class MD5Handle
    {
        /// <summary>
        /// MD5 16λ����
        /// </summary>
        /// <param name="ConvertString"></param>
        /// <returns></returns>
        public static string GetMd5HexStr(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }

        /**/
        /// <summary>
        /// MD5��32λ����
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md532(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//ʵ����һ��md5����
            // ���ܺ���һ���ֽ����͵����飬����Ҫע�����UTF8/Unicode�ȵ�ѡ��
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // ͨ��ʹ��ѭ�������ֽ����͵�����ת��Ϊ�ַ��������ַ����ǳ����ַ���ʽ������
            for (int i = 0; i < s.Length; i++)
            {
                // ���õ����ַ���ʹ��ʮ���������͸�ʽ����ʽ����ַ���Сд����ĸ�����ʹ�ô�д��X�����ʽ����ַ��Ǵ�д�ַ� 

                pwd = pwd + s[i].ToString("X");

            }
            return pwd;
        }

        /// <summary>
        /// ���ַ�������MD5��SHA1���ܲ���
        /// </summary>
        /// <param name="cleanString">�����ַ���</param>
        /// <returns>���ܺ���ַ���</returns>
        public static string Encrypt(string str)
        {
            Byte[] clearBytes = new UnicodeEncoding().GetBytes(str);
            Byte[] hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);
            return BitConverter.ToString(hashedBytes);
        }
    }
}
