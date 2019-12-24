using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VesinhPalmy_vn.Models;
using PagedList;
using PagedList.Mvc;
using System.Data.Objects;
using System.Data;
using System.IO;
using VesinhPalmy_vn.ViewModels;

namespace VesinhPalmy_vn.Controllers.Admins.Menu
{
    public class MenuController : Controller
    {
        vesinhpalmy_vn_dbEntities db = new vesinhpalmy_vn_dbEntities();

        #region[MenuIndex]
        public ActionResult MenuIndex(int? page)
        {
            var menu = db.Menus.OrderBy(m => m.Level).ToList();
            int pageSize = 20;

            int pageNumber = (page ?? 1);

            #region GetLastPage
            // begin [get last page]
            if (page != null)
                ViewBag.mPage = (int)page;
            else
                ViewBag.mPage = 1;

            int lastPage = menu.Count / pageSize;
            if (menu.Count % pageSize > 0)
            {
                lastPage++;
            }
            ViewBag.LastPage = lastPage;

            ViewBag.PageSize = pageSize;
            //end [get last page]
            #endregion GetLastPage

            PagedList<VesinhPalmy_vn.Models.Menu> pMenu = (PagedList<VesinhPalmy_vn.Models.Menu>)menu.ToPagedList(pageNumber, pageSize);

            if (Request.IsAjaxRequest())
                return PartialView("_MenuData", pMenu);
            else
                return View(pMenu);
        }
        #endregion

        #region[Create]

        [HttpGet]
        public ActionResult MenuCreate()
        {
            
            var groupnews = db.GroupNews.OrderBy(m => m.Level).ToList();
            
            List<SelectListItem> listpage = new List<SelectListItem>();
            listpage.Clear();
           
            for (int k = 0; k < groupnews.Count; k++)
            {
                listpage.Add(new SelectListItem { Text = "Nhóm tin tức - " + StringClass.ShowNameLevel(groupnews[k].Name, groupnews[k].Level), Value = "/"+ groupnews[k].Tag.ToString() });               
            }
            var menuModel = db.Menus.OrderBy(m => m.Level).ToList();
            
            List<SelectListItem> lstMenu = new List<SelectListItem>();
            lstMenu.Clear();
            for (int i = 0; i < menuModel.Count; i++)
            {
                if (menuModel[i].Level.Length == 5)
                {
                    lstMenu.Add(new SelectListItem { Text = StringClass.ShowNameLevel(menuModel[i].Name, menuModel[i].Level), Value = menuModel[i].Level.ToString() });
                }
                else
                {
                    lstMenu.Add(new SelectListItem { Text = StringClass.ShowNameLevel(menuModel[i].Name, menuModel[i].Level), Value = menuModel[i].Level.ToString() });
                }
            }

            ViewBag.lstMenu = lstMenu;
            ViewBag.LinkModule = listpage;
            ViewBag.LinkDDL = new SelectList(ShowLinkDDL(), "value", "text");
            ViewBag.PageTypeDDL = new SelectList(PageTypeDDL(), "value", "text");  
            ViewBag.Position = new SelectList(ShowPosition(), "value", "text");
            ViewBag.Target = new SelectList(ShowTarget(), "value", "text");
            ViewBag.Type = new SelectList(ShowType(), "value", "text");
            return View();
        }

