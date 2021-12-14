using DoAnWebFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
namespace DoAnWebFilm.Areas.Admin.Controllers
{
    public class AdminNguoiDungController : Controller
    {
        dbWebFilmDataContext db = new dbWebFilmDataContext();
        public ActionResult Index(int? page)
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;

            return View(db.NguoiDungs.ToList().OrderBy(n => n.id_nguoi_dung).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Search(String searchND, int? page)
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }

            int pageNumber = (page ?? 1);
            int pageSize = 10;

            var ngdung = from p in db.NguoiDungs
                       where p.tai_khoan.Contains(searchND)
                       select p;
            return View(ngdung.OrderBy(n => n.id_nguoi_dung).ToPagedList(pageNumber, pageSize));
        }

        // Create NguoiDung
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
        public ActionResult Create(NguoiDung nguoiDung)
        {


            if (nguoiDung.ten_nguoi_dung == null)
            {
                ViewData["Loi1"] = "Mời nhập tên người dùng";
            }
            else if (nguoiDung.tai_khoan == null)
            {
                ViewData["Loi2"] = "Mời nhập tên tài khoản";
            }
            else if (nguoiDung.mat_khau == null)
            {
                ViewData["Loi3"] = "Mời nhập mật khẩu";
            }
            else if (nguoiDung.email == null)
            {
                ViewData["Loi4"] = "Mời nhập email";
            }
            else
            {
                db.NguoiDungs.InsertOnSubmit(nguoiDung);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();

           
        }

        //Delete NguoiDung
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
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
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
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



            if (nguoiDung.ten_nguoi_dung == null)
            {
                ViewData["Loi1"] = "Mời nhập tên người dùng";
            }
            else if (nguoiDung.tai_khoan == null)
            {
                ViewData["Loi2"] = "Mời nhập tên tài khoản";
            }
            else if (nguoiDung.mat_khau == null)
            {
                ViewData["Loi3"] = "Mời nhập mật khẩu";
            }
            else if (nguoiDung.email == null)
            {
                ViewData["Loi4"] = "Mời nhập email";
            }
            else
            {
                NguoiDung nguoiDung2 = db.NguoiDungs.Single(n => n.id_nguoi_dung == nguoiDung.id_nguoi_dung);

                nguoiDung2.ten_nguoi_dung = nguoiDung.ten_nguoi_dung;
                nguoiDung2.tai_khoan = nguoiDung.tai_khoan;
                nguoiDung2.mat_khau = nguoiDung.mat_khau;
                nguoiDung2.email = nguoiDung.email;
                db.SubmitChanges();

                return RedirectToAction("Index");
            }
            return View();



            

        }
    }
}