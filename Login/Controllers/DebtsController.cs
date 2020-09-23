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
                double totalMes = debt.SumDebtsActualMounth(debt);
                Session["valorTotalMes"] = totalMes.ToString("C2");
                Session["valorTotal"] = debt.SumDebts(debt).ToString("C2");
                
                //soma total receita

                double receitaMensal = user.ReceitaMensal(usuario);
                double otReceitas = user.SumOtReceitas(user);
                double receitaResult = receitaMensal + otReceitas;

                Session["receitaTotal"] = receitaResult.ToString("C2");
                Session["receita"] = receitaMensal.ToString("C2");
                Session["totalOtReceitas"] = otReceitas.ToString("C2");

                double saldoMensal =  receitaResult - totalMes;
                Session["saldoMensal"] = saldoMensal.ToString("C2");

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
            string x = Session["usuarioLogadoID"].ToString();
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
        public ActionResult AdicionarReceita(Usuario user)
        {
            string nome = Session["usuarioLogadoID"].ToString();
            user.InserirReceita(user, nome);
            return RedirectToAction("Debts");
        }
        [HttpGet]
        public ActionResult LerOutrasReceitas()
        {
            Usuario user = new Usuario();
            if (Session["usuarioLogadoID"] != null)
            {
                string nome = Session["usuarioLogadoID"].ToString();
                var listaOtReceitas = user.LerOutrasReceitas(user, nome).ToList();
                return PartialView(listaOtReceitas);
            }return View("Debts");
        }
        [HttpPost]
        public ActionResult LerOutrasReceitas(Usuario user)
        {
            user.InserirOutrasReceitas(user);
            return Redirect(Request.UrlReferrer.ToString());
        }
        [HttpPost]
        public ActionResult OtDelete(int id)
        {
            Usuario user = new Usuario();
            user.OtDelete(id);
            return RedirectToAction("Debts");
        }
    }
}