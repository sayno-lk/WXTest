using Oss.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXTest.DB;
using WXTest.Models;

namespace WXTest.Controllers
{
    public class OssUploadController : Controller
    {
        wxtest_dbEntities db = new wxtest_dbEntities();
        // GET: OssUpload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadProductImg(int pId, int index)
        {
            if (Request.Files.Count == 0)
            {
                return Json(new { result_code = 0, result_msg = "文件格式不正确或者文件过大！" });
            }
            HttpPostedFileBase uploadFile = Request.Files[0];//只示范上传一个文件  
            if (uploadFile == null || uploadFile.ContentLength <= 0)
            {
                return Json(new { result_code = 0, result_msg = "无上传文件" });
            }
            // 如果没有上传文件
            //if (filedata == null || string.IsNullOrEmpty(filedata.FileName) || filedata.ContentLength == 0)
            //{
            //    return Json(new { result_code = 0, result_msg = "无上传文件" });
            //}
            var uploadFileExtension = Path.GetExtension(uploadFile.FileName).ToLower();
            string imgName = pId + "_" + index;
            OssUploadFile img1 = new OssUploadFile("Getinge/ProductInfo", imgName, uploadFileExtension);
            UploadFileHelper imgHelper = new UploadFileHelper(img1, uploadFile.InputStream);
            string imgUrl = imgHelper.UploadFile();
            Getinge_Product_Info model = (from a in db.Getinge_Product_Info
                                          where a.product_id == pId && a.info_img_index==index&& a.info_type == GetingeProductInfoTypeEnum.images.ToString()
                                          select a).FirstOrDefault();               
            if (model != null)
            {
                model.info_content = imgUrl;
                model.info_date = DateTime.Now;
                db.SaveChanges();
            }
            else
            {
                model = new Getinge_Product_Info();
                model.info_type = GetingeProductInfoTypeEnum.images.ToString();
                model.info_content = imgUrl;
                model.product_id = pId;
                model.info_img_index = index;
                model.info_date = DateTime.Now;
                db.Getinge_Product_Info.Add(model);
                db.SaveChanges();
            }
            return Json(new { code = 1, msg = imgUrl });
        }

        [HttpPost]
        public ActionResult UploadProductVideo(int pId)
        {
            if (Request.Files.Count == 0)
            {
                return Json(new { result_code = 0, result_msg = "文件格式不正确或者文件过大！" });
            }
            HttpPostedFileBase uploadFile = Request.Files[0];//只示范上传一个文件  
            if (uploadFile == null || uploadFile.ContentLength <= 0)
            {
                return Json(new { result_code = 0, result_msg = "无上传文件" });
            }
            // 如果没有上传文件
            //if (filedata == null || string.IsNullOrEmpty(filedata.FileName) || filedata.ContentLength == 0)
            //{
            //    return Json(new { result_code = 0, result_msg = "无上传文件" });
            //}
            var uploadFileExtension = Path.GetExtension(uploadFile.FileName).ToLower();
            string imgName = pId + "_video" ;
            OssUploadFile img1 = new OssUploadFile("Getinge/ProductInfo", imgName, uploadFileExtension);
            UploadFileHelper imgHelper = new UploadFileHelper(img1, uploadFile.InputStream);
            string imgUrl = imgHelper.UploadFile();
            Getinge_Product_Info model = (from a in db.Getinge_Product_Info
                                          where a.product_id == pId && a.info_type == GetingeProductInfoTypeEnum.video.ToString()
                                          select a).FirstOrDefault();
            if (model != null)
            {
                model.info_content = imgUrl;
                model.info_date = DateTime.Now;
                db.SaveChanges();
            }
            else
            {
                model = new Getinge_Product_Info();
                model.info_type = GetingeProductInfoTypeEnum.video.ToString();
                model.info_content = imgUrl;
                model.product_id = pId;
                model.info_date = DateTime.Now;
                db.Getinge_Product_Info.Add(model);
                db.SaveChanges();
            }
            return Json(new { code = 1, msg = imgUrl });
        }
    }
}