﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Uni_Shop.ModelDBs
{
    public partial class DonDat
    {
        public DonDat()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        public int MaDonDat { get; set; }
        public string NgayDatHang { get; set; }
        public string TrangThai { get; set; }
        public int MaNguoiDung { get; set; }

        public virtual NguoiDung MaNguoiDungNavigation { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
