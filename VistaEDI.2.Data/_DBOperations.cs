using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace VistaEDI._2.Data
{
    public abstract class DatabaseOperations
        {

            SqlConnection sqlConnection;
            SqlCommand sqlCommand;
            public string StoredProcedureName { get; set; }
            public Dictionary<string, object> SQLParameters { get; set; }
            public string ConnectionString { get; set; }

            /// <summary>
            ///The method to create and open the database connaction 
            /// </summary>
            /// <returns>SqlConnection</returns>
            private SqlConnection DBConnection()
            {
                try
                {
                    if (!string.IsNullOrEmpty(ConnectionString.Trim()))
                    {
                        //to get the database connection string
                        string connectionString = ConnectionString;
                        sqlConnection = new SqlConnection(connectionString);
                        sqlConnection.Open();
                        return sqlConnection;
                    }
                    else
                    {
                        throw new ArgumentNullException();
                    }
                }
                catch
                {
                    throw;
                }
            }
            /// <summary>
            /// The method is to close the database connection
            /// </summary>
            /// <returns>true/false</returns>
            private bool CloseDBConnection()
            {
                try
                {
                    sqlConnection.Dispose();
                    sqlConnection.Close();
                    return true;
                }
                catch
                {
                    throw;
                }
            }
            /// <summary>
            /// The method will call the sql server to perform the database operation
            /// </summary>
            /// <param name="sqlCommand">the param usually has stroted procedure and the input variable</param>
            /// <returns>It retrurns the data table which is return by the query</returns>
            private DataTable ExecuteProcedure(SqlCommand sqlCommand)
            {
                try
                {
                    sqlCommand.Connection = DBConnection();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dt);
                    da.Dispose();
                    sqlCommand.Dispose();
                    CloseDBConnection();
                    return dt;
                }
                catch
                {
                    throw;
                }

            }
            /// <summary>
            /// Common method to bind parameters with Stored procedure
            /// </summary>
            /// <returns> DataTable</returns>
            public DataTable Execute()
            {
                try
                {
                    if (!string.IsNullOrEmpty(StoredProcedureName.Trim()))
                    {
                        sqlCommand = new System.Data.SqlClient.SqlCommand(StoredProcedureName);
                        foreach (var parameter in SQLParameters)
                        {
                            sqlCommand.Parameters.AddWithValue("@" + parameter.Key, parameter.Value);
                        }
                        return ExecuteProcedure(sqlCommand);
                    }
                    else
                    {
                        throw new ArgumentNullException();
                    }
                }
                catch
                {
                    throw;
                }

            }

        }
}
