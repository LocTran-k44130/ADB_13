using ADBTeam13_DB4O.Models;
using Db4objects.Db4o;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBTeam13_DB4O
{
    public class Database
    {
        public string DbFileName { get; set; }
        // public static IObjectContainer DB => Db4oEmbedded.OpenFile(DbFileName);

        public IObjectContainer database = null;
        public IObjectContainer DB
        {
            get { return database; }
        }
        public void OpenDB()
        {
            try
            {
                database = Db4oEmbedded.OpenFile(DbFileName);
            }
            catch (Exception e) { };
            
        }
        public void CloseDB()
        {
            database.Close();
        }
        public void InsertObject<T>(T data)
        {
            OpenDB();
            database.Store(data);
            CloseDB();
        }

        public void DeleteObject<T>(T template)
        {
            OpenDB();
            IObjectSet result = database.QueryByExample(template);
            //get first
            T obj = (T)result[0];
            database.Delete(obj);
            CloseDB();
        }

        public List<string> getListCompanyName()
        {
            OpenDB();

            var companyNames = new List<string>();

            DB.Query(delegate (CongTy com)
            {
                companyNames.Add(com.TenCongTy);
                return true;
            });

            CloseDB();

            return companyNames;
        }

        public List<string> getListEmployeeName()
        {
            OpenDB();

            var employeeNames = new List<string>();

            DB.Query(delegate (NhanVien nv)
            {
                employeeNames.Add(nv.HoTen);
                return true;
            });

            CloseDB();

            return employeeNames;
        }
    }
}
