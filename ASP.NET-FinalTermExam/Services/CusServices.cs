using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ASP.NET_FinalTermExam.Services
{
    public class CusServices
    {
        public List<Models.CustomerView1> GetCusGrid()
        {
            Dao.CusDao empDao = new Dao.CusDao();
            var dt = empDao.GetCusGrid();

            return this.MapCusData(dt);
        }

        public List<Models.CustomerView1> SearchCus(Models.CustomerView1 cus)
        {
            Dao.CusDao cusDao = new Dao.CusDao();
            var dt = cusDao.GetCusGridByCondition(cus);

            return this.MapCusData(dt);
        }

        public List<Models.CustomerView1> MapCusData(DataTable cusData)
        {
            List<Models.CustomerView1> result = new List<Models.CustomerView1>();

            foreach (DataRow row in cusData.Rows)
            {
                try
                {
                    result.Add(new Models.CustomerView1()
                    {
                        CustomerID = (int)row["CustomerID"],
                        CompanyName = (string)row["CompanyName"],
                        ContactName = (string)row["ContactName"],
                        ContactTitle = (string)row["ContactTitle"],
                        CodeVal = (string)row["CodeVal"]

                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return result;
        }

        public List<Models.TextValue> GetContactTitleData()
        {
            Dao.CusDao cusDao = new Dao.CusDao();
            var dt = cusDao.GetContactTitle();

            return this.MapContactTitleData(dt);
        }

        public List<Models.TextValue> MapContactTitleData(DataTable contactData)
        {
            List<Models.TextValue> result = new List<Models.TextValue>();

            foreach (DataRow row in contactData.Rows)
            {
                try
                {
                    result.Add(new Models.TextValue()
                    {
                        value = Convert.ToInt16( row["CodeId"]),
                        text = (string)row["CodeVal"]
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return result;
        }

        public Boolean InsertCus(Models.Customers cusData)
        {
            Dao.CusDao cusDao = new Dao.CusDao();
            var r = cusDao.InsertCus(cusData);

            return r;
        }

        public Boolean DeleteCus(int id)
        {
            Dao.CusDao cusDao = new Dao.CusDao();
            var r = cusDao.DeleteCus(id);

            return r;
        }

        public Models.Customers GetCusById(int id)
        {
            Dao.CusDao cusDao = new Dao.CusDao();
            var datalist = cusDao.GetCusById(id);

            return this.MapCusDetailData(datalist).FirstOrDefault();
        }

        private List<Models.Customers> MapCusDetailData(DataTable cusData)
        {
            List<Models.Customers> result = new List<Models.Customers>();

            foreach (DataRow row in cusData.Rows)
            {
                try
                {
                    result.Add(new Models.Customers()
                    {
                        CustomerID = (int)row["CustomerID"] == null ? 0 : (int)row["CustomerID"],
                        CompanyName = String.IsNullOrEmpty(row["CompanyName"].ToString()) ? "" : row["CompanyName"].ToString(),
                        ContactTitle = String.IsNullOrEmpty(row["ContactTitle"].ToString()) ? "" : row["ContactTitle"].ToString(),
                        CreationDate = (DateTime)row["CreationDate"],
                        Address = String.IsNullOrEmpty(row["Address"].ToString()) ? "" : row["Address"].ToString(),
                        City = String.IsNullOrEmpty(row["City"].ToString()) ? "" : row["City"].ToString(),
                        Country = String.IsNullOrEmpty(row["Country"].ToString()) ? "" : row["Country"].ToString(),
                        Region = String.IsNullOrEmpty(row["Region"].ToString()) ? "" : row["Region"].ToString(),
                        PostalCode = String.IsNullOrEmpty(row["PostalCode"].ToString()) ? "" : row["PostalCode"].ToString(),
                        Phone = String.IsNullOrEmpty(row["Phone"].ToString()) ? "" : row["Phone"].ToString(),
                        Fax = String.IsNullOrEmpty(row["Fax"].ToString()) ? "" : row["Fax"].ToString(),

                    });
                }
                catch (Exception e)
                {
                    var aa = e;

                }

            }
            return result;
        }
    }
}