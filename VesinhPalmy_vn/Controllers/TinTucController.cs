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
    public class TinTucController : Controller
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
            ship.LinkToFirstPageFormat = "Đầu";
            ship.LinkToPreviousPageFormat = "«";
            ship.LinkToIndividualPageFormat = "{0}";
            ship.LinkToNextPageFormat = "»";
            ship.LinkToLastPageFormat = "Cuối";
            ship.PageCountAndCurrentLocationFormat = "Page {0} of {1}.";
            ship.ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.";
            ship.FunctionToDisplayEachPageNumber = null;
            ship.ClassToApplyToFirstListItemInPager = null;
            ship.ClassToApplyToLastListItemInPager = null;
            ship.ContainerDivClasses = new[] { "pagination-container" };
            ship.UlElementClasses = new[] { "paginations" };
            ship.LiElementClasses = Enumerable.Empty<string>();
            ViewBag.ship = ship;
            #endregion

            var menu = db.Menus.Where(m => m.Active == true && m.Name == "Tin tức").SingleOrDefault();
            var g = db.GroupNews.Where(m => m.Active == true && m.Name == "Tin tức").SingleOrDefault();
            int gid = g.Id;
            var list = db.News.Where(m => m.Active == true && m.IdGroup == gid).ToList();

            #region[Title, Keyword, Deskription]
            if (menu.Title.Length > 0) { ViewBag.tit = menu.Title; } else { ViewBag.tit = menu.Name; }
            if (menu.Description.Length > 0) { ViewBag.des = menu.Description; } else { ViewBag.des = menu.Name; }
            if (menu.Keyword.Length > 0) { ViewBag.key = menu.Keyword; } else { ViewBag.key = menu.Name; }
            #endregion
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Detail(int? page,string tag)
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
            ship.LinkToFirstPageFormat = "Đầu";
            ship.LinkToPreviousPageFormat = "«";
            ship.LinkToIndividualPageFormat = "{0}";
            ship.LinkToNextPageFormat = "»";
            ship.LinkToLastPageFormat = "Cuối";
            ship.PageCountAndCurrentLocationFormat = "Page {0} of {1}.";
            ship.ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.";
            ship.FunctionToDisplayEachPageNumber = null;
            ship.ClassToApplyToFirstListItemInPager = null;
            ship.ClassToApplyToLastListItemInPager = null;
            ship.ContainerDivClasses = new[] { "pagination-container" };
            ship.UlElementClasses = new[] { "paginations" };
            ship.LiElementClasses = Enumerable.Empty<string>();
            ViewBag.ship = ship;
            #endregion
            var g = db.GroupNews.Where(m => m.Active == true && m.Name == "Tin tức").SingleOrDefault();
            int gid = g.Id;
            var news = db.News.Where(n => n.Active == true && n.Tag == tag).SingleOrDefault();

            var list = db.News.Where(m => m.Active == true && m.IdGroup == gid).ToList();

            #region[Title, Keyword, Deskription]
            if (news.Title.Length > 0) { ViewBag.tit = news.Title; } else { ViewBag.tit = news.Name; }
            if (news.Description.Length > 0) { ViewBag.des = news.Description; } else { ViewBag.des = news.Name; }
            if (news.Keyword.Length > 0) { ViewBag.key = news.Keyword; } else { ViewBag.key = news.Name; }
            #endregion
            return View(list.ToPagedList(pageNumber, pageSize));
        }

    }
}
