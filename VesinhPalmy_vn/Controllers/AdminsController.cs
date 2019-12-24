using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using VesinhPalmy_vn.Models;


namespace VesinhPalmy_vn.Controllers
{
    public class AdminsController : Controller
    {

        vesinhpalmy_vn_dbEntities db = new vesinhpalmy_vn_dbEntities();
        #region[Default]
        public ActionResult Default()
        {
            if (Request.Cookies["Username"] != null)
            {
                //string a = Request.Cookies["Username"].Values["Permiss"].ToString();
                return View();
            }
            else
            {
                return RedirectToAction("admins");
            }
        }
        #endregion
        #region[ConfigEdit]
        public ActionResult ConfigEdit()
        {
            var config = db.Configs.FirstOrDefault();
            if (config != null)
            {
                return View(config);
            }
            return View();
        }
        #endregion

        


        #region[ConfigEdit]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ConfigEdit(FormCollection collection)
        {

            if (Request.Cookies["Username"] != null)
            {
                var config = db.Configs.FirstOrDefault();

                var Mail_Smtp = collection["Mail_Smtp"];
                var Mail_Port = collection["Mail_Port"];
                var Mail_Info = collection["Mail_Info"];
                var Mail_Noreply = collection["Mail_Noreply"];
                var Mail_Password = collection["Mail_Password"];

                var Copyright = collection["Copyright"];
                var Thanhtoan = collection["Thanhtoan"];
                var Canhbao = collection["Canhbao"];
                var Title = collection["Title"];
                var Description = collection["Description"];
                var Keyword = collection["Keyword"];

                if (config != null)
                {
                    config.Mail_Smtp = Mail_Smtp;
                    config.Mail_Port = short.Parse(Mail_Port);
                    config.Mail_Info = Mail_Info;
                    config.Mail_Noreply = Mail_Noreply;
                    config.Mail_Password = Mail_Password;
                    config.Copyright = Copyright;
                    config.Title = Title;
                    config.Description = Description;
                    config.Keyword = Keyword;        
                    UpdateModel(config);
                    db.SaveChanges();
                    return RedirectToAction("ConfigEditot", "Admins");
                }
                else
                {
                    config = new Config();
                    config.Mail_Smtp = Mail_Smtp;
                    config.Mail_Port = short.Parse(Mail_Port);
                    config.Mail_Info = Mail_Info;
                    config.Mail_Noreply = Mail_Noreply;
                    config.Mail_Password = Mail_Password;
                    config.Copyright = Copyright;
                    config.Title = Title;
                    config.Description = Description;
                    config.Keyword = Keyword;

                    db.Entry(config).State = EntityState.Added;
                    db.SaveChanges();
                    return RedirectToAction("Default", "Admins");
                }
                
            }
            else
            {
                return RedirectToAction("admins");
            }
        }
        #endregion

        #region[ConfigEditOt]
        public ActionResult ConfigEditot()
        {
            var config = db.Configs.FirstOrDefault();
            if (config != null)
            {
                return View(config);
            }
            return View();
        }
        #endregion

        #region[ConfigEditot]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ConfigEditot(FormCollection collection)
        {

            if (Request.Cookies["Username"] != null)
            {
                var config = db.Configs.FirstOrDefault();

                var Contact = collection["Contact"];
                //var Mail_Port = collection["Mail_Port"];
                //var Mail_Info = collection["Mail_Info"];
                //var Mail_Noreply = collection["Mail_Noreply"];
                //var Mail_Password = collection["Mail_Password"];

                var Copyright = collection["Copyright"];
                var Thanhtoan = collection["Thanhtoan"];
                var Canhbao = collection["Canhbao"];
                //var GoogleMap = collection["GoogleMap"];
                //var Video = collection["Video"];

                var Title = collection["Title"];
                var Description = collection["Description"];
                var Keyword = collection["Keyword"];

                if (config != null)
                {
                    config.Contact = Contact;
                    //config.Mail_Port = short.Parse(Mail_Port);
                    //config.Mail_Info = Mail_Info;
                    //config.Mail_Noreply = Mail_Noreply;
                    //config.Mail_Password = Mail_Password;
                    config.Copyright = Copyright;
                    config.Thanhtoan = Thanhtoan;
                    config.Canhbao = Canhbao;
                    //config.GoogleMap = GoogleMap;
                    //config.Video = Video;

                    config.Title = Title;
                    config.Description = Description;
                    config.Keyword = Keyword;
                    UpdateModel(config);
                    db.SaveChanges();
                    return RedirectToAction("ConfigEditot", "Admins");
                }
                else
                {
                    config = new Config();
                    config.Contact = Contact;
                    //config.Mail_Port = short.Parse(Mail_Port);
                    //config.Mail_Info = Mail_Info;
                    //config.Mail_Noreply = Mail_Noreply;
                    //config.Mail_Password = Mail_Password;
                    config.Copyright = Copyright;
                    config.Thanhtoan = Thanhtoan;
                    config.Canhbao = Canhbao;
                    config.Title = Title;
                    config.Description = Description;
                    config.Keyword = Keyword;

                    db.Entry(config).State = EntityState.Added;
                    db.SaveChanges();
                    return RedirectToAction("ConfigEditot", "Admins");
                }

            }
            else
            {
                return RedirectToAction("admins");
            }
        }
        #endregion

        #region[Logon]
        public ActionResult admins()
        {
            if (Request.Cookies["Username"] != null)
            {
                HttpCookie UserCookie = new HttpCookie("Username", "");
                UserCookie.Expires = DateTime.Now;
                Response.Cookies.Add(UserCookie);
            }
            return View();
        }
        #endregion
        #region[Logon]
        [HttpPost]
        public ActionResult admins(FormCollection collect)
        {
            var user = collect["Username"];
            var pass = collect["Pass"];
            var list = db.Members.Where(n => n.Username == user && n.Password==pass).ToList();
            if (list.Count>0)
            {

                HttpCookie UserCookie = new HttpCookie("Username");
                UserCookie.Values["UserNameText"] = user.ToString();
                UserCookie.Values["Permiss"] = list[0].Permiss.ToString();
                UserCookie.Values["PasswordText"] = pass.ToString();
                UserCookie.Values["FullName"] = Server.UrlEncode(list[0].Name.Trim());
                UserCookie.Expires = DateTime.Now.AddHours(2);
                Response.Cookies.Add(UserCookie);

                return RedirectToAction("Default", "Admins");
            }
            else
            {
                ViewBag.Err = "Đăng nhập không thành công!";
                return View();
            }
        }
        #endregion
        #region[Logon]
        public ActionResult Logout()
        {
            var cookie = new HttpCookie("Username");
            cookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(cookie);
            return RedirectToAction("admins");
        }
        #endregion
    }
}
