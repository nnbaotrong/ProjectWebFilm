using DoAnWebFilm.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebFilm.Areas.Admin.Controllers
{
    public class AdminDienVienController : Controller
    {
        dbWebFilmDataContext db = new dbWebFilmDataContext();
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            return View(db.DienViens.ToList());
        }

        //
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
        [ValidateInput(false)]
        public ActionResult Create(DienVien dienvien, HttpPostedFileBase fileUpload)
        {

            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {

                    if (dienvien.ten_dien_vien == null )
                    {
                        ViewData["Loi"] = "Mời nhập tên diễn viên";
                    }
                    else
                    {
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        DateTime localDate = DateTime.Now;
                        string date_str = localDate.ToString("ddMMyyyy_HHmmss");
                        fileName = date_str + "_" + fileName;
                        var path = Path.Combine(Server.MapPath("~/HinhDienVien"), fileName);

                        if (System.IO.File.Exists(path))
                            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        else
                        {
                            fileUpload.SaveAs(path);
                        }
                        dienvien.anh_bia = fileName;

                        db.DienViens.InsertOnSubmit(dienvien);
                        db.SubmitChanges();
                        return RedirectToAction("Index");
                    }
                  



                }

                return View();

            }
        }

        //Show detail actor
        //public ActionResult Details(int id)
        //{
        //    //Lay ra doi tuong sach theo ma
        //    DienVien dienvien = db.DienViens.SingleOrDefault(n => n.id_dien_vien == id);
        //    ViewBag.id_dien_vien = dienvien.id_dien_vien;

        //    if (dienvien == null)
        //    {
        //        Response.StatusCode = 404;
        //        return null;
        //    }
        //    return View(dienvien);
        //}

        //Delete Actor
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            //Lay ra doi tuong sach can xoa theo ma
            DienVien dienvien = db.DienViens.SingleOrDefault(n => n.id_dien_vien == id);
            ViewBag.id_dien_vien = dienvien.id_dien_vien;
            if (dienvien == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dienvien);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            //Lay ra doi tuong sach can xoa theo ma
            DienVien dienvien = db.DienViens.SingleOrDefault(n => n.id_dien_vien == id);
            ViewBag.id_dien_vien = dienvien.id_dien_vien;
            if (dienvien == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.DienViens.DeleteOnSubmit(dienvien);
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
            //Lay ra doi tuong sach theo ma
            DienVien dienvien = db.DienViens.SingleOrDefault(n => n.id_dien_vien == id);
            ViewBag.id_dien_vien = dienvien.id_dien_vien;
            if (dienvien == null)
            {
                Response.StatusCode = 404;
                return null;
            }
           
            return View(dienvien);
        }

        [ValidateInput(false)]
        public ActionResult Edit(DienVien dienvien, HttpPostedFileBase fileUpload)
        {

            DienVien dienvien2 = db.DienViens.Single(n => n.id_dien_vien == dienvien.id_dien_vien);
            if (fileUpload != null)
            {

                // Luu ten fie, luu y bo sung thu vien using System.IO;
                var fileName = Path.GetFileName(fileUpload.FileName);
                DateTime localDate = DateTime.Now;
                string date_str = localDate.ToString("ddMMyyyy_HHmmss");
                fileName = date_str + "_" + fileName;
                //Luu duong dan cua file
                var path = Path.Combine(Server.MapPath("~/HinhDienVien"), fileName);
                //Kiem tra hình anh ton tai chua?
                if (System.IO.File.Exists(path))
                    ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                else
                {
                    //Luu hinh anh vao duong dan
                    fileUpload.SaveAs(path);
                }
                dienvien.anh_bia = fileName;
                dienvien2.anh_bia = dienvien.anh_bia;
            }

            if (dienvien.ten_dien_vien == null)
            {
                ViewData["Loi"] = "Mời nhập tên diễn viên";
            }
            else
            {
                //Luu vao CSDL 
                dienvien2.ten_dien_vien = dienvien.ten_dien_vien;

                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


    }
}