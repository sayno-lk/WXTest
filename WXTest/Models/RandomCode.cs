using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class RandomCode
{
    /// <summary>
    /// 生成随机数(数字+字母)
    /// </summary>
    /// <param name="codeCount">位数</param>
    /// <returns></returns>
    public static string CreateRandomCode(int codeCount)
    {
        //string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
        string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z";
        string[] allCharArray = allChar.Split(',');

        string randomCode = "";
        int temp = -1;
        Random rand = new Random();
        for (int i = 0; i < codeCount; i++)
        {
            if (temp != -1)
            {
                try
                {
                    checked
                    {
                        rand = new Random(i * temp + Guid.NewGuid().GetHashCode());
                    }
                }
                catch (OverflowException ex)
                {
                    rand = new Random(Guid.NewGuid().GetHashCode());
                    //MessageBox.Show(ex.Message);                       
                }
            }
            int t = rand.Next(allCharArray.Length - 1);
            if (temp == t)
            {
                return CreateRandomCode(codeCount);
            }
            temp = t;
            randomCode += allCharArray[t];
        }
        return randomCode;
    }

    /// <summary>
    ///  生成随机数(数字)
    /// </summary>
    /// <param name="codeCount"></param>
    /// <returns></returns>
    public static string CreateRandomCodeNumber(int codeCount)
    {

        string allChar = "0,1,2,3,4,5,6,7,8,9";
        string[] allCharArray = allChar.Split(',');  
        string randomCode = "";
        int temp = -1;
        Random rand = new Random();
        for (int i = 0; i < codeCount; i++)
        {
            if (temp != -1)
            {
                try
                {
                    checked
                    {
                        rand = new Random(i * temp + Guid.NewGuid().GetHashCode());
                    }
                }
                catch (OverflowException ex)
                {
                    rand = new Random(Guid.NewGuid().GetHashCode());
                    //MessageBox.Show(ex.Message);                       
                }
            }
            int t = rand.Next(allCharArray.Length - 1);
            if (temp == t)
            {
                return CreateRandomCodeNumber(codeCount);
            }
            temp = t;
            randomCode += allCharArray[t];
        }
        return randomCode;
    }
}

