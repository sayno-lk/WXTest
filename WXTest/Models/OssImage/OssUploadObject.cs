using Oss.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oss.Models
{
    public class BaseOssFileData
    {
        public string FilePath { get; set; }
        public string PrefixName { get; set; }
        public string FormatName { get; set; }
    }
    public class OssUploadImage: BaseOssFileData
    {
        public OssUploadImage(string proName,string prefix)
        {
            FilePath = RootPath.Root + proName+"/";
            PrefixName = prefix;
            FormatName = "png";
        }
        /// <summary>
        /// 指定格式的图片文件
        /// </summary>
        /// <param name="proName">项目名称</param>
        /// <param name="prefix">图片名称（前缀）</param>
        /// <param name="formatName">图片格式</param>
        public OssUploadImage(string proName, string prefix, string formatName)
        {
            FilePath = RootPath.Root + proName + "/";
            PrefixName = prefix;
            FormatName = formatName;
        }
    }

    public class OssUploadFile : BaseOssFileData
    {
        /// <summary>
        /// 指定格式的图片文件
        /// </summary>
        /// <param name="proName">项目名称</param>
        /// <param name="prefix">图片名称（前缀）</param>
        /// <param name="formatName">图片格式</param>
        public OssUploadFile(string proName, string prefix, string formatName)
        {
            FilePath = RootPath.Root + proName + "/";
            PrefixName = prefix;
            FormatName = formatName;
        }
    }
    public class OssUploadVideo : BaseOssFileData
    {
   
        /// <summary>
        /// 视频文件
        /// </summary>
        /// <param name="proName">项目名称</param>
        /// <param name="prefix">文件名称（前缀）</param>
        /// <param name="formatName">文件格式</param>
        public OssUploadVideo(string proName, string prefix,string formatName)
        {
            FilePath = RootPath.Root + proName + "/";
            PrefixName = prefix;
            FormatName = formatName;
        }
    }
}