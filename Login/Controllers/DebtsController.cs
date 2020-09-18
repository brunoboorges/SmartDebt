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
            Usuario user = new Usuario();
            if (Session["usuarioLogadoID"] != null) {
                string usuario = Session["usuarioLogadoID"].ToString();
                Session["valorTotalMes"] = debt.SumDebtsActualMounth(debt).ToString("C2");
                Session["receita"] = user.ReceitaMensal(usuario).ToString("C2");
                Session["valorTotal"] = debt.SumDebts(debt).ToString("C2");
            var listaDebito = debt.ShowDebts(debt).ToList(); 
            return View(listaDebito);
            }
            else {
                return RedirectToAction("Login", "Login", new { area = "" });
            }

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
            String x = Session["usuarioLogadoID"].ToString();
            debt.InsertDebt(debt);
            return RedirectToAction("Debts");
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
            return PartialView();
        }

        [HttpGet]
        public ActionResult AdicionarReceita()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult AdicionarReceita(Debt debt)
        {
            
            return RedirectToAction("Debts");
        }
    }
}