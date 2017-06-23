using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_FinalTermExam.Controllers
{
    public class CusController : Controller
    {
        // GET: Cus
        public ActionResult Index()
        {
            return View();
        }



        public JsonResult GetCusGrid()
        {
            Services.CusServices cusService = new Services.CusServices();
            var data = cusService.GetCusGrid();

            return this.Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost()]
        public JsonResult SearchCus(Models.Customers cus)
        {
            Services.CusServices cusService = new Services.CusServices();
            var cusData = new Models.CustomerView1();
            cusData.CustomerID =  Convert.ToInt16(cus.CustomerID) ;
            cusData.CompanyName = "%" + Convert.ToString(cus.CompanyName) + "%";
            cusData.ContactName = "%" + Convert.ToString(cus.ContactName) + "%";
            cusData.ContactTitle = "000"+ Convert.ToString(cus.ContactTitle);

            var data = cusService.SearchCus(cusData);

            return this.Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteCus(int id)
        {
            Services.CusServices cusService = new Services.CusServices();
            var data = cusService.DeleteCus(id);

            return this.Json(data, JsonRequestBehavior.AllowGet);
        }
        

        /// <summary>
        /// 取得所有 PorductName 和 PorductID
        /// </summary>
        /// <returns></returns>
        public JsonResult ReadContactTitleList()
        {
            Services.CusServices cusService = new Services.CusServices();

            var data = cusService.GetContactTitleData();

            return this.Json(data, JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost()]
        public JsonResult InsertCus(Models.Customers cus)
        {
            Services.CusServices cusService = new Services.CusServices();
            try
            {

                var cusData = new Models.Customers();
                cusData.CompanyName = cus.CompanyName == null ? string.Empty : cus.CompanyName;
                cusData.ContactName = cus.ContactName == null ? string.Empty : cus.ContactName;
                cusData.ContactTitle = cus.ContactTitle;
                cusData.CreationDate = cus.CreationDate;
                cusData.Address = cus.Address == null ? string.Empty : cus.Address;
                cusData.City = cus.City == null ? string.Empty : cus.City;
                cusData.Country = cus.Country == null ? string.Empty : cus.Country;
                cusData.Region = cus.Region == null ? string.Empty : cus.Region;
                cusData.PostalCode = cus.PostalCode == null ? string.Empty : cus.PostalCode;
                cusData.Phone = cus.Phone == null ? string.Empty : cus.Phone;
                cusData.Fax = cus.Fax == null ? string.Empty : cus.Fax;

                var data = cusService.InsertCus(cusData);

                return this.Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                return this.Json(e, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult GetUpdateWindow(int id)
        {

            Services.CusServices cusService = new Services.CusServices();

            var data = cusService.GetCusById(id);

            ViewBag.CustomerID = data.CustomerID;
            ViewBag.CompanyName = data.CompanyName;
            ViewBag.ContactName = data.ContactName;
            ViewBag.ContactTitle = Convert.ToInt16( data.ContactTitle);
            ViewBag.CreationDate = data.CreationDate;
            ViewBag.Address = data.Address;
            ViewBag.City = data.City;
            ViewBag.Country = data.Country;
            ViewBag.Region = data.Region;
            ViewBag.PostalCode = data.PostalCode;
            ViewBag.Phone = data.Phone;
            ViewBag.Fax = data.Fax;

            return PartialView();
        }


    }


}