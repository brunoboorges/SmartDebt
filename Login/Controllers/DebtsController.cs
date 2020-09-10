using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login.Models;


namespace Login.Controllers
{
    public class DebtsController : Controller
    {
        // GET: Debts
        public ActionResult Debts(Debt debt)
        {          
            
            var listaDebito = debt.ShowDebts(debt).ToList(); 
            return View(listaDebito);
        }

        [HttpGet]
        public ActionResult DebtCreate()
        {
            Department dep = new Department();
            var listDepartment = dep.ShowDepartments(dep).ToList();
            ViewBag.ListDepartments = new SelectList(listDepartment);
            return PartialView();
        }
        [HttpPost]
        public ActionResult DebtCreate(Debt debt)
        {
            debt.InsertDebt(debt);
            return View();
        }

        
        [HttpPost]
        public ActionResult RemoveDebt(int id)
        {
            
            
            

            Debt debt = new Debt();
            debt.RemoveDebt(id);
            return RedirectToAction("Debts");
        }
        [HttpGet]
        public ActionResult RemoveDebt()
        {
            
            return View();
        }
    }
}