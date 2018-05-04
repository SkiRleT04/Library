using AutoMapper;
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
    public class BrochuresController : Controller
    {
        private BrochureService _brochureService;
        public BrochuresController()
        {
            _brochureService = new BrochureService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            IEnumerable<BrochureViewModel> brochureList = _brochureService.GetAll();
            DataSourceResult result = brochureList.ToDataSourceResult(request);
            return Json(result);
        }



        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, BrochureViewModel brochureVM)
        {
            _brochureService.Create(brochureVM);
            return Json(new[] { brochureVM }.ToDataSourceResult(request, ModelState));

        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, BrochureViewModel brochureVM)
        {
            _brochureService.Update(brochureVM);
            return Json(new[] { brochureVM }.ToDataSourceResult(request, ModelState));
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, BrochureViewModel brochureVM)
        {
            _brochureService.Delete(brochureVM.BrochureId);
            return Json(new[] { brochureVM }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult SaveToXml(string data)
        {
            int[] listId = Array.ConvertAll(data.Split(','), int.Parse);
            BrochureViewModel[] items = _brochureService.GetRange(listId).ToArray();
            byte[] bytesItems;
            Serializer.ObjectToXmlBytes(items, out bytesItems);
            if (bytesItems != null)
            {
                return File(bytesItems, "application/xml", $"TableBrochures.xml");
            }
            return Json(new { Status = "Bad", Message = "File is invalid or corrupt" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveToJson(string data)
        {
            int[] listId = Array.ConvertAll(data.Split(','), int.Parse);
            BrochureViewModel[] items = _brochureService.GetRange(listId).ToArray();
            byte[] bytesItems;
            Serializer.ObjectToJsonBytes(items, out bytesItems);
            if (bytesItems != null)
            {
                return File(bytesItems, "application/json", $"TableBrochures.json");
            }
            return Json(new { Status = "Bad", Message = "File is invalid or corrupt" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadFromFile(HttpPostedFileBase uploadFile)
        {

            var isValideFile = uploadFile != null && uploadFile.ContentLength > 0;
            if (isValideFile)
            {
                BrochureViewModel[] items;
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