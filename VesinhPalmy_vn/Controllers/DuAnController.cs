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
    public class DuAnController : Controller
    {
        vesinhpalmy_vn_dbEntities db = new vesinhpalmy_vn_dbEntities();

        public ActionResult Index()
        {
            string menu_name, menu_content, menu_link, menu_image, menu_image_alt = "";
            var menu = db.Menus.Where(m => m.Active == true && m.Name == "Dự án").SingleOrDefault();
            menu_name = menu.Name;
            menu_content = menu.Content + "<br />" + menu.Detail;
            menu_link = menu.Link;
            menu_image = menu.Image;
            menu_image_alt = menu.AltImg;

            #region[Title, Keyword, Deskription]
            if (menu.Title.Length > 0) { ViewBag.tit = menu.Title; } else { ViewBag.tit = menu.Name; }
            if (menu.Description.Length > 0) { ViewBag.des = menu.Description; } else { ViewBag.des = menu.Name; }
            if (menu.Keyword.Length > 0) { ViewBag.key = menu.Keyword; } else { ViewBag.key = menu.Name; }
            #endregion

            //var list = db.News.Where(m => m.Active == true && m.IdGroup == gid).Take(10).OrderByDescending(m => m.Ord).ToList();
            //var list = (from p in db.News where p.Active == true && p.Index == true orderby p.Ord descending select p).Take(8).ToList();
            ViewBag.MenuName = menu_name;
            ViewBag.MenuContent = menu_content;
            ViewBag.MenuLink = menu_link;
            ViewBag.MenuImage = menu_image;
            ViewBag.MenuImageAlt = menu_image_alt;
            return View();
        }    

    }
}
