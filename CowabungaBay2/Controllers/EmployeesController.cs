using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using CowabungaBay2.Models;

namespace CowabungaBay2.Controllers
{
    public class EmployeesController : Controller
    {
        private CowabungaBay2Context db = new CowabungaBay2Context();

        // GET: Employees
        public ActionResult Index()
        {

            return View(db.Employees.ToList());
        }
        public ActionResult NoI9()
        {
            return View(db.Employees.ToList());
        }

        public ActionResult NoPictureID()
        {
            return View(db.Employees.ToList());
        }
        public ActionResult Either()
        {
            return View(db.Employees.ToList());
        }

        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(UserInput userInput)
        {
            foreach (Employee x in db.Employees.ToList())
            {
                if (userInput.searchname == x.EmployeeName)
                {
                    return View("Details",x);
                }
            }
            ViewBag.Status = "That Employee Wasn't Found";
            return View();
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);

            if (employee == null)
            {
                return HttpNotFound();
            }
            if(employee.I9== null | employee.PictureID == null)
            {
                ViewBag.Name = employee.EmployeeName;
                if (employee.I9 != null)
                {
                    ViewBag.I9 = "<p style = \"color:green\" > &#10004;</p>";
                }
                else ViewBag.I9 = "<p style = \"color:red\" >X</p>";
                if (employee.PictureID != null)
                {
                    ViewBag.PictureID = "<p style = \"color:green\" > &#10004;</p>";
                }
                else ViewBag.PictureID = "<p style = \"color:red\" >X</p>";

                return View();
            }
            
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EmployeeName,I9,PictureID,I9Location,PictureIDLocation")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                bool first = false;
                foreach (var file in Request.Files)
                {
                    if (first ==false)
                    {
                        if(employee.I9Location != null)
                        {
                            WebImage i9image = WebImage.GetImageFromRequest(file.ToString());
                            employee.I9 = i9image.GetBytes();
                        }
                        first = true;
                    }
                    
                    else
                    {
                        if (employee.PictureIDLocation != null) {
                            WebImage picimage = WebImage.GetImageFromRequest(file.ToString());
                            employee.PictureID = picimage.GetBytes();
                        }
                        
                    }
                }
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        public ActionResult Download (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EmployeeName,I9,PictureID,I9Location,PictureIDLocation")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.Name = employee.EmployeeName;
            return View();
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
