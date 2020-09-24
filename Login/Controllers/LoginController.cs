using Login.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login.Controllers
{
    public class LoginController : Controller
    {
        

        // GET: Login
        
        [HttpGet]
        public ActionResult Login()
        {
            Session["UsuarioLogadoID"] = null;
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult Login(Usuario user)
        {
            Usuario login = new Usuario();
            
            if (login.Login(user))  
            { 
                Session["usuarioLogadoID"] = user.Nome;
                return RedirectToAction("Debts", "Debts", new { area = "" });
            }
            else { 
            return View("Error");
                
            }
        }
        public ActionResult Teste()
        {
            if (Session["usuarioLogadoID"] != null) { 

                return View();
            }
            return Redirect("Login");
        }

        public ActionResult Exemplo()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SignUp(Usuario user)
        {
            Usuario signUp = new Usuario();
            if (signUp.SignUp(user) == 2)
            {
                ViewBag.Message = "Usuario cadastrado com sucesso";
            }
            else if (signUp.SignUp(user) == 1)
            {
                ViewBag.Message = "Este usuário já existe, por favor tente outro nome de usuário.";
            }
            else if (signUp.SignUp(user) == 3)
            {
                ViewBag.Message = "Senhas não combinam, por favor tente novamente.";
            }
            else if (signUp.SignUp(user) == 4)
            {
                ViewBag.Message = "Usuario precisa ter mais do que 5 caracteres.";
            }
            else if (signUp.SignUp(user) == 5)
            {
                ViewBag.Message = "Senha precisa ter mais do que 5 caracteres.";
            }

            return View();
        }

        
    }
}