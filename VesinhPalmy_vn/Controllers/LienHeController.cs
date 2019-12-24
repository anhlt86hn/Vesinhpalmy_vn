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
    public class LienHeController : Controller
    {
        vesinhpalmy_vn_dbEntities db = new vesinhpalmy_vn_dbEntities();
        public ActionResult Index()
        {
            string menu_name = "";
            var menu = db.Menus.Where(m => m.Active == true && m.Name == "Liên hệ").SingleOrDefault();         
            menu_name = menu.Name;
               
            #region[Title, Keyword, Deskription]
            if (menu.Title.Length > 0) { ViewBag.tit = menu.Title; } else { ViewBag.tit = menu.Name; }
            if (menu.Description.Length > 0) { ViewBag.des = menu.Description; } else { ViewBag.des = menu.Name; }
            if (menu.Keyword.Length > 0) { ViewBag.key = menu.Keyword; } else { ViewBag.key = menu.Name; }
            #endregion
            
            string address_img = "";
            string email_img = "";
            string phone_img = "";
            string web_img = "";
                     
            var list = db.Images.Where(m => m.Active == true && m.Position == 200).ToList();

            address_img = list[0].Source;
            email_img = list[1].Source;
            phone_img = list[2].Source;
            web_img = list[3].Source;

            ViewBag.AddressImage = address_img;
            ViewBag.EmailImage = email_img;
            ViewBag.PhoneImage = phone_img;
            ViewBag.WebImage = web_img;
            ViewBag.MenuName = menu_name;
            return View();
        }
    }
}
