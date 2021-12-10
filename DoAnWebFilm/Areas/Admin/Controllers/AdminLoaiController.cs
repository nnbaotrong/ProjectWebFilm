using DoAnWebFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebFilm.Areas.Admin.Controllers
{
    public class AdminLoaiController : Controller
    {
        dbWebFilmDataContext db = new dbWebFilmDataContext();
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            return View(db.Loais.ToList());
        }

        // Create Loai
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
        public ActionResult Create(Loai loai)
        {
            db.Loais.InsertOnSubmit(loai);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        //Delete Loai
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            Loai loai = db.Loais.SingleOrDefault(n => n.id_loai == id);

            ViewBag.id_loai = loai.id_loai;
            if (loai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(loai);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {

            //Get object by id
            Loai loai = db.Loais.SingleOrDefault(n => n.id_loai == id);
            ViewBag.id_loai = loai.id_loai;
            if (loai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Loais.DeleteOnSubmit(loai);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        //Edit Loai
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            //Get object by id
            Loai loai = db.Loais.SingleOrDefault(n => n.id_loai == id);
            ViewBag.id_loai = loai.id_loai;
            if (loai == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(loai);
        }

        [ValidateInput(false)]
        public ActionResult Edit(Loai loai)
        {
            Loai loai2 = db.Loais.Single(n => n.id_loai == loai.id_loai);

            loai2.ten_loai = loai.ten_loai;
            db.SubmitChanges();

            return RedirectToAction("Index");

        }
    }
}