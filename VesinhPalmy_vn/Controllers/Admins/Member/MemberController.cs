using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Data;
using VesinhPalmy_vn.Models;

namespace VesinhPalmy_vn.Controllers.Admins.Member
{
    public class MemberController : Controller
    {
        vesinhpalmy_vn_dbEntities db = new vesinhpalmy_vn_dbEntities();
        #region[MemberIndex]
        public ActionResult MemberIndex(int? page, string currentMemberName)
        {
            if (Request.HttpMethod == "GET")
            {
                if (Session["MemberName"] != null)
                {
                    currentMemberName = Session["MemberName"].ToString();
                    Session["MemberName"] = null;
                }
            }
            else
            {
                page = 1;
            }

            ViewBag.CurrentMemberName = currentMemberName;

            var all = db.sp_Member_GetByAll().OrderBy(p => p.Username).ToList();

            if (!String.IsNullOrEmpty(currentMemberName))
            {
                all = all.Where(p => p.Name.ToUpper().Contains(currentMemberName.ToUpper())).OrderBy(p => p.Username).ToList();
            }

            int pageSize = 25;
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

            return View(all.ToPagedList(pageNumber, pageSize));
        }
        #endregion

        #region[MemberDelete]
        public ActionResult MemberDelete(int id)
        {
            var del = db.Members.First(p => p.Id == id);

            db.Members.Remove(del);
            db.SaveChanges();

            return RedirectToAction("MemberIndex", "Member");

        }
        #endregion

        #region[MemberCreate]
        // GET: /PriceCity/MemberCreate
        [HttpGet]
        public ActionResult MemberCreate()
        {
            ViewBag.Permiss = Permisstion("Adminstrator",  "Manager", "Nhân viên");
            return View();
        }
        public List<SelectListItem> Permisstion(string v1, string v2, string v3)
        {
            List<SelectListItem> sll = new List<SelectListItem>();

            sll.Add(new SelectListItem { Value = "0", Text = v1 });
            sll.Add(new SelectListItem { Value = "1", Text = v2 });
            sll.Add(new SelectListItem { Value = "2", Text = v3 });
            return sll;
        }
        // POST: /PriceCity/ MemberCreate
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MemberCreate(FormCollection collection, VesinhPalmy_vn.Models.Member pc)
        {
            if (Request.Cookies["Username"] != null)
            {
                pc.Name = collection["Name"];
                pc.Username = collection["Username"];
                string pass = collection["Password"].ToString();
                pc.Password = pass;
                pc.Email = collection["Email"];
                pc.Address = collection["Address"];
                pc.Tel = collection["Tel"];
                string per = "0";
                per = collection["Permiss"];
                pc.Permiss = int.Parse(per);
                db.Members.Add(pc);
                db.SaveChanges();
                return RedirectToAction("MemberIndex");
            }
            else
            {
                return Redirect("/Admins/admins");
            }
        }
        #endregion

        #region[Edit]

        [HttpGet]
        public ActionResult MemberEdit(int id)
        {
            var vMember = db.Members.Find(id);
            List<SelectListItem> sll = new List<SelectListItem>();
            if (vMember.Permiss == 0)
            {
                sll.Add(new SelectListItem { Value = "0", Text = "Administrator", Selected = true });
                sll.Add(new SelectListItem { Value = "1", Text = "Nhân viên", Selected = false });
                sll.Add(new SelectListItem { Value = "2", Text = "Manager", Selected = false });
            }
            else if (vMember.Permiss == 1)
            {
                sll.Add(new SelectListItem { Value = "0", Text = "Administrator", Selected = false });
                sll.Add(new SelectListItem { Value = "1", Text = "Manager", Selected = true });
                sll.Add(new SelectListItem { Value = "2", Text = "Nhân viên", Selected = false });
            }
            else
            {
                sll.Add(new SelectListItem { Value = "0", Text = "Administrator", Selected = false });
                sll.Add(new SelectListItem { Value = "1", Text = "Manager", Selected = false });
                sll.Add(new SelectListItem { Value = "2", Text = "Nhân viên", Selected = true });
            }
            ViewBag.Permiss = sll;
            return View(vMember);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MemberEdit(FormCollection collection, VesinhPalmy_vn.Models.Member pc)
        {
            if (Request.Cookies["Username"] != null)
            {
                pc.Name = collection["Name"];
                pc.Username = collection["Username"];
                string pass = collection["Password"].ToString();
                pc.Password = pass;
                pc.Email = collection["Email"];
                pc.Address = collection["Address"];
                pc.Tel = collection["Tel"];
                string per = "0";
                per = collection["Permiss"];
                pc.Permiss = int.Parse(per);
                db.sp_Member_Update(pc.Id, pc.Name, pc.Tel, pc.Email, pc.Address, pc.Image, pc.Username, pc.Password, pc.Permiss);
                db.SaveChanges();
                return RedirectToAction("MemberIndex");
            }
            else
            {
                return Redirect("/Admins/admins");
            }
        }

        #endregion

        #region[Ajax Change Member]

        // AJAX: /PriceCity/ChangeMember
        [HttpPost]
        public ActionResult ChangeMember(int id, string Username, string Password, string Tel, string Email, string Name)
        {
            var results = "";
            var vMember = db.Members.Find(id);
            if (vMember != null)
            {
                if (Username != null)
                {
                    vMember.Username = Username;
                    results = "UserName đã được thay đổi.";
                }
                else if (Password != null)
                {
                    vMember.Password = Password;
                    results = "Mật khẩu đã được thay đổi.";
                }
                else if (Tel != null)
                {
                    vMember.Tel = Tel;
                    results = "Số điện thoại đã được thay đổi.";
                }
                else if (Email != null)
                {
                    vMember.Email = Email;
                    results = "Địa chỉ Email đã được thay đổi.";
                }
                else if (Name != null)
                {
                    vMember.Name = Name;
                    results = "Tên thành viên đã được thay đổi.";
                }
            }
            db.Entry(vMember).State = EntityState.Modified;
            db.SaveChanges();

            return Json(results);
        }

        #endregion

        #region[MultiCommand]
        public ActionResult MultiCommand(FormCollection collection)
        {
            if (Request.Cookies["Username"] != null)
            {
                if (collection["btnDelete"] != null)
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
                                var Del = (from emp in db.Members where emp.Id == id select emp).SingleOrDefault();
                                db.Members.Remove(Del);
                                db.SaveChanges();
                            }
                        }
                    }
                    return RedirectToAction("MemberIndex");
                }
                else if (collection["btnSearch"] != null)
                {
                    if (collection["MemberName"].Length > 0)
                    {
                        Session["MemberName"] = collection["MemberName"];
                    }
                    return RedirectToAction("MemberIndex");
                }
                else
                {
                    return RedirectToAction("MemberIndex");
                }
            }
            else
            {
                return Redirect("/Admins/admins");
            }
        }
        #endregion

        #region[Autocomplete Member Name]
        // Autocomplete for textbox search 
        [HttpGet]
        public ActionResult Autocomplete(string term)
        {
            var MemberName = from p in db.Members
                                 select p.Name;
            string[] items = MemberName.ToArray();

            var filteredItems = items.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
