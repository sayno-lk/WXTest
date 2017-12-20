using AutoModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXTest.DB;

namespace WXTest.Controllers
{
    public class EcmoController : Controller
    {
        wxtest_dbEntities db = new wxtest_dbEntities();
        // GET: Ecmo
        public ActionResult Index(string openid)
        {
            if (string.IsNullOrEmpty(openid))
            {
                string returnUrl = HttpUtility.UrlDecode(Request.Url.ToString());
                string url = WxConfig.WxReturnUrl_Base + "&returnUrl=" + returnUrl;
                return Redirect(url);
            }
            else
            {
                Ecmo_User model = (from a in db.Ecmo_User
                                   where a.openid == openid
                                   select a).FirstOrDefault();
                if (model != null)
                {
                    return Redirect("demo");
                }
                return View();
            }
        }

        public ActionResult demo(string openid)
        {
            if (string.IsNullOrEmpty(openid))
            {
                string returnUrl = HttpUtility.UrlDecode(Request.Url.ToString());
                string url = WxConfig.WxReturnUrl_Base + "&returnUrl=" + returnUrl;
                return Redirect(url);
            }
            else
            {
                Ecmo_User model = (from a in db.Ecmo_User
                                   where a.openid == openid
                                   select a).FirstOrDefault();
                if (model == null)
                {
                    return Redirect("index");
                }
                return View();
            }
        }
        public ActionResult hy(string openid)
        {
            if (string.IsNullOrEmpty(openid))
            {
                string returnUrl = HttpUtility.UrlDecode(Request.Url.ToString());
                string url = WxConfig.WxReturnUrl_Base + "&returnUrl=" + returnUrl;
                return Redirect(url);
            }
            else
            {
                Ecmo_User model = (from a in db.Ecmo_User
                                   where a.openid == openid
                                   select a).FirstOrDefault();
                if (model == null)
                {
                    return Redirect("index");
                }
                return View();
            }
        }
        public ActionResult rl(string openid)
        {
            if (string.IsNullOrEmpty(openid))
            {
                string returnUrl = HttpUtility.UrlDecode(Request.Url.ToString());
                string url = WxConfig.WxReturnUrl_Base + "&returnUrl=" + returnUrl;
                return Redirect(url);
            }
            else
            {
                Ecmo_User model = (from a in db.Ecmo_User
                                   where a.openid == openid
                                   select a).FirstOrDefault();
                if (model == null)
                {
                    return Redirect("index");
                }
                return View();
            }
        }
        public ActionResult yl(string openid)
        {
            if (string.IsNullOrEmpty(openid))
            {
                string returnUrl = HttpUtility.UrlDecode(Request.Url.ToString());
                string url = WxConfig.WxReturnUrl_Base + "&returnUrl=" + returnUrl;
                return Redirect(url);
            }
            else
            {
                Ecmo_User model = (from a in db.Ecmo_User
                                   where a.openid == openid
                                   select a).FirstOrDefault();
                if (model == null)
                {
                    return Redirect("index");
                }
                return View();
            }
        }
        public ActionResult zl(string openid)
        {
            if (string.IsNullOrEmpty(openid))
            {
                string returnUrl = HttpUtility.UrlDecode(Request.Url.ToString());
                string url = WxConfig.WxReturnUrl_Base + "&returnUrl=" + returnUrl;
                return Redirect(url);
            }
            else
            {
                Ecmo_User model = (from a in db.Ecmo_User
                                   where a.openid == openid
                                   select a).FirstOrDefault();
                if (model == null)
                {
                    return Redirect("index");
                }
                return View();
            }
        }

        #region 页面接口

        public ActionResult AddUser(Ecmo_User user)
        {
            Ecmo_User model = (from a in db.Ecmo_User
                               where a.openid == user.openid
                               select a).FirstOrDefault();
            if (model != null)
            {
                return Json(new { code = 0, msg = "已经登陆" }, JsonRequestBehavior.DenyGet);
            }
            //model = (from a in db.Ecmo_User
            //         where a.user_phone == user_phone && a.openid == null
            //         select a).FirstOrDefault();
            //if (model == null)
            //{
            //    return Json(new { code = 0, msg = "未找到" }, JsonRequestBehavior.DenyGet);
            //}
            //else
            //{
            model = new Ecmo_User();
            model.openid = user.openid;
            model.user_name = user.user_name;
            model.user_hospital = user.user_hospital;
            model.user_dept = user.user_dept;
            model.user_phone = user.user_phone;
            model.user_date = DateTime.Now;
            db.Ecmo_User.Add(model);
            db.SaveChanges();
            return Json(new { code = 1, msg = "登陆成功" }, JsonRequestBehavior.DenyGet);
            //}
        }

        public ActionResult AddEnroll(Ecmo_Enroll enroll)
        {
            Ecmo_Enroll model = (from a in db.Ecmo_Enroll
                                 where a.openid == enroll.openid &&a.enroll_name==enroll.enroll_name
                                 select a).FirstOrDefault();
            if (model != null)
            {
                return Json(new { code = 0, msg = "请勿重复报名" }, JsonRequestBehavior.DenyGet);
            }

            model = new Ecmo_Enroll();
            model.openid = enroll.openid;
            model.enroll_name = enroll.enroll_name;   
            model.enroll_date = DateTime.Now;
            db.Ecmo_Enroll.Add(model);
            db.SaveChanges();
            return Json(new { code = 1, msg = "报名成功" }, JsonRequestBehavior.DenyGet);
           
        }


        #endregion
    }
}