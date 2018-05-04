using Library.BLL.Services;
using Library.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.ViewModels.Enums;
using Library.WEB.Config;

namespace Library.WEB.Controllers
{
    public class BookController : Controller
    {
        private string _connectionString;
        private BookService _bookService;
        private PublicHouseService _publicHouseService;
        ConnectionCfg cfg = new ConnectionCfg();

        public BookController()
        {
            _connectionString = cfg.connection;
            _bookService = new BookService(_connectionString);
            _publicHouseService = new PublicHouseService(_connectionString);
        }

        public JsonResult GetAll()
        {
            List<BookViewModel> bookViews = _bookService.GetAll().ToList();
            JsonResult result = Json(bookViews, JsonRequestBehavior.AllowGet);
            return result;
        }

        public ActionResult Index()
        {
            List<BookViewModel> bookViews = _bookService.GetAll().ToList();
                return View("Index", bookViews);
        }
        [HttpGet]
        public JsonResult GetAllPublicHouses()
        {
            List<PublicHouseViewModel> pubicHousesViews = _publicHouseService.GetAll().ToList();
            return Json(pubicHousesViews, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
                return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(BookViewModel book)
        {
            if (ModelState.IsValid)
            {
                GetAllPublicHouses();
                _bookService.Create(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

       

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            BookViewModel book = _bookService.Get(id);
            if (book != null)
            {
                return View("Edit", book);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(BookViewModel book)
        {
            if (ModelState.IsValid)
            {
                _bookService.Update(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _bookService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            BookViewModel book = _bookService.Get(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View("Details", book);
        }
    }
}