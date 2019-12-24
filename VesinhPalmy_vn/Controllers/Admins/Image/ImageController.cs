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


namespace VesinhPalmy_vn.Controllers.Admins.Image
{
    public class ImageController : Controller
    {
        vesinhpalmy_vn_dbEntities db = new vesinhpalmy_vn_dbEntities();

        #region[ImageIndex]
        public ActionResult ImageIndex(string sortOrder, string sortName, int? page, string posstionSearch, string sortPos, string getSortNameClass, string getSortOrderClass)
        {
            var all = db.Images.OrderBy(a => a.Position).ToList();

            #region Controls SelectListItem
            List<SelectListItem> sllTar = new List<SelectListItem>();
            List<SelectListItem> sllPos = new List<SelectListItem>();
            sllTar = CreateTarget("_blank", "_self", "_parient", "_top");
            sllPos = CreateSSL("Chưa đặt vị trí", "Logo", "Thumb Menu", "Slide");
            ViewBag.sllTarget = sllTar;
            ViewBag.sllPossition = sllPos;
            #endregion SelectListItem

            int pageSize = 10;

            int pageNumber = (page ?? 1);

            #region GetLastPage
            // begin [get last page]
            if (page != null)
                ViewBag.mPage = (int)page;
            else
                ViewBag.mPage = 1;

            int lastPage = all.Count / pageSize;
            if (all.Count % pageSize > 0)
            {
                lastPage++;
            }
            ViewBag.LastPage = lastPage;

            ViewBag.PageSize = pageSize;
            //end [get last page]
            #endregion GetLastPage

            #region Order
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.SortOrderParm = sortOrder == "ordAsc" ? "ordDesc" : "ordAsc";
            ViewBag.CurrentSortName = sortName;
            ViewBag.SortNameParm = sortName == "nameAsc" ? "nameDesc" : "nameAsc";
            
            switch (sortOrder)
            {
                case "ordDesc":
                    all = db.Images.OrderByDescending(a => a.Orders).ToList();
                    break;
                case "ordAsc":
                    all = db.Images.OrderBy(a => a.Orders).ToList();
                    break;
                default:
                    break;
            }

            switch (sortName)
            {
                case "nameDesc":
                    all = db.Images.OrderByDescending(a => a.Name).ToList();
                    break;
                case "nameAsc":
                    all = db.Images.OrderBy(a => a.Name).ToList();
                    break;
                default:
                    break;
            }
            #endregion Order

            PagedList<VesinhPalmy_vn.Models.Image> adv = (PagedList<VesinhPalmy_vn.Models.Image>)all.ToPagedList(pageNumber, pageSize);

            /// Kiem tra co tim kiem theo vi tri va phan trang theo vi tri khong
            if ((String.IsNullOrEmpty(posstionSearch) || posstionSearch == "") && (sortPos == null))
            {
                if (Request.IsAjaxRequest())
                    return PartialView("_ImageData", adv);
                else
                    return View(adv);
            }
            else /// Truong hop co tim kiem theo vi tri
            {
                int pos;
                /// Kiem tra xem co phan trang khong
                if (sortPos != null) /// Phan trang
                {
                    ViewBag.currentPos = sortPos;                    
                    pos = Convert.ToInt32(sortPos);
                    if (sortName == "nameAsc")
                        all = db.Images.Where(a => a.Position == pos).OrderBy(a => a.Name).ToList();
                    else if (sortName == "nameDesc")
                        all = db.Images.Where(a => a.Position == pos).OrderByDescending(a => a.Name).ToList();
                    else if (sortOrder == "ordDesc")
                        all = db.Images.Where(a => a.Position == pos).OrderByDescending(a => a.Orders).ToList();
                    else if (sortOrder == "ordAsc")
                        all = db.Images.Where(a => a.Position == pos).OrderBy(a => a.Orders).ToList();
                    else
                        all = db.Images.Where(a => a.Position == pos).OrderByDescending(a => a.Id).ToList();
                }
                else /// ko phan trang
                {
                    ViewBag.currentPos = posstionSearch;
                    pos = Convert.ToInt32(posstionSearch);
                    if (sortName == "nameAsc")
                        all = db.Images.Where(a => a.Position == pos).OrderBy(a => a.Name).ToList();
                    else if (sortName == "nameDesc")
                        all = db.Images.Where(a => a.Position == pos).OrderByDescending(a => a.Name).ToList();
                    else if (sortOrder == "ordDesc")
                        all = db.Images.Where(a => a.Position == pos).OrderByDescending(a => a.Orders).ToList();
                    else if (sortOrder == "ordAsc")
                        all = db.Images.Where(a => a.Position == pos).OrderBy(a => a.Orders).ToList();
                    else
                        all = db.Images.Where(a => a.Position == pos).OrderByDescending(a => a.Id).ToList();
                }

                //sllPos[pos].Selected = true;

                adv = (PagedList<VesinhPalmy_vn.Models.Image>)all.ToPagedList(pageNumber, pageSize);

                if (Request.IsAjaxRequest())
                    return PartialView("_ImageData", adv);
                else
                    return View(adv);
            }
        }
        #endregion

