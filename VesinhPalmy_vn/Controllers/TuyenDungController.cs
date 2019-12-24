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
    public class TuyenDungController : Controller
    {
        vesinhpalmy_vn_dbEntities db = new vesinhpalmy_vn_dbEntities();

        public ActionResult Index(int? page)
        {
            if (Request.HttpMethod == "GET")
            {
            }
            else
            {
                page = 1;
            }

            #region[Phân trang]
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            // Thiết lập phân trang
            PagedListRenderOptions ship = new PagedListRenderOptions();

            ship.DisplayLinkToFirstPage = PagedListDisplayMode.Always;
            ship.DisplayLinkToLastPage = PagedListDisplayMode.Always;
            ship.DisplayLinkToPreviousPage = PagedListDisplayMode.Always;
            ship.DisplayLinkToNextPage = PagedListDisplayMode.Always;
            ship.DisplayLinkToIndividualPages = true;
            ship.DisplayPageCountAndCurrentLocation = false;
            ship.MaximumPageNumbersToDisplay = 5;
            ship.DisplayEllipsesWhenNotShowingAllPageNumbers = true;
            ship.EllipsesFormat = "&#8230;";
            ship.LinkToFirstPageFormat = "Trang đầu";
            ship.LinkToPreviousPageFormat = "«";
            ship.LinkToIndividualPageFormat = "{0}";
            ship.LinkToNextPageFormat = "»";
            ship.LinkToLastPageFormat = "Trang cuối";
            ship.PageCountAndCurrentLocationFormat = "Page {0} of {1}.";
            ship.ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.";
            ship.FunctionToDisplayEachPageNumber = null;
            ship.ClassToApplyToFirstListItemInPager = null;
            ship.ClassToApplyToLastListItemInPager = null;
            ship.ContainerDivClasses = new[] { "pagination-container" };
            ship.UlElementClasses = new[] { "pagination" };
            ship.LiElementClasses = Enumerable.Empty<string>();

            ViewBag.ship = ship;
            #endregion

            var menu = db.Menus.Where(m => m.Active == true && m.Name == "Tuyển dụng").SingleOrDefault();
            string menu_name = menu.Name;
            string menu_link = menu.Link;

            string root_menu_lvl = menu.Level.Substring(0, 5);

            #region[Title, Keyword, Deskription]
            if (menu.Title.Length > 0) { ViewBag.tit = menu.Title; } else { ViewBag.tit = menu.Name; }
            if (menu.Description.Length > 0) { ViewBag.des = menu.Description; } else { ViewBag.des = menu.Name; }
            if (menu.Keyword.Length > 0) { ViewBag.key = menu.Keyword; } else { ViewBag.key = menu.Name; }
            #endregion

            var list = db.Menus.Where(m => m.Active == true && m.Level.Length == 10 && m.Level.Substring(0, 5) == root_menu_lvl).OrderBy(m => m.Level).ToList();

            ViewBag.MenuName = menu_name;
            ViewBag.MenuLink = menu_link;
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Detail(string tag)
        {                           
            string root_menu_name = "";
            string root_menu_link = "";                   
            var root_menu = db.Menus.Where(m => m.Active == true && m.Name == "Tuyển dụng").SingleOrDefault();           
            root_menu_name = root_menu.Name;
            root_menu_link = root_menu.Link;

            string menu_name, menu_content, menu_link = "";
            var menu = db.Menus.Where(m => m.Active == true && m.Tag == tag).SingleOrDefault();
            menu_name = menu.Name;
            menu_content = menu.Content + "<br />" + menu.Detail;
            menu_link = menu.Link;
            string menu_image = menu.Image;
            string menu_image_alt = menu.AltImg;

            #region[Title, Keyword, Deskription]
            if (menu.Title.Length > 0) { ViewBag.tit = menu.Title; } else { ViewBag.tit = menu.Name; }
            if (menu.Description.Length > 0) { ViewBag.des = menu.Description; } else { ViewBag.des = menu.Name; }
            if (menu.Keyword.Length > 0) { ViewBag.key = menu.Keyword; } else { ViewBag.key = menu.Name; }
            #endregion

            ViewBag.RootMenuName = root_menu_name;
            ViewBag.RootMenuLink = root_menu_link;
            ViewBag.MenuName = menu_name;
            ViewBag.MenuContent = menu_content;
            ViewBag.MenuLink = menu_link;
            ViewBag.MenuImage = menu_image;
            ViewBag.MenuImageAlt = menu_image_alt;
            return View();
        }    



    }
}
