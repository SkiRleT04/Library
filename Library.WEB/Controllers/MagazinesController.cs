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
    public class MagazinesController : Controller
    {
        private MagazineService _magazineService;
        public MagazinesController()
        {
            _magazineService = new MagazineService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            IEnumerable<MagazineViewModel> brochureList = _magazineService.GetAll();
            DataSourceResult result = brochureList.ToDataSourceResult(request);
            return Json(result);
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, MagazineViewModel magazineVM)
        {
            _magazineService.Create(magazineVM);
            return Json(new[] { magazineVM }.ToDataSourceResult(request, ModelState));

        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, MagazineViewModel magazineVM)
        {
            _magazineService.Update(magazineVM);
            return Json(new[] { magazineVM }.ToDataSourceResult(request, ModelState));
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, MagazineViewModel magazineVM)
        {
            _magazineService.Delete(magazineVM.MagazineId);
            return Json(new[] { magazineVM }.ToDataSourceResult(request, ModelState));
        }


        public ActionResult SaveToXml(string data)
        {
            int[] listId = Array.ConvertAll(data.Split(','), int.Parse);
            MagazineViewModel[] items = _magazineService.GetRange(listId).ToArray();
            byte[] bytesItems;
            Serializer.ObjectToXmlBytes(items, out bytesItems);
            if (bytesItems != null)
            {
                return File(bytesItems, "application/xml", $"TableMagazines.xml");
            }
            return Json(new { Status = "Bad", Message = "File is invalid or corrupt" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveToJson(string data)
        {
            int[] listId = Array.ConvertAll(data.Split(','), int.Parse);
            MagazineViewModel[] items = _magazineService.GetRange(listId).ToArray();
            byte[] bytesItems;
            Serializer.ObjectToJsonBytes(items, out bytesItems);
            if (bytesItems != null)
            {
                return File(bytesItems, "application/json", $"TableMagazines.json");
            }
            return Json(new { Status = "Bad", Message = "File is invalid or corrupt" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadFromFile(HttpPostedFileBase uploadFile)
        {

            var isValideFile = uploadFile != null && uploadFile.ContentLength > 0;
            if (isValideFile)
            {
                MagazineViewModel[] items;
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