        [HttpGet]
        public ActionResult CreateSub(string id)
        {         
            var groupnews = db.GroupNews.OrderBy(m => m.Level).ToList();
        
            List<SelectListItem> listpage = new List<SelectListItem>();
            listpage.Clear();
            
            for (int k = 0; k < groupnews.Count; k++)
            {
                listpage.Add(new SelectListItem { Text = "Nhóm tin tức - " + StringClass.ShowNameLevel(groupnews[k].Name, groupnews[k].Level), Value = "/"+groupnews[k].Tag.ToString() });
            }

            var menuModel = db.Menus.OrderBy(m => m.Level).ToList();
            List<SelectListItem> lstMenu = new List<SelectListItem>();
            lstMenu.Clear();
            for (int i = 0; i < menuModel.Count; i++)
            {
                if (menuModel[i].Level == id)
                {
                    lstMenu.Add(new SelectListItem { Text = StringClass.ShowNameLevel(menuModel[i].Name, menuModel[i].Level), Value = menuModel[i].Level.ToString(), Selected = (menuModel[i].Level == id) });
                }
                else
                {
                    lstMenu.Add(new SelectListItem { Text = StringClass.ShowNameLevel(menuModel[i].Name, menuModel[i].Level), Value = menuModel[i].Level.ToString() });
                }
            }
            ViewBag.LinkModule = listpage;
            ViewBag.lstMenu = lstMenu;
            ViewBag.LinkDDL = new SelectList(ShowLinkDDL(), "value", "text");
            ViewBag.PageTypeDDL = new SelectList(PageTypeDDL(), "value", "text");            
            ViewBag.Position = new SelectList(ShowPosition(), "value", "text");
            ViewBag.Target = new SelectList(ShowTarget(), "value", "text");
            ViewBag.Type = new SelectList(ShowType(), "value", "text");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MenuCreate(FormCollection collection, VesinhPalmy_vn.Models.Menu page)
        {
            if (Request.Cookies["Username"] != null)
            {
                page.Name = collection["Name"];
                page.Image = collection["Image"];
                if (collection["PageTypeDDL"] == "0")
                {
                    if (collection["LinkDDL"] == "0")
                    {
                        page.Link = collection["Link"];
                    }
                    else if (collection["LinkDDL"] == "1")
                    {
                        page.Link = collection["LinkModule"] + "/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                    else if (collection["LinkDDL"] == "2")
                    {
                        page.Link = "/gioi-thieu/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                    else if (collection["LinkDDL"] == "3")
                    {
                        page.Link = "/dich-vu/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                    else if (collection["LinkDDL"] == "4")
                    {
                        page.Link = "/tuyen-dung/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                    else if (collection["LinkDDL"] == "5")
                    {
                        page.Link = "/du-an/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                }
                else
                {
                    page.Link = "/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                }

                page.Target = collection["Target"];               
                page.Position = Convert.ToInt32(collection["Position"]);              
                page.Content = collection["Content"];
                page.Detail = collection["Detail"];
                page.Title = collection["Title"];
                page.Description = collection["Description"];
                page.Keyword = collection["Keyword"];
                string le = collection["lstMenu"];
                if (le.Length > 0)
                {
                    page.Level = le + "00000";
                }
                else
                {
                    page.Level = "00000";
                }
                page.Ord = Convert.ToInt32(collection["Order"]);
                page.Active = (collection["Active"] == "false") ? false : true;

                page.Tag = VesinhPalmy_vn.Models.StringClass.NameToTag(collection["Name"]);

                //StringClass.ShowNameLevel(collection["Name"], Model[i].Level);

                db.Menus.Add(page);
                
                db.SaveChanges();
                return RedirectToAction("MenuIndex");
            }
            else
            {
                return RedirectToAction("admins");
            }
        }

        #endregion Create

        #region DeleteItem
        [HttpGet]
        public ActionResult MenuDelete(int id, int page, int pagesize)
        {
            if (Request.Cookies["Username"] != null)
            {
                var mDelete = db.Menus.Where(m => m.Id == id).SingleOrDefault();

                db.Menus.Remove(mDelete);
                db.SaveChanges();

                List<VesinhPalmy_vn.Models.Menu> adv = db.Menus.ToList();

                if ((adv.Count % pagesize) == 0)
                {
                    if (page > 1)
                    {
                        page--;
                    }
                    else
                    {
                        return RedirectToAction("MenuIndex");
                    }
                }
                return RedirectToAction("MenuIndex", new { page = page });
            }
            else
            {
                return Redirect("/Admins/admins");
            }
        }
        #endregion DeleteItem

        #region[UpdateDirect]
        public ActionResult UpdateDirect(int id, string order, string link, string position)
        {
            var menu = db.Menus.Find(id);
            var result = string.Empty;
            if (menu != null)
            {
                if (order != null)
                {
                    menu.Ord = Convert.ToInt32(order);
                    result = "Thứ tự đã được thay đổi.";
                }
                else if (link != null)
                {
                    menu.Link = link;
                    result = "Link Menu đã được thay đổi.";
                }
                
                else if (position != null)
                {
                    menu.Position = Convert.ToInt32(position);
                    result = "Vai trò Menu đã được thay đổi.";
                }              
                else
                {
                    menu.Active = menu.Active == true ? false : true;
                    result = "Trạng thái Menu đã được thay đổi.";
                }
            }

            db.Entry(menu).State = System.Data.EntityState.Modified;
            db.SaveChanges();
            //var result = "Dữ liệu đã được thay đổi.";// userModule.Active;
            return Json(result);
        }
        #endregion

        #region Edit
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MenuEdit(int id)
        {
            if (Request.Cookies["Username"] != null)
            {
                var menu = db.Menus.Find(id);
              
                var groupnews = db.GroupNews.OrderBy(m => m.Level).ToList();
                List<SelectListItem> listpage = new List<SelectListItem>();
                listpage.Clear();
                
                
                for (int k = 0; k < groupnews.Count; k++)
                {
                    listpage.Add(new SelectListItem { Text = "Nhóm tin tức - " + StringClass.ShowNameLevel(groupnews[k].Name, groupnews[k].Level), Value = "/"+groupnews[k].Tag.ToString() });
                    //listpage.Add(new SelectListItem { Text = "Nhóm tin tức - " + StringClass.ShowNameLevel(groupnews[k].Name, groupnews[k].Level), Value = "/tin-tuc/" + groupnews[k].Tag.ToString() });
                }

                var menuList = db.Menus.OrderBy(m => m.Level).ToList();
                foreach (var item in menuList)
                {
                    item.Name = StringClass.ShowNameLevel(item.Name, item.Level);
                }


                if (menu.Level.Length > 5)
                {
                    string t = menu.Level.Substring(0, menu.Level.Length - 5);
                    ViewBag.lstMenu = new SelectList(menuList, "Level", "Name", t);
                }
                else
                {
                    ViewBag.lstMenu = new SelectList(menuList, "Level", "Name");
                }


                //ViewBag.lstMenu = lstMenu;
                ViewBag.LinkModule = listpage;
                ViewBag.LinkDDL = new SelectList(ShowLinkDDL(), "value", "text");
                ViewBag.PageTypeDDL = new SelectList(PageTypeDDL(), "value", "text");              
                ViewBag.Position = new SelectList(ShowPosition(), "value", "text");
                ViewBag.Target = new SelectList(ShowTarget(), "value", "text");
                ViewBag.Type = new SelectList(ShowType(), "value", "text");
                return View(menu);

            }
            else
            {
                return Redirect("/Admins/admins");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MenuEdit(FormCollection collection, VesinhPalmy_vn.Models.Menu menu, int id)
        {
            if (Request.Cookies["Username"] != null)
            {

                var curMenu = db.Menus.Find(id);
                menu.Name =collection["Name"];
                menu.Image = collection["Image"];
                menu.Name = menu.Name.Replace(".", "");
                menu.Ord = Convert.ToInt32(collection["Ord"]);
                
                if (collection["PageTypeDDL"] == "0")
                {
                    if (collection["LinkDDL"] == "0")
                    {
                        menu.Link = collection["Link"];
                    }
                    else if (collection["LinkDDL"] == "1")
                    {
                        menu.Link = collection["LinkModule"] + "/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                    else if (collection["LinkDDL"] == "2")
                    {
                        menu.Link = "/gioi-thieu/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                    else if (collection["LinkDDL"] == "3")
                    {
                        menu.Link = "/dich-vu/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                    else if (collection["LinkDDL"] == "4")
                    {
                        menu.Link = "/tuyen-dung/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                    else if (collection["LinkDDL"] == "5")
                    {
                        menu.Link = "/du-an/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                }
                else
                {
                    menu.Link = "/" + StringClass.NameToTag(collection["Name"].ToString()) + "";                    
                }

                string le = collection["lstMenu"];
                menu.Level = le + "00000";
                menu.Target = collection["Target"];
                
                menu.Position = Convert.ToInt32(collection["Position"]);
              
                menu.Content = collection["Content"];
                menu.Detail = collection["Detail"];
                menu.Title = collection["Title"];
                menu.Description = collection["Description"];
                menu.Keyword = collection["Keyword"];
                menu.Active = (collection["Active"] == "false") ? false : true;
                menu.Tag = VesinhPalmy_vn.Models.StringClass.NameToTag(collection["Name"]);

                //db.sp_Menu_Update(menu.Id, menu.Name, menu.Tag, menu.IdCategory, menu.Content, menu.Detail, menu.Level, menu.Title, menu.Description, menu.Keyword, menu.Target, menu.Link, menu.Ord, menu.Position, menu.Active);
                db.sp_Menu_Update(menu.Id, menu.Name, menu.Tag, menu.Content, menu.Detail, menu.Level, menu.Title, menu.Description, menu.Keyword, menu.Target, menu.Link, menu.Ord, menu.Active, menu.Image, menu.AltImg, menu.Position);
                //UpdateModel(menu);
                //db.Entry(menu).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("MenuIndex");
            }
            else
            {
                return Redirect("/Admins/admins");
            }
        }

        #endregion Edit

        public ActionResult Search(string searchString, int? page)
        {
            if (Request.Cookies["Username"] != null)
            {
                int pageSize = 2;
                int pageNumber = (page ?? 1);

                PagedList<VesinhPalmy_vn.Models.Menu> menu = (PagedList<VesinhPalmy_vn.Models.Menu>)db.Menus.Where(a => a.Name.ToUpper().Contains(searchString.ToUpper())).OrderByDescending(a => a.Level).ToPagedList(pageNumber, pageSize);

                if (menu.Count != 0)
                    return PartialView("_MenuData", menu);
                else
                    return PartialView("ErrorSearch");
            }
            else
            {
                return Redirect("/Admins/admins");
            }
        }

        #region MultipleDelete
        public ActionResult MultileDelete(FormCollection collect)
        {
            if (Request.Cookies["Username"] != null)
            {
                if (collect["btnDeleteAll"] != null)
                {
                    foreach (string key in Request.Form)
                    {
                        var checkbox = "";
                        if (key.StartsWith("chk"))
                        {
                            checkbox = Request.Form["" + key];
                            if (checkbox != "false")
                            {
                                Int32 id = Convert.ToInt32(key.Remove(0, 3));
                                var Del = db.Menus.Where(sp => sp.Id == id).SingleOrDefault();
                                db.Menus.Remove(Del);
                                db.SaveChanges();
                            }
                        }
                    }
                }
                return RedirectToAction("MenuIndex");
            }
            else
            {
                return Redirect("/Admins/admins");
            }
        }
        #endregion MultipleDetele

        #region backup
        //########################3


        #region[MenuIndex_back]
        public ActionResult MenuIndex_back()
        {
            string page = "1";//so phan trang hien tai
            var pagesize = 25;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }
            var all = db.Menus.OrderBy(m => m.Level).ToList();
            var pages = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            //var pages = db.sp_Page_Phantrang(page, pagesize, "", "[Level] asc").ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            if (numOfNews > 0)
            {
                ViewBag.Pager = VesinhPalmy_vn.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            }
            else
            {
                ViewBag.Pager = "";
            }
            return View(pages);
        }
        #endregion


        #region[MenuCreate]
        public ActionResult MenuCreate_back()
        {
           
            var groupnews = db.GroupNews.OrderBy(m => m.Level).ToList();

            List<SelectListItem> listpage = new List<SelectListItem>();
            listpage.Clear();
           
            for (int k = 0; k < groupnews.Count; k++)
            {
                listpage.Add(new SelectListItem { Text = "Nhóm tin tức - " + StringClass.ShowNameLevel(groupnews[k].Name, groupnews[k].Level), Value = "/" + groupnews[k].Tag.ToString() });
            }

            ViewBag.LinkModule = listpage;
            ViewBag.LinkDDL = new SelectList(ShowLinkDDL(), "value", "text");
            
            ViewBag.Position = new SelectList(ShowPosition(), "value", "text");
         
            ViewBag.Target = new SelectList(ShowTarget(), "value", "text");
            ViewBag.Type = new SelectList(ShowType(), "value", "text");
            return View();
        }
        #endregion
        #region[MenuCreate]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MenuCreate_back(FormCollection collection, VesinhPalmy_vn.Models.Menu page)
        {
            if (Request.Cookies["Username"] != null)
            {
                page.Name = collection["Name"];
                if (collection["PageTypeDDL"] == "0")
                {
                    if (collection["LinkDDL"] == "0")
                    {
                        page.Link = collection["Link"];
                    }
                    else if (collection["LinkDDL"] == "1")
                    {
                        page.Link = collection["LinkModule"] + "/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                    else if (collection["LinkDDL"] == "2")
                    {
                        page.Link = "/gioi-thieu/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                    else if (collection["LinkDDL"] == "3")
                    {
                        page.Link = "/dich-vu/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                    else if (collection["LinkDDL"] == "4")
                    {
                        page.Link = "/tuyen-dung/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                    else if (collection["LinkDDL"] == "5")
                    {
                        page.Link = "/du-an/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                }
                else
                {
                    page.Link = "/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                }

                page.Target = collection["Target"];                
                page.Position = Convert.ToInt32(collection["Position"]);                
                page.Content = collection["Content"];
                page.Detail = collection["Detail"];
                page.Title = collection["Title"];
                page.Description = collection["Description"];
                page.Keyword = collection["Keyword"];
                page.Ord = Convert.ToInt32(collection["Ord"]);
                page.Active = (collection["Active"] == "false") ? false : true;
                page.Tag = VesinhPalmy_vn.Models.StringClass.NameToTag(collection["Name"]);
                db.Menus.Add(page);
                db.SaveChanges();
                return RedirectToAction("MenuIndex");
            }
            else
            {
                return RedirectToAction("admins");
            }
        }
        #endregion
        #region[MenuAddSub]
        public ActionResult MenuAddSub()
        {
         
            var groupnews = db.GroupNews.OrderBy(m => m.Level).ToList();

            List<SelectListItem> listpage = new List<SelectListItem>();
            listpage.Clear();
           
            for (int k = 0; k < groupnews.Count; k++)
            {
                listpage.Add(new SelectListItem { Text = "Nhóm tin tức - " + StringClass.ShowNameLevel(groupnews[k].Name, groupnews[k].Level), Value = "/" + groupnews[k].Tag.ToString() });
            }

            ViewBag.LinkModule = listpage;
            
            ViewBag.Position = new SelectList(ShowPosition(), "value", "text");
            ViewBag.Target = new SelectList(ShowTarget(), "value", "text");
            ViewBag.Type = new SelectList(ShowType(), "value", "text");
            ViewBag.LinkDDL = new SelectList(ShowLinkDDL(), "value", "text");
            return View();
        }
        #endregion
        #region[MenuAddSub]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MenuAddSub(FormCollection collection, VesinhPalmy_vn.Models.Menu page, string Level)
        {
            if (Request.Cookies["Username"] != null)
            {
                page.Name = collection["Name"];
                if (collection["PageTypeDDL"] == "0")
                {
                    if (collection["LinkDDL"] == "0")
                    {
                        page.Link = collection["Link"];
                    }
                    else if (collection["LinkDDL"] == "1")
                    {
                        page.Link = collection["LinkModule"] + "/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                    else if (collection["LinkDDL"] == "2")
                    {
                        page.Link = "/gioi-thieu/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                    else if (collection["LinkDDL"] == "3")
                    {
                        page.Link = "/dich-vu/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                    else if (collection["LinkDDL"] == "4")
                    {
                        page.Link = "/tuyen-dung/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                    else if (collection["LinkDDL"] == "5")
                    {
                        page.Link = "/du-an/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                }
                else
                {
                    page.Link = "/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                }
                page.Target = collection["Target"];
                
                page.Position = Convert.ToInt32(collection["Position"]);
               
                page.Content = collection["Content"];
                page.Detail = collection["Detail"];
                page.Title = collection["Title"];
                page.Description = collection["Description"];
                page.Keyword = collection["Keyword"];
                page.Ord = Convert.ToInt32(collection["Ord"]);
                page.Active = (collection["Active"] == "false") ? false : true;
                page.Tag = VesinhPalmy_vn.Models.StringClass.NameToTag(collection["Name"]);
                page.Level = Level + "00000";
                db.Menus.Add(page);
                db.SaveChanges();
                return RedirectToAction("MenuIndex");
            }
            else
            {
                return RedirectToAction("admins");
            }
        }
        #endregion
        #region[MenuEdit]
        public ActionResult MenuEdit_back(int id)
        {
            var Edit = db.Menus.First(m => m.Id == id);
            ViewBag.LinkModule = new SelectList(showlistPagerodule(), "value", "text", Edit.Link);
            ViewBag.LinkDDL = new SelectList(ShowLinkDDL(), "value", "text", 1);
            ViewBag.Position = new SelectList(ShowPosition(), "value", "text", Edit.Position);
            
            ViewBag.Target = new SelectList(ShowTarget(), "value", "text", Edit.Target);
            //Level = Edit.Level;
            return View(Edit);
        }
        #endregion
        #region[MenuEdit]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MenuEdit_back(int id, FormCollection collection)
        {
            if (Request.Cookies["Username"] != null)
            {
                var page = db.Menus.First(m => m.Id == id);
                page.Name = collection["Name"];
                if (collection["PageTypeDDL"] == "0")
                {
                    if (collection["LinkDDL"] == "0")
                    {
                        page.Link = collection["Link"];
                    }
                    else if (collection["LinkDDL"] == "1")
                    {
                        page.Link = collection["LinkModule"] + "/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                    else if (collection["LinkDDL"] == "2")
                    {
                        page.Link = "/gioi-thieu/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                    else if (collection["LinkDDL"] == "3")
                    {
                        page.Link = "/dich-vu/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                    else if (collection["LinkDDL"] == "4")
                    {
                        page.Link = "/tuyen-dung/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                    else if (collection["LinkDDL"] == "5")
                    {
                        page.Link = "/du-an/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                }
                else
                {
                    page.Link = "/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                }
                page.Target = collection["Target"];
                page.Position = Convert.ToInt32(collection["Position"]);
                
                page.Content = collection["Content"];
                page.Detail = collection["Detail"];
                page.Title = collection["Title"];
                page.Description = collection["Description"];
                page.Keyword = collection["Keyword"];
                page.Ord = Convert.ToInt32(collection["Ord"]);
                page.Active = (collection["Active"] == "false") ? false : true;
                //var str = Level;
                //page.Level = Level;
                page.Tag = VesinhPalmy_vn.Models.StringClass.NameToTag(collection["Name"]);
                db.SaveChanges();
                return RedirectToAction("MenuIndex");
            }
            else
            {
                return RedirectToAction("admins");
            }
        }
        #endregion
        #region[MenuActive]
        public ActionResult MenuActive(int id)
        {
            if (Request.Cookies["Username"] != null)
            {
                var act = db.Menus.First(m => m.Id == id);
                if (act.Active == true)
                {
                    act.Active = false;
                }
                else
                {
                    act.Active = true;
                }
                db.SaveChanges();
                return RedirectToAction("MenuIndex");
            }
            else
            {
                return RedirectToAction("admins");
            }
        }
        #endregion
        #region[MenuDelete]
        public ActionResult MenuDelete(int id)
        {
            if (Request.Cookies["Username"] != null)
            {
                var del = db.Menus.First(m => m.Id == id);
                db.Menus.Remove(del);
                db.SaveChanges();
                return RedirectToAction("MenuIndex");
            }
            else
            {
                return RedirectToAction("admins");
            }
        }
        #endregion
        #region[MultiDelete]
        public ActionResult MultiDelete()
        {
            if (Request.Cookies["Username"] != null)
            {
                string str = "";
                foreach (string key in Request.Form)
                {
                    var checkbox = "";
                    if (key.StartsWith("chk"))
                    {
                        checkbox = Request.Form["" + key];
                        if (checkbox != "false")
                        {
                            Int32 id = Convert.ToInt32(key.Remove(0, 3));
                            var Del = (from emp in db.Menus where emp.Id == id select emp).SingleOrDefault();
                            db.Menus.Remove(Del);
                            str += id.ToString() + ",";
                            db.SaveChanges();
                        }
                    }
                }
                return RedirectToAction("MenuIndex");
            }
            else
            {
                return RedirectToAction("admins");
            }
        }
        #endregion
        #region[listDropDownList]
        private List<SelectListItem> listDropDownList()
        {
            List<SelectListItem> listpage = new List<SelectListItem>();
            listpage.Clear();

            var list = db.Menus.OrderBy(m => m.Level).ToList();
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    listpage.Add(new SelectListItem { Text = StringClass.ShowNameLevel(list[i].Name, list[i].Level), Value = "/sanpham/sp/" + StringClass.NameToTag(list[i].Name) + "/" });
                }
            }
            //listpage.Add(new SelectListItem { Text = "Tài liệu", Value = "/thu-vien/" });
            //listpage.Add(new SelectListItem { Text = "Liên hệ", Value = "/hoc-cung-doanh-nghiep/" });
            //listpage.Add(new SelectListItem { Text = "Đăng ký Online", Value = "/dang-ky-online/" });
            return listpage;
        }
        #endregion
        #region[Hien thi Dropdownlist - Module]
        public static List<DropDownList> showlistPagerodule()
        {
            vesinhpalmy_vn_dbEntities db = new vesinhpalmy_vn_dbEntities();
            var listg = db.Menus.OrderByDescending(m => m.Level).ToList();
            List<DropDownList> list = new List<DropDownList>();
            //list.Add(new DropDownList { value = "", text = "Trang chủ" });
            if (listg.Count > 0)
            {
                for (int i = 0; i < listg.Count; i++)
                {
                    list.Add(new DropDownList { value = listg[i].Link, text = StringClass.ShowNameLevel(listg[i].Name, listg[i].Level) });
                }

            }
            //list.Add(new DropDownList { value = "/thu-vien/", text = "Tài liệu" });
            //list.Add(new DropDownList { value = "/hoc-cung-doanh-nghiep/", text = "Liên hệ" });
            //list.Add(new DropDownList { value = "/dang-ky-online/", text = "Đăng ký Online" });

            return list;
        }
        #endregion


        #region[Hien thi Dropdownlist - Link]
        public static List<DropDownList> ShowLinkDDL()
        {
            List<DropDownList> list = new List<DropDownList>();
            list.Add(new DropDownList { value = "0", text = "Nhập liên kết" });
            list.Add(new DropDownList { value = "1", text = "Liên kết nhóm \"Tin tức\"" });
            list.Add(new DropDownList { value = "2", text = "Liên kết \"Giới thiệu\"" }); 
            list.Add(new DropDownList { value = "3", text = "Liên kết \"Dịch vụ\"" });
            list.Add(new DropDownList { value = "4", text = "Liên kết \"Tuyển dụng\"" });
            list.Add(new DropDownList { value = "5", text = "Liên kết \"Dự án\"" });
            return list;
        }
        #endregion

        #region[Dropdownlist - Kiểu trang]
        public static List<DropDownList> PageTypeDDL()
        {
            List<DropDownList> list = new List<DropDownList>();
            list.Add(new DropDownList { value = "0", text = "Trang liên kết" });
            list.Add(new DropDownList { value = "1", text = "Trang nội dung" });
            return list;
        }
        #endregion
    
        #region[Hien thi Dropdownlist - Position]
        public static List<DropDownList> ShowPosition()
        {
            List<DropDownList> list = new List<DropDownList>();
            list.Add(new DropDownList { value = "1", text = "Menu chung" });
            list.Add(new DropDownList { value = "2", text = "Menu phụ" });
            list.Add(new DropDownList { value = "3", text = "Menu con" });
            return list;
        }
        #endregion

        #region[Hien thi Dropdownlist - Place]
        public static List<DropDownList> ShowPlace()
        {
            List<DropDownList> list = new List<DropDownList>();
            list.Add(new DropDownList { value = "1", text = "2 Layout" });
            list.Add(new DropDownList { value = "2", text = "Chỉ 1 Layout của menu đó" });
            
           
            return list;
        }
        #endregion


        #region[Hien thi Dropdownlist - Target]
        public static List<DropDownList> ShowTarget()
        {
            List<DropDownList> list = new List<DropDownList>();
            list.Add(new DropDownList { value = "_self", text = "_self" });
            list.Add(new DropDownList { value = "_blank", text = "_blank" });
            return list;
        }
        #endregion


        #region[Hien thi Dropdownlist - Type]
        public static List<DropDownList> ShowType()
        {
            List<DropDownList> list = new List<DropDownList>();
            list.Add(new DropDownList { value = "0", text = "Trang nội dung" });
            list.Add(new DropDownList { value = "1", text = "Trang liên kết" });
            return list;
        }
        #endregion

        //##########################3
        #endregion



    }
}