        #region[ImageCreate]

        [HttpGet]
        public ActionResult ImageCreate()
        {
            ViewBag.sllPossition = CreateSSL("Chưa đặt vị trí", "Logo", "Thumb Menu", "Slide");
            ViewBag.sllTarget = CreateTarget("_blank", "_self", "_parient", "_top");
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ImageCreate(FormCollection collection, VesinhPalmy_vn.Models.Image image)
        {
            if (Request.Cookies["Username"] != null)
            {
                image.Name = collection["Name"]; 
                image.Source = collection["Picture"];
                image.Width = Convert.ToInt32(collection["Width"]);
                image.Height = Convert.ToInt32(collection["Height"]);
                image.Orders = Convert.ToInt32(collection["Orders"]);
                image.Link = collection["Link"];
                image.Click = Convert.ToInt32(collection["Click"]);
                image.Active = (collection["Active"] == "false") ? false : true;

                image.Position = Convert.ToInt32(collection["sllPossition"]);
                image.Description = collection["Description"];

                if (Convert.ToInt32(collection["sllTarget"]) == 0)
                {
                    image.Target = "_blank";
                }
                else if (Convert.ToInt32(collection["sllTarget"]) == 1)
                {
                    image.Target = "_self";
                }
                else if (Convert.ToInt32(collection["sllTarget"]) == 2)
                {
                    image.Target = "_Parient";
                }
                else if (Convert.ToInt32(collection["sllTarget"]) == 3)
                {
                    image.Target = "_top";
                }
                image.Click = 0;
                db.Images.Add(image);
                db.SaveChanges();
                return RedirectToAction("ImageIndex");
            }
            else
            {
                return Redirect("/Admins/admins");
            }
        }
        #endregion UserModuleCreate

        #region Edit
        [HttpGet]
        public ActionResult ImageEdit(int id)
        {
            var imageEdit = db.Images.SingleOrDefault(m => m.Id == id);
            ViewBag.sllPossition = CreateSSL("Chưa đặt vị trí", "Logo", "Thumb Menu", "Slide");
            ViewBag.sllTarget = CreateTarget("_blank", "_self", "_parient", "_top");

            return View(imageEdit);
        }



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ImageEdit(FormCollection collection, VesinhPalmy_vn.Models.Image image)
        {
            if (Request.Cookies["Username"] != null)
            {
                image.Name = collection["Name"];
                image.Source = collection["Source"];
                image.Height = Convert.ToInt32(collection["Height"]);
                image.Width = Convert.ToInt32(collection["Width"]);
                image.Position = Convert.ToInt32(collection["sllPossition"]);
                image.Link = collection["Link"];
                image.Target = collection["sllTarget"];
                image.Orders = Convert.ToInt32(collection["Orders"]);
                image.Active = (collection["Active"] == "false") ? false : true;
                image.Description = collection["Description"];
                image.Alternative = collection["Alternative"];
                db.Entry(image).State = EntityState.Modified;
                //db.sp_Image_Update(image.Id, image.Name, image.Source, image.Width, image.Height, image.Link, image.Click, image.Target, image.Position, image.Orders, image.Active, image.Description, image.Alternative);
                db.SaveChanges();
                return RedirectToAction("ImageIndex");
            }
            else
            {
                return Redirect("/Admins/admins");
            }
        }

        #endregion Edit

        #region DeleteItem
        public ActionResult ImageDelete(int id, int page, int pagesize)
        {
            if (Request.Cookies["Username"] != null)
            {
                var aDelete = db.Images.Where(m => m.Id == id).SingleOrDefault();

                db.Images.Remove(aDelete);
                db.SaveChanges();

                List<VesinhPalmy_vn.Models.Image> adv = db.Images.ToList();

                if ((adv.Count % pagesize) == 0)
                {
                    if (page > 1)
                    {
                        page--;
                    }
                    else
                    {
                        return RedirectToAction("ImageIndex");
                    }
                }
                return RedirectToAction("ImageIndex", new { page = page });
            }
            else
            {
                return Redirect("/Admins/admins");
            }
        }
        #endregion DeleteItem

        #region[MultiCommand]
        [HttpPost]
        public ActionResult MultiCommand(FormCollection collect)
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
                                var Del = db.Images.Where(sp => sp.Id == id).SingleOrDefault();
                                db.Images.Remove(Del);
                                db.SaveChanges();
                            }
                        }
                    }
                }
                else if (collect["sllPossition"] != null)
                {
                    //string i = collect["sllPossition"];
                    Session["sllPossition"] = collect["sllPossition"];
                    return RedirectToAction("ImageIndex", "Image");
                }
                return RedirectToAction("ImageIndex");
            }
            else
            {
                return Redirect("/Admins/admins");
            }
        }
        #endregion

        #region[ImageActive]
        public ActionResult ImageActive(int id)
        {
            var image = db.Images.Find(id);
            if (image != null)
                image.Active = image.Active == true ? false : true;

            db.Entry(image).State = System.Data.EntityState.Modified;
            db.SaveChanges();
            var result = "Trạng thái kích hoạt đã được thay đổi.";// userModule.Active;
            return Json(result);
        }
        #endregion

        #region[UpdateDirect]
        public ActionResult UpdateDirect(int id, string order, string width, string height, string name, string source)
        {
            var image = db.Images.Find(id);
            var result = string.Empty;
            if (image != null)
            {
                if (order != null)
                {
                    image.Orders = Convert.ToInt32(order);
                    result = "Thứ tự đã được thay đổi.";
                }
                else if (width != null)
                {
                    image.Width = Convert.ToInt32(width);
                    result = "Độ rộng ảnh đã được thay đổi.";
                }
                else if (height != null)
                {
                    image.Height = Convert.ToInt32(height);
                    result = "Chiều cao ảnh đã được thay đổi.";
                }
                else if (name != null)
                {
                    image.Name = name;
                    result = "Tên ảnh đã được thay đổi.";
                }
                else if (source != null)
                {
                    image.Source = source;
                    result = "Ảnh đã được thay đổi.";
                }
                else
                {
                    image.Active = image.Active == true ? false : true;
                    result = "Trạng thái quảng cáo đã được thay đổi.";
                }
            }

            db.Entry(image).State = System.Data.EntityState.Modified;
            db.SaveChanges();
            //var result = "Dữ liệu đã được thay đổi.";// userModule.Active;
            return Json(result);
        }
        #endregion

        #region OtherMethods

        public List<SelectListItem> CreateSSL(string v1, string v2, string v3, string v4)
        {
            List<SelectListItem> sll = new List<SelectListItem>();
            sll.Add(new SelectListItem { Value = "0", Text = v1 });
            sll.Add(new SelectListItem { Value = "1", Text = v2 });
            sll.Add(new SelectListItem { Value = "2", Text = v3 });            
            sll.Add(new SelectListItem { Value = "3", Text = v4 });
            return sll;
        }


        public List<SelectListItem> CreateTarget(string v1, string v2, string v3, string v4)
        {
            List<SelectListItem> sll = new List<SelectListItem>();

            sll.Add(new SelectListItem { Value = "0", Text = v1 });
            sll.Add(new SelectListItem { Value = "1", Text = v2 });
            sll.Add(new SelectListItem { Value = "2", Text = v3 });
            sll.Add(new SelectListItem { Value = "3", Text = v4 });
            return sll;
        }

        

        #region Target
        public List<SelectListItem> Target()
        {
            List<SelectListItem> Target = new List<SelectListItem>();
            Target.Add(new SelectListItem { Value = "0", Text = "_blank" });
            Target.Add(new SelectListItem { Value = "1", Text = "_self" });
            Target.Add(new SelectListItem { Value = "2", Text = "_parient" });
            Target.Add(new SelectListItem { Value = "3", Text = "_top" });
            return Target;
        }
        #endregion Target


        #region Possition
        public List<SelectListItem> Possition()
        {
            List<SelectListItem> Possition = new List<SelectListItem>();
            Possition.Add(new SelectListItem { Value = "0", Text = "Chưa đặt vị trí" });
            Possition.Add(new SelectListItem { Value = "1", Text = "Logo" });
            Possition.Add(new SelectListItem { Value = "2", Text = "Thumb Menu" });
            Possition.Add(new SelectListItem { Value = "3", Text = "Slide" });           
            return Possition;
        }
        #endregion Possition
        #endregion

        #region AddMultileImages

        #region[AddMutilImage]
        public ActionResult AddMultilImage()
        {
            //ViewBag.sllPossition = CreateSSL("Logo", "Quảng cáo trái", "Ảnh Module Hỗ trợ khách hàng", "Ảnh Slide");
            ViewBag.sllPossition = CreateSSL("Chưa đặt vị trí", "Logo", "Thumb Menu", "Slide");
            return View();
        }
        #endregion


        #region[AddMultilImage]
        [HttpPost]
        public ActionResult AddMultilImage(IEnumerable<HttpPostedFileBase> fileImg, FormCollection aForm)
        {
            if (Request.Cookies["Username"] != null)
            {
                int ID;
                var tmp = db.Images.Select(a => (int?)a.Id).Max();
                //if (tmp == null)
                //    tmp = 1;
                foreach (var file in fileImg)
                {
                    if (tmp == null)
                    { ID = 1; tmp = 1; }
                    else
                        ID = db.Images.Select(a => a.Id).Max();

                    if (file.ContentLength > 0)
                    {
                        //var b = (from k in db.ProImages select k.Id).Max();
                        var ab = Request.Files["fileImg"];
                        String FileExtn = System.IO.Path.GetExtension(file.FileName).ToLower();
                        if (!(FileExtn == ".jpg" || FileExtn == ".png" || FileExtn == ".gif"))
                        {
                            ViewBag.error = "Only jpg, gif and png files are allowed!";
                        }
                        else
                        {
                            VesinhPalmy_vn.Models.Image aImage = new VesinhPalmy_vn.Models.Image();
                            var Filename = Path.GetFileName(file.FileName);
                            //List<string> sizeImg = new List<string>();
                            //sizeImg.Add("_huge");
                            //sizeImg.Add("_big");
                            //sizeImg.Add("_noz");
                            //sizeImg.Add("_small");
                            //string co = "";
                            //string kco = "";
                            //for (int i = 0; i < sizeImg.Count; i++)
                            //{
                            //    var a = Filename.LastIndexOf(sizeImg[i]);
                            //    if (a > 0)
                            //    {
                            //        co = Filename.Substring(0, a);
                            //        kco = sizeImg[i];
                            //        break;
                            //    }
                            //}
                            //var fileName = String.Format("{0}" + kco + ".jpg", Guid.NewGuid().ToString());
                            //String imgPath = String.Format("Uploads/{0}{1}", file.FileName, FileExtn);
                            //file.Save(String.Format("{0}{1}", Server.MapPath("~"), imgPath), Img.RawFormat);
                            var path = Path.Combine(Server.MapPath(Url.Content("/Content/Images/")), Filename);
                            file.SaveAs(path);

                            if (Convert.ToInt32(aForm["sllPossition"]) == 0)
                                aImage.Position = 0;
                            else if (Convert.ToInt32(aForm["sllPossition"]) == 1)
                                aImage.Position = 1;
                            else if (Convert.ToInt32(aForm["sllPossition"]) == 2)
                                aImage.Position = 2;
                            else if (Convert.ToInt32(aForm["sllPossition"]) == 3)
                                aImage.Position = 3;
                            aImage.Id = ID + 1;
                            aImage.Name = "Ảnh thứ " + ID;
                            aImage.Orders = 0;
                            aImage.Click = 0;
                            aImage.Target = "_top";
                            aImage.Width = 0;
                            aImage.Height = 0;
                            //aImage.Position = 0;
                            aImage.Source = "/Content/Images/" + Filename;
                            //img.Date = DateTime.Now;
                            db.Images.Add(aImage);
                            db.SaveChanges();
                        }
                    }
                    var fd = file;
                }
                return RedirectToAction("ImageIndex");
            }
            else
            { return Redirect("/admins/admins"); }
        }
        #endregion

        #endregion AddMultileImages

        [HttpPost]
        public ActionResult Search(string searchString, int? page)
        {
            if (Request.Cookies["Username"] != null)
            {
                int pageSize = 4;
                int pageNumber = (page ?? 1);

                PagedList<VesinhPalmy_vn.Models.Image> adv1 = (PagedList<VesinhPalmy_vn.Models.Image>)db.Images.Where(a => a.Name.ToUpper().Contains(searchString.ToUpper())).OrderByDescending(a => a.Name).ToPagedList(pageNumber, pageSize);

                if (adv1.Count > 0)
                    return PartialView("_ImageData", adv1);
                else
                    return PartialView("ErrorSearch");
            }
            else
            {
                return Redirect("/Admins/admins");
            }
        }


        public IView imageEditot { get; set; }
    }
}
