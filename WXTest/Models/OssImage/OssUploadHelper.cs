using OSS;
using OSS.api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Oss.Models
{
    public class BaseHelper
    {
        public string BucketName { get; set; }
    }
    public class UploadImagesHelper: BaseHelper
    {
        public OssUploadImage Image { get; set; }

        public Stream Content { get; set; }//图片内容

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proCode">编号</param>
        /// <param name="imgData">图片基础数据</param>
        /// <param name="stream">文件流</param>
        public UploadImagesHelper(OssUploadImage imgData, Stream stream)
        {
            Image = imgData;
            Content = stream;
        }
        /// <summary>
        /// 上传背景
        /// </summary>
        /// <param name="proCode"></param>
        /// <returns></returns>
        public string UploadImg()
        {
            string imgUrl = "";
            try
            {
                string prefix = Image.PrefixName;
                string imgFileName = FileUrlTool.GetFileUrl(prefix);
                string key = Image.FilePath + imgFileName;
                PutObjectAPI.PutObjectFromStream( key, Content);
                imgUrl = FileUrlTool.GetFileUrl(key);
            }
            catch (Exception ex)
            {
                Log.Error("OssUploadHelper-UploadImg", ex.Message);
                imgUrl = "";
            }
            return imgUrl;
        }
    }

    public class UploadFileHelper : BaseHelper
    {
        public OssUploadFile OssFile { get; set; }

        public Stream Content { get; set; }//图片内容

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proCode">编号</param>
        /// <param name="imgData">图片基础数据</param>
        /// <param name="stream">文件流</param>
        public UploadFileHelper(OssUploadFile fileData, Stream stream)
        {
            OssFile = fileData;
            Content = stream;
        }
        /// <summary>
        /// 上传背景
        /// </summary>
        /// <param name="proCode"></param>
        /// <returns></returns>
        public string UploadFile()
        {
            string fileUrl = "";
            try
            {
                string prefix = OssFile.PrefixName;
                string fileName = prefix + OssFile.FormatName;
                string key = OssFile.FilePath + fileName;
                PutObjectAPI.PutObjectFromStream(key, Content);
                fileUrl = FileUrlTool.GetFileUrl(key);
            }
            catch (Exception ex)
            {
                Log.Error("OssUploadHelper-UploadFile", ex.Message);
                fileUrl = "";
            }
            return fileUrl;
        }
    }
    public class UploadVideoHelper : BaseHelper
    {
        public OssUploadVideo Video { get; set; }

        public Stream Content { get; set; }//图片内容

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proCode">编号</param>
        /// <param name="imgData">图片基础数据</param>
        /// <param name="stream">文件流</param>
        public UploadVideoHelper(OssUploadVideo video, Stream stream)
        {
            Video = video;
            Content = stream;
        }
        /// <summary>
        /// 上传背景
        /// </summary>
        /// <param name="proCode"></param>
        /// <returns></returns>
        public string UploadVideo()
        {
            string fileUrl = "";
            try
            {
                string prefix = Video.PrefixName;
                string fileName = prefix+ Video.FormatName;
                string key = Video.FilePath + fileName;
                PutObjectAPI.PutObjectFromStream(key, Content);
                fileUrl = FileUrlTool.GetFileUrl(key);
            }
            catch (Exception ex)
            {
                Log.Error("OssUploadHelper-UploadVideo", ex.Message);
                fileUrl = "";
            }
            return fileUrl;
        }
    }
}
