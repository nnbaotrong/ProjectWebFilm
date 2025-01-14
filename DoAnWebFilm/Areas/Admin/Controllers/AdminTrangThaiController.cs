﻿using DoAnWebFilm.Models;
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
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            return View(db.TrangThais.ToList());
        }

        // Create TrangThai
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
        public ActionResult Create(TrangThai trangThai)
        {
            if (trangThai.ten_trang_thai == null)
            {
                ViewData["Loi"] = "Mời nhập trang thái";
            }
            else
            {
                db.TrangThais.InsertOnSubmit(trangThai);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
   
        }

        //Delete TrangThai
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
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
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
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

            if (trangThai.ten_trang_thai == null)
            {
                ViewData["Loi"] = "Mời nhập trang thái";
            }
            else
            {
                TrangThai trangThai2 = db.TrangThais.Single(n => n.id_trang_thai == trangThai.id_trang_thai);

                trangThai2.ten_trang_thai = trangThai.ten_trang_thai;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}