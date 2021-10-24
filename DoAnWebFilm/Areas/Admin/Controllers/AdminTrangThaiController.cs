using DoAnWebFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebFilm.Areas.Admin.Controllers
{
    public class AdminTrangThaiController : Controller
    {
        dbWebFilmDataContext db = new dbWebFilmDataContext();
        public ActionResult Index()
        {
            return View(db.TrangThais.ToList());
        }

        // Create TrangThai
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TrangThai trangThai)
        {
            db.TrangThais.InsertOnSubmit(trangThai);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        //Delete TrangThai
        [HttpGet]
        public ActionResult Delete(int id)
        {
            TrangThai trangThai = db.TrangThais.SingleOrDefault(n => n.id_trang_thai == id);

            ViewBag.id_trang_thai = trangThai.id_trang_thai;
            if (trangThai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(trangThai);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {

            //Get object by id
            TrangThai trangThai = db.TrangThais.SingleOrDefault(n => n.id_trang_thai == id);

            ViewBag.id_trang_thai = trangThai.id_trang_thai;
            if (trangThai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.TrangThais.DeleteOnSubmit(trangThai);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        //Edit TrangThai
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Get object by id
            TrangThai trangThai = db.TrangThais.SingleOrDefault(n => n.id_trang_thai == id);
            ViewBag.id_trang_thai = trangThai.id_trang_thai;
            if (trangThai == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(trangThai);
        }

        [ValidateInput(false)]
        public ActionResult Edit(TrangThai trangThai)
        {
            TrangThai trangThai2 = db.TrangThais.Single(n => n.id_trang_thai == trangThai.id_trang_thai);

            trangThai2.ten_trang_thai = trangThai.ten_trang_thai;
            db.SubmitChanges();

            return RedirectToAction("Index");

        }
    }
}