using DoAnWebFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebFilm.Areas.Admin.Controllers
{
    public class AdminNguoiDungController : Controller
    {
        dbWebFilmDataContext db = new dbWebFilmDataContext();
        public ActionResult Index()
        {
            return View(db.NguoiDungs.ToList());
        }

        // Create NguoiDung
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NguoiDung nguoiDung)
        {
            db.NguoiDungs.InsertOnSubmit(nguoiDung);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        //Delete NguoiDung
        [HttpGet]
        public ActionResult Delete(int id)
        {
            NguoiDung nguoiDung = db.NguoiDungs.SingleOrDefault(n => n.id_nguoi_dung == id);

            ViewBag.id_nguoi_dung = nguoiDung.id_nguoi_dung;
            if (nguoiDung == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nguoiDung);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {

            //Get object by id
            NguoiDung nguoiDung = db.NguoiDungs.SingleOrDefault(n => n.id_nguoi_dung == id);
            ViewBag.id_nguoi_dung = nguoiDung.id_nguoi_dung;
            if (nguoiDung == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.NguoiDungs.DeleteOnSubmit(nguoiDung);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        //Edit NguoiDung
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Get object by id
            NguoiDung nguoiDung = db.NguoiDungs.SingleOrDefault(n => n.id_nguoi_dung == id);
            ViewBag.id_nguoi_dung = nguoiDung.id_nguoi_dung;
            if (nguoiDung == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(nguoiDung);
        }

        [ValidateInput(false)]
        public ActionResult Edit(NguoiDung nguoiDung)
        {
            NguoiDung nguoiDung2 = db.NguoiDungs.Single(n => n.id_nguoi_dung == nguoiDung.id_nguoi_dung);

            nguoiDung2.ten_nguoi_dung = nguoiDung.ten_nguoi_dung;
            nguoiDung2.tai_khoan = nguoiDung.tai_khoan;
            nguoiDung2.mat_khau = nguoiDung.mat_khau;
            nguoiDung2.email = nguoiDung.email;
            db.SubmitChanges();

            return RedirectToAction("Index");

        }
    }
}