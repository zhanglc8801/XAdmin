using XAdmin.Common.Security;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XAdmin.Common.Utils
{

    public enum MediaTypeHeader
    {
        application_json,
        application_xml
    }

    public class HttpClientHelper
    {
        public MediaTypeHeader mediaType { get; set; }

        private const string RQUESTHEADERTOKENKEY = "Authorization-Token";
        private const string AUTHSITE = "Request-Site";
        private const string AUTHSITEID = "Request-SiteID";

        public static readonly HttpClientHelper Instance;
        static HttpClientHelper()
        {
            Instance = new HttpClientHelper();
        }

        public T Get<T>(string url)
        {
            T returnResult;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = null;
                    client.GetAsync(url).ContinueWith(
                     (requestTask) =>
                     {
                         response = requestTask.Result;

                     }).Wait(60000);
                    if (response.IsSuccessStatusCode)
                    {
                        returnResult = response.Content.ReadAsAsync<T>().Result;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                        //returnResult = default(T);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    client.Dispose();
                }
            }
            return returnResult;
        }

        public T GetAuth<T>(string url)
        {
            T returnResult;
            using (var client = new HttpClient())
            {
                try
                {
                    //client.DefaultRequestHeaders.Add(RQUESTHEADERTOKENKEY, RSAClass.Encrypt(GetToken()));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetJWToken());
                    client.DefaultRequestHeaders.Add(AUTHSITEID, SiteConfigManager.SiteID);
                    client.DefaultRequestHeaders.Add(AUTHSITE, SiteConfigManager.SiteDomain);
                    HttpResponseMessage response = null;
                    client.GetAsync(url).ContinueWith(
                     (requestTask) =>
                     {
                         response = requestTask.Result;

                     }).Wait(60000);
                    if (response.IsSuccessStatusCode)
                    {
                        returnResult = response.Content.ReadAsAsync<T>().Result;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                        //returnResult = default(T);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    client.Dispose();
                }
            }
            return returnResult;
        }

        #region Post请求

        public T PostAuth<T, PT>(string url, PT objectParam)
        {
            T returnResult;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //client.DefaultRequestHeaders.Add(RQUESTHEADERTOKENKEY, RSAClass.Encrypt(GetToken()));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetJWToken());
                    client.DefaultRequestHeaders.Add(AUTHSITEID, SiteConfigManager.SiteID);
                    client.DefaultRequestHeaders.Add(AUTHSITE, SiteConfigManager.SiteDomain);
                    HttpResponseMessage response = null;
                    client.PostAsJsonAsync(url, objectParam).ContinueWith((requestTask) =>
                    {
                        response = requestTask.Result;
                    }).Wait(60000);

                    if (response.IsSuccessStatusCode)
                    {
                        returnResult = response.Content.ReadAsAsync<T>().Result;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                        //returnResult = default(T);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    client.Dispose();
                }
            }
            return returnResult;
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="url"></param>        
        /// <returns></returns>
        public async Task<T> PostAuthAsync<T, PT>(string url, PT objectParam)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //client.DefaultRequestHeaders.Add(RQUESTHEADERTOKENKEY, RSAClass.Encrypt(GetToken()));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetJWToken());
                    client.DefaultRequestHeaders.Add(AUTHSITEID, SiteConfigManager.SiteID);
                    client.DefaultRequestHeaders.Add(AUTHSITE, SiteConfigManager.SiteDomain);
                    T responseData;
                    client.Timeout = TimeSpan.FromSeconds(30);
                    using (var response = await client.PostAsJsonAsync(url, objectParam))
                    {
                        responseData = await response.Content.ReadAsAsync<T>();
                    }
                    return responseData;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    client.Dispose();
                }
            }

        }

        #endregion

        ///// <summary>
        ///// 返回Token
        ///// </summary>
        ///// <returns></returns>
        //private string GetToken()
        //{
        //    return SiteConfigManager.SiteDomain + SiteConfigManager.SiteID + DateTime.Now.ToString("yyyyMMdd");
        //}

        /// <summary>
        /// 返回Token
        /// </summary>
        /// <returns></returns>
        private string GetJWToken()
        {
            //生成Token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.GetJson("SecurityKey") + DateTime.Now.ToString("yyyyMMdd")));
            //List<Claim> claims = new List<Claim>();
            //claims.Add(new Claim("Name", model.UserModel.UserName));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                //claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
                );
            string t = new JwtSecurityTokenHandler().WriteToken(token);
            return t;
        }

    }
}
