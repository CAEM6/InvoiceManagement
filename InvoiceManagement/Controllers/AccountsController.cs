using InvoiceManagement.Models;
using InvoiceManagement.Utils;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace InvoiceManagement.Controllers
{
    public class AccountsController : Controller
    {
        private InvoiceManagementEntities db = new InvoiceManagementEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int? id)
        {
            var modelInDb = db.Accounts.Find(id);
            if (modelInDb == null)
            {
                modelInDb = new Account();
                modelInDb.Id = 0;
            }
            return View(modelInDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo,Descripcion")] Account account)
        {
            Response response = new Response();

            try
            {
                if (db.Accounts.FirstOrDefault(x => x.Codigo == account.Codigo && x.Id != account.Id) != null)
                {
                    response.Error = true;
                    response.Messages.Add($"El codigo {account.Codigo} ya se encuentra registrado en el sistema");
                }

                if (response.Error == false)
                {
                    if (account.Id == 0)
                        db.Accounts.Add(account);
                    else
                        db.Entry(account).State = EntityState.Modified;

                    db.SaveChanges();
                    response.Messages.Add($"Guardado con éxito");
                }
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionHandler.ResolveException(ex, ref response);
            }
            catch (DbUpdateException ex)
            {
                ExceptionHandler.ResolveException(ex, ref response);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ResolveException(ex, ref response);
            }

            dynamic success = new
            {
                error = response.Error,
                message = String.Join(",", response.Messages),
                id = account.Id,
            };

            return Json(success, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id")] Account account)
        {
            Response response = new Response();

            try
            {
                if (db.Accounts.FirstOrDefault(x => x.Id == account.Id) == null)
                {
                    response.Error = true;
                    response.Messages.Add($"El codigo {account.Codigo} no se encuentra registrado en el sistema");
                }

                if (db.Suppliers.Any(x => x.AccountId == account.Id))
                {
                    response.Error = true;
                    response.Messages.Add($"No se puede eliminar esta cuenta ya que se encuentra en uso");
                }

                if (response.Error == false)
                {
                    db.Accounts.Remove(account);
                    db.SaveChanges();
                    response.Messages.Add($"Eliminado con éxito");
                }
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionHandler.ResolveException(ex, ref response);
            }
            catch (DbUpdateException ex)
            {
                ExceptionHandler.ResolveException(ex, ref response);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ResolveException(ex, ref response);
            }

            dynamic success = new
            {
                error = response.Error,
                message = String.Join(",", response.Messages),
                id = response.Error is false ? GetLastId() : account.Id,
            };

            return Json(success, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetList(JqueryDataTableParam param)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var listado = db.Accounts.AsEnumerable();

            listado.ToList();

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                listado = listado.Where(x => x.Codigo.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Descripcion.ToString().Contains(param.sSearch.ToLower())).ToList();
            }

            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];

            switch (sortColumnIndex)
            {
                case 0:
                    listado = sortDirection == "asc" ? listado.OrderBy(c => c.Codigo).ThenBy(x => x.Id) : listado.OrderByDescending(c => c.Codigo).ThenByDescending(x => x.Id);
                    break;
                case 1:
                    listado = sortDirection == "asc" ? listado.OrderBy(c => c.Descripcion).ThenBy(x => x.Id) : listado.OrderByDescending(c => c.Descripcion).ThenByDescending(x => x.Id);
                    break;
                default:
                    listado = listado.OrderByDescending(c => c.Id);
                    break;
            }

            var displayResult = listado.Skip(param.iDisplayStart)
                .Take(param.iDisplayLength).ToList();
            var totalRecords = listado.Count();

            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            }, JsonRequestBehavior.AllowGet);

        }

        // TODO: cambiar ese metodo para hacerlo de manera global con los demas catalogos, aun no se va a usar
        private int GetLastId()
        {
            var lastIdInDb = db.Accounts.Select(x => x.Id).OrderByDescending(x => x).FirstOrDefault();
            return lastIdInDb;
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
