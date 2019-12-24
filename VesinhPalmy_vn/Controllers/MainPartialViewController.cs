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
    public class MainPartialViewController : Controller
    {
        //
        // GET: /MainPartialView/
        vesinhpalmy_vn_dbEntities db = new vesinhpalmy_vn_dbEntities();
        #region[Header]       

        //#region[Logo]
        //public ActionResult _Logo()
        //{
        //    string logo = "";
        //    var logo_img = db.Images.Where(l => l.Position == 1 && l.Active == true).Take(1).ToList();
        //    if (logo_img.Count > 0)
        //    {
        //        logo += "<img src=\"" + logo_img[0].Source + "\" alt=\"" + logo_img[0].Alternative + "" + "\" />";
        //    }
        //    ViewBag.Logo = logo;
        //    logo_img.Clear();
        //    logo_img = null;
        //    return PartialView();
        //}
        //#endregion
        
        #region[Pretty Menu]
        public ActionResult _PrettyMenu()
        {
            string menu = "";
            var cat = db.Menus.Where(m => m.Active == true && m.Level.Length == 5 && m.Position == 1).ToList();
            var list = db.Images.Where(l => l.Position == 2 && l.Active==true).Take(5).ToList();
            //var list = db.Images.Where(m => m.Active == true && m.Position == 100).Take(5).ToList();
            for (int i = 0; i < cat.Count; i++)
            {
                if (i == 0)
                {
                    menu += "<span id=\"menu_no_" + (i + 1) + "\" class=\"mmenu_items  mid\">";
                }
                else if (i == 1)
                {
                    menu += "<span id=\"menu_no_" + (i + 1) + "\" class=\"mmenu_items  top\">";
                }
                else if (i == 2)
                {
                    menu += "<span id=\"menu_no_" + (i + 1) + "\" class=\"mmenu_items  behind\">";
                }
                else if (i == 3)
                {
                    menu += "<span id=\"menu_no_" + (i + 1) + "\" class=\"mmenu_items  top\">";
                }
                else if (i == 4)
                {
                    menu += "<span id=\"menu_no_" + (i + 1) + "\" class=\"mmenu_items  mid last\">";
                }


                menu += "<div class=\"mmenu_items_pad\">";
                menu += "<div class=\"mmenu_img\">";
                menu += "<table width=\"100%\" height=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">";

                menu += "<tr><td align=\"center\"><a href=\"" + cat[i].Tag + "\"><img src=\"" + list[i].Source + "\" width=\"206\" height=\"115\" alt=\"" + list[i].Alternative + "\"></a></td></tr>";
                //menu += "<tr><td align=\"center\"><a href=\"" + cat[i].Tag + "\"><img src=\"" + list[i].Source + "\" width=\"206\" height=\"115\" alt=\"" + list[i].Alternative + "\"></a></td></tr>";
                //menu += "<tr><td align=\"center\"><a href=\"" + cat[i].Tag + "\"><img src=\"../uploads/files/menu_" + (i + 1) + ".jpg\" width=\"206\" height=\"115\" alt=\"Menu " + (i + 1) + "\" title=\"menu no." + (i + 1) + "\"></a></td></tr>";
                menu += "</table></div>";
                menu += "<div class=\"mmenu_name\"><a href=\"" + cat[i].Link + "\" title=\"" + cat[i].Title + "\" rel=\"menu\"><h2>" + cat[i].Name + "</h2></a></div></div>";
                menu += "<div class=\"shadow_menu\"></div></span>";
            }
            cat.Clear();
            cat = null;
            ViewBag.PrettyMenu = menu;
            return PartialView();
        }
        #endregion

        #region[Main Menu]
        public ActionResult _MainMenu()
        {
            string menu = "";
            var cat = db.Menus.Where(c => c.Active == true && c.Level.Length == 5).OrderBy(c => c.Ord).ToList();
            for (int i = 0; i < cat.Count; i++)
            {
                List<Menu> menus = db.Menus.ToList();
                List<Menu> catsub = new List<Menu>();
                string levelm = cat[i].Level;
                catsub = db.Menus.Where(m => m.Level.Length == 10 && m.Level.Substring(0, 5) == levelm && m.Active==true).OrderBy(m => m.Level).ToList();
                if (catsub.Count > 0)
                {
                    if ((Request.Url.ToString().IndexOf(cat[i].Link) > 0 && cat[i].Link != "/") || (cat[i].Link == "/"))
                    //if ((Request.Url.ToString().IndexOf(cat[i].Link) > 0 && cat[i].Link != "/") || (cat[i].Link == "/" && (Request.Url.ToString() == "http://localhost:1584/" || Request.Url.ToString() == "http://ilovestyle.vn/")))
                    {
                        if (i == 0)
                        {

                            menu += "<li><a class=\"active\" href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a>";
                            //chuoi += "<li style=\"background: none\"><a class=\"active\" href=\"" + cat[i].Link + "\">" + cat[i].Name + "</a>";
                        }
                        else
                        {

                            menu += "<li><a class=\"active\" href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a>";
                        }
                    }
                    else
                    {
                        if (i == 0)
                        {

                            menu += "<li><a href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a>";
                            //chuoi += "<li style=\"background: none\"><a href=\"" + cat[i].Link + "\">" + cat[i].Name + "</a>";
                        }
                        else
                        {

                            menu += "<li><a href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a>";
                        }
                    }
                    menu += "<ul class=\"sub-menu\">";
                    for (int k = 0; k < catsub.Count; k++)
                    {
                        string levelm10 = catsub[k].Level;
                        List<Menu> catsub10 = new List<Menu>();
                        catsub10 = db.Menus.Where(m => m.Level.Length == 15 && m.Level.Substring(0, 10) == levelm10 && m.Active==true).OrderBy(m => m.Level).ToList();
                        if (catsub10.Count == 0)
                        {
                            menu += "<li><a href=\"" + catsub[k].Link + "\"><span>" + catsub[k].Name + "</span></a></li>";
                        }
                        else
                        {
                            menu += "<li class=\"sub\"><a href=\"" + catsub[k].Link + "\"><span>" + catsub[k].Name + "</span></a>";
                            menu += "<ul>";
                            for (int n = 0; n < catsub10.Count; n++)
                            {
                                string levelm15 = catsub10[n].Level;
                                List<Menu> catsub15 = new List<Menu>();
                                catsub15 = db.Menus.Where(m => m.Level.Length == 20 && m.Level.Substring(0, 15) == levelm15 && m.Active==true).OrderBy(m => m.Level).ToList();
                                if (catsub15.Count == 0)
                                {
                                    menu += "<li><a href=\"" + catsub10[n].Link + "\"><span>" + catsub10[n].Name + "</span></a></li>";
                                }
                                else
                                {
                                    menu += "<li class=\"sub\"><a href=\"" + catsub10[n].Link + "\"><span>" + catsub10[n].Name + "</span></a>";
                                    menu += "<ul>";
                                    for (int m = 0; m < catsub15.Count; m++)
                                    {
                                        menu += "<li><a href=\"" + catsub15[m].Link + "\"><span>" + catsub15[m].Name + "</span></a></li>";
                                    }
                                    menu += "</ul></li>";
                                }
                            }
                            menu += "</ul></li>";
                        }
                    }
                    menu += "</ul></li>";
                }
                else
                {
                    if ((Request.Url.ToString().IndexOf(cat[i].Link) > 0 && cat[i].Link != "/") || (cat[i].Link == "/"))
                    //if ((Request.Url.ToString().IndexOf(cat[i].Link) > 0 && cat[i].Link != "/") || (cat[i].Link == "/" && (Request.Url.ToString() == "http://localhost:1584/" || Request.Url.ToString() == "http://ilovestyle.vn/")))
                    {
                        if (i == 0)
                        {

                            menu += "<li><a class=\"active\" href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a></li>";
                            //chuoi += "<li style=\"background: none\"><a class=\"active\" href=\"" + cat[i].Link + "\">" + cat[i].Name + "</a></li>";
                        }
                        else
                        {

                            menu += "<li><a class=\"active\" href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a></li>";
                        }
                    }
                    else
                    {
                        if (i == 0)
                        {

                            menu += "<li><a href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a></li>";
                            //chuoi += "<li style=\"background: none\"><a href=\"" + cat[i].Link + "\">" + cat[i].Name + "</a></li>";
                        }
                        else
                        {

                            menu += "<li><a href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a></li>";
                        }
                    }
                }
            }
            ViewBag.MainMenu = menu;
            cat.Clear();
            cat = null;
            return PartialView();
        }
#endregion
        #endregion

        #region[Headline]
        public ActionResult _Headline()
        {
            return PartialView();
        }
        #endregion

        #region[Content]
        #region[Image of 1st post - Home Page]
        public ActionResult _ImgPostHome1st()
        {
            string img = "";
            var list = db.Images.Where(l => l.Position == 4 && l.Active == true).OrderByDescending(l => l.Orders).Take(1).ToList();

            if (list.Count > 0)
            {
                img += "<img alt=\"" + list[0].Alternative + "\" src=\"" + list[0].Source + "\" />";
                //mbsi += "<img alt=\"" + sublist[0].Alternative + "\" src=\"" + sublist[0].Source + "\" style=\"width: 112px; height: 68px;\" />";
            }
            ViewBag.ImgPostHome1st = img;
            list.Clear();        
            list = null;     
            return PartialView();
        }
        #endregion

        //#region[Promotion Home]
        //public ActionResult _Promotion()
        //{
        //    string pro = "";
        //    string cat_name = "";
        //    string cat_link = "";
        //    var cat = db.GroupNews.Where(m => m.Active == true && m.Name == "HOT EVENT - Khuyến mại").SingleOrDefault();
        //    int catId = cat.Id;
        //    cat_name = cat.Name;
        //    cat_link = "/" + cat.Tag;

        //    var list = db.Images.Where(m => m.Position == 2 && m.Active == true).OrderByDescending(m => m.Orders).ToList();
        //    //var list = db.Advertises.Where(m => m.Position == 4 && m.Active == true).OrderByDescending(m => m.Orders).ToList();
        //    if (list.Count > 0)
        //    {
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            pro += "<a onmousedown=\"return rwt(this,'advertise'," + (i + 4) + ")\" href=\"" + list[i].Link + "\" target=\"_self\">";
        //            pro += "<img src=\"" + list[i].Source + "\" alt=\"" + list[i].Description + " title=\"" + list[i].Name + "\" /></a>";
        //        }
        //    }
        //    ViewBag.PromotionHome = pro;
        //    ViewBag.CatName = cat_name;
        //    ViewBag.CatLink = cat_link;
        //    list.Clear();
        //    list = null;
        //    return PartialView();
        //}
        //public ActionResult _Promotion()
        //{
        //    string chuoi = "";
        //    string cat_name = "";
        //    string cat_link = "";
        //    var cat = db.GroupNews.Where(m => m.Active == true && m.Name == "HOT EVENT - Khuyến mại").SingleOrDefault();
        //    cat_name = cat.Name;
        //    cat_link = "/" + cat.Tag;
        //    var list = db.Images.Where(m => m.Position == 2 && m.Active == true).ToList();
        //    chuoi += "<div id='rotator'>\n";
        //    chuoi += "<ul>\n";
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        if (i % list.Count == 0)
        //        {
        //            chuoi += "<li class='show'><a href='" + list[i].Link + "' title=\"" + list[i].Name + "\"><img src='" + list[i].Source + "' alt=\"" + list[i].Name + "\"/></a></li>\n";
        //            //chuoi += "<li class='show'><a href='" + list[i].Link + "' title=\"" + list[i].Name + "\"><img src='" + list[i].Image + "' alt=\"" + list[i].Name + "\" rel=\"nofollow\"/></a></li>\n";
        //        }
        //        else
        //        {
        //            chuoi += "<li><a href='" + list[i].Link + "' title=\"" + list[i].Name + "\"><img src='" + list[i].Source + "' alt=\"" + list[i].Name + "\"/></a></li>\n";
        //            //chuoi += "<li><a href='" + list[i].Link + "' title=\"" + list[i].Name + "\"><img src='" + list[i].Image + "' alt=\"" + list[i].Name + "\" rel=\"nofollow\"/></a></li>\n";
        //        }
        //    }
        //    chuoi += "</ul>\n";
        //    chuoi += "</div>\n";
        //    ViewBag.CatName = cat_name;
        //    ViewBag.CatLink = cat_link;
        //    ViewBag.PromotionHome = chuoi;
        //    list.Clear();
        //    list = null;
            
        //    return PartialView();
        //}
        //#endregion

        #region[Main Slide]
        public ActionResult _MainSlide()
        {
            string sl = "";
            var list = db.Images.Where(m => m.Position == 3 && m.Active == true).OrderByDescending(m => m.Orders).ToList();

            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {

                    sl += "<li><a href=\"" + list[i].Link + "\" target=\"_self\"><img src=\"" + list[i].Source + "\" alt=\"" + list[i].Alternative + "" + "\" /></a></li>";
                }
            }
            ViewBag.MainSlide = sl;
            list.Clear();
            list = null;
            return PartialView();
        }
        #endregion

        #region[Promotion Home]
        public ActionResult _PromotionHome()
        {
            string str = "";
            var list = db.Images.Where(m => m.Position == 3 && m.Active == true).OrderBy(m => m.Orders).ToList();     
            str = "<ol class=\"carousel-indicators\">";

            for (int i = list.Count - 1; i >= 0; i--)
           
            {
                if(i==(list.Count-1))
                {
                    str += "<li data-target=\""+list[i].Link+"\" data-slide-to=\"" + i + "\" class=\"active\"></li>";
                }
                else 
                {
                    str += "<li data-target=\""+list[i].Link+"\" data-slide-to=\"" + i + "\"></li>";
                }
            }
            str += "</ol><div class=\"carousel-inner\">";

            for (int i = list.Count - 1; i >= 0; i--)
            {
                if(i==(list.Count-1))
                {
                    str += "<div class=\"item active\"><a href=\"" + list[i].Link + "\" title=\"" + list[i].Name + "\"><img src=\"" + list[i].Source + "\">";
                    str += "<div class=\"carousel-caption\"><h3 style=\"font-size:24px;color:white;font-weight:bold;\">" + list[i].Name + "</h3></div></a></div>";
                }
                else
                {
                    str += "<div class=\"item\"><a href=\"" + list[i].Link + "\" title=\"" + list[i].Name + "\"><img src=\"" + list[i].Source + "\">";
                    str += "<div class=\"carousel-caption\"><h3 style=\"font-size:24px;color:white;font-weight:bold;\">" + list[i].Name + "</h3></div></a></div>";
                }
            }
            str += "</div>";
            ViewBag.PromotionHome = str;
            list.Clear();
            list = null;
            return PartialView();
        }      
        #endregion



        #region[SERVICES]
        public ActionResult _Services()
        {
            string services = "";
            var rootmenu = db.Menus.Where(m => m.Tag == "dich-vu").SingleOrDefault();
            string rootlvl = rootmenu.Level.Substring(0, 5);
            List<Menu> services_list = new List<Menu>();
            services_list = db.Menus.Where(m => m.Active == true && m.Level.Length == 10 && m.Level.Substring(0, 5) == rootlvl).OrderBy(m => m.Level).ToList();
            
            for (int i = 0; i < services_list.Count; i++)
            {
                if (i == 0)
                {
                    services += "";
                }
                else
                {
                    services += "<div class=\"service-home\">";
                    services += "<a title=\""+services_list[i].Title+"\" class=\"a-block\" href=\""+services_list[i].Link+"\">";
                    services += "<img class=\"block-img\" src=\"" + services_list[i].Image + "\" />";
                    services += "<div class=\"txt-holder\"><p><span class=\"txts\">" + services_list[i].Name + "</span>";
                    services+=  "<span class=\"bg\"></span></p></div></a></div>"; 
                 }
            }
           

            ViewBag.HomeContent = services;
            services_list.Clear();
            services_list = null;
            return PartialView();
        }
        #endregion
 
        #region[Activities's Images]
        public ActionResult _ActivitiesImages()
        {
            string activities_image = "";
            string activities_slider = "";
            var list = db.Images.Where(m => m.Position == 12 && m.Active == true).ToList();
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    activities_image += "<img id=\"image" + (i + 1) + "\" width=\"640\" height=\"426\" src=\"" + list[i].Source + "\" alt=\"" + list[i].Alternative + "\" title=\"" + list[i].Name + "\" />";
                    activities_slider += "<a href=\"#image" + (i + 1) + "\">" + (i + 1) + "</a>";
                }
            }
            ViewBag.ActivitiesImage = activities_image;
            ViewBag.ActivitiesSlider = activities_slider;
            list.Clear();
            list = null;
            return PartialView();
        }
        #endregion
 
        #endregion

        #region[Side bar]
       

        #region[Tips Home]
        public ActionResult _Tips20()
        {
            var cat = db.GroupNews.Where(m => m.Active == true && m.Name == "Kiến thức vệ sinh").SingleOrDefault();
            int catId = cat.Id;
            string cat_name = cat.Name;
            string cat_link = "/"+cat.Tag;
            var list = db.News.Where(m => m.Active == true && m.IdGroup == catId).OrderByDescending(m => m.Ord).Take(20).ToList();
            string tips = "";
            tips += "<ul class=\"box-list\">";
            for (int i = 0; i < list.Count; i++)
            {
                if (Request.Url.ToString().IndexOf(list[i].Tag) > 0)
                {
                    tips += "<li class=\"current\"><span class=\"list_active\"></span>";
                    tips += "<a href=\"/kien-thuc/" + list[i].Tag + "\" class=\"current\">";
                    tips += "<span>" + list[i].Name + "</span></a><div class=\"clear\"></div></li>";
                }
                else
                {
                    tips += "<li class=\"last\"><span class=\"list_active\"></span>";
                    tips += "<a href=\"/kien-thuc/" + list[i].Tag + "\" class=\"last\">";
                    tips += "<span>" + list[i].Name + "</span></a><div class=\"clear\"></div></li>";
                }
            }
            tips += "</ul>";
            ViewBag.MenuList = tips;
            ViewBag.CatName = cat_name;
            ViewBag.CatLink = cat_link;
            return PartialView();
        }
        public ActionResult _Tips10()
        {
            var cat = db.GroupNews.Where(m => m.Active == true && m.Name == "Kiến thức vệ sinh").SingleOrDefault();
            int catId = cat.Id;
            string cat_name = cat.Name;
            string cat_link = "/" + cat.Tag;
            var list = db.News.Where(m => m.Active == true && m.IdGroup == catId).OrderByDescending(m=>m.Ord).Take(10).ToList();
            string tips = "";
            tips += "<ul class=\"box-list\">";
            for (int i = 0; i < list.Count; i++)
            {
                if (Request.Url.ToString().IndexOf(list[i].Tag) > 0)
                {
                    tips += "<li class=\"current\"><span class=\"list_active\"></span>";
                    tips += "<a href=\"/kien-thuc/" + list[i].Tag + "\" class=\"current\">";
                    tips += "<span>" + list[i].Name + "</span></a><div class=\"clear\"></div></li>";
                }
                else
                {
                    tips += "<li class=\"last\"><span class=\"list_active\"></span>";
                    tips += "<a href=\"/kien-thuc/" + list[i].Tag + "\" class=\"last\">";
                    tips += "<span>" + list[i].Name + "</span></a><div class=\"clear\"></div></li>";
                }
            }
            tips += "</ul>";
            ViewBag.MenuList = tips;
            ViewBag.CatName = cat_name;
            ViewBag.CatLink = cat_link;
            return PartialView();
        }
        public ActionResult _Tips5()
        {
            var cat = db.GroupNews.Where(m => m.Active == true && m.Name == "Kiến thức vệ sinh").SingleOrDefault();
            int catId = cat.Id;
            string cat_name = cat.Name;
            string cat_link = "/" + cat.Tag;
            var list = db.News.Where(m => m.Active == true && m.IdGroup == catId).OrderByDescending(m => m.Ord).Take(5).ToList();
            string tips = "";
            tips += "<ul class=\"box-list\">";
            for (int i = 0; i < list.Count; i++)
            {
                if (Request.Url.ToString().IndexOf(list[i].Tag) > 0)
                {
                    tips += "<li class=\"current\"><span class=\"list_active\"></span>";
                    tips += "<a href=\"/kien-thuc/" + list[i].Tag + "\" class=\"current\">";
                    tips += "<span>" + list[i].Name + "</span></a><div class=\"clear\"></div></li>";
                }
                else
                {
                    tips += "<li class=\"last\"><span class=\"list_active\"></span>";
                    tips += "<a href=\"/kien-thuc/" + list[i].Tag + "\" class=\"last\">";
                    tips += "<span>" + list[i].Name + "</span></a><div class=\"clear\"></div></li>";
                }
            }
            tips += "</ul>";
            ViewBag.MenuList = tips;
            ViewBag.CatName = cat_name;
            ViewBag.CatLink = cat_link;
            return PartialView();
        }

        #endregion

        #region[Services List]
        public ActionResult _ServicesList()
        {
            var menu = db.Menus.Where(m => m.Active == true && m.Name == "Dịch vụ").SingleOrDefault();
            string cat_name = menu.Name;
            string cat_link = menu.Link;
            string root_menu_lvl = menu.Level.Substring(0, 5);
            var list = db.Menus.Where(m => m.Level.Length == 10 && m.Level.Substring(0, 5) == root_menu_lvl && m.Active==true).OrderBy(m => m.Ord).ToList();
            string str = "";
            str += "<ul class=\"box-list\">";
            for (int i = 0; i < list.Count; i++)
            {
                if (Request.Url.ToString().IndexOf(list[i].Tag) > 0)
                {
                    str += "<li class=\"current\"><span class=\"list_active\"></span>";
                    str += "<a href=\"" + list[i].Link + "\" class=\"current\">";
                    str += "<span>" + list[i].Name + "</span></a><div class=\"clear\"></div></li>";
                }
                else
                {
                    str += "<li class=\"last\"><span class=\"list_active\"></span>";
                    str += "<a href=\"" + list[i].Link + "\" class=\"last\">";
                    str += "<span>" + list[i].Name + "</span></a><div class=\"clear\"></div></li>";
                }
            }
            str += "</ul>";
            ViewBag.CatName = cat_name;
            ViewBag.CatLink = cat_link;
            ViewBag.MenuList = str;
            return PartialView();

        }
        #endregion

        #region[About List]
        public ActionResult _AboutList()
        {
            var menu = db.Menus.Where(m => m.Active == true && m.Name == "Giới thiệu").SingleOrDefault();
            string root_menu_lvl = menu.Level.Substring(0, 5);
            var list = db.Menus.Where(m => m.Level.Length == 10 && m.Level.Substring(0, 5) == root_menu_lvl && m.Active==true).OrderBy(m => m.Ord).ToList();
            string str = "";
            str += "<ul class=\"box-list\">";
            for (int i = 0; i < list.Count; i++)
            {
                if (Request.Url.ToString().IndexOf(list[i].Tag) > 0)
                {
                    str += "<li class=\"current\"><span class=\"list_active\"></span>";
                    str += "<a href=\"" + list[i].Link + "\" class=\"current\">";
                    str += "<span>" + list[i].Name + "</span></a><div class=\"clear\"></div></li>";
                }
                else
                {
                    str += "<li class=\"last\"><span class=\"list_active\"></span>";
                    str += "<a href=\"" + list[i].Link + "\" class=\"last\">";
                    str += "<span>" + list[i].Name + "</span></a><div class=\"clear\"></div></li>";
                }
            }
            str += "</ul>";
            ViewBag.MenuList = str;
            return PartialView();

        }
        #endregion

        #region[Promotion List]
        public ActionResult _PromotionList()
        {
            var cat = db.GroupNews.Where(m => m.Active == true && m.Name == "HOT EVENT - Khuyến mại").SingleOrDefault();
            int catId = cat.Id;
            string cat_name = cat.Name;
            string cat_link = "/" + cat.Tag;
            var list = db.News.Where(m => m.Active == true && m.IdGroup == catId).OrderByDescending(m => m.Ord).Take(5).ToList();
            string tips = "";
            tips += "<ul class=\"box-list\">";
            for (int i = 0; i < list.Count; i++)
            {
                if (Request.Url.ToString().IndexOf(list[i].Tag) > 0)
                {
                    tips += "<li class=\"current\"><span class=\"list_active\"></span>";
                    tips += "<a href=\"/khuyen-mai/" + list[i].Tag + "\" class=\"current\">";
                    tips += "<span>" + list[i].Name + "</span></a><div class=\"clear\"></div></li>";
                }
                else
                {
                    tips += "<li class=\"last\"><span class=\"list_active\"></span>";
                    tips += "<a href=\"/khuyen-mai/" + list[i].Tag + "\" class=\"last\">";
                    tips += "<span>" + list[i].Name + "</span></a><div class=\"clear\"></div></li>";
                }
            }
            tips += "</ul>";
            ViewBag.MenuList = tips;
            ViewBag.CatName = cat_name;
            ViewBag.CatLink = cat_link;
            return PartialView();
        }

        #endregion


        #region[Recruitment List]
        public ActionResult _RecruitmentList()
        {
            var menu = db.Menus.Where(m => m.Active == true && m.Name == "Tuyển dụng").SingleOrDefault();
            string cat_name = menu.Name;
            string cat_link = menu.Link;
            string root_menu_lvl = menu.Level.Substring(0, 5);
            var list = db.Menus.Where(m => m.Level.Length == 10 && m.Level.Substring(0, 5) == root_menu_lvl).OrderBy(m => m.Ord).ToList();
            string str = "";
            str += "<ul class=\"box-list\">";
            for (int i = 0; i < list.Count; i++)
            {
                if (Request.Url.ToString().IndexOf(list[i].Tag) > 0)
                {
                    str += "<li class=\"current\"><span class=\"list_active\"></span>";
                    str += "<a href=\"" + list[i].Link + "\" class=\"current\">";
                    str += "<span>" + list[i].Name + "</span></a><div class=\"clear\"></div></li>";
                }
                else
                {
                    str += "<li class=\"last\"><span class=\"list_active\"></span>";
                    str += "<a href=\"" + list[i].Link + "\" class=\"last\">";
                    str += "<span>" + list[i].Name + "</span></a><div class=\"clear\"></div></li>";
                }
            }
            str += "</ul>";
            ViewBag.CatName = cat_name;
            ViewBag.CatLink = cat_link;
            ViewBag.MenuList = str;
            return PartialView();

        }
        #endregion

        #region[Process List]
        public ActionResult _ProcessList()
        {
            var menu = db.Menus.Where(m => m.Active == true && m.Name == "Quy trình").SingleOrDefault();
            string root_menu_lvl = menu.Level.Substring(0, 5);
            var list = db.Menus.Where(m => m.Level.Length == 10 && m.Level.Substring(0, 5) == root_menu_lvl).OrderBy(m => m.Ord).ToList();
            string str = "";
            str += "<ul class=\"box-list\">";
            for (int i = 0; i < list.Count; i++)
            {
                if (Request.Url.ToString().IndexOf(list[i].Tag) > 0)
                {
                    str += "<li class=\"current\"><span class=\"list_active\"></span>";
                    str += "<a href=\"" + list[i].Link + "\" class=\"current\">";
                    str += "<span>" + list[i].Name + "</span></a><div class=\"clear\"></div></li>";
                }
                else
                {
                    str += "<li class=\"last\"><span class=\"list_active\"></span>";
                    str += "<a href=\"" + list[i].Link + "\" class=\"last\">";
                    str += "<span>" + list[i].Name + "</span></a><div class=\"clear\"></div></li>";
                }
            }
            str += "</ul>";
            ViewBag.MenuList = str;
            return PartialView();

        }
        #endregion

        #endregion

     
    }
}
