using DoAnWebFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PagedList;
using PagedList.Mvc;

namespace DoAnWebFilm.Controllers
{
    public class HomeController : Controller
    {

        dbWebFilmDataContext db = new dbWebFilmDataContext();
        private List<Phim> layPhimMoi(int count)
        {
            return db.Phims.OrderByDescending(a => a.id_phim).Take(count).ToList();
        }

        //Index
        public ActionResult Index(int ? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);

            var phimMoi = layPhimMoi(100);
            return View(phimMoi.ToPagedList(pageNum,pageSize));
        }

        //Details
        public ActionResult Details(int id)
        {
            var phim = from p in db.Phims
                       where p.id_phim == id
                       select p;
            return View(phim.Single());
        }
        //Watch Movie
        public ActionResult WatchMovie(int id)
        {
            var phim = from p in db.Phims
                       where p.id_phim == id
                       select p;
            return View(phim.Single());
        }

        //Type Movie
        public ActionResult TypeMovie()
        {
            var type = from t in db.Loais select t;
            return PartialView(type);
        }
        public ActionResult MovieForType(int id, int? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);

            var movie = from m in db.Phims 
                        join ml in db.ChiTietLoais
                        on m.id_phim equals ml.id_phim
                        where ml.id_loai == id select m;
            return View(movie.ToPagedList(pageNum, pageSize));
        }

        //Country
        public ActionResult Country()
        {
            var country = from c in db.QuocGias select c;
            return PartialView(country);
        }
        public ActionResult MovieForCountry(int id, int? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);

            var movie = from m in db.Phims where m.id_quoc_gia == id select m;
            return View(movie.ToPagedList(pageNum, pageSize));
        }

        //Release year
        public ActionResult ReleaseYear()
        {
            var releaseYear = from ry in db.NamPhatHanhs select ry;
            return PartialView(releaseYear);
        }
        public ActionResult MovieForReleaseYear(int id, int? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);
            var movie = from m in db.Phims where m.id_nam == id select m;
            return View(movie.ToPagedList(pageNum, pageSize));
        }

        //Search
        public ActionResult Search(String searchMovie, int? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);
      
            var phim = from p in db.Phims
                       where p.ten_phim.Contains(searchMovie)
                       select p;
            return View(phim.ToPagedList(pageNum, pageSize));
        }

        //Banner
        public ActionResult Banner()
        {
            var banner = from b in db.Banners select b;
            return PartialView(banner);
        }


        //Top Moive
        public ActionResult TopMovie()
        {
            var topMovie = layPhimMoi(5);
            return PartialView(topMovie);
        }

        public ActionResult MovieMore()
        {
            var movieMore = layPhimMoi(12);
            return PartialView(movieMore);
        }

        //Cmt
        public ActionResult ShowComment(int id)
        {
            var cmt = from bl in db.BinhLuans where bl.id_phim == id select bl;
            return PartialView(cmt);
        }


        [HttpPost]
        public ActionResult CreateComment(BinhLuan binhLuan)
        {
            DateTime localDate = DateTime.Now;
            var id = binhLuan.id_phim;
            binhLuan.ngay_binh_luan = localDate;
            db.BinhLuans.InsertOnSubmit(binhLuan);
            db.SubmitChanges();
            return RedirectToAction("WatchMovie/"+id);
        }


    }
}