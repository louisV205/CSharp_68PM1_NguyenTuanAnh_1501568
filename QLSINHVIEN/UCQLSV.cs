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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadLopHoc()
        {
            box_lophoc.DataSource = db.lophocs.ToList();

            box_lophoc.DisplayMember = "tenlop";
            box_lophoc.ValueMember = "malop";
        }
    }
}
