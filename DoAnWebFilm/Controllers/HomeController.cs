using DoAnWebFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Index()
        {
            var phimMoi = layPhimMoi(12);
            return View(phimMoi);
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
        public ActionResult MovieForType(int id)
        {
            var movie = from m in db.Phims where m.id_phim == id select m;
            return View(movie);
        }

        //Country
        public ActionResult Country()
        {
            var country = from c in db.QuocGias select c;
            return PartialView(country);
        }
        public ActionResult MovieForCountry(int id)
        {
            var movie = from m in db.Phims where m.id_quoc_gia == id select m;
            return View(movie);
        }

        //Release year
        public ActionResult ReleaseYear()
        {
            var releaseYear = from ry in db.NamPhatHanhs select ry;
            return PartialView(releaseYear);
        }
        public ActionResult MovieForReleaseYear(int id)
        {
            var movie = from m in db.Phims where m.id_nam == id select m;
            return View(movie);
        }
    }
}