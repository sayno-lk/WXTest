//序列化
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
}

//验证车牌号
function isVehicleNumber(vehicleNumber) {
    var result = false;
    if (vehicleNumber.length == 7) {
        var express = /^[京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[A-Z]{1}[A-Z0-9]{4}[A-Z0-9挂学警港澳]{1}$/;
        result = express.test(vehicleNumber);
    }
    return result;
}
//验证数字
function checkNumber(value) {    //定义正则表达式部分  
    //var reg = new RegExp("^[0-9]*$");
    var reg = /^[0-9]*$/;
    if (value == undefined || value == null || value == "") {
        return false;
    }
    if (reg.test(value)) {
        return true;
    }
    return false;
}
//验证数字
function checkNumber2(value) {    //定义正则表达式部分   
    var reg = new RegExp("^[0-9]*$");
    if (!reg.test(value)) {
        return false;
    }
    return true;
}
//验证电话号码
function checkTelephone(inpurStr) {
    var partten = /^0(([1,2]\d)|([3-9]\d{2}))\d{7,8}$/;
    if (partten.test(inpurStr)) {
        //alert('是电话号码');
        return true;
    }
    else {
        //alert('不是电话号码');
        return false;
    }
}
//验证手机号码
function checkPhone(phone) {
    if (!(/^1[345678]\d{9}$/.test(phone))) {
        //alert("手机号码有误，请重填");
        return false;
    }
    return true;
}
//验证电话号码
function checkPhone2(phone) {
    var myreg = /^(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]{1}))+\d{8})$/;
    if (!myreg.test(phone)) {
        //alert('请输入有效的手机号码！'); 
        return false;
    }
    return true;
}
//验证邮箱
function checkEmail(yx) {
    var reyx = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return (reyx.test(yx));
}
function checkEmail2(yx) {
    var reyx = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+/;
    return (reyx.test(yx));
}

//刷新验证码
function changeImg() {
    var imgNode = document.getElementById("codeImg");
    imgNode.src = "ValidateCode.aspx?t=" + (new Date()).valueOf();
}

//验证 是否含有非法字符
function checkIllegalChar(obj) {
    var value = obj.val();
    if (!checkChar(value)) {
        obj.val(value.substring(0, value.length - 1));
        obj.focus();

    }
}
//检查输入中的非法字符
function checkChar(InString) {
    var RefString = "<";
    var RefString2 = "%";
    var RefString3 = "\"";
    var RefString4 = ">";
    var RefString5 = "~";
    var RefString6 = "&";
    var RefString7 = "+";
    var RefString8 = "'";
    for (Count = 0; Count < InString.length; Count++) {
        TempChar = InString.substring(Count, Count + 1);
        if ((RefString.indexOf(TempChar, 0) == 0) || (RefString2.indexOf(TempChar, 0) == 0) || (RefString3.indexOf(TempChar, 0) == 0) || (RefString4.indexOf(TempChar, 0) == 0) || (RefString5.indexOf(TempChar, 0) == 0) || (RefString6.indexOf(TempChar, 0) == 0) || (RefString7.indexOf(TempChar, 0) == 0) || (RefString8.indexOf(TempChar, 0) == 0)) {
            alert("输入字符有误，请重新输入！");
            return (false);
        }
    }
    return (true);
}
