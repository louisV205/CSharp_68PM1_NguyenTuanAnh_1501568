using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLSINHVIEN
{
    public partial class UCQLSV : UserControl
    {
        databaseDataContext db = new databaseDataContext();
        int idSinhVienDangChon = 0;
        public UCQLSV()
        {
            InitializeComponent();
            LoadLopHoc();
        }

        private void UCQLSV_Load(object sender, EventArgs e)
        {
            List<sinhvien> dsSinhVien = db.sinhviens.ToList();
            dataGridView1.DataSource = dsSinhVien;
        }
        private void LoadSinhVien()
        {
            var ds = from sv in db.sinhviens
                     select new
                     {
                         sv.id,
                         sv.masv,
                         sv.hoten,
                         sv.ngaysinh,
                         sv.gioitinh,
                         sv.malop
                     };

            dataGridView1.DataSource = ds.ToList();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                sinhvien sv = new sinhvien();

                sv.masv = txt_mssv.Text.Trim();
                sv.hoten = txt_name.Text.Trim();
                sv.gioitinh = box_gioitinh.Text;
                sv.ngaysinh = date_ngaysinh.Value;

                sv.malop = box_lophoc.SelectedValue.ToString();

                db.sinhviens.InsertOnSubmit(sv);
                db.SubmitChanges();

                MessageBox.Show("Thêm thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }
        private void btn_edit_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSinhVienDangChon == 0)
                {
                    MessageBox.Show("Vui lòng chọn sinh viên cần sửa");
                    return;
                }

                string masv = txt_mssv.Text.Trim();
                string hoten = txt_name.Text.Trim();
                string gioitinh = box_gioitinh.Text;

                if (masv == "")
                {
                    MessageBox.Show("Vui lòng nhập mã sinh viên");
                    return;
                }

                if (hoten == "")
                {
                    MessageBox.Show("Vui lòng nhập họ tên");
                    return;
                }

                if (gioitinh == "")
                {
                    MessageBox.Show("Vui lòng chọn giới tính");
                    return;
                }

                if (box_lophoc.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn lớp học");
                    return;
                }

                sinhvien sv = db.sinhviens.SingleOrDefault(s => s.id == idSinhVienDangChon);

                if (sv == null)
                {
                    MessageBox.Show("Không tìm thấy sinh viên cần sửa");
                    return;
                }

                bool trungMaSinhVien = db.sinhviens.Any(s => s.masv == masv && s.id != idSinhVienDangChon);

                if (trungMaSinhVien)
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại");
                    return;
                }

                sv.masv = masv;
                sv.hoten = hoten;
                sv.gioitinh = gioitinh;
                sv.ngaysinh = date_ngaysinh.Value;
                sv.malop = box_lophoc.SelectedValue.ToString();

                db.SubmitChanges();
                LoadSinhVien();

                MessageBox.Show("Sửa thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            if (row.IsNewRow || row.Cells["id"].Value == null)
            {
                return;
            }

            idSinhVienDangChon = Convert.ToInt32(row.Cells["id"].Value);
            txt_mssv.Text = row.Cells["masv"].Value.ToString();
            txt_name.Text = row.Cells["hoten"].Value.ToString();
            date_ngaysinh.Value = Convert.ToDateTime(row.Cells["ngaysinh"].Value);
            box_gioitinh.Text = row.Cells["gioitinh"].Value.ToString();
            box_lophoc.SelectedValue = row.Cells["malop"].Value.ToString();
        }
        private void LoadLopHoc()
        {
            box_lophoc.DataSource = db.lophocs.ToList();

            box_lophoc.DisplayMember = "tenlop";
            box_lophoc.ValueMember = "malop";
        }
    }
}
