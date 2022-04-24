using System;
using System.Collections.Generic;

#nullable disable

namespace Uni_Shop.ModelDBs
{
    public partial class TaiKhoan
    {
        public TaiKhoan()
        {
            NguoiDungs = new HashSet<NguoiDung>();
            NhanViens = new HashSet<NhanVien>();
        }

        public int MaTaiKhoan { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public int? MaQuyen { get; set; }

        public virtual Quyen MaQuyenNavigation { get; set; }
        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
