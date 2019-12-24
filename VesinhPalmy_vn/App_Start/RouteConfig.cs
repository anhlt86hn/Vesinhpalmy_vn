using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VesinhPalmy_vn
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(name: "Home", url: "", defaults: new { controller = "Home", action = "Index" });
            routes.MapRoute("Menu Dịch vụ", url: "dich-vu", defaults: new { controller = "DichVu", action = "Index" });
            routes.MapRoute("Nhóm Dịch vụ", url: "dich-vu/{tag}", defaults: new { controller = "DichVu", action = "Detail" });
            routes.MapRoute("Menu giới thiệu", url: "gioi-thieu", defaults: new { controller = "GioiThieu", action = "Index" });
            routes.MapRoute("Chi tiết giới thiệu", url: "gioi-thieu/{tag}", defaults: new { controller = "GioiThieu", action = "Detail" });
            routes.MapRoute("Menu tuyển dụng", url: "tuyen-dung", defaults: new { controller = "TuyenDung", action = "Index" });
            routes.MapRoute("Chi tiết tuyển dụng", url: "tuyen-dung/{tag}", defaults: new { controller = "TuyenDung", action = "Detail" });
            routes.MapRoute("Menu Tin tức", url: "tin-tuc", defaults: new { controller = "TinTuc", action = "Index" });
            routes.MapRoute("Menu con của menu Tin tức", url: "tin-tuc/{tag}", defaults: new { controller = "TinTuc", action = "Detail" });
            routes.MapRoute("Menu Kiến thức", url: "kien-thuc", defaults: new { controller = "KienThuc", action = "Index" });
            routes.MapRoute("Menu con của menu Kiến thức vệ sinh", url: "kien-thuc/{tag}", defaults: new { controller = "KienThuc", action = "Detail" });
            routes.MapRoute("Menu HOT EVENT - Khuyến mại", url: "khuyen-mai", defaults: new { controller = "KhuyenMai", action = "Index" });
            routes.MapRoute("Menu con của menu HOT EVENT - Khuyến mại", url: "khuyen-mai/{tag}", defaults: new { controller = "KhuyenMai", action = "Detail" });
            routes.MapRoute("Menu Dự án", "du-an", new { controller = "DuAn", action = "Index" });
            routes.MapRoute("Menu Quy trinh", "quy-trinh", new { controller = "QuyTrinh", action = "Index" });
            routes.MapRoute("Chi tiết quy trinh", url: "quy-trinh/{tag}", defaults: new { controller = "QuyTrinh", action = "Detail" });
            routes.MapRoute("Liên hệ", url: "lien-he", defaults: new { controller = "LienHe", action = "Index" });

            routes.MapRoute("Danh muc Sản phẩm", "danh-muc/{tag}", new { controller = "SanPham", action = "ListCategory", tag = UrlParameter.Optional }, new { controller = "^S.*", action = "^ListCategory$" });
            routes.MapRoute("Danh mục sản phẩm/ danh mục sản phẩm con", url: "danh-muc/{cate}/{tag}", defaults: new { controller = "SanPham", action = "Detail" });
            routes.MapRoute("Menu Sản phẩm", "san-pham/{*catchall}", new { controller = "SanPham", action = "Index", tag = UrlParameter.Optional }, new { controller = "^S.*", action = "^Index$" });
            routes.MapRoute("Danh mục Sản phẩm", "san-pham/{tag}/{*catchall}", new { controller = "SanPham", action = "Albums", tag = UrlParameter.Optional }, new { controller = "^S.*", action = "^Albums$" });
            routes.MapRoute("Danh mục sản phẩm (theo trang)", "san-pham/{tag}/{page}/{*catchall}", new { controller = "SanPham", action = "Albums", tag = UrlParameter.Optional }, new { controller = "^S.*", action = "^Albums$" });
            routes.MapRoute("Detail", "thong-tin/{tag}/{*catchall}", new { controller = "SanPham", action = "Detail", tag = UrlParameter.Optional }, new { controller = "^S.*", action = "^Detail$" });

            routes.MapRoute(name: "menu", url: "{tag}", defaults: new { controller = "DichVu", action = "Detail" });

            #region[Admin]
            routes.MapRoute("Admin", "Admins/admins/{*catchall}", new { controller = "Admins", action = "admins", tag = UrlParameter.Optional }, new { controller = "^A.*", action = "^admins$" });
            routes.MapRoute("Logout", "Admins/Logout/{*catchall}", new { controller = "Admins", action = "Logout", tag = UrlParameter.Optional }, new { controller = "^A.*", action = "^Logout$" });
            routes.MapRoute("AdminDefault", "Admins/Default/{*catchall}", new { controller = "Admins", action = "Default", tag = UrlParameter.Optional }, new { controller = "^A.*", action = "^Default$" });
            #endregion
           
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}