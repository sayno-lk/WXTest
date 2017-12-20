using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


public static class StringExtensions
{
    public static bool IsMatch(this string str, string op)
    {
        if (str.Equals(String.Empty) || str == null) return false;
        Regex re = new Regex(op, RegexOptions.IgnoreCase);
        return re.IsMatch(str);
    }
    public static bool IsIP(this string input)
    {
        return input.IsMatch(@"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$"); //@"^(([01]?[\d]{1,2})|(2[0-4][\d])|(25[0-5]))(\.(([01]?[\d]{1,2})|(2[0-4][\d])|(25[0-5]))){3}$";
    }

    public static bool IsMAC(this string mac)
    {
        return mac.IsMatch(@"^(?in)([\da-f]{2}(-|$)){6}$");
    }

    public static int CnLength(this string str)
    {
        return Encoding.Default.GetBytes(str).Length;
    }

    public static string SubString(this string strInput, int len, string flg)
    {
        string myResult = string.Empty;
        if (len >= 0)
        {
            byte[] bsSrcString = Encoding.Default.GetBytes(strInput);
            if (bsSrcString.Length > len)
            {
                int nRealLength = len;
                int[] anResultFlag = new int[len];
                byte[] bsResult = null;

                int nFlag = 0;
                for (int i = 0; i < len; i++)
                {
                    if (bsSrcString[i] > 127)
                    {
                        nFlag++;
                        if (nFlag == 3) nFlag = 1;
                    }
                    else nFlag = 0;
                    anResultFlag[i] = nFlag;
                }
                if ((bsSrcString[len - 1] > 127) && (anResultFlag[len - 1] == 1))
                    nRealLength = len + 1;
                bsResult = new byte[nRealLength];
                Array.Copy(bsSrcString, bsResult, nRealLength);
                myResult = Encoding.Default.GetString(bsResult);
                myResult = myResult + (len >= strInput.CnLength() ? "" : flg);
            }
            else myResult = strInput;
        }
        return myResult;
    }

    /// <summary>
    /// 过滤SQL关键字
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string SafeSql(this string str)
    {
        str = str.IsNullOrEmpty() ? "" : str.Replace("'", "''");
        str = new Regex("exec", RegexOptions.IgnoreCase).Replace(str, "&#101;xec");
        str = new Regex("xp_cmdshell", RegexOptions.IgnoreCase).Replace(str, "&#120;p_cmdshell");
        str = new Regex("select", RegexOptions.IgnoreCase).Replace(str, "&#115;elect");
        str = new Regex("insert", RegexOptions.IgnoreCase).Replace(str, "&#105;nsert");
        str = new Regex("update", RegexOptions.IgnoreCase).Replace(str, "&#117;pdate");
        str = new Regex("delete", RegexOptions.IgnoreCase).Replace(str, "&#100;elete");
        str = new Regex("drop", RegexOptions.IgnoreCase).Replace(str, "&#100;rop");
        str = new Regex("create", RegexOptions.IgnoreCase).Replace(str, "&#99;reate");
        str = new Regex("rename", RegexOptions.IgnoreCase).Replace(str, "&#114;ename");
        str = new Regex("truncate", RegexOptions.IgnoreCase).Replace(str, "&#116;runcate");
        str = new Regex("alter", RegexOptions.IgnoreCase).Replace(str, "&#97;lter");
        str = new Regex("exists", RegexOptions.IgnoreCase).Replace(str, "&#101;xists");
        str = new Regex("master.", RegexOptions.IgnoreCase).Replace(str, "&#109;aster.");
        str = new Regex("restore", RegexOptions.IgnoreCase).Replace(str, "&#114;estore");
        return str;
    }
}

