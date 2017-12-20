using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Web.Script.Serialization;
using System.IO;
using System.Security.Cryptography;


public static class EncryptExtensions
{

    private static byte[] AESKeys = { 0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79, 0x53, 0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F };
    public static string AESEncode(this string encryptString, string encryptKey)
    {
        encryptKey = encryptKey.SubString(32, "");
        encryptKey = encryptKey.PadRight(32, ' ');

        RijndaelManaged rijndaelProvider = new RijndaelManaged();
        rijndaelProvider.Key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 32));
        rijndaelProvider.IV = AESKeys;
        ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();

        byte[] inputData = Encoding.UTF8.GetBytes(encryptString);
        byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);

        return Convert.ToBase64String(encryptedData);
    }
    public static string AESDecode(this string decryptString, string decryptKey)
    {
        try
        {
            decryptKey = decryptKey.SubString(32, "");
            decryptKey = decryptKey.PadRight(32, ' ');

            RijndaelManaged rijndaelProvider = new RijndaelManaged();
            rijndaelProvider.Key = Encoding.UTF8.GetBytes(decryptKey);
            rijndaelProvider.IV = AESKeys;
            ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();

            byte[] inputData = Convert.FromBase64String(decryptString);
            byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);

            return Encoding.UTF8.GetString(decryptedData);
        }
        catch { return string.Empty; }
    }

    private static byte[] DESKeys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
    public static string DESEncode(this string encryptString, string encryptKey)
    {
        encryptKey = encryptKey.SubString(8, "");
        encryptKey = encryptKey.PadRight(8, ' ');
        byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
        byte[] rgbIV = DESKeys;
        byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
        DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
        MemoryStream mStream = new MemoryStream();
        CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
        cStream.Write(inputByteArray, 0, inputByteArray.Length);
        cStream.FlushFinalBlock();
        return Convert.ToBase64String(mStream.ToArray());
    }
    public static string DESDecode(this string decryptString, string decryptKey)
    {
        try
        {
            decryptKey = decryptKey.SubString(8, "");
            decryptKey = decryptKey.PadRight(8, ' ');
            byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
            byte[] rgbIV = DESKeys;
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();

            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }
        catch { return string.Empty; }
    }

    public static string Base64Encode(this string encryptString)
    {
        byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(encryptString);
        return Convert.ToBase64String(encbuff);
    }
    public static string Base64Decode(this string decryptString)
    {
        byte[] decbuff = Convert.FromBase64String(decryptString);
        return System.Text.Encoding.UTF8.GetString(decbuff);
    }

    public static string RSADecrypt(this string s, string key)
    {
        string result = null;

        if (string.IsNullOrEmpty(s)) throw new ArgumentException("An empty string value cannot be encrypted.");

        if (string.IsNullOrEmpty(key)) throw new ArgumentException("Cannot decrypt using an empty key. Please supply a decryption key.");

        CspParameters cspp = new CspParameters();
        cspp.KeyContainerName = key;

        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
        rsa.PersistKeyInCsp = true;

        string[] decryptArray = s.Split(new string[] { "-" }, StringSplitOptions.None);
        byte[] decryptByteArray = Array.ConvertAll<string, byte>(decryptArray, (a => Convert.ToByte(byte.Parse(a, System.Globalization.NumberStyles.HexNumber))));

        byte[] bytes = rsa.Decrypt(decryptByteArray, true);

        result = System.Text.UTF8Encoding.UTF8.GetString(bytes);

        return result;
    }
    public static string RSAEncrypt(this string s, string key)
    {
        if (string.IsNullOrEmpty(s)) throw new ArgumentException("An empty string value cannot be encrypted.");

        if (string.IsNullOrEmpty(key)) throw new ArgumentException("Cannot encrypt using an empty key. Please supply an encryption key.");

        CspParameters cspp = new CspParameters();
        cspp.KeyContainerName = key;

        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
        rsa.PersistKeyInCsp = true;

        byte[] bytes = rsa.Encrypt(System.Text.UTF8Encoding.UTF8.GetBytes(s), true);

        return BitConverter.ToString(bytes);
    }

    public static string MD5(this string str)
    {
        string cl1 = str;
        string pwd = "";
        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();// 加密后是一个字节类型的数组 
        byte[] s = md5.ComputeHash(Encoding.Unicode.GetBytes(cl1));
        for (int i = 0; i < s.Length; i++)
        {// 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得 
            pwd = pwd + s[i].ToString("x");// 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
        }
        return pwd;
    }
    public static string MD5CSP(this string encypStr)
    {
        string retStr;
        MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

        //创建md5对象
        byte[] inputBye;
        byte[] outputBye;

        //使用GB2312编码方式把字符串转化为字节数组．
        inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);

        outputBye = m5.ComputeHash(inputBye);

        retStr = System.BitConverter.ToString(outputBye);
        retStr = retStr.Replace("-", "").ToLower();
        return retStr;
    }

    public static string SHA256(this string str)
    {
        byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
        SHA256Managed Sha256 = new SHA256Managed();
        byte[] Result = Sha256.ComputeHash(SHA256Data);
        return Convert.ToBase64String(Result);  //返回长度为44字节的字符串
    }

}

