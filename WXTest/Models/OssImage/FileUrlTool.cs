using OSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public static class FileUrlTool
{
    static string bucketName = OssConfig.BucketName;
    static string endpoint = OssConfig.Endpoint;

    /// <summary>
    /// 获取图片名称
    /// </summary>
    /// <param name="code">项目Code</param>
    /// <param name="prefix">类型前缀</param>
    /// <param name="imgFormat">格式</param>
    /// <returns></returns>
    public static string GetFileName(string filePrefix, string fileFormat)
    {
        return filePrefix + "." + fileFormat;
    }
    /// <summary>
    /// 返回图片的oss地址
    /// </summary>
    /// <param name="key">ossKey</param>
    /// <returns></returns>
    public static string GetFileUrl(string key)
    {
        string imgUrl = "http://" + bucketName + "." + endpoint + "/" + key + "?v=" + DateTime.Now.ToString("ssfff");
        return imgUrl;
    }
}

