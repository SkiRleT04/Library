using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Library.BLL.Services;
using Library.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.WEB.Controllers
{
    public class HomeController : Controller
    {
        private LibraryService _libraryService;

        public HomeController()
        {
            _libraryService = new LibraryService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            IEnumerable<CommonItemViewModel> commonItemList = _libraryService.GetAllWithOutRelations();
            DataSourceResult result = commonItemList.ToDataSourceResult(request);
            return Json(result);
        }
        
    }
}