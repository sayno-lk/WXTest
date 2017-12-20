using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSS
{
    internal class OssConfig    {

        public static string BucketName = "wx-main";

        public static string AccessKeyId = "LTAI2fmVk0vIP4FB";
        public static string AccessKeySecret = "xtIExch6ewKb3btKUCRGGExRYsjtHi";

        public static string Endpoint = "oss-cn-hangzhou.aliyuncs.com";

        public static string DirToDownload = HttpContext.Current.Request.PhysicalApplicationPath + "App_Data/images/";

        public static string FileToUpload = "img/2.png";//"bin文件下"

        public static string BigFileToUpload = "<your local big file to upload>";

    }
}