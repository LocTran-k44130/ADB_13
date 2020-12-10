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
    public partial class CapNhatGiamDoc : Form
    {
          

        public Database employeeDB = new Database
        {
            DbFileName = "ADBTeam13_EmployeeManager",
        };

        public Database companyDB = new Database
        {
            DbFileName = "ADBTeam13_CompanyManager",
        };

        public CapNhatGiamDoc()
        {
            InitializeComponent();
            setDataComboboxCty();
        }

        public void setDataComboboxCty()
        {
            List<string> congtys = companyDB.getListCompanyName();
            cbbCompanyName.Items.Clear();

            congtys.ForEach(cty =>
            { 
                cbbCompanyName.Items.Add(cty);
            });
        }


        // ddang lamf
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            companyDB.OpenDB();
            employeeDB.OpenDB();

            string tenCty = cbbCompanyName.Text;
            string giamDoc = cbbGiamDoc.Text;

            NhanVien giamdoc = employeeDB.DB.Query(delegate (NhanVien nv)
            {
                if (nv.HoTen == giamDoc)
                {
                    return true;
                }
                return false;
            }).FirstOrDefault();

            CongTy congty = companyDB.DB.Query(delegate (CongTy cty)
            {
                if (cty.TenCongTy == tenCty)
                {
                    return true;
                }
                return false;
            }).FirstOrDefault();

            

             

            companyDB.DB.Delete(congty);

            congty.GiamDoc = giamdoc;
            companyDB.InsertObject<CongTy>(congty);

            employeeDB.CloseDB();
            companyDB.CloseDB();

        }

        private void cbbCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {  
            var companyName = cbbCompanyName.Text;

            employeeDB.OpenDB();

            List<NhanVien> danhSachNV = employeeDB.DB.Query(delegate (NhanVien nv)
            {
                if (nv.HomeBase.TenCongTy == companyName)
                {
                    return true;
                }
                return false;
            }).ToList();

            cbbGiamDoc.Items.Clear();
            danhSachNV.ForEach(item =>
            {
                cbbGiamDoc.Items.Add(item.HoTen);
            });

            employeeDB.CloseDB();
        }
    }
}
