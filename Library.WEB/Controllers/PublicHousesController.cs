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
    public class PublicHousesController : Controller
    {
        private PublicHouseService _publicHouseService;
        public PublicHousesController()
        {
            _publicHouseService = new PublicHouseService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            IEnumerable<PublicHouseViewModel> publicHousesList = _publicHouseService.GetAllWithOutRelations();
            DataSourceResult result = publicHousesList.ToDataSourceResult(request);
            return Json(result);
        }


        public ActionResult ReadForDropDownSelect()
        {
            IEnumerable<PublicHouseViewModel> publicHousesList = _publicHouseService.GetAllWithOutRelations();
            return Json(publicHousesList, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles ="admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, PublicHouseViewModel publicHouseVM)
        {
            _publicHouseService.Create(publicHouseVM);
            return Json(new[] { publicHouseVM }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "admin")]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, PublicHouseViewModel publicHouseVM)
        {
            _publicHouseService.Update(publicHouseVM);
            return Json(new[] { publicHouseVM }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "admin")]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, PublicHouseViewModel publicHouseVM)
        {
            _publicHouseService.Delete(publicHouseVM.PublicHouseId);
            return Json(new[] { publicHouseVM }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult SaveToXml(string data)
        {
            int[] listId = Array.ConvertAll(data.Split(','), int.Parse);
            PublicHouseViewModel[] items = _publicHouseService.GetRange(listId).ToArray();
            

            byte[] bytesItems;
            Serializer.ObjectToXmlBytes(items, out bytesItems);
            if (bytesItems != null)
            {
                return File(bytesItems, "application/xml", $"TablePublicHouses.xml");
            }
            return Json(new { Status = "Bad", Message = "File is invalid or corrupt" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveToJson(string data)
        {
            int[] listId = Array.ConvertAll(data.Split(','), int.Parse);
            PublicHouseViewModel[] items = _publicHouseService.GetRange(listId).ToArray();
            

            byte[] bytesItems;
            Serializer.ObjectToJsonBytes(items, out bytesItems);
            if (bytesItems != null)
            {
                return File(bytesItems, "application/json", $"TablePublicHouses.json");
            }
            return Json(new { Status = "Bad", Message = "File is invalid or corrupt" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadFromFile(HttpPostedFileBase uploadFile)
        {

            var isValideFile = uploadFile != null && uploadFile.ContentLength > 0;
            if (isValideFile)
            {
                PublicHouseViewModel[] publicHouse;
                byte[] byteItems = new byte[uploadFile.ContentLength];
                uploadFile.InputStream.Read(byteItems, 0, byteItems.Length);

                string fileExtensions = Path.GetExtension(uploadFile.FileName);

                if (fileExtensions == ".xml")
                {
                    Serializer.BytesXmlToObject(byteItems, out publicHouse);
                }
                else
                {
                    Serializer.BytesJsonToObject(byteItems, out publicHouse);
                }

                if (publicHouse != null)
                {
                    return Json(publicHouse, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Status = "Bad", Message = "Error" }, JsonRequestBehavior.AllowGet);
        }
    }
}