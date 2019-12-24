using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using System.Data.Linq;
using System.Data.Mapping;
using VesinhPalmy_vn.Models;
using VesinhPalmy_vn.ViewModels;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using System.Net;
using PagedList;
using PagedList.Mvc;


namespace VesinhPalmy_vn.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        vesinhpalmy_vn_dbEntities db = new vesinhpalmy_vn_dbEntities();
        public ActionResult Index()
        {          
            #region[Title, Keyword, Deskription]
            var listconfig = (from p in db.Configs select p).ToList();
            if (listconfig.Count > 0) { ViewBag.tit = listconfig[0].Title; ViewBag.des = listconfig[0].Description; ViewBag.key = listconfig[0].Keyword; }
            listconfig.Clear();
            listconfig = null;
            #endregion          
            return View();       
        }
    }
}
