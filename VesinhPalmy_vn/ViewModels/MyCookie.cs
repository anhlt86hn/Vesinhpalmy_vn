﻿using VesinhPalmy_vn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VesinhPalmy_vn.ViewModels
{
    public class MyCookie
    {
        public static string CookieName { get; set; }
        //public virtual Product Pro { get; set; }  (DAY LA BIEN TAM XOA)
        //public virtual Application App { get; set; }


        //public MyCookie(Application app)
        //{
        //    CookieName = "MyCookie" + app;
        //    App = app;
        //}

       // DAY LA HA`M TA.M XOA'
        //public void SetCookie(Product pro)
        //{
        //    HttpCookie myCookie = HttpContext.Current.Request.Cookies[CookieName] ?? new HttpCookie(CookieName);
        //    myCookie.Values["ProId"] = pro.Id.ToString();
        //    myCookie.Values["Count"] = DateTime.Now.ToString();
        //    myCookie.Expires = DateTime.Now.AddDays(365);
        //    HttpContext.Current.Response.Cookies.Add(myCookie);
        //}

        //public HttpCookie GetCookie()
        //{
        //    HttpCookie myCookie = HttpContext.Current.Request.Cookies[CookieName];
        //    if (myCookie != null)
        //    {
        //        int pro = Convert.ToInt32(myCookie.Values["ProId"]);
        //        Product product = session.Get<Product>(pro);
        //        return product;
        //    }
        //    return null;
        //}
    }
}