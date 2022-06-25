using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDacSan.Models;
using PagedList;

namespace BanDacSan.Controllers
{
    public class HomeController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index(int ? page)
        {
            if (page == null)
            {
                page = 1;
            }
            var all_sp = (from tt in data.SanPhams select tt).OrderBy(m => m.IDSanPham);
            var all_vung = from vung in data.Vungs select vung;
            int pageSize = 3;
            int pageNum = page ?? 1;
            return View(all_sp.ToPagedList(pageNum, pageSize));
        }

        public ActionResult ConHang()
        {
            var all_sp = from tt in data.SanPhams select tt;
            var all_vung = from vung in data.Vungs select vung;
            return View(all_sp);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}