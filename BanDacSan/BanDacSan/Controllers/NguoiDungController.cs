using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDacSan.Models;

namespace BanDacSan.Controllers
{
    public class NguoiDungController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        
        // Mã hóa md5
        public static byte[] encryptData(string data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedBytes;
        }
        public static string md5(string data)
        {
            return BitConverter.ToString(encryptData(data)).Replace("-", "").ToLower();
        }

        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(FormCollection collection, NguoiDung nd)
        {
            var hoten = collection["HoTen"];
            var matkhau = md5(collection["MatKhau"]);
            var MatKhauXacNhan = md5(collection["MatKhauXacNhan"]);
            var email = collection["Email"];
            var diachi = collection["DiaChi"];
            var dienthoai = collection["DienThoai"];


            if (String.IsNullOrEmpty(MatKhauXacNhan))
            {
                ViewData["NhapMKXN"] = "Phải nhập mật khẩu xác nhận!";
            }
            else
            {
                if (!matkhau.Equals(MatKhauXacNhan))
                {
                    ViewData["MatKhauGiongNhau"] = "Mật khẩu và mật khẩu xác nhận phải giống nhau";
                }
                else
                {
                    nd.HoTen = hoten;
                    nd.MatKhau = matkhau;
                    nd.Email = email;
                    nd.DiaChi = diachi;
                    nd.DienThoai = dienthoai;
                    data.NguoiDungs.InsertOnSubmit(nd);
                    data.SubmitChanges();

                    return RedirectToAction("DangNhap");
                }
            }
            return this.Dangky();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var email = collection["Email"];
            var matkhau = md5(collection["MatKhau"]);
            NguoiDung nd = data.NguoiDungs.SingleOrDefault(n => n.Email == email && n.MatKhau == matkhau);

            if (nd != null)
            {
                ViewBag.ThongBao = "Đăng nhập thành công!";
                Session["TaiKhoan"] = nd;
            }
            else
            {
                ViewBag.ThongBao = "Sai thông tin đăng nhập";
            }
            return RedirectToAction("Index", "Home");
        }
    }
}