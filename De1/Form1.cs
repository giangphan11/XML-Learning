using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace De1
{
    public partial class Form1 : Form
    {
        string path = @"D:\K2 - Nam3\XML\LamDeKT\DeKT\De1\KhachHang.xml";
        XmlDocument doc = new XmlDocument();
        public Form1()
        {
            InitializeComponent();
        }
        private void loadCombo()
        {
            doc.Load(path);
            cboChiNhanh.Items.Clear();
            XmlNodeList list = doc.SelectNodes("/danhsachkhachhang/khachhang");

            foreach(XmlNode node in list)
            {
                XmlNode nodeChiNhanh = node.SelectSingleNode("@chinhanh");
                cboChiNhanh.Items.Add(nodeChiNhanh.InnerText);
            }
        }
        private void loadData()
        {
            doc.Load(path);
            int row = 0;
            gvKhachHang.Rows.Clear();
            XmlNodeList list = doc.SelectNodes("/danhsachkhachhang/khachhang");
            foreach(XmlNode node in list)
            {
                gvKhachHang.Rows.Add();

                XmlNode nodeChiNhanh = node.SelectSingleNode("@chinhanh");
                gvKhachHang.Rows[row].Cells[0].Value = nodeChiNhanh.InnerText;

                XmlNode nodeMaKH = node.SelectSingleNode("@makh");
                gvKhachHang.Rows[row].Cells[1].Value = nodeMaKH.InnerText;

                XmlNode nodeHoTen = node.SelectSingleNode("hoten");
                gvKhachHang.Rows[row].Cells[2].Value = nodeHoTen.InnerText;

                XmlNode nodeDiaChi = node.SelectSingleNode("diachi");
                gvKhachHang.Rows[row].Cells[3].Value = nodeDiaChi.InnerText;

                XmlNode nodesdt = node.SelectSingleNode("sdt");
                gvKhachHang.Rows[row].Cells[4].Value = nodesdt.InnerText;
                row++;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            loadCombo();
            loadData();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int dongChon = e.RowIndex;
            if (dongChon != -1 && dongChon < gvKhachHang.Rows.Count - 1)
            {
                DataGridViewRow row = gvKhachHang.Rows[dongChon];
                txtMa.Text = row.Cells[1].Value.ToString();
                txtTen.Text = row.Cells[2].Value.ToString();
                txtDiaChi.Text = row.Cells[3].Value.ToString();
                txtSDT.Text = row.Cells[4].Value.ToString();
                cboChiNhanh.SelectedItem = row.Cells[0].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            doc.Load(path);
            XmlElement goc = doc.DocumentElement;
            XmlNode nodeKhachHang = doc.CreateElement("khachhang");

            XmlAttribute ma = doc.CreateAttribute("makh");
            ma.InnerText = txtMa.Text;
            nodeKhachHang.Attributes.Append(ma);

            XmlAttribute chiNhanh = doc.CreateAttribute("chinhanh");
            chiNhanh.InnerText = cboChiNhanh.SelectedItem.ToString();
            nodeKhachHang.Attributes.Append(chiNhanh);

            XmlNode ten = doc.CreateElement("hoten");
            ten.InnerText = txtTen.Text;
            nodeKhachHang.AppendChild(ten);

            XmlNode diaChi = doc.CreateElement("diachi");
            diaChi.InnerText = txtDiaChi.Text;
            nodeKhachHang.AppendChild(diaChi);

            XmlNode sdt = doc.CreateElement("sdt");
            sdt.InnerText = txtSDT.Text;
            nodeKhachHang.AppendChild(sdt);

            goc.AppendChild(nodeKhachHang);

            doc.Save(path);
            loadData();
        }
        private bool checkMa(string ma)
        {
            doc.Load(path);
            XmlNode nodeCu = doc.SelectSingleNode("/danhsachkhachhang/khachhang[@makh='" + ma + "']");
            if (nodeCu != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            string ma1 = txtMa.Text;
            if (checkMa(ma1))
            {
                doc.Load(path);
                XmlElement goc = doc.DocumentElement;
                XmlNode nodeCu = doc.SelectSingleNode("/danhsachkhachhang/khachhang[@makh='" + ma1 + "']");

                XmlNode nodeMoi = doc.CreateElement("khachhang");
                XmlAttribute ma = doc.CreateAttribute("makh");
                ma.InnerText = txtMa.Text;
                nodeMoi.Attributes.Append(ma);

                XmlAttribute chinhanh = doc.CreateAttribute("chinhanh");
                chinhanh.InnerText = cboChiNhanh.SelectedItem.ToString();
                nodeMoi.Attributes.Append(chinhanh);

                XmlNode ten = doc.CreateElement("hoten");
                ten.InnerText = txtTen.Text;
                nodeMoi.AppendChild(ten);

                XmlNode diaChi = doc.CreateElement("diachi");
                diaChi.InnerText = txtDiaChi.Text;
                nodeMoi.AppendChild(diaChi);

                XmlNode sdt = doc.CreateElement("sdt");
                sdt.InnerText = txtSDT.Text;
                nodeMoi.AppendChild(sdt);

                goc.ReplaceChild(nodeMoi, nodeCu);
                doc.Save(path);
                loadData();
            }
            else
            {
                MessageBox.Show("Không tòn tại mã !");
                return;
            }
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMa.Text;
            if (checkMa(ma))
            {
                doc.Load(path);
                XmlElement goc = doc.DocumentElement;
                XmlNode nodeXoa = doc.SelectSingleNode("/danhsachkhachhang/khachhang[@makh='" + ma + "']");
                goc.RemoveChild(nodeXoa);
                doc.Save(path);
                loadData();
            }
            else
            {
                MessageBox.Show("Không tòn tại mã !");
                return;
            }
        }
    }
}
