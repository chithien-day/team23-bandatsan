using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace BanDacSan.Models
{
    public class GioHang
    {
        MyDataDataContext data = new MyDataDataContext();

        [Display(Name = "ID Sản Phẩm")]
        public int IDSanPham { get; set; }
        
        [Display(Name = "Tên Sản phẩm")]
        public string TenSanPham { get; set; }

        [Display(Name = "ID Vùng")]
        public int IDVung { get; set; }

        [Display(Name = "Giá bán")]
        public double GiaTien { get; set; }

        [Display(Name = "Số lượng")]
        public int iSoLuong { get; set; }


        [Display(Name = "Thành tiền")]
        public Double dThanhtien
        {
            get { return iSoLuong * GiaTien; }
        }
        public GioHang(int id)
        {
            IDSanPham = id;
            SanPham sanPham = data.SanPhams.Single(n => n.IDSanPham == id);
            TenSanPham = sanPham.TenSanPham;
            GiaTien = double.Parse(sanPham.GiaTien.ToString());
            iSoLuong = 1;
        }
    }
}