using DoAnWebFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebFilm.Areas.Admin.Controllers
{
    public class AdminBinhLuanController : Controller
    {
        dbWebFilmDataContext db = new dbWebFilmDataContext();
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            return View(db.BinhLuans.OrderByDescending(a => a.ngay_binh_luan).ToList());
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            BinhLuan binhLuan = db.BinhLuans.SingleOrDefault(n => n.id_binh_luan == id);
            ViewBag.id_binh_luan = binhLuan.id_binh_luan;
            if (binhLuan == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(binhLuan);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {

            //Get object by id
            BinhLuan binhLuan = db.BinhLuans.SingleOrDefault(n => n.id_binh_luan == id);
            ViewBag.id_binh_luan = binhLuan.id_binh_luan;
            if (binhLuan == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.BinhLuans.DeleteOnSubmit(binhLuan);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}