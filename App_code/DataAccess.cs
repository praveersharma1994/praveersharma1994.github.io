using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public class DataAccess
{
    private string ConnString = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;

    public static DataTable GetDataTable(string SQLQuery, CommandType cmdType, params SqlParameter[] param)
    {
        DataTable dt = new DataTable();

        DataAccess d = new DataAccess();
        using (var dbConnection = new SqlConnection(d.ConnString))
        {
            try
            {
                using (var command = new SqlCommand())
                {
                    command.CommandText = SQLQuery.Trim();
                    command.CommandType = cmdType;
                    command.Connection = dbConnection;
                    if (param != null)
                    {
                        command.Parameters.AddRange(param);
                    }
                    dbConnection.Open();
                    dt.Load(command.ExecuteReader(CommandBehavior.CloseConnection));

                }
            }
            catch (Exception ex)
            {
                //HelperMethod.LogError(ex);
            }
        }
        return dt;
    }

    public static DataSet GetDataSet(string SQLQuery, CommandType cmdType, params SqlParameter[] param)
    {
        DataSet ds = new DataSet();
        DataAccess d = new DataAccess();
        using (var dbConnection = new SqlConnection(d.ConnString))
        {
            try
            {
                using (var command = new SqlCommand())
                {
                    command.CommandText = SQLQuery.Trim();
                    command.CommandType = cmdType;
                    command.Connection = dbConnection;
                    if (param != null)
                    {
                        command.Parameters.AddRange(param);
                    }
                    dbConnection.Open();
                    using (var da = new SqlDataAdapter(command))
                    {
                        da.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                //HelperMethod.LogError(ex);
            }
        }
        return ds;
    }

    public static int ExecuteQuery(string SQLQuery, CommandType cmdType, params SqlParameter[] param)
    {
        int rowaffected = 0;

        DataAccess d = new DataAccess();
        using (var dbConnection = new SqlConnection(d.ConnString))
        {
            try
            {
                using (var command = new SqlCommand())
                {
                    command.CommandText = SQLQuery;
                    command.CommandType = cmdType;
                    command.Connection = dbConnection;
                    if (param != null)
                    {
                        command.Parameters.AddRange(param);
                    }
                    dbConnection.Open();
                    rowaffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //HelperMethod.LogError(ex);
            }
        }
        return rowaffected;
    }

    public static object ExecuteScalar(string SQLQuery, CommandType cmdType, params SqlParameter[] param)
    {
        object rowaffected = null;
        DataAccess d = new DataAccess();
        using (var dbConnection = new SqlConnection(d.ConnString))
        {
            try
            {
                using (var command = new SqlCommand())
                {
                    command.CommandText = SQLQuery;
                    command.CommandType = cmdType;
                    command.Connection = dbConnection;
                    if (param != null)
                    {
                        command.Parameters.AddRange(param);
                    }
                    dbConnection.Open();
                    rowaffected = command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                // HelperMethod.LogError(ex);
            }
        }
        return rowaffected;
    }
}
