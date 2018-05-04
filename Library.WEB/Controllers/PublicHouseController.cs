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
    public class PublicHouseController : Controller
    {
        private string _connectionString;
        private PublicHouseService _publicHouseService;
        ConnectionCfg cfg = new ConnectionCfg();

        public PublicHouseController()
        {
            _connectionString = cfg.connection;
            _publicHouseService = new PublicHouseService(_connectionString);
        }

        public ActionResult Index()
        {
            List<PublicHouseViewModel> publicHouseViews = _publicHouseService.GetAll().ToList();
                return View("Index", publicHouseViews);
        }

        public JsonResult GetAll()
        {
            List<PublicHouseViewModel> publicHouseViews = _publicHouseService.GetAll().ToList();
            return Json(publicHouseViews, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
                return View();
        }

        [HttpPost]
        public ActionResult Create(PublicHouseViewModel publicHouse)
        {
            if (ModelState.IsValid)
            {
                _publicHouseService.Create(publicHouse);
                return RedirectToAction("Index");
            }
            return View(publicHouse);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            PublicHouseViewModel publicHouse = _publicHouseService.Get(id);
            if (publicHouse != null)
            {
                return View("Edit", publicHouse);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(PublicHouseViewModel publicHouse)
        {
            if (ModelState.IsValid)
            {
                _publicHouseService.Update(publicHouse);
                return RedirectToAction("Index");
            }
            return View(publicHouse);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _publicHouseService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}