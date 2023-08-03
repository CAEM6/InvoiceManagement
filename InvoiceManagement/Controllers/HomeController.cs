using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult newInvoice() {
            ViewBag.Message = "Nueva Factura";
            return View();
        }
        public ActionResult reports()
        {
            ViewBag.Message = "Reportes";
            return View();
        }
        public ActionResult suppliers()
        {
            ViewBag.Message = "Proveedores";
            return View();
        }
        public ActionResult accounts()
        {
            ViewBag.Message = "Cuentas";
            return View();
        }

        public ActionResult searchInvoice()
        {
            ViewBag.Message = "Facturas";
            return View();
        }
    }
}