using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoModule.Models
{
    public static class WxConfig
    {
        public static string WxReturnUrl_Base = "http://go.tg-info.com/OAuth2/index?oauthScope=0";
        public static string WxReturnUrl_Userinfo = "http://go.tg-info.com/OAuth2/index?oauthScope=1";
    }
}