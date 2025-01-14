﻿using DoAnWebFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebFilm.Areas.Admin.Controllers
{
    public class AdminQuocGiaController : Controller
    {
        dbWebFilmDataContext db = new dbWebFilmDataContext();
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            return View(db.QuocGias.ToList());
        }

        // Create QuocGias
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
        public ActionResult Create(QuocGia quocGia)
        {

            if (quocGia.ten_quoc_gia == null)
            {
                ViewData["Loi"] = "Mời nhập tên quốc gia";
            }
            else
            {
                db.QuocGias.InsertOnSubmit(quocGia);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();

            
        }

        //Delete QuocGias
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            QuocGia quocGia = db.QuocGias.SingleOrDefault(n => n.id_quoc_gia == id);
            ViewBag.id_quoc_gia = quocGia.id_quoc_gia;
            if (quocGia == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(quocGia);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {

            //Get object by id
            QuocGia quocGia = db.QuocGias.SingleOrDefault(n => n.id_quoc_gia == id);
            ViewBag.id_quoc_gia = quocGia.id_quoc_gia;
            if (quocGia == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.QuocGias.DeleteOnSubmit(quocGia);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        //Edit QuocGias
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            //Get object by id
            QuocGia quocGia = db.QuocGias.SingleOrDefault(n => n.id_quoc_gia == id);
            ViewBag.id_quoc_gia = quocGia.id_quoc_gia;
            if (quocGia == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(quocGia);
        }

        [ValidateInput(false)]
        public ActionResult Edit(QuocGia quocGia)
        {

            if (quocGia.ten_quoc_gia == null)
            {
                ViewData["Loi"] = "Mời nhập tên quốc gia";
            }
            else
            {
                QuocGia quocGia2 = db.QuocGias.Single(n => n.id_quoc_gia == quocGia.id_quoc_gia);
                quocGia2.ten_quoc_gia = quocGia.ten_quoc_gia;
                db.SubmitChanges();

                return RedirectToAction("Index");
            }
            return View();

        }
    }
}