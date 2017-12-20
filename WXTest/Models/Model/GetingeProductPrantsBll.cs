using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WXTest.Models
{
    public class GetingeProductPrants
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int parent_id { get; set; }
        public int dept_id { get; set; }
        public string product_mark { get; set; }
        public int product_level { get; set; }
    }
    public class GetingeProductPrantsBll
    {
        public List<GetingeProductPrants> GetGetingeProductPrantsList(int productId)
        {
            List<GetingeProductPrants> list = new List<GetingeProductPrants>();
            string sql = @"WITH    ProductTree
          AS ( SELECT   [product_id] ,
                        [parent_id] ,
                        [dept_id] ,
                        [product_name] ,
                        [product_mark] ,
                        [product_level]
               FROM     [wxtest_db].[dbo].[Getinge_Product]
               WHERE    [product_id] = "+ productId + @"                        --需要查找的子节点
               UNION ALL
               SELECT   [wxtest_db].[dbo].[Getinge_Product].*
               FROM     ProductTree
                        JOIN [wxtest_db].[dbo].[Getinge_Product] ON ProductTree.parent_id = [wxtest_db].[dbo].[Getinge_Product].[product_id]
             )
    SELECT  [product_id] ,
            [parent_id] ,
            [dept_id] ,
            [product_name] ,
            [product_mark] ,
            [product_level]
    FROM    ProductTree
    ORDER BY [parent_id];";
            using (SqlDataReader reader = DBHerlper.GetReader(sql))
            {
                list = GetEcmoUserListByRow(reader);
            }
            return list;
        }
        public List<GetingeProductPrants> GetGetingeProductChildrenList(int productId)
        {
            List<GetingeProductPrants> list = new List<GetingeProductPrants>();
            string sql = @"WITH    ProductTree
          AS ( SELECT   [product_id] ,
                        [parent_id] ,
                        [dept_id] ,
                        [product_name] ,
                        [product_mark] ,
                        [product_level]
               FROM     [wxtest_db].[dbo].[Getinge_Product]
               WHERE    [product_id] = " + productId + @"  
               UNION ALL
               SELECT   [wxtest_db].[dbo].[Getinge_Product].*
               FROM     ProductTree
                        JOIN [wxtest_db].[dbo].[Getinge_Product] ON ProductTree.[product_id]= [wxtest_db].[dbo].[Getinge_Product].parent_id
             )
    SELECT  [product_id] ,
            [parent_id] ,
            [dept_id] ,
            [product_name] ,
            [product_mark] ,
            [product_level]
    FROM    ProductTree
    ORDER BY [parent_id];";
            using (SqlDataReader reader = DBHerlper.GetReader(sql))
            {
                list = GetEcmoUserListByRow(reader);
            }
            return list;
        }

        public List<GetingeProductPrants> GetGetingeDeptChildrenList(int deptId)
        {
            List<GetingeProductPrants> list = new List<GetingeProductPrants>();
            string sql = @"WITH    ProductTree
          AS ( SELECT   [dept_id]
      ,[parent_id]
      ,[dept_name]
      ,[product_id]
               FROM     [wxtest_db].[dbo].[Getinge_Dept_Product]
               WHERE    [dept_id] = "+deptId+ @"  --需要查找的父节点
               UNION ALL
               SELECT   [wxtest_db].[dbo].[Getinge_Dept_Product].*
               FROM     ProductTree
                        JOIN [wxtest_db].[dbo].[Getinge_Dept_Product] ON ProductTree.[dept_id]= [wxtest_db].[dbo].[Getinge_Dept_Product].parent_id
             )
    SELECT  t.*,b.[product_name],b.[product_mark],b.[product_level]
    FROM    ProductTree t
	LEFT JOIN 
                    [wxtest_db].[dbo].[Getinge_Product] b
					ON t.[product_id]= b.[product_id]
    ORDER BY [parent_id];";
            using (SqlDataReader reader = DBHerlper.GetReader(sql))
            {
                list = GetEcmoUserListByRow(reader);
            }
            return list;
        }

    
        private List<GetingeProductPrants> GetEcmoUserListByRow(SqlDataReader reader)
        {
            List<GetingeProductPrants> list = new List<GetingeProductPrants>();
            GetingeProductPrants productPrants = null;
            while (reader.Read())
            {
                productPrants = new GetingeProductPrants();
                if (reader["product_id"] != DBNull.Value) { productPrants.product_id = Convert.ToInt32(reader["product_id"]); }
                if (reader["parent_id"] != DBNull.Value) { productPrants.parent_id = Convert.ToInt32(reader["parent_id"]); }
                if (reader["dept_id"] != DBNull.Value) { productPrants.dept_id = Convert.ToInt32(reader["dept_id"]); }
                if (reader["product_name"] != DBNull.Value) { productPrants.product_name = Convert.ToString(reader["product_name"]); }
                if (reader["product_mark"] != DBNull.Value) { productPrants.product_mark = Convert.ToString(reader["product_mark"]); }
                if (reader["product_level"] != DBNull.Value) { productPrants.product_level = Convert.ToInt32(reader["product_level"]); }
                list.Add(productPrants);
            }
            return list;
        }
    }


}