using DoAnWebFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebFilm.Areas.Admin.Controllers
{
    public class AdminManagerController : Controller
    {
        dbWebFilmDataContext db = new dbWebFilmDataContext();
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            return View(db.AdminPros.ToList());
        }

        //
        //public ActionResult Details(int id)
        //{
        //    //Lay ra doi tuong sach theo ma
        //    AdminPro adminPro = db.AdminPros.SingleOrDefault(n => n.id == id);
        //    ViewBag.id = adminPro.id;

        //    if (adminPro == null)
        //    {
        //        Response.StatusCode = 404;
        //        return null;
        //    }
        //    return View(adminPro);
        //}

        // Create Admin
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(AdminPro adminPro)
        {

            if (adminPro.ten_admin == null)
            {
                ViewData["Loi1"] = "Mời nhập tên admin";
            }
            else if (adminPro.mat_khau_admin == null)
            {
                ViewData["Loi2"] = "Mời nhập mật khẩu admin";
            }
            else
            {
                db.AdminPros.InsertOnSubmit(adminPro);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();

         
        }

        //Delete Admin
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            AdminPro adminPro = db.AdminPros.SingleOrDefault(n => n.id == id);
            ViewBag.id = adminPro.id;
            if (adminPro == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(adminPro);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {

            //Get object by id
            AdminPro adminPro = db.AdminPros.SingleOrDefault(n => n.id == id);
            ViewBag.id = adminPro.id;
            if (adminPro == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.AdminPros.DeleteOnSubmit(adminPro);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        //Edit Admin
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            //Get object by id
            AdminPro adminPro = db.AdminPros.SingleOrDefault(n => n.id == id);
            ViewBag.id = adminPro.id;
            if (adminPro == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(adminPro);
        }

        [ValidateInput(false)]
        public ActionResult Edit(AdminPro adminPro)
        {

            if (adminPro.ten_admin == null)
            {
                ViewData["Loi1"] = "Mời nhập tên admin";
            }
            else if (adminPro.mat_khau_admin == null)
            {
                ViewData["Loi2"] = "Mời nhập mật khẩu admin";
            }
            else
            {
                AdminPro adminPro2 = db.AdminPros.Single(n => n.id == adminPro.id);

                adminPro2.ten_admin = adminPro.ten_admin;
                adminPro2.mat_khau_admin = adminPro.mat_khau_admin;
                db.SubmitChanges();

                return RedirectToAction("Index");
            }
            return View();

      

        }
    }
}