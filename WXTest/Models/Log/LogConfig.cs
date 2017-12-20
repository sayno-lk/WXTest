using System;
using System.Collections.Generic;
using System.Web;


/**
* 	配置账号信息
*/
public class LogConfig
{
    public const string APPID = "wx88ac3e6ab868f62f";
    public const string MCHID = "1421431102";
    public const string KEY = "365BCF9BD01FD934D77E0CF131CD6E8D";
    public const string APPSECRET = "bfbf498ac439cf67b39d13f600fe378d";

    //=======【证书路径设置】===================================== 
    /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
    */
    public const string SSLCERT_PATH = "cert/apiclient_cert.p12";
    public const string SSLCERT_PASSWORD = "1233410002";



    //=======【支付结果通知url】===================================== 
    /* 支付结果通知回调url，用于商户接收支付结果
    */
    //public const string NOTIFY_URL = "http://paysdk.weixin.qq.com/example/ResultNotifyPage.aspx";
    public const string NOTIFY_URL = "http://wechatweb.77hudong.com/pay/result.aspx";

    //=======【商户系统后台机器IP】===================================== 
    /* 此参数可手动配置也可在程序中自动获取
    */
    public const string IP = "8.8.8.8";


    //=======【代理服务器设置】===================================
    /* 默认IP和端口号分别为0.0.0.0和0，此时不开启代理（如有需要才设置）
    */
    public const string PROXY_URL = "http://10.152.18.220:8080";

    //=======【上报信息配置】===================================
    /* 测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
    */
    public const int REPORT_LEVENL = 1;

    //=======【日志级别】===================================
    /* 日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
    */
    public const int LOG_LEVENL = 3;
}
