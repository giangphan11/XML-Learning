using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BLL
{
    public class HocVienBLL
    {
        HocVienAccess vienAccess = new HocVienAccess();
        public List<HocVien> layDsHocVien()
        {
            return vienAccess.dsHocVien();
        }
        public List<HocVien> layDsHocVien(string gt)
        {
            return vienAccess.dsHocVien(gt);
        }
        public bool themHocVien(string ma, DateTime ns, string ten, string gt)
        {
            return vienAccess.themHocVien(ma, ns, ten, gt);
        }

        public bool xoaHocVien(string ma)
        {
            return vienAccess.xoaHocVien(ma);
        }
    }
}
