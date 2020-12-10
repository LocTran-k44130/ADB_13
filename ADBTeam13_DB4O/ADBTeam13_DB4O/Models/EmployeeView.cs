using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBTeam13_DB4O.Models
{
    public class EmployeeView
    {
        public Guid ID { get; set; }
        public string HoTen { get; set; }
        public string Skill { get; set; }
        public string HomeBase { get; set; }
        public NhanVien QuanLy { get; set; }
        public double Luong { get; set; }
    }
}
