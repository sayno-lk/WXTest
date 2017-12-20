
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