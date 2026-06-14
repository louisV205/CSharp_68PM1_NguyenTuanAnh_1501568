using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QLSINHVIEN
{
    public partial class UCQLLH : UserControl
    {
        databaseDataContext db = new databaseDataContext();
        int idLopDangChon = 0;
        public UCQLLH()
        {
            InitializeComponent();
            txt_id_lop.ReadOnly = true;
            loadlop();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                string malop = txt_ma_lop.Text.Trim();
                string tenlop = txt_ten_lop.Text.Trim();
                string ghichu = txt_ghichu_lop.Text.Trim();

                if (malop == "")
                {
                    MessageBox.Show("Vui lòng nhập mã lớp");
                    return;
                }

                if (tenlop == "")
                {
                    MessageBox.Show("Vui lòng nhập tên lớp");
                    return;
                }

                bool trungMaLop = db.lophocs.Any(l => l.malop == malop);

                if (trungMaLop)
                {
                    MessageBox.Show("Mã lớp đã tồn tại");
                    return;
                }

                lophoc lh = new lophoc();
                lh.malop = malop;
                lh.tenlop = tenlop;
                lh.ghichu = ghichu;

                db.lophocs.InsertOnSubmit(lh);
                db.SubmitChanges();

                loadlop();
                ClearFormLopHoc();

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
                if (idLopDangChon == 0)
                {
                    MessageBox.Show("Vui lòng chọn lớp học cần sửa");
                    return;
                }

                string malop = txt_ma_lop.Text.Trim();
                string tenlop = txt_ten_lop.Text.Trim();
                string ghichu = txt_ghichu_lop.Text.Trim();

                if (malop == "")
                {
                    MessageBox.Show("Vui lòng nhập mã lớp");
                    return;
                }

                if (tenlop == "")
                {
                    MessageBox.Show("Vui lòng nhập tên lớp");
                    return;
                }

                lophoc lh = db.lophocs.SingleOrDefault(l => l.id == idLopDangChon);

                if (lh == null)
                {
                    MessageBox.Show("Không tìm thấy lớp học cần sửa");
                    return;
                }

                bool trungMaLop = db.lophocs.Any(l => l.malop == malop && l.id != idLopDangChon);

                if (trungMaLop)
                {
                    MessageBox.Show("Mã lớp đã tồn tại");
                    return;
                }

                lh.malop = malop;
                lh.tenlop = tenlop;
                lh.ghichu = ghichu;

                db.SubmitChanges();
                loadlop();

                MessageBox.Show("Sửa thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (idLopDangChon == 0)
                {
                    MessageBox.Show("Vui lòng chọn lớp học cần xóa");
                    return;
                }

                lophoc lh = db.lophocs.SingleOrDefault(l => l.id == idLopDangChon);

                if (lh == null)
                {
                    MessageBox.Show("Không tìm thấy lớp học cần xóa");
                    return;
                }

                bool lopDangCoSinhVien = db.sinhviens.Any(s => s.malop == lh.malop);

                if (lopDangCoSinhVien)
                {
                    MessageBox.Show("Lớp học đang có sinh viên, không thể xóa");
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa lớp học này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    return;
                }

                db.lophocs.DeleteOnSubmit(lh);
                db.SubmitChanges();

                loadlop();
                ClearFormLopHoc();

                MessageBox.Show("Xóa thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            ClearFormLopHoc();
            loadlop();
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

            idLopDangChon = Convert.ToInt32(row.Cells["id"].Value);
            txt_id_lop.Text = row.Cells["id"].Value.ToString();
            txt_ma_lop.Text = row.Cells["malop"].Value.ToString();
            txt_ten_lop.Text = row.Cells["tenlop"].Value.ToString();
            txt_ghichu_lop.Text = row.Cells["ghichu"].Value == null ? "" : row.Cells["ghichu"].Value.ToString();
        }

        private void ClearFormLopHoc()
        {
            idLopDangChon = 0;
            txt_id_lop.Clear();
            txt_ma_lop.Clear();
            txt_ten_lop.Clear();
            txt_ghichu_lop.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {

        }

        private void button9_Click_1(object sender, EventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {

        }

        private void UCQLLH_Load(object sender, EventArgs e)
        {
            loadlop();
        }

        private void groupBox1_Enter_2(object sender, EventArgs e)
        {

        }

        private void label4_Click_2(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_2(object sender, EventArgs e)
        {

        }

        private void label5_Click_2(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged_2(object sender, EventArgs e)
        {

        }
        private void loadUC(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            this.Controls.Add(uc);
            uc.BringToFront();
        }
        public void button10_Click(object sender, EventArgs e)
        {
            if (idLopDangChon == 0)
            {
                MessageBox.Show("Vui lòng chọn lớp học cần xem danh sách sinh viên");
                return;
            }

            string malop = txt_ma_lop.Text.Trim();

            if (malop == "")
            {
                MessageBox.Show("Không tìm thấy mã lớp");
                return;
            }

            loadUC(new UCQLSV(malop));
        }

        private void textBox2_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void txt_class_id_TextChanged(object sender, EventArgs e)
        {

        }
        public void loadlop()
        {
            var ds = from l in db.lophocs
                     select new
                     {
                         l.id,
                         l.malop,
                         l.tenlop,
                         l.ghichu
                     };
            dataGridView1.DataSource = ds.ToList();
        }
    }

}
