using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VesinhPalmy_vn.Controllers
{
    public class ArchiveController : Controller
    {
        //
        // GET: /Archive/

        public ActionResult Index()
        {
            return View();
        }

        //  #region[Tips Home]
        //  public ActionResult _TipsHome()
        //  {
        //      string sl = "";
        //      string g_name = "";
        //      string g_link = "";
        //      var g = db.GroupNews.Where(m => m.Active == true && m.Name == "Kiến thức vệ sinh").SingleOrDefault();
        //      int gid = g.Id;
        //      g_name = g.Name;
        //      g_link = "/" + g.Tag;
        //      var list = db.Images.Where(m => m.Position == 3 && m.Active == true).OrderByDescending(m => m.Orders).ToList();
        //      //var list = db.Advertises.Where(m => m.Position == 4 && m.Active == true).OrderByDescending(m => m.Orders).ToList();
        //      if (list.Count > 0)
        //      {
        //          for (int i = 0; i < list.Count; i++)
        //          {
        //              sl += "<a onmousedown=\"return rwt(this,'advertise'," + (i + 4) + ")\" href=\"" + list[i].Link + "\" target=\"_self\">";
        //              sl += "<img src=\"" + list[i].Source + "\" alt=\"" + list[i].Description + " title=\"" + list[i].Name + "\" /></a>";
        //          }
        //      }
        //      ViewBag.TipsHome = sl;
        //      ViewBag.GroupNewsName = g_name;
        //      ViewBag.GroupNewsLink = g_link;
        //      list.Clear();
        //      list = null;
        //      return PartialView();
        //  }
        //  #endregion

        //#region[Tips]
        //public ActionResult _Tips()
        //{
        //    var g = db.GroupNews.Where(m => m.Active == true && m.Name == "Kiến thức vệ sinh").SingleOrDefault();
        //    var news = db.News.Where(k => k.Active == true && k.IdGroup == 52).Take(10).ToList();
        //    string chuoi = "";
        //    string groupnew_title = "";
        //    groupnew_title = g.Title;
        //    string new_title = "";
        //    for (int i = 0; i < news.Count;i++ )
        //    {
        //        if(Request.Url.ToString().IndexOf(news[i].Tag)>0)
        //        {
        //            new_title = news[i].Title;
        //            chuoi += "<li class=\"current\"><span class=\"list_active\"></span>";
        //            chuoi += "<a href=\"kien-thuc/" + news[i].Tag + "\" class=\"current\">";
        //            chuoi += "<span>" + news[i].Name + "</span></a><div class=\"clear\"></div></li>";
        //        }
        //        else
        //        {
        //            new_title = news[i].Title;
        //            chuoi += "<li class=\"last\"><span class=\"list_active\"></span>";
        //            chuoi += "<a href=\"kien-thuc/" + news[i].Tag + "\" class=\"last\">";
        //            chuoi += "<span>" + news[i].Name + "</span></a><div class=\"clear\"></div></li>";
        //        }
        //    }
        //    ViewBag.TipsTitle = groupnew_title;
        //    ViewBag.TipsNew = chuoi;
        //    news.Clear();
        //    news = null;
        //        return PartialView();
        //}
        //#endregion

        //#region[SERVICES]
        //public ActionResult _Services()
        //{
        //    string services = "";
        //    var rootmenu = db.Menus.Where(m => m.Tag == "dich-vu").SingleOrDefault();
        //    string rootlvl = rootmenu.Level.Substring(0, 5);
        //    List<Menu> services_list = new List<Menu>();
        //    services_list = db.Menus.Where(m => m.Active == true && m.Level.Length == 10 && m.Level.Substring(0, 5) == rootlvl).OrderBy(m => m.Level).ToList();
        //    for (int i = 0; i < services_list.Count; i++)
        //    {
        //        if(i==0)
        //        {
        //            services+="";
        //        }
        //        else
        //        {
        //            services+="<div class=\"service-wrapper\">";
        //            services+="<div class=\"box-title\"><ul><li><a href=\""+services_list[i].Link+"\"><span>"+services_list[i].Name+"</span></a></li></ul></div>";
        //            services += "<div class=\"service-detail\"><div class=\"service-thumbnail\"><img src=\"" + services_list[i].Image + "\" /></div>" + services_list[i].Content+"</div></div>";
        //        }
        //    }

        //    ViewBag.MainContent = services;
        //    services_list.Clear();
        //    services_list = null;
        //    return PartialView();
        //}
        //#endregion
    }
}
