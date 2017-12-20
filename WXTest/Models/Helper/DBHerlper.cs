using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

public class DBHerlper
{
    //private static readonly string StrConn = @"server=rdszs7hma4oekk42tfx4public.sqlserver.rds.aliyuncs.com,3433;database=WebService_DB;User ID=binland;pwd=password_1";
    private static readonly string StrConn = ConfigurationManager.AppSettings["connStr"].ToString();
    //private static readonly string StrConn = @"server=.;database= WebService_DB;User ID=sa;pwd=password_1";
    public static string getConnectionString()
    {
        return StrConn;
    }


    /// <summary>
    /// 执行增删改操作
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static bool ExecSql(string sql)
    {
        int count = -1;
        try
        {
            using (SqlConnection connection = new SqlConnection(getConnectionString()))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                count = command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        if (count >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 查询结果集
    /// </summary>
    /// <param name="safeSql">执行语句</param>
    /// <returns></returns>
    public static SqlDataReader GetReader(string safeSql)
    {
        SqlConnection connection = new SqlConnection(getConnectionString());
        connection.Open();
        SqlCommand cmd = new SqlCommand(safeSql, connection);
        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        return reader;

    }


    /// <summary>
    /// 执行存储过程，返回结果参数值
    /// </summary>
    /// <param name="storedProcName">存储过程名</param>
    /// <param name="parameters">存储过程参数</param>
    /// <param name="rowsAffected">结果参数值</param>
    /// <returns></returns>
    public static int RunProcedure(string storedProcName, IDataParameter[] parameters)
    {
        using (SqlConnection connection = new SqlConnection(getConnectionString()))
        {
            //try
            //{
            int result = 0;
            connection.Open();
            SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
            command.ExecuteNonQuery();
            result = (int)command.Parameters["ReturnValue"].Value;
            //connection.Close();
            return result;
            //}
            //catch (Exception)
            //{
            //    return -1;
            //}

        }
    }


    /// <summary>
    /// 执行存储过程，返回影响的行数		
    /// </summary>
    /// <param name="storedProcName">存储过程名</param>
    /// <param name="parameters">存储过程参数</param>
    /// <param name="rowsAffected">影响的行数</param>
    /// <returns></returns>
    public static int RunProcedures(string storedProcName, IDataParameter[] parameters)
    {
        using (SqlConnection connection = new SqlConnection(getConnectionString()))
        {
            try
            {
                //int result = 0;
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                command.Transaction = transaction;
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    //result = (int)command.Parameters["ReturnValue"].Value;
                    //connection.Close();
                    transaction.Commit();
                    return rowsAffected;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return -1;

                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }

    /// <summary>
    /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
    /// </summary>
    /// <param name="connection">数据库连接</param>
    /// <param name="storedProcName">存储过程名</param>
    /// <param name="parameters">存储过程参数</param>
    /// <returns>SqlCommand</returns>
    private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
    {
        SqlCommand command = new SqlCommand(storedProcName, connection);

        command.CommandType = CommandType.StoredProcedure;
        foreach (SqlParameter parameter in parameters)
        {
            if (parameter != null)
            {
                // 检查未分配值的输出参数,将其分配以DBNull.Value.
                if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                    (parameter.Value == null))
                {
                    parameter.Value = DBNull.Value;
                }
                command.Parameters.Add(parameter);
            }
        }

        return command;
    }
    /// <summary>
    /// 创建 SqlCommand 对象实例(用来返回一个整数值)	
    /// </summary>
    /// <param name="storedProcName">存储过程名</param>
    /// <param name="parameters">存储过程参数</param>
    /// <returns>SqlCommand 对象实例</returns>
    private static SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
    {
        SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
        command.Parameters.Add(new SqlParameter("ReturnValue",
            SqlDbType.Int, 4, ParameterDirection.ReturnValue,
            false, 0, 0, string.Empty, DataRowVersion.Default, null));
        return command;
    }

    public static bool GetIsNull(object obj)
    {
        try
        {
            string str = obj.ToString();
            if (str == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            return true;
        }
    }

    /// <summary>
    /// 存储过程参数判断Convert Int32
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetConvertInt32(string value)
    {
        if (value == "" || value == null)
        {
            return null;
        }
        else
        {
            try
            {
                return Convert.ToInt32(value).ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    /// <summary>
    /// 存储过程参数判断Convert Int16
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetConvertInt16(string value)
    {
        if (value == "" || value == null)
        {
            return null;
        }
        else
        {
            try
            {
                return Convert.ToInt16(value).ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    /// <summary>
    /// 存储过程参数判断Convert Int64
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetConvertInt64(string value)
    {
        if (value == "" || value == null)
        {
            return null;
        }
        else
        {
            try
            {
                return Convert.ToInt64(value).ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    /// <summary>
    /// 存储过程参数判断是否为空
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetConvertString(string value)
    {
        return (value == "" || value == null) ? null : value;
    }

    /// <summary>
    /// 存储过程参数判断Convert DateTime
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetConvertDateTime(string value)
    {
        if (value == "" || value == null)
        {
            return null;
        }
        else
        {
            try
            {
                return Convert.ToDateTime(value).ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    /// <summary>
    /// 存储过程参数判断Convert DateTime
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetConvertDouble(string value)
    {
        if (value == "" || value == null)
        {
            return null;
        }
        else
        {
            try
            {
                return Convert.ToDouble(value).ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    /// <summary>
    /// 存储过程参数判断Convert ToBoolean
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetConvertToBoolean(string value)
    {
        if (value == "" || value == null)
        {
            return null;
        }
        else
        {
            try
            {
                return value.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }


}
