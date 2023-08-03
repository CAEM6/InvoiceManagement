using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InvoiceManagement.Models;

namespace InvoiceManagement.Controllers
{
    public class InvoicesController : Controller
    {
        private InvoiceManagementEntities db = new InvoiceManagementEntities();

        // GET: Invoices
        public ActionResult newInvoice()
        {
            return View();
        }
        public ActionResult searchInvoice()
        {
            return View();
        }
        public ActionResult Index()
        {
            var invoices = db.Invoices.Include(i => i.Account).Include(i => i.Supplier);
            return View(invoices.ToList());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Codigo");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "RFC");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SupplierId,AccountId,Folio,NumTrans,OrdenCompra,Moneda,TipoPago,MetodoPago,Fecha,Iva,IvaPorcentaje,IvaRetenido,IvaTrasladado,Total,IsrRetenido,SubTotal,Pagado,UltimoPago,Balance,Notas")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Codigo", invoice.AccountId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "RFC", invoice.SupplierId);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Codigo", invoice.AccountId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "RFC", invoice.SupplierId);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SupplierId,AccountId,Folio,NumTrans,OrdenCompra,Moneda,TipoPago,MetodoPago,Fecha,Iva,IvaPorcentaje,IvaRetenido,IvaTrasladado,Total,IsrRetenido,SubTotal,Pagado,UltimoPago,Balance,Notas")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Codigo", invoice.AccountId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "RFC", invoice.SupplierId);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
