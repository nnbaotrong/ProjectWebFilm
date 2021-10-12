using DoAnWebFilm.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebFilm.Areas.Admin.Controllers
{
    public class AdminPhimController : Controller
    {
        dbWebFilmDataContext db = new dbWebFilmDataContext();
        public ActionResult Index()
        {
            return View(db.Phims.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
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

            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
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
                    phim.anh_bia = fileName;

                    db.Phims.InsertOnSubmit(phim);

                    db.SubmitChanges();
                }
                return RedirectToAction("Index");

            }
        }


    }
}