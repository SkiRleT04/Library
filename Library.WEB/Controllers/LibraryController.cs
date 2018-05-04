using Library.BLL.Services;
using Library.ViewModels.Models;
using Library.WEB.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BookingAppStore4.WEB.Controllers
{
    public class LibraryController : Controller
    {
        private string _connectionString;
        private LibraryService _libraryService;
        ConnectionCfg cfg = new ConnectionCfg();

        public LibraryController()
        {
            _connectionString = cfg.connection;
            _libraryService = new LibraryService(_connectionString);
        }

        public ActionResult Index()
        {
            List<LibraryViewModel> libraryViews = _libraryService.GetLibrary().ToList();
            return View(libraryViews);
        }

        public JsonResult GetAll()
        {
            List<LibraryViewModel> libraryViews = _libraryService.GetLibrary().ToList();
            JsonResult result = Json(libraryViews, JsonRequestBehavior.AllowGet);
            return result;
        }
    }
}