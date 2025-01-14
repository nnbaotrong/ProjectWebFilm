﻿using DoAnWebFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebFilm.Areas.Admin.Controllers
{
    public class AdminNamPhatHanhController : Controller
    {
        dbWebFilmDataContext db = new dbWebFilmDataContext();
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            return View(db.NamPhatHanhs.ToList());
        }

        // Create NamPhatHanh
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
        public ActionResult Create(NamPhatHanh namPhatHanh)
        {
            if (namPhatHanh.nam_phat_hanh == null)
            {
                ViewData["Loi"] = "Mời nhập năm";
            }
            else
            {
                db.NamPhatHanhs.InsertOnSubmit(namPhatHanh);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();

        }

        //Delete NamPhatHanh
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            NamPhatHanh namPhatHanh = db.NamPhatHanhs.SingleOrDefault(n => n.id_nam == id);

            ViewBag.id_nam = namPhatHanh.id_nam;
            if (namPhatHanh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(namPhatHanh);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {

            //Get object by id
            NamPhatHanh namPhatHanh = db.NamPhatHanhs.SingleOrDefault(n => n.id_nam == id);
            ViewBag.id_nam = namPhatHanh.id_nam;
            if (namPhatHanh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.NamPhatHanhs.DeleteOnSubmit(namPhatHanh);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        //Edit NamPhatHanh
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoanAdmin"] == null || Session["TaiKhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            //Get object by id
            NamPhatHanh namPhatHanh = db.NamPhatHanhs.SingleOrDefault(n => n.id_nam == id);
            ViewBag.id_loai = namPhatHanh.id_nam;
            if (namPhatHanh == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(namPhatHanh);
        }

        [ValidateInput(false)]
        public ActionResult Edit(NamPhatHanh namPhatHanh)
        {


            if (namPhatHanh.nam_phat_hanh == null)
            {
                ViewData["Loi"] = "Mời nhập năm";
            }
            else
            {
                NamPhatHanh namPhatHanh2 = db.NamPhatHanhs.Single(n => n.id_nam == namPhatHanh.id_nam);

                namPhatHanh2.nam_phat_hanh = namPhatHanh.nam_phat_hanh;
                db.SubmitChanges();

                return RedirectToAction("Index");
            }
            return View();
            

        }
    }
}