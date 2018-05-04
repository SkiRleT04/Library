using Library.BLL.Services;
using Library.ViewModels.Enums;
using Library.ViewModels.Models;
using Library.WEB.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.WEB.Controllers
{
    public class MagazineController : Controller
    {
        private string _connectionString;
        private MagazineService _magazineService;
        ConnectionCfg cfg = new ConnectionCfg();

        public MagazineController()
        {
            _connectionString = cfg.connection;
            _magazineService = new MagazineService(_connectionString);
        }


        public ActionResult Index()
        {
            List<MagazineViewModel> magazineViews = _magazineService.GetAll().ToList();
                return View("Index", magazineViews);
        }

        public JsonResult GetAll()
        {
            List<MagazineViewModel> magazineViews = _magazineService.GetAll().ToList();
            JsonResult result = Json(magazineViews, JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
                return View();
        }

        [HttpPost]
        public ActionResult Create(MagazineViewModel magazine)
        {
            if (ModelState.IsValid)
            {
                _magazineService.Create(magazine);
                return RedirectToAction("Index");
            }
            return View(magazine);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {

            MagazineViewModel magazine = _magazineService.Get(id);
            if (magazine != null)
            {
                return View("Edit", magazine);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(MagazineViewModel magazine)
        {
            if (ModelState.IsValid)
            {
                _magazineService.Update(magazine);
                return RedirectToAction("Index");
            }
            return View(magazine);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _magazineService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}