using AutoModule.Models;
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
    public class AdminController : Controller
    {
        wxtest_dbEntities db = new wxtest_dbEntities();

        // GET: Admin
        #region  1.Ecmo
        EcmoBll bll = new EcmoBll();
        public ActionResult AdminEcmo()
        {
            return View();
        }
        #region 1.Ecmo-后台接口
        [HttpPost]
        public ActionResult GetAdminEcmoUser(int page, int limit)
        {
            var list = bll.GetEcmoUserList();
            var data = list.Skip((page - 1) * limit).Take(limit).ToList();
            return Json(new { code = 0, msg = "", count = list.Count, data = data }, JsonRequestBehavior.DenyGet);
        }


        [HttpPost]
        public ActionResult DelEcmoUserData()
        {
            List<Ecmo_User> list = (from a in db.Ecmo_User
                                    select a).ToList();
            db.Ecmo_User.RemoveRange(list);
            db.SaveChanges();
            return Json(new { code = 1, msg = "删除成功" }, JsonRequestBehavior.DenyGet);
        }

        public ActionResult ExportEcmoUser()
        {
            var list = bll.GetEcmoUserList();
            MemoryStream stream = ImportExportTool_T.Export(list, 8);//导出数据

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "EcmoUser.xlsx");
        }

        #endregion
        #endregion

        #region  2.Getinge
        GetingeProductPrantsBll productTreeBll = new GetingeProductPrantsBll();
        public ActionResult AdminGetinge()
        {
            string str = ""; 
            return View();
        }
        public ActionResult AdminGetingeProductEdit(int productId)
        {

            var model = (from a in db.Getinge_Product
                         where a.product_id == productId
                         select a).FirstOrDefault();

            List<GetingeProductPrants> tree = productTreeBll.GetGetingeProductPrantsList(productId);
            // tree = tree.OrderBy(q => q.parent_id).ToList();
            ViewBag.ProName = tree;    
            var list = (from a in db.Getinge_Product_Info
                        where a.product_id == productId
                        orderby a.info_img_index ascending
                        select a).ToList();
            return View(list);
        }
        #region 2.Getinge-后台接口
        [HttpPost]
        public ActionResult GetAdminGetingeProduct()
        {
            var list = (from a in db.Getinge_Product
                        select a).ToList();
            var data = list.Skip(0).Take(10).ToList();
            return Json(new { code = 1, msg = "", count = list.Count, data = list }, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public ActionResult AddWord(int pId, string content)
        {
            Getinge_Product_Info model = (from a in db.Getinge_Product_Info
                                          where a.product_id == pId && a.info_type == GetingeProductInfoTypeEnum.word.ToString()
                                          select a).FirstOrDefault();
            //content = HttpUtility.UrlDecode(content);
            if (model != null)
            {
                model.info_content = content;
                model.info_date = DateTime.Now;
                db.SaveChanges();
            }
            else
            {
                model = new Getinge_Product_Info();
                model.info_type = GetingeProductInfoTypeEnum.word.ToString();
                model.info_content = content;
                model.product_id = pId;
                model.info_date = DateTime.Now;
                db.Getinge_Product_Info.Add(model);
                db.SaveChanges();
            }
            return Json(new { code = 1, msg = "OK" });
        }
        [HttpPost]
        public ActionResult DelGetingeProductVideo(int pId, string pType)
        {
            List<Getinge_Product_Info> list = (from a in db.Getinge_Product_Info
                                               where a.product_id == pId && a.info_type == GetingeProductInfoTypeEnum.video.ToString()
                                               select a).ToList();
            if (list.Count > 0)
            {
                db.Getinge_Product_Info.RemoveRange(list);
                db.SaveChanges();
                return Json(new { code = 1, msg = "删除成功" }, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(new { code = 0, msg = "不存在" }, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        public ActionResult DelGetingeProductWord(int pId, string pType)
        {
            List<Getinge_Product_Info> list = (from a in db.Getinge_Product_Info
                                               where a.product_id == pId && a.info_type == GetingeProductInfoTypeEnum.word.ToString()
                                               select a).ToList();
            db.Getinge_Product_Info.RemoveRange(list);
            db.SaveChanges();
            return Json(new { code = 1, msg = "删除成功" }, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public ActionResult DelGetingeProductImages(int pId, string pType)
        {
            List<Getinge_Product_Info> list = (from a in db.Getinge_Product_Info
                                               where a.product_id == pId && a.info_type == GetingeProductInfoTypeEnum.images.ToString()
                                               select a).ToList();
            db.Getinge_Product_Info.RemoveRange(list);
            db.SaveChanges();
            return Json(new { code = 1, msg = "删除成功" }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult UpDateGetingeProductName(int productId, string productName)
        {
            Getinge_Product model = (from a in db.Getinge_Product
                                     where a.product_id == productId
                                     select a).FirstOrDefault();
            if (model != null)
            {
                model.product_name = productName;
                db.SaveChanges();
                return Json(new { code = 1, msg = "更新成功" }, JsonRequestBehavior.DenyGet);

            }
            else
            {
                return Json(new { code = 0, msg = "不存在" }, JsonRequestBehavior.DenyGet);
            }

        }
        public ActionResult ExportEcmoUser1()
        {
            var list = bll.GetEcmoUserList();
            MemoryStream stream = ImportExportTool_T.Export(list, 8);//导出数据

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "EcmoUser.xlsx");
        }

        #endregion
        #endregion
    }
}