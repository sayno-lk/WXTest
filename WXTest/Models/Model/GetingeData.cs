using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WXTest.Models
{
    public enum GetingeDeptEnum
    {
        枚举1 = 1,
        枚举2 = 2,
        枚举3 = 3,
        枚举4 = 4,
        枚举5 = 5,
        枚举6 = 6,
        枚举7 = 7,
        枚举8 = 8,
        枚举9 = 9,
    }
    public enum GetingeProductInfoTypeEnum
    {
        title = 1,
        word = 2,
        images = 3,
        video = 4,
        smallTitle = 5,
        枚举6 = 6,
        枚举7 = 7,
        枚举8 = 8,
        枚举9 = 9,
    }

    public class GetingeData
    {
        public static Dictionary<int, string> GetingeDeptDic()
        {
            Dictionary<int, string> deptDic = new Dictionary<int, string>();
            deptDic.Add(1, "心血管及大血管外科");
            deptDic.Add(2, "心内科");
            deptDic.Add(3, "骨科");
            deptDic.Add(4, "基建及设备科");
            deptDic.Add(5, "ICU及呼吸科");
            deptDic.Add(6, "手术室/麻醉科");
            deptDic.Add(7, "神经外科");
            deptDic.Add(8, "生命科学领域");
            deptDic.Add(9, "消毒供应中心");      
          
            return deptDic;
        }

    }
}