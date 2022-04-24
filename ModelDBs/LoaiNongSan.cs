using System;
using System.Collections.Generic;

#nullable disable

namespace Uni_Shop.ModelDBs
{
    public partial class LoaiNongSan
    {
        public LoaiNongSan()
        {
            NongSans = new HashSet<NongSan>();
        }

        public int MaLoaiNongSan { get; set; }
        public string TenLoaiNongSan { get; set; }

        public virtual ICollection<NongSan> NongSans { get; set; }
    }
}
