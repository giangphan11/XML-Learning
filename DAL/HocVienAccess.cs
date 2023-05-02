using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Xml;
using System.Data;

namespace DAL
{
    public class HocVienAccess
    {
        string path = @"D:\K2 - Nam3\XML\LamDeKT\DeKT\De2\HocVien.xml";
        XmlDocument doc = new XmlDocument();

        public List<HocVien> dsHocVien()
        {
            doc.Load(path);
            List<HocVien> dsHV = new List<HocVien>();
            XmlNodeList list = doc.SelectNodes("/DSHV/HocVien");
            foreach(XmlNode node in list)
            {
                HocVien hv = new HocVien();
                hv.Ma = node.SelectSingleNode("@ma").InnerText;
                hv.HoTen = node.SelectSingleNode("HoTen").InnerText;
                hv.NgaySinh = DateTime.Parse(node.SelectSingleNode("@NgaySinh").InnerText);
                hv.GioiTinh = node.SelectSingleNode("GioiTinh").InnerText;
                dsHV.Add(hv);
            }
            return dsHV;
        }
        private bool checkMa(string ma)
        {
            doc.Load(path);
            XmlNode node = doc.SelectSingleNode("/DSHV/HocVien[@ma='"+ma+"']");
            if (node != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool themHocVien(string ma, DateTime ngaySinh,string ten,string gt)
        {
            if (checkMa(ma))
            {
                return false;
            }
            else
            {
                doc.Load(path);
                XmlElement goc = doc.DocumentElement;
                XmlNode nodeHocVien = doc.CreateElement("HocVien");
                XmlAttribute mahv = doc.CreateAttribute("ma");
                mahv.InnerText = ma;
                nodeHocVien.Attributes.Append(mahv);

                XmlAttribute ns = doc.CreateAttribute("NgaySinh");
                ns.InnerText = ngaySinh.ToString("yyyy-MM-dd");
                nodeHocVien.Attributes.Append(ns);

                XmlElement hTen = doc.CreateElement("HoTen");
                hTen.InnerText = ten;
                nodeHocVien.AppendChild(hTen);

                XmlElement gTinh = doc.CreateElement("GioiTinh");
                gTinh.InnerText = gt;
                nodeHocVien.AppendChild(gTinh);
                goc.AppendChild(nodeHocVien);
                doc.Save(path);
                return true;
            }
        }
        public List<HocVien> dsHocVien(string gt)
        {
            doc.Load(path);
            List<HocVien> dsHV = new List<HocVien>();
            XmlNodeList list = doc.SelectNodes("/DSHV/HocVien[GioiTinh='"+gt+"']");
            foreach (XmlNode node in list)
            {
                HocVien hv = new HocVien();
                hv.Ma = node.SelectSingleNode("@ma").InnerText;
                hv.HoTen = node.SelectSingleNode("HoTen").InnerText;
                hv.NgaySinh = DateTime.Parse(node.SelectSingleNode("@NgaySinh").InnerText);
                hv.GioiTinh = node.SelectSingleNode("GioiTinh").InnerText;
                dsHV.Add(hv);
            }
            return dsHV;
        }
        public bool xoaHocVien(string ma)
        {
            if (checkMa(ma))
            {
                doc.Load(path);
                XmlElement goc = doc.DocumentElement;
                XmlNode node = doc.SelectSingleNode("/DSHV/HocVien[@ma='" + ma + "']");
                goc.RemoveChild(node);
                doc.Save(path);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
