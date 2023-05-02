using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BLL;
namespace De2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void loadData()
        {
            HocVienBLL hocVienBLL = new HocVienBLL();
            gvHocvien.DataSource = hocVienBLL.layDsHocVien().ToList();
            gvHocvien.Columns[1].Width = 200;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
            //dateNgaySinh.Value.
        }

        private void gvHocvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int dongChon = e.RowIndex;
            if (dongChon != -1)
            {
                DataGridViewRow row = gvHocvien.Rows[dongChon];
                dateNgaySinh.Value =DateTime.Parse( row.Cells[2].Value.ToString());
                txtMa.Text = row.Cells[0].Value.ToString();
                txtTen.Text = row.Cells[1].Value.ToString();
                txtGioiTInh.Text = row.Cells[3].Value.ToString();
            }
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            txtGioiTInh.Text = "";
            txtMa.Text = "";
            txtTen.Text = "";
            txtMa.Focus();
            dateNgaySinh.Value = new DateTime(1998, 6, 20);
            //MessageBox.Show(dateNgaySinh.Value.ToString("dd/MM/yyyy"));
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            HocVienBLL hocVienBLL = new HocVienBLL();
            if (hocVienBLL.themHocVien(txtMa.Text, dateNgaySinh.Value, txtTen.Text, txtGioiTInh.Text)){
                MessageBox.Show("Thêm thành công !");
                loadData();
            }
            else
            {
                MessageBox.Show("Thêm thất bại !");
                return;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMa.Text;
            DialogResult dr=MessageBox.Show("Bạn coá chắc xoá mã "+ma+" không?","Xác nhận xoá",MessageBoxButtons.YesNoCancel);
            if (dr == DialogResult.Yes)
            {
                HocVienBLL hocVienBLL = new HocVienBLL();
                if (hocVienBLL.xoaHocVien(ma))
                {
                    MessageBox.Show("Xoá thành công !");
                    loadData();
                }
                else
                {
                    MessageBox.Show("Xoá thất bại !");
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string gt = txtGioiTInh.Text;
            HocVienBLL hocVienBLL = new HocVienBLL();
            gvHocvien.DataSource = hocVienBLL.layDsHocVien(gt).ToList();
            gvHocvien.Columns[1].Width = 200;
        }
    }
}
