using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Library.BLL.Services;
using Library.ViewModels.Models;
using Library.WEB.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.WEB.Controllers
{
    public class AuthorsController : Controller
    {

        private AuthorService _authorService;
        public AuthorsController()
        {
            _authorService = new AuthorService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            IEnumerable<AuthorViewModel> authorsList = _authorService.GetAllWithOutRelations();
            DataSourceResult result = authorsList.ToDataSourceResult(request);
            return Json(result);
        }


        public ActionResult ReadForDropDownSelect()
        {
            IEnumerable<AuthorViewModel> authorsList = _authorService.GetAllWithOutRelations();
            return Json(authorsList, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, AuthorViewModel authorVM)
        {
            _authorService.Create(authorVM);
            return Json(new[] { authorVM }.ToDataSourceResult(request, ModelState));
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, AuthorViewModel authorVM)
        {
            _authorService.Update(authorVM);
            return Json(new[] { authorVM }.ToDataSourceResult(request, ModelState));
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, AuthorViewModel authorVM)
        {
            _authorService.Delete(authorVM.AuthorId);
            return Json(new[] { authorVM }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        
        public ActionResult SaveToXml(string data)
        {
            int[] listId = Array.ConvertAll(data.Split(','), int.Parse);
            AuthorViewModel[] items = _authorService.GetRange(listId).ToArray();
            byte[] bytesItems;
            Serializer.ObjectToXmlBytes(items, out bytesItems);
            if (bytesItems != null)
            {
                return File(bytesItems, "application/xml", $"TableAuthors.xml");
            }
            return Json(new { Status = "Bad", Message = "File is invalid or corrupt" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveToJson(string data)
        {
            int[] listId = Array.ConvertAll(data.Split(','), int.Parse);
            AuthorViewModel[] items = _authorService.GetRange(listId).ToArray();
            byte[] bytesItems;
            Serializer.ObjectToJsonBytes(items, out bytesItems);
            if (bytesItems != null)
            {
                return File(bytesItems, "application/json", $"TableAuthors.json");
            }
            return Json(new { Status = "Bad", Message = "File is invalid or corrupt" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadFromFile(HttpPostedFileBase uploadFile)
        {

            var isValideFile = uploadFile != null && uploadFile.ContentLength > 0;
            if (isValideFile)
            {
                AuthorViewModel[] items;
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