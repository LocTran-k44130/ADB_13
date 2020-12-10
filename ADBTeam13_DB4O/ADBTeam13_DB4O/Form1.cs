using ADBTeam13_DB4O.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADBTeam13_DB4O
{
    public partial class Form1 : Form
    {
        public Database employeeDB = new Database
        {
            DbFileName = "ADBTeam13_EmployeeManager",
        };

        public Database companyDB = new Database
        {
            DbFileName = "ADBTeam13_CompanyManager",
        };
        public Form1()
        {
            InitializeComponent();
            updateComboboxHomeBase();
        }

        public void updateComboboxHomeBase()
        {
            List<string> companyNames = companyDB.getListCompanyName();
            cbbHomebase.Items.Clear();

            companyNames.ForEach(item =>
            {
                cbbHomebase.Items.Add(item);
            });
        }

        private void btnThemCongTy_Click(object sender, EventArgs e)
        {
            var cty = new CongTy
            {
                MaCongTy = Guid.NewGuid(),
                MaSoThue = txbMaSoThue.Text,
                TenCongTy = txbTenCongTy.Text,
                SoNha = txbSoNha.Text,
                DuongPho = txbDuongPho.Text,
                QuanHuyen = txbQuanHuyen.Text,
            };

            companyDB.InsertObject<CongTy>(cty);
            updateComboboxHomeBase();
        }

        private void btnThemNhanVien_Click(object sender, EventArgs e)
        {

            if(txbHoTen.Text == "")
            {
                MessageBox.Show("Tên Cty không được để trống", "Lưu ý");
                return;
            }

            double a;
            double.TryParse(txbLuong.Text, out a);

            if (a == 0)
            {
                MessageBox.Show("Lương không hợp lệ !", "Lưu ý");
                return;
            }
            else
            {
                if (a < 300)
                {
                    MessageBox.Show("Lương phải từ 300USB trở lên !", "Lưu ý :");
                    return;
                }
            }


            companyDB.OpenDB();


            CongTy cty = companyDB.DB.Query(delegate (CongTy c)
            {
                if (c.TenCongTy == cbbHomebase.Text)
                {
                    return true;
                }
                return false;
            }).FirstOrDefault();

            var nv = new NhanVien
            {
                ID = Guid.NewGuid(),
                HoTen = txbHoTen.Text,
                Skill = txbSkill.Text,
                HomeBase = cty,
                QuanLy = null,
                Luong = Double.Parse(txbLuong.Text)  
            };

            employeeDB.InsertObject<NhanVien>(nv); 
            companyDB.CloseDB();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            employeeDB.OpenDB();
            IList<NhanVien> employees = employeeDB.DB.Query(delegate (NhanVien em)
            {
                return true;
            }).ToList();

            employeeDB.CloseDB();

            var ems = new List<EmployeeView>();
            foreach (NhanVien item in employees)
            {
                var tmp = new EmployeeView
                {
                     ID = item.ID,
                     HoTen = item.HoTen,
                     Skill = item.Skill,
                     HomeBase = item.HomeBase.TenCongTy,
                     Luong = item.Luong,
                };
               
                ems.Add(tmp);
            }  
            dataGridView1.DataSource = ems;
        }

        private void btnCapNhatGiamDoc_Click(object sender, EventArgs e)
        {
            var capnhat = new CapNhatGiamDoc();
            capnhat.Show();  
        }

        private void btnCau9_Click(object sender, EventArgs e)
        { 
            companyDB.OpenDB();
            employeeDB.OpenDB();

            IList<CongTy> companies = companyDB.DB.Query(delegate (CongTy com)
            {
                string companyName = com.TenCongTy;
                double countSalary = 0;
                int countEmployee = 0;

                employeeDB.DB.Query(delegate (NhanVien em)
                {
                    if (em.HomeBase.TenCongTy == companyName)
                    {
                        countSalary += em.Luong;
                        countEmployee++;
                        return true;
                    }
                    return false;
                });

                if (countEmployee > 2 && countSalary > 1000)
                {
                    return true; 
                    
                }

                return false;
            }).ToList();

            employeeDB.CloseDB();
            companyDB.CloseDB();

            dataGridView1.DataSource = companies;
        }
    }
}
