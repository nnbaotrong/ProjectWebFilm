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

        public ActionResult Index()
        {
            var phimMoi = layPhimMoi(12);
            return View(phimMoi);
        }

        public ActionResult Details(int id)
        {
            var phim = from p in db.Phims
                       where p.id_phim == id
                       select p;
            return View(phim.Single());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}