using Library.BLL.Services;
using Library.DLL.Repositories;
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
    public class BrochureController : Controller
    {
        private string _connectionString;
        private BrochureService _brochureService;
        ConnectionCfg cfg = new ConnectionCfg();

        public BrochureController()
        {
            _connectionString = cfg.connection;
            _brochureService = new BrochureService(_connectionString);
        }


        public ActionResult Index()
        {
            List<BrochureViewModel> brochureViews = _brochureService.GetAll().ToList(); 
             return View("Index", brochureViews);
        }

        public JsonResult GetAll()
        {
            List<BrochureViewModel> brochureViews = _brochureService.GetAll().ToList();
            JsonResult result = Json(brochureViews, JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
                return View();
        }

        [HttpPost]
        public ActionResult Create(BrochureViewModel brochure)
        {
            if (ModelState.IsValid)
            {
                _brochureService.Create(brochure);
                return RedirectToAction("Index");
            }
            return View(brochure);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            BrochureViewModel brochure = _brochureService.Get(id);
            if (brochure != null)
            {
                return View("Edit", brochure);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(BrochureViewModel brochure)
        {
            if (ModelState.IsValid)
            {
                _brochureService.Update(brochure);
                return RedirectToAction("Index");
            }
            return View(brochure);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            _brochureService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}