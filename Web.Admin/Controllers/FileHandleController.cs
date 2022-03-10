using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using XAdmin.Common;
using XAdmin.Common.Utils;
using XAdmin.IService;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using Microsoft.AspNetCore.StaticFiles;

namespace Web.Admin.Controllers
{
    public class FileHandleController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IResourcesService _resourcesService;
        private readonly IUserInfoService _userInfoService;
        private readonly ISystemManagementService _sysService;
        public FileHandleController(IWebHostEnvironment webHostEnvironment,IResourcesService resourcesService, IUserInfoService userInfoService, ISystemManagementService sysService)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._resourcesService = resourcesService;
            this._userInfoService = userInfoService;
            this._sysService = sysService;
        }

        /// <summary>
        /// 文件存放路径
        /// </summary>
        private string UploadPath = SiteConfigManager.UploadPath;

        #region 获取指定文件的已上传的文件块
        /// <summary>
        /// 获取指定文件的已上传的文件块
        /// </summary>
        /// <returns></returns>
        public IActionResult GetMaxChunk()
        {
            int HaveFile = 0;//文件是否已存在
            string SavePath = _webHostEnvironment.ContentRootPath + UploadPath;
            try
            {
                var md5 = Convert.ToString(Request.Query["md5"]);
                var ext = Convert.ToString(Request.Query["ext"]);
                int chunk = 0;
                var fileName = md5 + "." + ext;

                FileInfo file = new FileInfo(SavePath + fileName);
                if (file.Exists)
                {
                    chunk = Int32.MaxValue;
                    HaveFile = 1;
                }
                else
                {
                    if (Directory.Exists(SavePath + "chunk\\" + md5))
                    {
                        DirectoryInfo dicInfo = new DirectoryInfo(SavePath + "chunk\\" + md5);
                        var files = dicInfo.GetFiles();
                        chunk = files.Count();
                        if (chunk > 1)
                        {
                            chunk = chunk - 1; //当文件上传中时，页面刷新，上传中断，这时最后一个保存的块的大小可能会有异常，所以这里直接删除最后一个块文件
                        }
                    }
                }
                string savePath = System.Web.HttpUtility.UrlEncode(SavePath + fileName);//保存路径
                string virtualPath = System.Web.HttpUtility.UrlEncode(UploadPath.Replace("\\","/") + fileName);//虚拟路径
                if ((".avi;.rmvb;.wmv;.mkv;.mpg;.flv;.ts").Contains(ext))
                {
                    return Json(new { code = -1, msg = "请上传MP4格式的视频", chunk = chunk, savePath = savePath, virtualPath = virtualPath, HaveFile = HaveFile });
                }
                else
                {
                    return Json(new { code = 1, msg = "", chunk = chunk, savePath = savePath, virtualPath = virtualPath, HaveFile = HaveFile });
                } 
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = ex.Message, chunk=0, HaveFile= HaveFile });
            }
        }
        #endregion

        #region 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileData"></param>
        /// <returns></returns>
        public IActionResult Upload()
        {
            var file = Request.Form.Files[0];
            string SavePath = _webHostEnvironment.ContentRootPath + UploadPath;

            string md5_key = string.Format("{0}md5", Request.Form["id"]);
            string md5_val = Request.Form[md5_key];
            //如果进行了分片
            if (Request.Form.Keys.Any(m => m == "chunk"))
            {
                //取得chunk和chunks
                int chunk = Convert.ToInt32(Request.Form["chunk"]);//当前分片在上传分片中的顺序（从0开始）
                int chunks = Convert.ToInt32(Request.Form["chunks"]);//总分片数
                //根据GUID创建用该GUID命名的临时文件夹
                string folder = SavePath + "chunk\\" + md5_val + "\\";
                string path = folder + chunk;

                //建立临时传输文件夹
                if (!Directory.Exists(Path.GetDirectoryName(folder)))
                {
                    Directory.CreateDirectory(folder);
                }

                FileStream addFile = null;
                BinaryWriter AddWriter = null;
                Stream stream = null;
                BinaryReader TempReader = null;
                try
                {
                    //addFile = new FileStream(path, FileMode.Append, FileAccess.Write);
                    addFile = new FileStream(path, FileMode.Create, FileAccess.Write);
                    AddWriter = new BinaryWriter(addFile);
                    //获得上传的分片数据流
                    stream = file.OpenReadStream();
                    TempReader = new BinaryReader(stream);
                    //将上传的分片追加到临时文件末尾
                    AddWriter.Write(TempReader.ReadBytes((int)stream.Length));
                }
                finally
                {
                    if (addFile != null)
                    {
                        addFile.Close();
                        addFile.Dispose();
                    }
                    if (AddWriter != null)
                    {
                        AddWriter.Close();
                        AddWriter.Dispose();
                    }
                    if (stream != null)
                    {
                        stream.Close();
                        stream.Dispose();
                    }
                    if (TempReader != null)
                    {
                        TempReader.Close();
                        TempReader.Dispose();
                    }
                }
                string savePath = SavePath + md5_val + Path.GetExtension(Request.Form.Files[0].FileName);
                string virtualPath = System.Web.HttpUtility.UrlEncode(UploadPath.Replace("\\", "/") + md5_val + Path.GetExtension(Request.Form.Files[0].FileName));//虚拟路径
                return Json(new { code = 1, msg = "", chunked = true, ext= Path.GetExtension(file.FileName), savePath = System.Web.HttpUtility.UrlEncode(savePath), virtualPath = virtualPath });
            }
            else//没有分片直接保存
            {
                string path = SavePath + md5_val + Path.GetExtension(Request.Form.Files[0].FileName);
                string virtualPath = System.Web.HttpUtility.UrlEncode(UploadPath.Replace("\\", "/") + md5_val + Path.GetExtension(Request.Form.Files[0].FileName));//虚拟路径
                //存储文件
                using (FileStream fs = System.IO.File.Create(path))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                return Json(new { code = 0, msg = "", chunked=false, savePath= System.Web.HttpUtility.UrlEncode(path), virtualPath= virtualPath });
            }
        }
        #endregion

        #region 合并文件
        /// <summary>
        /// 合并文件
        /// </summary>
        /// <returns></returns>
        public IActionResult MergeFiles()
        {
            string SavePath = _webHostEnvironment.ContentRootPath + UploadPath;

            string guid = Request.Form["md5"];
            string ext = Request.Form["ext"];
            string sourcePath = Path.Combine(SavePath, "chunk\\" + guid + "\\");//源数据文件夹
            string targetPath = Path.Combine(SavePath, guid + ext);//合并后的文件
            string virtualPath = System.Web.HttpUtility.UrlEncode(UploadPath.Replace("\\","/") + guid + ext);//虚拟路径

            DirectoryInfo dicInfo = new DirectoryInfo(sourcePath);
            if (Directory.Exists(Path.GetDirectoryName(sourcePath)))
            {
                FileInfo[] files = dicInfo.GetFiles();
                foreach (FileInfo file in files.OrderBy(f => int.Parse(f.Name)))
                {
                    FileStream addFile = new FileStream(targetPath, FileMode.Append, FileAccess.Write);
                    BinaryWriter AddWriter = new BinaryWriter(addFile);

                    //获得上传的分片数据流 
                    Stream stream = file.Open(FileMode.Open);
                    BinaryReader TempReader = new BinaryReader(stream);
                    //将上传的分片追加到临时文件末尾
                    AddWriter.Write(TempReader.ReadBytes((int)stream.Length));
                    //关闭BinaryReader文件阅读器
                    TempReader.Close();
                    stream.Close();
                    AddWriter.Close();
                    addFile.Close();

                    TempReader.Dispose();
                    stream.Dispose();
                    AddWriter.Dispose();
                    addFile.Dispose();
                }
                DeleteFolder(sourcePath);
                return Json(new { code=0,msg="", chunked=true, hasError= false, savePath= System.Web.HttpUtility.UrlEncode(targetPath), virtualPath= virtualPath });
            }
            else
            {
                return Json(new { code = 0, msg = "", hasError=true });
            }
        }
        #endregion

        #region 删除文件夹及其内容
        /// <summary>
        /// 删除文件夹及其内容
        /// </summary>
        /// <param name="dir"></param>
        private void DeleteFolder(string strPath)
        {
            //删除这个目录下的所有子目录
            if (Directory.GetDirectories(strPath).Length > 0)
            {
                foreach (string fl in Directory.GetDirectories(strPath))
                {
                    Directory.Delete(fl, true);
                }
            }
            //删除这个目录下的所有文件
            if (Directory.GetFiles(strPath).Length > 0)
            {
                foreach (string f in Directory.GetFiles(strPath))
                {
                    System.IO.File.Delete(f);
                }
            }
            Directory.Delete(strPath, true);
        }
        #endregion

        #region 更新用户上传记录
        /// <summary>
        /// 更新用户上传记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddUploadRecord(string fileName,long fileSize,string fileExt,string fileMd5,string filePath,string virtualPath)
        {
            virtualPath = virtualPath.Replace("\\", "/");
            ResourcesQuery query = new ResourcesQuery();
            query.SiteID = CurUser.UserModel.SiteID;
            query.FileMD5 = fileMd5.ToUpper();
            query.UserId = CurUser.UserModel.ID;
            Resources ResourcesModel = _resourcesService.GetResourcesModelByMD5(query);
            long linkFileID = 0;
            if (ResourcesModel != null)
            {
                linkFileID = ResourcesModel.ID;
            }

            #region 保存Resources信息
            Resources model = new Resources();
            model.SiteID = CurUser.UserModel.SiteID;
            model.FileName = Path.GetFileName(filePath);
            model.ClientFileName = fileName;
            model.FileSize = fileSize;
            model.FileType = Path.GetExtension(fileName);
            model.SavePath = virtualPath;
            model.UploadDate = DateTime.Now;
            model.UserId = CurUser.UserModel.ID;
            model.IsDeleted = false;
            model.FileClass = "File";
            model.FileMD5 = fileMd5.ToUpper();
            model.LinkFileID = linkFileID;
            ExecResult result = _resourcesService.InsertResources(model);
            #endregion

            if ((".mp4|.flv|.avi|.mkv|.mpg|.wmv").Contains(fileExt))
            {
                //获取视频文件信息
                string ffmpegPath = _webHostEnvironment.WebRootPath + "\\ffmpeg";
                string contentRootPath = _webHostEnvironment.ContentRootPath;

                string thumbnail = virtualPath.Substring(0, virtualPath.LastIndexOf('/')) + "/VideoImage/" + Path.GetFileName(virtualPath) + ".png";
                string ThumbnailPath = Path.GetFullPath(contentRootPath + thumbnail);
                try
                {
                    VideoInfo vi = await FFmpegUtil.GetVideoInfo(ffmpegPath, filePath);
                    FFmpegUtil.GetVideoThumbnail(ffmpegPath, filePath, ThumbnailPath, 50);
                    return Json(new { code = 1, length = vi.Length, height = vi.Height, width = vi.Width, Thumbnail = thumbnail });
                }
                catch (Exception ex)
                {
                    return Json(new { code = 1, msg = ex.Message });
                }
            }

            return Json(new { code = 1 });
        }
        #endregion

        #region 上传头像
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public IActionResult UploadPhoto()
        {
            var file = Request.Form.Files[0];
            string webRootPath = _webHostEnvironment.WebRootPath;
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string[] pictureFormatArray = { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };

            if (file.Length > 2097152)
            {
                return Json(new { code = "-1", src = "", msg = "上传文件超过大小限制,最大可上传2MB的图片文件。" });
            }
            else if (!pictureFormatArray.Contains(Path.GetExtension(file.FileName.ToLower())))
            {
                return Json(new { code = "-1", src = "", msg = "不允许上传此类型的文件！文件类型：" + Path.GetExtension(file.FileName) });
            }
            else
            {
                string filename = Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(file.FileName));
                string HeadPhotoPath = SiteConfigManager.HeadPhotoPath + DateTime.Now.Year + "/";
                string SavePath = contentRootPath + HeadPhotoPath;
                string SaveFileName = SavePath + filename;
                string fileMd5 = FileHelper.MD5Stream(file.OpenReadStream());
                if (!Directory.Exists(SavePath))
                {
                    Directory.CreateDirectory(SavePath);
                }
                ResourcesQuery query = new ResourcesQuery();
                query.SiteID = CurUser.UserModel.SiteID;
                query.FileMD5 = fileMd5;
                query.UserId = CurUser.UserModel.ID;
                Resources ResourcesModel = _resourcesService.GetResourcesModelByMD5(query);
                if (ResourcesModel != null)
                {
                    #region 更新Cookie中的Photo信息
                    LoginUserInfo LoginUserInfoModel = new LoginUserInfo();
                    LoginUserInfoModel.UserModel = CurUser.UserModel;
                    LoginUserInfoModel.CurRole = CurUser.CurRole;
                    LoginUserInfoModel.RoleList = CurUser.RoleList;
                    LoginUserInfoModel.UserModel.Photo = ResourcesModel.SavePath;
                    //CookieHelper.SetCookie("FASTPLAT_USERDATA", JsonConvert.SerializeObject(LoginUserInfoModel),30);
                    #endregion

                    #region 保存Resources信息
                    Resources model = new Resources();
                    model.SiteID = CurUser.UserModel.SiteID;
                    model.FileName = ResourcesModel.FileName;
                    model.ClientFileName = file.FileName;
                    model.FileSize = file.Length;
                    model.FileType = Path.GetExtension(filename);
                    model.SavePath = ResourcesModel.SavePath;
                    model.UploadDate = DateTime.Now;
                    model.UserId = CurUser.UserModel.ID;
                    model.IsDeleted = false;
                    model.FileClass = "Photo";
                    model.FileMD5 = "";
                    model.LinkFileID = ResourcesModel.ID;
                    ExecResult result = _resourcesService.InsertResources(model);
                    #endregion

                    return Json(new { code = "0", src = ResourcesModel.SavePath, msg = "已上传过该文件" });
                }
                else
                {
                    #region MyRegion
                    //保存文件信息
                    Resources model = new Resources();
                    model.SiteID = CurUser.UserModel.SiteID;
                    model.FileName = filename;
                    model.ClientFileName = file.FileName;
                    model.FileSize = file.Length;
                    model.FileType = Path.GetExtension(filename);
                    model.SavePath = HeadPhotoPath.Replace("\\", "/") + filename;
                    model.UploadDate = DateTime.Now;
                    model.UserId = CurUser.UserModel.ID;
                    model.IsDeleted = false;
                    model.FileClass = "Photo";
                    model.FileMD5 = fileMd5;
                    ExecResult result = _resourcesService.InsertResources(model);
                    if (result.code == 1)
                    {
                        //存储文件
                        using (FileStream fs = System.IO.File.Create(SaveFileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                        //更改用户头像字段信息
                        UserInfo userModel = new UserInfo();
                        userModel.ID = CurUser.UserModel.ID;
                        userModel.Photo = HeadPhotoPath.Replace("\\", "/") + filename;
                        ExecResult ChangeResult = _userInfoService.ChangeUserPhoto(userModel);
                        #region 更新Cookie中的Photo信息
                        LoginUserInfo LoginUserInfoModel = new LoginUserInfo();
                        LoginUserInfoModel.UserModel = CurUser.UserModel;
                        LoginUserInfoModel.CurRole = CurUser.CurRole;
                        LoginUserInfoModel.RoleList = CurUser.RoleList;
                        LoginUserInfoModel.UserModel.Photo = userModel.Photo;
                        //CookieHelper.SetCookie("FASTPLAT_USERDATA", JsonConvert.SerializeObject(LoginUserInfoModel),30);

                        var claims = new List<Claim>
                        {
                            new Claim("UserID", CurUser.UserModel.ID.ToString()),
                            new Claim("UserData", JsonConvert.SerializeObject(LoginUserInfoModel)),
                            new Claim("autologin","0"),
                        };
                        var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));


                        #endregion
                        return Json(new { code = "0", src = HeadPhotoPath.Replace("\\", "/") + filename, msg = "上传成功！" });
                    }
                    else
                    {
                        return Json(new { code = "-1", src = "", msg = result.msg });
                    }
                    #endregion
                }
            }
        }
        #endregion

        #region tinymce编辑器用图片上传处理
        /// <summary>
        /// tinymce编辑器用图片上传处理
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public IActionResult TinymceUpImage()
        {
            var file = Request.Form.Files[0];
            string webRootPath = _webHostEnvironment.WebRootPath;
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string[] pictureFormatArray = { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };

            if (file.Length > 5242880)
            {
                return Json(new { code = "-1", src = "", msg = "上传文件超过大小限制,最大可上传5MB的图片文件。" });
            }
            else if (!pictureFormatArray.Contains(Path.GetExtension(file.FileName.ToLower())))
            {
                return Json(new { code = "-1", src = "", msg = "不允许上传此类型的文件！文件类型：" + Path.GetExtension(file.FileName) });
            }
            else
            {
                string filename = Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(file.FileName));
                string ImagePath = "/Files/TinymceImg/" + DateTime.Now.Year + "/";
                string SavePath = contentRootPath + ImagePath;
                string SaveFileName = SavePath + filename;
                string fileMd5 = FileHelper.MD5Stream(file.OpenReadStream());
                if (!Directory.Exists(SavePath))
                {
                    Directory.CreateDirectory(SavePath);
                }
                ResourcesQuery query = new ResourcesQuery();
                query.FileMD5 = fileMd5;
                query.UserId = CurUser.UserModel.ID;
                Resources ResourcesModel = _resourcesService.GetResourcesModelByMD5(query);
                if (ResourcesModel != null)
                {
                    #region 保存Resources信息
                    Resources model = new Resources();
                    model.SiteID = CurUser.UserModel.SiteID;
                    model.FileName = ResourcesModel.FileName;
                    model.ClientFileName = file.FileName;
                    model.FileSize = file.Length;
                    model.FileType = Path.GetExtension(filename);
                    model.SavePath = ResourcesModel.SavePath;
                    model.UploadDate = DateTime.Now;
                    model.UserId = CurUser.UserModel.ID;
                    model.IsDeleted = false;
                    model.FileClass = "Editor";
                    model.FileMD5 = "";
                    model.LinkFileID = ResourcesModel.ID;
                    ExecResult result = _resourcesService.InsertResources(model);
                    #endregion
                    return Json(new { code = "0", src = ResourcesModel.SavePath, data = ResourcesModel.SavePath, msg = "已上传过该文件" });
                }
                else
                {
                    #region MyRegion
                    //保存文件信息
                    Resources model = new Resources();
                    model.SiteID = CurUser.UserModel.SiteID;
                    model.FileName = filename;
                    model.ClientFileName = file.FileName;
                    model.FileSize = file.Length;
                    model.FileType = Path.GetExtension(filename);
                    model.SavePath = ImagePath.Replace("\\", "/") + filename;
                    model.UploadDate = DateTime.Now;
                    model.UserId = CurUser.UserModel.ID;
                    model.IsDeleted = false;
                    model.FileClass = "Editor";
                    model.FileMD5 = fileMd5;
                    ExecResult result = _resourcesService.InsertResources(model);
                    if (result.code == 1)
                    {
                        //存储文件
                        using (FileStream fs = System.IO.File.Create(SaveFileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                        return Json(new { code = "0", src = ImagePath.Replace("\\", "/") + filename, data = ImagePath + filename, msg = "上传成功！" });
                    }
                    else
                    {
                        return Json(new { code = "-1", src = "", msg = result.msg });
                    }
                    #endregion
                }
            }
        }
        #endregion

        #region 标题图片上传
        /// <summary>
        /// 标题图片上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public IActionResult UpTitlePhoto()
        {
            var file = Request.Form.Files[0];
            string webRootPath = _webHostEnvironment.WebRootPath;
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string[] pictureFormatArray = { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };

            if (file.Length > 2097152)
            {
                return Json(new { code = "-1", src = "", msg = "上传文件超过大小限制,最大可上传2MB的图片文件。" });
            }
            else if (!pictureFormatArray.Contains(Path.GetExtension(file.FileName.ToLower())))
            {
                return Json(new { code = "-1", src = "", msg = "不允许上传此类型的文件！文件类型：" + Path.GetExtension(file.FileName) });
            }
            else
            {
                string filename = Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(file.FileName));
                string ImagePath = "/Files/TitlePhoto/" + DateTime.Now.Year + "/";
                string SavePath = contentRootPath + ImagePath;
                string SaveFileName = SavePath + filename;
                string fileMd5 = FileHelper.MD5Stream(file.OpenReadStream());
                System.Drawing.Image image = System.Drawing.Image.FromStream(file.OpenReadStream());

                if (!Directory.Exists(SavePath))
                {
                    Directory.CreateDirectory(SavePath);
                }
                ResourcesQuery query = new ResourcesQuery();
                query.SiteID = CurUser.UserModel.SiteID;
                query.FileMD5 = fileMd5;
                query.UserId = CurUser.UserModel.ID;
                Resources ResourcesModel = _resourcesService.GetResourcesModelByMD5(query);
                if (ResourcesModel != null)
                {
                    #region 保存Resources信息
                    //保存文件信息
                    Resources model = new Resources();
                    model.SiteID = CurUser.UserModel.SiteID;
                    model.FileName = ResourcesModel.FileName;
                    model.ClientFileName = file.FileName;
                    model.FileSize = file.Length;
                    model.FileType = Path.GetExtension(filename);
                    model.SavePath = ResourcesModel.SavePath;
                    model.UploadDate = DateTime.Now;
                    model.UserId = CurUser.UserModel.ID;
                    model.IsDeleted = false;
                    model.FileClass = "TitlePhoto";
                    model.FileMD5 = "";
                    model.LinkFileID = ResourcesModel.ID;
                    ExecResult result = _resourcesService.InsertResources(model);
                    #endregion
                    return Json(new { code = "0", src = ResourcesModel.SavePath, data = ResourcesModel.SavePath,width= image.Width,height=image.Height, msg = "已上传过该文件" });
                }
                else
                {
                    #region MyRegion
                    //保存文件信息
                    Resources model = new Resources();
                    model.SiteID = CurUser.UserModel.SiteID;
                    model.FileName = filename;
                    model.ClientFileName = file.FileName;
                    model.FileSize = file.Length;
                    model.FileType = Path.GetExtension(filename);
                    model.SavePath = ImagePath.Replace("\\", "/") + filename;
                    model.UploadDate = DateTime.Now;
                    model.UserId = CurUser.UserModel.ID;
                    model.IsDeleted = false;
                    model.FileClass = "TitlePhoto";
                    model.FileMD5 = fileMd5;
                    ExecResult result = _resourcesService.InsertResources(model);
                    if (result.code == 1)
                    {
                        //存储文件
                        using (FileStream fs = System.IO.File.Create(SaveFileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                        return Json(new { code = "0", src = ImagePath.Replace("\\", "/") + filename, width = image.Width, height = image.Height, data = ImagePath + filename, msg = "上传成功！" });
                    }
                    else
                    {
                        return Json(new { code = "-1", src = "", width = 0, height = 0, msg = result.msg });
                    }
                    #endregion
                }
            }
        }
        #endregion

        #region 判断文件是否存在
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public IActionResult Exists(string filePath)
        {
            FileInfo fi = new FileInfo(GetRealPath(filePath));
            if (fi.Exists)
                return Json(new { data = 1 });
            else
                return Json(new { data = 0 });
        }
        #endregion

        #region 删除文件
        /// <summary>
        /// 删除文件
        /// 未完成 还需要修改表Resources中的数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public IActionResult DeleteFile(string filePath)
        {
            filePath = GetRealPath(filePath);
            FileInfo fi = new FileInfo(filePath);
            if (fi.Exists)
            {
                //string fileMd5 = FileHelper.MD5Stream(filePath);
                fi.Delete();  
                return Json(new { data = 1,msg="已删除" });
            }
            else
                return Json(new { data = 0,msg="要删除的文件不存在" });
        }
        #endregion

        #region 获取图片宽高
        /// <summary>
        /// 获取图片宽高
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public IActionResult GetImageSize(string filePath)
        {
            filePath = GetRealPath(filePath);
            FileInfo fi = new FileInfo(filePath);
            if (fi.Exists)
            {
                Bitmap pic = new Bitmap(filePath);
                return Json(new { width = pic.Width, height = pic.Height });
            }
            else
            {
                return Json(new { width = 600, height = 300 });
            }
        } 
        #endregion

        #region 获取真实路径
        /// <summary>
        /// 获取真实路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string GetRealPath(string filePath)
        {
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            return Path.GetFullPath(contentRootPath+ filePath);
        }
        #endregion

        /// <summary>
        /// 下载文件内
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public FileResult DownloadFile(string filePath)
        {
            //获取文件的ContentType
            var provider = new FileExtensionContentTypeProvider();
            var memi = provider.Mappings[Path.GetExtension(filePath)];
            var stream = System.IO.File.OpenRead(GetRealPath(filePath));
            return File(stream, memi, Path.GetFileName(filePath));
        }


    }
}