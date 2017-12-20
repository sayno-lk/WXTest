using Aliyun.OSS;
using Aliyun.OSS.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace OSS.api
{
    public class PutObjectAPI
    {
        static string accessKeyId = OssConfig.AccessKeyId;
        static string accessKeySecret = OssConfig.AccessKeySecret;
        static string endpoint = OssConfig.Endpoint;
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);
        static string fileToUpload = OssConfig.FileToUpload;
        static string bucketName = OssConfig.BucketName;
        public static string PutObjectFromFile()
        {
            const string key = "101.png";
            string r = "";
            try
            {
                // string path = HttpRuntime.AppDomainAppPath + fileToUpload;
                client.PutObject(bucketName, key, "D:/1.png");//"D:/1.png";
                Console.WriteLine("Put object:{0} succeeded", key);
                r = string.Format("Put object:{0} succeeded", key);
            }
            catch (OssException ex)
            {
                r = string.Format("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                r = string.Format("Failed with error info: {0}", ex.Message);
            }
            return r;
        }
        /// <summary>
        /// 上传文件：文件流（包括图片和其他文件）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="stream"></param>
        public static void PutObjectFromStream(string key, Stream stream)
        {
            try
            {
                client.PutObject(bucketName, key, stream);
                Console.WriteLine("Put object:{0} succeeded", key);
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }
        }
        public static string PutObjectFromFile(string key, MemoryStream stream)
        {
            string r = "";
            try
            {
                // string path = HttpRuntime.AppDomainAppPath + fileToUpload;
                client.PutObject(bucketName, key, stream);
                // Console.WriteLine("Put object:{0} succeeded", key);
                r = string.Format("succeeded");
            }
            catch (OssException ex)
            {
                //Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                //   ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
                r = string.Format("error1");
            }
            catch (Exception ex)
            {
                //kConsole.WriteLine("Failed with error info: {0}", ex.Message);
                r = string.Format("error2");
            }
            return r;
        }

        public static void PutObjectFromString(string bucketName)
        {
            const string key = "tttt/101.txt";
            const string str = "Aliyun OSS SDK for C#";

            try
            {
                byte[] binaryData = Encoding.ASCII.GetBytes(str);
                var stream = new MemoryStream(binaryData);

                client.PutObject(bucketName, key, stream);
                Console.WriteLine("Put object:{0} succeeded", key);
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }
        }
    }
}