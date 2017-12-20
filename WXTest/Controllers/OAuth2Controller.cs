using Senparc.Weixin;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WXTest.Controllers
{
    public class OAuth2Controller : Controller
    {
        // GET: OAuth2
        //下面换成账号对应的信息，也可以放入web.config等地方方便配置和更换
        private string appId = ConfigurationManager.AppSettings["WeixinAppId"];
        private string secret = ConfigurationManager.AppSettings["WeixinAppSecret"];
        /// <summary>
        /// 授权页面API
        /// </summary>
        /// <param name="returnUrl">用户尝试进入的需要登录的页面</param>
        /// <returns></returns>
        public ActionResult Index(string returnUrl, int? oauthScope)
        {
            if (returnUrl == null)
            {
                returnUrl = "http://" + Request.Url.Authority + "/OAuth2/UserInfoCallback1";
            }
            var state = "JeffreySu-" + DateTime.Now.Millisecond;//随机数，用于识别请求可靠性
            Session["State"] = state;//储存随机数到Session

            ViewData["returnUrl"] = returnUrl;
            WeixinTrace.SendApiLog(oauthScope.ToString(), "请求微信oauthScope");
            //此页面引导用户点击授权
            string url = "";
            if (oauthScope == (int)OAuthScope.snsapi_userinfo)
            {
                url =
                OAuthApi.GetAuthorizeUrl(appId,
                "http://" + Request.Url.Authority + "/oauth2/UserInfoCallback?returnUrl=" + returnUrl.UrlEncode(),
                state, OAuthScope.snsapi_userinfo);
            }
            else
            {
                url =
                   OAuthApi.GetAuthorizeUrl(appId,
                   "http://" + Request.Url.Authority + "/oauth2/BaseCallback?returnUrl=" + returnUrl.UrlEncode(),
                   state, OAuthScope.snsapi_base);
            }
            return Redirect(url);
            //ViewData["UrlUserInfo"] = url;
            //return View();
        }

        /// <summary>
        /// OAuthScope.snsapi_userinfo方式回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="returnUrl">用户最初尝试进入的页面</param>
        /// <returns></returns>
        public ActionResult UserInfoCallback(string code, string state, string returnUrl)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Content("您拒绝了授权！");
            }

            if (state != Session["State"] as string)
            {
                //这里的state其实是会暴露给客户端的，验证能力很弱，这里只是演示一下，
                //建议用完之后就清空，将其一次性使用
                //实际上可以存任何想传递的数据，比如用户ID，并且需要结合例如下面的Session["OAuthAccessToken"]进行验证
                return Content("验证失败！请从正规途径进入！");
            }
            OAuthAccessTokenResult result = null;

            //通过，用code换取access_token
            try
            {
                result = OAuthApi.GetAccessToken(appId, secret, code);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            if (result.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + result.errmsg);
            }
            //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
            //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
            Session["OAuthAccessTokenStartTime"] = DateTime.Now;
            Session["OAuthAccessToken"] = result;

            //因为第一步选择的是OAuthScope.snsapi_userinfo，这里可以进一步获取用户详细信息
            try
            {
                OAuthUserInfo userInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);
                //WeixinTrace.SendLog("获取code成功", "actionUrl");
                string data = "openid=" + userInfo.openid + "&nickname=" + userInfo.nickname + "&headimgurl=" + userInfo.headimgurl;
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    if (returnUrl.IndexOf("?") == -1)
                    {
                        return Redirect(returnUrl + "?" + data);
                    }
                    else
                    {
                        return Redirect(returnUrl + "&" + data);
                    }
                }
                return View(userInfo);
            }
            catch (ErrorJsonResultException ex)
            {
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// OAuthScope.snsapi_base方式回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="returnUrl">用户最初尝试进入的页面</param>
        /// <returns></returns>
        public ActionResult BaseCallback(string code, string state, string returnUrl)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Content("您拒绝了授权！");
            }

            if (state != Session["State"] as string)
            {
                //这里的state其实是会暴露给客户端的，验证能力很弱，这里只是演示一下，
                //建议用完之后就清空，将其一次性使用
                //实际上可以存任何想传递的数据，比如用户ID，并且需要结合例如下面的Session["OAuthAccessToken"]进行验证
                return Content("验证失败！请从正规途径进入！");
            }

            //通过，用code换取access_token
            var result = OAuthApi.GetAccessToken(appId, secret, code);
            if (result.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + result.errmsg);
            }

            //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
            //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
            Session["OAuthAccessTokenStartTime"] = DateTime.Now;
            Session["OAuthAccessToken"] = result;

            //因为这里还不确定用户是否关注本微信，所以只能试探性地获取一下
            string data = "openid=" + result.openid;
            if (!string.IsNullOrEmpty(returnUrl))
            {
                if (returnUrl.IndexOf("?") == -1)
                {
                    return Redirect(returnUrl + "?" + data);
                }
                else
                {
                    return Redirect(returnUrl + "&" + data);
                }
            }
            ViewData["ByBase"] = true;
            return View();
        }
    }
}