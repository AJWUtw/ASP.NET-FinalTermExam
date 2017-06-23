using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ASP.NET_FinalTermExam.Dao
{
    public class CusDao
    {
        public string DBConn { get; set; }

        public CusDao()
        {
            this.DBConn = Common.DBConn.GetDBConnection("DBConn");
        }
        public DataTable GetCusGrid()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT * FROM Sales.Customers a JOIN CodeTable b ON a.ContactTitle = b.CodeID WHERE b.CodeType='Title'";

            using (SqlConnection conn = new SqlConnection(this.DBConn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return dt;
        }

        public DataTable GetContactTitle()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT * FROM CodeTable WHERE CodeType='Title'";

            using (SqlConnection conn = new SqlConnection(this.DBConn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return dt;
        }

        public DataTable GetCusGridByCondition(Models.CustomerView1 cus)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT * FROM Sales.Customers a JOIN CodeTable b ON a.ContactTitle = b.CodeID WHERE  (CustomerID = @CustomerID OR @CustomerID = 0) AND 
                                                            (CompanyName LIKE  @CompanyName OR @CompanyName IS NULL) AND 
                                                            (ContactName LIKE  @ContactName OR @ContactName IS NULL) AND 
                                                            (ContactTitle =  @ContactTitle OR @ContactTitle  IS NULL ) AND
                                                             b.CodeType='Title' ";
            try
            {
                using (SqlConnection conn = new SqlConnection(this.DBConn))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.Add(new SqlParameter("@CustomerID", cus.CustomerID == 0 ? 0 : cus.CustomerID ));
                    cmd.Parameters.Add(new SqlParameter("@CompanyName", cus.CompanyName == null ? null : cus.CompanyName));
                    cmd.Parameters.Add(new SqlParameter("@ContactName", cus.ContactName == null ? null : cus.ContactName));
                    cmd.Parameters.Add(new SqlParameter("@ContactTitle", cus.ContactTitle == null ? string.Empty : cus.ContactTitle));
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                    sqlAdapter.Fill(dt);
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                var a = e;
            }

            return dt;
        }
        /// <summary>
        /// 刪除客戶資料
        /// </summary>
        /// <param name=""></param>
        public Boolean DeleteCus(int id)
        {
            string sqlD = "DELETE FROM Sales.Customers WHERE CustomerID=@CustomerID";
            using (SqlConnection conn = new SqlConnection(this.DBConn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlD, conn);
                cmd.Parameters.Add(new SqlParameter("@CustomerID", id));
                try
                {
                    var aa = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    return false;
                    var aaa = e;
                }

                conn.Close();
            }
            return true;


        }

        /// <summary>
        /// 新增訂單資料 SqlTransaction.Rollback
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Boolean InsertCus(Models.Customers cusData)
        {

            string sql = @" Insert INTO Sales.Customers
                 ( CompanyName,ContactName,ContactTitle,CreationDate,Address,City,Country,Region,PostalCode,Phone,Fax )
                VALUES
                ( @CompanyName,@ContactName,@ContactTitle,@CreationDate,@Address,@City,@Country,@Region,@PostalCode,@Phone,@Fax )";
            int id = 0;
            using (SqlConnection conn = new SqlConnection(this.DBConn))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                SqlTransaction transaction;
                transaction = conn.BeginTransaction("SampleTransaction");
                command.Connection = conn;
                command.Transaction = transaction;
                try
                {
                    command.CommandText = sql;
                    command.Parameters.Add(new SqlParameter("@CompanyName", cusData.CompanyName));
                    command.Parameters.Add(new SqlParameter("@ContactName", cusData.ContactName));
                    command.Parameters.Add(new SqlParameter("@ContactTitle", cusData.ContactTitle));
                    command.Parameters.Add(new SqlParameter("@CreationDate", cusData.CreationDate));
                    command.Parameters.Add(new SqlParameter("@Address", cusData.Address));
                    command.Parameters.Add(new SqlParameter("@City", cusData.City));
                    command.Parameters.Add(new SqlParameter("@Country", cusData.Country));
                    command.Parameters.Add(new SqlParameter("@Region", cusData.Region));
                    command.Parameters.Add(new SqlParameter("@PostalCode", cusData.PostalCode));
                    command.Parameters.Add(new SqlParameter("@Phone", cusData.Phone));
                    command.Parameters.Add(new SqlParameter("@Fax", cusData.Fax));

                    object newId = command.ExecuteScalar();
                    id = Convert.ToInt32(newId);

                    transaction.Commit();
                    Console.WriteLine("Both records are written to database.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);

                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                    return false;
                }

                conn.Close();
            }
            return true;

        }


        public DataTable GetCusById(int id)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT * FROM Sales.Customers a JOIN CodeTable b ON a.ContactTitle = b.CodeID WHERE  CustomerID = @CustomerID AND 
                                                             b.CodeType='Title' ";
            using (SqlConnection conn = new SqlConnection(this.DBConn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@CustomerID", id));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return dt;
        }



    }
}