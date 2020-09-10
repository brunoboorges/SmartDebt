using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Login.Controllers
{
    public class DepartmentsController : Controller
    {
        // GET: Departments
        

        public ActionResult Departments(Department department)
        {
            var departmento = department.ShowDepartments(department).ToList();

            return View(departmento);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Department department)
        {
            department.PostarDepartamento(department);
            return Redirect("Departments");
        }
        //DELETEEE
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Department department = new Department();

            if (department.DeleteDepartment(id) )
            {
                return RedirectToAction("Departments");
            }
            return RedirectToAction("Departments");


        }
       [HttpGet]
        public ActionResult Delete(Department dep)
        {
            
            return PartialView();


        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Department dep)
        {
            dep.EditarDepartamento(dep);

            return View();

        }
    }
}