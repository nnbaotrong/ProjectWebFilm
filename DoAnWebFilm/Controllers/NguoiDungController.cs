using DoAnWebFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebFilm.Controllers
{
    public class NguoiDungController : Controller
    {
        dbWebFilmDataContext db = new dbWebFilmDataContext();

        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dangky(FormCollection collection, NguoiDung nd)
        {
            var hoten = collection["HotenND"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var nhaplaimatkhau = collection["Nhaplaimatkhau"];
            var email = collection["Email"];
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Mời Nhập họ và tên";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Mời Nhập Tên tài khoản";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Mời Nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(nhaplaimatkhau))
            {
                ViewData["Loi4"] = "Mời Nhập Lại mật khẩu";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email không được để trống";
            }
            else
            {
                NguoiDung tk = db.NguoiDungs.SingleOrDefault(n => n.tai_khoan == tendn );
                NguoiDung mal = db.NguoiDungs.SingleOrDefault(n => n.email == email);
                if (tk == null && mal == null)
                {
                    if (matkhau.Equals(nhaplaimatkhau))
                    {
                        nd.ten_nguoi_dung = hoten;
                        nd.tai_khoan = tendn;
                        nd.mat_khau = matkhau;
                        nd.email = email;
                        db.NguoiDungs.InsertOnSubmit(nd);
                        db.SubmitChanges();
                        return RedirectToAction("Dangnhap");
                    }
                    else
                    {
                        ViewData["LoiMK"] = "Mật khẩu nhập không giống";
                    }
                   
                }
               if(tk != null)
                {
                    ViewData["LoiTK"] = "Tên tài khoản tồn tại";
                }
                if (mal != null)
                {
                    ViewData["LoiMAL"] = "Email tồn tại";
                }


            }
            return this.Dangky();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Dangnhap(FormCollection collection)
        {
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                NguoiDung nd = db.NguoiDungs.SingleOrDefault(n => n.tai_khoan == tendn && n.mat_khau == matkhau);
                if (nd != null)
                {
                    ViewBag.Thongbao = "Chúc mừng bạn đăng nhập thành công";
                    Session["Taikhoan"] = nd;
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
    }
}