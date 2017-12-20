using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WXTest.Models
{

    public class EcmoUser
    {
        public int user_id { get; set; }
        public string openid { get; set; }
        public string user_name { get; set; }
        public string user_hospital { get; set; }
        public string user_dept { get; set; }
        public string user_phone { get; set; }
        public string enroll_name { get; set; }
        public DateTime user_date { get; set; }

    }

    public class EcmoBll
    {

        /// <summary>
        /// 查询指定序号后N条
        /// </summary>
        /// <param name="count">条数</param>
        /// <param name="index">序号</param>
        /// <returns></returns>
        public List<EcmoUser> GetEcmoUserList()
        {
            List<EcmoUser> list = new List<EcmoUser>();
            string sql = @"  SELECT    d.* ,
            enroll_name
  FROM      [wxtest_db].[dbo].Ecmo_User d
            LEFT JOIN ( SELECT  [openid] ,
                                STUFF(( SELECT  ',' + enroll_name
                                        FROM    [wxtest_db].[dbo].[Ecmo_Enroll] a
                                        WHERE   b.[openid] = a.[openid]
                                      FOR
                                        XML PATH('')
                                      ), 1, 1, '') enroll_name
                        FROM    [wxtest_db].[dbo].[Ecmo_Enroll] b
                        GROUP BY [openid]
                      ) c ON d.[openid] = c.[openid]";
            using (SqlDataReader reader = DBHerlper.GetReader(sql))
            {
                list = GetEcmoUserListByRow(reader);
            }
            return list;
        }


        private EcmoUser GetEcmoUserByRow(SqlDataReader reader)
        {
            EcmoUser ecmoUser = null;
            while (reader.Read())
            {
                ecmoUser = new EcmoUser();
                if (reader["user_id"] != DBNull.Value) { ecmoUser.user_id = Convert.ToInt32(reader["user_id"]); }
                if (reader["openid"] != DBNull.Value) { ecmoUser.openid = Convert.ToString(reader["openid"]); }
                if (reader["user_name"] != DBNull.Value) { ecmoUser.user_name = Convert.ToString(reader["user_name"]); }
                if (reader["user_hospital"] != DBNull.Value) { ecmoUser.user_hospital = Convert.ToString(reader["user_hospital"]); }
                if (reader["user_dept"] != DBNull.Value) { ecmoUser.user_dept = Convert.ToString(reader["user_dept"]); }
                if (reader["user_phone"] != DBNull.Value) { ecmoUser.user_phone = Convert.ToString(reader["user_phone"]); }
                if (reader["user_phone"] != DBNull.Value) { ecmoUser.user_phone = Convert.ToString(reader["user_phone"]); }
                if (reader["enroll_name"] != DBNull.Value) { ecmoUser.enroll_name = Convert.ToString(reader["enroll_name"]); }
                if (reader["user_date"] != DBNull.Value) { ecmoUser.user_date = Convert.ToDateTime(reader["user_date"]); }
            }
            return ecmoUser;
        }

        private List<EcmoUser> GetEcmoUserListByRow(SqlDataReader reader)
        {
            List<EcmoUser> list = new List<EcmoUser>();
            EcmoUser ecmoUser = null;
            while (reader.Read())
            {
                ecmoUser = new EcmoUser();
                if (reader["user_id"] != DBNull.Value) { ecmoUser.user_id = Convert.ToInt32(reader["user_id"]); }
                if (reader["openid"] != DBNull.Value) { ecmoUser.openid = Convert.ToString(reader["openid"]); }
                if (reader["user_name"] != DBNull.Value) { ecmoUser.user_name = Convert.ToString(reader["user_name"]); }
                if (reader["user_hospital"] != DBNull.Value) { ecmoUser.user_hospital = Convert.ToString(reader["user_hospital"]); }
                if (reader["user_dept"] != DBNull.Value) { ecmoUser.user_dept = Convert.ToString(reader["user_dept"]); }
                if (reader["user_phone"] != DBNull.Value) { ecmoUser.user_phone = Convert.ToString(reader["user_phone"]); }
                if (reader["user_phone"] != DBNull.Value) { ecmoUser.user_phone = Convert.ToString(reader["user_phone"]); }
                if (reader["enroll_name"] != DBNull.Value) { ecmoUser.enroll_name = Convert.ToString(reader["enroll_name"]); }
                if (reader["user_date"] != DBNull.Value) { ecmoUser.user_date = Convert.ToDateTime(reader["user_date"]); }

                list.Add(ecmoUser);
            }
            return list;
        }
    }

}