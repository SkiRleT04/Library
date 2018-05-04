using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Library.BLL.Services;
using Library.ViewModels.Models;
using Library.WEB.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.WEB.Controllers
{
    public class BooksController : Controller
    {
        private BookService _bookService;
        public BooksController()
        {
            _bookService = new BookService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            IEnumerable<BookViewModel> booksList = _bookService.GetAllWithOutRelations();
            DataSourceResult result = booksList.ToDataSourceResult(request);
            return Json(result);
        }


        public ActionResult ReadForDropDownSelect()
        {
            IEnumerable<BookViewModel> booksList = _bookService.GetAllWithOutRelations();
            return Json(booksList, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, BookViewModel bookVM)
        {
            _bookService.Create(bookVM);
            return Json(new[] { bookVM }.ToDataSourceResult(request, ModelState));
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, BookViewModel bookVM)
        {
            _bookService.Update(bookVM);
            return Json(new[] { bookVM }.ToDataSourceResult(request, ModelState));
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, BookViewModel bookVM)
        {
            _bookService.Delete(bookVM.BookId);
            return Json(new[] { bookVM }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }




        public ActionResult SaveToXml(string data)
        {
            int[] listId = Array.ConvertAll(data.Split(','), int.Parse);
            BookViewModel[] items = _bookService.GetRange(listId).ToArray();
            byte[] bytesItems;
            Serializer.ObjectToXmlBytes(items, out bytesItems);
            if (bytesItems != null)
            {
                return File(bytesItems, "application/xml", $"TableBooks.xml");
            }
            return Json(new { Status = "Bad", Message = "File is invalid or corrupt" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveToJson(string data)
        {
            int[] listId = Array.ConvertAll(data.Split(','), int.Parse);
            BookViewModel[] items = _bookService.GetRange(listId).ToArray();
            byte[] bytesItems;
            Serializer.ObjectToJsonBytes(items, out bytesItems);
            if (bytesItems != null)
            {
                return File(bytesItems, "application/json", $"TableBooks.json");
            }
            return Json(new { Status = "Bad", Message = "File is invalid or corrupt" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadFromFile(HttpPostedFileBase uploadFile)
        {

            var isValideFile = uploadFile != null && uploadFile.ContentLength > 0;
            if (isValideFile)
            {
                BookViewModel[] items;
                byte[] byteItems = new byte[uploadFile.ContentLength];
                uploadFile.InputStream.Read(byteItems, 0, byteItems.Length);

                string fileExtensions = Path.GetExtension(uploadFile.FileName);

                if (fileExtensions == ".xml")
                {
                    Serializer.BytesXmlToObject(byteItems, out items);
                }
                else
                {
                    Serializer.BytesJsonToObject(byteItems, out items);
                }

                if (items != null)
                {
                    return Json(items, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Status = "Bad", Message = "Error" }, JsonRequestBehavior.AllowGet);
        }
    }
}
