using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDacSan.Models;

namespace BanDacSan.Controllers
{
    public class SanPhamController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        // GET: SanPham
        public ActionResult ListSanPham()
        {
            var all_sp = from tt in data.SanPhams select tt;
            return View(all_sp);
        }

        // Chi tiết
        public ActionResult chitietsanpham(int id)
        {
            var ct_sp = data.SanPhams.Where(m => m.IDSanPham == id).First();
            var all_vung = from vung in data.Vungs select vung;
            return View(ct_sp);
        }

        public ActionResult chitietsanphamkh(int id)
        {
            var ct_sp = data.SanPhams.Where(m => m.IDSanPham == id).First();
            var all_vung = from vung in data.Vungs select vung;
            return View(ct_sp);
        }

        // Thêm
        public ActionResult themsanpham()
        {
            return View();
        }
        [HttpPost]
        public ActionResult themsanpham(FormCollection collection, SanPham s)
        {
            var t_idvung = Convert.ToInt32(collection["IDVung"]);
            var t_tensanpham = collection["TenSanPham"];
            var t_giatien = Convert.ToDecimal(collection["GiaTien"]); 
            var t_soluong = Convert.ToInt32(collection["SoLuong"]);
            var t_mota = collection["MoTa"];
            var t_hinhanh = collection["HinhAnh"];
            if (string.IsNullOrEmpty(t_tensanpham))
            {
                ViewData["Error"] = "Không được để trống dữ liệu";
            }
            else
            {
                s.IDVung = t_idvung;
                s.TenSanPham = t_tensanpham;
                s.GiaTien = t_giatien;
                s.SoLuong = t_soluong;
                s.MoTa = t_mota;
                s.HinhAnh = t_hinhanh;

                data.SanPhams.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("ListSanPham");
            }
            return this.themsanpham();
        }

        // Sửa
        public ActionResult chinhsua(int id)
        {
            var E_sp = data.SanPhams.First(m => m.IDSanPham == id);
            return View(E_sp);
        }
        [HttpPost]
        public ActionResult chinhsua(int id, FormCollection collection)
        {
            var E_idsanpham = data.SanPhams.First(m => m.IDSanPham == id);
            var E_idvung = Convert.ToInt32(collection["IDVung"]);
            var E_tensanpham = collection["TenSanPham"];
            var E_hinh = collection["HinhAnh"];
            var E_giaban = Convert.ToDecimal(collection["GiaTien"]);
            var E_soluong = Convert.ToInt32(collection["SoLuong"]);
            E_idsanpham.IDSanPham = id;
            if (string.IsNullOrEmpty(E_tensanpham))
            {
                ViewData["Error"] = "Không được bỏ trống dữ liệu";
            }
            else
            {
                E_idsanpham.IDVung = E_idvung;
                E_idsanpham.TenSanPham = E_tensanpham;
                E_idsanpham.HinhAnh = E_hinh;
                E_idsanpham.GiaTien = E_giaban;
                E_idsanpham.SoLuong = E_soluong;

                UpdateModel(E_idsanpham);
                data.SubmitChanges();
                return RedirectToAction("ListSanPham");
            }
            return this.chinhsua(id);
        }

        // Xóa
        public ActionResult xoa(int id)
        {
            var D_sp = data.SanPhams.First(m => m.IDSanPham == id);
            return View(D_sp);
        }
        [HttpPost]
        public ActionResult xoa(int id, FormCollection collection)
        {
            var D_sp = data.SanPhams.Where(m => m.IDSanPham == id).First();
            data.SanPhams.DeleteOnSubmit(D_sp);
            data.SubmitChanges();
            return RedirectToAction("ListSanPham");
        }

        // Chèn ảnh
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }
    }
}