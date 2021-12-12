using DoAnWebFilm.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PagedList;
using PagedList.Mvc;

namespace DoAnWebFilm.Areas.Admin.Controllers
{
    public class AdminPhimController : Controller
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

            return View(db.Phims.ToList().OrderBy(n => n.id_phim).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Search(String searchMovie, int? page)
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }

            int pageNumber = (page ?? 1);
            int pageSize = 10;

            var phim = from p in db.Phims
                       where p.ten_phim.Contains(searchMovie)
                       select p;
            return View(phim.OrderBy(n => n.id_phim).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {

            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }

            ViewBag.id_quoc_gia = new SelectList(db.QuocGias.ToList().OrderBy(n => n.ten_quoc_gia), "id_quoc_gia", "ten_quoc_gia");
            ViewBag.id_nam = new SelectList(db.NamPhatHanhs.ToList().OrderBy(n => n.nam_phat_hanh), "id_nam", "nam_phat_hanh");
            ViewBag.id_trang_thai = new SelectList(db.TrangThais.ToList().OrderBy(n => n.ten_trang_thai), "id_trang_thai", "ten_trang_thai");

          

            ViewBag.ma_loai = (from a in db.Loais select a).ToList();
            ViewBag.ma_dien_vien = (from a in db.DienViens select a).ToList();
            return View();

           
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Phim phim, HttpPostedFileBase fileUpload)
        {
            ViewBag.id_quoc_gia = new SelectList(db.QuocGias.ToList().OrderBy(n => n.ten_quoc_gia), "id_quoc_gia", "ten_quoc_gia");
            ViewBag.id_nam = new SelectList(db.NamPhatHanhs.ToList().OrderBy(n => n.nam_phat_hanh), "id_nam", "nam_phat_hanh");
            ViewBag.id_trang_thai = new SelectList(db.TrangThais.ToList().OrderBy(n => n.ten_trang_thai), "id_trang_thai", "ten_trang_thai");


            ViewBag.ma_loai = (from a in db.Loais select a).ToList();
            ViewBag.ma_dien_vien = (from a in db.DienViens select a).ToList();

            //Kiem tra duong dan file
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            //Them vao CSDL
            else
            {
                if (ModelState.IsValid)
                {
                    //Luu ten fie, luu y bo sung thu vien using System.IO;
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    DateTime localDate = DateTime.Now;
                    string date_str = localDate.ToString("ddMMyyyy_HHmmss");
                    fileName = date_str + "_" + fileName;
                    //Luu duong dan cua file
                    var path = Path.Combine(Server.MapPath("~/HinhPhim"), fileName);
                    //Kiem tra hình anh ton tai chua?
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        //Luu hinh anh vao duong dan
                        fileUpload.SaveAs(path);
                    }
                    phim.anh_bia = fileName;
                    //Luu vao CSDL
                    db.Phims.InsertOnSubmit(phim);
                    db.SubmitChanges();
                }
                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(int id)
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            //Lay ra doi tuong theo ma
            Phim phim = db.Phims.SingleOrDefault(n => n.id_phim == id);
            ViewBag.id_phim = phim.id_phim;
            if (phim == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(phim);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            //Lay ra doi tuong  can xoa theo ma
            Phim phim = db.Phims.SingleOrDefault(n => n.id_phim == id);
            ViewBag.id_phim = phim.id_phim;
            if (phim == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(phim);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            //Lay ra doi tuong  can xoa theo ma
            Phim phim = db.Phims.SingleOrDefault(n => n.id_phim == id);
            ViewBag.id_phim = phim.id_phim;
            if (phim == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Phims.DeleteOnSubmit(phim);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            //Lay ra doi tuong theo ma
            Phim phim = db.Phims.SingleOrDefault(n => n.id_phim == id);
            ViewBag.id_phim = phim.id_phim;
            if (phim == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Dua du lieu vao dropdownList
            ViewBag.id_quoc_gia = new SelectList(db.QuocGias.ToList().OrderBy(n => n.ten_quoc_gia), "id_quoc_gia", "ten_quoc_gia");
            ViewBag.id_nam = new SelectList(db.NamPhatHanhs.ToList().OrderBy(n => n.nam_phat_hanh), "id_nam", "nam_phat_hanh");
            ViewBag.id_trang_thai = new SelectList(db.TrangThais.ToList().OrderBy(n => n.ten_trang_thai), "id_trang_thai", "ten_trang_thai");

            ViewBag.ma_loai = (from a in db.Loais select a).ToList();
            ViewBag.ma_dien_vien = (from a in db.DienViens select a).ToList();
            return View(phim);
        }


        [ValidateInput(false)]
        public ActionResult Edit(Phim phim, HttpPostedFileBase fileUpload)
        {
            //Dua du lieu vao dropdownload
            ViewBag.id_quoc_gia = new SelectList(db.QuocGias.ToList().OrderBy(n => n.ten_quoc_gia), "id_quoc_gia", "ten_quoc_gia");
            ViewBag.id_nam = new SelectList(db.NamPhatHanhs.ToList().OrderBy(n => n.nam_phat_hanh), "id_nam", "nam_phat_hanh");
            ViewBag.id_trang_thai = new SelectList(db.TrangThais.ToList().OrderBy(n => n.ten_trang_thai), "id_trang_thai", "ten_trang_thai");


            ViewBag.ma_loai = (from a in db.Loais select a).ToList();
            ViewBag.ma_dien_vien = (from a in db.DienViens select a).ToList();

            Phim phim2 = db.Phims.Single(n => n.id_phim == phim.id_phim);
            if (fileUpload != null)
            {

                // Luu ten fie, luu y bo sung thu vien using System.IO;
                var fileName = Path.GetFileName(fileUpload.FileName);
                DateTime localDate = DateTime.Now;
                string date_str = localDate.ToString("ddMMyyyy_HHmmss");
                fileName = date_str + "_" + fileName;
                //Luu duong dan cua file
                var path = Path.Combine(Server.MapPath("~/HinhPhim"), fileName);
                //Kiem tra hình anh ton tai chua?
                if (System.IO.File.Exists(path))
                    ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                else
                {
                    //Luu hinh anh vao duong dan
                    fileUpload.SaveAs(path);
                }
                phim.anh_bia = fileName;
                phim2.anh_bia = phim.anh_bia;
            }
            //Luu vao CSDL 
            phim2.ten_phim = phim.ten_phim;
            phim2.link_phim = phim.link_phim;
            phim2.mo_ta = phim.mo_ta;
            phim2.trailer = phim.trailer;
            phim2.id_trang_thai = phim.id_trang_thai;
            phim2.id_quoc_gia = phim.id_quoc_gia;
            phim2.id_nam = phim.id_nam;

            db.SubmitChanges();

            return RedirectToAction("Index");

        }

        //Cmt
        public ActionResult ShowComment(int id)
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            var cmt = from bl in db.BinhLuans where bl.id_phim == id select bl;
            return PartialView(cmt);
        }
        [HttpPost]
        public ActionResult DeleteComment(int id)
        {
            //Lay ra doi tuong  can xoa theo ma
            Phim phim = db.Phims.SingleOrDefault(n => n.id_phim == id);
            ViewBag.id_phim = phim.id_phim;
            if (phim == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //db.Phims.DeleteOnSubmit(phim);
            //db.SubmitChanges();
            return RedirectToAction("Index");
        }


    }
}