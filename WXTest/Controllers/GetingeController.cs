using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXTest.DB;
using WXTest.Models;

namespace WXTest.Controllers
{
    public class GetingeController : Controller
    {
        // GET: Getinge
        wxtest_dbEntities db = new wxtest_dbEntities();
        GetingeProductPrantsBll bll = new GetingeProductPrantsBll();
        public ActionResult Nav()
        {
            return View();
        }
        public ActionResult Nav1()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        #region   公司信息
        //公司-公司信息
        public ActionResult Company()
        {
            return View();
        }
        //公司-招兵买马
        public ActionResult CompanyZp()
        {
            return View();
        }
        //公司-公司信息
        public ActionResult CompanyMain()
        {
            return View();
        }
        //公司-简介及愿景
        public ActionResult CompanyInfo()
        {
            return View();
        }
        //公司-我们的故事
        public ActionResult CompanyGs()
        {
            return View();
        }
        //公司-公司历史
        public ActionResult CompanyLs()
        {
            return View();
        }
        //公司-法律申明
        public ActionResult CompanyLaw()
        {
            return View();
        }
        #endregion   

        #region   产品信息
        public ActionResult Product()
        {
            return View();
        }
        //产品-产品目录
        public ActionResult ProductMain()
        {
            return View();
        }
        public ActionResult ProductSw()
        {
            return View();
        }
        public ActionResult ProductIc()
        {
            return View();
        }
        public ActionResult ProductAct(int productId)
        {
            if (productId == 1)
            {
                ViewBag.Title = "ACT重症和心血管系统";
            }
            else if (productId == 2)
            {
                ViewBag.Title = "SW外科系统";
            }
            else
            {
                ViewBag.Title = "IC感染控制";
            }

            Getinge_Product model = (from a in db.Getinge_Product
                                     where a.product_id == productId
                                     select a).FirstOrDefault();
            ViewBag.Model = model;
            List<GetingeProductPrants> list = bll.GetGetingeProductChildrenList(productId);
            return View(list);
        }

        //产品-产品信息
        public ActionResult ProductInfo(int productId)
        {
            var model = (from a in db.Getinge_Product
                         where a.product_id == productId
                         select a).FirstOrDefault();
            if (model != null)
            {
                ViewBag.ProName = model.product_name;
            }
            else
            {
                ViewBag.ProName = "";
            }
            List<Getinge_Product_Info> list = (from a in db.Getinge_Product_Info
                                               where a.product_id == productId
                                               select a).ToList();
            return View(list);
        }
        //产品-产品信息
        public ActionResult ProductInfo2()
        {
            return View();
        }
        //产品-产品信息
        public ActionResult ProductInfo3()
        {
            return View();
        }
        public ActionResult ProductSeach()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SeachProductByName(string name)
        {
            var list = (from a in db.Getinge_Product
                        where a.product_mark=="product" && a.product_name.Contains(name)
                        select a).Take(10).ToList();     
            return Json(new { code = 1, msg = list }, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region   科室
        //科室-科室目录
        public ActionResult DeptMain()
        {
            return View();
        }
        //科室-科室信息
        public ActionResult Dept(int id)
        {
            string title = GetingeData.GetingeDeptDic()[id];
            ViewData["id"] = id;
            ViewBag.Title = title;

            Getinge_Dept_Product model = (from a in db.Getinge_Dept_Product
                                          where a.dept_id == id
                                     select a).FirstOrDefault();
            ViewBag.Model = model;
            List<GetingeProductPrants> list = bll.GetGetingeDeptChildrenList(id);
            return View(list);
        }
        #endregion

        #region   联系我们
        public ActionResult ContactUs()
        {
            return View();
        }
        #endregion
    }
}