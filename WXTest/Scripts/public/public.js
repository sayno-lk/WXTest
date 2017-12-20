//请求数据URL后面加一个随机字符串
function getRandomString() {
    var randomString = "?r=";
    for (var i = 0; i < 10; i++) {
        randomString += Math.floor(Math.random() * 10);
    }
    return randomString;
}
//获取路径参数
function request(paras, url) {
    //alert(url);
    if (url == null || url == "") {
        url = location.href;
    }
    else {
        url = url;
    }
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {}
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}

function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null)
        //return  unescape(r[2]);
        return decodeURI(r[2]);
    else
        return null;
}
//阻止事件冒泡点击（div响应事件,但是里面的a不会再响应div的事件，a单独去执行自己的事件）
function stopBubble(e) {
    //if (e != null&&e!=undefined) {
    //    //如果提供了事件对象，则这是一个非IE浏览器
    //    if (e && e.stopPropagation) {
    //        //因此它支持W3C的stopPropagation()方法
    //        e.stopPropagation();
    //    } else {
    //        //否则，我们需要使用IE的方式来取消事件冒泡
    //        window.event.cancelBubble = true;
    //    }
    //}
    var e = getEvent();
    if (window.event) {
        //e.returnValue=false;//阻止自身行为
        e.cancelBubble = true;//阻止冒泡
    } else if (e.preventDefault) {
        //e.preventDefault();//阻止自身行为
        e.stopPropagation();//阻止冒泡
    }
}

function getEvent() {
    if (window.event) { return window.event; }
    func = getEvent.caller;
    while (func != null) {
        var arg0 = func.arguments[0];
        if (arg0) {
            if ((arg0.constructor == Event || arg0.constructor == MouseEvent
               || arg0.constructor == KeyboardEvent)
               || (typeof (arg0) == "object" && arg0.preventDefault
               && arg0.stopPropagation)) {
                return arg0;
            }
        }
        func = func.caller;
    }
    return null;
}

/*字符串转换日期*/
function ConvertJSONDateToJSDateObject(jsondate) {
    var date = new Date(parseInt(jsondate.replace("/Date(", "").replace(")/", ""), 10));
    return date;
}
/*日期格式化扩展方法*/
Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1,//month
        "d+": this.getDate(),    //day
        "h+": this.getHours(),   //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
        "S": this.getMilliseconds() //millisecond
    }
    if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
    (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o) if (new RegExp("(" + k + ")").test(format))
        format = format.replace(RegExp.$1,
        RegExp.$1.length == 1 ? o[k] :
        ("00" + o[k]).substr(("" + o[k]).length));
    return format;
}

//日期格式化
function getDateToFormatDate(date) {
    if (date == null || date == "") {
        //console.log(date);
        return "";
    }
    else {
        var dateFormat = ConvertJSONDateToJSDateObject(date).format("yyyy-MM-dd");
        //console.log(dateFormat);
        return dateFormat;
    }
}
//日期格式化2
function getDateToFormat(date) {
    if (date == undefined || date == null || date == "") {
        //console.log("");
        return "";
    }
    else {
        var dateFormat = ConvertJSONDateToJSDateObject(date).format("yyyy-MM-dd hh:mm:ss");
       // console.log(dateFormat);
        return dateFormat;
    }
}